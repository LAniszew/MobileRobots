using System;
using System.Diagnostics;
using System.Net.Sockets;

namespace Driver.Helpers
{
    public class ServerService
    {
        public static byte[] makeRequest(TcpClient socket, int request, int? optionalFrame1 = null,
            int? optionalFrame2 = null)
        {       
            
            byte[] _request = new byte[1024];
            byte[] response = new byte[1024];
            StackTrace stackTrace = new StackTrace();
            var callerName = stackTrace.GetFrame(1).GetMethod().Name;
            try
            {  
                _request = prepareFrame(request, optionalFrame1, optionalFrame2);
                socket.Client.Send(_request);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }


            switch (callerName)
            {
                case "getCoordiantes":
                    var bytesRead = socket.Client.Receive(response);
                    Array.Resize(ref response, bytesRead);

                    break;
                case "getRobot":
                    socket.Client.Receive(response);
                    foreach (var t in response)
                    {
                        if (t > 0)
                            Debug.WriteLine(t);
                    }
                    break;
                default:
                    socket.Client.Receive(response);
                    break;
            }
          
    
           
            return response;
           
        }

        public byte[] getAvailableRobots(TcpClient socket)
        {
            return makeRequest(socket, Constants.AvailableRobotsCommand);
        }

        public byte[] getAmountofRobots(TcpClient socket)
        {
            return makeRequest(socket, Constants.AmountofRobots);
        }

        public byte[] setGreenLEDAllRobots(TcpClient socket)
        {
            return makeRequest(socket, Constants.GreenLEDAllRobots);
        }

        public byte[] getRobot(TcpClient socket, int robotID)
        {
            return makeRequest(socket, Constants.GetRobot, robotID);
        }

        public byte[] releaseRobots(TcpClient socket)
        {
            return makeRequest(socket, Constants.ReleaseRobots);
        }

        public byte[] drivePercent(TcpClient socket, int leftMotor, int rightMotor)
        {
            return makeRequest(socket, Constants.DrivePrecent, leftMotor, rightMotor);
        }
        
        public byte[] reidentify(TcpClient socket)
        {
            return makeRequest(socket, Constants.Reidentify);
        }

        public byte[] getCoordiantes(TcpClient socket)
        {
            return makeRequest(socket, Constants.GetCoordiantes);
        }

        static byte[] prepareFrame(int request, int? optionalFrame1, int? optionalFrame2)
        {
            byte[] _request;
            byte[] squares = new byte[2];
            squares[0] = Convert.ToByte("91");
            squares[1] = Convert.ToByte("93");


            if (optionalFrame1 == null && optionalFrame2 == null)
            {
                _request = new[]
                {
                    squares[0],
                    Convert.ToByte(request),
                    squares[1]
                };
            }
            else if (optionalFrame2 == null)
            {
                _request = new[]
                {
                    squares[0],
                    Convert.ToByte(request),
                    Convert.ToByte(optionalFrame1),
                    squares[1]
                };
            }
            else if (optionalFrame1 < 0 || optionalFrame2 < 0)
            {
                _request = new[]
                {
                    squares[0],
                    Convert.ToByte(request),
                    unchecked ( (byte) Convert.ToSByte(optionalFrame1)),
                    unchecked ( (byte) Convert.ToSByte(optionalFrame2)),
                    squares[1]
                };
            }
            else
            {
                _request = new[]
                {
                    squares[0],
                    Convert.ToByte(request),
                    Convert.ToByte(optionalFrame1),
                    Convert.ToByte(optionalFrame2),
                    squares[1]
                };
            }
            return _request;
        }
    }
}