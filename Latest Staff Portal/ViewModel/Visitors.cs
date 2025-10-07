using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class Visitors
    {
        public string No { get; set; }
        public string visitorCat { get; set; }
        public string VisitorName { get; set; }
        public string PurposeofVisit { get; set; }
        public string Department { get; set; }
        public string IDNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string CarRegNumber { get; set; }
        public string PersonToSee { get; set; }
        public string VisitorPassNo { get; set; }
        public string Status { get; set; }
        public string InitiatedBy { get; set; }
        public string InitiatedByTime { get; set; }
        public string ClearedBy { get; set; }
        public string ClearedByTime { get; set; }
    }

    public class NewVisiorForm
    {
        public Visitors VisitDoc { get; set; }
        public List<SelectListItem> ListOfEmployee { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
    }

    public class VisitDetailList
    {
        public string Status { get; set; }
        public List<Visitors> VisitDetails { get; set; }
    }
}