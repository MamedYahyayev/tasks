using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Utility
{
    public class Generator
    {
        public static int GenerateId() => new Random().Next(1, 100_000_000);
    }
}
