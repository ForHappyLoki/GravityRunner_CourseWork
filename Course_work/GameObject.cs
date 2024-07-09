using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_work
{
    public abstract class GameObject : PictureBox
    {
        private bool _closely = false;
        public void ItemMove()
        {
            if (IsHandleCreated)
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        this.Location = new System.Drawing.Point(this.Location.X - 6, this.Location.Y);
                    });
                }
                catch (Exception ex) { }
            }

        }
        public void ItemMove(object sender, EventArgs e)
        {
            if (IsHandleCreated)
            {
                try
                {
                    Task.Run(() => Invoke((MethodInvoker)delegate
                    {
                        this.Location = new System.Drawing.Point(this.Location.X - 6, this.Location.Y);
                    }));
                }
                catch (Exception ex) { }
            }

        }
        public bool CheckDistance(PictureBox player)
        {
            if (this.Location.X - player.Location.X < 120 && !_closely)
            {
                _closely = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public abstract void PlayIdleAnimation(object sender, EventArgs e);
    }
}
