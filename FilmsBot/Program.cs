using System;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FilmsAPI.Models.DTO;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    static ITelegramBotClient bot = new TelegramBotClient(Environment.GetEnvironmentVariable("BOT_TOKEN"));


    public static Dictionary<long, string> stage = new Dictionary<long, string>();
    
    
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    var message = update.Message;
    if (update.Type == UpdateType.Message)
    {
        var name = update.Message.From.FirstName;
        if (message.Text.ToLower() == "/start")
        {
            await botClient.SendTextMessageAsync(message.Chat, $"Привіт, {name}!\n\n/menu - відкрити головне меню 🎦");
            return;
        }

        await MenuButton(botClient, update, cancellationToken);
    }
}
    
    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        // Console.WriteLine();
    }

    public static async Task MenuButton(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;
            if (message.Text != null && message.Text.ToLower() == "/menu")
            {
                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Випадковий фільм", "random_films")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Додати фільм до списку", "add_new_films")
                    },
                    
                });

                await botClient.SendTextMessageAsync(message.Chat, "Оберіть, що вам потрібно:)",
                    replyMarkup: inlineKeyboardMarkup);
            }
        }
    }

    static async Task Main(string[] args)
    {
        // Console.WriteLine($"Let's go {bot.GetMeAsync().Result.FirstName}!");
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // Отримати всі типи оновлень
        };

        bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
        Console.ReadLine();
    }
}