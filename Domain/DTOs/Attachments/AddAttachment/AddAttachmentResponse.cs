namespace Domain.DTOs.Attachments.AddAttachment
{
    public class AddAttachmentResponse
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public string URL { get; set; }
        public int TaskItemId { get; set; }
    }
}