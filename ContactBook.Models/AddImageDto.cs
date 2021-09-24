using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ContactBook.Models
{
    public class AddImageDto
    {
        public IFormFile Image { get; set; }
    }
}
