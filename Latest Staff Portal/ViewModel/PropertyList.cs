using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class Reservation
    {
        public string Etag { get; set; }
        public string No { get; set; }
        public string RequestorID { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public DateTime DocumentDate { get; set; }
        public string TimeCreated { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostingDate { get; set; }
        public string PostingTime { get; set; }
        public string Status { get; set; }
        public string DocumentStatus { get; set; }
        public string Type { get; set; }
    }

    public class NewReservationList
    {
        public string Etag { get; set; }
        public string No { get; set; }
        public string RequestorID { get; set; }
        public List<SelectListItem> EmployeeNo { get; set; }
        public List<SelectListItem> EmployeeName { get; set; }
        public string Description { get; set; }
        public DateTime DocumentDate { get; set; }
        public string TimeCreated { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostingDate { get; set; }
        public string PostingTime { get; set; }
        public string Status { get; set; }
        public string DocumentStatus { get; set; }
        public List<SelectListItem> Type { get; set; }
        public string implementingUnit { get; set; }
        public string RType { get; set; }
        public List<SelectListItem> ListOfImplementingUnits { get; set; }
        public string DutyStation { get; set; }
        public List<SelectListItem> ListOfDutyStations { get; set; }
    }


    public class PropertyList
    {
        public string Etag { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string LandRegistrationNumber { get; set; }
        public string DirectorateCode { get; set; }
        public double TotalSizeInSqFeet { get; set; }
        public string Direction { get; set; }
        public string LocationDescription { get; set; }
        public double LandSizeAcres { get; set; }
        public string SurveyPlanNumber { get; set; }
        public string DevelopmentStatus { get; set; }
        public string FileReferenceNumber { get; set; }
        public string OwnershipType { get; set; }
        public string EnvironmentalAssessmentStatus { get; set; }
        public int LeaseTenantSpaces { get; set; }
        public int LeaseContracts { get; set; }
        public string FaClassCode { get; set; }
        public string FaSubclassCode { get; set; }
        public string FaLocationCode { get; set; }
        public bool BudgetedAsset { get; set; }
        public string SerialNo { get; set; }
        public string MainAssetComponent { get; set; }
        public string ComponentOfMainAsset { get; set; }
        public string SearchDescription { get; set; }
        public string ResponsibleEmployee { get; set; }
        public bool Inactive { get; set; }
        public bool Blocked { get; set; }
        public bool Acquired { get; set; }
        public string LastDateModified { get; set; }
        public string DepreciationBookCode { get; set; }
        public string FaPostingGroup { get; set; }
        public string DepreciationMethod { get; set; }
        public string DepreciationStartingDate { get; set; }
        public int NumberOfDepreciationYears { get; set; }
        public string DepreciationEndingDate { get; set; }
        public int BookValue { get; set; }
        public string AddMoreDeprBooks { get; set; }
        public string VendorNo { get; set; }
        public string MaintenanceVendorNo { get; set; }
        public bool UnderMaintenance { get; set; }
        public string NextServiceDate { get; set; }
        public string WarrantyDate { get; set; }
        public bool Insured { get; set; }
    }

    public class ResponsibilityCentres
    {
        public string Etag { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Post_Code { get; set; }
        public string City { get; set; }
        public string Country_Region_Code { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Contact { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Location_Code { get; set; }
        public string Operating_Unit_Type { get; set; }
        public string Direct_Reports_To { get; set; }
        public string Indirect_Reports_To { get; set; }
        public string Hierarchical_Level_ID { get; set; }
        public string Headed_By_Title { get; set; }
        public string Current_Head { get; set; }
        public string Target_Type { get; set; }
        public string Phone_No { get; set; }
        public string Fax_No { get; set; }
        public string E_Mail { get; set; }
        public string Home_Page { get; set; }
    }


    public class Employee
    {
        public string OdataEtag { get; set; }
        public string No { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string RecordType { get; set; }
        public string CurrentPositionID { get; set; }
        public string JobTitle2 { get; set; }
        public string CurrentDutyStation { get; set; }
        public string CurrentJobGrade { get; set; }
        public string CurrentTermsOfService { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string CountryRegionCode { get; set; }
        public string PhoneNo { get; set; }
        public string SearchName { get; set; }
        public string CountyOfOrigin { get; set; }
        public string CountyOfOriginName { get; set; }
        public string CountyOfResidence { get; set; }
        public string CountyOfResidenceName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public bool Disabled { get; set; }
        public bool InsuranceCertificate { get; set; }
        public string EthnicOrigin { get; set; }
        public string LastDateModified { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string ResponsibilityCenter { get; set; }
        public bool HOD { get; set; }
        public bool RegionalManager { get; set; }
        public bool ICTHelpDeskAdmin { get; set; }
        public bool CEO { get; set; }
        public string UserID { get; set; }
        public string Supervisor { get; set; }
        public string ReliverCode { get; set; }
        public string DirectorateCode { get; set; }
        public string DepartmentCode { get; set; }
        public string Division { get; set; }
        public string EmployeeJobType { get; set; }
        public string Extension { get; set; }
        public string MobilePhoneNo { get; set; }
        public string Pager { get; set; }
        public string EMail { get; set; }
        public string CompanyEMail { get; set; }
        public string AltAddressCode { get; set; }
        public DateTime AltAddressStartDate { get; set; }
        public DateTime AltAddressEndDate { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string Ext { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime EndOfProbationDate { get; set; }
        public DateTime PensionSchemeJoin { get; set; }
        public DateTime MedicalSchemeJoin { get; set; }
        public DateTime RetirementDate { get; set; }
        public string FullPartTime { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string NoticePeriod { get; set; }
        public string SendAlertTo { get; set; }
        public string Status { get; set; }
        public string EmployeeStatus2 { get; set; }
        public DateTime InactiveDate { get; set; }
        public string CauseOfInactivityCode { get; set; }
        public string EmployeesType { get; set; }
        public string EmplymtContractCode { get; set; }
        public string StatisticsGroupCode { get; set; }
        public string ResourceNo { get; set; }
        public string SalespersPurchCode { get; set; }
        public string UnionCode { get; set; }
        public string UnionMembershipNo { get; set; }
        public string DisciplinaryStatus { get; set; }
        public string ReasonForTermination { get; set; }
        public DateTime TerminationDate { get; set; }
        public DateTime DateOfLeaving { get; set; }
        public string ExitInterviewConducted { get; set; }
        public DateTime ExitInterviewDate { get; set; }
        public string ExitInterviewDoneBy { get; set; }
        public decimal BondingAmount { get; set; }
        public int ExitStatus { get; set; }
        public bool AllowReEmploymentInFuture { get; set; }
        public string CurrencyCode { get; set; }
        public bool PaysTax { get; set; }
        public bool PayWages { get; set; }
        public string PayMode { get; set; }
        public string PIN { get; set; }
        public string NHIFNo { get; set; }
        public string SocialSecurityNo { get; set; }
        public string IDNumber { get; set; }
        public string EmployeePostingGroup { get; set; }
        public string PostingGroup { get; set; }
        public decimal ClaimLimit { get; set; }
        public string BankAccountNumber { get; set; }
        public string EmployeeBank { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankBranchName { get; set; }
        public string EmployeeBank2 { get; set; }
        public string BankName2 { get; set; }
        public string BankBranch2 { get; set; }
        public string BankBranchName2 { get; set; }
        public string BankAccountNo2 { get; set; }
        public string SalaryScale { get; set; }
        public string Present { get; set; }
        public bool AllowNegativeLeave { get; set; }
        public int OffDays { get; set; }
        public int LeaveDaysBF { get; set; }
        public int AllocatedLeaveDays { get; set; }
        public int TotalLeaveDays { get; set; }
        public int TotalLeaveTaken { get; set; }
        public int LeaveOutstandingBal { get; set; }
        public int LeaveBalance { get; set; }
        public int AcruedLeaveDays { get; set; }
        public decimal CashPerLeaveDay { get; set; }
        public decimal CashLeaveEarned { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveTypeFilter { get; set; }
        public string LeavePeriodFilter { get; set; }
        public bool OnLeave { get; set; }
        public DateTime DateFilter { get; set; }
        public string UserSignatureOdataMediaEditLink { get; set; }
        public string UserSignatureOdataMediaReadLink { get; set; }
    }

    public class NewProperty
    {
        // Properties for the model
        [Display(Name = "Number")] public List<SelectList> Number { get; set; }

        [Display(Name = "Responsible Employee")]
        public List<SelectListItem> ResponsibleEmployee { get; set; }

        [Display(Name = "Description")] public string Description { get; set; }

        [Display(Name = "Land Registration Number")]
        public string LandRegistrationNumber { get; set; }

        [Display(Name = "Directorate Code")] public List<SelectListItem> DirectorateCode { get; set; }

        [Display(Name = "Total Size in Sq Feet")]
        public double TotalSizeInSqFeet { get; set; }

        [Display(Name = "Direction")] public string Direction { get; set; }

        [Display(Name = "Location Description")]
        public string LocationDescription { get; set; }

        [Display(Name = "Land Size in Acres")] public double LandSizeAcres { get; set; }

        [Display(Name = "Survey Plan Number")] public string SurveyPlanNumber { get; set; }

        [Display(Name = "Development Status")] public List<SelectListItem> DevelopmentStatus { get; set; }

        [Display(Name = "File Reference Number")]
        public string FileReferenceNumber { get; set; }

        [Display(Name = "Ownership Type")] public List<SelectListItem> OwnershipType { get; set; }

        [Display(Name = "Environmental Assessment Status")]
        public List<SelectListItem> EnvironmentalAssessmentStatus { get; set; }

        [Display(Name = "Lease Tenant Spaces")]
        public int LeaseTenantSpaces { get; set; }

        [Display(Name = "Lease Contracts")] public int LeaseContracts { get; set; }

        [Display(Name = "Budgeted Asset")] public bool BudgetedAsset { get; set; }

        [Display(Name = "Main Asset Component")]
        public string MainAssetComponent { get; set; }

        [Display(Name = "Component of Main Asset")]
        public string ComponentOfMainAsset { get; set; }

        [Display(Name = "Search Description")] public string SearchDescription { get; set; }


        [Display(Name = "Inactive")] public bool Inactive { get; set; }

        [Display(Name = "Blocked")] public bool Blocked { get; set; }

        [Display(Name = "Acquired")] public bool Acquired { get; set; }
    }

    public class ReservationLine
    {
        public string No { get; set; }
        public string Line_No { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string ResourceNo { get; set; }
        public string Property_Code { get; set; }
        public string Property_Name { get; set; }
        public string Room_Type { get; set; }
        public string Description { get; set; }
        public string Start_Date_Time { get; set; }
        public string End_Date_Time { get; set; }
        public string Duration { get; set; }
        public string Rooms { get; set; }
        public string Floors { get; set; }
        public string Capacity { get; set; }

        public List<SelectListItem> ListOfProperty { get; set; }
        public List<SelectListItem> ListOfRoomType { get; set; }
        public List<SelectListItem> ListOfRooms { get; set; }
        public List<SelectListItem> ListOfFloors { get; set; }
    }

    public class ReservationLineList
    {
        public string Status { get; set; }
        public List<ReservationLine> ListOfReservationLine { get; set; }
    }

    public class PropertyFloor
    {
        public string Floor_No { get; set; }
        public string Property_No { get; set; }
        public string Description { get; set; }
        public bool Blocked { get; set; }
    }

    public class PropertyRooms
    {
        public string No { get; set; }
        public string Description { get; set; }
        public string Property_Code { get; set; }
        public string Name { get; set; }
        public string Item_Description { get; set; }
    }
}