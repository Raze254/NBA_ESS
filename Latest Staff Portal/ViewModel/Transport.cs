using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class TransportReqList
    {
        public string No { get; set; }
        public string Commencement { get; set; }
        public string Destination { get; set; }
        public string Vehicle { get; set; }
        public string Driver { get; set; }
        public string DateRequested { get; set; }
        public string DateOfTrip { get; set; }
        public DateTime TimeOfTrip { get; set; }
        public string ReturnDate { get; set; }
        public string respC { get; set; }
        public string NoOfDays { get; set; }
        public string NoOfExtPass { get; set; }
        public string Status { get; set; }
        public string Purpose { get; set; }
        public string ExternalDriver { get; set; }
        public string ExternalVehicle { get; set; }
        public string TransportCategory { get; set; }
        public string InspectionDate { get; set; }
        public string CurrentMileage { get; set; }
        public string VehicleType { get; set; }

        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim8 { get; set; }
        public string Dim7 { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public string TravelCost { get; set; }
        public string Accomodation { get; set; }
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
        public string TransportRequisitionNo { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedByName { get; set; }
        public string Position { get; set; }
        public string ApprovedImprestMemo { get; set; }
        public string From { get; set; }
        public string RouteCOde { get; set; }
        public List<SelectListItem> ListOfRouteCOde { get; set; }
        public string RouteDescription { get; set; }

        public DateTime DateofTrip { get; set; }


        public TimeSpan TimeRequested { get; set; }
        public DateTime Timeout { get; set; }
        public TimeSpan TimeIn { get; set; }
        public string JourneyRoute { get; set; }
        public int OpeningOdometerReading { get; set; }
        public int ClosingOdometerReading { get; set; }
        public int NoofDaysRequested { get; set; }
        public int NumberofHoursRequested { get; set; }
        public DateTime TripEndDate { get; set; }
        public TimeSpan TripEndTime { get; set; }

        public string ResponsibilityCenter { get; set; }

        public string WorkTicketNo { get; set; }
        public string UserId { get; set; }
        public bool HOD { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime DateofRequest { get; set; }
        public string Subject { get; set; }
        public string Comments { get; set; }
        public string FuelRequestCode { get; set; }
        public string ReasonforReopening { get; set; }
        public int Requisitiontype { get; set; }
        public List<SelectListItem> ListOfDrivers { get; set; }
        public List<SelectListItem> ListOfVehicles { get; set; }
    }

    public class NewTransportRequisition
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
        public string TravelCost { get; set; }
        public string Accomodation { get; set; }
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

    public class NewTransportDocument
    {
        public string Commencement { get; set; }
        public string Destination { get; set; }
        public string RespC { get; set; }
        public string DateTrip { get; set; }
        public string TimeTrip { get; set; }
        public string ReturnDate { get; set; }
        public string NoOfDays { get; set; }
        public string NoOfExtPass { get; set; }
        public string Purpose { get; set; }
        public string TransportCategory { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string Dim8 { get; set; }
        public string TravelCost { get; set; }
        public string Accomodation { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
    }

    public class Passengers
    {
        public string Type { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public List<SelectListItem> ListOfEmployee { get; set; }
    }

    public class TravellingStaff
    {
        public string Req_No { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public DateTime Date_of_Trip { get; set; }
        public int No_of_Days_Requested { get; set; }
        public DateTime Trip_End_Date { get; set; }
        public DateTime Trip_End_Time { get; set; }
    }

    public class TransDocument
    {
        public TransportReqList DocHeader { get; set; }
        public List<Passengers> ListOfPassengers { get; set; }
        public List<SelectListItem> ListOfDrivers { get; set; }
        public List<SelectListItem> ListOfVehicles { get; set; }
        public List<ExternalPassengers> ListOfExternalPassengers { get; set; }
    }

    public class ExternalPassengers
    {
        public string Status { get; set; }
        public string LineNo { get; set; }
        public string Names { get; set; }
        public string Organization { get; set; }
    }

    public class ExternalPassengerList
    {
        public string Status { get; set; }
        public List<ExternalPassengers> ListOfExternalPassengers { get; set; }
    }

    public class WorkTicket
    {
        public string Etag { get; set; }
        public string TicketNumber { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public DateTime DateOfRequest { get; set; }
        public DateTime MonthDate { get; set; }
        public string MonthName { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }
        public string ClosedBy { get; set; }
        public DateTime DateClosed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public TimeSpan TimeCreated { get; set; }
        public int TotalKmCovered { get; set; }
    }

    public class NewWorkTicket
    {
        public List<SelectListItem> ListOfDim1 { get; set; }
        public string Dim1 { get; set; }
        public string Vehicle { get; set; }
        public List<SelectListItem> ListOfVehicles { get; set; }
        public string AuthName { get; set; }
        public List<SelectListItem> ListOfAuths { get; set; }
        public string CostCentreCode { get; set; }
        public List<SelectListItem> ListOfCostCentreCodes { get; set; }
    }

    public class WorkTicketDocument
    {
        public string WorkTicketNo { get; set; }
        public string DateCreated { get; set; }
        public string AuthorizedBy { get; set; }
        public List<SelectListItem> ListOfAuthorizedBy { get; set; }
        public string MonthName { get; set; }
        public string MonthDate { get; set; }
        public string AuthorizedByName { get; set; }
        public string GOKWorkTicketNo { get; set; }
        public string TimeCreated { get; set; }
        public string PreviousWorkTicket { get; set; }
        public string TotalFuelCarriedFor { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string TotalFuelDrawn_Ltrs { get; set; }
        public string Status { get; set; }
        public string TotalKilometers { get; set; }
        public string ClosedBy { get; set; }
        public string TotalKmsCovered { get; set; }
        public string CostCentersCode { get; set; }
        public string TotalMilesPerLitre { get; set; }
        public string DateClosed { get; set; }
        public string TotalMilesPerLtr_Oil { get; set; }
        public string CreatedBy { get; set; }
        public string TotalOilDrawn_Ltrs { get; set; }
        public string Defect { get; set; }
        public string ActionTakenReported { get; set; }
        public string DefectDate { get; set; }
    }

    public class WorkTicketLines
    {
        public string Status { get; set; }
        public string TransportRequisitionNo { get; set; }
        public string Commencement { get; set; }
        public string RouteCode { get; set; }
        public string RouteDescription { get; set; }
        public string Date { get; set; }
        public string DriverNo { get; set; }
        public string DriverName { get; set; }
        public string DetailsofJourneyandRoute { get; set; }
        public decimal OilDraw_Litres { get; set; }
        public decimal FuelDrawn_Litres { get; set; }
        public string ReceiptNo { get; set; }
        public string TimeOut { get; set; }
        public string TimeIn { get; set; }
        public decimal OpeningOdometerReading { get; set; }
        public decimal ClosingOdometerReading { get; set; }
        public decimal FuelCarriedForward { get; set; }
        public string AuthorizedbyotherGOKOfficer { get; set; }
        public string Designation { get; set; }
        public string Defect { get; set; }
        public string ActionTakenReported { get; set; }
        public string DefectDate { get; set; }
        public string AuthorizedBy { get; set; }
        public string TotalKilometers { get; set; }
        public decimal TotalMilesPerLitre { get; set; }
        public decimal TotalMilesPerLtr_Oil { get; set; }
        public List<SelectListItem> TransportRequisitionNoList { get; set; }

        public List<SelectListItem> DriverNoList { get; set; }

        public string AuthName { get; set; }
        public List<SelectListItem> ListOfAuths { get; set; }
    }

    public class WorkTicketLinesList
    {
        public string Status { get; set; }

        public List<WorkTicketLines> ListOfWorkLines { get; set; }
    }

    public class TransportRequisitionList
    {
        public string TransportRequisitionNo { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedByName { get; set; }
        public string Position { get; set; }
        public string ApprovedImprestMemo { get; set; }
        public string From { get; set; }
        public string RouteCOde { get; set; }
        public List<SelectListItem> ListOfRouteCOde { get; set; }
        public string RouteDescription { get; set; }
        public string Destination { get; set; }
        public DateTime DateofTrip { get; set; }
        public DateTime ReturnDate { get; set; }

        public TimeSpan TimeRequested { get; set; }
        public DateTime Timeout { get; set; }
        public TimeSpan TimeIn { get; set; }
        public string JourneyRoute { get; set; }
        public int OpeningOdometerReading { get; set; }
        public int ClosingOdometerReading { get; set; }
        public int NoofDaysRequested { get; set; }
        public int NumberofHoursRequested { get; set; }
        public DateTime TripEndDate { get; set; }
        public TimeSpan TripEndTime { get; set; }
        public string Status { get; set; }
        public string ResponsibilityCenter { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public string WorkTicketNo { get; set; }
        public string UserId { get; set; }
        public bool HOD { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime DateofRequest { get; set; }
        public string Subject { get; set; }
        public string Comments { get; set; }
        public string FuelRequestCode { get; set; }
        public string ReasonforReopening { get; set; }
        public int Requisitiontype { get; set; }
        public List<SelectListItem> ListOfDrivers { get; set; }
        public List<SelectListItem> ListOfVehicles { get; set; }
        public string Vehicle { get; set; }
        public string Driver { get; set; }
    }

    public class VehicleIncident
    {
        public string IncidentNo { get; set; }
        public string IncidentDate { get; set; }
        public string IncidentTime { get; set; }
        public string IncidentDescription { get; set; }
        public string DriverID { get; set; }
        public List<SelectListItem> ListOfDrivers { get; set; }
        public string DriverName { get; set; }
        public string Location { get; set; }
        public string Recommendations { get; set; }
        public string Status { get; set; }
        public string ReportingDate { get; set; }
        public string DriverPhoneNumber { get; set; }
        public string DriverLicenceDetails { get; set; }
        public string VehicleID { get; set; }
        public List<SelectListItem> ListOfVehicles { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string LicencePlateNumber { get; set; }
        public int CurrentMileage { get; set; }
        public string PersonCulpable { get; set; }
        public string ResponsibleEmployee { get; set; }
        public List<SelectListItem> ListOfResponsibleEmployee { get; set; }
        public string WorkTicketNo { get; set; }
        public List<SelectListItem> ListOfWorkTickets { get; set; }
        public string DutyStation { get; set; }
        public List<SelectListItem> ListOfDutyStations { get; set; }
        public string StationName { get; set; }
        public bool Submitted { get; set; }
        public string SubmittedBy { get; set; }
        public string SubmittedOn { get; set; }
        public string ServiceProvider { get; set; }
        public string ServiceProviderName { get; set; }
        public string ActionTaken { get; set; }
        public string InspectionRemarks { get; set; }
        public decimal EstimatedAmount { get; set; }
        public int EstimatedNumber { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public bool Grounded { get; set; }
        public string GroundedBy { get; set; }
        public string GroundedOn { get; set; }
    }
}