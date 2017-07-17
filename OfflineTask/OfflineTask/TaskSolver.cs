using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTask
{
    public static class TaskSolver
    {
        private static int GetMaxX(List<Point> points)
        {
            int max = int.MinValue;

            foreach (var point in points)
            {
                if (point.X > max)
                    max = point.X;
            }

            return max;
        }

        private static int GetMaxY(List<Point> points)
        {
            int max = int.MinValue;

            foreach (var point in points)
            {
                if (point.Y > max)
                    max = point.Y;
            }

            return max;
        }

        private static bool IsPointsBelongToWall(List<Wall> walls, Point firstPoint, Point secondPoint)
        {
            //Уравнение прямой
            //(x-x1)*(y2-y1)-(x2-x1)*(y-y1)=0

            foreach (var wall in walls)
            {
                int firstPointCheckExpression = (firstPoint.X - wall.FirstPoint.X) *
                                                (wall.SecondPoint.Y - wall.FirstPoint.Y) -
                                                (wall.SecondPoint.X - wall.FirstPoint.X) *
                                                (firstPoint.Y - wall.FirstPoint.Y);
                int secondPointCheckExpression = (secondPoint.X - wall.FirstPoint.X) *
                                                 (wall.SecondPoint.Y - wall.FirstPoint.Y) -
                                                 (wall.SecondPoint.X - wall.FirstPoint.X) *
                                                 (secondPoint.Y - wall.FirstPoint.Y);

                if (firstPointCheckExpression == 0 && secondPointCheckExpression == 0)
                    if ((firstPoint.X >= Math.Min(wall.FirstPoint.X, wall.SecondPoint.X) &&
                        secondPoint.X <= Math.Max(wall.FirstPoint.X, wall.SecondPoint.X)) &&
                        (firstPoint.Y >= Math.Min(wall.FirstPoint.Y, wall.SecondPoint.Y) &&
                        secondPoint.Y <= Math.Max(wall.FirstPoint.Y, wall.SecondPoint.Y)))
                        return true;
            }

            return false;
        }

        private static Wall SearchWallByPoints(List<Wall> walls, Point firstPoint, Point secondPoint)
        {
            //Уравнение прямой
            //(x-x1)*(y2-y1)-(x2-x1)*(y-y1)=0

            foreach (var wall in walls)
            {
                int firstPointCheckExpression = (firstPoint.X - wall.FirstPoint.X) *
                                                (wall.SecondPoint.Y - wall.FirstPoint.Y) -
                                                (wall.SecondPoint.X - wall.FirstPoint.X) *
                                                (firstPoint.Y - wall.FirstPoint.Y);
                int secondPointCheckExpression = (secondPoint.X - wall.FirstPoint.X) *
                                                 (wall.SecondPoint.Y - wall.FirstPoint.Y) -
                                                 (wall.SecondPoint.X - wall.FirstPoint.X) *
                                                 (secondPoint.Y - wall.FirstPoint.Y);

                if (firstPointCheckExpression == 0 && secondPointCheckExpression == 0)
                {
                    if ((firstPoint.X >= Math.Min(wall.FirstPoint.X, wall.SecondPoint.X) &&
                        secondPoint.X <= Math.Max(wall.FirstPoint.X, wall.SecondPoint.X)) &&
                        (firstPoint.Y >= Math.Min(wall.FirstPoint.Y, wall.SecondPoint.Y) &&
                        secondPoint.Y <= Math.Max(wall.FirstPoint.Y, wall.SecondPoint.Y)))
                        return wall;
                }
            }

            return null;
        }

        private static List<Wall> SearchMostLeftWalls(List<Wall> walls, int[,] plane)
        {
            List<Wall> result = new List<Wall>();

            for (int i = 1; i < plane.GetUpperBound(0); i++)
            {
                for (int j = 1; j < plane.GetUpperBound(1); j++)
                {
                    if (plane[j, i] == (int)Enums.AreaState.dry &&
                        plane[j, i - 1] == (int)Enums.AreaState.flooded)
                    {
                        if (IsPointsBelongToWall(walls, new Point(i, j), new Point(i, j + 1)))
                        {
                            if (!result.Contains(SearchWallByPoints(walls, new Point(i, j), new Point(i, j + 1))))
                                result.Add(SearchWallByPoints(walls, new Point(i, j), new Point(i, j + 1)));
                        }
                    }
                }
            }

            return result;
        }

        private static List<Wall> SearchMostBottomWalls(List<Wall> walls, int[,] plane)
        {
            List<Wall> result = new List<Wall>();

            for (int i = 1; i < plane.GetUpperBound(1); i++)
            {
                for (int j = 1; j < plane.GetUpperBound(0); j++)
                {
                    if (plane[i, j] == (int)Enums.AreaState.dry &&
                        plane[i - 1, j] == (int)Enums.AreaState.flooded)
                    {
                        if (IsPointsBelongToWall(walls, new Point(j, i), new Point(j + 1, i)))
                        {
                            if (!result.Contains(SearchWallByPoints(walls, new Point(j, i), new Point(j + 1, i))))
                                result.Add(SearchWallByPoints(walls, new Point(j, i), new Point(j + 1, i)));
                        }
                    }
                }
            }

            return result;
        }

        private static List<Wall> SearchMostRightWalls(List<Wall> walls, int[,] plane)
        {
            List<Wall> result = new List<Wall>();

            for (int i = plane.GetUpperBound(0); i > 0; i--)
            {
                for (int j = 1; j < plane.GetUpperBound(1); j++)
                {
                    if (plane[j, i - 1] == (int)Enums.AreaState.dry &&
                        plane[j, i] == (int)Enums.AreaState.flooded)
                    {
                        if (IsPointsBelongToWall(walls, new Point(i, j), new Point(i, j + 1)))
                        {
                            if (!result.Contains(SearchWallByPoints(walls, new Point(i, j), new Point(i, j + 1))))
                                result.Add(SearchWallByPoints(walls, new Point(i, j), new Point(i, j + 1)));
                        }
                    }
                }
            }

            return result;
        }

        private static List<Wall> SearchMostUpperWalls(List<Wall> walls, int[,] plane)
        {
            List<Wall> result = new List<Wall>();

            for (int i = plane.GetUpperBound(1); i > 0; i--)
            {
                for (int j = 1; j < plane.GetUpperBound(0); j++)
                {
                    if (plane[i - 1, j] == (int)Enums.AreaState.dry &&
                        plane[i, j] == (int)Enums.AreaState.flooded)
                    {
                        if (IsPointsBelongToWall(walls, new Point(j, i), new Point(j + 1, i)))
                        {
                            if (!result.Contains(SearchWallByPoints(walls, new Point(j, i), new Point(j + 1, i))))
                                result.Add(SearchWallByPoints(walls, new Point(j, i), new Point(j + 1, i)));
                        }
                    }
                }
            }

            return result;
        }

        private static List<Wall> SearchBrokenWalls(List<Wall> walls, int[,] plane)
        {
            List<Wall> result = SearchMostLeftWalls(walls, plane);
            result.AddRange(SearchMostBottomWalls(walls, plane));
            result.AddRange(SearchMostRightWalls(walls, plane));
            result.AddRange(SearchMostUpperWalls(walls, plane));

            return result;
        }

        private static void RemoveBrokenWalls(List<Wall> walls, List<Wall> brokenWalls)
        {
            foreach (var wall in brokenWalls)
                walls.Remove(wall);
        }

        private static void FillPlane(List<Wall> walls, int[,] plane)
        {
            for (int i = 1; i < plane.GetUpperBound(1); i++)
            {

                for (int j = 1; j < plane.GetUpperBound(0); j++)
                {
                    if (plane[i, j] == (int)Enums.AreaState.dry)
                    {
                        if (plane[i, j - 1] == (int)Enums.AreaState.flooded &&
                            !IsPointsBelongToWall(walls, new Point(j, i), new Point(j, i + 1)))
                            plane[i, j] = (int)Enums.AreaState.flooded;
                        else if (plane[i - 1, j] == (int)Enums.AreaState.flooded &&
                                 !IsPointsBelongToWall(walls, new Point(j, i), new Point(j + 1, i)))
                            plane[i, j] = (int)Enums.AreaState.flooded;
                        else if (plane[i, j + 1] == (int)Enums.AreaState.flooded &&
                                 !IsPointsBelongToWall(walls, new Point(j + 1, i), new Point(j + 1, i + 1)))
                            plane[i, j] = (int)Enums.AreaState.flooded;
                        else if (plane[i + 1, j] == (int)Enums.AreaState.flooded &&
                                 !IsPointsBelongToWall(walls, new Point(j, i + 1), new Point(j + 1, i + 1)))
                            plane[i, j] = (int)Enums.AreaState.flooded;
                    }
                    if (plane[i, j] == (int)Enums.AreaState.flooded)
                    {
                        if (plane[i, j - 1] == (int)Enums.AreaState.dry &&
                            !IsPointsBelongToWall(walls, new Point(j, i), new Point(j, i + 1)))
                            plane[i, j - 1] = (int)Enums.AreaState.flooded;
                        else if (plane[i - 1, j] == (int)Enums.AreaState.dry &&
                                 !IsPointsBelongToWall(walls, new Point(j, i), new Point(j + 1, i)))
                            plane[i - 1, j] = (int)Enums.AreaState.flooded;
                        else if (plane[i, j + 1] == (int)Enums.AreaState.dry &&
                                 !IsPointsBelongToWall(walls, new Point(j + 1, i), new Point(j + 1, i + 1)))
                            plane[i, j + 1] = (int)Enums.AreaState.flooded;
                        else if (plane[i + 1, j] == (int)Enums.AreaState.dry &&
                                 !IsPointsBelongToWall(walls, new Point(j, i + 1), new Point(j + 1, i + 1)))
                            plane[i + 1, j] = (int)Enums.AreaState.flooded;
                    }
                }
            }

            for (int i = plane.GetUpperBound(1) - 1; i > 0; i--)
            {
                for (int j = plane.GetUpperBound(0) - 1; j > 0; j--)
                {
                    if (plane[i, j] == (int)Enums.AreaState.dry)
                    {
                        if (plane[i, j - 1] == (int)Enums.AreaState.flooded &&
                            !IsPointsBelongToWall(walls, new Point(j, i), new Point(j, i + 1)))
                            plane[i, j] = (int)Enums.AreaState.flooded;
                        else if (plane[i - 1, j] == (int)Enums.AreaState.flooded &&
                                 !IsPointsBelongToWall(walls, new Point(j, i), new Point(j + 1, i)))
                            plane[i, j] = (int)Enums.AreaState.flooded;
                        else if (plane[i, j + 1] == (int)Enums.AreaState.flooded &&
                                 !IsPointsBelongToWall(walls, new Point(j + 1, i), new Point(j + 1, i + 1)))
                            plane[i, j] = (int)Enums.AreaState.flooded;
                        else if (plane[i + 1, j] == (int)Enums.AreaState.flooded &&
                                 !IsPointsBelongToWall(walls, new Point(j, i + 1), new Point(j + 1, i + 1)))
                            plane[i, j] = (int)Enums.AreaState.flooded;
                    }
                    if (plane[i, j] == (int)Enums.AreaState.flooded)
                    {
                        if (plane[i, j - 1] == (int)Enums.AreaState.dry &&
                            !IsPointsBelongToWall(walls, new Point(j, i), new Point(j, i + 1)))
                            plane[i, j - 1] = (int)Enums.AreaState.flooded;
                        else if (plane[i - 1, j] == (int)Enums.AreaState.dry &&
                                 !IsPointsBelongToWall(walls, new Point(j, i), new Point(j + 1, i)))
                            plane[i - 1, j] = (int)Enums.AreaState.flooded;
                        else if (plane[i, j + 1] == (int)Enums.AreaState.dry &&
                                 !IsPointsBelongToWall(walls, new Point(j + 1, i), new Point(j + 1, i + 1)))
                            plane[i, j + 1] = (int)Enums.AreaState.flooded;
                        else if (plane[i + 1, j] == (int)Enums.AreaState.dry &&
                                 !IsPointsBelongToWall(walls, new Point(j, i + 1), new Point(j + 1, i + 1)))
                            plane[i + 1, j] = (int)Enums.AreaState.flooded;
                    }
                }
            }
        }

        private static bool IsHavingDryArea(int[,] plane)
        {
            for (int i = 1; i < plane.GetUpperBound(1); i++)
            {
                for (int j = 1; j < plane.GetUpperBound(0); j++)
                {
                    if (plane[i, j] == (int)Enums.AreaState.dry)
                        return true;
                }
            }

            return false;
        }

        private static int[,] InitializePlane(List<Point> points, List<Wall> walls)
        {
            int maxX = GetMaxX(points);
            int maxY = GetMaxY(points);

            int[,] plane = new int[maxX + 1, maxY + 1];

            for (int i = 0; i < maxX + 1; i++)
            {
                plane[0, i] = (int)Enums.AreaState.flooded;
                plane[maxY, i] = (int)Enums.AreaState.flooded;
            }

            for (int i = 0; i < maxY + 1; i++)
            {
                plane[i, 0] = (int)Enums.AreaState.flooded;
                plane[i, maxX] = (int)Enums.AreaState.flooded;
            }

            FillPlane(walls, plane);

            return plane;
        }

        public static void Run(List<Point> points, List<Wall> walls)
        {
            int[,] plane = InitializePlane(points, walls);

            while (IsHavingDryArea(plane))
            {
                List<Wall> brokenWalls = SearchBrokenWalls(walls, plane);

                RemoveBrokenWalls(walls, brokenWalls);

                FillPlane(walls, plane);
            }

            ConsolePrinter.PrintResult(walls);
        }
    }
}
