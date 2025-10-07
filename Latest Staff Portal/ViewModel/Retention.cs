using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class Retention
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string PostingDate { get; set; }
        public string Payee { get; set; }
        public string PayingBankAccount { get; set; }
        public string BankName { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public string Posted { get; set; }
        public string PostedBy { get; set; }
        public string PostedDate { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalAmount { get; set; }
        public string TotalAmountLCY { get; set; }
        public string  TotalNetAmount { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string DimensionSetId { get; set; }
        public string Eslip { get; set; }
        public List<SelectListItem> Pay_Mode { get; set; }
        public List<SelectListItem> Paying_Bank_Account { get; set; }
        public List<SelectListItem> Shortcut_Dimension_1_Code { get; set; }
        public List<SelectListItem> Department_Name { get; set; }
        public List<SelectListItem> Shortcut_Dimension_2_Code { get; set; }
        public List<SelectListItem> Currency_Code { get; set; }
        public List<SelectListItem> Strategic_Plan { get; set; }
        public List<SelectListItem> Reporting_Year_Code { get; set; }
        public List<SelectListItem> Workplan_Code { get; set; }
        public List<SelectListItem> Activity_Code { get; set; }
        public List<SelectListItem> Expenditure_Requisition_Code { get; set; }

    }

    public class RetentionLines
    {
        public string No { get; set; }
        public string LineNo { get; set; }
        public string BouncedPvNo { get; set; }
        public string Type { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string AppliesToDocType { get; set; }
        public string Amount { get; set; }
        public string AmountLCY { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string NetAmount { get; set; }
        public string NetAmountLCY { get; set; }
        public string PayeeBankCode { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBankBranchCode { get; set; }
        public string PayeeBankBranchName { get; set; }
        public string PayeeBankAccountNo { get; set; }
        public string PayeeAccountNo { get; set; }
        public string PayeeAccountName { get; set; }
        public List<SelectListItem> ListOfAccountTypes { get; set; }
        public List<SelectListItem> ListOfVendors { get; set; }
    }

    public class RetentionDocument
    {
        public string Status { get; set; }
        public List<RetentionLines> ListOfPvLines { get; set; }
    }

}