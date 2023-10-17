using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum AscActivityType
    {
        /// <summary>
        /// Customer authorized
        /// </summary>
        [Code("login")]
        Login = 0x01,
        /// <summary>
        /// Pricing calculated
        /// </summary>
        [Code("pricing")]
        GetPrice,
        /// <summary>
        /// Order submitted
        /// </summary>
        [Code("submit")]
        Submit,
        /// <summary>
        /// Money income
        /// </summary>
        [Code("income")]
        MoneyIncome,
        /// <summary>
        /// Fact change given
        /// </summary>
        [Code("changeExtracted")]
        TotalChangeGiven,
        /// <summary>
        /// Product issued
        /// </summary>
        [Code("extraction")]
        Extraction,
        /// <summary>
        /// Receipt printed
        /// </summary>
        [Code("printReceipt")]
        PrintReceipt,
        /// <summary>
        /// Customer logout
        /// </summary>
        [Code("logout")]
        Logout,
        /// <summary>
        /// Order resubmit
        /// </summary>
        [Code("resubmit")]
        Resubmit,
        /// <summary>
        /// Sberbank payment request
        /// </summary>
        [Code("sberRequest")]
        SberPlatiQrRequest,
        /// <summary>
        /// Customer specified consumption type
        /// </summary>
        [Code("consumptionType")]
        ConsumptionType,
        /// <summary>
        /// Chosen delivery method- Warehouse or Autostore
        /// </summary>
        [Code("deliveryMethod")]
        DeliveryMethod,
        /// <summary>
        /// Selected month
        /// </summary>
        [Code("selectedMonth")]
        SelectedMonth,
        /// <summary>
        /// Installments
        /// </summary>
        [Code("installments")]
        Installments,
        /// <summary>
        /// Customer's tax identifier
        /// </summary>
        [Code("customerTin")]
        CustomerTin,
        /// <summary>
        /// Uncollected item
        /// </summary>
        [Code("itemUncollected")]
        ItemUncollected,
        /// <summary>
        /// Customer selected language
        /// </summary>
        [Code("selectedLanguage")]
        SelectedLanguage
    }
}