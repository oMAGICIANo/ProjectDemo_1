using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectDemo_1.Properties;
using System.Media;

namespace ProjectDemo_1
{
    public partial class Form1 : Form
    {
        // 設定珠子行數、列數
        private const int ROW = 5, COLUMN = 6;
        // 設定珠子寬度、高度
        private const int WIDTH = 100, HEIGHT = 100;
        // 換珠子感應範圍
        private const int RANGE = 40;
        // 初始化珠子
        private PictureBox[,] beadGrid = new PictureBox[ROW, COLUMN];
        // 校正珠子位置
        private PictureBox[,] numberGrid = new PictureBox[ROW, COLUMN];
        // 分組編號
        private int[,] beadGroup = new int[ROW, COLUMN];
        // 暫存移動珠子物件
        private PictureBox fingerPictureBox;
        // 控制珠子是否移動
        private bool moveFlag = false;
        // 是否有移動珠子
        private int moveCount = 0;
        // 記錄消除珠子數量、種類
        private int combo, red, orange, green, white, ice, nuclear;
        // 是否有新增 combo
        private bool addComboFlag = false;
        // 關卡秒數
        private int time;
        // 生怪秒數
        private int monsterTime;
        // 升級速度
        private const int SPEED_TIME = 300;
        // 設定玩家、怪物物件
        private Player myPlayer;
        private const int PLAYER_HP = 10000;
        private const int PICTUREBOX_WIDTH = 940;
        private const int MONSTER_MAX = 700;
        private int monsterCount;
        private PictureBox[] monster = new PictureBox[MONSTER_MAX];
        private const int DAMAGE = 100;
        private int speed;
        private const int SPEED_MAX = 30;
        private const int SPEED_START = 1;
        // 核彈、凍結特效
        private PictureBox pictureBoxBomb, pictureBoxIce;
        // 回復力
        private const int RESILIENCE = 500; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitGrid();
            DisplayBeadInfo();

            SetObject();
            FindBeadPoint();
            EnabledTurnBead(false);

            SetLayout();
        }

        private void SetLayout()
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(pictureBoxStart.ClientRectangle);
            Region reg = new Region(path);
            pictureBoxStart.Region = reg;

            path.AddEllipse(pictureBoxStop.ClientRectangle);
            pictureBoxStop.Region = reg;

            path.AddEllipse(pictureBoxRestart.ClientRectangle);
            pictureBoxRestart.Region = reg;

