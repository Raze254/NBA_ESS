using iTextSharp.text;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Threading;
using System.Web.Mvc;
using System.Web.Services.Description;
using static iTextSharp.text.pdf.PdfDocument;

namespace Latest_Staff_Portal.Controllers;

[CustomeAuthentication]
[CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]
public class ImprestMemoController : Controller
{
    // GET: Imprest
    public ActionResult ImprestMemoRequisitionList()
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

    public PartialViewResult ImprestMemoRequisitionListPartialView()
    {
        try
        {
            var UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;
            var ImpList = new List<ImprestMemoList>();

            var role = Session["ESSRoleSetup"] as ESSRoleSetup;
            var page = $"SafariImprest?$filter=Requestor eq '{StaffNo}'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    var ImList = new ImprestMemoList
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

                    ImpList.Add(ImList);
                }
            }

            return PartialView("~/Views/ImprestMemo/ImprestMemoReqListView.cshtml", ImpList.OrderByDescending(x => x.No));
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

    public ActionResult NewImprestMemoRequest()
    {
        try
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");
            var employeeView = Session["EmployeeData"] as EmployeeView;
            var StaffNo = Session["Username"].ToString();
            var NewImprestMemo = new ImprestMemoList();
            string Dim1 = employeeView.GlobalDimension1Code;
            string Dim2 = employeeView.GlobalDimension2Code;

            NewImprestMemo.Global_Dimension_2_Code = employeeView.GlobalDimension2Code;

            #region Employee Data

            var pageData = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(pageData);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                if (details["value"].Count() > 0)
                    foreach (JObject config in details["value"])
                    {
                        //Dim1 = (string)config["Global_Dimension_1_Code"];
                        // Dim2 = (string)config["GlobalDimension2Code"];
                    }
            }

            #endregion

            #region Region

            var Dim1List = new List<DimensionValues>();
            /*var pageDepartment =
                "DimensionValueList?$filter=Global_Dimension_No eq 1 and Blocked eq false&$format=json";*/

            var pageDepartment = "DimensionValueList?$filter=Dimension_Code eq 'REGIONS' and Blocked eq false&$format=json";

            var httpResponseDepartment = Credentials.GetOdataData(pageDepartment);
            using (var streamReader = new StreamReader(httpResponseDepartment.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Department = new DimensionValues();
                    Department.Code = (string)config["Code"];
                    Department.Name = (string)config["Name"];
                    Dim1List.Add(Department);
                }
            }

            #endregion

            #region Department

            var Dim2List = new List<DimensionValues>();
            /*var pageDivision ="DimensionValueList?$filter=Global_Dimension_No eq 2 and Blocked eq false&$format=json";*/
            var pageDivision = "DimensionValueList?$filter=Dimension_Code eq 'DEPARTMENT' and Blocked eq false&$format=json";

