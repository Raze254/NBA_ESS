using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Latest_Staff_Portal.Controllers
{
    public class FleetController : Controller
    {
        // Transport Requisition
        public ActionResult TransportRequisitionList()
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
        public PartialViewResult TransportRequisitionListPartialView()
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var FleetList = new List<FleetRequests>();

                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = $"FleetRequisitionCard?$filter=Requested_By eq '{StaffNo}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var ImList = new FleetRequests
                        {
                            Transport_Requisition_No = (string)config["Transport_Requisition_No"],
                            Requested_By = (string)config["Requested_By"],
                            Requested_By_Name = (string)config["Requested_By_Name"],
                            Position = (string)config["Position"],
                            Approved_Imprest_Memo = (string)config["Approved_Imprest_Memo"],
                            From = (string)config["From"],
                            Destination = (string)config["Destination"],
                            Route_Description = (string)config["Route_Description"],
                            Date_of_Trip = (string)config["Date_of_Trip"],
                            Time_Requested = (string)config["Time_Requested"],
                            Time_out = (string)config["Time_out"],
                            Time_In = (string)config["Time_In"],
                            Journey_Route = (string)config["Journey_Route"],
                            Opening_Odometer_Reading = (int?)config["Opening_Odometer_Reading"] ?? 0,
                            Closing_Odometer_Reading = (int?)config["Closing_Odometer_Reading"] ?? 0,
                            No_of_Days_Requested = (int?)config["No_of_Days_Requested"] ?? 0,
                            Number_of_Hours_Requested = (int?)config["Number_of_Hours_Requested"] ?? 0,
                            Trip_End_Date = (string)config["Trip_End_Date"],
                            Trip_End_Time = (string)config["Trip_End_Time"],
                            Status = (string)config["Status"],
                            Responsibility_Center = (string)config["Responsibility_Center"],
                            Work_Ticket_No = (string)config["Work_Ticket_No"],
                            User_Id = (string)config["User_Id"],
                            HOD = (bool?)config["HOD"] ?? false,
                            Added_On = (DateTime?)config["Added_On"] ?? DateTime.MinValue,
                            Date_of_Request = (string)config["Date_of_Request"],
                            Vehicle_Allocated = (string)config["Vehicle_Allocated"],
                            Vehicle_Allocated_by = (string)config["Vehicle_Allocated_by"],
                            Driver_Allocated = (string)config["Driver_Allocated"],
                            Driver_Name = (string)config["Driver_Name"],
                            Subject = (string)config["Subject"],
                            Comments = (string)config["Comments"],
                            Fuel_Request_Code = (string)config["Fuel_Request_Code"],
                            Reason_for_Reopening = (string)config["Reason_for_Reopening"],
                            Reg_No = (string)config["Reg_No"],
                            Model = (string)config["Model"],
                            Officer_Taking_Over = (string)config["Officer_Taking_Over"],
                            Immediate_Former = (string)config["Immediate_Former"],
                            Spare_Wheel = (bool?)config["Spare_Wheel"] ?? false,
                            Wheel_Spammer = (bool?)config["Wheel_Spammer"] ?? false,
                            Hydraulic_Jack = (bool?)config["Hydraulic_Jack"] ?? false,
                            Radio = (bool?)config["Radio"] ?? false,
                            Wheel_Caps = (string)config["Wheel_Caps"],
                            Side_Mirrors = (bool?)config["Side_Mirrors"] ?? false,
                            Fuel_Card_No = (int?)config["Fuel_Card_No"] ?? 0,
                            Floor_Mats = (bool?)config["Floor_Mats"] ?? false,
                            Body_Condition = (string)config["Body_Condition"],
                            Current_Mileage = (int?)config["Current_Mileage"] ?? 0,
                            Tyre_Condition = (string)config["Tyre_Condition"],
                            Logbook = (bool?)config["Logbook"] ?? false,
                            Observations = (string)config["Observations"]

                        };

                        FleetList.Add(ImList);
                    }
                }

                return PartialView("~/Views/Fleet/PartialViews/TransportRequisitionListPartialView.cshtml", FleetList.OrderByDescending(x => x.Transport_Requisition_No));
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
        public ActionResult NewTransportRequisition()
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

                var fleetRequisition = new FleetRequests();

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

                #region ImprestMemo
                var ImprestMemoList = new List<DropdownList>();
                var pageImprestMemo = $"AllImprestMemos?$filter= Status eq 'Released' and Requestor eq '{StaffNo}'&$format=json";
                var httpResponseImprestMemo = Credentials.GetOdataData(pageImprestMemo);
                using (var streamReader = new StreamReader(httpResponseImprestMemo.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["No"]}",
                            Value = (string)config1["No"]
                        };
                        ImprestMemoList.Add(dropdownList);
                    }
                }
                #endregion

                #region ResponsibilityCenters
                var ResponsibilityCentersList = new List<DropdownList>();
                var pageResponsibilityCenters = $"ResponsibilityCenters?$format=json";
                var httpResponseResponsibilityCenters = Credentials.GetOdataData(pageResponsibilityCenters);
                using (var streamReader = new StreamReader(httpResponseResponsibilityCenters.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["Code"]}",
                            Value = (string)config1["Code"]
                        };
                        ResponsibilityCentersList.Add(dropdownList);
                    }
                }
                #endregion


                fleetRequisition.ListOfEmployees = EmpleyeeList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();


                fleetRequisition.ListOfEmployees = EmpleyeeList.Select(x =>
                   new SelectListItem
                   {
                       Text = x.Text,
                       Value = x.Value
                   }).ToList();

                fleetRequisition.ListOfImprestMemo = ImprestMemoList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();

                fleetRequisition.ListOfResponsibilityCenters = ResponsibilityCentersList.Select(x =>
                 new SelectListItem
                 {
                     Text = x.Text,
                     Value = x.Value
                 }).ToList();

                return PartialView("~/Views/Fleet/PartialViews/NewTransportRequisition.cshtml",
                    fleetRequisition);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitTransportRequisition(FleetRequests fleetRequisition)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                DateTime Date_of_Trip = DateTime.ParseExact(fleetRequisition.Date_of_Trip.Replace("-", "/"), "dd/MM/yyyy",
                       CultureInfo.InvariantCulture);
                DateTime Time_out = DateTime.Parse(fleetRequisition.Time_Requested);

                var memo = fleetRequisition.Approved_Imprest_Memo;
                if (memo == null)
                {
                    memo = "";
                }

                bool res = false;
                string res2 = Credentials.ObjNav.createFleetRequisition(
                    StaffNo,
                    "",//requisitionNo
                    fleetRequisition.From, //commencement
                    fleetRequisition.Destination,
                    Date_of_Trip,
                    Time_out,
                    fleetRequisition.Route_Description,
                    fleetRequisition.No_of_Days_Requested,
                    "",//purpose of trip
                    "",//comments
                    memo,
                    fleetRequisition.Number_of_Hours_Requested,
                    1, //requisition Type
                    fleetRequisition.Route_Description,
                    fleetRequisition.Capacity
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
        public ActionResult TransportRequisitionDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                var StaffNo = Session["Username"].ToString();
                var FleetDoc = new FleetRequests();
                var page = "FleetRequisitionCard?$filter=Transport_Requisition_No eq '" + DocNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        FleetDoc = new FleetRequests
                        {
                            Transport_Requisition_No = (string)config["Transport_Requisition_No"],
                            Requested_By = (string)config["Requested_By"],
                            Requested_By_Name = (string)config["Requested_By_Name"],
                            Position = (string)config["Position"],
                            Approved_Imprest_Memo = (string)config["Approved_Imprest_Memo"],
                            From = (string)config["From"],
                            Destination = (string)config["Destination"],
                            Route_Description = (string)config["Route_Description"],
                            Date_of_Trip = (string)config["Date_of_Trip"],
                            Time_Requested = (string)config["Time_Requested"],
                            Time_out = (string)config["Time_out"],
                            Time_In = (string)config["Time_In"],
                            Journey_Route = (string)config["Journey_Route"],
                            Opening_Odometer_Reading = (int?)config["Opening_Odometer_Reading"] ?? 0,
                            Closing_Odometer_Reading = (int?)config["Closing_Odometer_Reading"] ?? 0,
                            No_of_Days_Requested = (int?)config["No_of_Days_Requested"] ?? 0,
                            Number_of_Hours_Requested = (int?)config["Number_of_Hours_Requested"] ?? 0,
                            Trip_End_Date = (string)config["Trip_End_Date"],
                            Trip_End_Time = (string)config["Trip_End_Time"],
                            Status = (string)config["Status"],
                            Responsibility_Center = (string)config["Responsibility_Center"],
                            Work_Ticket_No = (string)config["Work_Ticket_No"],
                            User_Id = (string)config["User_Id"],
                            HOD = (bool?)config["HOD"] ?? false,
                            Added_On = (DateTime?)config["Added_On"] ?? DateTime.MinValue,
                            Date_of_Request = (string)config["Date_of_Request"],
                            Vehicle_Allocated = (string)config["Vehicle_Allocated"],
                            Vehicle_Allocated_by = (string)config["Vehicle_Allocated_by"],
                            Driver_Allocated = (string)config["Driver_Allocated"],
                            Driver_Name = (string)config["Driver_Name"],
                            Subject = (string)config["Subject"],
                            Comments = (string)config["Comments"],
                            Fuel_Request_Code = (string)config["Fuel_Request_Code"],
                            Reason_for_Reopening = (string)config["Reason_for_Reopening"],
                            Reg_No = (string)config["Reg_No"],
                            Model = (string)config["Model"],
                            Officer_Taking_Over = (string)config["Officer_Taking_Over"],
                            Immediate_Former = (string)config["Immediate_Former"],
                            Spare_Wheel = (bool?)config["Spare_Wheel"] ?? false,
                            Wheel_Spammer = (bool?)config["Wheel_Spammer"] ?? false,
                            Hydraulic_Jack = (bool?)config["Hydraulic_Jack"] ?? false,
                            Radio = (bool?)config["Radio"] ?? false,
                            Wheel_Caps = (string)config["Wheel_Caps"],
                            Side_Mirrors = (bool?)config["Side_Mirrors"] ?? false,
                            Fuel_Card_No = (int?)config["Fuel_Card_No"] ?? 0,
                            Floor_Mats = (bool?)config["Floor_Mats"] ?? false,
                            Body_Condition = (string)config["Body_Condition"],
                            Current_Mileage = (int?)config["Current_Mileage"] ?? 0,
                            Tyre_Condition = (string)config["Tyre_Condition"],
                            Logbook = (bool?)config["Logbook"] ?? false,
                            Observations = (string)config["Observations"]
                        };
                    }
                }

                return View(FleetDoc);
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
        public PartialViewResult TransportRequisitionLinesPartialView(string DocNo, string Status)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var FleetLines = new List<FleetRequestLines>();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = $"TransportPassengers?$filter=Req_No eq '{DocNo}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var FleetLine = new FleetRequestLines
                        {
                            Req_No = (string)config["Req_No"],
                            Employee_No = (string)config["Employee_No"],
                            Employee_Name = (string)config["Employee_Name"],
                            Date_of_Trip = (string)config["Date_of_Trip"],
                            No_of_Days_Requested = (int)config["No_of_Days_Requested"],
                            Trip_End_Date = (string)config["Trip_End_Date"],
                            Trip_End_Time = (string)config["Trip_End_Time"],
                        };



                        FleetLines.Add(FleetLine);
                    }
                }
                ViewBag.DocNo = DocNo;
                ViewBag.Status = Status;
                return PartialView("~/Views/Fleet/PartialViews/TransportRequisitionLinesPartialView.cshtml", FleetLines.OrderByDescending(x => x.Req_No));
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
        public PartialViewResult TransportRequisitionItemsPartialView(string DocNo, string Status)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var FleetItems = new List<FleetRequestItems>();

                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = $"FleetRequisitionItems?$filter=Requisition_Number eq '{DocNo}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var FleetItem = new FleetRequestItems
                        {
                            Ticket_No = (int)config["Ticket_No"],
                            Requisition_Number = (string)config["Requisition_Number"],
                            Item_Type = (string)config["Item_Type"],
                            Description = (string)config["Description"],
                            Serial_Number = (string)config["Serial_Number"],
                            Quantity = (int)config["Quantity"],
                            Purpose = (string)config["Purpose"],
                            Returnable = (bool)config["Returnable"],
                            Item_Return_Date = (string)config["Item_Return_Date"],
                            Remarks_Comments = (string)config["Remarks_Comments"],
                        };

                        FleetItems.Add(FleetItem);
                    }
                }
                ViewBag.Status = Status;
                ViewBag.DocNo = DocNo;

                return PartialView("~/Views/Fleet/PartialViews/TransportRequisitionItemsPartialView.cshtml", FleetItems.OrderByDescending(x => x.Ticket_No));
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
        public ActionResult NewTransportRequisitionLine(string docNo)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var fleetRequisition = new FleetRequestLines();



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
                            Text = (string)config1["First_Name"] + " " + (string)config1["Middle_Name"] + " " + (string)config1["Last_Name"] + " (" + (string)config1["No"] + ")",
                            Value = (string)config1["No"]
                        };
                        EmpleyeeList.Add(dropdownList);
                    }
                }
                #endregion

                fleetRequisition.ListOfEmployees = EmpleyeeList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                return PartialView("~/Views/Fleet/PartialViews/NewTransportRequisitionLine2.cshtml",
                    fleetRequisition);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitTransportRequisitionLine(FleetRequestLines fleetRequisition)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                var docNo = fleetRequisition.Req_No;
                /* DateTime Date_of_Trip = DateTime.ParseExact(fleetRequisition.Date_of_Trip.Replace("-", "/"), "dd/MM/yyyy",
                         CultureInfo.InvariantCulture);*/

                Credentials.ObjNav.AddTravellingStaff(
                    docNo,
                    StaffNo
                 );

                return Json(new { message = "Record successfully submitted", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteTransportRequisitionLine(string DocNo, string Employee_No)
        {
            try
            {


                Credentials.ObjNav.removeStaffFromTravelRequisition(Employee_No, DocNo, 1);


                return Json(new { message = "Record successfully submitted", success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult NewTransportRequisitionItem(string docNo)
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var fleetRequisitionItems = new FleetRequestItems();

                #region ItemCategories
                var ItemCategoriesList = new List<DropdownList>();
                var pageWp = $"ItemCategories?$format=json";
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
                            Text = $"{(string)config1["Description"]} - {(string)config1["Code"]}",
                            Value = (string)config1["Code"]
                        };
                        ItemCategoriesList.Add(dropdownList);
                    }
                }
                #endregion


                fleetRequisitionItems.ListOfItemCategories = ItemCategoriesList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();

                return PartialView("~/Views/Fleet/PartialViews/NewTransportRequisitionItem.cshtml",
                    fleetRequisitionItems);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitTransportRequisitionItem(FleetRequestItems fleetRequisitionItem)
        {
            try
            {
                var docNo = fleetRequisitionItem.Requisition_Number;
                int res;
                res = Credentials.ObjNav.AddFleetLines(docNo, fleetRequisitionItem.Item_Type, fleetRequisitionItem.Description, fleetRequisitionItem.Quantity, fleetRequisitionItem.Purpose);

                if (res > 0)
                {
                    var redirect = docNo;
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
        public JsonResult DeleteTransportRequisitionItem(string DocNo, string Ticket_No)
        {
            try
            {

                int res;
                res = Credentials.ObjNav.DeleteFleetLines(DocNo, int.Parse(Ticket_No), "", "", 0, "");

                if (res == 1)
                {
                    var redirect = "record successfully deleted";

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
        public JsonResult SendTransportReqDocForApproval(string DocNo)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                Credentials.ObjNav.SendtransportReqApprovalRequest(DocNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(59003, DocNo, UserID);
                var redirect = "Record successfully sent for approval";
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelTransportReqDocApproval(string DocNo)
        {
            try
            {


                Credentials.ObjNav.CanceltransportReqApprovalRequest(DocNo);
                var redirect = "Approval successfully cancelled";
                return Json(new { message = redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMemoDetails(string memo)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                var fleetRequisition = new FleetRequests();

                #region ImprestMemo
                var pageImprestMemo = $"AllImprestMemos?$filter=No eq '{memo}'&$format=json";
                var httpResponseImprestMemo = Credentials.GetOdataData(pageImprestMemo);

                using (var streamReader = new StreamReader(httpResponseImprestMemo.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    // Assuming only one memo will match
                    var memoDetails = new List<object>();

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;

                        var Start_Date = (string)config1["Start_Date"];
                        var No_of_days = (string)config1["No_of_days"];

                        memoDetails.Add(new
                        {
                            Start_Date,
                            No_of_days
                        });
                    }

                    return Json(new { message = memoDetails, success = true }, JsonRequestBehavior.AllowGet);
                }
                #endregion
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return Json(erroMsg, JsonRequestBehavior.AllowGet);
            }
        }



        // Maintenance requisition
        public ActionResult MaintenanceRequisitionList()
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
        public PartialViewResult MaintenanceRequisitionListPartialView()
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var MaintenanceList = new List<MaintenanceRequest2>();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = $"MaintenanceRequest?$filter=Requested_By eq '{StaffNo}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var Maintenance = new MaintenanceRequest2
                        {
                            Requisition_No = (string)config["Requisition_No"],
                            Vehicle_Reg_No = (string)config["Vehicle_Reg_No"],
                            Cost_Center_Name = (string)config["Cost_Center_Name"],
                            Vehicle_Location = (string)config["Vehicle_Location"],
                            Odometer_Reading = (int?)config["Odometer_Reading"] ?? 0,
                            Requested_By = (string)config["Requested_By"],
                            Department_Name = (string)config["Department_Name"],
                            Unit_Name = (string)config["Unit_Name"],
                            Vendor_Dealer = (string)config["Vendor_Dealer"],
                            Vendor_Name = (string)config["Vendor_Name"],
                            Responsible_Employee = (string)config["Responsible_Employee"],
                            Responsible_Employee_Name = (string)config["Responsible_Employee_Name"],
                            Request_Date = (string)config["Request_Date"],
                            Description = (string)config["Description"],
                            Service_Code = (string)config["Service_Code"],
                            Service_Name = (string)config["Service_Name"],
                            Status = (string)config["Status"],
                            Make = (string)config["Make"],
                            Model = (string)config["Model"],
                            Driver = (string)config["Driver"],
                            Driver_Name = (string)config["Driver_Name"],
                            Prepared_By = (string)config["Prepared_By"],
                            Closed_By = (string)config["Closed_By"],
                            Date_Closed = (string)config["Date_Closed"],
                            Vendor_Invoice_No = (string)config["Vendor_Invoice_No"],
                            Project_Number = (string)config["Project_Number"],
                            Task_Number = (string)config["Task_Number"],
                            Maintenance_Cost = (int?)config["Maintenance_Cost"] ?? 0,
                            Comments_Remarks = (string)config["Comments_Remarks"],
                            Parts_Changed = (string)config["Parts_Changed"],
                            Pre_Repair_Inspection = (string)config["Pre_Repair_Inspection"],
                            Post_Repair_Inspection = (string)config["Post_Repair_Inspection"]


                        };

                        MaintenanceList.Add(Maintenance);
                    }
                }

                return PartialView("~/Views/Fleet/PartialViews/MaintenanceRequisitionListPartialView.cshtml", MaintenanceList.OrderByDescending(x => x.Requisition_No));
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
        public ActionResult NewMaintenanceRequisition()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var maintenanceRequisition = new MaintenanceRequest2();

                #region VehicleRegNo
                var VehiclesList = new List<DropdownList>();
                var pageV = $"FleetVehiclesList?$format=json";
                var httpResponseV = Credentials.GetOdataData(pageV);
                using (var streamReader = new StreamReader(httpResponseV.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = $"{(string)config1["Registration_No"]}",
                            Value = (string)config1["Registration_No"]
                        };
                        VehiclesList.Add(dropdownList);
                    }
                }
                #endregion



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
                            Text = (string)config1["First_Name"] + " " + (string)config1["Last_Name"] + " (" + (string)config1["No"] + ")",
                            Value = (string)config1["No"]
                        };
                        EmpleyeeList.Add(dropdownList);
                    }
                }
                #endregion

                #region Vendors
                var VendorsList = new List<DropdownList>();
                var pageVendors = $"VendorList?$filter= Vendor_Type eq 'Fleet'&$format=json";
                var httpResponseVendors = Credentials.GetOdataData(pageVendors);
                using (var streamReader = new StreamReader(httpResponseVendors.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Name"] + " - " + (string)config1["No"],
                            Value = (string)config1["No"]
                        };
                        VendorsList.Add(dropdownList);
                    }
                }
                #endregion

                #region ServiceItemLists
                var ServiceItemList = new List<DropdownList>();
                var pageServiceItem = $"ServiceItemLists?$format=json";
                var httpResponseServiceItem = Credentials.GetOdataData(pageServiceItem);
                using (var streamReader = new StreamReader(httpResponseServiceItem.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Service_Name"] + "-" + (string)config1["Service_Code"],
                            Value = (string)config1["Service_Code"]
                        };
                        ServiceItemList.Add(dropdownList);
                    }
                }
                #endregion


                maintenanceRequisition.ListOfVehicles = VehiclesList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();


                maintenanceRequisition.ListOfEmployees = EmpleyeeList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();


                maintenanceRequisition.ListOfVendors = VendorsList.Select(x =>
                   new SelectListItem
                   {
                       Text = x.Text,
                       Value = x.Value
                   }).ToList();

                maintenanceRequisition.ListOfServiceItem = ServiceItemList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();



                return PartialView("~/Views/Fleet/PartialViews/NewMaintenanceRequisition.cshtml",
                    maintenanceRequisition);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitMaintenanceRequisition(MaintenanceRequest2 maintenanceRequisition)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                maintenanceRequisition.Vendor_Dealer = "";
                /*var docNo = maintenanceRequisition.Requisition_No;*/
                bool res = false;
                string res2 = Credentials.ObjNav.AddVehicleMaintenanceRequestDetails(
                    StaffNo,
                    employeeView.GlobalDimension1Code,
                    maintenanceRequisition.Vehicle_Reg_No,
                    maintenanceRequisition.Odometer_Reading,
                     1, //for service Type
                    maintenanceRequisition.Description,
                    "",
                    "",//maintenanceRequisition.Project_Number, //null
                    "",
                    maintenanceRequisition.Maintenance_Cost.ToString(),
                    maintenanceRequisition.Vendor_Dealer,
                    maintenanceRequisition.Service_Code

                );

                if (res2 != "")
                {
                    string docNo = res2.Split('*').Last();
                    var redirect = docNo;

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
        public ActionResult MaintenanceRequisitionDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                var StaffNo = Session["Username"].ToString();
                var maintenanceDoc = new MaintenanceRequest2();
                var page = "MaintenanceRequest?$filter=Requisition_No eq '" + DocNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        maintenanceDoc = new MaintenanceRequest2
                        {
                            Requisition_No = (string)config["Requisition_No"],
                            Vehicle_Reg_No = (string)config["Vehicle_Reg_No"],
                            Cost_Center_Name = (string)config["Cost_Center_Name"],
                            Vehicle_Location = (string)config["Vehicle_Location"],
                            Odometer_Reading = (int?)config["Odometer_Reading"] ?? 0,
                            Requested_By = (string)config["Requested_By"],
                            Department_Name = (string)config["Department_Name"],
                            Unit_Name = (string)config["Unit_Name"],
                            Vendor_Dealer = (string)config["Vendor_Dealer"],
                            Vendor_Name = (string)config["Vendor_Name"],
                            Responsible_Employee = (string)config["Responsible_Employee"],
                            Responsible_Employee_Name = (string)config["Responsible_Employee_Name"],
                            Request_Date = (string)config["Request_Date"],
                            Description = (string)config["Description"],
                            Service_Code = (string)config["Service_Code"],
                            Service_Name = (string)config["Service_Name"],
                            Status = (string)config["Status"],
                            Make = (string)config["Make"],
                            Model = (string)config["Model"],
                            Driver = (string)config["Driver"],
                            Driver_Name = (string)config["Driver_Name"],
                            Prepared_By = (string)config["Prepared_By"],
                            Closed_By = (string)config["Closed_By"],
                            Date_Closed = (string)config["Date_Closed"],
                            Vendor_Invoice_No = (string)config["Vendor_Invoice_No"],
                            Project_Number = (string)config["Project_Number"],
                            Task_Number = (string)config["Task_Number"],
                            Maintenance_Cost = (int?)config["Maintenance_Cost"] ?? 0,
                            Comments_Remarks = (string)config["Comments_Remarks"],
                            Parts_Changed = (string)config["Parts_Changed"],
                            Pre_Repair_Inspection = (string)config["Pre_Repair_Inspection"],
                            Post_Repair_Inspection = (string)config["Post_Repair_Inspection"]

                        };
                    }
                }

                return View(maintenanceDoc);
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
        public JsonResult SendMaintenanceReqForApproval(string DocNo)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var res = "";
                var message = "";
                var success = false;
                res = Credentials.ObjNav.SendVehicleMaintenancetforApproval(DocNo);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(59005, DocNo, UserID);
                success = true;

                return Json(new { message, success }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancelMaintenanceReqApproval(string DocNo)
        {
            try
            {
                var res = "";
                var message = "";
                var success = false;
                res = Credentials.ObjNav.fnCancelMaintenanceRequistionApproval(DocNo);
                success = true;
                return Json(new { message, success }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        // Work Ticket
        public ActionResult WorkTicketList()
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
        public PartialViewResult WorkTicketListPartialView()
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var WorkTicketList = new List<MonthlyWorkTicketCard>();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                //var page = $"MonthlyWorkTicketCard?$filter=Created_By eq '{UserID}'&$format=json";
                var page = $"MonthlyWorkTicketCard?$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var ticket = new MonthlyWorkTicketCard
                        {
                            Daily_Work_Ticket = (string)config["Daily_Work_Ticket"],
                            Month_Date = (string)config["Month_Date"],
                            Month_Name = (string)config["Month_Name"],
                            Ticket_No = (string)config["Ticket_No"],
                            Previous_Work_Ticket_No = (string)config["Previous_Work_Ticket_No"],
                            Vehicle_Registration_No = (string)config["Vehicle_Registration_No"],
                            Status = (string)config["Status"],
                            Comments = (string)config["Comments"],
                            Department = (string)config["Department"],
                            Closed_by = (string)config["Closed_by"],
                            Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                            Date_Closed = (string)config["Date_Closed"],
                            Created_By = (string)config["Created_By"],
                            Date_Created = (string)config["Date_Created"],
                            Authorized_By = (string)config["Authorized_By"],
                            Authorized_By_Name = (string)config["Authorized_By_Name"],
                            Time_Created = (string)config["Time_Created"],
                            Total_Fuel_Carried_Forward = (int)(config["Total_Fuel_Carried_Forward"] ?? 0),
                            Total_Fuel_Drawn_Ltrs = (double)(config["Total_Fuel_Drawn_Ltrs"] ?? 0.0),
                            Total_Kilometers = (int)(config["Total_Kilometers"] ?? 0),
                            Total_Km_x0027_s_Covered = (int)(config["Total_Km_x0027_s_Covered"] ?? 0),
                            Total_Miles_Per_Litre_Fuel = (int)(config["Total_Miles_Per_Litre_Fuel"] ?? 0),
                            Total_Miles_Per_Ltr_Oil = (int)(config["Total_Miles_Per_Ltr_Oil"] ?? 0),
                            Total_Oil_Drawn_Ltrs = (int)(config["Total_Oil_Drawn_Ltrs"] ?? 0),
                            Defect = (string)config["Defect"],
                            Defect_Date = (string)config["Defect_Date"],
                            Action_Taken_Reported = (string)config["Action_Taken_Reported"]



                        };

                        WorkTicketList.Add(ticket);
                    }
                }

                return PartialView("~/Views/Fleet/PartialViews/WorkTicketListPartialView.cshtml", WorkTicketList.OrderByDescending(x => x.Daily_Work_Ticket));
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
        public ActionResult NewWorkTicket()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var workTicket = new MonthlyWorkTicketCard();

                #region PayPeriods
                var PayPeriodsList = new List<DropdownList>();
                var pagePP = $"PayPeriods?$format=json";
                var httpResponsePP = Credentials.GetOdataData(pagePP);
                using (var streamReader = new StreamReader(httpResponsePP.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Starting_Date"] + "-" + (string)config1["Name"],
                            Value = (string)config1["Starting_Date"]
                        };
                        PayPeriodsList.Add(dropdownList);
                    }
                }
                #endregion


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

                #region VehicleRegistration
                var VehicleRegList = new List<DropdownList>();
                var pageVehicleReg = $"FleetVehiclesList?$format=json";
                var httpResponseVehicleReg = Credentials.GetOdataData(pageVehicleReg);
                using (var streamReader = new StreamReader(httpResponseVehicleReg.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Registration_No"] + " - " + (string)config1["Description"],
                            Value = (string)config1["Registration_No"]
                        };
                        VehicleRegList.Add(dropdownList);
                    }
                }
                #endregion

                #region Regions
                var RegionsList = new List<DropdownList>();
                var pageRegions = $"DimensionValueList?$format=json";
                var httpResponseRegions = Credentials.GetOdataData(pageRegions);
                using (var streamReader = new StreamReader(httpResponseRegions.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Name"] + "-" + (string)config1["Code"],
                            Value = (string)config1["Code"]
                        };
                        RegionsList.Add(dropdownList);
                    }
                }
                #endregion

                workTicket.ListPayPeriods = PayPeriodsList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();




                workTicket.ListOfEmployees = EmpleyeeList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();


                workTicket.ListOfVehicleReg = VehicleRegList.Select(x =>
                   new SelectListItem
                   {
                       Text = x.Text,
                       Value = x.Value
                   }).ToList();

                workTicket.ListOfRegions = RegionsList.Select(x =>
                  new SelectListItem
                  {
                      Text = x.Text,
                      Value = x.Value
                  }).ToList();



                return PartialView("~/Views/Fleet/PartialViews/NewWorkTicket.cshtml",
                    workTicket);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitWorkTicket(MonthlyWorkTicketCard workTicket)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                /*DateTime Month_Date = DateTime.ParseExact(workTicket.Month_Date.Replace("-", "/"), "dd/MM/yyyy",
                       CultureInfo.InvariantCulture);*/
                DateTime Month_Date = DateTime.Parse(workTicket.Month_Date);

                bool res = false;
                string res2 = Credentials.ObjNav.InsertWorkTicketHeader(
                    "",
                    workTicket.Vehicle_Registration_No,
                    Month_Date,
                    workTicket.Ticket_No,
                    workTicket.Previous_Work_Ticket_No,
                    workTicket.Global_Dimension_1_Code,
                    workTicket.Authorized_By,
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
        public ActionResult WorkTicketDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                var StaffNo = Session["Username"].ToString();
                var WorkTicketDoc = new MonthlyWorkTicketCard();
                var page = "MonthlyWorkTicketCard?$filter=Daily_Work_Ticket eq '" + DocNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        WorkTicketDoc = new MonthlyWorkTicketCard
                        {
                            Daily_Work_Ticket = (string)config["Daily_Work_Ticket"],
                            Month_Date = (string)config["Month_Date"],
                            Month_Name = (string)config["Month_Name"],
                            Ticket_No = (string)config["Ticket_No"],
                            Previous_Work_Ticket_No = (string)config["Previous_Work_Ticket_No"],
                            Vehicle_Registration_No = (string)config["Vehicle_Registration_No"],
                            Status = (string)config["Status"],
                            Comments = (string)config["Comments"],
                            Department = (string)config["Department"],
                            Closed_by = (string)config["Closed_by"],
                            Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"],
                            Date_Closed = (string)config["Date_Closed"],
                            Created_By = (string)config["Created_By"],
                            Date_Created = (string)config["Date_Created"],
                            Authorized_By = (string)config["Authorized_By"],
                            Authorized_By_Name = (string)config["Authorized_By_Name"],
                            Time_Created = (string)config["Time_Created"],
                            Total_Fuel_Carried_Forward = (int)(config["Total_Fuel_Carried_Forward"] ?? 0),
                            Total_Fuel_Drawn_Ltrs = (double)(config["Total_Fuel_Drawn_Ltrs"] ?? 0.0),
                            Total_Kilometers = (int)(config["Total_Kilometers"] ?? 0),
                            Total_Km_x0027_s_Covered = (int)(config["Total_Km_x0027_s_Covered"] ?? 0),
                            Total_Miles_Per_Litre_Fuel = (int)(config["Total_Miles_Per_Litre_Fuel"] ?? 0),
                            Total_Miles_Per_Ltr_Oil = (int)(config["Total_Miles_Per_Ltr_Oil"] ?? 0),
                            Total_Oil_Drawn_Ltrs = (int)(config["Total_Oil_Drawn_Ltrs"] ?? 0),
                            Defect = (string)config["Defect"],
                            Defect_Date = (string)config["Defect_Date"],
                            Action_Taken_Reported = (string)config["Action_Taken_Reported"]

                        };
                    }
                }

                return View(WorkTicketDoc);
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
        public PartialViewResult WorkTicketLinesPartialView(string DocNo)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var WorkTicketLines = new List<DailyWorkTicketLines>();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var role = Session["ESSRoleSetup"] as ESSRoleSetup;
                var page = $"DailyWorkTicketLines?$filter=Daily_Work_Ticket eq '{DocNo}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var WTLIne = new DailyWorkTicketLines
                        {
                            Daily_Work_Ticket = (string)config["Daily_Work_Ticket"],
                            EntryNo = (int?)config["EntryNo"] ?? 0,
                            Transport_Requisition_No = (string)config["Transport_Requisition_No"],
                            Commencement = (string)config["Commencement"],
                            Destination = (string)config["Destination"],
                            Route_Description = (string)config["Route_Description"],
                            Date = (string)config["Date"],
                            Driver_Allocated = (string)config["Driver_Allocated"],
                            Driver_Name = (string)config["Driver_Name"],
                            Journey_Route = (string)config["Journey_Route"],
                            Oil_Drawn_Litres = (int?)config["Oil_Drawn_Litres"] ?? 0,
                            Fuel_Drawn_Litres = (decimal?)config["Fuel_Drawn_Litres"] ?? 0,
                            Order_No = (string)config["Order_No"],
                            Time_out = (string)config["Time_out"],
                            Time_In = (string)config["Time_In"],
                            Opening_Odometer_Reading = (int?)config["Opening_Odometer_Reading"] ?? 0,
                            Closing_Odometer_Reading = (int?)config["Closing_Odometer_Reading"] ?? 0,
                            Total_Kilometres = (int?)config["Total_Kilometres"] ?? 0,
                            Miles_Per_Litre_Fuel = (int?)config["Miles_Per_Litre_Fuel"] ?? 0,
                            Miles_Per_Litre_Oil = (int?)config["Miles_Per_Litre_Oil"] ?? 0,
                            Fuel_Carried_Forward = (int?)config["Fuel_Carried_Forward"] ?? 0,
                            Authorized_By = (string)config["Authorized_By"],
                            GOK_Officer = (string)config["GOK_Officer"],
                            Position = (string)config["Position"],
                            Defect_Date = (string)config["Defect_Date"],
                            Defect = (string)config["Defect"],
                            Action_Taken_Reported = (string)config["Action_Taken_Reported"]

                        };



                        WorkTicketLines.Add(WTLIne);
                    }
                }
                ViewBag.DocNo = DocNo;
                return PartialView("~/Views/Fleet/PartialViews/WorkTicketLinesPartialView.cshtml", WorkTicketLines.OrderByDescending(x => x.Daily_Work_Ticket));
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
        public JsonResult SubmitUpdatedWorkTicketLine(DailyWorkTicketLines data)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;

                var WorkTicketDate = DateTime.Today;
                var Defect_Date = DateTime.Today;
                DateTime Time_out = DateTime.Today;
                DateTime Time_In = DateTime.Today;

                if (!string.IsNullOrEmpty(data.Date))
                {
                    WorkTicketDate = DateTime.ParseExact(
                        data.Date.Replace("-", "/"),
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture
                    );
                }

                if (!string.IsNullOrEmpty(data.Date))
                {
                    Defect_Date = DateTime.ParseExact(
                        data.Defect_Date.Replace("-", "/"),
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture
                    );
                }

                if (!string.IsNullOrEmpty(data.Time_out))
                {
                    Time_out = DateTime.Today.Add(TimeSpan.Parse(data.Time_out));
                }

                if (!string.IsNullOrEmpty(data.Time_out))
                {
                    Time_In = DateTime.Today.Add(TimeSpan.Parse(data.Time_In));
                }


                var res2 = "";
                Credentials.ObjNav.ModifyWorkTicketLines1(
                    data.Daily_Work_Ticket,
                    data.EntryNo,
                    data.Oil_Drawn_Litres,
                    data.Fuel_Drawn_Litres,
                    Time_out,
                    Time_In,
                    data.Opening_Odometer_Reading,
                    data.Closing_Odometer_Reading,
                    data.Miles_Per_Litre_Fuel,
                    data.Miles_Per_Litre_Oil,
                    data.Fuel_Carried_Forward,
                     ""

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
        public JsonResult GetWorkTicketLines(string DocNo)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                Credentials.ObjNav.LoadTransportReq(DocNo);
                return Json(new { message = "Lines Loaded", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendWorkTicketDocForApproval(string Daily_Work_Ticket)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var success = false;
                Credentials.ObjNav.SendWorkTicketforApproval(Daily_Work_Ticket);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(59008, Daily_Work_Ticket, UserID);
                success = true;
                var message = "Document sent for approval";
                return Json(new { message, success }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancelWorkTicketDocApproval(string Daily_Work_Ticket)
        {
            try
            {

                var success = false;
                Credentials.ObjNav.CancelWorkTicketforApproval(Daily_Work_Ticket);
                var message = "Document approval request cancelled";
                success = true;
                return Json(new { message, success }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }



        // Fuel Requisition
        public ActionResult FuelRequisitionList()
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
        public PartialViewResult FuelRequisitionListPartialView()
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var FuelList = new List<FuelRequisitionCard>();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                var role = Session["ESSRoleSetup"] as ESSRoleSetup;

                var page = $"FuelRequisitionCard?$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (JObject config in details["value"])
                    {
                        var ticket = new FuelRequisitionCard
                        {
                            Requisition_No = (string)config["Requisition_No"],
                            Total_Price_of_Fuel = (int?)config["Total_Price_of_Fuel"] ?? 0,
                            Transport_Requisition_No = (string)config["Transport_Requisition_No"],
                            Route_Code = (string)config["Route_Code"],
                            Route_Description = (string)config["Route_Description"],
                            Request_Date = (string)config["Request_Date"],
                            Status = (string)config["Status"],
                            Prepared_By = (string)config["Prepared_By"],
                            Posted_Invoice_No = (string)config["Posted_Invoice_No"],
                            Job_No = (string)config["Job_No"],
                            Job_Task = (string)config["Job_Task"],
                            Job_Name = (string)config["Job_Name"],
                            Job_Task_Name = (string)config["Job_Task_Name"]

                        };

                        FuelList.Add(ticket);
                    }
                }

                return PartialView("~/Views/Fleet/PartialViews/FuelRequisitionListPartialView.cshtml", FuelList.OrderByDescending(x => x.Requisition_No));
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
        public ActionResult NewFuelRequisition()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                var fuelReq = new FuelRequisitionCard();



                #region TransportRequisitionNo
                var TransportRequisitionNoList = new List<DropdownList>();
                var pageTR = $"ClosedFleetRequestsList?$filter=Status eq 'Closed'&format=json";
                var httpResponseTR = Credentials.GetOdataData(pageTR);
                using (var streamReader = new StreamReader(httpResponseTR.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);

                    foreach (var jToken in details["value"])
                    {
                        var config1 = (JObject)jToken;
                        var dropdownList = new DropdownList
                        {
                            Text = (string)config1["Transport_Requisition_No"],
                            Value = (string)config1["Transport_Requisition_No"]
                        };
                        TransportRequisitionNoList.Add(dropdownList);
                    }
                }
                #endregion



                fuelReq.ListOfTransportRequisitions = TransportRequisitionNoList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList();




                return PartialView("~/Views/Fleet/PartialViews/NewFuelRequisition.cshtml",
                    fuelReq);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }
        public JsonResult SubmitFuelRequisition(FuelRequisitionCard fuelReq)
        {
            try
            {
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                /*DateTime Request_Date = DateTime.ParseExact(fuelReq.Request_Date.Replace("-", "/"), "dd/MM/yyyy",
                       CultureInfo.InvariantCulture);
*/

                //DateTime Start_Date = DateTime.Parse(fuelReq.Start_Date);

                string DocNo = "";
                DocNo = Credentials.ObjNav.InsertFuelReq(
                    fuelReq.Total_Price_of_Fuel,
                    fuelReq.Transport_Requisition_No,
                    UserID

                );

                if (DocNo != "")
                {
                    var redirect = DocNo;

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
        public ActionResult FuelRequisitionDocumentView(string DocNo)
        {
            try
            {
                if (Session["Username"] == null)
                    return RedirectToAction("Login", "Login");
                var StaffNo = Session["Username"].ToString();
                var FuelDoc = new FuelRequisitionCard();
                var page = "FuelRequisitionCard?$filter=Requisition_No eq '" + DocNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        FuelDoc = new FuelRequisitionCard
                        {
                            Requisition_No = (string)config["Requisition_No"],
                            Total_Price_of_Fuel = (int?)config["Total_Price_of_Fuel"] ?? 0,
                            Transport_Requisition_No = (string)config["Transport_Requisition_No"],
                            Route_Code = (string)config["Route_Code"],
                            Route_Description = (string)config["Route_Description"],
                            Request_Date = (string)config["Request_Date"],
                            Status = (string)config["Status"],
                            Prepared_By = (string)config["Prepared_By"],
                            Posted_Invoice_No = (string)config["Posted_Invoice_No"],
                            Job_No = (string)config["Job_No"],
                            Job_Task = (string)config["Job_Task"],
                            Job_Name = (string)config["Job_Name"],
                            Job_Task_Name = (string)config["Job_Task_Name"]
                        };
                    }
                }
                return View(FuelDoc);
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
        public JsonResult SendFuelReqForApproval(string Requisition_No)
        {
            try
            {
                var res = "";
                var message = "";
                var success = false;
                var UserID = Session["UserID"].ToString();
                var StaffNo = Session["Username"].ToString();
                var employeeView = Session["EmployeeData"] as EmployeeView;
                res = Credentials.ObjNav.fnsendFuelRequistionForApproval(Requisition_No);
                Credentials.ObjNav.UpdateApprovalEntrySenderID(59005, Requisition_No, UserID);
                success = true;

                return Json(new { message, success }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancelFuelReqDocApproval(string Requisition_No)
        {
            try
            {
                var res = "";
                var message = "";
                var success = false;
                res = Credentials.ObjNav.fnCancelFuelRequistionApproval(Requisition_No);
                success = true;
                return Json(new { message, success }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}