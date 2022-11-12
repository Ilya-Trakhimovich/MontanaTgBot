using Telegram.Bot.Types;
using Telegram.Bot;

namespace MontanaTgBot.Services.Interface
{
    internal interface IMenuAction
    {
        Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
