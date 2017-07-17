using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTask
{
    public static class ConsolePrinter
    {
        public static void PrintPlane(int[,] plane)
        {
            for (int i = plane.GetLength(0) - 1; i > -1; i--)
            {
                for (int j = 0; j < plane.GetLength(1); j++)
                {
                    Console.Write("{0, 3}", plane[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void PrintResult(List<Wall> walls)
        {
            Console.WriteLine(walls.Count);

            foreach (var wall in walls)
            {
                Console.WriteLine(wall.WallId + 1);
            }
        }

        public static void PrintLine(string data)
        {
            Console.WriteLine(data);
        }
    }
}
