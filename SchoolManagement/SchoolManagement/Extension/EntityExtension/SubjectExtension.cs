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
        private const string UNDERSCORE_SIGN = "_";

        public static string GetName(this Subject? subject)
        {
            if (subject == null) return subject.ToString();
            
            var subjectName = subject.ToString();

            if (subjectName.Contains(UNDERSCORE_SIGN))
            {
                var subjectSplit = subjectName.Split(UNDERSCORE_SIGN);
                subjectName = "";
                foreach (var part  in subjectSplit)
                   subjectName += part[..1] + part[1..].ToLower() + " ";

                return subjectName;
            }

            return subjectName[..1] + subjectName[1..].ToLower();
        }
    }
}
