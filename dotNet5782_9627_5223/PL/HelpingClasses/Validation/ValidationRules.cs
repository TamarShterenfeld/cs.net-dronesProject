using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL
{
    class NameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required");
            return Validation.IsValidName((string)value) ? ValidationResult.ValidResult : new ValidationResult(false, "Name is not valid");
        }
    }

    class NumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required");
            return int.TryParse((string)value, out int num) ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a numerical value");
        }
    }

    class NotEmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value != null ?  ValidationResult.ValidResult : new ValidationResult(false, "Required");
        }
    }
}