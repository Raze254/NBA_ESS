using iTextSharp.text;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using ZXing;
using static iTextSharp.text.pdf.AcroFields;

namespace Latest_Staff_Portal.Controllers
{

    [CustomeAuthentication]
    [CustomAuthorization(Role = "ALLUSERS")]
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult PurchaseRequisitionList()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult PurchaseRequisitionListPartialView()
        {
            try
            {
                string userId = Session["UserId"].ToString();
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                List<PurchaseRequisitions> purchaseRequisitions = new List<PurchaseRequisitions>();
                string empNo = employeeView.No;



                string page = $"PurchaseRequisition?$filter=Request_By_No eq '{empNo}'&$format=json";


                HttpWebResponse httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    purchaseRequisitions.AddRange(from JObject config in details["value"]
                                                  select new PurchaseRequisitions
                                                  {
                                                      Document_Type = (string)config["Document_Type"],
                                                      No = (string)config["No"],
                                                      PRN_Type = (string)config["PRN_Type"],
                                                      Document_Date = (string)config["Document_Date"],
                                                      Location_Code = (string)config["Location_Code"],
                                                      Requisition_Product_Group = (string)config["Requisition_Product_Group"],
                                                      Requester_ID = (string)config["Requester_ID"],
                                                      Request_By_No = (string)config["Request_By_No"],
                                                      Request_By_Name = (string)config["Request_By_Name"],
                                                      Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                                                      Department_Name = (string)config["Geographical_Location_Name"],
                                                      Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                                                      Project_Name = (string)config["Project_Name"],
                                                      Procurement_Plan_ID = (string)config["Procurement_Plan_ID"],
                                                      Purchaser_Code = (string)config["Purchaser_Code"],
                                                      Assigned_Officer = (string)config["Assigned_Officer"],
                                                      Job = (string)config["Job"],
                                                      Job_Task_No = (string)config["Job_Task_No"],
                                                      PP_Planning_Category = (string)config["PP_Planning_Category"],
                                                      Description = (string)config["Description"],
                                                      PP_Total_Budget = (int?)config["PP_Total_Budget"] ?? 0,
                                                      PP_Total_Actual_Costs = (int?)config["PP_Total_Actual_Costs"] ?? 0,
                                                      PP_Solicitation_Type = (string)config["PP_Solicitation_Type"],
                                                      Other_Procurement_Methods = (string)config["Other_Procurement_Methods"],
                                                      PP_Bid_Selection_Method = (string)config["PP_Bid_Selection_Method"],
                                                      PP_Procurement_Method = (string)config["PP_Procurement_Method"],
                                                      PP_Invitation_Notice_Type = (string)config["PP_Invitation_Notice_Type"],
                                                      PP_Preference_Reservation_Code = (string)config["PP_Preference_Reservation_Code"],
                                                      PRN_Conversion_Procedure = (string)config["PRN_Conversion_Procedure"],
                                                      Ordered_PRN = (bool?)config["Ordered_PRN"] ?? false,
                                                      Linked_IFS_No = (string)config["Linked_IFS_No"],
                                                      Linked_LPO_No = (string)config["Linked_LPO_No"],
                                                      Consolidate_PRN = (bool?)config["Consolidate_PRN"] ?? false,
                                                      Consolidate_to_IFS_No = (string)config["Consolidate_to_IFS_No"],
                                                      Status = (string)config["Status"]
                                                  });
                }
                return PartialView(purchaseRequisitions.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public ActionResult NewPurchaseRequisition()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    PurchaseRequisitions purchaseReq = new PurchaseRequisitions();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;


                    var ProcPlanIDJson = ProcurementPlanIDLookup();
                    var PPlan = ProcPlanIDJson.Data;


                    #region Location_Code
                    List<DropdownList> Location_Code = new List<DropdownList>();
                    string pageLocation_Code = "Locations?$format=json";

                    HttpWebResponse httpResponseLocation_Code = Credentials.GetOdataData(pageLocation_Code);
                    using (var streamReader = new StreamReader(httpResponseLocation_Code.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Name"] + " (" + (string)config["Code"] + ")";
                            dropdownList3.Value = (string)config["Code"];
                            Location_Code.Add(dropdownList3);
                        }
                    }
                    #endregion

                    #region Region
                    List<DropdownList> Dim1 = new List<DropdownList>();
                    string pageDim1 = "DimensionValueList?$format=json";

                    HttpWebResponse httpResponseDim1 = Credentials.GetOdataData(pageDim1);
                    using (var streamReader = new StreamReader(httpResponseDim1.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Code"] + " -" + (string)config["Name"];
                            dropdownList3.Value = (string)config["Code"];
                            Dim1.Add(dropdownList3);
                        }
                    }
                    #endregion

                    #region Department
                    List<DropdownList> Dim2 = new List<DropdownList>();
                    /*string pageDim2 = "DimensionList?$filter=$format=json";*/
                    string pageDim2 = "DimensionValueList?$format=json";

                    HttpWebResponse httpResponseDim2 = Credentials.GetOdataData(pageDim2);
                    using (var streamReader = new StreamReader(httpResponseDim2.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Code"] + "-" + (string)config["Name"];
                            dropdownList3.Value = (string)config["Code"];
                            Dim2.Add(dropdownList3);
                        }
                    }
                    #endregion

                    #region PlanningCategory
                    List<DropdownList> PlanningCategory = new List<DropdownList>();
                    /*string pageDim2 = "DimensionList?$filter=$format=json";*/
                    string pagePlanningCategory = $"PlanningLinesApp?$filter=Procurement_Plan_ID  eq '{PPlan}'&$format=json";

                    HttpWebResponse httpResponsePlanningCategory = Credentials.GetOdataData(pagePlanningCategory);
                    using (var streamReader = new StreamReader(httpResponsePlanningCategory.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Planning_Category"] + "-" + (string)config["Description"];
                            dropdownList3.Value = (string)config["Planning_Category"];
                            PlanningCategory.Add(dropdownList3);
                        }
                    }
                    #endregion

                    #region Budget
                    List<DropdownList> BudgetList = new List<DropdownList>();
                    /*string pageDim2 = "DimensionList?$filter=$format=json";*/
                    string pageBudget = "ImplementationYears?$filter=Current eq true&$format=json";


                    HttpWebResponse httpResponseBudget = Credentials.GetOdataData(pageBudget);
                    using (var streamReader = new StreamReader(httpResponseBudget.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Budget_Code"];
                            dropdownList3.Value = (string)config["Annual_Year_Code"];
                            BudgetList.Add(dropdownList3);
                        }
                    }
                    #endregion

                    purchaseReq.ListOfLocation_Code = Location_Code.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();

                    purchaseReq.ListOfRegions = Dim1.Select(x =>
                                    new SelectListItem()
                                    {
                                        Text = x.Text,
                                        Value = x.Value
                                    }).ToList();

                    purchaseReq.ListOfDepartment = Dim2.Select(x =>
                                   new SelectListItem()
                                   {
                                       Text = x.Text,
                                       Value = x.Value
                                   }).ToList();

                    purchaseReq.ListOfPP_Planning_Category = PlanningCategory.Select(x =>
                             new SelectListItem()
                             {
                                 Text = x.Text,
                                 Value = x.Value
                             }).ToList();


                    purchaseReq.ListOfBudgets = BudgetList.Select(x =>
                             new SelectListItem()
                             {
                                 Text = x.Text,
                                 Value = x.Value
                             }).ToList();


                    return PartialView("~/Views/Purchase/PartialViews/NewPurchaseRequisition.cshtml", purchaseReq);

                }
            }

            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


