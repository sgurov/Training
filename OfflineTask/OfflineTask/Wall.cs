using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OfflineTask
{
    public class Wall
    {
        public int WallId { set; get; }
        public Point FirstPoint { set; get; }
        public Point SecondPoint { set; get; }

        public Wall(int wallId, Point firstPoint, Point secondPoint)
        {
            WallId = wallId;
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
        }
    }
}
