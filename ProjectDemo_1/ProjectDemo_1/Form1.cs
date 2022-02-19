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
        private int[,] beadLocationX = new int[ROW, COLUME];
        private int[,] beadLocationY = new int[ROW, COLUME];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeGrid();
        }

        private void InitializeGrid() 
        {
            this.panelGrid = new Panel();
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new Size(660, 550);
            this.panelGrid.Location = new Point(15, 100);

            int beadNumber = 0;

            for (int i = 0; i < ROW; i++) 
            {
                for (int j = 0; j < COLUME; j++) 
                {
                    this.pictureBoxeGrid[i, j] = new PictureBox();
                    this.pictureBoxeGrid[i, j].Location = new Point(0 + i * WIDTH, 0 + j * HEIGHT);
                    this.pictureBoxeGrid[i, j].Size = new Size(WIDTH, HEIGHT);
                    this.pictureBoxeGrid[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    this.pictureBoxeGrid[i, j].Name = "pictureBoxeGrid" + beadNumber.ToString();

                    beadNumber++;

                    RandomBead(this.pictureBoxeGrid[i, j]);

                    this.pictureBoxeGrid[i, j].MouseUp += new MouseEventHandler(this.pictureBoxeGrid_MouseUp);
                    this.pictureBoxeGrid[i, j].MouseDown += new MouseEventHandler(this.pictureBoxeGrid_MouseDown);
                    this.pictureBoxeGrid[i, j].MouseMove += new MouseEventHandler(this.pictureBoxeGrid_MouseMove);

                    //this.labelGridNumber[i, j] = new Label();
                    //this.labelGridNumber[i, j].Name = "labelGridNumber" + i.ToString() + j.ToString();
                    //this.labelGridNumber[i, j].Text = i.ToString() + j.ToString();
                    //this.labelGridNumber[i, j].Location = new Point(40 + i * width, 40 + j * height);
                    //this.labelGridNumber[i, j].Size = new Size(30, 30);

                    //this.panelGrid.Controls.Add(this.labelGridNumber[i, j]);
                    this.panelGrid.Controls.Add(this.pictureBoxeGrid[i, j]);

                    beadLocationX[i, j] = i * WIDTH;
                    beadLocationY[i, j] = j * HEIGHT;

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
                    break;
                case 1:
                    p.Image = Resources.orange_bead;
                    break;
                case 2:
                    p.Image = Resources.green_bead;
                    break;
                case 3:
                    p.Image = Resources.blue_bead;
                    break;
                case 4:
                    p.Image = Resources.purple_bead;
                    break;
            }

        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            this.panelGrid.Dispose();

            InitializeGrid();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //this.labelMousePositionX.Text = "X:" + e.Location.X.ToString();
            //this.labelMousePositionY.Text = "Y:" + e.Location.Y.ToString();
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
                    if (p.Name == this.pictureBoxeGrid[i, j].Name)
                    {
                        startX = this.pictureBoxeGrid[i, j].Location.X;
                        startY = this.pictureBoxeGrid[i, j].Location.Y;

                        this.labelMousePositionX.Text = startX.ToString();
                        this.labelMousePositionY.Text = startY.ToString();

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
                    if ((nowX >= this.pictureBoxeGrid[i, j].Location.X - TRIGGER_NUMBER && nowX <= this.pictureBoxeGrid[i, j].Location.X + TRIGGER_NUMBER) &&
                        (nowY >= this.pictureBoxeGrid[i, j].Location.Y - TRIGGER_NUMBER && nowY <= this.pictureBoxeGrid[i, j].Location.Y + TRIGGER_NUMBER))
                    {
                        this.labelMousePositionX.Text = p.Name;
                        this.labelMousePositionY.Text = this.pictureBoxeGrid[i, j].Name;

                        if (p.Name == this.pictureBoxeGrid[i, j].Name)
                        {
                            p.Location = new Point(startX, startY);
                        }
                        else 
                        {
                            p.Location = this.pictureBoxeGrid[i, j].Location;
                        }
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

                nowX = LocationX - this.Location.X - this.panelGrid.Location.X - 60;
                nowY = LocationY - this.Location.Y - this.panelGrid.Location.Y - 60;

                p.Location = new Point(nowX, nowY);

                this.labelMousePositionX.Text = "X:" + p.Location.X.ToString();
                this.labelMousePositionY.Text = "Y:" + p.Location.Y.ToString();

                for (int i = 0; i < ROW; i++)
                {
                    for (int j = 0; j < COLUME; j++)
                    {
                        if (p.Name != this.pictureBoxeGrid[i, j].Name)
                        {
                            if ((nowX >= this.pictureBoxeGrid[i, j].Location.X - TRIGGER_NUMBER && nowX <= this.pictureBoxeGrid[i, j].Location.X + TRIGGER_NUMBER) &&
                                (nowY >= this.pictureBoxeGrid[i, j].Location.Y - TRIGGER_NUMBER && nowY <= this.pictureBoxeGrid[i, j].Location.Y + TRIGGER_NUMBER))
                            {
                                this.labelState.Text = "";

                                int tempX = this.pictureBoxeGrid[i, j].Location.X;
                                int tempY = this.pictureBoxeGrid[i, j].Location.Y;
                                this.pictureBoxeGrid[i, j].Location = new Point(startX, startY);
                                startX = tempX;
                                startY = tempY;
                            }
                        }
                    }
                }
            }
        }


    }
}
