using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.src.Models;
using api.src.Validations;

namespace api.src.DTOs
{
    public class CreateUserDto
    {

        public required string Rut {get; set;}

        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe ser mayor a 3 y menor a 100 caracteres")]
        public required string Name {get; set;}
        [EmailAddress]
        public required string Email {get; set;}
        [BirthDate]    
        public required DateOnly BirthDate {get; set;}
        public int GenderId { get; set; }

    }
}