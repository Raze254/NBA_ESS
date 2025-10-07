using System.Collections.Generic;
using System.ComponentModel;

namespace Latest_Staff_Portal.ViewModel
{
    public class FullUtilization
    {
        public string No { get; set; }
        public string DepositReceiptNo { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Date { get; set; }
        public string PayMode { get; set; }
        public string ChequeNo { get; set; }
        public string RefundPolicy { get; set; }
        public string ChequeDate { get; set; }
        public string PayingBankAccount { get; set; }
        public string BankName { get; set; }
        public string Payee { get; set; }
        public string OnBehalfOf { get; set; }
        public string PaymentNarration { get; set; }
        public string FundingSource { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string ProjectName { get; set; }
        public string ReasonsToReopen { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalAmount { get; set; }
        public string TotalNetAmount { get; set; }
        public string Status { get; set; }
        public string PostingDate { get; set; }
        public string CaseNo { get; set; }
        public string CaseDescription { get; set; }
        public string TotalAmountToRefund { get; set; }
        public string TotalAmountToUtilize { get; set; }
        public string PRNNo { get; set; }
        public string RefundTo { get; set; }
        public string InvoiceId { get; set; }
        public string DimensionSetId { get; set; }
        public string Archived { get; set; }
        public string Posted { get; set; }
        public string DocumentType { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string PvType { get; set; }
    }

    public class DimensionSetValues
    {
        [DisplayName("Dimension Set ID")] public int DimensionSetId { get; set; }

        [DisplayName("Dimension Code")] public string DimensionCode { get; set; }

        [DisplayName("Dimension Name")] public string DimensionName { get; set; }

        [DisplayName("Dimension Value Code")] public string DimensionValueCode { get; set; }

        [DisplayName("Dimension Value Name")] public string DimensionValueName { get; set; }
    }

    public class RefundFiles
    {
        public string id_doc_base64 { get; set; }
        public string id_doc_mime { get; set; }
        public string manual_receipt_base64 { get; set; }
        public string manual_receipt_mime { get; set; }
        public string court_order_base64 { get; set; }
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
        public RefundFiles refund_files { get; set; }
    }

    public class DocumentAttachments
    {
        public string Name { get; set; }
        public string DocumentCategory { get; set; }
    }

    public class DocumentAttachmentLists
    {
        public List<DocumentAttachments> DocList { get; set; }
    }


    public class DepositLines
    {
        public string No { get; set; }
        public int LineNo { get; set; }
        public string Type { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string AppliesToDocType { get; set; }
        public string AppliesToDocNo { get; set; }
        public string Amount { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBank { get; set; }
        public string PayeeBranchCode { get; set; }
        public string PayeeBankAcc { get; set; }
        public string AmountToUtilize { get; set; }
        public string AmountToRefund { get; set; }
        public string AmountLCY { get; set; }
        public string NetAmount { get; set; }
        public string RemainingAmount { get; set; }
        public int VATRate { get; set; }
        public int VATSixPercentRate { get; set; }
        public string VATWithheldCode { get; set; }
        public string VATWithheldAmount { get; set; }
        public string Project { get; set; }
        public string JobTaskNo { get; set; }
        public string VoteItem { get; set; }
        public string VoteAmount { get; set; }
        public string ActualToDate { get; set; }
        public string Commitments { get; set; }
        public string AvailableFunds { get; set; }
        public string CurrencyCode { get; set; }
        public bool BudgetaryControlAC { get; set; }
        public int AdvanceRecovery { get; set; }
        public string RetentionAmount { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string ClaimDocNo { get; set; }
        public string VATCode { get; set; }
        public string WTaxCode { get; set; }
        public string WTVATCode { get; set; }
        public string VATAmount { get; set; }
        public string WTaxAmount { get; set; }
        public string TotalNetPay { get; set; }
        public string Status { get; set; }
        public string PayeeAccountType { get; set; }
        public string PayeeAccountNo { get; set; }
        public string PayeeAccountName { get; set; }
    }

    public class DepositReceipts
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string PayMode { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string Amount { get; set; }
        public string AmountLCY { get; set; }
        public string BankCode { get; set; }
        public string CurrencyCode { get; set; }
        public string ReceivedFrom { get; set; }
        public string OnBehalfOf { get; set; }
        public string PaymentReference { get; set; }
        public string Cashier { get; set; }
        public string PostedDate { get; set; }
        public string PostedTime { get; set; }
        public string PostedBy { get; set; }
        public string Status { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ProjectName { get; set; }
        public string PRNNo { get; set; }
        public string CaseNo { get; set; }
        public string CaseType { get; set; }
        public string CaseTitle { get; set; }
        public string Posted { get; set; }
    }

    public class DepositReceiptLine
    {
        public string ReceiptNo { get; set; }
        public int LineNo { get; set; }
        public string ReceiptType { get; set; }
        public string TransactionType { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string CurrencyCode { get; set; }
        public int CurrencyFactor { get; set; }
        public int AmountLCY { get; set; }
        public string AppliesToID { get; set; }
        public string AppliesToDocNo { get; set; }
        public int GrossAmount { get; set; }
        public string PostingGroup { get; set; }
    }
}