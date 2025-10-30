using Learnify.DTO.DTOs.MessageDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IMessageService : IGenericService<CreateMessageDto, UpdateMessageDto, ResultMessageDto>
    {
        Task<List<ResultMessageDto>> GetInboxAsync(int userId);
        Task<List<ResultMessageDto>> GetSentAsync(int userId);
        Task<List<ResultMessageDto>> GetDraftsAsync(int userId);
        Task<List<ResultMessageDto>> GetDeletedAsync(int userId);
        Task<List<ResultMessageDto>> GetSpamAsync(int userId);
    }
}
