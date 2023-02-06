using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Core.Const;

namespace Application.Helpers
{
    public class UserValidator
    {

        public bool ValidateName(string input)
        {
            Regex re = new Regex(ContentRegularExpression.NAME);
            if (input.IsNullOrWhiteSpace()) return false;
            
            if (input.Length < 2||!re.IsMatch(input)
            )
            {
                return false;
            }
            return true;

        }
        
        public bool ValidateUserName(string input)
        {
            if (input.IsNullOrWhiteSpace()) return false;

            Regex re = new Regex(ContentRegularExpression.USER_NAME);

            if (input.Length < 2||!re.IsMatch(input))
                return false;
            else
                return true;

        }

        public bool ValidateGender(string input,out int result)
        {
            result = 0;

            if (input.IsNullOrWhiteSpace()) return false;
            if (int.TryParse(input,out result))
            {
                return true;
            }

            return false;
        }

        public bool ValidatePassword(string input)
        {
            if (input.IsNullOrWhiteSpace()) return false;

            Regex re = new Regex(ContentRegularExpression.PASSWORD);

            if (input.Length < 2||!re.IsMatch(input))
                return false;
            else
                return true;
        }
        
        public bool ValidateEmail(string input)
        {
            if (input.IsNullOrWhiteSpace()) return false;

            Regex re = new Regex(ContentRegularExpression.EMAIL);
            
            if (re.IsMatch(input))
                return (true);
            else
                return (false);
            
        }

        public bool ValidateDate(string input,out DateTime result)
        {
            result = default;
            
            if (input.IsNullOrWhiteSpace()) return false;

            if (DateTime.TryParseExact(input, "dd/MM/yyyy", 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, out result))
            {
                return true;
            }
            
            return false;
           
        }
        
        
        public bool ValidateEmployeeCode(string input)
        {
            if (input.IsNullOrWhiteSpace()) return false;

            if (input.Length < 2 )
            {
                return false;
            }

            return true;
        }
        
        public bool ValidatePhone(string input)
        {
            if (input.IsNullOrWhiteSpace()) return false;

            Regex re = new Regex(ContentRegularExpression.NUMBER_PHONE);
            
            if (re.IsMatch(input))
                return (true);
            else
                return (false);

        }

        
    }
}