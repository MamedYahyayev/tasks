using SchoolManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Utility
{
    public class PersonValidator
    {
        public static ErrorModel? CheckEmpty(string propertyName, string property)
        {
            if(string.IsNullOrEmpty(property))
            {
                return new ErrorModel()
                {
                    HasError = true,
                    ErrorMessage = propertyName + " cannot be empty or null!"
                };

            }

            return null;
        }

        public static ErrorModel? CheckSmallerThanNow(DateTime birthDate)
        {
            if (birthDate > DateTime.Now)
            {
                return new ErrorModel()
                {
                    HasError = true,
                    ErrorMessage = "You cannot born in the future!"
                };

            }

            return null;
        }

    }
}
