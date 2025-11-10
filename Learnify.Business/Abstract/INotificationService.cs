using Learnify.Business.Utilities.Results;
using Learnify.DTO.DTOs.NotificationDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface INotificationService : IGenericService<CreateNotificationDto, UpdateNotificationDto, ResultNotificationDto>
    {
        Task<IDataResult<List<ResultNotificationDto>>> GetAllByUserIdAsync(int userId);
        Task<IDataResult<List<ResultNotificationDto>>> GetUnreadAsync(int userId);
        Task<IResult> MarkAsReadAsync(int id);
    }
}
