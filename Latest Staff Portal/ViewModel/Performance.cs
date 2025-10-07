using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel;

public class IndividualScoreCard
{
    public string Document_Stage;
    public string Supervisors_Employee_Name;
    public string Current_Position_Id { get; set; }
    public string DocNo { get; set; }
    public string comment { get; set; }

    public string Admin_Unit { get; set; }
    public string Admin_Unit_Name { set; get; }
    public string Responsible_Employee_No { get; set; }
    public string Employee_Name { get; set; }
    public string Functional_Template_ID { get; set; }
    public string Strategic_Plan_ID { get; set; }
    public string Strategy_Plan_ID { get; set; }
    public string Contract_Year { get; set; }
    public string Goal_Template_ID { get; set; }
    public string Approval_Status { get; set; }
    public string Status { get; set; }
    public string Change_Status { get; set; }
    public string Start_Date { get; set; }
    public string End_Date { get; set; }
    public string Designation { get; set; }
    public string Grade { get; set; }
    public string Responsibility_Center { get; set; }
    public string Responsibility_Center_Name { get; set; }
    public bool Blocked_x003F_ { get; set; }
    public string Created_By { get; set; }
    public string Created_On { get; set; }
    public decimal Total_Assigned_Weight_Percent { get; set; }
    public decimal Secondary_Assigned_Weight_Percent { get; set; }
    public string Populate_Activities_From { get; set; }
    public decimal JD_Assigned_Weight_Percent { get; set; }
    public string Department { get; set; }
    public string Directorate { get; set; }
    public List<SelectListItem> ListOfPMMU { get; set; }
    public List<SelectListItem> ListOfResponsibilityCentre { get; set; }
    public List<SelectListItem> ListOfDirectorate { get; set; }
    public List<SelectListItem> ListOfDepartment { get; set; }
    public List<SelectListItem> ListOfCostCentre { get; set; }
    public bool isDirector { get; set; }
}


public class AppraisalsList
{
    public string Appraisal_No { get; set; }
    public string Employee_No { get; set; }
    public string Employee_Name { get; set; }
    public string User_ID { get; set; }
    public string Job_Title { get; set; }
    public string Job_Description { get; set; }
    public string Appraisal_Type { get; set; }
    public string Appraisal_Period { get; set; }
    public string Status { get; set; }
    public string Appraisal_Stage { get; set; }
    public string Sent { get; set; }
    public string Supervisor { get; set; }
}


public class AppraisalsCard
{
    
    public string Appraisal_No { get; set; }
    public string Employee_No { get; set; }
    public string Employee_Name { get; set; }
    public string User_ID { get; set; }
    public string Dept_Code { get; set; }
    public string Department { get; set; }
    public string Job_Title { get; set; }
    public string Job_Description { get; set; }
    public string Supervisor { get; set; }
    public string Supervisor_Name { get; set; }
    public string Second_Supervisor { get; set; }
    public string Second_Supervisor_Name { get; set; }
    public string Terms_of_Service { get; set; }
    public string Appraisal_Period { get; set; }
    public string Appraisal_Date { get; set; }
    public string Evaluation_Period_Start { get; set; }
    public string Evaluation_Period_End { get; set; }
    public string Appraisal_Stage { get; set; }
    public string Appraisal_Method { get; set; }
    public string Appraisal_Approval_Status { get; set; }
    public int Appraisal_Score { get; set; }
    public int Staff_Attributes_Evaluation_Score { get; set; }
    public string Status { get; set; }
    public string Comments_Supervisor { get; set; }

    public string Return_Comments { get; set; }
    
    public List<SelectListItem> ListOfPeriods { get; set; }
}


public class PositionResponsibilities
{
    
    public string Position_ID { get; set; }
    public int Line_No { get; set; }
    public string Type { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
}

public class OtherDuties
{
    
