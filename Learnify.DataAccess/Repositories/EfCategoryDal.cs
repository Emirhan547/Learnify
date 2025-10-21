using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class EfCategoryDal : EfGenericRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(LearnifyContext context, DbSet<Category> set) : base(context, set)
        {
        }
    }
}
