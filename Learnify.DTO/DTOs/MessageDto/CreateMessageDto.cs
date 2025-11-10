namespace Learnify.DTO.DTOs.MessageDto
{
    public class CreateMessageDto
    {
        public int ReceiverId { get; set; }
        public string Subject { get; set; } = "";
        public string Content { get; set; } = "";
        public bool IsDraft { get; set; } = false;
        public object SenderId { get; set; }
    }
}
