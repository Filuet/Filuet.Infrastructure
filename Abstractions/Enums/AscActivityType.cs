﻿using System.ComponentModel;

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
        Extract,
        [Description("Product extract")]
        ProductExtract,
        [Description("Print receipt")]
        PrintReceipt,
        [Description("Logout")]
        Logout,
        [Description("Resubmit order")]
        Resubmit
    }
}