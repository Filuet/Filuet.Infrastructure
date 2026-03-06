using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;

namespace Filuet.Infrastructure.Communication.Helpers
{
    public class AzureQueueHelpers
    {
        private QueueClient _queue;
        public bool IsInitialized { get; private set; }

        public AzureQueueHelpers() { }

        public async Task InitAsync(string queueName, string storageConnectionString) {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(30000);
            // Get queue or create if doesn't exist
            _queue = new QueueClient(storageConnectionString, queueName);
            await _queue.CreateIfNotExistsAsync(null, tokenSource.Token);
            IsInitialized = true;
        }
        /// <summary>
        /// Use this method for already created queue, otherwise use InitAsync method
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="storageConnectionString"></param>
        public void InitializeExistingQueue(string queueName, string storageConnectionString) {
            _queue = new QueueClient(storageConnectionString, queueName);
            IsInitialized = true;
        }

        /// <summary>
        /// Used to add new message to storage queue for processing
        /// </summary>
        /// <param name="modifyRequest">Request object with needed details</param>=
        public async Task AddAsyncOperationRequestToQueue(string modifyRequest) {
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
        public void AddOperationRequestToQueue(string modifyRequest) {
            if (!IsInitialized)
                throw new InvalidAsynchronousStateException("Not initialized");

            var tokenSource = new CancellationTokenSource();

            tokenSource.CancelAfter(30000);
            // Add entry to queue
            _queue.SendMessage(Convert.ToBase64String(Encoding.UTF8.GetBytes(modifyRequest)), tokenSource.Token);
        }
    }
}