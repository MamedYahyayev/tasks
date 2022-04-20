using SchoolManagement.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    // TODO: Replace EntityType parameters with base Parameter
    public interface IFileService
    {
        string GetData(EntityType entity);

        void AppendData(EntityType entity, string data);

    }
}
