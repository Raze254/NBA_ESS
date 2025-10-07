using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class EmployeeRequistion
    {
        public string DocumentNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string TargetCandidateSource { get; set; }
        public string RequesterStaffNo { get; set; }
        public string RequesterName { get; set; }
        public string RequesterID { get; set; }
        public string RecruitmentPlanType { get; set; }
        public int PlanType { get; set; }
        public string RecruitmentPlanID { get; set; }
        public string PositionID { get; set; }
        public string JobTitleDesignation { get; set; }
        public string JobPurpose { get; set; }
        public int NoOfOpenings { get; set; }
        public string DutyStationID { get; set; }
        public List<SelectListItem> DutyStations { get; set; }
        public string PrimaryRecruitmentReason { get; set; }
        public List<SelectListItem> ReasonsForRecruitment { get; set; }
        public string RecruitmentJustification { get; set; }
        public string SourcingMethod { get; set; }
        public string ApprovalStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public TimeSpan CreatedTime { get; set; }
        public string RecruitmentCycleType { get; set; }
        public int CycleType { get; set; }
        public string RecruitmentLeadTime { get; set; }
        public DateTime PlannedRecruitmentStartDate { get; set; }
        public DateTime PlannedRecruitmentEndDate { get; set; }
        public string FundingSourceID { get; set; }
        public List<SelectListItem> FundingSources { get; set; }
        public string FinacialYearCode { get; set; }
        public List<SelectListItem> FinancialYears { get; set; }
        public decimal RecruitmentLineBudgetCost { get; set; }
        public List<SelectListItem> ListOfJobs { get; set; }
        public decimal AverageCostHire { get; set; }
        public string JobNo { get; set; }
        public string JobTaskNo { get; set; }
        public int StaffEstablishment { get; set; }
        public int CurrentHeadcount { get; set; }
        public string HierarchicallyReportsTo { get; set; }
        public List<SelectListItem> PositionsLists { get; set; }
        public string FunctionallyReportsTo { get; set; }
        public decimal EstimateGrossSalary { get; set; }
        public string JobGradeID { get; set; }
        public List<SelectListItem> SalaryScaleList { get; set; }
        public string OverallAppointmentAuthority { get; set; }
        public List<SelectListItem> AppointingAuths { get; set; }
        public string SeniorityLevel { get; set; }
        public string EmploymentType { get; set; }
        public string DefaultTermsOfServiceCode { get; set; }
        public List<SelectListItem> TermsOfService { get; set; }
        public List<SelectListItem> ListofJobs { get; set; }
        public List<SelectListItem> RecruitmentPlanCodes { get; set; }
        public List<SelectListItem> ListofContractTypes { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
    }

    public class JobsList
    {
        public string JobID { get; set; }
        public string JobDescription { get; set; }
    }

    public class ContractList
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class RecruitmentReasons
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int NoOfVacancies { get; set; }
        public bool Blocked { get; set; }
    }
}