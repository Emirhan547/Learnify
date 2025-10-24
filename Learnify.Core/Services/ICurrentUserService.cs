using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Core.Services
{
    public interface ICurrentUserService
    {
        int? GetUserId();
        string? GetUserEmail();
        string? GetUserRole();
    }
}
