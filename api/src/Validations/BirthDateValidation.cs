using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.Validations
{
    public class BirthDateAttribute : ValidationAttribute
    {
        public BirthDateAttribute() : base("La fecha de nacimiento debe ser anterior a la fecha actual.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime dateTime)
            {
                if (dateTime < DateTime.Now)
                {
                    return ValidationResult.Success;
                }
            }
            else if (value is DateOnly dateOnly)
            {
                if (dateOnly < DateOnly.FromDateTime(DateTime.Now))
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult($"El tipo {value.GetType().Name} no es soportado para la validación de fecha de nacimiento.");
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}