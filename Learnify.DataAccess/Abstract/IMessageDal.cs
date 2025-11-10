using Learnify.Entity.Concrete;

namespace Learnify.DataAccess.Abstract
{
    public interface IMessageDal : IGenericDal<Message>
    {
        Task<List<Message>> GetInboxAsync(int userId);
        Task<List<Message>> GetSentAsync(int userId);
        Task<List<Message>> GetDraftsAsync(int userId);
        Task<List<Message>> GetDeletedAsync(int userId);
        Task<List<Message>> GetSpamAsync(int userId);
    }
}
