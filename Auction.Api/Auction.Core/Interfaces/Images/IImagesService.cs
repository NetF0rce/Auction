using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Auction.Core.Interfaces.Images;
public interface IImagesService
{
    Task<ImageUploadResult> AddImageAsync(IFormFile formFile);
    Task<DeletionResult> DeleteImageAsync(string publicId);
}
