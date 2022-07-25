using Azure.Messaging.ServiceBus;
using Azure.Storage.Queues;
using Filuet.Infrastructure.Abstractions.Communication;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filuet.Infrastructure.Communication.Notifications
{
    /// <summary>
    /// Filuet Notification Service based on Azure Service Bus
    /// </summary>
    public class AzureServiceBusNotificationManager : INotificationManager
    {
        public event EventHandler<EventItem> OnEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceBusConnectionString"></param>
        /// <param name="queueName"></param>
        /// <param name="authorId">Terminal Id or something</param>
        public AzureServiceBusNotificationManager(string serviceBusConnectionString, string queueName, string authorId)
        {
            _busClient = new ServiceBusClient(serviceBusConnectionString);
            _sender = _busClient.CreateSender(queueName);
            _authorId = authorId;
        }

        private  ServiceBusMessage CreateMessage(NotificationTypes type, string message = "", string authorId = null)
        {
            using MemoryStream stream = new MemoryStream();
            NotificationModel model = new NotificationModel {
                TerminalId = authorId ?? _authorId,
                Type = type,
                Message = message
            };

            ProtoBuf.Serializer.Serialize(stream, model);
            byte[] bytes = stream.ToArray();
            stream.Position = 0;
            OnEvent?.Invoke(this, EventItem.Info($"Message sent {Encoding.UTF8.GetString(bytes)}"));
            return new ServiceBusMessage(bytes);
        }

        private Task SendMessage(NotificationTypes notificationType, string additionalInfo, string terminalId = null)
        {
            try
            {
                _lastNotification = notificationType;
                ServiceBusMessage brokeredMessage = CreateMessage(notificationType, ReplaceSemicolonWithSystemChar(additionalInfo), terminalId);
                return _sender.SendMessageAsync(brokeredMessage);
            }
            catch (Exception e)
            {
                OnEvent?.Invoke(this, EventItem.Error("Error when send message", e));
                return Task.FromResult(false);
            }
        }

        /// <inheritdoc cref="INotificationManager.AddMessageToSend(string,string)"/>
        public void AddMessageToSend(string message, string terminalId = null)
        {
            SendMessage(NotificationTypes.Custom, message, terminalId).Wait();
        }

        /// <inheritdoc cref="INotificationManager.AddMessageToSendAsync(string,string)"/>
        public async Task AddMessageToSendAsync(string message, string terminalId = null)
        {
            await SendMessage(NotificationTypes.Custom, message, terminalId).ConfigureAwait(false);
        }

        /// <inheritdoc cref="INotificationManager.AddMessageToSend(NotificationTypes, string,string)"/>
        public void AddMessageToSend(NotificationTypes type, string additionalInfo, string terminalId = null)
        {
            SendMessage(type, additionalInfo, terminalId).Wait();
        }

        /// <inheritdoc cref="INotificationManager.AddMessageToSendAsync(NotificationTypes, string,string)"/>
        public async Task AddMessageToSendAsync(NotificationTypes type, string additionalInfo, string terminalId = null)
        {
            await SendMessage(type, additionalInfo, terminalId).ConfigureAwait(false);
        }

        /// <inheritdoc cref="INotificationManager.ClearLastAlert"/>
        public void ClearLastAlert()
        {
            if (_lastNotification == null)
                return;

            try
            {
                ServiceBusMessage brokeredMessage = CreateMessage(NotificationTypes.AlertClear, $"lastAlert={_lastNotification}");
                _sender.SendMessageAsync(brokeredMessage).Wait();
            }
            catch (Exception e)
            {
                OnEvent?.Invoke(this, EventItem.Error("Error while clearing last alert", e));
            }
            finally
            {
                _lastNotification = null;
            }
        }

        public static string ReplaceSemicolonWithSystemChar(string input)
        {
            string result = input.Replace(';', 'Ì');
            // replacing in additionalInfo semicolon with Ì
            List<int> foundIndexes = new List<int>();
            for (int i = result.IndexOf('='); i > -1; i = result.IndexOf('=', i + 1))
            {
                foundIndexes.Add(i);
            }

            if (foundIndexes.Count <= 1)
                return result;

            foreach (int idx in foundIndexes.Skip(1))
            {
                int lastSemicolonIdx = result.LastIndexOf('Ì', idx);
                StringBuilder sb = new StringBuilder(result);
                sb[lastSemicolonIdx] = ';';
                result = sb.ToString();
            }

            return result;
        }

        private readonly ServiceBusClient _busClient;
        private readonly ServiceBusSender _sender;
        private readonly string _authorId;
        private NotificationTypes? _lastNotification;
    }
}