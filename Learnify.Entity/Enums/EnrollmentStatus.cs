using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Entity.Enums
{
    public enum EnrollmentStatus
    {
        Pending,      // kayıt oluşturuldu ama başlamadı
        Enrolled,     // öğrenci kayıtlı
        InProgress,   // derslere başlamış
        Completed,    // bitirmiş
        Cancelled     // iptal edilmiş
    }

}
