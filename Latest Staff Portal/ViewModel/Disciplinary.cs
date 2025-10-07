using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class HRDisciplinaryCase
    {
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Case_Number { get; set; }
        public string Case_Description { get; set; }
        public string Date_of_Complaint { get; set; }
        public string Type_of_Disciplinary_Case { get; set; }
        public string Status { get; set; }
        public string Created_By { get; set; }
        public List<SelectListItem> HRModelsList { get; set; }
        public List<SelectListItem> ModeOfLodgingCaseList { get; set; }
        public List<SelectListItem> ResponsibilityCentersList { get; set; }
        public string Period_From { get; set; }
        public string Period_To { get; set; }
        public bool Has_Multiple_Accused_Employees { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public string Designation { get; set; }
        public List<SelectListItem> OffenceTypeList { get; set; }
        public string Offence_Type { get; set; }
        public string Mode_of_Lodging_the_Complaint { get; set; }
        public string Responsibility_Center { get; set; }
        public bool Accuser_Is_Staff { get; set; }
        public string Accuser { get; set; }
        public string Non_Staff_Originator { get; set; }
        public string Previous_Interventions { get; set; }
        public string Action { get; set; }
    }

    public class HRModelsList
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ModeOfLodgingCaseList
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ResponsibilityCentersList
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class OffenceTypeList
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class DisciplineAccusedEmployees
    {
        public string Line_No { get; set; }
        public string Case_Number { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Designation { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
    }

    public class DisciplineDocumentView
    {
        public string No { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Designation { get; set; }
        public string Accused_Employee { get; set; }
        public string Accused_Employee_Name { get; set; }
        public string Accused_Employee_Designation { get; set; }
        public string Date_Created { get; set; }
        public string Document_Date { get; set; }
        public string Status { get; set; }
        public decimal Value_of_Property { get; set; }
        public string Description_of_Property { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> DocumentTypeList { get; set; }
        public string Action { get; set; }
    }

    public class DocumentType
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}