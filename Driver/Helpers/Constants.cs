using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Driver.Helpers
{
    public static class Constants
    {
        public const int max_speed = 100;
        public const double turning_multipler = 0.6;

       

        public const string IP = "192.168.2.20";
        public const int port = 10000; 
      //  public const string IP = "127.0.0.1";
      //  public const int port = 23;
        public const double ViewScale = 5.72;
        public const int AmountofRobots = 11;
        public const int GreenLEDAllRobots = 12;
        public const int GetRobot = 51;
        public const int ReleaseRobots = 52;
        public const int DriveValue = 14;
        public const int DrivePrecent = 15;
        public const int GetCoordiantes = 120;
        public const int AvailableRobotsCommand = 50;
        public static Collection<int> RobotIDs = new Collection<int> {30,31, 32, 33, 34, 35, 36, 37};
        public static int CurrentId;
        public const int Reidentify = 231;
    }
 }
 