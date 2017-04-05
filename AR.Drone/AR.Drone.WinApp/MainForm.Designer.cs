﻿namespace AR.Drone.WinApp
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
            this.btnFlatTrim = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnEmergency = new System.Windows.Forms.Button();
            this.tmrStateUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnSwitchCam = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnTurnLeft = new System.Windows.Forms.Button();
            this.btnTurnRight = new System.Windows.Forms.Button();
            this.btnHover = new System.Windows.Forms.Button();
            this.tvInfo = new System.Windows.Forms.TreeView();
            this.tmrVideoUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btnReadConfig = new System.Windows.Forms.Button();
            this.btnSendConfig = new System.Windows.Forms.Button();
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.btnStopRecording = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.btnAutopilot = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.pbOpenCV = new System.Windows.Forms.PictureBox();
            this.pbOpenCV3 = new System.Windows.Forms.PictureBox();
            this.pbOpenCV2 = new System.Windows.Forms.PictureBox();
            this.tbLowH = new System.Windows.Forms.TrackBar();
            this.tbHighH = new System.Windows.Forms.TrackBar();
            this.tbLowS = new System.Windows.Forms.TrackBar();
            this.tbHighS = new System.Windows.Forms.TrackBar();
            this.tbHighV = new System.Windows.Forms.TrackBar();
            this.tbLowV = new System.Windows.Forms.TrackBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowV)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(18, 18);
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
            this.btnStop.Location = new System.Drawing.Point(140, 18);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(112, 35);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Deactivate";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnFlatTrim
            // 
            this.btnFlatTrim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlatTrim.Location = new System.Drawing.Point(1331, 976);
            this.btnFlatTrim.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnFlatTrim.Name = "btnFlatTrim";
            this.btnFlatTrim.Size = new System.Drawing.Size(112, 35);
            this.btnFlatTrim.TabIndex = 3;
            this.btnFlatTrim.Text = "Flat Trim";
            this.btnFlatTrim.UseVisualStyleBackColor = true;
            this.btnFlatTrim.Click += new System.EventHandler(this.btnFlatTrim_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1460, 976);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 4;
            this.button2.Text = "Takeoff";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(1583, 976);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 35);
            this.button3.TabIndex = 5;
            this.button3.Text = "Land";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnEmergency
            // 
            this.btnEmergency.Location = new System.Drawing.Point(854, 18);
            this.btnEmergency.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEmergency.Name = "btnEmergency";
            this.btnEmergency.Size = new System.Drawing.Size(124, 35);
            this.btnEmergency.TabIndex = 6;
            this.btnEmergency.Text = "Emergency";
            this.btnEmergency.UseVisualStyleBackColor = true;
            this.btnEmergency.Click += new System.EventHandler(this.btnEmergency_Click);
            // 
            // tmrStateUpdate
            // 
            this.tmrStateUpdate.Interval = 500;
            this.tmrStateUpdate.Tick += new System.EventHandler(this.tmrStateUpdate_Tick);
            // 
            // btnSwitchCam
            // 
            this.btnSwitchCam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitchCam.Location = new System.Drawing.Point(1331, 1133);
            this.btnSwitchCam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSwitchCam.Name = "btnSwitchCam";
            this.btnSwitchCam.Size = new System.Drawing.Size(134, 35);
            this.btnSwitchCam.TabIndex = 8;
            this.btnSwitchCam.Text = "Video Channel";
            this.btnSwitchCam.UseVisualStyleBackColor = true;
            this.btnSwitchCam.Click += new System.EventHandler(this.btnSwitchCam_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Location = new System.Drawing.Point(1460, 1028);
            this.btnUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(112, 35);
            this.btnUp.TabIndex = 9;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Location = new System.Drawing.Point(1460, 1075);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(112, 35);
            this.btnDown.TabIndex = 10;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeft.Location = new System.Drawing.Point(1583, 1075);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(112, 35);
            this.btnLeft.TabIndex = 11;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(1706, 1073);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(112, 35);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRight.Location = new System.Drawing.Point(1829, 1073);
            this.btnRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(112, 35);
            this.btnRight.TabIndex = 13;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnForward
            // 
            this.btnForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForward.Location = new System.Drawing.Point(1706, 1028);
            this.btnForward.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(112, 35);
            this.btnForward.TabIndex = 14;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnTurnLeft
            // 
            this.btnTurnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTurnLeft.Location = new System.Drawing.Point(1585, 1030);
            this.btnTurnLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTurnLeft.Name = "btnTurnLeft";
            this.btnTurnLeft.Size = new System.Drawing.Size(112, 35);
            this.btnTurnLeft.TabIndex = 15;
            this.btnTurnLeft.Text = "Turn Left";
            this.btnTurnLeft.UseVisualStyleBackColor = true;
            this.btnTurnLeft.Click += new System.EventHandler(this.btnTurnLeft_Click);
            // 
            // btnTurnRight
            // 
            this.btnTurnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTurnRight.Location = new System.Drawing.Point(1827, 1030);
            this.btnTurnRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTurnRight.Name = "btnTurnRight";
            this.btnTurnRight.Size = new System.Drawing.Size(112, 35);
            this.btnTurnRight.TabIndex = 16;
            this.btnTurnRight.Text = "Turn Right";
            this.btnTurnRight.UseVisualStyleBackColor = true;
            this.btnTurnRight.Click += new System.EventHandler(this.btnTurnRight_Click);
            // 
            // btnHover
            // 
            this.btnHover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHover.Location = new System.Drawing.Point(1706, 976);
            this.btnHover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHover.Name = "btnHover";
            this.btnHover.Size = new System.Drawing.Size(112, 35);
            this.btnHover.TabIndex = 17;
            this.btnHover.Text = "Hover";
            this.btnHover.UseVisualStyleBackColor = true;
            this.btnHover.Click += new System.EventHandler(this.btnHover_Click);
            // 
            // tvInfo
            // 
            this.tvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvInfo.Location = new System.Drawing.Point(1331, 63);
            this.tvInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tvInfo.Name = "tvInfo";
            this.tvInfo.Size = new System.Drawing.Size(678, 874);
            this.tvInfo.TabIndex = 18;
            // 
            // tmrVideoUpdate
            // 
            this.tmrVideoUpdate.Interval = 20;
            this.tmrVideoUpdate.Tick += new System.EventHandler(this.tmrVideoUpdate_Tick);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(720, 18);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(124, 35);
            this.btnReset.TabIndex = 19;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnReadConfig
            // 
            this.btnReadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadConfig.Location = new System.Drawing.Point(1331, 1187);
            this.btnReadConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReadConfig.Name = "btnReadConfig";
            this.btnReadConfig.Size = new System.Drawing.Size(134, 35);
            this.btnReadConfig.TabIndex = 20;
            this.btnReadConfig.Text = "Read Config";
            this.btnReadConfig.UseVisualStyleBackColor = true;
            this.btnReadConfig.Click += new System.EventHandler(this.btnReadConfig_Click);
            // 
            // btnSendConfig
            // 
            this.btnSendConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendConfig.Location = new System.Drawing.Point(1331, 1232);
            this.btnSendConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSendConfig.Name = "btnSendConfig";
            this.btnSendConfig.Size = new System.Drawing.Size(134, 35);
            this.btnSendConfig.TabIndex = 21;
            this.btnSendConfig.Text = "Send Config";
            this.btnSendConfig.UseVisualStyleBackColor = true;
            this.btnSendConfig.Click += new System.EventHandler(this.btnSendConfig_Click);
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.Location = new System.Drawing.Point(261, 18);
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
            this.btnStopRecording.Location = new System.Drawing.Point(382, 18);
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
            this.btnReplay.Location = new System.Drawing.Point(504, 18);
            this.btnReplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(112, 35);
            this.btnReplay.TabIndex = 24;
            this.btnReplay.Text = "Replay";
            this.btnReplay.UseVisualStyleBackColor = true;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // btnAutopilot
            // 
            this.btnAutopilot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutopilot.Location = new System.Drawing.Point(1331, 1075);
            this.btnAutopilot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAutopilot.Name = "btnAutopilot";
            this.btnAutopilot.Size = new System.Drawing.Size(112, 35);
            this.btnAutopilot.TabIndex = 25;
            this.btnAutopilot.Text = "Auto&pilot";
            this.btnAutopilot.UseVisualStyleBackColor = true;
            this.btnAutopilot.Click += new System.EventHandler(this.btnAutopilot_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbOpenCV3);
            this.panel1.Controls.Add(this.pbOpenCV2);
            this.panel1.Controls.Add(this.pbOpenCV);
            this.panel1.Controls.Add(this.pbVideo);
            this.panel1.Location = new System.Drawing.Point(18, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1306, 984);
            this.panel1.TabIndex = 26;
            // 
            // pbVideo
            // 
            this.pbVideo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbVideo.Location = new System.Drawing.Point(4, 5);
            this.pbVideo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(640, 480);
            this.pbVideo.TabIndex = 3;
            this.pbVideo.TabStop = false;
            // 
            // pbOpenCV
            // 
            this.pbOpenCV.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbOpenCV.Location = new System.Drawing.Point(652, 5);
            this.pbOpenCV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbOpenCV.Name = "pbOpenCV";
            this.pbOpenCV.Size = new System.Drawing.Size(640, 480);
            this.pbOpenCV.TabIndex = 4;
            this.pbOpenCV.TabStop = false;
            // 
            // pbOpenCV3
            // 
            this.pbOpenCV3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbOpenCV3.Location = new System.Drawing.Point(652, 495);
            this.pbOpenCV3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbOpenCV3.Name = "pbOpenCV3";
            this.pbOpenCV3.Size = new System.Drawing.Size(640, 480);
            this.pbOpenCV3.TabIndex = 6;
            this.pbOpenCV3.TabStop = false;
            // 
            // pbOpenCV2
            // 
            this.pbOpenCV2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbOpenCV2.Location = new System.Drawing.Point(4, 495);
            this.pbOpenCV2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbOpenCV2.Name = "pbOpenCV2";
            this.pbOpenCV2.Size = new System.Drawing.Size(640, 480);
            this.pbOpenCV2.TabIndex = 5;
            this.pbOpenCV2.TabStop = false;
            // 
            // tbLowH
            // 
            this.tbLowH.Location = new System.Drawing.Point(52, 1053);
            this.tbLowH.Maximum = 255;
            this.tbLowH.Name = "tbLowH";
            this.tbLowH.Size = new System.Drawing.Size(502, 69);
            this.tbLowH.TabIndex = 27;
            this.tbLowH.Value = 170;
            // 
            // tbHighH
            // 
            this.tbHighH.Location = new System.Drawing.Point(670, 1053);
            this.tbHighH.Maximum = 255;
            this.tbHighH.Name = "tbHighH";
            this.tbHighH.Size = new System.Drawing.Size(502, 69);
            this.tbHighH.TabIndex = 28;
            this.tbHighH.Value = 179;
            // 
            // tbLowS
            // 
            this.tbLowS.Location = new System.Drawing.Point(52, 1118);
            this.tbLowS.Maximum = 255;
            this.tbLowS.Name = "tbLowS";
            this.tbLowS.Size = new System.Drawing.Size(502, 69);
            this.tbLowS.TabIndex = 29;
            this.tbLowS.Value = 150;
            // 
            // tbHighS
            // 
            this.tbHighS.Location = new System.Drawing.Point(670, 1118);
            this.tbHighS.Maximum = 255;
            this.tbHighS.Name = "tbHighS";
            this.tbHighS.Size = new System.Drawing.Size(502, 69);
            this.tbHighS.TabIndex = 29;
            this.tbHighS.Value = 255;
            // 
            // tbHighV
            // 
            this.tbHighV.Location = new System.Drawing.Point(670, 1187);
            this.tbHighV.Maximum = 255;
            this.tbHighV.Name = "tbHighV";
            this.tbHighV.Size = new System.Drawing.Size(502, 69);
            this.tbHighV.TabIndex = 29;
            this.tbHighV.Value = 255;
            // 
            // tbLowV
            // 
            this.tbLowV.Location = new System.Drawing.Point(52, 1187);
            this.tbLowV.Maximum = 255;
            this.tbLowV.Name = "tbLowV";
            this.tbLowV.Size = new System.Drawing.Size(502, 69);
            this.tbLowV.TabIndex = 29;
            this.tbLowV.Value = 60;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2029, 1296);
            this.Controls.Add(this.tbLowV);
            this.Controls.Add(this.tbHighV);
            this.Controls.Add(this.tbHighS);
            this.Controls.Add(this.tbLowS);
            this.Controls.Add(this.tbHighH);
            this.Controls.Add(this.tbLowH);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAutopilot);
            this.Controls.Add(this.btnReplay);
            this.Controls.Add(this.btnStopRecording);
            this.Controls.Add(this.btnStartRecording);
            this.Controls.Add(this.btnSendConfig);
            this.Controls.Add(this.btnReadConfig);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tvInfo);
            this.Controls.Add(this.btnHover);
            this.Controls.Add(this.btnTurnRight);
            this.Controls.Add(this.btnTurnLeft);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnSwitchCam);
            this.Controls.Add(this.btnEmergency);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnFlatTrim);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "AR.Drone Control";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenCV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnFlatTrim;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnEmergency;
        private System.Windows.Forms.Timer tmrStateUpdate;
        private System.Windows.Forms.Button btnSwitchCam;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnTurnLeft;
        private System.Windows.Forms.Button btnTurnRight;
        private System.Windows.Forms.Button btnHover;
        private System.Windows.Forms.TreeView tvInfo;
        private System.Windows.Forms.Timer tmrVideoUpdate;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnReadConfig;
        private System.Windows.Forms.Button btnSendConfig;
        private System.Windows.Forms.Button btnStartRecording;
        private System.Windows.Forms.Button btnStopRecording;
        private System.Windows.Forms.Button btnReplay;
        private System.Windows.Forms.Button btnAutopilot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbOpenCV;
        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.PictureBox pbOpenCV3;
        private System.Windows.Forms.PictureBox pbOpenCV2;
        private System.Windows.Forms.TrackBar tbLowH;
        private System.Windows.Forms.TrackBar tbHighH;
        private System.Windows.Forms.TrackBar tbLowS;
        private System.Windows.Forms.TrackBar tbHighS;
        private System.Windows.Forms.TrackBar tbHighV;
        private System.Windows.Forms.TrackBar tbLowV;
    }
}

