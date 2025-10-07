using System.Collections.Generic;

namespace Latest_Staff_Portal.ViewModel
{
    public class ImportantDocs
    {
        public string Name { get; set; }
        public string Extn { get; set; }
        public string CreationTime { get; set; }
        public string Status { get; set; }
    }

    public class ImportantDocsList
    {
        public string Status { get; set; }
        public List<ImportantDocs> DocList { get; set; }
    }
}