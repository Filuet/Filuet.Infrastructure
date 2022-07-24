using Filuet.Infrastructure.Abstractions.Communication;
using Filuet.Infrastructure.Communication.Notifications;
using Xunit;

namespace Test
{
    public class NotificationTest
    {
        [Theory]
        [InlineData("Endpoint=sb://ascnotifications.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=jNCSsy2JbREOh/dtJmdeVL+Ql+ms81eT5QVQVkynriM=", "notificationsqueue", "foo")]
        public void Test_Send(string serviceBusCs, string queueName, string author)
        {
            // Prepare
            INotificationManager notificationManager = new AzureServiceBusNotificationManager(serviceBusCs, queueName, author);

            // Pre-validate


            // Perform
            notificationManager.AddMessageToSend("Hello, World!");

            // Post-validate
        }
    }
}
