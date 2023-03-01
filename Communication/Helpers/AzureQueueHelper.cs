using Azure.Storage.Queues;
using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Filuet.Infrastructure.Communication.Helpers
{
    public class AzureQueueHelper
    {
        private QueueClient _queue;
        public bool IsInitialized { get; private set; }

        public AzureQueueHelper() { }

        public async Task InitAsync(string queueName, string storageConnectionString)
        {
            var tokenSource = new CancellationTokenSource();

            tokenSource.CancelAfter(30000);

            // Get queue... create if does not exist.
            _queue = new QueueClient(storageConnectionString, queueName);
            await _queue.CreateIfNotExistsAsync(null, tokenSource.Token);
            IsInitialized = true;
        }

        /// <summary>
        /// Used to add new message to storage queue for processing
        /// </summary>
        /// <param name="modifyRequest">Request object with needed details</param>=
        public async Task AddAsyncOperationRequestToQueue(string modifyRequest)
        {
            if (!IsInitialized)
                throw new InvalidAsynchronousStateException("Not initialized");

            var tokenSource = new CancellationTokenSource();

            tokenSource.CancelAfter(30000);
            // Add entry to queue
            await _queue.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(modifyRequest)), tokenSource.Token);
        }

        /// <summary>
        /// Used to add new message to storage queue for processing
        /// </summary>
        /// <param name="modifyRequest">Request object with needed details</param>
        public void AddOperationRequestToQueue(string modifyRequest)
        {
            if (!IsInitialized)
                throw new InvalidAsynchronousStateException("Not initialized");

            var tokenSource = new CancellationTokenSource();

            tokenSource.CancelAfter(30000);
            // Add entry to queue
            _queue.SendMessage(Convert.ToBase64String(Encoding.UTF8.GetBytes(modifyRequest)), tokenSource.Token);
        }
    }
}