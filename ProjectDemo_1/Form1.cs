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
        private int combo, bullet, bomb, white, ice, nuclear, virus;
        // 是否有新增 combo
        private bool addComboFlag = false;
        // 關卡秒數
        private int time;
        // 生怪秒數
        private int monsterTime;
        // 升級速度
        private const int SPEED_TIME = 300;
        // 設定玩家、怪物物件
        private const int MONSTER_MAX = 100;
        private Player myPlayer;
        private Monster[] myMonster = new Monster[MONSTER_MAX];
        private Label[] labelMonsterHP = new Label[MONSTER_MAX];
        private const int PLAYER_HP = 1000000;
        private const int PICTUREBOX_WIDTH = 940;
        private const int LABEL_HP_WIDTH = 40;
        private int monsterCount;
        private PictureBox[] monster = new PictureBox[MONSTER_MAX];
        private const int DAMAGE = 100;
        private int speed;
        private const int SPEED_MAX = 50;
        private const int SPEED_START = 1;
        private const int DOMA_HP = 300, GIANT_HP = 1000, FIRE_HP = 500;
        private const int DOMA_POWER = 100, GIANT_POWER = 1000, FIRE_POWER = 3000;
        // 核彈、凍結特效
        private PictureBox pictureBoxBomb, pictureBoxIce, pictureBoxVirus;
        // 回復力
        private const int RESILIENCE = 100;
        // 起手珠
        private PictureBox startBead;
        private int autoStartX, autoStartY;
        // 自動轉珠路徑
        private string autoPath = "";
        // 移動步數
        private int stepCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitGrid();
            DisplayBeadInfo();

            SetObject();
            SetMonster();

            FindBeadPoint();
            EnabledTurnBead(false);

            SetLayout();

            EnabledButton(true);
        }

        // 設定遊戲中的介面
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

            path.AddEllipse(pictureBoxAuto.ClientRectangle);
            pictureBoxAuto.Region = reg;

            pictureBoxStart.Click += new System.EventHandler(this.buttonStart_Click);
            pictureBoxStop.Click += new System.EventHandler(this.buttonStop_Click);
            pictureBoxRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            pictureBoxAuto.Click += new System.EventHandler(this.buttonBoxAuto_Click);
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

                EnabledButton(false);
                FindBeadPoint();
                EnabledTurnBead(false);
                BeadGroup();
                ResetComboCount();

                while (RemoveBead())
                {
                    await PutTaskDelay(500);

                    DropBead();
                    BeadGroup();

                    await PutTaskDelay(500);
                }

                DisplayBeadInfo();
                TurnSkill();
                RemoveMonster();
                EnabledTurnBead(true);
                EnabledButton(true);
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

        // 攻擊怪物
        private async void RemoveMonster()
        {
            int bombCount = 0;
            int bulletCount = 0;

            // 核彈攻擊
            if (nuclear > 0)
            {
                pictureBoxBomb = new PictureBox();
                pictureBoxBomb.Location = new Point(250, 75);
                pictureBoxBomb.Image = Resources.nuclear_bomb;
                pictureBoxBomb.Size = new Size(400, 400);
                pictureBoxBomb.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxBomb.Name = "NuclearBomb";

                panelFight.Controls.Add(pictureBoxBomb);

                await PutTaskDelay(1000);

                pictureBoxBomb.Image = Resources.boom;

                for (int i = 0; i < monster.Length; i++)
                {
                    if (monster[i].Visible)
                    {
                        myMonster[i].HP -= 10000;

                        if (myMonster[i].HP < 0)
                        {
                            labelMonsterHP[i].Text = "0";
                            float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * 0;
                            labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                        }
                        else
                        { 
                            labelMonsterHP[i].Text = myMonster[i].HP.ToString();
                            float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * myMonster[i].HP;
                            labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                        }

                        if (myMonster[i].HP <= 0)
                        {
                            monster[i].Image = Resources.explosion;
                        }
                    }
                }

                await PutTaskDelay(1000);

                for (int i = 0; i < monster.Length; i++)
                {
                    if (monster[i].Visible)
                    {
                        if (myMonster[i].HP <= 0)
                        { 
                            monster[i].Visible = false;
                            monster[i].Dispose();

                            labelMonsterHP[i].Visible = false;
                            labelMonsterHP[i].Dispose();

                            myPlayer.Score += 100;
                        }
                    }
                }

                pictureBoxBomb.Dispose();
            } 
            else
            {
                // 病毒攻擊
                for (int i = 0; i < monster.Length; i++)
                {
                    if (nuclear <= 0)
                    {
                        if (virus > 0 && monster[i].Visible)
                        {
                            myMonster[i].Infected = true;

                            if (myMonster[i].Type == "Doma")
                            {
                                monster[i].Image = Resources.doma_infected;
                            }
                            else if (myMonster[i].Type == "Giant")
                            {
                                monster[i].Image = Resources.giant_infected;
                            }
                            else
                            {
                                monster[i].Image = Resources.fire_infected;
                            }

                            if (myMonster[i].HP < 0)
                            {
                                labelMonsterHP[i].Text = "0";
                                float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * 0;
                                labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                            }
                            else
                            {
                                labelMonsterHP[i].Text = myMonster[i].HP.ToString();
                                float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * myMonster[i].HP;
                                labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                            }

                            if (myMonster[i].HP <= 0)
                            {
                                monster[i].Image = Resources.explosion;
                                await PutTaskDelay(500);

                                monster[i].Visible = false;
                                monster[i].Dispose();

                                labelMonsterHP[i].Visible = false;
                                labelMonsterHP[i].Dispose();

                                myPlayer.Score += 100;
                            }
                        }
                    }
                }

                // 子彈攻擊
                for (int j = 0; j < 900; j++)
                {
                    for (int i = 0; i < monster.Length; i++)
                    {
                        if (nuclear <= 0)
                        {
                            if (bullet > bulletCount && bullet > 0 && monster[i].Visible && monster[i].Location.X == j)
                            {
                                myMonster[i].HP -= 100;

                                if (myMonster[i].HP < 0)
                                {
                                    labelMonsterHP[i].Text = "0";
                                    float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * 0;
                                    labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                                }
                                else
                                {
                                    labelMonsterHP[i].Text = myMonster[i].HP.ToString();
                                    float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * myMonster[i].HP;
                                    labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                                }

                                if (myMonster[i].HP <= 0)
                                {
                                    monster[i].Image = Resources.explosion;
                                    await PutTaskDelay(500);

                                    monster[i].Visible = false;
                                    monster[i].Dispose();

                                    labelMonsterHP[i].Visible = false;
                                    labelMonsterHP[i].Dispose();

                                    myPlayer.Score += 100;
                                }

                                bulletCount++;
                            }
                        }
                    }
                }

                // 炸彈攻擊
                for (int j = 0; j < 900; j++)
                {
                    for (int i = 0; i < monster.Length; i++)
                    {
                        if (nuclear <= 0)
                        {
                            if (bomb > bombCount && bomb > 0 && monster[i].Visible && monster[i].Location.X == j)
                            {
                                myMonster[i].HP -= 300;

                                if (myMonster[i].HP < 0)
                                {
                                    labelMonsterHP[i].Text = "0";
                                    float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * 0;
                                    labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                                }
                                else
                                {
                                    labelMonsterHP[i].Text = myMonster[i].HP.ToString();
                                    float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * myMonster[i].HP;
                                    labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                                }

                                if (myMonster[i].HP <= 0)
                                {
                                    monster[i].Image = Resources.explosion;
                                    await PutTaskDelay(500);

                                    monster[i].Visible = false;
                                    monster[i].Dispose();

                                    labelMonsterHP[i].Visible = false;
                                    labelMonsterHP[i].Dispose();

                                    myPlayer.Score += 100;
                                }

                                bombCount++;
                            }
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

                // 生怪
                if (time % monsterTime == 0)
                {
                    if (monsterCount >= MONSTER_MAX)
                    {
                        for (int i = 0; i <= monster.Length; i++)
                        {
                            if (i == monster.Length)
                            {
                                monsterCount = 0;

                                SetMonster();

                                if (speed < SPEED_MAX)
                                {
                                    speed += 1;

                                    labelSpeed.Text = "Speed：" + speed;
                                }
                            }
                            else
                            {
                                if (monster[i].Visible)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    Level_1(monsterCount);

                    if (time > SPEED_TIME)
                    {
                        time = 1;
                    }
                }

                // 有染疫的怪物每秒扣血
                if (time % 10 == 0)
                {
                    for (int i = 0; i < monster.Length; i++)
                    {
                        if (myMonster[i].Infected && monster[i].Visible)
                        {
                            myMonster[i].HP -= 50;

                            if (myMonster[i].HP < 0)
                            {
                                labelMonsterHP[i].Text = "0";
                                float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * 0;
                                labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                            }
                            else
                            {
                                labelMonsterHP[i].Text = myMonster[i].HP.ToString();
                                float labelHP = ((float)LABEL_HP_WIDTH / myMonster[i].HP_Max) * myMonster[i].HP;
                                labelMonsterHP[i].Size = new Size((int)labelHP, 10);
                            }

                            if (myMonster[i].HP <= 0)
                            {
                                monster[i].Image = Resources.explosion;
                                //await PutTaskDelay(500);

                                monster[i].Visible = false;
                                monster[i].Dispose();

                                labelMonsterHP[i].Visible = false;
                                labelMonsterHP[i].Dispose();

                                myPlayer.Score += 100;
                            }
                        }
                    }
                }

                for (int i = 0; i < monster.Length; i++)
                {
                    if (monster[i].Visible)
                    {
                        monster[i].Location = new Point(monster[i].Location.X - myMonster[i].Speed * speed,
                                                        monster[i].Location.Y);
                        labelMonsterHP[i].Location = new Point(labelMonsterHP[i].Location.X - myMonster[i].Speed * speed,
                                                               labelMonsterHP[i].Location.Y);
                    }

                    if (monster[i].Location.X < 10 && monster[i].Visible)
                    {
                        monster[i].Visible = false;
                        monster[i].Dispose();

                        labelMonsterHP[i].Visible = false;
                        labelMonsterHP[i].Dispose();

                        myPlayer.HP -= myMonster[i].Power;

                        float pictureboxHP = ((float)PICTUREBOX_WIDTH / PLAYER_HP) * myPlayer.HP;
                        pictureBoxHP.Size = new Size((int)pictureboxHP, 25);

                        if (myPlayer.HP <= 0)
                        {
                            timerMain.Stop();
                            panelGrid.Controls.Clear();
                            //panelFight.Controls.Clear();

                            for (int j = 0; j < monster.Length; j++)
                            {
                                if (monster[j].Visible)
                                {
                                    monster[j].Dispose();
                                    labelMonsterHP[j].Dispose();
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

                labelMonsterHP[number].Visible = true;
                labelMonsterHP[number].BringToFront();

                panelFight.Controls.Add(monster[number]);
                panelFight.Controls.Add(labelMonsterHP[number]);

                monsterCount++;
            }
        }

        // 設定物件
        private void SetObject()
        {
            pictureBoxStop.Visible = false;
            pictureBoxRestart.Visible = false;
            pictureBoxAuto.Visible = false;

            //Form.CheckForIllegalCrossThreadCalls = false;

            time = 0;
            speed = 0;

            myPlayer = new Player("Hsu", PLAYER_HP, 0);

            labelGamePoint.Text = "Score：" + myPlayer.Score;

            labelHP.Text = "HP：" + myPlayer.HP + " / " + PLAYER_HP;
            float pictureboxHP = ((float)PICTUREBOX_WIDTH / PLAYER_HP) * myPlayer.HP;
            pictureBoxHP.Size = new Size((int)pictureboxHP, 25);

            labelSpeed.Text = "Speed：" + speed;
        }

        // 設定怪物生成
        private void SetMonster()
        {
            for (int i = 0; i < monster.Length; i++)
            {
                Random myRandom = new Random();

                //int randomHP = myRandom.Next(300, 501);

                labelMonsterHP[i] = new Label();

                monster[i] = new PictureBox();
                monster[i].Name = "M" + i.ToString();

                int randomPoint = myRandom.Next(1, 4);

                monster[i].Location = new Point(900, randomPoint * 100);
                monster[i].Size = new Size(60, 60);
                monster[i].SizeMode = PictureBoxSizeMode.Zoom;

                labelMonsterHP[i].Location = new Point(915, randomPoint * 100 + 60);
                labelMonsterHP[i].Size = new Size(LABEL_HP_WIDTH, 10);
                labelMonsterHP[i].ForeColor = Color.Black;
                labelMonsterHP[i].BackColor = Color.Red;
                labelMonsterHP[i].Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(monster[i].ClientRectangle);
                Region reg = new Region(path);
                monster[i].Region = reg;

                int randomImage = myRandom.Next(0, 3);

                switch (randomImage)
                {
                    case 0:
                        myMonster[i] = new Monster("M" + i.ToString(), "Doma", DOMA_HP, false, DOMA_POWER);

                        monster[i].Image = Resources.doma;
                        monster[i].Image.Tag = "1";
                        break;
                    case 1:
                        myMonster[i] = new Monster("M" + i.ToString(), "Giant", GIANT_HP, false, GIANT_POWER);

                        monster[i].Image = Resources.giant;
                        monster[i].Image.Tag = "2";
                        break;
                    case 2:
                        myMonster[i] = new Monster("M" + i.ToString(), "Fire", FIRE_HP, false, FIRE_POWER);

                        monster[i].Image = Resources.fire;
                        monster[i].Image.Tag = "3";
                        break;
                }

                labelMonsterHP[i].Text = myMonster[i].HP.ToString();

                monster[i].Visible = false;
                labelMonsterHP[i].Visible = false;
            }

            GC.Collect();
        }

        // 開始遊戲
        private void buttonStart_Click(object sender, EventArgs e)
        {
            pictureBoxStart.Visible = false;
            pictureBoxStop.Visible = true;
            pictureBoxRestart.Visible = false;
            pictureBoxAuto.Visible = true;

            timerMain.Start();

            EnabledTurnBead(true);
        }

        // 自動轉珠
        private void buttonBoxAuto_Click(object sender, EventArgs e)
        {
            // 開始
            EnabledTurnBead(false);

            // 找左下角珠子當起手珠
            startBead = new PictureBox();
            startBead = beadGrid[4, 0];
            startBead.Image = Resources.red_bead;
            startBead.BringToFront();

            autoStartX = startBead.Location.X;
            autoStartY = startBead.Location.Y;

            // 產生自動轉珠路徑
            autoPath = GeneratePath();
            autoPath = "NNNNESSSSENNNNESSSSENNNNESSSSWWWWW";
            stepCount = 0;

            // 開啟自動轉珠計時器
            timerAuto.Start();

            // 結束
            EnabledTurnBead(true);
        }

        private string GeneratePath()
        {
            // 

            return "";
        }

        // 自動轉珠 計時器
        private void timerAuto_Tick(object sender, EventArgs e)
        {
            string tempPath = autoPath.Substring(stepCount, 1);

            switch (tempPath)
            {
                case "N":
                    startBead.Location = new Point(startBead.Location.X, startBead.Location.Y - 100);
                    break;
                case "S":
                    startBead.Location = new Point(startBead.Location.X, startBead.Location.Y + 100);
                    break;
                case "W":
                    startBead.Location = new Point(startBead.Location.X - 100, startBead.Location.Y);
                    break;
                case "E":
                    startBead.Location = new Point(startBead.Location.X + 100, startBead.Location.Y);
                    break;
            }

            AutoChange();
            FindBeadPoint();

            stepCount++;

            if (stepCount == autoPath.Length)
            {
                timerAuto.Stop();
            }
            

        }

        // 自動轉珠 交換
        private void AutoChange()
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    if (beadGrid[i, j].Location == startBead.Location && beadGrid[i, j].Name != startBead.Name)
                    {
                        beadGrid[i, j].Location = new Point(autoStartX, autoStartY);
                        autoStartX = startBead.Location.X;
                        autoStartY = startBead.Location.Y;
                    }
                }
            }
        }

        // 暫停遊戲
        private void buttonStop_Click(object sender, EventArgs e)
        {
            pictureBoxStart.Visible = true;
            pictureBoxStop.Visible = false;
            pictureBoxRestart.Visible = true;
            pictureBoxAuto.Visible = false;

            timerMain.Stop();

            EnabledTurnBead(false);
        }

        // 重新開始
        private void buttonRestart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < monster.Length; i++)
            {
                monster[i].Dispose();
                labelMonsterHP[i].Dispose();
            }

            panelGrid.Controls.Clear();
            InitGrid();

            SetObject();
            SetMonster();

            pictureBoxStart.Visible = false;
            pictureBoxStop.Visible = true;
            pictureBoxRestart.Visible = false;
            pictureBoxAuto.Visible = true;

            timerMain.Start();

            FindBeadPoint();
            EnabledTurnBead(true);
        }

        // 隨機珠子顏色
        private void RandomBead(PictureBox p)
        {
            Random myRandom = new Random();

            int imageIndex = myRandom.Next(0, 100);

            if (0 <= imageIndex && imageIndex < 40)
            {
                p.Image = Resources.bullet_bead;
                p.Image.Tag = "1";
            }
            else if (40 <= imageIndex && imageIndex < 70)
            {
                p.Image = Resources.bomb_bead;
                p.Image.Tag = "2";
            }
            else if (70 <= imageIndex && imageIndex < 95)
            {
                p.Image = Resources.white_bead;
                p.Image.Tag = "3";
            }
            else if (95 <= imageIndex && imageIndex < 97)
            {
                p.Image = Resources.ice_bead;
                p.Image.Tag = "4";
            }
            else if (97 <= imageIndex && imageIndex < 98)
            {
                p.Image = Resources.nuclear_bead;
                p.Image.Tag = "5";
            }
            else
            {
                p.Image = Resources.virus_bead;
                p.Image.Tag = "6";
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
                            if (numberGrid[i, j].Visible)
                            {
                                TotalBead(numberGrid[i, j].Image.Tag.ToString());
                            }
                            if (numberGrid[i, j + 1].Visible)
                            {
                                TotalBead(numberGrid[i, j + 1].Image.Tag.ToString());
                            }
                            if (numberGrid[i, j + 2].Visible)
                            {
                                TotalBead(numberGrid[i, j + 2].Image.Tag.ToString());
                            }

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
                            if (numberGrid[i, j].Visible)
                            {
                                TotalBead(numberGrid[i, j].Image.Tag.ToString());
                            }
                            if (numberGrid[i + 1, j].Visible)
                            {
                                TotalBead(numberGrid[i + 1, j].Image.Tag.ToString());
                            }
                            if (numberGrid[i + 2, j].Visible)
                            {
                                TotalBead(numberGrid[i + 2, j].Image.Tag.ToString());
                            }

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

        // 加總各珠子的數量
        private void TotalBead(string imageTag)
        {
            switch (imageTag)
            {
                case "1":
                    bullet++;
                    break;
                case "2":
                    bomb++;
                    break;
                case "3":
                    white++;
                    break;
                case "4":
                    ice++;
                    break;
                case "5":
                    nuclear++;
                    break;
                case "6":
                    virus++;
                    break;
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
            bullet = 0; bomb = 0; white = 0; ice = 0; nuclear = 0; virus = 0;
        }

        // 延遲時間
        async Task PutTaskDelay(int ms)
        {
            await Task.Delay(ms);
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

        // 開關選項鈕
        private void EnabledButton(bool flag)
        {
            pictureBoxRestart.Enabled = flag;
            pictureBoxStart.Enabled = flag;
            pictureBoxStop.Enabled = flag;
            pictureBoxAuto.Enabled = flag;
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
                pictureBoxIce.Name = "Ice";

                panelFight.Controls.Add(pictureBoxIce);

                timerMain.Stop();

                await PutTaskDelay(3000);

                pictureBoxIce.Dispose();
                timerMain.Start();
            }

            // 病毒
            if (virus > 0 && nuclear <= 0)
            {
                pictureBoxVirus = new PictureBox();
                pictureBoxVirus.Location = new Point(250, 50);
                pictureBoxVirus.Image = Resources.virus;
                pictureBoxVirus.Size = new Size(400, 400);
                pictureBoxVirus.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxVirus.Name = "Virus";

                panelFight.Controls.Add(pictureBoxVirus);

                await PutTaskDelay(1000);

                pictureBoxVirus.Dispose();
            }
        }

    }
}