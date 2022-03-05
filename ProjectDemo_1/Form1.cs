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

namespace ProjectDemo_1
{
    public partial class Form1 : Form
    {
        // 設定珠子行數、列數
        private const int ROW = 5, COLUMN = 6;
        // 設定珠子寬度、高度
        private const int WIDTH = 100, HEIGHT = 100;
        // 換珠子感應範圍
        private const int RANGE = 35;
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
        private int combo, red, orange, green, blue, purple;
        // 是否有新增 combo
        private bool addComboFlag = false;
        // 關卡秒數
        private const int LEVEL_TIME = 60;
        private int time = LEVEL_TIME;
        private int timeCount;
        // 設定玩家、怪物物件
        private Player myPlayer;
        private const int PLAYER_HP = 5000;
        private const int MONSTER_MAX = 200;
        private int monsterCount;
        private PictureBox[] monster = new PictureBox[MONSTER_MAX];
        private const int DAMAGE = 100;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitGrid();
            DisplayBeadInfo();

            SetObject();
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
                    await PutTaskDelay();

                    DropBead();
                    BeadGroup();

                    await PutTaskDelay();
                }

                DisplayBeadInfo();
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

        // 重新生成版面
        private void buttonReset_Click(object sender, EventArgs e)
        {
            panelGrid.Controls.Clear();

            InitGrid();
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
            int blueCount = 0;
            int purpleCount = 0;

            for (int i = 0; i < monster.Length; i++)
            {
                if (red > redCount && red > 0 && monster[i].Visible && monster[i].Image.Tag.ToString() == "1")
                {
                    monster[i].Image = Resources.explosion;
                    await PutTaskDelay();

                    monster[i].Visible = false;
                    monster[i].Dispose();
                    redCount++;
                }

                if (orange > orangeCount && orange > 0 && monster[i].Visible && monster[i].Image.Tag.ToString() == "2")
                {
                    monster[i].Image = Resources.explosion;
                    await PutTaskDelay();

                    monster[i].Visible = false;
                    monster[i].Dispose();
                    orangeCount++;
                }

                if (green > greenCount && green > 0 && monster[i].Visible && monster[i].Image.Tag.ToString() == "3")
                {
                    monster[i].Image = Resources.explosion;
                    await PutTaskDelay();

                    monster[i].Visible = false;
                    monster[i].Dispose();
                    greenCount++;
                }

                if (blue > blueCount && blue > 0 && monster[i].Visible && monster[i].Image.Tag.ToString() == "4")
                {
                    monster[i].Image = Resources.explosion;
                    await PutTaskDelay();

                    monster[i].Visible = false;
                    monster[i].Dispose();
                    blueCount++;
                }

                if (purple > purpleCount && purple > 0 && monster[i].Visible && monster[i].Image.Tag.ToString() == "5")
                {
                    monster[i].Image = Resources.explosion;
                    await PutTaskDelay();

                    monster[i].Visible = false;
                    monster[i].Dispose();
                    purpleCount++;
                }
            }
        }

