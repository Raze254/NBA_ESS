namespace Latest_Staff_Portal.ViewModel
{
    public class DepositInvoicesList
    {
        public string Document_Type { get; set; }
        public string No { get; set; }
        public string Your_Reference { get; set; }
        public string Sell_to_Customer_No { get; set; }
        public string Posting_Date { get; set; }
        public bool Paid { get; set; }
        public string Invoice_Type { get; set; }
    }

    public class DepositInvoiceCard
    {
        public string Document_Type { get; set; }
        public string No { get; set; }
        public string Sell_to_Customer_No { get; set; }
        public string Sell_to_Customer_Name { get; set; }
        public string Your_Reference { get; set; }
        public string Posting_Date { get; set; }
        public string Due_Date { get; set; }
        public string Case_No { get; set; }
        public string Case_Type { get; set; }
        public string Case_Title { get; set; }
        public string Litigant_x0027_s_Lawyer { get; set; }
        public string Invoice_Type { get; set; }
        public bool Cancelled { get; set; }
        public string Cancelled_By { get; set; }
        public string Cancelled_Date { get; set; }
        public string Type { get; set; }
        public int Case_ID { get; set; }
        public bool Paid { get; set; }
        public string Bank_Type { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
    }
}