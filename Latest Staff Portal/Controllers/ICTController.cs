using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    [CustomeAuthentication]

    public class ICTController : Controller
    {
        public PartialViewResult DocumentCommentsView()
        {
            var StaffNo = Session["Username"].ToString();
            var RegDoc = new ICTAssetRequest();

            var page = "AssetMvtCard?$filter=Requestor eq '" + StaffNo + "'&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    RegDoc.DocNo = (string)config["Doc_No"];
                    RegDoc.Asset = (string)config["Asset_No"];
                    RegDoc.Description = (string)config["Asset_Description"];
                    RegDoc.Requestor_No = (string)config["Requestor"];
                    RegDoc.Requestor_Name = (string)config["Requestor_Name"];

                    var d = Convert.ToDateTime(new DateTime(0));
                    if ((DateTime)config["Date_Requested"] != new DateTime(0))
                        RegDoc.Date_Requested =
                            Convert.ToDateTime((string)config["Date_Requested"]).ToString("dd/MM/yyyy");
                    else
                        RegDoc.Date_Requested = "";
                    if ((DateTime)config["Date_Requested"] != new DateTime(0))
                        RegDoc.Date_Moved = Convert.ToDateTime((string)config["Date_Moved"]).ToString("dd/MM/yyyy");
                    else
                        RegDoc.Date_Moved = "";
                    if ((DateTime)config["Date_Requested"] != new DateTime(0))
                        RegDoc.Date_Returned =
                            Convert.ToDateTime((string)config["Date_Returned"]).ToString("dd/MM/yyyy");
                    else
                        RegDoc.Date_Returned = "";
                    RegDoc.Status = (string)config["Status"];
                    RegDoc.Remarks = (string)config["Remarks"];
                }
            }

            return PartialView("~/Views/ICT/DocumentComments.cshtml", RegDoc);
        }

        // ICT Help Desk
        public ActionResult ICTHelpDeskList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }
        public PartialViewResult ICTHelpDeskListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var ICTReqList = new List<ICTHelpDesk>();
            var employee = Session["EmployeeData"] as EmployeeView;
            var page = $"ICTHelpdeskRequest?$filter=Requesting_Officer eq '{employee.UserID}'&$orderby=Job_No desc&$format=json";


            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var ICTList = new ICTHelpDesk();
                    ICTList.Job_No = (string)config["Job_No"];
                    ICTList.Requesting_Officer = (string)config["Requesting_Officer"];
                    ICTList.Requesting_Officer_Name = (string)config["Requesting_Officer_Name"];
                    ICTList.Request_Date = (string)config["Request_Date"];
                    ICTList.Request_Time = (string)config["Request_Time"];
                    ICTList.Email = (string)config["Email"];
                    ICTList.Assigned_To = (string)config["Assigned_To"] ?? "-";
                    ICTList.Status = (string)config["Status"];
                    ICTList.ICT_Issue_Category = (string)config["HelpDesk_Category"];
                    ICTList.Description_of_the_issue = (string)config["Description_of_the_issue"];
                    ICTReqList.Add(ICTList);
                }
            }

            return PartialView("~/Views/ICT/ICTHelpDeskListPartialView.cshtml", ICTReqList);
        }
        public PartialViewResult NewICTHelpDeskRequest()
        {
            try
            {

                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();
                ICTHelpDesk helpDesk = new ICTHelpDesk();
                #region ICTCategory
                List<DropdownList> ICTCategory = new List<DropdownList>();
                string pageCategory = "ICTCategoryList?$format=json";

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
                        ICTCategory.Add(dropdownList3);
                    }
                }
                #endregion

                #region ICTSubcategory
                List<DropdownList> ICTSubCategory = new List<DropdownList>();
                string pageSubCategory = "HelpdeskSubcategory?$format=json";

                HttpWebResponse httpResponseSubCategory = Credentials.GetOdataData(pageSubCategory);
                using (var streamReader = new StreamReader(httpResponseSubCategory.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList3 = new DropdownList();
                        dropdownList3.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                        dropdownList3.Value = (string)config["Code"];
                        ICTSubCategory.Add(dropdownList3);
                    }
                }
                #endregion

                #region ICTInventory
                List<DropdownList> ICTInventory = new List<DropdownList>();
                string pageInventory = $"ICTInventoryList?$format=json";
                /*string pageInventory = $"ICTInventoryList?$filter = Current_Assigned_Employee eq '{StaffNo}'&$format=json"*/
                ;

                HttpWebResponse httpResponseInventory = Credentials.GetOdataData(pageInventory);
                using (var streamReader = new StreamReader(httpResponseInventory.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList3 = new DropdownList();
                        dropdownList3.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                        dropdownList3.Value = (string)config["Code"];
                        ICTInventory.Add(dropdownList3);
                    }
                }
                #endregion

                helpDesk.ListOfCategories = ICTCategory.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();
                helpDesk.ListOfSubCategories = ICTSubCategory.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();
                helpDesk.ListOfInventory = ICTInventory.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();

                return PartialView("~/Views/ICT/NewICTHelpDeskRequest.cshtml", helpDesk);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return PartialView("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitHelpDeskRequest(ICTHelpDesk issue)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;
                string status = "";
                status = Credentials.ObjNav.fnInsertHelpDeskIssue(UserID, Responsible_Employee_No, issue.HelpDesk_Category, issue.Helpdesk_subcategory, "", issue.Description_of_the_issue);

                if (status != "")
                {

                    return Json(new { message = status, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = status, success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CloseHelpDesk(string documentNumber, string Comments)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                bool status = false;
                var res= Credentials.ObjNav.Close(documentNumber, Comments);

                return Json(new { message = "HelpDesk issue closed successfully!", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult ICTHelpDeskDocumentView(string No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();
                ICTHelpDesk helpDesk = new ICTHelpDesk();

                //string page = $"ICTHelpdeskRequest?$filter=Job_No eq '{No}'&$format=json";
                string page = $"ICTHelpdeskAssign?$filter=Job_No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        helpDesk.Job_No = (string)config["Job_No"];

                        helpDesk.Requesting_Officer = (string)config["Requesting_Officer"];
                        helpDesk.Requesting_Officer_Name = (string)config["Requesting_Officer_Name"];
                        helpDesk.Request_Date = (string)config["Request_Date"];
                        helpDesk.Request_Time = (string)config["Request_Time"];
                        helpDesk.Email = (string)config["Email"];
                        helpDesk.Phone = (string)config["Phone"];
                        helpDesk.Employee_No = (string)config["Employee_No"];
                        helpDesk.Global_Dimension_2_Code = employeeView.GlobalDimension2Code;
                        helpDesk.Status = (string)config["Status"];
                        helpDesk.ICT_Issue_Category = (string)config["Helpdesk_Category"];
                        helpDesk.Helpdesk_subcategory = (string)config["Helpdesk_subcategory"];
                        helpDesk.Description_of_the_issue = (string)config["Description_of_the_issue"];


                        helpDesk.Assigned_To = (string)config["Assigned_To"];
                        helpDesk.Assigned_Date = (string)config["Assigned_Date"];
                        helpDesk.Expected_Resolution = (string)config["Expected_Resolution"];
                        helpDesk.Action_Taken = (string)config["Action_Taken"];
                        helpDesk.Comments = (string)config["Comments"];


                    }
                }


                #region ICTCategory
                List<DropdownList> ICTCategory = new List<DropdownList>();
                string pageCategory = "ICTCategoryList?$format=json";

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
                        ICTCategory.Add(dropdownList3);
                    }
                }
                #endregion

                #region ICTSubcategory
                List<DropdownList> ICTSubCategory = new List<DropdownList>();
                string pageSubCategory = "HelpdeskSubcategory?$format=json";

                HttpWebResponse httpResponseSubCategory = Credentials.GetOdataData(pageSubCategory);
                using (var streamReader = new StreamReader(httpResponseSubCategory.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList3 = new DropdownList();
                        dropdownList3.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                        dropdownList3.Value = (string)config["Code"];
                        ICTSubCategory.Add(dropdownList3);
                    }
                }
                #endregion

                #region ICTInventory
                List<DropdownList> ICTInventory = new List<DropdownList>();
                string pageInventory = $"ICTInventoryList?$format=json";
                /*string pageInventory = $"ICTInventoryList?$filter = Current_Assigned_Employee eq '{StaffNo}'&$format=json"*/
                ;

                HttpWebResponse httpResponseInventory = Credentials.GetOdataData(pageInventory);
                using (var streamReader = new StreamReader(httpResponseInventory.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        DropdownList dropdownList3 = new DropdownList();
                        dropdownList3.Text = (string)config["Description"] + " (" + (string)config["Code"] + ")";
                        dropdownList3.Value = (string)config["Code"];
                        ICTInventory.Add(dropdownList3);
                    }
                }
                #endregion



                helpDesk.ListOfCategories = ICTCategory.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();
                helpDesk.ListOfSubCategories = ICTSubCategory.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();
                helpDesk.ListOfInventory = ICTInventory.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.Text,
                                          Value = x.Value
                                      }).ToList();

                return View(helpDesk);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitUpdatedHelpDeskIssue(ICTHelpDesk issue)
        {
            try
            {

                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;

                bool status = false;

                status = Credentials.ObjNav.fnUpdateHelpDeskIssue( issue.Job_No, issue.HelpDesk_Category, issue.Helpdesk_subcategory, issue.Description_of_the_issue);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(56050, issue.Job_No, UserID);
                
                if (status)
                {
                    string Redirect = issue.Job_No;
                    return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Error submitting record. Try again.", success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitHelpDeskRequestToICT(string Job_No)
        {
            try
            {
                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                string Responsible_Employee_No = employeeView.No;
                string UserID = employeeView.UserID;
                bool result = false;
                result = Credentials.ObjNav.fnSendHelpDeskRequest(Job_No);

                string Redirect = Job_No;
                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        //Issuance Voucher
        public ActionResult IssuanceVoucherList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }
        public PartialViewResult IssuanceVoucherListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var issuanceVoucher = new List<IssuanceVoucher>();

            var page = $"FixedAssetCard?$filter=Responsible_Employee eq '{StaffNo}'&$format=json";
            //var page = $"FixedAssetCard?$filter=Issued_To_No eq '{StaffNo}' and Status eq 'Released'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var issuance = new IssuanceVoucher();
                    issuance.No = (string)config["No"];
                    issuance.Issued_To_User_ID = (string)config["Issued_To_User_ID"];
                    issuance.Issued_To_No = (string)config["Issued_To_No"];
                    issuance.Issued_Date = (string)config["Issued_Date"];
                    issuance.Description = (string)config["Description"];

                    issuanceVoucher.Add(issuance);
                }
            }

            return PartialView("~/Views/ICT/IssuanceVoucherListPartialView.cshtml", issuanceVoucher);
        }
        [HttpPost]
        public ActionResult IssuanceVoucherDocumentView(string No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();
                IssuanceVoucher issuanceVoucher = new IssuanceVoucher();

                // Updated API endpoint
                string page = $"ICTIssuanceVouchercard?$filter=No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        issuanceVoucher.No = (string)config["No"];
                        issuanceVoucher.Type = (string)config["Type"];
                        issuanceVoucher.Helpdesk_No = (string)config["Helpdesk_No"];
                        issuanceVoucher.Issued_To_User_ID = (string)config["Issued_To_User_ID"];
                        issuanceVoucher.Issued_To_No = (string)config["Issued_To_No"];
                        issuanceVoucher.Name = (string)config["Name"];
                        issuanceVoucher.Internal_external = (string)config["Internal_external"];
                        issuanceVoucher.Issued_Date = (string)config["Issued_Date"];
                        issuanceVoucher.Description = (string)config["Description"];
                        issuanceVoucher.Shortcut_Dimension_1_Code = (string)config["Shortcut_Dimension_1_Code"];
                        issuanceVoucher.Directorate = (string)config["Directorate"];
                        issuanceVoucher.Department = (string)config["Department"];
                        issuanceVoucher.Status = (string)config["Status"];
                        issuanceVoucher.Document_Status = (string)config["Document_Status"];
                        issuanceVoucher.Location_Code = (string)config["Location_Code"];
                        issuanceVoucher.Issued_By = (string)config["Issued_By"];





                    }
                }


                #region ICTCategory
                List<DropdownList> ICTCategory = new List<DropdownList>();
                string pageCategory = "ICTCategoryList?$format=json";

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
                        ICTCategory.Add(dropdownList3);
                    }
                }
                #endregion


                /*
                                issuanceVoucher.ListOfCategories = ICTCategory.Select(x =>
                                                      new SelectListItem()
                                                      {
                                                          Text = x.Text,
                                                          Value = x.Value
                                                      }).ToList();*/

                return View(issuanceVoucher);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult IssuanceVoucherLinesPartialView(string Document_No, string Status)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();

                List<IssuanceVoucherLines> issuanceVoucherLines = new List<IssuanceVoucherLines>();

                var page = "IssuanceVoucherLines?$filter=No eq '" + Document_No + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        IssuanceVoucherLines issuance = new IssuanceVoucherLines
                        {
                            No = (string)config["No"],
                            Type = (string)config["Type"],
                            Code = (string)config["Code"],
                            Description = (string)config["Description"],
                            Serial_No = (string)config["Serial_No"],
                            Duration_of_Use_start_date = (DateTime)config["Duration_of_Use_start_date"],
                            Duration_of_Use_end_date = (DateTime)config["Duration_of_Use_end_date"],
                            Duration_of_Use = (string)config["Duration_of_Use"],
                            FA_No = (string)config["FA_No"],
                            Reason_For_Movement = (string)config["Reason_For_Movement"],
                            Comments = (string)config["Comments"],
                            Return_Reason = (string)config["Return_Reason"],
                            Return_Condition = (string)config["Return_Condition"]
                        };
                        issuanceVoucherLines.Add(issuance);
                    }
                }
                return PartialView("~/Views/ICT/IssuanceVoucherLinesPartialView.cshtml", issuanceVoucherLines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult UpdateIssuanceVoucherLine(IssuanceVoucherLines issuanceVoucherLine)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    IssuanceVoucherLines issuanceLine = new IssuanceVoucherLines();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    /*issuanceVoucherLine.No = Document_No;*/

                    #region TransactionTypes
                    List<DropdownList> TransactionTypes = new List<DropdownList>();
                    string pagePPlan = "TransactionTypes?$format=json";

                    HttpWebResponse httpResponsePPlan = Credentials.GetOdataData(pagePPlan);
                    using (var streamReader = new StreamReader(httpResponsePPlan.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["Name"] + "-" + (string)config["No"];
                            dropdownList.Value = (string)config["No"];
                            TransactionTypes.Add(dropdownList);
                        }
                    }
                    #endregion

                    /*
                    issuanceLine.ListOfTransactionTypes = TransactionTypes.Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.Text,
                                           Value = x.Value
                                       }).ToList();*/
                    return PartialView("~/Views/ICT/PartialViews/NewIssuanceVoucherLine.cshtml", issuanceLine);

                }
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        //Fixed Assets
        public ActionResult FixedAssetsList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }
        public PartialViewResult FixedAssetsListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var fixedAssetsList = new List<FixedAssets>();


            var page = $"FixedAssetCard?$filter=Responsible_Employee eq '{StaffNo}'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var fixedAsset = new FixedAssets();
                    fixedAsset.No = (string)config["No"];
                    fixedAsset.Description = (string)config["Description"];
                    fixedAsset.FA_Location_Code = (string)config["FA_Location_Code"];
                    fixedAsset.Responsible_Employee = (string)config["Responsible_Employee"];
                    fixedAsset.FA_Class_Code = (string)config["FA_Class_Code"];
                    fixedAsset.FA_Subclass_Code = (string)config["FA_Subclass_Code"];
                    fixedAsset.Search_Description = (string)config["Search_Description"];
                    fixedAsset.Acquired = (bool)config["Acquired"];
                    fixedAsset.FA_Posting_Group = (string)config["FA_Posting_Group"];

                    fixedAssetsList.Add(fixedAsset);
                }
            }

            return PartialView("~/Views/ICT/PartialViews/FixedAssetsListPartialView.cshtml", fixedAssetsList);
        }
        [HttpPost]
        public ActionResult FixedAssetsDocumentView(string No)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();
                FixedAssets fixedAsset = new FixedAssets();

                var page = $"FixedAssetCard?$filter=No eq '{No}'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        fixedAsset.No = (string)config["No"];
                        fixedAsset.Description = (string)config["Description"];
                        fixedAsset.FA_Location_Code = (string)config["FA_Location_Code"];
                        fixedAsset.Responsible_Employee = (string)config["Responsible_Employee"];
                        fixedAsset.FA_Class_Code = (string)config["FA_Class_Code"];
                        fixedAsset.FA_Subclass_Code = (string)config["FA_Subclass_Code"];
                        fixedAsset.Search_Description = (string)config["Search_Description"];
                        fixedAsset.Acquired = (bool)config["Acquired"];
                        fixedAsset.FAPostingGroup = (string)config["FAPostingGroup"];
                    }
                }


                #region ICTCategory
                List<DropdownList> ICTCategory = new List<DropdownList>();
                string pageCategory = "ICTCategoryList?$format=json";

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
                        ICTCategory.Add(dropdownList3);
                    }
                }
                #endregion


                /* helpDesk.ListOfCategories = ICTCategory.Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.Text,
                                           Value = x.Value
                                       }).ToList();
               */

                return View(fixedAsset);
            }
            catch (Exception ex)
            {
                Error erroMsg = new Error
                {
                    Message = ex.Message
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }











        #region ICT Req

        public ActionResult ICTRequisitionList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult ICTListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var ICTReqList = new List<ICTRequest>();

            var page = "ICTRequisition?$filter=Requested_By eq '" + StaffNo + "'&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var ICTList = new ICTRequest();
                    ICTList.No = (string)config["No"];
                    ICTList.Date = Convert.ToDateTime((string)config["Date"]).ToString("dd/MM/yyyy");
                    ICTList.ReqCat = (string)config["Requisition_Category"];
                    ICTList.Directorate = (string)config["Global_Dimension_1_Code"];
                    ICTList.Department = (string)config["Global_Dimension_2_Code"];
                    ICTList.Description = (string)config["General_Description"];
                    ICTList.Urgency = (string)config["Urgency_Priority"];
                    ICTList.RequiredDate = Convert.ToDateTime((string)config["Required_Date"]).ToString("dd/MM/yyyy");
                    ICTList.Status = (string)config["Resolution_Status"];
                    ICTList.Assignee = (string)config["Assignee"] + "(" +
                                       CommonClass.GetEmployeeName((string)config["Assignee"]) + ")";
                    ICTList.Resoltion_Remarks = (string)config["Resolution_Remarks"];
                    ICTReqList.Add(ICTList);
                }
            }

            return PartialView("~/Views/ICT/ICTListView.cshtml", ICTReqList);
        }

        public ActionResult ICTAssignmentList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult ICTAssignedListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var ICTReqList = new List<ICTRequest>();

            var page = "ICTRequisition?$filter=Assignee eq '" + StaffNo + "'&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var ICTList = new ICTRequest();
                    ICTList.No = (string)config["No"];
                    ICTList.Date = Convert.ToDateTime((string)config["Date"]).ToString("dd/MM/yyyy");
                    ICTList.ReqCat = (string)config["Requisition_Category"];
                    ICTList.Directorate = (string)config["Global_Dimension_1_Code"];
                    ICTList.Department = (string)config["Global_Dimension_2_Code"];
                    ICTList.Description = (string)config["General_Description"];
                    ICTList.Urgency = (string)config["Urgency_Priority"];
                    ICTList.RequiredDate = Convert.ToDateTime((string)config["Required_Date"]).ToString("dd/MM/yyyy");
                    ICTList.Status = (string)config["Resolution_Status"];
                    ICTList.Assignee = (string)config["Requested_By"] + "(" +
                                       CommonClass.GetEmployeeName((string)config["Requested_By"]) + ")";
                    ICTList.Resoltion_Remarks = (string)config["Resolution_Remarks"];
                    ICTReqList.Add(ICTList);
                }
            }

            return PartialView("~/Views/ICT/ICTAssignedList.cshtml", ICTReqList);
        }

        public PartialViewResult NewICTRequest()
        {
            var StaffNo = Session["Username"].ToString();
            string Dir = "", Dep = "";

            #region Employee Data

            var pageData = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(pageData);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                if (details["value"].Count() > 0)
                    foreach (JObject config in details["value"])
                    {
                        Dir = (string)config["Global_Dimension_1_Code"];
                        Dep = (string)config["GlobalDimension2Code"];
                    }
            }

            #endregion

            if (Dir == "")
            {
                var erroMsg = new Error();
                erroMsg.Message = "Your directorate has not been set. Contact HR";
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }

            if (Dep == "")
            {
                var erroMsg = new Error();
                erroMsg.Message = "Your department has not been set. Contact HR";
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }

            var NewICTReq = new NewICTRequisition();

            #region Directorate List

            var DirectorateList = new List<DimensionValues>();
            var pageDir = "DimensionValues?$filter=Global_Dimension_No eq 1 and Blocked eq false&$format=json";

            var httpResponseDepartment = Credentials.GetOdataData(pageDir);
            using (var streamReader = new StreamReader(httpResponseDepartment.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Directorate = new DimensionValues();
                    Directorate.Code = (string)config["Code"];
                    Directorate.Name = (string)config["Name"];
                    DirectorateList.Add(Directorate);
                }
            }

            #endregion

            #region Department

            var DepartmentList = new List<DimensionValues>();
            var pageDepartment = "DimensionValues?$filter=Global_Dimension_No eq 1 and Blocked eq false&$format=json";

            var httpResponseDivision = Credentials.GetOdataData(pageDepartment);
            using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Department = new DimensionValues();
                    Department.Code = (string)config["Code"];
                    Department.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    DepartmentList.Add(Department);
                }
            }

            #endregion

            #region Categories

            var CategoryList = new List<DropdownList>();
            var pageResC = "ICTRequisitionCategory?$format=json";

            var httpResponseResC = Credentials.GetOdataData(pageResC);
            using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var CatList = new DropdownList();
                    CatList.Value = (string)config["Code"];
                    CatList.Text = (string)config["Description"];
                    CategoryList.Add(CatList);
                }
            }

            #endregion

            NewICTReq = new NewICTRequisition
            {
                Directorate = Dir,
                Department = Dep,
                ListOfDirectorate = DirectorateList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList(),
                ListOfDepartment = DepartmentList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList(),
                ListOfCategory = CategoryList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return PartialView("~/Views/ICT/NewICTRequest.cshtml", NewICTReq);
        }

        public PartialViewResult CancelICTRequestForm(string DocNo)
        {
            var c = new ICTCancel();
            c.DocNo = DocNo;
            return PartialView("~/Views/ICT/CancelRemarks.cshtml", c);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitICTRequest(ICTRequest NewReq, string base64Upload, string fileName, string Extn)
        {
            try
            {
                var requireddate = DateTime.ParseExact(NewReq.RequiredDate.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
                var username = Session["username"].ToString();

                var DocNo = ""; //Credentials.ObjNav.ICTRequisitionCreate(username, NewReq.Directorate,
                //NewReq.Department,"","", Convert.ToInt32(NewReq.Urgency), requireddate, NewReq.Description, NewReq.ReqCat);
                if (base64Upload != "" && fileName != "")
                {
                    var filePath = Server.MapPath("~/Uploads/" + fileName);
                    var uploaded = UploadDocuments.UploadEDMSDocumentAttachment(base64Upload, fileName, "HRMD", DocNo,
                        "Welfare", "", 54100, "");
                }

                //if (DocNo != "")
                //{
                //    foreach (var c in ICTReqLines)
                //    {
                //        string Descriprion = c.Description.Trim();
                //        string Quantity = c.Quantity.Trim();
                //        Credentials.ObjNav.InsertICTRequisitionLines(DocNo, Descriprion, Convert.ToInt32(Quantity));
                //    }
                //}
                return Json(
                    new { message = "ICT Requisition DocNo " + DocNo + " Submitted Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CancelICTRequest(string DocNo, string CancelR)
        {
            try
            {
                // Credentials.ObjNav.CancelICTRequisitionCreate(DocNo, CancelR);

                return Json(
                    new { message = "ICT Requisition DocNo " + DocNo + " cancelled Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult ICTResolveForm(string DocNo, string Status)
        {
            var c = new ICTCancel();
            c.DocNo = DocNo;
            c.Status = Status;
            return PartialView("~/Views/ICT/ICTResolveRemarks.cshtml", c);
        }

        public PartialViewResult ConfirmICTRequestForm(string DocNo)
        {
            var c = new ICTCancel();
            c.DocNo = DocNo;
            return PartialView("~/Views/ICT/ConfirmRemarks.cshtml", c);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ConfirmICTRequest(string DocNo, string Resolved, string ConfirmR)
        {
            try
            {
                //  Credentials.ObjNav.ConfirmClosureOfICTRequisition(DocNo, ConfirmR);

                return Json(new { message = "Confirmation Submitted Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ResolveICTRequest(string DocNo, string Resolved, string ConfirmR)
        {
            try
            {
                //Credentials.ObjNav.ICTRequisitionResolution(DocNo, ConfirmR);

                return Json(new { message = "ICT Support Issue Resolve Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region ICT Asste Req

        public ActionResult ICTAssetTransferList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult ICTAssetTransferListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var ICTAssetReqList = new List<ICTAssetRequest>();

            var page = "AssetMvtCard?$filter=Requestor eq '" + StaffNo + "'&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var RegList = new ICTAssetRequest();
                    RegList.DocNo = (string)config["Doc_No"];
                    RegList.Asset = (string)config["Asset_No"];
                    RegList.Description = (string)config["Asset_Description"];
                    RegList.Requestor_No = (string)config["Requestor"];
                    RegList.Requestor_Name = (string)config["Requestor_Name"];
                    RegList.Date_Requested =
                        Convert.ToDateTime((string)config["Date_Requested"]).ToString("dd/MM/yyyy");
                    RegList.Date_Moved = Convert.ToDateTime((string)config["Date_Moved"]).ToString("dd/MM/yyyy");
                    RegList.Date_Returned = Convert.ToDateTime((string)config["Date_Returned"]).ToString("dd/MM/yyyy");
                    RegList.Status = (string)config["Status"];
                    RegList.Remarks = (string)config["Remarks"];
                    ICTAssetReqList.Add(RegList);
                }
            }

            return PartialView("~/Views/ICT/ICTAssetTransferListView.cshtml",
                ICTAssetReqList.OrderByDescending(x => x.DocNo));
        }

        public PartialViewResult NewICTTransferRequest()
        {
            var StaffNo = Session["Username"].ToString();
            string Dir = "", Dep = "";

            #region Employee Data

            var pageData = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(pageData);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                if (details["value"].Count() > 0)
                    foreach (JObject config in details["value"])
                    {
                        Dir = (string)config["Global_Dimension_1_Code"];
                        Dep = (string)config["GlobalDimension2Code"];
                    }
            }

            #endregion

            if (Dir == "")
            {
                var erroMsg = new Error();
                erroMsg.Message = "Your directorate has not been set. Contact HR";
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }

            if (Dep == "")
            {
                var erroMsg = new Error();
                erroMsg.Message = "Your department has not been set. Contact HR";
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }

            var NewICTReq = new NewICTRequisition();

            #region Directorate List

            var DirectorateList = new List<DimensionValues>();
            var pageDir = "DimensionValues?$filter=Global_Dimension_No eq 1 and Blocked eq false&$format=json";

            var httpResponseDepartment = Credentials.GetOdataData(pageDir);
            using (var streamReader = new StreamReader(httpResponseDepartment.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Directorate = new DimensionValues();
                    Directorate.Code = (string)config["Code"];
                    Directorate.Name = (string)config["Name"];
                    DirectorateList.Add(Directorate);
                }
            }

            #endregion

            #region Department

            var DepartmentList = new List<DimensionValues>();
            var pageDepartment = "DimensionValues?$filter=Global_Dimension_No eq 1 and Blocked eq false&$format=json";

            var httpResponseDivision = Credentials.GetOdataData(pageDepartment);
            using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Department = new DimensionValues();
                    Department.Code = (string)config["Code"];
                    Department.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    DepartmentList.Add(Department);
                }
            }

            #endregion

            NewICTReq = new NewICTRequisition
            {
                Directorate = Dir,
                Department = Dep,
                ListOfDirectorate = DirectorateList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList(),
                ListOfDepartment = DepartmentList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList()
            };
            return PartialView("~/Views/ICT/NewICTAssetRequest.cshtml", NewICTReq);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitICTTransferRequest(ICTAssetRequest NewReq)
        {
            try
            {
                var requireddate = DateTime.ParseExact(NewReq.Date_Requested.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                var
                    DocNo = ""; //Credentials.ObjNav.SubmitICTAssetMovement(Session["username"].ToString(), requireddate, NewReq.Description,NewReq.reason);

                return Json(
                    new
                    {
                        message = "ICT Asset Requisition DocNo " + DocNo + " Submitted Successfully",
                        success = true
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult ICTAssetTransferDocView()
        {
            var StaffNo = Session["Username"].ToString();
            var RegDoc = new ICTAssetRequest();

            var page = "AssetMvtCard?$filter=Requestor eq '" + StaffNo + "'&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    RegDoc.DocNo = (string)config["Doc_No"];
                    RegDoc.Asset = (string)config["Asset_No"];
                    RegDoc.Description = (string)config["Asset_Description"];
                    RegDoc.Requestor_No = (string)config["Requestor"];
                    RegDoc.Requestor_Name = (string)config["Requestor_Name"];

                    var d = Convert.ToDateTime(new DateTime(0));
                    if ((DateTime)config["Date_Requested"] != new DateTime(0))
                        RegDoc.Date_Requested =
                            Convert.ToDateTime((string)config["Date_Requested"]).ToString("dd/MM/yyyy");
                    else
                        RegDoc.Date_Requested = "";
                    if ((DateTime)config["Date_Requested"] != new DateTime(0))
                        RegDoc.Date_Moved = Convert.ToDateTime((string)config["Date_Moved"]).ToString("dd/MM/yyyy");
                    else
                        RegDoc.Date_Moved = "";
                    if ((DateTime)config["Date_Requested"] != new DateTime(0))
                        RegDoc.Date_Returned =
                            Convert.ToDateTime((string)config["Date_Returned"]).ToString("dd/MM/yyyy");
                    else
                        RegDoc.Date_Returned = "";
                    RegDoc.Status = (string)config["Status"];
                    RegDoc.Remarks = (string)config["Remarks"];
                }
            }

            return PartialView("~/Views/ICT/ICTAssetRequestDocument.cshtml", RegDoc);
        }

        #endregion

        #region ICT Service Req

        public ActionResult ICTServMntList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult ICTServMntListPartialView()
        {
            var StaffNo = Session["Username"].ToString();
            var ServReqList = new List<ICTServiceRequest>();

            var page = "ICT_Service_Maintenance_Card?$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var Reg = new ICTServiceRequest();
                    Reg.DocNo = (string)config["Doc_No"];
                    Reg.Asset = (string)config["Asset_No"];
                    Reg.Description = (string)config["Asset_Description"];
                    Reg.ServiceDate = Convert.ToDateTime((string)config["Service_Date"]).ToString("dd/MM/yyyy");
                    Reg.LastServiceDate =
                        Convert.ToDateTime((string)config["Last_Service_Date"]).ToString("dd/MM/yyyy");
                    Reg.NextSeviceDate = Convert.ToDateTime((string)config["Next_Service_Date"]).ToString("dd/MM/yyyy");
                    Reg.Status = (string)config["Service_Status"];
                    Reg.Remarks = (string)config["Service_Details"];
                    ServReqList.Add(Reg);
                }
            }

            return PartialView("~/Views/ICT/ICTAssetServiceReqListView.cshtml",
                ServReqList.OrderByDescending(x => x.DocNo));
        }

        public PartialViewResult NewICTServMntRequest()
        {
            var StaffNo = Session["Username"].ToString();
            string Dir = "", Dep = "";

            #region Employee Data

            var pageData = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(pageData);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                if (details["value"].Count() > 0)
                    foreach (JObject config in details["value"])
                    {
                        Dir = (string)config["Global_Dimension_1_Code"];
                        Dep = (string)config["GlobalDimension2Code"];
                    }
            }

            #endregion

            if (Dir == "")
            {
                var erroMsg = new Error();
                erroMsg.Message = "Your directorate has not been set. Contact HR";
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }

            if (Dep == "")
            {
                var erroMsg = new Error();
                erroMsg.Message = "Your department has not been set. Contact HR";
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }

            var NewICTReq = new NewICTRequisition();

            #region Directorate List

            var DirectorateList = new List<DimensionValues>();
            var pageDir = "DimensionValues?$filter=Global_Dimension_No eq 1 and Blocked eq false&$format=json";

            var httpResponseDepartment = Credentials.GetOdataData(pageDir);
            using (var streamReader = new StreamReader(httpResponseDepartment.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Directorate = new DimensionValues();
                    Directorate.Code = (string)config["Code"];
                    Directorate.Name = (string)config["Name"];
                    DirectorateList.Add(Directorate);
                }
            }

            #endregion

            #region Department

            var DepartmentList = new List<DimensionValues>();
            var pageDepartment = "DimensionValues?$filter=Global_Dimension_No eq 1 and Blocked eq false&$format=json";

            var httpResponseDivision = Credentials.GetOdataData(pageDepartment);
            using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Department = new DimensionValues();
                    Department.Code = (string)config["Code"];
                    Department.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    DepartmentList.Add(Department);
                }
            }

            #endregion

            #region ICT Asset List

            var ICTAssetList = new List<DropdownList>();
            var pageICTAsset = "ICTAssetRegister?$format=json";

            var httpResponseICTAsset = Credentials.GetOdataData(pageICTAsset);
            using (var streamReader = new StreamReader(httpResponseICTAsset.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var Asset = new DropdownList();
                    Asset.Value = (string)config["Asset_No"];
                    Asset.Text = (string)config["Asset_Description"];
                    ICTAssetList.Add(Asset);
                }
            }

            #endregion

            NewICTReq = new NewICTRequisition
            {
                Code = "",
                Directorate = Dir,
                Department = Dep,
                ListOfDirectorate = DirectorateList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList(),
                ListOfDepartment = DepartmentList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList(),
                ListOfICTAsset = ICTAssetList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return PartialView("~/Views/ICT/NewICTServiceRequest.cshtml", NewICTReq);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitICTServmntRequest(ICTServiceRequest NewReq)
        {
            try
            {
                var ServiceD = DateTime.ParseExact(NewReq.ServiceDate.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                var
                    DocNo = ""; // Credentials.ObjNav.SubmitICTService_mainRequest(Session["username"].ToString(), NewReq.Asset, ServiceD,
                //Convert.ToInt32(NewReq.Category), NewReq.Description);

                return Json(
                    new
                    {
                        message = "ICT Asset Requisition DocNo " + DocNo + " Submitted Successfully",
                        success = true
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult ICTServMntDocView(string DocNo)
        {
            var StaffNo = Session["Username"].ToString();
            var RegDoc = new ICTServiceRequest();

            var page = "ICT_Service_Maintenance_Card?$filter=Doc_No eq '" + DocNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    RegDoc.DocNo = (string)config["Doc_No"];
                    RegDoc.Asset = (string)config["Asset_No"];
                    RegDoc.Description = (string)config["Asset_Description"];
                    RegDoc.Date = Convert.ToDateTime((string)config["Date_Created"]).ToString("dd/MM/yyyy");
                    RegDoc.ServiceDate = Convert.ToDateTime((string)config["Service_Date"]).ToString("dd/MM/yyyy");
                    RegDoc.LastServiceDate =
                        Convert.ToDateTime((string)config["Last_Service_Date"]).ToString("dd/MM/yyyy");
                    RegDoc.NextSeviceDate =
                        Convert.ToDateTime((string)config["Next_Service_Date"]).ToString("dd/MM/yyyy");
                    RegDoc.Status = (string)config["Service_Status"];
                    RegDoc.Remarks = (string)config["Service_Details"];
                }
            }

            return PartialView("~/Views/ICT/ServiceDocView.cshtml", RegDoc);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateICTServmntRequest(string DocNo, string LServDate, string NxtServDate)
        {
            try
            {
                var LSDte = DateTime.ParseExact(LServDate.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
                var NSDate = DateTime.ParseExact(NxtServDate.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                //Credentials.ObjNav.UpdateICTService_mainRequest(DocNo,"",DateTime.Today, LSDte, NSDate);

                return Json(
                    new
                    {
                        message = "ICT Asset Requisition DocNo " + DocNo + " Submitted Successfully",
                        success = true
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}