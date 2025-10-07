using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Latest_Staff_Portal.Controllers
{
    public class StoreRequisitionController : Controller
    {
        // GET: Store
        public ActionResult StoreRequisitionList()
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
        public PartialViewResult StoreRequisitionListPartialView()
        {
            try
            {
                string userId = Session["UserId"].ToString();
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                List<StoreRequisitions> storeRequisitions = new List<StoreRequisitions>();
                string empNo = employeeView.No;

                string page = $"StoreRequisition?$filter=Document_Type eq 'Store Requisition' and Request_By_No eq '{empNo}' and Requisition_Type ne 'Project' and (Status eq 'Open' or Status eq 'Pending Approval')&$format=json";

                HttpWebResponse httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        StoreRequisitions requisition = new StoreRequisitions
                        {
                            Document_Type = (string)config["Document_Type"],
                            No = (string)config["No"],
                            Requester_ID = (string)config["Requester_ID"],
                            Request_By_No = (string)config["Request_By_No"],
                            Request_By_Name = (string)config["Request_By_Name"],
                            HOD = (bool?)config["HOD"] ?? false,
                            Location_Code = (string)config["Location_Code"],
                            Order_Date = DateTime.ParseExact((string)config["Order_Date"], "yyyy-MM-dd",
                                CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            Description = (string)config["Description"],
                            Requisition_Type = (string)config["Requisition_Type"],
                            Status = (string)config["Status"],
                            Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"],
                            Department_Name = (string)config["Department_Name"],
                            Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"],
                            Project_Name = (string)config["Project_Name"],
                            No_of_Archived_Versions = (int?)config["No_of_Archived_Versions"] ?? 0,
                            Budget_Item = (string)config["Budget_Item"]
                        };

                        storeRequisitions.Add(requisition);
                    }
                }

                return PartialView(storeRequisitions.OrderByDescending(x => x.No));
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
        [HttpPost]
        public ActionResult StoreRequisitionsDocumentView(string Document_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                string StaffNo = Session["Username"].ToString();
                StoreRequisitions sr = new StoreRequisitions();

                string page = "StoreRequisition?$filter=No eq '" + Document_No + "'&format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {

                        sr.Document_Type = (string)config["Document_Type"];
                        sr.No = (string)config["No"];
                        sr.Requester_ID = (string)config["Requester_ID"];
                        sr.Request_By_No = (string)config["Request_By_No"];
                        sr.Request_By_Name = (string)config["Request_By_Name"];
                        sr.HOD = (bool)config["HOD"];
                        sr.Location_Code = (string)config["Location_Code"];
                        /* sr.Order_Date = (string)config["Order_Date"];*/
                        sr.Order_Date = DateTime.ParseExact((string)config["Order_Date"], "yyyy-MM-dd",
                                CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        sr.Description = (string)config["Description"];
                        sr.Requisition_Type = (string)config["Requisition_Type"];
                        sr.Status = (string)config["Status"];
                        sr.Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"];
                        sr.Department_Name = (string)config["Department_Name"];
                        sr.Shortcut_Dimension_2_Code = (string)config["Shortcut_Dimension_2_Code"];
                        sr.Project_Name = (string)config["Project_Name"];
                        sr.No_of_Archived_Versions = (int)config["No_of_Archived_Versions"];
                        sr.Budget_Item = (string)config["Budget_Item"];
                    }
                }
               
                return View(sr);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult StoreRequisitionLinesPartialView(string Document_No, string Status)
        {
            try
            {
                // Initialize the list to hold StoreRequisitionsLines
                List<StoreRequisitionsLines> storeRequisitionLines = new List<StoreRequisitionsLines>();

                // API endpoint for fetching Store Requisition Lines
                string pageLine = $"StoreRequisitionLines?$filter=Document_No eq '{Document_No}'&$format=json";
                HttpWebResponse httpResponseLine = Credentials.GetOdataData(pageLine);

                using (var streamReader = new StreamReader(httpResponseLine.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    // Parse the JSON response
                    foreach (JObject config in details["value"])
                    {
                        // Create a new instance of StoreRequisitionsLines and populate properties
                        StoreRequisitionsLines SRLine = new StoreRequisitionsLines
                        {
                            Document_Type = (string)config["Document_Type"],
                            Document_No = (string)config["Document_No"],
                            Line_No = (int)config["Line_No"],
                            Item_Category = (string)config["Item_Category"],
                            Service_Item_Code = (string)config["Service_Item_Code"],
                            Type = (string)config["Type"],
                            No = (string)config["No"],
                            Description = (string)config["Description"],
                            Location_Code = (string)config["Location_Code"],
                            Variant_Code = (string)config["Variant_Code"],
                            Unit_of_Measure_Code = (string)config["Unit_of_Measure_Code"],
                            Quantity_In_Store = config["Quantity_In_Store"] != null ? (int)config["Quantity_In_Store"] : 0,
                            Qty_Requested = (int)config["Qty_Requested"]
                        };

                        // Add to the list
                        storeRequisitionLines.Add(SRLine);
                    }
                }

                // Return the populated data to the partial view
                ViewBag.Status = Status;
                return PartialView(storeRequisitionLines);
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

        //create Store Requisition document
        public ActionResult NewStoreRequisition()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    StoreRequisitions storeReq = new StoreRequisitions();
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

                    #region Programme
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



                    storeReq.ListOfLocation_Code = Location_Code.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();

                    storeReq.ListOfProgramme = Dim1.Select(x =>
                                    new SelectListItem()
                                    {
                                        Text = x.Text,
                                        Value = x.Value
                                    }).ToList();

                    storeReq.ListOfDepartment = Dim2.Select(x =>
                                   new SelectListItem()
                                   {
                                       Text = x.Text,
                                       Value = x.Value
                                   }).ToList();
                    return PartialView("~/Views/StoreRequisition/PartialViews/NewStoreRequisition.cshtml", storeReq);

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
        public JsonResult SubmitStoreRequisition(StoreRequisitions newStoreRequisition)
        {
            try
            {

                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;


                string Location_Code = (string)newStoreRequisition.Location_Code;
                string Request_Date = (string)newStoreRequisition.Order_Date;
                string Shortcut_Dimension_1_Code = (string)newStoreRequisition.Shortcut_Dimension_1_Code;
                string Shortcut_Dimension_2_Code = (string)newStoreRequisition.Shortcut_Dimension_2_Code;
                string Description = (string)newStoreRequisition.Description;



                string docNo = "";

                docNo = Credentials.ObjNav.fnCreateStoreRequisition(
                    Responsible_Employee_No,
                    "",
                    Location_Code,
                    Description

                );

               /* docNo = Credentials.ObjNav.CreateStoreRequisition(
                    Responsible_Employee_No,
                    Description);*/

                if (docNo.StartsWith("danger"))
                {
                    return Json(new { message = docNo, success = false }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string Redirect = docNo;
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        //create new SR line
        public ActionResult NewStoreRequisitionLine(string Document_No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    StoreRequisitionsLines SRLine = new StoreRequisitionsLines();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    SRLine.Document_No = Document_No;

                    #region PP_Planning_Category
                    List<DropdownList> Category = new List<DropdownList>();
                    string pageCategory = "ProcurementCategories?$format=json";

                    HttpWebResponse httpResponseCategory = Credentials.GetOdataData(pageCategory);
                    using (var streamReader = new StreamReader(httpResponseCategory.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList3 = new DropdownList();
                            dropdownList3.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                            dropdownList3.Value = (string)config["Code"];
                            Category.Add(dropdownList3);
                        }
                    }
                    #endregion


                    SRLine.ListOfPP_Planning_Category = Category.Select(x =>
                           new SelectListItem()
                           {
                               Text = x.Text,
                               Value = x.Value
                           }).ToList();

                    return PartialView("~/Views/StoreRequisition/PartialViews/NewStoreRequisitionLine.cshtml", SRLine);
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
        public JsonResult SubmitStoreRequisitionLine(StoreRequisitionsLines newSRLine)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string Document_No = newSRLine.Document_No;
                string Item_Category = newSRLine.Item_Category;
                string Service_Item_Code = newSRLine.Service_Item_Code;
                int Qty_Requested = newSRLine.Qty_Requested;

                Credentials.ObjNav.fnCreateStoreReqLine(
                    Document_No,
                    Item_Category,
                    Service_Item_Code,
                    Qty_Requested
                );


            /*    string res = Credentials.ObjNav.createStoreRequisitionLine()(
                    staffNo,
                    Document_No,
                    Item_Category,
                    Service_Item_Code,
                    Qty_Requested,
                    ""
                );*/

                string redirect = Document_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        //Delete Imprest line
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteStoreRequisitionLine(string Document_No, int Line_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;



                Credentials.ObjNav.fnDeleteStoreReqLine(
                                  Document_No,
                                  Line_No
                              );


                //Lastly, Redirect back to the document
                string redirect = Document_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SendStoreReqDocForApproval(String Document_No)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;
                bool res = false;
                res = Credentials.ObjNav.fnSendStoreReqApproval(Document_No);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(38, Document_No, UserID);

                if (res)
                {
                    string redirect = Document_No;
                    return Json(new { message = "Document successfully sent for approval", success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string redirect = Document_No;
                    return Json(new { message = "Record not submitted. Try again", success = false }, JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CancelStoreReqApprovalRequest(String Document_No)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;
                string res = "";

                Credentials.ObjNav.fnCancelStoreReqApproval(Responsible_Employee_No, Document_No);

                string Redirect = "Store Requisition Approval Request Cancelled";
                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GenerateStoreRequisitionReport(string Document_No)
        {
            try
            {
                var message = "";
                bool success = false, view = false;

                message = Credentials.ObjNav.StoreReqReport(Document_No);
                if (message == "")
                {
                    success = false;
                    message = "File Not Found";
                }
                else
                {
                    success = true;
                }

                return Json(new { message, success, view }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        //lookups
        public JsonResult GetItems(string Item_Category)
        {
            try
            {
                string UOM= "";

                #region ItemsLookup
                List<DropdownList> ItemsLookup = new List<DropdownList>();
                /* string pageStrat = $"Strategies?$filter=Objective_ID eq '{Objective_ID}' and Strategic_Plan_ID eq '{Strategic_Plan_ID}'&$format=json";*/
                string pageItems = $"Items?$filter=Item_Category_Code eq '{Item_Category}' &$format=json";

                HttpWebResponse httpResponseItems = Credentials.GetOdataData(pageItems);
                using (var streamReader = new StreamReader(httpResponseItems.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList3 = new DropdownList();
                        dropdownList3.Text = (string)config["Description"] +" - (" + (string)config["No"] +") (" + (string)config["Base_Unit_of_Measure"] + ")";
                        dropdownList3.Value = (string)config["No"];            
                        ItemsLookup.Add(dropdownList3);
                    }
                    
                }
                #endregion
                // Create and return the JSON result
                var response = new
                {
                    UOM = ItemsLookup.FirstOrDefault()?.Extra ?? "",
                    ListOfItems = ItemsLookup.Select(x => new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value,
                        
                    }).ToList(),
                    success = true
                };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public string GetUOM(string Item_Category)
        {
            try
            {
                string uom = null;

                string pageItems = $"Items?$filter=Item_Category_Code eq '{Item_Category}'&$format=json";
                HttpWebResponse httpResponseItems = Credentials.GetOdataData(pageItems);

                using (var streamReader = new StreamReader(httpResponseItems.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        if (config["Base_Unit_of_Measure"] != null)
                        {
                            uom = (string)config["Base_Unit_of_Measure"];
                            break; // return the first match only; remove `break` to get last one
                        }
                    }
                }

                return uom ?? "UOM not found";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }

}
