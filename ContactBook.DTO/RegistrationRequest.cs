﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.DTO
{
    public class RegistrationRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
