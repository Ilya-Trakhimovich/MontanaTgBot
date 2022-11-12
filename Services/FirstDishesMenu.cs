using MontanaTgBot.Models.KeyBoardMarkups;
using MontanaTgBot.Services.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MontanaTgBot.Services
{
    internal class FirstDishesMenu : IMenuAction
    {
        private readonly ReplyKeyboardMarkup firstMealKeyboardMarkup;

        public FirstDishesMenu()
        {
            firstMealKeyboardMarkup = MontanaKeyboard.GetMontanaFirstDishMenuKeyboard();
        }

        public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // fileId - "AgACAgIAAxkDAAIBL2NuiVqOH3-uZF8nqOJr03FID4zWAAIKxTEbizx4S8Cc3WxUH2CCAQADAgADcwADKwQ"

            //Message mealIdMessage1 = await botClient.SendPhotoAsync(chatId: update.Message.Chat.Id,
            //                                                        caption: "Борщ\n<b>Комплектация</b>: <i>борщ- 250 мл, хлеб, чеснок, сало</i>",
            //                                                        photo: "AgACAgIAAxkDAAIBL2NuiVqOH3-uZF8nqOJr03FID4zWAAIKxTEbizx4S8Cc3WxUH2CCAQADAgADcwADKwQ",
            //                                                        parseMode: ParseMode.Html,
            //                                                        cancellationToken: cancellationToken);

            var chatId = update.Message?.Chat.Id;
            var firstMeals = GetFirstMeals();

            foreach (var f in firstMeals)
            {
                using var image = System.IO.File.OpenRead($"D:\\VS\\MontanaTgBot\\Sources\\FirstMeal\\{f.Key}.jpg");

                Message mealMessage1 = await botClient.SendPhotoAsync(chatId: chatId,
                    caption: $"{firstMeals[f.Key].Item1}\n<b>Комплектация</b>: <i>{firstMeals[f.Key].Item2}</i>",
                    photo: image,
                    parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken); ;
            }

            Message sentFirstMealtMessage = await botClient.SendTextMessageAsync(chatId: chatId,
                                                                                 text: "Заказать",
                                                                                 replyMarkup: firstMealKeyboardMarkup,
                                                                                 cancellationToken: cancellationToken);
        }



        private Dictionary<string, Tuple<string, string>> GetFirstMeals()
        {
            return new Dictionary<string, Tuple<string, string>>
            {
                { "borsh", new Tuple<string, string>( "Борщ", "борщ - 250 мл, хлеб, чеснок, сало" ) },
                { "solyanka", new Tuple<string, string>("Солянка", "солянка - 250 мл, хлеб")}
            };
        }
    }
}
