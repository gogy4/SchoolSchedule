using Application.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace Presentation.Bot;

public class BotHandler(IFetchImage fetchImage)
{
    public async Task HandleUpdateAsync(ITelegramBotService botService, long chatId, string message, ReplyMarkup replyMarkup = null)
    {
        message = message.ToLower();
        if (message == "расписание")
        {
            var imageUrl = await fetchImage.ExecuteAsync();

            if (!string.IsNullOrEmpty(imageUrl))
            {
                await botService.SendPhotoAsync(chatId, imageUrl);
            }
            else
            {
                await botService.SendMessageAsync(chatId, "Расписание не найдено");
            }
        }
        else
        {
            var reply = GetButtons();
            await botService.SendMessageAsync(chatId, "Выберите команду:", reply);
        }

    }

    private ReplyKeyboardMarkup GetButtons()
    {
        return new ReplyKeyboardMarkup
        {
            Keyboard = new List<IEnumerable<KeyboardButton>>
            {
                new List<KeyboardButton> { new("Расписание") }
            },
            ResizeKeyboard = true,
            OneTimeKeyboard = true
        };
    }
}