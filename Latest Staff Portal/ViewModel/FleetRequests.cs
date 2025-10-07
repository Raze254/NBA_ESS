using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Latest_Staff_Portal.ViewModel
{
    public class FleetRequests
    {
        public string Transport_Requisition_No { get; set; }
        public string Requested_By { get; set; }
        public string Requested_By_Name { get; set; }
        public string Position { get; set; }
        public string Approved_Imprest_Memo { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public string Route_Description { get; set; }
        public string Date_of_Trip { get; set; }
        public string Time_Requested { get; set; }
        public string Time_out { get; set; }
        public string Time_In { get; set; }
        public string Journey_Route { get; set; }
        public int Opening_Odometer_Reading { get; set; }
        public int Closing_Odometer_Reading { get; set; }
        public int No_of_Days_Requested { get; set; }
        public int Number_of_Hours_Requested { get; set; }
        public string Trip_End_Date { get; set; }
        public string Trip_End_Time { get; set; }
        public string Status { get; set; }
        public string Responsibility_Center { get; set; }
        public string Work_Ticket_No { get; set; }
        public string User_Id { get; set; }
        public bool HOD { get; set; }
        public DateTime Added_On { get; set; }
        public string Date_of_Request { get; set; }
        public string Vehicle_Allocated { get; set; }
        public string Vehicle_Allocated_by { get; set; }
        public string Driver_Allocated { get; set; }
        public string Driver_Name { get; set; }
        public string Subject { get; set; }
        public string Comments { get; set; }
        public string Fuel_Request_Code { get; set; }
        public string Reason_for_Reopening { get; set; }
        public string Reg_No { get; set; }
        public string Model { get; set; }
        public string Officer_Taking_Over { get; set; }
        public string Immediate_Former { get; set; }
        public bool Spare_Wheel { get; set; }
        public bool Wheel_Spammer { get; set; }
        public bool Hydraulic_Jack { get; set; }
        public bool Radio { get; set; }
        public string Wheel_Caps { get; set; }
        public bool Side_Mirrors { get; set; }
        public int Fuel_Card_No { get; set; }
        public bool Floor_Mats { get; set; }
        public string Body_Condition { get; set; }
        public int Current_Mileage { get; set; }
        public string Tyre_Condition { get; set; }
        public bool Logbook { get; set; }
        public string Observations { get; set; }
        public int Capacity { get; set; }

        public List<SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfImprestMemo { get; set; }
        public List<SelectListItem> ListOfResponsibilityCenters { get; set; }
    }
    public class FleetRequestLines
    {
        public string Req_No { get; set; }
        public string Employee_No { get; set; }
        public string Employee_Name { get; set; }
        public string Date_of_Trip { get; set; }
        public int No_of_Days_Requested { get; set; }
        public string Trip_End_Date { get; set; }
        public string Trip_End_Time { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }
    }
    public class FleetRequestItems
    {

        public int Ticket_No { get; set; }
        public string Requisition_Number { get; set; }
        public string Item_Type { get; set; }
        public string Description { get; set; }
        public string Serial_Number { get; set; }
        public int Quantity { get; set; }
        public string Purpose { get; set; }
        public bool Returnable { get; set; }
        public string Item_Return_Date { get; set; }
        public string Remarks_Comments { get; set; }
        public List<SelectListItem> ListOfItemCategories { get; set; }
    }


    public class MaintenanceRequest2
    {
        public string Requisition_No { get; set; }
        public string Vehicle_Reg_No { get; set; }
        public string Cost_Center_Name { get; set; }
        public string Vehicle_Location { get; set; }
        public int Odometer_Reading { get; set; }
        public string Requested_By { get; set; }
        public string Department_Name { get; set; }
        public string Unit_Name { get; set; }
        public string Vendor_Dealer { get; set; }
        public string Vendor_Name { get; set; }
        public string Responsible_Employee { get; set; }
        public string Responsible_Employee_Name { get; set; }
        public string Request_Date { get; set; }
        public string Description { get; set; }
        public string Service_Code { get; set; }
        public string Service_Name { get; set; }
        public string Status { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Driver { get; set; }
        public string Driver_Name { get; set; }
        public string Prepared_By { get; set; }
        public string Closed_By { get; set; }
        public string Date_Closed { get; set; }
        public string Vendor_Invoice_No { get; set; }
        public string Project_Number { get; set; }
        public string Task_Number { get; set; }
        public int Maintenance_Cost { get; set; }
        public string Comments_Remarks { get; set; }
        public string Parts_Changed { get; set; }
        public string Pre_Repair_Inspection { get; set; }
        public string Post_Repair_Inspection { get; set; }

        public List<SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfVendors { get; set; }
        public List<SelectListItem> ListOfServiceItem { get; set; }
        public List<SelectListItem> ListOfVehicles { get; set; }
        
    }

    public class MonthlyWorkTicketCard
    {
       
        public string Daily_Work_Ticket { get; set; }
        public string Month_Date { get; set; }
        public string Month_Name { get; set; }
        public string Ticket_No { get; set; }
        public string Previous_Work_Ticket_No { get; set; }
        public string Vehicle_Registration_No { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Department { get; set; }
        public string Closed_by { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Date_Closed { get; set; }
        public string Created_By { get; set; }
        public string Date_Created { get; set; }
        public string Authorized_By { get; set; }
        public string Authorized_By_Name { get; set; }
        public string Time_Created { get; set; }
        public int Total_Fuel_Carried_Forward { get; set; }
        public double Total_Fuel_Drawn_Ltrs { get; set; }
        public int Total_Kilometers { get; set; }
        public int Total_Km_x0027_s_Covered { get; set; }
        public int Total_Miles_Per_Litre_Fuel { get; set; }
        public int Total_Miles_Per_Ltr_Oil { get; set; }
        public int Total_Oil_Drawn_Ltrs { get; set; }
        public string Defect { get; set; }
        public string Defect_Date { get; set; }
        public string Action_Taken_Reported { get; set; }
        public List<SelectListItem> ListPayPeriods { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }
        public List<SelectListItem> ListOfVehicleReg { get; set; }
        public List<SelectListItem> ListOfRegions { get; set; }
    }

    public class DailyWorkTicketLines
    {
       
        public string Daily_Work_Ticket { get; set; }
        public int EntryNo { get; set; }
        public string Transport_Requisition_No { get; set; }
        public string Commencement { get; set; }
        public string Destination { get; set; }
        public string Route_Description { get; set; }
        public string Date { get; set; }
        public string Driver_Allocated { get; set; }
        public string Driver_Name { get; set; }
        public string Journey_Route { get; set; }
        public int Oil_Drawn_Litres { get; set; }
        public decimal Fuel_Drawn_Litres { get; set; }
        public string Order_No { get; set; }
        public string Time_out { get; set; }
        public string Time_In { get; set; }
        public int Opening_Odometer_Reading { get; set; }
        public int Closing_Odometer_Reading { get; set; }
        public int Total_Kilometres { get; set; }
        public int Miles_Per_Litre_Fuel { get; set; }
        public int Miles_Per_Litre_Oil { get; set; }
        public int Fuel_Carried_Forward { get; set; }
        public string Authorized_By { get; set; }
        public string GOK_Officer { get; set; }
        public string Position { get; set; }
        public string Defect_Date { get; set; }
        public string Defect { get; set; }
        public string Action_Taken_Reported { get; set; }
    }

    public class FuelRequisitionCard
    {
       
        public string Requisition_No { get; set; }
        public int Total_Price_of_Fuel { get; set; }
        public string Transport_Requisition_No { get; set; }
        public string Route_Code { get; set; }
        public string Route_Description { get; set; }
        public string Request_Date { get; set; }
        public string Status { get; set; }
        public string Prepared_By { get; set; }
        public string Posted_Invoice_No { get; set; }
        public string Job_No { get; set; }
        public string Job_Task { get; set; }
        public string Job_Name { get; set; }
        public string Job_Task_Name { get; set; }


        public List<SelectListItem> ListOfTransportRequisitions { get; set; }
    }


    public class FuelCard
    {
        
        public string No { get; set; }
        public string Registration_No { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Card_Type { get; set; }
        public int lowerlimit { get; set; }
        public int Total_Allocated_Limit { get; set; }
        public string Start_Date { get; set; }
        public string Requisition_Interval { get; set; }
        public string End_Date { get; set; }
        public string Fuel_Card_No { get; set; }
        public string Status { get; set; }
        public bool Posted { get; set; }
        public int Total_Amount_Spend { get; set; }
        public int Total_Balance { get; set; }
        public int Allocated_Vehicle_Limit { get; set; }
        public int Total_Amt_Spend_per_Vehicle { get; set; }
        public int Vehicle_Balance { get; set; }

        public List<SelectListItem> ListOfCardTypes { get; set; }

        public List<SelectListItem> ListOfVehicleReg { get; set; }
    }
}