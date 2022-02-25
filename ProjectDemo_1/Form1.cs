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
using System.Threading;

namespace ProjectDemo_1
{
    public partial class Form1 : Form
    {
        // 設定珠子行數、列數
        private const int COLUMN = 5, ROW = 6;
        // 設定珠子寬度、高度
        private const int WIDTH = 100, HEIGHT = 100;
        // 換珠子感應範圍
        private const int RANGE = 30;
        // 初始化珠子
        private PictureBox[,] beadGrid = new PictureBox[COLUMN, ROW];
        // 校正珠子位置
        private PictureBox[,] numberGrid = new PictureBox[COLUMN, ROW];
        // 分組編號
        private int[,] beadGroup = new int[COLUMN, ROW];
        // 暫存移動珠子物件
        private PictureBox fingerPictureBox;
        // 控制珠子是否移動
        private bool moveFlag = false;
        // 是否有移動珠子
        private int moveCount = 0;
        // 記錄消除珠子數量、種類
        private int combo, red, orange, green, blue, purple;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            p.Visible = true;

            moveFlag = false;
            fingerPictureBox.Dispose();

            labelTest2.Text = "Up";

            if (moveCount != 0)
            {
                for (int i = 0; i < COLUMN; i++)
                {
                    for (int j = 0; j < ROW; j++)
                    {
                        if ((beadGrid[i, j].Location.X - RANGE <= fingerPictureBox.Location.X && beadGrid[i, j].Location.X + RANGE >= fingerPictureBox.Location.X) &&
                            (beadGrid[i, j].Location.Y - RANGE <= fingerPictureBox.Location.Y && beadGrid[i, j].Location.Y + RANGE >= fingerPictureBox.Location.Y))
                        {
                            p.Location = beadGrid[i, j].Location;
                        }
                    }
                }

                DisplayBeadInfo();
                FindBeadPoint();
                DisplayBeadInfo2();
                BeadGroup();
                DisplayBeadInfo3();

                if (RemoveBead())
                {
                    DropBead();
                }

                DisplayBeadInfo4();
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveFlag)
            {
                PictureBox p = (PictureBox)sender;

                int mouseLocationX = Form1.MousePosition.X - this.Location.X - panelGrid.Location.X - 60;
                int mouseLocationY = Form1.MousePosition.Y - this.Location.Y - panelGrid.Location.Y - 60;

                labelTest.Text = "X: " + mouseLocationX + ", Y: " + mouseLocationY;

                fingerPictureBox.Location = new Point(mouseLocationX, mouseLocationY);

                for (int i = 0; i < COLUMN; i++)
                {
                    for (int j = 0; j < ROW; j++)
                    {
                        if ((beadGrid[i, j].Location.X - RANGE <= fingerPictureBox.Location.X && beadGrid[i, j].Location.X + RANGE >= fingerPictureBox.Location.X) &&
                            (beadGrid[i, j].Location.Y - RANGE <= fingerPictureBox.Location.Y && beadGrid[i, j].Location.Y + RANGE >= fingerPictureBox.Location.Y))
                        {
                            if (beadGrid[i, j].Name != p.Name)
                            { 
                                labelTest2.Text = "Change " + beadGrid[i, j].Name + " " + p.Name;

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

            labelTest.Text = "X: " + mouseLocationX + ", Y: " + mouseLocationY;

            fingerPictureBox.Location = new Point(mouseLocationX, mouseLocationY);
            fingerPictureBox.BringToFront();

            labelTest2.Text = "Down";

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
            for (int i = 0; i < COLUMN; i++)
            {
                for (int j = 0; j < ROW; j++)
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
            labelTest3.Text = "";

            for (int i = 0; i < COLUMN; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    labelTest3.Text += beadGrid[i, j].Location;
                }
                labelTest3.Text += "\n";
            }
        }

        // 顯示珠子資訊 2
        private void DisplayBeadInfo2()
        {
            labelTest4.Text = "";

            for (int i = 0; i < COLUMN; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    labelTest4.Text += numberGrid[i, j].Image.Tag.ToString() + " ";
                }
                labelTest4.Text += "\n";
            }
        }

        // 顯示珠子資訊 3
        private void DisplayBeadInfo3()
        {
            labelTest5.Text = "";

            for (int i = 0; i < COLUMN; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    labelTest5.Text += beadGroup[i, j].ToString() + " ";
                }
                labelTest5.Text += "\n";
            }
        }

        // 顯示珠子資訊 4
        private void DisplayBeadInfo4()
        {
            labelTest6.Text = "";

            for (int i = 0; i < COLUMN; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    labelTest6.Text += numberGrid[i, j].Visible.ToString() + " ";
                }
                labelTest6.Text += "\n";
            }

            labelTest7.Text = "Combo: " + combo + "\n\n" + 
                               "Red: " + red + "\n" +
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
                    for (int i = 0; i < COLUMN; i++)
                    {
                        for (int j = 0; j < ROW; j++)
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
            for (int i = 0; i < COLUMN; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    beadGroup[i, j] = 0;
                }
            }

            int groupNumber = 1;

            for (int i = 0; i < COLUMN; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    // 上
                    if (i - 1 >= 0)
                    {
                        if ((beadGroup[i - 1, j] != 0) && (numberGrid[i - 1, j].Image.Tag == numberGrid[i, j].Image.Tag))
                        {
                            for (int k = 0; k < COLUMN; k++)
                            {
                                for (int r = 0; r < ROW; r++)
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
                            for (int k = 0; k < COLUMN; k++)
                            {
                                for (int r = 0; r < ROW; r++)
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
                    if (i + 1 < COLUMN)
                    {
                        if ((beadGroup[i + 1, j] != 0) && (numberGrid[i + 1, j].Image.Tag == numberGrid[i, j].Image.Tag))
                        {
                            for (int k = 0; k < COLUMN; k++)
                            {
                                for (int r = 0; r < ROW; r++)
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
                    if (j + 1 < ROW)
                    {
                        if ((beadGroup[i, j + 1] != 0) && (numberGrid[i, j + 1].Image.Tag == numberGrid[i, j].Image.Tag))
                        {
                            for (int k = 0; k < COLUMN; k++)
                            {
                                for (int r = 0; r < ROW; r++)
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
            combo = 0;
            red = 0; orange = 0; green = 0; blue = 0; purple = 0;

            for (int n = 1; n <= 30; n++)
            {
                bool comboAddFlag = false;
                PictureBox tempP = new PictureBox();

                for (int i = 0; i < COLUMN; i++)
                {
                    for (int j = 0; j < ROW - 2; j++)
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

                for (int i = 0; i < COLUMN - 2; i++)
                {
                    for (int j = 0; j < ROW; j++)
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

                    Thread.Sleep(200);
                }
            }

            if (combo == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // 補滿被消除的珠子
        private void DropBead()
        { 
            
        }
    }
}