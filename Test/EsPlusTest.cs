using Filuet.Infrastructure.Communication;
using Filuet.Infrastructure.Ordering.Models;
using System;
using System.Net;
using Xunit;

namespace Test
{
    public class EsPlusTest
    {
        [Fact]
        public void Test_Ping_ES_Plus_Belt()
        {
            // Prepare
            ICommunicationChannel channel = new TcpChannel(x => { x.Endpoint = new System.Net.IPEndPoint(IPAddress.Parse("172.16.7.103"), 5051); });
            byte[] command = new byte[] { 2, 48, 48, 129, 67, 67, 190, 247, 47, 3 };

            // Pre-validate
            Assert.NotNull(channel);

            // Perform
            byte[] response = channel.SendCommand(command);

            // Post-validate
            Assert.True(response.Length == 8);
        }

        [Fact]
        public void Test_Ping_ES_Plus_Belt_New()
        {
            // Prepare
            ICommunicationChannel channel = new TcpChannel(x => { x.Endpoint = new System.Net.IPEndPoint(IPAddress.Parse("172.16.7.103"), 5050); });
            byte[] command = new byte[] { 2, 48, 48, 129, 67, 67, 190, 247, 47, 3 };

            // Pre-validate
            Assert.NotNull(channel);

            // Perform
            byte[] response = channel.SendCommand(command);
            byte[] response1 = channel.SendCommand(command);
            byte[] response2 = channel.SendCommand(command);
            byte[] response3 = channel.SendCommand(command);
            byte[] response4 = channel.SendCommand(command);

            // Post-validate
            Assert.True(response.Length == 8);
        }


        [Fact]
        public void Test_Ping_ES_Plus_Belt_Extract()
        {
            string d = "{\"Number\":\"6BK1001235\",\"Customer\":\"7918180560\",\"CustomerName\":\"FOO BAR\",\"CountryCode\":\"RU\",\"LanguageCode\":\"ru\",\"Date\":\"2021-12-21T16:44:13.7280901+03:00\",\"Points\":25,\"ExtraData\":{\"Tin\":\"5343240441\",\"Kiosk\":\"RUMSKBUTAS1\",\"SelectedMonth\":\"02 2021\"},\"Obtaining\":\"AS\",\"Amount\":{\"Value\":100,\"Currency\":\"RUB\"},\"Paid\":{\"Value\":100,\"Currency\":\"RUB\"},\"Items\":[{\"ProductUID\":\"0006\",\"Name\":\"Aloe\",\"Quantity\":2,\"Amount\":{\"Value\":50,\"Currency\":\"RUB\"},\"TotalAmount\":{\"Value\":100,\"Currency\":\"RUB\"},\"Points\":25}],\"UncollectedItems\":[{\"ProductUID\":\"0006\",\"Quantity\":1}]}";
            var t =Order.Deserialize(d);

            // Prepare
            ICommunicationChannel channel = new TcpChannel(x => { x.Endpoint = new System.Net.IPEndPoint(IPAddress.Parse("172.16.7.103"), 5051); });
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
