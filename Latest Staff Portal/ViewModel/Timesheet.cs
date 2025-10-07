using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class TimesheetHeader
    {
        public string No { get; set; }
        public string Period { get; set; }
        public string Remarks { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string RespC { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string List_of_Key_Tasks_Undertaken { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Doc_Date { get; set; }
        public string Status { get; set; }
        public string Hours_Worked { get; set; }
        public string Total_Days_Worked { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }

        public string YList { get; set; }
        public List<SelectListItem> ListOfYears { get; set; }

        public string MList { get; set; }
        public List<SelectListItem> ListOfMonths { get; set; }
        public List<TimesheetLines> ListOfTimesheetLines { get; set; }
    }

    public class TimesheetLines
    {
        public string DocNo { get; set; }
        public string Ledger_No { get; set; }
        public string Line_No { get; set; }
        public string Description { get; set; }
        public string Day_Type { get; set; }
        public string ProjectCode { get; set; }

        public string Date { get; set; }
        public string Hours { get; set; }
        public string Non_Working_Day { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PercentageLoe { get; set; }
    }

    public class TimesheetUnderTaken
    {
        public string DocNo { get; set; }
        public string Entry_No { get; set; }
        public string Line_No { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Task_Undertaken { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Hours { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public string Dim2 { get; set; }
    }

    public class TimesheetDocument
    {
        public TimesheetHeader DocHeader { get; set; }
        public List<TimesheetLines> ListOfTimesheetLines { get; set; }
    }

    public class TimesheetLinesList
    {
        public string Status { get; set; }
        public List<TimesheetLines> ListOfTimesheetLines { get; set; }
    }

    public class TimesheetUnderTakenList
    {
        public string Status { get; set; }
        public List<TimesheetUnderTaken> ListOfTimesheetUnderTaken { get; set; }
    }
}