using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class ResourceRequirements
    {
        public string No { get; set; }
        public string Description { get; set; }
        public string StrategyPlanID { get; set; }
        public string BudgetItem { get; set; }
        public List<SelectListItem> ListOfStrategyPlans { get; set; }
        public string YearReportingCode { get; set; }
        public List<SelectListItem> ListOfYears { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string GlobalDimension1Code { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string GlobalDimension3Code { get; set; }
        public string GlobalDimension4Code { get; set; }
        public string GlobalDimension5Code { get; set; }
        public string GlobalDimension6Code { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public string ApprovalStatus { get; set; }
        public string AnnualWorkplan { get; set; }
        public List<SelectListItem> ListOfAnualWorkplans { get; set; }
        public string FunctionalProcurmentPlanNo { get; set; }
        public List<SelectListItem> ListOfProcurementPlans { get; set; }
        public bool Posted { get; set; }
        public string Quarter1Amount { get; set; }
        public string Quarter2Amount { get; set; }
        public string Quarter3Amount { get; set; }
        public string Quarter4Amount { get; set; }
        public string TotalAmount { get; set; }
        public string Kraid { get; set; }
        public string CoreStrategyId { get; set; }
        public string ActivityId { get; set; }
        public string ActivityLine { get; set; }
        public string StrategyID { get; set; }
        public string PlanningBudgetType { get; set; }
    }

    public class StrategyWorkplanLine
    {
        public string No { get; set; }
        public string StrategyPlanID { get; set; }
        public string KRAID { get; set; }
        public List<SelectListItem> ListOfKraId { get; set; }
        public string CoreStrategyID { get; set; }
        public List<SelectListItem> ListOfCoreStrategyID { get; set; }
        public string StrategyID { get; set; }
        public List<SelectListItem> ListOfStrategyID { get; set; }
        public string ActivityID { get; set; }
        public List<SelectListItem> ListOfActivityID { get; set; }
        public string Description { get; set; }
        public string PerformanceIndicator { get; set; }
        public string AnnualTarget { get; set; }
    }

    public class StrategyWorkplanActivity

    {
        public string StrategicPlanId { get; set; }
        public string Code { get; set; }
        public string Descriptions { get; set; }
        public string Outputs { get; set; }
        public string PerformanceMeasures { get; set; }
        public string AnnualTarget { get; set; }
        public string LineNo { get; set; }
    }

    public class BudgetInput
    {
        public string WorkplanNo { get; set; }
        public string KRAID { get; set; }
        public string CoreStrategyID { get; set; }
        public string StrategyID { get; set; }
        public string ActivityId { get; set; }
        public int LineNo { get; set; }
        public string ActivityDescription { get; set; }
        public string Description { get; set; }
        public string TotalBudget { get; set; }
    }

    public class BudgetWorkings
    {
        public int EntryNo { get; set; }
        public string StrategyPlanId { get; set; }
        public string Kraid { get; set; }
        public string Item { get; set; }

        public string CoreStrategyID { get; set; }
        public string StrategyID { get; set; }
        public string ActivityID { get; set; }
        public int InputLineNo { get; set; }
        public string BudgetItem { get; set; }
        public string LineNo { get; set; }
        public string Type { get; set; }
        public string ItemType { get; set; }
        public string AccountType { get; set; }
        public string Description { get; set; }
        public string UnitCost { get; set; }
        public int Unit { get; set; }
        public string UnitOfMeasure { get; set; }
        public string TotalAmount { get; set; }
        public int Q1Quantity { get; set; }
        public decimal Q1Amount { get; set; }
        public int Q2Quantity { get; set; }
        public decimal Q2Amount { get; set; }
        public int Q3Quantity { get; set; }
        public decimal Q3Amount { get; set; }
        public int Q4Quantity { get; set; }
        public decimal Q4Amount { get; set; }
        public decimal[] Quantity { get; set; }
    }


    public class BudgetAmount
    {
        public string StrategyPlanID { get; set; }
        public string KRAID { get; set; }
        public string CoreStrategyID { get; set; }
        public string StrategyID { get; set; }
        public string ActivityID { get; set; }
        public int InputLineNo { get; set; }
        public string BudgetItem { get; set; }
        public string LineNo { get; set; }
        public string BudgetItemDescription { get; set; }
        public string ShortcutDimension3Code { get; set; }
        public string ShortcutDimension4Code { get; set; }
        public string ShortcutDimension5Code { get; set; }
        public string ShortcutDimension6Code { get; set; }
        public string Q1Quantity { get; set; }
        public string Q1UnitAmount { get; set; }
        public string Q1Amount { get; set; }
        public string Q2Quantity { get; set; }
        public string Q2UnitAmount { get; set; }
        public string Q2Amount { get; set; }
        public string Q3Quantity { get; set; }
        public string Q3UnitAmount { get; set; }
        public string Q3Amount { get; set; }
        public string Q4Quantity { get; set; }
        public string Q4UnitAmount { get; set; }
        public string Q4Amount { get; set; }
        public string TotalAmount { get; set; }
        public string[] ShortcutDimensionCodes { get; set; }
        public decimal[] Quantity { get; set; }
        public decimal[] UnitAmount { get; set; }
    }

    public class ExpenseRequisition
    {
        public string No { get; set; }
        public string Description { get; set; }
        public string CorporateStrategy { get; set; }
        public string ReportingPeriod { get; set; }
        public string BudgetCode { get; set; }
        public string Date { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GeographicalLocationName { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string AdminUnitName { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public string Workplan { get; set; }
        public List<SelectListItem> ListOfWorkPlans { get; set; }
        public string ActivityCode { get; set; }
        public List<SelectListItem> ListOfActivities { get; set; }
        public string ActivityDescription { get; set; }
        public bool RequiresImprest { get; set; }
        public bool RequiresSpecialAie { get; set; }
        public string ImprestType { get; set; }
        public int ImpType { get; set; }
        public string StartDate { get; set; }
        public int NoOfDays { get; set; }
        public string EndDate { get; set; }
        public string Subject { get; set; }
        public string Objective { get; set; }
        public bool RequiresDirectPayment { get; set; }
        public string DirectPay { get; set; }
        public bool RequiresPRN { get; set; }
        public string ProcurementPlanID { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> ListOfProcurementPlans { get; set; }
        public string PRNType { get; set; }
        public string LocationCode { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
        public string RequisitionProductGroup { get; set; }
        public string PPPlanCategory { get; set; }
        public List<SelectListItem> ListOfProcurementCategories { get; set; }
        public string ApprovalStatus { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string CommittedBy { get; set; }
        public DateTime DateCommitted { get; set; }
        public string RecalledBy { get; set; }
        public DateTime DateRecalled { get; set; }
        public string Aie { get; set; }
        public List<SelectListItem> ListOfAie { get; set; }

    }

    public class DraftWorkPlans
    {
        public string No { get; set; }
        public string Description { get; set; }
        public string StrategyPlanID { get; set; }
        public string YearReportingCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ApprovalStatus { get; set; }
        public string AnnualWorkplan { get; set; }
        public string FunctionalProcurmentPlanNo { get; set; }
        public bool Posted { get; set; }
        public string CeilingAmount { get; set; }
        public string Quarter1Amount { get; set; }
        public string Quarter2Amount { get; set; }
        public string Quarter3Amount { get; set; }
        public string Quarter4Amount { get; set; }
        public string TotalAmount { get; set; }
        public string Balance { get; set; }
        public string ResourceRequirementDoc { get; set; }
    }

    public class ExpenseRequisitionLine
    {
        public string ODataEtag { get; set; }
        public string DocumentNo { get; set; }
        public string ResourceReqNo { get; set; }
        public int LineNo { get; set; }
        public string ActivityId { get; set; }
        public List<SelectListItem> ListOfActivities { get; set; }
        public string GLAccount { get; set; }
        public List<SelectListItem> ListOfGl { get; set; }
        public string GLAccountName { get; set; }
        public string ShortcutDimension3Code { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public string ShortcutDimension4Code { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public string ShortcutDimension5Code { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public string ShortcutDimension6Code { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public string ShortcutDimension7Code { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public string ShortcutDimension8Code { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
        public int BudgetAllocation { get; set; }
        public int BudgetBalance { get; set; }
        public int TotalCommitments { get; set; }
        public int TotalAmount { get; set; }
        public int TotalAllocation { get; set; }
        public string Status { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        public string RecalledBy { get; set; }
        public DateTime RecalledOn { get; set; }
        public string Workplan { get; set; }
    }
    public class ContractPayments
    {
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public string Supplier { get; set; }
        public string ContractNo { get; set; }
        public string LpoNo { get; set; }
        public string ItemToLinePay { get; set; }
        public string ItemNo { get; set; }
        public string ExpenseDescription { get; set; }
        public string SupplierInvoiceNo { get; set; }
        public string SupplierInvoiceDate { get; set; }
        public string GLAccount { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }
        public string RecalledBy { get; set; }
        public DateTime RecalledOn { get; set; }
        public int SourceLineNo { get; set; }
    }
    public class ExpenseRequisitionLineList
    {
        public bool RequiresPRN { get; set; }

        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public bool RequiresImprest { get; set; }
        public bool RequiresDirectPay { get; set; }
        public string DirectPayType { get; set; }
        public string Status { get; set; }
        public List<ExpenseRequisitionLine> ListOfExpenseRequisitionLine { get; set; }
    }

    public class ExpenseRequisitionDocument
    {
        public ExpenseRequisition DocHeader { get; set; }
        public List<ExpenseRequisitionLine> ListOfExpenseRequisitionLine { get; set; }
    }

    public class WorkPlanBudgetLookup
    {
        public string StrategyPlanID { get; set; }
        public string KRAID { get; set; }
        public string CoreStrategyID { get; set; }
        public string StrategyID { get; set; }
        public string ActivityID { get; set; }
        public int InputLineNo { get; set; }
        public string BudgetItem { get; set; }
        public string LineNo { get; set; }
        public string InputDescription { get; set; }
        public string BudgetItemDescription { get; set; }
        public string ShortcutDimension3Code { get; set; }
        public string ShortcutDimension4Code { get; set; }
        public string ShortcutDimension5Code { get; set; }
        public string ShortcutDimension6Code { get; set; }
        public string ShortcutDimension7Code { get; set; }
        public string ShortcutDimension8Code { get; set; }
        public decimal Q1Amount { get; set; }
        public decimal Q2Amount { get; set; }
        public decimal Q3Amount { get; set; }
        public decimal Q4Amount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class ClaimLine
    {
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public string Payee { get; set; }
        public string Supplier { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string ExpenseDescription { get; set; }
        public string SupplierInvoiceNo { get; set; }
        public string SupplierInvoiceDate { get; set; }
        public string G_L_Account { get; set; }
        public string Vote_Item { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string Recalled_By { get; set; }
        public DateTime Recalled_On { get; set; }
        public int Source_Line_No { get; set; }
        public List<SelectListItem> ListOfSupplier { get; set; }
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public string ContractNo { get; set; }
        public string LpoNo { get; set; }
        public string ItemToLinePay { get; set; }
        public string ItemNo { get; set; }

    }

    public class ClaimLineList
    {
        public string Status { get; set; }
        public List<ClaimLine> ListOfCLaimLines { get; set; }
    }


    public class BudgetAllocation
    {
        public string DocumentNo { get; set; }
        public string BudgetCode { get; set; }
        public string Description { get; set; }
        public string PostingDate { get; set; }
        public string ConsolidationTemplate { get; set; }
        public string Quarter { get; set; }
        public string QuarterStartDate { get; set; }
        public string QuarterEndDate { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovalStatus { get; set; }
        public bool Posted { get; set; }
        public int NetAmount { get; set; }
        public List<SelectListItem> ListOfBudgetCode { get; set; }
        public List<SelectListItem> ListOfConsolidationTemplate { get; set; }

    }
    public class BudgetAllocationLines
    {
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public string Quarter { get; set; }
        public string GLAccountNo { get; set; }
        public string GLAccountName { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ShortuctDimension3Code { get; set; }
        public string ShortuctDimension4Code { get; set; }
        public string ShortuctDimension5Code { get; set; }
        public string ShortuctDimension6Code { get; set; }
        public string ShortuctDimension7Code { get; set; }
        public string ShortuctDimension8Code { get; set; }
        public string CurrentBalance { get; set; }
        public string AllocatedAmount { get; set; }
        public string Amount { get; set; }
        public string NewBalance { get; set; }
        public string NewAllocatedAmount { get; set; }
        public string AllocatedAmounts { get; set; }
        public string PostingDate { get; set; }
        public string QuarterStartDate { get; set; }
        public string QuarterEndDate { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class AllocationLines
    {
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public string Quarter { get; set; }
        public string GLAccountNo { get; set; }
        public string GLAccountName { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ShortuctDimension3Code { get; set; }
        public string ShortuctDimension4Code { get; set; }
        public string ShortuctDimension5Code { get; set; }
        public string ShortuctDimension6Code { get; set; }
        public string ShortuctDimension7Code { get; set; }
        public string ShortuctDimension8Code { get; set; }
        public int CurrentBalance { get; set; }
        public int AllocatedAmount { get; set; }
        public int Amount { get; set; }
        public int NewBalance { get; set; }
        public int NewAllocatedAmount { get; set; }
        public string QuarterStartDate { get; set; }
        public string QuarterEndDate { get; set; }
        public string SourceLineNo { get; set; }
    }
    public class Aie
    {
        public string No { get; set; }
        public string Description { get; set; }
        public string Geolocation { get; set; }
        public string AdminUnit { get; set; }
        public string ReportingPeriod { get; set; }
        public string Workplan { get; set; }
        public string BudgetCode { get; set; }
        public string Quarter { get; set; }
        public string AmountToAuthorize { get; set; }
        public string ApprovalStatus { get; set; }
    }
    public class AieLines
    {
        public string DocumentNo { get; set; }
        public string GLAccount { get; set; }
        public string GLAccountName { get; set; }
        public int AmountToAuthorize { get; set; }
        public int PlannedAmount { get; set; }
        public int Balance { get; set; }
    }
}