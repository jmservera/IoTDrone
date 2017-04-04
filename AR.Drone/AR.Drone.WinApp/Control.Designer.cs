namespace AR.Drone.WinApp
{
    partial class Control
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
            this.tmrStateUpdate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrStateUpdate
            // 
            this.tmrStateUpdate.Interval = 500;
            // 
            // Control
            // 
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "Control";
            this.ResumeLayout(false);

        }

        #endregion

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