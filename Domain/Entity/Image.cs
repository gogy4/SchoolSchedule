namespace Domain.Entity;

public class Image
{
    public string Url { get; private set; }

    public Image(string url)
    {
        Url = url;
    }
}