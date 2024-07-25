namespace Filuet.Infrastructure.Telegram.Entitites
{
    public class TelegramBotChatLink
    {
        /// <summary>
        /// Telegram Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Chat ID between the user and the Bot
        /// </summary>
        public long ChatId { get; set; }

        public override string ToString()
            => $"{Username} ({ChatId})";
    }
}