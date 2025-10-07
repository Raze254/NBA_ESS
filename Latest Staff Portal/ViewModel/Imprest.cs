using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{

    public class Imprest
    {

        public string No { get; set; }
        public string RequestDate { get; set; }
        public string TripNo { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string CustomerAC { get; set; }
        public string TripStartDate { get; set; }
        public string TripExpectedEndDate { get; set; }
        public int NoofDays { get; set; }
        public string PurposeofImprest { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string DeadlineforImprestReturn { get; set; }
        public string Status { get; set; }
        public string TransactionType { get; set; }
        public int Balance { get; set; }
        public int TotalAmountRequested { get; set; }
        public int ImprestAmount { get; set; }
        public int IssuedAmount { get; set; }
        public string Country { get; set; }
        public string ActivityDate { get; set; }
        public string City { get; set; }
        public string JobGroup { get; set; }
        public string ExternalApplication { get; set; }
        public int ApprovalStatus { get; set; }
        public string CBKWebsiteAddress { get; set; }
        public string PayMode { get; set; }
        public string BankAccount { get; set; }
        public string ChequeNo { get; set; }
        public string ImprestType { get; set; }
        public List<SelectListItem> ListOfSImprestTypes { get; set; }
        public List<SelectListItem> ListOfCustomerAC { get; set; }
        public List<SelectListItem> ListOfPayModes { get; set; }

        public List<SelectListItem> ListOfBankAC { get; set; }

        public List<SelectListItem> ListOfTransactionTypes { get; set; }
        

    }
    public class ImprestLines
    {
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public string TransactionType { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string UnitofMeasure { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; }
        public int RequestedAmount { get; set; }
        public int ActualSpent { get; set; }
        public int RemainingAmount { get; set; }
        public string ExpenseType { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string GlobalDimension1Code { get; set; }

        public List<SelectListItem> ListOfTransactionTypes { get; set; }
        public List<SelectListItem> ListOfAccountTypes { get; set; }
        public List<SelectListItem> ListOfAccountNo { get; set; }
        public List<SelectListItem> ListOfUOM { get; set; }
        public List<SelectListItem> ListOfExpenseTypes { get; set; }
    }

    public class ImprestMemo
    {
        public string No { get; set; }
        public string WarrantVoucherType { get; set; }
        public string WarrantNo { get; set; }
        public string Date { get; set; }
        public string Project { get; set; }
        public string ProjectDescription { get; set; }
        public string Subject { get; set; }
        public string Objective { get; set; }
        public string TermsOfReference { get; set; }
        public string UserID { get; set; }
        public string Requestor { get; set; }
        public string RequestorName { get; set; }
        public string DestinationName { get; set; }
        public string ImprestNaration { get; set; }
        public string Status { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ProjectName { get; set; }
        public decimal TotalSubsistenceAllowance { get; set; }
        public decimal TotalFuelCosts { get; set; }
        public decimal TotalMaintenanceCosts { get; set; }
        public decimal TotalCasualsCost { get; set; }
        public decimal TotalOtherCosts { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string ReasonToReopen { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public string TimeOut { get; set; }
        public string JourneyRoute { get; set; }
        public string WorkTypeFilter { get; set; }
        public string DimensionSetId { get; set; }
    }

    public class NewImprestRequisition
    {
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string Dim8 { get; set; }
        public string RespC { get; set; }
        public string UoM { get; set; }
        public string ImprestDueType { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfImprestDue { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
    }

    public class ImprestTypes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ImprestTypes2
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ImprestTypesList
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfImprestTypes { get; set; }
        public List<SelectListItem> ListOfImprestTypes2 { get; set; }
        public List<SelectListItem> ListOfUnitMeasure { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
        public string Days { get; set; }
    }

    public class ImprestHeader
    {
        public string No { get; set; }
        public string TravelType { get; set; }
        public string DateNeeded { get; set; }
        public string DateofTravel { get; set; }
        public string DateofReturn { get; set; }
        public string Remarks { get; set; }
        public NewImprestRequisition DocD { get; set; }
        public string Status { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalAmount { get; set; }
        public string RequestorNo { get; set; }
        public string RequestorName { get; set; }
        public string AccountNo { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string RespC { get; set; }
        public string ImprestDueType { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public List<ImprestLine> ListOfWorkshopLines { get; set; }
    }



    public class SafariTeam2
    {
        public string Imprest_Memo_No { get; set; }
        public int Line_No { get; set; }
        public string Work_Type { get; set; }
        public string Type { get; set; }
        public string Type_of_Expense { get; set; }
        public string No { get; set; }
        public string G_L_Account { get; set; }
        public string Task_No { get; set; }
        public string Name { get; set; }
        public string Unit_of_Measure { get; set; }
        public string Currency_Code { get; set; }
        public int Time_Period { get; set; }
        public int Direct_Unit_Cost { get; set; }
        public int Entitlement { get; set; }
        public int Transport_Costs { get; set; }
        public int Total_Entitlement { get; set; }
        public int Outstanding_Amount { get; set; }
        public string Tasks_to_Carry_Out { get; set; }
        public string Expected_Output { get; set; }
        public int Delivery { get; set; }
        public bool Project_Lead { get; set; }
        public string Dimension1Code { get; set; }
        public string Dimension2Code { get; set; }
        public string Dimension3Code { get; set; }
        public string Dimension4Code { get; set; }
        public string Dimension5Code { get; set; }
        public string Dimension6Code { get; set; }
        public string Dimension7Code { get; set; }
        public string Dimension8Code { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfImprestTypes { get; set; }
        public List<SelectListItem> ListOfResources { get; set; }
        public List<SelectListItem> ListOfUOM { get; set; }
        public List<SelectListItem> ListOfCurrencies { get; set; }
        public decimal Mileage_KM { get; set; }

        public decimal Total_Mileage_Cost { get; set; }

    }


    public class OtherCosts
    {
        
        public string Imprest_Memo_No { get; set; }
        public int Line_No { get; set; }
        public string Type { get; set; }
        public string Type_of_Expense { get; set; }
        public string Description { get; set; }
        public string No { get; set; }
        public string Required_For { get; set; }
        public int Quantity_Required { get; set; }
        public int No_of_Days { get; set; }
        public int Unit_Cost { get; set; }
        public int Line_Amount { get; set; }
        public string Dimension1Code { get; set; }
        public string Dimension2Code { get; set; }
        public string Dimension3Code { get; set; }
        public string Dimension4Code { get; set; }
        public string Dimension5Code { get; set; }
        public string Dimension6Code { get; set; }
        public string Dimension7Code { get; set; }
        public string Dimension8Code { get; set; }
        public List<SelectListItem> ListOfVoteItems { get; set; }
    }




    public class SafariTeam
    {
        public string ImprestMemoNo { get; set; }
        public int LineNo { get; set; }
        public string WorkType { get; set; }
        public string Type { get; set; }
        public string TypeOfExpense { get; set; }
        public string No { get; set; }
        public string GLAccount { get; set; }
        public string TaskNo { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public string CurrencyCode { get; set; }
        public int TimePeriod { get; set; }
        public decimal DirectUnitCost { get; set; }
        public decimal Entitlement { get; set; }
        public decimal TransportCosts { get; set; }
        public decimal TotalEntitlement { get; set; }
        public decimal OutstandingAmount { get; set; }
        public string TasksToCarryOut { get; set; }
        public string ExpectedOutput { get; set; }
        public decimal Delivery { get; set; }
        public bool ProjectLead { get; set; }
    }
    public class ImprestLine
    {
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public string Payee { get; set; }
        public string EmployeeNo { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }

        public string EmployeeName { get; set; }
        public string JobGroup { get; set; }
        public string GLAccount { get; set; }
        public string Destination { get; set; }
        public List<SelectListItem> ListOfDestinations { get; set; }

        public string VoteItem { get; set; }
        public List<SelectListItem> ListOfClaims { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string RecalledBy { get; set; }
        public string RecalledOn { get; set; }
        public string SourceLineNo { get; set; }

    }

    public class ImprestLinesList
    {
        public string Status { get; set; }
        public List<ImprestLine> ListOfImprestLines { get; set; }
    }

    public class SafariTeamList
    {
        public string Status { get; set; }
        public List<SafariTeam> ListOfSafariTeam { get; set; }
    }

    public class ImprestItemDetails
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfImprestTypes { get; set; }
        public List<SelectListItem> ListOfImprestTypes2 { get; set; }
        public List<SelectListItem> ListOfUoM { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
        public ImprestLine ItemDetails { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Quantity { get; set; }
        public string NoofDays { get; set; }
    }

    public class ImprestDocument
    {
        public ImprestHeader DocHeader { get; set; }
        public List<ImprestLine> ListOfImprestLines { get; set; }
    }

    public class StandingImprest
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string PostingDate { get; set; }
        public string StandingImprestType { get; set; }
        public string ChequeDate { get; set; }
        public string PayingBankAccount { get; set; }
        public string BankName { get; set; }
        public string PaymentNarration { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalAmount { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string CourtStation { get; set; }
        public string DepartmentName { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string AdminUnit { get; set; }
        public string ProjectName { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string Status { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedDate { get; set; }
        public string PayMode { get; set; }
        public string ChequeNo { get; set; }
        public string Payee { get; set; }
        public string CreatedBy { get; set; }
        public decimal TotalAmountLCY { get; set; }
        public string Project { get; set; }
        public string ProjectDescription { get; set; }
        public string DimensionSetId { get; set; }
        public string AvailableAmount { get; set; }
        public string CommittedAmount { get; set; }
        public string AieReceipt { get; set; }
        public string AieReceiptAmount { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string ValidatedBankName { get; set; }


    }

    public class StandingImprestLine
    {
        public string No { get; set; }
        public int LineNo { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string PayeeBankCode { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBankBranchCode { get; set; }
        public string PayeeBankBranchName { get; set; }
        public string PayeeBankAccountNo { get; set; }
        public string PayeeBankAccName { get; set; }
        public string Status { get; set; }
        public string ValidatedBankName { get; set; }
    }

    public class ImprestWarranties
    {
        public string No { get; set; }
        public int Dimension_Set_ID { get; set; }
        public string Date { get; set; }
        public string Posting_Date { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Payee_Bank_Account { get; set; }
        public string Payee_Bank_Code { get; set; }
        public string Payee { get; set; }
        public string ValidatedBankName { get; set; }
        public string Reference_No { get; set; }
        public string Pay_Mode { get; set; }
        public string Cheque_No { get; set; }
        public string Paying_Bank_Account { get; set; }
        public string Bank_Name { get; set; }
        public int Available_Amount { get; set; }
        public int Committed_Amount { get; set; }
        public string AIE_Receipt { get; set; }
        public string Travel_Date { get; set; }
        public string Payment_Narration { get; set; }
        public string Created_By { get; set; }
        public string Status { get; set; }
        public string Strategic_Plan { get; set; }
        public string Reporting_Year_Code { get; set; }
        public string Workplan_Code { get; set; }
        public string Activity_Code { get; set; }
        public string Expenditure_Requisition_Code { get; set; }
        public string Imprest_Memo_No { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Department_Name { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Project_Name { get; set; }
        public int Imprest_Amount { get; set; }
        public string Imprest_Deadline { get; set; }
        public bool Posted { get; set; }

        public List<SelectListItem> ListOfPayroll { get; set; }
        public List<SelectListItem> ListOfPayModes { get; set; }
        public List<SelectListItem> ListOfStratPlans { get; set; }
        public List<SelectListItem> ListOfWPA { get; set; }
        public List<SelectListItem> ListOfImplYears { get; set; }
        public List<SelectListItem> ListOfExpREq { get; set; }

        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
    }

    public class WarrantImprestLines
    {
        public string No { get; set; }
        public int Line_No { get; set; }
        public string Advance_Type { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Purpose { get; set; }
        public int Daily_Rate { get; set; }
        public int No_of_Days { get; set; }
        public string Vote_Item { get; set; }
        public string Currency_Code { get; set; }
        public int Amount { get; set; }
        public string Project { get; set; }
        public string Task_No { get; set; }

        public List<SelectListItem> ListOfGLAccounts { get; set; }

        public List<SelectListItem> ListOfVoteItems { get; set; }
    }

    public class WarrantImprestLinesList
    {
        public string Status { get; set; }
        public List<WarrantImprestLines> ListOfWarrantImprestLines { get; set; }
    }

    public class SpecialImprest
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string PostingDate { get; set; }
        public string StandingImprestType { get; set; }
        public string ChequeDate { get; set; }
        public string PayingBankAccount { get; set; }
        public string BankName { get; set; }
        public string PaymentNarration { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalAmount { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string Status { get; set; }
        public bool Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedDate { get; set; }
        public string PayMode { get; set; }
        public string Payee { get; set; }
        public string DimensionSetId { get; set; }
        public string AvailableAmount { get; set; }
        public string CommittedAmount { get; set; }
        public string AieReceipt { get; set; }
        public string AieReceiptAmount { get; set; }
        public string ValidatedBankName { get; set; }

    }


    public class SafariImprest
    {
        public string No { get; set; }
        public string Warrant_Voucher_Type { get; set; }
        public string Warrant_No { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public string Objective { get; set; }
        public string Terms_of_Reference { get; set; }
        public string Imprest_Naration { get; set; }
        public string User_ID { get; set; }
        public string Requestor { get; set; }
        public string Requestor_Name { get; set; }
        public bool HOD { get; set; }
        public string Start_Date { get; set; }
        public int No_of_days { get; set; }
        public string End_Date { get; set; }
        public string Return_Date { get; set; }
        public string Due_Date { get; set; }
        public int Total_Subsistence_Allowance { get; set; }
        public int Total_Fuel_Costs { get; set; }
        public int Total_Maintenance_Costs { get; set; }
        public int Total_Casuals_Cost { get; set; }
        public int Total_Other_Costs { get; set; }
        public string Status { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Department_Name { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Project_Name { get; set; }
        public int Dimension_Set_ID { get; set; }
        public string Strategic_Plan { get; set; }
        public string Reporting_Year_Code { get; set; }
        public string Workplan_Code { get; set; }
        public string Activity_Code { get; set; }
        public string Expenditure_Requisition_Code { get; set; }
        public string Reason_to_Reopen { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public string Time_Out { get; set; }
        public string Journey_Route { get; set; }
        public string Work_Type_Filter { get; set; }
    }

    public class SafariImprestwarrant
    {
        public string No { get; set; }
        public int Dimension_Set_ID { get; set; }
        public DateTime Date { get; set; }
        public DateTime Posting_Date { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Payee_Bank_Account { get; set; }
        public string Payee_Bank_Code { get; set; }
        public string Payee { get; set; }
        public string Pay_Mode { get; set; }
        public string Cheque_No { get; set; }
        public string Paying_Bank_Account { get; set; }
        public string Bank_Name { get; set; }
        public int Available_Amount { get; set; }
        public int Committed_Amount { get; set; }
        public DateTime Travel_Date { get; set; }
        public string Created_By { get; set; }
        public string Status { get; set; }
        public string Expenditure_Requisition_Code { get; set; }
        public string Imprest_Memo_No { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public int Imprest_Amount { get; set; }
        public DateTime Imprest_Deadline { get; set; }
        public bool Posted { get; set; }
    }

    public class SafariImprestLine
    {
        public string No { get; set; }
        public int Line_No { get; set; }
        public string Advance_Type { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Purpose { get; set; }
        public int Daily_Rate { get; set; }
        public int No_of_Days { get; set; }
        public string Vote_Item { get; set; }
        public string Currency_Code { get; set; }
        public int Amount { get; set; }
        public string Project { get; set; }
        public string Task_No { get; set; }
    }

    public class ApprovedSafariImprest
    {
        public string No { get; set; }
        public DateTime Date { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Imprest_Bank_Account_Number { get; set; }
        public string Imprest_Bank_Name { get; set; }
        public string Imprest_Bank_Branch_Name { get; set; }
        public string Pay_Mode { get; set; }
        public string Paying_Bank_Account { get; set; }
        public string Bank_Name { get; set; }
        public string Cheque_No { get; set; }
        public DateTime Cheque_Date { get; set; }
        public DateTime Posting_Date { get; set; }
        public string Payee { get; set; }
        public string Reference_No { get; set; }
        public string Payment_Narration { get; set; }
        public decimal Available_Amount { get; set; }
        public decimal Committed_Amount { get; set; }
        public string AIE_Receipt { get; set; }
        public string Created_By { get; set; }
        public DateTime Travel_Date { get; set; }
        public string Status { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Currency_Code { get; set; }
        public decimal Imprest_Amount { get; set; }
        public DateTime Imprest_Deadline { get; set; }
    }


}