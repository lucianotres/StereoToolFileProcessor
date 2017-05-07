using Newtonsoft.Json;
using StereoToolFileProcessor.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StereoToolFileProcessor
{
    public partial class FMain : Form
    {
        private string _appPath;
        private LameOptions _lame_cfg;
        private CancellationTokenSource _cts;

        public FMain()
        {
            InitializeComponent();

            //initialization vars
            _cts = null;
            _lstFiles = new List<string>();
            _appPath = AppDomain.CurrentDomain.BaseDirectory;

            //read configs
            rbCBR_CheckedChanged(this, EventArgs.Empty);
            ReadWriteConfig(false);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //certify that save configs on close
            ReadWriteConfig(true);
            base.OnClosing(e);
        }

        /// <summary>
        /// Read or Write the configs at Windows Registry
        /// </summary>
        /// <param name="write">True to write?</param>
        private void ReadWriteConfig(bool write)
        {
            if (write)
            {
                var cfg = new LameOptions();
                cfg.Birate = string.IsNullOrEmpty(cbxBirate.Text) ? 128 : int.Parse(cbxBirate.Text);
                cfg.BirateMax = string.IsNullOrEmpty(cbxBirateMax.Text) ? 256 : int.Parse(cbxBirateMax.Text);
                if (rbVBR.Checked)
                {
                    cfg.VBR = Convert.ToInt16(nudQuality.Value);
                }
                else
                {
                    cfg.VBR = null;
                }
                cfg.NoReplayGain = chkReplayGain.Checked;

                _lame_cfg = cfg;
                using (var reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\LTres\StereoToolFileProcessor", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    reg.SetValue("lame-config", JsonConvert.SerializeObject(cfg, Formatting.None));
                    reg.SetValue("path-origin", txtPathOrigin.Text);
                    reg.SetValue("path-destiny", txtPathDestiny.Text);
                    reg.SetValue("path-stereocfg", txtStereotool.Text);
                    reg.SetValue("replace", ckbReplace.Checked ? 1 : 0);
                }
            }
            else
            {
                using (var reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\LTres\StereoToolFileProcessor", false))
                {
                    if (reg != null)
                    {
                        var lameCfg = reg.GetValue("lame-config") as string;
                        txtPathOrigin.Text = reg.GetValue("path-origin") as string;
                        txtPathDestiny.Text = reg.GetValue("path-destiny") as string;
                        txtStereotool.Text = reg.GetValue("path-stereocfg") as string;
                        ckbReplace.Checked = (reg.GetValue("replace") as int?).GetValueOrDefault() == 1;
                        if (!string.IsNullOrEmpty(lameCfg))
                        {
                            var cfg = JsonConvert.DeserializeObject<LameOptions>(lameCfg);
                            _lame_cfg = cfg;

                            rbVBR.Checked = cfg.VBR.HasValue;
                            nudQuality.Value = cfg.VBR.GetValueOrDefault(4);
                            cbxBirate.Text = cfg.Birate.HasValue ? cfg.Birate.ToString() : "128";
                            cbxBirateMax.Text = cfg.BirateMax.HasValue ? cfg.BirateMax.ToString() : "256";
                            chkReplayGain.Checked = cfg.NoReplayGain;
                        }
                    }
                }
            } //if write
        }



        private List<string> _lstFiles;

        private async Task ExecuteConversion(CancellationToken ct)
        {
            string pathOrg = txtPathOrigin.Text;
            string pathDst = txtPathDestiny.Text;

            //list all files on the folder
            _lstFiles.Clear();
            FillFiles(pathOrg, "");

            //se progress size
            pbProgress.Maximum = _lstFiles.Count;
            pbProgress.Value = 0;

            //create the processor object
            var n = new MultiMp3STProcessor()
            {
                MaxConcurrentProcesses = 4,
                OutputPath = pathDst,
                STConfigFile = txtStereotool.Text,
                EncodeOptions = _lame_cfg
            };
            //event for progress
            n.ProcessProgress += (s, e) => pbProgress.Value = n.InputPaths.Where(w => w.Done).Count();

            //add the files to process
            foreach (var a in _lstFiles)
            {
                n.InputPaths.Add(new FileProcessInfo()
                {
                    Path = Path.Combine(pathOrg, a)
                });
            }

            //now execute all the process for all files
            await n.Execute(ct);

            //and finally
            pbProgress.Value = 0;
            if ((_cts == null) || !_cts.IsCancellationRequested)
            {
                MessageBox.Show("Process ended successfully!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Process canceled!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Fill files
        /// </summary>
        private void FillFiles(string pstOrg, string pst)
        {
            var dir = new DirectoryInfo(Path.Combine(pstOrg, pst));

            var zDir = dir.GetDirectories();
            foreach (var d in zDir)
                FillFiles(pstOrg, Path.Combine(pst, d.Name));

            var zArq = dir.GetFiles("*.mp3");
            foreach (var a in zArq)
                _lstFiles.Add(Path.Combine(pst, a.Name));
        }
        

        private void rbCBR_CheckedChanged(object sender, EventArgs e)
        {
            bool ehCBR = rbCBR.Checked;

            lblBirate.Text = (ehCBR ? "Bitrate" : "Bt.Min.");
            lblBirateMax.Visible = !ehCBR;
            lblQuality.Visible = !ehCBR;
            cbxBirateMax.Visible = !ehCBR;
            nudQuality.Visible = !ehCBR;
        }

        private string SwitchFolder(string origin)
        {
            fbdFolder.SelectedPath = origin;
            if (fbdFolder.ShowDialog() == DialogResult.OK)
                return fbdFolder.SelectedPath;
            else
                return origin;
        }

        private void btnPathOrigin_Click(object sender, EventArgs e)
        {
            txtPathOrigin.Text = SwitchFolder(txtPathOrigin.Text);
        }

        private void btnPathDestiny_Click(object sender, EventArgs e)
        {
            txtPathDestiny.Text = SwitchFolder(txtPathDestiny.Text);
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPathOrigin.Text))
            {
                MessageBox.Show("Need to inform the origin path!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrEmpty(txtPathDestiny.Text))
            {
                MessageBox.Show("Need to inform the destiny path!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            _cts = new CancellationTokenSource();
            ReadWriteConfig(true);
            try
            {
                tabConfigs.Enabled = false;
                tabConverter.Enabled = false;
                btnCancel.Visible = true;

                await ExecuteConversion(_cts.Token);
            }
            finally
            {
                tabConfigs.Enabled = true;
                tabConverter.Enabled = true;
                btnCancel.Visible = false;
            }
        }

        private void txtStereotool_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)0x08)
                txtStereotool.Text = "";
        }

        private void btnStereoTool_Click(object sender, EventArgs e)
        {
            ofdStereoTool.FileName = txtStereotool.Text;
            if (ofdStereoTool.ShowDialog() == DialogResult.OK)
                txtStereotool.Text = ofdStereoTool.FileName;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }

        private void fmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnCancelar_Click(btnCancel, EventArgs.Empty);
        }

    }
}
