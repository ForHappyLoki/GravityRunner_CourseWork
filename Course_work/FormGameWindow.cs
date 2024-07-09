using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Course_work
{
    public partial class FormGameWindow : Form
    {
        private int _gravity = 6;
        public double _gravity_modifier = 1;
        private int _flySpeed = 7;
        private int _flyFuel = 1000;
        private bool _on_ground = false;
        private bool _on_first_jump = false;
        private bool _jumpBool = false;
        public bool _changing_the_polarityBool = false;
        private bool _flyUp = false;
        private bool _flyDown = false;
        public bool _deathBool = false;
        private bool _winBool = false;

        private bool _doubleJump = false;
        private bool _doubleJumpHelp = false;
        private bool _heavyJump = false;

        private string _gameMode = "Defult";

        private List<int[,]> lvl1 = new List<int[,]>();
        private int lvl1Counter = 0;

        private List<GameObject> itemBox = new List<GameObject>();
        private List<GameObject> unrealBox = new List<GameObject>();
        private List<GameObject> realItem = new List<GameObject>();
        private List<GameObject> clear = new List<GameObject>();
        private List<GameObject> buffer = new List<GameObject>();
        private int _lvlCounter = 0;

        private PlatformCreator platformCreator = new PlatformCreator();
        private ObstacleCreator obstacleCreator = new ObstacleCreator();
        private GameItemJatpackCreator gameItemJatpackCreator = new GameItemJatpackCreator();
        private GameItemFinishCreator gameItemFinishCreator = new GameItemFinishCreator();
        private GameItemDoubleJumpCreator gameItemDoubleJumpCreator = new GameItemDoubleJumpCreator();
        private GameItemHugeLeapCreator gameItemHugeLeapCreator = new GameItemHugeLeapCreator();

        private Image playerAnimationRunImg1;
        private Image playerAnimationRunImg2;
        private int playerAnimationFrame = 0;
        private Image playerAnimationFly;

        private Image groundAnimationRun1;
        private int groundAnimationFrame = 0;
        private Image groundAnimationRun2;

        private Image BGAnimationRun;
        private int BGAnimationFrame = 0;

        private Image BlockImage;
        private Image BlockImage1;
        private Image BlockImage2;
        private Image BlockImage3;
        private Image BlockImage4;
        private Image BlockImage5;
        private Image BlockImage6;
        private Image BlockImage7;
        private Image BlockImage8;
        private Image BlockImage9;

        private Image BlockImage180x120down;
        private Image BlockImage180x120up;

        private Image BlockImage60x120down;
        private Image BlockImage60x120up;

        private Image BlockImage60x180down;
        private Image BlockImage60x180up;

        private Image BlockImage60x240down;
        private Image BlockImage60x240up;

        private Image ObstacleImage60x60down;
        private Image ObstacleImage60x60up;

        private Image ObstacleSprite60x60down;
        private Image ObstacleSprite60x60up;

        private Image ObstacleSprite60x120up;
        private Image ObstacleSprite60x120down;

        private Image BoostDJ60x60;
        private Image BoostHJ60x60;

        public Form f2;

        private System.Windows.Forms.Timer timerDistanse = new System.Windows.Forms.Timer();

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams CP = base.CreateParams;
                CP.ExStyle = CP.ExStyle | 0x2000000;
                return CP;
            }
        }
        public FormGameWindow(Form f2)
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeMap();
            this.f2 = f2;
        }
        private void OpenMainMenu()
        {
            this.Hide();
            f2.Show();
        }
        private void Update(object sender, EventArgs e)
        {
            bool find = false;
            find = CheckGround();
            if (lvl1.Count - 1 > _lvlCounter)
            {
                if (itemBox[itemBox.Count - 1].Location.X <= 1300)
                {
                    _lvlCounter++;
                    InitializingPartLvl(lvl1[_lvlCounter]);
                }
            }
            foreach (GameObject item in realItem)
            {
                if (((Player.Location.X > item.Location.X - 1 && Player.Location.X < item.Location.X + item.Width)
                        || (Player.Location.X + Player.Width + 1 > item.Location.X && Player.Location.X < item.Location.X + item.Width)))
                {
                    if (item.GetType().ToString() == "Course_work.Platform")
                    {
                        if (_gameMode == "Defult")
                        {
                            if (_gravity > 0)
                            {
                                if ((item.Location.Y - Player.Location.Y <= Player.Size.Height + _gravity * _gravity_modifier
                                    && item.Location.Y - Player.Location.Y >= Player.Size.Height - _gravity * _gravity_modifier))
                                {
                                    _gravity_modifier = 0;

                                    Player.Location = new Point(Player.Location.X, (int)(item.Location.Y - Player.Size.Height));
                                    _on_ground = true;
                                    find = true;
                                }
                            }
                            else
                            {
                                if ((Player.Location.Y - item.Location.Y <= item.Size.Height + _gravity * _gravity_modifier * -1
                                    && Player.Location.Y - item.Location.Y >= item.Size.Height - _gravity * _gravity_modifier * -1))
                                {
                                    _gravity_modifier = 0;
                                    Player.Location = new Point(Player.Location.X, (int)(item.Location.Y + item.Size.Height));
                                    _on_ground = true;
                                    find = true;
                                }
                            }
                        }
                        else if (_gameMode == "Fly")
                        {
                            if ((Player.Location.Y <= item.Size.Height + item.Location.Y + _gravity * _gravity_modifier * -1
                                && Player.Location.Y - item.Location.Y >= item.Size.Height - _gravity * _gravity_modifier * -1)
                                && _flyUp == true)
                            {
                                _gravity_modifier = 0;
                                Player.Location = new Point(Player.Location.X, (int)(item.Location.Y + item.Size.Height));
                            }
                            else if ((item.Location.Y - Player.Location.Y <= Player.Size.Height + _gravity * _gravity_modifier
                                && item.Location.Y - Player.Location.Y >= Player.Size.Height - _gravity * _gravity_modifier)
                                && _flyDown == true)
                            {
                                _gravity_modifier = 0;

                                Player.Location = new Point(Player.Location.X, (int)(item.Location.Y - Player.Size.Height));
                                _on_ground = true;
                            }
                        }
                        CheckDeath(item);
                    }
                    else if (item.GetType().ToString() == "Course_work.GameItemJatpack")
                    {
                        _gameMode = "Fly";
                        _gravity = 0;
                        _gravity_modifier = 1;
                        clear.Add(item);
                        item.Dispose();
                    }
                    else if (item.GetType().ToString() == "Course_work.Obstacle")
                    {
                        if (item.Size.Height == 120)
                        {
                            if (Player.Location.X > item.Location.X - item.Size.Width * 2 / 3 
                                && Player.Location.X < item.Location.X - item.Size.Width/3)
                            {
                                CheckDeath(item);
                            }
                        }
                        else
                        {
                            CheckDeath(item);
                        }
                    }
                    else if (item.GetType().ToString() == "Course_work.GameItemFinish")
                    {
                        _winBool = true;
                        Win();
                    }
                    else if (item.GetType().ToString() == "Course_work.GameItemHugeLeap")
                    {
                        if ((item.Location.Y <= Player.Size.Height + Player.Location.Y  
                            && item.Location.Y + item.Size.Height >= Player.Size.Height + Player.Location.Y))
                        {
                            _heavyJump = true;
                            clear.Add(item);
                            item.Dispose();
                        }
                    }
                    else if (item.GetType().ToString() == "Course_work.GameItemDoubleJump")
                    {
                        if ((item.Location.Y <= Player.Size.Height + Player.Location.Y
                            && item.Location.Y + item.Size.Height >= Player.Size.Height + Player.Location.Y))
                        {
                            _doubleJump = true;
                            clear.Add(item);
                            item.Dispose();
                        }
                    }
                }
                if (item.Location.X + item.Size.Width < 0)
                {
                    clear.Add(item);
                    item.Dispose();
                }
            }
            foreach (GameObject item in clear)
            {
                itemBox.Remove(item);
                realItem.Remove(item);
            }
            clear.Clear();
            CheckDeathOnCeiling();
            if (!find && _gameMode == "Defult")
            {
                _on_ground = false;
            }
            CheckMove();
            if (_gameMode == "Fly")
            {
                _flyFuel -= 1;
                if (_flyFuel < 0)
                {
                    _gameMode = "Defult";
                    _gravity_modifier = 1;
                    _gravity = 6;
                }
            }
            Player.Location = new Point(Player.Location.X, (int)(Player.Location.Y + _gravity * _gravity_modifier));
            Player.Refresh();
        }
        public void CheckMove()
        {
            if (!_on_ground && _gameMode == "Defult")
            {
                _gravity_modifier += 0.12;
            }
            if (_jumpBool && _gameMode == "Defult")
            {
                Jump();
            }
            if (_changing_the_polarityBool && _gameMode == "Defult")
            {
                ChangingThePolarity();
            }
            if (_gameMode == "Fly")
            {
                _gravity = 0;
            }
            if (_flyUp &&  _gameMode == "Fly")
            {
                _gravity = -_flySpeed;
            }
            else if (_flyDown && _gameMode == "Fly")
            {
                _gravity = _flySpeed;
            }
        }
        public void Win()
        {
            if (_on_ground)
            {
                _winBool = true;
                timerBG.Stop();
                timerDistanse.Stop();
                _gameMode = "Defult";
                if (_gravity == 0)
                {
                    _gravity = 7;
                }
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Tick += new EventHandler(UpdateAnimationRun);
                timer.Tick += new EventHandler(WinUpdate);
                timer.Interval = 16;
                timer.Start();
            }
        }
        public void WinUpdate(object sender, EventArgs e)
        {
            Player.Location = new Point(Player.Location.X + 7, Player.Location.Y);
            if (Player.Location.X > 1500)
            {
                Application.Restart();
            }
        }
        public void CheckDistance(object sender, EventArgs e)
        {
            foreach (GameObject item in itemBox)
            {
                if (item.CheckDistance(Player))
                {
                    realItem.Add(item);
                    buffer.Add(item);
                }
            }
            foreach (GameObject item in buffer)
            {
                itemBox.Remove(item);
            }
            buffer.Clear();
        }
        public void Jump()
        {
            if ((_on_ground || _deathBool || _doubleJump) && !_winBool)
            {
                if (_on_ground)
                {
                    _doubleJumpHelp = false;
                }
                _on_first_jump = true;
                if (_doubleJump && _doubleJumpHelp & !_on_ground)
                {
                    _doubleJump = false;
                }
                _gravity_modifier = -2.5;
                if (_heavyJump)
                {
                    _gravity_modifier = -3.6;
                    _heavyJump = false;
                }
                _on_ground = false;
                if (_gravity > 0)
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg1, 0, 0, new Rectangle(new Point(0, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                }
                else
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg2, 0, 0, new Rectangle(new Point(0, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                }
            }
        }
        public void ChangingThePolarity()
        {
            if (_on_ground || _on_first_jump)
            {
                _gravity *= -1;
                _gravity_modifier *= -1;
                _on_ground = false;
                _on_first_jump = false;
                if (_gravity > 0)
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg1, 0, 0, new Rectangle(new Point(0, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                }
                else
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg2, 0, 0, new Rectangle(new Point(0, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                }
            }

        }
        private bool CheckGround()
        {
            bool find = false;
            if (_gravity > 0 || _gameMode == "Fly")
            {
                if (ground.Location.Y <= Player.Size.Height + Player.Location.Y)
                {
                    if (_gameMode == "Defult" || _flyDown)
                    {
                        _gravity_modifier = 0;
                        Player.Location = new Point(Player.Location.X, (int)(ground.Location.Y - Player.Size.Height));
                    }
                    _on_ground = true;
                    find =  true;
                }
            }
            if (_gravity < 0 || _gameMode == "Fly")
            {
                if ( Player.Location.Y - (ceiling.Location.Y + ceiling.Size.Height) <= 0)
                {
                    if (_gameMode == "Defult" || _flyUp)
                    {
                        _gravity_modifier = 0;
                        Player.Location = new Point(Player.Location.X, (int)(ceiling.Location.Y + ceiling.Size.Height));
                    }
                    _on_ground = true;
                    find = true;
                }
            }
            if (_gameMode == "Fly")
            {
                _gravity_modifier = 1;
            }
            return find;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void OKP(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (_gameMode == "Defult")
                    {
                        _jumpBool = true;
                    }
                    else if (_gameMode == "Fly")
                    {
                        _flyUp = true;
                    }
                    break;
                case Keys.E:
                    _changing_the_polarityBool = true;
                    break;
                case Keys.S:
                    if (_gameMode == "Fly")
                    {
                        _flyDown = true;
                    }
                    break;
                case Keys.Escape:
                    timerBG.Stop();
                    timerDistanse.Stop();
                    OpenMainMenu();
                    break;

            }
        }
        private void OKU(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            switch (e.KeyCode.ToString())
            {
                case "W":
                    _jumpBool = false;
                    _doubleJumpHelp = true;
                    _flyUp = false;
                    break;
                case "E":
                    _changing_the_polarityBool = false;
                    break;
                case "S":
                    _flyDown = false;
                    break;
            }
        }
        private void CheckDeath(GameObject item)
        {
            if ((Player.Location.Y >= item.Location.Y && Player.Location.Y < item.Location.Y + item.Height)
                || (Player.Location.Y + Player.Height > item.Location.Y && Player.Location.Y + Player.Height < item.Location.Y + item.Height))
            {
                Death();
            }
        }
        private void CheckDeathOnCeiling()
        {
            if (_gameMode == "Defult")
            {
                if (_gravity > 0)
                {
                    if (Player.Location.Y - ceiling.Location.Y < ceiling.Size.Height)
                    {
                        Death();
                    }
                }
                if (_gravity < 0)
                {
                    if (ground.Location.Y < Player.Location.Y + Player.Size.Height)
                    {
                        Death();
                    }
                }
            }
        }
        public void Death()
        {
            _deathBool = true;
            timerBG.Stop();
            timerDistanse.Stop();
            _gameMode = "Defult";
            if (_gravity == 0)
            {
                _gravity = 7;
            }
            Jump();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(UpdateAnimationOfDead);
            timer.Interval = 16;
            timer.Start();
        }
        private void UpdateAnimationOfDead(object sender, EventArgs e)
        {
            Player.Location = new Point(Player.Location.X, (int)(Player.Location.Y + _gravity * _gravity_modifier));
            if (Player.Location.Y > 1000 || Player.Location.Y < -700)
            {
                Application.Restart();
            }
            _gravity_modifier += 0.12;
        }
        private void UpdateAnimationRun(object sender, EventArgs e)
        {
            if (_on_ground && _gameMode == "Defult")
            {
                if (_gravity > 0)
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg1, 0, 0, new Rectangle(new Point(60* playerAnimationFrame, 0), new Size(60,60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                    playerAnimationFrame++;
                    if (playerAnimationFrame > 9)
                    {
                        playerAnimationFrame = 0;
                    }
                }
                else
                {
                    Image playerPart = new Bitmap(60, 60);
                    Graphics g = Graphics.FromImage(playerPart);
                    g.DrawImage(playerAnimationRunImg2, 0, 0, new Rectangle(new Point(60 * playerAnimationFrame, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                    Player.Size = new Size(60, 60);
                    Player.Image = playerPart;
                    playerAnimationFrame++;
                    if (playerAnimationFrame > 9)
                    {
                        playerAnimationFrame = 0;
                    }
                }
            }
            else if (_gameMode == "Fly")
            {
                if (playerAnimationFrame > 3)
                {
                    playerAnimationFrame = 0;
                }
                Image playerPart = new Bitmap(60, 60);
                Graphics g = Graphics.FromImage(playerPart);
                g.DrawImage(playerAnimationFly, 0, 0, new Rectangle(new Point(60 * playerAnimationFrame, 0), new Size(60, 60)), GraphicsUnit.Pixel);
                Player.Size = new Size(60, 60);
                Player.Image = playerPart;
                playerAnimationFrame++;
            }
        }
        private void UpdateAnimationGround(object sender, EventArgs e)
        {
            Image groundPart = new Bitmap(1300, 50);
            Graphics g = Graphics.FromImage(groundPart);
            g.DrawImage(groundAnimationRun1, 0, 0, new Rectangle(new Point(groundAnimationFrame, 0), new Size(1300, 50)), GraphicsUnit.Pixel);
            ground.Size = new Size(1300, 50);
            ground.Image = groundPart;

            Image ceilingPart = new Bitmap(1300, 50);
            Graphics g2 = Graphics.FromImage(ceilingPart);
            g2.DrawImage(groundAnimationRun2, 0, 0, new Rectangle(new Point(groundAnimationFrame, 0), new Size(1300, 50)), GraphicsUnit.Pixel);
            ceiling.Size = new Size(1300, 50);
            ceiling.Image = ceilingPart;

            groundAnimationFrame += 6;
            if (groundAnimationFrame > 11700)
            {
                groundAnimationFrame = 0;
            }
        }
        private void UpdateAnimationBG(object sender, EventArgs e)
        {
            Image groundPart = new Bitmap(1300, 800);
            Graphics g = Graphics.FromImage(groundPart);
            g.DrawImage(BGAnimationRun, 0, 0, new Rectangle(new Point(BGAnimationFrame, 0), new Size(1300, 800)), GraphicsUnit.Pixel);
            BG.Image = groundPart; 

            BGAnimationFrame += 1;

            if (BGAnimationFrame > 1300)
            {
                BGAnimationFrame = 0;
            }

        }
        private void InitializingPartLvl(int[,] lvl)
        {
            int rows = lvl.GetUpperBound(0) + 1;    // количество строк
            int columns = lvl.Length / rows;        // количество столбцов
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    switch (lvl[i, j])
                    {
                        case 1:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage180x120down, new Size(180, 120)));
                            break;
                        case 2:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage180x120up, new Size(180, 120)));
                            break;
                        case 3:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage60x120down, new Size(60, 120)));
                            break;
                        case 4:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage60x120up, new Size(60, 120)));
                            break;
                        case 5:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage60x180down, new Size(60, 180)));
                            break;
                        case 6:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage60x180up, new Size(60, 180)));
                            break;
                        case 7:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage60x240down, new Size(60, 240)));
                            break;
                        case 8:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage60x240up, new Size(60, 240)));
                            break;
                        case 10:
                            unrealBox.Add(obstacleCreator.Create(i, j, ObstacleImage60x60down, new Size(60, 60)));
                            break;
                        case 11:
                            unrealBox.Add(obstacleCreator.Create(i, j, ObstacleImage60x60up, new Size(60, 60)));
                            break;
                        case 12:
                            unrealBox.Add(obstacleCreator.Create(i, j, ObstacleSprite60x60down, new Size(60, 60)));
                            break;
                        case 13:
                            unrealBox.Add(obstacleCreator.Create(i, j, ObstacleSprite60x60up, new Size(60, 60)));
                            break;
                        case 14:
                            itemBox.Add(obstacleCreator.Create(i, j, ObstacleSprite60x120down, new Size(60, 120)));
                            BG.Controls.Add(itemBox[itemBox.Count - 1]);
                            itemBox[itemBox.Count - 1].BackColor = Color.Transparent;
                            timerBG.Tick += itemBox[itemBox.Count - 1].ItemMove;
                            itemBox[itemBox.Count - 1].BringToFront();
                            break;
                        case 15:
                            itemBox.Add(obstacleCreator.Create(i, j, ObstacleSprite60x120up, new Size(60, 120))); 
                            BG.Controls.Add(itemBox[itemBox.Count - 1]);
                            itemBox[itemBox.Count - 1].BackColor = Color.Transparent;
                            timerBG.Tick += itemBox[itemBox.Count - 1].ItemMove;
                            itemBox[itemBox.Count - 1].BringToFront();
                            break;
                        case 20:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage, new Size(60, 60)));
                            break;
                        case 21:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage1, new Size(60, 60)));
                            break;
                        case 22:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage2, new Size(60, 60)));
                            break;
                        case 23:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage3, new Size(60, 60)));
                            break;
                        case 24:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage4, new Size(60, 60)));
                            break;
                        case 25:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage5, new Size(60, 60)));
                            break;
                        case 26:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage6, new Size(60, 60)));
                            break;
                        case 27:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage7, new Size(60, 60)));
                            break;
                        case 28:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage2, new Size(60, 60)));
                            break;
                        case 29:
                            unrealBox.Add(platformCreator.Create(i, j, BlockImage9, new Size(60, 60)));
                            break;
                        case 30:
                            Image JatpackSprite60x120up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\jetpackAnimation.png");
                            itemBox.Add(gameItemJatpackCreator.Create(i, j, JatpackSprite60x120up, new Size(60, 120)));
                            BG.Controls.Add(itemBox[itemBox.Count - 1]);
                            itemBox[itemBox.Count - 1].BackColor = Color.Transparent;
                            timerBG.Tick += itemBox[itemBox.Count - 1].ItemMove;
                            itemBox[itemBox.Count - 1].BringToFront();
                            break;
                        case 31:
                            itemBox.Add(gameItemHugeLeapCreator.Create(i, j, BoostHJ60x60, new Size(60, 60)));
                            BG.Controls.Add(itemBox[itemBox.Count - 1]);
                            itemBox[itemBox.Count - 1].BackColor = Color.Transparent;
                            timerBG.Tick += itemBox[itemBox.Count - 1].ItemMove;
                            itemBox[itemBox.Count - 1].BringToFront();
                            break;
                        case 32:
                            itemBox.Add(gameItemDoubleJumpCreator.Create(i, j, BoostDJ60x60, new Size(60, 60)));
                            BG.Controls.Add(itemBox[itemBox.Count - 1]);
                            itemBox[itemBox.Count - 1].BackColor = Color.Transparent;
                            timerBG.Tick += itemBox[itemBox.Count - 1].ItemMove;
                            itemBox[itemBox.Count - 1].BringToFront();
                            break;
                        case 100:
                            itemBox.Add(gameItemFinishCreator.Create(i, j, new Bitmap(1, 1), new Size(600, 600)));
                            BG.Controls.Add(itemBox[itemBox.Count - 1]);
                            itemBox[itemBox.Count - 1].BackColor = Color.Transparent;
                            timerBG.Tick += itemBox[itemBox.Count - 1].ItemMove;
                            itemBox[itemBox.Count - 1].BringToFront();
                            break;
                    }
                }
            }
            foreach (GameObject objects in unrealBox)
            {
                this.Controls.Add(objects);
                itemBox.Add(objects);
                objects.BringToFront();
                timerBG.Tick += objects.ItemMove;
            }

            Player.BringToFront();
            unrealBox.Clear();
        }
        private void InitializeLvl()
        {
            // 1 - 180x120down; 2 - 180x120up; 3 - 60x120down; 4 - 60x120up;
            // 5 - 60x180down; 6 - 60x180up; 7 - 60x240down; 8 - 60x240up;
            // 10 - magma60x60down; 11 - magma60x60up; 12 - intoMagma60x60down; 13 - intoMagma60x60up;
            // 20-29 - Грани куба и сам куб
            // 30 - Джетпак; 31 - Большой прыжок; 32 - Двойной прыжок
            int[,] lvl1Part1 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 2, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },  
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 5, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 4, 0, 0, 0, 0, 0, 7, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        };
            int[,] lvl1Part2 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 31 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },  
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 21, 24 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 22, 25 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 22, 25 },
                                        { 0, 0, 0, 0, 0, 0, 1, 0, 25, 25 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 25, 25 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 26, 26 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 10, 12 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 10, 12 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 10, 12 },
                                        { 15, 0, 0, 0, 0, 0, 1, 0, 24, 24},
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 25, 25 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 25, 25 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 22, 25 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 22, 25 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 23, 26 },
                                        };
            int[,] lvl1Part3 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        };
            int[,] lvl1Part4 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        };
            int[,] lvl1Part5 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 20, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 20, 0, 0, 0, 0, 0, 3, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 20, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 20, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 20, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 20, 0, 0, 0, 0, 0, 0 },
                                        };
            int[,] lvl1Part6 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 5, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 20, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 20, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        };
            int[,] lvl1Part7 = new int[,] {   { 11, 0, 0, 0, 0, 0, 0, 5, 0, 0 },
                                        { 11, 0, 0, 20, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 20, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 20, 0, 0, 0, 0, 0, 3, 0 },
                                        { 11, 0, 20, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 11, 0, 20, 0, 0, 0, 0, 0, 3, 0 },
                                        { 4, 0, 20, 0, 0, 0, 0, 0, 3, 0 },
                                        };
            int[,] lvl1Part8 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        };
            int[,] lvl1Part9 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        };
            int[,] lvl1Part10 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 6, 0, 0, 0, 0, 0, 0, 5, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 6, 0, 0, 20, 0, 0, 20, 5, 0, 0 },
                                        { 0, 0, 0, 20, 0, 0, 20, 0, 0, 0 },
                                        { 0, 0, 0, 20, 0, 0, 20, 0, 0, 0 },
                                        { 0, 0, 0, 20, 0, 0, 20, 0, 0, 0 },
                                        { 0, 0, 0, 20, 0, 0, 20, 0, 0, 0 },
                                        { 6, 0, 0, 20, 30, 0, 20, 5, 0, 0 },
                                        };
            int[,] lvl1Part11 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 3, 0, 4, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 3, 0, 4, 0, 0, 0, 3, 0, 4, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 20, 0, 0, 0, 0, 0, 20, 0 },
                                        { 0, 0, 0, 0, 0, 20, 0, 0, 0, 0 },
                                        { 20, 0, 0, 0, 0, 0, 0, 0, 0, 20 },
                                        { 0, 0, 20, 0, 0, 0, 0, 0, 20, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 20, 0, 0, 0, 0 },
                                        { 0, 0, 20, 0, 0, 20, 0, 0, 0, 0 },
                                        { 0, 0, 20, 0, 0, 0, 0, 0, 20, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 20, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        };
            int[,] lvl1Part12 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 20, 20, 0, 0, 0, 0, 0, 0, 20, 20 },
                                        { 0, 0, 0, 0, 0, 20, 20, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 20, 20, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 20, 20, 0, 0, 20, 20, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 20, 0, 0, 20, 20, 0, 0, 20, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        };
            int[,] lvl1Part13 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 14, 0, 20, 0, 0, 20, 15, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 14, 0, 20, 20, 15, 0, 0, 0 },
                                        };
            int[,] lvl1Part14 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 20, 15, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 20, 0 },
                                        { 0, 0, 0, 0, 0, 0, 14, 0, 20, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 20, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 20, 15, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 14, 0, 20 },
                                        };
            int[,] lvl1Part15 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 20, 0, 0, 0, 20, 0, 20, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 20, 0, 0, 0, 20, 0, 0, 0, 20, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 20, 0, 0, 0, 20, 0, 0, 0, 20 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 20, 0, 0, 20, 0, 0, 20, 0, 0, 0 },
                                        };
            int[,] lvl1Part16 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 20, 0, 0, 20, 0, 0, 0 },
                                        { 15, 0, 0, 20, 0, 0, 20, 0, 14, 0 },
                                        { 0, 0, 0, 20, 0, 0, 20, 0, 0, 0 },
                                        { 15, 0, 0, 20, 0, 0, 20, 0, 14, 0 },
                                        { 0, 0, 0, 20, 0, 0, 20, 0, 0, 0 },
                                        { 15, 0, 0, 20, 0, 0, 20, 0, 14, 0 },
                                        { 0, 0, 0, 20, 0, 0, 20, 0, 0, 0 },
                                        { 20, 20, 20, 20, 0, 0, 20, 20, 20, 20 },
                                        };
            int[,] lvl1Part17 = new int[,] {   { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 20, 0, 0, 0, 0, 0, 0, 20, 0 },
                                        { 20, 20, 0, 0, 0, 0, 0, 0, 20, 20 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        };
            int[,] lvl1Part18 = new int[,] {    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 15, 0, 0, 0, 0, 0, 0, 0, 14, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 4, 0, 32, 0, 0, 0, 0, 31, 3, 0 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 11, 0, 0, 0, 0, 0, 0, 0, 0, 10 },
                                        { 4, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        };
            int[,] lvl1Part10000 = new int[,] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 100, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        };
            lvl1.Add(lvl1Part1);
            lvl1.Add(lvl1Part2);
            lvl1.Add(lvl1Part3);
            lvl1.Add(lvl1Part4);
            lvl1.Add(lvl1Part5);
            lvl1.Add(lvl1Part6);
            lvl1.Add(lvl1Part7);
            lvl1.Add(lvl1Part8);
            lvl1.Add(lvl1Part9);

            lvl1.Add(lvl1Part10);
            lvl1.Add(lvl1Part11);
            lvl1.Add(lvl1Part12);
            lvl1.Add(lvl1Part13);
            lvl1.Add(lvl1Part14);
            lvl1.Add(lvl1Part15);
            lvl1.Add(lvl1Part16);

            lvl1.Add(lvl1Part17);
            lvl1.Add(lvl1Part18);

            lvl1.Add(lvl1Part10000);
        }
        private void InitializeMap()
        {
            playerAnimationRunImg1 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Player.png");
            playerAnimationRunImg2 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\PlayerUP.png");
            playerAnimationFly = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\FlyAnimation.png");
            groundAnimationRun1 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Ground.png");
            groundAnimationRun2 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Ground2.png");
            BGAnimationRun = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\BG.png");

            BlockImage = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block.png");
            BlockImage1 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block1.png");
            BlockImage2 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block2.png");
            BlockImage3 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block3.png");
            BlockImage4 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block4.png");
            BlockImage5 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block5.png");
            BlockImage6 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block6.png");
            BlockImage7 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block7.png");
            BlockImage8 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block8.png");
            BlockImage9 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block9.png");

            BlockImage180x120down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block180x120.png");
            BlockImage180x120up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block180x120up.png");
            BlockImage60x120down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x120down.png");
            BlockImage60x120up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x120up.png");
            BlockImage60x180down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x180down.png");
            BlockImage60x180up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x180up.png");
            BlockImage60x240down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x240down.png");
            BlockImage60x240up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Block60x240up.png");
            ObstacleImage60x60down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Magma60x60down.png");
            ObstacleImage60x60up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Magma60x60up.png");
            ObstacleSprite60x60down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\MagmaSprite60x60down.png");
            ObstacleSprite60x60up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\MagmaSprite60x60up.png");

            ObstacleSprite60x120down = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Obstacle1.png");
            ObstacleSprite60x120up = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\Obstacle2.png");

            BoostHJ60x60 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\HeavyJumpSprite.png");
            BoostDJ60x60 = new Bitmap("D:\\Learning\\Course_work_1_OOP\\Sprites\\DoubleJumpSprite.png");

            Player.Show();
            ground.Show();
            ceiling.Show();

            this.KeyDown += new KeyEventHandler(OKP);
            this.KeyUp += new KeyEventHandler(OKU);

            BG.Location = new Point(0, 0);
            BG.Size = new Size(1300, 700);
            BG.Image = new Bitmap(60, 60);

            BG.Controls.Add(Player);
            Player.BackColor = Color.Transparent;

            ground.Location = new Point(0, 650);
            ground.Size = new Size(1300, 50);

            ground.BringToFront();
            ceiling.BringToFront();
            Player.BringToFront();

            timerBG.Tick += new EventHandler(Update);
            timerBG.Tick += new EventHandler(UpdateAnimationRun);
            timerBG.Tick += new EventHandler(UpdateAnimationGround);
            timerBG.Tick += new EventHandler(UpdateAnimationBG);
            timerBG.Interval = 16;
            timerBG.Start();

            timerDistanse.Tick += new EventHandler(CheckDistance);
            timerDistanse.Interval = 100;
            timerDistanse.Start();

            DoubleBuffered = true;

            ceiling.Size = new Size(1300, 50);
            ground.Size = new Size(1300, 50);
            InitializeLvl();
            InitializingPartLvl(lvl1[0]);
        }
    }
}
