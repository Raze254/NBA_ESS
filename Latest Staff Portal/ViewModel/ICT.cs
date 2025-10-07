using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Latest_Staff_Portal.ViewModel
{

    public class ICTHelpDesk
    {
        
        public string Job_No { get; set; }
        public string Region_Name { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Constituency_Name { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Employee_No { get; set; }
        public string Requesting_Officer { get; set; }
        public string Requesting_Officer_Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ICT_Issue_Category { get; set; }
        public string Comments { get; set; }
        public string Request_Date { get; set; }
        public string Request_Time { get; set; }
        public string Status { get; set; }
        public bool Computer_not_Starting_up { get; set; }
        public bool Keyboard_x002C__Mouse_Failure { get; set; }
        public bool Printer_Failure { get; set; }
        public bool UPS_Failure { get; set; }
        public bool LCD_Monitor_Failure { get; set; }
        public bool Storage_Device_Failure { get; set; }
        public bool Hardware_Installation { get; set; }
        public string Others_x002C__specify_HW { get; set; }
        public bool Computer_Running_Loading_Slow { get; set; }
        public bool Network_Access_Problems { get; set; }
        public bool Antivirus_Inefficiency { get; set; }
        public bool Applications { get; set; }
        public bool Software_Installation { get; set; }
        public string Others_x002C__specify_SW { get; set; }
        public string HelpDesk_Category { get; set; }
        public string Helpdesk_subcategory { get; set; }
        public string ICT_Inventory { get; set; }
        public string ICT_Inventory_Name { get; set; }
        public string Description_of_the_issue { get; set; }
        public string Assigned_To { get; set; }
        public string Assigned_Date { get; set; }
        public string Expected_Resolution { get; set; }
        public string Action_Taken { get; set; }
        public List<SelectListItem> ListOfCategories { get; set; }
        public List<SelectListItem> ListOfSubCategories { get; set; }
        public List<SelectListItem> ListOfInventory { get; set; }
        

    }


    public class IssuanceVoucher
    {  
        public string No { get; set; }
        public string Type { get; set; }
        public string Helpdesk_No { get; set; }
        public string Issued_To_User_ID { get; set; }
        public string Issued_To_No { get; set; }
        public string Name { get; set; }
        public string Internal_external { get; set; }
        public string Issued_Date { get; set; }
        public string Description { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string Document_Status { get; set; }
        public string Location_Code { get; set; }
        public string Issued_By { get; set; }
    }

    public class IssuanceVoucherLines
    {      
        public string No { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Serial_No { get; set; }
        public DateTime Duration_of_Use_start_date { get; set; }
        public DateTime Duration_of_Use_end_date { get; set; }
        public string Duration_of_Use { get; set; }
        public string FA_No { get; set; }
        public string Reason_For_Movement { get; set; }
        public string Comments { get; set; }
        public string Return_Reason { get; set; }
        public string Return_Condition { get; set; }
    }




    public class FixedAssets
    {    
        public string No { get; set; }
        public string Description { get; set; }
        public string FA_Class_Code { get; set; }
        public string FA_Subclass_Code { get; set; }
        public string FA_Location_Code { get; set; }
        public bool Budgeted_Asset { get; set; }
        public string Serial_No { get; set; }
        public string Main_Asset_Component { get; set; }
        public string Component_of_Main_Asset { get; set; }
        public string Search_Description { get; set; }
        public string Responsible_Employee { get; set; }
        public bool Inactive { get; set; }
        public bool Blocked { get; set; }
        public bool Acquired { get; set; }
        public string Last_Date_Modified { get; set; }
        public string Depreciation_Book_Code { get; set; }
        public string FA_Posting_Group { get; set; }
        public string DepreciationBookCode { get; set; }
        public string FAPostingGroup { get; set; }
        public string DepreciationMethod { get; set; }
        public string DepreciationStartingDate { get; set; }
        public int NumberOfDepreciationYears { get; set; }
        public string DepreciationEndingDate { get; set; }
        public int BookValue { get; set; }
        public string DepreciationTableCode { get; set; }
        public bool UseHalfYearConvention { get; set; }
        public string AddMoreDeprBooks { get; set; }
        public string Vendor_No { get; set; }
        public string Maintenance_Vendor_No { get; set; }
        public bool Under_Maintenance { get; set; }
        public string Next_Service_Date { get; set; }
        public string Warranty_Date { get; set; }
        public bool Insured { get; set; }
        public string Tariff_No { get; set; }
        public string Country_Region_of_Origin_Code { get; set; }
        public bool Exclude_from_Intrastat_Report { get; set; }
        public int Net_Weight { get; set; }
        public int Gross_Weight { get; set; }
        public string Supplementary_Unit_of_Measure { get; set; }
    }










    public class ICTRequest
    {
        public string No { get; set; }
        public string Date { get; set; }
        public string ReqCat { get; set; }
        public string Directorate { get; set; }
        public string DirectorateName { get; set; }
        public string Department { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public string Urgency { get; set; }
        public string RequiredDate { get; set; }
        public string Status { get; set; }
        public string Resoltion_Remarks { get; set; }
        public string Assignee { get; set; }
    }

    public class ICTRequestLines
    {
        public string Description { get; set; }
        public string Quantity { get; set; }
    }

    public class NewICTRequisition
    {
        public string Code { get; set; }
        public string Directorate { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfCategory { get; set; }
        public List<SelectListItem> ListOfICTAsset { get; set; }
    }

    public class ICTAssetRequest
    {
        public string DocNo { get; set; }
        public string Asset { get; set; }
        public string Description { get; set; }
        public string Requestor_No { get; set; }
        public string Requestor_Name { get; set; }
        public string Date_Requested { get; set; }
        public string Date_Moved { get; set; }
        public string Date_Returned { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string reason { get; set; }
    }

    public class ICTServiceRequest
    {
        public string DocNo { get; set; }
        public string Asset { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string ServiceDate { get; set; }
        public string LastServiceDate { get; set; }
        public string NextSeviceDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Reason { get; set; }
    }

    public class ICTCancel
    {
        public string DocNo { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }
}