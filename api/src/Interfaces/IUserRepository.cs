using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.DTOs;
using api.src.Models;

namespace api.src.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByRut(string rut);
        Task<User> Post(User user);
        Task<List<User>> GetAll(string? gender);
        Task<User?> DeleteUser(string code);
        Task<User?> Put(string code, UpdateUserRequestDto updateUserRequestDto);
    }
}