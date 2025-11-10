namespace Learnify.DTO.DTOs.NotificationDto
{
    public class CreateNotificationDto
    {
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public int UserId { get; set; }   // ✅ hedef kullanıcı
    }
}
