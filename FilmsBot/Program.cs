using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


class Program
{
    static ITelegramBotClient
        bot = new TelegramBotClient("6271314742:AAHB9QjBuQOAjzi0VoCj71DNNgqarYy9SfE"); // token

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;
            var name = update.Message.From.FirstName;
            if (message.Text.ToLower() == "/start")
            {
                Console.WriteLine(
                    $"\nid_user = {update.Id} \t name_user = {update.Message.From.Username} " +
                    $"\t lang_user = {update.Message.From.LanguageCode}\n");

                await botClient.SendTextMessageAsync(message.Chat,
                    $"Привіт, {name}!\n\n/menu - подивитись всі фільми 🎦");
                return;
            }


            await MenuButton(botClient, update, cancellationToken);
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }

    public static async Task MenuButton(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;
            var name = update.Message.From.FirstName;

            if (message == null || message.Type != MessageType.Text) return; // перевіряємо на null

            if (message.Text != null && message.Text.ToLower() == "/menu")
            {
                InlineKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithUrl(
                            text: "Фільми в прокаті",
                            url: "https://en.wikipedia.org/wiki/Main_Page"
                        )
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithUrl(
                            text: "Додати фільм",
                            url: "https://en.wikipedia.org/wiki/Main_Page"
                        )
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithUrl(
                            text: "Видалити фільм",
                            url: "https://en.wikipedia.org/wiki/Main_Page"
                        )
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithUrl(
                            text: "Подивитись всі фільми",
                            url: "https://en.wikipedia.org/wiki/Main_Page"
                        )
                    }
                });

                await botClient.SendTextMessageAsync(message.Chat, "Оберіть, що вам потрібно:)",
                    replyMarkup: replyKeyboardMarkup);
            }
        }
    }


    static void Main(string[] args)
    {
        Console.WriteLine($"Let's go {bot.GetMeAsync().Result.FirstName}!");
        Update update = new Update();
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // отримати всі типи оновлень
        };

        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
}