using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;

namespace Filuet.Infrastructure.Communication.Helpers
{
    /// <summary>
    /// Helper class to work with Azure Storage Queues. It allows to create queues and send messages to them. 
    /// It uses a concurrent dictionary to store queue clients for different queues, so that it can be used in a multi-threaded environment. 
    /// It also uses a timeout of 30 seconds for all operations to avoid hanging in case of issues with Azure Storage.
    /// </summary>
    public class AzureStorageQueueHelper
    {
        private QueueClient _queueClient;
        ConcurrentDictionary<string, QueueClient> _queueClients = new();
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Creates a queue if it doesn't exist and stores the queue client in a concurrent dictionary for later use.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public async Task CreateAsync(string queueName, string connectionString) {
            var cancellationToken =
            _queueClient = new QueueClient(connectionString, queueName);
            await _queueClient.CreateIfNotExistsAsync(cancellationToken: new CancellationTokenSource(Timeout).Token);
            _queueClients.TryAdd(queueName, _queueClient);
        }

        /// <summary>
        /// Sends a message to the specified queue. 
        /// It retrieves the queue client from the concurrent dictionary and sends the message. 
        /// If the queue client is not initialized, it throws an exception.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public async Task SendMessageAsync(string queueName, string message) {
            if (!_queueClients.TryGetValue(queueName, out var queueClient)) {
                throw new System.InvalidOperationException($"Queue client for queue '{queueName}' is not initialized. Call CreateAsync first.");
            }
            await queueClient.SendMessageAsync(message, new CancellationTokenSource(Timeout).Token);
        }

        /// <summary>
        /// Sends a message to the specified queue.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public async Task SendMessageAsync(string queueName, byte[] message) {
            if (!_queueClients.TryGetValue(queueName, out var queueClient)) {
                throw new System.InvalidOperationException($"Queue client for queue '{queueName}' is not initialized. Call CreateAsync first.");
            }
            await queueClient.SendMessageAsync(System.Convert.ToBase64String(message), new CancellationTokenSource(30000).Token);
        }

    }
}
