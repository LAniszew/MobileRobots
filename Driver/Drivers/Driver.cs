using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Driver.Drivers
{
    public class Driver : IDriver
    {
        private readonly RobotModel robot;
        private readonly IController controller;
        
        public Driver(RobotModel robot, IController controller)
        {
            this.robot = robot;
            this.controller = controller;
        }
        
        public async void Drive()
        {
          
            while (true)
            {
                try
                {
                    
                    var info = controller.process(robot.socket);
                    await robot.Drive(info.controlParam1, info.controlParam2);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw new Exception();
                }
                await Task.Delay(50);
            }
        }


        public async void Connect()
        {
            try
            {
                await robot.connectRobot();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception();
            }
               
        }
    }
}