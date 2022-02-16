using System;
using System.Globalization;
using System.Windows.Controls;

namespace PL
{
     internal static class Validation
    {
        
        internal static bool IsValidPhone(string phone)
        {
            if(phone[0] != '0') return false;
            foreach (char ch in phone)
            {
                if (ch == '-') continue;
                if (!Char.IsDigit(ch)) return false;
            }
            return true;
        }
        internal static bool IsValidLocation(double longitude)
        {
            return longitude >= -180 && longitude <= 180;
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

        internal static bool IsValidNumber(string name)
        {
            if (name == null) return true;

            foreach (char ch in name)
            {
                if (!Char.IsDigit(ch)) return false;
            }
            return true;
        }

        internal static bool IsValidStringId(string name)
        {
            return name.Length == 9;
        }
        
        internal static bool  IsValid(object target, params ValidationRule[] validations)
        {
            CultureInfo c1 = new(1);
            foreach (var item in validations)
            {
                if (!item.Validate(target, c1).IsValid)
                    return false;
            }
            return true;
        }
    }
}
