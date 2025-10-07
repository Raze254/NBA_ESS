using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    public class WorkShopAdvanceLiquidation
    {
    }

    public class WorkShopAdvanceLiquidationList
    {
        public string No { get; set; }
        public string ReqDate { get; set; }
        public string Purpose { get; set; }
        public string Function { get; set; }
        public string Amount { get; set; }
        public string BudgetCeter { get; set; }
        public string ImpDocNo { get; set; }
        public string Status { get; set; }
    }

    public class NewWorkShopAdvanceLiquidation
    {
        public List<SelectListItem> ListOfTravelAdvance { get; set; }
    }

    public class WorkShopAdvanceLiquidationHeader
    {
        public string No { get; set; }
        public string SurrenderDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string ImprestNo { get; set; }
        public string ImpIssueDate { get; set; }
        public string Department { get; set; }
        public string Directorate { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
        public string Dim5 { get; set; }
        public string Dim6 { get; set; }
        public string Dim7 { get; set; }
        public string Dim8 { get; set; }
        public string RespC { get; set; }
        public string Dim1Name { get; set; }
        public string Dim2Name { get; set; }
        public string Dim3Name { get; set; }
        public string Dim4Name { get; set; }
        public string Dim5Name { get; set; }
        public string Dim6Name { get; set; }
        public string Dim7Name { get; set; }
        public string Dim8Name { get; set; }
        public string Status { get; set; }
        public string TotalAmount { get; set; }
        public string ImpPurpose { get; set; }
        public List<SelectListItem> ListOfDim1 { get; set; }
        public List<SelectListItem> ListOfDim2 { get; set; }
        public List<SelectListItem> ListOfDim3 { get; set; }
        public List<SelectListItem> ListOfDim4 { get; set; }
        public List<SelectListItem> ListOfDim5 { get; set; }
        public List<SelectListItem> ListOfDim6 { get; set; }
        public List<SelectListItem> ListOfDim7 { get; set; }
        public List<SelectListItem> ListOfDim8 { get; set; }
        public List<SelectListItem> ListOfDepartment { get; set; }
        public List<SelectListItem> ListOfDirectorate { get; set; }
        public List<SelectListItem> ListOfResponsibility { get; set; }
    }

    public class WorkShopAdvanceLiquidationLines
    {
        public string SurrenderDocNo { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Amount { get; set; }
        public string ActaulSpend { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptAmount { get; set; }
        public string EntryNo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class WorkShopAdvanceLiquidationLinesList
    {
        public string Status { get; set; }
        public List<WorkShopAdvanceLiquidationLines> ListOfWorkShopAdvanceLiquidationLines { get; set; }
    }

    public class WorkShopAdvanceLiquidationLineDetails
    {
        public string Receipt { get; set; }
        public List<SelectListItem> ListOfPostedReceipts { get; set; }
        public string DocNo { get; set; }
        public string AccountNo { get; set; }
        public string Amount { get; set; }
        public string ActaulAmount { get; set; }
        public string EntryNo { get; set; }
    }

    public class WorkShopAdvanceLiquidationDocument
    {
        public WorkShopAdvanceLiquidationHeader DocHeader { get; set; }
        public List<WorkShopAdvanceLiquidationLines> ListOfWorkShopAdvanceLiquidationLines { get; set; }
    }
}