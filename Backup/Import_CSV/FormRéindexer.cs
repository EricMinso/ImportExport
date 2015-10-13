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
    public partial class FormRéindexer : Form
    {
        private DataTable dataTable;

        public FormRéindexer( DataTable _dataTable )
        {
            InitializeComponent();
            this.dataTable = _dataTable;
        }

        private int MaxSelectedColonne( string strNomColonne )
        {
            int maxValeur = Int32.MinValue ;

            foreach (DataRow row in dataTable.Rows)
            {
                int valeur = Int32.Parse( row[strNomColonne].ToString() );

                if (valeur > maxValeur)
                {
                    maxValeur = valeur;
                }
            }

            return maxValeur;
        }

        private void MàJnumDépart()
        {
            try
            {
                this.numDépart.Value = 
                    (decimal) MaxSelectedColonne( this.cbNomsColonnes.SelectedItem.ToString() ) + 
                    Decimal.One ;
            }
            catch (Exception)
            {
                /*MessageBox.Show(
                    "Problème d'indexation : " + ex.Message,
                    "Erreur de conversion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );//*/
                this.numDépart.Value = Decimal.One;
            }
        }

        private void FormRéindexer_Load(object sender, EventArgs e)
        {
        	this.BringToFront();
        	
            this.numDépart.DecimalPlaces = 0;
            this.numDépart.Increment = Decimal.One;
            this.numDépart.Minimum = Decimal.MinValue;
            this.numDépart.Maximum = Decimal.MaxValue;

            if (dataTable != null && dataTable.Columns.Count > 0)
            {
                foreach (DataColumn colonne in dataTable.Columns)
                {
                    this.cbNomsColonnes.Items.Add(colonne.ColumnName);
                }

                this.cbNomsColonnes.SelectedIndex = 0;

                MàJnumDépart();
            }
            //else MessageBox.Show("ah bah non");
        }

        private void cbNomsColonnes_SelectedIndexChanged(object sender, EventArgs e)
        {
            MàJnumDépart();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                string strNomColonne = this.cbNomsColonnes.SelectedItem.ToString();

                if (this.rbIncrémentalNumérique.Checked)
                {
                    int compteur = (int)this.numDépart.Value;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        row[strNomColonne] = compteur;
                        compteur++;
                    }
                }
                else if (this.rbUnique.Checked)
                {
                    foreach (DataRow row in dataTable.Rows)
                        row[strNomColonne] = this.tbValeurUniqueCommune.Text;
                }
                else if (this.rbNULL.Checked)
                {
                    foreach (DataRow row in dataTable.Rows)
                        row[strNomColonne] = "";
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(
                    "Problème d'indexation : "+ex.Message,
                    "Erreur de conversion",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error 
                );
            }
        }

        /*private void tbValeurUniqueCommune_Enter(object sender, EventArgs e)
        {
            //rbUnique.Select();
        }//*/

        private void numDépart_Enter(object sender, EventArgs e)
        {
        }

        private void numDépart_Click(object sender, EventArgs e)
        {
            rbIncrémentalNumérique.Checked = true;
        }

        private void tbValeurUniqueCommune_Click(object sender, EventArgs e)
        {
            rbUnique.Checked = true;
        }

        private void rbIncrémentalNumérique_Click(object sender, EventArgs e)
        {
            numDépart.Focus();
        }

        private void rbUnique_Click(object sender, EventArgs e)
        {
            tbValeurUniqueCommune.Focus();
        }
    }
}
