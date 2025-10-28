// Learnify.DataAccess/Repositories/UnitOfWork.cs
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Learnify.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LearnifyContext _context;

        public UnitOfWork(LearnifyContext context)
        {
            _context = context;
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => _context.Database.BeginTransactionAsync(cancellationToken);

        public ValueTask DisposeAsync()
            => _context.DisposeAsync();
    }
}