        public JsonResult ProcurementPlanIDLookup()
        {
            string procPlanID = "";
            string page = "ProcurementSetup?$format=json";

            HttpWebResponse httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                var firstConfig = details["value"].FirstOrDefault();
                if (firstConfig != null)
                {
                    procPlanID = (string)firstConfig["Default_Procurement_Plan"];
                }
            }

            return Json(procPlanID, JsonRequestBehavior.AllowGet);
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitPurchaseRequisition(PurchaseRequisitions newPurchaseRequisition)
        {
            try
            {

                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                string Dim1 = employeeView.GlobalDimension1Code;
                string Dim2 = employeeView.GlobalDimension2Code;
                string DirectorateCode = employeeView.Directorate_Code;


                string Location_Code = (string)newPurchaseRequisition.Location_Code;

                string result = "";

                result = Credentials.ObjNav.fncreatePurchaseRequisition(
                        Responsible_Employee_No,
                        "",
                        1,
                        newPurchaseRequisition.PP_Planning_Category,
                        Dim1,
                        Dim2,
                        newPurchaseRequisition.Job,
                        "",
                        newPurchaseRequisition.Location_Code,
                        "",
                        Responsible_Employee_No

                );

                if (result.StartsWith("danger"))
                {
                    string Redirect = result;
                    return Json(new { message = Redirect, success = false }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string docNo = result.Split('*').Last();
                    if (docNo == "")
                    {
                        docNo = "Document not created. try again";
                        return Json(new { message = docNo, success = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { message = docNo, success = true }, JsonRequestBehavior.AllowGet);
                    }




                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult PurchaseRequisitionsDocumentView(string No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    string StaffNo = Session["Username"].ToString();
                    PurchaseRequisitions purchase = new PurchaseRequisitions();

                    string page = "PurchaseRequisition?$filter=No eq '" + No + "'&format=json";
                    HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {

                            purchase.Document_Type = (string)config["Document_Type"];
                            purchase.No = (string)config["No"];
                            purchase.PRN_Type = (string)config["PRN_Type"];
                            purchase.Document_Date = (string)config["Document_Date"];
                            purchase.Location_Code = (string)config["Location_Code"];
                            purchase.Requisition_Product_Group = (string)config["Requisition_Product_Group"];
                            purchase.Requester_ID = (string)config["Requester_ID"];
                            purchase.Request_By_No = (string)config["Request_By_No"];
                            purchase.Request_By_Name = (string)config["Request_By_Name"];
                            purchase.Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"];
                            purchase.Geographical_Location_Name = (string)config["Geographical_Location_Name"];
                            purchase.Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"];
                            purchase.Department_Name = (string)config["Admin_Unit_Name"];       
                            purchase.Project_Name = (string)config["Project_Name"];
                            purchase.Procurement_Plan_ID = (string)config["Procurement_Plan_ID"];
                            purchase.Purchaser_Code = (string)config["Purchaser_Code"];
                            purchase.Assigned_Officer = (string)config["Assigned_Officer"];
                            purchase.Job = (string)config["Job"];
                            purchase.Job_Task_No = (string)config["Job_Task_No"];
                            purchase.PP_Planning_Category = (string)config["PP_Planning_Category"];
                            purchase.Description = (string)config["Description"];
                            purchase.PP_Total_Budget = (int)config["PP_Total_Budget"];
                            purchase.PP_Total_Actual_Costs = (int?)config["PP_Total_Actual_Costs"] ?? 0;

                            purchase.PP_Solicitation_Type = (string)config["PP_Solicitation_Type"];
                            purchase.Other_Procurement_Methods = (string)config["Other_Procurement_Methods"];
                            purchase.PP_Bid_Selection_Method = (string)config["PP_Bid_Selection_Method"];
                            purchase.PP_Procurement_Method = (string)config["PP_Procurement_Method"];
                            purchase.PP_Invitation_Notice_Type = (string)config["PP_Invitation_Notice_Type"];
                            purchase.PP_Preference_Reservation_Code = (string)config["PP_Preference_Reservation_Code"];
                            purchase.PRN_Conversion_Procedure = (string)config["PRN_Conversion_Procedure"];

                            purchase.Linked_IFS_No = (string)config["Linked_IFS_No"];
                            purchase.Linked_LPO_No = (string)config["Linked_LPO_No"];

                            purchase.Consolidate_to_IFS_No = (string)config["Consolidate_to_IFS_No"];
                            purchase.Status = (string)config["Status"];

                        }
                    }


                    return View(purchase);
                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult PurchaseRequisitionLinesPartialView(string Document_No, String Status, String ProcurementPlanID, int ProcurementPlanCategory)
        {
            try
            {

                var purchaseRequisitionLines = new List<PurchaseRequisitionLines>();

                string pageLine = $"PurchaseRequisitionLines?$filter=Document_No eq '{Document_No}'&$format=json";
                HttpWebResponse httpResponseLine = Credentials.GetOdataData(pageLine);

                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    purchaseRequisitionLines.AddRange(from JObject config in details["value"]
                                                      select new PurchaseRequisitionLines
                                                      {
                                                          DocumentType = (string?)config["Document_Type"] ?? "",
                                                          DocumentNo = (string?)config["Document_No"] ?? "",
                                                          LineNo = (int?)config["Line_No"] ?? 0,
                                                          LineSelected = (bool?)config["Line_Selected"] ?? false,
                                                          Type = (string?)config["Type"] ?? "",
                                                          ProcurementPlanID = (string?)config["Procurement_Plan_ID"] ?? "",
                                                          ProcurementPlanCategory = (string?)config["Procurement_Plan_Category"] ?? "",
                                                          TechnicalSpecifications = (string?)config["Technical_Specifications"] ?? "",
                                                          Budget = (string?)config["Budget"] ?? "",
                                                          BudgetLine = (string?)config["Budget_Line"] ?? "",
                                                          Status = (string?)config["Status"] ?? "",
                                                          ProcurementPlanEntryNo = (int?)config["Procurement_Plan_Entry_No"] ?? 0,
                                                          ItemDescription = (string?)config["Item_Description"] ?? "",
                                                          PPSolicitationType = (string?)config["PP_Solicitation_Type"] ?? "",
                                                          PPProcurementMethod = (string?)config["PP_Procurement_Method"] ?? "",
                                                          OtherProcurementMethods = (string?)config["Other_Procurement_Methods"] ?? "",
                                                          PPPreferenceReservationCode = (string?)config["PP_Preference_Reservation_Code"] ?? "",
                                                          ItemCategoryCode = (string?)config["Item_Category_Code"] ?? "",
                                                          No = (string?)config["No"] ?? "",
                                                          Description = (string?)config["Description"] ?? "",
                                                          UnitOfMeasureCode = (string?)config["UnitofMeasureCode"] ?? "",
                                                          Quantity = (int?)config["Quantity"] ?? 0,
                                                          DirectUnitCost = (int?)config["Direct_Unit_Cost_Inc_VAT"] ?? 0,
                                                          Amount = (int?)config["Amount"] ?? 0,
                                                          AmountIncludingVAT = (int?)config["Amount_Including_VAT"] ?? 0,
                                                          CurrencyCode = (string?)config["Currency_Code"] ?? "",
                                                          LocationCode = (string?)config["Location_Code"] ?? "",
                                                          UnitCostLCY = (int?)config["Unit_Cost_LCY"] ?? 0,
                                                          ContractNoToPay = (string?)config["Contract_No_to_pay"] ?? "",
                                                          LPOCreated = (bool?)config["LPO_Created"] ?? false
                                                      });
                }
                var Lines = new PurchaseRequisitionLinesList
                {
                    Status = Status,
                    Procurement_Plan_ID = ProcurementPlanID,
                    Procurement_Plan_Category = ProcurementPlanCategory,
                    ListOfPurchaseRequisitionLines = purchaseRequisitionLines
                };

                ViewBag.Status = Status;

                return PartialView("~/Views/Purchase/PurchaseRequisitionLinesPartialView.cshtml", Lines);
            }
            catch (Exception ex)
            {
                // Handle and return error view
                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult UpdatePurchaseRequisitionHeader(string Document_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    PurchaseRequisitions purchaseReq = new PurchaseRequisitions();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    /* purchaseReq.EmployeeNo = employeeView.No;
                       purchaseReq.EmployeeName = employeeView.FullName;*/

                    #region Location_Code
                    List<DropdownList> Location_Code = new List<DropdownList>();
                    string pageLocation_Code = "Locations?$format=json";

                    HttpWebResponse httpResponseLocation_Code = Credentials.GetOdataData(pageLocation_Code);
                    using (var streamReader = new StreamReader(httpResponseLocation_Code.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Name"] + " (" + (string)config["Code"] + ")";
                            dropdownList3.Value = (string)config["Code"];
                            Location_Code.Add(dropdownList3);
                        }
                    }
                    #endregion



                    #region ProcurementPlanID
                    List<DropdownList> ProcurementPlanID = new List<DropdownList>();
                    string pagePP = "ProcurementPlan?$format=json";

                    HttpWebResponse httpResponsePP = Credentials.GetOdataData(pagePP);
                    using (var streamReader = new StreamReader(httpResponsePP.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                            dropdownList3.Value = (string)config["Code"];
                            ProcurementPlanID.Add(dropdownList3);
                        }
                    }
                    #endregion


                    #region PP_Planning_Category
                    List<DropdownList> Category = new List<DropdownList>();
                    /* string pageCategory = "ProcurementPlanLines?$format=json";*/
                    string pageCategory = "ProcurementPlanLines?$filter= Procurement_Plan_ID eq 'CPPLAN-003'&$format=json";


                    HttpWebResponse httpResponseCategory = Credentials.GetOdataData(pageCategory);
                    using (var streamReader = new StreamReader(httpResponseCategory.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Procurement_Plan_ID"] + " (" + (string)config["Description"] + ")";
                            dropdownList3.Value = (string)config["Planning_Category"];
                            Category.Add(dropdownList3);
                        }
                    }
                    #endregion




                    purchaseReq.No = Document_No;

                    purchaseReq.ListOfLocation_Code = Location_Code.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();





                    purchaseReq.ListOfProcurementPlans = ProcurementPlanID.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Text,
                                      Value = x.Value
                                  }).ToList();



                    purchaseReq.ListOfPP_Planning_Category = Category.Select(x =>
                                new SelectListItem()
                                {
                                    Text = x.Text,
                                    Value = x.Value
                                }).ToList();





                    return PartialView("~/Views/Purchase/PartialViews/NewPurchaseRequisition.cshtml", purchaseReq);

                }
            }

            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult NewPurchaseRequisitionLine(string Document_No, string Procurement_Plan_ID, string Procurement_Plan_Category)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    PurchaseRequisitionLines PRLine = new PurchaseRequisitionLines();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    PRLine.DocumentNo = Document_No;
                    PRLine.ProcurementPlanID = Procurement_Plan_ID;
                    PRLine.ProcurementPlanCategory = Procurement_Plan_Category;



                    #region Procurement_Plan_Entry
                    List<DropdownList> PlanEntry = new List<DropdownList>();

                    string pagePlanEntry = $"ProcurementPlanEntry?$filter=GlobalDimension1Code eq '{employeeView.GlobalDimension1Code}' and GlobalDimension2Code eq '{employeeView.GlobalDimension2Code}' and ProcurementPlanID eq '{Procurement_Plan_ID}' and PlanningCategory eq '{Procurement_Plan_Category}' and Blocked eq false&$format=json";

                    HttpWebResponse httpResponsePlanEntry = Credentials.GetOdataData(pagePlanEntry);
                    using (var streamReader = new StreamReader(httpResponsePlanEntry.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Description"];
                            dropdownList3.Value = (string)config["EntryNo"];
                            PlanEntry.Add(dropdownList3);
                        }
                    }
                    #endregion



                    PRLine.ListOfProcurement_Plan_Entry = PlanEntry.Select(x =>
                              new SelectListItem()
                              {
                                  Text = x.Text,
                                  Value = x.Value
                              }).ToList();

                    return PartialView("~/Views/Purchase/PartialViews/NewPurchaseRequisitionLine.cshtml", PRLine);

                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult DeletePurchaseRequisitionLine(string Document_No, string Line_No)
        {
            try
            {

                string res = Credentials.ObjNav.deletePurchaseRequisitionLines(int.Parse(Line_No), Document_No);
                string Redirect = "Record Deleted";
                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitPurchaseRequisitionLine(PurchaseRequisitionLines newPRLine)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                string TechnicalSpecifications = newPRLine.TechnicalSpecifications;

                string DocNo = "";
                DocNo = Credentials.ObjNav.createRequisitionLine1(
                    staffNo,
                    newPRLine.DocumentNo,
                    int.Parse(newPRLine.ItemCategoryCode),
                    newPRLine.Quantity,
                    TechnicalSpecifications,
                    newPRLine.DirectUnitCost
                );

                if (DocNo != "")
                {
                    string redirect = DocNo;
                    return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string redirect = "Record not saved. Please try again.";
                    return Json(new { message = redirect, success = false }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdatePurchaseRequisitionHeader(PurchaseRequisitions pr)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    PurchaseRequisitions purchaseReq = new PurchaseRequisitions();
                    Session["httpResponse"] = null;



                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    /* purchaseReq.EmployeeNo = employeeView.No;
                       purchaseReq.EmployeeName = employeeView.FullName;*/

                    #region Location_Code
                    List<DropdownList> Location_Code = new List<DropdownList>();
                    string pageLocation_Code = "Locations?$format=json";

                    HttpWebResponse httpResponseLocation_Code = Credentials.GetOdataData(pageLocation_Code);
                    using (var streamReader = new StreamReader(httpResponseLocation_Code.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Name"] + " (" + (string)config["Code"] + ")";
                            dropdownList3.Value = (string)config["Code"];
                            Location_Code.Add(dropdownList3);
                        }
                    }
                    #endregion



                    #region ProcurementPlanID
                    List<DropdownList> ProcurementPlanID = new List<DropdownList>();
                    string pagePP = "ProcurementPlan?$format=json";

                    HttpWebResponse httpResponsePP = Credentials.GetOdataData(pagePP);
                    using (var streamReader = new StreamReader(httpResponsePP.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                            dropdownList3.Value = (string)config["Code"];
                            ProcurementPlanID.Add(dropdownList3);
                        }
                    }
                    #endregion


                    #region PP_Planning_Category
                    List<DropdownList> Category = new List<DropdownList>();
                    /* string pageCategory = "ProcurementPlanLines?$format=json";*/
                    string Procurement_Plan_ID = pr.Procurement_Plan_ID;
                    string pageCategory2 = "ProcurementPlanLines?$format=json";
                    string pageCategory = $"ProcurementPlanLines?$filter= Procurement_Plan_ID eq '{Procurement_Plan_ID}'&$format=json";



                    HttpWebResponse httpResponseCategory = Credentials.GetOdataData(pageCategory);
                    using (var streamReader = new StreamReader(httpResponseCategory.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Description"] + "(" + (string)config["Planning_Category"] + ")";
                            dropdownList3.Value = (string)config["Planning_Category"];
                            Category.Add(dropdownList3);
                        }
                    }
                    #endregion




                    purchaseReq.No = pr.No;
                    purchaseReq.Location_Code = pr.Location_Code;
                    purchaseReq.PP_Planning_Category = pr.PP_Planning_Category;
                    purchaseReq.Requisition_Product_Group = pr.Requisition_Product_Group;

                    ViewBag.Requisition_Product_Group = pr.Requisition_Product_Group;

                    purchaseReq.ListOfLocation_Code = Location_Code.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();

                    purchaseReq.ListOfProcurementPlans = ProcurementPlanID.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Text,
                                      Value = x.Value
                                  }).ToList();



                    purchaseReq.ListOfPP_Planning_Category = Category.Select(x =>
                                new SelectListItem()
                                {
                                    Text = x.Text,
                                    Value = x.Value
                                }).ToList();





                    return PartialView("~/Views/Purchase/PartialViews/NewPurchaseRequisition.cshtml", purchaseReq);

                }
            }

            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }









        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SendPurchaseReqDocForApproval(String Document_No)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                Credentials.ObjNav.sendPurchaseRequisitionApproval(Responsible_Employee_No, Document_No);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(38, Document_No, UserID);

                string Redirect = "Purchase Requisition  Sent For Approval";
                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CancelPurchaseReqApprovalRequest(String Document_No)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                Credentials.ObjNav.fnCancelPurchaseRequisitionApproval(Responsible_Employee_No, Document_No);
                string Redirect = "Purchase Requisition Approval Request Cancelled";
                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}