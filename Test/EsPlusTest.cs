using Filuet.Infrastructure.Communication;
using System;
using Xunit;

namespace Test
{
    public class EsPlusTest
    {
        [Fact]
        public void Test_Ping_ES_Plus_Belt()
        {
            // Prepare
            ICommunicationChannel channel = new TcpChannel("172.16.7.103", 5051);
            byte[] command = new byte[] { 2, 48, 48, 129, 67, 67, 190, 247, 47, 3 };

            // Pre-validate
            Assert.NotNull(channel);

            // Perform
            byte[] response = channel.SendCommand(command, 3);

            // Post-validate
            Assert.True(response.Length == 8);
        }


        [Fact]
        public void Test_Ping_ES_Plus_Belt_Extract()
        {
            // Prepare
            ICommunicationChannel channel = new TcpChannel("172.16.7.103", 5051);
            byte[] command = new byte[] { 2, 48, 48, 129, 86, 146, 134, 241, 95, 3 };

            // Pre-validate
            Assert.NotNull(channel);

            // Perform
            byte[] response = channel.SendCommand(command);

            // Post-validate
            Assert.True(response.Length == 8);
        }
    }
}
