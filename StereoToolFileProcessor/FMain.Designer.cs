namespace StereoToolFileProcessor
{
    partial class FMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.ofdStereoTool = new System.Windows.Forms.OpenFileDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabConfigs = new System.Windows.Forms.TabPage();
            this.chkReplayGain = new System.Windows.Forms.CheckBox();
            this.gbxBitrate = new System.Windows.Forms.GroupBox();
            this.nudQuality = new System.Windows.Forms.NumericUpDown();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cbxBirateMax = new System.Windows.Forms.ComboBox();
            this.lblBirateMax = new System.Windows.Forms.Label();
            this.cbxBirate = new System.Windows.Forms.ComboBox();
            this.lblBirate = new System.Windows.Forms.Label();
            this.rbVBR = new System.Windows.Forms.RadioButton();
            this.rbCBR = new System.Windows.Forms.RadioButton();
            this.tabConverter = new System.Windows.Forms.TabPage();
            this.btnStereoTool = new System.Windows.Forms.Button();
            this.txtStereotool = new System.Windows.Forms.TextBox();
            this.txtPathDestiny = new System.Windows.Forms.TextBox();
            this.txtPathOrigin = new System.Windows.Forms.TextBox();
            this.lblStereoConfig = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.ckbReplace = new System.Windows.Forms.CheckBox();
            this.btnPathDestiny = new System.Windows.Forms.Button();
            this.lblPathDestiny = new System.Windows.Forms.Label();
            this.btnPathOrigin = new System.Windows.Forms.Button();
            this.lblPathOrigin = new System.Windows.Forms.Label();
            this.tabSTC = new System.Windows.Forms.TabControl();
            this.tabConfigs.SuspendLayout();
            this.gbxBitrate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            this.tabConverter.SuspendLayout();
            this.tabSTC.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbProgress
            // 
            resources.ApplyResources(this.pbProgress, "pbProgress");
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // ofdStereoTool
            // 
            resources.ApplyResources(this.ofdStereoTool, "ofdStereoTool");
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // tabConfigs
            // 
            this.tabConfigs.Controls.Add(this.chkReplayGain);
            this.tabConfigs.Controls.Add(this.gbxBitrate);
            resources.ApplyResources(this.tabConfigs, "tabConfigs");
            this.tabConfigs.Name = "tabConfigs";
            this.tabConfigs.UseVisualStyleBackColor = true;
            // 
            // chkReplayGain
            // 
            resources.ApplyResources(this.chkReplayGain, "chkReplayGain");
            this.chkReplayGain.Name = "chkReplayGain";
            this.chkReplayGain.UseVisualStyleBackColor = true;
            // 
            // gbxBitrate
            // 
            resources.ApplyResources(this.gbxBitrate, "gbxBitrate");
            this.gbxBitrate.Controls.Add(this.nudQuality);
            this.gbxBitrate.Controls.Add(this.lblQuality);
            this.gbxBitrate.Controls.Add(this.cbxBirateMax);
            this.gbxBitrate.Controls.Add(this.lblBirateMax);
            this.gbxBitrate.Controls.Add(this.cbxBirate);
            this.gbxBitrate.Controls.Add(this.lblBirate);
            this.gbxBitrate.Controls.Add(this.rbVBR);
            this.gbxBitrate.Controls.Add(this.rbCBR);
            this.gbxBitrate.Name = "gbxBitrate";
            this.gbxBitrate.TabStop = false;
            // 
            // nudQuality
            // 
            resources.ApplyResources(this.nudQuality, "nudQuality");
            this.nudQuality.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // lblQuality
            // 
            resources.ApplyResources(this.lblQuality, "lblQuality");
            this.lblQuality.Name = "lblQuality";
            // 
            // cbxBirateMax
            // 
            this.cbxBirateMax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBirateMax.FormattingEnabled = true;
            this.cbxBirateMax.Items.AddRange(new object[] {
            resources.GetString("cbxBirateMax.Items"),
            resources.GetString("cbxBirateMax.Items1"),
            resources.GetString("cbxBirateMax.Items2"),
            resources.GetString("cbxBirateMax.Items3"),
            resources.GetString("cbxBirateMax.Items4"),
            resources.GetString("cbxBirateMax.Items5"),
            resources.GetString("cbxBirateMax.Items6"),
            resources.GetString("cbxBirateMax.Items7"),
            resources.GetString("cbxBirateMax.Items8"),
            resources.GetString("cbxBirateMax.Items9"),
            resources.GetString("cbxBirateMax.Items10"),
            resources.GetString("cbxBirateMax.Items11"),
            resources.GetString("cbxBirateMax.Items12"),
            resources.GetString("cbxBirateMax.Items13"),
            resources.GetString("cbxBirateMax.Items14"),
            resources.GetString("cbxBirateMax.Items15"),
            resources.GetString("cbxBirateMax.Items16")});
            resources.ApplyResources(this.cbxBirateMax, "cbxBirateMax");
            this.cbxBirateMax.Name = "cbxBirateMax";
            // 
            // lblBirateMax
            // 
            resources.ApplyResources(this.lblBirateMax, "lblBirateMax");
            this.lblBirateMax.Name = "lblBirateMax";
            // 
            // cbxBirate
            // 
            this.cbxBirate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBirate.FormattingEnabled = true;
            this.cbxBirate.Items.AddRange(new object[] {
            resources.GetString("cbxBirate.Items"),
            resources.GetString("cbxBirate.Items1"),
            resources.GetString("cbxBirate.Items2"),
            resources.GetString("cbxBirate.Items3"),
            resources.GetString("cbxBirate.Items4"),
            resources.GetString("cbxBirate.Items5"),
            resources.GetString("cbxBirate.Items6"),
            resources.GetString("cbxBirate.Items7"),
            resources.GetString("cbxBirate.Items8"),
            resources.GetString("cbxBirate.Items9"),
            resources.GetString("cbxBirate.Items10"),
            resources.GetString("cbxBirate.Items11"),
            resources.GetString("cbxBirate.Items12"),
            resources.GetString("cbxBirate.Items13"),
            resources.GetString("cbxBirate.Items14"),
            resources.GetString("cbxBirate.Items15"),
            resources.GetString("cbxBirate.Items16")});
            resources.ApplyResources(this.cbxBirate, "cbxBirate");
            this.cbxBirate.Name = "cbxBirate";
            // 
            // lblBirate
            // 
            resources.ApplyResources(this.lblBirate, "lblBirate");
            this.lblBirate.Name = "lblBirate";
            // 
            // rbVBR
            // 
            resources.ApplyResources(this.rbVBR, "rbVBR");
            this.rbVBR.Name = "rbVBR";
            this.rbVBR.UseVisualStyleBackColor = true;
            this.rbVBR.CheckedChanged += new System.EventHandler(this.rbCBR_CheckedChanged);
            // 
            // rbCBR
            // 
            resources.ApplyResources(this.rbCBR, "rbCBR");
            this.rbCBR.Checked = true;
            this.rbCBR.Name = "rbCBR";
            this.rbCBR.TabStop = true;
            this.rbCBR.UseVisualStyleBackColor = true;
            this.rbCBR.CheckedChanged += new System.EventHandler(this.rbCBR_CheckedChanged);
            // 
            // tabConverter
            // 
            this.tabConverter.Controls.Add(this.btnStereoTool);
            this.tabConverter.Controls.Add(this.txtStereotool);
            this.tabConverter.Controls.Add(this.txtPathDestiny);
            this.tabConverter.Controls.Add(this.txtPathOrigin);
            this.tabConverter.Controls.Add(this.lblStereoConfig);
            this.tabConverter.Controls.Add(this.btnExecute);
            this.tabConverter.Controls.Add(this.ckbReplace);
            this.tabConverter.Controls.Add(this.btnPathDestiny);
            this.tabConverter.Controls.Add(this.lblPathDestiny);
            this.tabConverter.Controls.Add(this.btnPathOrigin);
            this.tabConverter.Controls.Add(this.lblPathOrigin);
            resources.ApplyResources(this.tabConverter, "tabConverter");
            this.tabConverter.Name = "tabConverter";
            this.tabConverter.UseVisualStyleBackColor = true;
            // 
            // btnStereoTool
            // 
            resources.ApplyResources(this.btnStereoTool, "btnStereoTool");
            this.btnStereoTool.Name = "btnStereoTool";
            this.btnStereoTool.UseVisualStyleBackColor = true;
            this.btnStereoTool.Click += new System.EventHandler(this.btnStereoTool_Click);
            // 
            // txtStereotool
            // 
            resources.ApplyResources(this.txtStereotool, "txtStereotool");
            this.txtStereotool.Name = "txtStereotool";
            this.txtStereotool.ReadOnly = true;
            this.txtStereotool.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStereotool_KeyPress);
            // 
            // txtPathDestiny
            // 
            resources.ApplyResources(this.txtPathDestiny, "txtPathDestiny");
            this.txtPathDestiny.Name = "txtPathDestiny";
            this.txtPathDestiny.ReadOnly = true;
            // 
            // txtPathOrigin
            // 
            resources.ApplyResources(this.txtPathOrigin, "txtPathOrigin");
            this.txtPathOrigin.Name = "txtPathOrigin";
            this.txtPathOrigin.ReadOnly = true;
            // 
            // lblStereoConfig
            // 
            resources.ApplyResources(this.lblStereoConfig, "lblStereoConfig");
            this.lblStereoConfig.Name = "lblStereoConfig";
            // 
            // btnExecute
            // 
            resources.ApplyResources(this.btnExecute, "btnExecute");
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // ckbReplace
            // 
            resources.ApplyResources(this.ckbReplace, "ckbReplace");
            this.ckbReplace.Name = "ckbReplace";
            this.ckbReplace.UseVisualStyleBackColor = true;
            // 
            // btnPathDestiny
            // 
            resources.ApplyResources(this.btnPathDestiny, "btnPathDestiny");
            this.btnPathDestiny.Name = "btnPathDestiny";
            this.btnPathDestiny.UseVisualStyleBackColor = true;
            this.btnPathDestiny.Click += new System.EventHandler(this.btnPathDestiny_Click);
            // 
            // lblPathDestiny
            // 
            resources.ApplyResources(this.lblPathDestiny, "lblPathDestiny");
            this.lblPathDestiny.Name = "lblPathDestiny";
            // 
            // btnPathOrigin
            // 
            resources.ApplyResources(this.btnPathOrigin, "btnPathOrigin");
            this.btnPathOrigin.Name = "btnPathOrigin";
            this.btnPathOrigin.UseVisualStyleBackColor = true;
            this.btnPathOrigin.Click += new System.EventHandler(this.btnPathOrigin_Click);
            // 
            // lblPathOrigin
            // 
            resources.ApplyResources(this.lblPathOrigin, "lblPathOrigin");
            this.lblPathOrigin.Name = "lblPathOrigin";
            // 
            // tabSTC
            // 
            this.tabSTC.Controls.Add(this.tabConverter);
            this.tabSTC.Controls.Add(this.tabConfigs);
            resources.ApplyResources(this.tabSTC, "tabSTC");
            this.tabSTC.Name = "tabSTC";
            this.tabSTC.SelectedIndex = 0;
            // 
            // FMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabSTC);
            this.Controls.Add(this.pbProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmPrincipal_FormClosing);
            this.tabConfigs.ResumeLayout(false);
            this.tabConfigs.PerformLayout();
            this.gbxBitrate.ResumeLayout(false);
            this.gbxBitrate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
            this.tabConverter.ResumeLayout(false);
            this.tabConverter.PerformLayout();
            this.tabSTC.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.OpenFileDialog ofdStereoTool;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabConfigs;
        private System.Windows.Forms.CheckBox chkReplayGain;
        private System.Windows.Forms.GroupBox gbxBitrate;
        private System.Windows.Forms.NumericUpDown nudQuality;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.ComboBox cbxBirateMax;
        private System.Windows.Forms.Label lblBirateMax;
        private System.Windows.Forms.ComboBox cbxBirate;
        private System.Windows.Forms.Label lblBirate;
        private System.Windows.Forms.RadioButton rbVBR;
        private System.Windows.Forms.RadioButton rbCBR;
        private System.Windows.Forms.TabPage tabConverter;
        private System.Windows.Forms.Button btnStereoTool;
        private System.Windows.Forms.TextBox txtStereotool;
        private System.Windows.Forms.TextBox txtPathDestiny;
        private System.Windows.Forms.TextBox txtPathOrigin;
        private System.Windows.Forms.Label lblStereoConfig;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.CheckBox ckbReplace;
        private System.Windows.Forms.Button btnPathDestiny;
        private System.Windows.Forms.Label lblPathDestiny;
        private System.Windows.Forms.Button btnPathOrigin;
        private System.Windows.Forms.Label lblPathOrigin;
        private System.Windows.Forms.TabControl tabSTC;
    }
}

