
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
            this.labelMousePositionX = new System.Windows.Forms.Label();
            this.labelMousePositionY = new System.Windows.Forms.Label();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.labelState = new System.Windows.Forms.Label();
            this.labelTest = new System.Windows.Forms.Label();
            this.labelTest2 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.labelTest3 = new System.Windows.Forms.Label();
            this.labelTest4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelMousePositionX
            // 
            this.labelMousePositionX.AutoSize = true;
            this.labelMousePositionX.BackColor = System.Drawing.Color.White;
            this.labelMousePositionX.Location = new System.Drawing.Point(12, 9);
            this.labelMousePositionX.Name = "labelMousePositionX";
            this.labelMousePositionX.Size = new System.Drawing.Size(127, 15);
            this.labelMousePositionX.TabIndex = 1;
            this.labelMousePositionX.Text = "labelMousePositionX";
            // 
            // labelMousePositionY
            // 
            this.labelMousePositionY.AutoSize = true;
            this.labelMousePositionY.Location = new System.Drawing.Point(12, 35);
            this.labelMousePositionY.Name = "labelMousePositionY";
            this.labelMousePositionY.Size = new System.Drawing.Size(126, 15);
            this.labelMousePositionY.TabIndex = 1;
            this.labelMousePositionY.Text = "labelMousePositionY";
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(597, 12);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(75, 23);
            this.buttonRestart.TabIndex = 2;
            this.buttonRestart.Text = "重新生成";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Location = new System.Drawing.Point(145, 9);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(64, 15);
            this.labelState.TabIndex = 3;
            this.labelState.Text = "labelState";
            // 
            // labelTest
            // 
            this.labelTest.AutoSize = true;
            this.labelTest.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTest.Location = new System.Drawing.Point(704, 105);
            this.labelTest.Name = "labelTest";
            this.labelTest.Size = new System.Drawing.Size(91, 24);
            this.labelTest.TabIndex = 5;
            this.labelTest.Text = "labelTest";
            // 
            // labelTest2
            // 
            this.labelTest2.AutoSize = true;
            this.labelTest2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTest2.Location = new System.Drawing.Point(1000, 105);
            this.labelTest2.Name = "labelTest2";
            this.labelTest2.Size = new System.Drawing.Size(102, 24);
            this.labelTest2.TabIndex = 6;
            this.labelTest2.Text = "labelTest2";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // labelTest3
            // 
            this.labelTest3.AutoSize = true;
            this.labelTest3.Location = new System.Drawing.Point(704, 359);
            this.labelTest3.Name = "labelTest3";
            this.labelTest3.Size = new System.Drawing.Size(65, 15);
            this.labelTest3.TabIndex = 7;
            this.labelTest3.Text = "labelTest3";
            // 
            // labelTest4
            // 
            this.labelTest4.AutoSize = true;
            this.labelTest4.Location = new System.Drawing.Point(843, 359);
            this.labelTest4.Name = "labelTest4";
            this.labelTest4.Size = new System.Drawing.Size(65, 15);
            this.labelTest4.TabIndex = 8;
            this.labelTest4.Text = "labelTest4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1384, 661);
            this.Controls.Add(this.labelTest4);
            this.Controls.Add(this.labelTest3);
            this.Controls.Add(this.labelTest2);
            this.Controls.Add(this.labelTest);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.labelMousePositionY);
            this.Controls.Add(this.labelMousePositionX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Demo 1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelMousePositionX;
        private System.Windows.Forms.Label labelMousePositionY;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Label labelTest;
        private System.Windows.Forms.Label labelTest2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label labelTest3;
        private System.Windows.Forms.Label labelTest4;
    }
}

