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

        private const int ROW = 6, COLUME = 5;
        private const int WIDTH = 100, HEIGHT = 100;
        private const int TRIGGER_NUMBER = 50;
        
        private PictureBox[,] pictureBoxeGrid = new PictureBox[ROW, COLUME];
        private Label[,] labelGridNumber = new Label[ROW, COLUME];
        private Panel panelGrid;
        private PictureBox[,] numberGrid = new PictureBox[ROW, COLUME];
        private int[,] groupGrid = new int[ROW, COLUME];
        bool[,] beadFlag = new bool[ROW, COLUME];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            OutputPBGInfo();
        }

        private void InitializeGrid()
        {
            panelGrid = new Panel();
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(660, 550);
            panelGrid.Location = new Point(15, 100);

            int beadNumber = 0;

            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUME; j++)
                {
                    pictureBoxeGrid[i, j] = new PictureBox();
                    pictureBoxeGrid[i, j].Location = new Point(0 + i * WIDTH, 0 + j * HEIGHT);
                    pictureBoxeGrid[i, j].Size = new Size(WIDTH, HEIGHT);
                    pictureBoxeGrid[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBoxeGrid[i, j].Name = "pictureBoxeGrid" + beadNumber.ToString();

                    beadNumber++;

                    RandomBead(pictureBoxeGrid[i, j]);

                    pictureBoxeGrid[i, j].MouseUp += new MouseEventHandler(this.pictureBoxeGrid_MouseUp);
                    pictureBoxeGrid[i, j].MouseDown += new MouseEventHandler(this.pictureBoxeGrid_MouseDown);
                    pictureBoxeGrid[i, j].MouseMove += new MouseEventHandler(this.pictureBoxeGrid_MouseMove);

                    panelGrid.Controls.Add(pictureBoxeGrid[i, j]);

                }
            }

            this.Controls.Add(panelGrid);
        }

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

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            this.panelGrid.Dispose();

            InitializeGrid();
            OutputPBGInfo();
        }

        bool moveFlag = false;
        int startX, startY, nowX, nowY;

        private void pictureBoxeGrid_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUME; j++)
                {
                    if (p.Name == pictureBoxeGrid[i, j].Name)
                    {
                        startX = pictureBoxeGrid[i, j].Location.X;
                        startY = pictureBoxeGrid[i, j].Location.Y;

                        labelMousePositionX.Text = startX.ToString();
                        labelMousePositionY.Text = startY.ToString();

                        labelState.Text = "Down";

                        moveFlag = true;
                    }
                }
            }
        }

        private void pictureBoxeGrid_MouseUp(object sender, MouseEventArgs e)
        {
            moveFlag = false;

            PictureBox p = (PictureBox)sender;

            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUME; j++)
                {
                    if ((nowX >= pictureBoxeGrid[i, j].Location.X - TRIGGER_NUMBER && nowX <= pictureBoxeGrid[i, j].Location.X + TRIGGER_NUMBER) &&
                        (nowY >= pictureBoxeGrid[i, j].Location.Y - TRIGGER_NUMBER && nowY <= pictureBoxeGrid[i, j].Location.Y + TRIGGER_NUMBER))
                    {
                        labelMousePositionX.Text = p.Name;
                        labelMousePositionY.Text = pictureBoxeGrid[i, j].Name;

                        labelState.Text = "Up";

                        if (p.Name == pictureBoxeGrid[i, j].Name)
                        {
                            p.Location = new Point(startX, startY);
                        }
                        else
                        {
                            p.Location = pictureBoxeGrid[i, j].Location;
                        }

                        OutputPBGInfo();
                        //CalculateGroup();
                        CalculateGroup2();
                    }
                }
            }

        }

        private void pictureBoxeGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveFlag)
            {
                PictureBox p = (PictureBox)sender;

                Int32 LocationX = Form1.MousePosition.X, LocationY = Form1.MousePosition.Y;

                nowX = LocationX - this.Location.X - panelGrid.Location.X - 60;
                nowY = LocationY - this.Location.Y - panelGrid.Location.Y - 60;

                p.Location = new Point(nowX, nowY);

                labelMousePositionX.Text = "X:" + p.Location.X.ToString();
                labelMousePositionY.Text = "Y:" + p.Location.Y.ToString();

                for (int i = 0; i < ROW; i++)
                {
                    for (int j = 0; j < COLUME; j++)
                    {
                        if (p.Name != this.pictureBoxeGrid[i, j].Name)
                        {
                            if ((nowX >= pictureBoxeGrid[i, j].Location.X - TRIGGER_NUMBER && nowX <= pictureBoxeGrid[i, j].Location.X + TRIGGER_NUMBER) &&
                                (nowY >= pictureBoxeGrid[i, j].Location.Y - TRIGGER_NUMBER && nowY <= pictureBoxeGrid[i, j].Location.Y + TRIGGER_NUMBER))
                            {
                                labelState.Text = "Change";

                                int tempX = pictureBoxeGrid[i, j].Location.X;
                                int tempY = pictureBoxeGrid[i, j].Location.Y;
                                pictureBoxeGrid[i, j].Location = new Point(startX, startY);
                                startX = tempX;
                                startY = tempY;
                            }
                        }
                    }
                }
            }
        }

        private void OutputPBGInfo()
        {
            labelTest.Text = "";
            labelTest2.Text = "";
            labelTest3.Text = "";

            for (int i = 0; i < COLUME; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    labelTest.Text += pictureBoxeGrid[j, i].Name + " ";
                    labelTest2.Text += pictureBoxeGrid[j, i].Location + " ";
                }
                labelTest.Text += "\n";
                labelTest2.Text += "\n";
            }

            for (int y = 0, k = 0; y <= 400; y += 100, k++)
            {
                for (int x = 0, r = 0; x <= 500; x += 100, r++)
                {
                    for (int i = 0; i < ROW; i++)
                    {
                        for (int j = 0; j < COLUME; j++)
                        {
                            if (pictureBoxeGrid[i, j].Location.X == x && pictureBoxeGrid[i, j].Location.Y == y)
                            {
                                numberGrid[r, k] = pictureBoxeGrid[i, j];
                            }
                        }
                        
                    }
                }
            }

            for (int i = 0; i < COLUME; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    labelTest3.Text += numberGrid[j, i].Image.Tag + " ";
                }
                labelTest3.Text += "\n";
            }
        }

        private void CalculateGroup()
        {
            int count = 1;

            for (int i = 0; i < COLUME; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    beadFlag[j, i] = false;
                    groupGrid[j, i] = 0;
                }
            }

            for (int i = 0; i < COLUME; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    if (groupGrid[j, i] == 0)
                    {
                        string tempStr = numberGrid[j, i].Image.Tag.ToString();
                        int tempI = i;
                        int tempJ = j;
                        beadFlag[j, i] = true;
                        groupGrid[j, i] = count;

                        if (tempJ - 1 >= 0)
                        {
                            int N = tempJ - 1;
                            while (!beadFlag[N, tempI])
                            {
                                if (numberGrid[N, tempI].Image.Tag.ToString() != tempStr) break;
                                else
                                {
                                    beadFlag[N, tempI] = true;
                                    groupGrid[N, tempI] = count;
                                }

                                if (N - 1 < 0) break;
                                else N -= 1;
                            }
                        }

                        if (tempJ + 1 < ROW)
                        {
                            int S = tempJ + 1;
                            while (!beadFlag[S, tempI])
                            {
                                if (numberGrid[S, tempI].Image.Tag.ToString() != tempStr) break;
                                else
                                {
                                    beadFlag[S, tempI] = true;
                                    groupGrid[S, tempI] = count;
                                }

                                if (S + 1 >= ROW) break;
                                else S += 1;
                            }
                        }

                        if (tempI - 1 >= 0)
                        {
                            int W = tempI - 1;
                            while (!beadFlag[tempJ, W])
                            {
                                if (numberGrid[tempJ, W].Image.Tag.ToString() != tempStr) break;
                                else
                                {
                                    beadFlag[tempJ, W] = true;
                                    groupGrid[tempJ, W] = count;
                                }

                                if (W - 1 < 0) break;
                                else W -= 1;
                            }
                        }

                        if (tempI + 1 < COLUME)
                        {
                            int E = tempI + 1;
                            while (!beadFlag[tempJ, E])
                            {
                                if (numberGrid[tempJ, E].Image.Tag.ToString() != tempStr) break;
                                else
                                {
                                    beadFlag[tempJ, E] = true;
                                    groupGrid[tempJ, E] = count;
                                }

                                if (E + 1 >= COLUME) break;
                                else E += 1;
                            }
                        }

                        count++;
                    }
                }
            }

            labelTest4.Text = "";

            for (int i = 0; i < COLUME; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    labelTest4.Text += groupGrid[j, i] + " " + beadFlag[j, i] + " ";
                }
                labelTest4.Text += "\n";
            }
        }

        private void CalculateGroup2()
        {
            int groupNumber = 1;

            for (int i = 0; i < COLUME; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    beadFlag[j, i] = false;
                    groupGrid[j, i] = 0;
                }
            }

            for (int i = 0; i < COLUME; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    
                }
            }

            labelTest4.Text = "";

            for (int i = 0; i < COLUME; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    labelTest4.Text += groupGrid[j, i] + " " + beadFlag[j, i] + " ";
                }
                labelTest4.Text += "\n";
            }
        }
    }
}