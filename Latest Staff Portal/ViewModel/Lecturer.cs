using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class Lecturer
    {
        public string No { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Cellular_Phone_Number { get; set; }
        public string Gender { get; set; }
        public string Company_E_Mail { get; set; }
    }

    public class LecturerAssignedUnits
    {
        public string Code { get; set; }
        public string Stage { get; set; }
        public string Semester { get; set; }
        public string Unit { get; set; }
        public string Unit_Name { get; set; }
        public string Campus_Code { get; set; }
        public string Name { get; set; }
        public string Student_Type { get; set; }
    }

    public class LecCategory
    {
        public string Code { get; set; }
    }

    public class ListLecCategory
    {
        public string Code { get; set; }
        public List<SelectListItem> ListOfLecCategories { get; set; }
    }
}