    public string Appraisal_No { get; set; }
    public string Appraisal_Period { get; set; }
    public string Employee_No { get; set; }
    public int Line_No { get; set; }
    public string Duties { get; set; }
}
public class PersonalGoalsObjectives
{ 
    public string Appraisal_No { get; set; }
    public string Appraisal_Period { get; set; }
    public string Employee_No { get; set; }
    public string Categorize_As { get; set; }
    public int Line_No { get; set; }
    public string Sub_Category { get; set; }
    public string Perfomance_Goals_and_Targets { get; set; }
    public int Min_Target_Score { get; set; }
    public int Max_Target_Score { get; set; }
    public int Target { get; set; }
    public int Weight { get; set; }
    public string Key_Perfomance_Indicator { get; set; }
    public decimal Self_Rating { get; set; }

    public decimal Moderated_Score { get; set; }
    public string Employee_Comments { get; set; }
    public decimal Supervisor_Rating { get; set; }
    public string Supervisor_Comments { get; set; }

}

public class KpiAppraisal
{
    public int Entry_No { get; set; }
    public string Appraisal_No { get; set; }
    public string Perfomance_Goals_and_Targets { get; set; }
    public string KPI { get; set; }
    public decimal Weight { get; set; }
}



public class StaffAttributesEvaluation
{
   
    public string Appraisal_No { get; set; }
    public string Appraisal_Period { get; set; }
    public string Employee_No { get; set; }
    public string Categorize_As { get; set; }
    public int Line_No { get; set; }
    public string Sub_Category { get; set; }
    public string Perfomance_Goals_and_Targets { get; set; }
    public int Min_Target_Score { get; set; }
    public int Max_Target_Score { get; set; }
    public int Self_Rating { get; set; }
    public string Employee_Comments { get; set; }
    public string Supervisor_Rating { get; set; }
    public string Supervisor_Comments { get; set; }
}


public class PerformanceTargetLines
{
    public string responsibleEmployee;
    public bool Capability { get; set; }
    public string DocNo { get; set; }
    public string PMMU_No { get; set; }
    public int Entry_No { get; set; }
    public string Activity { get; set; }
    public List<SelectListItem> ListOfActivity { get; set; }
    public List<SelectListItem> ListOfPositionTarget { get; set; }
    public List<SelectListItem> ListUnitsOfMeasure { get; set; }

    public List<SelectListItem> ListOfActivities { get; set; }

    public string Appraisee_Objective { get; set; }
    public string PAS_Activity { get; set; }
    public string Individual_Target { get; set; }
    public string Performance_Indicator { get; set; }
    public decimal Weight { get; set; }
    public int Target { get; set; }
    public decimal Score { get; set; }
    public string Source { get; set; }
    public int Self_Assessment_Target { get; set; }
    public int Joint_Agreed_Target { get; set; }
    public int Self_Assessment_Mid_year { get; set; }
    public int Supervisor_Assessment_Mid_year { get; set; }
    public decimal Weighted_Score { get; set; }
    public string Unit_of_Measure { get; set; }
    public string Populate_Activities_From { get; set; }

    public string pas_indicator { get; set; }
    public string stage { get; set; }

}

public class PASCoreValuesLines
{
    public bool capability { get; set; }
    public string Strategy_Plan_ID { get; set; }
    public string Core_Value { get; set; }
    public string Performance_Contract_Header { get; set; }
    public string Behavioural_expectation { get; set; }
    public string Key_Performance_Indicator { get; set; }
    public int Self_Assessment { get; set; }
    public int Joint_Assessment { get; set; }
    public string Appraisers_Comments { get; set; }
    public string Appraisee_Comments { get; set; }
    public List<SelectListItem> ListOfCoreValues { get; set; }
    public string Score { get; set; }
    public string DocNo { get; set; }
    public string Strategic_Plan_ID { get; set; }
    public int Line_No { get; set; }
    public string stage { get; set; }
}


public class ValuesActivitiesDropdown
{
    string Value { get; set; }
    string Text { get; set; }
    string Staj_ID { get; set; }
}


//*******************************************************
//PMMU
//*******************************************************
public class PMMUCard
{
    public string No { get; set; }

