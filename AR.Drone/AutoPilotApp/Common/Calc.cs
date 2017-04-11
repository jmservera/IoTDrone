using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp.Common
{
    public class Calc
    {
        public static float SignedPolygonArea(Point[] Points)
        {
            // Add the first point to the end.
            int num_points = Points.Length;
            PointF[] pts = new PointF[num_points + 1];
            Points.CopyTo(pts, 0);
            pts[num_points] = Points[0];

            // Get the areas.
            float area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return area;
        }
    }
}
