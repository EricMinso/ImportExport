using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Import_Export_Universel
{
    public partial class FormInput : Form
    {
        public string ValeurSaisie
        {
            get
            {
                return this.tbSaisie.Text;
            }
        }

        public FormInput( string titreFenetre, string texteLabel )
        {
            InitializeComponent();

            this.Text = titreFenetre;
            this.labelSaisie.Text = texteLabel;
        }

        private void FormInput_Load(object sender, EventArgs e)
        {
        	this.BringToFront();
            this.btOK.Enabled = false;
            this.tlpSaisie.Focus();
        }

        

        /*private void tbNomConfig_Validated(object sender, EventArgs e)
        {
            MessageBox.Show("validated");
        }

        private void tbNomConfig_Validating(object sender, CancelEventArgs e)
        {

        }//*/

        private void tbSaisie_KeyPress(object sender, KeyPressEventArgs e)
        {
            btOK.Enabled = (this.tbSaisie.TextLength > 0);

            if (e.KeyChar == '\n' || e.KeyChar == '\r' || e.KeyChar == 10 || e.KeyChar == 13 )
            {
                btOK.Focus();
                btOK.PerformClick();
            }
        }

        private void tbSaisie_TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = (this.tbSaisie.TextLength > 0);
        }
    }
}
