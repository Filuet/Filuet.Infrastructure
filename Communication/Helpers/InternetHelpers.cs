﻿using System.Net.NetworkInformation;

namespace Filuet.Infrastructure.Communication.Helpers
{
    public static class InternetHelpers
    {
        public static bool PingHost(this string nameOrAddress) {
            bool pingable = false;
            Ping pinger = null;

            try {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException) {
                // Discard PingExceptions and return false;
            }
            finally {
                if (pinger != null) {
                    pinger.Dispose();
                }
            }

            return pingable;
        }
    }
}