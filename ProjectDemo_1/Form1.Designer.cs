
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
            this.panelGrid = new System.Windows.Forms.Panel();
            this.labelTest = new System.Windows.Forms.Label();
            this.labelTest2 = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelTest3 = new System.Windows.Forms.Label();
            this.labelTest4 = new System.Windows.Forms.Label();
            this.labelTest5 = new System.Windows.Forms.Label();
            this.labelTest6 = new System.Windows.Forms.Label();
            this.labelTest7 = new System.Windows.Forms.Label();
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
            // labelTest
            // 
            this.labelTest.AutoSize = true;
            this.labelTest.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTest.Location = new System.Drawing.Point(12, 9);
            this.labelTest.Name = "labelTest";
            this.labelTest.Size = new System.Drawing.Size(114, 30);
            this.labelTest.TabIndex = 1;
            this.labelTest.Text = "labelTest";
            // 
            // labelTest2
            // 
            this.labelTest2.AutoSize = true;
            this.labelTest2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTest2.Location = new System.Drawing.Point(12, 56);
            this.labelTest2.Name = "labelTest2";
            this.labelTest2.Size = new System.Drawing.Size(128, 30);
            this.labelTest2.TabIndex = 1;
            this.labelTest2.Text = "labelTest2";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(535, 17);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "重新生成";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelTest3
            // 
            this.labelTest3.AutoSize = true;
            this.labelTest3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTest3.Location = new System.Drawing.Point(631, 17);
            this.labelTest3.Name = "labelTest3";
            this.labelTest3.Size = new System.Drawing.Size(65, 15);
            this.labelTest3.TabIndex = 1;
            this.labelTest3.Text = "labelTest3";
            // 
            // labelTest4
            // 
            this.labelTest4.AutoSize = true;
            this.labelTest4.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTest4.Location = new System.Drawing.Point(631, 150);
            this.labelTest4.Name = "labelTest4";
            this.labelTest4.Size = new System.Drawing.Size(128, 30);
            this.labelTest4.TabIndex = 1;
            this.labelTest4.Text = "labelTest4";
            // 
            // labelTest5
            // 
            this.labelTest5.AutoSize = true;
            this.labelTest5.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTest5.Location = new System.Drawing.Point(981, 150);
            this.labelTest5.Name = "labelTest5";
            this.labelTest5.Size = new System.Drawing.Size(128, 30);
            this.labelTest5.TabIndex = 1;
            this.labelTest5.Text = "labelTest5";
            // 
            // labelTest6
            // 
            this.labelTest6.AutoSize = true;
            this.labelTest6.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTest6.Location = new System.Drawing.Point(631, 386);
            this.labelTest6.Name = "labelTest6";
            this.labelTest6.Size = new System.Drawing.Size(85, 20);
            this.labelTest6.TabIndex = 1;
            this.labelTest6.Text = "labelTest6";
            // 
            // labelTest7
            // 
            this.labelTest7.AutoSize = true;
            this.labelTest7.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTest7.Location = new System.Drawing.Point(981, 386);
            this.labelTest7.Name = "labelTest7";
            this.labelTest7.Size = new System.Drawing.Size(128, 30);
            this.labelTest7.TabIndex = 1;
            this.labelTest7.Text = "labelTest7";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1384, 661);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelTest7);
            this.Controls.Add(this.labelTest6);
            this.Controls.Add(this.labelTest5);
            this.Controls.Add(this.labelTest4);
            this.Controls.Add(this.labelTest3);
            this.Controls.Add(this.labelTest2);
            this.Controls.Add(this.labelTest);
            this.Controls.Add(this.panelGrid);
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
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Label labelTest;
        private System.Windows.Forms.Label labelTest2;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelTest3;
        private System.Windows.Forms.Label labelTest4;
        private System.Windows.Forms.Label labelTest5;
        private System.Windows.Forms.Label labelTest6;
        private System.Windows.Forms.Label labelTest7;
    }
}

