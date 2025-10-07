using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class StaffInductionList
    {
        public string No { get; set; }
        public string InductionPeriod { get; set; }
        public string InductionStartDate { get; set; }
        public string InductionEndDate { get; set; }
        public string InductionTopic { get; set; }
        public string ApplicationDate { get; set; }
        public string Venue { get; set; }
        public string DurationUnit { get; set; }
        public string respC { get; set; }
        public string NoOfEmployees { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string RespC { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }

        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
    }

    public class NewStaffInduction
    {
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string Dim8 { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
        public string RespC { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
    }

    public class NewStaffInductionDocument
    {
        public string No { get; set; }
        public string InductionPeriod { get; set; }
        public string InductionStartDate { get; set; }
        public string InductionEndDate { get; set; }
        public string InductionTopic { get; set; }
        public string ApplicationDate { get; set; }
        public string Venue { get; set; }
        public string DurationUnit { get; set; }
        public string respC { get; set; }
        public string NoOfEmployees { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string Dim8 { get; set; }

        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
    }

    public class StaffInductionEmployees
    {
        public string Type { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public List<SelectListItem> ListOfEmployee { get; set; }
    }

    public class InductionStaff
    {
        public string Status { get; set; }
        public List<StaffInductionEmployees> ListOfEmployees { get; set; }
    }

    public class StaffInductionDocument
    {
        public StaffInductionList DocHeader { get; set; }
        public List<StaffInductionEmployees> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfDrivers { get; set; }
        public List<SelectListItem> ListOfVehicles { get; set; }
        public List<ExternalPassengers> ListOfExternalPassengers { get; set; }
    }

    public class ExternalEmployees
    {
        public string Status { get; set; }
        public string LineNo { get; set; }
        public string Names { get; set; }
        public string Organization { get; set; }
    }

    public class ExternalEmployeesList
    {
        public string Status { get; set; }
        public List<ExternalEmployees> ListOfExternalEmployees { get; set; }
    }
}