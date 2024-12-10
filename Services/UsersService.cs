using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSkyApiPostgres.Models;
using DotnetSkyApiPostgres.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace DotnetSkyApiPostgres.Services
{
    public interface IUsersService
    {
        Task<GetUserRequest> AddUserAsync(CreateUserRequest request);
        Task UpdateUserAsync(UpdateUserRequest request);
        Task DeleteUserAsync(Users data);
        Task<GetUserRequest?> GetUserByIdAsync(string id);
        Task<IEnumerable<GetUserRequest>> GetUsersAsync();
    }

    public sealed class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;

        public UsersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserRequest> AddUserAsync(CreateUserRequest request)
        {
            Users user = CreateUserRequest.ToUsersModel(request);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Users.ToGetUserRequest(user);
        }

        public async Task UpdateUserAsync(UpdateUserRequest request)
        {
            Users user = UpdateUserRequest.ToUsersModel(request);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Users data)
        {
            _context.Users.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<GetUserRequest?> GetUserByIdAsync(string id)
        {
            Users? user = await _context.Users.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            return Users.ToGetUserRequest(user);
        }

        public async Task<IEnumerable<GetUserRequest>> GetUsersAsync()
        {
            IEnumerable<Users> people = await _context.Users.AsNoTracking().ToListAsync();
            return people.Select(Users.ToGetUserRequest);
        }

        
    }
}