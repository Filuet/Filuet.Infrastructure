using System.ComponentModel;

namespace Filuet.Infrastructure.Ordering.Enums
{
    public enum OrderAction
    {
        [Description("Open")]
        /// <summary>
        /// Open session of a new order
        /// </summary>
        Open = 1,
        [Description("MoneyIncome")]
        /// <summary>
        /// On money income of any type: cash, card, external...
        /// </summary>
        MoneyIncome = 2,
        [Description("MoneyAccepted")]
        /// <summary>
        /// On money income is over
        /// </summary>
        MoneyAccepted = 3,
        [Description("ChangeIssue")]
        /// <summary>
        /// Extract change
        /// </summary>
        ChangeIssue = 4,
        [Description("Dispense")]
        /// <summary>
        /// Dispense the product
        /// </summary>
        Dispense = 5,
        [Description("PrintSlip")]
        /// <summary>
        /// Print receipt
        /// </summary>
        PrintSlip = 6,
        [Description("Complete")]
        /// <summary>
        /// Complete
        /// </summary>
        Complete = 7,
        [Description("ErrorOccured")]
        /// <summary>
        /// Error occured
        /// </summary>
        ErrorOccured = 8
    }
}
