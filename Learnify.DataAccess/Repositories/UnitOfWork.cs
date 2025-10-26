using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly LearnifyContext _context;

        public UnitOfWork(LearnifyContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
