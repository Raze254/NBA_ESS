using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class EmpTransferReqList
    {
        public string Document_No { get; set; }
        public string Employee_No { get; set; }
        public string Original_Station { get; set; }
        public string Original_Station_Name { get; set; }
        public string Transfer_Date { get; set; }
        public string New_Station { get; set; }
        public string New_Sattion_Name { get; set; }
        public string Employee_Name { get; set; }
        public string To_Hardship_Area_x003F_ { get; set; }
        public string Remarks { get; set; }
        public string Distance { get; set; }
        public string Status { get; set; }
        public string Created_By { get; set; }
        public string Date_Created { get; set; }
        public string Suggested_Station_1 { get; set; }
        public string Suggested_Station_1_Name { get; set; }
        public string Suggested_Station_2 { get; set; }
        public string Suggested_Station_2_Name { get; set; }
        public string Suggested_Station_3 { get; set; }
        public string Suggested_Station_3_Name { get; set; }
        public string Transfer_Reason_Code { get; set; }
    }

    public class EmpTransferView
    {
        public string Document_No { get; set; }
        public string Employee_Name { get; set; }
        public List<SelectListItem> ListOfDutyStations { get; set; }
        public string Original_Station { get; set; }
        public string Original_Station_Name { get; set; }
        public string Transfer_Date { get; set; }
        public string New_Station { get; set; }
        public string To_Hardship_Area_x003F_ { get; set; }
        public string Remarks { get; set; }
        public string Distance { get; set; }
        public string Status { get; set; }
        public string Created_By { get; set; }
        public string Date_Created { get; set; }
        public string Suggested_Station_1 { get; set; }
        public string Suggested_Station_1_Name { get; set; }
        public string Suggested_Station_2 { get; set; }
        public string Suggested_Station_2_Name { get; set; }
        public string Suggested_Station_3 { get; set; }
        public string Suggested_Station_3_Name { get; set; }
        public string Transfer_Reason_Code { get; set; }
        public List<SelectListItem> TransferReasonsList { get; set; }
        public string HOS_Recommendation { get; set; }
        public string MobilityRecommendation { get; set; }
        public string HeadofMobility { get; set; }
        public string DirectorRecommendation { get; set; }
        public string HOS_Remarks { get; set; }
    }

    public class DutyStationsList
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class TransferReasonsList
    {
        public string Reason_Code { get; set; }
        public string Description { get; set; }
    }

    public class NewSelfTransferReq
    {
        public string Document_No { get; set; }
        public string Employee_No { get; set; }
        public string Original_Station { get; set; }
        public string Transfer_Date { get; set; }
        public string Suggested_Station_1 { get; set; }
        public string Suggested_Station_2 { get; set; }
        public string Suggested_Station_3 { get; set; }
        public List<SelectListItem> ListOfDutyStations { get; set; }
        public List<SelectListItem> TransferReasonsList { get; set; }
        public string Transfer_Reason_Code { get; set; }
        public string Remarks { get; set; }
        public string Distance { get; set; }
    }

    public class ManagementTransferList
    {
        public string Document_No { get; set; }
        public string Employee_No { get; set; }
        public string Original_Station { get; set; }
        public string Original_Station_Name { get; set; }
        public string Transfer_Date { get; set; }
        public string New_Station { get; set; }
        public string New_Sattion_Name { get; set; }
        public string Employee_Name { get; set; }
        public string To_Hardship_Area_x003F_ { get; set; }
        public string Remarks { get; set; }
        public string Distance { get; set; }
        public string Status { get; set; }
        public string Created_By { get; set; }
        public string Date_Created { get; set; }
    }

    public class NewManagementTransferReq
    {
        public string Document_No { get; set; }
        public string Employee_Name { get; set; }
        public string Employee_No { get; set; }
        public string Original_Station { get; set; }
        public string Original_Station_Name { get; set; }
        public string Transfer_Date { get; set; }
        public string New_Station { get; set; }
        public string New_Sattion_Name { get; set; }
        public List<SelectListItem> ListOfDutyStations { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> TransferReasonsList { get; set; }
        public string Transfer_Reason_Code { get; set; }
        public string Remarks { get; set; }
        public string Distance { get; set; }
        public string To_Hardship_Area_x003F_ { get; set; }
        public string Status { get; set; }
    }
}