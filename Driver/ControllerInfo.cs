using System.Net.Sockets;

namespace Driver
{
    public class ControllerInfo
    {
        public int controlParam1 { get; }
        public int controlParam2 { get; }
        public double controlParam3 { get; }
      
        public ControllerInfo(int controlParam1, int controlParam2, double? controlParam3 = null)
        {
            this.controlParam1 = controlParam1;
            this.controlParam2 = controlParam2;
            if (controlParam3 != null) this.controlParam3 = (double) controlParam3;
        }
    }
}
