using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Data
{
   public class ContactContext : IdentityDbContext<AppUser>
   {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {

        }
   
   }
}
