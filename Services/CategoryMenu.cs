using MontanaTgBot.Models.KeyBoardMarkups;
using MontanaTgBot.Services.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MontanaTgBot.Services
{
    internal class CategoryMenu : IMenuAction
    {
        private readonly ReplyKeyboardMarkup menuKeyboardMarkup;

        public CategoryMenu()
        {
            menuKeyboardMarkup = MontanaKeyboard.GetCategoryMenuKeyboard();
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id,
                                                text: "Категории", // add to constants class
                                                replyMarkup: menuKeyboardMarkup,
                                                cancellationToken: cancellationToken);
        }
    }
}
