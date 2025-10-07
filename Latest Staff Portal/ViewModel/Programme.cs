using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class Programme
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ViewProgrammeStageDocFilters
    {
        public string AcademicYear { get; set; }
        public string ProgrammeOption { get; set; }
        public string Campus { get; set; }
        public string DocType { get; set; }
        public string Prog { get; set; }
        public string Stage { get; set; }
        public string ReportType { get; set; }
        public List<SelectListItem> ListOfAcademicYear { get; set; }
        public List<SelectListItem> ListOfProgrammeOption { get; set; }
        public List<SelectListItem> ListOfCampus { get; set; }
    }

    public class ProgrammeStageDocFilters
    {
        public string Prog { get; set; }
        public string Stage { get; set; }
        public string AcademicYear { get; set; }
        public string ProgrammeOption { get; set; }
        public string Campus { get; set; }
        public string DocType { get; set; }
        public string ReportType { get; set; }
    }

    public class ProgOptionList
    {
        public string Code { get; set; }
        public string Desription { get; set; }
    }

    public class ViewScoreSheetFilters
    {
        public string Semester { get; set; }
        public List<SelectListItem> ListOfSemesters { get; set; }
    }
}