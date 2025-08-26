using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LaboratorioBack.Validations
{
    public class CURPAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || (value is string str && string.IsNullOrWhiteSpace(str)))
            {
                return new ValidationResult("La CURP es requerida.");
            }

            var valueCURP = value.ToString();
            //var valueCURP = value.ToString()!.ToString();

            string patron = @"^[A-Z]{1}[AEIOU]{1}[A-Z]{2}\d{2}"
                     + @"(0[1-9]|1[0-2])"  // Mes
                     + @"(0[1-9]|[12][0-9]|3[01])" // Día
                     + @"[HM]{1}" // Sexo
                     + @"(AS|BC|BS|CC|CL|CM|CS|CH|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TL|TS|VZ|YN|ZS|NE)"
                     + @"[B-DF-HJ-NP-TV-Z]{3}" // Consonantes internas
                     + @"[0-9A-Z]{1}" // Homoclave
                     + @"\d{1}$"; // Dígito verificador

            var result = Regex.IsMatch(valueCURP, patron);

            if (result)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("El formato de la CURP es incorrecto.");

            //return base.IsValid(value, validationContext);
        }
    }
}