            pictureBoxStart.Click += new System.EventHandler(this.buttonStart_Click);
            pictureBoxStop.Click += new System.EventHandler(this.buttonStop_Click);
            pictureBoxRestart.Click += new System.EventHandler(this.buttonRestart_Click);
        }

        private async void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            p.Visible = true;

            moveFlag = false;
            fingerPictureBox.Dispose();

            if (moveCount != 0)
            {
                for (int i = 0; i < ROW; i++)
                {
                    for (int j = 0; j < COLUMN; j++)
                    {
                        if ((beadGrid[i, j].Location.X - RANGE <= fingerPictureBox.Location.X && beadGrid[i, j].Location.X + RANGE >= fingerPictureBox.Location.X) &&
                            (beadGrid[i, j].Location.Y - RANGE <= fingerPictureBox.Location.Y && beadGrid[i, j].Location.Y + RANGE >= fingerPictureBox.Location.Y))
                        {
                            p.Location = beadGrid[i, j].Location;
                        }
                    }
                }

                FindBeadPoint();
                EnabledTurnBead(false);
                BeadGroup();
                ResetComboCount();

                while (RemoveBead())
                {
                    await PutTaskDelay500();

                    DropBead();
                    BeadGroup();

                    await PutTaskDelay500();
                }

                DisplayBeadInfo();
                TurnSkill();
                RemoveMonster();
                EnabledTurnBead(true);
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveFlag)
            {
                PictureBox p = (PictureBox)sender;

                int mouseLocationX = Form1.MousePosition.X - this.Location.X - panelGrid.Location.X - 60;
                int mouseLocationY = Form1.MousePosition.Y - this.Location.Y - panelGrid.Location.Y - 60;

                fingerPictureBox.Location = new Point(mouseLocationX, mouseLocationY);

                for (int i = 0; i < ROW; i++)
                {
                    for (int j = 0; j < COLUMN; j++)
                    {
                        if ((beadGrid[i, j].Location.X - RANGE <= fingerPictureBox.Location.X && beadGrid[i, j].Location.X + RANGE >= fingerPictureBox.Location.X) &&
                            (beadGrid[i, j].Location.Y - RANGE <= fingerPictureBox.Location.Y && beadGrid[i, j].Location.Y + RANGE >= fingerPictureBox.Location.Y))
                        {
                            if (beadGrid[i, j].Name != p.Name)
                            { 
                                Point tempPoint = p.Location;
                                p.Location = beadGrid[i, j].Location;
                                beadGrid[i, j].Location = tempPoint;

                                moveCount++;
                            }
                        }
                    }
                }
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            
            p.Visible = false;

            fingerPictureBox = new PictureBox();
            fingerPictureBox.Location = new Point(p.Location.X, p.Location.Y);
            fingerPictureBox.Image = p.Image;
            fingerPictureBox.Size = new Size(WIDTH, HEIGHT);
            fingerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            fingerPictureBox.Name = "moveP";

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(fingerPictureBox.ClientRectangle);
            Region reg = new Region(path);
            fingerPictureBox.Region = reg;

            panelGrid.Controls.Add(fingerPictureBox);

            int mouseLocationX = Form1.MousePosition.X - this.Location.X - panelGrid.Location.X - 60;
            int mouseLocationY = Form1.MousePosition.Y - this.Location.Y - panelGrid.Location.Y - 60;

            fingerPictureBox.Location = new Point(mouseLocationX, mouseLocationY);
            fingerPictureBox.BringToFront();

            moveFlag = true;
            moveCount = 0;
        }

        // 初始化版面
        private void InitGrid()
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    beadGrid[i, j] = new PictureBox();
                    beadGrid[i, j].Location = new Point(0 + j * HEIGHT, 0 + i * WIDTH);
                    beadGrid[i, j].Size = new Size(WIDTH, HEIGHT);
                    beadGrid[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    beadGrid[i, j].Name = "P" + i.ToString() + j.ToString();
                    //beadGrid[i, j].Image = Resources.red_bead;

                    RandomBead(beadGrid[i, j]);

                    System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                    path.AddEllipse(beadGrid[i, j].ClientRectangle);
                    Region reg = new Region(path);
                    beadGrid[i, j].Region = reg;

                    beadGrid[i, j].MouseUp += new MouseEventHandler(pictureBox_MouseUp);
                    beadGrid[i, j].MouseDown += new MouseEventHandler(pictureBox_MouseDown);
                    beadGrid[i, j].MouseMove += new MouseEventHandler(pictureBox_MouseMove);

                    panelGrid.Controls.Add(beadGrid[i, j]);
                }
            }

            GC.Collect();
        }

        // Form1 關閉事件
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        // 攻擊怪
        private async void RemoveMonster()
        {
            int redCount = 0;
            int orangeCount = 0;
            int greenCount = 0;

            if (nuclear > 0)
            {
                pictureBoxBomb = new PictureBox();
                pictureBoxBomb.Location = new Point(250, 75);
                pictureBoxBomb.Image = Resources.nuclear_bomb;
                pictureBoxBomb.Size = new Size(400, 400);
                pictureBoxBomb.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxBomb.Name = "Bomb";

                panelFight.Controls.Add(pictureBoxBomb);

                await PutTaskDelay1000();

                pictureBoxBomb.Image = Resources.boom;

                for (int i = 0; i < monster.Length; i++)
                {
                    if (monster[i].Visible)
                    {
                        monster[i].Image = Resources.explosion;
                    }
                }

                await PutTaskDelay1000();

                for (int i = 0; i < monster.Length; i++)
                {
                    if (monster[i].Visible)
                    {
                        monster[i].Visible = false;
                        monster[i].Dispose();

                        myPlayer.Score += 100;
                    }
                }

                pictureBoxBomb.Dispose();
            } 
            else
            {
                for (int i = 0; i < monster.Length; i++)
                {
                    if (nuclear <= 0)
                    {
                        if (red > redCount && red > 0 && monster[i].Visible && monster[i].Image.Tag.ToString() == "1")
                        {
                            monster[i].Image = Resources.explosion;
                            await PutTaskDelay500();

                            monster[i].Visible = false;
                            monster[i].Dispose();
                            redCount++;

                            myPlayer.Score += 100;
                        }

                        if (orange > orangeCount && orange > 0 && monster[i].Visible && monster[i].Image.Tag.ToString() == "2")
                        {
                            monster[i].Image = Resources.explosion;
                            await PutTaskDelay500();

                            monster[i].Visible = false;
                            monster[i].Dispose();
                            orangeCount++;

                            myPlayer.Score += 100;
                        }

                        if (green > greenCount && green > 0 && monster[i].Visible && monster[i].Image.Tag.ToString() == "3")
                        {
                            monster[i].Image = Resources.explosion;
                            await PutTaskDelay500();

                            monster[i].Visible = false;
                            monster[i].Dispose();
                            greenCount++;

                            myPlayer.Score += 100;
                        }
                    }
                }
            }
        }

        // 計時器
        private void timerMain_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                monsterCount = 0;
                speed = SPEED_START;
                monsterTime = 20;

                labelGamePoint.Text = "Score：" + myPlayer.Score;
                labelSpeed.Text = "Speed：" + speed;

                labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;
                float pictureboxHP = ((float)PICTUREBOX_WIDTH / PLAYER_HP) * myPlayer.HP;
                pictureBoxHP.Size = new Size((int)pictureboxHP, 25);

                time++;
            }
            else
            {
                if (speed == 5)
                {
                    monsterTime = 15;
                }
                if (speed == 10)
                {
                    monsterTime = 10;
                }
                if (speed == 15)
                {
                    monsterTime = 5;
                }
                if (speed == 20)
                {
                    monsterTime = 3;
                }

                if (time % monsterTime == 0)
                {
                    Level_1(monsterCount);

                    if (time > SPEED_TIME)
                    {
                        time = 1;

                        if (speed < SPEED_MAX)
                        { 
                            speed += 1;

                            labelSpeed.Text = "Speed：" + speed;
                        }
                    }
                }

                for (int i = 0; i < monster.Length; i++)
                {
                    if (monster[i].Visible)
                    {
                        monster[i].Location = new Point(monster[i].Location.X - speed,
                                                        monster[i].Location.Y);
                    }

                    if (monster[i].Location.X < 10 && monster[i].Visible)
                    {
                        monster[i].Visible = false;
                        monster[i].Dispose();

                        myPlayer.HP -= DAMAGE;

                        float pictureboxHP = ((float)PICTUREBOX_WIDTH / PLAYER_HP) * myPlayer.HP;
                        pictureBoxHP.Size = new Size((int)pictureboxHP, 25);

                        if (myPlayer.HP <= 0)
                        {
                            timerMain.Stop();
                            panelGrid.Controls.Clear();
                            //panelFight.Controls.Clear();

                            for (int j = 0; j < monster.Length; j++)
                            {
                                if (monster[i].Visible)
                                {
                                    monster[i].Dispose();
                                }
                            }

                            pictureBoxStop.Visible = false;
                            pictureBoxRestart.Visible = true;

                            labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;

                            MessageBox.Show("Score：" + myPlayer.Score, 
                                            "分數",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        }

                        labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;
                    }
                }

                labelGamePoint.Text = "Score：" + myPlayer.Score;

                time++;
            }
        }

        // 關卡 1
        private void Level_1(int number)
        {
            if (monsterCount < MONSTER_MAX)
            {
                monster[number].Visible = true;
                monster[number].BringToFront();
                panelFight.Controls.Add(monster[number]);

                monsterCount++;
            }
        }

        // 設定物件
        private void SetObject()
        {
            pictureBoxStop.Visible = false;
            pictureBoxRestart.Visible = false;

            //Form.CheckForIllegalCrossThreadCalls = false;

            time = 0;
            speed = 0;

            myPlayer = new Player("Hsu", PLAYER_HP, 0);

            labelGamePoint.Text = "Score：" + myPlayer.Score;

            labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;
            float pictureboxHP = ((float)PICTUREBOX_WIDTH / PLAYER_HP) * myPlayer.HP;
            pictureBoxHP.Size = new Size((int)pictureboxHP, 25);

            labelSpeed.Text = "Speed：" + speed;

            for (int i = 0; i < monster.Length; i++)
            {
                monster[i] = new PictureBox();
                monster[i].Name = "M" + i.ToString();

                Random myRandom = new Random();
                int randomPoint = myRandom.Next(1, 4);

                monster[i].Location = new Point(900, randomPoint * 100);
                monster[i].Size = new Size(60, 60);
                monster[i].SizeMode = PictureBoxSizeMode.Zoom;

                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(monster[i].ClientRectangle);
                Region reg = new Region(path);
                monster[i].Region = reg;

                int randomImage = myRandom.Next(0, 3);

                switch (randomImage)
                {
                    case 0:
                        monster[i].Image = Resources.red_bead;
                        monster[i].Image.Tag = "1";
                        break;
                    case 1:
                        monster[i].Image = Resources.orange_bead;
                        monster[i].Image.Tag = "2";
                        break;
                    case 2:
                        monster[i].Image = Resources.green_bead;
                        monster[i].Image.Tag = "3";
                        break;
                }

                monster[i].Visible = false;
            }

            GC.Collect();
        }

        // 開始遊戲
        private void buttonStart_Click(object sender, EventArgs e)
        {
            pictureBoxStart.Visible = false;
            pictureBoxStop.Visible = true;
            pictureBoxRestart.Visible = false;

            timerMain.Start();

            EnabledTurnBead(true);
        }

        // 暫停遊戲
        private void buttonStop_Click(object sender, EventArgs e)
        {
            pictureBoxStart.Visible = true;
            pictureBoxStop.Visible = false;
            pictureBoxRestart.Visible = true;

            timerMain.Stop();

            EnabledTurnBead(false);
        }

        // 重新開始
        private void buttonRestart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < monster.Length; i++)
            {
                monster[i].Dispose();
            }

            panelGrid.Controls.Clear();
            InitGrid();

            SetObject();

            pictureBoxStart.Visible = false;
            pictureBoxStop.Visible = true;
            pictureBoxRestart.Visible = false;

            timerMain.Start();

            FindBeadPoint();
            EnabledTurnBead(true);
        }

        // 隨機珠子顏色
        private void RandomBead(PictureBox p)
        {
            Random myRandom = new Random();

            int imageIndex = myRandom.Next(0, 22);

            switch (imageIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    p.Image = Resources.red_bead;
                    p.Image.Tag = "1";
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    p.Image = Resources.orange_bead;
                    p.Image.Tag = "2";
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    p.Image = Resources.green_bead;
                    p.Image.Tag = "3";
                    break;
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                    p.Image = Resources.white_bead;
                    p.Image.Tag = "4";
                    break;
                case 20:
                    p.Image = Resources.ice_bead;
                    p.Image.Tag = "5";
                    break;
                case 21:
                    p.Image = Resources.nuclear_bead;
                    p.Image.Tag = "6";
                    break;
            }
        }

        // 顯示珠子資訊
        private void DisplayBeadInfo()
        {
            labelCombo.Text = "Combo：" + combo;
        }

        // 找到移動後的珠子的位置
        private void FindBeadPoint()
        {
            for (int y = 0, k = 0; y <= 400; y += 100, k++)
            {
                for (int x = 0, r = 0; x <= 500; x += 100, r++)
                {
                    for (int i = 0; i < ROW; i++)
                    {
                        for (int j = 0; j < COLUMN; j++)
                        {
                            if (beadGrid[i, j].Location.X == x && beadGrid[i, j].Location.Y == y)
                            {
                                numberGrid[k, r] = beadGrid[i, j];
                            }
                        }
                    }
                }
            }
        }

        // 把相連可消除的珠子分在同一組編號
        private void BeadGroup()
        {
            // Reset
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    beadGroup[i, j] = 0;
                }
            }

            int groupNumber = 1;

            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    // 上
                    if (i - 1 >= 0)
                    {
                        if ((beadGroup[i - 1, j] != 0) && (numberGrid[i - 1, j].Image.Tag == numberGrid[i, j].Image.Tag))
                        {
                            for (int k = 0; k < ROW; k++)
                            {
                                for (int r = 0; r < COLUMN; r++)
                                {
                                    if (beadGroup[i, j] == beadGroup[k, r])
                                    { 
                                        beadGroup[k, r] = beadGroup[i - 1, j];
                                    }
                                }
                            }
                        }
                    }
                    // 左
                    if (j - 1 >= 0)
                    {
                        if ((beadGroup[i, j - 1] != 0) && (numberGrid[i, j - 1].Image.Tag == numberGrid[i, j].Image.Tag))
                        {
                            for (int k = 0; k < ROW; k++)
                            {
                                for (int r = 0; r < COLUMN; r++)
                                {
                                    if (beadGroup[i, j] == beadGroup[k, r])
                                    {
                                        beadGroup[k, r] = beadGroup[i, j - 1];
                                    }
                                }
                            }
                        }
                    }
                    // 下
                    if (i + 1 < ROW)
                    {
                        if ((beadGroup[i + 1, j] != 0) && (numberGrid[i + 1, j].Image.Tag == numberGrid[i, j].Image.Tag))
                        {
                            for (int k = 0; k < ROW; k++)
                            {
                                for (int r = 0; r < COLUMN; r++)
                                {
                                    if (beadGroup[i, j] == beadGroup[k, r])
                                    {
                                        beadGroup[k, r] = beadGroup[i + 1, j];
                                    }
                                }
                            }
                        }
                    }
                    // 右
                    if (j + 1 < COLUMN)
                    {
                        if ((beadGroup[i, j + 1] != 0) && (numberGrid[i, j + 1].Image.Tag == numberGrid[i, j].Image.Tag))
                        {
                            for (int k = 0; k < ROW; k++)
                            {
                                for (int r = 0; r < COLUMN; r++)
                                {
                                    if (beadGroup[i, j] == beadGroup[k, r])
                                    {
                                        beadGroup[k, r] = beadGroup[i, j + 1];
                                    }
                                }
                            }
                        }
                    }

                    if (beadGroup[i, j] == 0)
                    {
                        beadGroup[i, j] = groupNumber;
                    }

                    groupNumber++;
                }
            }
        }

        // 消除珠子
        private bool RemoveBead()
        {
            addComboFlag = false;
            
            for (int n = 1; n <= 30; n++)
            {
                bool comboAddFlag = false;
                PictureBox tempP = new PictureBox();

                for (int i = 0; i < ROW; i++)
                {
                    for (int j = 0; j < COLUMN - 2; j++)
                    {
                        if (beadGroup[i, j] == n && beadGroup[i, j + 1] == n && beadGroup[i, j + 2] == n)
                        {
                            numberGrid[i, j].Visible = false;
                            numberGrid[i, j + 1].Visible = false;
                            numberGrid[i, j + 2].Visible = false;

                            tempP = numberGrid[i, j];
                            comboAddFlag = true;
                        }
                    }
                }

                for (int i = 0; i < ROW - 2; i++)
                {
                    for (int j = 0; j < COLUMN; j++)
                    {
                        if (beadGroup[i, j] == n && beadGroup[i + 1, j] == n && beadGroup[i + 2, j] == n)
                        {
                            numberGrid[i, j].Visible = false;
                            numberGrid[i + 1, j].Visible = false;
                            numberGrid[i + 2, j].Visible = false;

                            tempP = numberGrid[i, j];
                            comboAddFlag = true;
                        }
                    }
                }

                if (comboAddFlag) 
                {
                    combo++;

                    labelCombo.Text = "Combo：" + combo;

                    addComboFlag = true;

                    switch (tempP.Image.Tag)
                    {
                        case "1":
                            red++;
                            break;
                        case "2":
                            orange++;
                            break;
                        case "3":
                            green++;
                            break;
                        case "4":
                            white++;
                            break;
                        case "5":
                            ice++;
                            break;
                        case "6":
                            nuclear++;
                            break;
                    }

                    GC.Collect();
                }
            }

            if (addComboFlag)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // 補滿被消除的珠子
        private void DropBead()
        {
            for (int j = 0; j < COLUMN; j++)
            {
                for (int i = ROW - 2; i >= 0; i--)
                {
                    if (numberGrid[i, j].Visible)
                    {
                        for (int k = i; k < ROW - 1; k++)
                        {
                            if (numberGrid[k + 1, j].Visible == false)
                            {
                                Point tempP = numberGrid[k, j].Location;
                                numberGrid[k, j].Location = numberGrid[k + 1, j].Location;
                                numberGrid[k + 1, j].Location = tempP;
                            }
                            else 
                            {
                                break;
                            }

                            FindBeadPoint();
                        }
                    }
                }
            }

            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    if (!numberGrid[i, j].Visible)
                    {
                        RandomBead(numberGrid[i, j]);

                        numberGrid[i, j].Visible = true;
                    }
                }
            }

            GC.Collect();
        }

        // 重置 combo 
        private void ResetComboCount()
        {
            combo = 0;
            red = 0; orange = 0; green = 0; white = 0; ice = 0; nuclear = 0;
        }

        // 延遲時間
        async Task PutTaskDelay500()
        {
            await Task.Delay(500);
        }

        async Task PutTaskDelay1000()
        {
            await Task.Delay(1000);
        }

        //  開關是否可轉珠
        private void EnabledTurnBead(bool flag)
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    if (flag)
                    {
                        numberGrid[i, j].Enabled = true;
                    }
                    else
                    {
                        numberGrid[i, j].Enabled = false;
                    }
                }
            }
        }

        // 觸發技能
        private async void TurnSkill()
        {
            // 回血
            if (myPlayer.HP > 0)
            {
                if ((myPlayer.HP + white * RESILIENCE) > PLAYER_HP)
                {
                    myPlayer.HP = PLAYER_HP;
                }
                else
                {
                    myPlayer.HP += white * RESILIENCE;
                }

                labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;
                float pictureboxHP = ((float)PICTUREBOX_WIDTH / PLAYER_HP) * myPlayer.HP;
                pictureBoxHP.Size = new Size((int)pictureboxHP, 25);
            }

            // 凍結
            if (ice > 0 && nuclear <= 0)
            {
                pictureBoxIce = new PictureBox();
                pictureBoxIce.Location = new Point(250, 50);
                pictureBoxIce.Image = Resources.ice;
                pictureBoxIce.Size = new Size(400, 400);
                pictureBoxIce.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxIce.Name = "Bomb";

                panelFight.Controls.Add(pictureBoxIce);

                timerMain.Stop();

                for (int i = 0; i < 3; i++)
                    await PutTaskDelay1000();

                pictureBoxIce.Dispose();
                timerMain.Start();
            }
        }

    }
}