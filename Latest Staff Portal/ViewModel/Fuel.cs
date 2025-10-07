using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class FuelCardList
    {
        public string No { get; set; }
        public string ReceiptNo { get; set; }
        public string RegistrationNo { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public string FuelCardNo { get; set; }
        public string FuelCardPin { get; set; }
        public int VehicleLimit { get; set; }
        public int TotalAllocatedLimit { get; set; }
        public int TotalBalance { get; set; }
        public int AllocatedVehicleLimit { get; set; }
        public int VehicleBalance { get; set; }
        public int TotalAmountSpend { get; set; }
        public int TotalAmountSpendPerVehicle { get; set; }
    }

    public class FuelRechargeCard
    {
        public string No { get; set; }
        public string RegistrationNo { get; set; }
        public List<SelectListItem> ListOfRegistrationNo { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string CardType { get; set; }
        public List<SelectListItem> ListOfCardType { get; set; }
        public int LowerLimit { get; set; }
        public int TotalAllocatedLimit { get; set; }
        public DateTime StartDate { get; set; }
        public string RequisitionInterval { get; set; }
        public DateTime EndDate { get; set; }
        public string FuelCardNo { get; set; }
        public string Status { get; set; }
        public bool Posted { get; set; }
        public int TotalAmountSpend { get; set; }
        public int TotalBalance { get; set; }
        public int AllocatedVehicleLimit { get; set; }
        public int TotalAmountSpendPerVehicle { get; set; }
        public int VehicleBalance { get; set; }
    }

    public class Fuel
    {
        public string No { get; set; }
        public string Date_Requested { get; set; }
        public string Driver { get; set; }
        public string Driver_Name { get; set; }
        public string Vehicle { get; set; }
        public string Max_Amount { get; set; }
        public string Amount_Consumed { get; set; }
        public string Amount_To_Topup { get; set; }
        public string Status { get; set; }
    }

    public class FuelRequisition
    {
        public string No { get; set; }
        public string Date_Requested { get; set; }
        public string RequisitionType { get; set; }
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
        public string Date_Taken { get; set; }
        public string FuelType { get; set; }
        public string Quantity_Ltrs { get; set; }
        public string Price_per_Litre { get; set; }
        public string Total_Fuel_Price { get; set; }

        public string OilType { get; set; }
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

    public class FuelRequisitionList
    {
        public string RequisitionNo { get; set; }
        public string RequestDate { get; set; }
        public string JobNo { get; set; }
        public string JobName { get; set; }
        public decimal TotalPriceOfFuel { get; set; }
        public string Status { get; set; }
    }

    public class FuelRequisitionCard
    {
        public string RequisitionNo { get; set; }
        public decimal TotalPriceOfFuel { get; set; }
        public string TransportRequisitionNo { get; set; }
        public List<SelectListItem> ListOfTransportRequisitionNo { get; set; }
        public string RouteCode { get; set; }
        public string RouteDescription { get; set; }
        public string RequestDate { get; set; }
        public string Status { get; set; }
        public string PreparedBy { get; set; }
        public string PostedInvoiceNo { get; set; }
        public string JobNo { get; set; }
        public List<SelectListItem> ListOfJobNo { get; set; }
        public string JobTask { get; set; }
        public List<SelectListItem> ListOfJobTask { get; set; }
        public string JobName { get; set; }
        public string JobTaskName { get; set; }
    }

    public class FuelCardLine
    {
        public string Reciept_No { get; set; }
        public string No { get; set; }
        public int Amount { get; set; }
        public string DateofFueling { get; set; }
        public string Vehicle_RegNo { get; set; }
        public List<SelectListItem> ListOfVehicle_RegNo { get; set; }
        public string vehicle_Make { get; set; }
        public string Driver { get; set; }
        public List<SelectListItem> ListOfDriver { get; set; }
        public string Driver_Names { get; set; }
    }
}