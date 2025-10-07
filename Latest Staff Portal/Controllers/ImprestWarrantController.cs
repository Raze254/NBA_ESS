using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    public class ImprestWarrantController : Controller
    {
        // GET: ImprestWarrant
        public ActionResult ImprestWarrantRequisitionList()
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
        public PartialViewResult ImprestWarrantRequisitionListPartialView()
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var ImpList = new List<ImprestWarranties>();

                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = $"ImprestWarranties?$filter=Account_No eq '{StaffNo}' and Posted eq true &$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var ImList = new ImprestWarranties
                        {
                            No = (string)config["No"],
                            Dimension_Set_ID = (int?)config["Dimension_Set_ID"] ?? 0,
                            Date = (string)config["Date"],
                            Posting_Date = (string)config["Posting_Date"],
                            Account_Type = (string)config["Account_Type"],
                            Account_No = (string)config["Account_No"],
                            Account_Name = (string)config["Account_Name"],
                            Payee_Bank_Account = (string)config["Payee_Bank_Account"],
                            Payee_Bank_Code = (string)config["Payee_Bank_Code"],
                            Payee = (string)config["Payee"],
                            ValidatedBankName = (string)config["ValidatedBankName"],
                            Reference_No = (string)config["Reference_No"],
                            Pay_Mode = (string)config["Pay_Mode"],
                            Cheque_No = (string)config["Cheque_No"],
                            Paying_Bank_Account = (string)config["Paying_Bank_Account"],
                            Bank_Name = (string)config["Bank_Name"],
                            Available_Amount = (int?)config["Available_Amount"] ?? 0,
                            Committed_Amount = (int?)config["Committed_Amount"] ?? 0,
                            AIE_Receipt = (string)config["AIE_Receipt"],
                            Travel_Date = (string)config["Travel_Date"],
                            Payment_Narration = (string)config["Payment_Narration"],
                            Created_By = (string)config["Created_By"],
                            Status = (string)config["Status"],
                            Strategic_Plan = (string)config["Strategic_Plan"],
                            Reporting_Year_Code = (string)config["Reporting_Year_Code"],
                            Workplan_Code = (string)config["Workplan_Code"],
                            Activity_Code = (string)config["Activity_Code"],
                            Expenditure_Requisition_Code = (string)config["Expenditure_Requisition_Code"],
                            Imprest_Memo_No = (string)config["Imprest_Memo_No"],
                            Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                            Department_Name = (string)config["Department_Name"],
                            Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                            Project_Name = (string)config["Project_Name"],
                            Imprest_Amount = (int?)config["Imprest_Amount"] ?? 0,
                            Imprest_Deadline = (string)config["Imprest_Deadline"],
                            Posted = (bool?)config["Posted"] ?? false

                        };

                        ImpList.Add(ImList);
                    }
                }

                return PartialView("~/Views/ImprestWarrant/PartialViews/ImprestWarrantRequisitionListPartialView.cshtml", ImpList.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error
                {
                    Message = ex.Message.Replace("'", "")
                };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [HttpPost]
        public ActionResult ImprestWarrantDocumentView(string No)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");

                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var ImpDoc = new ImprestWarranties();

                var page = $"ImprestWarranties?$filter=No eq '{No}'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        ImpDoc = new ImprestWarranties
                        {
                            No = (string)config["No"],
                            Dimension_Set_ID = (int?)config["Dimension_Set_ID"] ?? 0,
                            Date = (string)config["Date"],
                            Posting_Date = (string)config["Posting_Date"],
                            Account_Type = (string)config["Account_Type"],
                            Account_No = (string)config["Account_No"],
                            Account_Name = (string)config["Account_Name"],
                            Payee_Bank_Account = (string)config["Payee_Bank_Account"],
                            Payee_Bank_Code = (string)config["Payee_Bank_Code"],
                            Payee = (string)config["Payee"],
                            ValidatedBankName = (string)config["ValidatedBankName"],
                            Reference_No = (string)config["Reference_No"],
                            Pay_Mode = (string)config["Pay_Mode"],
                            Cheque_No = (string)config["Cheque_No"],
                            Paying_Bank_Account = (string)config["Paying_Bank_Account"],
                            Bank_Name = (string)config["Bank_Name"],
                            Available_Amount = (int?)config["Available_Amount"] ?? 0,
                            Committed_Amount = (int?)config["Committed_Amount"] ?? 0,
                            AIE_Receipt = (string)config["AIE_Receipt"],
                            Travel_Date = (string)config["Travel_Date"],
                            Payment_Narration = (string)config["Payment_Narration"],
                            Created_By = (string)config["Created_By"],
                            Status = (string)config["Status"],
                            Strategic_Plan = (string)config["Strategic_Plan"],
                            Reporting_Year_Code = (string)config["Reporting_Year_Code"],
                            Workplan_Code = (string)config["Workplan_Code"],
                            Activity_Code = (string)config["Activity_Code"],
                            Expenditure_Requisition_Code = (string)config["Expenditure_Requisition_Code"],
                            Imprest_Memo_No = (string)config["Imprest_Memo_No"],
                            Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                            Department_Name = (string)config["Department_Name"],
                            Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                            Project_Name = (string)config["Project_Name"],
                            Imprest_Amount = (int?)config["Imprest_Amount"] ?? 0,
                            Imprest_Deadline = (string)config["Imprest_Deadline"],
                            Posted = (bool?)config["Posted"] ?? false
                        };
                    }
                }

                return View(ImpDoc);
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
        public PartialViewResult ImprestWarrantLines(string DocNo, string Status, string applyToDocumentNumber)
        {
            try
            {
                #region Imprest warrant Lines

                var imprestWarrantLines = new List<WarrantImprestLines>();
                var pageLine = "WarrantImprestLines?$filter=No eq '" + DocNo + "'&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);

                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        var warrantLine = new WarrantImprestLines
                        {
                            No = (string)config["No"],
                            Line_No = (int?)config["Line_No"] ?? 0,
                            Advance_Type = (string)config["Advance_Type"],
                            Account_Type = (string)config["Account_Type"],
                            Account_No = (string)config["Account_No"],
                            Account_Name = (string)config["Account_Name"],
                            Purpose = (string)config["Purpose"],
                            Daily_Rate = (int?)config["Daily_Rate"] ?? 0,
                            No_of_Days = (int?)config["No_of_Days"] ?? 0,
                            Vote_Item = (string)config["Vote_Item"],
                            Currency_Code = (string)config["Currency_Code"],
                            Amount = (int?)config["Amount"] ?? 0,
                            Project = (string)config["Project"],
                            Task_No = (string)config["Task_No"]
                        };

                        imprestWarrantLines.Add(warrantLine);
                    }
                }

                #endregion

                return PartialView("~/Views/ImprestWarrant/PartialViews/ImprestWarrantLines.cshtml", imprestWarrantLines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error
                {
                    Message = ex.Message.Replace("'", "")
                };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }



        public ActionResult NewImprestWarrantRequest()
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");
                var StaffNo = Session["Username"].ToString();

                var NewImprestWarrant = new ImprestWarranties();

                #region PayrollNo
                var PayrollNoList = new List<DropdownList>();
                var page = "Customers?$format=json";

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
                            Text = (string)config1["Name"] + "-" + (string)config1["No"],
                            Value = (string)config1["No"]
                        };
                        PayrollNoList.Add(dropdownList);
                    }
                }
                #endregion

                #region PayMode
                var PayModeList = new List<DropdownList>();
                var pagePayMode = "PayMode?$format=json";

                var httpResponsePayMode = Credentials.GetOdataData(pagePayMode);
                using (var streamReader = new StreamReader(httpResponsePayMode.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Description"],
                            Value = (string)config1["Code"]
                        };
                        PayModeList.Add(dropdownList);
                    }
                }
                #endregion

                #region StrategicPlan
                var StrategicPlanList = new List<DropdownList>();
                var pageStrategicPlan = "AllCSPS?$format=json";

                var httpResponseStrategicPlan = Credentials.GetOdataData(pageStrategicPlan);
                using (var streamReader = new StreamReader(httpResponseStrategicPlan.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Description"],
                            Value = (string)config1["Code"]
                        };
                        StrategicPlanList.Add(dropdownList);
                    }
                }
                #endregion

                #region WorkplanActivities

                var WorkplanActivitiesList = new List<DropdownList>();
                var pageWorkplanActivities = "WorkPlanActivities?$format=json";

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

                #region ImplementationYears

                var ImplementationYearsList = new List<DropdownList>();
                var pageImplementationYears = "ImplementationYears?$format=json";

                var httpImplementationYears = Credentials.GetOdataData(pageImplementationYears);
                using (var streamReader = new StreamReader(httpImplementationYears.GetResponseStream()))
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
                        ImplementationYearsList.Add(dropdownList);
                    }
                }
                #endregion

                #region ExpenseRequisitions

                var ExpenseRequisitionsList = new List<DropdownList>();
                var pageExpenseRequisitions = "ExpenseRequisitions?$format=json";

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


                NewImprestWarrant.ListOfPayroll = PayrollNoList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();


                NewImprestWarrant.ListOfPayModes = PayModeList.Select(x =>
                 new SelectListItem
                 {
                     Text = x.Text,
                     Value = x.Value
                 }).ToList();



                NewImprestWarrant.ListOfStratPlans = StrategicPlanList.Select(x =>
                      new SelectListItem
                      {
                          Text = x.Text,
                          Value = x.Value
                      }).ToList();

                NewImprestWarrant.ListOfWPA = WorkplanActivitiesList.Select(x =>
                      new SelectListItem
                      {
                          Text = x.Text,
                          Value = x.Value
                      }).ToList();


                NewImprestWarrant.ListOfImplYears = ImplementationYearsList.Select(x =>
                      new SelectListItem
                      {
                          Text = x.Text,
                          Value = x.Value
                      }).ToList();

                NewImprestWarrant.ListOfExpREq = ExpenseRequisitionsList.Select(x =>
                      new SelectListItem
                      {
                          Text = x.Text,
                          Value = x.Value
                      }).ToList();
                NewImprestWarrant.ListOfDim1 = Dim1List.Select(x =>
                      new SelectListItem
                      {
                          Text = x.Text,
                          Value = x.Value
                      }).ToList();
                NewImprestWarrant.ListOfDim2 = Dim2List.Select(x =>
                      new SelectListItem
                      {
                          Text = x.Text,
                          Value = x.Value
                      }).ToList();


                return View("~/Views/ImprestWarrant/PartialViews/NewImprestWarrantRequest.cshtml", NewImprestWarrant);

            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitImprestWarrantRequisition(ImprestWarranties data)
        {
            var successVal = false;
            try
            {
                /*string Dim1 = "", Dim2 = "", Dim3 = "", Dim4 = "", Dim5 = "", ImpDueTyp = "";
                if (ImprestMemoHeader.Dim1 != null) Dim1 = ImprestMemoHeader.Dim1;
                if (ImprestMemoHeader.Dim2 != null) Dim2 = ImprestMemoHeader.Dim2;
                if (ImprestMemoHeader.Dim3 != null) Dim3 = ImprestMemoHeader.Dim3;
                if (ImprestMemoHeader.Dim4 != null) Dim4 = ImprestMemoHeader.Dim4;
                if (ImprestMemoHeader.Dim5 != null) Dim5 = ImprestMemoHeader.Dim5;
                if (ImprestMemoHeader.ImprestDueType != null) ImpDueTyp = ImprestMemoHeader.ImprestDueType;
                var x = 0;
                var y = 0;
                var s = "";
                if (ImprestMemoHeader.Body != null) s = ImprestMemoHeader.Body;
                string[] Bd = { "", "", "", "", "", "", "", "", "", "", "" };
                var a = 0;
                for (var i = 0; i < s.Length; i = i + 250)
                {
                    x = 250;
                    y = i;


                    if (s.Length - y < x) x = s.Length - y;


                    var n = s.Substring(y, x);
                    a = a + 1;
                    Bd[a] = n;
                }*/

                if (Session["UserID"] == null || Session["Username"] == null) return RedirectToAction("Login", "Login");

                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();
                var Travel_Date = DateTime.ParseExact(data.Travel_Date.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
                var Imprest_Deadline = DateTime.ParseExact(data.Imprest_Deadline.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
                var DocNo = "";

                /*DocNo = Credentials.ObjNav.imprest();*/
                if (DocNo != "")
                {
                    var Redirect = "/ImprestWarrant/ImprestWarrantDocumentView?DocNo=" + DocNo;

                    Session["SuccessMsg"] = "ImprestMemo Requisition, Document No: " + DocNo +
                                            ", created Successfully. Add line(s) and attachment(s) then sent for approval";
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
        public ActionResult NewImprestWarrantLine(string docNo)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");
                var StaffNo = Session["Username"].ToString();

                var NewImprestWarrantLine = new WarrantImprestLines();
                NewImprestWarrantLine.No = docNo;

                #region GlAccount
                var GlAccountList = new List<DropdownList>();
                var pageGL = "glAccount?$format=json";

                var httpResponseGL = Credentials.GetOdataData(pageGL);
                using (var streamReader = new StreamReader(httpResponseGL.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Name"] + "-" + (string)config1["No"],
                            Value = (string)config1["No"]
                        };
                        GlAccountList.Add(dropdownList);
                    }
                }
                #endregion

                #region VoteItem
                var VoteItemList = new List<DropdownList>();
                var pagePayMode = "glAccount?$format=json";

                var httpResponsePayMode = Credentials.GetOdataData(pagePayMode);
                using (var streamReader = new StreamReader(httpResponsePayMode.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Name"] + "-" + (string)config1["No"],
                            Value = (string)config1["No"]
                        };
                        VoteItemList.Add(dropdownList);
                    }
                }
                #endregion



                NewImprestWarrantLine.ListOfGLAccounts = GlAccountList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();


                NewImprestWarrantLine.ListOfVoteItems = VoteItemList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();



                return View("~/Views/ImprestWarrant/PartialViews/NewImprestWarrantLine.cshtml", NewImprestWarrantLine);

            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitImprestWarrantLine(WarrantImprestLines data)
        {
            var successVal = false;
            try
            {
                /*string Dim1 = "", Dim2 = "", Dim3 = "", Dim4 = "", Dim5 = "", ImpDueTyp = "";
                if (ImprestMemoHeader.Dim1 != null) Dim1 = ImprestMemoHeader.Dim1;
                if (ImprestMemoHeader.Dim2 != null) Dim2 = ImprestMemoHeader.Dim2;
                if (ImprestMemoHeader.Dim3 != null) Dim3 = ImprestMemoHeader.Dim3;
                if (ImprestMemoHeader.Dim4 != null) Dim4 = ImprestMemoHeader.Dim4;
                if (ImprestMemoHeader.Dim5 != null) Dim5 = ImprestMemoHeader.Dim5;
                if (ImprestMemoHeader.ImprestDueType != null) ImpDueTyp = ImprestMemoHeader.ImprestDueType;
                var x = 0;
                var y = 0;
                var s = "";
                if (ImprestMemoHeader.Body != null) s = ImprestMemoHeader.Body;
                string[] Bd = { "", "", "", "", "", "", "", "", "", "", "" };
                var a = 0;
                for (var i = 0; i < s.Length; i = i + 250)
                {
                    x = 250;
                    y = i;


                    if (s.Length - y < x) x = s.Length - y;


                    var n = s.Substring(y, x);
                    a = a + 1;
                    Bd[a] = n;
                }*/

                if (Session["UserID"] == null || Session["Username"] == null) return RedirectToAction("Login", "Login");

                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();
               
                var DocNo = "";

                /*DocNo = Credentials.ObjNav.imprest();*/
                if (DocNo != "")
                {
                    var Redirect = "/ImprestWarrant/ImprestWarrantDocumentView?DocNo=" + DocNo;

                    Session["SuccessMsg"] = "Record adde successfully";
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { message = "Record not created. Please try again later...", success = false },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (successVal) Session["ErrorMsg"] = ex.Message.Replace("'", "");
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateImprestLine(string documentNo, string lineNo, string actualSpent, string receiptNo)
        {
            try
            {
                var staffNumber = Session["Username"].ToString();
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
        public JsonResult SendImprestWarrantForApproval(string DocNo)
        {
            try
            {
                {
                    var employee = Session["EmployeeData"] as EmployeeView;
                    var userId = employee?.UserID;
                    Credentials.ObjNav.SendImprestWarrantforApproval(DocNo);
                    Credentials.ObjNav.UpdateApprovalEntrySenderID(57000, DocNo, userId);
                    return Json(new { message = "Imprest Warrant, Document No " + DocNo + " sent for approval Successfully", success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


    }
}