        // 計時器
        private void timerMain_Tick(object sender, EventArgs e)
        {
            if (time == LEVEL_TIME)
            {
                monsterCount = 0;

                labelTimer.Text = "Time：" + time + " / " + LEVEL_TIME;
                float pictureboxTime = (940f / LEVEL_TIME) * time;
                pictureBoxTime.Size = new Size((int)pictureboxTime, 25);

                labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;
                float pictureboxHP = (940f / PLAYER_HP) * myPlayer.HP;
                pictureBoxHP.Size = new Size((int)pictureboxHP, 25);

                time--;
            }
            else if (time <= 0)
            {
                labelTimer.Text = "時間到！, 你贏了！";

                float pictureboxTime = (940f / LEVEL_TIME) * time;
                pictureBoxTime.Size = new Size((int)pictureboxTime, 25);

                labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;
                float pictureboxHP = (940f / PLAYER_HP) * myPlayer.HP;
                pictureBoxHP.Size = new Size((int)pictureboxHP, 25);

                timerMain.Stop();

                buttonStop.Enabled = false;
                buttonRestart.Enabled = true;
            }
            else
            {
                labelTimer.Text = "Time：" + time + " / " + LEVEL_TIME;

                float pictureboxTime = (940f / LEVEL_TIME) * time;

                pictureBoxTime.Size = new Size((int)pictureboxTime, 25);

                if (timeCount >= 100)
                { 
                    timeCount = 1;

                    Level_1(monsterCount);

                    time--;
                } 

                for (int i = 0; i < monster.Length; i++)
                {
                    if (monster[i].Visible)
                    {
                        monster[i].Location = new Point(monster[i].Location.X - 1,
                                                        monster[i].Location.Y);
                    }

                    if (monster[i].Location.X < 10 && monster[i].Visible)
                    {
                        monster[i].Visible = false;
                        monster[i].Dispose();

                        myPlayer.HP -= DAMAGE;

                        float pictureboxHP = (940f / PLAYER_HP) * myPlayer.HP;

                        pictureBoxHP.Size = new Size((int)pictureboxHP, 25);

                        if (myPlayer.HP <= 0)
                        {
                            labelTimer.Text = "你輸了！";

                            timerMain.Stop();

                            buttonStop.Enabled = false;
                            buttonRestart.Enabled = true;
                        }

                        labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;
                    }
                }
            }

            timeCount++;
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
            buttonReset.Enabled = false;
            buttonStop.Enabled = false;
            buttonRestart.Enabled = false;

            //Form.CheckForIllegalCrossThreadCalls = false;

            timeCount = 1;
            time = LEVEL_TIME;

            labelTimer.Text = "Time：" + time + " / " + LEVEL_TIME;
            float pictureboxTime = (940f / LEVEL_TIME) * time;
            pictureBoxTime.Size = new Size((int)pictureboxTime, 25);

            myPlayer = new Player("Hsu", PLAYER_HP);

            labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;
            float pictureboxHP = (940f / PLAYER_HP) * myPlayer.HP;
            pictureBoxHP.Size = new Size((int)pictureboxHP, 25);


            for (int i = 0; i < monster.Length; i++)
            {
                monster[i] = new PictureBox();
                monster[i].Name = "M" + i.ToString();

                Random myRandom = new Random();
                int randomPoint = myRandom.Next(1, 5);

                monster[i].Location = new Point(900, randomPoint * 100);
                monster[i].Size = new Size(50, 50);
                monster[i].SizeMode = PictureBoxSizeMode.Zoom;

                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(monster[i].ClientRectangle);
                Region reg = new Region(path);
                monster[i].Region = reg;

                int randomImage = myRandom.Next(0, 5);

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
                    case 3:
                        monster[i].Image = Resources.blue_bead;
                        monster[i].Image.Tag = "4";
                        break;
                    case 4:
                        monster[i].Image = Resources.purple_bead;
                        monster[i].Image.Tag = "5";
                        break;
                }

                monster[i].Visible = false;
            }

            GC.Collect();
        }

        // 開始遊戲
        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            buttonRestart.Enabled = false;

            timerMain.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            buttonRestart.Enabled = true;

            timerMain.Stop();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < monster.Length; i++)
            {
                monster[i].Dispose();
            }

            SetObject();

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            buttonRestart.Enabled = false;

            timerMain.Start();

        }

        // 隨機珠子顏色
        private void RandomBead(PictureBox p)
        {
            Random myRandom = new Random();

            int imageIndex = myRandom.Next(0, 5);

            switch (imageIndex)
            {
                case 0:
                    p.Image = Resources.red_bead;
                    p.Image.Tag = "1";
                    break;
                case 1:
                    p.Image = Resources.orange_bead;
                    p.Image.Tag = "2";
                    break;
                case 2:
                    p.Image = Resources.green_bead;
                    p.Image.Tag = "3";
                    break;
                case 3:
                    p.Image = Resources.blue_bead;
                    p.Image.Tag = "4";
                    break;
                case 4:
                    p.Image = Resources.purple_bead;
                    p.Image.Tag = "5";
                    break;
            }
        }

        // 顯示珠子資訊
        private void DisplayBeadInfo()
        {
            labelCombo.Text = "Combo: " + combo;

            labelColorCombo.Text = "Red: " + red + "\n" +
                                   "Orange: " + orange + "\n" +
                                   "Green: " + green + "\n" +
                                   "Blue: " + blue + "\n" +
                                   "Purple: " + purple;
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

                    labelCombo.Text = "Combo: " + combo;

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
                            blue++;
                            break;
                        case "5":
                            purple++;
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
            red = 0; orange = 0; green = 0; blue = 0; purple = 0;
        }

        // 延遲時間
        async Task PutTaskDelay()
        {
            await Task.Delay(500);
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

    }
}