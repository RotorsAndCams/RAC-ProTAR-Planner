namespace ptPlugin1
{
    partial class fleetSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lConnectedUAV = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tCallSign3 = new System.Windows.Forms.TextBox();
            this.tTail3 = new System.Windows.Forms.TextBox();
            this.tCallSign2 = new System.Windows.Forms.TextBox();
            this.cbEnable3 = new System.Windows.Forms.CheckBox();
            this.tTail2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tTail1 = new System.Windows.Forms.TextBox();
            this.cbEnable2 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbEnable1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tCallSign1 = new System.Windows.Forms.TextBox();
            this.bOK = new MissionPlanner.Controls.MyButton();
            this.bCancel = new MissionPlanner.Controls.MyButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lConnectedUAV
            // 
            this.lConnectedUAV.AutoSize = true;
            this.lConnectedUAV.Location = new System.Drawing.Point(16, 11);
            this.lConnectedUAV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lConnectedUAV.Name = "lConnectedUAV";
            this.lConnectedUAV.Size = new System.Drawing.Size(127, 16);
            this.lConnectedUAV.TabIndex = 0;
            this.lConnectedUAV.Text = "Connected UAV id\'s";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Command Sots";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Slot #";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Plane TAIL # (sysid)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Enabled";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(538, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Callsign";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.tCallSign3, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.tTail3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.tCallSign2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbEnable3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tTail2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tTail1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbEnable2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbEnable1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tCallSign1, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 91);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(765, 140);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tCallSign3
            // 
            this.tCallSign3.Location = new System.Drawing.Point(538, 109);
            this.tCallSign3.Margin = new System.Windows.Forms.Padding(4);
            this.tCallSign3.Name = "tCallSign3";
            this.tCallSign3.Size = new System.Drawing.Size(132, 22);
            this.tCallSign3.TabIndex = 9;
            // 
            // tTail3
            // 
            this.tTail3.Location = new System.Drawing.Point(309, 109);
            this.tTail3.Margin = new System.Windows.Forms.Padding(4);
            this.tTail3.Name = "tTail3";
            this.tTail3.Size = new System.Drawing.Size(132, 22);
            this.tTail3.TabIndex = 8;
            // 
            // tCallSign2
            // 
            this.tCallSign2.Location = new System.Drawing.Point(538, 74);
            this.tCallSign2.Margin = new System.Windows.Forms.Padding(4);
            this.tCallSign2.Name = "tCallSign2";
            this.tCallSign2.Size = new System.Drawing.Size(132, 22);
            this.tCallSign2.TabIndex = 6;
            // 
            // cbEnable3
            // 
            this.cbEnable3.AutoSize = true;
            this.cbEnable3.Location = new System.Drawing.Point(80, 109);
            this.cbEnable3.Margin = new System.Windows.Forms.Padding(4);
            this.cbEnable3.Name = "cbEnable3";
            this.cbEnable3.Size = new System.Drawing.Size(18, 17);
            this.cbEnable3.TabIndex = 7;
            this.cbEnable3.UseVisualStyleBackColor = true;
            // 
            // tTail2
            // 
            this.tTail2.Location = new System.Drawing.Point(309, 74);
            this.tTail2.Margin = new System.Windows.Forms.Padding(4);
            this.tTail2.Name = "tTail2";
            this.tTail2.Size = new System.Drawing.Size(132, 22);
            this.tTail2.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 105);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "3";
            // 
            // tTail1
            // 
            this.tTail1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tTail1.Location = new System.Drawing.Point(309, 41);
            this.tTail1.Margin = new System.Windows.Forms.Padding(4);
            this.tTail1.Name = "tTail1";
            this.tTail1.Size = new System.Drawing.Size(132, 22);
            this.tTail1.TabIndex = 2;
            // 
            // cbEnable2
            // 
            this.cbEnable2.AutoSize = true;
            this.cbEnable2.Location = new System.Drawing.Point(80, 74);
            this.cbEnable2.Margin = new System.Windows.Forms.Padding(4);
            this.cbEnable2.Name = "cbEnable2";
            this.cbEnable2.Size = new System.Drawing.Size(18, 17);
            this.cbEnable2.TabIndex = 4;
            this.cbEnable2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 70);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "2";
            // 
            // cbEnable1
            // 
            this.cbEnable1.AutoSize = true;
            this.cbEnable1.Location = new System.Drawing.Point(80, 39);
            this.cbEnable1.Margin = new System.Windows.Forms.Padding(4);
            this.cbEnable1.Name = "cbEnable1";
            this.cbEnable1.Size = new System.Drawing.Size(18, 17);
            this.cbEnable1.TabIndex = 1;
            this.cbEnable1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 35);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "1";
            // 
            // tCallSign1
            // 
            this.tCallSign1.Location = new System.Drawing.Point(538, 39);
            this.tCallSign1.Margin = new System.Windows.Forms.Padding(4);
            this.tCallSign1.Name = "tCallSign1";
            this.tCallSign1.Size = new System.Drawing.Size(132, 22);
            this.tCallSign1.TabIndex = 3;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(248, 263);
            this.bOK.Margin = new System.Windows.Forms.Padding(4);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(100, 28);
            this.bOK.TabIndex = 10;
            this.bOK.Text = "Update";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(464, 263);
            this.bCancel.Margin = new System.Windows.Forms.Padding(4);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(100, 28);
            this.bCancel.TabIndex = 11;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // fleetSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 335);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lConnectedUAV);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fleetSetup";
            this.Text = "fleetSetup";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lConnectedUAV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tCallSign3;
        private System.Windows.Forms.TextBox tTail3;
        private System.Windows.Forms.TextBox tCallSign2;
        private System.Windows.Forms.CheckBox cbEnable3;
        private System.Windows.Forms.TextBox tTail2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tTail1;
        private System.Windows.Forms.CheckBox cbEnable2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbEnable1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tCallSign1;
        private MissionPlanner.Controls.MyButton bOK;
        private MissionPlanner.Controls.MyButton bCancel;
    }
}