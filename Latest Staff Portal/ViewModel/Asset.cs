using System.Collections.Generic;
using System.Web.Mvc;
namespace Latest_Staff_Portal.ViewModel
{

        public class FunctionalDisposalPlan
        {
            public string Disposal_Catetory;
            public string No { get; set; }
            public string Administrative_Unit { get; set; }
            public string Disposal_Category { get; set; }
            public string Financial_Year_Code { get; set; }
            public string Start_Date { get; set; }
            public string End_Date { get; set; }
            public string Description { get; set; }
            public string Consolidate_Disposal_Plan { get; set; }
            public string Created_By { get; set; }
            public string Date_Created { get; set; }
            public string Time_Created { get; set; }
            public string Global_Dimension_1_Code { get; set; }
            public string Geo_Graphical_Name { get; set; }
            public string Global_Dimension_2_Code { get; set; }
            public string Document_Status { get; set; }
            public List<DropdownList> Dimension1Options { get; set; }
            public List<DropdownList> financialOptions { get; set; }
    }

        public class FunctionalDisposalPlanLines
        {
            public string Disposal_header { get; set; }
            public int Line_No { get; set; }
            public string Disposal_Catetory { get; set; }
            public string Type { get; set; }
            public string ItemNo { get; set; }
            public string DescriptionofItem { get; set; }
            public string UnitofIssue { get; set; }
            public int Quantity { get; set; }
            public string DateOfPurchase { get; set; }
            public int PurchaseUnitPrice { get; set; }
            public int TotalPurchasePrice { get; set; }
            public int Estimatedcurrentvalue { get; set; }
            public int TotalEstimatedcurrentvalue { get; set; }
            public string GeneralCondition { get; set; }
            public int ReserveValue { get; set; }
            public string DisposalcommitteeRecommend { get; set; }
            public string DisposalType { get; set; }
            public string DisposalMethods { get; set; }
            public bool Loaded_to_Consolidated_Plan { get; set; }
            public List<DropdownList> FixedAssetDropdown { get; set; }
            public List<DropdownList> ItemsDropdown { get; set; }
            public List<DropdownList> DisposalTypeDropdown { get; set; }
        }


    public class ConsolidatedDisposalPlan
    {
        public string No { get; set; }
        public string Financial_Year_Code { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string Description { get; set; }
        public string Created_By { get; set; }
        public string Date_Created { get; set; }
        public string Time_Created { get; set; }
        public string Department_Filter { get; set; }
        public string Status { get; set; }
    }

    public class ConsolidatedDisposalPlanLines
    {
        public string Disposal_header { get; set; }
        public int Line_No { get; set; }
        public string Disposal_Catetory { get; set; }
        public string Type { get; set; }
        public string ItemNo { get; set; }
        public string DescriptionofItem { get; set; }
        public string UnitofIssue { get; set; }
        public int Quantity { get; set; }
        public string DateOfPurchase { get; set; }
        public int PurchaseUnitPrice { get; set; }
        public int TotalPurchasePrice { get; set; }
        public int Estimatedcurrentvalue { get; set; }
        public int TotalEstimatedcurrentvalue { get; set; }
        public string GeneralCondition { get; set; }
        public int ReserveValue { get; set; }
        public string DisposalcommitteeRecommend { get; set; }
        public string DisposalType { get; set; }
        public string DisposalMethods { get; set; }
    }

}

