using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using Driver.AutomaticControl;
using Driver.AutomaticControl.TrajectoryGenerator;
using MetroFramework.Drawing.Html;
using Driver.GUI;
using Driver.Helpers;
using Driver.XMLReader;
using Microsoft.Build.Tasks;
using OpenTK.Graphics.OpenGL4;
using Grid = Driver.AutomaticControl.TrajectoryGenerator.Grid;




namespace Driver.GUI.View
{
    public partial class Home : UserControl
    {
       
        public Home()
        {
            InitializeComponent();
          
        }
    }
}