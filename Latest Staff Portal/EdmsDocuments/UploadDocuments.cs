using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Latest_Staff_Portal.Models;
using Latest_Staff_Portal.ViewModel;
using Newtonsoft.Json;

public static class UploadDocuments
{
    static UploadDocuments()
    {
        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
    }

    public static bool UploadEDMSDocumentAttachment(string fileBase64String, string fileName, string module,
        string documentNo, string documentType, string description, int tableId, string vendor)
    {
        var requestBody = new EdmsRequestBody();
        var employee = HttpContext.Current.Session["EmployeeData"] as EmployeeView;

        if (employee == null)
        {
            return false;
        }

        requestBody.VendorNo = vendor;
        requestBody.FileBase64String = fileBase64String;
        requestBody.FileName = fileName;
        requestBody.Module = module;
        requestBody.DocumentNo = documentNo;
        requestBody.DocumentType = documentType;
        requestBody.Description = description;
        requestBody.StaffNo = employee.No;
        requestBody.Employee_First_Name = employee.FirstName;
        requestBody.Employee_Middle_Name = employee.MiddleName;
        requestBody.Employee_Last_Name = employee.LastName;
        requestBody.TableId = tableId;

        var url = ConfigurationManager.AppSettings["EDMSUploadUrl"];
        if (string.IsNullOrEmpty(url))
        {
            return false;
        }

        var jsonPayload = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        try
        {
            using var httpClient = new HttpClient();
            var response = httpClient.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                var uploadResponse = JsonConvert.DeserializeObject<UploadResponse>(responseBody);

                if (uploadResponse.status_code == 0)
                {
                    var docId = uploadResponse.document_id;
                   /* Credentials.ObjNav.AttachDocumentsLink(documentNo, fileName, docId, tableId);*/
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        catch (HttpRequestException e)
        {
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
