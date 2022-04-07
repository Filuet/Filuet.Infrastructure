using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum PaymentMethod
    {
        [Code("cash")]
        Cash = 0x01,
        // Non cash
        [Code("card")]
        Card,
        [Code("virtualWallet")]
        VirtualWallet, // e.g. GooglePay, ApplePay
        [Code("loyaltyProgram")]
        LoyaltyProgram,
        [Code("e-money")] // Server based methods like paymentGateway
        EMoney
    }
}
