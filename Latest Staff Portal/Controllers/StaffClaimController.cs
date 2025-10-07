using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.Utils;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Latest_Staff_Portal.Controllers
{
    [CustomeAuthentication]
    [CustomAuthorization(Role = "ALLUSERS")]
    public class StaffClaimController : Controller
    {
        public ActionResult StaffClaims(string status)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                ViewBag.Status = status;
                return View();
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult StaffClaimsList(string status)
        {
            try
            {
                string staffNo = Session["Username"].ToString();
                List<StaffClaims> staffClaims = new List<StaffClaims>();
                var employee = Session["EmployeeData"] as EmployeeView;
                var station = employee?.GlobalDimension1Code;
                var page = "";
                if (string.IsNullOrEmpty(status))
                {
                    //page = "StaffClaim?$filter=Document_Type eq 'Staff Claims'&format=json";
                    // page = $"StaffClaim?$filter = Payment_Type eq 'Staff Claims' and Account_No eq '{staffNo}'&$format=json";
                    page = $"StaffClaims?$filter=Payment_Type eq 'Staff Claim' and Account_No eq '{staffNo}'&$format=json";


                }
                else
                {
                    page = "StaffClaim?$filter=Shortcut_Dimension_1_Code eq '" + station + "' and Payment Type eq 'Staff Claims' and Status eq '" + status + "' and Account_No eq '" + staffNo + "'&$format=json";
                }
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        var claim = new StaffClaims
                        {
                            No = (string)config["No"],
                            Date = (string)config["Date"],
                            AccountType = (string)config["Account_Type"],
                            AccountNo = (string)config["Account_No"],
                            AccountName = (string)config["Account_Name"],
                            PayingBankAccount = (string)config["Paying_Bank_Account"],
                            BankName = (string)config["Bank_Name"],
                            Payee = (string)config["Payee"],
                            PaymentNarration = (string)config["Payment_Narration"],
                            ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"],
                            DepartmentName = (string)config["Department_Name"],
                            ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"],
                            ProjectName = (string)config["Project_Name"],
                            TotalAmountLCY = (decimal)config["Total_Amount_LCY"],
                            StrategicPlan = (string)config["Strategic_Plan"],
                            ReportingYearCode = (string)config["Reporting_Year_Code"],
                            WorkplanCode = (string)config["Workplan_Code"],
                            ActivityCode = (string)config["Activity_Code"],
                            ExpenditureRequisitionCode = (string)config["Expenditure_Requisition_Code"],
                            Status = (string)config["Status"],
                            DimensionSetId = (string)config["Dimension_Set_ID"],
                            Posted = (string)config["Posted"],
                        };
                        staffClaims.Add(claim);
                    }

                }
                return PartialView("~/Views/StaffClaim/PartialViews/StaffClaimsList.cshtml", staffClaims.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [HttpPost]
        public ActionResult StaffClaimDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                StaffClaims claim = new StaffClaims();
                string page = "StaffClaims?$filter=No eq '" + DocNo + "'&format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        claim = new StaffClaims
                        {
                            No = (string)config["No"],
                            Date = (string)config["Date"],
                            AccountType = (string)config["Account_Type"],
                            AccountNo = (string)config["Account_No"],
                            AccountName = (string)config["Account_Name"],
                            PayingBankAccount = (string)config["Paying_Bank_Account"],
                            BankName = (string)config["Bank_Name"],
                            Payee = (string)config["Payee"],
                            PaymentNarration = (string)config["Payment_Narration"],
                            ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"],
                            DepartmentName = (string)config["Department_Name"],
                            ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"],
                            ProjectName = (string)config["Project_Name"],
                            TotalAmountLCY = (decimal)config["Total_Amount_LCY"],
                            StrategicPlan = (string)config["Strategic_Plan"],
                            ReportingYearCode = (string)config["Reporting_Year_Code"],
                            WorkplanCode = (string)config["Workplan_Code"],
                            ActivityCode = (string)config["Activity_Code"],
                            ExpenditureRequisitionCode = (string)config["Expenditure_Requisition_Code"],
                            Status = (string)config["Status"],
                            DimensionSetId = (string)config["Dimension_Set_ID"],
                            Posted = (string)config["Posted"],
                            AvailableAmount = ((decimal)config["Available_Amount"]).ToString("C", new CultureInfo("sw-KE")),
                            CommittedAmount = ((decimal)config["Committed_Amount"]).ToString("C", new CultureInfo("sw-KE")),
                            AieReceipt = (string)config["AIE_Receipt"],
                        };
                    }

                }
                return View(claim);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult StaffClaimLines(string DocNo, string Status)
        {
            try
            {
                List<StaffClaimLines> claimLines = new List<StaffClaimLines>();
                string pageLine = "StaffClaimLines?$filter=No eq '" + DocNo + "'&format=json";
                HttpWebResponse httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        StaffClaimLines claim = new StaffClaimLines
                        {
                            No = (string)config["No"],
                            Line_No = Convert.ToInt32(config["Line_No"]),
                            Bounced_Pv_No = (string)config["Bounced_Pv_No"],
                            Type = (string)config["Type"],
                            Account_Type = (string)config["Account_Type"],
                            Account_No = (string)config["Account_No"],
                            Account_Name = (string)config["Account_Name"],
                            Type_of_Expense = (string)config["Type_of_Expense"],
                            Payee_Bank_Account_No = (string)config["Payee_Bank_Account_No"],
                            Payee_Bank_Acc_Name = (string)config["Payee_Bank_Acc_Name"],
                            rbankName = (string)config["rbankName"],
                            Claim_Type = (string)config["Claim_Type"],
                            Date = (string)config["Date"],
                            Description = (string)config["Description"],
                            Project_Name1 = (string)config["Project_Name1"],
                            Currency_Code = (string)config["Currency_Code"],
                            Amount = Convert.ToInt32(config["Amount"]),
                            Amount_LCY = Convert.ToInt32(config["Amount_LCY"])

                        };

                        claimLines.Add(claim);
                    }
                }

                ViewBag.Status = Status;
                return PartialView("~/Views/StaffClaim/PartialViews/StaffClaimLines.cshtml", claimLines);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }




        public JsonResult DeleteStaffClaimExpense(string DocNo, int lineNo)
        {
            try
            {
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string empNo = employee?.No;
                var userId = employee?.UserID;
                Credentials.ObjNav.deleteStaffClaimLine(empNo, DocNo, lineNo);
                LogHelper.Log(DocNo, userId, "Delete staff claim lines");

                return Json(new { message = "Staff claim Line Deleted Successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GenerateFo22Report(string docNo)
        {
            try
            {
                var staffNo = Session["Username"].ToString();
                var _filename = staffNo.Replace("/", "");
                var message = "";
                bool success = false, view = false;

                var employeeView = Session["EmployeeData"] as EmployeeView;
                var dimension = employeeView?.GlobalDimension1Code;

                message = Credentials.ObjNav.GenerateClaimsReport(docNo);
                if (string.IsNullOrEmpty(message))
                {
                    success = false;
                    message = "File Not Found";
                }
                else
                {
                    success = true;
                }

                return Json(new { message, success, view }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CreateStaffClaim()
        {
            try
            {
                var message = "";
                bool success = false;
                var employee = Session["EmployeeData"] as EmployeeView;
                var staffNumber = Session["Username"].ToString();
                var userId = employee?.UserID;
                string DocNo = Credentials.ObjNav.fnInsertStaffClaims(staffNumber);
                if (DocNo != "")
                {
                    success = true;
                    message = DocNo;
                    return Json(new { message, success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    success = false;
                    message = "Staff claim Document not created. Try again";
                    return Json(new { message, success }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult NewStaffClaimLine(string Document_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    StaffClaimLines StaffClaimLine = new StaffClaimLines();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    StaffClaimLine.No = Document_No;

                    #region StaffClaimType
                    List<DropdownList> StaffClaimTypeList = new List<DropdownList>();
                    string pageCategory = "StaffClaimType?$format=json";

                    HttpWebResponse httpResponseCategory = Credentials.GetOdataData(pageCategory);
                    using (var streamReader = new StreamReader(httpResponseCategory.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Desciption"];
                            dropdownList3.Value = (string)config["Desciption"];
                            StaffClaimTypeList.Add(dropdownList3);
                        }
                    }
                    #endregion


                    StaffClaimLine.ListOfStaffClaimType = StaffClaimTypeList.Select(x =>
                           new SelectListItem()
                           {
                               Text = x.Text,
                               Value = x.Value
                           }).ToList();


                    return PartialView("~/Views/StaffClaim/PartialViews/NewStaffClaimLine.cshtml", StaffClaimLine);
                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitStaffClaimLine(StaffClaimLines newStaffClaimLine)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.fnInsertStaffClaimsLines(
                    newStaffClaimLine.No,
                    newStaffClaimLine.Claim_Type,
                    newStaffClaimLine.Description,
                    newStaffClaimLine.Amount
                );

                //Lastly, Redirect back to the document
                string redirect = "Record successfully inserted";
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteStaffClaimLine(string No, string Line_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.deleteStaffClaimLine(staffNo, No, int.Parse(Line_No));

                //Lastly, Redirect back to the document
                string redirect = "Record successfully Deleted";
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult SendStaffClaimsDocForApproval(string DocNo)
        {
            try
            {
                var staffNo = Session["Username"].ToString();
                var _filename = staffNo.Replace("/", "");
                var message = "";
                bool success = false, view = false;

                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                message = Credentials.ObjNav.sendStaffClaimApproval(staffNo, DocNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(57000, DocNo, UserID);

                if (string.IsNullOrEmpty(message))
                {
                    success = false;
                    message = "Document sent for approval";
                }
                else
                {
                    success = true;
                }

                return Json(new { message, success, view }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CancelStaffClaimsDocApproval(string DocNo)
        {
            try
            {
                var staffNo = Session["Username"].ToString();
                var _filename = staffNo.Replace("/", "");
                var message = "";
                bool success = true, view = false;

                var employeeView = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.CancelStaffClaimforApproval(DocNo);
                message = "Document approval cancelled";
                return Json(new { message, success, view }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}