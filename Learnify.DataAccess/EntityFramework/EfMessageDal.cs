using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.DataAccess.Repositories;
using Learnify.Entity.Concrete;
using Learnify.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.DataAccess.EntityFramework
{
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        public EfMessageDal(LearnifyContext context) : base(context) { }

        public Task<List<Message>> GetInboxAsync(int userId) =>
            Query()
            .Where(m => m.ReceiverId == userId
                        && m.Status == MessageStatus.Inbox)
            .Include(m => m.Sender)
            .OrderByDescending(m => m.Date)
            .ToListAsync();

        public Task<List<Message>> GetSentAsync(int userId) =>
            Query()
            .Where(m => m.SenderId == userId
                        && m.Status == MessageStatus.Sent)
            .Include(m => m.Receiver)
            .OrderByDescending(m => m.Date)
            .ToListAsync();

        public Task<List<Message>> GetDraftsAsync(int userId) =>
            Query()
            .Where(m => m.SenderId == userId
                        && m.Status == MessageStatus.Draft)
            .OrderByDescending(m => m.Date)
            .ToListAsync();

        public Task<List<Message>> GetDeletedAsync(int userId) =>
            Query()
            .Where(m => (m.SenderId == userId || m.ReceiverId == userId)
                        && m.Status == MessageStatus.Deleted)
            .OrderByDescending(m => m.Date)
            .ToListAsync();

        public Task<List<Message>> GetSpamAsync(int userId) =>
            Query()
            .Where(m => m.ReceiverId == userId
                        && m.Status == MessageStatus.Spam)
            .OrderByDescending(m => m.Date)
            .ToListAsync();
    }
}
