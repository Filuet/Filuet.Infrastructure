using Filuet.Infrastructure.Communication.Enums;
using System;
using System.IO.Ports;
using System.Threading;

namespace Filuet.Infrastructure.Communication.CommunicationChannels
{
    public class SerialPortChannel : ICommunicationChannel
    {
        private SerialPort _port;

        public SerialPortChannel(ushort serialPortNumber, ushort baudRate, TimeSpan timeout, TimeSpan commandsSendDelay)
        {
            _serialPortNumber = serialPortNumber;
            _baudRate = baudRate;
            _timeout = timeout;
            _commandsSendDelay = commandsSendDelay;

            if (_port == null)
                _port = new SerialPort($"COM{_serialPortNumber}", _baudRate);
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
                var count = 0;
                while (true)
                {
                    var result = Write(data);
                    if (result != PortCode.Success)
                        return null; //throw new ExternalException($"Error in [{Port.Name}] write command [{command.ByteArrayToString()}]");
                    Thread.Sleep(_commandsSendDelay);

                    byte[] buff;
                    var response = Read(out buff);
                    if (response == PortCode.Success)
                        return buff;

                    if (count > (_timeout.TotalMilliseconds / _commandsSendDelay.TotalMilliseconds))
                        return null; //throw new TimeoutException($"Timeout in read answer on command [{command.ByteArrayToString()}]");

                    Thread.Sleep(_commandsSendDelay);
                    count++;
                }
            }
        }

        /// <summary>
        /// Wrap and send command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private PortCode Write(byte[] command)
        {
            try
            {
                if (!_port.IsOpen)
                    _port.Open();
                if (!_port.IsOpen)
                    return PortCode.PortClosed;

                _port.Write(command, 0, command.Length);

                Thread.Sleep(_commandsSendDelay);

                // _log.Info($"[{_port.PortName}] {Encoding.Default.GetString(command)}");
                return PortCode.Success;
            }
            catch (TimeoutException)
            {
                // _log.Errore($"[{_port.PortName}] timeout");
                return PortCode.Timeout;
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentOutOfRangeException || ex is ArgumentException)
            {
                // _log.Error($"[{_port.PortName}] invalid command");
                return PortCode.Failure;
            }
            catch (InvalidOperationException)
            {
                // _log.Error($"[{_port.PortName}] port closed");
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
                _port.Read(data, 0, data.Length);
                buffer = data;
                // _log.info($"[{_port.PortName}] {(buffer?.Length > 0 ? Encoding.Default.GetString(buffer) : string.Empty)}");
                return PortCode.Success;
            }
            catch (TimeoutException)
            {
                // _log.Error($"[{_port.PortName}] timeout");
                buffer = null;
                return PortCode.Timeout;
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentOutOfRangeException || ex is ArgumentException)
            {
                // _log.Error($"[{_port.PortName}] invalid command");
                buffer = null;
                return PortCode.Failure;
            }
            catch (InvalidOperationException)
            {
                // _log.Error($"[{_port.PortName}] port closed");
                buffer = null;
                return PortCode.PortClosed;
            }
        }

        private ushort _serialPortNumber { get; set; }
        private ushort _baudRate { get; set; } = 9600;
        private TimeSpan _timeout { get; set; } = TimeSpan.FromSeconds(2);
        private TimeSpan _commandsSendDelay { get; set; } = TimeSpan.FromSeconds(0.2);
    }
}