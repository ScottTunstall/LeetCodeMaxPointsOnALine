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

        private struct Rect
        {
            public int X1;
            public int Y1;
            public int X2;
            public int Y2;

            public bool IsInBounds(in int x, in int y)
            {
                return (x >= X1 && x <= X2 && y >= Y1 && y <= Y2);
            }
        }

        public static int MaxPoints(int[][] points)
        {
            if (points.Length == 0)
                return 0;

            var asList = new List<Point>();

            foreach (var arrItem in points)
            {
                asList.Add(new Point(arrItem[0], arrItem[1]));
            }

            var minX = asList.Min(x => x.X);
            var maxX = asList.Max(x => x.X);
            var minY = asList.Min(x => x.Y);
            var maxY = asList.Max(x => x.Y);
            var rect = new Rect() {X1 = minX, X2 = maxX, Y1 = minY, Y2 = maxY};
        
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
                var key = pt.X + "_" + pt.Y;
                int count = 0;
                int toAdd = _screenMap[key];
                
                // Left
                count = CountPoints(pt, -1, 0, rect) + toAdd;
                best = Math.Max(best, count) ;

                // Above Left
                count = CountPoints(pt, -1, 1,rect) + toAdd;
                best = Math.Max(best, count) ;
                
                // Above
                count = CountPoints(pt, 0,1,rect) + toAdd;
                best = Math.Max(best, count) ;

                // Above Right
                count = CountPoints(pt, 1, 1, rect) + toAdd;
                best = Math.Max(best, count);

                // Right
                count = CountPoints(pt, 1, 0, rect) + toAdd;
                best = Math.Max(best, count);

                // Below Right
                count = CountPoints(pt, 1, -1, rect) + toAdd;
                best = Math.Max(best, count);

                // Below 
                count = CountPoints(pt, 0, -1, rect) + toAdd;
                best = Math.Max(best, count);

                // Below left
                count = CountPoints(pt, -1, -1, rect) + toAdd;
                best = Math.Max(best, count);
            }


            return best;
        }


        private static int CountPoints(Point startPoint, in int deltaX, in int deltaY, Rect rect)
        {
            int x = startPoint.X+deltaX;
            int y = startPoint.Y+deltaY;

            int count = 0;
            while (rect.IsInBounds(x,y))
            {
                if (HasPoint(x,y))
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
