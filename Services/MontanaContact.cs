using MontanaTgBot.Services.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MontanaTgBot.Services
{
    internal class MontanaContact : IMenuAction
    {
        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message locationMessage = await botClient.SendVenueAsync(chatId: update.Message.Chat.Id,
                                                                     latitude: 54.90708103435659, // add to constant
                                                                     longitude: 26.70834078274852,
                                                                     title: "Montana kitchen & bar",
                                                                     address: "к.п. Нарочь , ул. Набережная ,1, Беларусь",
                                                                     cancellationToken: cancellationToken);
        }
    }
}
