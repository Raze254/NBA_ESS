using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class ExitInterview
    {
        public string No { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Q5 { get; set; }

        public string Q6 { get; set; }
        public string Q7 { get; set; }
        public string Q8 { get; set; }
        public string Q9 { get; set; }
        public string Q10 { get; set; }
        public string Q11 { get; set; }
        public string Q12 { get; set; }
        public string Q13 { get; set; }
        public string Q14 { get; set; }
        public string Q15 { get; set; }
        public string Q16 { get; set; }
    }

    public class StaffClearance
    {
        public string DocumentNo { get; set; }
        public string Description { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeNames { get; set; }
        public string ExitMethod { get; set; }
        public string JobId { get; set; }
        public string ReasonsCode { get; set; }
        public List<SelectListItem> ReasonsList { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string DocumentDate { get; set; }
        public bool Posted { get; set; }
        public string DateofJoin { get; set; }
        public string LastworkingDate { get; set; }
        public string NoticeDate { get; set; }
        public string EmployeeExitDate { get; set; }
        public int Leave_Days_Due_Payable { get; set; }
        public int Pay_in_Line_of_Notice { get; set; }
        public string Notice_Period { get; set; }
        public int NoofHandOverNotes { get; set; }
        public int NoofOpenHandOverNotes { get; set; }
        public int NoofOpenHandClearedNotes { get; set; }
        public string ClearingSection { get; set; }
        public string PrimaryDirectorate { get; set; }
        public string PrimaryDirectorateName { get; set; }
        public string PrimaryDepartment { get; set; }
        public string PrimaryDepartmentName { get; set; }
        public string ResponsibleEmployee { get; set; }
    }

    public class HandoverItemLine
    {
        public string ExitHeaderNo { get; set; }
        public string DocumentType { get; set; }
        public int LineNo { get; set; }
        public string Type { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string ReceivedBy { get; set; }
    }
}