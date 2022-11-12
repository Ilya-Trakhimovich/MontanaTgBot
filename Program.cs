using MontanaTgBot.Data.Constants;
using MontanaTgBot.Services;
using MontanaTgBot.Services.Interface;
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

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    Dictionary<string, IMenuAction> montanaExecuteMenu = new Dictionary<string, IMenuAction>
    {
        { Constants.START, new StartMenu()},
        { Constants.MENU, new CategoriesMenu()},
        { Constants.CONTACT, new MontanaContact() },
        { Constants.FIRST_DISHES, new FirstDishesMenu()}
    };

    var validMenuAction = montanaExecuteMenu.ContainsKey(message.Text);

    if (validMenuAction)
    {
        var menuAction = montanaExecuteMenu[clientMessage];

        await menuAction.Execute(botClient, update, cancellationToken);
    }

    List<string> firstMealMenu = new List<string> { Constants.BORSCH, Constants.SOLYANKA };

    var validFirstMealsAction = firstMealMenu.Contains(clientMessage);

    if (validFirstMealsAction)
    {
        await new DishesMenuAmount().Execute(botClient, update, cancellationToken);
    }

    List<string> amountMenu = new List<string>
    {
        Constants.ONE, Constants.TWO, Constants.THREE, Constants.FOUR, Constants.FIVE_AND_MORE
    };

    var validAmountAction = amountMenu.Contains(clientMessage);

    if (validAmountAction)
    {
        await new PickDishMenu().Execute(botClient, update, cancellationToken);
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
