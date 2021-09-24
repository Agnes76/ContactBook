using System.Collections.Generic;
using System.Threading.Tasks;
using ContactBook.DTO;

namespace ContactBook.BL
{
    public interface IUserService
    {
        Task<bool> DeleteUser(string userId);
        Task<UserResponseDTO> GetUser(string userId);
        Task<bool> Update(string userId, UpdateUserRequest updateUser);
        Task<IEnumerable<UserResponseDTO>> GetAllUsers();
        Task<UserResponseDTO> GetUserByEmail(string email);
        Task<bool> UpdateImage(string id, string url);
        Task<UserResponseDTO> AddNewUser(RegistrationRequest registrationRequest);
        List<UserResponseDTO> GetUserBySearchTerm(string searchTerm);
    }
}