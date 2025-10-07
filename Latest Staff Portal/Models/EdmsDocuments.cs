namespace Latest_Staff_Portal.Models
{
    public class EdmsRequestBody
    {
        public string FileBase64String { get; set; }
        public string FileName { get; set; }
        public string Module { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public string StaffNo { get; set; }
        public string Employee_First_Name { get; set; }
        public string Employee_Middle_Name { get; set; }
        public string Employee_Last_Name { get; set; }
        public int TableId { get; set; }
        public string VendorNo { get; set; }
    }

    public class UploadResponse
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public string document_id { get; set; }
    }


    public class EdmsRetrieveDocument
    {
        public string Module { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentType { get; set; }
        public string document_id { get; set; }
    }

    public class FileResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string FileBase64String { get; set; }
        public string Mime { get; set; }
    }
}