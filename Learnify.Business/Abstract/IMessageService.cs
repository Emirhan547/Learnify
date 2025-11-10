using Learnify.Business.Utilities.Results;
using Learnify.DTO.DTOs.MessageDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IMessageService : IGenericService<CreateMessageDto, UpdateMessageDto, ResultMessageDto>
    {
        Task<IDataResult<List<ResultMessageDto>>> GetInboxAsync(int userId);
        Task<IDataResult<List<ResultMessageDto>>> GetSentAsync(int userId);
        Task<IDataResult<List<ResultMessageDto>>> GetDraftsAsync(int userId);
        Task<IDataResult<List<ResultMessageDto>>> GetDeletedAsync(int userId);
        Task<IDataResult<List<ResultMessageDto>>> GetSpamAsync(int userId);
    }
}
