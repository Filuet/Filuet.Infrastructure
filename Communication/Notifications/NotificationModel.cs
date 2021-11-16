using Filuet.Infrastructure.Abstractions.Enums;
using ProtoBuf;

namespace Filuet.Infrastructure.Communication.Notifications
{
    [ProtoContract(SkipConstructor = true)]
    public class NotificationModel
    { /// <summary>
      /// Id терминала, от которого пришло уведомление
      /// </summary>
        [ProtoMember(1)]
        public string TerminalId { get; set; }
        /// <summary>
        /// Терминалы шлют только тип уведомления. Шаблоны самих сообщений хранятся в БД службы уведомлений
        /// </summary>
        [ProtoMember(2, IsRequired = true, Name = "Type")]
        public NotificationTypes Type { get; set; }
        /// <summary>
        /// Какая либо дополнительная информация
        /// </summary>
        [ProtoMember(3)]
        public string Message { get; set; }
    }
}