using System;
using System.Diagnostics;


namespace Driver.Helpers
{
    public static class Helpers
    {
        public static Position fetchFromFrame(byte[] frame)
        {
            var position = new Position
            {
                X = 0,
                Y = 0,
                Theta = 0
            };

            if (frame.Length < 27)
            {
                return position;
            }
            var foundRobot = false;
            for (var i = 0; i < frame.Length; i++)
            {
              
           
                if (i + 24 >= frame.Length) break;
                if (frame[i] == Constants.CurrentId && !foundRobot)
                {
                    foundRobot = true;
                 
                 
                    var xbytes = new byte[8];
                    var ybytes = new byte[8];
                    var thetebytes = new byte[8];

                    Array.Copy(frame, i + 1, xbytes, 0, 8);
                    Array.Copy(frame, i + 9, ybytes, 0, 8);
                    Array.Copy(frame, i + 17, thetebytes, 0, 8);
                  
                    position.X = BitConverter.ToDouble(xbytes, 0);
                    position.Y = BitConverter.ToDouble(ybytes, 0);
                    position.Theta = BitConverter.ToDouble(thetebytes, 0);
                    position.Theta *= 180 / Math.PI;
                }
            }
            return position;
        }
    }
}



/*
byte[] odp;
lock (this.serverSocket)
{
byte[] Pozycja = new byte[] { Convert.ToByte('['), 120, Convert.ToByte(']') };
    this.serverSocket.Send(Pozycja);
odp = new byte[this.serverSocket.ReceiveBufferSize];
this.serverSocket.Receive(odp);
}

int frameLength = (int)BitConverter.ToUInt64(odp, 1);
int liczba = (frameLength - 10) / 25;

for (int i = 9; i < frameLength - 1; i+=25)
{


RobotLocation tmp = new RobotLocation();
tmp.ID = Convert.ToInt32(odp[i]);
tmp.X = BitConverter.ToDouble(odp, i+1);
tmp.Y = BitConverter.ToDouble(odp, i+9);
tmp.T = BitConverter.ToDouble(odp, i+17) * 180 / Math.PI;

result.Add(tmp);
}*/