            var httpResponseDivision = Credentials.GetOdataData(pageDivision);
            using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var DList = new DimensionValues();
                    DList.Code = (string)config["Code"];
                    DList.Name = (string)config["Name"];
                    Dim2List.Add(DList);
                }
            }

            #endregion


            #region StrategicPlan

            var StratPlanCList = new List<RespCenter>();
            var pageStratPlan = "AllCSPS?$format=json";

            var httpResponseStratPlan = Credentials.GetOdataData(pageStratPlan);
            using (var streamReader = new StreamReader(httpResponseStratPlan.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var RCList = new RespCenter();
                    RCList.Code = (string)config["Code"];
                    RCList.Name = (string)config["Description"] + "-" + (string)config["Code"];
                    StratPlanCList.Add(RCList);
                }
            }

            #endregion

            #region WorkplanActivities

            var WorkplanActivitiesList = new List<RespCenter>();
            var pageWorkplanActivities = "WorkplanActivities?$format=json";

            var httpWorkplanActivities = Credentials.GetOdataData(pageWorkplanActivities);
            using (var streamReader = new StreamReader(httpWorkplanActivities.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var RCList = new RespCenter();
                    RCList.Code = (string)config["Code"];
                    RCList.Name = (string)config["Descriptions"] + "-" + (string)config["Code"];
                    WorkplanActivitiesList.Add(RCList);
                }
            }
            #endregion

            #region ImplementationYears

            var ImplementationYearsList = new List<RespCenter>();
            var pageImplementationYears = "ImplementationYears?$format=json";

            var httpImplementationYears = Credentials.GetOdataData(pageImplementationYears);
            using (var streamReader = new StreamReader(httpImplementationYears.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var RCList = new RespCenter();
                    RCList.Code = (string)config["Annual_Year_Code"];
                    RCList.Name = (string)config["Description"];
                    ImplementationYearsList.Add(RCList);
                }
            }
            #endregion

            #region ExpenseRequisitions

            var ExpenseRequisitionsList = new List<RespCenter>();
            var pageExpenseRequisitions = "ExpenditureRequisitions?$format=json";

            var httpExpenseRequisitions = Credentials.GetOdataData(pageExpenseRequisitions);
            using (var streamReader = new StreamReader(httpExpenseRequisitions.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var RCList = new RespCenter();
                    RCList.Code = (string)config["Annual_Year_Code"];
                    RCList.Name = (string)config["Description"];
                    ExpenseRequisitionsList.Add(RCList);
                }
            }
            #endregion

            #region Responsibility

            var RespCList = new List<RespCenter>();
            var pageResC = "DimensionValueList?$format=json";

            var httpResponseResC3 = Credentials.GetOdataData(pageResC);
            using (var streamReader = new StreamReader(httpResponseResC3.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var RCList = new RespCenter();
                    RCList.Code = (string)config["Code"];
                    RCList.Name = (string)config["Name"];
                    RespCList.Add(RCList);
                }
            }

            #endregion

            #region ImprestDue

            // List<ImprestDuetype> ImprestDue = new List<ImprestDuetype>();
            var ImmpsDue = new List<ImpDuetyp>();
            var pageImprestDueType = "DimensionValueList?$format=json";

            var httpResponseImpDue = Credentials.GetOdataData(pageImprestDueType);
            using (var streamReader = new StreamReader(httpResponseImpDue.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var impD = new ImpDuetyp();
                    impD.Code = (string)config["Code"];
                    impD.Name = (string)config["Code"];
                    ImmpsDue.Add(impD);
                }
            }

            #endregion

            #region EmployeeList

            var employeeList = new List<EmployeeList>();
            /* var Department2 = CommonClass.EmployeeDepartment(StaffNo);

             var Departments = CommonClass.EmployeeDepartment(StaffNo);*/
            var pageReliever = "EmployeeList?$format=json";


            var httpResponseReliever = Credentials.GetOdataData(pageReliever);
            using (var streamReader = new StreamReader(httpResponseReliever.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                    if ((string)config["FirstName"] != "")
                    {
                        var Rlist = new EmployeeList();
                        Rlist.No = (string)config["No"];
                        Rlist.Name = (string)config["FirstName"] + " " + (string)config["MiddleName"] + " " +
                                     (string)config["LastName"];
                        employeeList.Add(Rlist);
                    }
            }

            #endregion

            NewImprestMemo = new ImprestMemoList
            {
                Global_Dimension_1_Code = Dim1,
                Global_Dimension_2_Code = Dim2,
                ListOfDim1 = Dim1List.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList(),
                ListOfDim2 = Dim2List.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList(),


                ListOfStratPlans = StratPlanCList.Select(x =>
                   new SelectListItem
                   {
                       Text = x.Name,
                       Value = x.Code
                   }).ToList(),

                ListOfWorkplanActivities = WorkplanActivitiesList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Name,
                      Value = x.Code
                  }).ToList(),


                ListOfImplementationYears = ImplementationYearsList.Select(x =>
                 new SelectListItem
                 {
                     Text = x.Name,
                     Value = x.Code
                 }).ToList(),


                ListOfExpenseRequisitions = ExpenseRequisitionsList.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Code
                }).ToList(),



                ListOfResponsibility = RespCList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList(),
                ListOfEmployeeList = employeeList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.No
                    }).ToList(),
                ListOfImprestDue = ImmpsDue.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList()
            };
            return View("~/Views/ImprestMemo/NewImprestMemoRequest2.cshtml", NewImprestMemo);

        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message.Replace("'", "");
            return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
        }
    }

    public JsonResult WorkplansJson(string Global_Dimension_1_Code, string Global_Dimension_2_Code, string Strategic_Plan, string Reporting_Year_Code)
    {
        try
        {
            List<object> workplan = new List<object>();

            var pageWorkplanActivities = $"DraftWorkPlans?$filter=Global_Dimension_1_Code eq '{Global_Dimension_1_Code}' and Global_Dimension_2_Code eq '{Global_Dimension_2_Code}' and Strategy_Plan_ID eq '{Strategic_Plan}' and Year_Reporting_Code eq '{Reporting_Year_Code}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(pageWorkplanActivities);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    workplan.Add(new
                    {
                        Code = (string)config["No"],
                        Descriptions = (string)config["Description"]
                    });
                }
            }

            return Json(workplan, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult WorkplanActivitiesJson(string WorkPlanCode)
    {
        try
        {
            List<object> workplan = new List<object>();

            var pageWorkplanActivities = $"StrategyWorkplanLines?$filter=No eq '{WorkPlanCode}'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(pageWorkplanActivities);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    workplan.Add(new
                    {
                        Code = (string)config["Resource_Req_No"],
                        Descriptions = (string)config["Description"]
                    });
                }
            }

            return Json(workplan, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        }
    }





    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult SubmitImprestMemoRequisition(ImprestMemoList data)
    {
        var successVal = false;
        try
        {

            if (Session["UserID"] == null || Session["Username"] == null) return RedirectToAction("Login", "Login");

            var StaffNo = Session["Username"].ToString();
            var UserID = Session["UserID"].ToString();
            var Start_Date = DateTime.ParseExact(data.Start_Date.Replace("-", "/"), "dd/MM/yyyy",
                CultureInfo.InvariantCulture);
            var res = "";

            res = Credentials.ObjNav.imprestGeneralDetails(
                StaffNo,
                "",
                data.Subject,
                data.Objective,
                data.Imprest_Naration,
                Start_Date,
                data.No_of_days,
                "",
                "",
                "",
                UserID

            );
            if (res != "")
            {
                if (res == "")
                {
                    var msg = res;
                    return Json(new { message = msg, success = false }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var Redirect = "/ImprestMemo/ImprestMemoDocumentView?DocNo=" + res;

                    Session["SuccessMsg"] = "ImprestMemo Requisition, Document No: " + res +
                                            ", created Successfully. Add line(s) and attachment(s) then sent for approval";
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
                }

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
    public ActionResult ImprestMemoDocumentView(string DocNo)
    {
        try
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");

            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;
            var ImpDoc = new ImprestMemoList();

            var page = $"SafariImprest?$filter=No eq '{DocNo}'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    ImpDoc = new ImprestMemoList
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
                        Total_Entitlement = (int)(config["Total_Entitlement"] ?? 0),
                        Total_Fuel_Costs = (int)(config["Total_Fuel_Costs"] ?? 0),
                        Total_Maintenance_Costs = (int)(config["Total_Maintenance_Costs"] ?? 0),
                        Total_Casuals_Cost = (int)(config["Total_Casuals_Cost"] ?? 0),
                        Total_Other_Costs = (int)(config["Total_Other_Costs"] ?? 0),
                        Status = (string)config["Status"],
                        Global_Dimension_1_Code = employeeView.GlobalDimension1Code,
                        Department_Name = (string)config["Department_Name"],
                        Global_Dimension_2_Code = employeeView.GlobalDimension2Code,
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
    public ActionResult ImprestMemoDocumentView2(string DocNo)
    {
        try
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");

            var StaffNo = Session["Username"].ToString();

            #region Imp Header

            var ImpDoc = new ImprestMemoHeader();

            #region Dim1 List

            var Dim1List = new List<DimensionValues>();
            var pageDepartment =
                "DimensionValueList?$format=json";

            var httpResponseDepartment = Credentials.GetOdataData(pageDepartment);
            using (var streamReader = new StreamReader(httpResponseDepartment.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Department = new DimensionValues();
                    Department.Code = (string)config["Code"];
                    Department.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    Dim1List.Add(Department);
                }
            }

            #endregion

            #region dim2

            var Dim2List = new List<DimensionValues>();
            var pageDivision = "DimensionValueList?$format=json";

            var httpResponseDivision = Credentials.GetOdataData(pageDivision);
            using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var DList = new DimensionValues();
                    DList.Code = (string)config["Code"];
                    DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    Dim2List.Add(DList);
                }
            }

            #endregion

            #region dim3

            var Dim3List = new List<DimensionValues>();
            var pageDim3 = "DimensionValueList?$format=json";

            var httpResponseDim3 = Credentials.GetOdataData(pageDim3);
            using (var streamReader = new StreamReader(httpResponseDim3.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var DList = new DimensionValues();
                    DList.Code = (string)config["Code"];
                    DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    Dim3List.Add(DList);
                }
            }

            #endregion

            #region dim4

            var Dim4List = new List<DimensionValues>();
            var pageDim4 = "DimensionValueList?$format=json";

            var httpResponseDim4 = Credentials.GetOdataData(pageDim4);
            using (var streamReader = new StreamReader(httpResponseDim4.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var DList = new DimensionValues();
                    DList.Code = (string)config["Code"];
                    DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    Dim4List.Add(DList);
                }
            }

            #endregion

            #region dim5

            var Dim5List = new List<DimensionValues>();
            var pageDim5 = "DimensionValueList?$format=json";

            var httpResponseDim5 = Credentials.GetOdataData(pageDim5);
            using (var streamReader = new StreamReader(httpResponseDim5.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var DList = new DimensionValues();
                    DList.Code = (string)config["Code"];
                    DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    Dim5List.Add(DList);
                }
            }

            #endregion

            #region Responsibility

            var RespCList = new List<RespCenter>();
            var pageResC = "ResponsibilityCenters?$format=json";

            var httpResponseResC = Credentials.GetOdataData(pageResC);
            using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var RCList = new RespCenter();
                    RCList.Code = (string)config["Code"];
                    RCList.Name = (string)config["Name"];
                    RespCList.Add(RCList);
                }
            }

            #endregion

            #region EmployeeList

            var employeeList = new List<EmployeeList>();
            // string Department2 = CommonClass.EmployeeDepartment(StaffNo);

            var Departments = CommonClass.EmployeeDepartment(StaffNo);
            var pageReliever = "EmployeeList?$filter=Status eq 'Active' and Directorate_Code eq '" + Departments +
                               "'&format=json";


            var httpResponseReliever = Credentials.GetOdataData(pageReliever);
            using (var streamReader = new StreamReader(httpResponseReliever.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                    if ((string)config["FirstName"] != "")
                    {
                        var Rlist = new EmployeeList();
                        Rlist.No = (string)config["No"];
                        Rlist.Name = (string)config["FirstName"] + " " + (string)config["MiddleName"] + " " +
                                     (string)config["LastName"];
                        employeeList.Add(Rlist);
                    }
            }

            #endregion

            #region ImprestDue

            // List<ImprestDuetype> ImprestDue = new List<ImprestDuetype>();
            var ImmpsDue = new List<ImpDuetyp>();
            var pageImprestDueType = "ImprestDueType?$format=json";

            var httpResponseImpDue = Credentials.GetOdataData(pageImprestDueType);
            using (var streamReader = new StreamReader(httpResponseImpDue.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var impD = new ImpDuetyp();
                    impD.Code = (string)config["Code"];
                    impD.Name = (string)config["Code"];
                    ImmpsDue.Add(impD);
                }
            }

            #endregion

            var page = "ImprestMemo?$filter=No eq '" + DocNo + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ImpDoc.No = (string)config["No"];
                    ImpDoc.DocumentDate = Convert.ToDateTime((string)config["Date"]).ToString("dd/MM/yyyy");
                    ImpDoc.DateNeeded = Convert.ToDateTime((string)config["DateRequired"]).ToString("dd/MM/yyyy");
                    ImpDoc.Remarks = (string)config["Purpose"];
                    ImpDoc.DocD = new NewImprestMemoRequisition
                    {
                        Dim1 = (string)config["GlobalDimension1Code"],
                        Dim2 = (string)config["ShortcutDimension2Code"],
                        Dim3 = (string)config["ShortcutDimension3Code"],
                        Dim4 = (string)config["ShortcutDimension4Code"],
                        Dim5 = (string)config["ShortcutDimension5Code"]
                    };
                    ImpDoc.Dim1 = (string)config["GlobalDimension1Code"];
                    ImpDoc.Dim2 = (string)config["ShortcutDimension2Code"];
                    ImpDoc.Dim3 = (string)config["ShortcutDimension3Code"];
                    ImpDoc.Dim4 = (string)config["ShortcutDimension4Code"];
                    ImpDoc.Dim5 = (string)config["ShortcutDimension5Code"];


                    ImpDoc.TotalAmount = Convert.ToDecimal((string)config["TotalNetAmount"]).ToString("#,##0.00");
                    ImpDoc.Status = (string)config["Status"];
                    ImpDoc.Subject = (string)config["Subject"];
                    ImpDoc.Body = (string)config["Body1"] + (string)config["Body2"] + (string)config["Body3"] +
                                  (string)config["Body4"] +
                                  (string)config["Body5"] + (string)config["Body6"] + (string)config["Body7"] +
                                  (string)config["Body8"] + (string)config["Body9"] +
                                  (string)config["Body10"];
                    ImpDoc.To = (string)config["MemoTo"];
                    ImpDoc.From = (string)config["MemoFrom"];
                    ImpDoc.ImprestDueType = (string)config["ImprestDueType"];
                }
            }

            ImpDoc.DocD.ListOfDim1 = Dim1List.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Code
                }).ToList();
            ImpDoc.DocD.ListOfDim2 = Dim2List.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Code
                }).ToList();
            ImpDoc.DocD.ListOfDim3 = Dim3List.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Code
                }).ToList();
            ImpDoc.DocD.ListOfDim4 = Dim4List.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Code
                }).ToList();
            ImpDoc.DocD.ListOfDim5 = Dim5List.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Code
                }).ToList();
            ImpDoc.DocD.ListOfResponsibility = RespCList.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Code
                }).ToList();

            ImpDoc.ListOfEmployeeList = employeeList.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.No
                }).ToList();
            ImpDoc.ListOfImprestDue = ImmpsDue.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Code
                }).ToList();

            #endregion

            return View(ImpDoc);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message.Replace("'", "");
            return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
        }
    }
    public PartialViewResult ImprestMemoDocumentLines(string DocNo, string Status)
    {
        try
        {
            var ImpLines = new List<SafariTeam2>();
            var pageLine = "SafariTeam?$filter=Imprest_Memo_No eq '" + DocNo + "'&$format=json";
            var httpResponseLine = Credentials.GetOdataData(pageLine);

            using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    var ImLine = new SafariTeam2
                    {
                        Imprest_Memo_No = (string)config["Imprest_Memo_No"],
                        Line_No = (int)config["Line_No"],
                        Work_Type = (string)config["Work_Type"],
                        Type = (string)config["Type"],
                        Type_of_Expense = (string)config["Type_of_Expense"],
                        No = (string)config["No"],
                        G_L_Account = (string)config["G_L_Account"],
                        Task_No = (string)config["Task_No"],
                        Name = (string)config["Name"],
                        Unit_of_Measure = (string)config["Unit_of_Measure"],
                        Currency_Code = (string)config["Currency_Code"],
                        Time_Period = (int)config["Time_Period"],
                        Direct_Unit_Cost = (int)config["Direct_Unit_Cost"],
                        Entitlement = (int)config["Entitlement"],
                        Transport_Costs = (int)config["Transport_Costs"],
                        Total_Entitlement = (int)config["Total_Entitlement"],
                        Outstanding_Amount = (int)config["Outstanding_Amount"],
                        Tasks_to_Carry_Out = (string)config["Tasks_to_Carry_Out"],
                        Expected_Output = (string)config["Expected_Output"],
                        Delivery = (int)config["Delivery"],
                        Project_Lead = (bool)config["Project_Lead"],
                        Dimension1Code = (string)config["Dimension1Code"],
                        Dimension2Code = (string)config["Dimension2Code"],
                        Dimension3Code = (string)config["Dimension3Code"],
                        Dimension4Code = (string)config["Dimension4Code"],
                        Dimension5Code = (string)config["Dimension5Code"],
                        Dimension6Code = (string)config["Dimension6Code"],
                        Dimension7Code = (string)config["Dimension7Code"],
                        Dimension8Code = (string)config["Dimension8Code"],
                        Mileage_KM = (decimal)config["Mileage_KM"],
                        Total_Mileage_Cost = (decimal)config["Total_Mileage_Cost"],
                    }
                ;

                    ImpLines.Add(ImLine);
                }
            }
            ViewBag.Status = Status;
            return PartialView(ImpLines);
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
    public PartialViewResult OtherCosts(string DocNo, string Status)
    {
        try
        {
            var otherCostsLines = new List<OtherCosts>();
            var pageLine = "OtherCosts?$filter=Imprest_Memo_No eq '" + DocNo + "'&$format=json";
            var httpResponseLine = Credentials.GetOdataData(pageLine);

            using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    var costLine = new OtherCosts
                    {
                        Imprest_Memo_No = (string)config["Imprest_Memo_No"],
                        Line_No = (int)config["Line_No"],
                        Type = (string)config["Type"],
                        Type_of_Expense = (string)config["Type_of_Expense"],
                        Description = (string)config["Description"],
                        No = (string)config["No"],
                        Required_For = (string)config["Required_For"],
                        Quantity_Required = (int)config["Quantity_Required"],
                        No_of_Days = (int)config["No_of_Days"],
                        Unit_Cost = (int)config["Unit_Cost"],
                        Line_Amount = (int)config["Line_Amount"],


                    }
                ;

                    otherCostsLines.Add(costLine);
                }
            }
            ViewBag.Status = Status;
            ViewBag.DocNo = DocNo;
            return PartialView(otherCostsLines);
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

    public PartialViewResult ImprestMemoItems(string DocNo, string Status)
    {
        try
        {
            #region Imp Lines

            var ImpLines = new List<ImprestMemoLines>();
            var pageLine = "ImprestMemoLine?$filter=No eq '" + DocNo +
                           "' and ImprestType eq 'ItemCash' &$format=json";
            var httpResponseLine = Credentials.GetOdataData(pageLine);
            using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var ImLine = new ImprestMemoLines();
                    ImLine.DocNo = (string)config["No"];
                    ImLine.AdvanceType = (string)config["AdvanceType"];
                    ImLine.Item = (string)config["AccountNo"];
                    ImLine.ItemDesc = (string)config["AccountName"];
                    ImLine.ItemDesc2 = (string)config["Purpose"];
                    ImLine.LnNo = (string)config["LineNo"];
                    ImLine.UoN = (string)config["UnitofMeasure"];
                    ImLine.Quantity = (string)config["Quantity"];
                    ImLine.UnitAmount = Convert.ToDecimal((string)config["UnitCostLCY"]).ToString("#,##0.00");
                    ImLine.Amount = Convert.ToDecimal((string)config["Amount"]).ToString("#,##0.00");
                    ImLine.EmployeeNo = (string)config["EmployeeName"];
                    ImpLines.Add(ImLine);
                }
            }

            #endregion

            var Lines = new ImprestMemoItemsList
            {
                Status = Status,
                ListOfImprestMemoLines = ImpLines
            };
            return PartialView("~/Views/ImprestMemo/ImprestMemoItemsList.cshtml", Lines);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message.Replace("'", "");
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }
    public PartialViewResult ImprestMemoNonStaff(string DocNo, string Status)
    {
        try
        {
            #region Imp Lines

            var ImpLines = new List<ImprestMemoNonStaff>();
            var pageLine = "ImprestMemoNonStaff?$filter=ImprestMemoNo eq '" + DocNo + "'&$format=json";
            var httpResponseLine = Credentials.GetOdataData(pageLine);
            using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var ImLine = new ImprestMemoNonStaff();
                    ImLine.ImprestMemoNo = (string)config["ImprestMemoNo"];
                    ImLine.Names = (string)config["Names"];
                    ImLine.Designation = (string)config["Designation"];
                    ImLine.Organization = (string)config["OrganizationInstitution"];
                    ImLine.LineNo = (string)config["LineNo"];
                    ImpLines.Add(ImLine);
                }
            }

            #endregion

            var Lines = new ImprestMemoNonStaffList
            {
                Status = Status,
                ListOfNonstaff = ImpLines
            };
            return PartialView("~/Views/ImprestMemo/ImprestMemoNonStaffList.cshtml", Lines);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message.Replace("'", "");
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult UpdateImprestMemoHeader(string DocNo, ImprestMemoHeader ImprestMemoHeader)
    {
        try
        {
            string Dim1 = "", Dim2 = "", Dim3 = "", Dim4 = "", Dim5 = "", Remarks = "";
            if (ImprestMemoHeader.DocD.Dim1 != null) Dim1 = ImprestMemoHeader.DocD.Dim1;
            if (ImprestMemoHeader.DocD.Dim2 != null) Dim2 = ImprestMemoHeader.DocD.Dim2;
            if (ImprestMemoHeader.DocD.Dim3 != null) Dim3 = ImprestMemoHeader.DocD.Dim3;
            if (ImprestMemoHeader.DocD.Dim4 != null) Dim4 = ImprestMemoHeader.DocD.Dim4;
            if (ImprestMemoHeader.DocD.Dim5 != null) Dim5 = ImprestMemoHeader.DocD.Dim5;
            if (ImprestMemoHeader.Remarks != null) Remarks = ImprestMemoHeader.Remarks;
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
            }

            var StaffNo = Session["Username"].ToString();
            var UserID = Session["UserID"].ToString();

            var DateRequired = DateTime.ParseExact(ImprestMemoHeader.DateNeeded.Replace("-", "/"), "dd/MM/yyyy",
                CultureInfo.InvariantCulture);
            // Credentials.ObjNav.UpdateImprestMemoHeader(DocNo.Trim(), DateRequired, Dim1, Dim2, Dim3, Dim4, Dim5, "", Remarks);
            //Credentials.ObjNav.ModifyImprestMemo(DocNo,StaffNo, DateRequired, Dim1, Dim2, Dim3, Dim4, Dim5, "", "", UserID, ImprestMemoHeader.ImprestDueType, ImprestMemoHeader.From, ImprestMemoHeader.To, ImprestMemoHeader.Subject,
            //       Bd[1], Bd[2], Bd[3], Bd[4], Bd[5], Bd[6], Bd[7], Bd[8], Bd[9], Bd[10]);
            return Json(new { message = "ImprestMemo header Updated successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    public ActionResult NewImprestMemoLine(string docNo)
    {
        try
        {
            var ImprestMemoLine = new SafariTeam2();

            var UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;


            #region Employees
            var EmpList = new List<DropdownList>();
            var pageEmp = "EmployeeList?$format=json";

            var httpResponseEmp = Credentials.GetOdataData(pageEmp);
            using (var streamReader = new StreamReader(httpResponseEmp.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (var jToken in details["value"])
                {
                    var config1 = (JObject)jToken;
                    var dropdownList = new DropdownList
                    {
                        Text = (string)config1["First_Name"] + " " + (string)config1["Middle_Name"] + " " + (string)config1["Last_Name"] + " (" + (string)config1["No"] + ")",
                        Value = (string)config1["No"]
                    };
                    EmpList.Add(dropdownList);
                }
            }

            #endregion



            #region WorkTypes

            var ImprestWorkTypes = new List<DropdownList>();
            var page = "WorkTypes?$format=json";

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
                        Text = $"{(string)config1["Description"]} - {(string)config1["Code"]}",
                        Value = (string)config1["Code"]
                    };
                    ImprestWorkTypes.Add(dropdownList);
                }
            }
            #endregion

            #region Resources

            var ResourcesList = new List<DropdownList>();
            var page2 = "Resources?$format=json";

            var httpResponse2 = Credentials.GetOdataData(page2);
            using (var streamReader = new StreamReader(httpResponse2.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (var jToken in details["value"])
                {
                    var config1 = (JObject)jToken;
                    var dropdownList = new DropdownList
                    {
                        Text = $"{(string)config1["Name"]} - {(string)config1["No"]}",
                        Value = (string)config1["No"]
                    };
                    ResourcesList.Add(dropdownList);
                }
            }

            #endregion

            #region UnitsOfMeasure

            var UOMList = new List<DropdownList>();
            var pageUOM = "UnitsOfMeasure?$format=json";

            var httpResponseUOM = Credentials.GetOdataData(pageUOM);
            using (var streamReader = new StreamReader(httpResponseUOM.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (var jToken in details["value"])
                {
                    var config1 = (JObject)jToken;
                    var dropdownList = new DropdownList
                    {
                        Text = $"{(string)config1["Description"]} - {(string)config1["Code"]}",
                        Value = (string)config1["Code"]
                    };
                    UOMList.Add(dropdownList);
                }
            }

            #endregion

            #region Currencies 

            var CurrenciesList = new List<DropdownList>();
            var pageCurrencies = "Currencies?$format=json";

            var httpResponseCurrencies = Credentials.GetOdataData(pageCurrencies);
            using (var streamReader = new StreamReader(httpResponseCurrencies.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (var jToken in details["value"])
                {
                    var config1 = (JObject)jToken;
                    var dropdownList = new DropdownList
                    {
                        Text = $"{(string)config1["Description"]} - {(string)config1["Code"]}",
                        Value = (string)config1["Code"]
                    };
                    CurrenciesList.Add(dropdownList);
                }
            }

            #endregion


            ImprestMemoLine.ListOfEmployees = EmpList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();

            ImprestMemoLine.ListOfImprestTypes = ImprestWorkTypes.Select(x =>
                   new SelectListItem
                   {
                       Text = x.Text,
                       Value = x.Value
                   }).ToList();


            ImprestMemoLine.ListOfUOM = UOMList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();

            ImprestMemoLine.ListOfResources = ResourcesList.Select(x =>
                 new SelectListItem
                 {
                     Text = x.Text,
                     Value = x.Value
                 }).ToList();
            ImprestMemoLine.ListOfCurrencies = CurrenciesList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

            return PartialView("~/Views/ImprestMemo/NewImprestMemoLine.cshtml", ImprestMemoLine);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message.Replace("'", "");
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }
    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult SubmitImprestMemoLine(string DocNo, string EmpNo, SafariTeam2 ImpLine)
    {
        try
        {
            var UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;
            string res = "";
            res = Credentials.ObjNav.addTeamMember(
                EmpNo,
                DocNo,
                ImpLine.Work_Type,
                ImpLine.Transport_Costs,
               ImpLine.Total_Mileage_Cost,
               ImpLine.Time_Period
             );
            if (res != "")
            {
                var DocNetAmount = GetImpDocNetAmount(DocNo);
                return Json(new { NetAmout = DocNetAmount, message = "ImprestMemo Line Added successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var DocNetAmount = GetImpDocNetAmount(DocNo);
                return Json(new { NetAmout = DocNetAmount, message = "Error creating record", success = false }, JsonRequestBehavior.AllowGet);
            }

        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    public ActionResult NewCostLine(string docNo)
    {
        try
        {
            var CostLine = new OtherCosts();

            var UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;


            #region VoteItems
            var itemList = new List<DropdownList>();
            var pageItem = "ReceiptsandPaymentTypes?$format=json";

            var httpResponseItem = Credentials.GetOdataData(pageItem);
            using (var streamReader = new StreamReader(httpResponseItem.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (var jToken in details["value"])
                {
                    var config1 = (JObject)jToken;
                    var dropdownList = new DropdownList
                    {
                        Text = (string)config1["Code"] +" - "+(string)config1["Description"] ,
                        Value = (string)config1["Code"]
                    };
                    itemList.Add(dropdownList);
                }
            }

            #endregion

            CostLine.ListOfVoteItems = itemList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();



            return PartialView(CostLine);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message.Replace("'", "");
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }
    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult SubmitCostLine(string DocNo, OtherCosts CostLine)
    {
        try
        {

            var UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;
            int res = Credentials.ObjNav.InsertOtherCost(
               DocNo,
               CostLine.Type_of_Expense,
               CostLine.Unit_Cost,
                CostLine.Required_For,
                0,
                 CostLine.No_of_Days
             );
            if (res==0)
            {

                return Json(new { message = "Record Not Added. Try again", success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Record Added successfully", success = true }, JsonRequestBehavior.AllowGet);
                

            }


        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult DeleteCostLine(string DocNo, string Line_No)
    {
        try
        {

            var UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;
            string res = "";
            res = Credentials.ObjNav.DeleteOtherCost(DocNo, int.Parse(Line_No));

            return Json(new { message = "Record deleted successfully", success = true }, JsonRequestBehavior.AllowGet);


        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult DeleteImprestMemoLine(string Work_Type, string DocNo, int LineNo)
    {
        try
        {

            var UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;
            bool res = false;
            res = Credentials.ObjNav.removeImprestMemoLines(
                LineNo,
                DocNo
             );
            if (res)
            {
                var DocNetAmount = GetImpDocNetAmount(DocNo);
                return Json(new { NetAmout = DocNetAmount, message = "ImprestMemo Line Deleted successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var DocNetAmount = GetImpDocNetAmount(DocNo);
                return Json(new { NetAmout = DocNetAmount, message = "Error Deleting record Added", success = false }, JsonRequestBehavior.AllowGet);
            }

        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult SendImprestMemoAppForApproval(string DocNo)
    {
        try
        {
            var UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;
            Credentials.ObjNav.sendImprestRequisitionApproval(StaffNo, DocNo);
            Credentials.ObjNav.UpdateApprovalEntrySenderID(57008, DocNo, UserID);
            return Json(new { message = "Imprest Memo Requisition sent for approval Successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult CancelImprestMemoAppForApproval(string DocNo)
    {
        try
        {
            var UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"].ToString();
            var employeeView = Session["EmployeeData"] as EmployeeView;

            Credentials.ObjNav.cancelRecordApproval(StaffNo, DocNo, "imprest memo");
            return Json(new { message = "ImprestMemo Requisition approval cancelled Successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult SubmitImprestMemoItem(string DocNo, ImprestMemoLines ImprestMemoLine)
    {
        try
        {
            var StaffNo = Session["Username"].ToString();
            var item = ImprestMemoLine.Item.Trim();
            var itemDesc = ImprestMemoLine.ItemDesc.Trim();
            var amnt = ImprestMemoLine.Amount.Trim();
            //   string noofdays = ImprestMemoLine.Quantity.Trim();

            string UoN = "", Destination = "";

            // Credentials.ObjNav.InsertImprestMemoItems(DocNo, item, Convert.ToDecimal(amnt), StaffNo, Convert.ToDecimal(ImprestMemoLine.Quantity), UoN, itemDesc);
            var DocNetAmount = GetImpDocNetAmount(DocNo);
            return Json(
                new { NetAmout = DocNetAmount, message = "ImprestMemo Line Added successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult SubmitImprestMemoNonStaff(string DocNo, string Names, string Organization, string Designation)
    {
        try
        {
            var StaffNo = Session["Username"].ToString();


            //  Credentials.ObjNav.InsertImprestMemoNonStaff(DocNo, Names, Organization, Designation);
            var DocNetAmount = GetImpDocNetAmount(DocNo);
            return Json(
                new { NetAmout = DocNetAmount, message = "ImprestMemo Line Added successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult UpdateImprestMemoLine(string DocNo, ImprestMemoLines ImprestMemoLine)
    {
        try
        {
            var StaffNo = Session["Username"].ToString();
            var item = ImprestMemoLine.Item.Trim();
            var itemDesc = ImprestMemoLine.ItemDesc.Trim();
            var amnt = ImprestMemoLine.Amount.Trim();
            var LnNo = Convert.ToInt32(ImprestMemoLine.LnNo.Trim());
            var noofdays = ImprestMemoLine.Quantity.Trim();

            //  Credentials.ObjNav.ImprestMemoRequisitionLinesUpdate(DocNo, LnNo, Convert.ToDecimal(amnt), Convert.ToDecimal(ImprestMemoLine.Quantity), itemDesc, Convert.ToInt32(ImprestMemoLine.Quantity));
            var DocNetAmount = GetImpDocNetAmount(DocNo);
            return Json(
                new { NetAmout = DocNetAmount, message = "ImprestMemo Line updated successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public PartialViewResult EditImprestMemoLine(string LnNo, string DocNo)
    {
        try
        {
            var ln = Convert.ToInt32(LnNo);

            #region ImprestMemo Lines

            var ImLine = new ImprestMemoLines();
            var pageLine = "ImprestMemoLines?$filter=No eq '" + DocNo + "' and Line_No eq " + ln + "&$format=json";
            var httpResponseLine = Credentials.GetOdataData(pageLine);
            using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    ImLine.DocNo = (string)config["No"];
                    ImLine.AdvanceType = (string)config["Advance_Type"];
                    ImLine.Item = (string)config["Account_No"];
                    ImLine.ItemDesc = (string)config["Account_Name"];
                    ImLine.ItemDesc2 = (string)config["Purpose"];
                    ImLine.LnNo = (string)config["Line_No"];
                    ImLine.UoN = (string)config["Unit_of_Measure"];
                    ImLine.Quantity = (string)config["Quantity"];
                    ImLine.UnitAmount = Convert.ToDecimal((string)config["Daily_Rate_Amount"]).ToString("#,##0.00");
                    ImLine.Amount = Convert.ToDecimal((string)config["Amount"]).ToString("#,##0.00");
                }
            }

            #endregion

            #region ImprestMemo Type List

            var ImprestMemoTList = new List<ImprestMemoTypes>();
            var page = "ImprestTypes?$filter=Description ne ''&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    var impList = new ImprestMemoTypes();
                    impList.Code = (string)config["Code"];
                    impList.Description = (string)config["Description"];
                    ImprestMemoTList.Add(impList);
                }
            }

            #endregion

            #region UoM

            var UoMList = new List<DropdownList>();
            var pageUoM = "UnitOfMeasure?$format=json";

            var httpResponseUoM = Credentials.GetOdataData(pageUoM);
            using (var streamReader = new StreamReader(httpResponseUoM.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var UoM = new DropdownList();
                    UoM.Value = (string)config["Code"];
                    UoM.Text = (string)config["Description"];
                    UoMList.Add(UoM);
                }
            }

            #endregion

            #region Destinations

            var DestList = new List<DropdownList>();
            var pageDest = "DestinationList?$format=json";

            var httpResponseDest = Credentials.GetOdataData(pageDest);
            using (var streamReader = new StreamReader(httpResponseDest.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Dest = new DropdownList();
                    Dest.Value = (string)config["DestinationCode"];
                    Dest.Text = (string)config["DestinationName"];
                    DestList.Add(Dest);
                }
            }

            #endregion

            var itemDetails = new ImprestMemoItemDetails
            {
                ItemDetails = ImLine,
                ListOfImprestTypes = ImprestMemoTList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Description,
                        Value = x.Code
                    }).ToList(),
                ListOfUoM = UoMList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList(),
                ListOfDestination = DestList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return PartialView("~/Views/ImprestMemo/ImprestMemoEditItemForm.cshtml", itemDetails);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult RemoveImprestMemoLine(string DocNo, string LnNo)
    {
        try
        {
            //  Credentials.ObjNav.ImprestMemoRequsitionRemoveLine(Convert.ToInt32(LnNo), DocNo);
            var DocNetAmount = GetImpDocNetAmount(DocNo);
            return Json(
                new { NetAmout = DocNetAmount, message = "ImprestMemo Line removed successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult RemoveImprestMemoNonstaff(string DocNo, string LnNo)
    {
        try
        {
            //Credentials.ObjNav.DeleteImprestMemoNonStaff( DocNo, Convert.ToInt32(LnNo));
            var DocNetAmount = GetImpDocNetAmount(DocNo);
            return Json(
                new { NetAmout = DocNetAmount, message = "ImprestMemo Line removed successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    protected string GetImpDocNetAmount(string DocNo)
    {
        var amount = "";
        var page = "SafariImprest?$select=Total_Subsistence_Allowance&$filter=No eq '" + DocNo + "'&$format=json";
        var httpResponse = Credentials.GetOdataData(page);
        using var streamReader = new StreamReader(httpResponse.GetResponseStream());
        var result = streamReader.ReadToEnd();

        var details = JObject.Parse(result);
        if (details["value"].Count() > 0)
            foreach (JObject config in details["value"])
                amount = Convert.ToDecimal((string)config["TotalNetAmount"]).ToString("#,##0.00");

        return amount;
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public PartialViewResult FileUploadForm()
    {
        return PartialView("~/Views/ImprestMemo/FileAttachmentForm.cshtml");
    }

    public PartialViewResult NewImrestMemoItem()
    {
        try
        {
            var locationList = new LocationList();


            #region Items List

            var ddlList = new List<DropdownList>();
            var page = "Item_List?$&orderby=Description&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var dll = new DropdownList();
                    dll.Value = (string)config["No"];
                    dll.Text = (string)config["Description"];
                    ddlList.Add(dll);
                }
            }

            #endregion

            locationList = new LocationList
            {
                ListOfLocations = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return PartialView("~/Views/ImprestMemo/ImprestMemoItemsAdd.cshtml", locationList);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }

    public PartialViewResult NewImrestMemoNonStaff()
    {
        try
        {
            return PartialView("~/Views/ImprestMemo/ImprestMemoNonStaffAdd.cshtml");
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }

    public string GetEmployeeJobID(string StaffNo)
    {
        try
        {
            var page = $"EmployeeHRCard?$filter=No eq '{StaffNo}'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                var items = details["value"] as JArray;
                if (items != null && items.Count > 0)
                {
                    var salaryScale = items[0]["Salary_Scale"]?.ToString();
                    return salaryScale ?? string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in GetEmployeeJobID: " + ex.Message);
        }

        return string.Empty;
    }


}