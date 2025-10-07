using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class LoanApplicationCard
    {
        public string LoanNo { get; set; }
        public string LoanProductType { get; set; }
        public string LoanCategory { get; set; }
        public string Description { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string ApplicationDate { get; set; }
        public decimal AmountRequested { get; set; }
        public string Reason { get; set; }
        public string ApprovalCommittee { get; set; }
        public string LoanStatus { get; set; }
        public DateTime IssuedDate { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string Instalment { get; set; }
        public decimal Repayment { get; set; }
        public decimal FlatRatePrincipal { get; set; }
        public decimal FlatRateInterest { get; set; }
        public decimal InterestRate { get; set; }
        public string InterestCalculationMethod { get; set; }
        public string PayrollGroup { get; set; }
        public bool OpeningLoan { get; set; }
        public decimal TotalRepayment { get; set; }
        public int PeriodRepayment { get; set; }
        public decimal InterestAmount { get; set; }
        public string ExternalDocumentNo { get; set; }
        public int Receipts { get; set; }
        public string HELBNo { get; set; }
        public string UniversityName { get; set; }
        public bool StopLoan { get; set; }
        public string RefinancedFromLoan { get; set; }
        public string DateFilter { get; set; }
        public string BasicPay { get; set; }
        public string NetPay { get; set; }
        public string DirectorComments { get; set; }
    }

    public class LoanApplicationsLIst
    {
        public string LoanNo { get; set; }
        public string LoanProductType { get; set; }
        public string Employee2 { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public DateTime ApplicationDate { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal ApprovedAmount { get; set; }
        public DateTime IssuedDate { get; set; }
        public int Instalment { get; set; }
        public decimal Repayment { get; set; }
        public decimal FlatRatePrincipal { get; set; }
        public decimal FlatRateInterest { get; set; }
        public decimal InterestRate { get; set; }
        public string InterestCalculationMethod { get; set; }
    }

    public class MedicalCardReplacement
    {
        public string DocumentNo { get; set; }
        public string DocumentDate { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public string DependantNo { get; set; }
        public string DependantName { get; set; }
        public string CardNo { get; set; }

        public string DocumentStatus { get; set; }
        public string Status { get; set; }
        public string RequestorID { get; set; }
        public List<SelectListItem> ListOfDependantNo { get; set; }
    }

    public class DependantChangeRequests
    {
        public string No { get; set; }
        public string RequestorID { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public string DocumentDate { get; set; }
        public string TimeCreated { get; set; }
        public string PostedBy { get; set; }
        public string PostingDate { get; set; }
        public string PostingTime { get; set; }
        public string Status { get; set; }
        public string DocumentStatus { get; set; }
        public string ReasonForChange { get; set; }
        public bool Posted { get; set; }
    }

    public class IncidenceReporting
    {
        public string Incident_ID { get; set; }
        public string Risk_Register_Type { get; set; }
        public string Risk_Management_Plan_ID { get; set; }
        public int Risk_ID { get; set; }
        public string Risk_Incident_Category { get; set; }
        public string Incident_Description { get; set; }
        public string Severity_Level { get; set; }
        public string Occurrence_Type { get; set; }
        public DateTime Incident_Date { get; set; }
        public string Incident_Start_Time { get; set; }
        public string Incident_End_Time { get; set; }
        public string Incident_Location_Details { get; set; }
        public int Primary_Trigger_ID { get; set; }
        public string Root_Cause_Summary { get; set; }
        public string Category_of_Person_Reporting { get; set; }
        public string Reported_By_Name { get; set; }
        public string Incident_Class { get; set; }
        public string HSE_Management_Plan_ID { get; set; }
        public int Hazard_ID { get; set; }
        public decimal Actual_Financial_Impact_LCY { get; set; }
        public int Actual_Schedule_Delay_Days { get; set; }
        public int Actual_No_Injured_Persons { get; set; }
        public int Actual_No_of_Fatalities { get; set; }
        public int No_of_Parties_Involved { get; set; }
        public int No_of_Corrective_Actions { get; set; }
        public int No_of_Preventive_Actions { get; set; }
        public string Police_Report_Reference_No { get; set; }
        public DateTime Police_Report_Date { get; set; }
        public string Police_Station { get; set; }
        public string Reporting_Officer { get; set; }
        public string Project_ID { get; set; }
        public string Work_Execution_Plan_ID { get; set; }
        public string Corporate_Strategic_Plan_ID { get; set; }
        public string Year_Code { get; set; }
        public string Directorate_ID { get; set; }
        public string Department_ID { get; set; }
        public string Region_ID { get; set; }
        public int Dimension_Set_ID { get; set; }
        public string Status { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date_Time { get; set; }
        public bool Posted { get; set; }
        public DateTime Posting_Date { get; set; }
    }

    public class DependentList
    {
        public string Employee_Code { get; set; }
        public int Line_No { get; set; }
        public string Document_No { get; set; }
        public string Relationship { get; set; }
        public List<SelectListItem> ListOfRelationships { get; set; }
        public string SurName { get; set; }
        public string Other_Names { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public string Type { get; set; }
        public string ID_No_Passport_No { get; set; }
        public bool Comment { get; set; }
    }

    public class MedicalClaim
    {
        public string Member_No { get; set; }
        public string Claim_No { get; set; }
        public string Claim_Type { get; set; }
        public string Claim_Date { get; set; }
        public int Dependants { get; set; }
        public string Patient_Name { get; set; }
        public string Document_Ref { get; set; }
        public string Date_of_Service { get; set; }
        public string Attended_By { get; set; }
        public decimal Amount_Charged { get; set; }
        public string Comments { get; set; }
    }

    public class HrWelfareIncidenceCard
    {
        public string Incidence_No { get; set; }
        public string Incidence_Date { get; set; }
        public string Incidence_Time { get; set; }
        public string Incidence_Type { get; set; }

        public string Incidence { get; set; }
        public List<SelectListItem> ListOfIncidences { get; set; }
        public string Affected_Employee { get; set; }
        public string Employee_Name { get; set; }
        public string Duty_Station { get; set; }
        public List<SelectListItem> ListOfDutyStations { get; set; }
        public string Action_Taken { get; set; }
        public string Incidence_Status { get; set; }
    }
}