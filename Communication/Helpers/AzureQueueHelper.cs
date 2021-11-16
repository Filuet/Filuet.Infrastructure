using Azure.Storage.Queues;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Filuet.Infrastructure.Communication.Helpers
{
    public static class AzureQueueHelper
    {
        /// <summary>
        /// Used to add new message to storage queue for processing
        /// </summary>
        /// <param name="modifyRequest">Request object with needed details</param>
        /// <param name="storageConnectionString">Storage connection string</param>
        public static async Task AddAsyncOperationRequestToQueue(string modifyRequest, string queueName, string storageConnectionString)
        {
            // Get queue... create if does not exist.
            QueueClient queue = new QueueClient(storageConnectionString, queueName);
            await queue.CreateIfNotExistsAsync();
            // Add entry to queue
            await queue.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(modifyRequest)));
        }

        /// <summary>
        /// Used to add new message to storage queue for processing
        /// </summary>
        /// <param name="modifyRequest">Request object with needed details</param>
        /// <param name="storageConnectionString">Storage connection string</param>
        public static void AddOperationRequestToQueue(string modifyRequest, string queueName, string storageConnectionString)
        {
            // Get queue... create if does not exist.
            QueueClient queue = new QueueClient(storageConnectionString, queueName);
            queue.CreateIfNotExists();
            // Add entry to queue
            queue.SendMessage(Convert.ToBase64String(Encoding.UTF8.GetBytes(modifyRequest)));
        }
    }
}