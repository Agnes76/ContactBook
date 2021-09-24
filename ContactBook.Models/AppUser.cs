using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ContactBook.Models
{
   public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; } = "default.jpg";
        public DateTime CreatedAt{ get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
