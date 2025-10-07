using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class P9Details
    {
        public string YList { get; set; }
        public List<SelectListItem> ListOfYears { get; set; }
    }

    public class PayslipDetails
    {
        public string YList { get; set; }
        public List<SelectListItem> ListOfYears { get; set; }
    }

    public class MonthList
    {
        public string MList { get; set; }
        public List<SelectListItem> ListOfMonths { get; set; }
    }

    public class YearCodes
    {
        public string YList { get; set; }
    }

    public class MonthCodes
    {
        public string MCode { get; set; }
        public string MDesc { get; set; }
    }
}