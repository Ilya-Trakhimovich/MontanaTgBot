using System.Reflection.PortableExecutable;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var botClient = new TelegramBotClient("5428969579:AAGkGOc2VavB-EOdg5BdczobBzOpi7zXkvg");

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
};
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;

    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    ReplyKeyboardMarkup mainKeyboardMarkup = new(new[]
    {
        new KeyboardButton[] { "Меню", "Контакты" }
    })
    {
        ResizeKeyboard = true
    };

    ReplyKeyboardMarkup menuKeyboardMarkup = new(new[]
    {
        new KeyboardButton[] { "Первые блюда" },
        new KeyboardButton[] { "Вторые блюда" },
        new KeyboardButton[] { "Напитки" },
    });

    string number = message.Text;

    switch (number)
    {
        case "/start":
            Message sentStartMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Сделайте заказ",
                replyMarkup: mainKeyboardMarkup,
                cancellationToken: cancellationToken);
            break;
        case "Меню":
            Message sentMenuMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "блюда",
                replyMarkup: menuKeyboardMarkup,
                cancellationToken: cancellationToken);
            break;
        case "Контакты":
            Message locationMessage = await botClient.SendVenueAsync(
               chatId: chatId,
               latitude: 54.90708103435659,
               longitude: 26.70834078274852,
               title: "Montana kitchen & bar",
               address: "к.п. Нарочь , ул. Набережная ,1, Беларусь",
               cancellationToken: cancellationToken);
            break;
        case "Первые блюда":
            Message mealMessage = await botClient.SendPhotoAsync(
            chatId: chatId,
            caption: "Борщ",
            photo: new Telegram.Bot.Types.InputFiles.InputOnlineFile(new Uri("https://github.com/Ilya-Trakhimovich/Photo/blob/master/src/FirstMeal/borsh.jpg")),
            parseMode: ParseMode.Html,
            cancellationToken: cancellationToken);
            break;
        default:
            Console.WriteLine("default");
            break;
    }
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);

    return Task.CompletedTask;
}
