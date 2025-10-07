using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class Contract
    {
        public string No { get; set; }

        public string Document_Type { get; set; }
        public DateTime Document_Date { get; set; }
        public string Contract_Description { get; set; }
        public DateTime Contract_Start_Date { get; set; }

        public string Contract_Duration { get; set; }
        public DateTime Contract_End_Date { get; set; }
        public string Institution { get; set; }
        public string Party_Description { get; set; }
        public string Vendor_Party { get; set; }
        public string Company_Head { get; set; }
        public string Mou_Description { get; set; }
        public string Scope_of_Service { get; set; }
        public string Scope_Description { get; set; }
        public string Vendor_Description { get; set; }
        public string Notice_of_Award_No { get; set; }
        public string Awarded_Bid_No { get; set; }
        public decimal Award_Tender_Sum_Inc_Taxes { get; set; }
        public string IFS_Code { get; set; }
        public string Tender_Name { get; set; }
        public string Serial_No { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public decimal Area_Space { get; set; }
        public string Payment_Status { get; set; }
        public string Job { get; set; }
        public string Contract_Type { get; set; }
        public string Governing_Laws { get; set; }
        public string Contract_Status { get; set; }
        public string Status { get; set; }
        public string Procuring_Entity_PE_Name { get; set; }
        public string PE_Representative { get; set; }
        public decimal Amount { get; set; }
        public DateTime Due_Date { get; set; }
        public string Your_Reference { get; set; }
        public string Reason_For_ammendment { get; set; }
        public bool Renewed { get; set; }
        public DateTime Renewed_Date { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }

        public List<SelectListItem> ListOfVendors { get; set; }
        public string Shortcut_Dimension_3_Code { get; set; }
        public DateTime Preliminary_Evaluation_Date { get; set; }
        public string Buy_from_Vendor_No { get; set; }
        public string Buy_from_Vendor_Name { get; set; }
        public string VAT_Registration_No { get; set; }
        public string Buy_from_Address { get; set; }
        public string Buy_from_Address_2 { get; set; }
        public string Buy_from_Post_Code { get; set; }
        public string Buy_from_City { get; set; }
        public string Buy_from_Contact_No { get; set; }
        public string Buy_from_Country_Region_Code { get; set; }
        public string Language_Code { get; set; }
    }

    public class ContractLines
    {
        public string OdataEtag { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public string Type { get; set; }
        public string No { get; set; }
        public string CrossReferenceNo { get; set; }
        public string VariantCode { get; set; }
        public string VATProdPostingGroup { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public string UnitOfMeasure { get; set; }
        public string FeeType { get; set; }
        public decimal DirectUnitCost { get; set; }
        public decimal IndirectCostPercent { get; set; }
        public decimal UnitCostLCY { get; set; }
        public string Budget { get; set; }
        public string BudgetLine { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public decimal UnitPriceLCY { get; set; }
        public decimal LineDiscountPercent { get; set; }
        public decimal LineAmount { get; set; }
        public decimal LineDiscountAmount { get; set; }
        public string LocationCode { get; set; }
        public bool AllowInvoiceDisc { get; set; }
        public int QtyToReceive { get; set; }
        public int QuantityReceived { get; set; }
        public int QuantityInvoiced { get; set; }
        public DateTime ExpectedReceiptDate { get; set; }
        public string ShortcutDimension2Code { get; set; }

        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public string ShortcutDimCode3 { get; set; }
        public string ShortcutDimCode4 { get; set; }
        public string ShortcutDimCode5 { get; set; }
        public string ShortcutDimCode6 { get; set; }
        public string ShortcutDimCode7 { get; set; }
        public string ShortcutDimCode8 { get; set; }
        public decimal AmountBeforeDiscount { get; set; }
        public decimal InvoiceDiscountAmount { get; set; }
        public decimal InvoiceDiscPct { get; set; }
        public decimal TotalAmountExclVAT { get; set; }
        public decimal TotalVATAmount { get; set; }
        public decimal TotalAmountInclVAT { get; set; }
    }

    public class ContractPaymentLines
    {
        public string ETag { get; set; }
        public string DocumentType { get; set; }
        public string No { get; set; }
        public string InstalmentCode { get; set; }
        public string PaymentCertificateType { get; set; }
        public string Description { get; set; }
        public decimal PaymentPercent { get; set; }
        public string CurrencyCode { get; set; }
        public decimal PlannedAmount { get; set; }
        public decimal PlannedAmountLCY { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PaidAmountLCY { get; set; }
    }

    public class PaymentLine
    {
        public string OdataEtag { get; set; }
        public string DocumentType { get; set; }
        public string No { get; set; }
        public string InstalmentCode { get; set; }
        public string PaymentCertificateType { get; set; }
        public string Description { get; set; }
        public decimal PaymentPercent { get; set; }
        public string CurrencyCode { get; set; }
        public decimal PlannedAmount { get; set; }
        public decimal PlannedAmountLcy { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PaidAmountLcy { get; set; }

        public List<SelectListItem> ListOfCurrencyCodes { get; set; }

        public List<SelectListItem> PaymentCertificateType4 { get; set; }

        public string SelectedCertificateType { get; set; }
    }
}