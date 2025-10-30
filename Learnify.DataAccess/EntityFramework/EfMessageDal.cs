using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;

using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        private readonly LearnifyContext _context;

        public EfMessageDal(LearnifyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetInboxMessagesAsync(int userId)
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.ReceiverId == userId && !m.IsDeleted && !m.IsDraft && !m.IsSpam)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<List<Message>> GetSentMessagesAsync(int userId)
        {
            return await _context.Messages
                .Include(m => m.Receiver)
                .Where(m => m.SenderId == userId && !m.IsDeleted && !m.IsDraft)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<List<Message>> GetDraftMessagesAsync(int userId)
        {
            return await _context.Messages
                .Where(m => m.SenderId == userId && m.IsDraft)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<List<Message>> GetDeletedMessagesAsync(int userId)
        {
            return await _context.Messages
                .Where(m => (m.SenderId == userId || m.ReceiverId == userId) && m.IsDeleted)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<List<Message>> GetSpamMessagesAsync(int userId)
        {
            return await _context.Messages
                .Where(m => m.ReceiverId == userId && m.IsSpam)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
    }
}
