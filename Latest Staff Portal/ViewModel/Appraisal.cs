using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class ANUPEAisalCardList
    {
        public string ApprisalCode { get; set; }
        public string StaffNo { get; set; }
        public string StaffName { get; set; }
        public string ApprisalPeriod { get; set; }
        public string ANUPEAisalType { get; set; }
        public string ANUPEAisalDate { get; set; }
        public string ANUPEAisalStartDate { get; set; }
        public string ANUPEAisalEndDate { get; set; }
        public string AppointmentDate { get; set; }
        public string Designation { get; set; }
        public string ANUPEAisalStage { get; set; }
        public string RespCenter { get; set; }
        public string Department { get; set; }
        public string UserID { get; set; }
        public string Status { get; set; }
        public string Open_To { get; set; }
        public bool AllowTargetSetting { get; set; }
        public bool AllowEvaluation { get; set; }
        public bool AllowMidYearReview { get; set; }
        public bool AllowEndYearReview { get; set; }
        public bool Supervisor_ANUPEAised_Q1 { get; set; }
        public bool Supervisor_ANUPEAised_Q2 { get; set; }
        public bool Supervisor_ANUPEAised_Q3 { get; set; }
        public bool Supervisor_ANUPEAised_Q4 { get; set; }
        public string ProbationEndDate { get; set; }
        public string ContractEndDate { get; set; }
        public string Supervisor_Name { get; set; }
        public string Supervisor_Comment { get; set; }
        public string Job_Title { get; set; }
        public string Job_Group { get; set; }
        public Quarters_Setup Qt_Setup { get; set; }
        public List<SelectListItem> ListOfCSP { get; set; }
        public List<SelectListItem> ListOfPMMU { get; set; }
        public List<SelectListItem> ListOfPerformanceTask { get; set; }
        public List<SelectListItem> ListOfScoreCard { get; set; }
        public List<SelectListItem> ListOfPerformanceContract { get; set; }
        public string CSP { get; set; }
        public string PMMP { get; set; }
        public string PerformanceTas { get; set; }
        public string ScoreCard { get; set; }
        public string Comments { get; set; }
        public string PerformanceContract { get; set; }
    }

    public class PerformanceEvaluation
    {
        public string Performance_Evaluation_ID { get; set; }
        public string Line_No { get; set; }
        public string Scorecard_ID { get; set; }
        public string Scorecard_Line_No { get; set; }
        public string Objective_Initiative { get; set; }
        public string Outcome_Performance_Indicator { get; set; }
        public string Unit_of_Measure { get; set; }
        public string Desired_Performance_Direction { get; set; }
        public string Performance_Rating_Scale { get; set; }
        public string Scale_Type { get; set; }
        public string Target_Qty { get; set; }
        public string Self_Review_Qty { get; set; }
        public string ANUPEAiserReview_Qty { get; set; }
        public string Final_Actual_Qty { get; set; }
    }

    public class PerformanceEvaluationList
    {
        public string Status { get; set; }
        public List<PerformanceEvaluation> ListOfPerformanceEvaluation { get; set; }
    }

    public class Quarters_Setup
    {
        public bool Q1 { get; set; }
        public bool Q2 { get; set; }
        public bool Q3 { get; set; }
        public bool Q4 { get; set; }
    }

    public class NewApprisalRequest
    {
        public string ANUPEAisalPeriod { get; set; }
        public string ANUPEAisalTypes { get; set; }
        public string RespC { get; set; }
        public string No { get; set; }
        public List<SelectListItem> ListOfApprisalPeriods { get; set; }
        public List<SelectListItem> ListOfApprisalTypes { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfEmpList { get; set; }
        public List<SelectListItem> ListOfCSP { get; set; }
        public List<SelectListItem> ListOfPMMU { get; set; }
        public List<SelectListItem> ListOfPerformanceTask { get; set; }
        public List<SelectListItem> ListOfScoreCard { get; set; }
        public List<SelectListItem> ListOfPerformanceContract { get; set; }
        public string CSP { get; set; }
        public string PMMP { get; set; }
        public string PerformanceTas { get; set; }
        public string ScoreCard { get; set; }
        public string Comments { get; set; }
        public string PerformanceContract { get; set; }
    }

    public class ANUPEAisalTypes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ANUPEAisalPeriods
    {
        public string Period { get; set; }
        public string No { get; set; }
    }

    public class CSP
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class PMMU
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class PerformanceTask
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ScoreCard
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class PerformanceContract
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class NewANUPEAisalDocument
    {
        public string ApprisalPeriod { get; set; }
        public string ApprisalType { get; set; }
        public string Responsibility { get; set; }
        public string Sup_No { get; set; }
    }

    public class DepartmentalObjectives
    {
        public string ANUPEAisal_No { get; set; }
        public string Objective_Code { get; set; }
        public string Perspective_Code { get; set; }
        public string Perspective_Description { get; set; }
        public string Objective_Description { get; set; }
        public string Ratings { get; set; }
        public string SupervisorRating { get; set; }
    }

    public class PerformanceIndicator
    {
        public string ANUPEAisal_No { get; set; }
        public string Line_No { get; set; }
        public string Objective { get; set; }
        public string KRA { get; set; }
        public string KPI { get; set; }
        public string Target { get; set; }
        public string Weight { get; set; }
        public string Unit { get; set; }
        public string Self_Score { get; set; }
        public string Supervisors_Score { get; set; }
        public string Agreed_Score { get; set; }
        public string ANUPEAisee_Comments { get; set; }
        public string Supervisor_Comments { get; set; }
        public string Level { get; set; }
        public string TimeFrame { get; set; }
        public string NewTimeFrame { get; set; }
        public bool Update { get; set; }
    }

    public class TrainingDev
    {
        public string ANUPEAisal_No { get; set; }
        public string Course_Name { get; set; }
        public string Line_No { get; set; }
        public string Objective { get; set; }
        public string Reason { get; set; }
        public string Action { get; set; }
        public string Duration_of_Course { get; set; }
        public string Expected_Start_Date { get; set; }
        public string Expected_End_Date { get; set; }
        public string Results_Obtained { get; set; }
        public string Remarks_ANUPEAisee { get; set; }
        public string Remarks_Supervisor { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
    }

    public class CompetenceValues
    {
        public string ANUPEAisal_No { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ANUPEAisal_Assesment { get; set; }
        public string Score { get; set; }
        public string Score_Descriptors { get; set; }
        public string Sup_Score { get; set; }
        public string Sup_Score_Descriptors { get; set; }
        public string Agreed_Score { get; set; }
        public string Agreed_Score_Descriptors { get; set; }
        public string Line_No { get; set; }
        public string Level { get; set; }
    }

    public class SectionDetails
    {
        public List<PerformanceIndicator> KPIList { get; set; }
        public string Level { get; set; }
        public bool AllowTargetSetting { get; set; }
        public bool AllowEvaluation { get; set; }
        public bool Supervisor_ANUPEAised { get; set; }
        public string Open_To { get; set; }
    }

    public class BehaviorSections
    {
        public List<CompetenceValues> ValList { get; set; }
        public string Level { get; set; }
        public bool AllowTargetSetting { get; set; }
        public bool AllowEvaluation { get; set; }
        public bool Supervisor_ANUPEAised { get; set; }
        public string Open_To { get; set; }
    }

    public class ExtraInf
    {
        public string ANUPEAisal_No { get; set; }
        public string Entry_Type { get; set; }
        public string Description { get; set; }
        public string Entry_No { get; set; }
    }

    public class EmpExtraInf
    {
        public List<ExtraInf> EmpExtraInfList { get; set; }
        public string Level { get; set; }
        public string Pending_On { get; set; }
        public string Title { get; set; }
        public string Section { get; set; }
    }
}