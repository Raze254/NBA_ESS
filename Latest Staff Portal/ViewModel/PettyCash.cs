using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class PettyCashVouchers
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string Pay_Mode { get; set; }
        public string Cheque_No { get; set; }
        public string Cheque_Date { get; set; }
        public string Paying_Bank_Account { get; set; }
        public string Payee { get; set; }
        public int Total_Amount_LCY { get; set; }
        public string Currency_Code { get; set; }
        public string Created_By { get; set; }
        public string Status { get; set; }
        public bool Posted { get; set; }
        public string Posted_By { get; set; }
        public string Posted_Date { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }

        public List<SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfFundingSource { get; set; }
        public List<SelectListItem> ListOfWorkplanActivities { get; set; }
        public List<SelectListItem> ListOfExpReq { get; set; }
        public List<SelectListItem> ListOfJobs { get; set; }
        public List<SelectListItem> ListOfJobTasks { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }

    }

    public class PettyCashVouchers2
    {
        public string No { get; set; }
        public string Account_Name { get; set; }
        public string Account_No { get; set; }
        public string Account_Type { get; set; }
        public string Activity_Code { get; set; }
        public int Actual_Amount { get; set; }
        public int Actual_Amount_Spent { get; set; }
        public int Actual_Amount_Spent_LCY { get; set; }
        public int Actual_Petty_Cash_Amount_Spent { get; set; }
        public int Actual_Project_Costs { get; set; }
        public int Advance_Recovery { get; set; }
        public int Amount_Approved { get; set; }
        public int Amount_Paid { get; set; }
        public int Amount_Pending { get; set; }
        public string Applies_To_Doc_No { get; set; }
        public string Approved_Work_Plan { get; set; }
        public bool Archive_Document { get; set; }
        public int Available_Amount { get; set; }
        public int Available_Funds { get; set; }
        public string Bank_Account_Name { get; set; }
        public string Bank_Code { get; set; }
        public string Bank_Name { get; set; }
        public int Board_PAYE { get; set; }
        public string Bounced_Payment_Doc_No { get; set; }
        public string Bounced_Payment_Type { get; set; }
        public string Bounced_Pv_Type { get; set; }
        public string Budget_Center_Name { get; set; }
        public bool Budgeted { get; set; }
        public string Cts_ID { get; set; }
        public bool Cancelled { get; set; }
        public string Cancelled_By { get; set; }
        public string Cancelled_Date { get; set; }
        public bool Car_Loan { get; set; }
        public string Case_Description { get; set; }
        public string Case_No { get; set; }
        public int Cash_Receipt_Amount { get; set; }
        public string Cashier { get; set; }
        public string Cheque_Date { get; set; }
        public string Cheque_No { get; set; }
        public string Cheque_Type { get; set; }
        public string Class { get; set; }
        public string Comments { get; set; }
        public int Commitments { get; set; }
        public int Commitments1 { get; set; }
        public int Committed_Amount { get; set; }
        public string Converted_By { get; set; }
        public string Court_Station { get; set; }
        public string Created_By { get; set; }
        public string Currency_Code { get; set; }
        public int Currency_Factor { get; set; }
        public string Date { get; set; }
        public string Date_Surrendered { get; set; }
        public string Date_Converted { get; set; }
        public string Date_Of_Joining { get; set; }
        public string Date_Of_Reporting { get; set; }
        public DateTime Date_Time_Sent { get; set; }
        public string Department_Code { get; set; }
        public string Department_Name { get; set; }
        public string Departure_Date { get; set; }
        public int Deposit_Amount { get; set; }
        public string Deposit_Receipt_No { get; set; }
        public string Destination { get; set; }
        public string Destination_Name { get; set; }
        public int Dimension_Set_ID { get; set; }
        public string Directorate_Code { get; set; }
        public string Division { get; set; }
        public string Division_Name { get; set; }
        public string Doc_Check_List_Code { get; set; }
        public string Document_Type { get; set; }
        public string E_Mail { get; set; }
        public string Eft_Code { get; set; }
        public string Effective_From_Month { get; set; }
        public string Error_Message { get; set; }
        public string Eslip { get; set; }
        public string Expenditure_Requisition_Code { get; set; }
        public string File_Name1 { get; set; }
        public string File_No { get; set; }
        public string File_No_Series { get; set; }
        public string Filename { get; set; }
        public bool Finance { get; set; }
        public bool Float_Reimbursement { get; set; }
        public string Folio_No { get; set; }
        public string Function_Name { get; set; }
        public string Funding_Source { get; set; }
        public bool Hod { get; set; }
        public int Imprest { get; set; }
        public int Imprest_Amount { get; set; }
        public int Imprest_Amount_LCY { get; set; }
        public int Imprest_Application_Commitment { get; set; }
        public string Imprest_Bank_Account_Number { get; set; }
        public string Imprest_Bank_Branch_Name { get; set; }
        public string Imprest_Bank_Name { get; set; }
        public bool Imprest_Created { get; set; }
        public string Imprest_Deadline { get; set; }
        public string Imprest_Issue_Date { get; set; }
        public string Imprest_Issue_Doc_No { get; set; }
        public string Imprest_Memo_No { get; set; }
        public string Imprest_Memo_Surrender_No { get; set; }
        public bool Imprest_Recovered { get; set; }
        public string Imprest_Type { get; set; }
        public int Imprest_Voucher_Amount { get; set; }
        public int Imprest_Voucher_Amount_LCY { get; set; }
        public int Interest_Amount { get; set; }
        public int Invoice_ID { get; set; }
        public int Invoice_Item_ID { get; set; }
        public string Job { get; set; }
        public string Job_Group { get; set; }
        public string Job_Name { get; set; }
        public int Job_Task_Budget { get; set; }
        public string Job_Task_Name { get; set; }
        public string Job_Task_No1 { get; set; }
        public string Job_Task_No { get; set; }
        public int Job_Task_Remaining_Amount { get; set; }
        public string Loan_Vendor { get; set; }
        public string Loan_Vendor_Name { get; set; }
        public int Monthly_Installment { get; set; }
        public int No_Of_Months_Deducted { get; set; }
        public int No_Printed { get; set; }
        public string No_Series { get; set; }
        public int No_Of_Resources { get; set; }
        public bool Notification_Sent { get; set; }
        public bool Notified { get; set; }
        public string Otp_Code { get; set; }
        public DateTime Otp_Datetime { get; set; }
        public string On_Behalf_Of { get; set; }
        public string Original_Document { get; set; }
        public string Original_Document_No { get; set; }
        public int Po_Commitments { get; set; }
        public int Prn_Commitments { get; set; }
        public string Prn_No { get; set; }
        public DateTime Pv_Creation_Date_Time { get; set; }
        public string Pv_Creator_ID { get; set; }
        public string Pv_No { get; set; }
        public int Pv_Remaining_Amount { get; set; }
        public string Pv_Type { get; set; }
        public string Pv_Voucher_Type { get; set; }
        public bool Part_Payment { get; set; }
        public string Pay_Mode { get; set; }
        public string Payee { get; set; }
        public string Payee_Bank_Account { get; set; }
        public string Payee_Bank_Branch { get; set; }
        public string Payee_Bank_Code { get; set; }
        public string Payee_Bank_Name { get; set; }
        public string Payee_Branch_Name { get; set; }
        public string Payee_Contact { get; set; }
        public string Paying_Bank_Account { get; set; }
        public string Paying_Bank_No { get; set; }
        public string Payment_Narration { get; set; }
        public string Payment_Release_Date { get; set; }
        public string Payment_Type { get; set; }
        public bool Payment_Processed { get; set; }
        public string Payroll_Deduction_Code { get; set; }
        public int Petty_Cash_Amount { get; set; }
        public string Petty_Cash_No { get; set; }
        public string Phone_Number { get; set; }
        public bool Posted { get; set; }
        public string Posted_By { get; set; }
        public string Posted_Date { get; set; }
        public string Posting_Date { get; set; }
        public string Posting_Group { get; set; }
        public string Posting_Group_Code { get; set; }
        public string Programme { get; set; }
        public string Project { get; set; }
        public int Project_Budget { get; set; }
        public string Project_Description { get; set; }
        public string Project_Name { get; set; }
        public string Purpose { get; set; }
        public string Reasons_To_Reopen { get; set; }
        public bool Receipt_Created { get; set; }
        public int Receipted_Petty_Cash_Amount { get; set; }
        public bool Recouped { get; set; }
        public string Recovery_From { get; set; }
        public string Recovery_Month { get; set; }
        public string Recovery_Start_Month { get; set; }
        public string Ref_No { get; set; }
        public string Reference_No { get; set; }
        public string Refund_Policy { get; set; }
        public string Refund_To { get; set; }
        public string Regional_Manager { get; set; }
        public string Regional_Manager_Name { get; set; }
        public string Rejected_By { get; set; }
        public int Remaining_Amount { get; set; }
        public int Remaining_Petty_Cash_Amount { get; set; }
        public string Remarks { get; set; }
        public string Reporting_Year_Code { get; set; }
        public string Resource_Req_No { get; set; }
        public int Resources_Total_Amount { get; set; }
        public int Resources_Total_Net { get; set; }
        public int Resources_Total_Other_Costs { get; set; }
        public int Resources_Total_WTax { get; set; }
        public string Responsibility_Center { get; set; }
        public string Retention_Receipt_No { get; set; }
        public string Return_Date { get; set; }
        public string Reversal_Date { get; set; }
        public bool Reversed { get; set; }
        public string Reversed_By { get; set; }
        public int Salary_Advance { get; set; }
        public int Salary_Last_Drawn { get; set; }
        public bool Salary_Payment { get; set; }
        public string Salary_Scale { get; set; }
        public bool Select { get; set; }
        public bool Selected { get; set; }
        public bool Send_For_Posting { get; set; }
        public string Sender_ID { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }

        public string Source_No { get; set; }
        public string Source_PV_No { get; set; }
        public string Source_Record_ID { get; set; }
        public string Standing_Imprest_Type { get; set; }
        public string Status { get; set; }
        public int Store_Requisition_Commitments { get; set; }
        public string Strategic_Plan { get; set; }
        public string Supplier_Name { get; set; }
        public string Surrender_Doc_No { get; set; }
        public string Surrender_Date { get; set; }
        public string Surrender_Due_Date { get; set; }
        public string Surrender_Status { get; set; }
        public bool Surrendered { get; set; }
        public string Surrendered_By { get; set; }
        public int Test { get; set; }
        public string Time_Posted { get; set; }
        public string Time_Converted { get; set; }
        public int Total_Amount { get; set; }
        public int Total_Amount_LCY { get; set; }
        public int Total_Amount_To_Refund { get; set; }
        public int Total_Amount_To_Utilize { get; set; }
        public int Total_Budget_Commitments { get; set; }
        public int Total_Committed_Amount { get; set; }
        public int Total_Loan_Amount { get; set; }
        public int Total_Net_Amount { get; set; }
        public int Total_Net_Pay { get; set; }
        public int Total_Payment_Amount_LCY { get; set; }
        public int Total_Retention_Amount { get; set; }
        public int Total_VAT_Amount { get; set; }
        public int Total_Witholding_Tax_Amount { get; set; }
        public string Travel_Date { get; set; }
        public string Type { get; set; }
        public string Unit_Name { get; set; }
        public bool Used_Claim { get; set; }
        public int VAT_Wthheld_Six { get; set; }
        public string Validated_Bank_Name { get; set; }
        public string Vendor_Bank { get; set; }
        public string Vendor_Bank_Account { get; set; }
        public string Vendor_Bank_Branch { get; set; }
        public string Volume_No { get; set; }
        public string Vote { get; set; }
        public int Vote_Amount { get; set; }
        public string Vote_Item { get; set; }
        public string Week_End_Date { get; set; }
        public string Week_Start_Date { get; set; }
        public string Workplan_Code { get; set; }
        public bool Banked { get; set; }
        public string Court_Order { get; set; }
        public string Cts_Receipt { get; set; }
        public string Document_ID { get; set; }
        public bool Is_Visible { get; set; }
        public bool Sent_To_Sharepoint { get; set; }

        public List<SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfFundingSource { get; set; }
        public List<SelectListItem> ListOfWorkplanActivities { get; set; }
        public List<SelectListItem> ListOfExpReq { get; set; }
        public List<SelectListItem> ListOfJobs { get; set; }
        public List<SelectListItem> ListOfJobTasks { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
    }

    public class PettyCashVoucherLines
    {

        public string No { get; set; }
        public int Line_No { get; set; }
        public string Bounced_Pv_No { get; set; }
        public string Type { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public int Amount { get; set; }
        public int Net_Amount { get; set; }
        public int Remaining_Amount { get; set; }
        public int VAT_Rate { get; set; }
        public int VAT_Six_Percent_Rate { get; set; }
        public string VAT_Withheld_Code { get; set; }
        public int VAT_Withheld_Amount { get; set; }
        public bool Budgetary_Control_A_C { get; set; }
        public int Advance_Recovery { get; set; }
        public int Retention_Amount { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Claim_Doc_No { get; set; }
        public string VAT_Code { get; set; }
        public string W_Tax_Code { get; set; }
        public string W_T_VAT_Code { get; set; }
        public int VAT_Amount { get; set; }
        public int W_Tax_Amount { get; set; }
        public int Total_Net_Pay { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfAccounts { get; set; }
    }

    public class NewPettyCashRequisition
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
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public string UoM { get; set; }
        public string PettyCashDueType { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }

        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfPettyCashDue { get; set; }
    }

    public class PettyCashTypes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class PettyCashTypesList
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfPettyCashTypes { get; set; }
        public List<SelectListItem> ListOfUnitMeasure { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
    }

    public class PettyCashHeader
    {
        public string No { get; set; }
        public string DateNeeded { get; set; }
        public string Remarks { get; set; }
        public NewPettyCashRequisition DocD { get; set; }
        public string Status { get; set; }
        public string TotalAmount { get; set; }
        public string RequestorNo { get; set; }
        public string RequestorName { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string Dim8 { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public string PettyCashDueType { get; set; }
        public string RespC { get; set; }
    }

    public class PettyCashLines
    {
        public string DocNo { get; set; }
        public string AdvanceType { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string ItemDesc2 { get; set; }
        public string Quantity { get; set; }
        public string UnitAmount { get; set; }
        public string Amount { get; set; }
        public string UoN { get; set; }
        public string Desination { get; set; }
        public string LnNo { get; set; }
    }

    public class PettyCashLinesList
    {
        public string Status { get; set; }
        public List<PettyCashLines> ListOfPettyCashLines { get; set; }
    }

    public class PettyCashItemDetails
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfPettyCashTypes { get; set; }
        public List<SelectListItem> ListOfUoM { get; set; }
        public List<SelectListItem> ListOfDestination { get; set; }
        public PettyCashLines ItemDetails { get; set; }
    }

    public class PettyCashDocument
    {
        public PettyCashHeader DocHeader { get; set; }
        public List<PettyCashLines> ListOfPettyCashLines { get; set; }
    }

    public class PettyCashSurrenders
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string Pay_Mode { get; set; }
        public string Cheque_No { get; set; }
        public string Cheque_Date { get; set; }
        public string Paying_Bank_Account { get; set; }
        public string Payee { get; set; }
        public int Total_Amount_LCY { get; set; }
        public int Actual_Petty_Cash_Amount_Spent { get; set; }
        public int Remaining_Petty_Cash_Amount { get; set; }
        public string Currency_Code { get; set; }
        public string Created_By { get; set; }
        public string Status { get; set; }
        public bool Posted { get; set; }
        public string Posted_By { get; set; }
        public string Posted_Date { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public int Petty_Cash_Amount { get; set; }

        public string Payment_Narration { get; set; }
    }


    public class PettyCashSurrender
    {

        public string No { get; set; }
        public string Job { get; set; }
        public string Job_Task_No { get; set; }
        public string Job_Name { get; set; }
        public string Project_Description { get; set; }
        public string Date { get; set; }
        public string Pay_Mode { get; set; }
        public string Paying_Bank_Account { get; set; }
        public string Bank_Name { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Petty_Cash_No { get; set; }
        public string Payee { get; set; }
        public string Payment_Narration { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Department_Name { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Project_Name { get; set; }
        public string Shortcut_Dimension_3_Code { get; set; }
        public string Unit_Name { get; set; }
        public string Currency_Code { get; set; }
        public string Posting_Date { get; set; }
        public int Total_Amount_LCY { get; set; }
        public int Actual_Petty_Cash_Amount_Spent { get; set; }
        public int Remaining_Petty_Cash_Amount { get; set; }
        public int Dimension_Set_ID { get; set; }
        public int Receipted_Petty_Cash_Amount { get; set; }
        public string Status { get; set; }
        public string Surrender_Date { get; set; }

        public List<SelectListItem> ListOfEmployees { get; set; }

        public List<SelectListItem> ListOfPettyCash { get; set; }
    }

    public class PettyCashSurrenderLines
    {
        
        public string No { get; set; }
        public int Line_No { get; set; }
        public string Account_Type { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int Actual_Spent { get; set; }
        public string Receipt_No { get; set; }
        public int Cash_Receipt_Amount { get; set; }
        public int Remaining_Amount { get; set; }
    }
}