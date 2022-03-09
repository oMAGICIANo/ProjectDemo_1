
namespace ProjectDemo_1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelGrid = new System.Windows.Forms.Panel();
            this.labelCombo = new System.Windows.Forms.Label();
            this.panelFight = new System.Windows.Forms.Panel();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.pictureBoxHP = new System.Windows.Forms.PictureBox();
            this.labelHP = new System.Windows.Forms.Label();
            this.labelGamePoint = new System.Windows.Forms.Label();
            this.pictureBoxRestart = new System.Windows.Forms.PictureBox();
            this.pictureBoxStop = new System.Windows.Forms.PictureBox();
            this.pictureBoxStart = new System.Windows.Forms.PictureBox();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.panelFight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRestart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStart)).BeginInit();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGrid.Location = new System.Drawing.Point(12, 12);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(600, 500);
            this.panelGrid.TabIndex = 0;
            // 
            // labelCombo
            // 
            this.labelCombo.AutoSize = true;
            this.labelCombo.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCombo.Location = new System.Drawing.Point(3, 411);
            this.labelCombo.Name = "labelCombo";
            this.labelCombo.Size = new System.Drawing.Size(117, 29);
            this.labelCombo.TabIndex = 3;
            this.labelCombo.Text = "labelCombo";
            // 
            // panelFight
            // 
            this.panelFight.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelFight.Controls.Add(this.labelSpeed);
            this.panelFight.Controls.Add(this.pictureBoxHP);
            this.panelFight.Controls.Add(this.labelHP);
            this.panelFight.Controls.Add(this.labelGamePoint);
            this.panelFight.Controls.Add(this.pictureBoxRestart);
            this.panelFight.Controls.Add(this.pictureBoxStop);
            this.panelFight.Controls.Add(this.labelCombo);
            this.panelFight.Controls.Add(this.pictureBoxStart);
            this.panelFight.Location = new System.Drawing.Point(632, 12);
            this.panelFight.Name = "panelFight";
            this.panelFight.Size = new System.Drawing.Size(940, 500);
            this.panelFight.TabIndex = 4;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelSpeed.Location = new System.Drawing.Point(3, 0);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(94, 29);
            this.labelSpeed.TabIndex = 12;
            this.labelSpeed.Text = "Speed：";
            // 
            // pictureBoxHP
            // 
            this.pictureBoxHP.BackColor = System.Drawing.Color.Red;
            this.pictureBoxHP.Location = new System.Drawing.Point(0, 474);
            this.pictureBoxHP.Name = "pictureBoxHP";
            this.pictureBoxHP.Size = new System.Drawing.Size(940, 25);
            this.pictureBoxHP.TabIndex = 11;
            this.pictureBoxHP.TabStop = false;
            // 
            // labelHP
            // 
            this.labelHP.AutoSize = true;
            this.labelHP.BackColor = System.Drawing.Color.Transparent;
            this.labelHP.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelHP.Location = new System.Drawing.Point(3, 441);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(61, 29);
            this.labelHP.TabIndex = 7;
            this.labelHP.Text = "HP: 0";
            // 
            // labelGamePoint
            // 
            this.labelGamePoint.AutoSize = true;
            this.labelGamePoint.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelGamePoint.Location = new System.Drawing.Point(3, 30);
            this.labelGamePoint.Name = "labelGamePoint";
            this.labelGamePoint.Size = new System.Drawing.Size(89, 29);
            this.labelGamePoint.TabIndex = 5;
            this.labelGamePoint.Text = "Score：";
            // 
            // pictureBoxRestart
            // 
            this.pictureBoxRestart.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxRestart.Image")));
            this.pictureBoxRestart.Location = new System.Drawing.Point(703, 0);
            this.pictureBoxRestart.Name = "pictureBoxRestart";
            this.pictureBoxRestart.Size = new System.Drawing.Size(75, 75);
            this.pictureBoxRestart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRestart.TabIndex = 0;
            this.pictureBoxRestart.TabStop = false;
            // 
            // pictureBoxStop
            // 
            this.pictureBoxStop.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxStop.Image")));
            this.pictureBoxStop.Location = new System.Drawing.Point(784, 0);
            this.pictureBoxStop.Name = "pictureBoxStop";
            this.pictureBoxStop.Size = new System.Drawing.Size(75, 75);
            this.pictureBoxStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxStop.TabIndex = 0;
            this.pictureBoxStop.TabStop = false;
            // 
            // pictureBoxStart
            // 
            this.pictureBoxStart.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxStart.Image")));
            this.pictureBoxStart.Location = new System.Drawing.Point(865, 0);
            this.pictureBoxStart.Name = "pictureBoxStart";
            this.pictureBoxStart.Size = new System.Drawing.Size(75, 75);
            this.pictureBoxStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxStart.TabIndex = 0;
            this.pictureBoxStart.TabStop = false;
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1584, 521);
            this.Controls.Add(this.panelFight);
            this.Controls.Add(this.panelGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Demo 1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelFight.ResumeLayout(false);
            this.panelFight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRestart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Label labelCombo;
        private System.Windows.Forms.Panel panelFight;
        private System.Windows.Forms.Label labelGamePoint;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Label labelHP;
        private System.Windows.Forms.PictureBox pictureBoxHP;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.PictureBox pictureBoxStart;
        private System.Windows.Forms.PictureBox pictureBoxStop;
        private System.Windows.Forms.PictureBox pictureBoxRestart;
    }
}

