using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Bot;
using System.Threading.Tasks;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var botToken = Environment.GetEnvironmentVariable("7579010241:AAGEBUpzrMRZC9_VsBHitKvRvDGFXiwnN9M") ?? "7579010241:AAGEBUpzrMRZC9_VsBHitKvRvDGFXiwnN9M";

        services.AddSingleton<ITelegramBotService>(new TelegramBotClient(botToken)); // Используем TelegramBotService
        services.AddSingleton<BotHandler>(); // Регистрируем BotHandler
        services.AddSingleton<IFetchImage, FetchImage>(); 
    })
    .Build();

var botHandler = builder.Services.GetRequiredService<BotHandler>();
var botService = builder.Services.GetRequiredService<ITelegramBotService>();

// Запуск прослушивания обновлений через StartReceivingAsync
botService.StartReceivingAsync(botHandler.HandleUpdateAsync);

await builder.RunAsync();