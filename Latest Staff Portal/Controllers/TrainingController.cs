using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static iTextSharp.text.pdf.events.IndexEvents;
using static Latest_Staff_Portal.Models.CommonClass;

namespace Latest_Staff_Portal.Controllers
{
    public class TrainingController : Controller
    {


        //training need requisition
        public ActionResult TrainingNeeds()
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
        public PartialViewResult TrainingNeedsList()
        {
            var employee = Session["EmployeeData"] as EmployeeView;
            var userId = employee?.UserID;
            var StaffNo = Session["Username"].ToString();
            var rsrceReq = $"TrainingNeedRequests?$filter=Employee_No eq '{StaffNo}'&$orderby=Code desc&$format=json";
            var httpResponse = Credentials.GetOdataData(rsrceReq);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var odataResponse = JsonConvert.DeserializeObject<ODataResponse<TrainingNeedRequests>>(result);
            var trainingNeedRequests = odataResponse?.Value ?? [];

            return PartialView("PartialViews/TrainingNeedsList", trainingNeedRequests);
        }
        public JsonResult CreateTrainingNeedsRequest()
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                //var loggedInUser = employee?.UserID;
                var loggedInUser = Session["UserID"].ToString();

                if (string.IsNullOrEmpty(loggedInUser))
                {
                    return Json(new { message = "User session expired. Please login again.", success = false },
                        JsonRequestBehavior.AllowGet);
                }

                var docNo = "";
                docNo = Credentials.ObjNav.createNewTrainingRequest(employee.No);


