namespace Latest_Staff_Portal.ViewModel
{
    public class BidOpeningLines
    {
        public string Code { get; set; }
        public string VendorNo { get; set; }
        public string IFSCode { get; set; }
        public string BidderName { get; set; }
        public string BidNumber { get; set; }
        public string BidNo { get; set; }
        public string ExternalBidReferenceNo { get; set; }
        public decimal TotalTenderPriceLCY { get; set; }
        public bool CompleteBidDocSubmitted { get; set; }
        public string BidWithdrawalNoticeNo { get; set; }
        public bool BidWithdrawalExists { get; set; }
        public string FinalBidOpeningResult { get; set; }
        public string TenderOpeningComRemarks { get; set; }
        public int NoOfRepresentatives { get; set; }
    }
}