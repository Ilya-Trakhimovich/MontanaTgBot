using MontanaTgBot.Models.KeyBoardMarkups;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace MontanaTgBot.Services
{
    internal class SelectDishMenu
    {
        private readonly ReplyKeyboardMarkup _selectedDishKeyboardMarkup;

        public SelectDishMenu()
        {
            _selectedDishKeyboardMarkup = MontanaKeyboard.GetSelectedDishMenuKeyboard();
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sentBorshAmounttMessage = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id,
                                                                                   text: $"Вы выбрали {update.Message.Text}",
                                                                                   replyMarkup: _selectedDishKeyboardMarkup,
                                                                                   cancellationToken: cancellationToken);
        }
    }
}
