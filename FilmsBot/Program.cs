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

        var text = update.Message.Text;
        if (stage.ContainsKey(update.Message.Chat.Id))
        {
            if (stage[message.Chat.Id] == "deleting")
            {
                string id_from_message = new String(text.Where(Char.IsDigit).ToArray());
                if (int.TryParse(id_from_message, out int id))
                {
                    string baseUrl = "http://localhost:5222/api/Movie/";
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage deleteResponse = await client.DeleteAsync(baseUrl + id);
                        if (deleteResponse.IsSuccessStatusCode)
                            await botClient.SendTextMessageAsync(update.Message.Chat, "Фільм успішно видалено.");
                        else if (deleteResponse.StatusCode == HttpStatusCode.NotFound)
                            await botClient.SendTextMessageAsync(update.Message.Chat, "Фільм не знайдено.");
                    }

                    // Видалення стану "deleting" після успішного видалення фільму
                    stage.Remove(message.Chat.Id);
                }
            }
        }
    }
    else if (update.Type == UpdateType.CallbackQuery)
    {
        var callbackQuery = update.CallbackQuery;
        if (callbackQuery.Data == "films_in_theaters")
        {
            string baseUrl = "http://localhost:5222/api/Movie/";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var films = await response.Content.ReadAsAsync<MovieDTO[]>();
                    string result = "💥 Список фільмів:\n\n";
                    foreach (var film in films)
                    {
                        result += $"🎥 Назва: {film.Title}\n";
                        result += $"🔥 Опис: {film.Description}\n\n";
                    }

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, result);
                }
            }
        }

        if (callbackQuery.Data == "delete_movie_by_id")
        {
            // Перевірка, чи користувач вже ввів ID фільму
            if (!stage.ContainsKey(callbackQuery.Message.Chat.Id))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, "Будь ласка, введіть ID:");
                stage[callbackQuery.Message.Chat.Id] = "deleting";
            }
        }
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
                        InlineKeyboardButton.WithCallbackData("Фільми в прокаті", "films_in_theaters")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Видалити фільм по ID", "delete_movie_by_id")
                    },
                    //new[]
                    //{
                    //    InlineKeyboardButton.WithCallbackData("Додати фільм", "add_films")
                    //}
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