using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GestionareHotel.Models
{
    class NumberValidationRuleDouble: ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double result = 0.0;
            bool canConvert = double.TryParse(value as string, out result);
            return new ValidationResult(canConvert, "Not a valid double");
        }
    }
}
