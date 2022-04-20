﻿using SchoolManagement.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    // TODO: Replace EntityType parameters with base Parameter
    public interface IFileService<T>
    {
        List<T> GetData(Type entity);

        void AppendData(Type entity, List<T> data);

    }
}