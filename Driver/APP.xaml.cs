using System;
using System.Collections.Generic;
using System.Windows;
using Driver.AutomaticControl.TrajectoryGenerator;
using MetroFramework.Drawing.Html;
using Driver.GUI;
using Driver.GUI.View;


namespace Driver
{
  
    public partial class app : Application
    {
        public void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
       
       }
    }
}
