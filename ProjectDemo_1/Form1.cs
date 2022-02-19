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

        private const int row = 6, colume = 5;
        private PictureBox[,] pictureBoxeGrid = new PictureBox[row, colume];
        private Label[,] labelGridNumber = new Label[row, colume];
        private Panel panelGrid;

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


            for (int i = 0; i < row; i++) 
            {
                for (int j = 0; j < colume; j++) 
                {
                    this.pictureBoxeGrid[i, j] = new PictureBox();
                    this.pictureBoxeGrid[i, j].Location = new Point(0 + i * 110, 0 + j * 110);
                    this.pictureBoxeGrid[i, j].Size = new Size(100, 100);
                    this.pictureBoxeGrid[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    this.pictureBoxeGrid[i, j].Name = "pictureBoxeGrid" + i.ToString() + j.ToString();

                    RandomBead(this.pictureBoxeGrid[i, j]);

                    this.pictureBoxeGrid[i, j].MouseUp += new MouseEventHandler(this.pictureBoxeGrid_MouseUp);
                    this.pictureBoxeGrid[i, j].MouseDown += new MouseEventHandler(this.pictureBoxeGrid_MouseDown);
                    this.pictureBoxeGrid[i, j].MouseMove += new MouseEventHandler(this.pictureBoxeGrid_MouseMove);

                    this.labelGridNumber[i, j] = new Label();
                    this.labelGridNumber[i, j].Name = "labelGridNumber" + i.ToString() + j.ToString();
                    this.labelGridNumber[i, j].Text = i.ToString() + j.ToString();
                    this.labelGridNumber[i, j].Location = new Point(40 + i * 110, 40 + j * 110);
                    this.labelGridNumber[i, j].Size = new Size(30, 30);

                    this.panelGrid.Controls.Add(this.labelGridNumber[i, j]);
                    this.panelGrid.Controls.Add(this.pictureBoxeGrid[i, j]);

                    this.Controls.Add(panelGrid);
                    
                }
            }
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
        int startX, startY;

        private void pictureBoxeGrid_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            moveFlag = true;

            startX = p.Location.X;
            startY = p.Location.Y;
        }

        private void pictureBoxeGrid_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            moveFlag = false;

            p.Location = new Point(startX, startY);
        }

        private void pictureBoxeGrid_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            this.labelState.Text = p.Name;

            if (moveFlag)
            {
                Int32 LocationX = Form1.MousePosition.X, LocationY = Form1.MousePosition.Y;
                this.labelMousePositionX.Text = "X:" + LocationX.ToString();
                this.labelMousePositionY.Text = "Y:" + LocationY.ToString();

                p.Location = new Point(LocationX - this.Location.X - this.panelGrid.Location.X - 60,
                                       LocationY - this.Location.Y - this.panelGrid.Location.Y - 60);
            }
        }


    }
}
