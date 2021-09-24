using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ContactBook.BL;
using ContactBook.Models;
using ContctBook.Core;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IUserService _userService;

        public ImageController(IImageService imageService, IUserService userService)
        {
           _imageService = imageService;
            _userService = userService;
        }

        [HttpPost()]
        public async Task<IActionResult> UploadImage([FromForm] AddImageDto imageDto)
        {
            try
            {
                var id = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var upload = await _imageService.UploadAsync(imageDto.Image);
                var result = new ImageAddedDto()
                {
                    PublicId = upload.PublicId,
                    Url = upload.Url.ToString()
                };
                await _userService.UpdateImage(id, result.Url);
                return Ok(result);
            }
            catch(MissingMemberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }  
            
    
        }
    }
}
 