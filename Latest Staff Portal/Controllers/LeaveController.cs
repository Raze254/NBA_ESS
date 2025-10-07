
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json.Linq;


namespace Latest_Staff_Portal.Controllers
{
    [CustomeAuthentication]

    public class LeaveController : Controller
    {
        // GET: Leave
        public ActionResult LeaveRequisitionList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult LeaveRequisitionListPartialView()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();
                var LeaveList = new List<LeaveReqList>();

                var page = "LeaveApplication?$filter=Employee_No eq '" + StaffNo + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var LvList = new LeaveReqList();
                        LvList.No = (string)config["Application_Code"];
                        LvList.Leave_Type = (string)config["Leave_Type"];
                        LvList.Applied_Days = (string)config["Days_Applied"];
                        LvList.Date = Convert.ToDateTime((string)config["Application_Date"]).ToString("dd/MM/yyyy");
                        LvList.Starting_Date = Convert.ToDateTime((string)config["Start_Date"]).ToString("dd/MM/yyyy");
                        LvList.End_Date = Convert.ToDateTime((string)config["End_Date"]).ToString("dd/MM/yyyy");
                        LvList.Return_Date = Convert.ToDateTime((string)config["Return_Date"]).ToString("dd/MM/yyyy");
                        LvList.Reliever = (string)config["Reliever_Name"];
                        LvList.Status = (string)config["Status"];
                        LeaveList.Add(LvList);
                    }
                }

                return PartialView("~/Views/Leave/LeaveReqListPartialView.cshtml",
                    LeaveList.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public PartialViewResult NewLeaveApplication()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var NewAppl = new NewLeaveApplication();
                var employee = Session["EmployeeData"] as EmployeeView;

                var Department = "";
                Department = CommonClass.EmployeeDepartment(StaffNo);
                if (Department == "")
                {
                    var erroMsg = new Error();
                    erroMsg.Message = "Your Administrative Unit has not been set. Contact HR";
                    return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
                }

                #region Implementing Unit List

                var Dim1List = new List<ImplementingUnitsList>();
                var pageDepartment = "DimensionValues?$filter=Dimension_Code eq 'DEPARTMENT'&$orderby=Name asc&format=json";
                var httpResponseDepartment = Credentials.GetOdataData(pageDepartment);
                using (var streamReader = new StreamReader(httpResponseDepartment.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var Impunit = new ImplementingUnitsList();
                        Impunit.Code = (string)config["Code"];
                        Impunit.Name = (string)config["Name"];
                        Dim1List.Add(Impunit);
                    }
                }

                #endregion

                var UserID = Session["UserID"].ToString();

                //var approverId = CommonClass.GetEmployeeApproverUserID(UserID);
                var approver = CommonClass.GetEmployeeNameByUserID(employee.Supervisor);

                #region LeaveTypes

                var leaveTps = new List<LvTypes>();

                var gender = CommonClass.GetEmployeeGender(StaffNo);
                if (string.IsNullOrEmpty(gender.Trim()))
                {
                    var erroMsg = new Error();
                    erroMsg.Message = "Your gender has not been set. Contact HR";
                    return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
                }

                if (string.IsNullOrEmpty(approver.Trim()))
                {
                    var erroMsg = new Error();
                    erroMsg.Message = "Your Supervisor has not been set. Contact HR";
                    return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
                }

                var page = "LeaveTypes?$filter=(Gender eq 'Both' or Gender eq '" + gender + "')&format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var LTpe = new LvTypes();
                        LTpe.Code = (string)config["Code"];
                        LTpe.Description = (string)config["Description"];
                        leaveTps.Add(LTpe);
                    }
                }

                #endregion

                #region ReliverList

                var relieverList = new List<RelieverList>();
                var pageReliever = "EmployeeList?$filter=No ne '" + StaffNo +
                                   "' and Status eq 'Active' and Global_Dimension_2_Code eq '" + Department +
                                   "'&$orderby=First_Name asc&format=json";

                var httpResponseReliever = Credentials.GetOdataData(pageReliever);
                using (var streamReader = new StreamReader(httpResponseReliever.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                        if ((string)config["First_Name"] != "")
                        {
                            var Rlist = new RelieverList();
                            Rlist.No = (string)config["No"];
                            Rlist.Name = (string)config["First_Name"] + " " + (string)config["Middle_Name"] + " " +
                                         (string)config["Last_Name"];
                            relieverList.Add(Rlist);
                        }
                }

                #endregion

                #region AdoptionLeave

                var AdoptionLeaveList = new List<DropdownList>();
                //string adpLeave = "AdoptionLeaveSetU?$format=json";
                var adpLeave = "AdoptionLeaveSetUp?$filter=Gender eq  '" + gender + "'&format=json";

                var httpResponseDriver = Credentials.GetOdataData(adpLeave);
                using (var streamReader = new StreamReader(httpResponseDriver.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var adpt = new DropdownList();
                        adpt.Value = (string)config["Code"];
                        adpt.Text = (string)config["Min_Age"] + "-" + (string)config["Max_Age"] +
                                    " Years"; //+ $"({(string)config["Days"]} Days)";
                        AdoptionLeaveList.Add(adpt);
                    }
                }

                #endregion

                NewAppl = new NewLeaveApplication
                {
                    LeaveBal = "0",
                    AllocatedDays = "0",
                    ReimbDays = "0",
                    LeaveTaken = "0",
                    EarnedLeaveDays = "0",
                    implementingUnit = Department,
                    SupervisorName = approver,
                    ListOfLeaveTypes = leaveTps.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Description,
                            Value = x.Code
                        }).ToList(),
                    ListOfImplementingUnits = Dim1List.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Code
                        }).ToList(),

                    ListOfRelievers = relieverList.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.No
                        }).ToList(),
                    ListOfChildsAge = AdoptionLeaveList.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Text,
                            Value = x.Value
                        }).ToList()
                };
                ViewBag.Phone = employee.Mobile_Phone_No;
                //ViewBag.Email = employee.Email;
                return PartialView("~/Views/Leave/NewLeaveApplication.cshtml", NewAppl);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLeaveRelievers(string Department)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var NewAppl = new NewLeaveApplication();

                #region ReliverList

                var relieverList = new List<RelieverList>();
                string pageReliever = "EmployeeList?$filter=No ne '" + StaffNo + "' and Status eq 'Active' and Global_Dimension_2_Code eq '" + Department + "'&$orderby=First_Name asc&format=json";

                var httpResponseReliever = Credentials.GetOdataData(pageReliever);
                using (var streamReader = new StreamReader(httpResponseReliever.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                        if ((string)config["First_Name"] != "")
                        {
                            var Rlist = new RelieverList();
                            Rlist.No = (string)config["No"];
                            Rlist.Name = (string)config["First_Name"] + " " + (string)config["Middle_Name"] + " " +
                                         (string)config["Last_Name"];
                            relieverList.Add(Rlist);
                        }
                }

                #endregion

                NewAppl = new NewLeaveApplication
                {
                    ListOfRelievers = relieverList.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.No
                        }).ToList()
                };
                return Json(new { NewAppl, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLeaveBalance(string LvType)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var employee = Session["EmployeeData"] as EmployeeView;
                var newBal = new LeaveBalance();

                var Lvbal = new List<DropDownBalance>();
                decimal availableDays = 0;
                var harshpdays = "";
                var s = new decimal[5];
                if (LvType.Contains("ANNUAL"))
                {
                    s = CommonClass.GetLeaveBal(StaffNo, LvType);
                    if (s[0] > 1)
                        // availableDays = (s[0] + s[3]) - Math.Abs(s[2]);
                        availableDays = s[0];
                }
                else if (availableDays == 0)
                {
                    var page = "HRLeaveTypes?$select=Days&$filter=Code eq '" + LvType + "'&$format=json";

                    var httpResponse = Credentials.GetOdataData(page);

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        var details = JObject.Parse(result);
                        foreach (JObject config in details["value"])
                            availableDays = availableDays + (int)config["Days"];
                    }

                    s[0] = 0;
                    s[1] = 0;
                    s[2] = 0;
                    s[3] = 0;
                    s[4] = 0;
                }


                if ((LvType.Contains("ANNUAL")) && (CommonClass.IsHardshipArea(employee.DutyStation)))
                {
                    harshpdays = "6";
                }


                if (availableDays > 0)
                {
                    for (var i = 1; i <= availableDays; i++)
                    {
                        var NewV = new DropDownBalance();
                        NewV.Code = i.ToString();
                        Lvbal.Add(NewV);
                    }

                    newBal = new LeaveBalance
                    {
                        AllocatedDays = s[0].ToString(),
                        CarryForawrd = s[4].ToString(),
                        ReimbDays = s[1].ToString(),
                        LeaveTaken = s[2].ToString(),
                        HardshipDays = harshpdays,
                        EarnedLeaveDays = s[3].ToString(),
                        Balance = availableDays.ToString(),
                        ListOfDays = Lvbal.Select(x =>
                            new SelectListItem
                            {
                                Text = x.Code,
                                Value = x.Code
                            }).ToList()
                    };
                    return Json(new { newBal, success = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(
                    new
                    {
                        message = "No Allocations for the specified leave type has been done. Contact HR",
                        success = false
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult End_ReturnDates(string startDate, string days, string LvType)
        {
            try
            {
                if (string.IsNullOrEmpty(days))
                {
                    days = "1";
                }
                var ERDate = new EndReturnDates();

                var startD = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var s = Credentials.ObjNav.GetEndReturnDate(startD, Convert.ToInt32(days), LvType);
                var endDate = s[0];
                var returndate = s[1];
                ERDate = new EndReturnDates
                {
                    EndDate = endDate.ToString("dd/MM/yyyy"),
                    ReturnDate = returndate.ToString("dd/MM/yyyy")
                };
                return Json(new { ERDate, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ChangeLeaveReliever(string DocNo, string NewReliever)
        {
            try
            {
                var msg = "";
                var successVal = false;
                Credentials.ObjNav.ChangeLeaveReliever(DocNo, NewReliever);
                msg = "Updated Successfully";
                successVal = true;
                return Json(new { message = msg, success = successVal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitLeaveApplication(LeaveReqList NewApp, string base64Upload, string fileName, string Extn,
            List<HandoverTask> tasks, List<HandoverFile> tasksFile)
        {
            try
            {
                var msg = "";
                var UserID = Session["UserID"].ToString();
                bool successVal = false, saveData = false;

                var approver = CommonClass.GetEmployeeApproverUserID(UserID);

                if (approver == "")
                {
                    msg = "Your approver has not been set up, kindly contact ICT or HR";
                    successVal = false;
                    saveData = false;
                }
                else
                {
                    if (base64Upload != "")
                    {
                        if (fileName != "")
                        {
                            var ext = Path.GetExtension(fileName);

                            if (ext.ToLower() == ".pdf" || ext.ToLower() == ".docx" || ext.ToLower() == ".doc" ||
                                ext.ToLower() == ".xlsx" ||
                                ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".png")
                            {
                                saveData = true;
                            }
                            else
                            {
                                msg =
                                    "Only files with extensions(.pdf, .docx, .doc, .xlsx, .jpeg, .jpg, .png) can be uploaded";
                                successVal = false;
                                saveData = false;
                            }
                        }
                        else
                        {
                            msg = "Incorrect uploaded file!!";
                            successVal = false;
                            saveData = false;
                        }
                    }
                    else
                    {
                        saveData = true;
                    }

                    if (saveData)
                    {
                        var startDate = DateTime.ParseExact(NewApp.Starting_Date.Replace("-", "/"), "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                        var endDate = DateTime.ParseExact(NewApp.End_Date.Replace("-", "/"), "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                        var returndate = DateTime.ParseExact(NewApp.Return_Date.Replace("-", "/"), "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                        var leavet = NewApp.Leave_Type;
                        var appdays = NewApp.Applied_Days;
                        var rmk = "";
                        if (NewApp.Remarks != null) rmk = NewApp.Remarks;
                        var rlvr = NewApp.Reliever;
                        var DocNo = "";

                        DocNo = Credentials.ObjNav.CreateLeaveApplication(Session["username"].ToString(), leavet, 1,
                            Convert.ToInt32(appdays), startDate, rlvr, NewApp.PhoneNo, "", "", returndate, rmk);
                        Credentials.ObjNav.UpdateLeaveApprover(approver, DocNo);

                        Credentials.ObjNav.sendLeaveForApproval(Session["username"].ToString(), DocNo);
                        Credentials.ObjNav.UpdateApprovalEntrySenderID(69205, DocNo, UserID);
                        var FromName = CommonClass.GetEmployeeName(Session["username"].ToString());
                        /*foreach (var task in tasks)
                            if (task.Activity != null)
                            {
                                var reason = "";
                                var Activ = "";
                                var Stat = "";
                                if (!string.IsNullOrEmpty(task.Reason)) reason = task.Reason;
                                if (!string.IsNullOrEmpty(task.Activity)) Activ = task.Activity;
                                if (!string.IsNullOrEmpty(task.Status)) Stat = task.Status;
                                Credentials.ObjNav.InsertHandoverNotes(DocNo, 1, Activ, Stat, reason);
                            }*/

                        /* foreach (var task in tasksFile)
                             if (task.Activity != null)
                             {
                                 var Status = "";
                                 var Activ= "";
                                 var Stat = "";
                                 if (!string.IsNullOrEmpty(task.Status)) Status = task.Status;
                                 if (!string.IsNullOrEmpty(task.Activity)) Activ = task.Activity;
                                 Credentials.ObjNav.InsertHandoverNotes(DocNo, 2, Activ, Status, "");
                             }*/

                        var appr = approver.Replace(@"COURT\", "");
                        var appro = CommonClass.GetDocumentApproverUserID("69025", DocNo);
                        appr = appro.Replace(@"COURT\", "");
                        var approveremail = CommonClass.GetEmployeeEmail(appr);
                        var approvername = CommonClass.GetEmployeeFirstName(appr);
                        var Designation = CommonClass.GetEmployeeSalutationDetails(appr);
                        var msgs = "";
                        msgs = "Dear " + Designation[0] + " " + approvername + ",<br /><br />"
                               + "You have a new leave application request from " + FromName +
                               " that requires your attention.<br /><br />"
                               + "<b />Details:<br /><br />"
                               + "<b>Applicant Name:</b> <i>" + FromName + "</i><br /><br />"
                               + "<b>Type of Leave: </b><i>" + leavet + "</i><br /><br />"
                               + "<b>Start Date: </b><i>" + NewApp.Starting_Date + "</i><br /><br />"
                               + "<b>End Date: </b><i>" + NewApp.End_Date + "</i><br /><br />"
                               + "<b>Return Date: </b><i>" + NewApp.Return_Date + "</i><br /><br />"
                               + "<b>No of Days: </b><i>" + NewApp.Applied_Days + "</i><br /><br />";
                        CommonClass.SendEmailAlert(msgs, approveremail,
                            "Leave Approval Request: " + DocNo + "(Leave Number)");
                        msg = "A leave Application, Document Number " + DocNo +
                              " has been submitted successfully and sent for approval";
                        successVal = true;
                        if (base64Upload != null && fileName != "")
                        {
                            var filePath = Server.MapPath("~/Uploads/" + fileName);


                            var uploaded = UploadDocuments.UploadEDMSDocumentAttachment(base64Upload, fileName, "HRMD",
                                DocNo, "Leave", "", 69205, "");
                            if (uploaded)
                            {
                                msg = "A leave Application, Document Number " + DocNo +
                                      " has been submitted successfully and sent for approval";
                                successVal = true;
                            }
                        }
                    }
                }

                return Json(new { message = msg, success = successVal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateLeaveApplication(LeaveReqList leaveReq)
        {
            try
            {
                var Remarks = "";
                if (leaveReq.Remarks != null) Remarks = leaveReq.Remarks;
                var startDate = DateTime.ParseExact(leaveReq.Starting_Date.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
                var endDate = DateTime.ParseExact(leaveReq.End_Date.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
                var returndate = DateTime.ParseExact(leaveReq.Return_Date.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                Credentials.ObjNav.FnCreateLeaveApplication(leaveReq.No, leaveReq.EmpNo, leaveReq.Leave_Type, 0,
                    1, startDate, leaveReq.Reliever, "", "", "", endDate, Remarks);

                return Json(
                    new { message = "Leave Application Updated and sent for approval Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult LeaveDocumentViewView(string DocNo)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var Department = CommonClass.EmployeeDepartment(StaffNo);


                #region ReliverList

                var relieverList = new List<RelieverList>();
                var pageReliever = "EmployeeList?$filter=No ne '" + StaffNo +
                                   "' and Status eq 'Active'&format=json";

                var httpResponseReliever = Credentials.GetOdataData(pageReliever);
                using (var streamReader = new StreamReader(httpResponseReliever.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                        if ((string)config["First_Name"] != "")
                        {
                            var Rlist = new RelieverList();
                            Rlist.No = (string)config["No"];
                            Rlist.Name = (string)config["First_Name"] + " " + (string)config["Middle_Name"] + " " +
                                         (string)config["Last_Name"];
                            relieverList.Add(Rlist);
                        }
                }

                #endregion

                #region LeaveTypes

                var leaveTps = new List<LvTypes>();

                var gender = CommonClass.GetEmployeeGender(StaffNo);
                if (string.IsNullOrEmpty(gender.Trim()))
                {
                    var erroMsg = new Error();
                    erroMsg.Message = "Your gender has not been set. Contact HR";
                    return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
                }

                var pageLeaveTypes = "HRLeaveTypes?$filter=(Gender eq 'Both' or Gender eq '" + gender + "')&format=json";

                var httpResponseLeave = Credentials.GetOdataData(pageLeaveTypes);
                using (var streamReader = new StreamReader(httpResponseLeave.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var LTpe = new LvTypes();
                        LTpe.Code = (string)config["Code"];
                        LTpe.Description = (string)config["Code"];
                        leaveTps.Add(LTpe);
                    }
                }

                #endregion


                var LeaveDoc = new LeaveReqList();

                var page = "LeaveApplication?$filter=Application_Code eq '" + DocNo + "'&format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        LeaveDoc.No = (string)config["Application_Code"];
                        LeaveDoc.EmpNo = (string)config["Employee_No"];
                        LeaveDoc.EmpName = CommonClass.GetEmployeeName((string)config["Employee_No"]);
                        LeaveDoc.Leave_Type = (string)config["Leave_Type"];
                        LeaveDoc.Applied_Days = (string)config["Days_Applied"];
                        LeaveDoc.Date = Convert.ToDateTime((string)config["Application_Date"]).ToString("dd/MM/yyyy");
                        LeaveDoc.Starting_Date =
                            Convert.ToDateTime((string)config["Start_Date"]).ToString("dd/MM/yyyy");
                        LeaveDoc.End_Date = Convert.ToDateTime((string)config["End_Date"]).ToString("dd/MM/yyyy");
                        LeaveDoc.Return_Date = Convert.ToDateTime((string)config["Return_Date"]).ToString("dd/MM/yyyy");
                        LeaveDoc.Reliever = (string)config["Reliever_Name"];

                        LeaveDoc.Department = (string)config["Department_Code"];
                        LeaveDoc.Remarks = (string)config["Applicant_Comments"];
                        LeaveDoc.Status = (string)config["Status"];
                    }

                    ViewBag.reliever = LeaveDoc.Reliever;
                }

                decimal availableDays = 0;
                var Lvbal = new List<DropDownBalance>();

                #region LeaveBal

                var s = CommonClass.GetLeaveBal(StaffNo, LeaveDoc.Leave_Type);

                #endregion

                if (LeaveDoc.Leave_Type.Contains("ANNUAL"))
                    availableDays = s[0];
                else
                    availableDays = s[0];
                for (var i = 1; i <= availableDays; i++)
                {
                    var NewV = new DropDownBalance();
                    NewV.Code = i.ToString();
                    Lvbal.Add(NewV);
                }

                var lvDoc = new LeaveDocumentDetails
                {
                    DocumentDetails = LeaveDoc,
                    ListOfRelievers = relieverList.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.No
                        }).ToList(),
                    ListOfDays = Lvbal.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Code,
                            Value = x.Code
                        }).ToList(),
                    ListOfLeaveTypes = leaveTps.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Description,
                            Value = x.Code
                        }).ToList()
                };
                return PartialView("~/Views/Leave/LeaveDocumentView.cshtml", lvDoc);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public JsonResult SendLeaveAppForApproval(string DocNo)
        {
            try
            {
                var mes = "";
                var suc = false;
                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();
                var approver = CommonClass.GetEmployeeApproverUserID(UserID);
                if (approver != null)
                {
                    Credentials.ObjNav.sendLeaveForApproval(StaffNo, DocNo);
                    Credentials.ObjNav.UpdateApprovalEntrySenderID(69205, DocNo, UserID);
                    mes = "Leave Application sent for approval Successfully";
                    suc = true;
                }
                else
                {
                    mes = "Your approver has not been set. Kindly contact HR or ICT";
                    suc = false;
                }


                return Json(new { message = mes, success = suc }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RecallLeaveAppForApproval(string DocNo)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();

                Credentials.ObjNav.CanceLeaveApproval(StaffNo, DocNo);

                return Json(new { message = "Leave Application approval Recalled Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelLeaveAppForApproval(string DocNo)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                Credentials.ObjNav.CanceLeaveApproval(StaffNo, DocNo);

                return Json(new { message = "Leave Application approval cancelled Successfully", success = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GenerateLeaveReport(string DocNo)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();

                var message = "";
                var filename = "";
                bool success = false, view = false;

                var StaffIDNo = CommonClass.GetEmployeeIdNo(StaffNo);
                if (StaffIDNo == "")
                {
                    success = false;
                    message = "Employee ID Number is not set. Contact HR";
                }
                else
                {
                    var _filename = StaffNo.Replace(@"/", @"");
                    // Credentials.ObjNav.GenerateLeaveReport(StaffNo, DocNo, "LeaveReport-" + DocNo + ".pdf");
                    var OldPayslip = "LeaveReport-" + DocNo + ".pdf";
                    filename = "LeaveReport-" + DocNo + ".pdf";
                    var filePath = Server.MapPath("~/Downloads/");
                    var FromPath = filePath + OldPayslip;
                    var TPath = filePath + filename;

                    //CommonClass.MoveFile(filename);
                    var DestinationPath = filePath + filename;
                    var file = new FileInfo(DestinationPath);
                    if (file.Exists)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        message = "File Not Found";
                    }

                    if (success) message = @"/Downloads/" + filename;
                }

                return Json(new { message, success, view }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EmployeesOnLeaveList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult EmployeesOnLeaveListPartialView()
        {
            try
            {
                var dt = DateTime.Today.AddDays(-2);
                var iposiku = dt.ToString("yyyy-MM-dd") + "T23:59:59.99Z";

                var StaffNo = Session["Username"].ToString();
                var LeaveList = new List<LeaveReqList>();
                var dept = CommonClass.EmployeeDepartment(StaffNo);
                var employee = Session["EmployeeData"] as EmployeeView;
                var dim1 = employee.GlobalDimension1Code;
                //Global_Dimension_1_Code eq '" + dept + "' and 
                // string page = "HRLeaveRequisition?$filter=(Status eq 'Released' or Status eq 'Open') and Return_Date gt " + iposiku + " &$format=json";
                string page = "LeaveApplication?$filter=(Status eq 'Released' or Status eq 'Approved') and Shortcut_Dimension_1_Code eq '" + dim1 + "' and Return_Date gt " + iposiku + " and Start_Date lt " + iposiku + "  &$format=json";


                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var LvList = new LeaveReqList();
                        LvList.No = (string)config["Application_Code"];
                        LvList.Leave_Type = (string)config["Leave_Type"];
                        LvList.EmpName = CommonClass.GetEmployeeName((string)config["Employee_No"]);
                        LvList.Applied_Days = (string)config["Days_Applied"];
                        LvList.Date = Convert.ToDateTime((string)config["Application_Date"]).ToString("dd/MM/yyyy");
                        LvList.Starting_Date = Convert.ToDateTime((string)config["Start_Date"]).ToString("dd/MM/yyyy");
                        LvList.End_Date = Convert.ToDateTime((string)config["End_Date"]).ToString("dd/MM/yyyy");
                        LvList.Return_Date = Convert.ToDateTime((string)config["Return_Date"]).ToString("dd/MM/yyyy");
                        LvList.Reliever = (string)config["Reliever_Name"];
                        LvList.Status = (string)config["Status"];
                        LvList.Department = CommonClass.GetDimensionValue((string)config["ShortcutDimension2Code"]);
                        LvList.IsSupervisor = CommonClass.ISSupervisor(Session["UserID"].ToString());
                        LvList.EmployeeCategory = CommonClass.GetEmployeeCategory((string)config["Employee_No"]);
                        LeaveList.Add(LvList);
                    }
                }

                return PartialView("~/Views/Leave/EmployeesOnLeave.cshtml", LeaveList.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public ActionResult EmployeesApprovedLeaveList()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult EmployeesApprovedLeaveListPartialView()
        {
            try
            {
                var dt = DateTime.Today.AddDays(-2);
                var iposiku = dt.ToString("yyyy-MM-dd") + "T23:59:59.99Z";

                var StaffNo = Session["Username"].ToString();
                var LeaveList = new List<LeaveReqList>();
                var dept = CommonClass.EmployeeDepartment(StaffNo);
                EmployeeView employee = Session["EmployeeData"] as EmployeeView;
                string dim1 = employee.AdministrativeUnit;
                //Global_Dimension_1_Code eq '" + dept + "' and 
                // string page = "HRLeaveRequisition?$filter=(Status eq 'Released' or Status eq 'Open') and Return_Date gt " + iposiku + " &$format=json";
                string page = "LeaveApplication?$filter=(Status eq 'Released') and Shortcut_Dimension_1_Code eq '" + dim1 + "' and Return_Date gt " + iposiku + " &$format=json";


                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var LvList = new LeaveReqList();
                        LvList.No = (string)config["Application_Code"];
                        LvList.Leave_Type = (string)config["Leave_Type"];
                        LvList.EmpName = CommonClass.GetEmployeeName((string)config["Employee_No"]);
                        LvList.Applied_Days = (string)config["Days_Applied"];
                        LvList.Date = Convert.ToDateTime((string)config["Application_Date"]).ToString("dd/MM/yyyy");
                        LvList.Starting_Date = Convert.ToDateTime((string)config["Start_Date"]).ToString("dd/MM/yyyy");
                        LvList.End_Date = Convert.ToDateTime((string)config["End_Date"]).ToString("dd/MM/yyyy");
                        LvList.Return_Date = Convert.ToDateTime((string)config["Return_Date"]).ToString("dd/MM/yyyy");
                        LvList.Reliever = (string)config["Reliever_Name"];
                        LvList.Status = (string)config["Status"];
                        LvList.Department = (string)config["Department_Name"];
                        LvList.IsSupervisor = CommonClass.ISSupervisor(Session["UserID"].ToString());
                        LeaveList.Add(LvList);
                    }
                }

                return PartialView("~/Views/Leave/EmployeesOnLeave.cshtml", LeaveList.OrderByDescending(x => x.No));
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }


        public ActionResult LeavePlanner()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult LeavePlannerListPartialView()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var Department = CommonClass.EmployeeDepartment(StaffNo);

                var page = "LeavePlannerList?$filter=Responsibility_Center eq '" + Department + "'&format=json";

                var leavePlannerList = new List<LeavePlannerList>();

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var plannerList = new LeavePlannerList();
                        plannerList.Application_Code = (string)config["Application_Code"];
                        plannerList.Names = (string)config["Implementing_Unit_Name"];
                        plannerList.Leave_Calendar = (string)config["Leave_Calendar"];
                        plannerList.Employee_No = (string)config["Responsibility_Center"];
                        plannerList.Job_Tittle = (string)config["Global_Dimension_1_Code"];

                        leavePlannerList.Add(plannerList);
                    }
                }

                return PartialView("~/Views/Leave/PartialView/LeavePlannerListPartialView.cshtml", leavePlannerList);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public ActionResult LeavePlannerDocument(string AppDoc)
        {
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");

            var leavePlanner = new LeavePlannerList();


            #region HrLeavePeriods

            var leavePrds = new List<DropdownList>();
            var employeepage = "HrLeavePeriods?$format=json";

            var httpResponseLvPrds = Credentials.GetOdataData(employeepage);
            using (var streamReader = new StreamReader(httpResponseLvPrds.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var ddl = new DropdownList();
                    ddl.Value = (string)config["Code"];
                    ddl.Text = (string)config["Code"] + "-" + (string)config["Description"];
                    leavePrds.Add(ddl);
                }
            }

            #endregion


            var page = "HRLeavePlanner?$filter=Application_Code eq '" + AppDoc + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    leavePlanner.Application_Code = (string)config["Application_Code"];
                    //leavePlanner.Responsibility_Center = (string)config["Responsibility_Center"];
                    leavePlanner.Leave_Calendar = (string)config["Leave_Calendar"];
                    //leavePlanner.Implementing_Unit_Name = (string)config["Implementing_Unit_Name"];
                    //leavePlanner.Global_Dimension_1_Code = (string)config["Global_Dimension_1_Code"];
                    leavePlanner.Employee_No = (string)config["Employee_No"];
                    leavePlanner.Names = (string)config["Names"];
                    leavePlanner.Job_Tittle = (string)config["Job_Tittle"];
                    leavePlanner.Status = (string)config["Status"];

                }

                leavePlanner.ListOfLeave_Calendar = leavePrds.Select(e => new SelectListItem
                {
                    Value = e.Value,
                    Text = e.Text
                }).ToList();
            }

            return View(leavePlanner);
        }

        public PartialViewResult LeavePlannerLines(string DocNo, string status)
        {
            var StaffNo = Session["Username"].ToString();
            var page = "HRLeavePlannerLines?$filter=Application_Code eq '" + DocNo +
                       "'&$format=json&$orderby=Employee_No";
            var plannerLines = new List<LeavePlannerLines>();

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            foreach (JObject config in details["value"])
            {
                var leavePlannerLines = new LeavePlannerLines();
                leavePlannerLines.Line_No = int.Parse((string)config["Line_No"]);
                leavePlannerLines.Application_Code = (string)config["Application_Code"];
                leavePlannerLines.Leave_Type = (string)config["Leave_Type"];
                leavePlannerLines.Days_Applied = int.Parse((string)config["Days_Applied"]);
                leavePlannerLines.Start_Date = DateTime.ParseExact((string)config["Start_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                leavePlannerLines.End_Date = DateTime.ParseExact((string)config["End_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                leavePlannerLines.Return_Date = DateTime.ParseExact((string)config["Return_Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                leavePlannerLines.Applicant_Comments = (string)config["Applicant_Comments"];
                leavePlannerLines.Request_Leave_Allowance = bool.Parse((string)config["Request_Leave_Allowance"]);
                leavePlannerLines.Reliever = (string)config["Employee_No"];
                leavePlannerLines.Reliever_Name = CommonClass.GetEmployeeName((string)config["Employee_No"]);
                leavePlannerLines.Approved_days = int.Parse((string)config["Approved_days"]);
                leavePlannerLines.Date_of_Exam = DateTime.Parse(config["Date_of_Exam"]?.ToString());
                leavePlannerLines.Details_of_Examination = (string)config["Details_of_Examination"];
                plannerLines.Add(leavePlannerLines);
            }

            var leavePlannerLinesList = new LeavePlannerLinesList
            {
                ListOfLeavePlannerLines = plannerLines,
                //status = status
            };
            return PartialView("~/Views/Leave/PartialView/LeavePlannerLinesPartialView.cshtml",
                leavePlannerLinesList);
        }

        public JsonResult NewLeavePlanner()
        {
            try
            {
                var emploeeNo = Session["Username"].ToString();

                var now = DateTime.Now;
                var startDateFilter = new DateTime(now.Year, 7, 1);
                var endDateFilter = new DateTime(now.Year + 1, 7, 1);

                var plannerList = new LeavePlannerList();
                var leavePrds = new List<DropdownList>();
                var startDateFilterStr = startDateFilter.ToString("yyyy-MM-dd");
                var endDateFilterStr = endDateFilter.ToString("yyyy-MM-dd");

                var employeepage =
                    $"HrLeavePeriods?$filter=Starts ge {startDateFilterStr} and Ends le {endDateFilterStr}&$format=json";


                var httpResponseLvPrds = Credentials.GetOdataData(employeepage);
                using (var streamReader = new StreamReader(httpResponseLvPrds.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"]) plannerList.Leave_Calendar = (string)config["Code"];
                }

                if (plannerList.Leave_Calendar == null)
                    return Json(new { message = "Leave Calendar is null!", success = false },
                        JsonRequestBehavior.AllowGet);

                var DocNo = Credentials.ObjNav.InsertLeavePlannerHeader(emploeeNo, plannerList.Leave_Calendar);

                var Redirect = "/Leave/LeavePlannerDocument?AppDoc=" + DocNo;

                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult InsertLeavePlannerLines(EditLeavePlannerLineObj editLeave)
        {
            try
            {
                var userId = Session["UserID"].ToString();
                var sDate = DateTime.ParseExact(editLeave.StartDate.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
                var emploeeNo = Session["Username"].ToString();
                Credentials.ObjNav.InsertLeavePlannerLines(editLeave.DocNo, emploeeNo, editLeave.LeaveType, sDate,
                    editLeave.DaysApplied);

                var Redirect = "/Leave/LeavePlannerDocument?AppDoc=" + editLeave.DocNo;

                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult NewLeavePlannerLines(string applicationCode)
        {
            var StaffNo = Session["Username"].ToString();
            var plannerLines = new LeavePlannerLines();

            #region LeaveTypes

            var leaveTypes = new List<DropdownList>();

            var gender = CommonClass.GetEmployeeGender(StaffNo);
            if (string.IsNullOrEmpty(gender.Trim()))
            {
                var erroMsg = new Error();
                erroMsg.Message = "Your gender has not been set. Contact HR";
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }

            var lvTypes = "LeaveTypes?$filter=(Gender eq 'Both' or Gender eq '" + gender + "')&format=json";
            //string lvTypes = "LeaveTypes?$filter=Code eq 'ANNUAL'&format=json";

            var httpResponseLvTypes = Credentials.GetOdataData(lvTypes);
            using (var streamReader = new StreamReader(httpResponseLvTypes.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                plannerLines.Application_Code = applicationCode;


                foreach (JObject config in details["value"])
                {
                    var ddl = new DropdownList();
                    ddl.Value = (string)config["Code"];
                    ddl.Text = (string)config["Code"] + "-" + (string)config["Description"];
                    leaveTypes.Add(ddl);
                }
            }

            #endregion

            plannerLines.ListOfLeave_Type = leaveTypes.Select(e => new SelectListItem
            {
                Value = e.Value,
                Text = e.Text
            }).ToList();
            plannerLines.Leave_Type = "ANNUAL";

            return PartialView("~/Views/Leave/PartialView/NewLeavePlannerLine.cshtml", plannerLines);
        }

        public PartialViewResult EditLeavePlannerLine(string applicationCode, int lineNo)
        {
            var plannerLines = new LeavePlannerLines();

            #region LeaveTypes

            var leaveTypes = new List<DropdownList>();
            var lvTypes = "LeaveTypes?$filter=Code eq 'ANNUAL'&format=json";

            var httpResponseLvTypes = Credentials.GetOdataData(lvTypes);
            using (var streamReader = new StreamReader(httpResponseLvTypes.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                plannerLines.Application_Code = applicationCode;
                plannerLines.Line_No = lineNo;
                plannerLines.Leave_Type = "ANNUAL";

                foreach (JObject config in details["value"])
                {
                    var ddl = new DropdownList();
                    ddl.Value = (string)config["Code"];
                    ddl.Text = (string)config["Code"] + "-" + (string)config["Description"];
                    leaveTypes.Add(ddl);
                }
            }

            #endregion

            plannerLines.ListOfLeave_Type = leaveTypes.Select(e => new SelectListItem
            {
                Value = e.Value,
                Text = e.Text
            }).ToList();

            return PartialView("~/Views/Leave/PartialView/EditLeavePlannerLine.cshtml", plannerLines);
        }


        public JsonResult ModifyLeavePlannerLines(EditLeavePlannerLineObj editLeave)
        {
            try
            {
                var userId = Session["UserID"].ToString();
                var sDate = DateTime.ParseExact(editLeave.StartDate.Replace("-", "/"), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);


                Credentials.ObjNav.ModifyLeavePlannerLines(editLeave.DocNo, editLeave.LineNo, editLeave.LeaveType,
                    sDate, editLeave.DaysApplied);

                var Redirect = "/Leave/LeavePlannerDocument?AppDoc=" + editLeave.DocNo;

                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendLeavePlannerApproval(string docNo)
        {
            try
            {
                var userId = Session["UserID"].ToString();


                Credentials.ObjNav.SendLeavePlannerApproval(docNo);

                var Redirect = "/Leave/LeavePlanner";
                var mes = "Leave planner sent for approval Successfully";

                return Json(new { message = mes, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelLeavePlannerHeader(string docNo)
        {
            try
            {
                var userId = Session["UserID"].ToString();


                Credentials.ObjNav.CancelLeavePlannerHeader(docNo);

                var Redirect = "Leave planner document approval request cancelled";

                return Json(new { message = Redirect, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLeaveDays(string code)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();


                var noOfDays = 0;

                var page = "AdoptionLeaveSetU?$filter=Code eq '" + code + "'&format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"]) noOfDays = (int)config["Days"];
                }


                return Json(new { noOfDays, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSupervisorName()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();

                var approver = CommonClass.GetEmployeeApproverUserID(UserID);

                var NewAppl = new NewLeaveApplication();
                var approvername = CommonClass.GetEmployeeNameByUserID(approver);
                if (approvername != "")
                    NewAppl.SupervisorName = approvername;
                else
                    NewAppl.SupervisorName = approver;

                return Json(new { NewAppl, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetRelieverName()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();

                var relieverCode = CommonClass.GetEmployeeRelieverUserID(StaffNo);

                var NewAppl = new NewLeaveApplication();
                var relievername = CommonClass.GetEmployeeNameByNo(relieverCode);

                if (relievername != "")
                    NewAppl.Reliever = relievername;
                else
                    NewAppl.Reliever = relieverCode;
                return Json(new { NewAppl, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LaeveApplicationRecall()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public PartialViewResult LaeveApplicationRecallListPartialView()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();

                var page = "LaeveApplicationRecall?$&format=json";

                var leaveApplicationRecalls = new List<LeaveApplicationRecall>();

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var applicationRecall = new LeaveApplicationRecall();
                        applicationRecall.Recall_No = (string)config["Recall_No"];
                        applicationRecall.Application_No = (string)config["Application_No"];
                        applicationRecall.Employee_No = (string)config["Employee_No"];
                        applicationRecall.Employee_Name = (string)config["Employee_Name"];
                        applicationRecall.Leave_Code = (string)config["Leave_Code"];
                        applicationRecall.Days_Applied = (int)config["Days_Applied"];
                        applicationRecall.Start_Date = (string)config["Start_Date"];
                        applicationRecall.End_Date = (string)config["End_Date"];
                        applicationRecall.Application_Date = (string)config["Application_Date"];
                        applicationRecall.Approved_Days = (int)config["Approved_Days"];
                        applicationRecall.Approved_Start_Date = (string)config["Approved_Start_Date"];
                        applicationRecall.Days_Recalled = (int)config["Days_Recalled"];
                        applicationRecall.Recall_Date_From = (string)config["Recall_Date_From"];
                        applicationRecall.Recall_Date_To = (string)config["Recall_Date_To"];
                        applicationRecall.Reason_for_Recall = (string)config["Reason_for_Recall"];
                        applicationRecall.Posted = (bool)config["Posted"];

                        leaveApplicationRecalls.Add(applicationRecall);
                    }
                }

                return PartialView("~/Views/Leave/PartialView/LaeveApplicationRecallListPartialView.cshtml",
                    leaveApplicationRecalls);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        public PartialViewResult NewLeaveRecall(string DocNo)
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var UserID = Session["UserID"].ToString();
                //List<LeaveReqList> LeaveList = new List<LeaveReqList>();
                var LeaveList = new LeaveReqList();

                var page = "HRLeaveRequisition?$filter=Application_Code eq '" + DocNo + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        LeaveList.No = (string)config["Application_Code"];
                        LeaveList.Leave_Type = (string)config["Leave_Type"];
                        LeaveList.Applied_Days = (string)config["Days_Applied"];
                        LeaveList.Date = Convert.ToDateTime((string)config["Application_Date"]).ToString("dd/MM/yyyy");
                        LeaveList.Starting_Date =
                            Convert.ToDateTime((string)config["Start_Date"]).ToString("dd/MM/yyyy");
                        LeaveList.End_Date = Convert.ToDateTime((string)config["End_Date"]).ToString("dd/MM/yyyy");
                        LeaveList.Return_Date =
                            Convert.ToDateTime((string)config["Return_Date"]).ToString("dd/MM/yyyy");
                        LeaveList.Reliever = (string)config["Reliever_Name"];
                        LeaveList.Status = (string)config["Status"];
                        LeaveList.EmpNo = (string)config["Employee_No"];
                        LeaveList.EmpName = CommonClass.GetEmployeeName((string)config["Employee_No"]);
                    }
                }

                return PartialView("~/Views/Leave/PartialView/NewLeaveRecall.cshtml", LeaveList);
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message;
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SubmitLeaveRecallApplication(string EmpNo, string DocumentNo, string RecalledDays,
            string RecalledDate, string RecallReason)
        {
            try
            {
                var msg = "";
                var DocNo = "";
                var UserID = Session["UserID"].ToString();
                bool successVal = false, saveData = false;

                var approver = CommonClass.GetEmployeeApproverUserID(UserID);
                if (approver == null)
                {
                    msg = "Your approver has not been set up, kindly contact ICT or HR";
                    successVal = false;
                    saveData = false;
                }

                else
                {
                    {
                        var startDate = DateTime.ParseExact(RecalledDate.Replace("-", "/"), "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);

                        DocNo = Credentials.ObjNav.InsertLeaveRecall(DocumentNo, Convert.ToDecimal(RecalledDays),
                            startDate, RecallReason);
                        Credentials.ObjNav.SendLeaveRecallApproval(Session["username"].ToString(), DocNo);
                        Credentials.ObjNav.UpdateApprovalEntrySenderID(69520, DocNo, UserID);
                        var FromName = CommonClass.GetEmployeeName(Session["username"].ToString());

                        var appr = approver.Replace(@"COURT\", "");
                        var appro = CommonClass.GetDocumentApproverUserID("69520", DocNo);
                        appr = appro.Replace(@"COURT\", "");
                        var approveremail = CommonClass.GetEmployeeEmail(appr);
                        var approvername = CommonClass.GetEmployeeFirstName(appr);
                        var Designation = CommonClass.GetEmployeeSalutationDetails(appr);
                        var msgs = "";
                        msgs = "Dear " + Designation[0] + " " + approvername + ",<br /><br />"
                               + "You have a new leave Recall application request from " + FromName +
                               " that requires your urgent attention.<br /><br />"
                               + "<b />Details:<br /><br />"
                               + "<b>Applicant Name:</b> <i>" + FromName + "</i><br /><br />"
                               + "<b>Employee Recalled: i>" + EmpNo + " " + CommonClass.GetEmployeeName(EmpNo) +
                               "</i><br /><br />"
                               + "<b>Days Recalled: </b><i>" + RecalledDays + "</i><br /><br />"
                               + "<b>Recalled from Date: </b><i>" + RecalledDate + "</i><br /><br />"
                               + "<b>Reason : </b><i>" + RecallReason + "</i><br /><br />"
                            ;
                        CommonClass.SendEmailAlert(msgs, approveremail,
                            "Leave Recall Approval Request: " + DocNo + "(Leave Recall)");
                        msg = "A leave Recall, Document Number " + DocNo +
                              " has been submitted successfully and sent for approval";
                    }
                }

                return Json(new { message = msg, success = successVal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message.Replace("'", ""), success = false },
                    JsonRequestBehavior.AllowGet);
            }
        }
    }
}
