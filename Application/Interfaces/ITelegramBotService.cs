using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Interfaces;

public interface ITelegramBotService
{
    Task SendMessageAsync(long chatId, string message, ReplyMarkup replyMarkup = null);
    Task SendPhotoAsync(long chatId, string photoUrl);
    
    void StartReceivingAsync(Func<ITelegramBotService, long, string, ReplyMarkup, Task> updateHandler);
}
