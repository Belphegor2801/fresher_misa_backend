using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    [AttributeUsage(AttributeTargets.All)]
    public class OnlyForGetAttribute : Attribute
    {
        public OnlyForGetAttribute()
        {
        }
    }
}
