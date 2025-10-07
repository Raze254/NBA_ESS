function navigateTo(url) {
    ShowProgress();
    window.location = url;
}
var DashBoardLink = function () { navigateTo("/Dashboard/Dashboard"); }
var DashboardLink = function () { navigateTo("/Dashboard/Dashboard"); }
var PersonalProfile = function () { navigateTo("/Dashboard/PersonalProfile"); }
var ProgrammeListLink = function () { navigateTo("/Programme/ProgrammeList"); }
var LecturerListLink = function () { navigateTo("/Lecturer/LecturerList"); }
var DepositReceiptsLink = function () { navigateTo("/Deposits/DepositReceipt"); }
var UtilizationReceiptLink = function () { navigateTo("/Deposits/UtilizationReceipt"); }
var RevenueReceiptLink = function () { navigateTo("/Deposits/RevenueReceipt"); }
var ForfeitureLink = function () { navigateTo("/Deposits/ForfeitureRefund"); }
var RefundLink = function () { navigateTo("/Deposits/FullRefund"); }
var UtilizationLink = function () { navigateTo("/Deposits/FullUtilization"); }
var PurchaseInvoice = function () { navigateTo("/PurchaseInvoice/PurchaseInvoice?status=Released"); }
var PostedPurchaseInvoices = function () { navigateTo("/PurchaseInvoice/PurchaseInvoice?status=Posted"); }
var PartTimeRequisitionLink = function () { navigateTo("/Lecturer/PartTimeRequistionList"); }
var CourseAllocationLink = function () { navigateTo("/Lecturer/CourseAllocationUnits"); }
var VoteBookBalanceLink = function () { navigateTo("/ViewDocuments/ViewDocuments?DocT=VOTEBOOK"); }
var LeaveRequisitionLink = function () { navigateTo("/Leave/LeaveRequisitionList"); }
var PurchaseRequisitionLink = function () { navigateTo("/Purchase/PurchaseRequisitionList"); }
var ApprovedPurchaseRequisitionLink = function () { navigateTo("/Purchase/ApprovedPurchaseRequisition"); }
var RequestForQuotationsLink = function () { navigateTo("/Purchase/RequestForQuotations"); }
var FrameworkContractLink = function () { navigateTo("/Purchase/FrameworkContract"); }
var Sharepoint = function () { navigateTo("/ViewDocuments/SharepointIntegration"); }
var DirectProcurementsLink = function () { navigateTo("/Purchase/DirectProcurements"); }
var IFSTenderCommitteeLink = function () { navigateTo("/Purchase/IFSTenderCommittee"); }
var AssignedPurchaseRequisitionLink = function () { navigateTo("/Purchase/AssignedPurchaseRequisition"); }
var PurchaseOrderLink = function () { navigateTo("/Purchase/PurchaseOrdersList"); }
var StoreRequisitionLink = function () { navigateTo("/Store/StoreRequisitionList"); }
var BidOpeningRegisterLink = function () { navigateTo("/BidResponseEvaluation/BidOpeningRegisterList"); }
var ImprestMemoLink = function () { navigateTo("/Imprest/ImprestMemo"); }
var ImprestSurrenderLink = function () { navigateTo("/ImprestSurrender/ImprestSurrender"); }
var StandingImprestLink = function () { navigateTo("/Imprest/StandingImprest"); }
var ImprestWarrantiesLink = function () { navigateTo("/Imprest/ImprestWarrant"); }
var SpecialImprestLink = function () { navigateTo("/Imprest/SpecialImprest"); }
var WorkShopAdvanceLink = function () { navigateTo("/WorkShopAdvance/WorkShopAdvanceRequisitionList"); }
var WorkShopAdvanceSurrenderLink = function () { navigateTo("/WorkshopLiquidation/WorkShopAdvanceLiquidationList"); }
var PettyCashLink = function () { navigateTo("/PettyCash/PettyCashRequisitionList"); }
var StaffClaimsLink = function () { navigateTo("/StaffClaim/StaffClaims"); }
var OpenStaffClaimsLink = function () { navigateTo("/StaffClaim/StaffClaims?status=Open"); }
var PendingStaffClaimsLink = function () { navigateTo("/StaffClaim/StaffClaims?status=Pending Approval"); }
var ReleasedStaffClaimsLink = function () { navigateTo("/StaffClaim/StaffClaims?status=Released"); }
var AlltimeDepositLink = function () { navigateTo("/PowerBi/AllTimeDeposits"); }
var DailyMonthlyLink = function () { navigateTo("/PowerBi/DailyMonthlyTrends"); }
var TodaysDepostLink = function () { navigateTo("/PowerBi/TodaysDeposit"); }
var AllReportsLink = function () { navigateTo("/PowerBi/AllReports"); }
var PowerBiLink = function () { navigateTo("/PowerBi/Deposits"); }
var ANUPEAisalRequisitionLink = function () { navigateTo("/ANUPEAisal/MyANUPEAisalList"); }
var SupervisorRequisitionReviewLink = function () { navigateTo("/ANUPEAisal/MyReviewANUPEAisalList"); }
var TransportRequisitionLink = function () { navigateTo("/Transport/TransportRequisitionList"); }
var TrainingRequisitionLink = function () { navigateTo("/Training/TrainingRequisitionList"); }
var TrainingExtensionLink = function () { navigateTo("/Training/TrainingExtensionRequisitionList"); }
var TrainingNeedsLink = function () { navigateTo("/Training/TrainingNeedsList"); }
var TrainingFeedbackLink = function () { navigateTo("/Training/TrainingFeedbackRequisitionList"); }
var TrainingEvaluationLink = function () { navigateTo("/Training/TrainingEvaluationRequisitionList"); }
var FuelRequisitionLink = function () { navigateTo("/Fuel/FuelCardList"); }
var FuelRequestLink = function () { navigateTo("/Fuel/FuelRequisitionList"); }
var VehicleServicingMaintenanceLink = function () { navigateTo("/Maintenance/MaintenanceRequisitionList"); }
var ICTRequisitionLink = function () { navigateTo("/ICT/ICTRequisitionList"); }
var ICTAssetRequisitionLink = function () { navigateTo("/ICT/ICTAssetTransferList"); }
var ICTAssetServicingMaintenanceLink = function () { navigateTo("/ICT/ICTServMntList"); }
var ICTAssignmentLink = function () { navigateTo("/ICT/ICTAssignmentList"); }
var NewVisitorLink = function () { navigateTo("/Visitors/NewVisitorsList"); }
var ActiveVisitorsLink = function () { navigateTo("/Visitors/ActiveVisitorsList"); }
var ClearedVisitorsLink = function () { navigateTo("/Visitors/ClearedVisitorsList"); }
var GatePassLink = function () { navigateTo("/GatePass/GatePassList"); }
var ApprovedGatePassLink = function () { navigateTo("/GatePass/ApprovedGatePassList"); }
var AssignedAssetLink = function () { navigateTo("/Asset/AssignedAssetList"); }
var AssetTransferLink = function () { navigateTo("/Asset/AssetTransferList"); }
var PayslipViewLink = function () { navigateTo("/ViewDocument/DocumentViewPayslip"); }
var P9ViewLink = function () { navigateTo("/ViewDocument/DocumentViewp9"); }
var LeaveStatementLink = function () { navigateTo("/ViewDocument/GetLeaveStatementReport"); }
var DocumentApprovalSummaryLink = function () { navigateTo("/DocumentApproval/DocumentForApprovalSummery?rn=Open"); }
var ApprovedDocLink = function () { navigateTo("/DocumentApproval/DocumentForApprovalSummery?rn=Approved"); }
var RejectedDocLink = function () { navigateTo("/DocumentApproval/DocumentForApprovalSummery?rn=Rejected"); }
var ChangePasswordLink = function () { navigateTo("/Settings/ChangePassword"); }
var PCALink = function () { navigateTo("/PCA/PCAs"); }
var TimeSheetLink = function () { navigateTo("/TimeSheet/TimeSheetList"); }
var EmployeeRequisitionLink = function () { navigateTo("/EmployeeRequistion/EmployeeRequisitionList"); }
var StaffInductionLink = function () { navigateTo("/StaffInduction/StaffInduction"); }
var ExitInterviewLink = function () { navigateTo("/StaffClearance/ExitInterview"); }
var StaffClearanceLink = function () { navigateTo("/StaffClearance/StaffClearance"); }
var LeaveReimbursement = function () { navigateTo("/LeaveReimbursement/LeaveReimbursementList"); }
var TimeOffLieu = function () { navigateTo("/TimeOffLieu/TimeOffLieuList"); }
var EmployeesOnLeaveLink = function () { navigateTo("/Leave/EmployeesOnLeaveList"); }
var EmployeesApprovedLeaveLink = function () { navigateTo("/Leave/EmployeesApprovedLeaveList"); }
var CarryForward = function () { navigateTo("/TimeOffLieu/CarryForwardList"); }
var LeaveAllocationLink = function () { navigateTo("/TimeOffLieu/LeaveAllocationList"); }
var SelfEmpTransferLink = function () { navigateTo("/Mobility/EmployeeTransferList"); }
var HRDisciplinaryCaseLink = function () { navigateTo("/Disciplinary/HRDicsiplinaryCasesList"); }
var ManagementTransferLink = function () { navigateTo("/Mobility/ManagementTransferList"); }
var PropertyListLink = function () { navigateTo("/FacilitiesAdministration/PropertyList"); }
var WorkTicketLink = function () { navigateTo("/Transport/WorkTicket"); }
var ReservationListLink = function () { navigateTo("/FacilitiesAdministration/ReservationRequestList"); }
var ApprovedListLink = function () { navigateTo("/FacilitiesAdministration/AprrovedReservationList"); }
var PerformanceANUPEAisalSystem = function () { navigateTo("/Pas/IndividualScorecardList"); }
var PMMULink = function () {
    navigateTo("/Performance/PMMU");
}
var AppointmentCommitteeLink = function () {
    navigateTo("/Performance/CommitteeAppointmentVouchers");
}
var LeavePlanner = function () { navigateTo("/Leave/LeavePlanner"); }
var CarLoan = function () { navigateTo("/WelfareManagement/CarLoanApplicationsList"); }
var MortgageLoan = function () { navigateTo("/WelfareManagement/MortgageLoanApplicationsList"); }
var AdvancedSalary = function () { navigateTo("/WelfareManagement/SalaryAdvanceLoanApplicationsList"); }
var MedicalCardReplacement = function () { navigateTo("/WelfareManagement/MedicalCardReplacement"); }
var VehicleIncidence = function () { navigateTo("/Transport/VehicleIncidentView"); }
var DependantsChangeRequest = function () { navigateTo("/WelfareManagement/DependantChangeRequests"); }
var HumanResourceLink = function () { navigateTo("/Dashboard/HumanResource"); }
var ProjectsLink = function () { navigateTo("/Dashboard/Projects"); }
var DSPOPLink = function () { navigateTo("/Dashboard/DSPOP"); }
var DASSLink = function () { navigateTo("/Dashboard/DASS"); }
var FAQsLink = function () { navigateTo("/Common/FAQsView"); }
var HRInformation = function () { navigateTo("/Common/HRInformation"); }
var AttendanceList = function () { navigateTo("/Dashboard/Attendance"); }
var FinanceLink = function () { navigateTo("/Dashboard/Finance"); }
var AccountsLink = function () { navigateTo("/Dashboard/Accounts"); }
var ProcurementLink = function () { navigateTo("/BidEvaluation/EvaluationProcess"); }

