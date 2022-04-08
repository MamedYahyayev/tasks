using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    public interface ICrudOperation<T>
    {
        IList<T> GetAll();

        T GetById(int id);

        bool Insert(T entity);

        bool Update(T entity);

        bool Delete(int id);

        IList<T> Search(string keyword);
    }
}
