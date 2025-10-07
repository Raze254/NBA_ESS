using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class SponsorshipList
    {
        public string No { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
    }

    public class NewSponsorship
    {
        public string Campus { get; set; }
        public string Department { get; set; }
        public string Customer { get; set; }
        public string RespC { get; set; }
        public List<SelectListItem> ListOfCustomers { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
        public List<SelectListItem> ListOfCampus { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
    }

    public class ResourcesList
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class NewSponsorshipDocument
    {
        public string No { get; set; }
        public string Title { get; set; }
        public string Objective { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PInvest { get; set; }
        public string RespC { get; set; }
        public string DateCreated { get; set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
    }
}