using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Windows;
using Driver.AutomaticControl;
using Driver.Helpers;
 
 namespace Driver
 {
     public class CameraController : IController, Helpers.IObservable<Position>
     {
         private Regulator Regulator { get; set; }
         private readonly ServerService serverService;
         public Queue<Point> path  { get; set; }
         public Position ActualCoordinated { get; set; }
         public double Theta { get; set; }
         private readonly ISet<Helpers.IObserver<Position>> observers;
         
         public CameraController(ServerService serverService, Regulator regulator)
         {
             this.serverService = serverService;
             Regulator = regulator;
             observers = new HashSet<Helpers.IObserver<Position>>();
             path = new Queue<Point>();
             Theta = -1; // Not Set
         }

       
         public ControllerInfo process(TcpClient socket)
         {
              var nextCoordianates = new Tuple<int, int>(0,0);
              Regulator.road = path;
              var actualCoordiantes = Helpers.Helpers.fetchFromFrame(serverService.getCoordiantes(socket));
              nextCoordianates = Regulator.getCoordinates(actualCoordiantes);

              Regulator.Theta = Theta;

              NotifyObservers(actualCoordiantes);

              return new ControllerInfo(nextCoordianates.Item1, nextCoordianates.Item2);
         }

         public void Subscribe(Helpers.IObserver<Position> observer)
         {
             observers.Add(observer);
         }

         public void Unsubscribe(Helpers.IObserver<Position> observer)
         {
             observers.Remove(observer);
         }

         public void NotifyObservers(Position data)
         {
             foreach (var observer in observers)
             {
                 observer.GetNotified(data);
             }
         }
     }
 }