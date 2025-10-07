using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Latest_Staff_Portal.CustomSecurity;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Latest_Staff_Portal.Controllers;

[CustomeAuthentication]
[CustomAuthorization(Role = "ALLUSERS,ACCOUNTANTS,PROCUREMENT")]
public class CommonController : Controller
{
    private static readonly HttpClient client = new HttpClient();

    // GET: Common        
    public JsonResult GetServiceList()
    {
        try
        {
            #region Service List

            var ddlList = new List<DropdownList>();
            var page =
                "Item_Service?$select=No,Name&$filter=Gen_Prod_Posting_Group eq 'SERVICES' and Account_Type eq 'Posting' and Direct_Posting eq true&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var dll = new DropdownList();
                    dll.Value = (string)config["No"];
                    dll.Text = (string)config["Code"] + "  " + (string)config["Name"];
                    ddlList.Add(dll);
                }
            }

            #endregion

            var DropDownData = new DropdownListData
            {
                ListOfddlData = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return Json(new { DropDownData, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult GetServiceListFiltered(string ExpenseCode)
    {
        try
        {
            #region Service List

            var ddlList = new List<DropdownList>();
            var page =
                "Item_Service?$select=No,Name&$filter=Gen_Prod_Posting_Group eq 'SERVICES' and Expense_Code eq '" +
                ExpenseCode + "' and Account_Type eq 'Posting' and Direct_Posting eq true&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var dll = new DropdownList();
                    dll.Value = (string)config["No"];
                    dll.Text = (string)config["Code"] + "  " + (string)config["Name"];
                    ddlList.Add(dll);
                }
            }

            #endregion

            var DropDownData = new DropdownListData
            {
                ListOfddlData = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return Json(new { DropDownData, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult GetItemList()
    {
        try
        {
            #region Items List

            var ddlList = new List<DropdownList>();
            var page = "Item_List?$&orderby=Description&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var dll = new DropdownList();
                    dll.Value = (string)config["No"];
                    dll.Text = (string)config["Description"] + " _ (" + (string)config["Inventory"] + ")";
                    ddlList.Add(dll);
                }
            }

            #endregion

            var DropDownData = new DropdownListData
            {
                ListOfddlData = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return Json(new { DropDownData, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult GetItems(string procurementCategory)
    {
        try
        {
            #region Items List

            var ddlList = new List<DropdownList>();
            var page =
                "Item_List?$filter=(Type eq 'Non-Inventory' or Type eq 'Inventory') and Procurement_Category eq '" +
                procurementCategory + "'&format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var dll = new DropdownList();
                    dll.Value = (string)config["No"];
                    dll.Text = (string)config["No"] + " " + (string)config["Description"];
                    ddlList.Add(dll);
                }
            }

            #endregion

            var dropDownData = new DropdownListData
            {
                ListOfddlData = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return Json(new { DropDownData = dropDownData, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult GetFixedAssets()
    {
        try
        {
            #region Items List

            var ddlList = new List<DropdownList>();
            var page = "FixedAssets?$&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var dll = new DropdownList();
                    dll.Value = (string)config["No"];
                    dll.Text = (string)config["Description"];
                    ddlList.Add(dll);
                }
            }

            #endregion

            var DropDownData = new DropdownListData
            {
                ListOfddlData = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return Json(new { DropDownData, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public decimal GetItemCost(string ItemNo)
    {
        decimal cost = 0;
        try
        {
            #region Items List

            var ddlList = new List<DropdownList>();
            var page = "Item_List?$&filter=No_ eq '" + ItemNo + "'&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);


            foreach (JObject config in details["value"])
                cost = Convert.ToDecimal(config["Unit_Cost"].ToString());

            #endregion
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
        }

        return cost;
    }

    public JsonResult GetUnitCost(string ItemNo)
    {
        try
        {
            decimal cost = 0;
            // cost = Credentials.ObjNav.ReturnUnitCost(ItemNo);
            return Json(cost, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult GetFixedAssetList()
    {
        try
        {
            #region Items List

            var ddlList = new List<DropdownList>();
            var page = "FixedAssetsList?$&orderby=Description&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var dll = new DropdownList();
                    dll.Value = (string)config["No"];
                    dll.Text = (string)config["Description"] + "~" + (string)config["Search_Description"];
                    ddlList.Add(dll);
                }
            }

            #endregion

            var DropDownData = new DropdownListData
            {
                ListOfddlData = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return Json(new { DropDownData, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public void NullibySessions()
    {
        Session["SuccessMsg"] = null;
        Session["ErrorMsg"] = null;
    }

    public JsonResult GetServiceList2()
    {
        try
        {
            #region Service List

            var ddlList = new List<DropdownList>();
            var page = "GLAccounts?$select=No,Name&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var dll = new DropdownList();
                    dll.Value = (string)config["No"];
                    dll.Text = (string)config["No"] + "  " + (string)config["Name"];
                    ddlList.Add(dll);
                }
            }

            #endregion

            var dropDownData = new DropdownListData
            {
                ListOfddlData = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return Json(new { DropDownData = dropDownData, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }


    public JsonResult DelegateApproval(string DocNo)
    {
        try
        {
            var rtVal = false;
            var msg = "";

            if (Session["UserID"] != null)
            {
                var userID = Session["UserID"].ToString();
                Credentials.ObjNav.DelegateApproval(DocNo);
                rtVal = true;
                msg = $"Document {DocNo} Delegated Successfully";
            }
            else
            {
                rtVal = false;
                msg = "/Login/Login";
            }

            return Json(new { message = msg, success = true, Redirect = rtVal }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public PartialViewResult DocumentApprovalTrail(string DocNo, string RecID)
    {
        var StaffNo = Session["Username"].ToString();
        var ApprovalTrail = new List<ApprovalEntries>();
        var page = "";
        //if (RecID.Contains("&"))
        //{
        //    page = "ApprovalEntries?$filter=Document_No eq '" + DocNo + "' and Status ne 'Canceled' and Status ne 'Rejected'&$format=json";
        //}
        //else
        //{
        //    page = "ApprovalEntries?$filter=Record_ID_to_Approve eq '" + RecID + "' and Status ne 'Canceled' and Status ne 'Rejected'&$format=json";
        //}
        page = "ApprovalEntries?$filter=Document_No eq '" + DocNo +
               "' and Status ne 'Canceled' and Status ne 'Rejected'&$format=json";
        var httpResponse = Credentials.GetOdataData(page);
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            foreach (var jToken in details["value"])
            {
                var config = (JObject)jToken;
                var AppTra = new ApprovalEntries
                {
                    DocNo = DocNo,

                    UserID = ((string)config["Approver_ID"]),
                    Name = CommonClass.GetEmployeeNameByUserID((string)config["Approver_ID"]),
                    DateSendForApproval = ((DateTime)config["Date_Time_Sent_for_Approval"]).ToString("dd/MM/yyyy"),
                    DueDate = ((DateTime)config["Last_Date_Time_Modified"]).ToString(),
                    Status = (string)config["Status"]
                };
                // 50186
                if ((string)config["Table_ID"] == "69205")
                {
                    if ((((string)config["Status"] == "Approved") && ((string)config["Sequence_No"] == "1"))) { AppTra.Status = "Forwarded"; }
                    if ((string)config["Status"] == "Created") AppTra.Status = "";
                }

                if ((string)config["Table_ID"] == "50186")
                {

                }

                AppTra.Sequence = Convert.ToInt32((string)config["Sequence_No"]);
                ApprovalTrail.Add(AppTra);
            }
        }

        return PartialView("~/Views/Shared/Partial Views/ApprovalTrail.cshtml",
            ApprovalTrail.OrderBy(x => x.Sequence).ToList());
    }



    public PartialViewResult DocumentApprovalComments(string DocNo)
    {
        var StaffNo = Session["Username"].ToString();
        var docComments = new List<DocumentComments>();
      
        var RecordIDText = "";


        var page1 = $"ApprovalEntries?$filter=Document_No eq '{DocNo}'&$format=json";
        var httpResponse1 = Credentials.GetOdataData(page1);

        using (var streamReader = new StreamReader(httpResponse1.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);

            var firstEntry = details["value"].FirstOrDefault();
            if (firstEntry != null)
            {
                RecordIDText = (string)firstEntry["RecordIDText"];
            }
        }


        var page = "ApprovalComments?$filter=Record_ID_to_Approve eq '" + RecordIDText + "' &$format=json";
        var httpResponse = Credentials.GetOdataData(page);
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            foreach (var jToken in details["value"])
            {
                var config = (JObject)jToken;
                var comment = new DocumentComments
                {
                    Entry_No = (int)config["Entry_No"],
                    Rejection_Reason = (string)config["Rejection_Reason"],
                    Conditions_for_Approval = (string)config["Conditions_for_Approval"],
                    Comment = (string)config["Comment"],
                    User_ID = (string)config["User_ID"],
                    Date_and_Time = (string)config["Date_and_Time"],
                    Table_ID = (int)config["Table_ID"],
                    Record_ID_to_Approve = (string)config["Record_ID_to_Approve"]
                };


                docComments.Add(comment);
            }
        }

        return PartialView("~/Views/Common/PartialViews/DocumentApprovalComments.cshtml", docComments);
    }



    private string UrlEncode(string url)
    {
        var toBeEncoded = new Dictionary<string, string>
        {
            { "%", "%25" }, { "!", "%21" }, { "#", "%23" }, { " ", "%20" },
            { "$", "%24" }, { "&", "%26" }, { "'", "%27" }, { "(", "%28" }, { ")", "%29" }, { "*", "%2A" },
            { "+", "%2B" }, { ",", "%2C" },
            { "/", "%2F" }, { ":", "%3A" }, { ";", "%3B" }, { "=", "%3D" }, { "?", "%3F" }, { "@", "%40" },
            { "[", "%5B" }, { "]", "%5D" }
        };
        var replaceRegex = new Regex(@"[%!# $&'()*+,/:;=?@\[\]]");
        MatchEvaluator matchEval = match => toBeEncoded[match.Value];
        var encoded = replaceRegex.Replace(url, matchEval);
        return encoded;
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public PartialViewResult FileUploadForm(string lineNo = null)
    {
        ViewBag.LineNo = lineNo;
        return PartialView("~/Views/Shared/Partial Views/FileAttachmentForm.cshtml");
    }

    public PartialViewResult FileUploadForm2(string AttachmentCode)
    {
        ViewBag.AttachmentCode = AttachmentCode;
        return PartialView("~/Views/Shared/Partial Views/FileAttachmentForm2.cshtml");
    }
    public PartialViewResult Dimensions(string dimensionSetId)
    {
        try
        {
            var assignedAssetList = new List<DimensionSetValues>();
            var page = "DimensionSetEntries?$filter=Dimension_Set_ID eq " + dimensionSetId + "&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var assignedAsset = new DimensionSetValues
                    {
                        DimensionCode = (string)config["Dimension_Code"],
                        DimensionName = (string)config["Dimension_Name"],
                        DimensionValueCode = (string)config["DimensionValueCode"],
                        DimensionValueName = (string)config["Dimension_Value_Name"]
                    };
                    assignedAssetList.Add(assignedAsset);
                }
            }

            return PartialView("Dimensions", assignedAssetList);
        }
        catch (Exception ex)
        {
            var assignedAssetList = new List<DimensionSetValues>();
            return PartialView("Dimensions", assignedAssetList);
        }
    }
    public PartialViewResult DocumentAttachments(string DocNo, string documentStatus)
    {
        #region Document Attachment

        var DocAttachment = new List<DocumentAttachment>();
        var page = "DocumentAttachment?$filter=No eq '" + DocNo + "'&$format=json";


        var httpResponseResC = Credentials.GetOdataData(page);
        using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);

            foreach (JObject config in details["value"])
            {
                var docAttList = new DocumentAttachment();
                docAttList.TableId = (int)config["Table_ID"];
                docAttList.No = (string)config["No"];
                docAttList.DocumentType = (string)config["Document_Type"];
                docAttList.LineNo = (int)config["Line_No"];
                docAttList.ID = (int)config["ID"];
                docAttList.Name = (string)config["Name"];
                docAttList.DocumentCategory = (string)config["Document_Category"];
                docAttList.DocumentDescription = (string)config["Document_Description"];
                docAttList.FileExtension = (string)config["File_Extension"];
                docAttList.FileType = (string)config["File_Type"];
                docAttList.User = (string)config["User"];
                docAttList.AttachedDate = DateTime.Parse((string)config["Attached_Date"]);
                docAttList.DocumentFlowPurchase = (bool)config["Document_Flow_Purchase"];
                docAttList.DocumentFlowSales = (bool)config["Document_Flow_Sales"];
                docAttList.DocumentId = (string)config["Document_ID"];
                DocAttachment.Add(docAttList);
            }
        }

        #endregion

        ViewBag.documentStatus = documentStatus;

        return PartialView("~/Views/Shared/Partial Views/ImportantDocsToApprove.cshtml", DocAttachment);
    }
    
    public PartialViewResult DocumentAttachments2(string DocNo, string status)
    {
        #region Document Attachment

        ViewBag.Status = status;

        var DocAttachment = new List<DocumentAttachment>();
        var page = "DocumentAttachment?$filter=No eq '" + DocNo + "'&$format=json";


        var httpResponseResC = Credentials.GetOdataData(page);
        using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);

            foreach (JObject config in details["value"])
            {
                var docAttList = new DocumentAttachment();
                docAttList.TableId = (int)config["Table_ID"];
                docAttList.No = (string)config["No"];
                docAttList.DocumentType = (string)config["Document_Type"];
                docAttList.LineNo = (int)config["Line_No"];
                docAttList.ID = (int)config["ID"];
                docAttList.Name = (string)config["Name"];
                docAttList.DocumentCategory = (string)config["Document_Category"];
                docAttList.DocumentDescription = (string)config["Document_Description"];
                docAttList.FileExtension = (string)config["File_Extension"];
                docAttList.FileType = (string)config["File_Type"];
                docAttList.User = (string)config["User"];
                docAttList.AttachedDate = DateTime.Parse((string)config["Attached_Date"]);
                docAttList.DocumentFlowPurchase = (bool)config["Document_Flow_Purchase"];
                docAttList.DocumentFlowSales = (bool)config["Document_Flow_Sales"];
                docAttList.DocumentId = (string)config["Document_ID"];
                DocAttachment.Add(docAttList);
            }
        }

        #endregion

        return PartialView("~/Views/Shared/Partial Views/ImportantDocsToApprove2.cshtml", DocAttachment);
    }
    public PartialViewResult DocumentAttachmentList(string DocNo, int TableID, string Status)
    {
        #region Document Attachment

        var DocAttachment = new List<DocumentAttachment>();
        var page = "DocumentAttachment?$filter=Table_ID eq " + TableID + " and No eq '" + DocNo + "'&format=json";

        var httpResponseResC = Credentials.GetOdataData(page);
        using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);

            foreach (JObject config in details["value"])
            {
                var docAttList = new DocumentAttachment();
                docAttList.TableId = (int)config["Table_ID"];
                docAttList.No = (string)config["No"];
                docAttList.DocumentType = (string)config["Document_Type"];
                docAttList.LineNo = (int)config["Line_No"];
                docAttList.ID = (int)config["ID"];
                docAttList.Name = (string)config["Name"];
                docAttList.DocumentCategory = (string)config["Document_Category"];
                docAttList.DocumentDescription = (string)config["Document_Description"];
                docAttList.FileExtension = (string)config["File_Extension"];
                docAttList.FileType = (string)config["File_Type"];
                docAttList.User = (string)config["User"];
                docAttList.AttachedDate = DateTime.Parse((string)config["Attached_Date"]);
                docAttList.DocumentFlowPurchase = (bool)config["Document_Flow_Purchase"];
                docAttList.DocumentFlowSales = (bool)config["Document_Flow_Sales"];
                docAttList.DocumentId = (string)config["Document_ID"];
                DocAttachment.Add(docAttList);
            }
        }

        #endregion

        var documentList = new DocumentAttachmentList
        {
            Status = Status,

            DocList = DocAttachment
        };
        return PartialView("~/Views/Shared/Partial Views/ImportantDocs.cshtml", documentList);
    }
    [HttpPost]
    public PartialViewResult EdmsDocumentAttachmentList(string DocNo, string documentStatus, int? LineNo)
    {
        var docAttachment = new List<EdmsDocuments>();

        if (string.IsNullOrEmpty(DocNo))
        {
            ViewBag.Message = "Document number is missing or invalid.";
            ViewBag.Status = documentStatus;
            ViewBag.AttachmentCode = DocNo;
            return PartialView("EdmsDocumentAttachmentList", docAttachment);
        }

        try
        {
            string page;

            if (LineNo.HasValue)
            {
                page = $"Attachments?$filter=Document_No eq '{DocNo}' and itemNo eq {LineNo}&format=json";
            }
            else
            {
                page = $"Attachments?$filter=Document_No eq '{DocNo}'&format=json";
            }

            var httpResponseResC = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponseResC.GetResponseStream());
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);

            ViewBag.Status = documentStatus;
            ViewBag.AttachmentCode = DocNo;

            foreach (var jToken in details["value"])
            {
                var config = (JObject)jToken;
                var docAttList = new EdmsDocuments
                {
                    LineNo = (int)config["LineNo"],
                    DocumentNo = (string)config["Document_No"],
                    Link = (string)config["Link"],
                    Module = (string)config["Module"],
                    FileType = (string)config["FileType"],
                    User = (string)config["User"],
                    FileName = (string)config["FileName"],
                    DocumentId = (string)config["DocumentID"],
                    OrderNo = (string)config["Order_No"]
                };
                docAttachment.Add(docAttList);
            }
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while fetching document attachments: " + ex.Message;
        }

        return PartialView("EdmsDocumentAttachmentList", docAttachment);
    }

    public PartialViewResult EdmsDocumentAttachmentList2(string DocNo, string documentStatus)
    {
        #region Document Attachment
        var docAttachment = new List<EdmsDocuments>();
        if (string.IsNullOrEmpty(DocNo))
        {
            // Optionally set a message or log the issue
            ViewBag.Message = "Document number is missing or invalid.";
            ViewBag.Status = documentStatus;
            ViewBag.AttachmentCode = DocNo;
            return PartialView("EdmsDocumentAttachmentList", docAttachment);
        }

        try
        {
            var page = "Attachments?$filter=Document_No eq '" + DocNo + "'&format=json";
            var httpResponseResC = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                ViewBag.Status = documentStatus;
                ViewBag.AttachmentCode = DocNo;

                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var docAttList = new EdmsDocuments
                    {
                        LineNo = (int)config["LineNo"],
                        DocumentNo = (string)config["Document_No"],
                        Link = (string)config["Link"],
                        Module = (string)config["Module"],
                        FileType = (string)config["FileType"],
                        User = (string)config["User"],
                        FileName = (string)config["FileName"],
                        DocumentId = (string)config["DocumentID"],
                        OrderNo = (string)config["Order_No"]
                    };
                    docAttachment.Add(docAttList);
                }
            }
        }
        catch (Exception ex)
        {
            // Log the error and optionally return a meaningful message
            ViewBag.ErrorMessage = "An error occurred while fetching document attachments: " + ex.Message;
        }
        #endregion
        return PartialView("EdmsDocumentAttachmentList2", docAttachment);
    }

    public async Task<ActionResult> GetEdmsDocuments(string module, string documentNo, string documentType, string documentId)
    {
        try
        {
            var result = await RetrieveDocuments.RetrieveDocument(module, documentNo, documentType, documentId);

            if (result.Base64 != null)
            {
                var jsonResponse = JsonConvert.SerializeObject(new
                {
                    base64 = result.Base64,
                    mimeType = result.MimeType,
                    success = true
                });

                return Content(jsonResponse, "application/json");
            }

            var notFoundResponse = JsonConvert.SerializeObject(new { message = "File not found", success = false });
            return Content(notFoundResponse, "application/json");
        }
        catch (Exception ex)
        {
            var errorResponse = JsonConvert.SerializeObject(new { message = ex.Message, success = false });
            return Content(errorResponse, "application/json");
        }
    }

    public async Task<JsonResult> RemoveFileAsync(string documentId, string lineNo)
    {
        var apiUrl = ConfigurationManager.AppSettings["EDMSRemoveUrl"];
        var jsonData = $"{{\"document_Id\":\"{documentId}\"}}";

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };


        using var client = new HttpClient(handler);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        try
        {
            Credentials.ObjNav.deleteAttachDocs(documentId, Convert.ToInt32(lineNo));
            var response = await client.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                switch (result.status_code)
                {
                    case 0:
                        return Json(new { result.message, success = true }, JsonRequestBehavior.AllowGet);
                    case 1:
                        return Json(new { result.message, success = false }, JsonRequestBehavior.AllowGet);
                    default:
                        return Json(new { message = "Unexpected response from the server.", success = false }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { message = "Error deleting file! Kindly try again", success = false }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = $"An error occurred: {ex.Message}", success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public PartialViewResult DocumentAttachmentsToApprove(string DocNo, int TableID)
    {
        #region Document Attachment

        var DocAttachment = new List<DocumentAttachment>();
        var page = "DocumentAttachment?$filter=Table_ID eq " + TableID + " and No eq '" + DocNo + "'&$format=json";

        var httpResponseResC = Credentials.GetOdataData(page);
        using (var streamReader = new StreamReader(httpResponseResC.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);

            foreach (JObject config in details["value"])
            {
                var docAttList = new DocumentAttachment();
                docAttList.TableId = (int)config["Table_ID"];
                docAttList.No = (string)config["No"];
                docAttList.DocumentType = (string)config["Document_Type"];
                docAttList.LineNo = (int)config["Line_No"];
                docAttList.ID = (int)config["ID"];
                docAttList.Name = (string)config["Name"];
                docAttList.DocumentCategory = (string)config["Document_Category"];
                docAttList.DocumentDescription = (string)config["Document_Description"];
                docAttList.FileExtension = (string)config["File_Extension"];
                docAttList.FileType = (string)config["File_Type"];
                docAttList.User = (string)config["User"];
                docAttList.AttachedDate = DateTime.Parse((string)config["Attached_Date"]);
                docAttList.DocumentFlowPurchase = (bool)config["Document_Flow_Purchase"];
                docAttList.DocumentFlowSales = (bool)config["Document_Flow_Sales"];
                docAttList.DocumentId = (string)config["Document_ID"];
                DocAttachment.Add(docAttList);
            }
        }

        #endregion

        return PartialView("~/Views/Shared/Partial Views/ImportantDocsToApprove.cshtml", DocAttachment);
    }

    [HttpPost]
    public JsonResult SaveAttachedFile(string DocNo, string base64Upload, string fileName, string Extn, int TableID, string module, string documentType, string vendor, string lineNo)
    {
        try
        {
            var successVal = false;
            var msg = "";
            var lineNumber = 0;
            if (!string.IsNullOrEmpty(lineNo))
            {
                lineNumber = Convert.ToInt32(lineNo);
            }
            if (base64Upload != "" && Extn != "")
            {
                var ext = Path.GetExtension(fileName);

                if (ext.ToLower() == ".pdf" || ext.ToLower() == ".docx" || ext.ToLower() == ".doc" ||
                    ext.ToLower() == ".xlsx" ||
                    ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".png")
                {
                    var filePath = Server.MapPath("~/Uploads/" + fileName);
                    Credentials.ObjNav.UploadAttachedDocument(DocNo,fileName, base64Upload, TableID);
                  
                    msg = "Attachment uploaded successfully";
                    successVal = true;
                   
                }
                else
                {
                    msg = "Only files with extensions(.pdf, .docx, .doc, .xlsx, .jpeg, .jpg, .png) can be uploaded";
                    successVal = false;
                }
            }
            else
            {
                msg = "Incorrect file!!";
                successVal = false;
            }

            return Json(new { message = msg, success = successVal }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    [HttpGet]
    public virtual ActionResult AttachmentDownload(string fileName)
    {
        var fullPath = Server.MapPath("~/Uploads/" + fileName);
        return File(fullPath, "application/octet-stream", fileName);
    }

    public async Task<ActionResult> ViewAttachedDocument(string tableID, string documentNo, string documentType, string documentId)
    {
        try
        {
            var result = Credentials.ObjNav.GetDocumentAttachment(int.Parse(tableID), documentNo, int.Parse(documentId));

            if (result!= null)
            {
                var jsonResponse = JsonConvert.SerializeObject(new
                {
                    base64 = result,
                    success = true
                });

                return Content(jsonResponse, "application/json");
            }

            var notFoundResponse = JsonConvert.SerializeObject(new { message = "File not found", success = false });
            return Content(notFoundResponse, "application/json");
        }
        catch (Exception ex)
        {
            var errorResponse = JsonConvert.SerializeObject(new { message = ex.Message, success = false });
            return Content(errorResponse, "application/json");
        }
    }

    public JsonResult DeleteAttachedDocument(string DocNo, int lineNo, int tblID, int DocID)
    {
        try
        {
            Credentials.ObjNav.DeleteDocumentAttachment(DocNo, tblID, DocID);
            return Json(new { message = "Attachment file deleted successfully", success = true });
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false });
        }
    }

    public PartialViewResult MyDocumentComments(string DocNo)
    {
        try
        {
            var userID = Session["UserID"].ToString();
            var docComments = new List<DocComments>();

            var page = "ApprovalComments?select=Comment&$filter=Document_No eq '" + DocNo + "' and User_ID eq '" +
                       userID + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (JObject config in details["value"])
                {
                    var comment = new DocComments();
                    comment.Comment = config["Comment"].ToString();
                    docComments.Add(comment);
                }
            }

            return PartialView("~/Views/DocumentApproval/Document Approval Views/DocumentComments.cshtml",
                docComments);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }

    public PartialViewResult DocumentComments(string DocNo)
    {
        try
        {
            var userID = Session["UserID"].ToString();
            var docComments = new List<DocComments>();

            var page = "ApprovalComments?select=Comment,User_ID&$filter=Document_No eq '" + DocNo +
                       "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var comment = new DocComments();
                    comment.Comment = config["Comment"].ToString();
                    var s = config["User_ID"].ToString().Split('\\');
                    var EmplName = CommonClass.GetEmployeeName(s[1]);
                    if (EmplName != null)
                        comment.CommentBy = EmplName;
                    else
                        comment.CommentBy = (string)config["User_ID"];
                    docComments.Add(comment);
                }
            }

            return PartialView("~/Views/Shared/Partial Views/ApprovalTrail.cshtml", docComments);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }

    public ActionResult ErrorMessange()
    {
        return View();
    }

    public ActionResult Unauthorized()
    {
        return View();
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public JsonResult GetEmployeeList()
    {
        try
        {
            #region Employee List

            var EmployeeList = new List<DropdownList>();
            var StaffNo = Session["Username"].ToString();
            var Department = CommonClass.EmployeeDepartment(StaffNo);
            var page = "EmployeeList?$filter=Status eq 'Active' and Directorate_Code eq '" + Department +
                       "'&format=json";


            var httpResponseCampus = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponseCampus.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var ddl = new DropdownList();
                    ddl.Value = (string)config["No"];
                    ddl.Text = (string)config["FirstName"] + " " + (string)config["MiddleName"] + " " +
                               (string)config["LastName"];
                    EmployeeList.Add(ddl);
                }
            }

            #endregion

            return Json(new { ddlList = EmployeeList, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public JsonResult GetEmployeesByDepartment(string Department)
    {
        try
        {
            var StaffNo = Session["Username"].ToString();
            var NewAppl = new NewLeaveApplication();

            #region ReliverList

            var relieverList = new List<RelieverList>();
            //
            var pageReliever = "EmployeeList?$filter=Status eq 'Active' and Directorate_Code eq '" + Department +
                               "'&format=json";

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

   /* public PartialViewResult DocumentApprovalComments(string docNo)
    {
        try
        {
            var recordIdToApprove = CommonClass.GetRecordToApprove(docNo);
            var approvalComments = new List<DocComments>();
            var page = "ApprovalComments?$select=Comment,User_ID&$filter=Record_ID_to_Approve eq '" + recordIdToApprove + "'&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);
                if (!details["value"].Any())
                    return PartialView("~/Views/Shared/Partial Views/DocumentApprovalComments.cshtml",
                        approvalComments.OrderBy(x => x.Sequence));
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var newComm = new DocComments
                    {
                        Comment = (string)config["Comment"]
                    };
                    var s = config["User_ID"]?.ToString().Split('\\');
                    var emplName = CommonClass.GetEmployeeName(s?[1]);
                    if (emplName == "")
                        newComm.CommentBy = (string)config["User_ID"];
                    else
                        newComm.CommentBy = emplName;
                    approvalComments.Add(newComm);
                }
            }

            return PartialView("~/Views/Shared/Partial Views/DocumentApprovalComments.cshtml",
                approvalComments.OrderBy(x => x.Sequence));
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }
*/
    public PartialViewResult CommonActions(string DocNo, string Status, string DocType)
    {
        try
        {
            var docNo = new DocumentNumber();
            docNo.Code = DocNo;
            docNo.Status = Status;
            docNo.DocType = DocType;
            return PartialView("~/Views/Common/CommonAction.cshtml", docNo);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error();
            erroMsg.Message = ex.Message;
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }

    public JsonResult GetDimensionNames(string Dim1, int DimNo)
    {
        try
        {
            var Listdim2 = new ViewModel.DimensionValues();
            var DName = "";
            var dim = Dim1.Replace("&", "%26");
            var page = "DimensionValues?$filter=Code eq '" + dim + "' and Global_Dimension_No eq " + DimNo +
                       " and Blocked eq false&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                if (details["value"].Count() > 0)
                    foreach (JObject config in details["value"])
                        DName = (string)config["Name"];
            }

            //return Json(DName, JsonRequestBehavior.AllowGet);
            Listdim2 = new ViewModel.DimensionValues
            {
                DName = DName

                //ListOfDim2 = Dim2List.Select(x =>
                //                  new SelectListItem()
                //                  {
                //                      Text = x.Name,
                //                      Value = x.Code
                //                  }).ToList()
            };

            return Json(Listdim2, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult GetDimensionName(string Dim1)
    {
        try
        {
            var Listdim2 = new ViewModel.DimensionValues();
            var DName = "";
            var dim = Dim1.Replace("&", "%26");
            var page = "DimensionValues?$filter=Code eq '" + dim + "' and Blocked eq false&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);
                if (details["value"].Count() > 0)
                    foreach (JObject config in details["value"])
                        DName = (string)config["Name"];
            }

            //return Json(DName, JsonRequestBehavior.AllowGet);
            Listdim2 = new ViewModel.DimensionValues
            {
                DName = DName

                //ListOfDim2 = Dim2List.Select(x =>
                //                  new SelectListItem()
                //                  {
                //                      Text = x.Name,
                //                      Value = x.Code
                //                  }).ToList()
            };

            return Json(Listdim2, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public string GetDimensionNm(string Dim1)
    {
        var DName = "";
        try
        {
            var Listdim2 = new ViewModel.DimensionValues();
            var dim = Dim1.Replace("&", "%26");

            var page = "DimensionValues?$filter=Code eq '" + dim + "' and Blocked eq false&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Any())
                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    DName = (string)config["Name"];
                }
        }
        catch (Exception ex)
        {
        }

        return DName;
    }
    public static string GetStaticDimensionName(string Dim1)
    {
        var DName = "";
        try
        {
            var Listdim2 = new ViewModel.DimensionValues();
            var dim = Dim1.Replace("&", "%26");

            var page = "DimensionValues?$filter=Code eq '" + dim + "' and Blocked eq false&$format=json";
            var httpResponse = Credentials.GetOdataData(page);
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            if (details["value"].Any())
                foreach (JObject config in details["value"])
                    DName = (string)config["Name"];
        }
        catch (Exception ex)
        {
        }

        return DName;
    }
    public JsonResult FilterDimension2(string Dim1)
    {
        try
        {
            var Listdim2 = new ViewModel.DimensionValues();
            // 

            #region dim3

            var Dim2List = new List<ViewModel.DimensionValues>();
            var pageDim2 = "DimensionValues?$filter=Global_Dimension_No eq 2 and (Shortcut_Dimension_1_Code eq '" +
                           Dim1 + "' or Shortcut_Dimension_1_Code eq '') and Blocked eq false&$format=json";
            var apply = false; ///Credentials.ObjNav.ApplyDonorFilter(Dim1);
            if (apply)
                pageDim2 = "DimensionValues?$filter=Global_Dimension_No eq 2 and Shortcut_Dimension_1_Code eq '" +
                           Dim1 + "'  and Blocked eq false&$format=json";
            var httpResponseDim2 = Credentials.GetOdataData(pageDim2);
            using (var streamReader = new StreamReader(httpResponseDim2.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var DList = new ViewModel.DimensionValues();
                    DList.Code = (string)config["Code"];
                    DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    Dim2List.Add(DList);
                }
            }

            var dname = GetDimensionNm(Dim1);

            #endregion

            Listdim2 = new ViewModel.DimensionValues
            {
                DName = dname,
                ListOfDim2 = Dim2List.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList()
            };

            return Json(Listdim2, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult FilterDimension3(string Dim1, string Dim2)
    {
        try
        {
            var Listdim3 = new ViewModel.DimensionValues();
            // 

            #region dim3

            var Dim3List = new List<ViewModel.DimensionValues>();
            // or Shortcut_Dimension_2_Code eq ''
            var pageDim3 = "DimensionValues?$filter=Global_Dimension_No eq 3 and Shortcut_Dimension_1_Code eq '" +
                           Dim1 + "'  and (Shortcut_Dimension_2_Code eq '" + Dim2 +
                           "') and Blocked eq false&$format=json";
            var apply = false; //Credentials.ObjNav.ApplyDonorFilter(Dim1);
            if (apply)
                pageDim3 = "DimensionValues?$filter=Global_Dimension_No eq 3 and Shortcut_Dimension_1_Code eq '" +
                           Dim1 + "'  and Blocked eq false&$format=json";
            var httpResponseDim3 = Credentials.GetOdataData(pageDim3);
            using (var streamReader = new StreamReader(httpResponseDim3.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var DList = new ViewModel.DimensionValues();
                    DList.Code = (string)config["Code"];
                    DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    Dim3List.Add(DList);
                }
            }

            #endregion

            var dname = GetDimensionNm(Dim2);
            Listdim3 = new ViewModel.DimensionValues
            {
                DName = dname,
                ListOfDim3 = Dim3List.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList()
            };

            return Json(Listdim3, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult FilterDimension4(string Dim1, string Dim2, string Dim3)
    {
        try
        {
            var Listdim4 = new ViewModel.DimensionValues();
            // 

            #region dim3

            var Dim4List = new List<ViewModel.DimensionValues>();
            //or Shortcut_Dimension_2_Code eq ''
            var pageDim4 = "DimensionValues?$filter=Global_Dimension_No eq 4 and Shortcut_Dimension_1_Code eq '" +
                           Dim1 + "' and (Shortcut_Dimension_2_Code eq '" + Dim2 +
                           "')  and Shortcut_Dimension_3_Code eq '" + Dim3 + "' and  Blocked eq false&$format=json";
            var apply = false; //Credentials.ObjNav.ApplyDonorFilter(Dim1);
            if (apply)
                pageDim4 = "DimensionValues?$filter=Global_Dimension_No eq 4 and Shortcut_Dimension_1_Code eq '" +
                           Dim1 + "'  and Blocked eq false&$format=json";
            var httpResponseDim4 = Credentials.GetOdataData(pageDim4);
            using (var streamReader = new StreamReader(httpResponseDim4.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (JObject config in details["value"])
                {
                    var DList = new ViewModel.DimensionValues();
                    DList.Code = (string)config["Code"];
                    DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                    Dim4List.Add(DList);
                }
            }

            var dname = GetDimensionNm(Dim3);

            #endregion

            Listdim4 = new ViewModel.DimensionValues
            {
                DName = dname,
                ListOfDim4 = Dim4List.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Code
                    }).ToList()
            };

            return Json(Listdim4, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult FilterDimension7(string Dim1, string Dim3)
    {
        try
        {
            var Listdim7 = new ViewModel.DimensionValues();
            // 

            #region dim3

            var Dim7List = new List<ViewModel.DimensionValues>();
            var dim = Dim3.Replace("&", "%26");
            //or Shortcut_Dimension_2_Code eq ''
            var
                pageDim4 = ""; //"DimensionValues?$filter=Global_Dimension_No eq 7 and Shortcut_Dimension_1_Code eq '" + Dim1 + "' and Shortcut_Dimension_3_Code eq '" + Dim3 + "' and  Blocked eq false&$format=json";
            var apply = false; //Credentials.ObjNav.ApplyDonorFilter(Dim1);
            if (apply)
            {
                pageDim4 = "DimensionValues?$filter=Global_Dimension_No eq 7 and Shortcut_Dimension_1_Code eq '" +
                           Dim1 + "' and Shortcut_Dimension_3_Code eq '" + dim +
                           "' and Blocked eq false&$format=json";

                var httpResponseDim4 = Credentials.GetOdataData(pageDim4);
                using (var streamReader = new StreamReader(httpResponseDim4.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var DList = new ViewModel.DimensionValues();
                        DList.Code = (string)config["Code"];
                        DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                        Dim7List.Add(DList);
                    }
                }

                #endregion

                Listdim7 = new ViewModel.DimensionValues
                {
                    ListOfDim7 = Dim7List.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Code
                        }).ToList()
                };
            }

            return Json(Listdim7, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public JsonResult FilterDimension6(string Dim1)
    {
        try
        {
            var Listdim6 = new ViewModel.DimensionValues();
            // 

            #region dim6

            var Dim6List = new List<ViewModel.DimensionValues>();
            var dim = Dim1.Replace("&", "%26");
            //or Shortcut_Dimension_2_Code eq ''
            var
                pageDim4 = ""; //"DimensionValues?$filter=Global_Dimension_No eq 7 and Shortcut_Dimension_1_Code eq '" + Dim1 + "' and Shortcut_Dimension_3_Code eq '" + Dim3 + "' and  Blocked eq false&$format=json";
            var apply = false; // Credentials.ObjNav.ApplyDonorFilter(Dim1);
            if (apply)
            {
                pageDim4 = "DimensionValues?$filter=Global_Dimension_No eq 6 and Shortcut_Dimension_1_Code eq '" +
                           dim + "' and  Blocked eq false&$format=json";

                var httpResponseDim4 = Credentials.GetOdataData(pageDim4);
                using (var streamReader = new StreamReader(httpResponseDim4.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    var details = JObject.Parse(result);


                    foreach (JObject config in details["value"])
                    {
                        var DList = new ViewModel.DimensionValues();
                        DList.Code = (string)config["Code"];
                        DList.Name = (string)config["Code"] + "  " + (string)config["Name"];
                        Dim6List.Add(DList);
                    }
                }

                #endregion

                Listdim6 = new ViewModel.DimensionValues
                {
                    ListOfDim6 = Dim6List.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Code
                        }).ToList()
                };
            }

            return Json(Listdim6, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public ActionResult FAQsView()
    {
        if (Session["Username"] == null) return RedirectToAction("Login", "Login");

        var faqList = new List<FAQs>();
        var page = "FAQs?&$format=json";

        var httpResponse = Credentials.GetOdataData(page);
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            var details = JObject.Parse(result);
            foreach (JObject config in details["value"])
            {
                var faq = new FAQs();
                //faq.Question = (string)config["No"];
                faq.Question = (string)config["Question"];
                faq.Answer = (string)config["Answer"];
                faq.Created_At = DateTime.Parse((string)config["Created_At"]);
                faqList.Add(faq);
            }
        }

        return View(faqList);
    }
    public ActionResult Notifications(string type)
    {
        try
        {
            ViewBag.Type = type;
            if (Session["Username"] == null) return RedirectToAction("Login", "Login");
            return View();
        }
        catch (Exception ex)
        {
            var errorMsg = new Error
            {
                Message = ex.Message
            };
            return View("~/Views/Common/ErrorMessange.cshtml", errorMsg);
        }
    }
    public PartialViewResult GetNoticesList(string type)
    {
        try
        {
            var dateToday = DateTime.Today.ToString("yyyy-MM-dd");

            var page = type switch
            {
                "Memo" => $"NoticeBoard?$filter=Category eq 'Memo' and Expiration_Date ge {dateToday} &$format=json",
                "Circular" => $"NoticeBoard?$filter=Category eq 'Circular' and Expiration_Date ge {dateToday}&$format=json",
                "Manuals" => $"NoticeBoard?$filter=Category eq 'Manuals' and Expiration_Date ge {dateToday} &$format=json",
                "SOPs" => $"NoticeBoard?$filter=Category eq 'SOPs' and Expiration_Date ge {dateToday}&$format=json",
                "Others" => $"NoticeBoard?$filter=Category eq 'Others' and Expiration_Date ge {dateToday}&$format=json",
                _ => ""
            };

            if (string.IsNullOrEmpty(page))
            {
                return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", new Error { Message = "Invalid category type." });
            }

            var notices = new List<NoticeBoard>();
            var httpResponse = Credentials.GetOdataData(page);

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                notices.AddRange(details["value"]
                    .Cast<JObject>()
                    .Select(config => new NoticeBoard
                    {
                        No = (string)config["No"],
                        Title = (string)config["Title"],
                        Description = (string)config["Description"],
                        Category = (string)config["Category"],
                        Priority = (string)config["Priority"],
                        DatePosted = (string)config["Date_Posted"],
                        ExpirationDate = (string)config["Expiration_Date"], 
                    })
                );
            }
            ViewBag.type = type;
            return PartialView("~/Views/Common/PartialViews/NotificationListPartialView.cshtml", notices);
        }
        catch (Exception ex)
        {
            var erroMsg = new Error { Message = ex.Message };
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }
    }




    public ActionResult HRInformation()
    {
        if (Session["Username"] == null)
            return RedirectToAction("Login", "Login");
        return View();
    }

    public JsonResult GetBudgets()
    {
        try
        {
            // Create and return the JSON result
            #region dim

            var dimensionValues = new List<DropdownList>();
            var pageDim = $"GLBudgets?$filter=Approval_Status eq 'Open' and Budget_Type eq 'Original' and Blocked eq false&$format=json";

            var httpResponsedim = Credentials.GetOdataData(pageDim);
            using (var streamReader = new StreamReader(httpResponsedim.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var dropdownList = new DropdownList();
                    dropdownList.Text = (string)config["Description"];
                    dropdownList.Value = (string)config["Name"];
                    dimensionValues.Add(dropdownList);
                }
            }

            #endregion
            var response = new
            {
                ListOfActivities = dimensionValues.Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList(),
                success = true
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult GetAdminUnit()
    {
        try
        {
            #region dim
            var employeeView = Session["EmployeeData"] as EmployeeView;
            var geographical = employeeView?.GlobalDimension1Code;
            var dimensionValues = new List<DropdownList>();

            var pageDim = $"DimensionValues?$filter=Shortcut_Dimension_1_Code eq '" + geographical + "' and Dimension_Code eq 'ADMINISTRATIVE' and Blocked eq false&$format=json";

            var httpResponsedim = Credentials.GetOdataData(pageDim);
            using (var streamReader = new StreamReader(httpResponsedim.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var dropdownList = new DropdownList();
                    dropdownList.Text = (string)config["Name"];
                    dropdownList.Value = (string)config["Code"];
                    dimensionValues.Add(dropdownList);
                }
            }

            #endregion
            var response = new
            {
                ListOfActivities = dimensionValues.Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList(),
                success = true
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult GetAdminUnits(string geoLocation)
    {
        try
        {
            #region OutComeList

            var ddlList = new List<DropdownList>();
            var page = "DimensionValues?$filter=Global_Dimension_No eq 2 and Blocked eq false and Shortcut_Dimension_1_Code eq '" + geoLocation + "'&$format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var dll = new DropdownList();
                    dll.Value = (string)config["Code"];
                    dll.Text = (string)config["Code"] + "-" + (string)config["Name"];
                    ddlList.Add(dll);
                }
            }

            #endregion

            var dropDownData = new DropdownListData
            {
                ListOfddlData = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return Json(new { DropDownData = dropDownData, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult GetBankAccounts()
    {
        try
        {
            #region dim
            var employeeView = Session["EmployeeData"] as EmployeeView;
            var geographical = employeeView?.GlobalDimension1Code;
            var dimensionValues = new List<DropdownList>();

            var pageDim = $"CashBooks?$format=json";

            var httpResponsedim = Credentials.GetOdataData(pageDim);
            using (var streamReader = new StreamReader(httpResponsedim.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var details = JObject.Parse(result);


                foreach (var jToken in details["value"])
                {
                    var config = (JObject)jToken;
                    var dropdownList = new DropdownList();
                    dropdownList.Text = (string)config["Name"];
                    dropdownList.Value = (string)config["No"];
                    dimensionValues.Add(dropdownList);
                }
            }

            #endregion
            var response = new
            {
                ListOfActivities = dimensionValues.Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList(),
                success = true
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult DimensionValues()
    {
        try
        {
            // Create and return the JSON result
            var response = new
            {
                ListOfActivities = DimensinValuesList.GetDimensionValues(5).Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList(),
                success = true
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult GeneralDimensionValue(int dimensionNumber)
    {
        try
        {
            var response = new
            {
                ListOfActivities = DimensinValuesList.GetDimensionValues(dimensionNumber).Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList(),
                success = true
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }

    /// <summary>
    /// Vote Book Report
    /// </summary>
    public ActionResult GenerateVoteBookReport(string startDate, string endDate, string budget, string classDimension, string reportType)
    {
        try
        {
            var message = "";
            var success = false;
            var employee = Session["EmployeeData"] as EmployeeView;
            var geoLocation = employee.GlobalDimension1Code;
            var startDateTime = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var endDateTime = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            message = Credentials.ObjNav.GenerateVoteBookReport(startDateTime, endDateTime, budget, classDimension, geoLocation, Convert.ToInt32(reportType));
            if (string.IsNullOrEmpty(message))
            {
                message = "File Not Found";
            }
            else
            {
                success = true;
            }

            return Json(new { message, success, view = false }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public ActionResult GenerateCommitmentReport(string startDate, string endDate, string reportType)
    {
        try
        {
            var message = "";
            var success = false;
            var employee = Session["EmployeeData"] as EmployeeView;
            var startDateTime = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var endDateTime = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            message = Credentials.ObjNav.generateCommitmentReport(Convert.ToInt32(reportType), startDateTime, endDateTime);
            if (string.IsNullOrEmpty(message))
            {
                message = "File Not Found";
            }
            else
            {
                success = true;
            }

            return Json(new { message, success, view = false }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult GeoLocations()
    {
        try
        {
            var response = new
            {
                ListOfActivities = DimensinValuesList.GetDimensionValues(1).Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList(),
                success = true
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message.Replace("'", ""), success = false },
                JsonRequestBehavior.AllowGet);
        }
    }
   /* public ActionResult GenerateOutStandingDeposit(ReportParameters reportParameters)
    {
        try
        {
            var message = "";
            var success = false;
            var depositType = 0;
            var startDateTime = DateTime.ParseExact(reportParameters.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var endDateTime = DateTime.ParseExact(reportParameters.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            message = Credentials.ObjNav.GenerateOutstanding(startDateTime, endDateTime, Convert.ToInt32(reportParameters.ReportType), reportParameters.Geo, Convert.ToInt32(reportParameters.ReportFormat));
            if (string.IsNullOrEmpty(message))
            {
                message = "File Not Found";
            }
            else
            {
                success = true;
            }

            return Json(new { message, success, view = false }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }*/
   /* public ActionResult GenerateCollectionReports(ReportParameters reportParameters)
    {
        try
        {
            var message = "";
            var success = false;
            var depositType = 0;
            var employee = Session["EmployeeData"] as EmployeeView;
            depositType = reportParameters.DepositType switch
            {
                ("Payment") => 1,
                ("Movement") => 3,
                _ => 0
            };
            if (string.IsNullOrEmpty(reportParameters.AdminUnit))
            {
                reportParameters.AdminUnit = "";
            }
            var startDateTime = DateTime.ParseExact(reportParameters.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var endDateTime = DateTime.ParseExact(reportParameters.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            message = Credentials.ObjNav.GenerateDepositCollection(startDateTime, endDateTime, Convert.ToInt32(reportParameters.ReportType)
                , reportParameters.Geo, "", reportParameters.AdminUnit, Convert.ToInt32(reportParameters.ReportFormat), depositType);
            if (string.IsNullOrEmpty(message))
            {
                message = "File Not Found";
            }
            else
            {
                success = true;
            }

            return Json(new { message, success, view = false }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }*/

  /*  public ActionResult GenerateRevenueReport(ReportParameters reportParameters)
    {
        try
        {
            var message = "";
            var success = false;

            if (string.IsNullOrWhiteSpace(reportParameters.StartDate) ||
                string.IsNullOrWhiteSpace(reportParameters.EndDate))
            {
                return Json(new
                {
                    message = "Start Date and End Date are required.",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(reportParameters.AdminUnit))
            {
                reportParameters.AdminUnit = "";
            }

            var startDateTime = DateTime.ParseExact(reportParameters.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var endDateTime = DateTime.ParseExact(reportParameters.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var geo = reportParameters.Geo ?? string.Empty;
            var bankAcc = reportParameters.BankAcc ?? string.Empty;
            var revenueType = string.IsNullOrWhiteSpace(reportParameters.RevenueType) ? 0 : Convert.ToInt32(reportParameters.RevenueType);
            var reportType = string.IsNullOrWhiteSpace(reportParameters.ReportType) ? 0 : Convert.ToInt32(reportParameters.ReportType);
            var reportFormat = string.IsNullOrWhiteSpace(reportParameters.ReportFormat) ? 0 : Convert.ToInt32(reportParameters.ReportFormat);

            message = Credentials.ObjNav.GenerateRevenueReports(
                startDateTime,
                endDateTime,
                reportType,
                geo,
                bankAcc,
                reportParameters.AdminUnit,
                revenueType,
                "", reportParameters.GlAccountList
            );

            if (string.IsNullOrEmpty(message))
            {
                message = "File Not Found";
            }
            else
            {
                success = true;
            }

            return Json(new
            {
                message,
                success,
                view = false
            }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new
            {
                message = ex.Message,
                success = false
            }, JsonRequestBehavior.AllowGet);
        }
    }*/
  /*  public ActionResult GenerateTrialBalance(ReportParameters reportParameters)
    {
        try
        {
            var message = "";
            var success = false;

            if (string.IsNullOrWhiteSpace(reportParameters.StartDate) ||
                string.IsNullOrWhiteSpace(reportParameters.EndDate))
            {
                return Json(new
                {
                    message = "Start Date and End Date are required.",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(reportParameters.AdminUnit))
            {
                reportParameters.AdminUnit = "";
            }

            var startDateTime = DateTime.ParseExact(reportParameters.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var endDateTime = DateTime.ParseExact(reportParameters.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var geo = reportParameters.Geo ?? string.Empty;
            var reportType = string.IsNullOrWhiteSpace(reportParameters.ReportType) ? 0 : Convert.ToInt32(reportParameters.ReportType);
            var reportFormat = string.IsNullOrWhiteSpace(reportParameters.ReportFormat) ? 0 : Convert.ToInt32(reportParameters.ReportFormat);

            message = Credentials.ObjNav.GenerateTrialBalanceReport(
                startDateTime,
                endDateTime,
                geo,
                reportParameters.AdminUnit,
                reportParameters.AccountList,
                reportFormat,
                reportType
            );

            if (string.IsNullOrEmpty(message))
            {
                message = "File Not Found";
            }
            else
            {
                success = true;
            }

            return Json(new
            {
                message,
                success,
                view = false
            }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new
            {
                message = ex.Message,
                success = false
            }, JsonRequestBehavior.AllowGet);
        }
    }*/
    public ActionResult PrintReport()
    {
        return PartialView("PartialViews/PerActivity");
    }

    public PartialViewResult FileAttachmentForm()
    {
        try
        {
            return PartialView("~/Views/Common/PartialViews/FileAttachmentForm.cshtml");
        }
        catch (Exception ex)
        {
            var erroMsg = new Error { Message = ex.Message };
            return PartialView("~/Views/Shared/Partial Views/ErroMessangeView.cshtml", erroMsg);
        }

    }

    public PartialViewResult UpdateBankDetails()
    {
        #region Bank Codes
        var bankCodes = new CommonDropDownList();

        var dim2 = new List<DropdownList>();
        var uniqueCodes = new HashSet<string>();
        var pageDim2 = "BankAccounts?$select=BankCode,BankName&$format=json";

        var httpResponseDivision = Credentials.GetOdataData(pageDim2);
        using (var streamReader = new StreamReader(httpResponseDivision.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            var details = JObject.Parse(result);

            dim2.AddRange(from JObject config in details["value"] let code = (string)config["BankCode"] let bankName = (string)config["BankName"] where uniqueCodes.Add(code) select new DropdownList { Text = code + " - " + bankName, Value = code });
        }
        #endregion

        bankCodes.ListOfBankCodes = dim2.Select(x =>
            new SelectListItem
            {
                Text = x.Text,
                Value = x.Value
            }).ToList();
        return PartialView(bankCodes);
    }
    public JsonResult GetBankBranches(string bankCode)
    {
        try
        {
            #region Branches

            var ddlList = new List<DropdownList>();
            var page = "BankBranches?$filter=BankCode eq '" + bankCode + "'&format=json";

            var httpResponse = Credentials.GetOdataData(page);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var details = JObject.Parse(result);

                ddlList.AddRange(from JObject config in details["value"] let branchCode = (string)config["BranchCode"] where branchCode.Length > 3 select new DropdownList { Value = branchCode, Text = branchCode + "-" + (string)config["BranchName"] });
            }

            #endregion


            var dropDownData = new DropdownListData
            {
                ListOfddlData = ddlList.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Text,
                        Value = x.Value
                    }).ToList()
            };
            return Json(new { DropDownData = dropDownData, success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    public JsonResult UpdateSecondaryBankDetails(BankDetails bankDetails)
    {
        try
        {
            var staffNo = Session["Username"]?.ToString() ?? string.Empty;
            Credentials.ObjNav.updateHrBankDetails(staffNo, bankDetails.BankCode, bankDetails.BankBranch, bankDetails.BankAccount);
            Session["BankName2"] = CommonClass.GetEmployeeBankName(bankDetails.BankCode);
            Session["BankCode2"] = bankDetails.BankCode;
            Session["BankBranch2"] = bankDetails.BankBranch;
            Session["BankAccountNumber2"] = bankDetails.BankAccount;

            return Json(new { message = "Bank Updated Successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult ValidateBankAccount(string DocNo)
    {
        try
        {
            Credentials.ObjNav.UpdateBankName(DocNo);
            return Json(new { message = "Bank Updated Successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult UpdateEmployeeBankDetails(string DocNo)
    {
        try
        {
            Credentials.ObjNav.updateEmpBankDetails(DocNo);
            return Json(new { message = "Details Updated Successfully", success = true }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }
    [AcceptVerbs(HttpVerbs.Post)]
    public JsonResult ValidateBankAccountLines(string DocNo)
    {
        try
        {
            Credentials.ObjNav.updateClaimsBank(DocNo);
            return Json(new { message = "Bank Updated Successfully", success = true },
                JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
        }
    }

    public static List<DropdownList> GetDropdownList(string endpoint, string valueField, string textField, Dictionary<string, string> optionalParameters = null)
    {
        var requestUrl = endpoint;
        if (optionalParameters is { Count: > 0 })
        {
            var paramList = optionalParameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}");
            requestUrl += "?" + string.Join("&", paramList);
        }

        var httpResponse = Credentials.GetOdataData(requestUrl);
        using var streamReader = new StreamReader(httpResponse.GetResponseStream());
        var result = streamReader.ReadToEnd();
        var details = JObject.Parse(result);

        return (from JObject config in details["value"] select new DropdownList { Value = (string)config[valueField], Text = (string)config[valueField] + " - " + (string)config[textField] }).ToList();
    }

}