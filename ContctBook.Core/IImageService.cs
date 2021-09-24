using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using ContactBook.Models;
using Microsoft.AspNetCore.Http;

namespace ContctBook.Core
{
    public interface IImageService
    {
        Task<UploadResult> UploadAsync(IFormFile image);
    }
}