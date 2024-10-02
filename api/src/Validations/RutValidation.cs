using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace api.src.Validations
    {
    public class RutAttribute : ValidationAttribute
    {
        public RutAttribute() : base("El RUT ingresado no es válido.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string rut = value.ToString().Replace(".", "").Replace("-", "").Trim().ToUpper();

            if (!Regex.IsMatch(rut, @"^\d{7,8}[0-9K]$"))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            char dv = rut[^1];
            string rutSinDv = rut[..^1];

            if (CalcularDv(rutSinDv) != dv)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        private static char CalcularDv(string rutSinDv)
        {
            int suma = 0;
            int multiplicador = 2;

            for (int i = rutSinDv.Length - 1; i >= 0; i--)
            {
                suma += int.Parse(rutSinDv[i].ToString()) * multiplicador;
                multiplicador = multiplicador == 7 ? 2 : multiplicador + 1;
            }

            int resto = suma % 11;
            int resultado = 11 - resto;

            if (resultado == 11)
                return '0';
            if (resultado == 10)
                return 'K';
            return resultado.ToString()[0];
        }
    }
}