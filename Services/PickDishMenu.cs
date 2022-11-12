
using MontanaTgBot.Models.KeyBoardMarkups;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace MontanaTgBot.Services
{
    internal class PickDishMenu
    {
        private readonly ReplyKeyboardMarkup _pickedDishKeyboardMarkup;

        public PickDishMenu()
        {
            _pickedDishKeyboardMarkup = MontanaKeyboard.GetPickedDishMenuKeyboard();
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sentBorshAmounttMessage = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id,
                                                                                   text: $"Вы выбрали {update.Message.Text}",
                                                                                   replyMarkup: _pickedDishKeyboardMarkup,
                                                                                   cancellationToken: cancellationToken);
        }
    }
}
