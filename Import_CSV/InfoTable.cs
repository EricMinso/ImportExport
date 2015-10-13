
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;


namespace Import_Export_CSV
{
    //[Serializable]
    public class InfoTable
    {
        // Champs
        private string strNomTable;
        private int count;
        private bool erreur;
        private bool ligneCalculée;
        //private bool estTable;
        private bool estVue;

        // Accesseurs
        public string NomTable
        {
            get { return strNomTable; }
        }

        public int Count
        {
            get { return count; }
        }

        //public bool EstTable
        //{
        //    get { return estTable; }
        //}

        public bool EstVue
        {
            get { return estVue; }
        }

        public bool Erreur
        {
            get { return erreur; }
        }

        // Constructeur
        public InfoTable(string newNomTable, bool newEstVue )
        {
            this.strNomTable = newNomTable;
            this.estVue = newEstVue;
            this.ligneCalculée = false;
            this.count = -1;
            this.erreur = false;
        }

        // Constructeur
        public InfoTable(string newNomTable, bool newEstVue, int newCount, bool newErreur)
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
                if (erreur)
                    return this.strNomTable + " (##ERR##)";
                else
                    return this.strNomTable + " (" + this.count + ") ";
            }
            else return this.strNomTable;
        }
    }
}
