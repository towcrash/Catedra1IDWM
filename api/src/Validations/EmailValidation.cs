using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace api.src.Validations
{
    public class CustomEmailAttribute : ValidationAttribute
{
    private readonly string _regex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public CustomEmailAttribute() : base("El campo {0} debe ser una dirección de correo electrónico válida.")
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        string email = value.ToString();

        if (string.IsNullOrWhiteSpace(email))
        {
            return new ValidationResult("El correo electrónico no puede estar vacío.");
        }

        if (Regex.IsMatch(email, _regex))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
}
}