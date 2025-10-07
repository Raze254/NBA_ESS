var links = [
    { text: 'Dashboard', url: '/Dashboard/Dashboard' },
    { text: 'Personal Profile', url: '/Dashboard/PersonalProfile' },
    { text: 'Programme List', url: '/Programme/ProgrammeList' },
    { text: 'Lecturer List', url: '/Lecturer/LecturerList' },
    { text: 'Part Time Requisition List', url: '/Lecturer/PartTimeRequistionList' },
    { text: 'Course Allocation Units', url: '/Lecturer/CourseAllocationUnits' },
    { text: 'Vote Book Balance', url: '/ViewDocuments/ViewDocuments?DocT=VOTEBOOK' },
    { text: 'Leave Requisition List', url: '/Leave/LeaveRequisitionList' },
    { text: 'Purchase Requisition List', url: '/Purchase/PurchaseRequisitionList' },
    { text: 'Store Requisition List', url: '/Store/StoreRequisitionList' },
    { text: 'Imprest Requisition List', url: '/Imprest/ImprestRequisitionList' },
    { text: 'Imprest Memo Requisition List', url: '/ImprestMemo/ImprestMemoRequisitionList' },
    { text: 'Imprest Surrender List', url: '/ImprestSurrender/ImprestSurrender' },
    { text: 'Standing Imprest', url: '/Imprest/StandingImprest' },
    { text: 'Imprest Warranties', url: '/Imprest/ImprestWarrant' },
    { text: 'WorkShop Advance Requisition List', url: '/WorkShopAdvance/WorkShopAdvanceRequisitionList' },
    { text: 'WorkShop Advance Liquidation List', url: '/WorkshopLiquidation/WorkShopAdvanceLiquidationList' },
    { text: 'Petty Cash Requisition List', url: '/PettyCash/PettyCashRequisitionList' },
    { text: 'Staff Claim Requisition List', url: '/StaffClaim/StaffClaims' },
    { text: 'My ANUPEAisal List', url: '/ANUPEAisal/MyANUPEAisalList' },
    { text: 'My Review ANUPEAisal List', url: '/ANUPEAisal/MyReviewANUPEAisalList' },
    { text: 'Transport Requisition List', url: '/Transport/TransportRequisitionList' },
    { text: 'Training Requisition List', url: '/Training/TrainingRequisitionList' },
    { text: 'Training Extension Requisition List', url: '/Training/TrainingExtensionRequisitionList' },
    { text: 'Training Needs List', url: '/Training/TrainingNeedsList' },
    { text: 'Training Feedback Requisition List', url: '/Training/TrainingFeedbackRequisitionList' },
    { text: 'Training Evaluation Requisition List', url: '/Training/TrainingEvaluationRequisitionList' },
    { text: 'Fuel Card List', url: '/Fuel/FuelCardList' },
    { text: 'Fuel Requisition List', url: '/Fuel/FuelRequisitionList' },
    { text: 'Maintenance Requisition List', url: '/Maintenance/MaintenanceRequisitionList' },
    { text: 'ICT Requisition List', url: '/ICT/ICTRequisitionList' },
    { text: 'ICT Asset Transfer List', url: '/ICT/ICTAssetTransferList' },
    { text: 'ICT ServMnt List', url: '/ICT/ICTServMntList' },
    { text: 'ICT Assignment List', url: '/ICT/ICTAssignmentList' },
    { text: 'New Visitors List', url: '/Visitors/NewVisitorsList' },
    { text: 'Active Visitors List', url: '/Visitors/ActiveVisitorsList' },
    { text: 'Cleared Visitors List', url: '/Visitors/ClearedVisitorsList' },
    { text: 'Gate Pass List', url: '/GatePass/GatePassList' },
    { text: 'Approved Gate Pass List', url: '/GatePass/ApprovedGatePassList' },
    { text: 'Assigned Asset List', url: '/Asset/AssignedAssetList' },
    { text: 'Asset Transfer List', url: '/Asset/AssetTransferList' },
    { text: 'Document View Payslip', url: '/ViewDocument/DocumentViewPayslip' },
    { text: 'Document View P9', url: '/ViewDocument/DocumentViewp9' },
    { text: 'Leave Statement Report', url: '/ViewDocument/GetLeaveStatementReport' },
    { text: 'Document For Approval Summery (Open)', url: '/DocumentApproval/DocumentForApprovalSummery?rn=Open' },
    { text: 'Document For Approval Summery (Approved)', url: '/DocumentApproval/DocumentForApprovalSummery?rn=Approved' },
    { text: 'Document For Approval Summery (Rejected)', url: '/DocumentApproval/DocumentForApprovalSummery?rn=Rejected' },
    { text: 'Change Password', url: '/Settings/ChangePassword' },
    { text: 'PCA Approved List', url: '/PCA/PCAs' },
    { text: 'Time Sheet List', url: '/TimeSheet/TimeSheetList' },
    { text: 'Employee Requisition List', url: '/EmployeeRequistion/EmployeeRequisitionList' },
    { text: 'Staff Induction', url: '/StaffInduction/StaffInduction' },
    { text: 'Exit Interview', url: '/StaffClearance/ExitInterview' },
    { text: 'Staff Clearance', url: '/StaffClearance/StaffClearance' },
    { text: 'Leave Reimbursement List', url: '/LeaveReimbursement/LeaveReimbursementList' },
    { text: 'Time Off Lieu List', url: '/TimeOffLieu/TimeOffLieuList' },
    { text: 'Employees On Leave List', url: '/Leave/EmployeesOnLeaveList' },
    { text: 'Employees Approved Leave List', url: '/Leave/EmployeesApprovedLeaveList' },
    { text: 'Carry Forward List', url: '/TimeOffLieu/CarryForwardList' },
    { text: 'Leave Allocation List', url: '/TimeOffLieu/LeaveAllocationList' },
    { text: 'Self Emp Transfer List', url: '/Mobility/EmployeeTransferList' },
    { text: 'HR Disciplinary Cases List', url: '/Disciplinary/HRDicsiplinaryCasesList' },
    { text: 'Management Transfer List', url: '/Mobility/ManagementTransferList' },
    { text: 'Property List', url: '/FacilitiesAdministration/PropertyList' },
    { text: 'Work Ticket', url: '/Transport/WorkTicket' },
    { text: 'Reservation Request List', url: '/FacilitiesAdministration/ReservationRequestList' },
    { text: 'Approved Reservation List', url: '/FacilitiesAdministration/AprrovedReservationList' },
    { text: 'Individual Scorecard List', url: '/Performance/IndividualScorecardList' },
    { text: 'Leave Planner', url: '/Leave/LeavePlanner' },
    { text: 'Car Loan Applications List', url: '/WelfareManagement/CarLoanApplicationsList' },
    { text: 'Mortgage Loan Applications List', url: '/WelfareManagement/MortgageLoanApplicationsList' },
    { text: 'Salary Advance Loan Applications List', url: '/WelfareManagement/SalaryAdvanceLoanApplicationsList' },
    { text: 'Medical Card Replacement', url: '/WelfareManagement/MedicalCardReplacement' },
    { text: 'Vehicle Incident View', url: '/Transport/VehicleIncidentView' },
    { text: 'Dependant Change Requests', url: '/WelfareManagement/DependantChangeRequests' },
    { text: 'Human Resource Dashboard', url: '/Dashboard/HumanResource' },
    { text: 'Projects', url: '/Dashboard/Projects' },
    { text: 'DASS', url: '/Dashboard/DASS' },
    { text: 'FAQs', url: '/Common/FAQsView' },
    { text: 'HR Information', url: '/Common/HRInformation' },
    { text: 'Attendance List', url: '/Dashboard/Attendance' },
    { text: 'Finance', url: '/Dashboard/Finance' },
    { text: 'Accounts', url: '/Dashboard/Accounts' },
    { text: 'Procurement', url: '/Dashboard/Procurement' },
    { text: 'Resource Requirements', url: '/Workplans/ResourceRequirements' },
    { text: 'Expense Requisition', url: '/Workplans/ExpenseRequisition' },
    { text: 'Functional Procurement Plan', url: '/Purchase/FunctionalProcurementPlan' },
    { text: 'Store Requisition', url: '/Store/StoreRequisition' },
    { text: 'Draft Work Plans', url: '/Workplans/DraftWorkPlans' },
    { text: 'Project Proposal', url: '/Projects/ProjectProposal' },
    { text: 'Design Control', url: '/Projects/DesignControl' }
];

// Function to filter links based on search input
function filterLinks(searchValue) {
    var filteredLinks = links.filter(function (link) {
        return link.text.toLowerCase().includes(searchValue.toLowerCase());
    });

    displaySuggestions(filteredLinks);
}

// Function to display filtered links as suggestions
function displaySuggestions(filteredLinks) {
    var suggestionsList = document.getElementById('suggestions');
    suggestionsList.innerHTML = '';

    filteredLinks.forEach(function (link) {
        var listItem = document.createElement('li');
        var linkElement = document.createElement('a');
        linkElement.href = link.url;
        linkElement.textContent = link.text;
        listItem.appendChild(linkElement);
        suggestionsList.appendChild(listItem);
    });
}

// Function to navigate to selected link
function searchLinks() {
    var searchValue = document.getElementById('searchInput').value.toLowerCase();
    var filteredLinks = links.filter(function (link) {
        return link.text.toLowerCase() === searchValue;
    });

    if (filteredLinks.length > 0) {
        window.location = filteredLinks[0].url;
    } else {
        alert('No matching link found.');
    }
}