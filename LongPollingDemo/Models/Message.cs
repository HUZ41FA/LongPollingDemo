using System.ComponentModel.DataAnnotations;

namespace LongPollingDemo.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Content { get; set; } = null!;
        public int ReadCounter { get; set; } = 0;
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
