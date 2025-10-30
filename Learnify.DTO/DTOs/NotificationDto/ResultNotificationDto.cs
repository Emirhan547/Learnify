

namespace Learnify.DTO.DTOs.NotificationDto
{
    public class ResultNotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
    }
}
