using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{

    public class TrainingRequisition
    {
       
        public string Code { get; set; }
        public string Training_Plan_No { get; set; }
        public string Employee_Department { get; set; }
        public string Course_Title { get; set; }
        public string Description { get; set; }
        public string Start_DateTime { get; set; }
        public string End_DateTime { get; set; }
        public int Duration { get; set; }
        public string Duration_Type { get; set; }
        public string Training_Region { get; set; }
        public int Cost { get; set; }
        public string Training_Venue_Region_Code { get; set; }
        public string Training_Venue_Region { get; set; }
        public string Training_Responsibility_Code { get; set; }
        public string Training_Responsibility { get; set; }
        public string Location { get; set; }
        public string Provider { get; set; }
        public string Provider_Name { get; set; }
        public string Training_Type { get; set; }
        public int Training_Duration_Hrs { get; set; }
        public int Planned_Budget { get; set; }
        public int Planned_No_to_be_Trained { get; set; }
        public int No_of_Participants { get; set; }
        public int Total_Procurement_Cost { get; set; }
        public int Other_Costs { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Created_By { get; set; }
        public string Created_On { get; set; }
        public string Status { get; set; }
        public bool Bonded { get; set; }
        public string Bonding_Period { get; set; }
        public string Expected_Bond_End { get; set; }
        public string Expected_Bond_Start { get; set; }
        public int Bond_Penalty { get; set; }

        public List<SelectListItem> ListOfTrainingPlan { get; set; }


        public List<SelectListItem> ListOfResponsibilityCenters { get; set; }
        public List<SelectListItem> ListOfTrainingCourses { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
        public List<SelectListItem> ListOfRegions { get; set; }
        public List<SelectListItem> ListOfProviders { get; set; }
    }

    public class TrainingParticipantsList
    {
        
        public int Line_No { get; set; }
        public string Training_Code { get; set; }
        public string Employee_Code { get; set; }
        public string Training_Responsibility_Code { get; set; }
        public string Employee_Name { get; set; }
        public string Type { get; set; }
        public string Destination { get; set; }
        public int No_of_Days { get; set; }
        public int Total_Amount { get; set; }
        public string Training_Responsibility { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Witness { get; set; }
        public string Witness_Name { get; set; }
        public bool Charge_Levy { get; set; }
        public string Requestor { get; set; }

        public List<SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfExpenseTypes { get; set; }
        public List<SelectListItem> ListOfRegions { get; set; }
    }


    public class TrainingNeedRequests
    {
        public string Code { get; set; }
        public string Financial_Year { get; set; }
        public string Duty_Station { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Department { get; set; }
        public string Job_Title { get; set; }
        public bool Disabled { get; set; }
        public string Description { get; set; }
        public string Employment_Date { get; set; }

        public string Training_Plan_No { get; set; }
        public string Status { get; set; }
        public int Estimate_Cost { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_On { get; set; }
        public bool Submitted { get; set; }
        public string Course_ID { get; set; }
        public string Course_Description { get; set; }
        public string Source { get; set; }
        public string Comments { get; set; }
    }
    public class TrainingNeedsLines
    {
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public string Duty_Station_code { get; set; }
        public string Employee_No { get; set; }
        public string Financial_Year { get; set; }
        public string Employee_Name { get; set; }
        public string Domain { get; set; }
        public string Domain_Name { get; set; }
        public string Course_ID { get; set; }
        public string Course_Description { get; set; }
        public int CPD_Points { get; set; }
        public string Course_Provider { get; set; }
        public string Provider_Name { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public int Total_Cost { get; set; }
        public string Source { get; set; }
        public string Comments { get; set; }
        public int No_of_Linked_Objectives { get; set; }
    }
    public class TrainingCost
    {
        public string Training_ID { get; set; }
        public int Entry_No { get; set; }
        public string Course_ID { get; set; }
        public string Cost_Category { get; set; }
        public string Cost_Item { get; set; }
        public string Item_Category { get; set; }
        public string Service_Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Unit_of_Measure { get; set; }
        public int Unit_Cost_LCY { get; set; }
        public int Quantity { get; set; }
        public int Line_Amount { get; set; }
    }
    public class TrainingRequisitionViewModel
    {
        public string DocNo { get; set; }
        public string CourseID { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; } = new();
    }
    public class TrainingLineViewModel
    {
        public string DocumentNo { get; set; }
        public string EmployeeNo { get; set; }
        public string DomainId { get; set; }
        public string CourseID { get; set; }

        public string CourseDescription { get; set; }
        public int Points { get; set; }
        public string CourseProvider { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        // Dropdown Lists
        public List<SelectListItem> ListOfEmployees { get; set; } = new();
        public List<SelectListItem> ListOfDomains { get; set; } = new();
        public List<SelectListItem> ListOfProviders { get; set; } = new();
        public List<SelectListItem> ListOfCourses { get; set; } = new();
    }
    public class TrainingEvaluationViewModel
    {
        public string EmployeeNo { get; set; }
        public string TrainingApplicationCode { get; set; }
        public List<SelectListItem> trainigApplicationList { get; set; } = new();
    }
    public class TrainingCostViewModel
    {
        public string DocumentNo { get; set; }
        public string CourseID { get; set; }
        public string CostCategory { get; set; }
        public string CostItem { get; set; }
        public decimal UnitCost { get; set; }
        public int Quantity { get; set; }

        // Dropdown Lists
        public List<SelectListItem> ListOfCourses { get; set; } = new();
        public List<SelectListItem> ListOfCostCategories { get; set; } = new();
        public List<SelectListItem> ListOfProcurableItems { get; set; } = new();
        public List<SelectListItem> ListOfOtherCostItems { get; set; } = new();
    }
    public class TrainingLine
    {
        public string DocNo { get; set; }
        public string EmpNo { get; set; }
        public string DomainId { get; set; }
        public string CourseID { get; set; }
        public string CourseDescription { get; set; }
        public decimal Points { get; set; }
        public string CourseProvider { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class TrainingRequisitionCreate
    {
        public string DocNo { get; set; }
        public string EmpNo { get; set; }
        public string CourseID { get; set; }
    }
    public class TrainingApplications
    {
        public string Code { get; set; }
        public string CourseTitle { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Status { get; set; }
        public int Duration { get; set; }
        public decimal Cost { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string Provider { get; set; }
        public string EmployeeNo { get; set; }
        public string ApplicationDate { get; set; }
        public string NoSeries { get; set; }
        public string EmployeeDepartment { get; set; }
        public string EmployeeName { get; set; }
        public string ProviderName { get; set; }
        public int NoOfParticipants { get; set; }
        public decimal ApprovedCost { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ActualStartDate { get; set; }
        public string ActualEndDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public bool ImprestCreated { get; set; }
        public decimal TrainingPlanCost { get; set; }
        public decimal Budget { get; set; }
        public decimal Actual { get; set; }
        public decimal Commitment { get; set; }
        public string GLAccount { get; set; }
        public string BudgetName { get; set; }
        public decimal AvailableFunds { get; set; }
        public string Local { get; set; }
        public bool RequiresFlight { get; set; }
        public string SupervisorNo { get; set; }
        public string SupervisorName { get; set; }
        public string TrainingPlanNo { get; set; }
        public string TrainingType { get; set; }
        public string Institution { get; set; }
        public string TrainingNeed { get; set; }
        public string WorkStation { get; set; }
        public string TrainingVenueRegionCode { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfCourses { get; set; }
        public List<SelectListItem> ListOfTrainers { get; set; }
        public List<SelectListItem> ListOfTrainingPlan { get; set; }
        public List<SelectListItem> ListOfTrainingVeneu { get; set; }
        public List<SelectListItem> ListOfCostCentre { get; set; }
    }
    public class TrainingEvaluation
    {
        public string No { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Department { get; set; }
        public string Job_Title { get; set; }
        public string Application_Code { get; set; }
        public string Course_Title { get; set; }
        public DateTime Start_DateTime { get; set; }
        public DateTime End_DateTime { get; set; }
        public string Venue { get; set; }
        public int No_of_Participants { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_On { get; set; }
        public string HOD_Remarks { get; set; }
        public string HR_Remarks { get; set; }
        public string Status { get; set; }
    }
    public class TrainingEvaluationLine
    {
        public string Training_Evaluation_No { get; set; }
        public string Training_Category { get; set; }
        public int Entry_No { get; set; }
        public string Category_Description { get; set; }
        public string Rating_Code { get; set; }
        public string Rating_Description { get; set; }
        public string Comments { get; set; }
        public List<SelectListItem> ListOfCategories { get; set; } = new();
    }


    public class TrainingAssessment
    {
        
        public string No { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Department { get; set; }
        public string Job_Title { get; set; }
        public string Application_Code { get; set; }
        public string Course_Title { get; set; }
        public string Start_DateTime { get; set; }
        public string End_DateTime { get; set; }
        public string Venue { get; set; }
        public int No_of_Participants { get; set; }
        public string Created_By { get; set; }
        public string Created_On { get; set; }
        public string Status { get; set; }

        public List<SelectListItem> ListOfApplicationCodes { get; set; }
    }









}