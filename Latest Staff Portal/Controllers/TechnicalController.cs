using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Latest_Staff_Portal.Controllers
{
    public class TechnicalController : Controller
    {
        //Lab Sample management
        public ActionResult LabSampleManagementList()
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                return View();
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult LabSampleManagementListPartialView()
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var SampleList = new List<LabSampleManagement>();


                //var page = $"LabSampleManagement?$filter=Created_By eq '{StaffNo}'&$format=json";
                var page = $"LabSampleManagement?$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var sample = new LabSampleManagement
                        {
                            Sample_ID = (string)config["Sample_ID"],
                            Description = (string)config["Description"],
                            Received_At = (string)config["Received_At"],
                            Analysed_At = (string)config["Analysed_At"],
                            Exported_At = (string)config["Exported_At"],
                            TurnAround_Time = (string)config["TurnAround_Time"],
                            Status = (string)config["Status"],
                            Created_By = (string)config["Created_By"],
                        };

                        SampleList.Add(sample);
                    }
                }
                return PartialView("~/Views/Technical/PartialViews/LabSampleManagementListPartialView.cshtml", SampleList.OrderByDescending(x => x.Sample_ID));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error
                {
                    Message = ex.Message.Replace("'", "")
                };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [HttpPost]
        public ActionResult LabSampleManagementDocumentView(string Sample_ID)
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                var StaffNo = Session["Username"].ToString();
                var LabSample = new LabSampleManagement();
                var page = "LabSampleManagement?$filter=Sample_ID eq '" + Sample_ID + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        LabSample = new LabSampleManagement
                        {
                            Sample_ID = (string)config["Sample_ID"],
                            Description = (string)config["Description"],
                            Received_At = (string)config["Received_At"],
                            Analysed_At = (string)config["Analysed_At"],
                            Exported_At = (string)config["Exported_At"],
                            TurnAround_Time = (string)config["TurnAround_Time"],
                            Status = (string)config["Status"],
                            Created_By = (string)config["Created_By"],
                        };
                    }
                }

                return View(LabSample);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error
                {
                    Message = ex.Message.Replace("'", "")
                };
                return View("~/Views/Common/ErrorMessange.cshtml", erroMsg);
            }
        }
        public PartialViewResult LabSampleManagementLinesPartialView(string Sample_ID, string Status)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var LabSampleLines = new List<LabSampleMgtLines>();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = $"LabSampleMgtLines?$filter=Sample_ID eq '{Sample_ID}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var LabSampleLine = new LabSampleMgtLines
                        {
                            Line_No = (int)config["Line_No"],
                            Sample_ID = (string)config["Sample_ID"],
                            Sample_Type = (string)config["Sample_Type"],
                            Source = (string)config["Source"],
                            Staff_No = (string)config["Staff_No"],
                            Staff_Name = (string)config["Staff_Name"],
                            Quantity_ml = (int)config["Quantity_ml"],
                            Storage_Location = (string)config["Storage_Location"],
                            Notes = (string)config["Notes"],
                        };



                        LabSampleLines.Add(LabSampleLine);
                    }
                }
                ViewBag.DocNo = Sample_ID;
                ViewBag.Status = Status;
                return PartialView("~/Views/Technical/PartialViews/LabSampleManagementLinesPartialView.cshtml", LabSampleLines.OrderByDescending(x => x.Line_No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error
                {
                    Message = ex.Message.Replace("'", "")
                };
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public ActionResult NewLabSampleManagement()
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var labSample = new LabSampleManagement();

                #region Employees
                var EmpleyeeList = new List<DropdownList>();
                var pageWp = $"EmployeeList?$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["FullName"]} - {(string)config1["No"]}",
                            Value = (string)config1["No"]
                        };
                        EmpleyeeList.Add(dropdownList);
                    }
                }
                #endregion

                //labSample.ListOfEmployees = EmpleyeeList.Select(x =>
                //    new SelectListItem
                //    {
                //        Text = x.Text,
                //        Value = x.Value
                //    }).ToList();

                return PartialView("~/Views/Technical/PartialViews/NewLabSampleManagement.cshtml", labSample);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitLabSampleManagement(LabSampleManagement labSample)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                DateTime receivedAt = DateTime.ParseExact(labSample.Received_At.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime analysedAt = DateTime.ParseExact(labSample.Analysed_At.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime exportedAt = DateTime.ParseExact(labSample.Exported_At.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);


                string res2 = "";
                res2 = Credentials.ObjNav.InsertLabSample(
                    labSample.Description,
                    receivedAt,
                    analysedAt,
                    exportedAt,
                    UserID
                );

                if (res2 != "")
                {
                    var redirect = res2;

                    return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var redirect = "Error adding record. Try again";
                    return Json(new { message = redirect, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult NewLabSampleLine(string Sample_ID)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var labSampleLines = new LabSampleMgtLines();

                labSampleLines.Sample_ID = Sample_ID;

                #region Employees
                var EmpleyeeList = new List<DropdownList>();
                var pageWp = $"EmployeeList?$format=json";
                var httpResponseWp = Credentials.GetOdataData(pageWp);
                using (var streamReader = new StreamReader(httpResponseWp.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["FullName"]} - {(string)config1["No"]}",
                            Value = (string)config1["No"]
                        };
                        EmpleyeeList.Add(dropdownList);
                    }
                }
                #endregion

                //labSampleLines.ListOfEmployees = EmpleyeeList.Select(x =>
                //    new SelectListItem
                //    {
                //        Text = x.Text,
                //        Value = x.Value
                //    }).ToList();


                return PartialView("~/Views/Technical/PartialViews/NewLabSampleLine.cshtml", labSampleLines);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitLabSampleLine(LabSampleMgtLines labSampleLine)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;


                bool res = false;
                int res2 = Credentials.ObjNav.InsertSampleLine(
                    labSampleLine.Sample_ID,
                    labSampleLine.Sample_Type,
                    labSampleLine.Source,
                    StaffNo,
                    labSampleLine.Quantity_ml,
                    labSampleLine.Storage_Location,
                    labSampleLine.Notes

                );

                if (res2 != 0)
                {
                    var redirect = res;
                    return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var redirect = "Error adding record. Try again";
                    return Json(new { message = redirect, success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteLabSampleLine(LabSampleMgtLines labSampleLine)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.DeleteSampleLine(
                    labSampleLine.Sample_ID,
                    labSampleLine.Line_No
                );
                return Json(new { message = "Record successfully deleted", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SendLabSampleDocForApproval(string Sample_ID)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.SendLabSampleMgtForApproval(Sample_ID);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(50227, Sample_ID, UserID);
                
                return Json(new { message = "Document successfully sent for approval", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelLabSampleDocApproval(string Sample_ID)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.CancelLabSampleMgtForApproval(Sample_ID);
               
                return Json(new { message = "Document approval request successfully cancelled", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }



    }
}