using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class GatePass
    {
        public string No { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string DateOut { get; set; }
        public string TimeOut { get; set; }
        public string AssetTransferNo { get; set; }
        public string DateCreated { get; set; }
        public string AssetDescription { get; set; }
        public string AssetFromLocation { get; set; }
        public string AssetToLocation { get; set; }
        public string ResponsibilityCenter { get; set; }
        public string Status { get; set; }
        public string ToBeReturned { get; set; }
        public string Comment { get; set; }
    }

    public class GateDetailList
    {
        public string Status { get; set; }
        public List<GatePass> GatePassDetails { get; set; }
    }

    public class NewGatePassForm
    {
        public GatePass GatePassDoc { get; set; }
        public List<SelectListItem> ListOfAssetTransfer { get; set; }
    }
}