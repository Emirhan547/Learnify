
namespace Learnify.DTO.DTOs.MessageDto
{
    public class ResultMessageDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public bool IsDraft { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSpam { get; set; }
    }
}
