using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.src.Validations;

namespace api.src.DTOs
{
    public class UpdateUserRequestDto
    {
        
        public string Rut {get; set;} = string.Empty;

        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe ser mayor a 3 y menor a 100 caracteres")]
        public string Name {get; set;} = string.Empty;
        [EmailAddress]
        public string Email {get; set;} = string.Empty;
        [BirthDate]    
        public DateOnly BirthDate {get; set;}
        public int GenderId { get; set; }
    }
}