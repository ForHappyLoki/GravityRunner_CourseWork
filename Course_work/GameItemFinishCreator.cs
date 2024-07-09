using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work
{
    public class GameItemFinishCreator : GameObjectCreator
    {
        public override GameObject Create(int posX, int posY, Image image, Size size)
        {
            return new GameItemFinish(posX, posY, size);
        }
    }
}
