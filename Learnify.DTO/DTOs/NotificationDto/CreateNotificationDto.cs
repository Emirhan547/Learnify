namespace Learnify.DTO.DTOs.NotificationDto
{
    public class CreateNotificationDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
