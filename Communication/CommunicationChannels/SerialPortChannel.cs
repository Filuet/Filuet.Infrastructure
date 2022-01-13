using Filuet.Infrastructure.Abstractions.Helpers;
using Filuet.Infrastructure.Communication.Enums;
using System;
using System.IO.Ports;
using System.Threading;

namespace Filuet.Infrastructure.Communication.CommunicationChannels
{
    public class SerialPortChannel : ICommunicationChannel
    {
        public SerialPortChannel(ushort serialPortNumber, ushort baudRate)
        {
            _serialPortNumber = serialPortNumber;
            _baudRate = baudRate;

            if (_port == null)
                _port = new SerialPort($"COM{_serialPortNumber}", _baudRate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">command</param>
        /// <returns></returns>
        public byte[] SendCommand(byte[] data, TimeSpan readDelay, TimeSpan timeout, byte? endOfResponse = null)
        {
            lock (_port)
            {
                var result = Write(data, readDelay);
                if (result != PortCode.Success)
                    return null; //throw new ExternalException($"Error in [{Port.Name}] write command [{command.ByteArrayToString()}]");

                byte[] buff;
                var response = Read(out buff, endOfResponse, timeout);
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
        private PortCode Write(byte[] command, TimeSpan readDelay)
        {
            try
            {
                if (!_port.IsOpen)
                    _port.Open();
                if (!_port.IsOpen)
                    return PortCode.PortClosed;

                _port.Write(command, 0, command.Length);

                _readDelay = readDelay;
                _readDelay = _readDelay.Milliseconds == 0 ? TimeSpan.FromMilliseconds(200) : _readDelay;
                Thread.Sleep(_readDelay);

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
        private PortCode Read(out byte[] buffer, byte? endOfResponse, TimeSpan timeout)
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
                TaskHelpers.ExecuteWithTimeLimit(timeout, () =>
                {
                    TimeSpan newReadDelay = TimeSpan.FromMilliseconds(_readDelay.Milliseconds);
                    while (data.Length == 0)
                    {
                        _port.Read(data, 0, data.Length);

                        if (data.Length >= 0)
                            break;

                        newReadDelay += TimeSpan.FromMilliseconds(newReadDelay.Milliseconds * 2); // Slow down the poll as prescripted
                        Thread.Sleep(newReadDelay);
                    }
                });

                buffer = data;

                if (endOfResponse.HasValue && buffer.Length > 0 && endOfResponse == buffer[buffer.Length - 1])
                    return PortCode.Success;
                else return PortCode.Failure;
                // _log.info($"[{_port.PortName}] {(buffer?.Length > 0 ? Encoding.Default.GetString(buffer) : string.Empty)}");
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

        private SerialPort _port;
        private ushort _serialPortNumber { get; set; }
        private ushort _baudRate { get; set; } = 9600;
        private TimeSpan _readDelay;
    }
}