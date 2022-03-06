
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
            this.panelGrid = new System.Windows.Forms.Panel();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelCombo = new System.Windows.Forms.Label();
            this.panelFight = new System.Windows.Forms.Panel();
            this.labelGamePoint = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.labelHP = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.labelColorCombo = new System.Windows.Forms.Label();
            this.pictureBoxHP = new System.Windows.Forms.PictureBox();
            this.labelSpeed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHP)).BeginInit();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panelGrid.Location = new System.Drawing.Point(10, 150);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(600, 500);
            this.panelGrid.TabIndex = 0;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(12, 17);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "重新生成";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelCombo
            // 
            this.labelCombo.AutoSize = true;
            this.labelCombo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCombo.Location = new System.Drawing.Point(378, 80);
            this.labelCombo.Name = "labelCombo";
            this.labelCombo.Size = new System.Drawing.Size(232, 47);
            this.labelCombo.TabIndex = 3;
            this.labelCombo.Text = "labelCombo";
            // 
            // panelFight
            // 
            this.panelFight.BackColor = System.Drawing.Color.Silver;
            this.panelFight.Location = new System.Drawing.Point(628, 150);
            this.panelFight.Name = "panelFight";
            this.panelFight.Size = new System.Drawing.Size(940, 500);
            this.panelFight.TabIndex = 4;
            // 
            // labelGamePoint
            // 
            this.labelGamePoint.AutoSize = true;
            this.labelGamePoint.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelGamePoint.Location = new System.Drawing.Point(628, 38);
            this.labelGamePoint.Name = "labelGamePoint";
            this.labelGamePoint.Size = new System.Drawing.Size(101, 30);
            this.labelGamePoint.TabIndex = 5;
            this.labelGamePoint.Text = "Score：";
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // labelHP
            // 
            this.labelHP.AutoSize = true;
            this.labelHP.BackColor = System.Drawing.Color.Transparent;
            this.labelHP.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelHP.Location = new System.Drawing.Point(628, 86);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(71, 30);
            this.labelHP.TabIndex = 7;
            this.labelHP.Text = "HP: 0";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 46);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "開　始";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(12, 75);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 9;
            this.buttonStop.Text = "暫　停";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(12, 104);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(75, 23);
            this.buttonRestart.TabIndex = 9;
            this.buttonRestart.Text = "重新開始";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // labelColorCombo
            // 
            this.labelColorCombo.AutoSize = true;
            this.labelColorCombo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelColorCombo.Location = new System.Drawing.Point(93, 17);
            this.labelColorCombo.Name = "labelColorCombo";
            this.labelColorCombo.Size = new System.Drawing.Size(169, 24);
            this.labelColorCombo.TabIndex = 10;
            this.labelColorCombo.Text = "labelColorCombo";
            // 
            // pictureBoxHP
            // 
            this.pictureBoxHP.BackColor = System.Drawing.Color.Red;
            this.pictureBoxHP.Location = new System.Drawing.Point(628, 119);
            this.pictureBoxHP.Name = "pictureBoxHP";
            this.pictureBoxHP.Size = new System.Drawing.Size(940, 25);
            this.pictureBoxHP.TabIndex = 11;
            this.pictureBoxHP.TabStop = false;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelSpeed.Location = new System.Drawing.Point(1422, 75);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(109, 30);
            this.labelSpeed.TabIndex = 12;
            this.labelSpeed.Text = "Speed：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1584, 661);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.labelHP);
            this.Controls.Add(this.pictureBoxHP);
            this.Controls.Add(this.labelColorCombo);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelGamePoint);
            this.Controls.Add(this.panelFight);
            this.Controls.Add(this.labelCombo);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.panelGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Demo 1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelCombo;
        private System.Windows.Forms.Panel panelFight;
        private System.Windows.Forms.Label labelGamePoint;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Label labelHP;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Label labelColorCombo;
        private System.Windows.Forms.PictureBox pictureBoxHP;
        private System.Windows.Forms.Label labelSpeed;
    }
}

