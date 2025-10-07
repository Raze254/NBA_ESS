using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using Latest_Staff_Portal.CustomSecurity;
//using System.Web.Optimization;

namespace Latest_Staff_Portal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            JsonValueProviderFactory jsonValueProviderFactory = null;

            foreach (var factory in ValueProviderFactories.Factories)
                if (factory is JsonValueProviderFactory)
                    jsonValueProviderFactory = factory as JsonValueProviderFactory;

            //remove the default JsonVAlueProviderFactory
            if (jsonValueProviderFactory != null) ValueProviderFactories.Factories.Remove(jsonValueProviderFactory);

            //add the custom one
            ValueProviderFactories.Factories.Add(new CustomJsonValueProviderFactory());
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                if (authCookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var udata = ticket.UserData;
                    var userData = udata.Split('|');

                    var myUser = new CustomPrincipal(userData[0]);
                    myUser.UserID = userData[1];
                    myUser.Email = userData[2];
                    myUser.RoleName = userData[3];

                    HttpContext.Current.User = myUser;
                }
            }
            catch (CryptographicException cex)
            {
                FormsAuthentication.SignOut();
            }
        }
    }

    public sealed class CustomJsonValueProviderFactory : ValueProviderFactory
    {
        private static void AddToBackingStore(Dictionary<string, object> backingStore, string prefix, object value)
        {
            var d = value as IDictionary<string, object>;
            if (d != null)
            {
                foreach (var entry in d)
                    AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
                return;
            }

            var l = value as IList;
            if (l != null)
            {
                for (var i = 0; i < l.Count; i++) AddToBackingStore(backingStore, MakeArrayKey(prefix, i), l[i]);
                return;
            }

            // primitive
            backingStore[prefix] = value;
        }

        private static object GetDeserializedObject(ControllerContext controllerContext)
        {
            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json",
                    StringComparison.OrdinalIgnoreCase))
                // not JSON request
                return null;

            var reader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
            var bodyText = reader.ReadToEnd();
            if (string.IsNullOrEmpty(bodyText))
                // no JSON data
                return null;
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength =
                int.MaxValue; //increase MaxJsonLength.  This could be read in from the web.config if you prefer
            var jsonData = serializer.DeserializeObject(bodyText);
            return jsonData;
        }

        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null) throw new ArgumentNullException("controllerContext");

            var jsonData = GetDeserializedObject(controllerContext);
            if (jsonData == null) return null;

            var backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            AddToBackingStore(backingStore, string.Empty, jsonData);
            return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
        }

        private static string MakeArrayKey(string prefix, int index)
        {
            return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
        }

        private static string MakePropertyKey(string prefix, string propertyName)
        {
            return string.IsNullOrEmpty(prefix) ? propertyName : prefix + "." + propertyName;
        }
    }
}