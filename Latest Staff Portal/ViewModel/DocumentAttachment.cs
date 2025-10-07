using System;
using System.Collections.Generic;

namespace Latest_Staff_Portal.ViewModel
{
    public class DocumentAttachment
    {
        public int TableId { get; set; }
        public string No { get; set; }
        public string DocumentType { get; set; }
        public int LineNo { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string DocumentCategory { get; set; }
        public string DocumentDescription { get; set; }
        public string FileExtension { get; set; }
        public string FileType { get; set; }
        public string User { get; set; }
        public DateTime AttachedDate { get; set; }
        public bool DocumentFlowPurchase { get; set; }
        public bool DocumentFlowSales { get; set; }
        public string DocumentId { get; set; }
        public string Module { get; set; }
    }

    public class DocumentAttachmentList
    {
        public string Status { get; set; }
        public List<DocumentAttachment> DocList { get; set; }
    }
}