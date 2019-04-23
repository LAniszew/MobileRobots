using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using Driver.Helpers;



namespace Driver.AutomaticControl
{
    public class Regulator
    {
        public Queue<Point> road { get; set; }
        public Tuple<int, int> position { get; set; }
        private Point currentTarget;
        private static readonly int TOLERANCE = 15;
        private bool angleCalibrated = false;
        public double Theta { get; set; }


        public Tuple<int, int> getCoordinates(Position position)
        {
            if (road.Count <= 0)
            {
                Debug.WriteLine("Theta" + Theta);
                if (Theta != -1 && Math.Abs(position.Theta - Theta) > 10)
                {
                    return new Tuple<int, int>(-30, 30);
                   
                }
                return new Tuple<int, int>(0, 0);
            }
            currentTarget = road.Peek();
            Debug.WriteLine(position.X + " " + position.Y);
            var angle = Math.Atan2(currentTarget.Y - position.Y, currentTarget.X - position.X) * 180 / Math.PI;
            
            if (angle < 0)
            {
                angle = angle + 360;
            }
          
            angle -= position.Theta;
           
           
            if (getDifference(currentTarget.X, position.X) < 40 && getDifference(currentTarget.Y, position.Y) < 40 || currentTarget == null)
            {
                Debug.WriteLine($"position:, {position.X}, {position.Y} ACHIVED!!!!");
                currentTarget = road.Dequeue();
            }


            if (angle <= 20 && angle>= -20)
            {
                return new Tuple<int, int>(80, 80);
               
            }



            if ((angle < -20 && angle > -180) || (angle >180 && angle < 360))
            {
                if (getDifference(currentTarget.X, position.X) < 200  && getDifference(currentTarget.X, position.X) > 30 || position.X > 1800 || position.X < 200  || position.Y > 1800 || position.Y < 200) 
                {
                    Debug.WriteLine("prawo");
                    return new Tuple<int, int>(-50,50);
                }
               
                return new Tuple<int, int>(50,80);
            }
             if((angle > 20 && angle < 180) || (angle < -180 && angle > -360))
            {
                if (getDifference(currentTarget.X, position.X) < 200 && getDifference(currentTarget.X, position.X) > 30 || position.Y > 1800 || position.Y < 200 || position.X > 1800 || position.X < 200 )
                {
                    Debug.WriteLine("lewo");
                    return new Tuple<int, int>(50,-50);
                }
        
                return new Tuple<int, int>(80,50);
            }

            return new Tuple<int, int>(0, 0);
        }

        private double getDifference(double next, double current)
        {
            return Math.Abs(next - current);
        }
    }
}