using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work
{
    public class ObstacleCreator : GameObjectCreator
    {
        public override GameObject Create(int posX, int posY, Image image, Size size)
        {
            return new Obstacle(posX, posY, image, size);
        }
    }
}
