using System.Threading.Tasks;
using ContactBook.Models;

namespace ContactBook.BL
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUser user);
    }
}