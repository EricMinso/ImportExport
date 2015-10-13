
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;


namespace Import_Export_Universel
{
    //[Serializable]
    public class InfoTable
    {
        // Champs
        private string strNomTable;
        private int count;
        private Exception erreur;
        private bool ligneCalculée;
        //private bool estTable;
        private bool estVue;

        // Accesseurs
        public string NomTable
        {
            get { return strNomTable; }
            set { strNomTable = value; }
        }

        public int Count
        {
            get { return count;  }
            set { count = value; ligneCalculée = true; }
        }

        //public bool EstTable
        //{
        //    get { return estTable; }
        //}

        public bool EstVue
        {
            get { return estVue; }
        }

        public Exception Erreur
        {
            get { return erreur;  }
            set { erreur = value; }
        }

        // Constructeur
        public InfoTable(string newNomTable, bool newEstVue )
        {
            this.strNomTable = newNomTable;
            this.estVue = newEstVue;
            this.ligneCalculée = false;
            this.count = -1;
            this.erreur = null;
        }

        // Constructeur
        public InfoTable(string newNomTable, bool newEstVue, int newCount, Exception newErreur)
        {
            this.strNomTable = newNomTable;
            this.estVue = newEstVue;
            this.ligneCalculée = true;
            this.count = newCount;
            this.erreur = newErreur;
        }

        public override string ToString()
        {
            if (ligneCalculée)
            {
                if (erreur!=null)
                    return this.strNomTable + " (##ERR## " +erreur+ ")";
                else
                    return this.strNomTable + " (" + this.count + ") ";
            }
            else return this.strNomTable;
        }
    }
}
