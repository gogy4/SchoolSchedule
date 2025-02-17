using Domain.Entity;

namespace Domain.Services;

public interface IImageService
{
    Image GetImage(string imagePath);
}