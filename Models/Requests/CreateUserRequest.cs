using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSkyApiPostgres.Models.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public required string UserId { get; set; }
        public string? Name { get; set; }
        public string? Nip { get; set; }
        public string? Branch { get; set; }
        public string? Nohp { get; set; }
        public string? Role { get; set; }

        public static Users ToUsersModel(CreateUserRequest request)
        {
            return new Users
            {
                Id = request.UserId,
                Name = request.Name,
                Nip = request.Nip,
                Branch = request.Branch,
                Nohp = request.Nohp,
                Role = request.Role
            };
        }
    }
}