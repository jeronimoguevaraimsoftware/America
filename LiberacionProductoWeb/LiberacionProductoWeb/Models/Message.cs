using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace LiberacionProductoWeb.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IEnumerable<FileDto> Attachments { get; set; }
        public Message(IEnumerable<string> to, string subject, string content, IEnumerable<FileDto> attachments)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
    public class FileDto
    {
        public IEnumerable<byte> Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public FileDto(IEnumerable<byte> content, string fileName, string contentType)
        {
            this.Content = content;
            this.FileName = fileName;
            this.ContentType = contentType;
        }
    }
}
