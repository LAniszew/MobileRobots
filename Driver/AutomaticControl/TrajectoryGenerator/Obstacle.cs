using System.Collections.Generic;
using System.Windows;


namespace Driver.AutomaticControl.TrajectoryGenerator
{
    public class Obstacle
    {
        private readonly List<Point> obstaclePoints;

        public Obstacle(List<Point> obstacle)
        {
            obstaclePoints = obstacle;
        }

        public float[,] toMatrix()
        {
            var result = new float[2000, 2000];
            for(int i=0; i<2000; i++)
            for (int j = 0; j < 2000; j++)
                result[i, j] = 1.0f;
            foreach (var obstacle in obstaclePoints)
            {
                result[(int)obstacle.X, (int)obstacle.Y] = 0.0f;
            }
            return result;
        }
    }
}

