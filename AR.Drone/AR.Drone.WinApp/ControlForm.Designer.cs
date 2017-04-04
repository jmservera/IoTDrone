namespace AR.Drone.WinApp
{
    partial class ControlForm
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
            this.tvInfo = new System.Windows.Forms.TreeView();
            this.btnAutopilot = new System.Windows.Forms.Button();
            this.btnSendConfig = new System.Windows.Forms.Button();
            this.btnReadConfig = new System.Windows.Forms.Button();
            this.btnHover = new System.Windows.Forms.Button();
            this.btnTurnRight = new System.Windows.Forms.Button();
            this.btnTurnLeft = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnSwitchCam = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnFlatTrim = new System.Windows.Forms.Button();
            this.tmrStateUpdate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tvInfo
            // 
            this.tvInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.tvInfo.Location = new System.Drawing.Point(675, 0);
            this.tvInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tvInfo.Name = "tvInfo";
            this.tvInfo.Size = new System.Drawing.Size(835, 791);
            this.tvInfo.TabIndex = 19;
            // 
            // btnAutopilot
            // 
            this.btnAutopilot.Location = new System.Drawing.Point(13, 113);
            this.btnAutopilot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAutopilot.Name = "btnAutopilot";
            this.btnAutopilot.Size = new System.Drawing.Size(112, 35);
            this.btnAutopilot.TabIndex = 41;
            this.btnAutopilot.Text = "Auto&pilot";
            this.btnAutopilot.UseVisualStyleBackColor = true;
            this.btnAutopilot.Click += new System.EventHandler(this.btnAutopilot_Click);
            // 
            // btnSendConfig
            // 
            this.btnSendConfig.Location = new System.Drawing.Point(13, 270);
            this.btnSendConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSendConfig.Name = "btnSendConfig";
            this.btnSendConfig.Size = new System.Drawing.Size(134, 35);
            this.btnSendConfig.TabIndex = 40;
            this.btnSendConfig.Text = "Send Config";
            this.btnSendConfig.UseVisualStyleBackColor = true;
            this.btnSendConfig.Click += new System.EventHandler(this.btnSendConfig_Click);
            // 
            // btnReadConfig
            // 
            this.btnReadConfig.Location = new System.Drawing.Point(13, 225);
            this.btnReadConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReadConfig.Name = "btnReadConfig";
            this.btnReadConfig.Size = new System.Drawing.Size(134, 35);
            this.btnReadConfig.TabIndex = 39;
            this.btnReadConfig.Text = "Read Config";
            this.btnReadConfig.UseVisualStyleBackColor = true;
            this.btnReadConfig.Click += new System.EventHandler(this.btnReadConfig_Click);
            // 
            // btnHover
            // 
            this.btnHover.Location = new System.Drawing.Point(388, 14);
            this.btnHover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHover.Name = "btnHover";
            this.btnHover.Size = new System.Drawing.Size(112, 35);
            this.btnHover.TabIndex = 38;
            this.btnHover.Text = "Hover";
            this.btnHover.UseVisualStyleBackColor = true;
            this.btnHover.Click += new System.EventHandler(this.btnHover_Click);
            // 
            // btnTurnRight
            // 
            this.btnTurnRight.Location = new System.Drawing.Point(509, 68);
            this.btnTurnRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTurnRight.Name = "btnTurnRight";
            this.btnTurnRight.Size = new System.Drawing.Size(112, 35);
            this.btnTurnRight.TabIndex = 37;
            this.btnTurnRight.Text = "Turn Right";
            this.btnTurnRight.UseVisualStyleBackColor = true;
            this.btnTurnRight.Click += new System.EventHandler(this.btnTurnRight_Click);
            // 
            // btnTurnLeft
            // 
            this.btnTurnLeft.Location = new System.Drawing.Point(267, 68);
            this.btnTurnLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTurnLeft.Name = "btnTurnLeft";
            this.btnTurnLeft.Size = new System.Drawing.Size(112, 35);
            this.btnTurnLeft.TabIndex = 36;
            this.btnTurnLeft.Text = "Turn Left";
            this.btnTurnLeft.UseVisualStyleBackColor = true;
            this.btnTurnLeft.Click += new System.EventHandler(this.btnTurnLeft_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(388, 66);
            this.btnForward.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(112, 35);
            this.btnForward.TabIndex = 35;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(511, 111);
            this.btnRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(112, 35);
            this.btnRight.TabIndex = 34;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(388, 111);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(112, 35);
            this.btnBack.TabIndex = 33;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(265, 113);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(112, 35);
            this.btnLeft.TabIndex = 32;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(142, 113);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(112, 35);
            this.btnDown.TabIndex = 31;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(142, 66);
            this.btnUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(112, 35);
            this.btnUp.TabIndex = 30;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnSwitchCam
            // 
            this.btnSwitchCam.Location = new System.Drawing.Point(13, 171);
            this.btnSwitchCam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSwitchCam.Name = "btnSwitchCam";
            this.btnSwitchCam.Size = new System.Drawing.Size(134, 35);
            this.btnSwitchCam.TabIndex = 29;
            this.btnSwitchCam.Text = "Video Channel";
            this.btnSwitchCam.UseVisualStyleBackColor = true;
            this.btnSwitchCam.Click += new System.EventHandler(this.btnSwitchCam_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(265, 14);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 35);
            this.button3.TabIndex = 28;
            this.button3.Text = "Land";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(142, 14);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 27;
            this.button2.Text = "Takeoff";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnFlatTrim
            // 
            this.btnFlatTrim.Location = new System.Drawing.Point(13, 14);
            this.btnFlatTrim.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnFlatTrim.Name = "btnFlatTrim";
            this.btnFlatTrim.Size = new System.Drawing.Size(112, 35);
            this.btnFlatTrim.TabIndex = 26;
            this.btnFlatTrim.Text = "Flat Trim";
            this.btnFlatTrim.UseVisualStyleBackColor = true;
            this.btnFlatTrim.Click += new System.EventHandler(this.btnFlatTrim_Click);
            // 
            // tmrStateUpdate
            // 
            this.tmrStateUpdate.Interval = 500;
            this.tmrStateUpdate.Tick += new System.EventHandler(this.tmrStateUpdate_Tick);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1510, 791);
            this.Controls.Add(this.btnAutopilot);
            this.Controls.Add(this.btnSendConfig);
            this.Controls.Add(this.btnReadConfig);
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
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnFlatTrim);
            this.Controls.Add(this.tvInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ControlForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ControlForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvInfo;
        private System.Windows.Forms.Button btnAutopilot;
        private System.Windows.Forms.Button btnSendConfig;
        private System.Windows.Forms.Button btnReadConfig;
        private System.Windows.Forms.Button btnHover;
        private System.Windows.Forms.Button btnTurnRight;
        private System.Windows.Forms.Button btnTurnLeft;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnSwitchCam;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnFlatTrim;
        private System.Windows.Forms.Timer tmrStateUpdate;
    }
}