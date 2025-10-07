using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class PaymentVoucher
    {
        public string No { get; set; }
        public string File_No { get; set; }
        public string Project_Description { get; set; }
        public string Vote_Item { get; set; }
        public string Job_Task_Name { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Date { get; set; }
        public string Posting_Date { get; set; }
        public string Salary_Payment { get; set; }
        public string Budgeted { get; set; }
        public string Project { get; set; }
        public string Pay_ModeID { get; set; }
        public List<SelectListItem> Pay_Mode { get; set; }
        public string Cheque_No { get; set; }
        public string Cheque_Date { get; set; }
        public string Paying_Bank_AccountID { get; set; }
        public List<SelectListItem> Paying_Bank_Account { get; set; }
        public string Bank_Name { get; set; }
        public string Payee { get; set; }
        public string On_behalf_of { get; set; }
        public string Payment_Narration { get; set; }
        public string Shortcut_Dimension_1_CodeID { get; set; }
        public List<SelectListItem> Shortcut_Dimension_1_Code { get; set; }
        public string Department_NameID { get; set; }
        public List<SelectListItem> Department_Name { get; set; }
        public string Shortcut_Dimension_2_CodeID { get; set; }
        public List<SelectListItem> Shortcut_Dimension_2_Code { get; set; }
        public string Project_Name { get; set; }
        public string Shortcut_Dimension_3_Code { get; set; }
        public string Reasons_to_Reopen { get; set; }
        public string Unit_Name { get; set; }
        public string Currency_CodeID { get; set; }
        public List<SelectListItem> Currency_Code { get; set; }
        public string Total_Amount { get; set; }
        public string Total_VAT_Amount { get; set; }
        public string Total_Witholding_Tax_Amount { get; set; }
        public string VAT_Wthheld_six_Percent { get; set; }
        public string Total_Retention_Amount { get; set; }
        public string Advance_Recovery { get; set; }
        public string Total_Net_Amount { get; set; }
        public string Board_PAYE { get; set; }
        public string Amount_Paid { get; set; }
        public string PV_Remaining_Amount { get; set; }
        public string Status { get; set; }
        public string Strategic_PlanID { get; set; }
        public string Posted { get; set; }
        public List<SelectListItem> Strategic_Plan { get; set; }
        public string Reporting_Year_CodeID { get; set; }
        public List<SelectListItem> Reporting_Year_Code { get; set; }
        public string Workplan_CodeID { get; set; }
        public List<SelectListItem> Workplan_Code { get; set; }
        public string Activity_CodeID { get; set; }
        public List<SelectListItem> Activity_Code { get; set; }
        public string Expenditure_Requisition_CodeID { get; set; }
        public List<SelectListItem> Expenditure_Requisition_Code { get; set; }
        public string DimensionSetId { get; set; }

    }

    public class PvLines
    {
        public string No { get; set; }
        public string LineNo { get; set; }
        public string Type { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string AppliesToDocType { get; set; }
        public string AppliesToDocNo { get; set; }
        public string Amount { get; set; }
        public string AmountLCY { get; set; }
        public string Description { get; set; }
        public string VATCode { get; set; }
        public string VATRate { get; set; }
        public string VATAmount { get; set; }
        public string WTaxCode { get; set; }
        public string WTaxRate { get; set; }
        public string WTaxAmount { get; set; }
        public string WTVATCode { get; set; }
        public string VATWithheldCode { get; set; }
        public string VATWithheldAmount { get; set; }
        public string RetentionCode { get; set; }
        public string RetentionRate { get; set; }
        public string RetentionAmount { get; set; }
        public string NetAmount { get; set; }
        public string RemainingAmount { get; set; }
        public string VATSixPercentRate { get; set; }
        public string Project { get; set; }
        public string JobTaskNo { get; set; }
        public string VoteItem { get; set; }
        public string VoteAmount { get; set; }
        public string ActualToDate { get; set; }
        public string Commitments { get; set; }
        public string AvailableFunds { get; set; }
        public string CurrencyCode { get; set; }
        public bool BudgetaryControlAC { get; set; }
        public string AdvanceRecovery { get; set; }
        public string PayingBankAcc { get; set; }
        public string EFTCode { get; set; }
        public string HeaderDimension1Code { get; set; }
        public string PaymentType2 { get; set; }
        public string PVType2 { get; set; }
        public string PayeeBankCode { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBankBranchCode { get; set; }
        public string PayeeBankBranchName { get; set; }
        public string PayeeBankAccountNo { get; set; }
        public string PayeeBankAccName { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string ClaimDocNo { get; set; }
        public bool Posted2 { get; set; }
        public string TotalNetPay { get; set; }
        public string Status { get; set; }

        public List<SelectListItem> ListOfAccountTypes { get; set; }
    }

    public class PvDocument
    {
        public string Status { get; set; }
        public List<PvLines> ListOfPvLines { get; set; }
    }

}