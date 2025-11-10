using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Learnify.DataAccess.Repositories
{
    public class EfNotificationDal : GenericRepository<Notification>, INotificationDal
    {
        public EfNotificationDal(LearnifyContext context) : base(context) { }

        public async Task<List<Notification>> GetUnreadAsync(int userId)
        {
            return await Query()
                .Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }
    }
}
