using System;
using System.Web.Mvc;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;

namespace Latest_Staff_Portal.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult ChangePassword()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public JsonResult ChangePassord(string newpass)
        {
            try
            {
                if (Session["UserID"] == null)
                    return Json(new { message = "/Login/Login", success = false, redirect = true },
                        JsonRequestBehavior.AllowGet);

                var StaffNo = Session["UserID"].ToString();
                var UserName = Session["Username"].ToString();
                var s = new string[2];
                if (StaffNo.Contains("\\"))
                {
                    s = StaffNo.Split('\\');
                    StaffNo = s[1];
                }

                var ok = Credentials.ResetPassword(StaffNo, newpass);
               /* Credentials.ObjNav.PasswordChanged(StaffNo, false);*/
                // bool ok = Credentials.ObjNav.ResetPassword(UserName, newpass);

                if (ok == "CHANGED")
                    return Json(new { message = "Password Changed Successfully", success = true },
                        JsonRequestBehavior.AllowGet);
                return Json(new { message = ok, success = false, redirect = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false, redirect = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult RequestOtp(string newpass)
        {
            try
            {
                return PartialView("~/Views/Login/RequestOtp.cshtml");
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public JsonResult GeneratePortalOTPCode()
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var empNo = Session["EmpNo"].ToString();
                /*Credentials.ObjNav.GeneratePortalOTPCode(empNo);*/
                //Credentials.ObjNav.ReturnOTPExpiration(Session["EmpNo"].ToString());


                var Redirect = "/Settings/InputOtp";

                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GenerateEmailOTPCode()
        {
            try
            {
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string empNo = Session["EmpNo"].ToString();
                Credentials.ObjNav.GeneratePortalEmailOTPCode(empNo);
                string Redirect = "/Settings/InputOtp";
                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult InputOtp(string newpass)
        {
            try
            {
                return PartialView("~/Views/Login/InputOtp.cshtml");
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
    }
}