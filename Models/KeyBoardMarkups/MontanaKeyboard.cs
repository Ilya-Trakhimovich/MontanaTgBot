using MontanaTgBot.Data.Constants;
using Telegram.Bot.Types.ReplyMarkups;

namespace MontanaTgBot.Models.KeyBoardMarkups
{
    internal static class MontanaKeyboard
    {
        public static ReplyKeyboardMarkup GetStartMenuKeyboard()
        {
            return new(new[]
            {
                new KeyboardButton[] { Constants.MENU, Constants.CONTACT }
            })
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup GetCategoryMenuKeyboard()
        {
            return new(new[]
            {
                new KeyboardButton[] { Constants.FIRST_DISHES},
                new KeyboardButton[] { Constants.SECOND_DISHES },
                new KeyboardButton[] { Constants.PIZZA},
                new KeyboardButton[] { Constants.DRINKS },
            })
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup GetAmountMenuKeyboard()
        {
            return new(new[]
            {
                new KeyboardButton[] { Constants.ONE, Constants.TWO },
                new KeyboardButton[] { Constants.THREE, Constants.FOUR},
                new KeyboardButton[] {  Constants.FIVE_AND_MORE },
            })
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup GetDishMenuKeyboard(params string[] dishes)
        {
            var dishKeyBoard = new KeyboardButton[dishes.Length];

            for (var i = 0; i < dishes.Length; i++)
            {
                dishKeyBoard[i] = dishes[i];
            }

            return new(new[] { dishKeyBoard }) { ResizeKeyboard = true };
        }

        public static ReplyKeyboardMarkup GetSelectedDishMenuKeyboard()
        {
            return new(new[]
            {
                new KeyboardButton[] { Constants.MENU },
                new KeyboardButton[] { Constants.CART }
            })
            {
                ResizeKeyboard = true
            };
        }
    }
}
