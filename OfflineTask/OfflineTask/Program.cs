using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTask
{
    class Program
    {
        static List<Point> InitializePoints(string[] data)
        {
            List<Point> points = new List<Point>();

            int N = Int32.Parse(data[0]);

            for (int i = 1; i < N + 1; i++)
            {
                string[] temp = data[i].Split(' ');
                points.Add(new Point(Int32.Parse(temp[0]), Int32.Parse(temp[1])));
            }

            return points;
        }

        static List<Wall> InitializeWalls(string[] data, List<Point> points)
        {
            List<Wall> walls = new List<Wall>();

            int N = Int32.Parse(data[0]);
            int W = Int32.Parse(data[N + 1]);

            int wallId = 0;

            for (int i = N + 2; i < N + W + 2; i++)
            {
                string[] temp = data[i].Split(' ');
                walls.Add(new Wall(wallId, points[Int32.Parse(temp[0]) - 1], points[Int32.Parse(temp[1]) - 1]));
                wallId++;
            }

            return walls;
        }

        static void Main(string[] args)
        {
            string[] input = FileHandler.ReadInputData("input.txt");

            List<Point> points = InitializePoints(input);
            List<Wall> walls = InitializeWalls(input, points);

            TaskSolver.Run(points, walls);

            Console.ReadLine();
        }
    }
}
