using System;

namespace Filuet.Infrastructure.Abstractions.Models
{
    /// <summary>
    /// System event represented by log item
    /// </summary>
    public sealed class EventItem
    {
        public enum EventLevel
        {
            Trace = 0,
            Debug,
            Info,
            Warning,
            Error,
            Critical
        }

        public EventLevel Level { get; private set; } = EventLevel.Debug;

        /// <summary>
        /// A message with layout
        /// </summary>
        public string LayoutMessage { get; private set; }

        /// <summary>
        /// An exception
        /// </summary>
        public string ErrorMessage { get; private set; }

        public Exception Exception { get; private set; }

        /// <summary>
        /// Item timestamp
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        public bool IsError { get { return !string.IsNullOrWhiteSpace(ErrorMessage) || Exception != null; } }


        static public EventItem Info(string info)
            => new EventItem { Level = EventLevel.Info, LayoutMessage = info.Trim(), Timestamp = DateTime.Now };

        static public EventItem Debug(string info)
            => new EventItem { Level = EventLevel.Debug, LayoutMessage = info.Trim(), Timestamp = DateTime.Now };

        static public EventItem Error(string error = "", Exception exception = null, string info = null)
        {
            if (exception == null && error.Length == 0)
                throw new ArgumentException("Incorrect error log arguments");

            return new EventItem { Level = EventLevel.Error, ErrorMessage = error?.Trim(), LayoutMessage = info?.Trim(), Exception = exception, Timestamp = DateTime.Now };
        }

        internal EventItem SetTimestamp(DateTimeOffset timestamp)
        {
            Timestamp = timestamp;
            return this;
        }

        static public EventItem Create(DateTimeOffset timestamp, string info, string error, Exception exception = null)
        {
            EventItem result = null;

            if (exception != null || !string.IsNullOrWhiteSpace(error))
                result = Error(error, exception, info);

            result = Info(info);

            return result?.SetTimestamp(timestamp);
        }
    }
}