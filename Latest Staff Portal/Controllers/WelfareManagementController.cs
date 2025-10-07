using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
   /* [CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]*/
    public class WelfareManagementController : Controller
    {
        // GET: WelfareManagement
        public ActionResult CarLoanApplicationsList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult CarLoanApplicationsListPartialView()
        {
            var StaffNo = Session["Username"].ToString();

            var loanApplications = new List<LoanApplicationsLIst>();

            var lnApplications = "CarLoanApplications?$filter=Employee2 eq '" + StaffNo + "' &$format=json";

            var httpResponseLvTypes = Credentials.GetOdataData(lnApplications);
            using (var streamReader = new StreamReader(httpResponseLvTypes.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var loan = new LoanApplicationsLIst();
                    loan.LoanNo = (string)config["Loan_No"];
                    loan.LoanProductType = (string)config["Loan_Product_Type"];
                    loan.Employee2 = (string)config["Employee2"];
                    loan.EmployeeName = (string)config["Employee_Name"];
                    loan.Description = (string)config["Description"];
                    loan.ApplicationDate = DateTime.Parse((string)config["Application_Date"]);
                    loan.AmountRequested = (decimal)config["Amount_Requested"];
                    loan.ApprovedAmount = (decimal)config["Approved_Amount"];
                    loan.IssuedDate = DateTime.Parse((string)config["Issued_Date"]);
                    loan.Instalment = (int)config["Instalment"];
                    loan.Repayment = (decimal)config["Repayment"];
                    loan.FlatRatePrincipal = (decimal)config["Flat_Rate_Principal"];
                    loan.FlatRateInterest = (decimal)config["Flat_Rate_Interest"];
                    loan.InterestRate = (decimal)config["Interest_Rate"];
                    loan.InterestCalculationMethod = (string)config["Interest_Calculation_Method"];
                    loanApplications.Add(loan);
                }
            }


            return PartialView("~/Views/WelfareManagement/PartialViews/LoanApplicationsListPartialView.cshtml",
                loanApplications);
        }

        public PartialViewResult NewCarLoanApplication()
        {
            var StaffNo = Session["Username"].ToString();
            var loanProductType = "CAR LOAN";


            var loanApplication = new LoanApplicationCard();

            #region Employee

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponseCampus = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponseCampus.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    loanApplication.EmployeeNo = (string)config["No"];
                    loanApplication.EmployeeName = (string)config["First_Name"] + " " + (string)config["Middle_Name"] +
                                                   " " + (string)config["Last_Name"];
                }
            }

            #endregion

            #region carLoanDetails

            var carLoan = "Loans?$filter=Code eq '" + loanProductType + "'&$format=json";

            var httpResponseCarLoan = Credentials.GetOdataData(carLoan);
            using (var streamReader = new StreamReader(httpResponseCarLoan.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    loanApplication.LoanProductType = (string)config["Code"];
                    loanApplication.Description = (string)config["Description"];
                }
            }

            #endregion


            return PartialView("~/Views/WelfareManagement/PartialViews/NewLoanApplication.cshtml", loanApplication);
        }

        public ActionResult MortgageLoanApplicationsList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult MortgageLoanApplicationsListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var loanProductType = "MORTGAGE LOAN";

            var loanApplications = new List<LoanApplicationsLIst>();

            var lnApplications = "LoanApplications?$filter=Employee2 eq '" + StaffNo + "' and Loan_Product_Type eq '" +
                                 loanProductType + "'&$format=json";

            var httpResponseLvTypes = Credentials.GetOdataData(lnApplications);
            using (var streamReader = new StreamReader(httpResponseLvTypes.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var loan = new LoanApplicationsLIst();
                    loan.LoanNo = (string)config["Loan_No"];
                    loan.LoanProductType = (string)config["Loan_Product_Type"];
                    loan.Employee2 = (string)config["Employee2"];
                    loan.EmployeeName = (string)config["Employee_Name"];
                    loan.Description = (string)config["Description"];
                    loan.ApplicationDate = DateTime.Parse((string)config["Application_Date"]);
                    loan.AmountRequested = (decimal)config["Amount_Requested"];
                    loan.ApprovedAmount = (decimal)config["Approved_Amount"];
                    loan.IssuedDate = DateTime.Parse((string)config["Issued_Date"]);
                    loan.Instalment = (int)config["Instalment"];
                    loan.Repayment = (decimal)config["Repayment"];
                    loan.FlatRatePrincipal = (decimal)config["Flat_Rate_Principal"];
                    loan.FlatRateInterest = (decimal)config["Flat_Rate_Interest"];
                    loan.InterestRate = (decimal)config["Interest_Rate"];
                    loan.InterestCalculationMethod = (string)config["Interest_Calculation_Method"];
                    loanApplications.Add(loan);
                }
            }


            return PartialView("~/Views/WelfareManagement/PartialViews/LoanApplicationsListPartialView.cshtml",
                loanApplications);
        }

        public PartialViewResult NewMortgageLoanApplication()
        {
            var StaffNo = Session["Username"].ToString();
            var loanProductType = "MORTGAGE LOAN";


            var loanApplication = new LoanApplicationCard();

            #region Employee

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponseCampus = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponseCampus.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    loanApplication.EmployeeNo = (string)config["No"];
                    loanApplication.EmployeeName = (string)config["First_Name"] + " " + (string)config["Middle_Name"] +
                                                   " " + (string)config["Last_Name"];
                }
            }

            #endregion

            #region carLoanDetails

            var carLoan = "Loans?$filter=Code eq '" + loanProductType + "'&$format=json";

            var httpResponseCarLoan = Credentials.GetOdataData(carLoan);
            using (var streamReader = new StreamReader(httpResponseCarLoan.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    loanApplication.LoanProductType = (string)config["Code"];
                    loanApplication.Description = (string)config["Description"];
                }
            }

            #endregion


            return PartialView("~/Views/WelfareManagement/PartialViews/NewLoanApplication.cshtml", loanApplication);
        }

        public ActionResult SalaryAdvanceLoanApplicationsList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult SalaryAdvanceApplicationsListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var loanProductType = "ADVANCE";

            var loanApplications = new List<LoanApplicationsLIst>();

            var lnApplications = "LoanApplications?$filter=Employee2 eq '" + StaffNo + "' and Loan_Product_Type eq '" +
                                 loanProductType + "'&$format=json";

            var httpResponseLvTypes = Credentials.GetOdataData(lnApplications);
            using (var streamReader = new StreamReader(httpResponseLvTypes.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var loan = new LoanApplicationsLIst();
                    loan.LoanNo = (string)config["Loan_No"];
                    loan.LoanProductType = (string)config["Loan_Product_Type"];
                    loan.Employee2 = (string)config["Employee2"];
                    loan.EmployeeName = (string)config["Employee_Name"];
                    loan.Description = (string)config["Description"];
                    loan.ApplicationDate = DateTime.Parse((string)config["Application_Date"]);
                    loan.AmountRequested = (decimal)config["Amount_Requested"];
                    loan.ApprovedAmount = (decimal)config["Approved_Amount"];
                    loan.IssuedDate = DateTime.Parse((string)config["Issued_Date"]);
                    loan.Instalment = (int)config["Instalment"];
                    loan.Repayment = (decimal)config["Repayment"];
                    loan.FlatRatePrincipal = (decimal)config["Flat_Rate_Principal"];
                    loan.FlatRateInterest = (decimal)config["Flat_Rate_Interest"];
                    loan.InterestRate = (decimal)config["Interest_Rate"];
                    loan.InterestCalculationMethod = (string)config["Interest_Calculation_Method"];
                    loanApplications.Add(loan);
                }
            }


            return PartialView("~/Views/WelfareManagement/PartialViews/LoanApplicationsListPartialView.cshtml",
                loanApplications);
        }

        public PartialViewResult SalaryAdvanceLoanApplication()
        {
            var StaffNo = Session["Username"].ToString();
            var loanProductType = "ADVANCE";


            var loanApplication = new LoanApplicationCard();

            #region Employee

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponseCampus = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponseCampus.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    loanApplication.EmployeeNo = (string)config["No"];
                    loanApplication.EmployeeName = (string)config["FullName"];
                }
            }

            #endregion

            #region carLoanDetails

            var carLoan = "Loans?$filter=Code eq '" + loanProductType + "'&$format=json";

            var httpResponseCarLoan = Credentials.GetOdataData(carLoan);
            using (var streamReader = new StreamReader(httpResponseCarLoan.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    loanApplication.LoanProductType = (string)config["Code"];
                    loanApplication.Description = (string)config["Description"];
                }
            }

            decimal[] Pay = { 0, 0 };
            try
            {
                Pay = Credentials.ObjNav.GetPreviousPay(StaffNo);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            var pay1 = Convert.ToString(Pay[0]);
            loanApplication.NetPay = pay1;
            loanApplication.BasicPay = Convert.ToString(Pay[1]);

            #endregion


            return PartialView("~/Views/WelfareManagement/PartialViews/NewLoanApplication.cshtml", loanApplication);
        }

        public ActionResult LoanApplicationCardDocument(string loanNo)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");

            var loanApplicationCard = new LoanApplicationCard();


            //string loanApplicationsCard = "LoanApplicationsCard?$filter=Loan_No eq '" + loanNo + "'&$format=json";
            //HttpWebResponse httpResponse = Credentials.GetOdataData(loanApplicationsCard);
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    var result = streamReader.ReadToEnd();

            //    var details = JObject.Parse(result);
            //    foreach (JObject config in details["value"])
            //    {
            var loanApplicationsCard = "CarLoanApplicationsCard?$filter=Loan_No eq '" + loanNo + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(loanApplicationsCard);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    loanApplicationCard.LoanNo = (string)config["Loan_No"];
                    loanApplicationCard.LoanProductType = (string)config["Loan_Product_Type"];
                    loanApplicationCard.LoanCategory = (string)config["Loan_Category"];
                    loanApplicationCard.Description = (string)config["Description"];
                    loanApplicationCard.EmployeeNo = (string)config["Employee_No"];
                    loanApplicationCard.EmployeeName = (string)config["Employee_Name"];
                    loanApplicationCard.ApplicationDate = (string)config["Application_Date"];
                    loanApplicationCard.AmountRequested = (decimal)config["Amount_Requested"];
                    loanApplicationCard.Reason = (string)config["Reason"];
                    loanApplicationCard.ApprovalCommittee = (string)config["Approval_Committee"];
                    loanApplicationCard.LoanStatus = (string)config["Loan_Status"];
                    loanApplicationCard.IssuedDate = DateTime.Parse((string)config["Issued_Date"]);
                    loanApplicationCard.ApprovedAmount = (decimal)config["Approved_Amount"];
                    //loanApplicationCard.Instalment = (decimal)config["Instalment"];
                    loanApplicationCard.Repayment = (decimal)config["Repayment"];
                    loanApplicationCard.FlatRatePrincipal = (decimal)config["Flat_Rate_Principal"];
                    loanApplicationCard.FlatRateInterest = (decimal)config["Flat_Rate_Interest"];
                    loanApplicationCard.InterestRate = (decimal)config["Interest_Rate"];
                    loanApplicationCard.InterestCalculationMethod = (string)config["Interest_Calculation_Method"];
                    loanApplicationCard.PayrollGroup = (string)config["Payroll_Group"];
                    loanApplicationCard.OpeningLoan = (bool)config["Opening_Loan"];
                    loanApplicationCard.TotalRepayment = (decimal)config["Total_Repayment"];
                    //loanApplicationCard.PeriodRepayment = (decimal)config["Period_Repayment"];
                    loanApplicationCard.InterestAmount = (decimal)config["Interest_Amount"];
                    loanApplicationCard.ExternalDocumentNo = (string)config["External_Document_No"];
                    //loanApplicationCard.Receipts = (decimal)config["Receipts"];
                    loanApplicationCard.HELBNo = (string)config["HELB_No"];
                    loanApplicationCard.UniversityName = (string)config["University_Name"];
                    loanApplicationCard.StopLoan = (bool)config["Stop_Loan"];
                    loanApplicationCard.RefinancedFromLoan = (string)config["Refinanced_From_Loan"];
                    loanApplicationCard.DateFilter = (string)config["Date_filter"];
                }
            }

            return View(loanApplicationCard);
        }

        public ActionResult MedicalCardReplacement()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult MedicalCardReplacementPartialView()
        {
            var StaffNo = Session["Username"].ToString();

            var medicalCards = new List<MedicalCardReplacement>();

            var lnApplications = "MedicalCardReplacementList?$filter=Employee_No eq '" + StaffNo + "'&$format=json";

            var httpResponseLvTypes = Credentials.GetOdataData(lnApplications);
            using (var streamReader = new StreamReader(httpResponseLvTypes.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var medical = new MedicalCardReplacement();
                    medical.DocumentNo = (string)config["Document_No"];
                    medical.EmployeeNo = (string)config["Employee_No"];
                    medical.EmployeeName = (string)config["Employee_Name"];
                    medical.Description = (string)config["Description"];
                    medical.DocumentDate = (string)config["Document_Date"];
                    medical.DocumentStatus = (string)config["Document_Status"];
                    medical.Status = (string)config["Status"];
                    medical.RequestorID = (string)config["Requestor_ID"];
                    medicalCards.Add(medical);
                }
            }

            return PartialView("~/Views/WelfareManagement/PartialViews/MedicalCardReplacementPartialView.cshtml",
                medicalCards);
        }

        public PartialViewResult MedicalCardRequirements()
        {
            var StaffNo = Session["Username"].ToString();


            var medicalCard = new MedicalCardReplacement();

            #region Employee

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponseCampus = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponseCampus.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    medicalCard.EmployeeNo = (string)config["No"];
                    medicalCard.EmployeeName = (string)config["First_Name"] + " " + (string)config["Middle_Name"] +
                                               " " + (string)config["Last_Name"];
                }
            }

            #endregion


            return PartialView("~/Views/WelfareManagement/PartialViews/MedicalCardRequirements.cshtml", medicalCard);
        }

        public PartialViewResult NewMedicalCard()
        {
            var StaffNo = Session["Username"].ToString();


            var medicalCard = new MedicalCardReplacement();

            #region Employee

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponseCampus = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponseCampus.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    medicalCard.EmployeeNo = (string)config["No"];
                    medicalCard.EmployeeName = (string)config["First_Name"] + " " + (string)config["Middle_Name"] +
                                               " " + (string)config["Last_Name"];
                }
            }

            #endregion


            #region EmployeeDependants

            var employeeDependants = new List<DropdownList>();
            var dependants = "EmployeeDependants?$filter=Employee_Code eq '" + StaffNo + "'&$format=json";

            var httpResponseDependants = Credentials.GetOdataData(dependants);

            using (var streamReader = new StreamReader(httpResponseDependants.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var ddl = new DropdownList();
                    ddl.Value = (string)config["Line_No"];
                    ddl.Text = (string)config["Line_No"] + "-" + (string)config["SurName"] + " " +
                               (string)config["OtherNames"];
                    employeeDependants.Add(ddl);
                }
            }

            #endregion

            medicalCard.ListOfDependantNo = employeeDependants.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

            return PartialView("~/Views/WelfareManagement/PartialViews/NewMedicalCardView.cshtml", medicalCard);
        }

        public ActionResult MedicalCardReplacementDocument(string documentNo)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");

            var medicalCardReplacement = new MedicalCardReplacement();


            var medicalCard = "MedicalCardReplacement?$filter=Document_No eq '" + documentNo + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(medicalCard);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    medicalCardReplacement.DocumentNo = (string)config["Document_No"];
                    medicalCardReplacement.DocumentDate = (string)config["Document_Date"];
                    medicalCardReplacement.EmployeeNo = (string)config["Employee_No"];
                    medicalCardReplacement.EmployeeName = (string)config["Employee_Name"];
                    medicalCardReplacement.Description = (string)config["Description"];
                    medicalCardReplacement.DependantNo = (string)config["Dependant_No"];
                    medicalCardReplacement.DependantName = (string)config["Dependant_Name"];
                    medicalCardReplacement.CardNo = (string)config["Card_No"];
                }
            }

            return View(medicalCardReplacement);
        }

        public ActionResult DependantChangeRequests()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult DependantsChangeRequestPartialView()
        {
            var StaffNo = Session["Username"].ToString();

            var dependantChangeRequests = new List<DependantChangeRequests>();

            var dependentsChange = "DependantsChangeRequests?$filter=EmployeeNo eq '" + StaffNo + "'&$format=json";


            var httpResponseLvTypes = Credentials.GetOdataData(dependentsChange);
            using (var streamReader = new StreamReader(httpResponseLvTypes.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var dependantChange = new DependantChangeRequests();
                    dependantChange.No = (string)config["No"];
                    dependantChange.RequestorID = (string)config["RequestorID"];
                    dependantChange.EmployeeNo = (string)config["EmployeeNo"];
                    dependantChange.EmployeeName = (string)config["EmployeeName"];
                    dependantChange.Description = (string)config["Description"];
                    dependantChange.DocumentDate = (string)config["DocumentDate"];
                    dependantChange.TimeCreated = (string)config["TimeCreated"];
                    dependantChange.PostedBy = (string)config["PostedBy"];
                    dependantChange.PostingDate = (string)config["PostingDate"];
                    dependantChange.PostingTime = (string)config["PostingTime"];
                    dependantChange.Status = (string)config["Status"];
                    dependantChange.DocumentStatus = (string)config["DocumentStatus"];
                    dependantChangeRequests.Add(dependantChange);
                }
            }

            return PartialView("~/Views/WelfareManagement/PartialViews/DependantsChangeRequestsListView.cshtml",
                dependantChangeRequests);
        }

        public PartialViewResult NewDependantsChangeRequest()
        {
            var StaffNo = Session["Username"].ToString();


            var changeRequests = new DependantChangeRequests();

            #region Employee

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponseCampus = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponseCampus.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    changeRequests.EmployeeNo = (string)config["No"];
                    changeRequests.EmployeeName = (string)config["First_Name"] + " " + (string)config["Middle_Name"] +
                                                  " " + (string)config["Last_Name"];
                }
            }

            #endregion


            return PartialView("~/Views/WelfareManagement/PartialViews/NewDependantsChangeRequest.cshtml",
                changeRequests);
        }

        public ActionResult DependentsChangeDocumentView(string documentNo)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");

            var dependantChange = new DependantChangeRequests();
            var page = "DependentsChangeRequest?$filter=No eq '" + documentNo + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    dependantChange.No = (string)config["No"];
                    dependantChange.Description = (string)config["Description"];
                    dependantChange.RequestorID = (string)config["RequestorID"];
                    dependantChange.EmployeeNo = (string)config["EmployeeNo"];
                    dependantChange.EmployeeName = (string)config["EmployeeName"];
                    dependantChange.DocumentDate = (string)config["DocumentDate"];
                    dependantChange.ReasonForChange = (string)config["Reason_For_Change"];
                    dependantChange.TimeCreated = (string)config["TimeCreated"];
                    dependantChange.PostedBy = (string)config["PostedBy"];
                    dependantChange.PostingDate = (string)config["PostingDate"];
                    dependantChange.PostingTime = (string)config["PostingTime"];
                    dependantChange.Status = (string)config["Status"];
                    dependantChange.DocumentStatus = (string)config["DocumentStatus"];
                    dependantChange.Posted = (bool)config["Posted"];
                }
            }

            return View(dependantChange);
        }

        public PartialViewResult DependentsList(string documentNo)
        {
            var page = "DependentsList?$filter=Document_No eq '" + documentNo + "'&$format=json";

            var dependentLists = new List<DependentList>();

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            foreach (JObject config in details["value"])
            {
                var dependent = new DependentList();
                dependent.Employee_Code = (string)config["Employee_Code"];
                dependent.Line_No = (int)config["Line_No"];
                dependent.Document_No = (string)config["Document_No"];
                dependent.Relationship = (string)config["Relationship"];
                dependent.SurName = (string)config["SurName"];
                dependent.Other_Names = (string)config["Other_Names"];
                dependent.Date_Of_Birth = (DateTime)config["Date_Of_Birth"];
                dependent.Type = (string)config["Type"];
                dependent.ID_No_Passport_No = (string)config["ID_No_Passport_No"];
                dependent.Comment = (bool)config["Comment"];
                dependentLists.Add(dependent);
            }

            return PartialView("~/Views/WelfareManagement/PartialViews/DependantsList.cshtml", dependentLists);
        }

        public PartialViewResult NewDependent(string relationship)
        {
            var StaffNo = Session["Username"].ToString();
            var dependent = new DependentList();

            #region Fleet Requisition List

            var relationships = new List<DropdownList>();
            var rshps = "HrLookUp?$format=json";

            var httpResponse = Credentials.GetOdataData(rshps);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var ddl = new DropdownList();
                    ddl.Value = (string)config["Code"];
                    ddl.Text = (string)config["Code"] + "  " + (string)config["Type"];
                    relationships.Add(ddl);
                }
            }

            #endregion

            dependent.Employee_Code = StaffNo;
            dependent.Relationship = relationship;

            return PartialView("~/Views/WelfareManagement/PartialViews/NewDependent.cshtml", dependent);
        }

        public PartialViewResult DependentRequirements()
        {
            var StaffNo = Session["Username"].ToString();
            var dependent = new DependentList();

            #region Fleet Requisition List

            var relationships = new List<DropdownList>();
            var type = "Next of Kin";
            var rshps = "HrLookUp?$filter=Type eq '" + type + "'&$format=json";


            var httpResponse = Credentials.GetOdataData(rshps);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var ddl = new DropdownList();
                    ddl.Value = (string)config["Code"];
                    ddl.Text = (string)config["Code"];
                    relationships.Add(ddl);
                }
            }

            #endregion

            dependent.Employee_Code = StaffNo;
            dependent.ListOfRelationships = relationships.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

            return PartialView("~/Views/WelfareManagement/PartialViews/DependentRequirement.cshtml", dependent);
        }


        public JsonResult SubmitLoansApplication(LoanApplicationCard NewApp, string fileName, string FileType,
            string FileContent)
        {
            try
            {
                var employee = Session["EmployeeData"] as EmployeeView;

                var userId = Session["Username"].ToString();
                var loanProductType = NewApp.LoanProductType;
                var Installment = 12;
                var successVal = false;
                var msg = "";
                if (!string.IsNullOrEmpty(NewApp.Instalment)) Installment = Convert.ToInt32(NewApp.Instalment);


                // DateTime appDate = DateTime.ParseExact(ApplicationDate.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var DocNo = Credentials.ObjNav.createLoansApplication(DateTime.Now, userId, NewApp.LoanProductType,
                    NewApp.AmountRequested, NewApp.Reason, Convert.ToInt32(Installment));
                msg = "A Loan Application, Document Number " + DocNo +
                      " has been submitted successfully and sent for approval";
                successVal = true;

                if (FileContent != "" && fileName != "")
                {
                    var filePath = Server.MapPath("~/Uploads/" + fileName);
                    var tableid = 69011;


                    var uploaded = UploadDocuments.UploadEDMSDocumentAttachment(FileContent, fileName, "HRMD", DocNo,
                        "Welfare", "", tableid, "");

                    if (uploaded)
                    {
                        msg = "A Loan Application, Document Number " + DocNo +
                              " has been submitted successfully and sent for approval";
                        successVal = true;
                    }
                    //else
                    //{
                    //    msg = "There was an error uploading the Loan application attachment, Document Number " + DocNo + ". Please try again.";
                    //    successVal = false;
                    //}
                }

                if (successVal) SendLoanAppForApproval(DocNo);

                var Redirect = "/WelfareManagement/LoanApplicationCardDocument?loanNo=" + DocNo;

                return Json(new { message = Redirect, success = successVal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendLoanAppForApproval(string DocNo)
        {
            try
            {
                var userId = Session["UserID"].ToString();


                // DateTime effectiveDate = DateTime.ParseExact(NewApp.Effective_Date.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var result = Credentials.ObjNav.SendLoanAppForApproval(DocNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(69011, DocNo, userId);

                if (result.Equals("Success"))
                {
                    var Redirect = "/WelfareManagement/LoanApplicationCardDocument?loanNo=" + DocNo;

                    return Json(new { message = $"{DocNo} Sent for Approval", Redirect, success = true },
                        JsonRequestBehavior.AllowGet);
                }

                return Json(new { message = "Failed. Try Again Later!!!.", success = false },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SubmitNewMedicalCard(DependantChangeRequests NewApp)
        {
            try
            {
                var userId = Session["UserID"].ToString();

                var doctType = 0;

                var appDate = DateTime.ParseExact(NewApp.DocumentDate.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);


                var DocNo = Credentials.ObjNav.createDependantsChange(appDate, userId, NewApp.RequestorID, "", "",
                    NewApp.Description, doctType);

                var Redirect = "/WelfareManagement/DependentsChangeDocumentView?documentNo=" + DocNo;

                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult HrMedicalClaimListView()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult HrMedicalClaimListPartialView()
        {
            try
            {
                var userId = Session["Username"].ToString();

                var page = "HrMedicalClaimList?$filter=Member_No eq '" + userId + "'&$format=json";

                var medicalClaims = new List<MedicalClaim>();

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var claim = new MedicalClaim();
                    claim.Claim_No = (string)config["Claim_No"];
                    claim.Member_No = (string)config["Member_No"];
                    claim.Claim_Type = (string)config["Claim_Type"];
                    claim.Claim_Date = (string)config["Claim_Date"];
                    claim.Dependants = Convert.ToInt32(config["Dependants"]);
                    claim.Patient_Name = (string)config["Patient_Name"];
                    claim.Document_Ref = (string)config["Document_Ref"];
                    claim.Date_of_Service = (string)config["Date_of_Service"];
                    claim.Attended_By = (string)config["Attended_By"];
                    claim.Amount_Charged = Convert.ToDecimal(config["Amount_Charged"]);
                    claim.Comments = (string)config["Comments"];

                    medicalClaims.Add(claim);
                }

                return PartialView("~/Views/WelfareManagement/PartialViews/HrMedicalClaimListPartialView.cshtml",
                    medicalClaims);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


        public JsonResult SubmitDependantsChange(DependantChangeRequests NewApp)
        {
            try
            {
                var userId = Session["UserID"].ToString();
                var staffNo = Session["Username"].ToString();


                var doctType = 0;

                var appDate = DateTime.ParseExact(NewApp.DocumentDate.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);


                var DocNo = Credentials.ObjNav.createDependantsChange(appDate, staffNo, userId, "", "",
                    NewApp.Description, doctType);

                var Redirect = "/WelfareManagement/DependentsChangeDocumentView?documentNo=" + DocNo;

                if (!string.IsNullOrEmpty(DocNo))
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
                return Json(new { message = "Failed to submit request. Please try again later.", success = false });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult IncidenceReporting()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult HrWelfareIncidenceList()
        {
            var staffNo = Session["UserName"].ToString();
            var page = "HrWelfareIncidenceCard?$filter=Affected_Employee eq '" + staffNo + "'&$format=json";

            var hrWelfareIncidences = new List<HrWelfareIncidenceCard>();

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            foreach (JObject config in details["value"])
            {
                var hrWelfareIncidence = new HrWelfareIncidenceCard();
                hrWelfareIncidence.Incidence_No = (string)config["Incidence_No"];
                hrWelfareIncidence.Incidence_Date = (string)config["Incidence_Date"];
                hrWelfareIncidence.Incidence_Time = (string)config["Incidence_Time"];
                hrWelfareIncidence.Incidence_Type = (string)config["Incidence_Type"];
                hrWelfareIncidence.Incidence = (string)config["Incidence"];
                hrWelfareIncidence.Affected_Employee = (string)config["Affected_Employee"];
                hrWelfareIncidence.Employee_Name = (string)config["Employee_Name"];
                hrWelfareIncidence.Duty_Station = (string)config["Duty_Station"];
                hrWelfareIncidence.Action_Taken = (string)config["Action_Taken"];
                hrWelfareIncidence.Incidence_Status = (string)config["Incidence_Status"];

                hrWelfareIncidences.Add(hrWelfareIncidence);
            }

            return PartialView("~/Views/WelfareManagement/PartialViews/HrWelfareIncidenceList.cshtml",
                hrWelfareIncidences);
        }

        public PartialViewResult NewIncident()
        {
            var StaffNo = Session["Username"].ToString();

            var hrWelfareIncidence = new HrWelfareIncidenceCard();

            #region Employee

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";
            var httpResponseCampus = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponseCampus.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                    hrWelfareIncidence.Employee_Name = (string)config["First_Name"] + " " +
                                                       (string)config["Middle_Name"] + " " +
                                                       (string)config["Last_Name"];
            }

            #endregion

            #region Incident Type

            var incidences = new List<DropdownList>();
            var incidentTypes = "IncidentCategoryList?&$format=json";
            var httpResponsetypes = Credentials.GetOdataData(incidentTypes);
            using (var streamReader = new StreamReader(httpResponsetypes.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                if (details["value"].Count() > 0)
                    foreach (JObject config in details["value"])
                    {
                        var dropdownList = new DropdownList();
                        dropdownList.Value = (string)config["Code"];
                        dropdownList.Text = (string)config["Code"] + "-" + (string)config["Description"];
                        incidences.Add(dropdownList);
                    }
            }

            #endregion

            #region DutyStation

            var dutyStations = new List<DropdownList>();
            var pageDutyStation = "DutyStation?$format=json";
            var httpResponseDutyStation = Credentials.GetOdataData(pageDutyStation);
            using (var streamReader = new StreamReader(httpResponseDutyStation.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var list = new DropdownList();
                    list.Value = (string)config["Code"];
                    list.Text = (string)config["Code"] + "-" + (string)config["Description"];
                    dutyStations.Add(list);
                }
            }

            #endregion

            hrWelfareIncidence.Affected_Employee = StaffNo;

            hrWelfareIncidence.ListOfDutyStations = dutyStations.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

            hrWelfareIncidence.ListOfIncidences = incidences.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();

            return PartialView("~/Views/WelfareManagement/PartialViews/NewIncident.cshtml", hrWelfareIncidence);
        }

        public ActionResult SubmitIncident(HrWelfareIncidenceCard hrWelfareIncidence)
        {
            var successVal = false;
            try
            {
                if (Session["UserID"] == null || Session["Username"] == null) return RedirectToAction("Login", "Login");

                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();

                var date = DateTime.ParseExact(hrWelfareIncidence.Incidence_Date, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
                var time = DateTime.ParseExact(hrWelfareIncidence.Incidence_Time, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                var result = "";
               /* result = Credentials.ObjNav.createWelfareIncident(StaffNo, hrWelfareIncidence.Incidence, date, time,
                    hrWelfareIncidence.Action_Taken, hrWelfareIncidence.Duty_Station);*/
                if (result.Equals("Success"))
                {
                    var Redirect = "/WelfareManagement/IncidenceReporting";

                    Session["SuccessMsg"] = "Incident Reported Successfully.";
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { message = "Document not created. Please try again later...", success = false },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (successVal) Session["ErrorMsg"] = ex.Message.Replace("'", "");
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SubmitNewCardRequest(MedicalCardReplacement request)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();

                if (Session["UserID"] == null || Session["Username"] == null)
                    return Json(new { message = "User is not authenticated. Please log in.", success = false });
                var date = DateTime.ParseExact(request.DocumentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var result = "";

                /*   result = Credentials.ObjNav.CreateMedicalCard(StaffNo, request.CardNo, date, request.DependantNo,
                       request.Description);
   */

                if (result.Equals("Success"))
                    return Json(new { message = "New card request submitted successfully.", success = true });
                return Json(new { message = "Failed to submit request. Please try again later.", success = false });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false });
            }
        }
    }
}