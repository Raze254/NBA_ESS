using System;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using Latest_Staff_Portal.NAVWS;
using Latest_Staff_Portal.NAVWS2;

namespace Latest_Staff_Portal.Models
{
    public class Credentials
    {
        private static DirectorySearcher dirSearch = null;
        public static string fileSourcePath = @"D:\PORTALS\CHS\Documents\";
        public static string fileDestinationPath = @"D:\PORTALS\CHS\Documents\";
        public static string fileUploadsPath = @"D:\PORTALS\CHS\Documents\";

        public static HrPortal ObjNav
        {
            get
            {
                var ws = new HrPortal();

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    ws.Credentials = credentials;
                    ws.PreAuthenticate = true;
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }


        public static HRportal2 ObjNav2
        {
            get
            {
                var ws = new HRportal2();

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    ws.Credentials = credentials;
                    ws.PreAuthenticate = true;
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }

        public static HttpWebResponse GetOdataData(string page)
        {
            HttpWebResponse httpResponse = null;
            var httpWebRequest =
                (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["ODATA_URI"] + page);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);

            httpWebRequest.Timeout = 1000000;
            httpWebRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            return httpResponse;
        }

        public static string ResetPassword(string username, string newpass)
        {
            var rval = "";
            try
            {
                var UName = "";
                if (username.Contains(@"\\"))
                    UName = username.Replace(@"\\", "").Trim();
                else if (username.Contains(@"\"))
                    UName = username.Replace(@"\", "").Trim();
                else
                    UName = username.Trim();
                var AdminAccountName = WebConfigurationManager.AppSettings["AD_USER"];
                var AdminPassword = WebConfigurationManager.AppSettings["ADW_PWD"];
                var Domain = WebConfigurationManager.AppSettings["DOMAIN"];

                using (var pContext = new PrincipalContext(ContextType.Domain,
                           ConfigurationManager.AppSettings["AD_Server"], AdminAccountName, AdminPassword))
                {
                    var up = UserPrincipal.FindByIdentity(pContext, username);
                    if (up != null)
                    {
                        up.SetPassword(newpass);
                        up.Save();
                        rval = "CHANGED";
                    }
                }
            }
            catch (Exception ex)
            {
                rval = ex.InnerException.Message;
            }

            return rval;
        }

        public static bool UploadProfilePic(string StaffNo, string base64String, string filePath, string fileName)
        {
            var Uploaded = false;
            try
            {
                File.WriteAllBytes(filePath, Convert.FromBase64String(base64String));
               /* if (CommonClass.IfFileExists(filePath))*/
                    /*ObjNav.ImportStaffProfilePicture(StaffNo, base64String, fileName);*/
                Uploaded = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return Uploaded;
        }

        public static string UploadEDMSDocumentAttachment(string DocNo, string base64String, string filePath,
            int TableID)
        {
            var Uploaded = "";
            try
            {
                File.WriteAllBytes(filePath, Convert.FromBase64String(base64String));

               /* if (CommonClass.IfFileExists(filePath))*/
                    /*ObjNav.UploadAttachedDocument(DocNo, filePath, base64String, TableID);*/

                Uploaded = "SUCCESS";
            }
            catch (Exception ex)
            {
                Uploaded = ex.Message;
            }

            return Uploaded;
        }

        public static string GetDocumentAttachmet(int TblID, string DocNo, int Id)
        {
            var PicString = "";
            try
            {
               /* PicString = ObjNav.GetDocumentAttachment(TblID, DocNo, Id);*/
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return PicString;
        }

        public static void DownloadAttachment(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        internal static async Task GetOdataDataAsync(string pageUrl)
        {
            throw new NotImplementedException();
        }
        public static string SaveBase64DocumentAttachment(string base64String, string filePath)
        {
            string Uploaded = "";
            try
            {
                File.WriteAllBytes(filePath, Convert.FromBase64String(base64String));

                Uploaded = "SUCCESS";
            }
            catch (Exception ex)
            {
                Uploaded = ex.Message;
            }
            return Uploaded;
        }
    }
}