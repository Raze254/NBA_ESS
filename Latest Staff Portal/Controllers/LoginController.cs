using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            Session.Remove("Username");
            Session.Remove("StaffDetails");
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            var user = new Authedication();
            return View(user);
        }

        [HttpPost]
        public JsonResult LoginUser(Authedication userlogin)
        {
            var msg = "";
            var success = false;
            var UserName = userlogin.UserName.ToUpper();
            var passwrd = userlogin.Password;
            var userID = "";
            var staffNo = "";
            var Redirect = "";
            try
            {
                //using var pc = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["AD_Server"]);
                //var isValid = pc.ValidateCredentials(UserName, passwrd);
               var isValid = true;
                if (isValid)
                {
                    if (UserName.Contains("\\"))
                        userID = UserName;
                    else
                        userID = ConfigurationManager.AppSettings["DOMAIN"] + @"\" + UserName;
                    Session["LoginId"] = userID;
                    Session["LoginUseName"] = UserName;
                    if (ConfigurationManager.AppSettings["IS_PROD"] == "NON-PROD")
                    {
                        Redirect = "/Dashboard/Dashboard";
                        msg = Redirect;
                        try
                        {
                            Redirect = "/Dashboard/Dashboard";
                            var page = "UserSetup?$filter=User_ID eq '" + userID + "'&format=json";
                            var httpResponse = Credentials.GetOdataData(page);
                            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                            var result = streamReader.ReadToEnd();
                            var details = JObject.Parse(result);
                            if (details["value"].Any())
                            {
                                foreach (var jToken in details["value"])
                                {
                                    var config = (JObject)jToken;
                                    if ((string)config["Employee_No"] != "")
                                    {
                                        userID = (string)config["User_ID"];
                                        staffNo = (string)config["Employee_No"];

                                        var Role = "";
                                        Session["Username"] = (string)config["Employee_No"];
                                        Session["UserID"] = (string)config["User_ID"];
                                        var IDno = (string)config["IDNumber"];
                                        var Email = (string)config["Email"];
                                        var PhoneNo = (string)config["PhoneNo"];
                                        EmployeeData(staffNo);
                                        Role = "ALLUSERS";
                                        SetUserAuthedication(UserName, Email, Role);
                                        msg = Redirect;
                                        success = true;
                                    }
                                    else
                                    {
                                        msg =
                                            "No Employee Number assigned to the applied username. Contact HR / ICT";
                                        success = false;
                                    }
                                }
                            }
                            else
                            {
                                msg = "No Employee Number assigned to the applied username. Contact HR / ICT";
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                            success = false;
                        }
                    }


                    //request for OTP
                    else
                    {
                        Redirect = "/Settings/RequestOtp";
                        msg = Redirect;
                        success = true;
                        var page = "UserSetup?$filter=User_ID eq '" + userID + "'&format=json";

                        var httpResponse = Credentials.GetOdataData(page);
                        using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                        var result = streamReader.ReadToEnd();

                        var details = JObject.Parse(result);

                        if (details["value"].Any())
                        {
                            foreach (var jToken in details["value"])
                            {
                                var config = (JObject)jToken;
                                if ((string)config["Employee_No"] != "")
                                {
                                    if (ConfigurationManager.AppSettings["IS_PROD"] == "NON-PROD")
                                    {
                                        Session["Username"] = (string)config["Employee_No"];
                                        Session["UserID"] = (string)config["User_ID"];
                                    }

                                    userID = (string)config["User_ID"];
                                    staffNo = (string)config["Employee_No"];
                                    var Role = "";
                                    var IDno = (string)config["IDNumber"];
                                    var Email = "";
                                    var PhoneNo = "";

                                    var pageEmp = "EmployeeList?$filter=No eq '" + staffNo + "'&$format=json";

                                    var httpResponseEmp = Credentials.GetOdataData(pageEmp);
                                    using (var streamReaderEmp =
                                           new StreamReader(httpResponseEmp.GetResponseStream()))
                                    {
                                        var resultEmp = streamReaderEmp.ReadToEnd();

                                        var det = JObject.Parse(resultEmp);

                                        foreach (var data in det["value"])
                                        {
                                            Email = (string)data["E_Mail"];
                                            PhoneNo = (string)data["Phone_No"];
                                        }
                                    }

                                    Session["Phone"] = PhoneNo;
                                    Session["Email"] = Email;
                                    Session["EmpNo"] = staffNo;
                                    msg = Redirect;
                                    success = true;
                                }
                                else
                                {
                                    msg =
                                        "No Employee Number assigned to the applied username. Contact HR / ICT";
                                    success = false;
                                }
                            }
                        }
                        else
                        {
                            msg = "No Employee Number assigned to the applied username. Contact HR / ICT";
                            success = false;
                        }
                    }

                }
                else
                {
                    msg = "Invalid username or password!";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                success = false;
            }

            return Json(new { message = msg, success }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmOtp(string otp)
        {
            try
            {
                var msg = "";
                var success = false;
                var staffNo = "";
                var Redirect = "";

                var employee = Session["EmployeeData"] as EmployeeView;
                var upperOtp = otp.ToUpper();
                bool confirmOtp = true;
                confirmOtp = Credentials.ObjNav.ReturnPortalOTPCode(Session["EmpNo"].ToString(), upperOtp);
                if (confirmOtp)
                {
                    try
                    {
                        var userID = Session["LoginId"].ToString();
                        var UserName = Session["LoginUseName"].ToString();
                        Redirect = "/Dashboard/Dashboard";

                        var page = $"UserSetup?$filter=User_ID eq '{userID}'&format=json";


                        var httpResponse = Credentials.GetOdataData(page);
                        using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                        var result = streamReader.ReadToEnd();

                        var details = JObject.Parse(result);

                        if (details["value"].Any())
                        {
                            foreach (var jToken in details["value"])
                            {
                                var config = (JObject)jToken;
                                if ((string)config["Employee_No"] != "")
                                {
                                    userID = (string)config["User_ID"];
                                    staffNo = (string)config["Employee_No"];

                                    var Role = "";
                                    Session["Username"] = (string)config["Employee_No"];
                                    Session["UserID"] = (string)config["User_ID"];
                                    var IDno = (string)config["IDNumber"];
                                    var Email = (string)config["EMail"];
                                    var PhoneNo = (string)config["CellPhoneNumber"];

                                    /*ESSRoleSetup(userID, UserName, Email);*/
                                    EmployeeData(staffNo);
                                    /* if (Credentials.ObjNav.CheckPasswordChanged((string)config["Employee_No"]))
                                         Redirect = "/Settings/ChangePassword";
 */

                                    msg = Redirect;
                                    success = true;
                                }
                                else
                                {
                                    msg = "No Employee Number assigned to the applied username. Contact HR / ICT";
                                    success = false;
                                }
                            }
                        }
                        else
                        {
                            msg = "No Employee Number assigned to the applied username. Contact HR / ICT";
                            success = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        success = false;
                    }

                    return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
                }

                msg = "Incorrect OTP. Please try again!!!";
                return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        private EmployeeView EmployeeData(string staffNo)
        {
            try
            {
                var empView = new EmployeeView();
                var page = $"EmployeeList?$filter=No eq '{staffNo}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        empView.No = (string)config["No"];
                        empView.Title = (string)config["Title"];
                        empView.FirstName = (string)config["First_Name"];
                        empView.LastName = (string)config["Last_Name"];
                        empView.MiddleName = (string)config["Middle_Name"];
                        empView.Record_Type = (string)config["Record_Type"];
                        empView.Current_Position_ID = (string)config["Current_Position_ID"];
                        empView.Job_Title2 = (string)config["Job_Title2"];
                        empView.Search_Name = (string)config["Search_Name"];
                        empView.County_of_Origin = (string)config["County_of_Origin"];
                        empView.County_of_Origin_Name = (string)config["County_of_Origin_Name"];
                        empView.County_of_Residence = (string)config["County_of_Residence"];
                        empView.County_of_Residence_Name = (string)config["County_of_Residence_Name"];
                        empView.Gender = (string)config["Gender"];
                        empView.Marital_Status = (string)config["Marital_Status"];
                        empView.Religion = (string)config["Religion"];
                        empView.Disabled = (bool?)config["Disabled"] ?? false;
                        empView.Disability_No = (string)config["Disability_No"];
                        empView.Disability_Cert_Expiry = (string)config["Disability_Cert_Expiry"];
                        empView.Insurance_Certificate = (bool?)config["Insurance_Certificate"] ?? false;
                        empView.Ethnic_Origin = (string)config["Ethnic_Origin"];
                        empView.Last_Date_Modified = (string)config["Last_Date_Modified"];
                        empView.HOD = (bool?)config["HOD"] ?? false;
                        empView.Is_Part_Of_Disciplinary_Team = (bool?)config["Is_Part_Of_Disciplinary_Team"] ?? false;
                        empView.Regional_Manager = (bool?)config["Regional_Manager"] ?? false;
                        empView.ICT_Help_Desk_Admin = (bool?)config["ICT_Help_Desk_Admin"] ?? false;
                        empView.Is_Supply_Chain_Officer = (bool?)config["Is_Supply_Chain_Officer"] ?? false;
                        empView.CEO = (bool?)config["CEO"] ?? false;
                        empView.UserID = (string)config["User_ID"];
                        empView.Supervisor = (string)config["Supervisor"];
                        empView.Reliver_Code = (string)config["Reliver_Code"];
                        empView.Salary_Scale = (string)config["Salary_Scale"];
                        empView.Present = (string)config["Present"];
                        empView.Employee_Posting_Group1 = (string)config["Employee_Posting_Group1"];
                        empView.Increment_Date = (string)config["Increment_Date"];
                        empView.Employee_Category_Type = (string)config["Employee_Category_Type"];
                        empView.Incremental_Month = (string)config["Incremental_Month"];
                        empView.Last_Increment_Date = (string)config["Last_Increment_Date"];

                        empView.GlobalDimension1Code = (string)config["Global_Dimension_1_Code"];
                        empView.Department_Name = (string)config["Department_Name"];

                        empView.GlobalDimension2Code = (string)config["Global_Dimension_2_Code"];
                        empView.Administrative_Unit_Name = (string)config["Administrative_Unit_Name"];

                        empView.Directorate_Code = (string)config["Directorate_Code"];
                        empView.Implementing_Unit_Name = (string)config["Implementing_Unit_Name"];

                        empView.DutyStation = (string)config["Duty_Station"];
                        empView.Station_Name = (string)config["Station_Name"];
                        empView.Account_Type = (string)config["Account_Type"];
                        empView.Division = (string)config["Division"];
                        empView.Employee_Job_Type = (string)config["Employee_Job_Type"];
                        empView.Responsibility_Center = (string)config["Responsibility_Center"];
                        empView.Job_Cadre = (string)config["Job_Cadre"];
                        empView.Job_Cadre_Name = (string)config["Job_Cadre_Name"];
                        empView.Address = (string)config["Address"];
                        empView.Address_2 = (string)config["Address_2"];
                        empView.Post_Code = (string)config["Post_Code"];
                        empView.City = (string)config["City"];
                        empView.Country_Region_Code = (string)config["Country_Region_Code"];
                        empView.Citizenship_Type = (string)config["Citizenship_Type"];
                        empView.Employee_Type = (string)config["Employee_Type"];
                        empView.Phone_No = (string)config["Phone_No"];
                        empView.Extension = (string)config["Extension"];
                        empView.Mobile_Phone_No = (string)config["Mobile_Phone_No"];
                        empView.Pager = (string)config["Pager"];
                        empView.Address2 = (string)config["Address2"];
                        empView.Phone_No_2 = (string)config["Phone_No_2"];
                        empView.E_Mail = (string)config["E_Mail"];
                        empView.Company_E_Mail = (string)config["Company_E_Mail"];
                        empView.Alt_Address_Code = (string)config["Alt_Address_Code"];
                        empView.Alt_Address_Start_Date = (string)config["Alt_Address_Start_Date"];
                        empView.Alt_Address_End_Date = (string)config["Alt_Address_End_Date"];
                        empView.Work_Phone_Number = (string)config["Work_Phone_Number"];
                        empView.Ext = (string)config["Ext"];
                        empView.Date_Of_Birth = (string)config["Date_Of_Birth"];
                        empView.Age = (string)config["Age"];
                        empView.Employment_Date = (string)config["Employment_Date"];
                        empView.End_Of_Probation_Date = (string)config["End_Of_Probation_Date"];
                        empView.Inducted = (bool?)config["Inducted"] ?? false;
                        empView.Pension_Scheme_Join = (string)config["Pension_Scheme_Join"];
                        empView.TimeinPension = (string)config["TimeinPension"];
                        empView.Retirement_Date = (string)config["Retirement_Date"];
                        empView.Full_Part_Time = (string)config["Full_Part_Time"];
                        empView.Contract_Start_Date = (string)config["Contract_Start_Date"];
                        empView.Contract_End_Date = (string)config["Contract_End_Date"];
                        empView.Re_Employment_Date = (string)config["Re_Employment_Date"];
                        empView.Notice_Period = (string)config["Notice_Period"];
                        empView.Status = (string)config["Status"];
                        empView.Employee_Status_2 = (string)config["Employee_Status_2"];
                        empView.Inactive_Date = (string)config["Inactive_Date"];
                        empView.Cause_of_Inactivity_Code = (string)config["Cause_of_Inactivity_Code"];
                        empView.Emplymt_Contract_Code = (string)config["Emplymt_Contract_Code"];
                        empView.Resource_No = (string)config["Resource_No"];
                        empView.Due_for_Retirement = (bool?)config["Due_for_Retirement"] ?? false;
                        empView.Salespers_Purch_Code = (string)config["Salespers_Purch_Code"];
                        empView.Disciplinary_status = (string)config["Disciplinary_status"];
                        empView.Reason_for_termination = (string)config["Reason_for_termination"];
                        empView.Termination_Date = (string)config["Termination_Date"];
                        empView.Date_Of_Leaving = (string)config["Date_Of_Leaving"];
                        empView.Exit_Interview_Conducted = (string)config["Exit_Interview_Conducted"];
                        empView.Exit_Interview_Date = (string)config["Exit_Interview_Date"];
                        empView.Exit_Interview_Done_by = (string)config["Exit_Interview_Done_by"];
                        empView.Bonding_Amount = (int?)config["Bonding_Amount"] ?? 0;
                        empView.Exit_Status = (string)config["Exit_Status"];
                        empView.Allow_Re_Employment_In_Future = (bool?)config["Allow_Re_Employment_In_Future"] ?? false;
                        empView.Pays_tax_x003F_ = (bool?)config["Pays_tax_x003F_"] ?? false;
                        empView.Pay_Wages = (bool?)config["Pay_Wages"] ?? false;
                        empView.Pay_Mode = (string)config["Pay_Mode"];
                        empView.P_I_N = (string)config["P_I_N"];
                        empView.N_H_I_F_No = (string)config["N_H_I_F_No"];
                        empView.Social_Security_No = (string)config["Social_Security_No"];
                        empView.ID_Number = (string)config["ID_Number"];
                        empView.Employee_Posting_Group = (string)config["Employee_Posting_Group"];
                        empView.Posting_Group = (string)config["Posting_Group"];
                        empView.Claim_Limit = (int?)config["Claim_Limit"] ?? 0;
                        empView.BankAccountNumber = (string)config["Bank_Account_Number"];
                        empView.Employee_x0027_s_Bank = (string)config["Employee_x0027_s_Bank"];
                        empView.Bank_Name = (string)config["Bank_Name"];
                        empView.Bank_Branch = (string)config["Bank_Branch"];
                        empView.Bank_Branch_Name = (string)config["Bank_Branch_Name"];
                        empView.Employee_x0027_s_Bank_2 = (string)config["Employee_x0027_s_Bank_2"];
                        empView.Bank_Name_2 = (string)config["Bank_Name_2"];
                        empView.Bank_Branch_2 = (string)config["Bank_Branch_2"];
                        empView.Payee_Bank_Acc_Name = (string)config["Payee_Bank_Acc_Name"];
                        empView.Bank_Branch_Name_2 = (string)config["Bank_Branch_Name_2"];
                        empView.BankAccountNumber2 = (string)config["Bank_Account_No_2"];
                        empView.Allow_Negative_Leave = (bool?)config["Allow_Negative_Leave"] ?? false;
                        empView.Off_Days = (int?)config["Off_Days"] ?? 0;
                        empView.Leave_Days_B_F = (int?)config["Leave_Days_B_F"] ?? 0;
                        empView.Allocated_Leave_Days = (int?)config["Allocated_Leave_Days"] ?? 0;
                        empView.Total_Leave_Days = (int?)config["Total_Leave_Days"] ?? 0;
                        empView.Total_Leave_Taken = (int?)config["Total_Leave_Taken"] ?? 0;
                        empView.Leave_Outstanding_Bal = (int?)config["Leave_Outstanding_Bal"] ?? 0;
                        empView.Leave_Balance = (int?)config["Leave_Balance"] ?? 0;
                        empView.Acrued_Leave_Days = (int?)config["Acrued_Leave_Days"] ?? 0;
                        empView.Cash_per_Leave_Day = (int?)config["Cash_per_Leave_Day"] ?? 0;
                        empView.Cash_Leave_Earned = (int?)config["Cash_Leave_Earned"] ?? 0;
                        empView.Leave_Status = (string)config["Leave_Status"];
                        empView.Leave_Type_Filter = (string)config["Leave_Type_Filter"];
                        empView.Leave_Period_Filter = (string)config["Leave_Period_Filter"];
                        empView.On_Leave = (bool?)config["On_Leave"] ?? false;
                        empView.Date_Filter = (string)config["Date_Filter"];

                        empView.User_SignatureodatamediaEditLink = (string)config["User_Signature@odata.mediaEditLink"];
                        empView.User_SignatureodatamediaReadLink = (string)config["User_Signature@odata.mediaReadLink"];
                    }
                }

                Session["EmployeeData"] = empView;
                return empView;
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                throw;
            }
        }

        private void SetUserAuthedication(string UserName, string email, string role)
        {
            try
            {
                var userModel = new UserViewModel();
                userModel.UserName = UserName;
                userModel.Email = email;
                userModel.RoleName = role;
                var userData = string.Format("{0}|{1}|{2}|{3}", userModel.UserName, userModel.UserID, userModel.Email,
                    userModel.RoleName);
                var ticket = new FormsAuthenticationTicket(1, userModel.UserName, DateTime.Now,
                    DateTime.Now.AddMinutes(1), false, userData);
                var encTicket = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            var user = new Authedication();
            return View(user);
        }

        [HttpPost]
        public ActionResult ForgotPassword(Authedication userlogin)
        {
            var msg = "";
            var email = string.Empty;
            var success = false;
            var UserName = userlogin.UserName.ToUpper();
            try
            {
                var userID = "";
                if (UserName.Contains("\\"))
                    userID = UserName;
                else
                    userID = ConfigurationManager.AppSettings["DOMAIN"] + @"\" + UserName;

                var page = "EmployeeList?$filter=User_ID eq '" + userID + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                if (details["value"].Any())
                {
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var User = (string)config["User_ID"];
                        email = (string)config["Company_E_Mail"];
                        if (User != "")
                        {
                            if (email != "")
                            {
                                #region generate random password

                                var rand = new Random();
                                var randAlpha = new Random();
                                var newpassint = rand.Next(10000, 99999);

                                var alphabetPosition = randAlpha.Next(1, 26);
                                var isCap = alphabetPosition % 2 == 0 ? true : false;
                                var theAlphabet = GetTheAlphabet(alphabetPosition, isCap);

                                alphabetPosition = randAlpha.Next(1, 26);
                                isCap = alphabetPosition % 2 == 0 ? true : false;
                                theAlphabet += GetTheAlphabet(alphabetPosition, isCap);

                                alphabetPosition = randAlpha.Next(1, 26);
                                isCap = alphabetPosition % 2 == 0 ? true : false;
                                theAlphabet += GetTheAlphabet(alphabetPosition, isCap);

                                alphabetPosition = randAlpha.Next(1, 26);
                                isCap = alphabetPosition % 2 == 0 ? true : false;
                                theAlphabet += GetTheAlphabet(alphabetPosition, isCap);

                                //string newpass = theAlphabet + "#" + newpassint.ToString() + "?" + alphabetPosition.ToString() + "@";
                                var newpass = theAlphabet + "#" + newpassint + "@" + alphabetPosition;

                                #endregion generate random password

                                var ok = Credentials.ResetPassword(User, newpass);
                                if (ok == "CHANGED")
                                {
                                    const string subject = "STAFF PORTAL CREDENTIALS";
                                    var emailmsg =
                                        "Staff portal credentials reset:<br />New password is <b />" + newpass +
                                        "" +
                                        "<br />Remember to change your password after you login";
                                    if (CommonClass.SendEmailAlert(emailmsg, email, subject))
                                    {
                                        msg = "A New password has been send to your Email<b>(" + email +
                                              ")</b>. Use it to login. Remember to change your password after you login";
                                        success = true;
                                    }
                                    else
                                    {
                                        msg =
                                            "An error occured while sending you the credentials.Please contact the ICT office administrator.";
                                        success = false;
                                    }
                                }
                                else
                                {
                                    msg = "Failed to reset password";
                                    success = false;
                                }
                            }
                            else
                            {
                                msg = "Warning!, password reset failed!. E-Mail empty. Contact your administrator!";
                                success = false;
                            }
                        }
                        else
                        {
                            msg = "User Name not found. Confirm if the user name is correct";
                            success = false;
                        }
                    }
                }
                else
                {
                    msg = "User Name not found. Confirm if the user name is correct";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.Replace("'", "");
                success = false;
            }

            return Json(new { message = msg, success }, JsonRequestBehavior.AllowGet);
        }

        private string GetTheAlphabet(int alphabetPosition, bool isCap)
        {
            var rval = string.Empty;
            switch (alphabetPosition)
            {
                case 1:
                    rval = "A";
                    break;
                case 2:
                    rval = "B";
                    break;
                case 3:
                    rval = "C";
                    break;
                case 4:
                    rval = "D";
                    break;
                case 5:
                    rval = "E";
                    break;
                case 6:
                    rval = "F";
                    break;
                case 7:
                    rval = "G";
                    break;
                case 8:
                    rval = "H";
                    break;
                case 9:
                    rval = "I";
                    break;
                case 10:
                    rval = "J";
                    break;
                case 11:
                    rval = "K";
                    break;
                case 12:
                    rval = "L";
                    break;
                case 13:
                    rval = "M";
                    break;
                case 14:
                    rval = "N";
                    break;
                case 15:
                    rval = "O";
                    break;
                case 16:
                    rval = "P";
                    break;
                case 17:
                    rval = "Q";
                    break;
                case 18:
                    rval = "R";
                    break;
                case 19:
                    rval = "S";
                    break;
                case 20:
                    rval = "T";
                    break;
                case 21:
                    rval = "U";
                    break;
                case 22:
                    rval = "V";
                    break;
                case 23:
                    rval = "W";
                    break;
                case 24:
                    rval = "X";
                    break;
                case 25:
                    rval = "Y";
                    break;
                default:
                    rval = "Z";
                    break;
            }

            return isCap ? rval : rval.ToLower();
        }


        public ESSRoleSetup ESSRoleSetup(string empNo, string UserName, string Email)
        {
            var roleSetupValue = new ESSRoleSetup();
            var Role = "";

            var rolePage = "ESSRoleSetup?$filter=User_ID eq '" + empNo + "'&$format=json";

            var httpResponseSettings = Credentials.GetOdataData(rolePage);
            using (var streamReader = new StreamReader(httpResponseSettings.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    roleSetupValue.User_ID = (string)config["User_ID"];
                    roleSetupValue.UserName = (string)config["UserName"];
                    roleSetupValue.Employee_Name = (string)config["Employee_Name"];
                    roleSetupValue.Employee_No = (string)config["Employee_No"];
                    roleSetupValue.Accounts_User = (bool)config["Accounts_User"];
                    roleSetupValue.Accounts_Approver = (bool)config["Accounts_Approver"];
                    roleSetupValue.Audit_Officer = (bool)config["Audit_Officer"];
                    roleSetupValue.HQ_Accountant = (bool)config["HQ_Accountant"];
                    roleSetupValue.HQ_Finance_Officer = (bool)config["HQ_Finance_Officer"];
                    roleSetupValue.HQ_Procurement_Officer = (bool)config["HQ_Procurement_Officer"];
                    roleSetupValue.Station_Accountant = (bool)config["Station_Accountant"];
                    roleSetupValue.Station_Procurement_Office = (bool)config["Station_Procurement_Office"];
                    roleSetupValue.DAAS_Officer = (bool)config["DAAS_Officer"];
                    roleSetupValue.HR_Welfare_Officer = (bool)config["HR_Welfare_Officer"];
                    roleSetupValue.Mobility_Officer = (bool)config["Mobility_Officer"];
                    roleSetupValue.Procurement_officer = (bool)config["Procurement_officer"];
                    roleSetupValue.Recruitment_Officer = (bool)config["Recruitment_Officer"];
                    roleSetupValue.Revenue_Officer = (bool)config["Revenue_Officer"];
                    roleSetupValue.Transport_Officer = (bool)config["Transport_Officer"];
                }
            }

            if (roleSetupValue.HQ_Accountant || roleSetupValue.Station_Accountant)
            {
                Role = "ACCOUNTANTS";
            }
            else if (roleSetupValue.HQ_Procurement_Officer || roleSetupValue.Station_Procurement_Office)
            {
                Role = "PROCUREMENT";
            }
            else
            {
                Role = "ALLUSERS";
            }
            SetUserAuthedication(UserName, Email, Role);
            Session["ESSRoleSetup"] = roleSetupValue;

            return roleSetupValue;
        }


    }
}