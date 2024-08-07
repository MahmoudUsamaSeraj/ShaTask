#nullable enable 
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShaTask.Models
{
    public class Account:IdentityUser
    {
        [MaxLength(30)]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        public string Name { get; set; }
		public string Address { get; set; }
	}
}
