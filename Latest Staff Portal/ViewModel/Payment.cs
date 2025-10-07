using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class PaymentHeader
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string Remarks { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string RespC { get; set; }
        public string Pay_Mode { get; set; }
        public string ChequeNo { get; set; }
        public string PayingBank { get; set; }
        public string Paying_Bank_Account { get; set; }
        public string PaymentTo { get; set; }
        public string OnBehalfOf { get; set; }
        public string RaisedBy { get; set; }
        public string TotalAmount { get; set; }
        public string TotalVATAmount { get; set; }
        public string TotalWithHTAXAmount { get; set; }
        public string TotalRETAmount { get; set; }
        public string TotalVATWITHHAmount { get; set; }
        public string TotalPAYEAmount { get; set; }
        public string TotalNETAmount { get; set; }
        public string Status { get; set; }
    }

    public class PaymentLines
    {
        public string DocNo { get; set; }
        public string Type { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Amount { get; set; }
    }

    public class PaymentDocument
    {
        public PaymentHeader DocHeader { get; set; }
        public List<PaymentLines> ListOfPaymentLines { get; set; }
    }

    public class Eft
    {
        public string No { get; set; }
        public string BankNo { get; set; }
        public string PayeeBankName { get; set; }
        public string Remarks { get; set; }
        public string Transferred { get; set; }
        public string DateTransferred { get; set; }
        public string TimeTransferred { get; set; }
        public string TransferredBy { get; set; }
        public string DateEntered { get; set; }
        public string TimeEntered { get; set; }
        public string EnteredBy { get; set; }
        public string SalaryProcessingNo { get; set; }
        public string SalaryOptions { get; set; }
        public string Total { get; set; }
        public string TotalCount { get; set; }
        public string RTGS { get; set; }
        public string Status { get; set; }
        public string Batch { get; set; }
        public List<SelectListItem> ListOfBanks { get; set; }
        public List<SelectListItem> ListOfBatches { get; set; }
    }

    public class EftLines
    {
        public string DocumentNo { get; set; }
        public string LineNo { get; set; }
        public bool Select { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string PayeeBankCode { get; set; }
        public string PayeeBankName { get; set; }
        public string PayeeBankBranchCode { get; set; }
        public string PayeeBankBranchName { get; set; }
        public string PayeeBankAccountNo { get; set; }
        public string PayeeBankAccountName { get; set; }
        public string PayMode { get; set; }
        public string HeaderStatus { get; set; }
        public string PostingDate { get; set; }
        public string SourceDocumentType { get; set; }
        public string SourceDocumentNo { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string Voided { get; set; }
        public string VoidedBy { get; set; }
        public string DateVoided { get; set; }
        public string ReasonForVoiding { get; set; }
        public string Amount { get; set; }
    }
}