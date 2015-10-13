using System;
using System.Windows.Forms;

namespace Import_Export_CSV
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
    }
}
