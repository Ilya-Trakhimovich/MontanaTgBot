using MontanaTgBot.Data.Constants;
using MontanaTgBot.Helpers;
using MontanaTgBot.Models.KeyBoardMarkups;
using MontanaTgBot.Services.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MontanaTgBot.Services
{
    internal class DishMenu : IDishMenu
    {
        public async Task Execute(string category, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, params string[] dishes)
        {
            var chatId = update.Message?.Chat.Id;
            var firstMeals = GetDisheDescriptionByCategory(category);
            var dishesKeyboardMarkup = MontanaKeyboard.GetDishMenuKeyboard(dishes);

            foreach (var f in firstMeals)
            {
                using var image = System.IO.File.OpenRead($"D:\\VS\\MontanaTgBot\\Sources\\{category}\\{f.Key}.jpg");

                var rubles = MenuHelper.GetCorrectRuble(firstMeals[f.Key].Item3);

                Message mealMessage1 = await botClient.SendPhotoAsync(chatId: chatId,
                    caption: $"<b>{firstMeals[f.Key].Item1}</b>\n" +
                    $"<b>{Constants.PACKAGE}</b>: " +
                    $"<i>{firstMeals[f.Key].Item2}</i>\n" +
                    $"<b>{Constants.PRICE}</b>: " +
                    $"<i>{firstMeals[f.Key].Item3} {rubles}</i>",
                    photo: image,
                    parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken); ;
            }

            Message sentFirstMealtMessage = await botClient.SendTextMessageAsync(chatId: chatId,
                                                                                 text: "Что будете заказывать? Выберите ниже",
                                                                                 replyMarkup: dishesKeyboardMarkup,
                                                                                 cancellationToken: cancellationToken);
        }

        private Dictionary<string, Tuple<string, string, double>> GetDisheDescriptionByCategory(string category)
        {
            if (category == DishConstants.FIRST_DISHES)
            {
                return new Dictionary<string, Tuple<string, string, double>>
                {
                    { DishConstants.BORSCH_IMAGE, GetDishDescription(DishConstants.BORSCH, PackageConstants.BORSCH_PACKAGE, PriceConstants.BORSCH_PRICE)},
                    { DishConstants.SOLYANKA_IMAGE, GetDishDescription(DishConstants.BORSCH, PackageConstants.BORSCH_PACKAGE, PriceConstants.BORSCH_PRICE)}
                };
            }
            else if (category == DishConstants.SECOND_DISHES)
            {
                return new Dictionary<string, Tuple<string, string, double>>
                {
                    { DishConstants.PASTA_IMAGE, GetDishDescription(DishConstants.PASTA, PackageConstants.PASTA_PACKAGE, PriceConstants.PASTA_PRICE)},
                    { DishConstants.DRANIKI_IMAGE, GetDishDescription(DishConstants.DRANIKI, PackageConstants.DRANIKI_PACKAGE, PriceConstants.DRANIKI_PRICE)}
                };
            }
            else if (category == DishConstants.PIZZA)
            {
                return new Dictionary<string, Tuple<string, string, double>>
                {
                    { DishConstants.PIZZA1_IMAGE, GetDishDescription(DishConstants.PIZZA1, PackageConstants.PIZZA1_PACKAGE, PriceConstants.PIZZA1_PRICE)},
                    { DishConstants.PIZZA2_IMAGE, GetDishDescription(DishConstants.PIZZA2, PackageConstants.PIZZA2_PACKAGE, PriceConstants.PIZZA2_PRICE)}
                };
            }
            else if (category == DishConstants.DRINKS)
            {
                return new Dictionary<string, Tuple<string, string, double>>
                {
                    { DishConstants.COLA_IMAGE, GetDishDescription(DishConstants.COLA, PackageConstants.DRINKS_500_PACKAGE, PriceConstants.COLA_PRICE) },
                    { DishConstants.SPRITE_IMAGE, GetDishDescription(DishConstants.SPRITE, PackageConstants.DRINKS_500_PACKAGE, PriceConstants.SPRITE_PRICE)}
                };
            }

            return new Dictionary<string, Tuple<string, string, double>>();
        }

        private Tuple<string, string, double> GetDishDescription(string dish, string package, double price)
        {
            return new Tuple<string, string, double>($"{dish}", $"{dish.ToLower()} - {package}", price);
        }
    }
}
