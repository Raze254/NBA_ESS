using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Latest_Staff_Portal.Controllers;

[CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]

public class PerformanceController : Controller
{
    //*******************************************************************************************
    //PMMU
    //********************************************************************************************
    public ActionResult PMMU()
    {
        if (Session["Username"] == null)
        {
            return RedirectToAction("Login", "Login");
        }
        else
        {
            PMMUCard pmmu = new PMMUCard();
            #region GeneralSetup
            string pageGenSetup = "GeneralSetup?$format=json";
            HttpWebResponse httpResponseGenSetup = Credentials.GetOdataData(pageGenSetup);
            using (var streamReader = new StreamReader(httpResponseGenSetup.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    pmmu.PMMU_Document_Stage = (string)config["PMMU_Document_Stage"];

                }
            }
            #endregion

            return View(pmmu);
        }
    }
    public PartialViewResult PMMUList()
    {
        EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
        string staffName = employeeView.FirstName + " " + employeeView.LastName;
        string StaffNo = Session["Username"].ToString();

        List<PMMUCard> PMMUCardList = new List<PMMUCard>();

        // First API call to fetch Implementation Years
        string pageIY = "STAJList?$filter=Implementation_Status eq 'Ongoing'&$format=json";
        ImplementationYears implementationYear = new ImplementationYears();

        try
        {
            HttpWebResponse httpResponseIY = Credentials.GetOdataData(pageIY);
            using var streamReader = new StreamReader(httpResponseIY.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);

            foreach (JObject config in details["value"])
            {
                // Parse the Start_Date into DateTime
                DateTime startDate = DateTime.Parse((string)config["Start_Date"]);

                // Calculate June 30 of the following year
                DateTime endDate = new DateTime(startDate.Year + 1, 6, 30);  // 6 = June, day 30

                // Now assign the implementationYear
                implementationYear = new ImplementationYears
                {
                    Description = (string)config["Description"],
                    Start_Date = (string)config["Start_Date"],   // You can store as string or DateTime if needed
                    End_Date = endDate.ToString("yyyy-MM-dd"),   // Format as string in 'YYYY-MM-DD'
                    Strategy_Framework = (string)config["Strategy_Framework"]
                };
            }
        }
        catch (WebException ex)
        {
            // Handle potential errors (for example, log the error)
            Console.WriteLine("Error fetching implementation year data: " + ex.Message);
        }

        // Second API call to fetch PMMU List
        string Document_Type = "Individual Scorecard";
        string ScoreCardType = "Directors";
        string page = $"PMMUList?$filter=Document_Type eq '{Document_Type}' and ScoreCardType eq '{ScoreCardType}'&$orderby=No desc&$format=json";

        try
        {
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);

            foreach (var jToken in details["value"])
            {
                var config = (JObject)jToken;
                PMMUCard pmmuCard = new PMMUCard
                {
                    No = (string)config["No"],
                    Responsible_Employee_No = (string)config["Responsible_Employee_No"],
                    Employee_Name = (string)config["Employee_Name"],
                    Description = (string)config["Description"],
                    Strategy_Plan_ID = (string)config["Strategy_Plan_ID"],
                    Functional_Template_ID = (string)config["Functional_Template_ID"],
                    Contract_Year = implementationYear.Strategy_Framework,
                    Goal_Template_ID = (string)config["Goal_Template_ID"],
                    Evaluation_Committee = (string)config["Evaluation_Committee"],
                    Approval_Status = (string)config["Approval_Status"],
                    Status = (string)config["Status"],
                    Date_Approved = (string)config["Date_Approved"],  // Parse date to DateTime
                    Change_Status = (string)config["Change_Status"],
                    Start_Date = implementationYear.Start_Date,          // Use implementation year start date
                    End_Date = implementationYear.End_Date,              // Use implementation year end date
                    Designation = (string)config["Designation"],
                    Grade = (string)config["Grade"],
                    Admin_Unit = (string)config["Admin_Unit_Name"],
                    Blocked_x003F_ = (bool)config["Blocked_x003F_"],
                    Created_By = (string)config["Created_By"],
                    Created_On = (string)config["Created_On"],
                    Created_On_2 = (DateTime)config["Created_On"],// Parse date to DateTime
                    Total_Assigned_Weight_Percent = (int)config["Total_Assigned_Weight_Percent"],
                    Secondary_Assigned_Weight_Percent = (int)config["Secondary_Assigned_Weight_Percent"],
                    JD_Assigned_Weight_Percent = (int)config["JD_Assigned_Weight_Percent"]
                };
                PMMUCardList.Add(pmmuCard);
            }
        }
        catch (WebException ex)
        {
            // Handle potential errors for the second API call
            Console.WriteLine("Error fetching PMMU list: " + ex.Message);
        }

