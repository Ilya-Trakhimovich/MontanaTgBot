using MontanaTgBot.Models.KeyBoardMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MontanaTgBot.Services
{
    internal class DishesMenuAmount
    {
        private readonly ReplyKeyboardMarkup _amountKeyboardMarkup;

        public DishesMenuAmount()
        {
            _amountKeyboardMarkup = MontanaKeyboard.GetMontanaAmountMenuKeyboard();
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sentBorshAmounttMessage = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id,
                                                                                   text: "Выберите количество",
                                                                                   replyMarkup: _amountKeyboardMarkup,
                                                                                   cancellationToken: cancellationToken);
        }
    }
}
