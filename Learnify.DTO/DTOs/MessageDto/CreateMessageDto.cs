namespace Learnify.DTO.DTOs.MessageDto
{
    public class CreateMessageDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsDraft { get; set; } = false;
    }
}
