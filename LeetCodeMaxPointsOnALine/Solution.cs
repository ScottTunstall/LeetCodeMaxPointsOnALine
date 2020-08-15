using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LeetCodeMaxPointsOnALine
{
    public class Solution
    {
        public Dictionary<Point,int> _duplicates = new Dictionary<Point, int>();
        public Dictionary<string, int> _slopeCountDictionary = new Dictionary<string, int>();

        public int MaxPoints(int[][] points)
        {
            if (points.Length <2)
                return points.Length;
                
            _slopeCountDictionary.Clear();
            var asList = new List<Point>();

            foreach (var arrItem in points)
            {
                asList.Add(new Point(arrItem[0], arrItem[1]));
            }

            var firstPoint = asList[0];
            if (asList.All(x => x == firstPoint))
                return asList.Count;

            // first strip out duplicate points, remembering how many there are for each point
            for (int i = 0; i < asList.Count; i++)
            {
                var pt = asList[i];
                for (int j = i+1; j < asList.Count; j++)
                {
                    if (pt == asList[j])
                    {
                        _duplicates[pt] = _duplicates.ContainsKey(pt) ? _duplicates[pt] +1 : 1;
                        asList.RemoveAt(j);
                    }
                }
            }


            for (int i = 0; i < asList.Count - 1; i++)
            {
                var startPoint = asList[i];
                var countOfDuplicates = _duplicates.ContainsKey(startPoint) ? _duplicates[startPoint] : 0;

                for (int j = 1; j < asList.Count; j++)
                {
                    var endPoint = asList[j];
                    if (startPoint == endPoint)
                        continue;

                    var slope = CalculateSlope(startPoint, endPoint);

                    string key = i + "_" + slope.ToString();
                    IncrementValueForKey(key, countOfDuplicates); // pointsSameAsOrigin+1);
                }
            }

            return _slopeCountDictionary.Max(x => x.Value);
        }

        private void IncrementValueForKey(in string key, int defaultValue)
        {
            if (!_slopeCountDictionary.ContainsKey(key))
            {
                _slopeCountDictionary[key] = defaultValue + 2;
            }
            else
            {
                _slopeCountDictionary[key] = _slopeCountDictionary[key] + 1;
            }
        }

        private static float CalculateSlope(Point p1, Point p2)
        {
            if (p1 == p2)
                return 0;

            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;

            if (x1 == x2)
                return 0;

            float slope = (float)(y2 - y1) / (x2 - x1);
            float roundedTo2Dp = (float)Math.Round(slope, 2);
            
            
            //Console.WriteLine($"Slope between {x1},{y1} and {x2},{y2} is: {roundedTo2Dp}");
            
            return roundedTo2Dp;
        }
    }
}
