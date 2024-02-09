using Auction.Core.Interfaces.Images;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Auction.Core.Services.Images;

public class ImagesService : IImagesService
{
    private readonly Cloudinary _cloudinary;

    public ImagesService(IConfiguration configuration)
    {
        var account = new Account(
                configuration["CloudinarySettings:CloudName"]!,
                configuration["CloudinarySettings:APIKey"]!,
                configuration["CloudinarySettings:APISecret"]!
            );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> AddImageAsync(IFormFile image)
    {
        var uploadResult = new ImageUploadResult();

        if (image.Length > 0)
        {
            using var stream = image.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, stream),
                Transformation = new Transformation()
                    .Height(720)
                    .Width(540)
                    .Crop("fill")
                    .Gravity("face")
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public async Task<DeletionResult> DeleteImageAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        return await _cloudinary.DestroyAsync(deleteParams);
    }
}
