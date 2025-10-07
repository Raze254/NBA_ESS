using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class PurchaseRequisitions
    {

        public string Document_Type { get; set; }
        public string No { get; set; }
        public string PRN_Type { get; set; }
        public string Document_Date { get; set; }
        public string Location_Code { get; set; }
        public string Requisition_Product_Group { get; set; }
        public string Requester_ID { get; set; }
        public string Request_By_No { get; set; }
        public string Request_By_Name { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Geographical_Location_Name { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Department_Name { get; set; }

        public string Project_Name { get; set; }
        public string Procurement_Plan_ID { get; set; }
        public string Purchaser_Code { get; set; }
        public string Assigned_Officer { get; set; }
        public string Job { get; set; }
        public string Job_Task_No { get; set; }
        public string PP_Planning_Category { get; set; }
        public string Description { get; set; }
        public int PP_Total_Budget { get; set; }
        public int PP_Total_Actual_Costs { get; set; }
        public string PP_Solicitation_Type { get; set; }
        public string Other_Procurement_Methods { get; set; }
        public string PP_Bid_Selection_Method { get; set; }
        public string PP_Procurement_Method { get; set; }
        public string PP_Invitation_Notice_Type { get; set; }
        public string PP_Preference_Reservation_Code { get; set; }
        public string PRN_Conversion_Procedure { get; set; }
        public bool Ordered_PRN { get; set; }
        public string Linked_IFS_No { get; set; }
        public string Linked_LPO_No { get; set; }
        public bool Consolidate_PRN { get; set; }
        public string Consolidate_to_IFS_No { get; set; }
        public string Status { get; set; }

        public List<SelectListItem> ListOfLocation_Code { get; set; }
        public List<SelectListItem> ListOfRegions { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfProcurementPlans { get; set; }
        public List<SelectListItem> ListOfAssigned_Officer { get; set; }
        public List<SelectListItem> ListOfPP_Planning_Category { get; set; }
        public List<SelectListItem> ListOfPP_Preference_Reservation_Code { get; set; }
        public List<SelectListItem> ListOfPP_Solicitation_Type { get; set; }

        public List<SelectListItem> ListOfBudgets { get; set; }
    }


    public class PurchaseRequisitionLines
    {
        public string DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public bool LineSelected { get; set; }
        public string Type { get; set; }
        public string ProcurementPlanID { get; set; }
        public string ProcurementPlanCategory { get; set; }
        public string TechnicalSpecifications { get; set; }
        public string Budget { get; set; }
        public string BudgetLine { get; set; }
        public string Status { get; set; }
        public int ProcurementPlanEntryNo { get; set; }
        public string ItemDescription { get; set; }
        public string PPSolicitationType { get; set; }
        public string PPProcurementMethod { get; set; }
        public string OtherProcurementMethods { get; set; }
        public string PPPreferenceReservationCode { get; set; }
        public string ItemCategoryCode { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public int Quantity { get; set; }
        public int DirectUnitCost { get; set; }
        public int Amount { get; set; }
        public int AmountIncludingVAT { get; set; }
        public string CurrencyCode { get; set; }
        public string LocationCode { get; set; }
        public int UnitCostLCY { get; set; }
        public string ContractNoToPay { get; set; }
        public bool LPOCreated { get; set; }
        public List<SelectListItem> ListOfProcurementPlans { get; set; }
        public List<SelectListItem> ListOfBudget { get; set; }
        public List<SelectListItem> ListOfBudget_Line { get; set; }
        public List<SelectListItem> ListOfProcurement_Plan_Entry { get; set; }
        public List<SelectListItem> ListOfPP_Preference_Reservation_Code { get; set; }
        public List<SelectListItem> ListOfPP_Planning_Category { get; set; }
        public List<SelectListItem> ListOfItems { get; set; }
        public List<SelectListItem> ListOfItemsUOM { get; set; }
        public List<SelectListItem> ListOfLocations { get; set; }
    }
    public class PurchaseRequisitionLinesList
    {
        public string Status { get; set; }
        public string Procurement_Plan_ID { get; set; }

        public int Procurement_Plan_Category { get; set; }
        public List<PurchaseRequisitionLines> ListOfPurchaseRequisitionLines { get; set; }
    }
    public class ExpensePRNLine
    {
        public string DocumentNo { get; set; }
        public int LineNo { get; set; }
        public int ProcPlanEntryNo { get; set; }
        public string Item { get; set; }
        public string EntryNo { get; set; }
        public List<SelectListItem> ListOfProcurementPlanNos { get; set; }
        public List<SelectListItem> ListOfProcurementItems { get; set; }
        public string ProcType { get; set; }
        public int pType { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public string ExpenseDescription { get; set; }
        public string GLAccount { get; set; }
        public string Status { get; set; }
        public string RecalledBy { get; set; }
        public DateTime RecalledOn { get; set; }
        public string SourceLineNo { get; set; }
    }

    public class ExpensePRNLineList
    {
        public string Status { get; set; }
        public List<ExpensePRNLine> ListOfExpensePRNLine { get; set; }
    }
}