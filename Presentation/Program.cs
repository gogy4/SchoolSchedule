using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Bot;
using System.Threading.Tasks;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var botToken = Environment.GetEnvironmentVariable("ваш ключ") ?? "ваш ключ";

        services.AddSingleton<ITelegramBotService>(new TelegramBotClient(botToken)); 
        services.AddSingleton<BotHandler>(); 
        services.AddSingleton<IFetchImage, FetchImage>(); 
    })
    .Build();

var botHandler = builder.Services.GetRequiredService<BotHandler>();
var botService = builder.Services.GetRequiredService<ITelegramBotService>();

// Запуск прослушивания обновлений через StartReceivingAsync
botService.StartReceivingAsync(botHandler.HandleUpdateAsync);

await builder.RunAsync();