        return PartialView("~/Views/Performance/Partial Views/PMMUList.cshtml", PMMUCardList);
    }

    // Helper function to parse nullable DateTime values
    private DateTime? ParseDate(JToken dateToken)
    {
        if (dateToken != null && !string.IsNullOrEmpty(dateToken.ToString()))
        {
            return DateTime.Parse(dateToken.ToString());
        }
        return null;
    }

    [HttpPost]
    public ActionResult PMMUDocumentView(string No, string PMMU_Document_Stage, string Target_Setting_Commitee, string Evaluation_Committee)
    {
        try
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string staffName = employeeView.FirstName + " " + employeeView.LastName;
            string StaffNo = Session["Username"].ToString();




            // First API call to fetch Implementation Years
            string pageIY = "STAJList?$filter=Implementation_Status eq 'Ongoing'&$format=json";
            ImplementationYears implementationYear = new ImplementationYears();

            try
            {
                HttpWebResponse httpResponseIY = Credentials.GetOdataData(pageIY);
                using var streamReader = new StreamReader(httpResponseIY.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    // Parse the Start_Date into DateTime
                    DateTime startDate = DateTime.Parse((string)config["Start_Date"]);

                    // Calculate June 30 of the following year
                    DateTime endDate = new DateTime(startDate.Year + 1, 6, 30);  // June 30 of the following year

                    // Assign the values to the ImplementationYears object
                    implementationYear = new ImplementationYears
                    {
                        Description = (string)config["Description"],
                        Start_Date = startDate.ToString("yyyy-MM-dd"),   // Format Start_Date as 'YYYY-MM-DD'
                        End_Date = endDate.ToString("yyyy-MM-dd"),       // Format End_Date as 'YYYY-MM-DD'
                        Strategy_Framework = (string)config["Strategy_Framework"]
                    };
                }
            }
            catch (WebException ex)
            {
                // Handle potential errors (for example, log the error)
                Console.WriteLine("Error fetching implementation year data: " + ex.Message);
            }


            PMMUCard pmmu = new PMMUCard();

            #region GeneralSetup
            string pageGenSetup = "GeneralSetup?$format=json";
            HttpWebResponse httpResponseGenSetup = Credentials.GetOdataData(pageGenSetup);
            using (var streamReader = new StreamReader(httpResponseGenSetup.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    pmmu.PMMU_Document_Stage = (string)config["PMMU_Document_Stage"];
                }
            }
            #endregion

            // Updated API endpoint
            string page = $"PMMUList?$filter=No eq '{No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            string staj = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    pmmu.No = (string)config["No"];
                    pmmu.Responsible_Employee_No = StaffNo;
                    pmmu.Employee_Name = staffName;
                    pmmu.Description = (string)config["Description"];
                    pmmu.Strategy_Plan_ID = (string)config["Strategy_Plan_ID"];
                    pmmu.Functional_Template_ID = (string)config["Functional_Template_ID"];
                    pmmu.Contract_Year = implementationYear.Strategy_Framework;
                    pmmu.Goal_Template_ID = (string)config["Goal_Template_ID"];
                    pmmu.Target_Setting_Commitee = (string)config["Target_Setting_Commitee"];
                    pmmu.Evaluation_Committee = (string)config["Evaluation_Commitee"];
                    pmmu.Approval_Status = (string)config["Approval_Status"];
                    pmmu.Status = (string)config["Status"];
                    pmmu.Date_Approved = (string)config["Date_Approved"];
                    pmmu.Change_Status = (string)config["Change_Status"];
                    pmmu.Start_Date = implementationYear.Start_Date;
                    pmmu.End_Date = implementationYear.End_Date;
                    pmmu.Designation = (string)config["Designation"];
                    pmmu.Grade = (string)config["Grade"];
                    pmmu.Admin_Unit = (string)config["Admin_Unit"];
                    pmmu.Admin_Unit_Name = (string)config["Admin_Unit_Name"];
                    pmmu.Blocked_x003F_ = (bool)config["Blocked_x003F_"];
                    pmmu.Created_By = (string)config["Created_By"];
                    pmmu.Created_On = (string)config["Created_On"];
                    pmmu.Total_Assigned_Weight_Percent = (int)config["Total_Assigned_Weight_Percent"];
                    pmmu.Secondary_Assigned_Weight_Percent = (int)config["Secondary_Assigned_Weight_Percent"];
                    pmmu.JD_Assigned_Weight_Percent = (int)config["JD_Assigned_Weight_Percent"];
                    staj = (string)config["Strategy_Plan_ID"];
                }
            }

            #region NegotiationCommitteeMembers
            List<PMMUCard> NegotiationCommitteeList = new List<PMMUCard>();
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            string pageCommittee = $"CommitteeAppointmentLines?$format=json&$filter=Document_No eq '{pmmu.Target_Setting_Commitee}'";

            try
            {
                HttpWebResponse httpResponseCommittee = Credentials.GetOdataData(pageCommittee);
                using var streamReader = new StreamReader(httpResponseCommittee.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var members = details["value"];
                if (members != null)
                {
                    foreach (JObject config in members)
                    {
                        PMMUCard negotiationCommitteeMember = new PMMUCard
                        {
                            Member_No = (string)config["Member_No"],
                            Role = (string)config["Role"],
                            Member_Name = (string)config["Member_Name"],
                            Member_Email = (string)config["Member_Email"],
                        };
                        NegotiationCommitteeList.Add(negotiationCommitteeMember);

                        // Convert to SelectListItem
                        selectListItems.Add(new SelectListItem
                        {
                            Text = negotiationCommitteeMember.Member_Name, // Text to be displayed in the dropdown
                            Value = negotiationCommitteeMember.Member_No    // Value associated with the dropdown item
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the error here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            #endregion
            pmmu.ListOfNegotiationCommitteeMembers = selectListItems;

            #region EvaluationCommitteeMembers
            List<PMMUCard> EvaluationCommitteeList = new List<PMMUCard>();
            List<SelectListItem> selectListItems2 = new List<SelectListItem>();

            string pageCommittee2 = $"CommitteeAppointmentLines?$format=json&$filter=Document_No eq '{pmmu.Target_Setting_Commitee}'";

            try
            {
                HttpWebResponse httpResponseCommittee = Credentials.GetOdataData(pageCommittee);
                using var streamReader = new StreamReader(httpResponseCommittee.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                var members = details["value"];
                if (members != null)
                {
                    foreach (JObject config in members)
                    {
                        PMMUCard evaluationCommitteeMember = new PMMUCard
                        {
                            Member_No = (string)config["Member_No"],
                            Role = (string)config["Role"],
                            Member_Name = (string)config["Member_Name"],
                            Member_Email = (string)config["Member_Email"],
                        };
                        EvaluationCommitteeList.Add(evaluationCommitteeMember);

                        // Convert to SelectListItem
                        selectListItems2.Add(new SelectListItem
                        {
                            Text = evaluationCommitteeMember.Member_Name, // Text to be displayed in the dropdown
                            Value = evaluationCommitteeMember.Member_No    // Value associated with the dropdown item
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the error here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            #endregion
            pmmu.ListOfEvaluationCommitteeMembers = selectListItems;
            ViewBag.TotalWeight2 = GetTotalWeight(No, staj);
            return View(pmmu);
        }
        catch (Exception ex)
        {
            Error erroMsg = new Error
            {
                Message = ex.Message
            };
            return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
        }
    }
    public PartialViewResult PMMULines(string No, string Approval_Status, string PMMU_Document_Stage, string Strategy_Plan_ID)
    {
        try
        {
            List<PMMULines> pmmuLines = new List<PMMULines>();

            string page = "PMMULines?$filter=Contract_No eq '" + No + "'&Strategy_Plan_ID eq '" + Strategy_Plan_ID + "'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);

            int TotalWeight = 0;  // Initialize the total weight

            foreach (JObject config in details["value"])
            {
                // Parse the weight first
                int weight = (int)config["Weight"];

                // Add the current weight to the running total
                TotalWeight += weight;

                // Create the PMMULines object
                PMMULines pmmuLine = new PMMULines
                {
                    Contract_No = (string)config["Contract_No"],
                    Strategy_Plan_ID = (string)config["Strategy_Plan_ID"],
                    Entry_No = (int)config["Entry_No"],
                    Outcome = (string)config["Outcome"],
                    Strategic_Objective = (string)config["Strategic_Objective"],
                    Strategies = (string)config["Strategies"],
                    Perspectives = (string)config["Perspectives"],
                    Strategy_Output_Code = (string)config["Strategy_Output_Code"],
                    Outputs = (string)config["Outputs"],
                    Output_Indicators = (string)config["Output_Indicators"],
                    Activities = (string)config["Activities"],
                    Key_result_Areas = (string)config["Key_result_Areas"],
                    Key_Indicators = (string)config["Key_Indicators"],
                    Unit_of_Measure = (string)config["Unit_of_Measure"],
                    Baseline_Target = (string)config["Baseline_Target"],
                    Weight = weight,  // Assign the parsed weight
                    National_Average = (int)config["National_Average"],
                    Best_Achievement = (int)config["Best_Achievement"],
                    Target = (int)config["Target"],
                    Achieved_Target = (int)config["Achieved_Target"],
                    Score = (int)config["Score"],
                    Comments = (string)config["Comments"],
                    TotalWeight = TotalWeight  // Update TotalWeight for each line
                };

                // Add the object to the list
                pmmuLines.Add(pmmuLine);
            }

            // Correct variable usage: change TW to TotalWeight
            PMMULinesList Lines = new PMMULinesList
            {
                Approval_Status = Approval_Status,
                PMMU_Document_Stage = PMMU_Document_Stage,
                ListOfPMMULines = pmmuLines,
                TotalWeight = TotalWeight  // Assign the aggregated total weight
            };

            ViewBag.TotalWeight = TotalWeight;
            PMMUCard pmmu = new PMMUCard();
            pmmu.TotalWeight = TotalWeight;
            return PartialView("~/Views/Performance/Partial Views/PMMULines.cshtml", Lines);
        }
        catch (Exception ex)
        {
            Error erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }

    public int GetTotalWeight(string No, string Strategy_Plan_ID)
    {
        string page = "PMMULines?$filter=Contract_No eq '" + No + "'&Strategy_Plan_ID eq '" + Strategy_Plan_ID + "'&$format=json";
        HttpWebResponse httpResponse = Credentials.GetOdataData(page);
        int TotalWeight = 0;
        using var streamReader = new StreamReader(httpResponse.GetResponseStream());
        var result = streamReader.ReadToEnd();

        var details = JObject.Parse(result);

        // Initialize the total weight

        foreach (JObject config in details["value"])
        {
            // Parse the weight first
            int weight = (int)config["Weight"];

            // Add the current weight to the running total
            TotalWeight += weight;
        }

        return TotalWeight;
    }

    //create PMMU document
    public ActionResult NewPMMU()
    {
        try
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                PMMUCard pmmu = new PMMUCard();
                Session["httpResponse"] = null;
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                pmmu.Responsible_Employee_No = employeeView.No;
                pmmu.Employee_Name = employeeView.FirstName + " " + employeeView.LastName + " (" + employeeView.No + ")";




                #region StrategyPlanID
                List<DropdownList> StrategyPlanID = new List<DropdownList>();
                string pageSPI = "AllCSPS?$filter=Implementation_Status eq 'Ongoing'&$format=json";

                HttpWebResponse httpResponseSPI = Credentials.GetOdataData(pageSPI);
                using (var streamReader = new StreamReader(httpResponseSPI.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList3 = new DropdownList();
                        dropdownList3.Text = (string)config["Code"];
                        dropdownList3.Value = (string)config["Code"];
                        StrategyPlanID.Add(dropdownList3);
                    }
                }
                #endregion

                #region EvaluationCommittee
                List<DropdownList> EvaluationCommittee = new List<DropdownList>();
                string pageEC = "CommitteeAppointmentVouchers?$format=json";

                HttpWebResponse httpResponseEC = Credentials.GetOdataData(pageEC);
                using (var streamReader = new StreamReader(httpResponseEC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Name"] + " (" + (string)config["Document_No"] + ")";
                        dropdownList.Value = (string)config["Document_No"];
                        EvaluationCommittee.Add(dropdownList);
                    }
                }
                #endregion



                pmmu.ListOfStrategyPlanID = StrategyPlanID.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                pmmu.ListOfNegotiationCommittee = EvaluationCommittee.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();



                return PartialView("~/Views/Performance/Partial Views/NewPMMU.cshtml", pmmu);

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

    public JsonResult SubmitPMMU(PMMUCard newPMMU)
    {
        try
        {

            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string Responsible_Employee_No = employeeView.No;
            string UserID = employeeView.UserID;

            string Description = "";
            if (newPMMU.Description != null) { Description = (string)newPMMU.Description; }

            string Strategy_Plan_ID = "";
            if (newPMMU.Strategy_Plan_ID != null) { Strategy_Plan_ID = (string)newPMMU.Strategy_Plan_ID; }

            string Admin_Unit = "";
            if (newPMMU.Admin_Unit != null) { Admin_Unit = (string)newPMMU.Admin_Unit; }

            string Target_Setting_Commitee = "N/a";
            if (newPMMU.Target_Setting_Commitee == "N/a") { Target_Setting_Commitee = (string)newPMMU.Target_Setting_Commitee; }

            string Evaluation_Committee = "N/a";
            if (newPMMU.Evaluation_Committee == "N/a") { Evaluation_Committee = (string)newPMMU.Evaluation_Committee; }

            string pmmuH = "";

            string staffNo = Session["Username"].ToString();
            EmployeeView employee = Session["EmployeeData"] as EmployeeView;
            //string userId = employee.UserID;

            string docNo = docNo = Credentials.ObjNav.InsertPMMUHeader(
                Strategy_Plan_ID,
                Description,
                pmmuH,
                Responsible_Employee_No,
                Target_Setting_Commitee,
                Evaluation_Committee,
                UserID,
                1
            );

            if (docNo != "")
            {
                string Redirect = docNo;
                //Session["SuccessMsg"] = "Purchase Requisition, Document No: " + DocNo + ", created Successfully. Add line(s) and attachment(s) then sent for approval";
                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Error submitting record. Try again.", success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult SubmitPMMUCopies(PMMUCard newPMMU)
    {
        try
        {
            int numberOfCopies = newPMMU.NumberOfCopies;

            // Retrieve employee details from the session
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            string Responsible_Employee_No = employeeView?.No ?? "";
            string UserID = employeeView?.UserID ?? "";

            bool created = false;

            /*created = Credentials.ObjNav.CreatePmmuCopies(
                newPMMU.No,
               numberOfCopies
            );*/



            return Json(new { message = "PMMU Copies created", success = true }, JsonRequestBehavior.AllowGet);

            /*else
            {
                return Json(new { message = "Error creating copies. Try again.", success = false }, JsonRequestBehavior.AllowGet);
            }*/
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    //update PMMU header
    public ActionResult UpdatePMMUHeader(PMMUCard record)
    {
        try
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                string NegotiationCommitteeVoucher = record.Target_Setting_Commitee;
                string EvaluationCommitteeVoucher = record.Evaluation_Committee;

                PMMUCard pmmu = new PMMUCard();
                Session["httpResponse"] = null;
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                pmmu.Responsible_Employee_No = employeeView.No;
                pmmu.Employee_Name = employeeView.FirstName + " " + employeeView.LastName + " (" + employeeView.No + ")";

                #region StrategyPlanID
                List<DropdownList> StrategyPlanID = new List<DropdownList>();
                string pageSPI = "AllCSPS?$filter=Implementation_Status eq 'Ongoing'&$format=json";

                HttpWebResponse httpResponseSPI = Credentials.GetOdataData(pageSPI);
                using (var streamReader = new StreamReader(httpResponseSPI.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList3 = new DropdownList();
                        dropdownList3.Text = (string)config["Code"];
                        dropdownList3.Value = (string)config["Code"];
                        StrategyPlanID.Add(dropdownList3);
                    }
                }
                #endregion

                #region GeneralSetup
                //List<DropdownList> StrategyPlanID = new List<DropdownList>();
                string pageGenSetup = "GeneralSetup?$format=json";

                HttpWebResponse httpResponseGenSetup = Credentials.GetOdataData(pageGenSetup);
                using (var streamReader = new StreamReader(httpResponseGenSetup.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        pmmu.PMMU_Document_Stage = (string)config["PMMU_Document_Stage"];
                    }
                }
                #endregion

                #region CommitteeVouchers
                List<DropdownList> EvaluationCommittee = new List<DropdownList>();
                string documentType = "PMMU";
                //string pageEC = "CommitteeAppointmentVouchers?$format=json";
                string pageEC = $"CommitteeAppointmentVouchers?$orderby=Document_No desc&$filter=Document_Type eq '{documentType}'&$format=json";

                HttpWebResponse httpResponseEC = Credentials.GetOdataData(pageEC);
                using (var streamReader = new StreamReader(httpResponseEC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Document_No"] + "-" + (string)config["Description"] + " ";
                        dropdownList.Value = (string)config["Document_No"];
                        EvaluationCommittee.Add(dropdownList);
                    }
                }
                #endregion

                #region NegotiationCommitteeMembers

                List<PMMUCard> NegotiationCommitteeMembersList = new List<PMMUCard>();
                List<SelectListItem> selectListItems = new List<SelectListItem>();

                string pageCommittee = $"CommitteeAppointmentLines?$format=json&$filter=Document_No eq '{NegotiationCommitteeVoucher}'";

                try
                {
                    HttpWebResponse httpResponseCommittee = Credentials.GetOdataData(pageCommittee);
                    using var streamReader = new StreamReader(httpResponseCommittee.GetResponseStream());
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var members = details["value"];
                    if (members != null)
                    {
                        foreach (JObject config in members)
                        {
                            PMMUCard negotiationCommitteeMember = new PMMUCard
                            {
                                Member_No = (string)config["Member_No"],
                                Role = (string)config["Role"],
                                Member_Name = (string)config["Member_Name"],
                                Member_Email = (string)config["Member_Email"],
                            };
                            NegotiationCommitteeMembersList.Add(negotiationCommitteeMember);

                            // Convert to SelectListItem
                            selectListItems.Add(new SelectListItem
                            {
                                Text = negotiationCommitteeMember.Member_Name, // Text to be displayed in the dropdown
                                Value = negotiationCommitteeMember.Member_No    // Value associated with the dropdown item
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the error here
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                #endregion

                #region EvaluationCommitteeMembers

                List<PMMUCard> EvaluationCommitteeMembersList = new List<PMMUCard>();
                List<SelectListItem> selectListItems2 = new List<SelectListItem>();

                string pageCommittee2 = $"CommitteeAppointmentLines?$format=json&$filter=Document_No eq '{NegotiationCommitteeVoucher}'";

                try
                {
                    HttpWebResponse httpResponseCommittee2 = Credentials.GetOdataData(pageCommittee2);
                    using var streamReader = new StreamReader(httpResponseCommittee2.GetResponseStream());
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var members = details["value"];
                    if (members != null)
                    {
                        foreach (JObject config in members)
                        {
                            PMMUCard evaluationCommitteeMember = new PMMUCard
                            {
                                Member_No = (string)config["Member_No"],
                                Role = (string)config["Role"],
                                Member_Name = (string)config["Member_Name"],
                                Member_Email = (string)config["Member_Email"],
                            };
                            EvaluationCommitteeMembersList.Add(evaluationCommitteeMember);

                            // Convert to SelectListItem
                            selectListItems2.Add(new SelectListItem
                            {
                                Text = evaluationCommitteeMember.Member_Name, // Text to be displayed in the dropdown
                                Value = evaluationCommitteeMember.Member_No    // Value associated with the dropdown item
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the error here
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                #endregion




                pmmu.ListOfStrategyPlanID = StrategyPlanID.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                //negotioation committee vouchers
                pmmu.ListOfNegotiationCommittee = EvaluationCommittee.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                //Evaluation committee vouchers
                pmmu.ListOfEvaluationCommittee = EvaluationCommittee.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();


                //Negotioation committee members
                pmmu.ListOfNegotiationCommitteeMembers = selectListItems.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                //Evaluation committee members
                pmmu.ListOfEvaluationCommitteeMembers = selectListItems2.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                pmmu.Approval_Status = record.Approval_Status;
                return PartialView("~/Views/Performance/Partial Views/UpdatePMMUHeader.cshtml", pmmu);
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
    public JsonResult SubmitUpdatedPMMU(PMMUCard updatedPMMU)
    {
        try
        {
            string No = "";
            if (updatedPMMU.No != null) { No = (string)updatedPMMU.No; }

            string Description = "";
            if (updatedPMMU.Description != null) { Description = (string)updatedPMMU.Description; }

            string Strategy_Plan_ID = "";
            if (updatedPMMU.Strategy_Plan_ID != null) { Strategy_Plan_ID = (string)updatedPMMU.Strategy_Plan_ID; }

            string Target_Setting_Commitee = (string)updatedPMMU.Target_Setting_Commitee;
            if (Target_Setting_Commitee == null) { Target_Setting_Commitee = "N/a"; }

            string Evaluation_Committee = (string)updatedPMMU.Evaluation_Committee;
            if (Evaluation_Committee == null) { Evaluation_Committee = "N/a"; }

            string pmmuH = "";
            string employNum = "";

            string staffNo = Session["Username"].ToString();
            EmployeeView employee = Session["EmployeeData"] as EmployeeView;
            string userId = employee.UserID;

            string docNo = "";
            docNo = Credentials.ObjNav.UpdatePMMUHeader(
                No,
                Strategy_Plan_ID,
                Description,
                pmmuH,
                employNum,
                Target_Setting_Commitee,
                Evaluation_Committee

            );

            /* if (docNo != "")
             {*/
            string Redirect = No;
            //Session["SuccessMsg"] = "Purchase Requisition, Document No: " + DocNo + ", created Successfully. Add line(s) and attachment(s) then sent for approval";
            return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            /* }
             else
             {
                 return Json(new { message = "Error submitting record. Try again.", success = false }, JsonRequestBehavior.AllowGet);
             }*/
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    //create new PMMU line
    public ActionResult NewPMMULine(string Strategy_Plan_ID, string Contract_No)
    {
        try
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                PMMULines pmmuLine = new PMMULines();
                Session["httpResponse"] = null;
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                pmmuLine.Strategy_Plan_ID = Strategy_Plan_ID;
                pmmuLine.Contract_No = Contract_No;

                ViewBag.TotalWeight3 = GetTotalWeight(pmmuLine.Contract_No, pmmuLine.Strategy_Plan_ID);


                #region Outcome
                List<DropdownList> outcome = new List<DropdownList>();
                string pagePPlan = "KeyResultAreas?$filter=Strategic_Plan_ID eq '" + Strategy_Plan_ID + "'&$format=json";

                HttpWebResponse httpResponsePPlan = Credentials.GetOdataData(pagePPlan);
                using (var streamReader = new StreamReader(httpResponsePPlan.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Theme_ID"] + "-" + (string)config["Description"];
                        dropdownList.Value = (string)config["Strategic_Plan_ID"];
                        outcome.Add(dropdownList);
                    }
                }
                #endregion

                #region Perspectives
                List<DropdownList> perspectives = new List<DropdownList>();

                string pagePers = "Perspectives?$filter=Strategy_Plan_ID eq '" + Strategy_Plan_ID + "'&$format=json";


                HttpWebResponse httpResponsepagePers = Credentials.GetOdataData(pagePers);
                using (var streamReader = new StreamReader(httpResponsepagePers.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Code"];
                        dropdownList.Value = (string)config["Code"];
                        perspectives.Add(dropdownList);
                    }
                }
                #endregion

                #region UOM
                List<DropdownList> uom = new List<DropdownList>();
                string pageUOM = "uOm?$format=json";

                HttpWebResponse httpResponsepageUOM = Credentials.GetOdataData(pageUOM);
                using (var streamReader = new StreamReader(httpResponsepageUOM.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Code"];
                        dropdownList.Value = (string)config["Code"];
                        uom.Add(dropdownList);
                    }
                }
                #endregion

                pmmuLine.ListOfOutcomes = outcome.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                pmmuLine.ListOfPerspectives = perspectives.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                pmmuLine.ListOfUOM = uom.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                return PartialView("~/Views/Performance/Partial Views/NewPMMULine.cshtml", pmmuLine);

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
    public JsonResult SubmitPMMULine(PMMULines newPMMULine)
    {
        try
        {
            string staffNo = Session["Username"]?.ToString();
            EmployeeView employee = Session["EmployeeData"] as EmployeeView;
            string Contract_No = newPMMULine.Contract_No;

            string OutcomeAndDesciption = newPMMULine.Theme_ID;
            string[] splitOutcome = OutcomeAndDesciption.Split('-');
            string Outcome = splitOutcome[0];// Access the parts of the split array

            string Strategic_Plan_ID = newPMMULine.Outcome;
            string Strategic_Objective = newPMMULine.Strategic_Objective;
            string Strategies = newPMMULine.Strategies;
            string Perspectives = newPMMULine.Perspectives;
            string Strategy_Output_Code = newPMMULine.Strategy_Output_Code;
            string Outputs = newPMMULine.Outputs;
            string Output_Indicators = newPMMULine.Output_Indicators;

            List<string> ActivitiesList = newPMMULine.ActivitiesArray.ToList();
            // Convert the List<string> to an array of decimals
            decimal[] ActivitiesArray = ActivitiesList
                .Select(activity =>
                {
                    decimal value;
                    return decimal.TryParse(activity, out value) ? value : 0; // Default to 0 if parsing fails
                })
                .ToArray();

            List<string> SubWeightsList = newPMMULine.SubWeightsArray.ToList();
            decimal[] SubWeightsArray = SubWeightsList
                .Select(activity =>
                {
                    decimal value;
                    return decimal.TryParse(activity, out value) ? value : 0; // Default to 0 if parsing fails
                })
                .ToArray();

            string Unit_of_Measure = newPMMULine.Unit_of_Measure;
            string Baseline_Target = newPMMULine.Baseline_Target;
            decimal Weight = newPMMULine.Weight;
            decimal Target = newPMMULine.Target;
            decimal Achieved_Target = newPMMULine.Achieved_Target;
            int Test2 = 10;

            decimal Net_Weight = (Achieved_Target / Target) * Weight; //score
            decimal[] quantity = new decimal[] { Weight, Test2, Achieved_Target, Target };
            string Comments = newPMMULine.Comments;
            if (Comments == null) { Comments = " "; }

            //First insert the line and get the line  number
            int LineNumber = 1;
            LineNumber = Credentials.ObjNav.InsertPmmuLines(
                Contract_No,
                Strategic_Plan_ID,
                quantity,
                Outcome,
                Strategies,
                Strategy_Output_Code,
                Unit_of_Measure,
                Baseline_Target,
                Strategic_Objective,
                Perspectives,
                Comments
            );

            //Next, insert the activities for that line

            int activitiesCount = ActivitiesList.Count;

            for (int i = 0; i < activitiesCount; i++)
            {
                string activity = ActivitiesList[i];
                decimal act = ActivitiesArray[i];
                decimal sub = SubWeightsArray[i];

                Credentials.ObjNav.insertPMMULinesActivities(
                    activity,
                    Strategy_Output_Code,
                    Contract_No,
                    LineNumber,
                    Strategic_Plan_ID,
                    sub,
                    act
                );
            }

            //Lastly, Redirect back to the document
            string redirect = Contract_No;
            return Json(new { message = redirect, success = redirect }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }




    [HttpPost]
    public JsonResult AddActivities(PMMULinesActivities activitiesData, string Contract_No, string Strategy_Plan_ID, string Entry_No)
    {
        try
        {



            #region pmmulines


            string pageLines = "PMMULines?$filter=Contract_No eq '" + Contract_No +
                               "' and Strategy_Plan_ID eq '" + Strategy_Plan_ID +
                               "' and Entry_No eq " + Entry_No + "&$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(pageLines);

            // Initialize a list to hold the PMMULinesActivities objects
            List<PMMULines> linesList = new List<PMMULines>();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    PMMULines lines = new PMMULines
                    {
                        Contract_No = (string)config["Contract_No"], //present
                        Entry_No = (int)config["Entry_No"], //present
                        Strategy_Plan_ID = (string)config["Strategy_Plan_ID"], //present
                        Strategy_Output_Code = (string)config["Strategy_Output_Code"],
                        //Activities = (string)config["Activities"],
                        Baseline_Target = (string)config["Baseline_Target"],
                        /* Weight = (int)config["Weight"],
                         Target = (int)config["Target"],
                         Achieved_Target = (int)config["Achieved_Targets"],*/

                    };

                    linesList.Add(lines);

                }
            }

            #endregion

            List<string> activities = activitiesData.ActivitiesArray;
            List<decimal> subWeights = activitiesData.SubWeightsArray;
            List<decimal> achievedTargets = activitiesData.AchievedTargetsArray;


            for (int i = 0; i < activities.Count; i++)
            {
                string activity = activities[i];
                decimal subWeight = subWeights[i];
                decimal achievedTarget = achievedTargets[i];

                var line = linesList[0];
                // Now you can insert or process these values
                // Call your insert function here
                Credentials.ObjNav.insertPMMULinesActivities(
                    activity,
                    line.Strategy_Output_Code,
                    Contract_No,
                    line.Entry_No,
                    Strategy_Plan_ID,
                    subWeight,
                    achievedTarget
                );
            }


            //Lastly, Redirect back to the document
            string redirect = Contract_No;
            return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }


    //Update PMMU line
    public JsonResult UpdatePMMULine(PMMULines updatedPMMULine)
    {
        try
        {
            string staffNo = Session["Username"]?.ToString();
            EmployeeView employee = Session["EmployeeData"] as EmployeeView;

            string docNo = updatedPMMULine.Contract_No;
            int Line_No = updatedPMMULine.Entry_No;
            string Outcome = updatedPMMULine.Theme_ID;
            string Strategic_Plan_ID = updatedPMMULine.Strategy_Plan_ID;
            string Strategic_Objective = updatedPMMULine.Strategic_Objective;
            string Strategies = updatedPMMULine.Strategies;
            string Perspectives = updatedPMMULine.Perspectives;
            string Strategy_Output_Code = updatedPMMULine.Strategy_Output_Code;
            string Outputs = updatedPMMULine.Outputs;
            string Output_Indicators = updatedPMMULine.Output_Indicators;
            string Activities = updatedPMMULine.Activities;
            string Unit_of_Measure = updatedPMMULine.Unit_of_Measure;
            if (Unit_of_Measure == null) { Unit_of_Measure = " "; }
            string Baseline_Target = updatedPMMULine.Baseline_Target;
            decimal Weight = updatedPMMULine.Weight;
            decimal Target = updatedPMMULine.Target;
            decimal Achieved_Target = updatedPMMULine.Achieved_Target;
            decimal Test2 = 70;
            decimal Net_Weight = Achieved_Target / 10;
            //decimal Net_Weight = (Achieved_Target / Target) * Weight; //score
            decimal[] quantity = new decimal[] { Weight, Test2, Achieved_Target, Target };

            string Comments = updatedPMMULine.Comments;
            if (Comments == null) { Comments = " "; }


            string res = "";
            res = Credentials.ObjNav.UpdatePmmuLine(
                docNo,
                Line_No,
                Strategic_Plan_ID,
                quantity,
                Outcome,
                Strategies,
                Strategy_Output_Code,
                Unit_of_Measure,
                Baseline_Target,
                Strategic_Objective,
                Perspectives,
                Comments
            );

            if (res != "")
            {
                string redirect = "/Performance/PMMUDocumentView?No=" + docNo;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Error submitting record. Try again", success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult GetStrategicObjectives(string Strategic_Plan_ID, string Outcome)
    {
        try
        {
            #region StrategicObjectivesLookup
            List<DropdownList> StrategicObjectivesLookup = new List<DropdownList>();
            // string pageStratObj = $"CoreStrategy?$filter=Strategic_Plan_ID eq '{Strategic_Plan_ID}'&$format=json";

            string pageStratObj = $"CoreStrategy?$filter=Strategic_Plan_ID eq '{Strategic_Plan_ID}' and Theme_ID eq '{Outcome}'&$format=json";

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
            // Create and return the JSON result
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
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult GetStrategies(string Objective_ID, string Strategic_Plan_ID)
    {
        try
        {
            #region StrategiesLookup
            List<DropdownList> StrategiesLookup = new List<DropdownList>();
            string pageStrat = $"Strategies?$filter=Objective_ID eq '{Objective_ID}' and Strategic_Plan_ID eq '{Strategic_Plan_ID}'&$format=json";

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
            // Create and return the JSON result
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
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult GetStrategyOutputCodes(string Strategy_ID, string Strategic_Plan_ID)
    {
        try
        {
            #region StrategyOutputCodesLookup
            List<DropdownList> StrategyOutputCodesLookup = new List<DropdownList>();
            string pageSOC = $"StrategyOutput?$filter=Strategy_ID eq '{Strategy_ID}'and Strategic_Plan_ID eq '{Strategic_Plan_ID}'&$format=json";

            HttpWebResponse httpResponseSOC = Credentials.GetOdataData(pageSOC);
            using (var streamReader = new StreamReader(httpResponseSOC.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    DropdownList dropdownList = new DropdownList();
                    dropdownList.Text = (string)config["Strategy_Output_Code"] + "-" + (string)config["Outputs"];
                    dropdownList.Value = (string)config["Strategy_Output_Code"];
                    StrategyOutputCodesLookup.Add(dropdownList);
                }
            }
            #endregion
            // Create and return the JSON result
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
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult GetOutputs(string Stategy_Output_Code, string Strategic_Plan_ID)
    {
        try
        {
            #region OutputsLookup
            List<DropdownList> OutputsLookup = new List<DropdownList>();
            List<DropdownList> OutputIndicatorsLookup = new List<DropdownList>();
            string pageOutput = $"StrategyOutput?$filter=Strategy_Output_Code eq '{Stategy_Output_Code}' and Strategic_Plan_ID eq '{Strategic_Plan_ID}' &$format=json";

            HttpWebResponse httpResponseOutput = Credentials.GetOdataData(pageOutput);
            using (var streamReader = new StreamReader(httpResponseOutput.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    DropdownList outputsDropdown = new DropdownList();
                    outputsDropdown.Text = (string)config["Outputs"];
                    outputsDropdown.Value = (string)config["Outputs"];
                    OutputsLookup.Add(outputsDropdown);
                }

                foreach (JObject config in details["value"])
                {
                    DropdownList outputIndicatorsDropdown = new DropdownList();
                    outputIndicatorsDropdown.Text = (string)config["Output_Indicators"];
                    outputIndicatorsDropdown.Value = (string)config["Output_Indicators"];
                    OutputIndicatorsLookup.Add(outputIndicatorsDropdown);
                }



            }
            #endregion
            // Create and return the JSON result
            var response = new
            {
                ListOfOutputs = OutputsLookup.Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList(),


                ListOfOutputIndicators = OutputIndicatorsLookup.Select(x => new SelectListItem
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
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }


    //submit draft PMMU
    public JsonResult SubmitDraftPMMU(string DocNo)
    {
        try
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

            string staffNo = Session["Username"].ToString();
            string msg = "";
            bool valSucc = false;
            string userId = employeeView.UserID;

            //Credentials.ObjNav.SubmitDraftPMMU(DocNo);

            msg = "Draft PMMU, (Document No, " + DocNo + ") successfully submitted";
            valSucc = true;

            return Json(new { message = msg, success = valSucc }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult CancelSubmitDraftPMMU(string DocNo)
    {
        try
        {
            EmployeeView employee = Session["EmployeeData"] as EmployeeView;
            string empNo = employee.No;
            //Credentials.ObjNav.CancelSubmitDraftPMMU(empNo, DocNo);
            return Json(new { message = "Submit Draft PMMU Cancelled Successfully", success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }


    //send for approval
    public JsonResult SendPMMUAppForApproval(string DocNo)
    {
        try
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

            string staffNo = Session["Username"].ToString();
            string msg = "";
            bool valSucc = false;
            string userId = employeeView.UserID;



            Credentials.ObjNav.SendPMMUApproval(DocNo);

            msg = "PMMU, (Document No, " + DocNo + ") successfully sent for approval ";
            valSucc = true;

            return Json(new { message = msg, success = valSucc }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult CancelPMMUApproval(string DocNo)
    {
        try
        {
            EmployeeView employee = Session["EmployeeData"] as EmployeeView;
            string empNo = employee.No;
            Credentials.ObjNav.CancelPMMUApproval(DocNo);
            return Json(new { message = "PMMU approval cancelled Successfully", success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult StartEvaluation(string DocNo)
    {
        try
        {
            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

            string staffNo = Session["Username"].ToString();
            string msg = "";
            bool valSucc = false;
            string userId = employeeView.UserID;

            Credentials.ObjNav.StartPMMUEvaluation(DocNo);

            msg = "PMMU, Evaluation started";
            valSucc = true;
            return Json(new { message = msg, success = valSucc }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    //Generate PMMU report
    public JsonResult GeneratePMMUReport(string DocNo)
    {
        try
        {
            string message = "";
            bool success = false, view = false;

            message = Credentials.ObjNav.GeneratePMMUReport(DocNo);
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

    public ActionResult PmmuActivities(string Contract_No, int Entry_No, string Strategy_Output_Code, string Strategy_Plan_ID, int Weight, string Document_Stage)
    {
        try
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Session["httpResponse"] = null;
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                #region Activities

                string pageActivities = $"PMMUOutputActivities?$filter=Strategy_Output_Code eq '{Strategy_Output_Code}' and Contract_No eq '{Contract_No}' and Entry_No eq {Entry_No}&$format=json";

                HttpWebResponse httpResponse = Credentials.GetOdataData(pageActivities);

                // Initialize a list to hold the PMMULinesActivities objects
                List<PMMULinesActivities> activitiesList = new List<PMMULinesActivities>();




                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        PMMULinesActivities activities = new PMMULinesActivities
                        {
                            Contract_No = (string)config["Contract_No"],
                            Entry_No = (int)config["Entry_No"],
                            Strategy_Plan_ID = (string)config["Strategy_Plan_ID"],
                            Strategy_Output_Code = (string)config["Strategy_Output_Code"],
                            Entry = (int)config["Entry"],
                            Activities = (string)config["Activities"],
                            SubWeights = (decimal)config["Sub_Weights"],
                            AchievedTargets = (decimal)config["Achieved_Targets"],
                            Weight = Weight,
                            Document_Stage = Document_Stage
                        };

                        activitiesList.Add(activities);

                    }
                }

                #endregion

                // Pass activities to the view
                return PartialView("~/Views/Performance/Partial Views/PmmuActivities.cshtml", activitiesList);
            }
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

    public ActionResult SubmitUpdatedActivities(List<PMMULinesActivities> activities)
    {
        try
        {
            string staffNo = Session["Username"]?.ToString();
            if (string.IsNullOrEmpty(staffNo))
            {
                return Json(new { message = "User session expired. Please log in again.", success = false }, JsonRequestBehavior.AllowGet);
            }

            EmployeeView employee = Session["EmployeeData"] as EmployeeView;
            if (employee == null)
            {
                return Json(new { message = "Unable to retrieve employee data.", success = false }, JsonRequestBehavior.AllowGet);
            }

            string res = "";
            bool allSuccess = true;
            foreach (var activity in activities)
            {
                // Ensure data consistency with client-side
                string contractNo = activity.Contract_No;
                int lineNo = activity.Entry_No;
                int entry = activity.Entry;
                string strategicPlanID = activity.Strategy_Plan_ID;
                string strategyOutputCode = activity.Strategy_Output_Code;
                decimal subWeights = activity.SubWeights;

                // Call the method to update activities
                res = Credentials.ObjNav.updatePMMULinesActivities(
                    entry,
                    activity.Activities,
                    strategyOutputCode,
                    contractNo,
                    lineNo,
                    strategicPlanID,
                    subWeights,
                    activity.AchievedTargets
                );

                // If an update fails, log the error and set the flag
                if (string.IsNullOrEmpty(res))
                {
                    allSuccess = false;
                    break; // Optionally, you can accumulate all failed updates instead of breaking
                }
            }

            if (allSuccess && !string.IsNullOrEmpty(res))
            {
                string redirect = activities.First().Contract_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Error updating records. Try again.", success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        catch (Exception ex)
        {
            // Log exception details (e.g., using a logging framework)
            return Json(new { message = "An error occurred: " + ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }


    //****************************************************************************
    // Commitee Appointment Vouchers
    //********************************************************************************
    public ActionResult CommitteeAppointmentVouchers()
    {
        if (Session["Username"] == null)
        {
            return RedirectToAction("Login", "Login");
        }
        else
        {
            return View();
        }
    }
    public PartialViewResult CommitteeAppointmentVouchersList()
    {
        EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
        string staffName = employeeView.FirstName + " " + employeeView.LastName;
        string StaffNo = Session["Username"].ToString();


        List<CommitteeAppointmentVouchers> committeeAppointmentVouchersList = new List<CommitteeAppointmentVouchers>();

        string documentType = "PMMU";
        string page = $"CommitteeAppointmentVoucher?$orderby=Document_No desc&$filter=Document_Type eq '{documentType}'&$format=json";

        HttpWebResponse httpResponse = Credentials.GetOdataData(page);
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);

            foreach (JObject config in details["value"])
            {
                CommitteeAppointmentVouchers appointmentVoucher = new CommitteeAppointmentVouchers
                {
                    Document_No = (string)config["Document_No"],
                    Document_Type = (string)config["Document_Type"],
                    Committee_Type_ID = (string)config["Committee_Type_ID"],
                    Vacancy_ID = (string)config["Vacancy_ID"],
                    Document_Date = (string)config["Document_Date"],
                    Description = (string)config["Description"],
                    Appointment_Effective_Date = (string)config["Appointment_Effective_Date"],
                    Tenure_End_Date = (string)config["Tenure_End_Date"],
                    Appointing_Authority = (string)config["Appointing_Authority"],
                    Raised_By = employeeView.UserID,
                    Staff_ID = employeeView.No,
                    Name = staffName,
                    Terms_of_Reference = (string)config["Terms_of_Reference"],
                    Additional_Brief = (string)config["Additional_Brief"],
                    Venue = (string)config["Venue"],
                    Approval_Status = (string)config["Approval_Status"]

                };

                committeeAppointmentVouchersList.Add(appointmentVoucher);
            }
        }
        return PartialView("~/Views/Performance/Partial Views/CommitteeAppointmentVouchersList.cshtml", committeeAppointmentVouchersList);
    }
    [HttpPost]
    public ActionResult CommitteeAppointmentVouchersDocumentView(string Document_No)
    {
        try
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
            //appointmentCommittee.Employee_Name = employeeView.FirstName + " " + employeeView.LastName + " (" + employeeView.No + ")";

            string StaffNo = Session["Username"].ToString();

            CommitteeAppointmentVouchers appointmentVoucher = new CommitteeAppointmentVouchers();

            // Updated API endpoint
            string page = $"CommitteeAppointmentVoucher?$filter=Document_No eq '{Document_No}'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                // Assuming the API returns a single item, so use FirstOrDefault to get that item
                var config = details["value"].FirstOrDefault();
                if (config != null)
                {
                    appointmentVoucher.Document_No = (string)config["Document_No"];
                    appointmentVoucher.Document_Type = (string)config["Document_Type"];
                    appointmentVoucher.Committee_Type_ID = (string)config["Committee_Type_ID"];
                    appointmentVoucher.Vacancy_ID = (string)config["Vacancy_ID"];
                    appointmentVoucher.Document_Date = (string)config["Document_Date"];
                    appointmentVoucher.Description = (string)config["Description"];
                    appointmentVoucher.Appointment_Effective_Date = (string)config["Appointment_Effective_Date"];
                    appointmentVoucher.Tenure_End_Date = (string)config["Tenure_End_Date"];
                    appointmentVoucher.Appointing_Authority = (string)config["Appointing_Authority"];
                    appointmentVoucher.Raised_By = employeeView.UserID;
                    appointmentVoucher.Staff_ID = employeeView.No;
                    appointmentVoucher.Name = (string)config["Name"];
                    appointmentVoucher.Terms_of_Reference = (string)config["Terms_of_Reference"];
                    appointmentVoucher.Additional_Brief = (string)config["Additional_Brief"];
                    appointmentVoucher.Venue = (string)config["Venue"];
                    appointmentVoucher.Approval_Status = (string)config["Approval_Status"];

                }
            }
            return View(appointmentVoucher);
        }
        catch (Exception ex)
        {
            Error erroMsg = new Error
            {
                Message = ex.Message
            };
            return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
        }
    }
    public PartialViewResult AppointmentVouchersLines(string Document_No, string status)
    {
        try
        {
            List<CommitteeAppointmentLines> committeeAppointmentLines = new List<CommitteeAppointmentLines>();

            //string page = "PMMULines?$format=json";
            string page = "CommitteeAppointmentLines?$filter=Document_No eq '" + Document_No + "'&$format=json";
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    CommitteeAppointmentLines appointmentLine = new CommitteeAppointmentLines
                    {
                        Document_No = (string)config["Document_No"],
                        Line_No = (int)config["Line_No"],
                        Member_No = (string)config["Member_No"],
                        Role = (string)config["Role"],
                        Member_Name = (string)config["Member_Name"],
                        Member_Email = (string)config["Member_Email"],
                    };
                    committeeAppointmentLines.Add(appointmentLine);
                }
            }
            CommitteeAppointmentLinesList CALines = new CommitteeAppointmentLinesList
            {
                Status = status,
                ListOfCommitteeAppointmentLines = committeeAppointmentLines
            };
            return PartialView("~/Views/Performance/Partial Views/AppointmentVouchersLines.cshtml", CALines);
        }
        catch (Exception ex)
        {
            Error erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }

    public ActionResult NewAppointmentVoucher()
    {
        try
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                CommitteeAppointmentVouchers appointmentVoucher = new CommitteeAppointmentVouchers();
                Session["httpResponse"] = null;
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                //pmmu.Created_By = employeeView.UserID;

                #region EvaluationCommittee
                List<DropdownList> EvaluationCommittee = new List<DropdownList>();
                string pageEC = "CommitteeAppointmentVouchers?$format=json";

                HttpWebResponse httpResponseEC = Credentials.GetOdataData(pageEC);
                using (var streamReader = new StreamReader(httpResponseEC.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList = new DropdownList();
                        dropdownList.Text = (string)config["Name"] + " (" + (string)config["Document_No"] + ")";
                        dropdownList.Value = (string)config["Document_No"];
                        EvaluationCommittee.Add(dropdownList);
                    }
                }
                #endregion

                /* appointmentVoucher.ListOfImplementingUnit = ImplementingUnit.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Text,
                                      Value = x.Value
                                  }).ToList();*/

                return PartialView("~/Views/Performance/Partial Views/NewAppointmentVoucher.cshtml", appointmentVoucher);
            }
        }

        catch (Exception ex)
        {
            Error erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }
    public JsonResult SubmitCommitteeAppointmentVouchers(CommitteeAppointmentVouchers newAppointmentVoucher)
    {
        try
        {
            // Default values
            string Document_Type = newAppointmentVoucher.Document_Type ?? "";
            int Document_Type_No = int.Parse(Document_Type);


            // Handle Document Date
            DateTime DocDate = DateTime.Now;
            if (!string.IsNullOrEmpty(newAppointmentVoucher.Document_Date))
            {
                DateTime.TryParseExact(newAppointmentVoucher.Document_Date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DocDate);
            }

            // Handle Description
            string Description = newAppointmentVoucher.Description ?? "";

            // Handle Appointment Effective Date
            DateTime AppointmentEffectiveDate = DateTime.Now;
            if (!string.IsNullOrEmpty(newAppointmentVoucher.Appointment_Effective_Date))
            {
                DateTime.TryParseExact(newAppointmentVoucher.Appointment_Effective_Date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out AppointmentEffectiveDate);
            }

            // Handle Tenure End Date
            DateTime TenureEndDate = DateTime.Now;
            if (!string.IsNullOrEmpty(newAppointmentVoucher.Tenure_End_Date))
            {
                DateTime.TryParseExact(newAppointmentVoucher.Tenure_End_Date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out TenureEndDate);
            }

            // Handle Venue and Additional Brief
            string Venue = newAppointmentVoucher.Venue ?? "";
            string Additional_Brief = newAppointmentVoucher.Additional_Brief ?? "";

            // Get Staff No from session
            string staffNo = Session["Username"].ToString();
            EmployeeView employee = Session["EmployeeData"] as EmployeeView;
            string userId = employee?.No ?? "";

            // Insert the appointment into the system
            string docNo2 = Credentials.ObjNav.insertAppointmentComittee(
                Document_Type_No,
                DocDate,
                Description,
                AppointmentEffectiveDate,
                TenureEndDate,
                Venue,
                Additional_Brief
            );

            // Check if insertion was successful
            if (!string.IsNullOrEmpty(docNo2))
            {
                string Redirect = docNo2;
                return Json(new { message = Redirect, success = true, Redirect }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Error submitting data. Try again.", success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public ActionResult UpdateCommitteeHeader(CommitteeAppointmentVouchers record)
    {
        try
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                CommitteeAppointmentVouchers appointmentCommittee = new CommitteeAppointmentVouchers();
                Session["httpResponse"] = null;
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                //appointmentCommittee.Responsible_Employee_No = employeeView.No;
                //appointmentCommittee.Employee_Name = employeeView.FirstName + " " + employeeView.LastName + " (" + employeeView.No + ")";
                return PartialView("~/Views/Performance/Partial Views/UpdateCommitteeHeader.cshtml");
            }
        }

        catch (Exception ex)
        {
            Error erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }
    public JsonResult SubmitUpdatedCommitteeHeader(CommitteeAppointmentVouchers newCommittee)
    {
        try
        {
            string Doc_No = newCommittee.Document_No ?? "";

            // Default values
            string Document_Type = newCommittee.Document_Type ?? "1";
            int Document_Type_No = int.Parse(Document_Type);

            // Handle Document Date
            DateTime DocDate = DateTime.Now;
            if (!string.IsNullOrEmpty(newCommittee.Document_Date))
            {
                bool isParsed = DateTime.TryParseExact(newCommittee.Document_Date,
                    "yyyy-MM-dd",  // Date-only format
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime parsedDate);

                if (isParsed)
                {
                    // Combine parsed date with current time
                    TimeSpan currentTime = DateTime.Now.TimeOfDay;
                    DocDate = parsedDate.Add(currentTime);
                }
                else
                {
                    // Handle parsing failure (optional)
                    Console.WriteLine($"Failed to parse date: {newCommittee.Appointment_Effective_Date}");
                }
            }

            // Handle Description
            string Description = newCommittee.Description ?? "";

            // Handle Appointment Effective Date
            DateTime AppointmentEffectiveDate = DateTime.Now;
            if (!string.IsNullOrEmpty(newCommittee.Appointment_Effective_Date))
            {
                bool isParsed = DateTime.TryParseExact(newCommittee.Appointment_Effective_Date,
                    "yyyy-MM-dd",  // Date-only format
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime parsedDate);

                if (isParsed)
                {
                    // Combine parsed date with current time
                    TimeSpan currentTime = DateTime.Now.TimeOfDay;
                    AppointmentEffectiveDate = parsedDate.Add(currentTime);
                }
                else
                {
                    // Handle parsing failure (optional)
                    Console.WriteLine($"Failed to parse date: {newCommittee.Appointment_Effective_Date}");
                }
            }

            // Handle Tenure End Date
            DateTime TenureEndDate = DateTime.Now;

            if (!string.IsNullOrEmpty(newCommittee.Tenure_End_Date))
            {
                bool isParsed = DateTime.TryParseExact(newCommittee.Tenure_End_Date,
                    "yyyy-MM-dd",  // Date-only format
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime parsedDate);

                if (isParsed)
                {
                    // Combine parsed date with current time
                    TimeSpan currentTime = DateTime.Now.TimeOfDay;
                    TenureEndDate = parsedDate.Add(currentTime);
                }
                else
                {
                    // Handle parsing failure (optional)
                    Console.WriteLine($"Failed to parse date: {newCommittee.Tenure_End_Date}");
                }
            }

            // Handle Venue and Additional Brief
            string Venue = newCommittee.Venue ?? "";
            string Additional_Brief = newCommittee.Additional_Brief ?? "";

            string staffNo = Session["Username"].ToString();
            EmployeeView employee = Session["EmployeeData"] as EmployeeView;
            string userId = employee.UserID;

            string docNo = "";
            docNo = Credentials.ObjNav.UpdateAppointmentComittee(
                Doc_No,
                Document_Type_No,
                DocDate,
                Description,
                AppointmentEffectiveDate,
                TenureEndDate,
                Venue,
                Additional_Brief
            );

            if (docNo != "")
            {
                string Redirect = docNo;
                //Session["SuccessMsg"] = "Purchase Requisition, Document No: " + DocNo + ", created Successfully. Add line(s) and attachment(s) then sent for approval";
                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Error submitting record. Try again.", success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public ActionResult NewAppointmentCommitteeLine(string docNo, string docType)
    {
        try
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                CommitteeAppointmentLines appointmentCommittee = new CommitteeAppointmentLines();
                Session["httpResponse"] = null;
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                #region ListOfMembers

                List<DropdownList> ListOfMemberNo = new List<DropdownList>();
                string pageMemberNo = "Resources?$filter=Resource_Group_No eq 'JSG 6'&$format=json";


                HttpWebResponse httpResponseMemberNo = Credentials.GetOdataData(pageMemberNo);
                using (var streamReader = new StreamReader(httpResponseMemberNo.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        DropdownList Member_No = new DropdownList();
                        Member_No.Text = (string)config["No"] + " - " + (string)config["Name"];
                        Member_No.Value = (string)config["No"];
                        ListOfMemberNo.Add(Member_No);
                    }
                }
                #endregion

                appointmentCommittee.ListOfMembers = ListOfMemberNo.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();
                appointmentCommittee.Document_No = docNo;
                appointmentCommittee.Document_Type = docType;
                return PartialView("~/Views/Performance/Partial Views/NewAppointmentCommitteeLine.cshtml", appointmentCommittee);

            }
        }
        catch (Exception ex)
        {
            Error erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }
    [HttpPost]
    public JsonResult SubmitCommitteeAppointmentLine(CommitteeAppointmentLines newCommitteeAppointmentLine)
    {
        try
        {
            string staffNo = Session["Username"]?.ToString();
            EmployeeView employee = Session["EmployeeData"] as EmployeeView;

            string Document_No = newCommitteeAppointmentLine.Document_No;
            string Document_Type = newCommitteeAppointmentLine.Document_Type;
            int Document_Type_No = 0;
            switch (Document_Type)
            {
                case "Appointment":
                    Document_Type_No = 1;
                    break;
                case "Termination":
                    Document_Type_No = 2;
                    break;
                case "PMMU":
                    Document_Type_No = 3;
                    break;
                case "PAS":
                    Document_Type_No = 4;
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    Document_Type_No = 0;
                    break;
            }
            //int Document_Type_No = int.Parse(Document_Type);
            string Member_No = newCommitteeAppointmentLine.Member_No;
            string Member_Name = newCommitteeAppointmentLine.Member_Name;
            string Role = newCommitteeAppointmentLine.Role;
            int Member_Role = int.Parse(Role);

            Credentials.ObjNav.CreateAppointmentLines(
                Document_No,
                Document_Type_No,
                Member_No,
                Member_Role
            );
            string redirect = Document_No;
            return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult GetMemberDetails(string Member_No)
    {
        try
        {
            #region MemberDetailsLookup
            List<DropdownList> NamesLookup = new List<DropdownList>();
            List<DropdownList> EmailLookup = new List<DropdownList>();

            string pageDetails = $"Resources?$filter=No eq '{Member_No}'&$format=json";
            HttpWebResponse httpResponseDetails = Credentials.GetOdataData(pageDetails);
            using (var streamReader = new StreamReader(httpResponseDetails.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {

                    DropdownList names = new DropdownList();
                    names.Text = (string)config["Name"];
                    names.Value = (string)config["Name"];
                    NamesLookup.Add(names);

                    DropdownList email = new DropdownList();
                    email.Text = (string)config["Name"];
                    email.Value = (string)config["Name"];
                    EmailLookup.Add(email);
                }
            }

            #endregion

            // Create and return the JSON result
            var response = new
            {
                ListOfMemberNames = NamesLookup.Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList(),

                ListOfMemberEmail = EmailLookup.Select(x => new SelectListItem
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
            return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public string CheckPASDocStage()
    {
        try
        {
            string pageGenSetup = "GeneralSetup?$format=json";
            HttpWebResponse httpResponseGenSetup = Credentials.GetOdataData(pageGenSetup);
            using var streamReader = new StreamReader(httpResponseGenSetup.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);
            string pasDocStage = (string)details["value"][0]["PAS_Document_Stage"];
            pasDocStage = string.IsNullOrWhiteSpace(pasDocStage) ? "Initial stage" : pasDocStage;
            return pasDocStage;
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message.Replace("'", "");
        }
    }
    public List<SelectListItem> GetActivityDropdown(string source, string contractNo)

    {
        EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
        string positionCode = employeeView.Current_Position_ID;
        List<SelectListItem> activityDropdown = new List<SelectListItem>();
        string page = source == "PMMU"
            ? $"PMMUOutputActivities?$filter=Contract_No eq '{contractNo}'"
            : $"PositionTargetList?$filter=Position_Code eq '{positionCode}'";

        try
        {

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);


            foreach (JObject config in details["value"])
            {
                SelectListItem dropdownItem = new SelectListItem
                {
                    Text = source == "PMMU" ? (string)config["Activities"] : (string)config["Strategic_Objective"],
                    Value = source == "PMMU" ? (string)config["Activities"] : (string)config["Line_No"]
                };
                activityDropdown.Add(dropdownItem);
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine("Error fetching activity data: " + ex.Message);
        }

        return activityDropdown;
    }
    public List<SelectListItem> GetUnitsOfMeasureDropdown()
    {
        List<SelectListItem> unitsOfMeasureDropdown = new List<SelectListItem>();
        string page = "UnitsOfMeasure";

        try
        {
            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);


            foreach (JObject config in details["value"])
            {
                SelectListItem dropdownItem = new SelectListItem
                {
                    Text = (string)config["Description"],
                    Value = (string)config["Code"]
                };
                unitsOfMeasureDropdown.Add(dropdownItem);
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine("Error fetching Units of Measure: " + ex.Message);
        }

        return unitsOfMeasureDropdown;
    }

    public ActionResult PositionTargetList()
    {
        List<PositionTargetList> list = new List<PositionTargetList>();
        try
        {
            string page = "PositionTargetList";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var results = streamReader.ReadToEnd();

                var details = JObject.Parse(results);

                foreach (JObject config in details["value"])
                {
                    PositionTargetList target = new PositionTargetList();
                    target.Position_Code = (string)config["Position_Code"];
                    target.Strategic_Objective = (string)config["Strategic_Objective"];
                    list.Add(target);
                }

            }

        }
        catch (Exception e)
        {
            throw;
        }
        return View(list);
    }

}