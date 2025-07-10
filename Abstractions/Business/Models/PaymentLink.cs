using System;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class PaymentLink
    {
        public string Url { get; set; }
        public DateTimeOffset Expiration { get; set; }

        public override string ToString()
            => $"{Url} {(Expiration < DateTimeOffset.Now ? "" : string.Empty)}".Trim();
    }
}
