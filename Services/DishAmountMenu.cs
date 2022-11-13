using MontanaTgBot.Models.KeyBoardMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MontanaTgBot.Services
{
    internal class DishAmountMenu
    {
        private readonly ReplyKeyboardMarkup _amountKeyboardMarkup;

        public DishAmountMenu()
        {
            _amountKeyboardMarkup = MontanaKeyboard.GetAmountMenuKeyboard();
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sentBorshAmounttMessage = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id,
                                                                                   text: "Сколько порций? Выберите количество",
                                                                                   replyMarkup: _amountKeyboardMarkup,
                                                                                   cancellationToken: cancellationToken);
        }
    }
}
