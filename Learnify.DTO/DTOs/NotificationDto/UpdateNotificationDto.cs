namespace Learnify.DTO.DTOs.NotificationDto
{
    public class UpdateNotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public bool IsRead { get; set; }
        public object UserId { get; set; }
    }
}
