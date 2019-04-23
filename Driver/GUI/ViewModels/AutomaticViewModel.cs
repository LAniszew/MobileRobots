using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Driver.AutomaticControl.TrajectoryGenerator;
using Driver.Helpers;
using Grid = Driver.AutomaticControl.TrajectoryGenerator.Grid;

namespace Driver.GUI.ViewModels
{
    public class AutomaticViewModel : RobotViewModel, Helpers.IObserver<Position>
    {
        #region Commands
        private ICommand driveCommand;
        private ICommand xmlLoadCommand;
        private ICommand reidentifyCommand;
        private ICommand loadTask1Command;
        private ICommand addPointCommand;
        #endregion

        #region Fields
        private int selectedRobot;
        private string currentXText;
        private string currentYText;
        private string currentThetaText;
        private string addXInput;
        private string addYInput;
        private string addThetaInput;
        private Ellipse robotPointer;
        #endregion

        #region Properties
        public Canvas Workspace { get; set; }
        public Collection<int> Robots { get; }

        public int SelectedRobot
        {
            get { return selectedRobot; }
            set
            {
                selectedRobot = value;
                Constants.CurrentId = value;
                OnPropertyChanged("SelectedRobot");
                Debug.WriteLine(SelectedRobot);
            }
        }

        public string CurrentXText
        {
            get { return currentXText;}
            set
            {
                currentXText = value;
                OnPropertyChanged("CurrentXText");
            }
        }
        public string CurrentYText
        {
            get { return currentYText; }
            set
            {
                currentYText = value;
                OnPropertyChanged("CurrentYText");
            }
        }
        
        public string CurrentThetaText
        {
            get { return currentThetaText; }
            set
            {
                currentThetaText = value;
                OnPropertyChanged("CurrentThetaText");
            }
        }

        public string AddXInput
        {
            get { return addXInput; }
            set
            {
                addXInput = value;
                OnPropertyChanged("AddXInput");
            }
        }
        public string AddYInput
        {
            get { return addYInput; }
            set
            {
                addYInput = value;
                OnPropertyChanged("AddYInput");
            }
        }
        
        public string AddThetaInput
        {
            get { return addThetaInput; }
            set
            {
                addThetaInput = value;
                OnPropertyChanged("AddThetaInput");
            }
        }
        #endregion

        public AutomaticViewModel(Canvas workspace)
        {
            Workspace = workspace;
            Robots = Helpers.Constants.RobotIDs;
            SelectedRobot = Robots[0];
            controller.Subscribe(this);
            robotPointer = new Ellipse()
            {
                Height = 17,
                Width = 17,
                Fill = new SolidColorBrush(Color.FromRgb(0, 0, 255))
            };
            Workspace.Children.Add(robotPointer);
        }


