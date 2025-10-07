using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class LeaveReimbursementList
    {
        public string No { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string Leave_Type { get; set; }
        public string Days_Applied { get; set; }
        public string Date { get; set; }
        public string Starting_Date { get; set; }
        public string End_Date { get; set; }
        public string Return_Date { get; set; }
        public string Reliever { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Department { get; set; }
        public string Responsibility { get; set; }
        public string DaystoReimburse { get; set; }
        public string LeaveNo { get; set; }
        public List<SelectListItem> ListOfLeaveApplied { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class LeaveReimbursementDocument
    {
        public LeaveReimbursementList DocumentDetails { get; set; }
        public string Reliever { get; set; }
        public List<SelectListItem> ListOfLeaveTypes { get; set; }
        public List<SelectListItem> ListOfRelievers { get; set; }
        public string Balance { get; set; }
        public List<SelectListItem> ListOfDays { get; set; }
        public string LeaveNo { get; set; }
        public List<SelectListItem> ListOfLeaveApplied { get; set; }
    }

    public class NewLeaveReimbursement
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
        public List<SelectListItem> ListOfLeaveTypes { get; set; }
        public List<SelectListItem> ListOfRelievers { get; set; }
        public string LeaveNo { get; set; }
        public List<SelectListItem> ListOfLeaveApplied { get; set; }
    }

    public class LeaveDetails
    {
        public string LeaveNo { get; set; }
        public string AppliedDays { get; set; }

        public string LeaveType { get; set; }
        public List<SelectListItem> ListOfDays { get; set; }
        public string Starting_Date { get; set; }
        public string End_Date { get; set; }
        public string Return_Date { get; set; }
    }

    public class DimensionValue
    {
        public string Dimension_Code { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Dimension_Value_Type { get; set; }
        public string Totaling { get; set; }
        public bool Blocked { get; set; }
        public bool Hardship_Area { get; set; }
        public string Hardship_Type { get; set; }
        public string Consolidation_Code { get; set; }
    }
}