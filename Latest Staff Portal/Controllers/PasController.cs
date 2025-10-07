using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Mvc;
using System.Windows.Documents;
using static System.Net.WebRequestMethods;

namespace Latest_Staff_Portal.Controllers
{

    public class PasController : Controller
    {
        private string pasDocStage;

        private bool capability;
        string StaffNo;




        public ActionResult IndividualScorecardList()
        {
            var StaffNo = Session["Username"]?.ToString();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                string currentYearCode = "";
                string startDate = "";
                string endDate = "";
                string pageResC = $"ImplementationYears?$filter=Current eq true&$format=json";
                HttpWebResponse httpResponseResC = Credentials.GetOdataData(pageResC);

                using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    if (details["value"] != null && details["value"].Any())
                    {
                        JObject firstCurrentYear = details["value"].First as JObject;
                        if (firstCurrentYear != null)
                        {
                            currentYearCode = firstCurrentYear["Annual_Year_Code"]?.ToString();
                            startDate = firstCurrentYear["Start_Date"]?.ToString();
                            endDate = firstCurrentYear["End_Date"]?.ToString();
                        }
                    }
                }

                string UserID = Session["UserID"].ToString();
                List<IndividualScoreCard> ScoreCardList = new List<IndividualScoreCard>();
                //string page = $"IndividualScoreCard?$filter=CreatedBy eq '{UserID}' and (Document_Stage eq 'Returned To Employee' or Document_Stage eq 'Mid-Year Review' or Document_Stage eq '' or Document_Stage eq 'Sent To Supervisor' or Document_Stage eq 'Awaiting Evaluation') and DocumentType eq 'Individual Scorecard' and ScoreCardType eq 'Staff' &$format=json";
                /* string page = $"IndividualScoreCard?" +
               $"$filter=Created_By eq '{UserID}' and " +
               $"(Document_Stage eq 'Returned To Employee' or " +
               $"Document_Stage eq 'Mid-Year Review' or " +
               $"Document_Stage eq '' or " +
               $"Document_Stage eq 'Sent To Supervisor')&$format=json";*/

                //string page = $"AppraisalsList?$filter=User_ID eq '{UserID}' and Appraisal_Stage eq 'Target Setting'&$format=json";

                string page = $"AppraisalsList?$filter=Appraisal_Stage eq 'Target Setting'&$format=json";



                HttpWebResponse httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        IndividualScoreCard TrList = new IndividualScoreCard
                        {
                            DocNo = (string)config["No"],
                            Employee_Name = (string)config["EmployeeName"],
                            Functional_Template_ID = (string)config["FunctionalTemplateID"],
                            Strategy_Plan_ID = (string)config["StrategyPlanID"],
                            Contract_Year = (string)config["ContractYear"],
                            Start_Date = (string)config["StartDate"],
                            End_Date = (string)config["EndDate"],
                            Responsibility_Center_Name = (string)config["ResponsibilityCenterName"],
                            Admin_Unit_Name = (string)config["AdminUnitName"],
                            Status = (string)config["Status"],
                            Document_Stage = (string)config["DocumentStage"] == " " ? "No Action Taken" : (string)config["DocumentStage"]
                        };

