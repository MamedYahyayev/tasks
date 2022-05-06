using System;
using System.Data;

namespace SchoolManagement.Service
{
    public interface IExporter
    {
        void Export(DataSet dataSet);
    }
}
