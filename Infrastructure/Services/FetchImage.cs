using Application.Interfaces;
using HtmlAgilityPack;

namespace Infrastructure.Services;

public class FetchImage : IFetchImage
{
    public async Task<string> ExecuteAsync()
    {
        const string url = "https://sh2-kuvandyk-r56.gosweb.gosuslugi.ru/glavnoe/raspisanie/";

        var httpClient = new HttpClient();
        var htmlContent = await httpClient.GetStringAsync(url);
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(htmlContent);
        var imgNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'object-item full  ')]//img");
        if (imgNode == null) return null;
        var imageUrl = "https://sh2-kuvandyk-r56.gosweb.gosuslugi.ru" +
                       imgNode.GetAttributeValue("src", string.Empty);
        return imageUrl;
    }
}