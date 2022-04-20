using SchoolManagement.Model;
using SchoolManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolManagement.Utility
{
    public class PersonValidator
    {
        public static ErrorModel? CheckEmpty(string propertyName, string property)
        {
            if (string.IsNullOrEmpty(property) || string.IsNullOrWhiteSpace(property))
            {
                return new ErrorModel()
                {
                    HasError = true,
                    ErrorMessage = propertyName + " cannot be empty or null"
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
                    ErrorMessage = "You cannot born in the future"
                };

            }

            return null;
        }

        public static Dictionary<string, ErrorModel> ValidatePerson(Person person, Dictionary<string, ErrorModel> errors)
        {
            var error = CheckEmpty(nameof(person.Name), person.Name);
            if (error != null) errors.Add(nameof(person.Name), error);

            error = CheckEmpty(nameof(person.Surname), person.Surname);
            if (error != null) errors.Add(nameof(person.Surname), error);

            error = CheckSmallerThanNow((DateTime)person.BirthDate);
            if (error != null) errors.Add(nameof(person.BirthDate), error);

            return errors;
        }

        public static ErrorModel IsNumberAndPositive(string propertyName, string property)
        {

            if (string.IsNullOrEmpty(property))
            {
                return new ErrorModel()
                {
                    HasError = true,
                    ErrorMessage = propertyName + " cannot be empty or null"
                };
            }

            var regex = new Regex(@"^[0-9]*$");
            var isNumber = regex.IsMatch(property);
            if (!isNumber)
            {
                return new ErrorModel()
                {
                    HasError = true,
                    ErrorMessage = propertyName + " cannot contains letters or symbols!"
                };
            }

            return null;
        }

    }
}
