using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers
{
    [CustomeAuthentication]
    [CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]
    public class ViewDocumentController : Controller
    {
        // GET: ViewDocument
        public ActionResult DocumentViewPayslip()
        {
            var ListYears = new PayslipDetails();

            #region Years

            var yearCodes = new List<YearCodes>();

            //string page = "PrPayrollPeriods?$select=PeriodYear&$filter=Closed eq true&format=json";
            var page = "PrPayrollPeriods?$select=PeriodYear&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var Years = new YearCodes();
                    Years.YList = (string)config["PeriodYear"];
                    yearCodes.Add(Years);
                }
            }

            #endregion

            ListYears = new PayslipDetails
            {
                ListOfYears = yearCodes.Select(x =>
                    new SelectListItem
                    {
                        Text = x.YList,
                        Value = x.YList
                    }).OrderBy(x => Convert.ToInt32(x.Value)).DistinctBy(x => x.Value).ToList()
            };
            return View(ListYears);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetMonths(int Year)
        {
            try
            {
                var ListMonths = new MonthList();

                #region Months

                var Months = new List<MonthCodes>();

                var page = "PrPayrollPeriods?$select=PeriodMonth&$filter=PeriodYear eq " + Year +
                           " and Closed eq true&format=json";
                //  string page = "PrPayrollPeriods?$select=PeriodMonth&$filter=PeriodYear eq " + Year + "&format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);
                    foreach (JObject config in details["value"])
                    {
                        var months = new MonthCodes();
                        months.MCode = (string)config["PeriodMonth"];
                        if (months.MCode == "1")
                            months.MDesc = "January";
                        else if (months.MCode == "2")
                            months.MDesc = "February";
                        else if (months.MCode == "3")
                            months.MDesc = "March";
                        else if (months.MCode == "4")
                            months.MDesc = "April";
                        else if (months.MCode == "5")
                            months.MDesc = "May";
                        else if (months.MCode == "6")
                            months.MDesc = "June";
                        else if (months.MCode == "7")
                            months.MDesc = "July";
                        else if (months.MCode == "8")
                            months.MDesc = "August";
                        else if (months.MCode == "9")
                            months.MDesc = "September";
                        else if (months.MCode == "10")
                            months.MDesc = "October";
                        else if (months.MCode == "11")
                            months.MDesc = "November";
                        else
                            months.MDesc = "December";
                        Months.Add(months);
                    }
                }

                #endregion

                ListMonths = new MonthList
                {
                    ListOfMonths = Months.Select(x =>
                        new SelectListItem
                        {
                            Text = x.MDesc,
                            Value = x.MCode
                        }).OrderBy(x => Convert.ToInt32(x.Value)).DistinctBy(x => x.Value).ToList()
                };

                return Json(ListMonths, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DocumentViewp9()
        {
            var ListYears = new P9Details();

            #region YearList

            var yearCodes = new List<YearCodes>();

            var page = "PREmployeeTransactions?$select=Period_Year&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var Years = new YearCodes();
                    Years.YList = (string)config["Period_Year"];
                    yearCodes.Add(Years);
                }
            }

            #endregion

            ListYears = new P9Details
            {
                ListOfYears = yearCodes.Select(x =>
                    new SelectListItem
                    {
                        Text = x.YList,
                        Value = x.YList
                    }).DistinctBy(x => x.Value).ToList()
            };
            return View(ListYears);
        }
        public JsonResult GetPayslipReport(string Year, string Month)
        {
            try
            {
                string StaffNo = Session["Username"].ToString();
                string StaffIDNo = "WAPI ID"; // replace with actual logic

                bool success = false;
                string message = "";


                int Pmonth = string.IsNullOrEmpty(Month) ? 0 : Convert.ToInt32(Month);
                int PYear = string.IsNullOrEmpty(Year) ? 0 : Convert.ToInt32(Year);

                // This should return a Base64-encoded PDF string
                message = Credentials.ObjNav.GeneratePaySlipReport2(StaffNo, Pmonth, PYear);
                success = !string.IsNullOrWhiteSpace(message);
                if (success)
                {
                    return Json(new { message, success }, JsonRequestBehavior.AllowGet);
                }else
                {
                    message = "File not found";
                    return Json(new { message, success=false }, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #region add password to pdf document
        internal static void addPassword(string TfileName, string NewFileName, string password)
        {
            try
            {
                using (Stream input = new FileStream(TfileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream output = new FileStream(NewFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfReader reader = new PdfReader(input);
                    PdfEncryptor.Encrypt(reader, output, true, password, password, PdfWriter.ALLOW_PRINTING);
                }
                if (System.IO.File.Exists(TfileName) == true)
                {
                    System.IO.File.Delete(TfileName);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        #endregion
        public JsonResult GetP9Report(string Year)
        {
            try
            {
                string staffNo = Session["Username"].ToString();
                string message = "";
                bool success = false;

                int period = Convert.ToInt32(Year);
                message = Credentials.ObjNav.GeneratePNineReport(staffNo, period);            
                if (!string.IsNullOrWhiteSpace(message))
                {
                    success = true;
                    message = message.Trim(); // just in case
                    return Json(new { message, success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "file not found";
                    return Json(new { message, success = false }, JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult LeaveStatement()
        {
            return View();
        }

        public JsonResult GetLeaveStatementReport()
        {
            try
            {
                var StaffNo = Session["Username"].ToString();
                var message = "";
                var filename = "";
                bool success = false, view = false;
                var fil = Credentials.ObjNav.GenerateLeaveStatementReport(StaffNo,
                    "LVSTATEMENT-" + StaffNo.Replace("'", "") + ".pdf");
                var filePath = Server.MapPath("~/Downloads/");
                filename = "LVSTATEMENT-" + StaffNo.Replace("'", "") + ".pdf";

                var bytes = Convert.FromBase64String(fil);
                var stream =
                    new FileStream(filePath + filename, FileMode.CreateNew);
                var writer =
                    new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();


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
                return Json(new { message, success, view }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}