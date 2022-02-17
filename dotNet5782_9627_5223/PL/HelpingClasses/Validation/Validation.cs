using System;
using System.Globalization;
using System.Windows.Controls;

namespace PL
{
    internal static class Validation
    {
        #region IsValid_Methods
        internal static bool IsValidPhone(string phone)
        {
            if (phone[0] != '0') return false;
            if(phone.Length < 9) return false;
            foreach (char ch in phone)
            {
                if (ch == '-') continue;
                if (!Char.IsDigit(ch)) return false;
            }
            return true;
        }

        internal static bool IsValidDouble(string location)
        {
            foreach (char ch in location)
            {
                if (double.TryParse(location, out double result) == false) return false;
            }
            return true;
        }
        internal static bool IsValidLocation(string num)
        {
            if (num == null) return true;
            if (num == "") return true;
            return double.Parse(num) <= 90 && double.Parse(num) >= -90;
        }
        internal static bool IsValidName(string name)
        {
            if (name == null) return true;

            foreach (char ch in name)
            {
                if (ch == ' ') continue;
                if (!Char.IsLetter(ch)) return false;
            }
            return true;
        }

        internal static bool IsValidNumber(string number)
        {
            if (number == null) return true;

            foreach (char ch in number)
            {
                if (number[0] == '-') continue;
                if (!Char.IsDigit(ch)) return false;
            }
            return true;
        }

        internal static bool IsValidStringId(string name)
        {
            return name.Length == 9;
        }

        internal static bool IsValid(object target, params ValidationRule[] validations)
        {
            CultureInfo c1 = new(1);
            foreach (var item in validations)
            {
                if (!item.Validate(target, c1).IsValid)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
