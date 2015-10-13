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
    public class AssociationChampsBDTampon// : ICollection, IEnumerable
    {
        #region Variables

        // Données publiques destinées à être exportées dans le XML
        public string Version = Application.ProductVersion;
        public DateTime DateCréation = DateTime.Now;

        //private List<string> lstClés;
        //private List<string> lstValeurs;
        //private Dictionary<string, string> dictionnaire;

        // Données privées
        private List<string> lstClésPrimaires;
        private List<string> lstChampsBD;
        private List<string> lstChampsTampon;
        //private NameValueCollection assoBdTampon; -> non xml sérialisable
        private bool insertsOK;
        private bool updatesOK;
        private string conditionsOptionnelles;

        #endregion
        #region Accesseurs / Indexeurs

        public string this[string clé]
        {
            get 
            {
                int index = getIndex( this.lstChampsBD, clé );
                return this.lstChampsTampon[index];
            }
            set
            {
                int index = getIndex( this.lstChampsBD, clé );
                this.lstChampsTampon[index] = value; 
            }
        }

        public string this[int index]
        {
            get { return lstChampsTampon[ index ];     } //return this.assoBdTampon[index];  }
            set { this.lstChampsTampon[index] = value; } //this.assoBdTampon[index] = value; }
        }

        public List<string> ClésPrimaires
        {
            get { return lstClésPrimaires;  }
            set { lstClésPrimaires = value; }
        }

        public List<string> ChampsBD
        {
            get { return lstChampsBD; }
            set { lstChampsBD = value; }
        }

        public List<string> ChampsTampon
        {
            get { return lstChampsTampon; }
            set { lstChampsTampon = value; }
        }

        public bool InsertsOK
        {
            get { return this.insertsOK;  }
            set { this.insertsOK = value; }
        }

        public bool UpdatesOK
        {
            get { return this.updatesOK;  }
            set { this.updatesOK = value; }
        }

        public string ConditionsAdditionnelles
        {
            get { return conditionsOptionnelles; }
            set { conditionsOptionnelles = value; }
        }

        /*NameValueCollection BdTampon
        {
            get { return null; }// assoBdTampon; }
            //set { assoBdTampon = value; }
        }
        /*public List<string> Valeurs
        {
            get { return lstValeurs; }
            //set { lstValeurs = value; }
        }

        public Dictionary<string, string> Dictionnaire
        {
            get { return dictionnaire; }
            //set { dictionnaire = value; }
        }//*/

        #endregion
        #region Constructeur
        
        public AssociationChampsBDTampon()
	    {
            this.lstClésPrimaires = new List<string>();
            this.lstChampsBD = new List<string>();
            this.lstChampsTampon = new List<string>();
            //this.assoBdTampon = new NameValueCollection();

            this.insertsOK = false;
            this.updatesOK = false;
            this.conditionsOptionnelles = "";
            //this.lstValeurs = new List<string>();
            //this.dictionnaire = new Dictionary<string, string>();
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

        public void Add(string clé, string valeur)
        {
            //this.lstClés.Add(clé);
            //this.lstValeurs.Add(valeur);
            //this.dictionnaire.Add(clé, valeur);
            this.lstChampsBD.Add(clé);
            this.lstChampsTampon.Add(valeur);     
        }

        public void Clear()
        {
            this.lstClésPrimaires.Clear();
            this.lstChampsBD.Clear();
            this.lstChampsTampon.Clear();        
        }

        public bool Enregistrer( string strNomFichier )
        {
            try
            {
                FileStream theFile = File.Create(strNomFichier);
                StreamWriter stream = new StreamWriter(theFile);
                XmlSerializer serializer = new XmlSerializer(typeof(AssociationChampsBDTampon));
                
                serializer.Serialize(stream, this);

                stream.Close();
                theFile.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return false;
        }

        public bool Charger(string strNomFichier)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());

                TextReader reader = new StreamReader(strNomFichier);
                AssociationChampsBDTampon asso = (AssociationChampsBDTampon) serializer.Deserialize(reader);
                reader.Close();

                this.lstClésPrimaires = asso.lstClésPrimaires;
                this.lstChampsBD = asso.lstChampsBD;
                this.lstChampsTampon = asso.lstChampsTampon;
                //this.assoBdTampon = asso.assoBdTampon;

                this.insertsOK = asso.insertsOK;
                this.updatesOK = asso.updatesOK;
                this.conditionsOptionnelles = asso.conditionsOptionnelles;

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw ex;
            }
            //return false;
        }

        #endregion

        
    }
}

