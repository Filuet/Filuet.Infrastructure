using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace Filuet.Infrastructure.Abstractions.Communication
{
    public interface INotificationManager : IEventProducer
    {
        event EventHandler<EventItem> OnEvent;

        /// <summary>
        /// Добавить кастомное сообщение в список на отправку
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="terminalId">Имя терминала отправителя</param>
        void AddMessageToSend(string message, string terminalId = null);
        /// <summary>
        /// Асинхронно добавить кастомное сообщение в список на отправку
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="terminalId">Имя терминала отправителя</param>
        Task AddMessageToSendAsync(string message, string terminalId = null);
        /// <summary>
        /// Добавить уведомление определённого типа в список на отправку
        /// </summary>
        /// <param name="type">Тип уведомления</param>
        /// <param name="message">Любая дополнительная информация</param>
        /// <param name="terminalId">Имя терминала отправителя</param>
        void AddMessageToSend(NotificationTypes type, string message, string terminalId = null);
        /// <summary>
        /// Асинхронно добавить уведомление определённого типа в список на отправку
        /// </summary>
        /// <param name="type">Тип уведомления</param>
        /// <param name="message">Информация от терминала со значениями в формате 'имя аргумента'='значение', разделенные ';'</param>
        /// <param name="terminalId">Имя терминала отправителя</param>
        Task AddMessageToSendAsync(NotificationTypes type, string message, string terminalId = null);
        /// <summary>
        /// Send notification that previous alert is not valid anymore ("Alert has been cleared" or smthg)
        /// </summary>
        void ClearLastAlert();
    }
}
