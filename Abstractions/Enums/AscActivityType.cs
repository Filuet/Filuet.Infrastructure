using System.ComponentModel;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum AscActivityType
    {
        [Description("Login")]
        Login = 0x01,
        [Description("Get cart price")]
        GetPrice,
        [Description("Submit order")]
        Submit,
        [Description("Money income")]
        MoneyIncome,
        [Description("Money change")]
        Change,
        [Description("Extraction")]
        Extraction,
        [Description("Print receipt")]
        PrintReceipt,
        [Description("Logout")]
        Logout,
        [Description("Resubmit order")]
        Resubmit,
        [Description("Sber PlatiQr request")]
        SberPlatiQrRequest,
        [Description("Order consumption type selection")]
        ConsumptionType
    }
}