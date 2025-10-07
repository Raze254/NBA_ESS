using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class StaffClaims
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string PayingBankAccount { get; set; }
        public string BankName { get; set; }
        public string Payee { get; set; }
        public string PaymentNarration { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string ProjectName { get; set; }
        public decimal TotalAmountLCY { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string Status { get; set; }
        public string DimensionSetId { get; set; }
        public string Posted { get; set; }
        public string AvailableAmount { get; set; }
        public string CommittedAmount { get; set; }
        public string AieReceipt { get; set; }
        public string AieReceiptAmount { get; set; }
        public string ValidatedBankName { get; set; }

    }


    public class StaffClaimTypes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class StaffClaimTypesList
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfStaffClaimTypes { get; set; }
    }

    public class StaffClaimItemDetails
    {
        public List<SelectListItem> ListOfStaffClaimTypes { get; set; }
        public StaffClaimLines ItemDetails { get; set; }
    }

    public class StaffClaimLines
    {
        public string No { get; set; }
        public int Line_No { get; set; }
        public string Bounced_Pv_No { get; set; }
        public string Type { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Type_of_Expense { get; set; }
        public string Payee_Bank_Account_No { get; set; }
        public string Payee_Bank_Acc_Name { get; set; }
        public string rbankName { get; set; }
        public string Claim_Type { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Project_Name1 { get; set; }
        public string Currency_Code { get; set; }
        public int Amount { get; set; }
        public int Amount_LCY { get; set; }
        public List<SelectListItem> ListOfStaffClaimType { get; set; }
    }

    public class StaffClaimLinesList
    {
        public string Status { get; set; }
        public List<ClaimLine> ListOfStaffClaimLines { get; set; }
    }
}