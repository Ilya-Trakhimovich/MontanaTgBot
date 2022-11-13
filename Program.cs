using MontanaTgBot.Data.Constants;
using MontanaTgBot.Helpers;
using MontanaTgBot.Models;
using MontanaTgBot.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var botClient = new TelegramBotClient("");
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
    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;
    
    var chatId = message.Chat.Id;
    var clientMessage = message.Text.ToLower();

    Console.WriteLine($"Received '{messageText}' message in chat {chatId}.");

    await HandleStartMenu(botClient, update, cancellationToken, clientMessage);
    await HandleCategoryDishMenu(botClient, update, cancellationToken, clientMessage);    
    await HandleSelectedDish(botClient, update, cancellationToken, clientMessage);
    await HandleAmountMenu(botClient, update, cancellationToken, clientMessage);
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

async Task HandleStartMenu(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string clientMessage)
{
    var startMenu = MenuHelper.GetStartMenu();
    var validMenuAction = startMenu.ContainsKey(clientMessage);

    if (validMenuAction)
    {
        var menuAction = startMenu[clientMessage];

        await menuAction.Execute(botClient, update, cancellationToken);
    }
}

async Task HandleCategoryDishMenu(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string clientMessage)
{
    var categoryMenu = MenuHelper.GetCategorytMenu();

    if (categoryMenu.Contains(clientMessage))
    {
        var dishTuple = MenuHelper.GetDishesByCategory(clientMessage);

        if (dishTuple.Item1) await new DishMenu().Execute(dishTuple.Item2, botClient, update, cancellationToken, dishTuple.Item3);
    }
}

async Task HandleAmountMenu(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string clientMessage)
{
    var amountMenu = MenuHelper.GetAmountMenu();
    var validAmountAction = amountMenu.Contains(clientMessage);

    if (validAmountAction)
        await new SelectDishMenu().Execute(botClient, update, cancellationToken);
}

async Task HandleSelectedDish(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string clientMessage)
{
    var isDishExist = MenuHelper.IsEnteredDishExist(clientMessage);

    if (isDishExist) await new DishAmountMenu().Execute(botClient, update, cancellationToken); 
}