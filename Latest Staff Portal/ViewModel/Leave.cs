using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class LeaveReqList
    {
        public string No { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string Leave_Type { get; set; }
        public string Applied_Days { get; set; }
        public string Date { get; set; }
        public string Starting_Date { get; set; }
        public string End_Date { get; set; }
        public string Return_Date { get; set; }
        public string Reliever { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Department { get; set; }
        public string Responsibility { get; set; }
        public string PhoneNo { get; set; }
        public string RelieverImplementingUnit { get; set; }
        public string EmployeeCategory { get; set; }
        public List<SelectListItem> ListOfImplementingUnits { get; set; }
        public List<SelectListItem> ListOfRelievers { get; set; }
        public List<HandoverTask> ListOfHandoverTasks { get; set; }
        public List<HandoverFile> ListOfHandoverFiles { get; set; }
        public bool IsSupervisor { get; set; }
        public string RelieverName { get; set; }
        public string RelieverAdminUnitName { get; set; }
        public string AdminUnitName { get; set; }
        public string GeographicalName { get; set; }
    }

    public class LeaveDocumentDetails
    {
        public LeaveReqList DocumentDetails { get; set; }
        public string Reliever { get; set; }
        public List<SelectListItem> ListOfLeaveTypes { get; set; }
        public List<SelectListItem> ListOfRelievers { get; set; }
        public string Balance { get; set; }
        public List<SelectListItem> ListOfDays { get; set; }
    }

    public class NewLeaveApplication
    {
        public string Leave_Type { get; set; }
        public string Reliever { get; set; }
        public string LeaveBal { get; set; }
        public string AllocatedDays { get; set; }
        public string ReimbDays { get; set; }
        public string CarryForawrd { get; set; }
        public string EarnedLeaveDays { get; set; }
        public string LeaveTaken { get; set; }
        public string AppliedDays { get; set; }
        public string implementingUnit { get; set; }
        public string PhoneNo { get; set; }
        public List<SelectListItem> ListOfLeaveTypes { get; set; }
        public List<SelectListItem> ListOfRelievers { get; set; }
        public List<SelectListItem> ListOfImplementingUnits { get; set; }
        public int ChildsAge { get; set; }
        public List<SelectListItem> ListOfChildsAge { get; set; }
        public string SupervisorName { get; set; }
        public string CostCenterName { get; set; }
        public string DutyStation { get; set; }
        public string HardshipDays { get; set; }
    }

    public class LvTypes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class RelieverList
    {
        public string No { get; set; }
        public string Name { get; set; }
    }

    public class ImplementingUnitsList
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class LeaveBalance
    {
        public string Balance { get; set; }
        public string AllocatedDays { get; set; }
        public string CarryForawrd { get; set; }
        public string ReimbDays { get; set; }
        public string EarnedLeaveDays { get; set; }
        public string LeaveTaken { get; set; }
        public string HardshipDays { get; set; }
        public List<SelectListItem> ListOfDays { get; set; }
    }

    public class DropDownBalance
    {
        public string Code { get; set; }
    }

    public class EndReturnDates
    {
        public string EndDate { get; set; }
        public string ReturnDate { get; set; }
    }

    public class LeavePlannerList
    {
        public string Application_Code { get; set; }
        public string Employee_No { get; set; }
        public List<SelectListItem> ListOfEmployeeNo { get; set; }
        public string Leave_Calendar { get; set; }
        public List<SelectListItem> ListOfLeave_Calendar { get; set; }
        public string Names { get; set; }
        public string Job_Tittle { get; set; }
        public string Status { get; set; }
    }

    public class LeavePlannerLines
    {
        public int Line_No { get; set; }
        public string Application_Code { get; set; }
        public string Leave_Type { get; set; }
        public List<SelectListItem> ListOfLeave_Type { get; set; }
        public int Days_Applied { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string Return_Date { get; set; }
        public string Applicant_Comments { get; set; }
        public bool Request_Leave_Allowance { get; set; }
        public string Reliever { get; set; }
        public string Reliever_Name { get; set; }
        public int Approved_days { get; set; }
        public DateTime Date_of_Exam { get; set; }
        public string Details_of_Examination { get; set; }
    }

    public class LeavePlannerLinesList
    {
        public List<LeavePlannerLines> ListOfLeavePlannerLines { get; set; }
    }

    public class EditLeavePlannerLineObj
    {
        public string DocNo { get; set; }
        public int LineNo { get; set; }
        public string LeaveType { get; set; }
        public string StartDate { get; set; }
        public decimal DaysApplied { get; set; }
    }

    public class LeaveApplicationRecall
    {
        public string Recall_No { get; set; }
        public string Application_No { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Leave_Code { get; set; }
        public int Days_Applied { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string Application_Date { get; set; }
        public int Approved_Days { get; set; }
        public string Approved_Start_Date { get; set; }
        public int Days_Recalled { get; set; }
        public string Recall_Date_From { get; set; }
        public string Recall_Date_To { get; set; }
        public string Reason_for_Recall { get; set; }
        public bool Posted { get; set; }
    }

    public class LeavePlannerDocument
    {
        public LeavePlannerList DocHeader { get; set; }

        public List<LeavePlannerLines> ListOfLeavePlannerLines { get; set; }
    }

    public class HandoverTask
    {
        public string No { get; set; }
        public string Activity { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

    public class HandoverFile
    {
        public string No { get; set; }
        public string Activity { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}