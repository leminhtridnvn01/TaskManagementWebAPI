using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