ProcurementDashboard = function () { navigateTo("/Dashboard/Procurement"); }
var ResourceRequirementsLink = function () { navigateTo("/Workplans/ResourceRequirements"); }
var ExpenseRequisitionLink = function () { navigateTo("/ExpenseRequisition/ExpenseRequisition"); }
var FunctionalProcurementPlanLink = function () { navigateTo("/Purchase/FunctionalProcurementPlan"); }

var RevisionProcurementPlanLink = function () { navigateTo("/Purchase/RevisionVouchers"); }
var DraftWorkPlansLink = function () { navigateTo("/Workplans/DraftWorkPlans"); }
var SuppWorkPlansLink = function () { navigateTo("/Workplans/SupplementaryWorkPlans"); }
var ReallocationLink = function () { navigateTo("/Workplans/BudgetReallocation") }
var UserRequestLink = function () { navigateTo("/ProjectManagement/UserRequests"); }
var ProjectProposalLink = function () { navigateTo("/Projects/ProjectProposal"); }

var DesignControl = function () { navigateTo("/Projects/DesignControl"); }
var BankAccountsLink = function () { navigateTo("/Deposits/BankLedgerEntries"); }
var ContractRequisitionLink = function () { navigateTo("/Contract/ContractList"); }
var PvLink = function () { navigateTo("/PaymentVoucher/PaymentVoucherView"); }
var EftLink = function () { navigateTo("/Eft/EftView"); }
var DraftTenderInvitationLink = function () { navigateTo("/Purchase/InvitationToTender"); }
var ProfessionalOpinionLink = function () { navigateTo("/BidResponseEvaluation/ProfessionalOpinion"); }
var EvaluationTemplateLink = function () { navigateTo("/BidResponseEvaluation/EvaluationTemplate"); }
var PremBidEvaluation = function () { navigateTo("/BidResponseEvaluation/PreliminaryEvaluationView"); }
var TechnicalBidEvaluation = function () { navigateTo("/BidResponseEvaluation/TechnicalEvaluationView"); }
var FinanceBidEvaluation = function () { navigateTo("/BidResponseEvaluation/FinancialEvaluationView"); }
var FinalEvaluation = function () { navigateTo("/BidResponseEvaluation/FinalEvaluationView"); }
var ScoreCardListLink = function () { navigateTo("/Pas/IndividualScoreCardList"); }
var SelfSponsoredTrainingLink = function () { navigateTo("/Training/SelfSponsored"); }
var SupervisorRequisitiontReviewLink = function () { navigateTo("/Pas/SupervisorsApprisals") }
var PASReportLink = function () { navigateTo("/Pas/EmployeePASReport") }


