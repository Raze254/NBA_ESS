using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    [CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]
    public class ExpenseRequisitionController : Controller
    {
        public ActionResult ExpenseRequisition()
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
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }

        public PartialViewResult ExpenseRequisitionList()
        {
            var employee = Session["EmployeeData"] as EmployeeView;
            var userId = employee?.UserID;
            var rsrceReq = "";
            var role = Session["ESSRoleSetup"] as ESSRoleSetup;
            var dimension1 = employee?.GlobalDimension1Code;
            var expenseRequisitions = new List<ExpenseRequisition>();
            rsrceReq = "ExpenseRequisitionCard?$filter=Created_By eq '" + userId + "' &$format=json";

            var httpResponse = Credentials.GetOdataData(rsrceReq);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                expenseRequisitions.AddRange(from JObject config in details["value"]
                    select new ExpenseRequisition
                    {
                        No = (string)config["No"],
                        Description = (string)config["Description"],
                        CorporateStrategy = (string)config["Corporate_Strategy"],
                        ReportingPeriod = (string)config["Reporting_Period"],
                        BudgetCode = (string)config["Budget_Code"],
                        Date = DateTime.ParseExact((string)config["Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy"),
                        GlobalDimension1Code = (string)config["Global_Dimension_1_Code"],
                        GlobalDimension2Code = (string)config["Global_Dimension_2_Code"],
                        Workplan = (string)config["Workplan"],
                        ActivityCode = (string)config["Activity_Code"],
                        ActivityDescription = (string)config["Activity_Description"],
                        RequiresImprest = bool.Parse((string)config["Requires_Imprest"]),
                        ImprestType = (string)config["Imprest_Type"],
                        StartDate = DateTime.ParseExact((string)config["Start_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy"),
                        NoOfDays = int.Parse((string)config["No_Of_Days"]),
                        EndDate = DateTime.ParseExact((string)config["End_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy"),
                        Subject = (string)config["Subject"],
                        Objective = (string)config["Objective"],
                        RequiresDirectPayment = bool.Parse((string)config["Requires_Direct_Payment"]),
                        RequiresPRN = bool.Parse((string)config["Requires_PRN"]),
                        ProcurementPlanID = (string)config["Procurement_Plan_ID"],
                        PRNType = (string)config["PRN_Type"],
                        LocationCode = (string)config["Location_Code"],
                        RequisitionProductGroup = (string)config["Requisition_Product_Group"],
                        PPPlanCategory = (string)config["PP_Plan_Category"],
                        ApprovalStatus = (string)config["Approval_Status"],
                        Status = (string)config["Status"],
                        CreatedBy = (string)config["Created_By"],
                        DateCreated = (DateTime)config["Date_Created"],
                        CommittedBy = (string)config["Committed_By"],
                        DateCommitted = (DateTime)config["Date_Committed"],
                        RecalledBy = (string)config["Recalled_By"],
                        DateRecalled = (DateTime)config["Date_Recalled"]
                    });
            }

            return PartialView("~/Views/ExpenseRequisition/PartialViews/ExpenseRequisitionList.cshtml", expenseRequisitions);
        }
        [HttpPost]
        public ActionResult ExpenseRequisitionDocument(string DocNo)
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var expense = new ExpenseRequisition();

                var rsrceReq = $"ExpenseRequisitionCard?$filter=No eq '" + DocNo + "' &$format=json";
                var httpResponse = Credentials.GetOdataData(rsrceReq);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        expense.No = (string)config["No"];
                        expense.Description = (string)config["Description"];
                        expense.CorporateStrategy = (string)config["Corporate_Strategy"];
                        expense.ReportingPeriod = (string)config["Reporting_Period"];
                        expense.BudgetCode = (string)config["Budget_Code"];
                        expense.Date = DateTime
                            .ParseExact((string)config["Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        expense.GlobalDimension1Code = (string)config["Global_Dimension_1_Code"];
                        expense.GlobalDimension2Code = (string)config["Global_Dimension_2_Code"];
                        expense.Workplan = (string)config["Workplan"];
                        expense.ActivityCode = (string)config["Activity_Code"];
                        expense.ActivityDescription = (string)config["Activity_Description"];
                        expense.RequiresImprest = bool.Parse((string)config["Requires_Imprest"]);
                        expense.ImprestType = (string)config["Imprest_Type"];
                        expense.StartDate = DateTime
                            .ParseExact((string)config["Start_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        expense.NoOfDays = int.Parse((string)config["No_Of_Days"]);
                        expense.EndDate = DateTime
                            .ParseExact((string)config["End_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd/MM/yyyy");
                        expense.Subject = (string)config["Subject"];
                        expense.Objective = (string)config["Objective"];
                        expense.RequiresDirectPayment = bool.Parse((string)config["Requires_Direct_Payment"]);
                        expense.DirectPay = (string)config["Direct_Payment_Type"];
                        expense.RequiresPRN = bool.Parse((string)config["Requires_PRN"]);
                        expense.ProcurementPlanID = (string)config["Procurement_Plan_ID"];
                        expense.PRNType = (string)config["PRN_Type"];
                        expense.LocationCode = (string)config["Location_Code"];
                        expense.RequisitionProductGroup = (string)config["Requisition_Product_Group"];
                        expense.PPPlanCategory = (string)config["PP_Plan_Category"];
                        expense.ApprovalStatus = (string)config["Approval_Status"];
                        expense.Status = (string)config["Status"];
                        expense.CreatedBy = (string)config["Created_By"];
                        expense.DateCreated = (DateTime)config["Date_Created"];
                        expense.CommittedBy = (string)config["Committed_By"];
                        expense.DateCommitted = (DateTime)config["Date_Committed"];
                        expense.RecalledBy = (string)config["Recalled_By"];
                        expense.DateRecalled = (DateTime)config["Date_Recalled"];
                        expense.Aie = (string)config["AIE"];
                    }
                }

                expense.GeographicalLocationName =
                    DimensinValuesList.GetDimensionValueName(expense.GlobalDimension1Code);
                expense.AdminUnitName = DimensinValuesList.GetDimensionValueName(expense.GlobalDimension2Code);
                LoadOptions();
                LoadImprestTypes();
                LoadDirectPaymentTypes();
                LoadActivities(expense.Workplan);
                LoadProcurementCategories();
                return View(expense);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        protected void LoadOptions()
        {
            try
            {
                List<DropdownList> dropDownList = new List<DropdownList>();
                for (int i = 1; i < 3; i++)
                {
                    DropdownList ddl = new DropdownList
                    {
                        Value = i != 1 ? "True" : "False",
                        Text = i != 1 ? "True" : "False"
                    };
                    dropDownList.Add(ddl);
                }
                ((dynamic)base.ViewBag).OptionList = dropDownList;
            }
            catch (Exception exception)
            {
                exception.Data.Clear();
            }
        }
        protected void LoadImprestTypes()
        {
            try
            {
                List<SelectListItem> imprestTypes = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Safari Imprest", Text = "Safari Imprest" },
                    new SelectListItem { Value = "Standing Imprest", Text = "Standing Imprest" },
                    new SelectListItem { Value = "Special Imprest", Text = "Special Imprest" }
                };

                ((dynamic)ViewBag).ImprestTypes = imprestTypes;
            }
            catch (Exception exception)
            {
                exception.Data.Clear();
            }
        }


        protected void LoadDirectPaymentTypes()
        {
            try
            {
                List<SelectListItem> directPaymentTypes =
                [
                    new() { Value = "Non Staff Claim", Text = "Non-Employee & Other Claims" },
                    new() { Value = "Staff Claim", Text = "Employee Claim" },
                    new() { Value = "Contract Payment", Text = "Contract Payment" }
                ];
                ((dynamic)ViewBag).DirectPaymentTypes = directPaymentTypes;
            }
            catch (Exception exception)
            {
                exception.Data.Clear();
            }
        }

        protected void LoadActivities(string workPlanCode)
        {
            try
            {
                var workPlanActivities = new List<DropdownList>();
                var strategyPlanId = "";
                var pageWp = $"StrategyWorkplanLines?$filter=No eq '{workPlanCode}' &$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config["Activity_ID"]} {(string)config["Description"]}",
                            Value = (string)config["Activity_ID"]
                        };
                        workPlanActivities.Add(dropdownList);
                    }
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
        protected void LoadProcurementCategories()
        {
            try
            {

                var procurementCategories = new List<DropdownList>();
                var pagePC = "PlanningLinesApp?&$format=json";

                var httpResponsePC = Credentials.GetOdataData(pagePC);
                using (var streamReader = new StreamReader(httpResponsePC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["Planning_Category"] + "-" + (string)config["Description"],
                            Value = (string)config["Planning_Category"]
                        };
                        procurementCategories.Add(dropdownList);
                    }
                }
                ((dynamic)ViewBag).Activities = procurementCategories.Select(x => new SelectListItem
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


        public JsonResult SubmitExpenseRequisition(ExpenseRequisition expenseRequisition)
        {
            try
            {
                var reqImprest = false;
                if (expenseRequisition.RequiresImprest != null) reqImprest = expenseRequisition.RequiresImprest;

                var impType = 0;
                if (expenseRequisition.ImpType != null) impType = expenseRequisition.ImpType;

                var sdate = DateTime.MinValue;
                if (expenseRequisition.StartDate != null) sdate = Convert.ToDateTime(expenseRequisition.StartDate);

                var days = 0;
                if (expenseRequisition.NoOfDays != null) days = expenseRequisition.NoOfDays;

                var objective = "";
                if (expenseRequisition.Objective != null) objective = expenseRequisition.Objective;

                var reqDirPay = false;
                if (expenseRequisition.RequiresDirectPayment != null)
                    reqDirPay = expenseRequisition.RequiresDirectPayment;
                var prnType = 0;
                if (expenseRequisition.PRNType != null &&
                    int.TryParse(expenseRequisition.PRNType, out var prnTypeValue)) prnType = prnTypeValue;
                var reprn = false;
                if (expenseRequisition.RequiresPRN != null) reprn = expenseRequisition.RequiresPRN;

                var procPlan = "";
                if (expenseRequisition.ProcurementPlanID != null) procPlan = expenseRequisition.ProcurementPlanID;

                var dirPay = 0;
                if (expenseRequisition.DirectPay != null &&
                    int.TryParse(expenseRequisition.DirectPay, out var dirPayValue)) dirPay = dirPayValue;

                var locCode = "";
                if (expenseRequisition.LocationCode != null) locCode = expenseRequisition.LocationCode;

                var reqProductGroup = 0;
                if (expenseRequisition.RequisitionProductGroup != null)
                    reqProductGroup = Convert.ToInt32(expenseRequisition.RequisitionProductGroup);

                var ppPlanCat = "";
                if (expenseRequisition.PPPlanCategory != null) ppPlanCat = expenseRequisition.PPPlanCategory;
                var subject = "";
                if (expenseRequisition.Subject != null) subject = expenseRequisition.Subject;
                var aie = "";

                if (expenseRequisition.Aie != null) aie = expenseRequisition.Aie;

                var employee = Session["EmployeeData"] as EmployeeView;
                expenseRequisition.GlobalDimension1Code = employee.GlobalDimension1Code;
                var userId = employee.UserID;

                var docNo = "";

                docNo = Credentials.ObjNav.InsertExpenseRequisition(
                    expenseRequisition.Description,
                    expenseRequisition.GlobalDimension1Code,
                    expenseRequisition.GlobalDimension2Code,
                    "",
                    reqImprest,
                    impType,
                    sdate,
                    days,
                    subject,
                    objective,
                    reqDirPay,
                    reprn,
                    procPlan,
                    prnType,
                    locCode,
                    reqProductGroup,
                    ppPlanCat,
                    userId,
                    dirPay, "",
                    aie);
                if (docNo != "")
                {
                    var Redirect = docNo;
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { message = "Document not created. Please try again later...", success = false },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateExpenseRequisition(ExpenseRequisition expenseRequisition)
        {
            try
            {
                var reqImprest = false;
                if (expenseRequisition.RequiresImprest != null) reqImprest = expenseRequisition.RequiresImprest;

                var impType = 0;
                if (expenseRequisition.ImprestType != null)
                {
                    switch (expenseRequisition.ImprestType)
                    {
                        case "Safari Imprest":
                            impType = 1;
                            break;
                        case "Standing Imprest":
                            impType = 2;
                            break;
                        case "Special Imprest":
                            impType = 3;
                            break;
                        default:
                            impType = 0;
                            break;
                    }
                }

                var sdate = DateTime.MinValue;
                if (expenseRequisition.StartDate != null) sdate = DateTime.ParseExact(expenseRequisition.StartDate, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                var days = 0;
                if (expenseRequisition.NoOfDays != null) days = expenseRequisition.NoOfDays;

                var objective = "";
                if (expenseRequisition.Objective != null) objective = expenseRequisition.Objective;

                var reqDirPay = false;
                if (expenseRequisition.RequiresDirectPayment != null)
                    reqDirPay = expenseRequisition.RequiresDirectPayment;
                var prnType = 0;
                if (expenseRequisition.PRNType != null &&
                    int.TryParse(expenseRequisition.PRNType, out var prnTypeValue)) prnType = prnTypeValue;
                var reprn = false;
                if (expenseRequisition.RequiresPRN != null) reprn = expenseRequisition.RequiresPRN;

                var dirPay = 0;
                if (expenseRequisition.DirectPay != null)
                {
                    switch (expenseRequisition.DirectPay)
                    {
                        case "Non Staff Claim":
                            dirPay = 0;
                            break;
                        case "Staff Claim":
                            dirPay = 1;
                            break;
                        default:
                            dirPay = 0;
                            break;
                    }
                }
                var reqProductGroup = "";
                if (expenseRequisition.RequisitionProductGroup != null)
                    reqProductGroup = expenseRequisition.RequisitionProductGroup;

                var ppPlanCat = "";
                if (expenseRequisition.PPPlanCategory != null) ppPlanCat = expenseRequisition.PPPlanCategory;
                var subject = "";
                if (expenseRequisition.Subject != null) subject = expenseRequisition.Subject;
                Credentials.ObjNav.ModifyExpenseRequisition(
                    expenseRequisition.Description,
                    reqImprest,
                    impType, "",
                    sdate,
                    days,
                    subject,
                    objective,
                    reqDirPay,
                    reprn,
                    prnType,
                    reqProductGroup,
                    ppPlanCat,
                    dirPay, expenseRequisition.No);
                return Json(new { message = "Expense Header Updated Successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult ExpenseRequisitionLine(string DocNo, string status, bool reqImprest, bool reqPrn,
            bool dirPay, string dirPayType, string GlobalDimension1Code, string GlobalDimension2Code)
        {
            var expenseRequisitionLines = new List<ExpenseRequisitionLine>();

            var rsrceReq = "ExpenseRequisitionLine?$filter=Document_No eq '" + DocNo + "' &$format=json";

            var httpResponse = Credentials.GetOdataData(rsrceReq);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var expense = new ExpenseRequisitionLine();
                    expense.LineNo = (int?)config["Line_No"] ?? 0;
                    expense.GLAccount = (string)config["G_L_Account"];
                    expense.GLAccountName = (string)config["G_L_Account_Name"];
                    expense.ShortcutDimension3Code = (string)config["Shortcut_Dimension_3_Code"];
                    expense.ShortcutDimension4Code = (string)config["Shortcut_Dimension_4_Code"];
                    expense.ShortcutDimension5Code = (string)config["Shortcut_Dimension_5_Code"];
                    expense.ShortcutDimension6Code = (string)config["Shortcut_Dimension_6_Code"];
                    expense.ShortcutDimension7Code = (string)config["Shortcut_Dimension_7_Code"];
                    expense.ShortcutDimension8Code = (string)config["Shortcut_Dimension_8_Code"];
                    expense.BudgetAllocation = (int?)config["Budget_Allocation"] ?? 0;
                    expense.BudgetBalance = (int?)config["Budget_Balance"] ?? 0;
                    expense.TotalCommitments = (int?)config["Total_Committments"] ?? 0;
                    expense.TotalAmount = (int?)config["Total_Amount"] ?? 0;
                    expense.TotalAllocation = (int?)config["Total_Allocation"] ?? 0;
                    expense.Status = (string)config["Status"];
                    expense.Error = (bool?)config["Error"] ?? false;
                    expense.ErrorMessage = (string)config["Error_Message"];
                    expense.RecalledBy = (string)config["Recalled_By"];
                    expense.RecalledOn = (DateTime)config["Recalled_On"];
                    expense.ActivityId = (string)config["Activity_Id"];
                    expense.Workplan = (string)config["Workplan"];
                    expense.ResourceReqNo = (string)config["Resource_Req_No"];
                    expense.DocumentNo = DocNo;
                    expenseRequisitionLines.Add(expense);
                }
            }
            var expenseRequisitionLineList = new ExpenseRequisitionLineList
            {
                GlobalDimension1Code = GlobalDimension1Code,
                GlobalDimension2Code = GlobalDimension2Code,
                Status = status,
                RequiresImprest = reqImprest,
                RequiresPRN = reqPrn,
                RequiresDirectPay = dirPay,
                DirectPayType = dirPayType,
                ListOfExpenseRequisitionLine = expenseRequisitionLines
            };
            return PartialView("~/Views/ExpenseRequisition/PartialViews/ExpenseRequisitionLine.cshtml",
                expenseRequisitionLineList);
        }


        public ActionResult NewExpenseRequisition()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }


                var expenseRequisition = new ExpenseRequisition();
                Session["httpResponse"] = null;
                var employeeView = Session["EmployeeData"] as EmployeeView;
                expenseRequisition.CreatedBy = employeeView.UserID;
                expenseRequisition.GlobalDimension2Code = employeeView.GlobalDimension2Code;

                expenseRequisition.LocationCode = employeeView.GlobalDimension1Code;
                expenseRequisition.Name = CommonController.GetStaticDimensionName(employeeView.GlobalDimension2Code);
                expenseRequisition.Date = DateTime.Now.ToString();

                #region dim2

                var dim2 = new List<DropdownList>();
                var pageDim2 = "DimensionValues?$filter=Global_Dimension_No eq 2 and Blocked eq false&$format=json";

                var httpResponseDivision = Credentials.GetOdataData(pageDim2);
                using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["Code"] + "-" + (string)config["Name"],
                            Value = (string)config["Code"]
                        };
                        dim2.Add(dropdownList);
                    }
                }

                #endregion

                #region draftWorkPlans

                var draftWorkPlans = new List<DropdownList>();
                var baseUrl = "DraftWorkPlan";
                var pageWp = "";
                var rsrceReq = "";
                var geographical = employeeView?.GlobalDimension1Code;
                var adminUnit = employeeView?.AdministrativeUnit;
                if (geographical == "00000001")
                {
                    pageWp = $"DraftWorkPlans?$filter=Global_Dimension_1_Code eq '{expenseRequisition.LocationCode}' and Global_Dimension_2_Code eq '{expenseRequisition.GlobalDimension2Code}' and Posted eq true and Archived eq false&$format=json";

                }
                else
                {
                    pageWp = $"DraftWorkPlans?$filter=Global_Dimension_1_Code eq '{expenseRequisition.LocationCode}' and Global_Dimension_2_Code eq '{expenseRequisition.GlobalDimension2Code}' and Posted eq true and Archived eq false&$format=json";

                }
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["No"] + "-" + (string)config["Description"],
                            Value = (string)config["No"]
                        };
                        draftWorkPlans.Add(dropdownList);
                    }
                }

                #endregion

                #region WorkPlanActivities

                var workPlanActivities = new List<DropdownList>();
                var pageWPA = "WorkplanActivities?&$format=json";
                //string pageWPA = $"WorkPlanActivities?$filter=G_L_Account eq '{glAccount}' and Line_No eq {lineNo}&$format=json";


                var httpResponseWPA = Credentials.GetOdataData(pageWPA);
                using (var streamReader = new StreamReader(httpResponseWPA.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Code"] + "-" + (string)config["Descriptions"];
                        dropdownList.Value = (string)config["Code"];
                        workPlanActivities.Add(dropdownList);
                    }
                }

                #endregion

                #region ProcurementPlan

                var procurementPlans = new List<DropdownList>();
                var pagePPlan = "ApprovedProcurementPlan?&$format=json";


                var httpResponsePPlan = Credentials.GetOdataData(pagePPlan);
                using (var streamReader = new StreamReader(httpResponsePPlan.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Code"] + "-" + (string)config["Description"];
                        dropdownList.Value = (string)config["Code"];
                        procurementPlans.Add(dropdownList);
                    }
                }

                #endregion

                #region Locations

                var locations = new List<DropdownList>();
                var pageLoc = "Locations?&$format=json";

                var httpResponseLoc = Credentials.GetOdataData(pageLoc);
                using (var streamReader = new StreamReader(httpResponseLoc.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["Code"] + "-" + (string)config["Name"],
                            Value = (string)config["Code"]
                        };
                        locations.Add(dropdownList);
                    }
                }

                #endregion
                #region AIEs
                var firstWorkPlan = draftWorkPlans != null && draftWorkPlans.Any()
    ? draftWorkPlans[0].Value
    : null;

                var aies = new List<DropdownList>();
                var pageAie = "AieList?$filter=Workplan_Code eq '" + firstWorkPlan + "' and Type eq 'Special AIE'&$format=json";

                var httpResponseAie = Credentials.GetOdataData(pageAie);
                using (var streamReader = new StreamReader(httpResponseAie.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    aies.AddRange(from JObject config in details["value"] select new DropdownList { Text = (string)config["Source_Document_No"] + "-" + (string)config["Description"], Value = (string)config["Source_Document_No"] });
                }

                #endregion
                #region ProcurementCategories

                var procurementCategories = new List<DropdownList>();
                var pagePC = "PlanningLinesApp?&$format=json";

                var httpResponsePC = Credentials.GetOdataData(pagePC);
                using (var streamReader = new StreamReader(httpResponsePC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Code"] + "-" + (string)config["Description"];
                        dropdownList.Value = (string)config["Code"];
                        procurementCategories.Add(dropdownList);
                    }
                }

                #endregion

                expenseRequisition.Workplan = firstWorkPlan;
                expenseRequisition.ListOfProcurementCategories = procurementCategories.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfLocations = locations.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfProcurementPlans = procurementPlans.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfActivities = workPlanActivities.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfWorkPlans = draftWorkPlans.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfDim2 = dim2.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfAie = aies.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                return PartialView("~/Views/ExpenseRequisition/PartialViews/NewExpenseRequisition.cshtml", expenseRequisition);
            }

            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult GetWorkPlans(string adminUnit)
        {
            try
            {
                #region draftWorkPlans
                var draftWorkPlans = new List<DropdownList>();
                var baseUrl = "DraftWorkPlan";
                var pageWp = "";
                var rsrceReq = "";
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var geographical = employeeView?.GlobalDimension1Code;
                pageWp = "DraftWorkPlans?$filter=Global_Dimension_1_Code eq '" + geographical + "' and Global_Dimension_2_Code eq '" + adminUnit + "' and Posted eq true and Archived eq false&$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["No"] + "-" + (string)config["Description"],
                            Value = (string)config["No"]
                        };
                        draftWorkPlans.Add(dropdownList);
                    }
                }
                #endregion
                var response = new
                {
                    ListOfActivities = draftWorkPlans.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                    success = true
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAies(string workplan)
        {
            try
            {
                #region draftWorkPlans
                var draftWorkPlans = new List<DropdownList>();
                var pageWp = "AieList?$filter=Workplan_Code eq '" + workplan + "' and Type eq 'Special AIE'&$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["Source_Document_No"] + "-" + (string)config["Description"],
                            Value = (string)config["Source_Document_No"]
                        };
                        draftWorkPlans.Add(dropdownList);
                    }
                }
                #endregion
                var response = new
                {
                    ListOfActivities = draftWorkPlans.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                    success = true
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetActivities(string workplanNo)
        {
            try
            {
                var workPlanActivities = new List<DropdownList>();
                var strategyPlanId = "";
                var pageWp = $"StrategyWorkplanLines?$filter=No eq '{workplanNo}' &$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config["Activity_ID"]} {(string)config["Description"]}",
                            Value = (string)config["Activity_ID"]
                        };
                        workPlanActivities.Add(dropdownList);
                    }
                }
                var response = new
                {
                    ListOfActivities = workPlanActivities.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                    success = true
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProcurementPlanCategories(string procurementPlanId)
        {
            try
            {
                var procurementCategories = new List<DropdownList>();

                /* var pagePC = $"ApprovedPlanEntries?$filter=Procurement_Plan_ID eq '{procurementPlanId}' &$format=json";*/
                var pagePC = $"PlanningLinesApp?$filter=Procurement_Plan_ID eq '{procurementPlanId}' &$format=json";


                var httpResponsePC = Credentials.GetOdataData(pagePC);
                using (var streamReader = new StreamReader(httpResponsePC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config["Planning_Category"] + "-" + (string)config["Description"],
                            Value = (string)config["Planning_Category"]
                        };
                        procurementCategories.Add(dropdownList);
                    }
                }

                var response = new
                {
                    ListofprocurementCategories = procurementCategories.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                    success = true
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SubmitExpenseRequisitionLine(ExpenseRequisitionLine expenseRequisition)
        {
            try
            {
                var docNo = expenseRequisition.DocumentNo;

                Credentials.ObjNav.InsertExpenseRequisitionLine(docNo, expenseRequisition.GLAccount,
                    "",
                    "", expenseRequisition.TotalAmount,
                    "", "", expenseRequisition.ActivityId);
                var redirect = docNo;

                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult NewExpenseRequisitionLine(string docNo, string workPlan, string activityId)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var expenseRequisition = new ExpenseRequisitionLine();
                Session["httpResponse"] = null;
                expenseRequisition.DocumentNo = docNo;
                expenseRequisition.Workplan = workPlan;

                var workPlanActivities = new List<DropdownList>();
                var resourceReqNo = "";
                var pageWp = $"StrategyWorkplanLines?$filter=No eq '{workPlan}' &$format=json";
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
                            Text = $"{(string)config1["Activity_ID"]} - {(string)config1["Description"]}",
                            Value = (string)config1["Activity_ID"]
                        };
                        workPlanActivities.Add(dropdownList);
                    }
                }

                expenseRequisition.ResourceReqNo = resourceReqNo;
                expenseRequisition.ListOfActivities = workPlanActivities.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfDim3 = DimensionValues(3).Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfDim4 = DimensionValues(4).Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfDim5 = DimensionValues(5).Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                expenseRequisition.ListOfDim6 = DimensionValues(6).Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                return PartialView("~/Views/ExpenseRequisition/PartialViews/NewExpenseRequisitionLine.cshtml",
                    expenseRequisition);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        private List<DropdownList> DimensionValues(int dimNo)
        {
            #region Dimensions

            var dimensionValues = new List<DropdownList>();
            var pageDim5 = $"DimensionValues?$filter=Global_Dimension_No eq {dimNo} and Blocked eq false&$format=json";

            var httpResponsedim5 = Credentials.GetOdataData(pageDim5);
            using var streamReader = new StreamReader(httpResponsedim5.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);


            foreach (var jToken in details["value"])
            {
                var config = (JObject)jToken;
                var dropdownList = new DropdownList();
                dropdownList.Text = (string)config["Code"] + "-" + (string)config["Name"];
                dropdownList.Value = (string)config["Code"];
                dimensionValues.Add(dropdownList);
            }

            #endregion

            return dimensionValues;
        }




        public ActionResult SendExpenseReqForApproval(string docNo)
        {
            var successVal = false;
            try
            {
                if (Session["UserID"] == null || Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                decimal totalAmount = 0;
                decimal availableBalance = 0;
                var glAccount = "";
                decimal totalAllocation = 0;
                var rsrceReq = "ExpenseRequisitionLine?$filter=Document_No eq '" + docNo + "' &$format=json";
                var httpResponse = Credentials.GetOdataData(rsrceReq);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    if (details != null)
                        foreach (var jToken in details["value"])
                        {
                            var config = (JObject)jToken;
                            glAccount = (string)config["G_L_Account_Name"];
                            availableBalance = (int)config["Budget_Balance"];
                            totalAmount = (int)config["Total_Amount"];
                            totalAllocation = (int)config["Total_Allocation"];

                            if (availableBalance == 0)
                                return Json(
                                    new
                                    {
                                        message = $"You have Fully Utilized your Budget for {glAccount}.",
                                        success = false
                                    }, JsonRequestBehavior.AllowGet);
                            if (totalAmount > availableBalance)
                                return Json(
                                    new
                                    {
                                        message =
                                            $"Requested amount of {totalAmount} for G/L Account {glAccount}exceeds the available budget.",
                                        success = false
                                    }, JsonRequestBehavior.AllowGet);
                            if (totalAmount != totalAllocation)
                                return Json(
                                    new
                                    {
                                        message =
                                            $"Requested amount of {totalAmount} Not equal to the Allocated amount {totalAllocation}.",
                                        success = false
                                    }, JsonRequestBehavior.AllowGet);
                            if (totalAmount == 0)
                                return Json(new { message = "Amount Cannot be zero.", success = false },
                                    JsonRequestBehavior.AllowGet);
                        }
                }


                var employee = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.ExpenseRequisitionApproval(docNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(50110, docNo, employee?.UserID);

                return Json(new { message = "Expense Requisition Sent for Approval", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult CancelExpReqApprovalRequest(string docNo)
        {
            try
            {
                if (Session["UserID"] == null || Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                Credentials.ObjNav.CancelExpenseRequisitionApproval(docNo);
                return Json(new { message = "Approval Request Cancelled Successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetGLDimenions(string budgetItem, string activityId)
        {
            try
            {
                var workPlanBudget = new WorkPlanBudgetLookup();
                var strategyPlanId = "";

                var pageWp =
                    $"WorkPlanBudgetLookup?$filter=Budget_Item eq '{budgetItem}' and Activity_ID eq '{activityId}'&$format=json";

                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        workPlanBudget.BudgetItem = (string)config["Budget_Item"];
                        workPlanBudget.ShortcutDimension3Code = $"({(string)config["Shortcut_Dimension_3_Code"]}){DimensinValuesList.GetDimensionValueName((string)config["Shortcut_Dimension_3_Code"])}";
                        workPlanBudget.ShortcutDimension4Code = $"({(string)config["Shortcut_Dimension_4_Code"]}){DimensinValuesList.GetDimensionValueName((string)config["Shortcut_Dimension_4_Code"])}";
                        workPlanBudget.ShortcutDimension5Code = $"({(string)config["Shortcut_Dimension_5_Code"]}){DimensinValuesList.GetDimensionValueName((string)config["Shortcut_Dimension_5_Code"])}";
                        workPlanBudget.ShortcutDimension6Code = $"({(string)config["Shortcut_Dimension_6_Code"]}){DimensinValuesList.GetDimensionValueName((string)config["Shortcut_Dimension_6_Code"])}";
                        workPlanBudget.ShortcutDimension7Code = $"({(string)config["Shortcut_Dimension_7_Code"]}){DimensinValuesList.GetDimensionValueName((string)config["Shortcut_Dimension_7_Code"])}";
                        workPlanBudget.ShortcutDimension8Code = $"({(string)config["Shortcut_Dimension_8_Code"]}){DimensinValuesList.GetDimensionValueName((string)config["Shortcut_Dimension_8_Code"])}";
                    }
                }


                return Json(workPlanBudget, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetActivityGL(string workplanNo, string activityId)
        {
            try
            {
                #region WorkPlanBudgetLookup

                var workPlanBudgetLookup = new List<DropdownList>();
                var pageExpenseRequisition =
                    $"WorkPlanBudgetLookup?$filter=Strategy_Plan_ID eq '{workplanNo}' and Activity_ID eq '{activityId}'&$format=json";

                var httpResponseWorkPlan = Credentials.GetOdataData(pageExpenseRequisition);
                using (var streamReader = new StreamReader(httpResponseWorkPlan.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Budget_Item"] + "-" +
                                            (string)config["Budget_Item_Description"];
                        dropdownList.Value = (string)config["Budget_Item"];
                        workPlanBudgetLookup.Add(dropdownList);
                    }
                }

                #endregion

                // Create and return the JSON result
                var response = new
                {
                    LisOfGl = workPlanBudgetLookup.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                    success = true
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DeleteExpenseLine(string docNo, int lineNo)
        {
            var successVal = false;
            try
            {
                if (Session["UserID"] == null || Session["Username"] == null) return RedirectToAction("Login", "Login");
                Credentials.ObjNav.ClearExpenseLines(docNo, lineNo);
                return Json(new { message = "Expense Requisition Line Deleted Successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteAllocationLines(string docNo, int lineNo, int sourceLine)
        {
            var successVal = false;
            try
            {
                if (Session["UserID"] == null || Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                Credentials.ObjNav.DeleteExpenseReqLine(docNo, lineNo, sourceLine);


                return Json(new { message = "Expense Allocation Deleted Successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateExpenseLineLine(string docNo, int lineNo, string resourceNo, decimal amount)
        {
            try
            {
                if (Session["UserID"] == null || Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                Credentials.ObjNav.ModifyExpenseLines(docNo, lineNo, resourceNo, amount);
                return Json(new { message = "Expense Requisition Line Saved Successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GenerateExpenseRequisitionReport(string documentNumber)
        {
            try
            {
                var message = "";
                bool success = false, view = false;

                message = Credentials.ObjNav.GenerateExpenseRequisition(documentNumber);
                if (message == "")
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
        public JsonResult GenerateWarrantReport(string documentNumber)
        {
            try
            {
                var message = "";
                bool success = false, view = false;

                message = Credentials.ObjNav.GenerateWarrantReport(documentNumber);
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
        public PartialViewResult NewStaffClaimLine(string glAccount, int lineNo, string documentNo)
        {
            try
            {
                var imprestLine = new ImprestLine();

                #region Employee

                var employeeList = new List<DropdownList>();
                var pageReliever = "Resources?$filter=Type eq 'Person' and Name ne ''&format=json";

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

                ViewBag.LineNo = lineNo;
                ViewBag.GLAccount = glAccount;
                ViewBag.DocumentNumber = documentNo;
                imprestLine.ListOfEmployees = employeeList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                return PartialView("~/Views/ExpenseRequisition/PartialViews/AddStaffClaimLine.cshtml", imprestLine);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public PartialViewResult ViewStaffClaim(string docNo, string glAccount, int lineNo, string Status)
        {
            try
            {
                var claimLines = new List<ClaimLine>();
                var pageLine =
                    $"EXRStaffClaimLines?$filter=Document_No eq '{docNo}' and Source_Line_No eq {lineNo}&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var claim = new ClaimLine
                        {
                            Document_No = (string)config["Document_No"],
                            Line_No = (int)config["Line_No"],
                            Payee = (string)config["Payee"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            G_L_Account = (string)config["G_L_Account"],
                            Vote_Item = (string)config["Vote_Item"],
                            Quantity = (int)config["Quantity"],
                            Rate = (decimal)config["Rate"],
                            Total = (decimal)config["Total"],
                            Status = (string)config["Status"],
                            Recalled_By = (string)config["Recalled_By"],
                            Recalled_On = (DateTime)config["Recalled_On"],
                            Source_Line_No = (int)config["Source_Line_No"]
                        };
                        claimLines.Add(claim);
                    }
                }

                var lines = new ClaimLineList
                {
                    Status = Status,
                    ListOfCLaimLines = claimLines
                };
                return PartialView("~/Views/ExpenseRequisition/PartialViews/StaffClaimLines.cshtml", lines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public PartialViewResult ViewNonStaffClaim(string docNo, string glAccount, int lineNo, string Status)
        {
            try
            {
                var claimLines = new List<ClaimLine>();
                var pageLine = $"NonStaffClaimDirectPayment?$filter=Document_No eq '{docNo}' and Source_Line_No eq {lineNo}&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var claim = new ClaimLine
                        {
                            Document_No = (string)config["Document_No"],
                            Line_No = (int)config["Line_No"],
                            Payee = (string)config["Payee"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            G_L_Account = (string)config["G_L_Account"],
                            Vote_Item = (string)config["Vote_Item"],
                            Quantity = (int)config["Quantity"],
                            Rate = (decimal)config["Rate"],
                            Total = (decimal)config["Total"],
                            Status = (string)config["Status"],
                            Recalled_By = (string)config["Recalled_By"],
                            Recalled_On = (DateTime)config["Recalled_On"],
                            SupplierInvoiceNo = (string)config["Supplier_Invoice_No"],
                            SupplierInvoiceDate = (string)config["Supplier_Invoice_Date"],
                            Supplier = (string)config["Supplier"],
                            ExpenseDescription = (string)config["Expense_Description"],
                            Source_Line_No = (int)config["Source_Line_No"]
                        };
                        claimLines.Add(claim);
                    }
                }

                var lines = new ClaimLineList
                {
                    Status = Status,
                    ListOfCLaimLines = claimLines
                };
                return PartialView("PartialViews/NonStaffClaimLines", lines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public PartialViewResult NewNonStaffClaimLine(string glAccount, int lineNo, string documentNo)
        {
            try
            {

                var newNonStaffClaim = new ClaimLine();

                #region Vendor List
                var vendorList = new List<DropdownList>();
                //
                var page = "ProcurementVendors?$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var list = new DropdownList
                        {
                            Value = (string)config["No"],
                            Text = (string)config["Name"]
                        };
                        vendorList.Add(list);
                    }
                }
                #endregion

                ViewBag.GLAccount = glAccount;
                ViewBag.LineNo = lineNo;
                ViewBag.DocumentNumber = documentNo;
                newNonStaffClaim.ListOfSupplier = vendorList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                return PartialView("PartialViews/AddNonStaffClaim", newNonStaffClaim);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitExpenseLines(ClaimLine lines)
        {
            try
            {
                var staffNo = Session["Username"]?.ToString() ?? string.Empty;
                var employee = Session["EmployeeData"] as EmployeeView;
                var globalDimension1Code = employee?.GlobalDimension1Code ?? string.Empty;
                var userId = employee?.UserID ?? string.Empty;
                var suupplierDate = DateTime.ParseExact(lines.SupplierInvoiceDate.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Credentials.ObjNav.InsertDirectExpense(lines.Document_No, suupplierDate, lines.SupplierInvoiceNo, lines.ExpenseDescription, lines.G_L_Account, lines.Quantity
                , lines.Rate, "", "", lines.Line_No, 1, lines.Supplier);
                return Json(new { message = "Staff Claim  line submitted successfully.", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SubmitContractLines(ClaimLine lines)
        {
            try
            {
                var staffNo = Session["Username"]?.ToString() ?? string.Empty;
                var employee = Session["EmployeeData"] as EmployeeView;
                var globalDimension1Code = employee?.GlobalDimension1Code ?? string.Empty;
                var userId = employee?.UserID ?? string.Empty;
                var suupplierDate = DateTime.ParseExact(lines.SupplierInvoiceDate.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Credentials.ObjNav.InsertContractPayment(lines.Document_No, lines.ContractNo, lines.LpoNo, lines.ItemNo, suupplierDate, lines.SupplierInvoiceNo, lines.ExpenseDescription, lines.G_L_Account, lines.Quantity
                    , lines.Rate, "", "", lines.Line_No, 1, lines.Supplier);
                return Json(new { message = "Contract Payment line submitted successfully.", success = true }, JsonRequestBehavior.AllowGet);
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
            return PartialView("PartialViews/FileAttachmentForm");
        }
        public PartialViewResult NewExpensePRNLine(string glAccount, int lineNo, string procurementPlan, string GlobalDimension1Code, string GlobalDimension2Code)
        {
            try
            {
                ExpensePRNLine expensePRNLine = new ExpensePRNLine();


                #region ProcurementPlanEntry
                List<DropdownList> procurementPlans = new List<DropdownList>();
                string page = $"ApprovedProcPlan?$filter=Code eq '{procurementPlan}' and Approval_Status eq 'Released'&$format=json";

                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList list = new DropdownList();
                        list.Text = (string)config["Code"] + "-" + (string)config["Description"];
                        list.Value = (string)config["Code"];
                        procurementPlans.Add(list);

                    }

                }
                #endregion

                #region ProcurementItems
                List<DropdownList> procurementItems = new List<DropdownList>();
                string pageItems = $"ProcurementPlanEntry?$filter=ProcurementPlanID eq '{procurementPlan}' and PlanningCategory eq '{glAccount}' and GlobalDimension1Code eq '{GlobalDimension1Code}' and GlobalDimension2Code eq '{GlobalDimension2Code}' and Blocked eq false &$format=json";
                HttpWebResponse httpResponseItems = Credentials.GetOdataData(pageItems);
                using (var streamReader = new StreamReader(httpResponseItems.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList list = new DropdownList();
                        list.Text = (string)config["EntryNo"] + "-" + (string)config["Description"];
                        list.Value = (string)config["EntryNo"];
                        procurementItems.Add(list);

                    }

                }
                #endregion


                expensePRNLine.GLAccount = glAccount;
                expensePRNLine.LineNo = lineNo;

                expensePRNLine.ListOfProcurementPlanNos = procurementPlans.Select(x =>
                                     new SelectListItem()
                                     {
                                         Text = x.Text,
                                         Value = x.Value
                                     }).ToList();
                expensePRNLine.ListOfProcurementItems = procurementItems.Select(x =>
                                    new SelectListItem()
                                    {
                                        Text = x.Text,
                                        Value = x.Value
                                    }).ToList();


                return PartialView("~/Views/Purchase/PartialViews/NewPurchaseLine.cshtml", expensePRNLine);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitExpensePRNLine(ExpensePRNLine expensePRNLine)
        {
            try
            {
                // Initialize variables with default values
                string glAccount = string.Empty;
                int lineNo = 0;
                int procPlanEntryNo = 0;
                string expenseDescription = string.Empty;
                int quantity = 0;
                decimal rate = 0m;

                // Extracting and validating fields from the model
                if (expensePRNLine.GLAccount != null)
                {
                    glAccount = expensePRNLine.GLAccount;
                }

                if (expensePRNLine.LineNo != null)
                {
                    lineNo = (int)expensePRNLine.LineNo;
                }

                if (expensePRNLine.ProcPlanEntryNo != null)
                {
                    procPlanEntryNo = expensePRNLine.ProcPlanEntryNo;
                }

                if (expensePRNLine.ExpenseDescription != null)
                {
                    expenseDescription = expensePRNLine.ExpenseDescription;
                }

                if (expensePRNLine.Quantity != null)
                {
                    quantity = (int)expensePRNLine.Quantity;
                }

                if (expensePRNLine.Rate != null)
                {
                    rate = (decimal)expensePRNLine.Rate;
                }

                // Get user details from session
                string staffNo = Session["Username"]?.ToString() ?? string.Empty;
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string globalDimension1Code = employee?.GlobalDimension1Code ?? string.Empty;
                string userId = employee?.UserID ?? string.Empty;

                //Call service to insert PRN line
                if (quantity == 0)
                {
                    quantity = 1;
                }
                Credentials.ObjNav.InsertExpenseLinesPurchase(expensePRNLine.DocumentNo, "", procPlanEntryNo, expensePRNLine.pType, 0, "", expenseDescription, glAccount, quantity, rate, lineNo);
                //procedure InsertExpenseLinesPurchase(supplier: Text; ProcPlanEntry: Integer; ProcType: Option; LineType: Option; ItemNo: Text; ExpenseDescription: Text; GLAccount: Text; Quantity: Decimal; Rate: Decimal)

                return Json(new { message = "Success", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult NewContractPayment(string glAccount, int lineNo, string documentNo)
        {
            try
            {

                var newNonStaffClaim = new ClaimLine();

                #region Vendor List
                var vendorList = new List<DropdownList>();
                //
                var page = "ProcurementVendors?$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var list = new DropdownList
                        {
                            Value = (string)config["No"],
                            Text = (string)config["Name"]
                        };
                        vendorList.Add(list);
                    }
                }
                #endregion

                ViewBag.GLAccount = glAccount;
                ViewBag.LineNo = lineNo;
                ViewBag.DocumentNumber = documentNo;
                newNonStaffClaim.ListOfSupplier = vendorList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                return PartialView("PartialViews/NewContractPayment", newNonStaffClaim);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult GetVendorContracts(string vendor)
        {
            try
            {
                var workPlanActivities = new List<DropdownList>();
                var strategyPlanId = "";
                var pageWp = $"PurchaseList?$filter=Buy_from_Vendor_No eq '{vendor}' and Document_Type eq 'Blanket Order'&$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config["No"]} {(string)config["Buy_from_Vendor_Name"]}",
                            Value = (string)config["No"]
                        };
                        workPlanActivities.Add(dropdownList);
                    }
                }
                var response = new
                {
                    ListOfActivities = workPlanActivities.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                    success = true
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetVendorLpo(string vendor)
        {
            try
            {
                var workPlanActivities = new List<DropdownList>();
                var strategyPlanId = "";
                var pageWp = $"PurchaseList?$filter=Buy_from_Vendor_No eq '{vendor}' and Document_Type eq 'Order'&$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config["Document_No"]} {(string)config["Description"]}",
                            Value = (string)config["Document_No"]
                        };
                        workPlanActivities.Add(dropdownList);
                    }
                }
                var response = new
                {
                    ListOfActivities = workPlanActivities.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                    success = true
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetVendorPurchaseLines(string vendor, string lpoNumber)
        {
            try
            {
                var workPlanActivities = new List<DropdownList>();
                var strategyPlanId = "";
                var pageWp = $"PurchaseLines?$filter=Buy_from_Vendor_No eq '{vendor}' and Document_No eq and '{lpoNumber}' Document_Type eq 'Order'&$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config["No"]} {(string)config["Description"]}",
                            Value = (string)config["No"]
                        };
                        workPlanActivities.Add(dropdownList);
                    }
                }
                var response = new
                {
                    ListOfActivities = workPlanActivities.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                    success = true
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult ViewContractPayment(string docNo, string glAccount, int lineNo, string Status)
        {
            try
            {
                var claimLines = new List<ClaimLine>();
                var pageLine = $"DirectExpenses?$filter=Document_No eq '{docNo}' and Source_Line_No eq {lineNo} and G_L_Account eq '{glAccount}' &$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);
                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var claim = new ClaimLine
                        {
                            Document_No = (string)config["Document_No"],
                            Line_No = (int)config["Line_No"],
                            Payee = (string)config["Payee"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            G_L_Account = (string)config["G_L_Account"],
                            Vote_Item = (string)config["Vote_Item"],
                            Quantity = (int)config["Quantity"],
                            Rate = (decimal)config["Rate"],
                            Total = (decimal)config["Total"],
                            Status = (string)config["Status"],
                            Recalled_By = (string)config["Recalled_By"],
                            Recalled_On = (DateTime)config["Recalled_On"],
                            SupplierInvoiceNo = (string)config["Supplier_Invoice_No"],
                            SupplierInvoiceDate = (string)config["Supplier_Invoice_Date"],
                            Supplier = (string)config["Supplier"],
                            ExpenseDescription = (string)config["Expense_Description"],
                            Source_Line_No = (int)config["Source_Line_No"],
                            LpoNo = (string)config["Lpo_No"],
                            ContractNo = (string)config["Contract_No"],
                            ItemNo = (string)config["Item_No"],
                            ItemToLinePay = (string)config["Item_To_Line_pay"],
                        };
                        claimLines.Add(claim);
                    }
                }
                var lines = new ClaimLineList
                {
                    Status = Status,
                    ListOfCLaimLines = claimLines
                };
                return PartialView("PartialViews/ContractPaymentLines", lines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        ///AIE/////
        /// 
        public ActionResult AieView(string aieType)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");
                ViewBag.AieType = aieType;
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error
                {
                    Message = ex.Message
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult AieList(string aieType)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var geographicalLocation = employee?.GlobalDimension1Code;

                if (aieType == "Special")
                {
                    var specialAieList = new List<AieLines>();

                    var page = "SpecialAieLines?$filter=Global_Dimension_1_Code eq '" + geographicalLocation + "'" +
                               " and Approval_Status eq 'Approved' and Posted eq true and Type eq 'Custom'&$format=json";

                    var httpResponse = Credentials.GetOdataData(page);
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (var jToken in details["value"])
                        {
                            var config = (JObject)jToken;
                            var assignedAsset = new AieLines
                            {
                                DocumentNo = (string)config["Document_No"],
                                GLAccount = (string)config["G_L_Account"],
                                GLAccountName = (string)config["G_L_Account_Name"],
                                AmountToAuthorize = (int)config["Amount_to_Authorize"],
                                PlannedAmount = (int)config["Planned_Amount"],
                                Balance = (int)config["Balance"]
                            };
                            specialAieList.Add(assignedAsset);
                        }
                    }
                    return PartialView("PartialViews/AieLinesList", specialAieList);
                }
                else
                {
                    var normalAieList = new List<Aie>();

                    var page = "AieCard?$filter=Global_Dimension_1_Code eq '" + geographicalLocation +
                               "' and Approval_Status eq 'Approved' and Posted eq true and Type eq 'Default'&$format=json";

                    var httpResponse = Credentials.GetOdataData(page);
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (var jToken in details["value"])
                        {
                            var config = (JObject)jToken;
                            var assignedAsset = new Aie
                            {
                                No = (string)config["No"],
                                Description = (string)config["Description"],
                                Geolocation = CommonController.GetStaticDimensionName((string)config["Global_Dimension_1_Code"]),
                                AdminUnit = CommonController.GetStaticDimensionName((string)config["Global_Dimension_2_Code"]),
                                ReportingPeriod = (string)config["Reporting_Period"],
                                Workplan = (string)config["Workplan"],
                                BudgetCode = (string)config["Budget_Code"],
                                Quarter = (string)config["Quarter"],
                                AmountToAuthorize = ((decimal)config["Amount_to_Authorize"]).ToString("C", new CultureInfo("sw-KE")),
                                ApprovalStatus = (string)config["Approval_Status"]
                            };
                            normalAieList.Add(assignedAsset);
                        }
                    }

                    return PartialView("PartialViews/AieList", normalAieList);
                }
            }
            catch (Exception ex)
            {
                var errorMsg = new Error
                {
                    Message = ex.Message
                };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", errorMsg);
            }
        }


        public ActionResult AieReport(string docNo)
        {
            ViewBag.DocumentNo = docNo;
            return PartialView("PartialViews/PerActivity");
        }
        public ActionResult GenerateAieReport(string documentNumber, string reportType)
        {
            try
            {
                string message = "";
                bool success = false;
                bool isExcel = reportType == "1";

                message = Credentials.ObjNav.GenerateAieReport(documentNumber, Convert.ToInt32(reportType));

                if (string.IsNullOrEmpty(message))
                {
                    success = false;
                    message = "File Not Found";
                }
                else
                {
                    success = true;
                }
                return Json(new { message, success, view = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}