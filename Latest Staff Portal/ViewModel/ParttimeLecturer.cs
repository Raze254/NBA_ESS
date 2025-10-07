using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class PartTimeLecturerRegList
    {
        public IEnumerable<PartTimeLecturerDetails> ListOfPartTimeRequisition { get; set; }
    }

    public class PartTimeLecturerDetails
    {
        public string IDNo { get; set; }
        public string Date { get; set; }
        public string Surname { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
    }

    public class ParttimeView
    {
        public string ID_Number { get; set; }
        public string Surname { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Department_Code { get; set; }
        public string School_Code { get; set; }
        public string Campus_Code { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public List<SelectListItem> ListOfHREmpInitials { get; set; }
        public List<SelectListItem> ListOfCampus { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfSchool { get; set; }
    }
}