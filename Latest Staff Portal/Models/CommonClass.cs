using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Latest_Staff_Portal.Models
{
    public class CommonClass
    {
        public static string ProfilePicture(string User)
        {
            var PicString = "";
            try
            {
                var StaffNo = User;
                PicString = Credentials.ObjNav.GetProfilePicture(StaffNo);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return PicString;
        }

        public static string[] GetStaffDetails(string User)
        {
            var s = new string[2];
            try
            {
                var StaffNo = User;
                var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (JObject config in details["value"])
                {
                    s[0] = (string)config["Gender"];
                    s[1] = (string)config["FirstName"] + " " + (string)config["MiddleName"] + " " +
                           (string)config["LastName"];
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return s;
        }

        public static bool IsICTStaff(string User)
        {
            var s = false;
            try
            {
                var StaffNo = User;
                var page = "EmployeeList?$filter=No eq '" + StaffNo + "' and ICT_Officer eq true&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (JObject config in details["value"]) s = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return s;
        }

        public static bool IsHardshipArea(string DutyStation)
        {
            var s = false;
            try
            {

                var page = "DutyStation?$filter=Code eq '" + DutyStation + "' and Hardship_Area eq true&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);

                foreach (JObject config in details["value"]) s = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return s;
        }
        public static string GetVehicle(string VehicleNo)
        {
            var Name = "";

            var page = "FltVehicleList?$select=Registration_No,Description,No&$filter=No eq '" + VehicleNo +
                       "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["Registration_No"] + " (" + (string)config["Description"] + " )";

            return Name;
        }

        public static string GetESSOTPNotificationSetup()
        {
            var Name = "";

            var page = "HRSetup?$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["Send_ESS_OTP_Via"];

            return Name;
        }

        public static bool MoveFile(string FileName)
        {
            var s = false;
            try
            {
                var sourcefile = Credentials.fileSourcePath + FileName;
                var destinationfile = Credentials.fileDestinationPath + FileName;
                if (File.Exists(destinationfile))
                {
                    File.Delete(destinationfile);
                    File.Move(sourcefile, destinationfile);
                }

                if (File.Exists(destinationfile) == false) File.Move(sourcefile, destinationfile);
                s = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return s;
        }

        public static bool MoveUploadedFile(string base64Upload, string FilePath, string FileName)
        {
            var s = false;
            try
            {
                SaveUploadedFile(base64Upload, FilePath);

                var sourcefile = FilePath;
                var destinationfile = Credentials.fileUploadsPath + FileName;
                if (File.Exists(destinationfile))
                {
                    File.Delete(destinationfile);
                    File.Move(sourcefile, destinationfile);
                }

                if (File.Exists(destinationfile) == false) File.Move(sourcefile, destinationfile);
                s = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return s;
        }

        public static bool DeleteFile(string FilePath)
        {
            var s = false;
            try
            {
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    s = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return s;
        }

        public static bool SendEmailAlert(string body, string recepient, string subject)
        {
            var x = false;

            try
            {
                x = Credentials.ObjNav.SendEmail(recepient, subject, body);

                //string SMTPHost = "smtp.office365.com";
                //string fromAddress = "notifications@greencom.co.ke";
                //string toAddress = recepient;
                //System.Net.Mail.MailMessage mail_ = new System.Net.Mail.MailMessage();
                //mail_.To.Add(toAddress);
                //mail_.Subject = subject;
                //mail_.From = new System.Net.Mail.MailAddress(fromAddress);
                //mail_.Body = body;
                //mail_.IsBodyHtml = true;

                //var smtp = new SmtpClient(SMTPHost, 587)
                //{
                //    Credentials = new NetworkCredential("notifications@greencom.co.ke", "123@CTeam2023"),
                //    EnableSsl = true
                //};
                //smtp.Send(mail_);
                x = true;
            }
            catch (Exception ex2)
            {
                ex2.Data.Clear();
            }

            return x;
        }

        public static string GetDimensionValue(string DimCode)
        {
            var DimVal = "";
            try
            {
                var page = "DimensionValues?$filter=Code eq '" + DimCode + "'&format=json";

                var httpResponseResC = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponseResC.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"]) DimVal = (string)config["Name"];
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return DimVal;
        }

        public class ODataResponse<T>
        {
            [JsonProperty("value")]
            public List<T> Value { get; set; }

            [JsonProperty("@odata.context")]
            public string Context { get; set; }

            [JsonProperty("@odata.count")]
            public int? Count { get; set; }
        }

        public static string EmployeeDepartment(string StaffNo)
        {
            var Department = "";

            var page = "EmployeeList?$select=Global_Dimension_2_Code&$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Department = (string)config["Global_Dimension_2_Code"];

            return Department;
        }

        public static string EmployeeDutyStation(string StaffNo)
        {
            var Department = "";

            var page = "EmployeeList?$select=Duty_Station&$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Department = (string)config["Duty_Station"];

            return Department;
        }

        public static bool DisregardDirectorate(string StaffNo)
        {
            var DisRegard = false;

            var page = "EmployeeList?$select=HOD&$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    DisRegard = (bool)config["HOD"];

            return DisRegard;
        }

        public static bool ISSupervisor(string EmpUserID)
        {
            var DisRegard = false;

            var page = "EmployeeList?$filter=Supervisor eq '" + EmpUserID + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    DisRegard = true;

            return DisRegard;
        }

        public static string GetOpenAppraisalPeriod()
        {
            var code = "";

            var page = "ApprisalPeriods?$filter=Open_Period eq true&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    code = (string)config["Code"];

            return code;
        }

        public static string GetAppraisalStage()
        {
            var code = "";

            var page = "HRAppraisalPeriodSetup?$filter=Open_Quarter eq true&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    code = (string)config["Appraisal_Type"] + "-" + (string)config["Appraisal_Stages"];
            else
                code = "Closed";

            return code;
        }

        public static bool IsTargetSettingAllowed(string Appraisal_Period, string Quarter)
        {
            var allow = false;

            var page = "HRAppraisalPeriodSetup?$filter=Code eq '" + Appraisal_Period + "' and Appraisal_Type eq '" +
                       Quarter + "' and Open_Quarter eq true and Appraisal_Stages eq 'Target Setting'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0) allow = true;

            return allow;
        }

        public static bool IsEvaluationAllowed(string Appraisal_Period, string Quarter)
        {
            var allow = false;

            var page = "HRAppraisalPeriodSetup?$filter=Code eq '" + Appraisal_Period + "' and Appraisal_Type eq '" +
                       Quarter + "' and Open_Quarter eq true and Appraisal_Stages eq 'Evaluation'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0) allow = true;

            return allow;
        }

        public static bool[] Qts_Setup(string Appraisal_Period)
        {
            var Qts = new bool[4];

            var page = "HRAppraisalPeriodSetup?$filter=Code eq '" + Appraisal_Period +
                       "' and Open_Quarter eq true&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                {
                    if ((string)config["Appraisal_Type"] == "Quater 1")
                        Qts[0] = true;
                    else
                        Qts[0] = false;
                    if ((string)config["Appraisal_Type"] == "Quater 2")
                        Qts[1] = true;
                    else
                        Qts[1] = false;
                    if ((string)config["Appraisal_Type"] == "Quater 3")
                        Qts[2] = true;
                    else
                        Qts[2] = false;
                    if ((string)config["Appraisal_Type"] == "Quater 4")
                        Qts[3] = true;
                    else
                        Qts[3] = false;
                }

            return Qts;
        }

        public static string GetEmployeeName(string StaffNo)
        {
            var Name = "";
            var page = "EmployeeList?$select=First_Name,Middle_Name,Last_Name&$filter=No eq '" + StaffNo +
                       "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["First_Name"] + " " + (string)config["Middle_Name"] + " " +
                           (string)config["Last_Name"];

            return Name;
        }
         public static string GetEmployeeName2(string StaffNo)
        {
            var Name = "";
            var page = "EmployeeList?$select=First_Name,Middle_Name,Last_Name&$filter=User_ID eq '" + StaffNo +
                       "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["First_Name"] + " " + (string)config["Middle_Name"] + " " +
                           (string)config["Last_Name"];

            return Name;
        }

        public static string GetEmployeeFirstName(string StaffNo)
        {
            var Name = "";

            var page = "EmployeeList?$select=First_Name,Middle_Name,Last_Name&$filter=No eq '" + StaffNo +
                       "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["First_Name"];

            return Name;
        }

        public static string GetEmployeeEmail(string StaffNo)
        {
            var Name = "";

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["Company_E_Mail"];

            return Name;
        }
        public static string GetEmployeePhone(string StaffNo)
        {
            var Name = "";

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["Phone_No"];

            return Name;
        }
        public static string[] GetEmployeeSalutationDetails(string StaffNo)
        {
            var Name = new string[2] { "", "" };

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                {
                    Name[1] = (string)config["Job_Title2"];
                    Name[0] = (string)config["Title"];
                }

            return Name;
        }
        public static string GetEmployeeCategory(string StaffNo)
        {
            var category = "";

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Any())
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    category = (string)config["Employee_Category_Type"];
                }

            return category;
        }
        public static string GetEmployeeNameByUserID(string UserID)
        {
            var Name = "";

            var page = "UserSetup?$filter=User_ID eq '" + UserID + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["Employee_Name"];
            return Name;
        }
        public static string GetEmployeeNameByNo(string UserID)
        {
            var Name = "";

            var page = "UserSetup?$filter=Employee_No eq '" + UserID + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["Employee_Name"];
            return Name;
        }
        public static string CanScheduleToBank(string userId)
        {
            var Name = "";

            var page = "UserSetup?$filter=User_ID eq '" + userId + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Any())
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    Name = (string)config["Send_EFT_to_Bank"];
                }

            return Name;
        }
        public static string AllowEditBankDetails()
        {
            var Name = "";

            var page = "IntegrationSetup?$filter=Primary_Key eq ''&$format=json";
            var httpResponse = Credentials.GetOdataData(page);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            var firstItem = details["value"].FirstOrDefault();

            if (firstItem != null)
            {
                Name = (string)firstItem["AllowUpdateDetails"];
            }

            return Name;
        }
        public static string GetEmployeeBankName(string bankCode)
        {
            string name = "";

            string page = "EmployeeBankList?$filter=Code eq '" + bankCode + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);

            if (details["value"] != null && details["value"].Any())
            {
                name = (string)details["value"][0]?["Bank_Name"];
            }

            return name;
        }
        public static string GetEmployeeBankBranchName(string bankCode, string branchCode)
        {
            string name = "";

            string page = "EmployeeBankList?$filter=Code eq '" + bankCode + "' and Bank_Branch_No eq '" + branchCode + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);

            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);

            if (details["value"] != null && details["value"].Any())
            {
                name = (string)details["value"][0]?["Branch_Name"];
            }

            return name;
        }
        public static string GetEmployeeApproverUserID(string UserID)
        {
            var Name = "";
            var page = "ApprovalUserSetup?$filter=User_ID eq '" + UserID + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["Approver_ID"];

            return Name;
        }

        public static string GetEmployeeSupervisorID(string UserID)
        {
            var Name = "";

            var page = "ApprovalUserSetup?$filter=User_ID eq '" + UserID + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["Approver_ID"];

            return Name;
        }
        public static string GetDocumentApproverUserID(string TbID, string DocNo)
        {
            var Name = "";

            var
                page = "ApprovalEntries?$filter=Document_No eq '" + DocNo + "' and Status eq 'Open'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Name = (string)config["Approver_ID"];

            return Name;
        }

        public static string GetEmployeeRelieverUserID(string UserID)
        {
            var Reliver_Code = "";

            var page = "EmployeeList?$filter=No eq '" + UserID + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    Reliver_Code = (string)config["Reliver_Code"];

            return Reliver_Code;
        }

        public static string GetEmployeeGender(string StaffNo)
        {
            var gender = "";

            var page = "EmployeeCard?$select=Gender&$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    gender = (string)config["Gender"];

            return gender;
        }

        public static string GetEmployeeIdNo(string staffNo)
        {
            var IDNo = "";

            var page = "EmployeeList?$select=ID_Number&$filter=No eq '" + staffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Any())
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    IDNo = (string)config["ID_Number"];
                }

            return IDNo;
        }

        public static string GetEmployeeUserID(string StaffNo)
        {
            var IDNo = "";

            var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Count() > 0)
                foreach (JObject config in details["value"])
                    IDNo = (string)config["ID_Number"];

            return IDNo;
        }

        public static bool SaveUploadedFile(string base64String, string filePath)
        {
            var Uploaded = false;
            try
            {
                File.WriteAllBytes(filePath, Convert.FromBase64String(base64String));
                Uploaded = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return Uploaded;
        }

        public static decimal GetStaffCustomerBal(string CustNo)
        {
            decimal Bal = 0;
            try
            {
                var page = "CustomerList?$filter=No eq '" + CustNo + "'&format=json";

                var httpResponseResC = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponseResC.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                if (details["value"].Count() > 0)
                    foreach (JObject config in details["value"])
                        Bal = (decimal)config["Balance_LCY"];
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return Bal;
        }

        public static int GetApprovals(string userID)
        {
            var Bal = 0;
            try
            {
                Bal = Credentials.ObjNav.CountApprovals(userID);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return Bal;
        }

        public static int GetTimesheets(string userID)
        {
            var Bal = 0;
            try
            {
                Bal = Credentials.ObjNav.CountUnfilledTimesheets(userID);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return Bal;
        }

        public static decimal[] TravelAdvancePerMonth(string StaffNo)
        {
            decimal[] cp = { 0 };
            try
            {
                var Datefilter = "01/01/" + Convert.ToString(DateTime.Now.Year);

                cp = Credentials.ObjNav.TravelAdvancePerMonth(StaffNo, Convert.ToDateTime(Datefilter));
            }
            catch (Exception EX)
            {
                EX.Data.Clear();
            }

            return cp;
        }

        public static decimal[] TravelLiquidationPerMonth(string StaffNo)
        {
            decimal[] cp = { 0 };
            try
            {
                var Datefilter = "01/01/" + Convert.ToString(DateTime.Now.Year);

                cp = Credentials.ObjNav.TravelLiquidationPerMonth(StaffNo, Convert.ToDateTime(Datefilter));
            }
            catch (Exception EX)
            {
                EX.Data.Clear();
            }

            return cp;
        }

        public static decimal[] StaffClaimPerMonth(string StaffNo)
        {
            decimal[] cp = { 0 };
            try
            {
                var Datefilter = "01/01/" + Convert.ToString(DateTime.Now.Year);

                cp = Credentials.ObjNav.StaffClaimPerMonth(StaffNo, Convert.ToDateTime(Datefilter));
            }
            catch (Exception EX)
            {
                EX.Data.Clear();
            }

            return cp;
        }
        public static int ImprestWarrants(string StaffNo)
        {
            var imprestWarranties = new List<ImprestWarranties>();
            try
            {

                string page = "ImprestWarranties?$filter=Account_No eq '" + StaffNo + "'&$format=json";


                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var imprest = new ImprestWarranties();
                    imprestWarranties.Add(imprest);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return imprestWarranties.Count();
        }
        public static int StaffClaims(string StaffNo)
        {
            var imprestWarranties = new List<ImprestWarranties>();
            try
            {

                var page = "StaffClaims?$filter=Account_No eq '" + StaffNo + "' and Document_Type eq 'Staff Claims'&format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var imprest = new ImprestWarranties();
                    imprestWarranties.Add(imprest);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return imprestWarranties.Count();
        }
        public static decimal[] WorkshopAdvancePerMonth(string StaffNo)
        {
            decimal[] cp = { 0 };
            try
            {
                var Datefilter = "01/01/" + Convert.ToString(DateTime.Now.Year);

                cp = Credentials.ObjNav.WorkshopAdvancePerMonth(StaffNo, Convert.ToDateTime(Datefilter));
            }
            catch (Exception EX)
            {
                EX.Data.Clear();
            }

            return cp;
        }

        public static bool IfFileExists(string FileName)
        {
            var s = false;
            try
            {
                if (File.Exists(FileName)) File.Delete(FileName);
                s = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return s;
        }

        public static string GetDocRejectionComment(string DocNo, int SeqNo)
        {
            var comment = "";
            try
            {
                var page = "ApprovalComments?$select=Comment&$filter=Document_No eq '" + DocNo +
                           "' and Sequence_No eq " + SeqNo + "&format=json";
                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                if (details["value"].Count() > 0)
                    foreach (JObject config in details["value"])
                        comment = (string)config["Comment"];
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return comment;
        }

        public static decimal[] GetLeaveBal(string StaffNo, string LvType)
        {
            var LvDays = new decimal[5];
            try
            {
                LvDays = Credentials.ObjNav.GetLeaveBalances(StaffNo, LvType);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return LvDays;
        }


        public int GetLeaveBalWithoutFunction(string staffNo, string lvType)
        {
            int leaveBalance = 0;

            try
            {
                var page = $"EmployeeLeaveBalances?$filter=No eq '{staffNo}'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var json = streamReader.ReadToEnd();

                var data = JObject.Parse(json);
                var item = data["value"]?.FirstOrDefault();

                if (item != null)
                {
                    leaveBalance = (int?)item["Leave_Outstanding_Bal"] ?? 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching leave balance: {ex.Message}");
            }

            return leaveBalance;
        }



        public static string GetFixedAssetDescription(string AssetNo)
        {
            var s = "";
            try
            {
                var page = "FixedAssetsList?$&select=Description&$filter=No eq '" + AssetNo + "'&$format=json";

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"]) s = (string)config["Description"];
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return s;
        }

        public static bool LeavePlannerExists(string StaffNo)
        {
            var exists = false;
            try
            {
                var Department = EmployeeDepartment(StaffNo);

                var page = "LeavePlannerList?$filter=Responsibility_Center eq '" + Department + "'&format=json";

                var leavePlannerList = new List<LeavePlannerList>();

                var httpResponse = Credentials.GetOdataData(page);
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"]) exists = true;
            }
            catch (Exception ex)
            {
                var erroMsg = new Error();
                erroMsg.Message = ex.Message.Replace("'", "");
            }


            return exists;
        }

        public static bool isDirector(string StaffNo)
        {
            bool Director_Registrar = false;

            try
            {
                var page = "EmployeeList?$filter=No eq '" + StaffNo + "'&$format=json";
                var httpResponse = Credentials.GetOdataData(page);

                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                if (details["value"] != null && details["value"].Any())
                {
                    foreach (var jToken in details["value"])
                    {
                        var config = (JObject)jToken;
                        if (config["Director_Registrar"] != null && config["Director_Registrar"].Type != JTokenType.Null)
                        {
                            Director_Registrar = (bool)config["Director_Registrar"];
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                Director_Registrar = false;
            }

            return Director_Registrar;
        }

    }
}