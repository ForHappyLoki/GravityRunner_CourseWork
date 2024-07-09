﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work
{
    public class Obstacle : GameObject
    {
        static readonly int _Height = 60;
        static readonly int _Width = 60;
        public Obstacle(int posX, int posY, Image image, Size size) : base()
        {
            this.Location = new System.Drawing.Point(1300 + posX * 60, 50 + 60 * posY);
            this.BackColor = Color.Black;
            this.Size = size;
            this.Image = image;
        }
        public override void PlayIdleAnimation(object sender, EventArgs e)
        {

        }
    }
}
