namespace Latest_Staff_Portal.ViewModel
{
    public class Proposal
    {
        public string Line_No { get; set; }
        public string Proposal_No { get; set; }
        public string Description { get; set; }
        public string Principal_Investigator { get; set; }
        public string Objective { get; set; }
    }

    public class ProposalReview
    {
        public string Review_Remarks { get; set; }
        public string Recommendation { get; set; }
        public string Line_No { get; set; }
        public string Proposal_No { get; set; }
    }
}