using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface IMessageDal : IGenericDal<Message>
    {
        Task<List<Message>> GetInboxMessagesAsync(int userId);
        Task<List<Message>> GetSentMessagesAsync(int userId);
        Task<List<Message>> GetDraftMessagesAsync(int userId);
        Task<List<Message>> GetDeletedMessagesAsync(int userId);
        Task<List<Message>> GetSpamMessagesAsync(int userId);
    }
}
