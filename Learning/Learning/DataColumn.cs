using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    public class DataColumn : DynamicObject
    {
        public int this[Enum value]
        {
            get { return Convert.ToInt32(value); }
        }
        
    }

}
