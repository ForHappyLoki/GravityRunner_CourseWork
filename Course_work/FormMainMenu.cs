using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_work
{
    public partial class FormMainMenu : Form
    {
        public FormGameWindow form1;
        public Form f;
        public Button buttonStart;
        public Button buttonExit;
        public FormMainMenu()
        {
            InitializeComponent();
            InitializeMenu();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public void InitializeMenu()
        {
            buttonStart = new Button();
            buttonStart.Text = "Start";
            buttonStart.Font = new Font("Segoe UI", 40, FontStyle.Regular);
            buttonStart.FlatStyle = FlatStyle.Flat;
            buttonStart.Click += Start;
            buttonStart.Size = new Size(200, 100);
            buttonStart.Location = new Point(550, 250);
            this.Controls.Add(buttonStart);

            buttonExit = new Button();
            buttonExit.Text = "Exit";
            buttonExit.Font = new Font("Segoe UI", 40, FontStyle.Regular);
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Click += Exit;
            buttonExit.Size = new Size(200, 100);
            buttonExit.Location = new Point(550, 400);
            this.Controls.Add(buttonExit);
        }
        private void Exit(object sender, EventArgs e)
        {
            this.Dispose();
            if (f != null)
            {
                f.Dispose();
            }
        }
        public void Start(object sender, EventArgs e)
        {
            form1 = new FormGameWindow(this);
            form1.Size = new Size(1300, 700);
            f = form1;
            form1.Show();
            this.Hide();
        }
    }
}
