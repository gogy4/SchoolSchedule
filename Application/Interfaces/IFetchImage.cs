namespace Application.Interfaces;

public interface IFetchImage
{
    Task<string> ExecuteAsync();
}