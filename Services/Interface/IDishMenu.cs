using Telegram.Bot.Types;
using Telegram.Bot;

namespace MontanaTgBot.Services.Interface
{
    internal interface IDishMenu
    {
        Task Execute(string category, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, params string[] dishes);
    }
}
