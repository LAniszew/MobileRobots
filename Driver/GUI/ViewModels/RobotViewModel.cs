using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Driver.Annotations;
using Driver.Helpers;
using System.Windows;
using Driver.AutomaticControl;
using Driver.GUI.View;

namespace Driver.GUI.ViewModels
{
    public abstract class RobotViewModel : INotifyPropertyChanged
    {
        protected ICommand connect;
        protected ICommand disconnect;
        protected RobotModel robot = new RobotModel(Constants.IP, Constants.port);
        private ServerService ServrService = new ServerService();
        private Regulator Regulator = new Regulator();
        protected CameraController controller;

        public RobotViewModel()
        {
            controller = new CameraController(this.ServrService, this.Regulator);
        }

        protected Drivers.Driver driver { get; set; }



        public Drivers.Driver Driver
        {
            get { return driver = new Drivers.Driver(robot, controller); }
            set
            {
                driver = value;
                OnPropertyChanged("Driver");
            }
        }




        public abstract ICommand Connect { get; }
        public abstract ICommand Disconnect { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}