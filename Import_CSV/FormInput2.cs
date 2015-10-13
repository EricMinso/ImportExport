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
    public partial class FormInput2 : Form
    {
        public string TexteSélectionné
        {
            get
            {
                return this.lbSaisie.Text;
            }
        }
        public object ItemSélectionné
        {
            get
            {
                return this.lbSaisie.SelectedItem;
            }
        }
        public int IndexSélectionné
        {
            get
            {
                return this.lbSaisie.SelectedIndex;
            }
        }

        public FormInput2( string titreFenetre, string texteLabel, List<object> listeOptions )
        {
            InitializeComponent();

            this.Text = titreFenetre;
            this.labelSaisie.Text = texteLabel;

            foreach (object elt in listeOptions)
                this.lbSaisie.Items.Add(elt);

            this.lbSaisie.SelectedIndex = 0;
        }

        public FormInput2( string titreFenetre, string texteLabel, List<string> listeOptions )
        {
            InitializeComponent();

            this.Text = titreFenetre;
            this.labelSaisie.Text = texteLabel;

            foreach (string elt in listeOptions)
                this.lbSaisie.Items.Add(elt);

            this.lbSaisie.SelectedIndex = 0;
        }

        public FormInput2( string titreFenetre, string texteLabel, List<ItemDeListe> listeOptions )
        {
            InitializeComponent();

            this.Text = titreFenetre;
            this.labelSaisie.Text = texteLabel;

            foreach (ItemDeListe elt in listeOptions)
                this.lbSaisie.Items.Add(elt);

            this.lbSaisie.SelectedIndex = 0;
        }

        private void FormInput2_Load(object sender, EventArgs e)
        {
            //this.btOK.Enabled = false;
            this.BringToFront();
            this.lbSaisie.Focus();
        }

        private void lbSaisie_DoubleClick(object sender, EventArgs e)
        {
            this.AcceptButton.PerformClick();
        }

        

        /*private void tbNomConfig_Validated(object sender, EventArgs e)
        {
            MessageBox.Show("validated");
        }

        private void tbNomConfig_Validating(object sender, CancelEventArgs e)
        {

        }

        private void tbSaisie_KeyPress(object sender, KeyPressEventArgs e)
        {
            btOK.Enabled = (this.lbSaisie.SelectedIndex > 0);

            if (e.KeyChar == '\n' || e.KeyChar == '\r' || e.KeyChar == 10 || e.KeyChar == 13 )
            {
                btOK.Focus();
                btOK.PerformClick();
            }
        }

        private void tbSaisie_TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = (this.tbSaisie.TextLength > 0);
        }//*/
    }
}
