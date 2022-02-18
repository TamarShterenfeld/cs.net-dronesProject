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
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return Validation.IsValidPhone(value.ToString()) ? ValidationResult.ValidResult : new ValidationResult(false, "Phone isn't valid.");
        }
        #endregion
    }
    class  NameRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return Validation.IsValidName(value.ToString()) ? ValidationResult.ValidResult : new ValidationResult(false, "Name is not valid.");
        }
        #endregion
    }

    class StringIdRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return Validation.IsValidStringId(value.ToString()) ? ValidationResult.ValidResult : new ValidationResult(false, "Id's length must be exactly nine.");
        }
        #endregion
    }
    class RealPositiveNumberRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString() == null || value.ToString() == "") return new ValidationResult(false, "Required.");
            return int.Parse(value.ToString()) > 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a real positive value.");
        }
        #endregion
    }

    class PositiveNumberRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return int.Parse(value.ToString()) >= 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a positive value.");
        }
        #endregion
    }

    class PositiveDoubleRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return  double.Parse(value.ToString()) >= 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a positive value.");
        }
        #endregion
    }

    class RealPositiveDoubleRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return double.Parse(value.ToString()) > 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a positive value.");
        }
        #endregion
    }
    class NumberRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return Validation.IsValidNumber(value.ToString()) ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain only digits.");
        }
        #endregion
    }

    class DoubleRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Required.");
            return Validation.IsValidDouble(value.ToString()) ? ValidationResult.ValidResult : new ValidationResult(false, "Input has to contain a double value.");
        }
        #endregion
    }


    class NotEmptyRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value != null ?  ValidationResult.ValidResult : new ValidationResult(false, "Required.");
        }
        #endregion
    }

    class NotInitalizeRule : ValidationRule
    {
        #region ValidationResult
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return int.Parse(value.ToString()) != 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Not initalized field.");
        }
        #endregion
    }

}



