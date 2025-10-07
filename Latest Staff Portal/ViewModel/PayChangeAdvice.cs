using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Latest_Staff_Portal.ViewModel
{
    //public class PCAHeader
    //{
    //    public string PCANo { get; set; }
    //    public string Date { get; set; }
    //    public string Remarks { get; set; }
    //    public string Directorate { get; set; }
    //    public string Department { get; set; }
    //    public string RespC { get; set; }
    //    public string EmployeeNo { get; set; }
    //    public string EmployeeName { get; set; }
    //    public string BasicPay { get; set; }
    //    public bool NSSF  { get; set; }
    //    public bool NHIF { get; set; }
    //    public bool PAYE { get; set; }
    //    public string Comments { get; set; }
    //    public string PayrollPeriod { get; set; }
    //    public string TransferAppointmentNo { get; set; }        
    //    public string Status { get; set; }
    //    public string Dim1 { get; set; }
    //    public string Dim2 { get; set; }
    //    public string Dim3 { get; set; }
    //    public string Dim4 { get; set; }
    //    public string Dim5 { get; set; }
    //    public List<SelectListItem> ListOfDim1 { get; set; }
    //    public List<SelectListItem> ListOfDim2 { get; set; }
    //    public List<SelectListItem> ListOfDim3 { get; set; }
    //    public List<SelectListItem> ListOfDim4 { get; set; }
    //    public List<SelectListItem> ListOfDim5 { get; set; }
    //    public List<SelectListItem> ListOfResponsibility { get; set; }
    //}


    public class PayChangeAdvice
    {
        public string ChangeAdviceSerialNo { get; set; }
        public string Document_Type { get; set; }
        public bool Change_Bank_Details { get; set; }
        public string EmployeeCode { get; set; }
        public List<SelectListItem> EmployeesList { get; set; }
        public string EmployeeName { get; set; }
        public string Current_Job_Grade { get; set; }
        public string Salary_Scale { get; set; }
        public string Present { get; set; }
        public string Designation { get; set; }
        public string BasicPay { get; set; }
        public bool PaysNSSF { get; set; }
        public bool PaysNHIF { get; set; }
        public bool PaysPAYE { get; set; }
        public bool Pays_NITA { get; set; }
        public string Effective_Date { get; set; }
        public string Comments { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime PayrollPeriod { get; set; }
        public List<SelectListItem> PayrollPrds { get; set; }
        public string Implementing_Unit { get; set; }
        public string Duty_Station { get; set; }
        public string Source_Document { get; set; }
        public string Document_No { get; set; }
        public string Employee_Status { get; set; }
        public string Employee_Status_II { get; set; }
        public string Bank_Account_Number { get; set; }
        public string Employee_Bank { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_Branch { get; set; }
        public string Bank_Branch_Name { get; set; }
        public bool Effected { get; set; }
        public string PAyrollCode { get; set; }
        public string Final_Approver_Status { get; set; }
        public int Open_Approver_Count { get; set; }
        public int Final_Approver_Seq_No { get; set; }
        public DateTime Posted_Date { get; set; }
        public string User_ID { get; set; }
    }

    public class PCALines
    {
        public string ChangeAdviceSerialNo { get; set; }
        public string EmployeeCode { get; set; }
        public string TransactionCode { get; set; }
        public List<SelectListItem> ListOfTransactionCodes { get; set; }
        public DateTime PayrollPeriod { get; set; }
        public string DocumentType { get; set; }
        public string TransactionName { get; set; }
        public string ChangeType { get; set; }
        public int Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Duration { get; set; }
        public double Rate { get; set; }
        public double Kilometers { get; set; }
        public int NoOfDays { get; set; }
        public double Amount { get; set; }
        public double Payable { get; set; }
        public decimal Paid { get; set; }
        public double Difference { get; set; }
        public double SubTotal { get; set; }
        public double Balance { get; set; }
        public string Comments { get; set; }
        public double EmployerAmount { get; set; }
        public string GlobalDimension1Code { get; set; }
        public string GlobalDimension2Code { get; set; }
        public string ShortcutDimension3Code { get; set; }
        public string ShortcutDimension4Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}