using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ContctBook.Core;
using ContactBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ContctBook.Core
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;
        private readonly ImageUploadSettings _accountSettings;

        
        public ImageService(IConfiguration config, IOptions<ImageUploadSettings> accountSettings)
        {
            this._accountSettings = accountSettings.Value;
            _config = config;
            _cloudinary = new Cloudinary(new Account(_accountSettings.CloudName, _accountSettings.ApiKey, _accountSettings.ApiSecret));
        }
         
        public async Task<UploadResult> UploadAsync(IFormFile image)
        {
            var pictureMaxLength = Convert.ToInt32(_config.GetSection("PhotoSettings:Size").Get<string>());
            if (image.Length > pictureMaxLength)
            {
                throw new ArgumentOutOfRangeException("Maximum Image size required is 3mb");
            }

            var pictureFormat = false;

            var listOfImageExtensions = _config.GetSection("PhotoSettings:Formats").Get<List<string>>();

            foreach (var item in listOfImageExtensions)
            {
                if (image.FileName.EndsWith(item))
                {
                    pictureFormat = true;
                    break;
                }
            }
            if (pictureFormat == false)
            {
                throw new ArgumentException("File format not supported");
            }

            var uploadResult = new ImageUploadResult();
            using (var imageStream = image.OpenReadStream())
            {
                string filename = Guid.NewGuid().ToString() + image.FileName;

                uploadResult = await _cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(filename, imageStream),
                    Transformation = new Transformation().Crop("thumb").Gravity("face").Width(150).Height(150)
                });
            }
            return uploadResult;
        }

        
    }
}