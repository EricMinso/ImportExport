using System;
using System.Windows.Forms;

namespace Import_Export_Universel
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Settings p = new Settings();
            //Properties.Settings.Default.toto = "bob";
            string tamere = p["toto"].ToString();

            Settings1.Default.*/
            //System.Xml.XPath (" SELECT lavaleurkejeveux FROM fichier config
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(FormPrincipale.Instance);
        }
        
        
        public static void MsgInformation( string strMessage )
        {
        	MsgInformation( strMessage, "Information" );
        }
        public static void MsgInformation( string strMessage, string strTitre )
        {
        	MessageBox.Show(
                strMessage,
                strTitre,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
             );
        }
        public static void MsgWarning( string strMessage )
        {
        	MsgWarning( strMessage, "Attention !" );
        }
        public static void MsgWarning( string strMessage, string strTitre )
        {
        	MessageBox.Show(
                strMessage,
                strTitre,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
             );
        }
        public static void MsgErreur( string strMessage )
        {
        	MsgErreur( strMessage, "Erreur !" );
        }
        public static void MsgErreur( Exception ex )
        {
        	string strMessage = ex.Message;
        	string strTitre = "Erreur : " + ex.ToString();
        	
        	if( ex.InnerException != null )
        	{
        		strMessage += "\n" + ex.InnerException.Message;
        		strTitre += " [" + ex.InnerException.ToString() + "]" ;
        	}
        			
        	MsgErreur( strMessage, strTitre );
        }
        public static void MsgErreur( string strMessage, string strTitre )
        {
        	MessageBox.Show(
                strMessage,
                strTitre,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
             );
        }
    }
}
