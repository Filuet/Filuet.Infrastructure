using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.DataProvider.Interfaces;

namespace Filuet.Infrastructure.Telegram.Entities
{
    /// <summary>
    /// Представляет маршрутизацию отправки уведомлений от терминалов. Маршрутизация описывается терминалом, от которого пришло уведомление и типом уведомления
    /// </summary>
    public class TerminalNotificationsRouting : IEntity
    {
        /// <summary>
        /// Id терминала, от которого пришло сообщение
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// Тип уведомления
        /// </summary>
        public NotificationTypes NotificationType { get; set; }
        /// <summary>
        /// Шаблон заголовка Email, отправляемого получателям
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Шаблон тела Email, отправляемого получателям
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Is email body should be in HTML format
        /// </summary>
        public bool IsBodyHtml { get; set; }
        /// <summary>
        /// Email получателей уведомления через запятую
        /// </summary>
        public ICollection<string>? RecipientsCollection { get; set; }
        /// <summary>
        /// Username'ы в Telegram получателей уведомления через запятую
        /// </summary
        public ICollection<string>? TelegramUsernamesCollection { get; set; }
        /// <summary>
        /// Любая дополнительная информация
        /// </summary>
        public string? Commentary { get; set; }

        public bool Disabled { get; set; }
    }
}
