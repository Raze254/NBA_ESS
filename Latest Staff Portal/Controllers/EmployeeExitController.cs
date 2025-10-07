using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf.parser;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    public class EmployeeExitController : Controller
    {
        // GET: Employee Exit List
        public ActionResult EmployeeExitVoucherList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }
        public PartialViewResult EmployeeExitVoucherListPartialView()
        {

            var StaffNo = Session["Username"].ToString();
            var UserID = Session["UserID"]?.ToString();
            var empExitList = new List<EmployeeExitVoucher>();

            /*var page = "EmployeeExit?$filter=CreatedBy eq '" + UserID + "'&format=json";*/
            var page = "EmployeeExit?$filter=EmployeeNo eq '" + StaffNo + "'&format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var exitList = new EmployeeExitVoucher();

                    exitList.DocumentNo = (string)config["DocumentNo"];
                    exitList.EmployeeNo = (string)config["EmployeeNo"];
                    exitList.EmployeeNames = (string)config["EmployeeNames"];
                    exitList.ExitMethod = (string)config["ExitMethod"];
                    exitList.JobId = (string)config["JobId"];
                    exitList.ReasonsCode = (string)config["ReasonsCode"];

                    empExitList.Add(exitList);
                }
            }

            return PartialView("~/Views/EmployeeExit/PartialViews/EmployeeExitVoucherListPartialView.cshtml", empExitList);
        }
        public ActionResult NewExitVoucherRequest()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    EmployeeExitVoucher exitPlan = new EmployeeExitVoucher();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;


                    #region ExitMethods
                    List<DropdownList> ExitMethods = new List<DropdownList>();
                    string pageExitMethods = "ExitMethods?$format=json";

                    HttpWebResponse httpResponseExitMEthods = Credentials.GetOdataData(pageExitMethods);
                    using (var streamReader = new StreamReader(httpResponseExitMEthods.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["Description"];
                            dropdownList.Value = (string)config["Code"];
                            ExitMethods.Add(dropdownList);
                        }
                    }
                    #endregion

                    #region Reasons
                    List<DropdownList> Reasons = new List<DropdownList>();
                    string pageReasons = "ReasonsForExit?$format=json";

                    HttpWebResponse httpResponseReasons = Credentials.GetOdataData(pageReasons);
                    using (var streamReader = new StreamReader(httpResponseReasons.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["Description"];
                            dropdownList.Value = (string)config["Code"];
                            Reasons.Add(dropdownList);
                        }
                    }
                    #endregion


                    exitPlan.ListOfExitMethods = ExitMethods.Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.Text,
                                           Value = x.Value
                                       }).ToList();

                    exitPlan.ListOfReasons = Reasons.Select(x =>
                                   new SelectListItem()
                                   {
                                       Text = x.Text,
                                       Value = x.Value
                                   }).ToList();

                    return PartialView("~/Views/EmployeeExit/PartialViews/NewExitVoucher.cshtml", exitPlan);

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
        public JsonResult SubmitExitVoucherRequest(EmployeeExitVoucher newExitVoucher)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                string Description = newExitVoucher.Description;
                string ExitMethod = newExitVoucher.ExitMethod;
                string Reasons = newExitVoucher.ReasonsCode;


                DateTime LastworkingDate = DateTime.ParseExact(newExitVoucher.LastworkingDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime NoticeDate = DateTime.ParseExact(newExitVoucher.NoticeDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime EmployeeExitDate = DateTime.ParseExact(newExitVoucher.EmployeeExitDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string DocumentNo = newExitVoucher.DocumentNo;

                if (DocumentNo == null) //creating new exit voucher
                {
                    string res = Credentials.ObjNav.createEmployeeExitVoucher(
                        staffNo,
                        Reasons,
                        Description,
                        LastworkingDate,
                        NoticeDate,
                        EmployeeExitDate
                     );

                }
                else  //updating exit voucher
                {

                    /*Credentials.ObjNav.fnUpdateImprestLines(
                                      Document_No,
                                      Line_No,
                                      transType,
                                      Quantity,
                                      UnitPrice,
                                      UnitofMeasure
                                  );*/
                }


                //Lastly, Redirect back to the document
                string redirect = "Exit Voucher successfully created";
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeExitVoucherDocumentView(string DocumentNo)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;
                var StaffNo = Session["Username"].ToString();
                EmployeeExitVoucher exitVoucher = new EmployeeExitVoucher();

                // Updated API endpoint
                var page = "EmployeeExit?$filter= DocumentNo eq '" + DocumentNo + "'&$format=json";
                HttpWebResponse httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    var config = details["value"].FirstOrDefault();
                    if (config != null)
                    {
                        exitVoucher.DocumentNo = (string)config["DocumentNo"];
                        exitVoucher.Description = (string)config["Description"];
                        exitVoucher.EmployeeNo = (string)config["EmployeeNo"];
                        exitVoucher.EmployeeNames = (string)config["EmployeeNames"];
                        exitVoucher.ExitMethod = (string)config["ExitMethod"];
                        exitVoucher.JobId = (string)config["JobId"];
                        exitVoucher.ReasonsCode = (string)config["ReasonsCode"];
                        exitVoucher.CreatedOn = (string)config["CreatedOn"];
                        exitVoucher.CreatedBy = (string)config["CreatedBy"];
                        exitVoucher.DocumentDate = (string)config["DocumentDate"];
                        exitVoucher.Posted = (bool)config["Posted"];
                        exitVoucher.DateofJoin = (string)config["DateofJoin"];
                        exitVoucher.LastworkingDate = (string)config["LastworkingDate"];
                        exitVoucher.NoticeDate = (string)config["NoticeDate"];
                        exitVoucher.EmployeeExitDate = (string)config["EmployeeExitDate"];
                        exitVoucher.Leave_Days_Due_Payable = (int)config["Leave_Days_Due_Payable"];
                        exitVoucher.Pay_in_Line_of_Notice = (int)config["Pay_in_Line_of_Notice"];
                        exitVoucher.Notice_Period = (string)config["Notice_Period"];
                        exitVoucher.NoofHandOverNotes = (int)config["NoofHandOverNotes"];
                        exitVoucher.NoofOpenHandOverNotes = (int)config["NoofOpenHandOverNotes"];
                        exitVoucher.NoofOpenHandClearedNotes = (int)config["NoofOpenHandClearedNotes"];


                    }
                }

                return View(exitVoucher);
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
        public PartialViewResult EmployeeExitVoucherLinesPartialView(string Document_No, string Status)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml",
                        new Error { Message = "Session expired. Please log in again." });
                }

                var exitVoucherLinesList = new List<ExitPlanLines>();

                // API endpoint for fetching Imprest Surrender Lines
                var page = $"ExitPlanLines?$filter=Exit_Header_No eq '{Document_No}'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var config in details["value"])
                    {
                        var line = new ExitPlanLines
                        {
                            Exit_Header_No = (string)config["Exit_Header_No"],
                            Document_Type = (string)config["Document_Type"],
                            PrimaryDirectorate = (string)config["PrimaryDirectorate"],
                            PrimaryDirectorateName = (string)config["PrimaryDirectorateName"],
                            ResponsibleEmployee = (string)config["ResponsibleEmployee"],
                            Responsible_Employee_Name = (string)config["Responsible_Employee_Name"],
                            PlannedDate = (string)config["PlannedDate"],
                            ActualDate = (string)config["ActualDate"],
                            Status = (string)config["Status"],
                            clearedBy = (string)config["clearedBy"],
                        };
                        exitVoucherLinesList.Add(line);
                    }
                }

                // Pass sorted list to partial view
                return PartialView("~/Views/EmployeeExit/PartialViews/EmployeeExitVoucherLinesPartialView.cshtml",
                    exitVoucherLinesList.OrderByDescending(x => x.LineNo));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error
                {
                    Message = ex.Message
                };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public ActionResult NewExitVoucherLine(string Exit_Header_No, int LineNo)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    ExitPlanLines exitPlanLines = new ExitPlanLines();
                    Session["httpResponse"] = null;
                    EmployeeView employeeView = Session["EmployeeData"] as EmployeeView;

                    exitPlanLines.Exit_Header_No = Exit_Header_No;
                    exitPlanLines.LineNo = LineNo == 0 ? 0 : LineNo; // Use ternary operator for explicit defaulting


                    #region ResponsibilityCenters
                    List<DropdownList> ResponsibilityCenters = new List<DropdownList>();
                    string pagePPlan = "ResponsibilityCenters?$format=json";

                    HttpWebResponse httpResponsePPlan = Credentials.GetOdataData(pagePPlan);
                    using (var streamReader = new StreamReader(httpResponsePPlan.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var details = JObject.Parse(result);

                        foreach (JObject config in details["value"])
                        {
                            DropdownList dropdownList = new DropdownList();
                            dropdownList.Text = (string)config["Name"] + " (" + (string)config["Code"] + ")";
                            dropdownList.Value = (string)config["Code"];
                            ResponsibilityCenters.Add(dropdownList);
                        }
                    }
                    #endregion



                    exitPlanLines.ListOfResponsibilityCenters = ResponsibilityCenters.Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.Text,
                                           Value = x.Value
                                       }).ToList();

                    return PartialView("~/Views/EmployeeExit/PartialViews/NewExitVoucherLine.cshtml", exitPlanLines);

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
        public JsonResult SubmitExitVoucherLine(ExitPlanLines newExitPlanLine)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string Exit_Header_No = newExitPlanLine.Exit_Header_No;
                int Line_No = newExitPlanLine.LineNo;
                string PrimaryDirectorate = newExitPlanLine.PrimaryDirectorate;
                string PlannedDate = newExitPlanLine.PlannedDate;

                if (Line_No == 0)
                {
                    /*Credentials.ObjNav.fnInsertImprestLines(
                                       Document_No,
                                       transType,
                                       Quantity,
                                       UnitPrice,
                                       UnitofMeasure
                                   );*/
                }
                else
                {
                    /*Credentials.ObjNav.fnUpdateImprestLines(
                                      Document_No,
                                      Line_No,
                                      transType,
                                      Quantity,
                                      UnitPrice,
                                      UnitofMeasure
                                  );*/
                }


                //Lastly, Redirect back to the document
                string redirect = Exit_Header_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SendExitVoucherDocForApproval(string Exit_Header_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                /*Credentials.ObjNav.fnInsertImprestLines(
                                   Document_No,
                                   transType,
                                   Quantity,
                                   UnitPrice,
                                   UnitofMeasure
                               );*/


                string redirect = Exit_Header_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreateHandoverVoucher(string Exit_Header_No)
        {
            try
            {
                string staffNo = Session["Username"]?.ToString();
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;

                /*Credentials.ObjNav.fnInsertImprestLines(
                                   Exit_Header_No,
                                  
                               );*/


                string redirect = Exit_Header_No;
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}