        public ICommand ReidentifyCommand
        {
            get
            {
                if (reidentifyCommand == null)
                {
                    reidentifyCommand = new RelayCommand(obj =>
                    {
                        robot.reidentify();


                    });
                }
                return reidentifyCommand;
            }
        }
        public ICommand AddPointCommand
        {
            get
            {
                if (addPointCommand == null)
                {
                    addPointCommand = new RelayCommand(obj =>
                    {
                        if (string.IsNullOrWhiteSpace(addXInput) || string.IsNullOrWhiteSpace(addYInput))
                            return;

                        var point = new Point()
                        {
                            X = double.Parse(addXInput),
                            Y = double.Parse(addYInput)
                        };

                        controller.Theta = double.Parse(addThetaInput);
                        controller.path.Enqueue(point);
                         
                        var pointToDraw = new Ellipse()
                        {
                            Height = 12,
                            Width = 12,
                            Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0))
                        };
                        Workspace.Children.Add(pointToDraw);
                        pointToDraw.SetValue(Canvas.LeftProperty, point.X/Constants.ViewScale);
                        pointToDraw.SetValue(Canvas.TopProperty, point.Y/Constants.ViewScale);
                    });
                    
                }
                return addPointCommand;
            }
        }

        public ICommand XmlLoadCommand
        {
            get
            {
                if (xmlLoadCommand == null)
                {
                    xmlLoadCommand = new RelayCommand(obj =>
                    {

                        OpenFileDialog choofdlog = new OpenFileDialog();
                        choofdlog.Filter = "All Files (*.*)|*.*";
                        choofdlog.FilterIndex = 1;
                        choofdlog.Multiselect = false;

                        string sFileName = "";
                        if (choofdlog.ShowDialog() == DialogResult.OK)
                        {
                            sFileName = choofdlog.FileName;
                        }



                        if (sFileName.EndsWith("xml"))
                        {
                            int width = 2000;
                            int height = 2000;
                            
                            if(!Workspace.Children.Contains(robotPointer))
                            {
                                Workspace.Children.Add(robotPointer);
                            }
                            var sizes = new float[2000, 2000];
                            for (int i = 0; i < 2000; i++)
                            for (int j = 0; j < 2000; j++)
                                sizes[i, j] = 1.0f;
                            var xmlReader = new XMLReader.XMLReader();
                            var taskResources = xmlReader.readXml(sFileName);
                            var taskResourcesList = taskResources._Obstacles._Obstacle
                                .Select(obstaclee => obstaclee._Point).ToList();
                            var tempList1 = new List<List<Point>>();
                            var tempList2 = new List<Point>();
                            foreach (var obstacle in taskResourcesList)
                            {

                                foreach (var point in obstacle)
                                {
                                    tempList2.Add(new Point(Convert.ToDouble(point._X), Convert.ToDouble(point._Y)));
                                }
                                tempList1.Add(new List<Point>(tempList2));
                                tempList2.Clear();
                            }

                            var resultList = new List<Tuple<Point, Point>>();
                            tempList1.ForEach(list =>
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    Tuple<Point, Point> obstacleCorners;
                                    if (i + 1 == list.Count)
                                    {
                                        obstacleCorners = Tuple.Create(list.ElementAt(i), list.ElementAt(0));
                                    }
                                    else
                                    {
                                        obstacleCorners = Tuple.Create(list.ElementAt(i),
                                            list.ElementAtOrDefault(i + 1));
                                    }
                                    resultList.Add(obstacleCorners);
                                }
                            });
                            var basicGrid = new Grid(width, height, sizes);

                            Point start = new Point(Convert.ToDouble(taskResources._Start._X),
                                Convert.ToDouble(taskResources._Start._Y));
                            Point target = new Point(Convert.ToDouble(taskResources._Destination._X),
                                Convert.ToDouble(taskResources._Destination._Y));


                            // Find path, this will be a list of Point's or empty list.
                            // Last argument will make this search be 8 directional
                            List<Point> obstaclePath = new List<Point>();
                            foreach (var pointspair in resultList)
                            {
                                var pathHelper = PathFinding.findPath(basicGrid, pointspair.Item1, pointspair.Item2,
                                    false);
                                obstaclePath.AddRange(pathHelper);
                            }

                            var obstacles = new Obstacle(obstaclePath);

                            var grid = new Grid(width, height, obstacles.toMatrix());
                            var path = PathFinding.findPath(grid, start, target, true);
                            var viewPoints = new PointCollection();
                            var polyline = new Polyline();

                            foreach (var point in path)
                            {
                                viewPoints.Add(new Point(point.X/Constants.ViewScale, point.Y/Constants.ViewScale));
                            }

                            PointCollection points = viewPoints;

                            polyline.Points = points;
                            polyline.Stroke = new SolidColorBrush(Colors.Green);
                            polyline.StrokeThickness = 4;

                            foreach (var obstacleee in tempList1)
                            {
                                Polyline innerLine = new Polyline();
                                Polyline obstaclePoints = new Polyline();
                                obstacleee.Add(obstacleee.First());
                                var obstaleViewPoints = new PointCollection();
                                foreach (var point in obstacleee)
                                {
                                    obstaleViewPoints.Add(new Point(point.X / Constants.ViewScale, point.Y / Constants.ViewScale));
                                }
                                PointCollection przeszkodapoints3 = obstaleViewPoints;
                                innerLine.Points = przeszkodapoints3;
                                innerLine.Stroke = new SolidColorBrush(Colors.Yellow);
                                innerLine.StrokeThickness = 1;
                                obstaclePoints.Points = przeszkodapoints3;
                                obstaclePoints.Stroke = new SolidColorBrush(Colors.Blue);
                                obstaclePoints.StrokeThickness = 2;
                                Workspace.Children.Add(obstaclePoints);
                                Workspace.Children.Add(innerLine);
                            }
                              Workspace.Children.Add(polyline);
                              controller.path = new Queue<Point>();
                              var x = 0;
                              foreach (var point in path)
                              {
                                  if(x % 150 == 0) {
                                  controller.path.Enqueue(point);
                                  }
                                  x++;
                              }
                        }

                    });
                }
                return xmlLoadCommand;
            }
        }

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
                        robot.release();
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

        public ICommand LoadTask1Command
        {
            get
            {
                if (loadTask1Command == null)
                {
                    loadTask1Command = new RelayCommand(obj =>
                    {
                        OpenFileDialog choofdlog = new OpenFileDialog();
                        choofdlog.Filter = "All Files (*.*)|*.*";
                        choofdlog.FilterIndex = 1;
                        choofdlog.Multiselect = false;

                        string sFileName = "";
                        if (choofdlog.ShowDialog() == DialogResult.OK)
                        {
                            sFileName = choofdlog.FileName;
                        }



                        if (sFileName.EndsWith("xml"))
                        {
                            int width = 2000;
                            int height = 2000;
                            if (!Workspace.Children.Contains(robotPointer))
                            {
                                Workspace.Children.Add(robotPointer);
                            }

                            var sizes = new float[2000, 2000];
                            for (int i = 0; i < 2000; i++)
                            for (int j = 0; j < 2000; j++)
                                sizes[i, j] = 1.0f;

                            var xmlReader = new XMLReader.XMLReader();
                            var taskResources = xmlReader.readTask1(sFileName);
                            controller.path = new Queue<Point>();

                            var test =  taskResources.TargetPosition[0].Y;

                            Debug.WriteLine(test);
                            foreach (var point in taskResources.TargetPosition)
                            {
                                var x = double.Parse(point.X, CultureInfo.InvariantCulture);
                                var y = double.Parse(point.Y, CultureInfo.InvariantCulture);
                                var targetPointer = new Ellipse()
                                {
                                    Height = 10,
                                    Width = 10,
                                    Fill = new SolidColorBrush(Color.FromRgb(200,150,100))
                                };
                                Workspace.Children.Add(targetPointer);
                                targetPointer.SetValue(Canvas.LeftProperty, x/Constants.ViewScale);
                                targetPointer.SetValue(Canvas.TopProperty, y/Constants.ViewScale);
                                controller.path.Enqueue(new Point(x, y));

                            }
                            controller.Theta = double.Parse(taskResources.TargetPosition.Last().ThetaDeg, CultureInfo.InvariantCulture);
                        }

                    });
                }
                return loadTask1Command;
            }
        }

        public void GetNotified(Position data)
        {
            
            robotPointer.SetValue(Canvas.LeftProperty, data.X/Constants.ViewScale);
            robotPointer.SetValue(Canvas.TopProperty, data.Y/Constants.ViewScale);
            CurrentXText = Convert.ToString(data.X, CultureInfo.InvariantCulture);
            CurrentYText = Convert.ToString(data.Y, CultureInfo.InvariantCulture);
            CurrentThetaText =  Convert.ToString(data.Theta, CultureInfo.InvariantCulture);
            Debug.WriteLine(data.X + "   " + data.Y);
        }
    }
}