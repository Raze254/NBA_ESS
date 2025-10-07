using System.Collections.Generic;

namespace Latest_Staff_Portal.ViewModel
{
    public class ProgStages
    {
        public string ProgDescr { get; set; }
        public IEnumerable<PStageList> ListOfProgrammeStages { get; set; }
    }

    public class PStageList
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}