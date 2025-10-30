namespace Learnify.DTO.DTOs.MessageDto
{
    public class UpdateMessageDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsDraft { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSpam { get; set; }
    }
}
