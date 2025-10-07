using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Latest_Staff_Portal.ViewModel
{

    public class EmployeeExitVoucher
    {
        public string DocumentNo { get; set; }
        public string Description { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeNames { get; set; }
        public string ExitMethod { get; set; }
        public string JobId { get; set; }
        public string ReasonsCode { get; set; }
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
        public List<SelectListItem> ListOfExitMethods { get; set; }
        public List<SelectListItem> ListOfReasons { get; set; }
    }

    public class ExitPlanLines
    {

        public string Exit_Header_No { get; set; }
        public string Document_Type { get; set; }
        public int LineNo { get; set; }
        public string PrimaryDirectorate { get; set; }
        public string PrimaryDirectorateName { get; set; }
        public string ResponsibleEmployee { get; set; }
        public string Responsible_Employee_Name { get; set; }
        public string PlannedDate { get; set; }
        public string ActualDate { get; set; }
        public string Status { get; set; }
        public string clearedBy { get; set; }
        public List<SelectListItem> ListOfResponsibilityCenters { get; set; }
    }
}