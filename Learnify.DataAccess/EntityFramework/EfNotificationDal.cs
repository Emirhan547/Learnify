using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.DataAccess.Repositories;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.DataAccess.EntityFramework
{
    public class EfNotificationDal : GenericRepository<Notification>, INotificationDal
    {
        private readonly LearnifyContext _context;

        public EfNotificationDal(LearnifyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetUnreadAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }
    }
}