                        ScoreCardList.Add(TrList);
                    }
                }
                ViewBag.HasScoreCardData = ScoreCardList.Any();
                ViewBag.CurrentYearCode = currentYearCode;
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.PASDocStage = CheckPASDocStage();

                return View();
            }
        }


        public ActionResult IndividualScorecardListPartialView()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            string UserID = Session["UserID"].ToString();
            var StaffNo = Session["Username"]?.ToString();

            capability = HandleCapability(1);
            List<AppraisalsList> ScoreCardList = new List<AppraisalsList>();

            /*            string page = $"IndividualScoreCard?$filter=Responsible_Employee_No eq '{StaffNo}' and " +
                          $"(Document_Stage eq 'Returned To Employee' or " +
                          $"Document_Stage eq 'Mid-Year Review' or " +
                          $"Document_Stage eq '' or " +
                          $"Document_Stage eq 'Sent To Supervisor' or " +
                          $"Document_Stage eq 'Evaluation')&$format=json";*/


            string page = $"AppraisalsList?$filter=Appraisal_Stage eq 'Target Setting'&$format=json";




            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    AppraisalsList TrList = new AppraisalsList();
                    TrList.Appraisal_No = (string)config["Appraisal_No"];
                    TrList.Employee_Name = (string)config["Employee_Name"];
                    

                    ScoreCardList.Add(TrList);
                }
            }
            ViewBag.Capable = HandleCapability(1);
            ViewBag.PASDocStage = CheckPASDocStage();
            ViewBag.HasMultipleScorecards = ScoreCardList.Count > 1;
            return PartialView("~/Views/Pas/Partial Views/IndividualScorecardListView.cshtml", ScoreCardList);
        }


        public JsonResult UpdatePMMU(string docNo, string pmmuValue)
        {
            try
            {
                string UserID = Session["UserID"].ToString();
                // UpdatePMMUHeader(docNo, "", "", pmmuValue, UserID, "", "");
                //Credentials.ObjNav.UpdatePMMUHeader1(docNo, pmmuValue);
                string successMessage = "Update made successfully successfully";
                return Json(new { successMessage, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ViewIndividualScorecard(string DocNo)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();

            try
            {
                IndividualScoreCard individualScoreCardItem = new IndividualScoreCard();


                string page = "IndividualScoreCard?$filter=No eq '" + DocNo + "'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {

                        individualScoreCardItem.DocNo = (string)config["No"];
                        individualScoreCardItem.Approval_Status = (string)config["ApprovalStatus"];
                        individualScoreCardItem.Department = (string)config["Department"];
                        individualScoreCardItem.Contract_Year = (string)config["ContractYear"];
                        individualScoreCardItem.Designation = (string)config["Designation"];

                        individualScoreCardItem.Employee_Name = string.IsNullOrEmpty((string)config["EmployeeName"])
                            ? staffName
                            : (string)config["EmployeeName"];

                        individualScoreCardItem.Responsibility_Center =
                            string.IsNullOrEmpty((string)config["ResponsibilityCenterName"])
                                ? employeeView.Responsibility_Center
                                : (string)config["ResponsibilityCenterName"];

                        individualScoreCardItem.Responsibility_Center_Name =
                            (string)config["ResponsibilityCenterName"];

                        individualScoreCardItem.End_Date = (string)config["EndDate"];
                        individualScoreCardItem.Start_Date = (string)config["StartDate"];
                        individualScoreCardItem.Status = (string)config["Status"];
                        individualScoreCardItem.Strategy_Plan_ID = (string)config["StrategyPlanID"];
                        individualScoreCardItem.Total_Assigned_Weight_Percent =
                            (decimal)config["TotalAssignedWeight"];
                        individualScoreCardItem.Responsible_Employee_No =
                            string.IsNullOrEmpty((string)config["EmployeeNo"])
                                ? StaffNo
                                : (string)config["EmployeeNo"];
                        //individualScoreCardItem.Blocked_x003F_ = (bool)config["Blocked_x003F_"];
                        individualScoreCardItem.Created_By = (string)config["CreatedBy"];
                        individualScoreCardItem.Created_On = (string)config["CreatedOn"];
                        individualScoreCardItem.Change_Status = (string)config["ChangeStatus"];
                        individualScoreCardItem.JD_Assigned_Weight_Percent = (int)config["JDAssignedWeight"];
                        individualScoreCardItem.Functional_Template_ID = (string)config["FunctionalTemplateID"];
                        individualScoreCardItem.Document_Stage = (string)config["DocumentStage"];
                        individualScoreCardItem.Supervisors_Employee_Name = (string)config["SupervisorsEmployeeName"];
                        individualScoreCardItem.Admin_Unit_Name = (string)config["AdminUnitName"];
                        individualScoreCardItem.Populate_Activities_From = (string)config["PopulateActivitiesFrom"];
                        //individualScoreCardItem.comment = (string)config["comment"];
                        bool isSupervisor = HandleCapability(0);
                        bool isEmployeeDirector = !string.IsNullOrEmpty((string)config["EmployeeNo"]) && CommonClass.isDirector((string)config["EmployeeNo"]);

                        if (isEmployeeDirector && isSupervisor)
                        {
                            individualScoreCardItem.isDirector = true;
                        }
                        else
                        {
                            individualScoreCardItem.isDirector = CommonClass.isDirector(StaffNo);
                        }

                    }
                }

                // Add PMMU dropdown list data
                List<DropdownList> PMMUList = new List<DropdownList>();
                string pageResC = "PerfomanceContracts?$filter=Document_Type eq 'Individual Scorecard' and Score_Card_Type eq 'Directors' &$format=json";

                HttpWebResponse httpResponseResC = Credentials.GetOdataData(pageResC);
                using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        DropdownList CList = new DropdownList();
                        CList.Value = (string)config["No"];
                        CList.Text = (string)config["No"] + " " + (string)config["Responsibility_Center_Name"] + " " + (string)config["Description"];
                        PMMUList.Add(CList);
                    }
                }

                // Add the PMMU dropdown list to the model
                individualScoreCardItem.ListOfPMMU = PMMUList.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value,
                        Selected = x.Value == individualScoreCardItem.Functional_Template_ID
                    }).ToList();

                capability = HandleCapability(1);
                ViewBag.Capable = capability;
                ViewBag.TrainingCount = CheckTrainingCount(DocNo);
                ViewBag.DocumentStage = CheckPASDocStage();
                ViewBag.PASDocStage = CheckPASDocStage();

                return View(individualScoreCardItem);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public PartialViewResult NewIndividualScorecard()
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string DepartmentName = employeeView.Department_Name;
            string DirectorateCode = employeeView.Directorate_Code;
            string StaffNo = Session["Username"].ToString();
            IndividualScoreCard NewAppl = new IndividualScoreCard();
            string Dep = "", Division = "";

            #region Employee Data

            string pageData = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(pageData);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                if (details["value"].Count() > 0)
                {
                    foreach (JObject config in details["value"])
                    {
                        Dep = (string)config["Global_Dimension_1_Code"];
                        Division = (string)config["Department_Code"];
                    }
                }
            }

            #endregion

            {
                #region Dim1 List

                List<DimensionValues> Dim1List = new List<DimensionValues>();
                string pageDepartment =
                    "ResponsibilityCenters?$format=json&$filter=Operating_Unit_Type eq 'Directorate' or Operating_Unit_Type eq 'Department/Center' or Operating_Unit_Type eq 'Court Stations' or Operating_Unit_Type eq 'Office'";



                HttpWebResponse httpResponseDepartment = Credentials.GetOdataData(pageDepartment);
                using (var streamReader = new StreamReader(httpResponseDepartment.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        DimensionValues Department = new DimensionValues();
                        Department.Code = (string)config["Code"];
                        Department.Name = (string)config["Name"];

                        Dim1List.Add(Department);
                    }
                }

                #endregion

                #region dim2

                List<DimensionValues> Dim2List = new List<DimensionValues>();
                string pageDivision = "ResponsibilityCentre?$filter=Operating_Unit_Type eq 'Department'&format=json";

                HttpWebResponse httpResponseDivision = Credentials.GetOdataData(pageDivision);
                using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        DimensionValues DList = new DimensionValues();
                        DList.Code = (string)config["Code"];
                        DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                        Dim2List.Add(DList);
                    }
                }

                #endregion

                #region PMMUs

                List<DropdownList> PMMUList = new List<DropdownList>();

                /*confirm if Score_Card_Type is exposed in the ODATA*/
                string pageResC =
                    "PerfomanceContracts?$filter=Document_Type eq  'Individual Scorecard' and Score_Card_Type eq 'Directors' &$format=json";

                HttpWebResponse httpResponseResC = Credentials.GetOdataData(pageResC);
                using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        DropdownList CList = new DropdownList();
                        CList.Value = (string)config["No"];
                        CList.Text = (string)config["No"] + " " + (string)config["Responsibility_Center_Name"] + " " +
                                     (string)config["Description"];
                        PMMUList.Add(CList);
                    }
                }

                #endregion

                #region Trainers

                List<DropdownList> TrainerList = new List<DropdownList>();
                string pageTrainer = "TrainingProviders?$format=json";

                HttpWebResponse httpResponseTrainer = Credentials.GetOdataData(pageTrainer);
                using (var streamReader = new StreamReader(httpResponseTrainer.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        DropdownList CList = new DropdownList();
                        CList.Value = (string)config["No"];
                        CList.Text = (string)config["Name"];
                        TrainerList.Add(CList);
                    }
                }

                #endregion

                #region TrainingPlan

                List<DropdownList> TrainingPlanList = new List<DropdownList>();
                string pageTrainingPlan = "TrainingPlanList?$format=json";

                HttpWebResponse httpResponseTrainingPlan = Credentials.GetOdataData(pageTrainingPlan);
                using (var streamReader = new StreamReader(httpResponseTrainingPlan.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        DropdownList CList = new DropdownList();
                        CList.Value = (string)config["No"];
                        CList.Text = (string)config["Description"] + " (" + (string)config["Start_Date"] + " - " +
                                     (string)config["End_Date"] + " )";
                        TrainingPlanList.Add(CList);
                    }
                }

                #endregion

                #region TrainingVeneu

                List<DropdownList> TrainingVeneuList = new List<DropdownList>();
                string pageTrainingVeneu = "Locations?$format=json";

                HttpWebResponse httpResponseTrainingVeneu = Credentials.GetOdataData(pageTrainingVeneu);
                using (var streamReader = new StreamReader(httpResponseTrainingVeneu.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        DropdownList CList = new DropdownList();
                        CList.Value = (string)config["Code"];
                        CList.Text = (string)config["Name"];
                        TrainingVeneuList.Add(CList);
                    }
                }

                #endregion

                #region CostCentre

                List<DropdownList> CostCentreList = new List<DropdownList>();
                string pageCostCentre = "DimensionValues?$filter=Global_Dimension_No eq 1&format=json";

                HttpWebResponse httpResponseCostCentre = Credentials.GetOdataData(pageCostCentre);
                using (var streamReader = new StreamReader(httpResponseCostCentre.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        DropdownList CList = new DropdownList();
                        CList.Value = (string)config["Code"];
                        CList.Text = (string)config["Name"];
                        CostCentreList.Add(CList);
                    }
                }

                #endregion

                NewAppl = new IndividualScoreCard
                {
                    Department = Dep,
                    Directorate = Division,
                    ListOfDepartment = Dim1List.Select(x =>
                        new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Code
                        }).ToList(),
                    ListOfDirectorate = Dim2List.Select(x =>
                        new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Code
                        }).ToList(),
                    ListOfPMMU = PMMUList.Select(x =>
                        new SelectListItem()
                        {
                            Text = x.Text,
                            Value = x.Value
                        }).ToList(),
                    ListOfCostCentre = CostCentreList.Select(x =>
                        new SelectListItem()
                        {
                            Text = x.Text,
                            Value = x.Value
                        }).ToList()
                };
                return PartialView("~/Views/Pas/Partial Views/NewIndividualScoreCard.cshtml", NewAppl);
            }
        }

        public ActionResult GetDate(string strategyPlanId)
        {
            string pageResC = $"PMMUList?$filter=No eq '{strategyPlanId}'&$format=json";
            DropdownList CList = new DropdownList();

            HttpWebResponse httpResponseResC = Credentials.GetOdataData(pageResC);
            using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    CList.Value = (string)config["Start_Date"];
                    CList.Text = (string)config["End_Date"];
                }
            }

            return Json(CList);
        }

        [HttpPost]
        public ActionResult ActivityResponse(int activityId, string source)
        {
            var pageResC = $"PositionTargetList?$filter=Line_No eq {activityId}&$format=json";
            var CList = new DropdownList();

            try
            {
                using (var response = Credentials.GetOdataData(pageResC))
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    var firstDetail = details["value"].FirstOrDefault();
                    if (firstDetail != null)
                    {
                        CList.Value = (string)firstDetail["KPI"] ?? "Default Value";
                        CList.Text = (string)firstDetail["Assigned_Weight_Percent"] ?? "Default Weight";
                    }
                }

                return Json(CList);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [HttpPost]
        public ActionResult SubmitIndividualScorecard(string Responsibility_Center, string Functional_Template_ID,
            int Populate_Activities_From)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string UserNo = employeeView.No;
            string respCentre = employeeView.Responsibility_Center;
            string implementingUnit = employeeView.GlobalDimension2Code;
            string userId = employeeView.UserID;
            string FnTempId = "";
            if (Functional_Template_ID != null)
            {
                FnTempId = Functional_Template_ID;
            }

            try
            {
                string DocNo = Credentials.ObjNav.InsertPMMUHeader("", "", FnTempId, UserNo,
                    "", "", userId, 1);
                if (DocNo != "")
                {

                    string successMessage = "Individual Score card, Document No: " + DocNo + ", created Successfully.";
                    return Json(new { message = DocNo, successMessage, success = true },
                        JsonRequestBehavior.AllowGet);
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

        public PartialViewResult NewPASCoreValuesLine(string strategic_ID, int hideTriger)
        {
            string pasDocStage = CheckPASDocStage();
            try
            {
                PASCoreValuesLines NewAppl = new PASCoreValuesLines();
                List<DropdownList> CoreValuesDropDown = new List<DropdownList>();
                string page = $"StrategyCoreValue?$filter=Strategic_Plan_ID eq '{strategic_ID}'";
                /*string page = $"StrategyCoreValue";*/
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Core_Value"];
                        dropdownList.Value = (string)config["Core_Value"];
                        CoreValuesDropDown.Add(dropdownList);
                    }
                }

                NewAppl.ListOfCoreValues = CoreValuesDropDown.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                ViewBag.StrategicID = strategic_ID;
                ViewBag.Trigger = hideTriger;
                ViewBag.pasDocStage = pasDocStage;
                return PartialView("~/Views/Pas/Partial Views/NewPASCoreValuesLine.cshtml", NewAppl);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public JsonResult SubmitPerformanceTargetLines(PerformanceTargetLines NewApp)
        {

            try
            {
                var (totalWeight, isWeightValid) = CheckPASWeight(NewApp.DocNo);

                if (isWeightValid)
                {
                    return Json(
                        new
                        {
                            success = false,
                            message =
                                $"Please ensure that the total weight is strictly 100. Current total is {totalWeight}. Please add or remove activities."
                        },
                        JsonRequestBehavior.AllowGet);
                }


                if (NewApp.Source == "Job Description")
                {
                    try
                    {
                        var page = $"PositionTargetList?$filter=Strategic_Objective eq '{NewApp.Activity}'&$format=json";
                        HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            var details = JObject.Parse(result);
                            foreach (JObject config in details["value"])
                            {
                                NewApp.Activity = (string)config["Strategic_Objective"];
                            }
                        }
                    }
                    catch (Exception e)
                    {

                        return Json(new { message = e.Message, success = false }, JsonRequestBehavior.AllowGet);

                    }
                }

                if (NewApp.Joint_Agreed_Target == 0)
                {
                    NewApp.Weighted_Score = 0;
                }
                else
                {
                    NewApp.Weighted_Score =
                        (decimal)Math.Round(
                            (Convert.ToDouble(NewApp.Joint_Agreed_Target) / Convert.ToDouble(NewApp.Target)) *
                            (double)NewApp.Weight, 2);
                }

                string pmmuNumber = "";
                if (NewApp.PMMU_No != null)
                {
                    pmmuNumber = NewApp.PMMU_No;
                }

                int PopulateActivitiesFromVal = NewApp.Source == "PMMU" ? 1 :
                    (NewApp.Source == "Job Description" ? 2 : 0);

                Credentials.ObjNav.InsertPerfomanceTargetsLines(
                        NewApp.DocNo,
                        NewApp.Activity, //strategic objective
                        NewApp.Appraisee_Objective, 
                        "",
                        NewApp.Weight,
                        NewApp.Target,
                        NewApp.pas_indicator,   /*NewApp.PAS_Activity, //null- for indicatior*/
                        " ",
                        NewApp.Unit_of_Measure,
                        NewApp.Self_Assessment_Target,
                        NewApp.Joint_Agreed_Target,
                        NewApp.Weighted_Score,
                        pmmuNumber
                       /* PopulateActivitiesFromVal,
                        0,
                        0*/
                    );
                return Json(new { message = "Performance Target Line added successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult PerformanceTargetLines(int capability, string? DocNo = null, string? contractNo = null, string? actionStage = null, string? Populate_Activities_From = null)
        {
            try
            {
                string StaffNo = Session["Username"].ToString();

                List<PerformanceTargetLines> performanceTargetLinesList = new List<PerformanceTargetLines>();
                string page = $"PerformanceTargetLines?$filter=Contract_No eq '{DocNo}'&$format=json";
                /*string page = $"PerformanceTargetLines";*/
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        PerformanceTargetLines performanceTargetLines = new PerformanceTargetLines();
                        performanceTargetLines.DocNo = (string)config["Contract_No"];
                        performanceTargetLines.PMMU_No = (string)config["PMMU_No"];
                        performanceTargetLines.Entry_No = (int)config["Entry_No"];
                        performanceTargetLines.Activity = (string)config["Activity"];
                        performanceTargetLines.PAS_Activity = (string)config["PAS_Activity"];
                        performanceTargetLines.Individual_Target = (string)config["Individual_Target"];
                        performanceTargetLines.Performance_Indicator = (string)config["Performance_Indicator"];
                        performanceTargetLines.Weight = (decimal)config["Weight"];
                        performanceTargetLines.Target = (int)config["Target"];
                        performanceTargetLines.Score = (decimal)config["Joint_Agreed_Target"];
                        performanceTargetLines.Weighted_Score = (decimal)config["Weighted_Score"];
                        performanceTargetLines.Self_Assessment_Target = (int)config["Self_Assessment_Target"];
                        performanceTargetLines.Unit_of_Measure = (string)config["Unit_of_Measure"];
                        performanceTargetLines.Joint_Agreed_Target = (int)config["Joint_Agreed_Target"];
                        performanceTargetLines.Supervisor_Assessment_Mid_year = (int)config["Supervisor_Assessment_Mid_year"];
                        performanceTargetLines.Self_Assessment_Mid_year = (int)config["Self_Assessment_Mid_year"];
                        performanceTargetLines.Appraisee_Objective = (string)config["Appraisee_Objective"];
                        //performanceTargetLines.Populate_Activities_From = (string)config["Populate_Activities_From"];
                        performanceTargetLines.Capability = HandleCapability(capability);
                        //performanceTargetLines.Populate_Activities_From = Populate_Activities_From;
                        performanceTargetLinesList.Add(performanceTargetLines);

                    }

                }
                var (totalWeight, isWeightValid) = CheckPASWeight(DocNo);

                ViewBag.Capable = HandleCapability(capability);
                ViewBag.ContractNo = contractNo;
                ViewBag.PAS_Stage = CheckPASDocStage();
                ViewBag.ActionStage = actionStage;
                ViewBag.TotalWeight = totalWeight * (decimal)0.8;
                ViewBag.OveralWeight = totalWeight;
                ViewBag.isDirector = CommonClass.isDirector(StaffNo);

                return PartialView("~/Views/Pas/Partial Views/PerformanceTargetLines.cshtml",
                    performanceTargetLinesList);

            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


        public ActionResult NewPerformanceTargetLine(string contractNo, string PopulateActivitiesFrom, int hideTriger, string responsibleEmployee, string currentPositionId = "")
        {
            string StaffNo = Session["Username"].ToString();
            string pasDocStage = CheckPASDocStage();
            try
            {
                PerformanceTargetLines NewAppl = new PerformanceTargetLines();
                List<DropdownList> ActivityList = new List<DropdownList>();

                string current_position_id = "";
                //current_position_id= Credentials.ObjNav.GetCurrentPositionId(responsibleEmployee);
                NewAppl.ListUnitsOfMeasure = GetUnitsOfMeasureDropdown();
                NewAppl.responsibleEmployee = responsibleEmployee;
                NewAppl.ListOfActivity = GetActivityDropdown(PopulateActivitiesFrom, contractNo, current_position_id);
                ViewBag.Trigger = hideTriger;
                ViewBag.pasDocStage = pasDocStage;
                ViewBag.contract_No = contractNo;
                ViewBag.PopulateActivitiesFrom = PopulateActivitiesFrom;

                bool isSupervisor = HandleCapability(0);
                bool isEmployeeDirector = !string.IsNullOrEmpty(responsibleEmployee) && CommonClass.isDirector(responsibleEmployee);

                if (isEmployeeDirector && isSupervisor)
                {
                    ViewBag.isDirector = true;
                }
                else
                {
                    ViewBag.isDirector = CommonClass.isDirector(StaffNo);
                }

                return PartialView("~/Views/Pas/Partial Views/NewPerformanceTargetLine.cshtml", NewAppl);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult GeneratePASReport(string documentNumber)
        {
            try
            {
                string message = "";
                bool success = false, view = false;

                message = Credentials.ObjNav.GeneratePASReport(documentNumber);
                if (message == "")
                {
                    success = false;
                    message = "File Not Found";
                }
                else
                {
                    success = true;
                }

                return Json(new { message = message, success, view }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SubmitCoreValues(PASCoreValuesLines NewApp)
        {
            NewApp.Appraisee_Comments =
                string.IsNullOrEmpty(NewApp.Appraisee_Comments) ? " " : NewApp.Appraisee_Comments;
            NewApp.Appraisers_Comments =
                string.IsNullOrEmpty(NewApp.Appraisers_Comments) ? " " : NewApp.Appraisers_Comments;
            try
            {
                Credentials.ObjNav.InsertPASValues(NewApp.DocNo, NewApp.Strategy_Plan_ID, NewApp.Core_Value,
                    NewApp.Appraisee_Comments, NewApp.Appraisers_Comments, NewApp.Behavioural_expectation, 0, 0, "");


                string successMessage = "PAS Core Values added successfully";
                return Json(new { successMessage, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteIndividualScorecard(string DocNo)
        {
            try
            {
                //Credentials.ObjNav.deletePMMU(DocNo);


                string successMessage = "Document deleted successfully";
                return Json(new { successMessage, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult SupervisorsApprisals()
        {
            StaffNo = Session["Username"]?.ToString();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            string pasDocStage = CheckPASDocStage();
            capability = HandleCapability(0);
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            bool isSupervisor = HandleCapability(0);
            ;
            try
            {
                if (Session["Username"] == null || !isSupervisor)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    ViewBag.PASDocStage = pasDocStage;
                    return View();
                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


        public PartialViewResult SupervisorAppraisalList()
        {
            ESSRoleSetup roleSetup = new ESSRoleSetup();
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string Employee_No = employeeView.No;
            string pasDocStage = CheckPASDocStage();

            List<IndividualScoreCard> ScoreCardList = new List<IndividualScoreCard>();


            string page =
                $"IndividualScoreCard?$filter=(Supervisors_Employee_No eq '{Employee_No}' and (Document_Stage eq 'Returned To Employee' or  Document_Stage eq 'Sent To Supervisor' or Document_Stage eq 'Mid-Year Review' or Document_Stage eq 'Returned to Supervisor' or Document_Stage eq 'Completed Mid-Year Review'  or Document_Stage eq 'Awaiting Evaluation' or Document_Stage eq 'Evaluated and Agreed'))&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    IndividualScoreCard TrList = new IndividualScoreCard();
                    TrList.DocNo = (string)config["No"];
                    TrList.Responsible_Employee_No = (string)config["Responsible_Employee_No"];
                    TrList.Employee_Name = (string)config["Employee_Name"];
                    TrList.Functional_Template_ID = (string)config["Functional_Template_ID"];
                    TrList.Document_Stage = (string)config["Document_Stage"];
                    TrList.Strategy_Plan_ID = (string)config["Strategy_Plan_ID"];
                    TrList.Contract_Year = (string)config["Contract_Year"];
                    TrList.Start_Date = (string)config["Start_Date"];
                    TrList.End_Date = (string)config["End_Date"];
                    TrList.Responsibility_Center_Name = (string)config["Responsibility_Center_Name"];
                    TrList.Status = (string)config["Status"];
                    ScoreCardList.Add(TrList);
                }
            }

            ViewBag.Capable = HandleCapability(0);
            ViewBag.PASDocStage = pasDocStage;

            return PartialView("~/Views/Pas/Partial Views/SupervisorAppraisalListListView.cshtml", ScoreCardList);
        }

        [HttpPost]
        public ActionResult SupervisorReviewDocView(string DocNo)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();
            try
            {
                IndividualScoreCard individualScoreCardItem = new IndividualScoreCard();


                string page = "IndividualScoreCard?$filter=No eq '" + DocNo + "'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {

                        individualScoreCardItem.DocNo = (string)config["No"];
                        individualScoreCardItem.Approval_Status = (string)config["Approval_Status"];
                        individualScoreCardItem.Department = (string)config["Department"];
                        individualScoreCardItem.Contract_Year = (string)config["Contract_Year"];
                        individualScoreCardItem.Designation = (string)config["Designation"];

                        individualScoreCardItem.Employee_Name = string.IsNullOrEmpty((string)config["Employee_Name"])
                            ? staffName
                            : (string)config["Employee_Name"];

                        individualScoreCardItem.Responsibility_Center = (string)config["Responsibility_Center"];
                        individualScoreCardItem.Responsibility_Center_Name =
                            (string)config["Responsibility_Center_Name"];

                        individualScoreCardItem.End_Date = (string)config["End_Date"];
                        individualScoreCardItem.Start_Date = (string)config["Start_Date"];
                        individualScoreCardItem.Status = (string)config["Status"];
                        individualScoreCardItem.Strategy_Plan_ID = (string)config["Strategy_Plan_ID"];
                        individualScoreCardItem.Total_Assigned_Weight_Percent =
                            (int)config["Total_Assigned_Weight_Percent"];
                        individualScoreCardItem.Responsible_Employee_No =
                            string.IsNullOrEmpty((string)config["Responsible_Employee_No"])
                                ? StaffNo
                                : (string)config["Responsible_Employee_No"];
                        individualScoreCardItem.Blocked_x003F_ = (bool)config["Blocked_x003F_"];
                        individualScoreCardItem.Created_By = (string)config["Created_By"];
                        individualScoreCardItem.Created_On = (string)config["Created_On"];
                        individualScoreCardItem.Change_Status = (string)config["Change_Status"];
                        individualScoreCardItem.JD_Assigned_Weight_Percent = (int)config["JD_Assigned_Weight_Percent"];
                        individualScoreCardItem.Functional_Template_ID = (string)config["Functional_Template_ID"];
                        individualScoreCardItem.Document_Stage = (string)config["Document_Stage"];
                        individualScoreCardItem.Admin_Unit_Name = (string)config["Admin_Unit_Name"];
                        individualScoreCardItem.Populate_Activities_From = (string)config["Populate_Activities_From"];
                        individualScoreCardItem.Supervisors_Employee_Name = (string)config["Supervisors_Employee_Name"];
                        individualScoreCardItem.Current_Position_Id = (string)config["positionId"];
                        bool isSupervisor = HandleCapability(0);
                        bool isEmployeeDirector = !string.IsNullOrEmpty((string)config["Responsible_Employee_No"]) && CommonClass.isDirector((string)config["Responsible_Employee_No"]);

                        if (isEmployeeDirector && isSupervisor)
                        {
                            individualScoreCardItem.isDirector = true;
                        }
                        else
                        {
                            individualScoreCardItem.isDirector = CommonClass.isDirector((string)config["Responsible_Employee_No"]);
                        }
                    }

                }

                ViewBag.capability = HandleCapability(0);
                ViewBag.PASDocStage = CheckPASDocStage();
                ViewBag.TrainingCount = CheckTrainingCount(DocNo);

                return View(individualScoreCardItem);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public JsonResult SubmitSupervisorAction(string respemployee, string DocNo, int Action, string reason)
        {

            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string supervisorID = employeeView.UserID;
            try
            {
                string returnReason = "";
                if (Action == 3)
                {
                    returnReason = reason;
                }
                var (totalWeight, isWeightValid) = CheckPASWeight(DocNo);

                if (!isWeightValid)
                {
                    return Json(
                        new
                        {
                            success = false,
                            message = $"Please ensure that the total weight is strictly 100. Current total is {totalWeight}. Please add or remove activities."
                        },
                        JsonRequestBehavior.AllowGet);
                }

                //Credentials.ObjNav.SuperVisorActions(supervisorID, respemployee, DocNo, Action, returnReason);
                return Json(new { success = true, message = "Action completed successfully." },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message.Replace("'", "") },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public PartialViewResult PASCoreValuesLines(string DocNo, string Strategic_Plan, int capability,
            string actionStage, bool hide = false)
        {
            try
            {
                string StaffNo = Session["Username"]?.ToString();
                List<PASCoreValuesLines> PASCoreValuesLinesList = new List<PASCoreValuesLines>();
                string odataQuery = $"PASCoreValuesLines?$filter=Performance_Contract_Header eq '{DocNo}'&$format=json";

                using (var httpResponse = Credentials.GetOdataData(odataQuery))
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        PASCoreValuesLines pasCoreValuesLines = new PASCoreValuesLines
                        {
                            DocNo = (string)config["Performance_Contract_Header"],
                            Appraisee_Comments = (string)config["Appraisee_Comments"],
                            Appraisers_Comments = (string)config["Appraisers_Comments"],
                            Strategic_Plan_ID = (string)config["Strategic_Plan_ID"],
                            Core_Value = (string)config["Core_Value"],
                            Behavioural_expectation = (string)config["Description"],
                            Joint_Assessment = (int)config["Joint_Assessment"],
                            Key_Performance_Indicator = (string)config["Key_Performance_Indicator"],
                            Self_Assessment = (int)config["Self_Assessment"],
                            Line_No = (int)config["Line_No"],
                            Score = (string)config["Score"]
                        };
                        PASCoreValuesLinesList.Add(pasCoreValuesLines);
                    }
                }

                ViewBag.Hide = hide;
                ViewBag.Capable = HandleCapability(capability);
                ViewBag.PASDocStage = CheckPASDocStage();
                ViewBag.ActionStage = actionStage;
                return PartialView("~/Views/Pas/Partial Views/PASCoreValuesLines.cshtml", PASCoreValuesLinesList);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


        public JsonResult SubmitAppraiseeAction(string docNo, int actionValue)
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string userID = employeeView.UserID;

            try
            {
                var (totalWeight, isWeightValid) = CheckPASWeight(docNo);

                if (!isWeightValid)
                {
                    return Json(
                        new
                        {
                            success = false,
                            message = $"Please ensure that the total weight is 100. Current total is {totalWeight}. Please add or remove activities."
                        },
                        JsonRequestBehavior.AllowGet);
                }

                Credentials.ObjNav.UpdateEmployeeActions(docNo, actionValue, userID);
                return Json(new { success = true, message = "Action completed successfully." },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message.Replace("'", "") },
                    JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public PartialViewResult UpdateCoreValuesLine(PASCoreValuesLines data)
        {

            string DocNo = data.DocNo;
            int Line_No = data.Line_No;
            string Core_Value = data.Core_Value;
            string Appraisee_Comments = data.Appraisee_Comments;
            string Strategic_Plan_ID = data.Strategic_Plan_ID;
            string pasDocStage = CheckPASDocStage();

            try
            {


                PASCoreValuesLines NewAppl = new PASCoreValuesLines();
                List<DropdownList> CoreValuesDropDown = new List<DropdownList>();
                string page = $"StrategyCoreValue?$filter=Strategic_Plan_ID eq '{Strategic_Plan_ID}'&$format=json";
                /*string page = $"StrategyCoreValue&$format=json";*/
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Core_Value"];
                        dropdownList.Value = (string)config["Core_Value"];
                        CoreValuesDropDown.Add(dropdownList);
                    }
                }

                NewAppl.ListOfCoreValues = CoreValuesDropDown.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                NewAppl.DocNo = DocNo;
                NewAppl.Line_No = Line_No;
                NewAppl.Core_Value = Core_Value;
                NewAppl.Appraisee_Comments = Appraisee_Comments;
                NewAppl.Strategic_Plan_ID = data.Strategic_Plan_ID;
                NewAppl.Joint_Assessment = data.Joint_Assessment;
                NewAppl.Self_Assessment = data.Self_Assessment;
                NewAppl.Key_Performance_Indicator = data.Key_Performance_Indicator;
                NewAppl.Behavioural_expectation = data.Behavioural_expectation;
                NewAppl.stage = data.stage;

                ViewBag.Capable = data.capability;
                ViewBag.pasDocStage = pasDocStage;
                return PartialView("~/Views/Pas/Partial Views/UpdateCoreValuesLine.cshtml", NewAppl);

            }
            catch (Exception ex)
            {
                Error erroMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public JsonResult SubmitUpdateCoreValuesLine(PASCoreValuesLines updatedItems)
        {
            try
            {
                decimal weightedScore = 0;
                if (updatedItems.Joint_Assessment != 0)
                {
                    weightedScore = Math.Round((decimal)((updatedItems.Joint_Assessment / 5.0) * 4), 2);
                }

                string DocNo = updatedItems.DocNo;
                int Line_No = updatedItems.Line_No;
                string Core_Value = updatedItems.Core_Value;
                string Strategic_Plan_ID = updatedItems.Strategic_Plan_ID;
                int Joint_Assessment = updatedItems.Joint_Assessment;
                int Self_Assessment = updatedItems.Self_Assessment;
                string Behavioural_expectation = updatedItems.Behavioural_expectation;

                string Appraisee_Comments = !string.IsNullOrWhiteSpace(updatedItems.Appraisee_Comments)
                    ? updatedItems.Appraisee_Comments
                    : " ";

                string Appraisers_Comments = !string.IsNullOrWhiteSpace(updatedItems.Appraisers_Comments)
                    ? updatedItems.Appraisers_Comments
                    : " ";


                Credentials.ObjNav.UpdatePASValues(DocNo, Strategic_Plan_ID, Core_Value, Line_No, Appraisee_Comments,
                    Appraisers_Comments, Behavioural_expectation, Joint_Assessment, Self_Assessment, weightedScore,
                    Behavioural_expectation);
                return Json(new { success = true, message = "Record updated successfully!" },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public PartialViewResult ViewTargetLine(PerformanceTargetLines data)
        {
            string StaffNo = Session["Username"]?.ToString();
            if (data == null)
            {
                return PartialView("~/Views/Pas/Partial Views/ViewTargetLine.cshtml", new PerformanceTargetLines());
            }

            string source = "";

            var lineContent = new PerformanceTargetLines
            {
                DocNo = data.DocNo,
                Entry_No = data.Entry_No,
                Activity = data.Activity,
                Individual_Target = data.Individual_Target,
                Performance_Indicator = data.Performance_Indicator,
                PAS_Activity = data.PAS_Activity,
                Unit_of_Measure = data.Unit_of_Measure,
                Weight = data.Weight,
                Target = data.Target,
                Self_Assessment_Target = data.Self_Assessment_Target,
                Joint_Agreed_Target = data.Joint_Agreed_Target,
                Weighted_Score = data.Weighted_Score,
                PMMU_No = data.PMMU_No,
                stage = data.stage,
                ListUnitsOfMeasure = GetUnitsOfMeasureDropdown()
            };

            ViewBag.contract_No = data.PMMU_No;
            ViewBag.DocNo = data.DocNo;
            ViewBag.Entry_No = data.Entry_No;
            ViewBag._Stage = CheckPASDocStage();
            ViewBag.isCapable = data.Capability;
            ViewBag.populatedFrom = data.Populate_Activities_From;
            ViewBag.isDirector = CommonClass.isDirector(StaffNo);
            return PartialView("~/Views/Pas/Partial Views/ViewTargetLine.cshtml", lineContent);
        }

        public string GetPopulateActivitiesFrom(string contractNo)
        {
            try
            {
                string populateActivitiesFrom = string.Empty;

                string page = "IndividualScoreCard?$filter=No eq '" + contractNo + "'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        populateActivitiesFrom = (string)config["Populate_Activities_From"];
                    }
                }

                return populateActivitiesFrom;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult SubmitUpdatedTargetLine(PerformanceTargetLines newTarget)
        {
            try
            {
                string DocNo = newTarget.DocNo;
                int Entry_No = newTarget.Entry_No;
                string Activity = "";



                if (newTarget.Source == "Job Description")
                {
                    Activity = GetJobDescriptionName(newTarget.Source, newTarget.Activity);
                }
                else
                {
                    Activity = newTarget.Activity;
                }

                string Individual_Target = newTarget.Individual_Target;
                string PAS_Activity = " ";
                //note that PAS_Activity is Performance Indicator field in the NAV 
                string Performance_Indicator = newTarget.PAS_Activity;
                string Unit_of_Measure = newTarget.Unit_of_Measure;
                decimal Weight = newTarget.Weight;
                decimal Target = newTarget.Target;
                decimal Self_Assessment_Target = newTarget.Self_Assessment_Target;
                decimal Joint_Agreed_Target = newTarget.Joint_Agreed_Target;
                decimal Weighted_Score = newTarget.Weighted_Score;
                string PMMU_No = newTarget.PMMU_No;


                if (Joint_Agreed_Target == 0)
                {
                    Weighted_Score = 0;
                }
                else
                {
                    Weighted_Score =
                        (int)Math.Round(
                            (Convert.ToDouble(Joint_Agreed_Target) / Convert.ToDouble(Target)) * (double)Weight, 2);
                }

                Credentials.ObjNav.UpdatePerfomanceTargetsLines(DocNo, Entry_No, Activity, PAS_Activity,
                    Individual_Target, Weight, Target, Performance_Indicator, Performance_Indicator, Unit_of_Measure,
                    Self_Assessment_Target, Joint_Agreed_Target, Weighted_Score, PMMU_No);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public List<SelectListItem> GetUnitsOfMeasureDropdown()
        {
            List<SelectListItem> unitsOfMeasureDropdown = new List<SelectListItem>();
            /*string page = "UnitsOfMeasure";*/
            string page = "UnitsOfMeasure?$filter=ForAppraisal eq true";


            try
            {
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        SelectListItem dropdownItem = new SelectListItem
                        {
                            Text = (string)config["Description"],
                            Value = (string)config["Description"]
                        };
                        unitsOfMeasureDropdown.Add(dropdownItem);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error fetching Units of Measure: " + ex.Message);
            }

            return unitsOfMeasureDropdown;
        }

        public List<SelectListItem> GetActivityDropdown(string source, string contractNo, string responsibleEmployee, string currentPositionId = "")

        {
            string StaffNo = Session["Username"]?.ToString();

            string current_position_id = "";
            //current_position_id= Credentials.ObjNav.GetCurrentPositionId(responsibleEmployee);


            List<SelectListItem> activityDropdown = new List<SelectListItem>();



            string page = source == "PMMU"
                    //? $"PMMULines?$filter=Contract_No eq '{contractNo}' and Strategy_Plan_ID eq 'STAJ-00001'"
                    ? $"PMMULines?$filter=Contract_No eq '{contractNo}'"
                    : $"PositionTargetList?$filter=Position_Code eq '{current_position_id}'";

            try
            {

                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {

                        SelectListItem dropdownItem = new SelectListItem
                        {
                            Text = source == "PMMU"
                                ? (string)config["Outputs"]
                                : (string)config["Strategic_Objective"],
                            Value = source == "PMMU" ? (string)config["Entry_No"] : (string)config["Line_No"]
                        };
                        activityDropdown.Add(dropdownItem);
                    }
                }
            }


            catch (Exception ex)
            {

                Console.WriteLine("Error fetching activity data: " + ex.Message);
            }

            return activityDropdown;
        }

        public JsonResult SetSource(string activityFrom, string contractNo, string responsibleEmployee)
        {
            var activities = GetActivityDropdown(activityFrom, contractNo, responsibleEmployee);
            ViewBag.ActivityList = activities;
            return Json(activities, JsonRequestBehavior.AllowGet);
        }



        public PartialViewResult PASTrainingNeeds(string DocNo, string actionStage, bool hide = false)
        {
            try
            {
                List<PASTrainingNeedsLine> PASTrainingNeeds = new List<PASTrainingNeedsLine>();
                string odataQuery = $"PASTrainingNeeds?$filter=PAS_No eq '{DocNo}'&$format=json";

                using (var httpResponse = Credentials.GetOdataData(odataQuery))
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        PASTrainingNeedsLine PASTrainingNeedsLines = new PASTrainingNeedsLine
                        {
                            DocNo = (string)config["PAS_No"],
                            Line_No = (int)config["Line_No"],
                            Training_Need = (string)config["Training_Need"],
                            Supervisors_Remarks = (string)config["Supervisors_Remarks"],
                            stage = actionStage,
                        };
                        PASTrainingNeeds.Add(PASTrainingNeedsLines);
                    }
                }


                ViewBag.TrainingCount = CheckTrainingCount(DocNo);

                ViewBag.Hide = hide;
                ViewBag.stage = actionStage;

                return PartialView("~/Views/Pas/Partial Views/PASTrainingNeedsLines.cshtml", PASTrainingNeeds);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public int CheckTrainingCount(string DocNo)
        {
            try
            {
                List<PASTrainingNeedsLine> PASTrainingNeeds = new List<PASTrainingNeedsLine>();
                string odataQuery = $"PASTrainingNeeds?$filter=PAS_No eq '{DocNo}'&$format=json";

                using (var httpResponse = Credentials.GetOdataData(odataQuery))
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        PASTrainingNeedsLine PASTrainingNeedsLines = new PASTrainingNeedsLine
                        {
                            DocNo = (string)config["PAS_No"],
                            Line_No = (int)config["Line_No"],
                            Training_Need = (string)config["Training_Need"],
                            Supervisors_Remarks = (string)config["Supervisors_Remarks"],
                        };
                        PASTrainingNeeds.Add(PASTrainingNeedsLines);
                    }
                }

                return PASTrainingNeeds.Count;
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error { Message = ex.Message };
                return 0;
            }
        }


        public PartialViewResult NewPASTrainingNeed(string docNo, int capability)
        {

            try
            {
                PASTrainingNeedsLine PASTrainingNeeds = new PASTrainingNeedsLine();
                PASTrainingNeeds.DocNo = docNo;
                ViewBag.capable = HandleCapability(capability);
                return PartialView("~/Views/Pas/Partial Views/NewPASTrainingNeed.cshtml", PASTrainingNeeds);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [HttpPost]
        public JsonResult SubmitTrainingNeed(PASTrainingNeedsLine data)
        {
            try
            {
                string DocNo = data.DocNo;
                string Training_Need = data.Training_Need;
                string Supervisors_Remarks = string.IsNullOrWhiteSpace(data.Supervisors_Remarks) ? " " : data.Supervisors_Remarks;

                Credentials.ObjNav.insertPASTrainingNeeds(DocNo, Training_Need, Supervisors_Remarks);


                return Json(new { message = "Training Suggestion added successfully", success = true },
                    JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message.Replace("'", "") },
                      JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult CoreValuesResponse(string coreValue)
        {
            var pageResC = $"StrategyCoreValue?$filter=Core_Value eq '{coreValue}'&$format=json";
            var CList = new DropdownList();

            try
            {
                using (var response = Credentials.GetOdataData(pageResC))
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    var firstDetail = details["value"].FirstOrDefault();
                    if (firstDetail != null)
                    {
                        CList.Value = (string)firstDetail["Description"] ?? "Default Value";
                        CList.Text = (string)firstDetail["Description"] ?? "Default Text";
                    }
                }

                return Json(CList);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public string GetJobDescriptionName(string source, string activity)
        {

            try
            {
                var page = $"PositionTargetList?$filter=Line_No eq {activity}&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        return (string)config["Strategic_Objective"];
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return null;
        }

        public string CheckPASDocStage()
        {
            try
            {
                string pageGenSetup = "GeneralSetup?$format=json";
                HttpWebResponse httpResponseGenSetup = Credentials.GetOdataData(pageGenSetup);
                using (var streamReader = new StreamReader(httpResponseGenSetup.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    string pasDocStage = (string)details["value"][0]["PAS_Document_Stage"];
                    pasDocStage = string.IsNullOrWhiteSpace(pasDocStage) ? "Initial stage" : pasDocStage;
                    return pasDocStage;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message.Replace("'", "");
            }
        }

        public JsonResult GetStrategyOutputCodes(string Strategy_ID)
        {
            try
            {
                #region StrategyOutputCodesLookup

                List<DropdownList> StrategyOutputCodesLookup = new List<DropdownList>();
                string pageSOC = $"StrategyOutput?$filter=Strategy_ID eq '{Strategy_ID}'&$format=json";

                HttpWebResponse httpResponseSOC = Credentials.GetOdataData(pageSOC);
                using (var streamReader = new StreamReader(httpResponseSOC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Strategy_Output_Code"] + "-" + (string)config["Outputs"];
                        dropdownList.Value = (string)config["Outputs"];
                        StrategyOutputCodesLookup.Add(dropdownList);
                    }
                }

                #endregion

                var response = new
                {
                    ListOfStrategyOutputCodes = StrategyOutputCodesLookup.Select(x => new SelectListItem
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

        public JsonResult GetStrategies(string Objective_ID)
        {
            try
            {
                #region StrategiesLookup

                List<DropdownList> StrategiesLookup = new List<DropdownList>();
                string pageStrat = $"Strategies?$filter=Objective_ID eq '{Objective_ID}'&$format=json";

                HttpWebResponse httpResponseStrat = Credentials.GetOdataData(pageStrat);
                using (var streamReader = new StreamReader(httpResponseStrat.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Strategy_ID"] + "-" + (string)config["Description"];
                        dropdownList.Value = (string)config["Strategy_ID"];
                        StrategiesLookup.Add(dropdownList);
                    }
                }

                #endregion

                var response = new
                {
                    ListOfStrategies = StrategiesLookup.Select(x => new SelectListItem
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

        public JsonResult GetStrategicObjectives(string Strategic_Plan_ID)
        {
            try
            {
                #region StrategicObjectivesLookup

                List<DropdownList> StrategicObjectivesLookup = new List<DropdownList>();
                string pageStratObj = $"CoreStrategy?$filter=Strategic_Plan_ID eq '{Strategic_Plan_ID}'&$format=json";

                HttpWebResponse httpResponseStratObj = Credentials.GetOdataData(pageStratObj);
                using (var streamReader = new StreamReader(httpResponseStratObj.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Objective_ID"] + "-" + (string)config["Description"];
                        dropdownList.Value = (string)config["Objective_ID"];
                        StrategicObjectivesLookup.Add(dropdownList);
                    }
                }

                #endregion

                var response = new
                {
                    ListOfStrategicObjectives = StrategicObjectivesLookup.Select(x => new SelectListItem
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

        public bool HandleCapability(int toggleValue)
        {
            if (toggleValue == 1) return false;

            var employee = Session["EmployeeData"] as EmployeeView;
            return CommonClass.ISSupervisor(employee.UserID);
        }



        public JsonResult DeleteTargetLine(string DocNo, int Entry_No)
        {

            try
            {
                Credentials.ObjNav.DeletePerfomanceTargetsLines(DocNo, Entry_No);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }



        }

        public JsonResult DeleteCoreValuesLine(string DocNo, int Line_No, string Strategic_Plan_ID)
        {
            try
            {
                Credentials.ObjNav.DeletePASValues(DocNo, Strategic_Plan_ID, Line_No);
                return Json(new { success = true, message = "Core Value Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult DeleteTrainingNeed(string DocNo, int Line_No)
        {
            try
            {
                Credentials.ObjNav.DeletePASTrainingNeeds(DocNo, Line_No);
                return Json(new { success = true, message = "Training Suggestion deleted successfully" },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult EditTraining(PASTrainingNeedsLine data, int capability)
        {
            try
            {
                var PASTrainingNeeds = new PASTrainingNeedsLine
                {
                    DocNo = data.DocNo,
                    Line_No = data.Line_No,
                    Training_Need = data.Training_Need,
                    Supervisors_Remarks = data.Supervisors_Remarks
                };
                ViewBag.capable = HandleCapability(capability);
                return PartialView("~/Views/Pas/Partial Views/EditTraining.cshtml", PASTrainingNeeds);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public JsonResult SendForApproval(string docNo)
        {
            try
            {
                Credentials.ObjNav.sendPasforApproval(docNo);
                return Json(new { success = true, message = "Document sent for approval" },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelApproval(string docNo)
        {
            try
            {
                Credentials.ObjNav.cancelPasApproval(docNo);
                return Json(new { success = true, message = "Approval cancelled" },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateTrainingLines(PASTrainingNeedsLine data)
        {
            data.Supervisors_Remarks = string.IsNullOrWhiteSpace(data.Supervisors_Remarks) ? " " : data.Supervisors_Remarks;
            try
            {
                string Supervisors_Remarks = string.IsNullOrWhiteSpace(data.Supervisors_Remarks) ? " " : data.Supervisors_Remarks;

                string docNo = data.DocNo;
                int lineNo = data.Line_No;
                string trainingNeed = data.Training_Need;
                string supervisorsRemarks = Supervisors_Remarks;

                Credentials.ObjNav.updatePASTrainingNeeds(docNo, trainingNeed, supervisorsRemarks, lineNo);
                return Json(new { success = true, message = "Training Suggestion updated successfully" },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult AutoCreateCoreValues(string DocNo)
        {
            try
            {
                string Strategy_Plan_ID = GetStrategicPlanId(DocNo);

                if (string.IsNullOrEmpty(Strategy_Plan_ID))
                {
                    return Json(new { message = "Strategic Plan ID not found", success = false }, JsonRequestBehavior.AllowGet);
                }

                PASCoreValuesLines NewAppl = new PASCoreValuesLines();
                string page = $"StrategyCoreValue?$filter=Strategic_Plan_ID eq '{Strategy_Plan_ID}'&$format=json";

                HttpWebResponse httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        try
                        {
                            Credentials.ObjNav.InsertPASValues(DocNo, Strategy_Plan_ID, (string)config["Core_Value"],
                                                               " ", " ", (string)config["Description"], 0, 0, (string)config["Description"]);
                        }
                        catch (Exception ex)
                        {

                            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public string GetStrategicPlanId(string docNo)
        {
            try
            {
                string page = $"IndividualScoreCard?$filter=No eq '{docNo}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        return (string)config["Strategy_Plan_ID"];
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public PartialViewResult FileAttachmentForm()
        {
            try
            {
                return PartialView("~/Views/Pas/Partial Views/FileAttachmentForm.cshtml");
            }
            catch (Exception ex)
            {
                var erroMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }

        }

        public ActionResult EmployeePASReport()
        {
            try
            {

                List<SelectListItem> dimensionDropdown = AdminUnitDropdown();
                ViewBag.DimensionDropdown = dimensionDropdown;

                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


        public ActionResult GenerateEmployeePASReport(string AdminUnit, int DocType, bool filterByAdmin, bool filterByDoc, bool type)
        {
            try
            {
                string base64String = "";
                base64String = Credentials.ObjNav.GeneratePASEmpReport(AdminUnit, DocType, filterByDoc, type);
                if (string.IsNullOrEmpty(base64String))
                {
                    return Json(new { message = "File Not Found", success = false }, JsonRequestBehavior.AllowGet);
                }

                byte[] fileBytes = Convert.FromBase64String(base64String);

                if (type == true)
                {
                    return File(fileBytes, "application/vnd.ms-excel", "Employee_Target_Setting_Report.xlsx");
                }

                return Json(new { message = base64String, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }



        public List<SelectListItem> AdminUnitDropdown()
        {
            List<SelectListItem> activityDropdown = new List<SelectListItem>();
            string page = "DimensionValueList";

            try
            {
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        SelectListItem dropdownItem = new SelectListItem
                        {
                            Text = (string)config["Name"],
                            Value = (string)config["Code"]
                        };
                        activityDropdown.Add(dropdownItem);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error fetching activity data: " + ex.Message);
            }

            return activityDropdown;
        }


        public (decimal TotalWeight, bool IsEighty) CheckPASWeight(string DocNo)
        {
            string page = $"PerformanceTargetLines?$filter=Contract_No eq '{DocNo}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                decimal totalWeight = 0;

                foreach (JObject config in details["value"])
                {
                    decimal weight = (decimal)config["Weight"];
                    totalWeight += weight;
                }

                bool isEighty = totalWeight == 100;
                return (totalWeight, isEighty);
            }
        }


        public ActionResult GenerateTargetNotSetReport(int type, string Dimension1 = "", string Dimension2 = "")
        {
            try
            {
                //string base64String = Credentials.ObjNav.GenerateTargetNotSetReport(Dimension1, Dimension2, type);
                string base64String = "";
                if (string.IsNullOrEmpty(base64String))
                {
                    return Json(new { message = "File Not Found", success = false }, JsonRequestBehavior.AllowGet);
                }
                byte[] fileBytes = Convert.FromBase64String(base64String);

                if (type == 1)
                {
                    return File(fileBytes, "application/vnd.ms-excel", "TargetNotSetReport.xlsx");
                }
                return File(fileBytes, "application/pdf", "TargetNotSetReport.pdf");

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult TargetNotSetReport()
        {
            try
            {
                List<SelectListItem> dimensionDropdown = AdminUnitDropdown();
                ViewBag.DimensionDropdown = dimensionDropdown;

                List<DropdownList> dimensionValues = DimensinValuesList.GetDimensionValues(1);
                List<SelectListItem> dimensionValuesDropdown = dimensionValues
                    .Select(d => new SelectListItem
                    {
                        Text = d.Text,
                        Value = d.Value
                    }).ToList();
                ViewBag.DimensionValuesDropdown = dimensionValuesDropdown;

                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


        public JsonResult GetIndicator(string PopulateActivitiesFrom, string Entry_No, string contractNo)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string positionCode = employeeView.Current_Position_ID;
                List<SelectListItem> activityDropdown = new List<SelectListItem>();
                //string pageIndicator2 = $"PMMULines?$filter=Contract_No eq '{contractNo}' and Entry_No eq {Entry_No}&$format=json";

                string pageIndicator = PopulateActivitiesFrom == "PMMU"
                ? $"PMMULines?$filter=Contract_No eq '{contractNo}' and Entry_No eq {Entry_No}&$format=json"
                : $"PositionTargetList?$filter=Position_Code eq '{positionCode}'  and Line_No eq {Entry_No}";


                HttpWebResponse httpResponseProcDoc = Credentials.GetOdataData(pageIndicator);
                using (var streamReader = new StreamReader(httpResponseProcDoc.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    if (details["value"] != null && details["value"].Any())
                    {
                        var document = details["value"].First(); // Get the first matching document

                        if (PopulateActivitiesFrom == "PMMU")
                        {
                            string indicator = document["Output_Indicators"]?.ToString() ?? "N/a";
                            var EntryNo = document["Entry_No"]?.ToString() ?? "N/a";
                            var StrategyOutputCode = document["Strategy_Output_Code"]?.ToString() ?? "N/a";
                            var ContractNo = document["Contract_No"]?.ToString() ?? "N/a";

                            ViewBag.EntryNo = EntryNo;
                            ViewBag.Strategy_Output_Code = StrategyOutputCode;
                            ViewBag.ContractNo = ContractNo;

                            var description = new
                            {
                                indicator = indicator,
                                EntryNo = EntryNo,
                                StrategyOutputCode = StrategyOutputCode,
                                ContractNo = ContractNo
                            };

                            return Json(new { success = true, description = description }, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            string indicator = document["KPI"]?.ToString() ?? "N/a";
                            var EntryNo = document["Line_No"]?.ToString() ?? "N/a";
                            var Unit_Of_Measure = document["Unit_Of_Measure"]?.ToString() ?? "N/a";
                            var Assigned_Weight_Percent = document["Assigned_Weight_Percent"]?.ToString() ?? "0";
                            /*var StrategyOutputCode = document["Strategy_Output_Code"]?.ToString() ?? "N/a";
                            var ContractNo = document["Contract_No"]?.ToString() ?? "N/a";*/

                            ViewBag.EntryNo = EntryNo;
                            /*ViewBag.Strategy_Output_Code = StrategyOutputCode;
                            ViewBag.ContractNo = ContractNo;*/

                            var description = new
                            {
                                indicator = indicator,
                                EntryNo = EntryNo,
                                Unit_Of_Measure = Unit_Of_Measure,
                                Assigned_Weight_Percent = Assigned_Weight_Percent


                                /* StrategyOutputCode = StrategyOutputCode,
                                 ContractNo = ContractNo*/
                            };

                            return Json(new { success = true, description = description }, JsonRequestBehavior.AllowGet);

                        }

                    }
                }

                return Json(new { success = false, description = "Document not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message.Replace("'", "") }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetActivities(string PopulateActivitiesFrom, string contractNo, string selectedValue, string EntryNo, string StrategyOutputCode)
        {
            PerformanceTargetLines ActivitiesList = new PerformanceTargetLines();
            List<DropdownList> Activities = new List<DropdownList>();
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string positionCode = employeeView.Current_Position_ID;




            string page = PopulateActivitiesFrom == "PMMU"
                ? $"PMMUOutputActivities?$filter=Strategy_Output_Code eq '{StrategyOutputCode}' and Contract_No eq '{contractNo}' and Entry_No eq {selectedValue}&$format=json"
                : $"PositionTargetList?$filter=Position_Code eq '{positionCode}'";

            //string page2 = $"PositionTargetList?$filter=Strategic_Objective eq '{StrategyOutputCode}' and Line_No eq '{contractNo}'&$format=json";


            HttpWebResponse httpResponse = Credentials.GetOdataData(page);

            if (httpResponse != null && httpResponse.GetResponseStream() != null)
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    if (details["value"] != null)
                    {
                        foreach (JObject config in details["value"])
                        {
                            // Ensure "Entry" and "Activities" exist in API response
                            DropdownList dropDown = new DropdownList
                            {

                                Text = PopulateActivitiesFrom == "PMMU"
                                ? (string)config["Activities"]
                                : (string)config["Strategic_Objective"],
                                Value = PopulateActivitiesFrom == "PMMU" ? (string)config["Entry"] : (string)config["Line_No"]

                            };
                            Activities.Add(dropDown);
                        }
                    }
                }
            }

            return Json(Activities);
        }


        public JsonResult GetActivitiesForEdit(string strategicObjective, string contractNo, string populateActivitiesFrom)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string positionCode = employeeView.Current_Position_ID;
                List<SelectListItem> activitiesDropdown = new List<SelectListItem>();

                string apiEndpoint;
                if (populateActivitiesFrom == "PMMU")
                {
                    apiEndpoint = $"PMMULines?$filter=Contract_No eq '{contractNo}' and Outputs eq '{strategicObjective}'&$format=json";
                }
                else
                {

                    apiEndpoint = $"PositionTargetList?$filter=Position_Code eq '{positionCode}' and Strategic_Objective eq '{strategicObjective}'";
                }

                HttpWebResponse httpResponse = Credentials.GetOdataData(apiEndpoint);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    if (details["value"] != null && details["value"].Any())
                    {
                        var matchedObject = details["value"].First();

                        if (populateActivitiesFrom == "PMMU")
                        {
                            string entryNo = matchedObject["Entry_No"]?.ToString();
                            string strategyOutputCode = matchedObject["Strategy_Output_Code"]?.ToString() ?? "";

                            if (!string.IsNullOrEmpty(entryNo))
                            {
                                return GetActivities(populateActivitiesFrom, contractNo, entryNo, entryNo, strategyOutputCode);
                            }
                        }
                        else
                        {
                            string lineNo = matchedObject["Line_No"]?.ToString();
                            if (!string.IsNullOrEmpty(lineNo))
                            {
                                return GetActivities("Job Description", null, lineNo, null, null);
                            }
                        }
                    }
                }
                return Json(new { success = true, result = new List<SelectListItem>() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message.Replace("'", "") }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult EvidenceView(string DocNo, int LineNo, string Objective, string PAS_Stage, string documentStage)
        {
            try
            {
                var EvidenceVal = new Evidence
                {
                    DocNo = DocNo,
                    Line_No = LineNo,
                    Objective = Objective,
                    PAS_Stage = PAS_Stage,
                    Document_Stage = documentStage
                };

                return PartialView("~/Views/Pas/Partial Views/EvidenceView.cshtml", EvidenceVal);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error { Message = ex.Message };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }




    }

}

