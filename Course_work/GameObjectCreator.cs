using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work
{
    public abstract class GameObjectCreator
    {
        abstract public GameObject Create(int posX, int posY, Image image, Size size);
    }
}
