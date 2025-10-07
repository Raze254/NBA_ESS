using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class BidEvaluationTemplate
    {
        public string Code { get; set; }
        public string Template_type { get; set; }
        public string Description { get; set; }
        public string Default_Procurement_Type { get; set; }
        public int Total_Preliminary_Checks_Score { get; set; }
        public int Total_Technical_Evaluation_Percent { get; set; }
        public int Total_Financial_Evaluation { get; set; }
        public int Total_Assigned_Score_Weight_Percent { get; set; }
        public int Default_YES_Bid_Rating_Score_Percent { get; set; }
        public string NO_Bid_Rating_Response_Value { get; set; }
        public int Default_NO_Bid_Rating_Score_Percent { get; set; }
        public int _x0031__POOR_Option_Text_Bid_Score_Percent { get; set; }
        public int _x0032__FAIR_Option_Text_Bid_Score_Percent { get; set; }
        public int _x0033__GOOD_Option_Text_Bid_Score_Percent { get; set; }
        public int _x0034__VERY_GOOD_Text_Bid_Score_Percent { get; set; }
        public int _x0035__EXCELLENT_Text_Bid_Score_Percent { get; set; }
        public string Blocked { get; set; }
        public string No_Series { get; set; }
        public string Created_By { get; set; }
        public string Created_Date { get; set; }
        public string Created_Time { get; set; }
        public string Solicitation_Type { get; set; }
    }
    public class BidEvaluationTemplateLine
    {
        public string Template_ID { get; set; }
        public string Criteria_Group_ID { get; set; }
        public int Requirement_ID { get; set; }
        public string Evaluation_Type { get; set; }
        public string Evaluation_Requirement { get; set; }
        public string Rating_Type { get; set; }
        public int Assigned_Weight_Percent { get; set; }
        public int Target_Qty { get; set; }
        public string Desired_Perfomance_Direction { get; set; }
        public string Target_Value { get; set; }
    }
    public class BidEvaluationTemplateCard
    {
        public string Code { get; set; }
        public string Template_type { get; set; }
        public string Description { get; set; }
        public int Total_Preliminary_Checks_Score { get; set; }
        public int Total_Technical_Evaluation_Percent { get; set; }
        public int Total_Financial_Evaluation { get; set; }
        public string Created_By { get; set; }
        public string Created_Date { get; set; }
        public string Created_Time { get; set; }
        public string Blocked { get; set; }
        public int Default_YES_Bid_Rating_Score_Percent { get; set; }
        public int Default_NO_Bid_Rating_Score_Percent { get; set; }
        public string YES_Bid_Rating_Response_Value { get; set; }
        public string NO_Bid_Rating_Response_Value { get; set; }
        public int Max_Bid_Value_Limit { get; set; }
        public int _x0031__POOR_Option_Text_Bid_Score_Percent { get; set; }
        public int _x0032__FAIR_Option_Text_Bid_Score_Percent { get; set; }
        public int _x0033__GOOD_Option_Text_Bid_Score_Percent { get; set; }
        public int _x0034__VERY_GOOD_Text_Bid_Score_Percent { get; set; }
        public int _x0035__EXCELLENT_Text_Bid_Score_Percent { get; set; }
    }
    public class BidCriteriaGroup
    {
        public string Template_ID { get; set; }
        public string Criteria_Group_ID { get; set; }
        public string Evaluation_Type { get; set; }
        public string Description { get; set; }
        public int Total_Weight_Percent { get; set; }
    }
    public class BidEvaluationRegister
    {
        public string Code { get; set; }
        public string EvaluationDate { get; set; }
        public string EvaluationType { get; set; }
        public string EvaluatorCategory { get; set; }
        public string IFSCode { get; set; }
        public bool PreliminaryCheck  {get; set; }
        public string BidNo { get; set; }
        public string VendorNo { get; set; }
        public string BidderName { get; set; }
        public string Description { get; set; }
        public string EvaluationVenue { get; set; }
        public string BidScoringDocumentNo { get; set; }
        public string BidOpeningRegisterNo { get; set; }
        public string BidOpeningDate { get; set; }
        public string TenderEvaluationDeadline { get; set; }
        public string AppointedBidEvaluationCommi { get; set; }
        public string PrimaryRegion { get; set; }
        public string BidEnvelopType { get; set; }
        public string PurchaserCode { get; set; }
        public string EvaluationLead { get; set; }
        public string EvaluationLeadName { get; set; }
        public int NoOfPassedRequirements { get; set; }
        public int NoOfFailedRequirements { get; set; }
        public string DocumentStatus { get; set; }
        public string PreliminaryEvaluationScore { get; set; }
        public string TenderEvaluationCommRemarks { get; set; }
        public bool Posted { get; set; }
        public string Created_By { get; set; }

        public string Posted_By { get; set; }
        public string FinalOpeningDone { get; set; }
        public int WeightedTechnicalEvalScore { get; set; }
        public string TechnicalEvaluationDecision { get; set; }
        public int WeightedFinancialEvalScore { get; set; }
        public string AwardDecision { get; set; }
        public string PreliminaryEvaluationNo { get; set; }
        public string FinanceOpeningRegisterNo { get; set; }
        public string FinanceOpeningDate { get; set; }
        public string AwardType { get; set; }
        public string AnnualProcurementPlanId { get; set; }
        public string ProcurementPlanLineNo { get; set; }
        public string ProcurementPlanEntryNo1 { get; set; }
        public string FinancialYearCode { get; set; }
        public string BudgetNarration { get; set; }
        public int AvailableProcurementBudget { get; set; }
        public List<SelectListItem> ListOfRfQs { get; set; }
        public List<SelectListItem> ListOfLeads { get; set; }
        public bool IsSecretary { get; set; }
    }
    public class RecurringPurchaseLines
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Currency_Code { get; set; }
        public bool Posted { get; set; }
    }
    public class BidEvaluationRegisterLines
    {
        public string Code { get; set; }
        public int LineNo { get; set; }
        public string VendorNo { get; set; }
        public int RequirementID { get; set; }
        public string EvaluationRequirement { get; set; }
        public string CriteriaGroup { get; set; }
        public string CriteriaGroupID { get; set; }
        public string RatingType { get; set; }
        public string ResponseValue { get; set; }
        public string Remarks { get; set; }
        public int TargetQty { get; set; }
        public int BidResponseActualQty { get; set; }
        public string TargetValueKPI { get; set; }
        public int ScorePercent { get; set; }
        public int AssignedWeightPercent { get; set; }
        public int WeightedLineScore { get; set; }
        public string EvaluationResults { get; set; }
        public string BidScoringDocumentNo { get; set; }
        public List<SelectListItem> ListOfFindings { get; set; }

    }
    public class FinancialEvaluationLines
    {
        public string Document_ID { get; set; }
        public string Document_Type { get; set; }
        public string No { get; set; }
        public int Line_No { get; set; }
        public string Bidder_No { get; set; }
        public string Bidder_Name { get; set; }
        public int Read_out_Bid_Price_A { get; set; }
        public int Market_Price { get; set; }
        public int Final_Evaluated_Bid_Price { get; set; }
        public string Evaluation_Committee_Remarks { get; set; }
        public string Evaluation_Committee_Recomm { get; set; }
        public int Weighted_Tech_Score_Percent { get; set; }
        public string Tech_Evaluation_Ranking { get; set; }
        public string Financial_Evaluation_Date { get; set; }
        public string Financial_Evaluation_Ranking { get; set; }
        public string Ranking { get; set; }
        public int Weighted_Financial_Score_Percent { get; set; }
        public int Aggregate_Weighted_Score_Percent { get; set; }
        public int Arithmetic_Corrections_B { get; set; }
        public int Corrected_Bid_Price { get; set; }
    }
    public class FinalEvaluation
    {
        public string Tender_Name;
        public string Code { get; set; }
        public string DocumentDate { get; set; }
        public string FinancialEvaluationID { get; set; }
        public string IFSCode { get; set; }
        public string TenderName { get; set; }
        public string Description { get; set; }
        public string RaisedBy { get; set; }
        public string Name { get; set; }
        public string ProcurementMethod { get; set; }
        public bool SpecialRFQ { get; set; }
        public string X003CGeneralProcurementRemarksX003E { get; set; }
        public string ProcurementOfficerLicenseNo { get; set; }
        public string TenderCommitteeRole { get; set; }
        public string DesignationTitle { get; set; }
        public string PrimaryRegion { get; set; }
        public string BidOpeningDate { get; set; }
        public string EvaluationCompletionDate { get; set; }
        public string BidEvaluationTemplate { get; set; }
        public string AwardType { get; set; }
        public string AppointedBidOpeningComm { get; set; }
        public string AppointedBidEvaluationComm { get; set; }
        public string AppointedBidEvaluationCom { get; set; }
        public string GeneralProcurementRemarks { get; set; }
        public int NoOfSubmittedBids { get; set; }
        public string ApprovalStatus { get; set; }
        public string DocumentStatus { get; set; }
        public bool IsSecretary { get; set; }
    }
    public class FinalEvaluationLines
    {
        public string TabulationID { get; set; }
        public string TabulationType { get; set; }
        public string DocumentType { get; set; }
        public string No { get; set; }
        public string BidSubmissionNo { get; set; }
        public string BidderNo { get; set; }
        public string BidderName { get; set; }
        public bool PassedTechEvaluation { get; set; }
        public string EvaluationCommitteeRecomm { get; set; }
        public string EvaluationCommitteeRemarks { get; set; }
        public string AGPOCertficateNumber { get; set; }
        public string AGPOCategory { get; set; }
        public int ReadOutBidPriceA { get; set; }
        public int FinalEvaluatedBidPrice { get; set; }
        public string FinancialEvaluationDate { get; set; }
        public string PreBidRegisterNo { get; set; }
        public bool AttendedPreBidConference { get; set; }
        public string PrebidConferenceDate { get; set; }
        public string BidOpeningRegisterNo { get; set; }
        public string BidOpeningDate { get; set; }
        public bool LateBidOpeningStage { get; set; }
        public string WithdrawnBidOpeningStage { get; set; }
        public string WithdrawalNoticeNo { get; set; }
        public bool SuccesfulBidOpeningStage { get; set; }
        public string PreliminaryEvaluationDate { get; set; }
        public bool ResponsiveBid { get; set; }
        public string TechnicalEvaluationDate { get; set; }
        public int WeightedTechScorePercent { get; set; }
        public string TechEvaluationRanking { get; set; }
        public int WeightedFinancialScorePercent { get; set; }
        public string FinancialEvaluationRanking { get; set; }
        public int AggregateWeightedScorePercent { get; set; }
        public string AggregateRanking { get; set; }
        public string DueDilgenceDate { get; set; }
        public int CorrectedBidPriceCX003DAX002BB { get; set; }
        public string DueDiligenceRating { get; set; }
        public int UnconditionalDiscAmountE { get; set; }
        public int CorrectedX0026DiscBidPrice { get; set; }
        public int AnyAdditionalAdjustmentsG { get; set; }
        public int AnyPricedDeviationsH { get; set; }
        public string FinancialEvaluationCommRem { get; set; }
        public int ArithmeticCorrectionsB { get; set; }
        public string BidOpeningResult { get; set; }
        public string BidOpeningCommitteeRemarks { get; set; }
        public string PreliminaryEvaluationReg { get; set; }
        public string PreliminaryEvaluationOutcome { get; set; }
        public string PreminaryEvaluationCommittee { get; set; }
        public string BidOpeningFinanceRegister { get; set; }
        public string BidOpeningDateFinancial { get; set; }
        public string FinancialEvalRegisterNo { get; set; }
    }
    public class TechnicalEvaluation
    {
        public string Code { get; set; }
        public string Evaluation_Date { get; set; }
        public string IFS_Code { get; set; }
        public string Preliminary_Evaluation_No { get; set; }
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
        public bool Posted { get; set; }
        public string Created_By { get; set; }
        public string Technical_Evaluation_Decision { get; set; }
        public string Tender_Evaluation_Comm_Remarks { get; set; }
        public string Tech_Evaluation_Ranking { get; set; }
        public int Weighted_Technical_Eval_Score { get; set; }
        public bool IsSecretary { get; set; }
        public bool isMember { get; set; }
        public string Posted_By { get; set; }
        public DateTime Posted_Date { get; set; }
        public string PreliminaryEvaluationScore { get; set; }
    }
    public class TechBidEvaluationScoreEntry
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
    public class FinancialEvaluation
    {
        public string Code { get; set; }
        public string Evaluation_Type { get; set; }
        public string Award_Type { get; set; }
        public string Evaluator_Category { get; set; }
        public string Evaluation_Lead { get; set; }
        public string Evaluation_Lead_Name { get; set; }
        public string Bid_No { get; set; }
        public string Vendor_No { get; set; }
        public string Bidder_Name { get; set; }
        public string Description { get; set; }
        public string Evaluation_Date { get; set; }
        public string Evaluation_Venue { get; set; }
        public string IFS_Code { get; set; }
        public string Bid_Scoring_Document_No { get; set; }
        public string Bid_Scoring_Document_Type { get; set; }
        public string Bid_Opening_Register_No { get; set; }
        public string Bid_Opening_Date { get; set; }
        public string Tender_Evaluation_Deadline { get; set; }
        public string Appointed_Bid_Evaluation_Commi { get; set; }
        public string Primary_Region { get; set; }
        public string Bid_Envelop_Type { get; set; }
        public bool Final_Opening_Done { get; set; }
        public string Purchaser_Code { get; set; }
        public string Preliminary_Evaluation_Score { get; set; }
        public int Weighted_Technical_Eval_Score { get; set; }
        public string Technical_Evaluation_Decision { get; set; }
        public int Weighted_Financial_Eval_Score { get; set; }
        public string Award_Decision { get; set; }
        public string Tender_Evaluation_Comm_Remarks { get; set; }
    }
    public class FinancialEvaluationCard
    {
        public string Code { get; set; }
        public string IFS_Code { get; set; }
        public string Description { get; set; }
        public string Evaluation_Date { get; set; }
        public string Evaluation_Venue { get; set; }
        public string Finance_Opening_Register_No { get; set; }
        public string Finance_Opening_Date { get; set; }
        public string Bid_Scoring_Document_No { get; set; }
        public string Tender_Evaluation_Deadline { get; set; }
        public string Appointed_Bid_Evaluation_Commi { get; set; }
        public string Primary_Region { get; set; }
        public string Bid_Envelop_Type { get; set; }
        public string Award_Type { get; set; }
        public string Purchaser_Code { get; set; }
        public string Evaluation_Lead { get; set; }
        public string Evaluation_Lead_Name { get; set; }
        public string Document_Status { get; set; }
        public bool Posted { get; set; }
        public string Posted_By { get; set; }
        public string Posted_Date { get; set; }
        public string Annual_Procurement_Plan_ID { get; set; }
        public string Procurement_Plan_Line_No { get; set; }
        public string Procurement_Plan_Entry_No1 { get; set; }
        public string Financial_Year_Code { get; set; }
        public string Budget_Narration { get; set; }
        public int Available_Procurement_Budget { get; set; }
        public int Weighted_Financial_Eval_Score { get; set; }
        public string Award_Decision { get; set; }
        public string Tender_Evaluation_Comm_Remarks { get; set; }
    }

    public class LPOPurchaseOrder
    {
        public string Document_Type { get; set; }
        public string No { get; set; }
        public string Buy_from_Vendor_No { get; set; }
        public string Buy_from_Vendor_Name { get; set; }
        public string Posting_Description { get; set; }
        public string Buy_from_Address { get; set; }
        public string Buy_from_Address_2 { get; set; }
        public string Buy_from_City { get; set; }
        public string Buy_from_County { get; set; }
        public string Buy_from_Post_Code { get; set; }
        public string Buy_from_Country_Region_Code { get; set; }
        public string Buy_from_Contact_No { get; set; }
        public string BuyFromContactPhoneNo { get; set; }
        public string BuyFromContactMobilePhoneNo { get; set; }
        public string BuyFromContactEmail { get; set; }
        public string Buy_from_Contact { get; set; }
        public string Document_Date { get; set; }
        public string Posting_Date { get; set; }
        public string VAT_Reporting_Date { get; set; }
        public string Due_Date { get; set; }
        public string Job_Queue_Status { get; set; }
        public string Vendor_Invoice_No { get; set; }
        public string Purchaser_Code { get; set; }
        public int No_of_Archived_Versions { get; set; }
        public string Order_Date { get; set; }
        public string Responsibility_Center { get; set; }
        public string Procurement_Type { get; set; }
        public string Quote_No { get; set; }
        public string Vendor_Order_No { get; set; }
        public string Vendor_Shipment_No { get; set; }
        public string Order_Address_Code { get; set; }
        public string Assigned_User_ID { get; set; }
        public string Status { get; set; }
        public string Receiving_No_Series { get; set; }
        public string Posting_No { get; set; }
        public string Currency_Code { get; set; }
        public string Expected_Receipt_Date { get; set; }
        public bool Prices_Including_VAT { get; set; }
        public string VAT_Bus_Posting_Group { get; set; }
        public string Vendor_Posting_Group { get; set; }
        public string Payment_Terms_Code { get; set; }
        public string Payment_Method_Code { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public int Payment_Discount_Percent { get; set; }
        public string Pmt_Discount_Date { get; set; }
        public string Journal_Templ_Name { get; set; }
        public bool Tax_Liable { get; set; }
        public string Tax_Area_Code { get; set; }
        public string Shipment_Method_Code { get; set; }
        public string Payment_Reference { get; set; }
        public string Creditor_No { get; set; }
        public string On_Hold { get; set; }
        public string Inbound_Whse_Handling_Time { get; set; }
        public string Lead_Time_Calculation { get; set; }
        public string Requested_Receipt_Date { get; set; }
        public string Promised_Receipt_Date { get; set; }
        public string ShippingOptionWithLocation { get; set; }
        public string Location_Code { get; set; }
        public string Sell_to_Customer_No { get; set; }
        public string Ship_to_Code { get; set; }
        public string Ship_to_Name { get; set; }
        public string Ship_to_Address { get; set; }
        public string Ship_to_Address_2 { get; set; }
        public string Ship_to_City { get; set; }
        public string Ship_to_County { get; set; }
        public string Ship_to_Post_Code { get; set; }
        public string Ship_to_Country_Region_Code { get; set; }
        public string Ship_to_Contact { get; set; }
        public string PayToOptions { get; set; }
        public string Pay_to_Name { get; set; }
        public string Pay_to_Address { get; set; }
        public string Pay_to_Address_2 { get; set; }
        public string Pay_to_City { get; set; }
        public string Pay_to_County { get; set; }
        public string Pay_to_Post_Code { get; set; }
        public string Pay_to_Country_Region_Code { get; set; }
        public string Pay_to_Contact_No { get; set; }
        public string Pay_to_Contact { get; set; }
        public string PayToContactPhoneNo { get; set; }
        public string PayToContactMobilePhoneNo { get; set; }
        public string PayToContactEmail { get; set; }
        public string Remit_to_Code { get; set; }
        public string Remit_to_Name { get; set; }
        public string Remit_to_Address { get; set; }
        public string Remit_to_Address_2 { get; set; }
        public string Remit_to_City { get; set; }
        public string Remit_to_County { get; set; }
        public string Remit_to_Post_Code { get; set; }
        public string Remit_to_Country_Region_Code { get; set; }
        public string Remit_to_Contact { get; set; }
        public string Transaction_Specification { get; set; }
        public string Transaction_Type { get; set; }
        public string Transport_Method { get; set; }
        public string Entry_Point { get; set; }
        public string Area { get; set; }
        public bool Applicable_For_Serv_Decl { get; set; }
        public int Prepayment_Percent { get; set; }
        public bool Compress_Prepayment { get; set; }
        public string Prepmt_Payment_Terms_Code { get; set; }
        public string Prepayment_Due_Date { get; set; }
        public int Prepmt_Payment_Discount_Percent { get; set; }
        public string Prepmt_Pmt_Discount_Date { get; set; }
        public string Vendor_Cr_Memo_No { get; set; }
    }




}


