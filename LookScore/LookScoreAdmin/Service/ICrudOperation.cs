using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreAdmin.Service
{
    public interface ICrudOperation<T>
    {
        T[] FindAll();

        T FindOne();

        void Insert(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
