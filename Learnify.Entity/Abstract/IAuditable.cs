using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Entity.Abstract
{
    public interface IAuditable
    {
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        bool IsActive { get; set; }
    }
}
