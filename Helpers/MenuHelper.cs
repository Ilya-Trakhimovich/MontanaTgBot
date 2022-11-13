using MontanaTgBot.Data.Constants;
using MontanaTgBot.Services;
using MontanaTgBot.Services.Interface;
using System.Threading;
using Telegram.Bot.Types;

namespace MontanaTgBot.Helpers
{
    internal static class MenuHelper
    {
        //TODO: check whether a specific library exists for determinig correct word ending
        public static string GetCorrectRuble(double price)
        {
            var amount1 = new List<double> { 1, 21, 31, 41, 51, 61 };
            var amount2 = new List<double> { 2, 3, 4, 22, 23, 24, 32, 33, 34, 42, 43, 44, 52, 53, 54, 62, 63, 64 };

            if (amount1.Contains(price))
                return Constants.Ruble1;
            else if (amount2.Contains(price))
                return Constants.Ruble2;

            return Constants.Rubles;
        }

        public static IDictionary<string, IMenuAction> GetStartMenu()
        {
            return new Dictionary<string, IMenuAction>
            {
                { Constants.START, new StartMenu()},
                { Constants.MENU.ToLower(), new CategoryMenu()},
                { Constants.CONTACT.ToLower(), new MontanaContact() }
            };
        }

        public static IList<string> GetCategorytMenu()
        {
            return new List<string>
            {
                Constants.FIRST_DISHES.ToLower(),
                Constants.SECOND_DISHES.ToLower(),
                Constants.PIZZA.ToLower(),
                Constants.DRINKS.ToLower()
            };
        }        

        public static IList<string> GetAmountMenu()
        {
            return new List<string>
            {
                Constants.ONE, Constants.TWO, Constants.THREE, Constants.FOUR, Constants.FIVE_AND_MORE.ToLower()
            };
        }

        public static (bool, string, string[]) GetDishesByCategory(string category)
        {
            var firstDishesMenu = GetFirstDishesMenu();
            if (firstDishesMenu.ContainsKey(category)) return (true, DishConstants.FIRST_DISHES, firstDishesMenu[category]);

            var secondDishesMenu = GetSecondDishesMenu();
            if (secondDishesMenu.ContainsKey(category)) return (true, DishConstants.SECOND_DISHES, secondDishesMenu[category]);

            var pizzaMenu = GetPizzaMenu();
            if (pizzaMenu.ContainsKey(category)) return (true, DishConstants.PIZZA, pizzaMenu[category]);

            var drinksMenu = GetDrinksMenu();
            if (drinksMenu.ContainsKey(category)) return (true, DishConstants.DRINKS, drinksMenu[category]);

            return (false, string.Empty, null);
        }

        public static bool IsEnteredDishExist(string dish)
        {
            var firstDishes = GetFirstDishesMenu();
            if (firstDishes[Constants.FIRST_DISHES.ToLower()].Contains(dish)) return true;

            var secondDishes = GetSecondDishesMenu();
            if (secondDishes[Constants.SECOND_DISHES.ToLower()].Contains(dish)) return true;

            var pizzaMenu = GetPizzaMenu();
            if (pizzaMenu[Constants.PIZZA.ToLower()].Contains(dish)) return true;

            var drinksMenu = GetDrinksMenu();
            if (drinksMenu[Constants.DRINKS.ToLower()].Contains(dish)) return true;

            return false;
        }

        private static IDictionary<string, string[]> GetFirstDishesMenu()
        {
            return new Dictionary<string, string[]>
            {
                { Constants.FIRST_DISHES.ToLower(), new string[] { DishConstants.BORSCH.ToLower(), DishConstants.SOLYANKA.ToLower() }}
            };
        }

        private static IDictionary<string, string[]> GetSecondDishesMenu()
        {
            return new Dictionary<string, string[]>
            {
                { Constants.SECOND_DISHES.ToLower(), new string[] { DishConstants.PASTA.ToLower(), DishConstants.DRANIKI.ToLower() } }
            };
        }

        private static IDictionary<string, string[]> GetPizzaMenu()
        {
            return new Dictionary<string, string[]>
            {
                { Constants.PIZZA.ToLower(), new string[] { DishConstants.PIZZA1.ToLower(), DishConstants.PIZZA2.ToLower() }}
            };
        }

        private static IDictionary<string, string[]> GetDrinksMenu()
        {
            return new Dictionary<string, string[]>
            {
                { Constants.DRINKS.ToLower(),  new string[] { DishConstants.COLA.ToLower(), DishConstants.SPRITE.ToLower() }}
            };
        }        
    }
}
