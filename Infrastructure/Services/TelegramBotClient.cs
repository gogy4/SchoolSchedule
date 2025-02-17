using Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure.Services;

public class TelegramBotClient(string token) : ITelegramBotService
{
    private readonly Telegram.Bot.TelegramBotClient botClient = new(token);

    [Obsolete("Obsolete")]
    public async Task SendMessageAsync(long chatId, string message, ReplyMarkup? replyMarkup = null)
    {
        await botClient.SendTextMessageAsync(chatId, message, replyMarkup: replyMarkup);
    }

    [Obsolete("Obsolete")]
    public async Task SendPhotoAsync(long chatId, string imageUrl)
    {
        var inputFile = new InputFileUrl(imageUrl);
        await botClient.SendPhotoAsync(chatId, inputFile);
    }


    public void StartReceivingAsync(Func<ITelegramBotService, long, string, ReplyMarkup, Task> updateHandler)
    {
        botClient.StartReceiving(
            async (client, update, cancellationToken) =>
            {
                if (update?.Message?.Text != null)
                {
                    var chatId = update.Message.Chat.Id;
                    var message = update.Message.Text;
                    var replyMarkup = new ReplyKeyboardMarkup
                    {
                        Keyboard = new List<IEnumerable<KeyboardButton>>
                        {
                            new List<KeyboardButton> { new("Расписание") }
                        },
                        ResizeKeyboard = true,
                        OneTimeKeyboard = true
                    };
                    await updateHandler(this, chatId, message, replyMarkup);
                }
            },
            async (client, exception, cancellationToken) => { Console.WriteLine($"Error: {exception.Message}"); });
    }
}