    public int NumberOfCopies { get; set; }
    public string Responsible_Employee_No { get; set; }
    public string Employee_Name { get; set; }
    public string Description { get; set; }
    public string Strategy_Plan_ID { get; set; }
    public string Functional_Template_ID { get; set; }
    public string Contract_Year { get; set; }
    public string Goal_Template_ID { get; set; }
    public string Target_Setting_Commitee { get; set; }
    public string Evaluation_Committee { get; set; }
    public string Approval_Status { get; set; }
    public string Status { get; set; }
    public int TotalWeight { get; set; }
    public string Date_Approved { get; set; } // Use DateTime? for dates
    public string PMMU_Document_Stage { get; set; }
    public string Change_Status { get; set; }
    public string Start_Date { get; set; } // Use DateTime? for dates
    public string End_Date { get; set; } // Use DateTime? for dates
    public string Designation { get; set; }
    public string Grade { get; set; }
    public string Admin_Unit { get; set; }
    public string Admin_Unit_Name { get; set; }
    public bool Blocked_x003F_ { get; set; }
    public string Created_By { get; set; }
    public string Created_On { get; set; } // Use DateTime? for dates

    public DateTime Created_On_2 { get; set; }
    public int Total_Assigned_Weight_Percent { get; set; }
    public string Strategic_Objective { get; set; }
    public int Secondary_Assigned_Weight_Percent { get; set; }
    public int JD_Assigned_Weight_Percent { get; set; }

    // Lists for committee and strategy plan details
    public List<SelectListItem> ListOfNegotiationCommittee { get; set; }
    public List<SelectListItem> ListOfEvaluationCommittee { get; set; }
    public List<SelectListItem> ListOfImplementingUnit { get; set; }
    public List<SelectListItem> ListOfStrategyPlanID { get; set; }

    // Committee members details
    public string Member_Email { get; set; }
    public string Member_Name { get; set; }
    public string Role { get; set; }
    public string Member_No { get; set; }

    // Lists for committee members
    public List<SelectListItem> ListOfNegotiationCommitteeMembers { get; set; }
    public List<SelectListItem> ListOfEvaluationCommitteeMembers { get; set; }
}


public class ImplementationYears
{
    public string Description { get; set; }
    public string Primary_Theme { get; set; }
    public string Strategy_Framework { get; set; }
    public string Start_Date { get; set; }
    public string End_Date { get; set; }
    public string Duration_Years { get; set; }
    public string Vision_Statement { get; set; }
    public string Mission_Statement { get; set; }
    public string Implementation_Status { get; set; }
    public string Approval_Status { get; set; }
}

public class PMMULines
{

    public string Contract_No { get; set; }
    public string Strategy_Plan_ID { get; set; }
    public string Theme_ID { get; set; }
    public int Entry_No { get; set; }
    public string Outcome { get; set; }
    public string Strategic_Objective { get; set; }
    public string Strategies { get; set; }
    public string Perspectives { get; set; }
    public string Strategy_Output_Code { get; set; }
    public string Outputs { get; set; }
    public string Output_Indicators { get; set; }
    public string Activities { get; set; }
    public string ActivitySubWeight { get; set; }
    public string ActivitiesAchievedtarget { get; set; }
    public List<string> ActivitiesArray { get; set; }
    public List<string> SubWeightsArray { get; set; }
    public string Key_result_Areas { get; set; }
    public string Key_Indicators { get; set; }
    public string Unit_of_Measure { get; set; }
    public string Baseline_Target { get; set; }
    public int National_Average { get; set; }
    public int Best_Achievement { get; set; }
    public int Weight { get; set; }
    public int TotalWeight { get; set; }
    public int Target { get; set; }
    public int Score { get; set; }  //net weight
    public int Achieved_Target { get; set; }
    public string Comments { get; set; }
    public List<SelectListItem> ListOfOutcomes { get; set; }
    public List<SelectListItem> ListOfStrategicObjectives { get; set; }
    public List<SelectListItem> ListOfStrategies { get; set; }
    public List<SelectListItem> ListOfPerspectives { get; set; }
    public List<SelectListItem> ListOfUOM { get; set; }

