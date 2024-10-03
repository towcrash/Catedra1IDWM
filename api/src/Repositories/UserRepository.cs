using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.src.Data;
using api.src.DTOs;
using api.src.Interfaces;
using api.src.Models;

namespace api.src.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> ExistsByCode(string rut)
        {
            return await _dataContext.Users.AnyAsync(p => p.Rut == rut);
        }
        public async Task<User> Post(User user)
        {
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
        public async Task<List<User>> GetAll(string? gender)
        {
            var query = _dataContext.Users.Include(p => p.Rut).AsQueryable();
            if (!string.IsNullOrEmpty(gender))
            {
                query = query.Where(p => p.Gender.GenderName.ToLower() == gender.ToLower());                
            } 
            return await query.ToListAsync();
        }

        public async Task<User?> DeleteProduct(string rut)
        {
            var userToDelete = await _dataContext.Users.FirstOrDefaultAsync(p => p.Rut == rut);
            if (userToDelete != null)
            {
                _dataContext.Users.Remove(userToDelete);
                await _dataContext.SaveChangesAsync();
            }
            return userToDelete;
        }

        public async Task<User?> Put(string rut, UpdateUserRequestDto updateUserRequestDto)
        {
            var userModel = await _dataContext.Users.FirstOrDefaultAsync(p => p.Rut == rut);
            if (userModel == null)
            {
                return null;
            }
            userModel.Rut = updateUserRequestDto.Rut;
            userModel.Name = updateUserRequestDto.Name;
            userModel.Email = updateUserRequestDto.Email;
            userModel.BirthDate = updateUserRequestDto.BirthDate;
            userModel.GenderId = updateUserRequestDto.GenderId;

            await _dataContext.SaveChangesAsync();
            return userModel;
        }
    }
}