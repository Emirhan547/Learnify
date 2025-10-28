// Learnify.DataAccess/Abstract/IUnitOfWork.cs
using Microsoft.EntityFrameworkCore.Storage;

namespace Learnify.DataAccess.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