                if (docNo != "")
                {
                    return Json(new { message = docNo, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Document Not created. Try again", success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TrainingNeedsDocumentView(string code)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");
                var employee = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();
                var TrainingNeedDoc = new TrainingNeedRequests();
                var page = "TrainingNeedRequests?$filter=Code eq '" + code + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        TrainingNeedDoc = new TrainingNeedRequests
                        {
                            Code = (string)config["Code"],
                            Financial_Year = (string)config["Financial_Year"],
                            Duty_Station = (string)config["Duty_Station"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Department = (string)config["Department"],
                            Job_Title = (string)config["Job_Title"],
                            Disabled = (bool)config["Disabled"],
                            Description = (string)config["Description"],
                            Employment_Date = (string)config["Employment_Date"],
                            Training_Plan_No = (string)config["Training_Plan_No"],
                            Status = (string)config["Status"],
                            Estimate_Cost = (int)config["Estimate_Cost"],
                            Created_By = (string)config["Created_By"],
                            Created_On = (DateTime)config["Created_On"],
                            Submitted = (bool)config["Submitted"],
                            Course_ID = (string)config["Course_ID"],
                            Course_Description = (string)config["Course_Description"],
                            Source = (string)config["Source"],
                            Comments = (string)config["Comments"]

                        };
                    }
                }

                /*  TrainingNeedDoc.Department = employee.Department_Name;
                  TrainingNeedDoc.Duty_Station = employee.GlobalDimension2Code;*/

                return View(TrainingNeedDoc);
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



        public PartialViewResult TrainingNeedsLines(string trainingHeaderNo, string status)
        {
            ViewBag.Status = status;
            var employee = Session["EmployeeData"] as EmployeeView;

            var rsrceReq = $"TrainingNeedsLines?$filter=Document_No eq '{trainingHeaderNo}'&$format=json";

            var httpResponse = Credentials.GetOdataData(rsrceReq);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var odataResponse = JsonConvert.DeserializeObject<ODataResponse<TrainingNeedsLines>>(result);
            var trainingNeedsLines = odataResponse?.Value ?? [];

            return PartialView("PartialViews/TrainingNeedsLines", trainingNeedsLines);
        }
        public ActionResult NewTrainingLine(string docNo, string empNo = "")
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var employee = Session["EmployeeData"] as EmployeeView;
                var trainingLine = new TrainingLineViewModel();
                Session["httpResponse"] = null;

                trainingLine.DocumentNo = docNo;


                // Get Training Domains List
                var domainsList = new List<DropdownList>();
                var pageDomains = "TrainingDomains?$format=json";
                var httpResponseDomains = Credentials.GetOdataData(pageDomains);
                using (var streamReader = new StreamReader(httpResponseDomains.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    domainsList.AddRange(from JObject config in details["value"] select new DropdownList { Text = $"{(string)config["Code"]} - {(string)config["Description"]}", Value = (string)config["Code"] });
                }

                // Get Course Providers List
                var providersList = new List<DropdownList>();
                var pageProviders = "vendorlist?$filter=Trainer eq true&$format=json";
                var httpResponseProviders = Credentials.GetOdataData(pageProviders);
                using (var streamReader = new StreamReader(httpResponseProviders.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    providersList.AddRange(from JObject config in details["value"] select new DropdownList { Text = $"{(string)config["Name"]}", Value = (string)config["No"] });
                }

                // Get Courses List  
                var coursesList = new List<DropdownList>();
                var pageCourses = "CourseList?$filter=Blocked eq false&$format=json";
                var httpResponseCourses = Credentials.GetOdataData(pageCourses);
                using (var streamReader = new StreamReader(httpResponseCourses.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    coursesList.AddRange(from JObject config in details["value"] select new DropdownList { Text = $"{(string)config["Code"]} - {(string)config["Descritpion"]}", Value = (string)config["Code"] });
                }

                trainingLine.ListOfDomains = domainsList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                trainingLine.ListOfProviders = providersList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                trainingLine.ListOfCourses = coursesList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                return PartialView("PartialViews/NewTrainingLine", trainingLine);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitTrainingLine(TrainingLine trainingLine)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;

                if (trainingLine.Points == null)
                {
                    trainingLine.Points = 0;
                }

                if (trainingLine.CourseProvider == null)
                {
                    trainingLine.CourseProvider = "";
                }

                var success = false;

                Credentials.ObjNav.FnInsertTrainingLines(
                    trainingLine.DocNo,
                    employee?.No,
                    trainingLine.CourseID,
                    trainingLine.CourseDescription,
                    trainingLine.DomainId,
                    Convert.ToInt32(trainingLine.Points),
                    trainingLine.CourseProvider,
                    trainingLine.StartDate,
                    trainingLine.EndDate
                );

                return Json(new { message = "Training line submitted successfully", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteTrainingLine(string code, string lineNo)
        {
            try
            {
                Credentials.ObjNav.DeleteTrainingNeedLine(code, Convert.ToInt32(lineNo));


                return Json(new { message = "Training line deleted successfully", success = true });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TrainingCostLines(string code, string course)
        {

            try
            {
                var rsrceReq = $"TrainingCost?$filter=Training_ID eq '{code}' and Course_ID eq '{course}' &$format=json";
                var httpResponse = Credentials.GetOdataData(rsrceReq);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var odataResponse = JsonConvert.DeserializeObject<ODataResponse<TrainingCost>>(result);
                var trainingCosts = odataResponse?.Value ?? [];

                ViewBag.activeCode = code;
                ViewBag.activeCourse = course;
                return View("PartialViews/TrainingCosts", trainingCosts);
            }
            catch (Exception ex)
            {

                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult NewTrainingCostLine(string docNo, string activeCourse, string courseId = "")
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var trainingCost = new TrainingCostViewModel();

                trainingCost.DocumentNo = docNo;

                var costCategoriesList = new List<DropdownList>
            {
                new DropdownList { Text = "Procurable", Value = "0" },
                new DropdownList { Text = "Other Costs", Value = "1" }
            };
                var procurableItemsList = new List<DropdownList>();
                var pageProcurable = "HRModelsList?$filter=Type eq 'Training Item Cost'&$format=json";
                var httpResponseProcurable = Credentials.GetOdataData(pageProcurable);
                using (var streamReader = new StreamReader(httpResponseProcurable.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    procurableItemsList.AddRange(from JObject config in details["value"] select new DropdownList { Text = $"{(string)config["Code"]} - {(string)config["Item_Description"]}", Value = (string)config["Item_Code"] });
                }


                ViewBag.Course = courseId;
                trainingCost.CourseID = courseId;
                ViewBag.activeCourse = activeCourse;

                ViewBag.DsaUnitCost = getDSAUnitCost(trainingCost);

                trainingCost.ListOfCostCategories = costCategoriesList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                trainingCost.ListOfOtherCostItems = procurableItemsList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                return PartialView("PartialViews/NewTrainingCostLine", trainingCost);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitTrainingCost(TrainingCost trainingCost)
        {
            try
            {
                var res = Credentials.ObjNav.addTrainingCost(
                    trainingCost.Training_ID,
                    Convert.ToInt32(trainingCost.Cost_Category),
                    trainingCost.Cost_Item,
                    trainingCost.Unit_Cost_LCY,
                    trainingCost.Quantity
                );

                if (res != "")
                {
                    return Json(new { message = "Training Cost Submitted Successfully!", success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Error adding record. Try again", success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteTrainingCost(string docNo, string courseID, int entryNo)
        {
            try
            {
                Credentials.ObjNav.RemoveTrainingCost(docNo, entryNo);

                return Json(new { message = "Training cost deleted successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }





        public PartialViewResult TrainingEvaluationListPartialView()
        {
            var employee = Session["EmployeeData"] as EmployeeView;
            var userId = employee?.UserID;
            var StaffNo = Session["Username"].ToString();

            var page = $"TrainingAssessment?$filter=Employee_No eq '{StaffNo}'&$orderby=No desc&$format=json";


            //var page = $"TrainingAssessment";
            var httpResponse = Credentials.GetOdataData(page);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var odataResponse = JsonConvert.DeserializeObject<ODataResponse<TrainingEvaluation>>(result);
            var trainingEvaluations = odataResponse?.Value ?? [];

            return PartialView("PartialViews/TrainingEvaluationListPartialView", trainingEvaluations);
        }
        public ActionResult TrainingEvaluationDocumentView2(string docNo)
        {
            var rsrceReq = $"TrainingAssessment?$filter=No eq '{docNo}'&$format=json";
            var httpResponse = Credentials.GetOdataData(rsrceReq);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var odataResponse = JsonConvert.DeserializeObject<ODataResponse<TrainingEvaluation>>(result);
            var trainingEvaluations = odataResponse?.Value?.FirstOrDefault();
            return View("TrainingEvaluationDocumentView", trainingEvaluations);
        }

        public ActionResult TrainingEvaluationDocumentView(string docNo)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");
                var employee = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();
                var TrainingEval = new TrainingEvaluation();
                var page = $"TrainingAssessment?$filter=No eq '{docNo}'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        TrainingEval = new TrainingEvaluation
                        {
                            No = (string)config["No"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Department = (string)config["Department"],
                            Job_Title = (string)config["Job_Title"],
                            Application_Code = (string)config["Application_Code"],
                            Course_Title = (string)config["Course_Title"],
                            Start_DateTime = DateTime.Parse((string)config["Start_DateTime"]),
                            End_DateTime = DateTime.Parse((string)config["End_DateTime"]),
                            Venue = (string)config["Venue"],
                            No_of_Participants = Convert.ToInt32(config["No_of_Participants"]),
                            Created_By = (string)config["Created_By"],
                            Created_On = DateTime.Parse((string)config["Created_On"]),
                            HOD_Remarks = (string)config["HOD_Remarks"],
                            HR_Remarks = (string)config["HR_Remarks"],
                            Status = (string)config["Status"]
                        };
                    }
                }
                return View(TrainingEval);
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







        public JsonResult GenerateEvaluationReport(string DocNo)
        {
            try
            {
                string staffNo = Session["Username"].ToString();
                string message = "";
                bool success = false, view = false;

                //message = Credentials.ObjNav.fnGenerateTrainingEvaluationReport(DocNo);
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
        public JsonResult GetCoursesByDomain(string domainId)
        {
            try
            {
                var coursesList = new List<DropdownList>();
                var pageCourses = $"CourseList?$filter=Domain eq '{domainId}'&$format=json";
                var httpResponse = Credentials.GetOdataData(pageCourses);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    coursesList.AddRange(from JObject config in details["value"] select new DropdownList { Text = $"{(string)config["Code"]} - {(string)config["Descritpion"]}", Value = (string)config["Code"] });
                }

                var selectListItems = coursesList.Select(x => new { Value = x.Value, Text = x.Text }).ToList();

                return Json(new { success = true, courses = selectListItems }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message.Replace("'", "") }, JsonRequestBehavior.AllowGet);
            }
        }







        public PartialViewResult NewTrainingEvaluation()
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var trainingEvaluation = new TrainingEvaluationViewModel();
                var StaffNo = Session["Username"].ToString();

                //trainingLine.DocumentNo = docNo;


                // Get Training Domains List
                var trainigApplicationList = new List<DropdownList>();
                var trainingApplication = $"TrainingRequisition?$filter=Employee_No eq '{StaffNo}' and Status eq 'Approved'&$format=json";
                //var trainingApplication = $"TrainingApplications";
                var httpResponseDomains = Credentials.GetOdataData(trainingApplication);
                using (var streamReader = new StreamReader(httpResponseDomains.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    trainigApplicationList.AddRange(from JObject config in details["value"] select new DropdownList { Text = $"{(string)config["Code"]} - {(string)config["Description"]}", Value = (string)config["Code"] });
                }
                trainingEvaluation.trainigApplicationList = trainigApplicationList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                return PartialView("PartialViews/NewTrainingEvaluation", trainingEvaluation);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult CreateTrainingEvaluationRequest(string ApplicationCode)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                //var loggedInUser = employee?.UserID;
                var staffNo = Session["Username"].ToString();

                if (string.IsNullOrEmpty(staffNo))
                {
                    return Json(new { message = "User session expired. Please login again.", success = false },
                        JsonRequestBehavior.AllowGet);
                }

                var docNo = "";
                //Credentials.ObjNav.createNewTrainingRequest(staffNo, ApplicationCode);

                return Json(new { message = docNo, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        private decimal getDSAUnitCost(TrainingCostViewModel trainingCostViewModel)
        {
            try
            {
                var staffNo = Session["Username"].ToString();
                decimal unitCost = 0;
                //Credentials.ObjNav.SetDSAUnitCost(staffNo, trainingCostViewModel.DocumentNo, trainingCostViewModel.CourseID);
                return unitCost;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching DSA Unit Cost: " + ex.Message);
            }
        }





        // Training Requisition
        public ActionResult TrainingRequisitionList()
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
        public PartialViewResult TrainingRequisitionListPartialView()
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                var TrainingReqList = new List<TrainingRequisition>();

                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = $"TrainingRequisition?$filter=Created_By eq '{StaffNo}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var reqList = new TrainingRequisition
                        {
                            Code = (string)config["Code"],
                            Training_Plan_No = (string)config["Training_Plan_No"],
                            Employee_Department = (string)config["Employee_Department"],
                            Course_Title = (string)config["Course_Title"],
                            Description = (string)config["Description"],
                            Start_DateTime = (string)config["Start_DateTime"],
                            End_DateTime = (string)config["End_DateTime"],
                            Duration = (int?)config["Duration"] ?? 0,
                            Duration_Type = (string)config["Duration_Type"],
                            Training_Region = (string)config["Training_Region"],
                            Cost = (int?)config["Cost"] ?? 0,
                            Training_Venue_Region_Code = (string)config["Training_Venue_Region_Code"],
                            Training_Venue_Region = (string)config["Training_Venue_Region"],
                            Training_Responsibility_Code = (string)config["Training_Responsibility_Code"],
                            Training_Responsibility = (string)config["Training_Responsibility"],
                            Location = (string)config["Location"],
                            Provider = (string)config["Provider"],
                            Provider_Name = (string)config["Provider_Name"],
                            Training_Type = (string)config["Training_Type"],
                            Training_Duration_Hrs = (int?)config["Training_Duration_Hrs"] ?? 0,
                            Planned_Budget = (int?)config["Planned_Budget"] ?? 0,
                            Planned_No_to_be_Trained = (int?)config["Planned_No_to_be_Trained"] ?? 0,
                            No_of_Participants = (int?)config["No_of_Participants"] ?? 0,
                            Total_Procurement_Cost = (int?)config["Total_Procurement_Cost"] ?? 0,
                            Other_Costs = (int?)config["Other_Costs"] ?? 0,
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Created_By = (string)config["Created_By"],
                            Created_On = (string)config["Created_On"],
                            Status = (string)config["Status"],
                            Bonded = (bool?)config["Bonded"] ?? false,
                            Bonding_Period = (string)config["Bonding_Period"],
                            Expected_Bond_End = (string)config["Expected_Bond_End"],
                            Expected_Bond_Start = (string)config["Expected_Bond_Start"],
                            Bond_Penalty = (int?)config["Bond_Penalty"] ?? 0
                        };

                        TrainingReqList.Add(reqList);
                    }
                }

                return PartialView("~/Views/TRaining/PartialViews/TrainingRequisitionListPartialView.cshtml", TrainingReqList.OrderByDescending(x => x.Code));
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
        public ActionResult NewTrainingRequisition()
        {
            try
            {

                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                var currentTrainingPlan = GetCurrentTrainingPlan();

                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var trainingRequisition = new TrainingRequisition();

                #region TrainingPlanNo
                var TrainingPlanList = new List<DropdownList>();
                var pageTPN = $"ApprovedTrainingPlans?$filter=No eq '{currentTrainingPlan}'&$format=json";
                var httpResponseTPN = Credentials.GetOdataData(pageTPN);
                using (var streamReader = new StreamReader(httpResponseTPN.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Description"] + "-" + (string)config1["No"],
                            Value = (string)config1["No"]
                        };
                        TrainingPlanList.Add(dropdownList);
                    }
                }
                #endregion

                #region ResponsibilityCenters
                var ResponsibilityCentersList = new List<DropdownList>();
                var pageResponsibilityCenters = $"ResponsibilityCenters?$format=json";
                var httpResponseResponsibilityCenters = Credentials.GetOdataData(pageResponsibilityCenters);
                using (var streamReader = new StreamReader(httpResponseResponsibilityCenters.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["Code"]}",
                            Value = (string)config1["Code"]
                        };
                        ResponsibilityCentersList.Add(dropdownList);
                    }
                }
                #endregion

                #region TrainingCourses
                var TrainingCoursesList = new List<DropdownList>();
                var pageTrainingCourses = $"TrainingPlanLines?$filter=Training_Plan_Code eq '{currentTrainingPlan}' and Employee_No eq '{StaffNo}' and Status eq 'Approved'&$format=json";
                var httpTrainingCourses = Credentials.GetOdataData(pageTrainingCourses);
                using (var streamReader = new StreamReader(httpTrainingCourses.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Course_Description"] + "-" + (string)config1["Course_Title"],
                            Value = (string)config1["Course_Title"]
                        };
                        TrainingCoursesList.Add(dropdownList);
                    }
                }
                #endregion

                #region Locations
                var LocationsList = new List<DropdownList>();
                var pageLocations = $"Locations?$format=json";
                var httpLocations = Credentials.GetOdataData(pageLocations);
                using (var streamReader = new StreamReader(httpLocations.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Code"] + "" + (string)config1["Name"],
                            Value = (string)config1["Code"]
                        };
                        LocationsList.Add(dropdownList);
                    }
                }
                #endregion

                #region Region
                var RegionsList = new List<DropdownList>();
                var pageRegions = $"DimensionValueList?$format=json";
                var httpResponseRegions = Credentials.GetOdataData(pageRegions);
                using (var streamReader = new StreamReader(httpResponseRegions.GetResponseStream()))
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
                        RegionsList.Add(dropdownList);
                    }
                }
                #endregion

                #region Provider
                var ProviderList = new List<DropdownList>();
                var pageProvider = $"ProcurementVendors?$format=json";
                var httpResponseProvider = Credentials.GetOdataData(pageProvider);
                using (var streamReader = new StreamReader(httpResponseProvider.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Name"] + " - " + (string)config1["No"],
                            Value = (string)config1["No"]
                        };
                        ProviderList.Add(dropdownList);
                    }
                }
                #endregion



                trainingRequisition.ListOfTrainingPlan = TrainingPlanList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();

                trainingRequisition.ListOfResponsibilityCenters = ResponsibilityCentersList.Select(x =>
                 new SelectListItem
                 {
                     Text = x.Text,
                     Value = x.Value
                 }).ToList();

                trainingRequisition.ListOfTrainingCourses = TrainingCoursesList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

                trainingRequisition.ListOfLocations = LocationsList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

                trainingRequisition.ListOfRegions = RegionsList.Select(x =>
                   new SelectListItem
                   {
                       Text = x.Text,
                       Value = x.Value
                   }).ToList();

                trainingRequisition.ListOfProviders = ProviderList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();



                return PartialView("~/Views/Training/PartialViews/NewTrainingRequisition.cshtml",
                    trainingRequisition);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitTrainingRequisition(TrainingRequisition trainingRequisition)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                string res2 = "";
                /* res2 = Credentials.ObjNav.createTrainingRequisition(
                    trainingRequisition.Training_Plan_No,
                    trainingRequisition.Course_Title,
                    trainingRequisition.Training_Venue_Region_Code,
                    trainingRequisition.Training_Responsibility_Code,
                    trainingRequisition.Location,
                    trainingRequisition.Provider,
                    StaffNo,
                    trainingRequisition.Description
                    );*/

                res2 = Credentials.ObjNav.FnCreateTrainingRequisition(
                    StaffNo,
                    StaffNo,
                    trainingRequisition.Course_Title,
                    ""
                    );

                if (res2 != "")
                {
                    var redirect = res2;

                    return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var redirect = "Error adding record. Try again";
                    return Json(new { message = redirect, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult TrainingRequisitionDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null) return RedirectToAction("Login", "Login");
                var employeeView = Session["EmployeeData"] as EmployeeView;

                var StaffNo = Session["Username"].ToString();
                var TrainingReqDoc = new TrainingRequisition();
                var page = "TrainingRequisition?$filter=Code eq '" + DocNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        TrainingReqDoc = new TrainingRequisition
                        {
                            Code = (string)config["Code"],
                            Training_Plan_No = (string)config["Training_Plan_No"],
                            Employee_Department = (string)config["Employee_Department"],
                            Course_Title = (string)config["Course_Title"],
                            Description = (string)config["Description"],
                            Start_DateTime = (string)config["Start_DateTime"],
                            End_DateTime = (string)config["End_DateTime"],
                            Duration = (int?)config["Duration"] ?? 0,
                            Duration_Type = (string)config["Duration_Type"],
                            Training_Region = (string)config["Training_Region"],
                            Cost = (int?)config["Cost"] ?? 0,
                            Training_Venue_Region_Code = (string)config["Training_Venue_Region_Code"],
                            Training_Venue_Region = (string)config["Training_Venue_Region"],
                            Training_Responsibility_Code = (string)config["Training_Responsibility_Code"],
                            Training_Responsibility = (string)config["Training_Responsibility"],
                            Location = (string)config["Location"],
                            Provider = (string)config["Provider"],
                            Provider_Name = (string)config["Provider_Name"],
                            Training_Type = (string)config["Training_Type"],
                            Training_Duration_Hrs = (int?)config["Training_Duration_Hrs"] ?? 0,
                            Planned_Budget = (int?)config["Planned_Budget"] ?? 0,
                            Planned_No_to_be_Trained = (int?)config["Planned_No_to_be_Trained"] ?? 0,
                            No_of_Participants = (int?)config["No_of_Participants"] ?? 0,
                            Total_Procurement_Cost = (int?)config["Total_Procurement_Cost"] ?? 0,
                            Other_Costs = (int?)config["Other_Costs"] ?? 0,
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Created_By = (string)config["Created_By"],
                            Created_On = (string)config["Created_On"],
                            Status = (string)config["Status"],
                            Bonded = (bool?)config["Bonded"] ?? false,
                            Bonding_Period = (string)config["Bonding_Period"],
                            Expected_Bond_End = (string)config["Expected_Bond_End"],
                            Expected_Bond_Start = (string)config["Expected_Bond_Start"],
                            Bond_Penalty = (int?)config["Bond_Penalty"] ?? 0
                        };
                    }
                }
                TrainingReqDoc.Employee_Department = employeeView.GlobalDimension2Code;
                return View(TrainingReqDoc);
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
        public PartialViewResult TrainingRequisitionLinesPartialView(string DocNo, string status, string Training_Venue_Region_Code)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var TrainingParticipantsLines = new List<TrainingParticipantsList>();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = $"HRTrainingPartcipant?$filter=Training_Code eq '{DocNo}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var TrainingParticipantsLine = new TrainingParticipantsList
                        {
                            Line_No = (int)(config["Line_No"] ?? 0),
                            Training_Code = (string)(config["Training_Code"] ?? ""),
                            Employee_Code = (string)(config["Employee_Code"] ?? ""),
                            Training_Responsibility_Code = (string)(config["Training_Responsibility_Code"] ?? ""),
                            Employee_Name = (string)(config["Employee_Name"] ?? ""),
                            Type = (string)(config["Type"] ?? ""),
                            Destination = (string)(config["Destination"] ?? ""),
                            No_of_Days = (int)(config["No_of_Days"] ?? 0),
                            Total_Amount = (int)(config["Total_Amount"] ?? 0),
                            Training_Responsibility = (string)(config["Training_Responsibility"] ?? ""),
                            Global_Dimension_1_Code = (string)(config["Global_Dimension_1_Code"] ?? ""),
                            Witness = (string)(config["Witness"] ?? ""),
                            Witness_Name = (string)(config["Witness_Name"] ?? ""),
                            Charge_Levy = (bool)(config["Charge_Levy"] ?? false),
                            Requestor = (string)(config["Requestor"] ?? "")
                        };

                        TrainingParticipantsLines.Add(TrainingParticipantsLine);
                    }
                }
                ViewBag.DocNo = DocNo;
                ViewBag.TrainingVenue = Training_Venue_Region_Code;
                ViewBag.status = status;
                return PartialView("~/Views/Training/PartialViews/TrainingRequisitionLinesPartialView.cshtml", TrainingParticipantsLines.OrderByDescending(x => x.Line_No));
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
        public ActionResult NewTrainingRequisitionLine(string docNo, string TrainingVenue)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var trainingReqLine = new TrainingParticipantsList();
                trainingReqLine.Destination = TrainingVenue;


                #region Employees
                var EmpleyeeList = new List<DropdownList>();
                var pageWp = $"EmployeeList?$format=json";
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
                            Text = (string)config1["First_Name"] + " " + (string)config1["Last_Name"] + " " + (string)config1["First_Name"] + " (" + (string)config1["No"] + ")",
                            Value = (string)config1["No"]
                        };
                        EmpleyeeList.Add(dropdownList);
                    }
                }
                #endregion

                #region TypeOfExpense
                var ExpenseTypeList = new List<DropdownList>();
                var pageExpenseType = $"ReceiptsandPaymentTypes?$format=json";
                var httpResponseExpenseType = Credentials.GetOdataData(pageExpenseType);
                using (var streamReader = new StreamReader(httpResponseExpenseType.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["Code"]} - {(string)config1["Description"]}",
                            Value = (string)config1["Code"]
                        };
                        ExpenseTypeList.Add(dropdownList);
                    }
                }
                #endregion

                #region Region
                var RegionsList = new List<DropdownList>();
                var pageRegions = $"DimensionValueList?$format=json";
                var httpResponseRegions = Credentials.GetOdataData(pageRegions);
                using (var streamReader = new StreamReader(httpResponseRegions.GetResponseStream()))
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
                        RegionsList.Add(dropdownList);
                    }
                }
                #endregion

                trainingReqLine.ListOfEmployees = EmpleyeeList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                trainingReqLine.ListOfExpenseTypes = ExpenseTypeList.Select(x =>
                   new SelectListItem
                   {
                       Text = x.Text,
                       Value = x.Value
                   }).ToList();

                trainingReqLine.ListOfRegions = RegionsList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();

                return PartialView("~/Views/Training/PartialViews/NewTrainingRequisitionLine.cshtml",
                    trainingReqLine);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitTrainingRequisitionLine(TrainingParticipantsList trainingParticipant)
        {
            try
            {
                var docNo = trainingParticipant.Training_Code;
                bool res = false;
                string res1 = Credentials.ObjNav.addTrainingParticipants(
                    trainingParticipant.Type,
                    trainingParticipant.Employee_Code,
                    trainingParticipant.Destination, //not needed
                    trainingParticipant.No_of_Days,  //not needed
                    docNo
                 );


                /*  if (res)
                  {*/
                var redirect = docNo;

                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
                /*}
                else
                {
                    var redirect = "Error adding record. Try again";
                    return Json(new { message = redirect, success = false }, JsonRequestBehavior.AllowGet);
                }*/

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SendTrainingReqDocForApproval(string DocNo)
        {
            try
            {

                var employee = Session["EmployeeData"] as EmployeeView;
                var staffNo = employee?.No;
                var userId = employee?.UserID;
                bool res = false;
                var res2 = "";
                res2 = Credentials.ObjNav.sendTrainingRequestApproval(staffNo, DocNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(57000, DocNo, employee?.UserID);

                if (res)
                {
                    var redirect = res;

                    return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var redirect = "Error adding record. Try again";
                    return Json(new { message = redirect, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancelTrainingReqApproval(string DocNo)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var staffNo = employee.No;
                /* Credentials.ObjNav.cancelImprestApprovalRequest(staffNo, DocNo);*/
                return Json(new { message = "Training requisition approval cancelled Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CoursesJSon(string Training_Plan_No)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var staffNo = employee.No;
                List<object> courses = new List<object>();

                string page = $"TrainingPlanLines?$filter=Training_Plan_Code eq '{Training_Plan_No}' and Employee_No eq '{staffNo}' and Status eq 'Approved'&format=json";

                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        courses.Add(new
                        {
                            Course_Title = (string)config["Course_Title"],
                            Course_Description = (string)config["Course_Description"]
                        });
                    }
                }

                return Json(courses, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        ///Training Assessment
        public ActionResult TrainingEvaluationRequisitionList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }
        public PartialViewResult TrainingEvaluationRequisitionListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var UserID = Session["UserID"].ToString();
            var trainingApplications = new List<TrainingApplications>();

            var page = "HRTrainingApplication?$filter=Created_By eq '" + UserID + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var training = new TrainingApplications();
                    training.Code = (string)config["Code"];
                    training.CourseTitle = (string)config["Course_Title"];
                    training.StartDateTime = (string)config["Start_DateTime"];
                    training.EndDateTime = (string)config["End_DateTime"];
                    training.Status = (string)config["Status"];
                    training.Duration = (int)config["Duration"];
                    training.Cost = (decimal)config["Cost"];
                    training.Location = (string)config["Location"];
                    training.Description = (string)config["Description"];
                    training.Year = (string)config["Year"];
                    training.Provider = (string)config["Provider"];
                    training.EmployeeNo = (string)config["Employee_No"];
                    training.ApplicationDate = (string)config["Application_Date"];
                    training.NoSeries = (string)config["No_Series"];
                    training.EmployeeDepartment = (string)config["Employee_Department"];
                    training.EmployeeName = (string)config["Employee_Name"];
                    training.ProviderName = (string)config["Provider_Name"];
                    training.NoOfParticipants = (int)config["No_of_Participants"];
                    training.ApprovedCost = (decimal)config["Approved_Cost"];
                    training.GlobalDimension1Code = (string)config["Global_Dimension_1_Code"];
                    training.GlobalDimension2Code = (string)config["Global_Dimension_2_Code"];
                    training.ActualStartDate = (string)config["Actual_Start_Date"];
                    training.ActualEndDate = (string)config["Actual_End_Date"];
                    training.EstimatedCost = (decimal)config["Estimated_Cost"];
                    training.ImprestCreated = (bool)config["Imprest_Created"];
                    training.TrainingPlanCost = (decimal)config["Training_Plan_Cost"];
                    training.Budget = (decimal)config["Budget"];
                    training.Actual = (decimal)config["Actual"];
                    training.Commitment = (decimal)config["Commitment"];
                    training.GLAccount = (string)config["GL_Account"];
                    training.BudgetName = (string)config["Budget_Name"];
                    training.AvailableFunds = (decimal)config["Available_Funds"];
                    training.Local = (string)config["Local"];
                    training.RequiresFlight = (bool)config["Requires_Flight"];
                    training.SupervisorNo = (string)config["Supervisor_No"];
                    training.SupervisorName = (string)config["Supervisor_Name"];
                    training.TrainingPlanNo = (string)config["Training_Plan_No"];
                    training.TrainingVenueRegionCode = (string)config["Training_Venue_Region_Code"];
                    trainingApplications.Add(training);
                }
            }

            return PartialView("~/Views/Training/Partial Views/TraningEvaluationList.cshtml", trainingApplications);
        }
        public PartialViewResult NewTrainingAssessmentRequest()
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var trainingAssessment = new TrainingAssessment();
                var StaffNo = Session["Username"].ToString();


                #region Application_Code
                List<DropdownList> ApplicationCodeList = new List<DropdownList>();
                //var pageA = $"TrainingApplications?$filter=Employee_No eq '{StaffNo}' and Status eq 'Approved'&$format=json";
                var pageAC = $"TrainingApplications?$filter=Employee_No eq '{StaffNo}'&$format=json";
                HttpWebResponse httpResponsePageAC = Credentials.GetOdataData(pageAC);
                using (var streamReader = new StreamReader(httpResponsePageAC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList3 = new DropdownList();
                        dropdownList3.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                        dropdownList3.Value = (string)config["Code"];
                        ApplicationCodeList.Add(dropdownList3);
                    }
                }
                #endregion




                trainingAssessment.ListOfApplicationCodes = ApplicationCodeList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                return PartialView("PartialViews/NewTrainingAssessmentRequest", trainingAssessment);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitTrainingAssessment(string Application_Code, string No_of_Participants)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                string userId = Session["UserId"].ToString();
                string empNo = employee.No;

                var docNo = Credentials.ObjNav.FnCreateTrainingFeedback(empNo, Application_Code, int.Parse(No_of_Participants));

                if (docNo != "")
                {
                    return Json(new { message = docNo, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Error creating training assessment document. Try again", success = false }, JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult TrainingEvaluationLines(string trainingHeaderNo, string status)
        {
            ViewBag.Status = status;
            var employee = Session["EmployeeData"] as EmployeeView;

            var rsrceReq = $"SelfTrainingImpactAssessment?$filter=Training_Evaluation_No eq '{trainingHeaderNo}'&$format=json";

            var httpResponse = Credentials.GetOdataData(rsrceReq);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var odataResponse = JsonConvert.DeserializeObject<ODataResponse<TrainingEvaluationLine>>(result);
            var trainingEvaluationLines = odataResponse?.Value ?? [];

            return PartialView("PartialViews/TrainingEvaluationLines", trainingEvaluationLines);
        }
        public ActionResult NewTrainingAssessmentLine(string docNo, string empNo = "")
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var trainingEvaluation = new TrainingEvaluationLine();
                trainingEvaluation.Training_Evaluation_No = docNo;

                // Get Training Categories List
                var categoriesList = new List<DropdownList>();
                var pageCategories = "EvaluationCategorySetup?$filter=Rating_Category eq 'Self'&$format=json";
                var httpResponseDomains = Credentials.GetOdataData(pageCategories);
                using (var streamReader = new StreamReader(httpResponseDomains.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    categoriesList.AddRange(from JObject config in details["value"] select new DropdownList { Text = $"{(string)config["Code"]} - {(string)config["Description"]}", Value = (string)config["Code"] });
                }

                trainingEvaluation.ListOfCategories = categoriesList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                return PartialView("PartialViews/NewTrainingEvaluationLine", trainingEvaluation);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitTrainingAssessmentLine(TrainingEvaluationLine trainingEvaluationLine)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.InsertSelfAssessmentLine(
                    trainingEvaluationLine.Training_Evaluation_No, trainingEvaluationLine.Training_Category, trainingEvaluationLine.Comments
                );

                return Json(new { message = "Training evaluation submitted successfully", success = true });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SendTrainingRequestForApproval(string Code)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;

                if (string.IsNullOrEmpty(Code))
                {
                    return Json(new { message = "Document Number is required", success = false },
                        JsonRequestBehavior.AllowGet);
                }

                var success = Credentials.ObjNav.sendTrainingRequestApproval(employee?.No, Code);

                return Json(new { message = "Training request sent for approval successfully", success = true },
                    JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancelTrainingRequestApproval(string docNo)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                if (string.IsNullOrEmpty(docNo))
                {
                    return Json(new { message = "Document Number is required", success = false },
                        JsonRequestBehavior.AllowGet);
                }

                Credentials.ObjNav.cancelTrainingRequestApproval(employee?.No, docNo);

                return Json(new { message = "Training request approval cancelled successfully", success = true },
                    JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult GetCourseDetails(string courseId)
        {
            try
            {
                if (string.IsNullOrEmpty(courseId))
                {
                    return Json(new { success = false, message = "Course ID is required" }, JsonRequestBehavior.AllowGet);
                }

                var pageCourse = $"TrainingCourses?$filter=Course_ID eq '{courseId}'&$format=json";
                var httpResponse = Credentials.GetOdataData(pageCourse);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var courseData = details["value"].FirstOrDefault();

                if (courseData == null)
                {
                    return Json(new { success = false, message = "Course not found" }, JsonRequestBehavior.AllowGet);
                }

                var config = (JObject)courseData;
                var courseDetails = new
                {
                    CourseId = (string)config["Course_ID"],
                    Description = (string)config["Course_Description"],
                    DomainId = (string)config["Domain_Code"],
                    DomainName = (string)config["Domain_Name"],
                    ProviderId = (string)config["Provider_Code"],
                    ProviderName = (string)config["Provider_Name"],
                    Duration = (int?)config["Duration"] ?? 1,
                    DurationType = (string)config["Duration_Type"] ?? "Days",
                    TrainingType = (string)config["Training_Type"] ?? "External",
                    EstimatedCost = (decimal?)config["Estimated_Cost"] ?? 0,
                    StartDate = config["Start_Date"] != null ? DateTime.Parse((string)config["Start_Date"]).ToString("dd/MM/yyyy") : "",
                    EndDate = config["End_Date"] != null ? DateTime.Parse((string)config["End_Date"]).ToString("dd/MM/yyyy") : "",
                    Location = (string)config["Location"] ?? ""
                };

                return Json(new { success = true, course = courseDetails }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message.Replace("'", "") }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SubmitTrainingNeed(string docNo)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.FnSubmitTrainingNeeds(docNo);

                return Json(new { message = "Training needs submitted sucessfully!", success = true },
                    JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SubmitTrainingEvaluation(string docNo)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;

                //Credentials.ObjNav.SubmitTrainingEvaluation(docNo);

                return Json(new { message = "Training evaluation submitted sucessfully!", success = true },
                    JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CreateTrainingRequisition(TrainingRequisitionCreate requisition)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;
                var loggedInUser = employee?.UserID;

                if (string.IsNullOrEmpty(loggedInUser))
                {
                    return Json(new { message = "User session expired. Please login again.", success = false },
                        JsonRequestBehavior.AllowGet);
                }

                if (string.IsNullOrEmpty(requisition.CourseID))
                {
                    return Json(new { message = "Course ID is required", success = false },
                        JsonRequestBehavior.AllowGet);
                }

                var documentNumber = "";
                /* documentNumber= Credentials.ObjNav.FnCreateTrainingRequisition(
                  employee.No,
                  loggedInUser,
                  requisition.CourseID
              );*/

                return Json(new { message = documentNumber, success = true }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        //HumanResourcesSetup
        //Current_Training_Plan

        private string GetCurrentTrainingPlan()
        {
            var pageCourse = $"HumanResourcesSetup?$format=json";
            var httpResponse = Credentials.GetOdataData(pageCourse);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);
            var courseData = details["value"].FirstOrDefault();


            return courseData["Current_Training_Plan"]?.ToString();
        }

    }
}