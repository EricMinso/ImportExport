
using System.IO;
using System.Xml.Serialization;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Import_Export_Universel
{
    [Serializable]
    public class ConfigurationConnexion
    {
        private static bool existeFichierConfig = false;

        [XmlIgnore]
        public static bool ExisteFichierConfig
        {
            get
            {
                return existeFichierConfig;
            }
        }

        [XmlIgnore]
        public static readonly string NomFichierConfigXML = "Import_Export_CSV.Config.xml";
        public static List<ConfigurationConnexion> listeDesConfigs = new List<ConfigurationConnexion>();

        public static void ChargerListeConfig()
        {
            listeDesConfigs.Clear();

            if( ! OuvrirFichier() || ConfigurationConnexion.listeDesConfigs.Count == 0 )
                ChargerListeConfigParDefaut();            
        }

        public static void ChargerListeConfigParDefaut()
        {
            existeFichierConfig = false;

            //if( listeDesConfigs ==

            listeDesConfigs.Clear();
            listeDesConfigs.Add(
            new ConfigurationConnexion( 
                "Nouveau",
                "",
                "",
                "",
                "",
                ConnexionBD.Indefini,
                new ConnexionBD.Options( false, false ) 
            ));
            /*
            listeDesConfigs.Add(
            new Config( 
                "SQL Server PC-S-35 (Eric)",
                "pc-s-35\\spsql",
                "sa",
                "SecurityMaster08",
                "SecurePerfect",
                ConnexionBD.SQLServer,
                new ConnexionBD.Options ( false, false ) 
            ));

            listeDesConfigs.Add(
            new Config( 
                "SQL Server PC-V-14 (Gaël)",
                "pc-v-14",
                "root",
                "root",
                "base_portail_ET",
                ConnexionBD.SQLServer,
                new ConnexionBD.Options ( false, false ) 
            ));

            listeDesConfigs.Add(
            new Config( 
                "PP4v08 (Gaël)",
                "pp4v08",
                "informix",
                "informix",
                "proteus",
                ConnexionBD.Informix,
                new ConnexionBD.Options ( false, false )
            ));

            listeDesConfigs.Add(
            new Config( 
                "PP4v08/ODBC (Gaël)",
                "pp4v08",
                "informix",
                "informix",
                "proteus",
                ConnexionBD.Informix,
                new ConnexionBD.Options ( false, true )
            ));

            listeDesConfigs.Add(
            new Config( 
                "Portail EuroTunnel",
                "portalfr",
                "sa",
                "SecurityMaster08",
                "base_portail_ET",
                ConnexionBD.SQLServer,
                new ConnexionBD.Options ( false, false )
            ));

            listeDesConfigs.Add(
            new Config( 
                "cicfr (PP4 ET)",
                "cicfr",
                "root",
                "via2s",
                "proteus",
                ConnexionBD.Informix,
                new ConnexionBD.Options ( false, false )
            ));

            listeDesConfigs.Add(
            new Config( 
                "cicfr/ODBC (PP4 ET)",
                "cicfr",
                "root",
                "via2s",
                "proteus",
                ConnexionBD.Informix,
                new ConnexionBD.Options ( false, true )
            ));// */
        }

        public static bool Enregistrer()
        {
            try
            {
                FileStream theFile = File.Create(NomFichierConfigXML);
                StreamWriter stream = new StreamWriter(theFile);
                XmlSerializer serializer = new XmlSerializer(ConfigurationConnexion.listeDesConfigs.GetType());
                serializer.Serialize(stream, ConfigurationConnexion.listeDesConfigs);
                stream.Close();
                theFile.Close();

                existeFichierConfig = true;
                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
            return false;
        }

        private static bool OuvrirFichier()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(ConfigurationConnexion.listeDesConfigs.GetType());

                TextReader reader = new StreamReader(NomFichierConfigXML);
                ConfigurationConnexion.listeDesConfigs = (List<ConfigurationConnexion>)serializer.Deserialize(reader);
                reader.Close();

                existeFichierConfig = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message + "\n\nIgnorez ce message s'il s'agit du premier lancement du programme : cela signifie qu'aucun fichier de configuration n'existe encore. Créez-en un à partir de l'onglet 'Configuration'.", "Lecture de la configuration XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            existeFichierConfig = false;
            return false;
        }

        public static ConfigurationConnexion Get(string strNomConfig)
        {
            foreach (ConfigurationConnexion config in listeDesConfigs)
            {
                if (config.NomConfig.Equals(strNomConfig))
                    return config;
            }

            return null;
        }

        // Champs
        private string strNomConfig;
        private string strHost;
        private string strLogin;
        private string strPass;
        private string strBase;
        private ConnexionBD.Type typeBase;
        private ConnexionBD.Options structOptions;

        private List<string> listeString = new List<string>();

        private string version;
        private DateTime dateCréation;

        //public List<string> MegaList { get; set; } //= new List<string>();
        //public NameValueCollection MegaCol = new NameValueCollection();

        public string Version
        {
            get
            {
                return this.version;
            }
            set 
            {
                this.version = value;
            }
        }

        public DateTime DateCréation
        {
            get
            {
                return this.dateCréation;
            }
            set 
            {
                this.dateCréation = value;
            }
        }

        public string NomConfig
        {
            get { return strNomConfig; }
            set { strNomConfig = value; }
        }

        public string Host
        {
            get { return strHost; }
            set { strHost = value; }
        }

        public string Login
        {
            get { return strLogin; }
            set { strLogin = value; }
        }

        public string Pass
        {
            get { return strPass; }
            set { strPass = value; }
        }

        public string Base
        {
            get { return strBase; }
            set { strBase = value; }
        }

        public ConnexionBD.Type TypeBase
        {
            get { return typeBase; }
            set { typeBase = value; }
        }

        public ConnexionBD.Options Options
        {
            get { return structOptions; }
            set { structOptions = value; }
        }

        // Constructeur
        public ConfigurationConnexion()
        {
            this.strNomConfig = "";
            this.strHost = "";
            this.strLogin = "";
            this.strPass = "";
            this.strBase = "";
            this.typeBase = ConnexionBD.Indefini;
            //this.typeBase = new ConnexionBD.Type(ConnexionBD.Indefini.strValue, ConnexionBD.Indefini.enumerationValue, ConnexionBD.Indefini.intValue);
            this.structOptions = new ConnexionBD.Options( false, false ) ;

            this.version = Application.ProductVersion;
            this.dateCréation = DateTime.Now;
        }

        public ConfigurationConnexion(
            string strNomConfig,
            string strHost,
            string strLogin,
            string strPass,
            string strBase,
            ConnexionBD.Type typeBase,
            ConnexionBD.Options structOptions )
        {
            /*this.MegaList = new List<string>();
            this.MegaList.Add("un");
            this.MegaList.Add("deux");
            this.MegaList.Add("trois");//*/
            this.strNomConfig = strNomConfig;
            this.strHost = strHost;
            this.strLogin = strLogin;
            this.strPass = strPass;
            this.strBase = strBase;
            this.typeBase = typeBase;
            //this.typeBase = new ConnexionBD.Type(typeBase.strValue, typeBase.enumerationValue, typeBase.intValue);
            this.structOptions = structOptions;

            this.version = Application.ProductVersion;
            this.dateCréation = DateTime.Now;
        }

        public override string ToString()
        {
            return this.strNomConfig;
        }
    }
}
