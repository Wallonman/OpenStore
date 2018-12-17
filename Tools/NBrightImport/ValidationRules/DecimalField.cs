using System.Globalization;
using System.Windows.Controls;

namespace ZIndex.DNN.NBrightImport.ValidationRules
{
    public class DecimalField : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                var input = value as string;

                decimal decimalValue;
                if( decimal.TryParse(input, out decimalValue) )
                    return new ValidationResult(true, null);
            }

            return new ValidationResult(false, "Valeur numérique invalide");

        }
    }
}