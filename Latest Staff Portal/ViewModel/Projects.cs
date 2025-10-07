using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class ProjectProposal
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string GlobalDimension2Code { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public string AdministrationUnitName { get; set; }
        public string RequestDescription { get; set; }
        public string Justification { get; set; }
        public string CreatedBy { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public string DeferalComments { get; set; }
    }

    public class DesignControlHeader
    {
        public string ProjectNo { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AdministrativeUnit { get; set; }
        public string GeographicLocation { get; set; }
        public string TotalEstimatedCost { get; set; }
        public string ProjectClassification { get; set; }
        public string CreatedBy { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public string CurrentActioningMember { get; set; }
        public string DesignApprovalStage { get; set; }
    }

    public class DesignControlTasks
    {
        public string HeaderNo { get; set; }
        public string EntryNo { get; set; }
        public string AttachmentCode { get; set; }
        public string DesignControlNo { get; set; }
        public string DesignControlType { get; set; }
        public string Name { get; set; }
        public string NoOfItems { get; set; }
    }
}