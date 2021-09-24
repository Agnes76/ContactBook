using System.Threading.Tasks;
using ContactBook.DTO;

namespace ContactBook.BL
{
    public interface IAuthentication
    {
        Task<UserResponseDTO> Login(UserRequest userRequest);
        Task<UserResponseDTO> Register(RegistrationRequest registrationRequest);
    }
}