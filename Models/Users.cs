using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotnetSkyApiPostgres.Models.Requests;

namespace DotnetSkyApiPostgres.Models
{
    [Table("users")]
    public class Users
    {
        [Column("userid", TypeName = "varchar(32)")]
        public string? Id { get; set; }

        [Column("name", TypeName = "varchar(50)")]
        public string? Name { get; set; }

        [Column("nip", TypeName = "varchar(8)")]
        public string? Nip { get; set; }

        [Column("branch", TypeName = "varchar(10)")]
        public string? Branch { get; set; }

        [Column("nohp", TypeName = "varchar(12)")]
        public string? Nohp { get; set; }

        [Column("role", TypeName = "varchar(32)")]
        public string? Role { get; set; }
        
        public static GetUserRequest ToGetUserRequest(Users data)
        {
            return new GetUserRequest
            {
                UserId = data.Id!,
                Name = data.Name,
                Nip = data.Nip,
                Branch = data.Branch,
                Nohp = data.Nohp,
                Role = data.Role
            };
        }
    }
}