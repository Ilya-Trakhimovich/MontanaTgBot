using MontanaTgBot.Models.KeyBoardMarkups;
using MontanaTgBot.Services.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MontanaTgBot.Services
{
    internal class CategoriesMenu : IMenuAction
    {
        private readonly ReplyKeyboardMarkup menuKeyboardMarkup;

        public CategoriesMenu()
        {
            menuKeyboardMarkup = MontanaKeyboard.GetMontanaMenuKeyboard();
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sentMenuMessage = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id,
                                                                           text: "блюда", // add constants class
                                                                           replyMarkup: menuKeyboardMarkup,
                                                                           cancellationToken: cancellationToken);
        }
    }
}
