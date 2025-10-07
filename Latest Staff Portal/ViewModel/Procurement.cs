using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class InvitationToTender
    {
        public string Assigned_Procurement_Officer;
        public string Currency_Code;
        public string Project_ID;
        public string Road_Code;
        public string odataetag { get; set; }
        public string Code { get; set; }
        public string Invitation_Notice_Type { get; set; }
        public string Document_Date { get; set; }
        public string Tender_Name { get; set; }
        public string Tender_Summary { get; set; }
        public string External_Document_No { get; set; }
        public string Stage_1_EOI_Invitation { get; set; }
        public string PRN_No { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Location_Code { get; set; }
        public string Description { get; set; }
        public string Procurement_Type { get; set; }
        public string Requisition_Product_Group { get; set; }
        public string Procurement_Category_ID { get; set; }
        public string Lot_No { get; set; }
        public string Target_Bidder_Group { get; set; }
        public string Solicitation_Type { get; set; }
        public string Bid_Selection_Method { get; set; }
        public string Requisition_Template_ID { get; set; }
        public string Bid_Scoring_Template { get; set; }
        public string Bid_Opening_Committe { get; set; }
        public string Bid_Evaluation_Committe { get; set; }
        public string Procurement_Method { get; set; }
        public bool Enforce_Mandatory_Pre_bid_Visi { get; set; }
        public string Mandatory_Pre_bid_Visit_Date { get; set; }
        public string Prebid_Meeting_Address { get; set; }
        public string Prebid_Meeting_Register_ID { get; set; }
        public string Bid_Envelop_Type { get; set; }
        public bool Sealed_Bids { get; set; }
        public string Tender_Validity_Duration { get; set; }
        public string Tender_Validity_Expiry_Date { get; set; }
        public string Purchaser_Code { get; set; }
        public string Language_Code { get; set; }
        public bool Mandatory_Special_Group_Reserv { get; set; }
        public bool Bid_Tender_Security_Required { get; set; }
        public int Bid_Security_Percent { get; set; }
        public int Bid_Security_Amount_LCY { get; set; }
        public string Bid_Security_Validity_Duration { get; set; }
        public string Bid_Security_Expiry_Date { get; set; }
        public bool Insurance_Cover_Required { get; set; }
        public bool Performance_Security_Required { get; set; }
        public int Performance_Security_Percent { get; set; }
        public bool Advance_Payment_Security_Req { get; set; }
        public int Advance_Payment_Security_Percent { get; set; }
        public int Advance_Amount_Limit_Percent { get; set; }
        public string Appointer_of_Bid_Arbitrator { get; set; }
        public string Status { get; set; }
        public bool Published { get; set; }
        public string Bid_Submission_Method { get; set; }
        public string Cancel_Reason_Code { get; set; }
        public string Cancellation_Date { get; set; }
        public string Cancellation_Secret_Code { get; set; }
        public int No_of_Submission { get; set; }
        public string Created_Date_Time { get; set; }
        public string Created_by { get; set; }
        public string Submission_Start_Date { get; set; }
        public string Submission_Start_Time { get; set; }
        public string Submission_End_Date { get; set; }
        public string Submission_End_Time { get; set; }
        public string Bid_Opening_Date { get; set; }
        public string Bid_Opening_Time { get; set; }
        public string Bid_Opening_Venue { get; set; }
        public string Procuring_Entity_Name_Contact { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Post_Code { get; set; }
        public string City { get; set; }
        public string Country_Region_Code { get; set; }
        public string Phone_No { get; set; }
        public string E_Mail { get; set; }
        public string Tender_Box_Location_Code { get; set; }
        public string Primary_Tender_Submission { get; set; }
        public string Primary_Engineer_Contact { get; set; }
        public string Requesting_Region { get; set; }
        public string Requesting_Directorate { get; set; }
        public string Requesting_Department { get; set; }
        public string Procurement_Plan_ID { get; set; }
        public int Procurement_Plan_Entry_No { get; set; }
        public string Job { get; set; }
        public string Job_Task_No { get; set; }
        public string PP_Planning_Category { get; set; }
        public string Document_Status { get; set; }
        public string PP_Funding_Source_ID { get; set; }
        public double PP_Total_Budget { get; set; }
        public int PP_Total_Actual_Costs { get; set; }
        public int PP_Total_Commitments { get; set; }
        public double PP_Total_Available_Budget { get; set; }
        public string PP_Preference_Reservation_Code { get; set; }
        public string Financial_Year_Code { get; set; }
    }

    public class ApprovedTenderInvitation
    {
        public string Code { get; set; }
        public string Procurement_Method { get; set; }
        public string Solicitation_Type { get; set; }
        public string External_Document_No { get; set; }
        public string Procurement_Type { get; set; }
        public string Procurement_Category_ID { get; set; }
        public string Project_ID { get; set; }
        public string Assigned_Procurement_Officer { get; set; }
        public string Road_Code { get; set; }
        public string Description { get; set; }
        public string Currency_Code { get; set; }
    }

    //Request for quotations
    public class RequestForQuotationsList
    {
        public string Code { get; set; }
        public string Document_Date { get; set; }
        public string External_Document_No { get; set; }
        public string PRN_No { get; set; }
        public string Location_Code { get; set; }
        public string Description { get; set; }
        public string Submission_End_Date { get; set; }
        public string Submission_End_Time { get; set; }
        public string Bid_Opening_Date { get; set; }
        public string Bid_Opening_Time { get; set; }
        public string Procurement_Type { get; set; }
        public string Requisition_Product_Group { get; set; }
        public string Procurement_Category_ID { get; set; }
        public string Target_Bidder_Group { get; set; }
        public string Solicitation_Type { get; set; }
        public string Bid_Submission_Method { get; set; }
        public string Procurement_Method { get; set; }
        public bool Sealed_Bids { get; set; }
        public string Tender_Validity_Duration { get; set; }
        public string Tender_Validity_Expiry_Date { get; set; }
        public string Purchaser_Code { get; set; }
        public string Purchaser_Name { get; set; }
        public bool Mandatory_Special_Group_Reserv { get; set; }
        public string Appointer_of_Bid_Arbitrator { get; set; }
        public string Bid_Scoring_Template { get; set; }
        public string Bid_Opening_Committe { get; set; }
        public string Bid_Evaluation_Committe { get; set; }
        public string Document_Status { get; set; }
        public bool Published { get; set; }
        public string Date_Time_Published { get; set; }
        public string Status { get; set; }
        public int No_of_Submission { get; set; }
        public string Created_Date_Time { get; set; }
        public string Created_by { get; set; }
        public string Procurement_Plan_ID { get; set; }
        public int Procurement_Plan_Entry_No { get; set; }
        public string PP_Planning_Category { get; set; }
        public string PP_Funding_Source_ID { get; set; }
        public int PP_Total_Budget { get; set; }
        public int PP_Total_Actual_Costs { get; set; }
        public int PP_Total_Commitments { get; set; }
        public int PP_Total_Available_Budget { get; set; }
        public string PP_Preference_Reservation_Code { get; set; }
        public string Financial_Year_Code { get; set; }
    }

    public class RequestForQuotationCard
    {
        public string Code { get; set; }
        public string Document_Date { get; set; }
        public string External_Document_No { get; set; }
        public string PRN_No { get; set; }
        public string Location_Code { get; set; }
        public string Description { get; set; }
        public string Submission_End_Date { get; set; }
        public string Submission_End_Time { get; set; }
        public string Bid_Opening_Date { get; set; }
        public string Bid_Opening_Time { get; set; }
        public string Procurement_Type { get; set; }
        public string Requisition_Product_Group { get; set; }
        public string Procurement_Category_ID { get; set; }
        public string Target_Bidder_Group { get; set; }
        public string Solicitation_Type { get; set; }
        public string Bid_Submission_Method { get; set; }
        public string Procurement_Method { get; set; }
        public bool Sealed_Bids { get; set; }
        public string Tender_Validity_Duration { get; set; }
        public string Tender_Validity_Expiry_Date { get; set; }
        public string Purchaser_Code { get; set; }
        public string Purchaser_Name { get; set; }
        public bool Mandatory_Special_Group_Reserv { get; set; }
        public string Appointer_of_Bid_Arbitrator { get; set; }
        public string Bid_Scoring_Template { get; set; }
        public string Bid_Opening_Committe { get; set; }
        public List<SelectListItem> ListOfEvaluatin { get; set; }
        public List<SelectListItem> ListOfOpening { get; set; }

        public List<SelectListItem> ListOfScoringTemplate { get; set; }
        public string Bid_Evaluation_Committe { get; set; }

        public string Document_Status { get; set; }
        public bool Published { get; set; }
        public string Date_Time_Published { get; set; }
        public string Status { get; set; }
        public int No_of_Submission { get; set; }
        public string Created_Date_Time { get; set; }
        public string Created_by { get; set; }
        public string Procurement_Plan_ID { get; set; }
        public int Procurement_Plan_Entry_No { get; set; }
        public string PP_Planning_Category { get; set; }
        public string PP_Funding_Source_ID { get; set; }
        public int PP_Total_Budget { get; set; }
        public int PP_Total_Actual_Costs { get; set; }
        public int PP_Total_Commitments { get; set; }
        public int PP_Total_Available_Budget { get; set; }
        public string PP_Preference_Reservation_Code { get; set; }
        public string Financial_Year_Code { get; set; }
    }

    public class RequestForQuotationsLines
    {
        public string Standard_Purchase_Code { get; set; }
        public int Line_No { get; set; }
        public string Type { get; set; }
        public string FilteredTypeField { get; set; }
        public string No { get; set; }
        public string Variant_Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public int Amount_Excl_VAT { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string ShortcutDimCode_x005B_3_x005D_ { get; set; }
        public string ShortcutDimCode_x005B_4_x005D_ { get; set; }
        public string ShortcutDimCode_x005B_5_x005D_ { get; set; }
        public string ShortcutDimCode_x005B_6_x005D_ { get; set; }
        public string ShortcutDimCode_x005B_7_x005D_ { get; set; }
        public string Item_Category { get; set; }
        public string ShortcutDimCode_x005B_8_x005D_ { get; set; }

        public List<SelectListItem> ListOfItems { get; set; }
    }

    public class RequestForQuotationsLinesList
    {
        public string Status { get; set; }
        public List<RequestForQuotationsLines> ListOfRequestForQuotationsLines { get; set; }
    }

    public class RecurringPurchaseLine
    {
        public string Vendor_No { get; set; }
        public string Code { get; set; }
        public string Vendor_Name { get; set; }
        public string Vendor_Phone_No { get; set; }
        public string Primary_Email { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }

    public class RecurringPurchaseLinesList
    {
        public string Status { get; set; }

        public List<RecurringPurchaseLine> ListOfBiders { get; set; }
    }

    public class ApprovedOpenTenderInvitationList
    {
        public string Code { get; set; }
        public string Procurement_Method { get; set; }
        public string Solicitation_Type { get; set; }
        public string External_Document_No { get; set; }
        public string Procurement_Type { get; set; }
        public string Procurement_Category_ID { get; set; }
        public string Project_ID { get; set; }
        public string Assigned_Procurement_Officer { get; set; }
        public string Road_Code { get; set; }
        public string Description { get; set; }
        public string Currency_Code { get; set; }
    }


    //Tender Committee
    public class IFSTenderCommitteeList
    {
        public string Document_No { get; set; }
        public string Committee_Type { get; set; }
        public string Description { get; set; }
        public string Appointment_Effective_Date { get; set; }
        public string Appointing_Authority { get; set; }
        public string Tender_Name { get; set; }
        public string Approval_Status { get; set; }
        public string Primary_Region { get; set; }
        public string Primary_Directorate { get; set; }
        public string Primary_Department { get; set; }
        public bool Blocked { get; set; }
        public string Created_By { get; set; }
        public string Created_Date { get; set; }
        public string Created_Time { get; set; }
        public string IFS_Code { get; set; }
        public string Document_Date { get; set; }
    }

    public class IFSTenderCommitteeCard
    {
        public string Document_No { get; set; }
        public string Committee_Type { get; set; }
        public string IFS_Code { get; set; }
        public string Description { get; set; }
        public string Document_Date { get; set; }
        public string Appointment_Effective_Date { get; set; }
        public string Appointing_Authority { get; set; }
        public string Tender_Name { get; set; }
        public int Duration { get; set; }
        public string Raised_By { get; set; }
        public string Name { get; set; }
        public string Staff_Id { get; set; }
        public string Designation { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Location { get; set; }

        public string Global_Dimension_2_Code { get; set; }
        public string AdminUnit { get; set; }
        public int Min_Required_No_of_Members { get; set; }
        public int Actual_No_of_Committee_Memb { get; set; }
        public string Approval_Status { get; set; }
        public string Created_By { get; set; }
        public string Created_Date { get; set; }
        public string Created_Time { get; set; }
        public List<SelectListItem> ListOfCommitteeTypes { get; set; }
        public List<SelectListItem> ListOfIFSCodes { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
    }

    public class IFSTenderCommitteeLines
    {
        public string Document_No { get; set; }
        public string Committee_Type { get; set; }
        public int Line_No { get; set; }
        public string Role { get; set; }
        public int MRole { get; set; }
        public string Member_No { get; set; }
        public string Member_Name { get; set; }
        public string Designation { get; set; }
        public string Member_Email { get; set; }
        public List<SelectListItem> ListOfCommitteeTypes { get; set; }
        public List<SelectListItem> ListOfmemberNo { get; set; }
    }

    public class IFSTenderCommitteeLineList
    {
        public List<IFSTenderCommitteeLines> ListOfTenderCommitteeLine { get; set; }
        public string Status { get; set; }
    }


    //Framework Contract
    public class FrameworkContractList
    {
        public string Code { get; set; }
        public string Tender_Name { get; set; }
        public string Status { get; set; }
        public string PRN_No { get; set; }
        public string Awarded_Bidder_No { get; set; }
        public string Awarded_Bidder_Name { get; set; }
        public bool RFQ_Sent { get; set; }
        public string Procurement_Method { get; set; }
        public string Awarded_Quote_No { get; set; }
        public int Awarded_Bidder_Sum { get; set; }
        public string External_Document_No { get; set; }
        public string Tender_Summary { get; set; }
        public string Procurement_Category_ID { get; set; }
    }

    public class FrameworkContractCard
    {
        public string Code { get; set; }
        public string External_Document_No { get; set; }
        public string PRN_No { get; set; }
        public string Responsibility_Center { get; set; }
        public string Location_Code { get; set; }
        public string Tender_Name { get; set; }
        public string Tender_Summary { get; set; }
        public string Procurement_Type { get; set; }
        public string Status { get; set; }
        public string Requisition_Product_Group { get; set; }
        public string Procurement_Category_ID { get; set; }
        public string Procurement_Category { get; set; }
        public string Solicitation_Type { get; set; }
        public string Procurement_Method { get; set; }
        public string Purchaser_Code { get; set; }
        public string Language_Code { get; set; }
        public string Created_Date_Time { get; set; }
        public string Created_by { get; set; }
        public string Framework_Agreement_No { get; set; }
        public string Awarded_Quote_No { get; set; }
        public string Awarded_Bidder_No { get; set; }
        public string Awarded_Bidder_Name { get; set; }
        public int Awarded_Bidder_Sum { get; set; }
        public string Requesting_Region { get; set; }
        public string Requesting_Directorate { get; set; }
        public string Requesting_Department { get; set; }
        public string Procurement_Plan_ID { get; set; }
        public int Procurement_Plan_Entry_No { get; set; }
        public string Job { get; set; }
        public string Job_Task_No { get; set; }
        public string PP_Planning_Category { get; set; }
        public string PP_Funding_Source_ID { get; set; }
        public int PP_Total_Budget { get; set; }
        public int PP_Total_Actual_Costs { get; set; }
        public int PP_Total_Commitments { get; set; }
        public int PP_Total_Available_Budget { get; set; }
        public string PP_Preference_Reservation_Code { get; set; }
        public string Financial_Year_Code { get; set; }
        public string Project_ID { get; set; }
        public List<SelectListItem> ListOfResponsibilityCentres { get; set; }
        public List<SelectListItem> ListOfLocationCodes { get; set; }
        public List<SelectListItem> ListOfProcurementTypes { get; set; }
        public List<SelectListItem> ListOfReqProductGroup { get; set; }
        public List<SelectListItem> ListOfProcurementCategories { get; set; }
        public List<SelectListItem> ListOfPurchaserCodes { get; set; }
        public List<SelectListItem> ListOfLanguageCodes { get; set; }

    }
 
    
    //(For Lines, I am using RequestForQuotationsLines above)

    public class FrameworkContractLinesList
    {
        public string Status { get; set; }
        public List<RequestForQuotationsLines> ListOfFrameworkContractLines { get; set; }
    }

   

    //Direct Procurements
    public class DirectProcurementsList
    {
        public string Code { get; set; }
        public string Tender_Name { get; set; }
        public string Status { get; set; }
        public string PRN_No { get; set; }
        public string Awarded_Bidder_No { get; set; }
        public string Awarded_Bidder_Name { get; set; }
        public bool RFQ_Sent { get; set; }
        public string Procurement_Method { get; set; }
        public string Awarded_Quote_No { get; set; }
        public int Awarded_Bidder_Sum { get; set; }
        public string External_Document_No { get; set; }
        public string Tender_Summary { get; set; }
        public string Procurement_Category_ID { get; set; }
    }

    public class DirectProcurementsCard
    {
        public string Code { get; set; }
        public string Reference { get; set; }
        public string PRNNo { get; set; }
        public string ResponsibilityCenter { get; set; }
        public string LocationCode { get; set; }
        public string TenderName { get; set; }
        public string Description { get; set; }
        public string ProcurementType { get; set; }
        public string RequisitionProductGroup { get; set; }
        public string ProcurementCategoryID { get; set; }
        public string ProcurementCategory { get; set; }
        public string SolicitationType { get; set; }
        public string ProcurementMethod { get; set; }
        public bool HasanEmailAttachment { get; set; }
        public bool RFQSent { get; set; }
        public string PurchaserCode { get; set; }
        public string EvaluationCriteria { get; set; }
        public string LanguageCode { get; set; }
        public string PreliminaryEvaluation { get; set; }
        public string Status { get; set; }
        public string CreatedDateTime { get; set; }
        public string Createdby { get; set; }
        public string AwardedQuoteNo { get; set; }
        public string AwardedBidderNo { get; set; }
        public string AwardedBidderName { get; set; }
        public int AwardedBidderSum { get; set; }
        public string ProcurementPlanID { get; set; }
        public int ProcurementPlanEntryNo { get; set; }
        public string PPPlanningCategory { get; set; }
        public string PPFundingSourceID { get; set; }
        public int PPTotalBudget { get; set; }
        public int PPTotalActualCosts { get; set; }
        public int PPTotalCommitments { get; set; }
        public int PPTotalAvailableBudget { get; set; }
        public string PPPreferenceReservationCode { get; set; }
        public string FinancialYearCode { get; set; }
    }

    //published IFS
    public class publishedIFSList
    {
        public string Code { get; set; }
        public string Procurement_Method { get; set; }
        public string Solicitation_Type { get; set; }
        public string External_Document_No { get; set; }
        public string Procurement_Type { get; set; }
        public string Procurement_Category_ID { get; set; }
        public string Project_ID { get; set; }
        public string Assigned_Procurement_Officer { get; set; }
        public string Road_Code { get; set; }
        public string Description { get; set; }
        public string Currency_Code { get; set; }
    }


    //Final Evaluation Report
    public class FinalEvaluationReportCard
    {
        public string Code { get; set; }
        public string Document_Date { get; set; }
        public string Financial_Evaluation_ID { get; set; }
        public string IFS_Code { get; set; }
        public string Tender_Name { get; set; }
        public string Description { get; set; }
        public string Raised_By { get; set; }
        public string Name { get; set; }
        public string Procurement_Method { get; set; }
        public bool Special_RFQ { get; set; }
        public string _x003C_General_Procurement_Remarks_x003E_ { get; set; }
        public string Procurement_Officer_License_No { get; set; }
        public string Tender_Committee_Role { get; set; }
        public string Designation_Title { get; set; }
        public string Primary_Region { get; set; }
        public string Bid_Opening_Date { get; set; }
        public string Evaluation_Completion_Date { get; set; }
        public string Bid_Evaluation_Template { get; set; }
        public string Award_Type { get; set; }
        public string Appointed_Bid_Opening_Comm { get; set; }
        public string Appointed_Bid_Evaluation_Com { get; set; }
        public string General_Procurement_Remarks { get; set; }
        public int No_of_submitted_bids { get; set; }
        public string Approval_Status { get; set; }
        public string Document_Status { get; set; }
    }

    public class FinalEvaluationReportLines
    {
        public string Tabulation_ID { get; set; }
        public string Tabulation_Type { get; set; }
        public string Document_Type { get; set; }
        public string No { get; set; }
        public string Bid_Submission_No { get; set; }
        public string Bidder_No { get; set; }
        public string Bidder_Name { get; set; }
        public bool Passed_Tech_Evaluation { get; set; }
        public string Evaluation_Committee_Recomm { get; set; }
        public string Evaluation_Committee_Remarks { get; set; }
        public string AGPO_Certficate_Number { get; set; }
        public string AGPO_Category { get; set; }
        public int Read_out_Bid_Price_A { get; set; }
        public int Final_Evaluated_Bid_Price { get; set; }
        public string Financial_Evaluation_Date { get; set; }
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
        public string Tech_Evaluation_Ranking { get; set; }
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


    //Purchase Order
    public class PurchaseOrder
    {
        public object Remit_to_Code;
        public object Requisition_No;
        public string PRN_No;
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
        public string Transaction_Specification { get; set; }
        public string Transaction_Type { get; set; }
        public string Transport_Method { get; set; }
        public string Entry_Point { get; set; }
        public string Area { get; set; }
        public int Prepayment_Percent { get; set; }
        public bool Compress_Prepayment { get; set; }
        public string Prepmt_Payment_Terms_Code { get; set; }
        public string Prepayment_Due_Date { get; set; }
        public int Prepmt_Payment_Discount_Percent { get; set; }
        public string Prepmt_Pmt_Discount_Date { get; set; }
        public string Vendor_Cr_Memo_No { get; set; }
        public string Amount { get; set; }
        public string Amount_Including_VAT { get; set; }
    }

    public class UpdatePurchaseOrderLineViewModel
    {
        public int Unit_Cost_LCY;
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public string Description { get; set; }
        public int Direct_Unit_Cost { get; set; }
        public int Line_Amount { get; set; }
        public int Quantity { get; set; }
        public string Gen_Bus_Posting_Group { get; set; }
        public string Gen_Prod_Posting_Group { get; set;  }
    }


    public class PurchaseOrderLines
    {
        public string Document_Type { get; set; }
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public string Type { get; set; }
        public string FilteredTypeField { get; set; }
        public string No { get; set; }
        public string Item_Reference_No { get; set; }
        public string IC_Partner_Code { get; set; }
        public string IC_Partner_Ref_Type { get; set; }
        public string IC_Partner_Reference { get; set; }
        public string Variant_Code { get; set; }
        public bool Nonstock { get; set; }
        public int VAT_Percent { get; set; }
        public string VAT_Prod_Posting_Group { get; set; }
        public string Description { get; set; }
        public string Description_2 { get; set; }
        public bool Drop_Shipment { get; set; }
        public string Return_Reason_Code { get; set; }
        public string Location_Code { get; set; }
        public string Bin_Code { get; set; }
        public int Quantity { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public int Reserved_Quantity { get; set; }
        public int Job_Remaining_Qty { get; set; }
        public string Unit_of_Measure { get; set; }
        public int Approved_Requisition_Amount { get; set; }
        public int Approved_Unit_Cost { get; set; }
        public int Direct_Unit_Cost { get; set; }
        public int Indirect_Cost_Percent { get; set; }
        public int Unit_Cost_LCY { get; set; }
        public int Unit_Price_LCY { get; set; }
        public int Line_Amount { get; set; }
        public bool Tax_Liable { get; set; }
        public string Tax_Area_Code { get; set; }
        public string Tax_Group_Code { get; set; }
        public bool Use_Tax { get; set; }
        public int Line_Discount_Percent { get; set; }
        public int Line_Discount_Amount { get; set; }
        public string Procurement_Plan { get; set; }
        public string Procurement_Plan_Item { get; set; }
        public int Prepayment_Percent { get; set; }
        public int Prepmt_Line_Amount { get; set; }
        public int Prepmt_Amt_Inv { get; set; }
        public bool Allow_Invoice_Disc { get; set; }
        public int Inv_Discount_Amount { get; set; }
        public int Inv_Disc_Amount_to_Invoice { get; set; }
        public int Qty_to_Receive { get; set; }
        public int Quantity_Received { get; set; }
        public int Qty_to_Invoice { get; set; }
        public int Quantity_Invoiced { get; set; }
        public int Qty_Rcd_Not_Invoiced { get; set; }
        public int Prepmt_Amt_to_Deduct { get; set; }
        public int Prepmt_Amt_Deducted { get; set; }
        public bool Allow_Item_Charge_Assignment { get; set; }
        public int Qty_to_Assign { get; set; }
        public int Item_Charge_Qty_to_Handle { get; set; }
        public int Qty_Assigned { get; set; }
        public string Job_No { get; set; }
        public string Job_Task_No { get; set; }
        public int Job_Planning_Line_No { get; set; }
        public string Job_Line_Type { get; set; }
        public int Job_Unit_Price { get; set; }
        public int Job_Line_Amount { get; set; }
        public int Job_Line_Discount_Amount { get; set; }
        public int Job_Line_Discount_Percent { get; set; }
        public int Job_Total_Price { get; set; }
        public int Job_Unit_Price_LCY { get; set; }
        public int Job_Total_Price_LCY { get; set; }
        public int Job_Line_Amount_LCY { get; set; }
        public int Job_Line_Disc_Amount_LCY { get; set; }
        public string Requested_Receipt_Date { get; set; }
        public string Promised_Receipt_Date { get; set; }
        public string Planned_Receipt_Date { get; set; }
        public string Expected_Receipt_Date { get; set; }
        public string Order_Date { get; set; }
        public string Lead_Time_Calculation { get; set; }
        public string Planning_Flexibility { get; set; }
        public string Prod_Order_No { get; set; }
        public int Prod_Order_Line_No { get; set; }
        public string Operation_No { get; set; }
        public string Work_Center_No { get; set; }
        public bool Finished { get; set; }
        public int Whse_Outstanding_Qty_Base { get; set; }
        public string Inbound_Whse_Handling_Time { get; set; }
        public string Blanket_Order_No { get; set; }
        public int Blanket_Order_Line_No { get; set; }
        public int Appl_to_Item_Entry { get; set; }
        public string Deferral_Code { get; set; }
        public string Depreciation_Book_Code { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string ShortcutDimCode3 { get; set; }
        public string ShortcutDimCode4 { get; set; }
        public string ShortcutDimCode5 { get; set; }
        public string ShortcutDimCode6 { get; set; }
        public string ShortcutDimCode7 { get; set; }
        public string ShortcutDimCode8 { get; set; }
        public string Gen_Bus_Posting_Group { get; set; }
        public string Gen_Prod_Posting_Group { get; set; }
        public int Over_Receipt_Quantity { get; set; }
        public string Over_Receipt_Code { get; set; }
        public int Gross_Weight { get; set; }
        public int Net_Weight { get; set; }
        public int Unit_Volume { get; set; }
        public int Units_per_Parcel { get; set; }
        public string FA_Posting_Date { get; set; }
        public int AmountBeforeDiscount { get; set; }
        public int Invoice_Discount_Amount { get; set; }
        public int Invoice_Disc_Pct { get; set; }
        public int Total_Amount_Excl_VAT { get; set; }
        public int Total_VAT_Amount { get; set; }
        public int Total_Amount_Incl_VAT { get; set; }
    }

    public class PurchaseOrderLineList
    {
        public string Status { get; set; }
        public List<PurchaseOrderLines> ListOfPurchaseOrderLine { get; set; }
    }
}