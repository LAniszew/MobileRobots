using System.Windows.Input;

namespace Driver.GUI.ViewModels
{
    public class ManualViewModel : RobotViewModel
    {
        public ICommand driveCommand;

        public override ICommand Connect
        {
            get
            {
                if (connect == null)
                {
                    connect = new RelayCommand(obj =>
                    {
                        robot.connectAsync();
                        Driver.Connect();
                    });
                }
                return connect;
            }
        }
        public override ICommand Disconnect
        {
            get
            {
                if (disconnect == null)
                {
                    disconnect = new RelayCommand(obj =>
                    {
                        robot.disconnect();
                    });
                }
                return disconnect;
            }
        }
        public ICommand DriveCommand
        {
            get
            {
                if (driveCommand == null)
                {
                    driveCommand = new RelayCommand(obj =>
                    { 
                        Driver.Drive();
                    });
                }
                return driveCommand;
            }
        }
    }
}