using System.Globalization;
using System.Windows.Controls;

namespace ZIndex.DNN.NBrightImport.ValidationRules
{
    public class RequiredField : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                var input = value as string;

                if (input.Length > 0)
                    return new ValidationResult(true, null);
            }

            return new ValidationResult(false, "Champ obligatoire");

        }
    }
}
