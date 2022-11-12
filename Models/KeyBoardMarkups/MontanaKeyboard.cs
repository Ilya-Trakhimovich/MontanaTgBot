using Telegram.Bot.Types.ReplyMarkups;

namespace MontanaTgBot.Models.KeyBoardMarkups
{
    internal static class MontanaKeyboard
    {
        public static ReplyKeyboardMarkup GetMontanaStartMenuKeyboard()
        {
            return new(new[]
            {
                new KeyboardButton[] { "Меню", "Контакты" }
            })
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup GetMontanaMenuKeyboard()
        {
            return new(new[]
            {
                new KeyboardButton[] { "Первые блюда" },
                new KeyboardButton[] { "Вторые блюда" },
                new KeyboardButton[] { "Напитки" },
            })
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup GetMontanaAmountMenuKeyboard()
        {
            return new(new[]
            {
                new KeyboardButton[] { "1", "2" },
                new KeyboardButton[] { "3", "4" },
                new KeyboardButton[] { "5 и более (с Вами свяжется администратор)" },
            })
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup GetMontanaFirstDishMenuKeyboard()
        {
            return new(new[]
            {
                new KeyboardButton[] { "Борщ", "Солянка" }
            })
            {
                ResizeKeyboard = true
            };
        }

        public static ReplyKeyboardMarkup GetPickedDishMenuKeyboard()
        {
            return new(new[]
            {
                new KeyboardButton[] { "Меню" },
                new KeyboardButton[] { "Корзина" }
            })
            {
                ResizeKeyboard = true
            };
        }
    }
}
