using SchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Model.Enum
{
    public static class SubjectExtension
    {
        public static string GetName(this Subject subject)
        {
            var subjectName = subject.ToString();

            if(subjectName.Contains("_"))
            {
                var subjectSplit = subjectName.Split('_');
                subjectName = "";
                foreach (var part  in subjectSplit)
                   subjectName += part[..1] + part[1..].ToLower() + " ";

                return subjectName;
            }

            return subjectName[..1] + subjectName[1..].ToLower();
        }
    }
}