    public List<SelectListItem> ListOfActivities { get; set; }
    public List<Tuple<string, decimal>> ActivitiesAndSubWeightsArray { get; set; }

}



public class UnitsOfMeasure
{
    public string odataetag { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string International_Standard_Code { get; set; }
    public bool Coupled_to_CRM { get; set; }
}

public class PMMULinesList
{
    public string PMMU_Document_Stage { get; set; }
    public string Approval_Status { get; set; }
    public string Status { get; set; }
    public int TotalWeight { get; set; }
    public List<PMMULines> ListOfPMMULines { get; set; }
}

public class PMMULinesActivities
{
    public string Contract_No { get; set; }
    public int Entry_No { get; set; }
    public int Entry { get; set; }
    public string Strategy_Plan_ID { get; set; }
    public string Strategy_Output_Code { get; set; }
    public string Activities { get; set; } // Ensure this matches the JavaScript structure
    public decimal Weight { get; set; }
    public string Document_Stage { get; set; }
    public decimal SubWeights { get; set; } // Changed to decimal for consistency
    public decimal AchievedTargets { get; set; } // Changed to decimal for consistency

    public List<string> ActivitiesArray { get; set; }
    public List<decimal> SubWeightsArray { get; set; }
    public List<decimal> AchievedTargetsArray { get; set; }
}



//*******************************************************
//Committee Appointment Vouchers
//*******************************************************
public class CommitteeAppointmentVouchers
{
    public string Document_No { get; set; }
    public string Document_Type { get; set; }
    public List<SelectListItem> ListOfDocumentTypes { get; set; }
    public string Committee_Type_ID { get; set; }
    public string Vacancy_ID { get; set; }
    public string Document_Date { get; set; }
    public string Description { get; set; }
    public string Appointment_Effective_Date { get; set; }
    public string Tenure_End_Date { get; set; }
    public string Appointing_Authority { get; set; }
    public string Raised_By { get; set; }
    public string Staff_ID { get; set; }
    public string Name { get; set; }
    public string Terms_of_Reference { get; set; }
    public string Additional_Brief { get; set; }
    public string Venue { get; set; }
    public string Approval_Status { get; set; }
}

public class CommitteeAppointmentLines
{
    public string Document_No { get; set; }
    public int Line_No { get; set; }
    public string Document_Type { get; set; }
    public string Member_No { get; set; }
    public string Role { get; set; }
    public string Member_Name { get; set; }
    public string Member_Email { get; set; }

    public List<SelectListItem> ListOfMembers { get; set; }


}

public class CommitteeAppointmentLinesList
{
    public string Status { get; set; }
    public List<CommitteeAppointmentLines> ListOfCommitteeAppointmentLines { get; set; }
}

public class PASTrainingNeedsLine
{
    public string DocNo { get; set; }
    public int Line_No { get; set; }
    public string Training_Need { get; set; }
    public string Supervisors_Remarks { get; set; }
    public string stage { get; set; }


}

public class Evidence
{
    public string DocNo { get; set; }
    public int Line_No { get; set; }
    public string Objective { get; set; }
    public string PAS_Stage { get; set; }
    public string Document_Stage { get; set; }
}
public class PositionTargetList
{
    public string Position_Code { get; set; }
    public int Line_No { get; set; }
    public string Strategic_Objective { get; set; }
    public string KPI { get; set; }
    public string Unit_of_Measure { get; set; }
    public decimal Target { get; set; }
    public decimal Assigned_Weight_Percent { get; set; }
}
