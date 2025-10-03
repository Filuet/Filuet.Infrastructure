using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Ordering.Dto
{
    public enum OrderRollbackReason
    {
        /// <summary>
        /// The order pricing is outdated or the payment link has expired
        /// </summary>
        [Code("Expired")]
        Expired = 0x01,
        /// <summary>
        /// Cancelled by the customer
        /// </summary>
        [Code("Cancelled")]
        Cancelled,
        /// <summary>
        /// Order params have been changed, cost recalculation is needed
        /// </summary>
        [Code("Changed")]
        Changed 
    }
}
