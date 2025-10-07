using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{

    public class StoreRequisitions
    {
      
        public string Document_Type { get; set; }
        public string No { get; set; }
        public string Requester_ID { get; set; }
        public string Request_By_No { get; set; }
        public string Request_By_Name { get; set; }
        public bool HOD { get; set; }
        public string Location_Code { get; set; }
        public string Order_Date { get; set; }
        public string Description { get; set; }
        public string Requisition_Type { get; set; }
        public string Status { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Department_Name { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Project_Name { get; set; }
        public int No_of_Archived_Versions { get; set; }
        public string Budget_Item { get; set; }

        public List<SelectListItem> ListOfLocation_Code { get; set; }
        public List<SelectListItem> ListOfProgramme { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }

    }


    public class StoreRequisitionsLines
    {
        public string Document_Type { get; set; }
        public string Document_No { get; set; }
        public int Line_No { get; set; }
        public string Item_Category { get; set; }
        public string Service_Item_Code { get; set; }
        public string Type { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string Location_Code { get; set; }
        public string Variant_Code { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public int Quantity_In_Store { get; set; }
        public int Qty_Requested { get; set; }
        public List<SelectListItem> ListOfPP_Planning_Category { get; set; }
        public List<SelectListItem> ListOfItems { get; set; }
        public List<SelectListItem> ListOfDescription { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
    }
}