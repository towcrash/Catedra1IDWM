using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.DTOs;
using api.src.Models;

namespace api.src.Mappers
{
    public static class UserMapper
    {
        public static CreateUserDto ToUserDto(this User user)
        {
            return new CreateUserDto
            {
                Rut = user.Rut,
                Name = user.Name,
                Email = user.Email,
                BirthDate = user.BirthDate,
                GenderId = user.GenderId

            };
        }
        public static User ToUserFromUserDto(this CreateUserDto createUserDto)
        {
            return new User 
            {
                Rut = createUserDto.Rut,
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                BirthDate = createUserDto.BirthDate,
                GenderId = createUserDto.GenderId
            };         
        } 
        
    }
}