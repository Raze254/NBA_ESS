using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.Utils;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    [CustomeAuthentication]
    [CustomAuthorization(Role = "ALLUSERS")]
    public class ImprestSurrenderController : Controller
    {
        // GET: ImprestSurrender
        public ActionResult ImprestSurrender()
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
        public PartialViewResult ImprestSurrenderList(string status)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
/*                var impAccount = ImprestAccount();
                if (impAccount != "") StaffNo = impAccount;*/

                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var employee = Session["EmployeeData"] as EmployeeView;
                var dimension1 = employee?.GlobalDimension1Code;
                var page = "";

                if (status == "Approved")
                {
                    page = "ApprovedImprestSurrender?$filter=Account_No eq '" + StaffNo + "'&$format=json";
                }
                else if (status == "Pending Approval")
                {
                    page = "ImprestSurrender?$filter=Account_No eq '" + StaffNo + "' and Status eq 'Pending Approval'&$format=json";
                }
                else
                {
                    page = "ImprestSurrender?$filter=Account_No eq '" + StaffNo + "' and Status eq 'Open'&$format=json";
                }



                if (status != "Approved")
                {
                    var imprestSurrenders = new List<ImprestSurrender>();
                    var httpResponse = Credentials.GetOdataData(page);
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        var details = JObject.Parse(result);
                        foreach (var jToken in details["value"])
                        {
                            var config = (JObject)jToken;
                            var imprest = new ImprestSurrender();
                            imprest.No = (string)config["No"];
                            imprest.Date = DateTime.ParseExact((string)config["Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                            imprest.ImprestDeadline = DateTime.ParseExact((string)config["Imprest_Deadline"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                            imprest.ImprestMemoSurrenderNo = (string)config["Imprest_Memo_Surrender_No"];
                            imprest.AccountType = (string)config["Account_Type"];
                            imprest.AccountNo = (string)config["Account_No"];
                            imprest.AccountName = (string)config["Account_Name"];
                            imprest.Payee = (string)config["Payee"];
                            imprest.HOD = (bool)config["HOD"];
                            imprest.CurrencyCode = (string)config["Currency_Code"];
                            imprest.CreatedBy = (string)config["Created_By"];
                            imprest.Status = (string)config["Status"];
                            imprest.ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"];
                            imprest.DepartmentName = (string)config["Department_Name"];
                            imprest.ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"];
                            imprest.ProjectName = (string)config["Project_Name"];
                            imprest.ImprestAmount = ((decimal)config["Imprest_Amount"]).ToString("C", new CultureInfo("sw-KE"));
                            imprest.ImprestIssueDocNo = (string)config["Imprest_Issue_Doc_No"];
                            imprest.ReferenceNo = (string)config["Reference_No"];
                            imprest.ActualAmountSpent = ((decimal)config["Actual_Amount_Spent"]).ToString("C", new CultureInfo("sw-KE"));
                            imprest.ActualAmountSpentLCY = (decimal)config["Actual_Amount_Spent_LCY"];
                            imprest.CashReceiptAmount = (decimal)config["Cash_Receipt_Amount"];
                            imprest.RemainingAmount = ((decimal)config["Remaining_Amount"]).ToString("C", new CultureInfo("sw-KE"));
                            imprest.StrategicPlan = (string)config["Strategic_Plan"];
                            imprest.ReportingYearCode = (string)config["Reporting_Year_Code"];
                            imprest.WorkplanCode = (string)config["Workplan_Code"];
                            imprest.ActivityCode = (string)config["Activity_Code"];
                            imprest.ExpenditureRequisitionCode = (string)config["Expenditure_Requisition_Code"];
                            imprestSurrenders.Add(imprest);
                        }
                    }
                  
                    ViewBag.status = status;
                    return PartialView("~/Views/ImprestSurrender/ImprestSurrenderListView.cshtml",
                        imprestSurrenders.OrderByDescending(x => x.No));
                }
                else
                {
                    var approvedmprestSurr = new List<ApprovedImprestSurrender>();
                    var httpResponse = Credentials.GetOdataData(page);
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        var details = JObject.Parse(result);
                        foreach (var jToken in details["value"])
                        {
                            var config = (JObject)jToken;
                            var imprestSurr = new ApprovedImprestSurrender
                            {
                                No = (string)config["No"],
                                Date = (string)config["Date"],
                                Posting_Date = (string)config["Posting_Date"],
                                Imprest_Deadline = (string)config["Imprest_Deadline"],
                                Imprest_Issue_Doc_No = (string)config["Imprest_Issue_Doc_No"],
                                Reference_No = (string)config["Reference_No"],
                                Account_Type = (string)config["Account_Type"],
                                Account_No = (string)config["Account_No"],
                                Account_Name = (string)config["Account_Name"],
                                Payee = (string)config["Payee"],
                                Created_By = (string)config["Created_By"],
                                Status = (string)config["Status"],
                                Currency_Code = (string)config["Currency_Code"],
                                Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                                Department_Name = (string)config["Department_Name"],
                                Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                                Project_Name = (string)config["Project_Name"],
                                Imprest_Amount = (int)config["Imprest_Amount"],
                                Actual_Amount_Spent = (int)config["Actual_Amount_Spent"],
                                Actual_Amount_Spent_LCY = (int)config["Imprest_Amount"],
                                Cash_Receipt_Amount = (int)config["Cash_Receipt_Amount"],
                                Remaining_Amount = (int)config["Remaining_Amount"],
                                Strategic_Plan = (string)config["Strategic_Plan"],
                                Reporting_Year_Code = (string)config["Reporting_Year_Code"],
                                Workplan_Code = (string)config["Workplan_Code"],
                                Activity_Code = (string)config["Activity_Code"],
                                Expenditure_Requisition_Code = (string)config["Expenditure_Requisition_Code"]
                            };

                            approvedmprestSurr.Add(imprestSurr);
                        }

                    }
                    if (status == "Approved")
                    {
                        status = "Released";
                    }
                    ViewBag.status = status;
                    return PartialView("~/Views/ImprestSurrender/ApprovedImprestSurrenderListPartialView.cshtml",
                        approvedmprestSurr.OrderByDescending(x => x.No));
                }

            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult NewImprestSurrender()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    RedirectToAction("Login", "Login");
                }
                string staffNo = Session["Username"].ToString();
                var employee = Session["EmployeeData"] as EmployeeView;
                var impAccount = ImprestAccount();
                if (impAccount != "")
                {
                    staffNo = impAccount;
                }
                #region Imprest Document List
                var imprestList = new List<DropdownList>();
                var pageLine = "ImprestWarranties?$filter=Account_No eq '" + staffNo + "' and Surrendered eq false and Posted eq true and Selected eq false &$format=json";
                //var pageLine = "ImprestWarranties?$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    imprestList.AddRange(from JObject config in details["value"] select new DropdownList { Text = (string)config["No"] + "-" + (string)config["Payment_Narration"], Value = (string)config["No"] });
                }
                #endregion
                var impSurrender = new ImprestSurrender
                {
                    ListOfImprestIssueDoc = imprestList.Select(x =>
                        new SelectListItem()
                        {
                            Text = x.Text,
                            Value = x.Value
                        }).ToList()
                };
                return PartialView("PartialViews/NewImprestSurrender",
                    impSurrender);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        protected string ImprestAccount()
        {
            var ImpAcc = "";
            var userID = Session["UserID"].ToString();
            var page = "UserSetup?$select=Imprest_Account&$filter=User_ID eq '" + userID + "'&format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Any())
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    ImpAcc = (string)config["Imprest_Account"];
                }

            return ImpAcc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        protected bool isImprestSurrendered(string DocNo, decimal Amount)
        {
            var ext = false;
            var page = "ImprestSurrenderList?$select=Amount&$filter=Imprest_Issue_Doc_No eq '" + DocNo +
                       "'&format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Any())
            {
                decimal amt = 0;
                foreach (JObject config in details["value"]) amt = amt + (decimal)config["Amount"];
                if (amt >= Amount) ext = true;
            }

            return ext;
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitImprestSurrender(string imprestNumber)
        {
            try
            {
                var cl = false;
                var staffNo = Session["Username"].ToString();
                var employee = Session["EmployeeData"] as EmployeeView;
                var userId = employee?.UserID;
                var documentNumber = Credentials.ObjNav.createImprestSurrender1(staffNo);
                string docNo = Credentials.ObjNav.InsertImperestNo(imprestNumber, documentNumber);



                if (docNo != "")
                {
                    var successMessage = "Imprest Surrender Document created Successfully";
                    return Json(new { message = docNo, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string Redirect = documentNumber;
                    return Json(new { message = Redirect, success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult ViewSurrenderDocument(string DocNo)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");

                #region Imp Surrender Header

                var imprestSurrender = new ImprestSurrender();

                var page = "ImprestSurrender?$filter=No eq '" + DocNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        imprestSurrender.No = (string)config["No"];
                        imprestSurrender.Date = DateTime.ParseExact((string)config["Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprestSurrender.ImprestDeadline = DateTime.ParseExact((string)config["Imprest_Deadline"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprestSurrender.ImprestMemoSurrenderNo = (string)config["Imprest_Memo_Surrender_No"];
                        imprestSurrender.AccountType = (string)config["Account_Type"];
                        imprestSurrender.AccountNo = (string)config["Account_No"];
                        imprestSurrender.AccountName = (string)config["Account_Name"];
                        imprestSurrender.Payee = (string)config["Payee"];
                        imprestSurrender.HOD = (bool)config["HOD"];
                        imprestSurrender.CurrencyCode = (string)config["Currency_Code"];
                        imprestSurrender.CreatedBy = (string)config["Created_By"];
                        imprestSurrender.Status = (string)config["Status"];
                        imprestSurrender.Posted = (string)config["Posted"];
                        imprestSurrender.ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"];
                        imprestSurrender.DepartmentName = (string)config["Department_Name"];
                        imprestSurrender.ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"];
                        imprestSurrender.ProjectName = (string)config["Project_Name"];
                        imprestSurrender.ImprestAmount = ((decimal)config["Imprest_Amount"]).ToString("C", new CultureInfo("sw-KE"));
                        imprestSurrender.ImprestIssueDocNo = (string)config["Imprest_Issue_Doc_No"];
                        imprestSurrender.ReferenceNo = (string)config["Reference_No"];
                        imprestSurrender.ActualAmountSpent = ((decimal)config["Actual_Amount_Spent"]).ToString("C", new CultureInfo("sw-KE"));
                        imprestSurrender.ActualAmountSpentLCY = (decimal)config["Actual_Amount_Spent_LCY"];
                        imprestSurrender.CashReceiptAmount = (decimal)config["Cash_Receipt_Amount"];
                        imprestSurrender.RemainingAmount = ((decimal)config["Remaining_Amount"]).ToString("N2");
                        imprestSurrender.StrategicPlan = (string)config["Strategic_Plan"];
                        imprestSurrender.ReportingYearCode = (string)config["Reporting_Year_Code"];
                        imprestSurrender.WorkplanCode = (string)config["Workplan_Code"];
                        imprestSurrender.ActivityCode = (string)config["Activity_Code"];
                        imprestSurrender.ExpenditureRequisitionCode = (string)config["Expenditure_Requisition_Code"];
                        imprestSurrender.PostingDate = (string)config["Posting_Date"];
                    }
                }
                #endregion

                return View(imprestSurrender);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        [HttpPost]
        public ActionResult ViewApprovedSurrenderDocument(string DocNo)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");



                var imprestSurrender = new ApprovedImprestSurrender();

                var page = "ApprovedImprestSurrender?$filter=No eq '" + DocNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;

                        imprestSurrender = new ApprovedImprestSurrender
                        {
                            No = (string)config["No"],
                            Date = (string)config["Date"],
                            Posting_Date = (string)config["Posting_Date"],
                            Imprest_Deadline = (string)config["Imprest_Deadline"],
                            Imprest_Issue_Doc_No = (string)config["Imprest_Issue_Doc_No"],
                            Reference_No = (string)config["Reference_No"],
                            Account_Type = (string)config["Account_Type"],
                            Account_No = (string)config["Account_No"],
                            Account_Name = (string)config["Account_Name"],
                            Payee = (string)config["Payee"],
                            Created_By = (string)config["Created_By"],
                            Status = (string)config["Status"],
                            Currency_Code = (string)config["Currency_Code"],
                            Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                            Department_Name = (string)config["Department_Name"],
                            Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                            Project_Name = (string)config["Project_Name"],
                            Imprest_Amount = (int)config["Imprest_Amount"],
                            Actual_Amount_Spent = (int)config["Actual_Amount_Spent"],
                            Actual_Amount_Spent_LCY = (int)config["Actual_Amount_Spent_LCY"],
                            Cash_Receipt_Amount = (int)config["Cash_Receipt_Amount"],
                            Remaining_Amount = (int)config["Remaining_Amount"],
                            Strategic_Plan = (string)config["Strategic_Plan"],
                            Reporting_Year_Code = (string)config["Reporting_Year_Code"],
                            Workplan_Code = (string)config["Workplan_Code"],
                            Activity_Code = (string)config["Activity_Code"],
                            Expenditure_Requisition_Code = (string)config["Expenditure_Requisition_Code"]
                        };
                    }
                }
                return View(imprestSurrender);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult ImprestSurrenderLines(string DocNo, string Status, string applyToDocumentNumber)
        {
            try
            {
                #region Imprest Surrender Lines

                var imprestSurrenderLines = new List<ImprestSurrenderLine>();
                var pageLine = "ImprestSurrenderLines?$filter=No eq '" + DocNo + "'&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var imprestSurrenderLine = new ImprestSurrenderLine();
                        imprestSurrenderLine.No = (string)config["No"];
                        imprestSurrenderLine.LineNo = (int)config["Line_No"];
                        imprestSurrenderLine.AdvanceType = (string)config["Advance_Type"];
                        imprestSurrenderLine.AccountType = (string)config["Account_Type"];
                        imprestSurrenderLine.AccountNo = (string)config["Account_No"];
                        imprestSurrenderLine.AccountName = (string)config["Account_Name"];
                        imprestSurrenderLine.JobNo = (string)config["Job_No"];
                        imprestSurrenderLine.JobTaskNo = (string)config["Job_Task_No"];
                        imprestSurrenderLine.Purpose = (string)config["Purpose"];
                        imprestSurrenderLine.Amount = ((decimal)config["Amount"]).ToString("N2");
                        imprestSurrenderLine.ActualSpent = ((decimal)config["Actual_Spent"]).ToString("N2");
                        imprestSurrenderLine.ActualSpentLCY = ((decimal)config["Actual_Spent_LCY"]).ToString("C", new CultureInfo("sw-KE"));
                        imprestSurrenderLine.RemainingAmountLCY = ((decimal)config["Remaining_Amount_LCY"]).ToString("C", new CultureInfo("sw-KE"));
                        imprestSurrenderLine.ReceiptNo = (string)config["Receipt_No"];
                        imprestSurrenderLine.CashReceiptAmount = ((decimal)config["Cash_Receipt_Amount"]).ToString("N2");
                        imprestSurrenderLine.RemainingAmount = ((decimal)config["Remaining_Amount"]).ToString("N2");
                        imprestSurrenderLines.Add(imprestSurrenderLine);
                    }
                }
                #endregion

                LoadReceipts(applyToDocumentNumber);
                var lines = new ImprestSurrenderLinesList
                {
                    Status = Status,
                    ListOfImprestSurrenderLines = imprestSurrenderLines
                };
                return PartialView("~/Views/ImprestSurrender/ImprestSurrenderLineView.cshtml", lines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateSurrenderLine(string documentNo, string lineNo, string actualSpent, string receiptNo)
        {
            try
            {

                var staffNumber = Session["Username"].ToString();
                var test = 0;
                Credentials.ObjNav.updateSurrenderLine(staffNumber, documentNo, Convert.ToInt32(lineNo), Convert.ToDecimal(actualSpent), receiptNo);
                var msg = "Imprest Surrender line updated Successfully";
                return Json(new { message = msg, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SendImprestSurrenderForApproval(string DocNo)
        {
            try
            {
                {
                    var employee = Session["EmployeeData"] as EmployeeView;
                    var staffNumber = Session["Username"].ToString();
                    var userId = employee?.UserID;
                    Credentials.ObjNav.fnSendImprestSurrenderApproval(staffNumber, DocNo);
                    Credentials.ObjNav.UpdateApprovalEntrySenderID(57000, DocNo, userId);
                    return Json(new { message = "Imprest Surrender, Document No " + DocNo + " sent for approval Successfully", success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateImprestSurrender(string DocNo)
        {
            try
            {
                {
                    var employee = Session["EmployeeData"] as EmployeeView;
                    var userId = employee?.UserID;
                    // Credentials.ObjNav.(DocNo);
                    Credentials.ObjNav.UpdateApprovalEntrySenderID(57000, DocNo, userId);
                    return Json(new { message = "Imprest Surrender, Document No " + DocNo + " sent for approval Successfully", success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CancelImprestSurrenderForApproval(string DocNo)
        {
            try
            {
                Credentials.ObjNav.CancelFullUtilVoucher(DocNo);
                return Json(new { message = "Document approval cancelled Successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult FileUploadForm()
        {
            return PartialView("~/Views/ImprestSurrender/FileAttachmentForm.cshtml");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ModifyImprestReceipt(ImprestReceipt receipt)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var userId = employee?.UserID;

                Credentials.ObjNav.updateReceipt(
                  receipt.DocNo,
                  receipt.PayMode,
                  receipt.PayRef
                );
                Credentials.ObjNav.UpdateApprovalEntrySenderID(56007, receipt.DocNo, userId);

                if (receipt.Status == "Open")
                {
                    //Credentials.ObjNav.ReceiptsendApprovalRequest(receipt.DocNo);
                }
                return Json(new { message = "Record submitted.", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ModifyImprestSurrender(string DocNo, string PostingDate)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var userId = employee?.UserID;
                /*DateTime postingDate = DateTime.ParseExact(PostingDate.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);*/
                // Credentials.ObjNav.updateApprovedPayment(DocNo, postingDate);
                return Json(new { message = "Imprest Surrender Modified Successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        protected void LoadPayModes()
        {
            try
            {
                var workPlanActivities = new List<DropdownList>();
                var pageWp = "PayMode?$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    workPlanActivities.AddRange(from JObject config in details["value"] select new DropdownList { Text = (string)config["Description"], Value = (string)config["Code"] });
                }
                ((dynamic)ViewBag).Activities = workPlanActivities.Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

            }
            catch (Exception exception)
            {
                exception.Data.Clear();
            }
        }
        protected void LoadReceipts(string documentNumber)
        {
            try
            {
                var receiptsList = new List<DropdownList>();

                var pagePC = $"Receipts?$filter=applies_to_doc_no eq '{documentNumber}'&$format=json";
                //var pagePC = "Receipts?$format=json";

                var httpResponsePC = Credentials.GetOdataData(pagePC);
                using (var streamReader = new StreamReader(httpResponsePC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    receiptsList.AddRange(
                        from JObject item in details["value"]
                        select new DropdownList
                        {
                            Text = (string)item["No"],
                            Value = (string)item["No"]
                        }
                    );
                }
                ViewBag.Receipts = receiptsList.Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();
            }
            catch (Exception ex)
            {        
                Console.WriteLine($"Error loading receipts: {ex.Message}");
            }
        }

        public JsonResult GenerateImprestSurrender(string documentNumber)
        {
            try
            {
                var message = "";
                bool success = false, view = false;

                // message = Credentials.ObjNav.GenerateImprestSurrender(documentNumber);
                if (message == "")
                {
                    message = "File Not Found";
                }
                else
                {
                    success = true;
                }
                return Json(new { message, success }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult GenerateImprestReceipt(string documentNumber)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var userId = employee?.UserID;
                var receiptNumber = Credentials.ObjNav.GenerateReceipt(documentNumber);
                ViewBag.DocumentNumber = receiptNumber;
                LoadPayModes();
                var assignedAsset = new DepositReceipts();
                var page = "DepositReceipt?$filter=No eq '" + receiptNumber + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"]!)
                    {
                        var config = (JObject)jToken;
                        var amountLCYString = (string)config["Amount_LCY"];
                        decimal amountLCY;
                        var isValidAmount = decimal.TryParse(amountLCYString, out amountLCY);

                        // Format the amount with commas
                        var formattedAmountLcy = isValidAmount ? amountLCY.ToString("N0") : "";
                        assignedAsset = new DepositReceipts
                        {
                            No = (string)config["No"],
                            Date = (string)config["Date"],
                            /* Date = DateTime
                                 .ParseExact((string)config["Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                                 .ToString("dd/MM/yyyy"),*/
                            PayMode = (string)config["Pay_Mode"],
                            PaymentReference = (string)config["Payment_Reference"],
                            PostedDate = (string)config["Posting_Date"],
                            /*PostedDate = DateTime.ParseExact((string)config["Posting_Date"], "yyyy-MM-dd",
                                CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),*/
                            Status = (string)config["Status"]
                        };
                    }
                }
                return PartialView("PartialViews/GenerateImprestReceipt", assignedAsset);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("Partial Views/ErroMessangeView", erroMsg);
            }
        }
        public JsonResult GenerateImprestReceipt2(string documentNumber)
        {
            try
            {
                var res = "";
                var message = "Receipt Generated";
                bool success = false;
                Credentials.ObjNav.GenerateReceipt(documentNumber);
                return Json(new { message, success }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult InitiateStaffClaim(string documentNumber)
        {
            try
            {
                var message = "";
                bool success = false;
                var employee = Session["EmployeeData"] as EmployeeView;
                var staffNumber = Session["Username"].ToString();
                var userId = employee?.UserID;
                message = Credentials.ObjNav.InitiateStaffClaim(documentNumber, userId);
                if (message=="")
                {
                    return Json(new { message, success }, JsonRequestBehavior.AllowGet);
                }
                else {
                    success = true;
                    return Json(new { message, success }, JsonRequestBehavior.AllowGet);
                }
                    
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}