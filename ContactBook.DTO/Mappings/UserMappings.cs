using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Models;

namespace ContactBook.DTO.Mappings
{
    public class UserMappings
    {
        public static UserResponseDTO GetUserResponse(AppUser user)
        {
            return new UserResponseDTO
            {
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id
            };
        }

        public static List<UserResponseDTO> GetUsersResponse(List<AppUser> users)
        {
            var userResponseDTOs = new List<UserResponseDTO>();
            foreach (var user in users)
            {
                var gerUser = new UserResponseDTO 
                {
                    Email = user.Email,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    PhoneNumber = user.PhoneNumber,
                    Id = user.Id
                };

                userResponseDTOs.Add(gerUser);
            }
            return userResponseDTOs;
        }

        public static AppUser GetUser(RegistrationRequest request)
        {
            return new AppUser
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                LastName = request.LastName,
                FirstName = request.FirstName,
                UserName = string.IsNullOrWhiteSpace(request.UserName) ? request.Email : request.UserName
            };
        }
    }
}
