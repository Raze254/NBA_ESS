using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class MaintenanceRequisition
    {
        public string No { get; set; }
        public string Date_Requested { get; set; }
        public string TypeofMaintenance { get; set; }
        public string MaintenanceDescription { get; set; }
        public string Driver { get; set; }

        public string Driver_Name { get; set; }
        public string Vehicle { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string RespC { get; set; }
        public List<SelectListItem> ListOfVehicles { get; set; }
        public List<SelectListItem> ListOfDrivers { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDivision { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public string Vendors { get; set; }
        public List<SelectListItem> ListOfVendors { get; set; }
        public string Request_Date { get; set; }
        public string LastServiceDate { get; set; }
        public string Quantity_Ltrs { get; set; }
        public string Price_per_Litre { get; set; }
        public string Total_Fuel_Price { get; set; }
        public string Oil { get; set; }
        public string Coolant { get; set; }
        public string Carwash { get; set; }
        public string BatteryWater { get; set; }
        public string WheelAlignment { get; set; }
        public string WheelBalancing { get; set; }
        public string Total_Cost { get; set; }
        public string Odometer_Reading { get; set; }
        public string Status { get; set; }
        public string Preparedby { get; set; }
        public string ClosedBy { get; set; }
        public string Date_Closed { get; set; }
        public string Vendor_InvoiceNo { get; set; }
        public string Posted_InvoiceNo { get; set; }
        public string Description { get; set; }
        public string VenderName { get; set; }
        public string DriverName { get; set; }
        public string FuelCardNo { get; set; }
        public string Fixed_AssetNo { get; set; }
        public string QuotationNo { get; set; }
    }

    public class MaintenanceRequest
    {
        public string RequisitionNo { get; set; }
        public string VehicleRegNo { get; set; }
        public string Vendor { get; set; }
        public List<SelectListItem> ListOfVehicleRegNo { get; set; }
        public List<SelectListItem> ListOfVendorDealer { get; set; }
        public string OdometerReading { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public string ChequeNo { get; set; }
        public DateTime DateTakenForMaintenance { get; set; }
        public string TypeOfMaintenance { get; set; }
        public string PreparedBy { get; set; }
        public List<SelectListItem> ListOfClosedBy { get; set; }
        public DateTime DateClosed { get; set; }
        public List<SelectListItem> ListOfVendorInvoiceNo { get; set; }
        public string VendorInvoice { get; set; }
        public List<SelectListItem> ListOfPostedInvoiceNo { get; set; }
        public string PostedInvoiceNo { get; set; }
        public string ClosedBy { get; set; }
        public string VendorDealer { get; set; }
        public string VendorInvoiceNo { get; set; }
    }
}