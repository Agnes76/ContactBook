using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ContactBook.BL;
using ContactBook.DTO;
using ContactBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
           _userService = userService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllUsers([FromQuery] PagingParameter pagingParameter)
        {
            try
            {
                var result = await _userService.GetAllUsers();
                return Ok(result.Skip(pagingParameter.PageSize * (pagingParameter.PageNumber - 1))
                                                            .Take(pagingParameter.PageSize).ToList());
            }
            catch(KeyNotFoundException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (ArgumentException)
            {
                return StatusCode(500);
            }
        }



        [HttpGet("id/{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                return Ok(await _userService.GetUser(userId));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }



        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                return Ok(await _userService.GetUserByEmail(email));
            }
            catch (ArgumentNullException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPatch("update")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateUserRequest updateUserRequest)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var result = await _userService.Update(userId, updateUserRequest);
                return NoContent();
            }
            catch(MissingMemberException mmex)
            {
                return BadRequest(mmex.Message);
            }
            catch(ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

       
        [HttpDelete("id/delete")]
        [Authorize(Roles ="Admin, Regular")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return NoContent();
            }
            catch(ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpPost("AddNew")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewUser(RegistrationRequest registrationRequest)
        {
            try
            {
                var result = await _userService.AddNewUser(registrationRequest);
                return Ok(result);
            }
            catch(ArgumentNullException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (MissingFieldException argex)
            {

                return BadRequest(argex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("SearchTerm")]
        [Authorize]
        public IActionResult GetUserBySearchTerm(string searchTerm)
        {
            try
            {
                return Ok(_userService.GetUserBySearchTerm(searchTerm));
            }
            catch (ArgumentNullException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
