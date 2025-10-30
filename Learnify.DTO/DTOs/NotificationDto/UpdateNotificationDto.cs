namespace Learnify.DTO.DTOs.NotificationDto
{
    public class UpdateNotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public int UserId { get; set; }
    }
}
