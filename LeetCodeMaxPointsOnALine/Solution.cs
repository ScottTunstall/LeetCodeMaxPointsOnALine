using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LeetCodeMaxPointsOnALine
{
    public class Solution
    {
        Dictionary<string, int> _slopeCountDictionary = new Dictionary<string, int>();

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

            for (int i = 0; i < asList.Count - 1; i++)
            {
                var startPoint = asList[i];
                
                for (int j = 1; j < asList.Count; j++)
                {
                    var endPoint = asList[j];

                    var slope = CalculateSlope(startPoint, endPoint);

                    string key = i + "_" + slope.ToString();
                    IncrementValueForKey(key);
                }
            }

            return _slopeCountDictionary.Max(x => x.Value) + 1;
        }

        private void IncrementValueForKey(in string key)
        {
            if (!_slopeCountDictionary.ContainsKey(key))
            {
                _slopeCountDictionary[key] = 1;
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

            float slope = (float)(y2 - y1) / (x2 - x1);
            return (float)Math.Round(slope, 2);
        }
    }
}
