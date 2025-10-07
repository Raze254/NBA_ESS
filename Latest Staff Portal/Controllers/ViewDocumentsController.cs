using System;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.ViewModel;

namespace Latest_Staff_Portal.Controllers
{
    [CustomeAuthentication]
    [CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]
    public class ViewDocumentsController : Controller
    {
        // GET: ViewDocuments
        public ActionResult ViewDocuments()
        {
            return View();
        }
        public ActionResult SharepointIntegration()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
    }
}