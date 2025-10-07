using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class VendorDueDiligence
    {
        public string Code { get; set; }
        public string Evaluation_Date { get; set; }
        public string IFS_Code { get; set; }
        public string Bid_No { get; set; }
        public string Vendor_No { get; set; }
        public string Bidder_Name { get; set; }
        public string Description { get; set; }
        public string Evaluation_Venue { get; set; }
        public string Bid_Scoring_Document_No { get; set; }
        public string Bid_Opening_Register_No { get; set; }
        public string Bid_Opening_Date { get; set; }
        public string Tender_Evaluation_Deadline { get; set; }
        public string Appointed_Bid_Evaluation_Commi { get; set; }
        public string Primary_Region { get; set; }
        public string Bid_Envelop_Type { get; set; }
        public string Purchaser_Code { get; set; }
        public string Evaluation_Lead { get; set; }
        public string Evaluation_Lead_Name { get; set; }
        public int No_of_Passed_Requirements { get; set; }
        public int No_of_Failed_Requirements { get; set; }
        public string Document_Status { get; set; }
        public string Due_Diligence_Report_ID { get; set; }
        public string Due_Diligence_Rating { get; set; }
        public List<SelectListItem> ListOfEvaluationCommittee { get; set; } //negotiation committee vouchers
    }


    public class RFQS
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class DueDiligenceLines
    {
        public string Code { get; set; }
        public int Line_No { get; set; }
        public string Vendor_No { get; set; }
        public int Requirement_ID { get; set; }
        public string Evaluation_Requirement { get; set; }
        public string Criteria_Group { get; set; }
        public string Rating_Type { get; set; }
        public string Response_Value { get; set; }
        public string Remarks { get; set; }
        public int Target_Qty { get; set; }
        public int Bid_Response_Actual_Qty { get; set; }
        public string Target_Value_KPI { get; set; }
        public int Score_Percent { get; set; }
        public int Assigned_Weight_Percent { get; set; }
        public int Weighted_Line_Score { get; set; }
        public string Evaluation_Results { get; set; }
    }




    //notice of awards
    public class NoticeOfAward
    {

        public string Code { get; set; }
        public string Document_Date { get; set; }
        public string Professional_Opinion_ID { get; set; }
        public string Final_Evaluation_Report_ID { get; set; }
        public string IFS_Code { get; set; }
        public string Tender_Name { get; set; }
        public string Description { get; set; }
        public string Primary_Region { get; set; }
        public string Awarded_Bid_No { get; set; }
        public string Awarded_Bidder_No { get; set; }
        public string Awarded_Bidder_Name { get; set; }
        public int Award_Tender_Sum_Inc_Taxes { get; set; }
        public string Award_Acceptance_Deadline { get; set; }
        public string Award_Acceptance_Response { get; set; }
        public string Award_Type { get; set; }
        public string Min_Contract_Holding { get; set; }
        public string Earliest_Contract_Issuance_Dt { get; set; }
        public string Approval_Status { get; set; }
        public string Document_Status { get; set; }
        public string General_Procurement_Remarks { get; set; }
    }


    public class NoticeOfAwardLines
    {
        public string Tabulation_ID { get; set; }
        public string Tabulation_Type { get; set; }
        public string Document_Type { get; set; }
        public string No { get; set; }
        public string Bidder_No { get; set; }
        public string Bidder_Name { get; set; }
        public string Pre_bid_Register_No { get; set; }
        public bool Attended_Pre_bid_Conference { get; set; }
        public string Prebid_Conference_Date { get; set; }
        public string Bid_Opening_Register_No { get; set; }
        public string Bid_Opening_Date { get; set; }
        public bool Late_Bid_Opening_Stage { get; set; }
        public string Withdrawn_Bid_Opening_Stage { get; set; }
        public string Withdrawal_Notice_No { get; set; }
        public bool Succesful_Bid_Opening_Stage { get; set; }
        public string Preliminary_Evaluation_Date { get; set; }
        public bool Responsive_Bid { get; set; }
        public string Technical_Evaluation_Date { get; set; }
        public int Weighted_Tech_Score_Percent { get; set; }
        public bool Passed_Tech_Evaluation { get; set; }
        public string Tech_Evaluation_Ranking { get; set; }
        public int Final_Evaluated_Bid_Price { get; set; }
        public string Financial_Evaluation_Date { get; set; }
        public int Weighted_Financial_Score_Percent { get; set; }
        public string Financial_Evaluation_Ranking { get; set; }
        public int Aggregate_Weighted_Score_Percent { get; set; }
        public string Aggregate_Ranking { get; set; }
        public string Due_Dilgence_Date { get; set; }
        public int Corrected_Bid_Price_C_x003D_A_x002B_B { get; set; }
        public string Due_Diligence_Rating { get; set; }
        public int Unconditional_Disc_Amount_E { get; set; }
        public int Corrected__x0026__Disc_Bid_Price { get; set; }
        public int Any_Additional_Adjustments_G { get; set; }
        public int Any_Priced_Deviations_H { get; set; }
        public string Financial_Evaluation_Comm_Rem { get; set; }
        public string Evaluation_Committee_Recomm { get; set; }
        public int Read_out_Bid_Price_A { get; set; }
        public int Arithmetic_Corrections_B { get; set; }
        public string Bid_Opening_Result { get; set; }
        public string Bid_Opening_Committee_Remarks { get; set; }
        public string Preliminary_Evaluation_Reg { get; set; }
        public string Preliminary_Evaluation_Outcome { get; set; }
        public string Preminary_Evaluation_Committee { get; set; }
        public string Bid_Opening_Finance_Register { get; set; }
        public string Bid_Opening_Date_Financial { get; set; }
        public string Financial_Eval_Register_No { get; set; }
    }

    public class NoticeOfAwardLinesList
    {
        public string Status { get; set; }
        public List<NoticeOfAwardLines> ListOfNOALines { get; set; }
    }







}