namespace AR.Drone.WinApp
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnEmergency = new System.Windows.Forms.Button();
            this.tmrStateUpdate = new System.Windows.Forms.Timer(this.components);
            this.tmrVideoUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.btnStopRecording = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.tbLowH = new System.Windows.Forms.TrackBar();
            this.tbHighH = new System.Windows.Forms.TrackBar();
            this.tbLowS = new System.Windows.Forms.TrackBar();
            this.tbHighS = new System.Windows.Forms.TrackBar();
            this.tbHighV = new System.Windows.Forms.TrackBar();
            this.tbLowV = new System.Windows.Forms.TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.simulatorBtn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbOpenCV3 = new System.Windows.Forms.PictureBox();
            this.pbOpenCV2 = new System.Windows.Forms.PictureBox();
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.pbOpenCV = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbLowH = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowV)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(4, 5);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 35);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Activate";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(126, 5);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(112, 35);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Deactivate";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnEmergency
            // 
            this.btnEmergency.Location = new System.Drawing.Point(840, 5);
            this.btnEmergency.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEmergency.Name = "btnEmergency";
            this.btnEmergency.Size = new System.Drawing.Size(124, 35);
            this.btnEmergency.TabIndex = 6;
            this.btnEmergency.Text = "Emergency";
            this.btnEmergency.UseVisualStyleBackColor = true;
            this.btnEmergency.Click += new System.EventHandler(this.btnEmergency_Click);
            // 
            // tmrVideoUpdate
            // 
            this.tmrVideoUpdate.Interval = 20;
            this.tmrVideoUpdate.Tick += new System.EventHandler(this.tmrVideoUpdate_Tick);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(706, 5);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(124, 35);
            this.btnReset.TabIndex = 19;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.Location = new System.Drawing.Point(247, 5);
            this.btnStartRecording.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.Size = new System.Drawing.Size(112, 35);
            this.btnStartRecording.TabIndex = 22;
            this.btnStartRecording.Text = "Start Rec.";
            this.btnStartRecording.UseVisualStyleBackColor = true;
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.Location = new System.Drawing.Point(368, 5);
            this.btnStopRecording.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Size = new System.Drawing.Size(112, 35);
            this.btnStopRecording.TabIndex = 23;
            this.btnStopRecording.Text = "Stop Rec.";
            this.btnStopRecording.UseVisualStyleBackColor = true;
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);
            // 
            // btnReplay
            // 
            this.btnReplay.Location = new System.Drawing.Point(490, 5);
            this.btnReplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(112, 35);
            this.btnReplay.TabIndex = 24;
            this.btnReplay.Text = "Replay";
            this.btnReplay.UseVisualStyleBackColor = true;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // tbLowH
            // 
            this.tbLowH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLowH.Location = new System.Drawing.Point(0, 0);
            this.tbLowH.Maximum = 255;
            this.tbLowH.Name = "tbLowH";
            this.tbLowH.Size = new System.Drawing.Size(372, 65);
            this.tbLowH.TabIndex = 27;
            this.tbLowH.Value = 170;
            this.tbLowH.ValueChanged += new System.EventHandler(this.tbLowH_ValueChanged);
            // 
            // tbHighH
            // 
            this.tbHighH.Location = new System.Drawing.Point(432, 3);
            this.tbHighH.Maximum = 255;
            this.tbHighH.Name = "tbHighH";
            this.tbHighH.Size = new System.Drawing.Size(424, 65);
            this.tbHighH.TabIndex = 28;
            this.tbHighH.Value = 179;
            // 
            // tbLowS
            // 
            this.tbLowS.Location = new System.Drawing.Point(3, 74);
            this.tbLowS.Maximum = 255;
            this.tbLowS.Name = "tbLowS";
            this.tbLowS.Size = new System.Drawing.Size(423, 65);
            this.tbLowS.TabIndex = 29;
            this.tbLowS.Value = 150;
            // 
            // tbHighS
            // 
            this.tbHighS.Location = new System.Drawing.Point(432, 74);
            this.tbHighS.Maximum = 255;
            this.tbHighS.Name = "tbHighS";
            this.tbHighS.Size = new System.Drawing.Size(424, 65);
            this.tbHighS.TabIndex = 29;
            this.tbHighS.Value = 255;
            // 
            // tbHighV
            // 
            this.tbHighV.Location = new System.Drawing.Point(432, 145);
            this.tbHighV.Maximum = 255;
            this.tbHighV.Name = "tbHighV";
            this.tbHighV.Size = new System.Drawing.Size(424, 67);
            this.tbHighV.TabIndex = 29;
            this.tbHighV.Value = 255;
            // 
            // tbLowV
            // 
            this.tbLowV.Location = new System.Drawing.Point(3, 145);
            this.tbLowV.Maximum = 255;
            this.tbLowV.Name = "tbLowV";
            this.tbLowV.Size = new System.Drawing.Size(423, 67);
            this.tbLowV.TabIndex = 29;
            this.tbLowV.Value = 60;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.simulatorBtn);
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Controls.Add(this.btnStop);
            this.panel2.Controls.Add(this.btnEmergency);
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.Controls.Add(this.btnStartRecording);
            this.panel2.Controls.Add(this.btnStopRecording);
            this.panel2.Controls.Add(this.btnReplay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1468, 58);
            this.panel2.TabIndex = 30;
            // 
            // simulatorBtn
            // 
            this.simulatorBtn.Location = new System.Drawing.Point(1383, 4);
            this.simulatorBtn.Name = "simulatorBtn";
            this.simulatorBtn.Size = new System.Drawing.Size(160, 37);
            this.simulatorBtn.TabIndex = 25;
            this.simulatorBtn.Text = "simulator";
            this.simulatorBtn.UseVisualStyleBackColor = true;
            this.simulatorBtn.Click += new System.EventHandler(this.simulatorBtn_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 674);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1468, 215);
            this.panel3.TabIndex = 31;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pbOpenCV3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pbOpenCV2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pbVideo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbOpenCV, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 58);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1468, 616);
            this.tableLayoutPanel1.TabIndex = 32;
            // 
            // pbOpenCV3
            // 
            this.pbOpenCV3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbOpenCV3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOpenCV3.Location = new System.Drawing.Point(4, 313);
            this.pbOpenCV3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbOpenCV3.Name = "pbOpenCV3";
            this.pbOpenCV3.Size = new System.Drawing.Size(726, 298);
            this.pbOpenCV3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOpenCV3.TabIndex = 7;
            this.pbOpenCV3.TabStop = false;
            // 
            // pbOpenCV2
            // 
            this.pbOpenCV2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbOpenCV2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOpenCV2.Location = new System.Drawing.Point(738, 313);
            this.pbOpenCV2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbOpenCV2.Name = "pbOpenCV2";
            this.pbOpenCV2.Size = new System.Drawing.Size(726, 298);
            this.pbOpenCV2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOpenCV2.TabIndex = 6;
            this.pbOpenCV2.TabStop = false;
            // 
            // pbVideo
            // 
            this.pbVideo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbVideo.Location = new System.Drawing.Point(4, 5);
            this.pbVideo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(726, 298);
            this.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbVideo.TabIndex = 4;
            this.pbVideo.TabStop = false;
            // 
            // pbOpenCV
            // 
            this.pbOpenCV.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbOpenCV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOpenCV.Location = new System.Drawing.Point(738, 5);
            this.pbOpenCV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbOpenCV.Name = "pbOpenCV";
            this.pbOpenCV.Size = new System.Drawing.Size(726, 298);
            this.pbOpenCV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOpenCV.TabIndex = 5;
            this.pbOpenCV.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbHighV, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbLowV, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbLowS, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbHighS, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbHighH, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(859, 215);
            this.tableLayoutPanel2.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbLowH);
            this.panel1.Controls.Add(this.lbLowH);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 65);
            this.panel1.TabIndex = 31;
            // 
            // lbLowH
            // 
            this.lbLowH.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbLowH.Location = new System.Drawing.Point(372, 0);
            this.lbLowH.Name = "lbLowH";
            this.lbLowH.Size = new System.Drawing.Size(51, 65);
            this.lbLowH.TabIndex = 28;
            this.lbLowH.Text = "label1";
            this.lbLowH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 889);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "AR.Drone Control";
            ((System.ComponentModel.ISupportInitialize)(this.tbLowH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowV)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnEmergency;
        private System.Windows.Forms.Timer tmrStateUpdate;
        private System.Windows.Forms.Timer tmrVideoUpdate;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStartRecording;
        private System.Windows.Forms.Button btnStopRecording;
        private System.Windows.Forms.Button btnReplay;
        private System.Windows.Forms.TrackBar tbLowH;
        private System.Windows.Forms.TrackBar tbHighH;
        private System.Windows.Forms.TrackBar tbLowS;
        private System.Windows.Forms.TrackBar tbHighS;
        private System.Windows.Forms.TrackBar tbHighV;
        private System.Windows.Forms.TrackBar tbLowV;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button simulatorBtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pbOpenCV3;
        private System.Windows.Forms.PictureBox pbOpenCV2;
        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.PictureBox pbOpenCV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbLowH;
    }
}

