namespace MontanaTgBot.Models
{
    internal class Customer
    {
        public long ChatId { get; set; }
        public string? UserName { get; set; }
        public string PnoneNumber { get; set; }

        public Cart Cart { get; set; }

        public Customer(long chatId, string? userName, string pnoneNumber)
        {
            ChatId = chatId;
            UserName = userName;
            PnoneNumber = pnoneNumber;
        }
    }
}
