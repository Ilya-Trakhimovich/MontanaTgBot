using MontanaTgBot.Models.KeyBoardMarkups;
using MontanaTgBot.Services.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MontanaTgBot.Services
{
    internal class StartMenu : IMenuAction
    {
        private readonly ReplyKeyboardMarkup firstMeakKeyboardMarkup;

        public StartMenu()
        {
            firstMeakKeyboardMarkup = MontanaKeyboard.GetStartMenuKeyboard();
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sentStartMessage = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id,
                                                                            text: "Приветствую! У нас очень вкусная кухня, поехали!",
                                                                            replyMarkup: firstMeakKeyboardMarkup,
                                                                            cancellationToken: cancellationToken);
        }
    }
}
