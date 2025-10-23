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
    public class EfLessonDal : GenericRepository<Lesson>, ILessonDal
    {
        public EfLessonDal(LearnifyContext context) : base(context)
        {
        }
    }
}
