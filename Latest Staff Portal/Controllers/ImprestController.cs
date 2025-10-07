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
    public class ImprestController : Controller
    {
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
                foreach (JObject config in details["value"])
                    ImpAcc = (string)config["Imprest_Account"];

            return ImpAcc;
        }
        public PartialViewResult ImprestLines(string docNo, string glAccount, int lineNo, string Status)
        {
            try
            {
                #region Imp Lines

                var imprestLines = new List<ImprestLine>();
                var pageLine =
                    $"ExpenseImprestLines?$filter=Document_No eq '{docNo}' and Source_Line_No eq {lineNo}&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var imprest = new ImprestLine
                        {
                            DocumentNo = config["Document_No"]?.ToString(),
                            LineNo = Convert.ToInt32(config["Line_No"]),
                            Payee = config["Payee"]?.ToString(),
                            EmployeeNo = config["Employee_No"]?.ToString(),
                            EmployeeName = config["Employee_Name"]?.ToString(),
                            JobGroup = config["Job_Group"]?.ToString(),
                            GLAccount = config["G_L_Account"]?.ToString(),
                            Destination = config["Destination"]?.ToString(),
                            VoteItem = config["Vote_Item"]?.ToString(),
                            Quantity = Convert.ToInt32(config["Quantity"]),
                            Rate = Convert.ToDecimal(config["Rate"]),
                            Total = Convert.ToDecimal(config["Total"]),
                            Status = config["Status"]?.ToString(),
                            RecalledBy = config["Recalled_By"]?.ToString(),
                            RecalledOn = DateTime.ParseExact((string)config["Recalled_On"], "dd/MM/yyyy HH:mm:ss",
                                CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            SourceLineNo = config["Source_Line_No"]?.ToString()
                        };
                        imprestLines.Add(imprest);
                    }
                }

                #endregion

                ViewBag.SourceLineNo = lineNo;
                var Lines = new ImprestLinesList
                {
                    Status = Status,
                    ListOfImprestLines = imprestLines
                };
                return PartialView("~/Views/Imprest/PartialViews/ImprestLines.cshtml", Lines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public PartialViewResult SafariTeam(string docNo, string Status)
        {
            try
            {
                #region Imp Lines

                var safariTeams = new List<SafariTeam>();
                var pageLine = $"SafariTeam?$filter=Imprest_Memo_No eq '{docNo}'&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var safariTeam = new SafariTeam();
                        safariTeam.ImprestMemoNo = (string)config["Imprest_Memo_No"];
                        safariTeam.LineNo = Convert.ToInt32(config["Line_No"]);
                        safariTeam.WorkType = (string)config["Work_Type"];
                        safariTeam.Type = (string)config["Type"];
                        safariTeam.TypeOfExpense = (string)config["Type_of_Expense"];
                        safariTeam.No = (string)config["No"];
                        safariTeam.GLAccount = (string)config["G_L_Account"];
                        safariTeam.TaskNo = (string)config["Task_No"];
                        safariTeam.Name = (string)config["Name"];
                        safariTeam.UnitOfMeasure = (string)config["Unit_of_Measure"];
                        safariTeam.CurrencyCode = (string)config["Currency_Code"];
                        safariTeam.TimePeriod = Convert.ToInt32(config["Time_Period"]);
                        safariTeam.DirectUnitCost = Convert.ToDecimal(config["Direct_Unit_Cost"]);
                        safariTeam.Entitlement = Convert.ToDecimal(config["Entitlement"]);
                        safariTeam.TransportCosts = Convert.ToDecimal(config["Transport_Costs"]);
                        safariTeam.TotalEntitlement = Convert.ToDecimal(config["Total_Entitlement"]);
                        safariTeam.OutstandingAmount = Convert.ToDecimal(config["Outstanding_Amount"]);
                        safariTeam.TasksToCarryOut = (string)config["Tasks_to_Carry_Out"];
                        safariTeam.ExpectedOutput = (string)config["Expected_Output"];
                        safariTeam.Delivery = Convert.ToDecimal(config["Delivery"]);
                        safariTeam.ProjectLead = Convert.ToBoolean(config["Project_Lead"]);
                        safariTeams.Add(safariTeam);
                    }
                }

                #endregion

                var Lines = new SafariTeamList
                {
                    Status = Status,
                    ListOfSafariTeam = safariTeams
                };
                return PartialView("~/Views/Imprest/PartialViews/safariTeam.cshtml", Lines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public PartialViewResult NewImprestLine(string glAccount, int lineNo, string documentNo, string documentType)
        {
            try
            {
                var imprestLine = new ImprestLine();
                #region Employee

                var employeeList = new List<DropdownList>();
                //
                var pageReliever = "Resources?$filter=Type eq 'Person' and Name ne '' &format=json";

                var httpResponseReliever = Credentials.GetOdataData(pageReliever);
                using (var streamReader = new StreamReader(httpResponseReliever.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var list = new DropdownList
                        {
                            Value = (string)config["No"],
                            Text = (string)config["No"] + "-" + (string)config["Name"]
                        };
                        employeeList.Add(list);
                    }
                }

                #endregion
                #region Destinations

                var DestList = new List<DropdownList>();
                var pageDest = "WorkTypes?$format=json";

                var httpResponseDest = Credentials.GetOdataData(pageDest);
                using (var streamReader = new StreamReader(httpResponseDest.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var Dest = new DropdownList
                        {
                            Value = (string)config["Code"],
                            Text = (string)config["Description"]
                        };
                        DestList.Add(Dest);
                    }
                }

                #endregion
                imprestLine.GLAccount = glAccount;
                imprestLine.LineNo = lineNo;
                imprestLine.DocumentNo = documentNo;
                ViewBag.DocumentType = documentType;
                imprestLine.ListOfEmployees = employeeList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                imprestLine.ListOfDestinations = DestList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                return PartialView("~/Views/Imprest/PartialViews/NewImprestLine.cshtml", imprestLine);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public JsonResult SendImprestAppForApproval(string docNo)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var staffNo = employee?.No;
                var userId = employee?.UserID;
                Credentials.ObjNav.FullUtilVoucherApproval(docNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(57000, docNo, employee?.UserID);
                LogHelper.Log(docNo, userId, "Send imprest approval request");
                return Json(new { message = "Imprest Acknowledged Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelImprestAppForApproval(string DocNo)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var staffNo = employee.No;
                Credentials.ObjNav.cancelImprestApprovalRequest(staffNo, DocNo);
                return Json(new { message = "Imprest approval cancelled Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult FileUploadForm()
        {
            return PartialView("~/Views/Imprest/FileAttachmentForm.cshtml");
        }

        /// <summary>
        /// Standing Imprest
        /// </summary>
        /// <returns></returns>
        public ActionResult StandingImprest()
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

        public PartialViewResult StandingImprestList()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();

                var UserID = Session["UserID"].ToString();
                var impAccount = ImprestAccount();
                if (impAccount != "") StaffNo = impAccount;
                var standingImprests = new List<StandingImprest>();
                var employee = Session["EmployeeData"] as EmployeeView;
                var dimension1 = employee?.GlobalDimension1Code;

                var page = "StandingImprestWarranties?$filter=Shortcut_Dimension_1_Code eq '" + dimension1 + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var imprest = new StandingImprest
                        {
                            No = (string)config["No"],
                            Date = DateTime.ParseExact((string)config["Date"], "yyyy-MM-dd",
                                CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            PayMode = (string)config["Pay_Mode"],
                            ChequeNo = (string)config["Cheque_No"],
                            ChequeDate = DateTime.ParseExact((string)config["Cheque_Date"], "yyyy-MM-dd",
                                CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            Payee = (string)config["Payee"],
                            PayingBankAccount = (string)config["Paying_Bank_Account"],
                            CreatedBy = (string)config["Created_By"],
                            Status = (string)config["Status"],
                            TotalAmountLCY = (decimal)config["Total_Amount"],
                            Project = (string)config["Project"],
                            ProjectDescription = (string)config["Project_Description"],
                            ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"],
                            ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"],
                            PaymentNarration = (string)config["Payment_Narration"]


                        };
                        standingImprests.Add(imprest);
                    }
                }

                return PartialView("~/Views/Imprest/PartialViews/StandingImprestList.cshtml",
                    standingImprests.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [HttpPost]
        public ActionResult StandingImprestDocument(string docNo)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");
            try
            {
                var imprest = new StandingImprest();

                var page = "StandingImprest?$filter=No eq '" + docNo + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        imprest.No = (string)config["No"];
                        imprest.Date = DateTime.ParseExact((string)config["Date"], "yyyy-MM-dd",
                            CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprest.PayMode = (string)config["Pay_Mode"];
                        imprest.ChequeNo = (string)config["Cheque_No"];
                        imprest.ChequeDate = DateTime.ParseExact((string)config["Cheque_Date"], "yyyy-MM-dd",
                            CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprest.PostingDate = DateTime.ParseExact((string)config["Posting_Date"], "yyyy-MM-dd",
                            CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprest.StandingImprestType = (string)config["Standing_Imprest_Type"];
                        imprest.PayingBankAccount = (string)config["Paying_Bank_Account"];
                        imprest.BankName = (string)config["Bank_Name"];
                        imprest.PaymentNarration = (string)config["Payment_Narration"];
                        imprest.CurrencyCode = (string)config["Currency_Code"];
                        imprest.TotalAmount = ((decimal)config["Total_Amount"]).ToString("C", new CultureInfo("sw-KE"));
                        imprest.ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"];
                        imprest.DepartmentName = (string)config["Department_Name"];
                        imprest.ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"];
                        imprest.ProjectName = (string)config["Project_Name"];
                        imprest.StrategicPlan = (string)config["Strategic_Plan"];
                        imprest.ReportingYearCode = (string)config["Reporting_Year_Code"];
                        imprest.WorkplanCode = (string)config["Workplan_Code"];
                        imprest.ActivityCode = (string)config["Activity_Code"];
                        imprest.Status = (string)config["Status"];
                        imprest.Posted = (bool)config["Posted"];
                        imprest.PostedBy = (string)config["Posted_By"];
                        imprest.PostedDate = DateTime.ParseExact((string)config["Posted_Date"], "yyyy-MM-dd",
                            CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprest.DimensionSetId = (string)config["Dimension_Set_ID"];
                        imprest.ExpenditureRequisitionCode = (string)config["Expenditure_Requisition_Code"];
                        imprest.AvailableAmount = (string)config["Available_Amount"];
                        imprest.CommittedAmount = (string)config["Committed_Amount"];
                        imprest.AieReceipt = (string)config["AIE_Receipt"];

                    }
                }

                imprest.CourtStation = DimensinValuesList.GetDimensionValueName(imprest.ShortcutDimension1Code);
                imprest.AdminUnit = DimensinValuesList.GetDimensionValueName(imprest.ShortcutDimension2Code);

                return View(imprest);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public PartialViewResult StandingImprestLine(string docNo, string status)
        {
            try
            {
                var standingImprestLines = new List<StandingImprestLine>();

                var page = "StandingImprestLines?$filter=No eq '" + docNo + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var imprestLine = new StandingImprestLine();
                        imprestLine.No = (string)config["No"];
                        imprestLine.LineNo = (int)config["Line_No"];
                        imprestLine.AccountType = (string)config["Account_Type"];
                        imprestLine.AccountNo = (string)config["Account_No"];
                        imprestLine.AccountName = (string)config["Account_Name"];
                        imprestLine.Description = (string)config["Description"];
                        imprestLine.Amount = (decimal)config["Amount"];
                        imprestLine.ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"];
                        imprestLine.ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"];
                        imprestLine.PayeeBankCode = (string)config["Payee_Bank_Code"];
                        imprestLine.PayeeBankName = (string)config["Payee_Bank_Name"];
                        imprestLine.PayeeBankBranchCode = (string)config["Payee_Bank_Branch_Code"];
                        imprestLine.PayeeBankBranchName = (string)config["Payee_Bank_Branch_Name"];
                        imprestLine.PayeeBankAccountNo = (string)config["Payee_Bank_Account_No"];
                        imprestLine.PayeeBankAccName = (string)config["Payee_Bank_Acc_Name"];
                        imprestLine.ValidatedBankName = (string)config["rbankName"];
                        imprestLine.Status = status;

                        standingImprestLines.Add(imprestLine);
                    }
                }

                return PartialView("~/Views/Imprest/PartialViews/StandingImprestLines.cshtml",
                    standingImprestLines.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        /// <summary>
        /// Imprest Warrant
        /// </summary>
        /// <returns></returns>
        public ActionResult ImprestWarrant(string userType)
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                ViewBag.UserType = userType ?? "Accounts";
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

       /* public PartialViewResult ImprestWarrantList()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var page = "";

                var UserID = Session["UserID"].ToString();
                var impAccount = ImprestAccount();
                if (impAccount != "") StaffNo = impAccount;
                var imprestWarranties = new List<ImprestWarranties>();
                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var employee = Session["EmployeeData"] as EmployeeView;
                var station = employee?.GlobalDimension1Code; 
                page = "ImprestWarranties?$filter=Account_No eq '" + StaffNo + "' and Status eq 'Released'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var imprest = new ImprestWarranties();
                        imprest.No = (string)config["No"];
                        imprest.Date = DateTime
                            .ParseExact((string)config["Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.PostingDate = DateTime
                            .ParseExact((string)config["Posting_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.AccountType = (string)config["Account_Type"];
                        imprest.AccountNo = (string)config["Account_No"];
                        imprest.AccountName = (string)config["Account_Name"];
                        imprest.ReferenceNo = (string)config["Reference_No"];
                        imprest.PayMode = (string)config["Pay_Mode"];
                        imprest.ChequeNo = (string)config["Cheque_No"];
                        imprest.PayingBankAccount = (string)config["Paying_Bank_Account"];
                        imprest.BankName = (string)config["Bank_Name"];
                        imprest.DimensionSetId = (string)config["Dimension_Set_ID"];
                        imprest.TravelDate = DateTime
                            .ParseExact((string)config["Travel_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.PaymentNarration = (string)config["Payment_Narration"];
                        imprest.CreatedBy = (string)config["Created_By"];
                        imprest.Status = (string)config["Status"];
                        imprest.StrategicPlan = (string)config["Strategic_Plan"];
                        imprest.ReportingYearCode = (string)config["Reporting_Year_Code"];
                        imprest.WorkplanCode = (string)config["Workplan_Code"];
                        imprest.ActivityCode = (string)config["Activity_Code"];
                        imprest.ExpenditureRequisitionCode = (string)config["Expenditure_Requisition_Code"];
                        imprest.ImprestMemoNo = (string)config["Imprest_Memo_No"];
                        imprest.ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"];
                        imprest.ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"];
                        imprest.ImprestAmount = ((decimal)config["Imprest_Amount"]).ToString("C", new CultureInfo("sw-KE"));
                        imprest.Posted = (string)config["Posted"];
                        imprest.ValidatedBankName = (string)config["ValidatedBankName"];
                        imprest.ImprestDeadline = DateTime
                            .ParseExact((string)config["Imprest_Deadline"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");

                        imprestWarranties.Add(imprest);
                    }
                }

                return PartialView("~/Views/Imprest/PartialViews/ImprestWarrantList.cshtml",
                    imprestWarranties.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }*/
        [HttpPost]
        /*public ActionResult ImprestWarrantDocument(string docNo)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");
            try
            {
                var imprest = new ImprestWarranties();
                var page = "ImprestWarranties?$filter=No eq '" + docNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        imprest.No = (string)config["No"];
                        imprest.Date = DateTime.ParseExact((string)config["Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprest.PostingDate = DateTime.ParseExact((string)config["Posting_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprest.AccountType = (string)config["Account_Type"];
                        imprest.AccountNo = (string)config["Account_No"];
                        imprest.AccountName = (string)config["Account_Name"];
                        imprest.ReferenceNo = (string)config["Reference_No"];
                        imprest.PayMode = (string)config["Pay_Mode"];
                        imprest.ChequeNo = (string)config["Cheque_No"];
                        imprest.PayingBankAccount = (string)config["Paying_Bank_Account"];
                        imprest.BankName = (string)config["Bank_Name"];
                        imprest.TravelDate = DateTime.ParseExact((string)config["Travel_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprest.PaymentNarration = (string)config["Payment_Narration"];
                        imprest.CreatedBy = (string)config["Created_By"];
                        imprest.Status = (string)config["Status"];
                        imprest.StrategicPlan = (string)config["Strategic_Plan"];
                        imprest.ReportingYearCode = (string)config["Reporting_Year_Code"];
                        imprest.WorkplanCode = (string)config["Workplan_Code"];
                        imprest.ActivityCode = (string)config["Activity_Code"];
                        imprest.ExpenditureRequisitionCode = (string)config["Expenditure_Requisition_Code"];
                        imprest.ImprestMemoNo = (string)config["Imprest_Memo_No"];
                        imprest.ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"];
                        imprest.ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"];
                        imprest.ValidatedBankName = (string)config["ValidatedBankName"];
                        imprest.ImprestAmount = ((decimal)config["Imprest_Amount"]).ToString("C", new CultureInfo("sw-KE"));
                        imprest.AvailableAmount = ((decimal)config["Available_Amount"]).ToString("C", new CultureInfo("sw-KE"));
                        imprest.CommittedAmount = ((decimal)config["Committed_Amount"]).ToString("C", new CultureInfo("sw-KE"));
                        imprest.DimensionSetId = (string)config["Dimension_Set_ID"];
                        imprest.Posted = (string)config["Posted"];
                        imprest.ImprestDeadline = DateTime.ParseExact((string)config["Imprest_Deadline"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        imprest.AieReceipt = (string)config["AIE_Receipt"];
                    }
                }
                imprest.LocationName = DimensinValuesList.GetDimensionValueName(imprest.ShortcutDimension1Code);
                imprest.AdminUnitName = DimensinValuesList.GetDimensionValueName(imprest.ShortcutDimension2Code);

                return View(imprest);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }*/
      /*  [HttpPost]*/
       /* public ActionResult ImprestWarrantUserDocument(string docNo)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");
            try
            {
                var imprest = new ImprestWarranties();

                var page = "PaymentsQuery?$filter=No eq '" + docNo + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        imprest.No = (string)config["No"];
                        imprest.Date = DateTime
                            .ParseExact((string)config["ImprestDeadline"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.PostingDate = DateTime
                            .ParseExact((string)config["PostingDate"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.AccountType = (string)config["AccountType"];
                        imprest.AccountNo = (string)config["AccountNo"];
                        imprest.AccountName = (string)config["AccountName"];
                        imprest.ReferenceNo = (string)config["ReferenceNo"];
                        imprest.AdminUnitName = (string)config["ShortcutDimension2Code"];
                        imprest.PayMode = (string)config["PayMode"];
                        imprest.ChequeNo = (string)config["ChequeNo"];
                        imprest.PayingBankAccount = (string)config["PayingBankAccount"];
                        imprest.BankName = (string)config["BankName"];
                        imprest.TravelDate = DateTime
                            .ParseExact((string)config["TravelDate"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.PaymentNarration = (string)config["PaymentNarration"];
                        imprest.CreatedBy = (string)config["CreatedBy"];
                        imprest.Status = (string)config["Status"];
                        imprest.StrategicPlan = (string)config["StrategicPlan"];
                        imprest.ReportingYearCode = (string)config["ReportingYearCode"];
                        imprest.WorkplanCode = (string)config["WorkplanCode"];
                        imprest.ActivityCode = (string)config["ActivityCode"];
                        imprest.ExpenditureRequisitionCode = (string)config["ExpenditureRequisitionCode"];
                        imprest.ImprestMemoNo = (string)config["ImprestMemoNo"];
                        imprest.ShortcutDimension1Code = (string)config["ShortcutDimension1Code"];
                        imprest.ShortcutDimension2Code = (string)config["Shortcut_Dimension2Code"];
                        imprest.ImprestAmount = ((decimal)config["ImprestAmount"]).ToString("C", new CultureInfo("sw-KE"));
                        imprest.ImprestDeadline = DateTime
                            .ParseExact((string)config["ImprestDeadline"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.DimensionSetId = (string)config["DimensionSetID"];
                        imprest.ValidatedBankName = (string)config["ValidatedBankName"];
                        imprest.Payee = (string)config["Payee"];
                        imprest.PayeeBankAccount = (string)config["PayeeBankAccount"];
                        imprest.PayeeBankName = CommonClass.GetEmployeeBankName((string)config["PayeeBankCode"]);
                        imprest.PayeeBranchName = CommonClass.GetEmployeeBankBranchName((string)config["PayeeBankCode"], (string)config["PayeeBankBranch"]);
                    }
                }

                imprest.LocationName = DimensinValuesList.GetDimensionValueName(imprest.ShortcutDimension1Code);
                imprest.AdminUnitName = DimensinValuesList.GetDimensionValueName(imprest.ShortcutDimension2Code);

                return View(imprest);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }*/
        public string GetValidatedBankAccountName(string DocNo)
        {
            var Name = "";
            var page = "ImprestWarranties?$filter=No eq '" + DocNo + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Any())
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    Name = (string)config["ValidatedBankName"];
                }

            return Name;
        }
        /*public PartialViewResult ImprestWarrantLine(string docNo, string Status)
        {
            try
            {
                #region Imp warrant Lines

                var warrantImprestLines = new List<WarrantImprestLines>();

                var page = "WarrantImprestLines?$filter=No eq '" + docNo + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var imprestLine = new WarrantImprestLines();
                        imprestLine.No = (string)config["No"];
                        imprestLine.LineNo = (int)config["Line_No"];
                        imprestLine.AdvanceType = (string)config["Advance_Type"];
                        imprestLine.AccountType = (string)config["Account_Type"];
                        imprestLine.AccountNo = (string)config["Account_No"];
                        imprestLine.AccountName = (string)config["Account_Name"];
                        imprestLine.Purpose = (string)config["Purpose"];
                        imprestLine.DailyRate = (decimal)config["Daily_Rate"];
                        imprestLine.NoOfDays = (int)config["No_of_Days"];
                        imprestLine.CurrencyCode = (string)config["Currency_Code"];
                        imprestLine.Amount = (decimal)config["Amount"];
                        imprestLine.Project = (string)config["Project"];
                        imprestLine.TaskNo = (string)config["Task_No"];
                        warrantImprestLines.Add(imprestLine);
                    }
                }

                #endregion

                var Lines = new WarrantImprestLinesList
                {
                    Status = Status,
                    ListOfWarrantImprestLines = warrantImprestLines
                };
                return PartialView("~/Views/Imprest/PartialViews/WarrantImprestLines.cshtml", Lines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
*/
        public ActionResult SendImprestApprovalRequest(string docNo)
        {
            var successVal = false;
            try
            {
                if (Session["UserID"] == null || Session["Username"] == null) return RedirectToAction("Login", "Login");
                var employee = Session["EmployeeData"] as EmployeeView;
                var userId = employee?.UserID;
                Credentials.ObjNav.FullUtilVoucherApproval(docNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(57000, docNo, userId);
                LogHelper.Log(docNo, userId, "Send imprest warrant approval");
                return Json(new { message = "Success", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (successVal) Session["ErrorMsg"] = ex.Message.Replace("'", "");
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult CancelImprestApprovalRequest(string docNo)
        {
            var successVal = false;
            try
            {
                if (Session["UserID"] == null || Session["Username"] == null) return RedirectToAction("Login", "Login");
                var employee = Session["EmployeeData"] as EmployeeView;
                var userId = employee?.UserID;
                Credentials.ObjNav.CancelFullUtilVoucher(docNo);
                LogHelper.Log(docNo, userId, "Cancel imprest warrant approval");
                return Json(new { message = "Approval request cancelled successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SubmitImprestLine(ImprestLine imprestLine, bool isclaim)
        {
            try
            {
                // Initialize variables with default values
                var glAccount = string.Empty;
                var lineNo = 0;
                var employeeNo = string.Empty;
                var employeeName = string.Empty;
                var voteItem = string.Empty;
                var destination = string.Empty;
                var quantity = 0;


                // Extracting and validating fields from the model
                if (imprestLine.GLAccount != null) glAccount = imprestLine.GLAccount;

                if (imprestLine.LineNo != null) lineNo = imprestLine.LineNo;

                if (imprestLine.EmployeeNo != null) employeeNo = imprestLine.EmployeeNo;

                if (imprestLine.EmployeeName != null) employeeName = imprestLine.EmployeeName;

                if (imprestLine.VoteItem != null) voteItem = imprestLine.VoteItem;

                if (imprestLine.Destination != null) destination = imprestLine.Destination;

                if (imprestLine.Quantity != null) quantity = imprestLine.Quantity;
                var type = 0;
                if (isclaim)
                {
                    quantity = 1;
                    type = 3;
                }

                // Get user details from session
                var staffNo = Session["Username"]?.ToString() ?? string.Empty;
                var employee = Session["EmployeeData"] as EmployeeView;
                var globalDimension1Code = employee?.GlobalDimension1Code ?? string.Empty;
                var userId = employee?.UserID ?? string.Empty;

                Credentials.ObjNav.InsertExpenseLinesImprest(
                    imprestLine.DocumentNo,
                    employeeNo,
                    destination,
                    glAccount,
                    quantity,
                    imprestLine.Rate,
                    destination,
                    glAccount, lineNo, type
                );
                LogHelper.Log(imprestLine.DocumentNo, userId, "insert staff claim line");


                return Json(new { message = "Success", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetImprestRates(string EmpNo, string Destination)
        {
            try
            {
                decimal amount = 0;
                amount = Credentials.ObjNav.GetDSARates(EmpNo, Destination);

                return Json(new { amount, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Special Imprest Functions
        /// </summary>


        public ActionResult SafariImprestWarrant()
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
        public PartialViewResult SafariImprestWarrantList()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();

                var UserID = Session["UserID"].ToString();
                var impAccount = ImprestAccount();
                if (impAccount != "") StaffNo = impAccount;
                var safariImprest = new List<SafariImprest>();
                var employee = Session["EmployeeData"] as EmployeeView;
                var dimension1 = employee?.GlobalDimension1Code;

                //var page = $"SafariImprestWarranty?$filter=Status eq '{status}' and Created_By eq '{UserID}'&$format=json";
                var page = $"SafariImprest?$Created_By eq '{UserID}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var imprest = new SafariImprest
                        {
                            No = (string)config["No"],
                            Warrant_Voucher_Type = (string)config["Warrant_Voucher_Type"],
                            Warrant_No = (string)config["Warrant_No"],
                            Date = (string)config["Date"],
                            Subject = (string)config["Subject"],
                            Objective = (string)config["Objective"],
                            Terms_of_Reference = (string)config["Terms_of_Reference"],
                            Imprest_Naration = (string)config["Imprest_Naration"],
                            User_ID = (string)config["User_ID"],
                            Requestor = (string)config["Requestor"],
                            Requestor_Name = (string)config["Requestor_Name"],
                            HOD = (bool)(config["HOD"] ?? false),
                            Start_Date = (string)config["Start_Date"],
                            No_of_days = (int)(config["No_of_days"] ?? 0),
                            End_Date = (string)config["End_Date"],
                            Return_Date = (string)config["Return_Date"],
                            Due_Date = (string)config["Due_Date"],
                            Total_Subsistence_Allowance = (int)(config["Total_Subsistence_Allowance"] ?? 0),
                            Total_Fuel_Costs = (int)(config["Total_Fuel_Costs"] ?? 0),
                            Total_Maintenance_Costs = (int)(config["Total_Maintenance_Costs"] ?? 0),
                            Total_Casuals_Cost = (int)(config["Total_Casuals_Cost"] ?? 0),
                            Total_Other_Costs = (int)(config["Total_Other_Costs"] ?? 0),
                            Status = (string)config["Status"],
                            Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                            Department_Name = (string)config["Department_Name"],
                            Global_Dimension_2_Code = (string)config["Global_Dimension_2_Code"],
                            Project_Name = (string)config["Project_Name"],
                            Dimension_Set_ID = (int)(config["Dimension_Set_ID"] ?? 0),
                            Strategic_Plan = (string)config["Strategic_Plan"],
                            Reporting_Year_Code = (string)config["Reporting_Year_Code"],
                            Workplan_Code = (string)config["Workplan_Code"],
                            Activity_Code = (string)config["Activity_Code"],
                            Expenditure_Requisition_Code = (string)config["Expenditure_Requisition_Code"],
                            Reason_to_Reopen = (string)config["Reason_to_Reopen"],
                            From = (string)config["From"],
                            Destination = (string)config["Destination"],
                            Time_Out = (string)config["Time_Out"],
                            Journey_Route = (string)config["Journey_Route"],
                            Work_Type_Filter = (string)config["Work_Type_Filter"]

                        };
                        safariImprest.Add(imprest);
                    }
                }

                return PartialView("~/Views/Imprest/PartialViews/SafariImprestWarrantList.cshtml",
                    safariImprest.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult ApprovedSafariImprestWarrant()
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
        public PartialViewResult ApprovedSafariImprestWarrantList()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();

                var UserID = Session["UserID"].ToString();
                var impAccount = ImprestAccount();
                if (impAccount != "") StaffNo = impAccount;
                var approvedImprest = new List<ApprovedSafariImprest>();
                var employee = Session["EmployeeData"] as EmployeeView;
                var dimension1 = employee?.GlobalDimension1Code;
                var account = employee?.BankAccountNumber2;
                var account2 = employee?.BankAccountNumber;


                var page = "";
                if (dimension1 == "FINANCE")
                    page = "ApprovedSafariImprestWarrant?$format=json";
                else
                    page = "ApprovedSafariImprestWarrant?$filter=Created_By eq '" + UserID + "'&$format=json";



                //var page = $"SafariImprestWarranty?$filter=Status eq '{status}' &$format=json";
                //var page = "ApprovedSafariImprestWarrant?$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var imprest = new ApprovedSafariImprest
                        {
                            No = (string)config["No"],
                            Date = DateTime.ParseExact((string)config["Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            Account_Type = (string)config["Account_Type"],
                            Account_No = (string)config["Account_No"],
                            Account_Name = (string)config["Account_Name"],
                            Imprest_Bank_Account_Number = (string)config["Imprest_Bank_Account_Number"],
                            Imprest_Bank_Name = (string)config["Imprest_Bank_Name"],
                            Imprest_Bank_Branch_Name = (string)config["Imprest_Bank_Branch_Name"],
                            Pay_Mode = (string)config["Pay_Mode"],
                            Paying_Bank_Account = (string)config["Paying_Bank_Account"],
                            Bank_Name = (string)config["Bank_Name"],
                            Cheque_No = (string)config["Cheque_No"],
                            Cheque_Date = DateTime.ParseExact((string)config["Cheque_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            Posting_Date = DateTime.ParseExact((string)config["Posting_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            Payee = (string)config["Payee"],
                            Reference_No = (string)config["Reference_No"],
                            Payment_Narration = (string)config["Payment_Narration"],
                            Available_Amount = Convert.ToDecimal(config["Available_Amount"]),
                            Committed_Amount = Convert.ToDecimal(config["Committed_Amount"]),
                            AIE_Receipt = (string)config["AIE_Receipt"],
                            Created_By = (string)config["Created_By"],
                            Travel_Date = DateTime.ParseExact((string)config["Travel_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            Status = (string)config["Status"],
                            Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                            Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                            Currency_Code = (string)config["Currency_Code"],
                            Imprest_Amount = Convert.ToDecimal(config["Imprest_Amount"]),
                            Imprest_Deadline = DateTime.ParseExact((string)config["Imprest_Deadline"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        };
                        approvedImprest.Add(imprest);
                    }
                }

                return PartialView("~/Views/Imprest/PartialViews/ApprovedSafariImprestWarrantList.cshtml",
                    approvedImprest.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

       /* public ActionResult SafariImprestWarrantDocument(string docNo)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");
            try
            {
                var imprest = new ImprestWarranties();

                var page = "PaymentsQuery?$filter=No eq '" + docNo + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        imprest.No = (string)config["No"];
                        imprest.Date = DateTime
                            .ParseExact((string)config["ImprestDeadline"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.PostingDate = DateTime
                            .ParseExact((string)config["PostingDate"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.AccountType = (string)config["AccountType"];
                        imprest.AccountNo = (string)config["AccountNo"];
                        imprest.AccountName = (string)config["AccountName"];
                        imprest.ReferenceNo = (string)config["ReferenceNo"];
                        imprest.AdminUnitName = (string)config["ShortcutDimension2Code"];
                        imprest.PayMode = (string)config["PayMode"];
                        imprest.ChequeNo = (string)config["ChequeNo"];
                        imprest.PayingBankAccount = (string)config["PayingBankAccount"];
                        imprest.BankName = (string)config["BankName"];
                        imprest.TravelDate = DateTime
                            .ParseExact((string)config["TravelDate"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.PaymentNarration = (string)config["PaymentNarration"];
                        imprest.CreatedBy = (string)config["CreatedBy"];
                        imprest.Status = (string)config["Status"];
                        imprest.StrategicPlan = (string)config["StrategicPlan"];
                        imprest.ReportingYearCode = (string)config["ReportingYearCode"];
                        imprest.WorkplanCode = (string)config["WorkplanCode"];
                        imprest.ActivityCode = (string)config["ActivityCode"];
                        imprest.ExpenditureRequisitionCode = (string)config["ExpenditureRequisitionCode"];
                        imprest.ImprestMemoNo = (string)config["ImprestMemoNo"];
                        imprest.ShortcutDimension1Code = (string)config["ShortcutDimension1Code"];
                        imprest.ShortcutDimension2Code = (string)config["Shortcut_Dimension2Code"];
                        imprest.ImprestAmount = ((decimal)config["ImprestAmount"]).ToString("C", new CultureInfo("sw-KE"));
                        imprest.ImprestDeadline = DateTime
                            .ParseExact((string)config["ImprestDeadline"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        imprest.DimensionSetId = (string)config["DimensionSetID"];
                        imprest.ValidatedBankName = (string)config["ValidatedBankName"];
                        imprest.Payee = (string)config["Payee"];
                        imprest.PayeeBankAccount = (string)config["PayeeBankAccount"];
                        imprest.PayeeBankName = CommonClass.GetEmployeeBankName((string)config["PayeeBankCode"]);
                        imprest.PayeeBranchName = CommonClass.GetEmployeeBankBranchName((string)config["PayeeBankCode"], (string)config["PayeeBankBranch"]);
                    }
                }

                imprest.LocationName = DimensinValuesList.GetDimensionValueName(imprest.ShortcutDimension1Code);
                imprest.AdminUnitName = DimensinValuesList.GetDimensionValueName(imprest.ShortcutDimension2Code);

                return View(imprest);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

*/

        public ActionResult SpecialImprest()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error
                {
                    Message = ex.Message.Replace("'", "")
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult SpecialImprestList()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var page = "";

                var UserID = Session["UserID"].ToString();
                var impAccount = ImprestAccount();
                if (impAccount != "") StaffNo = impAccount;
                var specialImprests = new List<SpecialImprest>();
                var employee = Session["EmployeeData"] as EmployeeView;
                var dimension1 = employee.GlobalDimension1Code;
                var userId = employee.UserID;
                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                if (role.Station_Accountant || role.HQ_Accountant)
                    page = $"SpecialImprestWarranty?$filter=Shortcut_Dimension_1_Code eq '{dimension1}'&$format=json";
                else
                    page = $"SpecialImprestWarranty?$filter=Created_By eq '{userId}'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var imprest = new SpecialImprest();
                        imprest.No = (string)config["No"];
                        imprest.Date = (string)config["Date"];
                        imprest.PostingDate = (string)config["Posting_Date"];
                        imprest.PaymentNarration = (string)config["Payment_Narration"];
                        imprest.TotalAmount = ((decimal)config["Total_Amount"]).ToString("C", new CultureInfo("sw-KE"));
                        imprest.Status = (string)config["Status"];
                        imprest.Posted = (bool)config["Posted"];
                        specialImprests.Add(imprest);
                    }
                }

                return PartialView("~/Views/Imprest/PartialViews/SpecialImprestList.cshtml",
                    specialImprests.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [HttpPost]
        public ActionResult SpecialImprestDocument(string docNo)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");
            try
            {
                var imprest = new SpecialImprest();

                var page = "SpecialImprestWarranty?$filter=No eq '" + docNo + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        imprest = new SpecialImprest
                        {
                            No = (string)config["No"],
                            Date = (string)config["Date"],
                            PostingDate = (string)config["Posting_Date"],
                            StandingImprestType = (string)config["Standing_Imprest_Type"],
                            ChequeDate = (string)config["Cheque_Date"],
                            PayingBankAccount = (string)config["Paying_Bank_Account"],
                            BankName = (string)config["Bank_Name"],
                            PaymentNarration = (string)config["Payment_Narration"],
                            CurrencyCode = (string)config["Currency_Code"],
                            TotalAmount = ((decimal)config["Total_Amount"]).ToString("C", new CultureInfo("sw-KE")),
                            ShortcutDimension1Code = (string)config["Shortcut_Dimension_1_Code"],
                            ShortcutDimension2Code = (string)config["Shortcut_Dimension_2_Code"],
                            StrategicPlan = (string)config["Strategic_Plan"],
                            ReportingYearCode = (string)config["Reporting_Year_Code"],
                            WorkplanCode = (string)config["Workplan_Code"],
                            ActivityCode = (string)config["Activity_Code"],
                            ExpenditureRequisitionCode = (string)config["Expenditure_Requisition_Code"],
                            Status = (string)config["Status"],
                            Posted = (bool)config["Posted"],
                            PostedBy = (string)config["Posted_By"],
                            PostedDate = (string)config["Posted_Date"],
                            PayMode = (string)config["Pay_Mode"],
                            Payee = (string)config["Payee"],
                            DimensionSetId = (string)config["Dimension_Set_ID"],
                            AvailableAmount = ((decimal)config["Available_Amount"]).ToString("C", new CultureInfo("sw-KE")),
                            CommittedAmount = ((decimal)config["Committed_Amount"]).ToString("C", new CultureInfo("sw-KE")),
                            AieReceipt = (string)config["AIE_Receipt"],
                        };
                    }
                }
                return View(imprest);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult CreateWarranties(string DocNo)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var userId = employee?.UserID;
                Credentials.ObjNav.CreateWaranties(userId, DocNo);
                LogHelper.Log(DocNo, userId, "create imprest warranty");
                return Json(new { message = "Imprest warranty created successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PostWarrant(string DocNo)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var userId = employee?.UserID;
                Credentials.ObjNav.PostImprest(DocNo);
                LogHelper.Log(DocNo, userId, "post imprest warrant");
                return Json(new { message = "Warrant Posted Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteImprestExpense(string DocNo, int lineNo)
        {
            try
            {
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string empNo = employee.No;
                //Credentials.ObjNav.d(DocNo, lineNo);
                return Json(new { message = "Imprest Line Deleted Successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}