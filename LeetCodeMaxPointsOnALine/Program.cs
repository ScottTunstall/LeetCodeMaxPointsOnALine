using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace LeetCodeMaxPointsOnALine
{
    class Program
    {
        private static Dictionary<string, int> _screenMap = new Dictionary<string, int>();

        public static int MaxPoints(int[][] points)
        {
            if (points.Length == 0)
                return 0;

            var asList = new List<Point>();

            foreach (var arrItem in points)
            {
                asList.Add(new Point(arrItem[0], arrItem[1]));
            }


            foreach (var pt in asList)
            {
                var key = pt.X + "_" + pt.Y;
                if (!_screenMap.ContainsKey(key))
                    _screenMap[key] = 1;
                else
                    _screenMap[key] = _screenMap[key] + 1;
            }

            int best = 0;
            foreach (var pt in asList)
            {
                if (pt.X ==1 && pt.Y==4) Debugger.Break();

                var key = pt.X + "_" + pt.Y;
                int count = 0;
                int toAdd = _screenMap[key];
                
                // Left
                count = CountPoints(pt, -1, 0) + toAdd;
                best = Math.Max(best, count) ;

                // Above Left
                count = CountPoints(pt, -1, 1) + toAdd;
                best = Math.Max(best, count) ;
                
                // Above
                count = CountPoints(pt, 0,1) + toAdd;
                best = Math.Max(best, count) ;

                // Above Right
                count = CountPoints(pt, 1, 1) + toAdd;
                best = Math.Max(best, count);

                // Right
                count = CountPoints(pt, 1, 0) + toAdd;
                best = Math.Max(best, count);

                // Below Right
                count = CountPoints(pt, 1, -1) + toAdd;
                best = Math.Max(best, count);

                // Below 
                count = CountPoints(pt, 0, -1) + toAdd;
                best = Math.Max(best, count);

                // Below left
                count = CountPoints(pt, -1, -1) + toAdd;
                best = Math.Max(best, count);
            }


            return best;
        }


        private static int CountPoints(Point startPoint, int deltaX, int deltaY)
        {
            int x = startPoint.X+deltaX;
            int y = startPoint.Y+deltaY;

            int count = 0;
            while (HasPoint(x, y))
            {
                count++;
                x += deltaX;
                y += deltaY;
            }

            return count;
        }



        private static bool HasPoint(int x, int y)
        {
            var key = x + "_" + y;
            if (_screenMap.TryGetValue(key, out int value))
            {
                return value == 1;
            }

            return false;
        }


        static void Main(string[] args)
        {
            //var points = new int[][]
            //{
            //    new[] {1, 1},
            //    new[] {2, 2},
            //    new[] {3,3}
            //};

            //Debug.Assert(MaxPoints(points) == 3);

            var points = new int[][]
            {
                new[] {1, 1},
                new[] {3, 2},
                new[] {5,3},
                new[] {4,1},
                new[] {2,3},
                new[] {1,4},
            };

            //var points = new int[][]
            //{
            //    new[] {0, 0},
            //    new[] {0, 0},
            //};

            var count = MaxPoints(points);

            Console.WriteLine(count);
        }
    }
}
