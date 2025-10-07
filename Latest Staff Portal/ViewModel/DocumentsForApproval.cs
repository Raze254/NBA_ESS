namespace Latest_Staff_Portal.ViewModel
{
    public class DocumentsForApproval
    {
        public string Entry_No { get; set; }
        public string TabelID { get; set; }
        public string Document_No { get; set; }
        public string Document_Type { get; set; }
        public string Sender_Name { get; set; }
        public string DateSend { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        public string Sequence { get; set; }
        public string Sender { get; set; }
        public string Approver { get; set; }
        public bool CommentFound { get; set; }
        public string Comment { get; set; }
    }

    public class DocumentsForApprovalList
    {
        public string TableID { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string DocType { get; set; }
    }

    public class DocumentCount
    {
        public string Status { get; set; }
        public int LeaveCount { get; set; }
        public int PRNCount { get; set; }
        public int LPOCount { get; set; }
        public int StoreReq { get; set; }
        public int SRNCount { get; set; }
        public int ImpWarrantCount { get; set; }
        public int WorkTicketCount { get; set; }
        public int ImpMemoCount { get; set; }
        public int SurrCount { get; set; }
        public int ClaimCount { get; set; }

        public int PettyCashCount { get; set; }
        public int PettyCashSurrCount { get; set; }
        public int Transport { get; set; }
        public int TransferOrder { get; set; }
        public int Training { get; set; }
        public int PVCount { get; set; }
        public int TNA { get; set; }
        public int impMemoCount { get; set; }
        public int FuelCount { get; set; }
        public int MaintenanceCount { get; set; }
        public int LVReimbursementCount { get; set; }
        public int LVTimeoffCount { get; set; }
        public int LVCarryForwardCount { get; set; }
        public int TimeSheet { get; set; }
        public int Workshop { get; set; }
        public int RFQCount { get; set; }
        public int MobilityCount { get; set; }
        public int PCACount { get; set; }
        public int WelfareCount { get; set; }
        public int TrainingExtention { get; set; }
        public int TrainingCount { get; }
        public int VehicleCount { get; set; }
        public int Leaveplanner { get; set; }
        public int FunctionalProcurementPlan { get; set; }
        public int FunctionalProcurementPlanAmmendment { get; set; }
        public int ExpenseRequisition { get; set; }
        public int ResourceRequirements { get; set; }
        public int Workplans { get; set; }
        public int Payments { get; set; }
        public int Efts { get; set; }
        public int Resource { get; set; }
        public int LeaveRecall { get; set; }
        public int IFSTenderCom { get; set; }
        public int StandingImprest { get; set; }
        public int SafariImprest { get; set; }
        public int SpecialImprest { get; set; }
        public int StaffClaim { get; set; }
        public int Receipts { get; set; }
        public int FDosposalPlan { get; set; }
        public int LabSampleCount { get; set; }
        
    }

    public class DocumentRejectionComment
    {
        public bool CommentFound { get; set; }
        public string Comment { get; set; }
    }
}