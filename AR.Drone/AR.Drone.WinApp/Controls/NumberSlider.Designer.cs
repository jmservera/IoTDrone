namespace AR.Drone.WinApp.Controls
{
    partial class NumberSlider
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
            this.tbLowH = new System.Windows.Forms.TrackBar();
            this.lbLowH = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowH)).BeginInit();
            this.SuspendLayout();
            // 
            // tbLowH
            // 
            this.tbLowH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLowH.Location = new System.Drawing.Point(0, 0);
            this.tbLowH.Maximum = 255;
            this.tbLowH.Name = "tbLowH";
            this.tbLowH.Size = new System.Drawing.Size(368, 51);
            this.tbLowH.TabIndex = 29;
            this.tbLowH.Value = 170;
            this.tbLowH.ValueChanged += new System.EventHandler(this.tbLowH_ValueChanged);
            // 
            // lbLowH
            // 
            this.lbLowH.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbLowH.Location = new System.Drawing.Point(368, 0);
            this.lbLowH.Name = "lbLowH";
            this.lbLowH.Size = new System.Drawing.Size(66, 51);
            this.lbLowH.TabIndex = 30;
            this.lbLowH.Text = "label1";
            this.lbLowH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NumberSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbLowH);
            this.Controls.Add(this.lbLowH);
            this.Name = "NumberSlider";
            this.Size = new System.Drawing.Size(434, 51);
            ((System.ComponentModel.ISupportInitialize)(this.tbLowH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbLowH;
        private System.Windows.Forms.Label lbLowH;
    }
}
