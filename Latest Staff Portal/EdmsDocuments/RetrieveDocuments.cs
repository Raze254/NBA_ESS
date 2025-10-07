using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Latest_Staff_Portal.Models;
using Newtonsoft.Json;

public static class RetrieveDocuments
{
    static RetrieveDocuments()
    {
        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
    }

    public static async Task<dynamic> RetrieveDocument(string module, string documentNo, string documentType, string documentId)
    {
       
       
        var requestBody = new EdmsRetrieveDocument
        {
            Module = module,
            DocumentNo = documentNo,
            DocumentType = documentType,
            document_id = documentId
        };

        var url = ConfigurationManager.AppSettings["EDMSRetrieveUrl"];
        if (string.IsNullOrEmpty(url))
        {
            return "";
        }

        var jsonPayload = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        try
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var fileResponse = JsonConvert.DeserializeObject<FileResponse>(responseBody);

                    if (fileResponse != null && fileResponse.StatusCode == 0)
                    {
                        return new { Base64 = fileResponse.FileBase64String, MimeType = fileResponse.Mime };
                    }
                    return null;
                }
                return "";
            }
        }
        catch (HttpRequestException e)
        {
            return "";
        }
        catch (Exception ex)
        {
            return "";
        }
    }
}
