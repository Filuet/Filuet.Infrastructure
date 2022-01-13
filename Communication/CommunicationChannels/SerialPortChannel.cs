using Filuet.Infrastructure.Abstractions.Helpers;
using Filuet.Infrastructure.Communication.Enums;
using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace Filuet.Infrastructure.Communication.CommunicationChannels
{
    public class SerialPortChannel : ICommunicationChannel
    {
        public SerialPortChannel(Action<SerialPortChannelSettings> channelSetup)
        {
            _settings = channelSetup.CreateTargetAndInvoke();

            if (_port == null)
                _port = new SerialPort($"COM{_settings.SerialPortNumber}", _settings.BaudRate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">command</param>
        /// <returns></returns>
        public byte[] SendCommand(byte[] data)
        {
            lock (_port)
            {
                var result = Write(data);
                if (result != PortCode.Success)
                    return null; //throw new ExternalException($"Error in [{Port.Name}] write command [{command.ByteArrayToString()}]");

                Thread.Sleep(_settings.ReadDelay);

                byte[] buff;
                var response = Read(out buff);
                if (response == PortCode.Success)
                    return buff;
                else
                    return null; //throw new TimeoutException($"Timeout in read answer on command [{command.ByteArrayToString()}]");
            }
        }

        /// <summary>
        /// Wrap and send command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public PortCode Write(byte[] command)
        {
            try
            {
                if (!_port.IsOpen)
                    _port.Open();
                if (!_port.IsOpen)
                    return PortCode.PortClosed;

                _port.Write(command, 0, command.Length);

                return PortCode.Success;
            }
            catch (TimeoutException)
            {
                return PortCode.Timeout;
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentOutOfRangeException || ex is ArgumentException)
            {
                return PortCode.Failure;
            }
            catch (InvalidOperationException)
            {
                return PortCode.PortClosed;
            }
        }

        /// <summary>
        /// Read data from port
        /// </summary>
        /// <returns></returns>
        private PortCode Read(out byte[] buffer)
        {
            if (!_port.IsOpen)
                _port.Open();

            if (!_port.IsOpen)
            {
                buffer = null;
                return PortCode.PortClosed;
            }

            byte[] data = new byte[_port.BytesToRead];

            try
            {
                _port.ReadTimeout = (int)_settings.ReceiveTimeout.TotalMilliseconds;
                int readBytes = _port.Read(data, 0, data.Length);

                buffer = data.Take(readBytes).ToArray();

                return buffer.Length > 0 ? PortCode.Success : PortCode.Failure;
            }
            catch (TimeoutException)
            {
                buffer = null;
                return PortCode.Timeout;
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentOutOfRangeException || ex is ArgumentException)
            {
                buffer = null;
                return PortCode.Failure;
            }
            catch (InvalidOperationException)
            {
                buffer = null;
                return PortCode.PortClosed;
            }
        }

        private SerialPort _port;
        private readonly SerialPortChannelSettings _settings;
    }
}