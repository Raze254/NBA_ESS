using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Latest_Staff_Portal.ViewModel
{
    public class EmployeeView2
    {   
        public string No { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Initials { get; set; }
        public string Job_Title { get; set; }
        public string Job_Title2 { get; set; }
        public string Supervisor { get; set; }
        public string Post_Code { get; set; }
        public string Country_Region_Code { get; set; }
        public string Phone_No { get; set; }
        public string Extension { get; set; }
        public string Mobile_Phone_No { get; set; }
        public string E_Mail { get; set; }
        public string Statistics_Group_Code { get; set; }
        public string Resource_No { get; set; }
        public bool Privacy_Blocked { get; set; }
        public string Search_Name { get; set; }
        public int Balance_LCY { get; set; }
        public bool Comment { get; set; }
        public string Global_Dimension_1_Filter { get; set; }
        public string Global_Dimension_2_Filter { get; set; }
        public string Date_Filter { get; set; }
        public string Currency_Filter { get; set; }
    
        
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        
        public string RecordType { get; set; }
        public string CurrentPositionID { get; set; }
        public string JobTitle2 { get; set; }
        public string SearchName { get; set; }
        public string CountyofOrigin { get; set; }
        public string CountyofOriginName { get; set; }
        public string CountyofResidence { get; set; }
        public string CountyofResidenceName { get; set; }
        
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public bool Disabled { get; set; }
        public string DisabilityNo { get; set; }
        public DateTime DisabilityCertExpiry { get; set; }
        public bool InsuranceCertificate { get; set; }
        public string EthnicOrigin { get; set; }
        public DateTime LastDateModified { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }

        public string DepartmentName { get; set; }
        public string ResponsibilityCenter { get; set; }
        public bool HeadofStation { get; set; }
        public bool HOD { get; set; }
        public bool IsChiefJustice { get; set; }
        public bool IsPartOfDisciplinaryTeam { get; set; }
        public bool ICTHelpDeskAdmin { get; set; }
        public string UserID { get; set; }  
        public string ReliverCode { get; set; }
        public string SalaryScale { get; set; }
        public string Present { get; set; }
        public string EmployeePostingGroup1 { get; set; }
        public DateTime IncrementDate { get; set; }
        public string IncrementalMonth { get; set; }
        public DateTime LastIncrementDate { get; set; }
        public string DirectorateCode { get; set; }
        public string ImplementingUnitName { get; set; }
        public string DepartmentCode { get; set; }
        public string DutyStation { get; set; }
        public string OrgUnit { get; set; }
        public string OrganisationUnitName { get; set; }
        public string AccountType { get; set; }
        public string Division { get; set; }
        public string EmployeeJobType { get; set; }
        public string CourtLevel { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string CountryRegionCode { get; set; }
        public string CitizenshipType { get; set; }
        public string EmployeeType { get; set; }
        public string PhoneNo { get; set; }
        
        public string MobilePhoneNo { get; set; }
        public string Pager { get; set; }
        public string PhoneNo2 { get; set; }
        public string Email { get; set; }
        public string CompanyEmail { get; set; }
        public string AltAddressCode { get; set; }
        public DateTime AltAddressStartDate { get; set; }
        public DateTime AltAddressEndDate { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string Ext { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime EndOfProbationDate { get; set; }
        public bool Inducted { get; set; }
        public DateTime RetirementDate { get; set; }
        public string FullPartTime { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public DateTime ReEmploymentDate { get; set; }
        public string NoticePeriod { get; set; }
        public string Status { get; set; }
        public string EmployeeStatus2 { get; set; }
        public DateTime InactiveDate { get; set; }
        public string CauseofInactivityCode { get; set; }
        public string EmplymtContractCode { get; set; }
        public string ResourceNo { get; set; }
        public string SalespersPurchCode { get; set; }
        public string Disciplinarystatus { get; set; }
        public string Reasonfortermination { get; set; }
        public DateTime TerminationDate { get; set; }
        public DateTime DateOfLeaving { get; set; }
        public string ExitInterviewConducted { get; set; }
        public DateTime ExitInterviewDate { get; set; }
        public string ExitInterviewDoneby { get; set; }
        public int BondingAmount { get; set; }
        public string ExitStatus { get; set; }
        public bool AllowReEmploymentInFuture { get; set; }
        public string CurrencyCode { get; set; }
        public bool Paystax_x003F_ { get; set; }
        public bool PayWages { get; set; }
        public string PayMode { get; set; }
        public string PIN { get; set; }
        public string NHIFNo { get; set; }
        public string SocialSecurityNo { get; set; }
        public string IDNumber { get; set; }
        public string PostingGroup { get; set; }
        public int ClaimLimit { get; set; }
        public string BankAccountNumber { get; set; }
        public string Employeex0027sBank { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankBranchName { get; set; }
        public string Employeex0027sBank2 { get; set; }
        public string BankName2 { get; set; }
        public string BankBranch2 { get; set; }
        public string BankBranchName2 { get; set; }
        public string BankAccountNo2 { get; set; }
        public bool AllowNegativeLeave { get; set; }
        public int OffDays { get; set; }
        public int LeaveDaysBF { get; set; }
        public int AllocatedLeaveDays { get; set; }
        public int TotalLeaveDays { get; set; }
        public int TotalLeaveTaken { get; set; }
        public string AdministrativeUnit { get; set; }
        public int LeaveOutstandingBal { get; set; }
        public int LeaveBalance { get; set; }
        public int AcruedLeaveDays { get; set; }
        public int CashperLeaveDay { get; set; }
        public int CashLeaveEarned { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveTypeFilter { get; set; }
        public string LeavePeriodFilter { get; set; }
        public bool OnLeave { get; set; }
        public string DateFilter { get; set; }
        public List<EmployeeDependant> EmployeeDependants { get; set; }
        public List<EmployeeQualification> EmployeeQualifications { get; set; }
        public int AllocatedAssets { get; set; }
        public string UserRole { get; set; }
        public bool IsSupervisor { get; set; }
        public int AssignedPurchaseReq { get; set; }

    }



    public class EmployeeView
    {
        
        public string No { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Record_Type { get; set; }
        public string Current_Position_ID { get; set; }
        public string Job_Title2 { get; set; }
        public string Search_Name { get; set; }
        public string County_of_Origin { get; set; }
        public string County_of_Origin_Name { get; set; }
        public string County_of_Residence { get; set; }
        public string County_of_Residence_Name { get; set; }
        public string Gender { get; set; }
        public string Marital_Status { get; set; }
        public string Religion { get; set; }
        public bool Disabled { get; set; }
        public string Disability_No { get; set; }
        public string Disability_Cert_Expiry { get; set; }
        public bool Insurance_Certificate { get; set; }
        public string Ethnic_Origin { get; set; }
        public string Last_Date_Modified { get; set; }
        public bool HOD { get; set; }
        public bool Is_Part_Of_Disciplinary_Team { get; set; }
        public bool Regional_Manager { get; set; }
        public bool ICT_Help_Desk_Admin { get; set; }
        public bool Is_Supply_Chain_Officer { get; set; }
        public bool CEO { get; set; }
        public string UserID { get; set; }
        public string Supervisor { get; set; }
        public string Reliver_Code { get; set; }
        public string Salary_Scale { get; set; }
        public string Present { get; set; }
        public string Employee_Posting_Group1 { get; set; }
        public string Increment_Date { get; set; }
        public string Employee_Category_Type { get; set; }
        public string Incremental_Month { get; set; }
        public string Last_Increment_Date { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string Department_Name { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string AdministrativeUnit { get; set; }
        public string Directorate_Code { get; set; }
        public string Implementing_Unit_Name { get; set; }
        public string Department_Code { get; set; }

        public string Administrative_Unit_Name { get; set; }
        public string DutyStation { get; set; }
        public string Station_Name { get; set; }
        public string Account_Type { get; set; }
        public string Division { get; set; }
        public string Employee_Job_Type { get; set; }
        public string Responsibility_Center { get; set; }
        public string Job_Cadre { get; set; }
        public string Job_Cadre_Name { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Post_Code { get; set; }
        public string City { get; set; }
        public string Country_Region_Code { get; set; }
        public string Citizenship_Type { get; set; }
        public string Employee_Type { get; set; }
        public string Phone_No { get; set; }
        public string Extension { get; set; }
        public string Mobile_Phone_No { get; set; }
        public string Pager { get; set; }
        public string Address2 { get; set; }
        public string Phone_No_2 { get; set; }
        public string E_Mail { get; set; }
        public string Company_E_Mail { get; set; }
        public string Alt_Address_Code { get; set; }
        public string Alt_Address_Start_Date { get; set; }
        public string Alt_Address_End_Date { get; set; }
        public string Work_Phone_Number { get; set; }
        public string Ext { get; set; }
        public string Date_Of_Birth { get; set; }
        public string Age { get; set; }
        public string Employment_Date { get; set; }
        public string End_Of_Probation_Date { get; set; }
        public bool Inducted { get; set; }
        public string Pension_Scheme_Join { get; set; }
        public string TimeinPension { get; set; }
        public string Retirement_Date { get; set; }
        public string Full_Part_Time { get; set; }
        public string Contract_Start_Date { get; set; }
        public string Contract_End_Date { get; set; }
        public string Re_Employment_Date { get; set; }
        public string Notice_Period { get; set; }
        public string Status { get; set; }
        public string Employee_Status_2 { get; set; }
        public string Inactive_Date { get; set; }
        public string Cause_of_Inactivity_Code { get; set; }
        public string Emplymt_Contract_Code { get; set; }
        public string Resource_No { get; set; }
        public bool Due_for_Retirement { get; set; }
        public string Salespers_Purch_Code { get; set; }
        public string Disciplinary_status { get; set; }
        public string Reason_for_termination { get; set; }
        public string Termination_Date { get; set; }
        public string Date_Of_Leaving { get; set; }
        public string Exit_Interview_Conducted { get; set; }
        public string Exit_Interview_Date { get; set; }
        public string Exit_Interview_Done_by { get; set; }
        public int Bonding_Amount { get; set; }
        public string Exit_Status { get; set; }
        public bool Allow_Re_Employment_In_Future { get; set; }
        public bool Pays_tax_x003F_ { get; set; }
        public bool Pay_Wages { get; set; }
        public string Pay_Mode { get; set; }
        public string P_I_N { get; set; }
        public string N_H_I_F_No { get; set; }
        public string Social_Security_No { get; set; }
        public string ID_Number { get; set; }
        public string Employee_Posting_Group { get; set; }
        public string Posting_Group { get; set; }
        public int Claim_Limit { get; set; }
        public string BankAccountNumber { get; set; }
        public string Employee_x0027_s_Bank { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_Branch { get; set; }
        public string Bank_Branch_Name { get; set; }
        public string Employee_x0027_s_Bank_2 { get; set; }
        public string Bank_Name_2 { get; set; }
        public string Bank_Branch_2 { get; set; }
        public string Payee_Bank_Acc_Name { get; set; }
        public string Bank_Branch_Name_2 { get; set; }
        public string BankAccountNumber2 { get; set; }
        public bool Allow_Negative_Leave { get; set; }
        public int Off_Days { get; set; }
        public int Leave_Days_B_F { get; set; }
        public int Allocated_Leave_Days { get; set; }
        public int Total_Leave_Days { get; set; }
        public int Total_Leave_Taken { get; set; }
        public int Leave_Outstanding_Bal { get; set; }
        public int Leave_Balance { get; set; }
        public int Acrued_Leave_Days { get; set; }
        public int Cash_per_Leave_Day { get; set; }
        public int Cash_Leave_Earned { get; set; }
        public string Leave_Status { get; set; }
        public string Leave_Type_Filter { get; set; }
        public string Leave_Period_Filter { get; set; }
        public bool On_Leave { get; set; }
        public string Date_Filter { get; set; }

        [JsonProperty("User_Signature@odata.mediaEditLink")]
        public string User_SignatureodatamediaEditLink { get; set; }

        [JsonProperty("User_Signature@odata.mediaReadLink")]
        public string User_SignatureodatamediaReadLink { get; set; }
    }


    public class EmpInitial
    {
        public string Code { get; set; }
    }

    public class LeaveBalView
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string IDNo { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string LeaveType { get; set; }
        public string CarryForward { get; set; }
        public string LeaveTaken { get; set; }
        public string TotalBal { get; set; }
        public string Available { get; set; }
        public string Earned { get; set; }
        public string Allocated { get; set; }
    }

    public class EmployeeDependant
    {
        public string EmployeeCode { get; set; }
        public string Type { get; set; }
        public string Relationship { get; set; }
        public string SurName { get; set; }
        public string OtherNames { get; set; }
        public int LineNo { get; set; }
        public string MemberID { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Category { get; set; }
        public string CardNo { get; set; }
        public string NumberInTheFamily { get; set; }
    }

    public class EmployeeQualification
    {
        public string Employee_No { get; set; }
        public int Line_No { get; set; }
        public string Qualification_Category { get; set; }
        public string Qualification_Code { get; set; }
        public string Description { get; set; }
        public string Institution_Company { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string Type { get; set; }
        public string Expiration_Date { get; set; }
        public int Cost { get; set; }
        public string Year { get; set; }
        public string Specialization { get; set; }
        public string Grades { get; set; }
        public string Course_Grade { get; set; }
        public bool Comment { get; set; }
    }

    public class TalentManagementCard
    {
        public string No { get; set; }
        public string Fullname { get; set; }

        public string Talent9BoxCode { get; set; }
        public string PotentialLevel { get; set; }
        public string PerformanceLevel { get; set; }
    }

    public class TrainingHistory
    {
        public string TrainingNo { get; set; }
        public int LineNo { get; set; }
        public string EmployeeNo { get; set; }
        public string Training { get; set; }
        public string Institution { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Funding { get; set; }
    }

    public class Attendance
    {
        public int EntryNo { get; set; }
        public DateTime ClockinDate { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public TimeSpan ClockinTime { get; set; }
        public TimeSpan ClockoutTime { get; set; }
    }

    public class ESSRoleSetup
    {
        public string OdataEtag { get; set; }
        public string User_ID { get; set; }
        public string UserName { get; set; }
        public string Employee_Name { get; set; }
        public string Employee_No { get; set; }
        public bool Accounts_User { get; set; }
        public bool Accounts_Approver { get; set; }
        public bool Audit_Officer { get; set; }
        public bool Audit__x0026__Risk_Officer { get; set; }
        public bool HQ_Accountant { get; set; }
        public bool HQ_Finance_Officer { get; set; }
        public bool HQ_Procurement_Officer { get; set; }
        public bool Station_Accountant { get; set; }
        public bool Station_Procurement_Office { get; set; }
        public bool DAAS_Officer { get; set; }
        public bool HR_Welfare_Officer { get; set; }
        public bool Mobility_Officer { get; set; }
        public bool Procurement_officer { get; set; }
        public bool Recruitment_Officer { get; set; }
        public bool Revenue_Officer { get; set; }
        public bool Transport_Officer { get; set; }
    }

    public class NextOfKin
    {
        public string Employee_Code { get; set; }
        public string Type { get; set; }
        public string Relationship { get; set; }
        public string SurName { get; set; }
        public string Other_Names { get; set; }
        public int Line_No { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public string Home_Tel_No { get; set; }
        public string E_mail { get; set; }
        public string Gender { get; set; }
        public string ID_No_Passport_No { get; set; }
        public List<SelectListItem> ListOfRelationship { get; set; } = new();
    }
}