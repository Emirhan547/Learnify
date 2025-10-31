using Learnify.DTO.DTOs.NotificationDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface INotificationService : IGenericService<CreateNotificationDto, UpdateNotificationDto, ResultNotificationDto>
    {
        Task<List<ResultNotificationDto>> GetAllByUserIdAsync(int userId);

        Task<List<ResultNotificationDto>> GetUnreadAsync(int userId);
        Task MarkAsReadAsync(int id);
    }
}
