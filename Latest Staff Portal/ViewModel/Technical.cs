using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Latest_Staff_Portal.ViewModel
{
    public class LabSampleManagement
    {
       
        public string Sample_ID { get; set; }
        public string Description { get; set; }
        public string Received_At { get; set; }
        public string Analysed_At { get; set; }
        public string Exported_At { get; set; }
        public string TurnAround_Time { get; set; }
        public string Status { get; set; }
        public string Created_By { get; set; }
    }

    public class LabSampleMgtLines
    {
      
        public int Line_No { get; set; }
        public string Sample_ID { get; set; }
        public string Sample_Type { get; set; }
        public string Source { get; set; }
        public string Staff_No { get; set; }
        public string Staff_Name { get; set; }
        public int Quantity_ml { get; set; }
        public string Storage_Location { get; set; }
        public string Notes { get; set; }
    }
}