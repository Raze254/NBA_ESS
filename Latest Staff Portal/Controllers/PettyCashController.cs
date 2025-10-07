using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    public class PettyCashController : Controller
    {
        // Petty Cash Voucher
        public ActionResult PettyCashVouchersList(string status)
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
        public PartialViewResult PettyCashVouchersListPartialView(string status)
        {
            try
            {
                string staffNo = Session["Username"].ToString();
                List<PettyCashVouchers> pettyCashVouchersList = new List<PettyCashVouchers>();
                var employee = Session["EmployeeData"] as EmployeeView;
                var station = employee?.GlobalDimension1Code;
                var page = "";

                page = $"PettyCashVouchers?$filter=Created_By eq '{employee.UserID}'&$format=json";

                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        var pettyCashVoucher = new PettyCashVouchers
                        {
                            No = (string)config["No"],
                            Date = (string)config["Date"],
                            Pay_Mode = (string)config["Pay_Mode"],
                            Cheque_No = (string)config["Cheque_No"],
                            Cheque_Date = (string)config["Cheque_Date"],
                            Paying_Bank_Account = (string)config["Paying_Bank_Account"],
                            Payee = (string)config["Payee"],
                            Total_Amount_LCY = (int)(config["Total_Amount_LCY"] ?? 0),
                            Currency_Code = (string)config["Currency_Code"],
                            Created_By = (string)config["Created_By"],
                            Status = (string)config["Status"],
                            Posted = (bool)(config["Posted"] ?? false),
                            Posted_By = (string)config["Posted_By"],
                            Posted_Date = (string)config["Posted_Date"],
                            Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                            Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"]
                        };
                        pettyCashVouchersList.Add(pettyCashVoucher);
                    }
                }
                return PartialView("~/Views/PettyCash/PartialViews/PettyCashVouchersListPartialView.cshtml", pettyCashVouchersList.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [HttpPost]
        public ActionResult PettyCashVouchersDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var employee = Session["EmployeeData"] as EmployeeView;

                PettyCashVouchers2 pettyCash = new PettyCashVouchers2();
                //string page = "PettyCashVouchers?$filter=No eq '" + DocNo + "'&format=json";

                string page = "PaymentsApi2?$filter=no eq '" + DocNo + "'&format=json";



                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        pettyCash = new PettyCashVouchers2
                        {
                            No = (string)config["no"],
                            Date = (string)config["date"],
                            Pay_Mode = (string)config["payMode"],
                            Cheque_No = (string)config["chequeNo"],
                            Cheque_Date = (string)config["chequeDate"],
                            Paying_Bank_Account = (string)config["payingBankAccount"],
                            Payee = (string)config["payee"],
                            Total_Amount_LCY = (int)(config["totalAmountLCY"] ?? 0),
                            Currency_Code = (string)config["currencyCode"],
                            Created_By = (string)config["createdBy"],
                            Status = (string)config["status"],
                            Posted = (bool)(config["posted"] ?? false),
                            Posted_By = (string)config["postedBy"],
                            Posted_Date = (string)config["postedDate"],
                            Shortcut_Dimension_1_Code = employee.GlobalDimension1Code,
                            Shortcut_Dimension_2_Code = employee.GlobalDimension2Code,
                            Payment_Narration = (string)config["paymentNarration"]

                        };
                    }

                }
                return View(pettyCash);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult PettyCashVouchersLines(string DocNo, string Status)
        {
            try
            {
                List<PettyCashVoucherLines> pettyLines = new List<PettyCashVoucherLines>();
                string pageLine = "PCVLines?$filter=No eq '" + DocNo + "'&format=json";
                HttpWebResponse httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        PettyCashVoucherLines claim = new PettyCashVoucherLines
                        {
                            No = (string)config["No"],
                            Line_No = (int)config["Line_No"],
                            Bounced_Pv_No = (string)config["Bounced_Pv_No"],
                            Type = (string)config["Type"],
                            Account_Type = (string)config["Account_Type"],
                            Account_No = (string)config["Account_No"],
                            Account_Name = (string)config["Account_Name"],
                            Amount = (int)config["Amount"],
                            Net_Amount = (int)config["Net_Amount"],
                            Remaining_Amount = (int)config["Remaining_Amount"],
                            VAT_Rate = (int)config["VAT_Rate"],
                            VAT_Six_Percent_Rate = (int)config["VAT_Six_Percent_Rate"],
                            VAT_Withheld_Code = (string)config["VAT_Withheld_Code"],
                            VAT_Withheld_Amount = (int)config["VAT_Withheld_Amount"],
                            Budgetary_Control_A_C = (bool)config["Budgetary_Control_A_C"],
                            Advance_Recovery = (int)config["Advance_Recovery"],
                            Retention_Amount = (int)config["Retention_Amount"],
                            Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                            Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                            Claim_Doc_No = (string)config["Claim_Doc_No"],
                            VAT_Code = (string)config["VAT_Code"],
                            W_Tax_Code = (string)config["W_Tax_Code"],
                            W_T_VAT_Code = (string)config["W_T_VAT_Code"],
                            VAT_Amount = (int)config["VAT_Amount"],
                            W_Tax_Amount = (int)config["W_Tax_Amount"],
                            Total_Net_Pay = (int)config["Total_Net_Pay"],
                            Status = (string)config["Status"]

                        };

                        pettyLines.Add(claim);
                    }
                }
                ViewBag.DocNo = DocNo;
                ViewBag.Status = Status;
                return PartialView("~/Views/PettyCash/PartialViews/PettyCashVouchersLines.cshtml", pettyLines);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }





        public ActionResult NewPettyCashVoucherRequest()
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();
                var NewPettyCashVoucher = new PettyCashVouchers2();
                string Dim1 = "", Dim2 = "";

                #region Employees
                var EmployeesList = new List<DropdownList>();
                var pageEmployee = "Customers?$format=json";
                var httpResponseEmployee = Credentials.GetOdataData(pageEmployee);
                using (var streamReader = new StreamReader(httpResponseEmployee.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Name"] + " (" + (string)config1["No"] + ")",
                            Value = (string)config1["No"]
                        };
                        EmployeesList.Add(dropdownList);
                    }
                }
                #endregion
                #region FundingSource
                var FundingSourceList = new List<DropdownList>();
                var page = "DimensionValueList?$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["Name"]} - {(string)config1["Code"]}",
                            Value = (string)config1["Code"]
                        };
                        FundingSourceList.Add(dropdownList);
                    }
                }
                #endregion
                #region WorkplanActivities

                var WorkplanActivitiesList = new List<DropdownList>();
                var pageWorkplanActivities = "WorkplanActivities?$format=json";

                var httpWorkplanActivities = Credentials.GetOdataData(pageWorkplanActivities);
                using (var streamReader = new StreamReader(httpWorkplanActivities.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Descriptions"],
                            Value = (string)config1["Code"]
                        };
                        WorkplanActivitiesList.Add(dropdownList);
                    }
                }
                #endregion
                #region ExpenseRequisitions

                var ExpenseRequisitionsList = new List<DropdownList>();
                var pageExpenseRequisitions = "ExpenditureRequisitions?$format=json";

                var httpExpenseRequisitions = Credentials.GetOdataData(pageExpenseRequisitions);
                using (var streamReader = new StreamReader(httpExpenseRequisitions.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["Description"],
                            Value = (string)config["Annual_Year_Code"]
                        };
                        ExpenseRequisitionsList.Add(dropdownList);
                    }
                }
                #endregion
                #region Job
                var JobList = new List<DropdownList>();
                var pageJob = "JobList?$format=json";
                var httpResponseJob = Credentials.GetOdataData(pageJob);
                using (var streamReader = new StreamReader(httpResponseJob.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["Name"]} - {(string)config1["Code"]}",
                            Value = (string)config1["Code"]
                        };
                        JobList.Add(dropdownList);
                    }
                }
                #endregion
                #region JobTaskNo
                var JobTaskList = new List<DropdownList>();
                var pageJobTask = "ProjectTask?$format=json";
                var httpResponseJobTask = Credentials.GetOdataData(pageJobTask);
                using (var streamReader = new StreamReader(httpResponseJobTask.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["Name"]} - {(string)config1["Code"]}",
                            Value = (string)config1["Code"]
                        };
                        JobTaskList.Add(dropdownList);
                    }
                }
                #endregion
                #region Region

                var Dim1List = new List<DropdownList>();

                var pageDepartment = "DimensionValueList?$filter=Dimension_Code eq 'REGIONS' and Blocked eq false&$format=json";

                var httpResponseDepartment = Credentials.GetOdataData(pageDepartment);
                using (var streamReader = new StreamReader(httpResponseDepartment.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["Name"],
                            Value = (string)config["Code"]
                        };
                        Dim1List.Add(dropdownList);
                    }
                }

                #endregion
                #region Department

                var Dim2List = new List<DropdownList>();
                /*var pageDivision ="DimensionValueList?$filter=Global_Dimension_No eq 2 and Blocked eq false&$format=json";*/
                var pageDivision = "DimensionValueList?$filter=Dimension_Code eq 'DEPARTMENT' and Blocked eq false&$format=json";

                var httpResponseDivision = Credentials.GetOdataData(pageDivision);
                using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["Name"],
                            Value = (string)config["Code"]
                        };
                        Dim1List.Add(dropdownList);
                        Dim2List.Add(dropdownList);
                    }
                }

                #endregion



                NewPettyCashVoucher.ListOfEmployees = EmployeesList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();


                NewPettyCashVoucher.ListOfFundingSource = FundingSourceList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();


                NewPettyCashVoucher.ListOfWorkplanActivities = WorkplanActivitiesList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();
                NewPettyCashVoucher.ListOfExpReq = ExpenseRequisitionsList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

                NewPettyCashVoucher.ListOfJobs = JobList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();
                NewPettyCashVoucher.ListOfJobTasks = JobTaskList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();
                NewPettyCashVoucher.ListOfDim1 = Dim1List.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();
                NewPettyCashVoucher.ListOfDim2 = Dim2List.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

                NewPettyCashVoucher.Paying_Bank_Account = StaffNo;
                NewPettyCashVoucher.Payee = employeeView.FirstName + " " + employeeView.MiddleName + " " + employeeView.LastName;
                return View("~/Views/PettyCash/PartialViews/NewPettyCashVouchersRequest.cshtml", NewPettyCashVoucher);

            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitPettyCashVoucherRequest(string Account_Type, string Paying_Bank_Account, string Payment_Narration)
        {
            var successVal = false;
            try
            {

                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();

                var DocNo = "";

                DocNo = Credentials.ObjNav.createPettyCash(
                    "",
                    StaffNo,
                    Payment_Narration
                );
                if (DocNo != "")
                {
                    string[] parts = DocNo.Split('*');
                    string DocumentNo = parts[1];
                    var Redirect = DocumentNo;
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { message = "Document not created. Please try again later...", success = false },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (successVal) Session["ErrorMsg"] = ex.Message.Replace("'", "");
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult NewPettyCashVoucherLine(string docNo)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();

                var pcvLine = new PettyCashVoucherLines();

                #region AccountNo
                var AccountNoList = new List<DropdownList>();
                var pageWp = $"GLAccounts?$filter=Direct_Posting eq true &$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Name"] + " (" + (string)config1["No"] + ")",
                            Value = (string)config1["No"]
                        };
                        AccountNoList.Add(dropdownList);
                    }
                }
                #endregion

                pcvLine.ListOfAccounts = AccountNoList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                pcvLine.Account_No = StaffNo;
                pcvLine.Account_Name = employeeView.FirstName + " " + employeeView.MiddleName + " " + employeeView.LastName;
                return PartialView("~/Views/PettyCash/PartialViews/NewPettyCashVoucherLine.cshtml",
                    pcvLine);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitPettyCashVoucherLine(string DocNo, string Account_No, int Amount)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var AccType = "";

                string res = Credentials.ObjNav.createPettyCashLines(
                    DocNo,
                    Amount,
                    Account_No
                 );

                if (res == "")
                {

                }

                return Json(new { message = "Record successfully submitted", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeletePettyCashVoucherLine(string DocNo, int Line_No)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.deletePettyCashLines(
                    Line_No,
                    DocNo
                 );

                return Json(new { message = "Record successfully Deleted", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendPettyCashVoucherForApproval(string DocNo)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var AccType = "";

                Credentials.ObjNav.SendDocForApprval(DocNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(57000, DocNo, UserID);
                return Json(new { message = "Record successfully submitted", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancelPettyCashVoucherApproval(string DocNo)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var AccType = "";

                Credentials.ObjNav.CancelDocapproval(DocNo);

                return Json(new { message = "Document Approval successfully cancelled", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }





        // GET: Petty Cash surrender
        public ActionResult PettyCashSurrenderList(string status)
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
        public PartialViewResult PettyCashSurrenderListPartialView(string status)
        {
            try
            {
                string staffNo = Session["Username"].ToString();
                List<PettyCashSurrenders> pettyCashSurrenderList = new List<PettyCashSurrenders>();
                var employee = Session["EmployeeData"] as EmployeeView;
                var station = employee?.GlobalDimension1Code;
                var page = "";

                //page = $"PostedPettyCashSurrenders?$filter=Created_By eq '{employee.UserID}'&$format=json";

                page = $"PaymentsApi2?$filter=paymentType eq 'Petty Cash Surrender' and createdBy eq '{employee.UserID}'&$format=json";


                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        var pettyCashSurr = new PettyCashSurrenders
                        {
                            No = (string)config["no"],
                            Date = (string)config["date"],
                            Pay_Mode = (string)config["payMode"],
                            Cheque_No = (string)config["chequeNo"],
                            Cheque_Date = (string)config["chequeDate"],
                            Paying_Bank_Account = (string)config["payingBankAccount"],
                            Payee = (string)config["payee"],
                            Total_Amount_LCY = (int)(config["totalAmountLCY"] ?? 0),
                            Currency_Code = (string)config["currencyCode"],
                            Created_By = (string)config["createdBy"],
                            Status = (string)config["status"],
                            Posted = (bool)(config["posted"] ?? false),
                            Posted_By = (string)config["postedBy"],
                            Posted_Date = (string)config["postedDate"],
                            Shortcut_Dimension_1_Code = (string)config["shortcutDimension1Code"],
                            Shortcut_Dimension_2_Code = (string)config["shortcutDimension1Code"],
                            Payment_Narration = (string)config["paymentNarration"],
                        };
                        pettyCashSurrenderList.Add(pettyCashSurr);
                    }
                }
                return PartialView("~/Views/PettyCash/PartialViews/PettyCashSurrenderListPartialView.cshtml", pettyCashSurrenderList.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [HttpPost]
        public ActionResult PettyCashSurrenderDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                PettyCashVouchers2 pettyCash = new PettyCashVouchers2();
                //string page = "PettyCashVouchers?$filter=No eq '" + DocNo + "'&format=json";

                string page = "PaymentsApi2?$filter=no eq '" + DocNo + "'&format=json";



                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        pettyCash = new PettyCashVouchers2
                        {
                            No = (string)config["no"],
                            Date = (string)config["date"],
                            Pay_Mode = (string)config["payMode"],
                            Cheque_No = (string)config["chequeNo"],
                            Cheque_Date = (string)config["chequeDate"],
                            Paying_Bank_Account = (string)config["payingBankAccount"],
                            Payee = (string)config["payee"],
                            Total_Amount_LCY = (int)(config["totalAmountLCY"] ?? 0),
                            Currency_Code = (string)config["currencyCode"],
                            Created_By = (string)config["createdBy"],
                            Status = (string)config["status"],
                            Posted = (bool)(config["posted"] ?? false),
                            Posted_By = (string)config["postedBy"],
                            Posted_Date = (string)config["postedDate"],
                            Shortcut_Dimension_1_Code = (string)config["shortcutDimension1Code"],
                            Shortcut_Dimension_2_Code = (string)config["shortcutDimension1Code"],
                            Payment_Narration = (string)config["paymentNarration"],

                        };
                    }

                }
                return View(pettyCash);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult PettyCashSurrenderLines(string DocNo, string Status)
        {
            try
            {
                List<PettyCashSurrenderLines> pettyLines = new List<PettyCashSurrenderLines>();
                string pageLine = "PettyCashSurrenderLines?$filter=No eq '" + DocNo + "'&format=json";
                HttpWebResponse httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        PettyCashSurrenderLines claim = new PettyCashSurrenderLines
                        {
                            No = (string)config["No"],
                            Line_No = (int)config["Line_No"],
                            Account_Type = (string)config["Account_Type"],
                            Account_No = (string)config["Account_No"],
                            Account_Name = (string)config["Account_Name"],
                            Description = (string)config["Description"],
                            Amount = (int)config["Amount"],
                            Actual_Spent = (int)config["Actual_Spent"],
                            Receipt_No = (string)config["Receipt_No"],
                            Cash_Receipt_Amount = (int)config["Cash_Receipt_Amount"],
                            Remaining_Amount = (int)config["Remaining_Amount"],


                        };

                        pettyLines.Add(claim);
                    }
                }
                ViewBag.DocNo = DocNo;
                ViewBag.Status = Status;
                return PartialView("~/Views/PettyCash/PartialViews/PettyCashSurrenderLines.cshtml", pettyLines);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public ActionResult NewPettyCashSurrenderRequest()
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");
                var employee = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();
                var NewPettyCashSurrender = new PettyCashSurrender();
                string Dim1 = "", Dim2 = "";

                #region Employees
                var EmployeesList = new List<DropdownList>();
                var pageEmployee = "Customers?$format=json";
                var httpResponseEmployee = Credentials.GetOdataData(pageEmployee);
                using (var streamReader = new StreamReader(httpResponseEmployee.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Name"] + " (" + (string)config1["No"] + ")",
                            Value = (string)config1["No"]
                        };
                        EmployeesList.Add(dropdownList);
                    }
                }
                #endregion


                #region PettyCashNo
                var PettyCashNoList = new List<DropdownList>();
                var pagePettyCash = $"PaymentsApi2?$filter=paymentType eq 'Petty Cash' and Posted eq true and Surrendered eq false and accountNo eq '{StaffNo}' &$format=json";
                var httpResponsePettyCash = Credentials.GetOdataData(pagePettyCash);
                using (var streamReader = new StreamReader(httpResponsePettyCash.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["no"],
                            Value = (string)config1["no"]
                        };
                        PettyCashNoList.Add(dropdownList);
                    }
                }
                #endregion


                NewPettyCashSurrender.ListOfEmployees = EmployeesList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();


                NewPettyCashSurrender.ListOfPettyCash = PettyCashNoList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();
                NewPettyCashSurrender.Account_No = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName + " (" + StaffNo + ")";
                return View("~/Views/PettyCash/PartialViews/NewPettyCashSurrenderRequest.cshtml", NewPettyCashSurrender);

            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitPettyCashSurrenderRequest(string Account_Type, string Account_No, string Petty_Cash_No)
        {
            var successVal = false;
            try
            {

                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();

                var DocNo = "";

                DocNo = Credentials.ObjNav.SurrenderPettyCash(
                    "",
                    StaffNo,
                    Petty_Cash_No,
                    ""
                );
                Credentials.ObjNav.InsertPettyCash(DocNo, Petty_Cash_No);

                var Redirect = DocNo;
                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                if (successVal) Session["ErrorMsg"] = ex.Message.Replace("'", "");
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult UpdatePettyCashSurrenderLine(string documentNo, string lineNo, string actualSpent, string receiptNo)
        {
            var successVal = false;
            try
            {

                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();

                var DocNo = "";

                DocNo = Credentials.ObjNav.editPettyCashSurrenderLines(
                    documentNo,
                    int.Parse(lineNo),
                    decimal.Parse(actualSpent),
                    receiptNo
                );

                if (DocNo.StartsWith("success*"))
                {
                    /*string[] parts = DocNo.Split('*');
                    string DocumentNo = parts[1];*/
                    var Redirect = "Record successfully updated";
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { message = "Record not updated. Please try again later...", success = false },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (successVal) Session["ErrorMsg"] = ex.Message.Replace("'", "");
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult SendPettyCashSurrenderForApproval(string DocNo)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var AccType = "";

                string res = "";
                Credentials.ObjNav.SendDocForApprval(DocNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(57000, DocNo, UserID);
                return Json(new { message = "Record successfully sent for approval", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelPettyCashSurrenderApproval(string DocNo)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var AccType = "";

                string res = "";
                Credentials.ObjNav.CancelDocapproval(DocNo);

                return Json(new { message = "Document Approval successfully cancelled", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


    }
}