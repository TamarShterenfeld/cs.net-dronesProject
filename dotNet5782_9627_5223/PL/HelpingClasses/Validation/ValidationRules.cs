using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL
{
    class PhoneRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return Validation.IsValidPhone(value.ToString()) ? ValidationResult.ValidResult : new ValidationResult(false, "Phone isn't valid.");
        }
    }
    class  NameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return Validation.IsValidName(value.ToString()) ? ValidationResult.ValidResult : new ValidationResult(false, "Name is not valid.");
        }
    }

    class StringIdRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return Validation.IsValidStringId(value.ToString()) ? ValidationResult.ValidResult : new ValidationResult(false, "Id's length must be exactly nine.");
        }
    }
    class RealPositiveNumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return long.Parse(value.ToString()) > 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a real positive value.");
        }
    }

    class PositiveNumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return long.Parse(value.ToString()) >= 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a positive value.");
        }
    }

    class PositiveDoubleRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return  double.Parse(value.ToString()) >= 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a positive value.");
        }
    }
    class NumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return Validation.IsValidNumber(value.ToString()) ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain only digits.");
        }
    }

    class DoubleRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return double.TryParse(value.ToString(), out double num) ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a double value.");
        }
    }


    class NotEmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value != null ?  ValidationResult.ValidResult : new ValidationResult(false, "Required.");
        }
    }

    class NotInitalizeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return long.Parse(value.ToString()) != 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Not initalized field.");
        }
    }

}



