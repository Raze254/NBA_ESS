using System;

namespace Latest_Staff_Portal.ViewModel
{
    public class BankLedgerEntry
    {
        public int Entry_No { get; set; }
        public DateTime Posting_Date { get; set; }
        public string Document_Type { get; set; }
        public string Document_No { get; set; }
        public string Bank_Account_No { get; set; }
        public string Description { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Our_Contact_Code { get; set; }
        public string Currency_Code { get; set; }
        public double Amount { get; set; }
        public double Debit_Amount { get; set; }
        public double Credit_Amount { get; set; }
        public double RunningBalance { get; set; }
        public double Amount_LCY { get; set; }
        public double Debit_Amount_LCY { get; set; }
        public double Credit_Amount_LCY { get; set; }
        public double RunningBalanceLCY { get; set; }
        public double Remaining_Amount { get; set; }
        public string Bal_Account_Type { get; set; }
        public string Bal_Account_No { get; set; }
        public bool Open { get; set; }
        public string User_ID { get; set; }
        public string Source_Code { get; set; }
        public string Reason_Code { get; set; }
        public bool Reversed { get; set; }
        public int Reversed_by_Entry_No { get; set; }
        public string Payment_Reference_No { get; set; }
        public string PaymentRef { get; set; }
        public int Reversed_Entry_No { get; set; }
        public string External_Document_No { get; set; }
        public int Dimension_Set_ID { get; set; }
        public string Shortcut_Dimension_3_Code { get; set; }
        public string Shortcut_Dimension_4_Code { get; set; }
        public string Shortcut_Dimension_5_Code { get; set; }
        public string Shortcut_Dimension_6_Code { get; set; }
        public string Shortcut_Dimension_7_Code { get; set; }
        public string Shortcut_Dimension_8_Code { get; set; }
    }
}