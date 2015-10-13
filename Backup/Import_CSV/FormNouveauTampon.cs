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
    public partial class FormNouveauTampon : Form
    {
        public int NouvellesLignesVierges
        {
            get
            {
                return (int) this.numNbLignes.Value;
            }
        }

        public FormNouveauTampon()
        {
            InitializeComponent();
            this.tbListeDesColonnes.Focus();
        }

        public List<string> ListeDesColonnes()
        {
            List<string> liste = new List<string>();
            
            foreach (string elt in this.tbListeDesColonnes.Text.Split(new char[] { '\n' }))
            {
                string element = elt.Trim();

                if (element.Length > 0)
                    liste.Add(element);
            }

            return liste;
        }

        /*public bool ConcaténerTampon
        {
            get
            {
                return this.rbConcaténerTampon.Checked;
            }
        }*/

        
        void FormNouveauTamponLoad(object sender, EventArgs e)
        {
        	this.BringToFront();
        	tbListeDesColonnes.Focus();
        }
    }
}
