namespace Filuet.Infrastructure.Communication.Enums
{
    /// <summary>
    /// Port state codes
    /// </summary>
    /// <remarks>Ex PortResultCodes</remarks>
    public enum PortCode : byte
    {
        Success = 0x00,
        PortClosed = 0x01,
        Timeout = 0x02,
        /// <summary>
        /// Checksum error
        /// </summary>
        CrcFailed = 0x03, // TODO: Abolish
        /// <summary>
        /// Typical USB-converter error during removing the device
        /// </summary>
        PortDoesNotExists = 0x04,
        Failure = 0x05
    }
}