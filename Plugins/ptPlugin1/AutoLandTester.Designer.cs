namespace ptPlugin1
{
    partial class AutoLandTester
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bStartLanding = new System.Windows.Forms.Button();
            this.UpDwn_Direction = new System.Windows.Forms.NumericUpDown();
            this.UpDwn_Distance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lLastUpdate = new System.Windows.Forms.Label();
            this.lActivePlaneID = new System.Windows.Forms.Label();
            this.lState = new System.Windows.Forms.Label();
            this.lDistanceToTarget = new System.Windows.Forms.Label();
            this.CHK_ApproachOverride = new System.Windows.Forms.CheckBox();
            this.lineSeparator1 = new MissionPlanner.Controls.LineSeparator();
            this.lineSeparator2 = new MissionPlanner.Controls.LineSeparator();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.CB_colors = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lineSeparator3 = new MissionPlanner.Controls.LineSeparator();
            this.lineSeparator4 = new MissionPlanner.Controls.LineSeparator();
            this.lineSeparator5 = new MissionPlanner.Controls.LineSeparator();
            this.CHK_ClockwiseTurn = new System.Windows.Forms.CheckBox();
            this.UpDwn_CruiseSpeed = new System.Windows.Forms.NumericUpDown();
            this.UpDwn_MeasSpeed = new System.Windows.Forms.NumericUpDown();
            this.UpDwn_LineupSpeed = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.UpDwn_FinalSpeed = new System.Windows.Forms.NumericUpDown();
            this.lineSeparator6 = new MissionPlanner.Controls.LineSeparator();
            this.label12 = new System.Windows.Forms.Label();
            this.CB_LandingZones = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.bAbortLanding = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_Direction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_Distance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_CruiseSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_MeasSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_LineupSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_FinalSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // bStartLanding
            // 
            this.bStartLanding.Location = new System.Drawing.Point(302, 47);
            this.bStartLanding.Name = "bStartLanding";
            this.bStartLanding.Size = new System.Drawing.Size(149, 36);
            this.bStartLanding.TabIndex = 0;
            this.bStartLanding.Text = "Start Landing";
            this.bStartLanding.UseVisualStyleBackColor = true;
            this.bStartLanding.Click += new System.EventHandler(this.bStartLanding_Click);
            // 
            // UpDwn_Direction
            // 
            this.UpDwn_Direction.Location = new System.Drawing.Point(189, 201);
            this.UpDwn_Direction.Maximum = new decimal(new int[] {
            355,
            0,
            0,
            0});
            this.UpDwn_Direction.Name = "UpDwn_Direction";
            this.UpDwn_Direction.Size = new System.Drawing.Size(82, 22);
            this.UpDwn_Direction.TabIndex = 1;
            this.UpDwn_Direction.ValueChanged += new System.EventHandler(this.UpDwn_Direction_ValueChanged);
            // 
            // UpDwn_Distance
            // 
            this.UpDwn_Distance.Location = new System.Drawing.Point(189, 169);
            this.UpDwn_Distance.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.UpDwn_Distance.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.UpDwn_Distance.Name = "UpDwn_Distance";
            this.UpDwn_Distance.Size = new System.Drawing.Size(83, 22);
            this.UpDwn_Distance.TabIndex = 2;
            this.UpDwn_Distance.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.UpDwn_Distance.ValueChanged += new System.EventHandler(this.UpDwn_Distance_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Distance (meter)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Direction (deg)";
            // 
            // lLastUpdate
            // 
            this.lLastUpdate.AutoSize = true;
            this.lLastUpdate.Location = new System.Drawing.Point(16, 644);
            this.lLastUpdate.Name = "lLastUpdate";
            this.lLastUpdate.Size = new System.Drawing.Size(116, 16);
            this.lLastUpdate.TabIndex = 8;
            this.lLastUpdate.Text = "Last update: -- sec";
            // 
            // lActivePlaneID
            // 
            this.lActivePlaneID.AutoSize = true;
            this.lActivePlaneID.Location = new System.Drawing.Point(20, 19);
            this.lActivePlaneID.Name = "lActivePlaneID";
            this.lActivePlaneID.Size = new System.Drawing.Size(96, 16);
            this.lActivePlaneID.TabIndex = 9;
            this.lActivePlaneID.Text = "Active Plane: --";
            // 
            // lState
            // 
            this.lState.AutoSize = true;
            this.lState.Location = new System.Drawing.Point(20, 47);
            this.lState.Name = "lState";
            this.lState.Size = new System.Drawing.Size(101, 16);
            this.lState.TabIndex = 10;
            this.lState.Text = "Landing state: --";
            // 
            // lDistanceToTarget
            // 
            this.lDistanceToTarget.AutoSize = true;
            this.lDistanceToTarget.Location = new System.Drawing.Point(16, 618);
            this.lDistanceToTarget.Name = "lDistanceToTarget";
            this.lDistanceToTarget.Size = new System.Drawing.Size(125, 16);
            this.lDistanceToTarget.TabIndex = 11;
            this.lDistanceToTarget.Text = "Distance to target: --";
            // 
            // CHK_ApproachOverride
            // 
            this.CHK_ApproachOverride.AutoSize = true;
            this.CHK_ApproachOverride.Location = new System.Drawing.Point(23, 143);
            this.CHK_ApproachOverride.Name = "CHK_ApproachOverride";
            this.CHK_ApproachOverride.Size = new System.Drawing.Size(174, 20);
            this.CHK_ApproachOverride.TabIndex = 15;
            this.CHK_ApproachOverride.Text = "Override approach point";
            this.CHK_ApproachOverride.UseVisualStyleBackColor = true;
            this.CHK_ApproachOverride.CheckedChanged += new System.EventHandler(this.CHK_ApproachOverride_CheckedChanged);
            // 
            // lineSeparator1
            // 
            this.lineSeparator1.Location = new System.Drawing.Point(19, 594);
            this.lineSeparator1.MaximumSize = new System.Drawing.Size(2000, 2);
            this.lineSeparator1.MinimumSize = new System.Drawing.Size(0, 2);
            this.lineSeparator1.Name = "lineSeparator1";
            this.lineSeparator1.Size = new System.Drawing.Size(427, 2);
            this.lineSeparator1.TabIndex = 16;
            // 
            // lineSeparator2
            // 
            this.lineSeparator2.Location = new System.Drawing.Point(23, 135);
            this.lineSeparator2.MaximumSize = new System.Drawing.Size(2000, 2);
            this.lineSeparator2.MinimumSize = new System.Drawing.Size(0, 2);
            this.lineSeparator2.Name = "lineSeparator2";
            this.lineSeparator2.Size = new System.Drawing.Size(428, 2);
            this.lineSeparator2.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(383, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Main panel";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(355, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "Override panel";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(363, 618);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Development";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "Wind measurement time (sec)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(193, 277);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(82, 22);
            this.numericUpDown1.TabIndex = 23;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // CB_colors
            // 
            this.CB_colors.DisplayMember = "Text";
            this.CB_colors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_colors.FormattingEnabled = true;
            this.CB_colors.Location = new System.Drawing.Point(154, 470);
            this.CB_colors.Name = "CB_colors";
            this.CB_colors.Size = new System.Drawing.Size(121, 24);
            this.CB_colors.TabIndex = 24;
            this.CB_colors.ValueMember = "ID";
            this.CB_colors.SelectedIndexChanged += new System.EventHandler(this.CB_colors_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 478);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 25;
            this.label7.Text = "Color";
            // 
            // lineSeparator3
            // 
            this.lineSeparator3.Location = new System.Drawing.Point(23, 241);
            this.lineSeparator3.MaximumSize = new System.Drawing.Size(2000, 2);
            this.lineSeparator3.MinimumSize = new System.Drawing.Size(0, 2);
            this.lineSeparator3.Name = "lineSeparator3";
            this.lineSeparator3.Size = new System.Drawing.Size(423, 2);
            this.lineSeparator3.TabIndex = 26;
            // 
            // lineSeparator4
            // 
            this.lineSeparator4.Location = new System.Drawing.Point(28, 514);
            this.lineSeparator4.MaximumSize = new System.Drawing.Size(2000, 2);
            this.lineSeparator4.MinimumSize = new System.Drawing.Size(0, 2);
            this.lineSeparator4.Name = "lineSeparator4";
            this.lineSeparator4.Size = new System.Drawing.Size(248, 2);
            this.lineSeparator4.TabIndex = 27;
            // 
            // lineSeparator5
            // 
            this.lineSeparator5.Location = new System.Drawing.Point(27, 308);
            this.lineSeparator5.MaximumSize = new System.Drawing.Size(2000, 2);
            this.lineSeparator5.MinimumSize = new System.Drawing.Size(0, 2);
            this.lineSeparator5.Name = "lineSeparator5";
            this.lineSeparator5.Size = new System.Drawing.Size(248, 2);
            this.lineSeparator5.TabIndex = 28;
            // 
            // CHK_ClockwiseTurn
            // 
            this.CHK_ClockwiseTurn.AutoSize = true;
            this.CHK_ClockwiseTurn.Location = new System.Drawing.Point(31, 535);
            this.CHK_ClockwiseTurn.Name = "CHK_ClockwiseTurn";
            this.CHK_ClockwiseTurn.Size = new System.Drawing.Size(114, 20);
            this.CHK_ClockwiseTurn.TabIndex = 29;
            this.CHK_ClockwiseTurn.Text = "Clockwise turn";
            this.CHK_ClockwiseTurn.UseVisualStyleBackColor = true;
            this.CHK_ClockwiseTurn.CheckedChanged += new System.EventHandler(this.CHK_ClockwiseTurn_CheckedChanged);
            // 
            // UpDwn_CruiseSpeed
            // 
            this.UpDwn_CruiseSpeed.Location = new System.Drawing.Point(193, 324);
            this.UpDwn_CruiseSpeed.Name = "UpDwn_CruiseSpeed";
            this.UpDwn_CruiseSpeed.Size = new System.Drawing.Size(82, 22);
            this.UpDwn_CruiseSpeed.TabIndex = 30;
            this.UpDwn_CruiseSpeed.ValueChanged += new System.EventHandler(this.UpDwn_CruiseSpeed_ValueChanged);
            // 
            // UpDwn_MeasSpeed
            // 
            this.UpDwn_MeasSpeed.Location = new System.Drawing.Point(193, 353);
            this.UpDwn_MeasSpeed.Name = "UpDwn_MeasSpeed";
            this.UpDwn_MeasSpeed.Size = new System.Drawing.Size(82, 22);
            this.UpDwn_MeasSpeed.TabIndex = 31;
            this.UpDwn_MeasSpeed.ValueChanged += new System.EventHandler(this.UpDwn_MeasSpeed_ValueChanged);
            // 
            // UpDwn_LineupSpeed
            // 
            this.UpDwn_LineupSpeed.Location = new System.Drawing.Point(193, 382);
            this.UpDwn_LineupSpeed.Name = "UpDwn_LineupSpeed";
            this.UpDwn_LineupSpeed.Size = new System.Drawing.Size(82, 22);
            this.UpDwn_LineupSpeed.TabIndex = 32;
            this.UpDwn_LineupSpeed.ValueChanged += new System.EventHandler(this.UpDwn_LineupSpeed_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 326);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 16);
            this.label8.TabIndex = 33;
            this.label8.Text = "Cruise speed";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 355);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 16);
            this.label9.TabIndex = 34;
            this.label9.Text = "Measurement speed";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 384);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 16);
            this.label10.TabIndex = 35;
            this.label10.Text = "Lineup speed";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 416);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 16);
            this.label11.TabIndex = 36;
            this.label11.Text = "Final approach speed";
            // 
            // UpDwn_FinalSpeed
            // 
            this.UpDwn_FinalSpeed.Location = new System.Drawing.Point(193, 414);
            this.UpDwn_FinalSpeed.Name = "UpDwn_FinalSpeed";
            this.UpDwn_FinalSpeed.Size = new System.Drawing.Size(82, 22);
            this.UpDwn_FinalSpeed.TabIndex = 37;
            this.UpDwn_FinalSpeed.ValueChanged += new System.EventHandler(this.UpDwn_FinalSpeed_ValueChanged);
            // 
            // lineSeparator6
            // 
            this.lineSeparator6.Location = new System.Drawing.Point(28, 452);
            this.lineSeparator6.MaximumSize = new System.Drawing.Size(2000, 2);
            this.lineSeparator6.MinimumSize = new System.Drawing.Size(0, 2);
            this.lineSeparator6.Name = "lineSeparator6";
            this.lineSeparator6.Size = new System.Drawing.Size(248, 2);
            this.lineSeparator6.TabIndex = 39;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(316, 251);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 16);
            this.label12.TabIndex = 40;
            this.label12.Text = "Changeable params";
            // 
            // CB_LandingZones
            // 
            this.CB_LandingZones.DisplayMember = "Text";
            this.CB_LandingZones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_LandingZones.FormattingEnabled = true;
            this.CB_LandingZones.Location = new System.Drawing.Point(106, 77);
            this.CB_LandingZones.Name = "CB_LandingZones";
            this.CB_LandingZones.Size = new System.Drawing.Size(165, 24);
            this.CB_LandingZones.TabIndex = 41;
            this.CB_LandingZones.ValueMember = "ID";
            this.CB_LandingZones.SelectedIndexChanged += new System.EventHandler(this.CB_LandingZones_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 16);
            this.label13.TabIndex = 42;
            this.label13.Text = "Target:";
            // 
            // bAbortLanding
            // 
            this.bAbortLanding.Location = new System.Drawing.Point(302, 89);
            this.bAbortLanding.Name = "bAbortLanding";
            this.bAbortLanding.Size = new System.Drawing.Size(149, 36);
            this.bAbortLanding.TabIndex = 43;
            this.bAbortLanding.Text = "Abort Landing";
            this.bAbortLanding.UseVisualStyleBackColor = true;
            this.bAbortLanding.Click += new System.EventHandler(this.bAbortLanding_Click);
            // 
            // AutoLandTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bAbortLanding);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.CB_LandingZones);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lineSeparator6);
            this.Controls.Add(this.UpDwn_FinalSpeed);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.UpDwn_LineupSpeed);
            this.Controls.Add(this.UpDwn_MeasSpeed);
            this.Controls.Add(this.UpDwn_CruiseSpeed);
            this.Controls.Add(this.CHK_ClockwiseTurn);
            this.Controls.Add(this.lineSeparator5);
            this.Controls.Add(this.lineSeparator4);
            this.Controls.Add(this.lineSeparator3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CB_colors);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lineSeparator2);
            this.Controls.Add(this.lineSeparator1);
            this.Controls.Add(this.CHK_ApproachOverride);
            this.Controls.Add(this.lDistanceToTarget);
            this.Controls.Add(this.lState);
            this.Controls.Add(this.lActivePlaneID);
            this.Controls.Add(this.lLastUpdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpDwn_Distance);
            this.Controls.Add(this.UpDwn_Direction);
            this.Controls.Add(this.bStartLanding);
            this.Name = "AutoLandTester";
            this.Size = new System.Drawing.Size(470, 689);
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_Direction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_Distance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_CruiseSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_MeasSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_LineupSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDwn_FinalSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bStartLanding;
        private System.Windows.Forms.NumericUpDown UpDwn_Direction;
        private System.Windows.Forms.NumericUpDown UpDwn_Distance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lLastUpdate;
        private System.Windows.Forms.Label lActivePlaneID;
        private System.Windows.Forms.Label lState;
        private System.Windows.Forms.Label lDistanceToTarget;
        private System.Windows.Forms.CheckBox CHK_ApproachOverride;
        private MissionPlanner.Controls.LineSeparator lineSeparator1;
        private MissionPlanner.Controls.LineSeparator lineSeparator2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox CB_colors;
        private System.Windows.Forms.Label label7;
        private MissionPlanner.Controls.LineSeparator lineSeparator3;
        private MissionPlanner.Controls.LineSeparator lineSeparator4;
        private MissionPlanner.Controls.LineSeparator lineSeparator5;
        private System.Windows.Forms.CheckBox CHK_ClockwiseTurn;
        private System.Windows.Forms.NumericUpDown UpDwn_CruiseSpeed;
        private System.Windows.Forms.NumericUpDown UpDwn_MeasSpeed;
        private System.Windows.Forms.NumericUpDown UpDwn_LineupSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown UpDwn_FinalSpeed;
        private MissionPlanner.Controls.LineSeparator lineSeparator6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox CB_LandingZones;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button bAbortLanding;
    }
}
