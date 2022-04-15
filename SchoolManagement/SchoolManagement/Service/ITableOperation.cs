using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    public interface ITableOperation
    {
        void Delete(int id);

        void Search(string keyword);
    }
}
