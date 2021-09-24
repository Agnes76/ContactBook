using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.DTO;
using ContactBook.DTO.Mappings;
using ContactBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.BL
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Update(string userId, UpdateUserRequest updateUser)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.FirstName = string.IsNullOrWhiteSpace(updateUser.FirstName) ? user.FirstName : updateUser.FirstName;
                user.LastName = string.IsNullOrWhiteSpace(updateUser.LastName) ? user.LastName : updateUser.LastName;
                user.PhoneNumber = string.IsNullOrWhiteSpace(updateUser.PhoneNumber) ? user.PhoneNumber : updateUser.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }
                throw new MissingMemberException(errors);
            }
            throw new ArgumentException("Resource not found");
        }

        public async Task<bool> DeleteUser(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }
                throw new MissingMemberException(errors);
            }
            throw new ArgumentException("Resource not found");
        }

        public async Task<UserResponseDTO> GetUser(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return UserMappings.GetUserResponse(user);
            }

            throw new ArgumentException("Resource not found");
        }


        public async Task<UserResponseDTO> GetUserByEmail(string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser != null)
                return UserMappings.GetUserResponse(appUser);
            throw new KeyNotFoundException("User not found!");
        }



        public async Task<IEnumerable<UserResponseDTO>> GetAllUsers()
        {
            var usersDTO = new List<UserResponseDTO>();
            var users = await _userManager.Users.ToListAsync();
            if (users != null)
            {
                foreach (var user in users)
                {
                    var mapuser = UserMappings.GetUserResponse(user);
                    usersDTO.Add(mapuser);
                }
                return usersDTO;
            }
            throw new ArgumentNullException("User not found!");
        }


        public async Task<bool> UpdateImage(string id, string url)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
            {
                appUser.PhotoUrl = string.IsNullOrWhiteSpace(url) ? "default.jpg" : url;
                var result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                    return true;

                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(errors))
                    throw new MissingMemberException(errors);
            }
            throw new ArgumentNullException("User not found!\nMake sure you login, and try again");
        }


        public List<UserResponseDTO> GetUserBySearchTerm(string searchTerm)
        {
            var user =  _userManager.Users.Where(x =>
                x.FirstName.Contains(searchTerm) ||
                x.LastName.Contains(searchTerm) ||
                x.Email.Contains(searchTerm) ||
                x.PhoneNumber.Contains(searchTerm) ||
                x.UserName.Contains(searchTerm)).ToList();

            if (user != null)
            {
                return UserMappings.GetUsersResponse(user);
            }
            throw new ArgumentNullException("No user found!");
        }


        public async Task<UserResponseDTO> AddNewUser(RegistrationRequest registrationRequest)
        {
            AppUser user = UserMappings.GetUser(registrationRequest);

            if (user == null)
            {
                throw new ArgumentNullException("user not found");
            }

            IdentityResult result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Regular");
                return UserMappings.GetUserResponse(user);
            }
            string errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }
            throw new MissingFieldException(errors);
        }
    }
}

 