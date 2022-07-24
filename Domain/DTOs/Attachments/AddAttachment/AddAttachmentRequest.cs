namespace Domain.DTOs.Attachments.GetAttachment
{
    public class AddAttachmentRequest
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public string URL { get; set; }
    }
}