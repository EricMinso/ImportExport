using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;

namespace Import_Export_CSV
{
    [Serializable]
    public class AssociationTablesBDFichiersImport
    {
        public const string AucunFichierAssociation = "Pas de Fichier";

        #region Variables

        // Données publiques destinées à être exportées dans le XML
        public string Version = Application.ProductVersion;
        public DateTime DateCréation = DateTime.Now;

        // Données privées
        private List<string> lstTablesBD;
        private List<string> lstFichiersImport;
        private List<string> lstFichiersAssociation;

        #endregion
        #region Accesseurs / Indexeurs

        public string this[string clé]
        {
            get 
            {
                int index = getIndex( this.lstTablesBD, clé );
                return this.lstFichiersImport[index];
            }
            set
            {
                int index = getIndex( this.lstTablesBD, clé );
                this.lstFichiersImport[index] = value; 
            }
        }

        public string this[int index]
        {
            get { return lstFichiersImport[ index ];     } //return this.assoBdTampon[index];  }
            set { this.lstFichiersImport[index] = value; } //this.assoBdTampon[index] = value; }
        }

        public List<string> TablesBD
        {
            get { return lstTablesBD; }
            set { lstTablesBD = value; }
        }

        public List<string> FichiersImport
        {
            get { return lstFichiersImport; }
            set { lstFichiersImport = value; }
        }

        public List<string> FichiersAssociation
        {
            get { return lstFichiersAssociation;  }
            set { lstFichiersAssociation = value; }
        }

        #endregion
        #region Constructeur

        public AssociationTablesBDFichiersImport()
	    {
            this.lstTablesBD = new List<string>();
            this.lstFichiersImport = new List<string>();
            this.lstFichiersAssociation = new List<string>();
	    }

        #endregion
        #region Méthodes

        private int getIndex( List<string> liste, string chaine )
        {
            for( int i=0; i<liste.Count; i++ )
                if( liste[i].Equals( chaine ))
                    return i;
            return -1;
        }

        public void Add(string tableBD, string fichierImport)
        {
            this.lstTablesBD.Add(tableBD);
            this.lstFichiersImport.Add(fichierImport);
            this.lstFichiersAssociation.Add(AucunFichierAssociation);
        }

        public void Add(string tableBD, string fichierImport, string fichierAssociation)
        {
            this.lstTablesBD.Add(tableBD);
            this.lstFichiersImport.Add(fichierImport);
            this.lstFichiersAssociation.Add(fichierAssociation);
        }

        public void Clear()
        {
            this.lstTablesBD.Clear();
            this.lstFichiersImport.Clear();
            this.lstFichiersAssociation.Clear();
        }

        public bool Enregistrer( string strNomFichier )
        {
            try
            {
                FileStream theFile = File.Create(strNomFichier);
                StreamWriter stream = new StreamWriter(theFile);
                XmlSerializer serializer = new XmlSerializer(typeof(AssociationTablesBDFichiersImport));
                
                serializer.Serialize(stream, this);

                stream.Close();
                theFile.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Charger(string strNomFichier)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());

                TextReader reader = new StreamReader(strNomFichier);
                AssociationTablesBDFichiersImport asso = (AssociationTablesBDFichiersImport)serializer.Deserialize(reader);
                reader.Close();

                this.lstTablesBD = asso.lstTablesBD;
                this.lstFichiersImport = asso.lstFichiersImport;
                this.lstFichiersAssociation = asso.lstFichiersAssociation;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
