using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class ImprestSurrender
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string ImprestDeadline { get; set; }
        public string ImprestMemoSurrenderNo { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Payee { get; set; }
        public bool HOD { get; set; }
        public string CurrencyCode { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string DepartmentName { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string ProjectName { get; set; }
        public string ImprestAmount { get; set; }
        public string ImprestIssueDocNo { get; set; }
        public List<SelectListItem> ListOfImprests { get; set; }
        public string ReferenceNo { get; set; }
        public string ActualAmountSpent { get; set; }
        public decimal ActualAmountSpentLCY { get; set; }
        public decimal CashReceiptAmount { get; set; }
        public string RemainingAmount { get; set; }
        public string StrategicPlan { get; set; }
        public string ReportingYearCode { get; set; }
        public string WorkplanCode { get; set; }
        public string ActivityCode { get; set; }
        public string ExpenditureRequisitionCode { get; set; }
        public string PostingDate { get; set; }
        public string Posted { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfImprestIssueDoc { get; set; }
    }

    public class ApprovedImprestSurrender
    {
        
        public string No { get; set; }
        public string Date { get; set; }
        public string Posting_Date { get; set; }
        public string Imprest_Deadline { get; set; }
        public string Imprest_Issue_Doc_No { get; set; }
        public string Reference_No { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Payee { get; set; }
        public string Created_By { get; set; }
        public string Status { get; set; }
        public string Currency_Code { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Department_Name { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Project_Name { get; set; }
        public int Imprest_Amount { get; set; }
        public int Actual_Amount_Spent { get; set; }
        public int Actual_Amount_Spent_LCY { get; set; }
        public int Cash_Receipt_Amount { get; set; }
        public int Remaining_Amount { get; set; }
        public string Strategic_Plan { get; set; }
        public string Reporting_Year_Code { get; set; }
        public string Workplan_Code { get; set; }
        public string Activity_Code { get; set; }
        public string Expenditure_Requisition_Code { get; set; }
    }

    public class ImprestSurrenderLine
    {
        public string No { get; set; }
        public int LineNo { get; set; }
        public string AdvanceType { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string JobNo { get; set; }
        public string JobTaskNo { get; set; }
        public string Purpose { get; set; }
        public string Amount { get; set; }
        public string ActualSpent { get; set; }
        public string ActualSpentLCY { get; set; }
        public string RemainingAmountLCY { get; set; }
        public string ReceiptNo { get; set; }
        public string CashReceiptAmount { get; set; }
        public string RemainingAmount { get; set; }
    }

    public class ImprestSurrenderLinesList
    {
        public string Status { get; set; }
        public List<ImprestSurrenderLine> ListOfImprestSurrenderLines { get; set; }
    }

    public class ImpSurrenderLineDetails
    {
        public string Receipt { get; set; }
        public List<SelectListItem> ListOfPostedReceipts { get; set; }
        public string DocNo { get; set; }
        public string AccountNo { get; set; }
        public string Amount { get; set; }
        public string ActaulAmount { get; set; }
        public string EntryNo { get; set; }
        public string ClaimAmount { get; set; }
    }
    public class ImprestReceipt
    {
        public string DocNo { get; set; }
        public string Description { get; set; }
        public string DocumentDate { get; set; }
        public string PostingDate { get; set; }
        public string PayMode { get; set; }
        public string PayRef { get; set; }
        public string LoggedInUser { get; set; }
        public string Status { get; set; }
    }

}