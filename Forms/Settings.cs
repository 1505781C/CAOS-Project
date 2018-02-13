using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerArchitectureAndOperatingSystems.Forms {
    public partial class Settings : Form {

        Boolean default_test = true;

        public Settings() {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e) {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            load_Default();
        }

        private void load_Default() {
            if (default_test == true) {
                txtPagesize.Text = "8";

                txtPartition1.Enabled = true;
                txtPartition1.Text = "180";
                checkParition1.Checked = true;

                txtPartition2.Enabled = false;
                txtPartition2.Text = "";
                checkParition2.Checked = false;

                txtPartition3.Enabled = true;
                txtPartition3.Text = "130";
                checkParition3.Checked = true;

                txtPartition4.Enabled = true;
                txtPartition4.Text = "80";
                checkParition4.Checked = true;

                txtPartition5.Enabled = false;
                txtPartition5.Text = "";
                checkParition5.Checked = false;

                txtPartition6.Enabled = false;
                txtPartition6.Text = "";
                checkParition6.Checked = false;

                txtPartition7.Enabled = false;
                txtPartition7.Text = "";
                checkParition7.Checked = false;

                txtPartition8.Enabled = false;
                txtPartition8.Text = "";
                checkParition8.Checked = false;

                txtPartition9.Enabled = false;
                txtPartition9.Text = "";
                checkParition9.Checked = false;

                txtPartition10.Enabled = false;
                txtPartition10.Text = "";
                checkParition10.Checked = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPagesize.Text = "8";

            txtPartition1.Enabled = true;
            txtPartition1.Text = "180";
            checkParition1.Checked = true;

            txtPartition2.Enabled = false;
            txtPartition2.Text = "";
            checkParition2.Checked = false;

            txtPartition3.Enabled = true;
            txtPartition3.Text = "130";
            checkParition3.Checked = true;

            txtPartition4.Enabled = true;
            txtPartition4.Text = "80";
            checkParition4.Checked = true;

            txtPartition5.Enabled = false;
            txtPartition5.Text = "";
            checkParition5.Checked = false;

            txtPartition6.Enabled = false;
            txtPartition6.Text = "";
            checkParition6.Checked = false;

            txtPartition7.Enabled = false;
            txtPartition7.Text = "";
            checkParition7.Checked = false;

            txtPartition8.Enabled = false;
            txtPartition8.Text = "";
            checkParition8.Checked = false;

            txtPartition9.Enabled = false;
            txtPartition9.Text = "";
            checkParition9.Checked = false;

            txtPartition10.Enabled = false;
            txtPartition10.Text = "";
            checkParition10.Checked = false;
        }

        private void btnApply_Click(object sender, EventArgs e) {
            // Save Settings to File
            
            this.Close();
        }

        private void checkParition1_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition1.Checked) {
                this.txtPartition1.Text = "";
                this.txtPartition1.Enabled = false;
            }
            else {
                this.txtPartition1.Enabled = true;
            }
        }
        private void checkParition2_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition2.Checked) {
                this.txtPartition2.Text = "";
                this.txtPartition2.Enabled = false;
            }
            else {
                this.txtPartition2.Enabled = true;
            }
        }
        private void checkParition3_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition3.Checked) {
                this.txtPartition3.Text = "";
                this.txtPartition3.Enabled = false;
            }
            else {
                this.txtPartition3.Enabled = true;
            }
        }
        private void checkParition4_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition4.Checked) {
                this.txtPartition4.Text = "";
                this.txtPartition4.Enabled = false;
            }
            else {
                this.txtPartition4.Enabled = true;
            }
        }
        private void checkParition5_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition5.Checked)
            {
                this.txtPartition5.Text = "";
                this.txtPartition5.Enabled = false;
            }
            else
            {
                this.txtPartition5.Enabled = true;
            }
        }
        private void checkParition6_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition6.Checked)
            {
                this.txtPartition6.Text = "";
                this.txtPartition6.Enabled = false;
            }
            else
            {
                this.txtPartition6.Enabled = true;
            }
        }
        private void checkParition7_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition7.Checked)
            {
                this.txtPartition7.Text = "";
                this.txtPartition7.Enabled = false;
            }
            else
            {
                this.txtPartition7.Enabled = true;
            }
        }
        private void checkParition8_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition8.Checked)
            {
                this.txtPartition8.Text = "";
                this.txtPartition8.Enabled = false;
            }
            else
            {
                this.txtPartition8.Enabled = true;
            }
        }
        private void checkParition9_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition9.Checked)
            {
                this.txtPartition9.Text = "";
                this.txtPartition9.Enabled = false;
            }
            else
            {
                this.txtPartition9.Enabled = true;
            }
        }
        private void checkParition10_CheckedChanged(object sender, EventArgs e) {
            if (!this.checkParition10.Checked)
            {
                this.txtPartition10.Text = "";
                this.txtPartition10.Enabled = false;
            }
            else
            {
                this.txtPartition10.Enabled = true;
            }
        }
    }
}
