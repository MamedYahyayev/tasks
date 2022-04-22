using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    public interface ICrudOperation<T>
    {
        List<T> GetAll(bool includeAllFields);

        T GetById(int id, bool includeAllFields);

        void Insert(T entity);

        void Update(T entity);

        void Delete(int id);

        IList<T> Search(string keyword, bool includeAllFields);
    }
}
