using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace INTEXII.Models
{
    public class Users : IdentityUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}