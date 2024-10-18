using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum NotificationTypes : int
    {
        /// <summary>
        /// Any data. See Message field
        /// </summary>
        [Code("Custom")]
        Custom = 0,
        /// <summary>
        /// Sku is running low
        /// </summary>
        [Code("LowSkuQuantity")]
        LowSkuQuantity = 1,
        /// <summary>
        /// The kiosk is going to OOS because of DB exception
        /// </summary>
        [Code("TerminalToOosFromDb")]
        TerminalToOosFromDb = 3,
        /// <summary>
        /// The kiosk is going to OOS because of a device exception
        /// </summary>
        [Code("TerminalToOosFromInoperableState ")]
        TerminalToOosFromInoperableState = 5,
        /// <summary>
        /// The kiosk asks for a maintenance
        /// </summary>
        [Code("TerminalNeedMaintenance")]
        TerminalNeedMaintenance = 6,
        /// <summary>
        /// A dispensing error occured
        /// </summary>
        [Code("DispenseError")]
        DispenseError = 8,
        /// <summary>
        /// Need to check notes and make an intentory
        /// </summary>
        [Code("NeedCashInventory")]
        NeedCashInventory = 10,
        /// <summary>
        /// The last session was closed successfully
        /// </summary>
        [Code("ZReportCloseSuccess")]
        ZReportCloseSuccess = 12,
        /// <summary>
        /// The last session wasn't closed
        /// </summary>
        [Code("ZReportCloseFail")]
        ZReportCloseFail = 13,
        /// <summary>
        /// Notification that previous alert is not valid anymore
        /// </summary>
        [Code("AlertClear")]
        AlertClear = 14,
        /// <summary>
        /// Notification that order request submitting was fault
        /// </summary>
        [Code("SubmitError")]
        SubmitError = 15,
        /// <summary>
        /// Notification that found "red" barcode on belt
        /// </summary>
        [Code("RedAlertBarcode")]
        RedAlertBarcode = 16,
        /// <summary>
        /// Notification about cash flow in PosTools (Widthdrawal\Income)
        /// </summary>
        [Code("CashFlow")]
        CashFlow = 17,
        /// <summary>
        /// Deliver XReport on button click
        /// </summary>
        [Code("XReportDelivery")]
        XReportDelivery = 18,
        /// <summary>
        /// Deliver ZReport on button click
        /// </summary>
        [Code("ZReportDelivery")]
        ZReportDelivery = 19,
        /// <summary>
        /// Underpayment on Change issue
        /// </summary>
        [Code("CashPaymentIssue")]
        CashPaymentIssue = 20,
        /// <summary>
        /// No barcodes exported for the last N days
        /// </summary>
        [Code("NoBacrodesExported")]
        NoBacrodesExported = 21
    }
}