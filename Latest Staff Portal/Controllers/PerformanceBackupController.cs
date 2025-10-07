using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Windows.Media.Media3D;

namespace Latest_Staff_Portal.Controllers
{
    public class PerformanceAppraisalController : Controller
    {
        // GET: PerformanceAppraisal
        public ActionResult HRAppraisalsList()
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
        public PartialViewResult HRAppraisalsListPartialView(string Appraisal_Stage, string Status)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                var supervisorID = GetSupervisorID(StaffNo); //wrong

                var AppraisalList = new List<AppraisalsList>();

                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = "";
                if (employeeView.UserID != supervisorID) //if logged in user is not a supervisor
                {
                    if (Appraisal_Stage == "Appraisee Reviewed")
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq 'End Year Evalauation' and Status eq '{Appraisal_Stage}' and  Employee_No eq '{StaffNo}'&$format=json";
                    }
                    else if (Appraisal_Stage == "Immediate Supervisor Reviewed")
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq 'End Year Evalauation' and Status eq '{Appraisal_Stage}' and  Employee_No eq '{StaffNo}'&$format=json";
                    }

                    else if (Appraisal_Stage == "Approved" || Appraisal_Stage == "Closed" || Appraisal_Stage == "HR")
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq 'End Year Evalauation' and Status eq 'Approved/Closed/HR' and  Employee_No eq '{StaffNo}'&$format=json";
                    }
                    else
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq '{Appraisal_Stage}' and  Employee_No eq '{StaffNo}'&$format=json";
                    }
                }
                else
                {
                    if (Appraisal_Stage == "Appraisee Reviewed")
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq 'End Year Evalauation' and Status eq '{Appraisal_Stage}'&$format=json";
                    }
                    else if (Appraisal_Stage == "Immediate Supervisor Reviewed")
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq 'End Year Evalauation' and Status eq '{Appraisal_Stage}'&$format=json";
                    }

                    else if (Appraisal_Stage == "Approved" || Appraisal_Stage == "Closed" || Appraisal_Stage == "HR")
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq 'End Year Evalauation' and Status eq 'Approved/Closed/HR' &$format=json";
                    }
                    else if (Appraisal_Stage == "Target Approval")
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq '{Appraisal_Stage}' &$format=json";
                    }
                    else
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq '{Appraisal_Stage}' and Employee_No eq '{StaffNo}'&$format=json";
                    }
                    /*else
                    {
                        page = $"Hr360AppraisalCard?$filter=Appraisal_Stage eq '{Appraisal_Stage}' and  Employee_No eq '{StaffNo}'&$format=json";
                    }*/
                }


                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var appraisal = new AppraisalsList
                        {
                            Appraisal_No = (string)config["Appraisal_No"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            User_ID = (string)config["User_ID"],
                            Job_Title = (string)config["Job_Title"],
                            Job_Description = (string)config["Job_Description"],
                            Appraisal_Type = (string)config["Appraisal_Type"],
                            Appraisal_Period = (string)config["Appraisal_Period"],
                            Status = (string)config["Status"],
                            Appraisal_Stage = (string)config["Appraisal_Stage"],
                            Sent = (string)config["Sent"]
                        };

                        AppraisalList.Add(appraisal);
                    }
                }
                ViewBag.title = Appraisal_Stage;
                return PartialView("~/Views/PerformanceAppraisal/PartialViews/HRAppraisalsListPartialView.cshtml", AppraisalList.OrderByDescending(x => x.Appraisal_No));
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
        public ActionResult NewHRAppraisal()
        {
            try
            {
                var ObjectivesLine = new AppraisalsCard();

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;


                #region Appraisal_Period

                var PeriodsList = new List<DropdownList>();
                var page = "HrLookUp?$filter=Type eq 'Appraisal Period'&$format=json";

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
                            Text = (string)config1["Code"],
                            Value = (string)config1["Code"]
                        };
                        PeriodsList.Add(dropdownList);
                    }
                }
                #endregion

                ObjectivesLine.Employee_No = StaffNo;
                ObjectivesLine.Employee_Name = employeeView.FirstName + " " + employeeView.MiddleName + " " + employeeView.LastName;
                ObjectivesLine.ListOfPeriods = PeriodsList.Select(x =>
                       new SelectListItem
                       {
                           Text = x.Text,
                           Value = x.Value
                       }).ToList();
                return PartialView("~/Views/PerformanceAppraisal/PartialViews/NewHRAppraisal.cshtml", ObjectivesLine);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitHRAppraisal(string Appraisal_Period)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                var supervisorID = CommonClass.GetEmployeeSupervisorID(UserID);
                string res = "";
                res = Credentials.ObjNav.HRAppraisalHeaderInsert(
                    Appraisal_Period,
                    StaffNo

                    );

                return Json(new { message = res, success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitHRAppraisal2(string Appraisal_No, string Perfomance_Goals_and_Targets, string Weight, string Key_Perfomance_Indicator)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                var supervisorID = CommonClass.GetEmployeeSupervisorID(UserID);



                string res = "";
                /* res = Credentials.ObjNav.HRAppraisalHeaderInsert(
                     "",
                      "",
                       0,
                     StaffNo,
                     supervisorID,
                      "",
                      "",
                       "",
                        "",
                      "",
                       ""
                     );*/

                return Json(new { message = res, success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult HRAppraisalDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var employeeView = Session["EmployeeData"] as EmployeeView;
                string StaffNo = Session["Username"].ToString();
                AppraisalsCard appraisal = new AppraisalsCard();

                var EmployeeJobID = GetEmployeeJobID(StaffNo);

                string page = "Hr360AppraisalCard?$filter=Appraisal_No eq '" + DocNo + "'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        appraisal = new AppraisalsCard
                        {
                            Appraisal_No = (string)config["Appraisal_No"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            User_ID = (string)config["User_ID"],
                            Job_Title = (string)config["Job_Title"],
                            Job_Description = (string)config["Job_Description"],
                            Supervisor = (string)config["Supervisor"],
                            Supervisor_Name = (string)config["Supervisor_Name"],
                            Appraisal_Period = (string)config["Appraisal_Period"],
                            Appraisal_Date = (string)config["Appraisal_Date"],
                            Evaluation_Period_Start = (string)config["Evaluation_Period_Start"],
                            Evaluation_Period_End = (string)config["Evaluation_Period_End"],
                            Appraisal_Approval_Status = (string)config["Appraisal_Approval_Status"],
                            Status = (string)config["Status"],
                            Appraisal_Stage = (string)config["Appraisal_Stage"],
                            Comments_Supervisor = (string)config["Comments_Supervisor"],
                            Appraisal_Score = (int)config["Appraisal_Score"],
                            Staff_Attributes_Evaluation_Score = (int)config["Staff_Attributes_Evaluation_Score"],
                            Return_Comments = (string)config["Return_Comments"]

                        };
                        break; // Since you're only expecting one record based on "No"
                    }
                }
                ViewBag.LoggedInUserID = employeeView.UserID;
                ViewBag.Salary_Scale = EmployeeJobID;
                return View(appraisal);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult DutiesLines(string Job_ID, string Status, string Appraisal_Stage)
        {
            try
            {
                var responsibilitiesLines = new List<PositionResponsibilities>();
                var pageLine = "PositionResponsibilities?$filter=Position_ID eq '" + Job_ID + "'&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);

                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var respLine = new PositionResponsibilities
                        {
                            Position_ID = (string)config["Position_ID"],
                            Line_No = (int)config["Line_No"],
                            Type = (string)config["Type"],
                            Category = (string)config["Category"],
                            Description = (string)config["Description"],

                        };

                        responsibilitiesLines.Add(respLine);
                    }
                }
                ViewBag.Status = Status;
                return PartialView("~/Views/PerformanceAppraisal/PartialViews/DutiesLines.cshtml", responsibilitiesLines);
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
        public PartialViewResult ObjectivesLines(string DocNo, string Status, string Appraisal_Stage, string SupervisorID)
        {
            try
            {
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var goalsLines = new List<PersonalGoalsObjectives>();
                var pageLine = "PersonalGoalsObjectives?$filter=Appraisal_No eq '" + DocNo + "'&$format=json";
                var httpResponseLine = Credentials.GetOdataData(pageLine);

                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var goalsLine = new PersonalGoalsObjectives
                        {
                            Appraisal_No = (string)config["Appraisal_No"],
                            Appraisal_Period = (string)config["Appraisal_Period"],
                            Employee_No = (string)config["Employee_No"],
                            Categorize_As = (string)config["Categorize_As"],
                            Line_No = (int)config["Line_No"],
                            Sub_Category = (string)config["Sub_Category"],
                            Perfomance_Goals_and_Targets = (string)config["Perfomance_Goals_and_Targets"],
                            Min_Target_Score = (int)config["Min_Target_Score"],
                            Max_Target_Score = (int)config["Max_Target_Score"],
                            Target = (int)config["Target"],
                            Weight = (int)config["Weight"],
                            Key_Perfomance_Indicator = (string)config["Key_Perfomance_Indicator"],
                            Self_Rating = (int)config["Self_Rating"],
                            Employee_Comments = (string)config["Employee_Comments"],
                            Supervisor_Rating = (int)config["Supervisor_Rating"],
                            Supervisor_Comments = (string)config["Supervisor_Comments"],

                        };

                        goalsLines.Add(goalsLine);
                    }
                }
                ViewBag.Status = Status;
                ViewBag.Appraisal_Stage = Appraisal_Stage ?? "";
                ViewBag.LoggedInUserID = employeeView.UserID;
                ViewBag.SupervisorID = SupervisorID;
                return PartialView("~/Views/PerformanceAppraisal/PartialViews/ObjectivesLines.cshtml", goalsLines);
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
        public PartialViewResult KPIs(string Appraisal_No, string Line_No, string Perfomance_Goals_and_Targets, string Appraisal_Stage, string LoggedInUserID, string SupervisorID)
        {
            try
            {
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var kpiLines = new List<KpiAppraisal>();
                var pageKPIs = $"KpiAppraisal?$filter=Appraisal_No eq '{Appraisal_No}' and Entry_No eq {int.Parse(Line_No)}&$format=json";

                //var pageKPIs = $"KpiAppraisal?$filter=Appraisal_No eq '{Appraisal_No}' and Entry_No eq '{Line_No}'&$format=json";
                var httpResponseKPIs = Credentials.GetOdataData(pageKPIs);

                using (var streamReader = new StreamReader(httpResponseKPIs.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var Kpi = new KpiAppraisal
                        {
                            Appraisal_No = (string)config["Appraisal_No"],
                            Entry_No = (int)config["Entry_No"],
                            Perfomance_Goals_and_Targets = (string)config["Perfomance_Goals_and_Targets"],
                            KPI = (string)config["KPI"],
                            Weight = (int)config["Weight"],

                        };

                        kpiLines.Add(Kpi);
                    }
                }
                ViewBag.Appraisal_No = Appraisal_No;
                ViewBag.Line_No = Line_No;
                ViewBag.Perfomance_Goals_and_Targets = Perfomance_Goals_and_Targets;
                ViewBag.Appraisal_Stage = Appraisal_Stage;
                ViewBag.LoggedInUserID = LoggedInUserID;
                ViewBag.SupervisorID = SupervisorID;
                return PartialView("~/Views/PerformanceAppraisal/PartialViews/KPIs.cshtml", kpiLines);
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
        public ActionResult NewKPI(string Appraisal_No, string Line_No, string Perfomance_Goals_and_Targets)
        {
            try
            {
                var KPILine = new KpiAppraisal();

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                KPILine.Appraisal_No = Appraisal_No;
                KPILine.Perfomance_Goals_and_Targets = Perfomance_Goals_and_Targets;



                #region Appraisal_Period

                var PeriodsList = new List<DropdownList>();
                var page = "HrLookUp?$filter=Type eq 'Appraisal Period'&$format=json";

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
                            Text = (string)config1["Code"],
                            Value = (string)config1["Code"]
                        };
                        PeriodsList.Add(dropdownList);
                    }
                }
                #endregion


                //ObjectivesLine.ListOfPeriods = PeriodsList.Select(x =>
                //       new SelectListItem
                //       {
                //           Text = x.Text,
                //           Value = x.Value
                //       }).ToList();
                ViewBag.Line_No = Line_No;
                return PartialView("~/Views/PerformanceAppraisal/PartialViews/NewKPI.cshtml", KPILine);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitKPI(string Appraisal_No, string Line_No, string Perfomance_Goals_and_Targets, string Weight, string KPI)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                int res = Credentials.ObjNav.InsertKpiRecord(int.Parse(Line_No), Appraisal_No, KPI, decimal.Parse(Weight));
                return Json(new { message = "Record Added successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteKPILine(string Appraisal_No, string Entry_No)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                string res = Credentials.ObjNav.DeleteKpiRecord(int.Parse(Entry_No));
                return Json(new { message = "Record Added successfully", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult EvaluationLines(string DocNo, string Status, string Appraisal_Stage, string SupervisorID, string Job_Group)
        {
            try
            {
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var evalLines = new List<StaffAttributesEvaluation>();
                var pageLine = "";

                if (Job_Group == "High")
                {
                    pageLine = "StaffAttributesEvaluation?$filter=Appraisal_No eq '" + DocNo + "'&$format=json";
                }
                else
                {
                    pageLine = "StaffAttributesEvaluation?$filter=Appraisal_No eq '" + DocNo + "' and Grade eq 'All' &$format=json";
                }

                //var pageLine = "HRAppraisalEvaluationAreas?$filter=Appraisal_No eq '" + DocNo + "'&$format=json";

                var httpResponseLine = Credentials.GetOdataData(pageLine);

                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var evalLine = new StaffAttributesEvaluation
                        {
                            Appraisal_No = (string)config["Appraisal_No"],
                            Appraisal_Period = (string)config["Appraisal_Period"],
                            Employee_No = (string)config["Employee_No"],
                            Categorize_As = (string)config["Categorize_As"],
                            Line_No = (int)config["Line_No"],
                            Sub_Category = (string)config["Sub_Category"],
                            Perfomance_Goals_and_Targets = (string)config["Perfomance_Goals_and_Targets"],
                            Min_Target_Score = (int)config["Min_Target_Score"],
                            Max_Target_Score = (int)config["Max_Target_Score"],
                            Self_Rating = (int)config["Self_Rating"],
                            Employee_Comments = (string)config["Employee_Comments"],
                            Supervisor_Rating = (string)config["Supervisor_Rating"],
                            Supervisor_Comments = (string)config["Supervisor_Comments"]


                        };

                        evalLines.Add(evalLine);
                    }
                }
                ViewBag.Status = Status;
                ViewBag.Appraisal_Stage = Appraisal_Stage ?? "";
                ViewBag.LoggedInUserID = employeeView.UserID;
                ViewBag.SupervisorID = SupervisorID;
                return PartialView("~/Views/PerformanceAppraisal/PartialViews/EvaluationLines.cshtml", evalLines);
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



        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult LoadDeptPerformanceTargets(string Appraisal_No)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.AddDeptPerformanceTargets(Appraisal_No);

                return Json(new { message = "Department targets  successfully loaded", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AppraisalSection1(string Appraisal_No)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                //Credentials.ObjNav.LoadAppraisalSectionsLowerScale(Appraisal_No);
                Credentials.ObjNav.LoadAppraisalSectionsHighScale(Appraisal_No);

                return Json(new { message = "Appraisal Sections successfully loaded", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AppraisalSection2(string Appraisal_No)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.LoadAppraisalSectionsLowerScale(Appraisal_No);
                //Credentials.ObjNav.LoadAppraisalSectionsHighScale(Appraisal_No);
                return Json(new { message = "Appraisal Sections successfully loaded", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult NewObjective(string docNo)
        {
            try
            {
                var ObjectivesLine = new PersonalGoalsObjectives();

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


                /* ImprestMemoLine.ListOfImprestTypes = ImprestWorkTypes.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Text,
                            Value = x.Value
                        }).ToList();*/
                return PartialView("~/Views/PerformanceAppraisal/PartialViews/NewObjective.cshtml", ObjectivesLine);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitObjective(string Appraisal_No, string Perfomance_Goals_and_Targets, string Weight, string Key_Perfomance_Indicator)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                string res = "";

                res = Credentials.ObjNav.AddHrAppraisalLines(Appraisal_No, Perfomance_Goals_and_Targets, Key_Perfomance_Indicator);
                return Json(new { message = "Record Added successfully", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateObjectiveLine(PersonalGoalsObjectives data)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                if (data.Supervisor_Rating == null)
                {
                    data.Supervisor_Rating = 0;
                }

                if (data.Supervisor_Comments == null)
                {
                    data.Supervisor_Comments = "";
                }

                if (data.Self_Rating == null)
                {
                    data.Self_Rating = 0;
                }

                if (data.Employee_Comments == null)
                {
                    data.Employee_Comments = "";
                }

                string res = "";

                res = Credentials.ObjNav.ModifyHrAppraisalLines(data.Appraisal_No, data.Line_No, data.Perfomance_Goals_and_Targets, 0, data.Weight, data.Key_Perfomance_Indicator, data.Self_Rating, data.Employee_Comments, data.Supervisor_Rating, data.Supervisor_Comments);

                return Json(new { message = "Record updated successfully", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteObjectiveLine(PersonalGoalsObjectives data)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                string res = "";

                res = Credentials.ObjNav.DeleteHrAppraisalLines(data.Appraisal_No, data.Line_No);

                return Json(new { message = "Record Deleted successfully", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SendToSupervisor(string Appraisal_No)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.Sendtosupervisor(Appraisal_No);

                return Json(new { message = "Document successfully sent to supervisor", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ApproveTargets(string Appraisal_No)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.ApproveTarget(Appraisal_No);

                return Json(new { message = "Targets successfully approved", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ReturnToAppraisee(string Appraisal_No, string Return_Comments)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.ReturntoAppraisee(Appraisal_No, Return_Comments);

                return Json(new { message = "Targets successfully returned to appraisee", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SendForReview(string Appraisal_No)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.SendforReview(Appraisal_No);

                return Json(new { message = "Document successfully sent to supervisor", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult NewEvaluationLine(string docNo)
        {
            try
            {
                var EvaluationLine = new StaffAttributesEvaluation();

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


                /* ImprestMemoLine.ListOfImprestTypes = ImprestWorkTypes.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Text,
                            Value = x.Value
                        }).ToList();*/
                return PartialView("~/Views/PerformanceAppraisal/PartialViews/NewEvaluationLine.cshtml", EvaluationLine);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitNewEvaluationLine(string Appraisal_No, string Perfomance_Goals_and_Targets, string Weight, string Key_Perfomance_Indicator)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                string res = "";
                //Credentials.ObjNav.AddDeptPerformanceTargets(Appraisal_No);
                //Credentials.ObjNav.ModifyHrAppraisalLines(Appraisal_No, 1, Perfomance_Goals_and_Targets, 0, int.Parse(Weight), Key_Perfomance_Indicator, 0, "", 0, "");

                return Json(new { message = "Record Added successfully", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateEvaluationLine(PersonalGoalsObjectives data)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                string res = "";

                res = Credentials.ObjNav.SupervisorsRating(data.Appraisal_No, data.Line_No, data.Supervisor_Rating, data.Supervisor_Comments);

                return Json(new { message = "Record suceessfully updated", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CloseAppraisal(string Appraisal_No)
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.CloseAppraisal(Appraisal_No);

                return Json(new { message = "Appraisal successfully closed", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
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

        public string GetSupervisorID(string StaffNo)
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
                        var supervisor = items[0]["Supervisor"]?.ToString();
                        return supervisor ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetSupervisorID: " + ex.Message);
            }

            return string.Empty;
        }
    }
}