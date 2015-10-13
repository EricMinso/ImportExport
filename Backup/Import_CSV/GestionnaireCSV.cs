using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace Import_Export_Universel
{
    public class GestionnaireCSV
    {
        public static string CaractèreSéparateur = "|";
        public static string Répertoire = "";
        public static bool TrimSpaces = true;
        public static bool ControleNbColonnes = false;

        public static void ExporterTamponDansCSV(DataTable tampon, string strFichier )
        {
            string strEntetes = tampon.Columns[0].ToString();

            if (TrimSpaces)
                strEntetes = strEntetes.Trim();

            for (int j = 1; j < tampon.Columns.Count; j++)
            {
                string nomColonne = tampon.Columns[j].ToString();

                if (TrimSpaces)
                    nomColonne = nomColonne.Trim();

                strEntetes += CaractèreSéparateur + nomColonne;
            }

            // Ecriture des données de la table
            FileStream fsSortie = new FileStream(strFichier, FileMode.Create );
            StreamWriter swSortie = new StreamWriter(fsSortie, System.Text.Encoding.Default);

            swSortie.WriteLine(strEntetes);
            string strData = "";

            for (int i = 0; i < tampon.Rows.Count; i++)
            {
                strData = tampon.Rows[i][0].ToString();

                if (TrimSpaces)
                    strData = strData.Trim();

                for (int j = 1; j < tampon.Columns.Count; j++)
                {
                    string strDataColonne = tampon.Rows[i][j].ToString();

                    if (TrimSpaces)
                        strDataColonne = strDataColonne.Trim();

                    strData += CaractèreSéparateur + strDataColonne;
                }

                swSortie.WriteLine(strData);
            }

            // Fermeture
            swSortie.Close();
            fsSortie.Close();
        }

        public static int NombreDeLignes(string strNomFichier)
        {
            int compteur = 0;

            try
            {
                StreamReader stream = new StreamReader(
                        new FileStream(strNomFichier, FileMode.Open
                ));

                while (stream.ReadLine() != null)
                    compteur++;

                stream.Close();
            }
            catch (Exception)
            { }

            return compteur;
        }

        public static DataTable OuvrirFichierCSV(string strNomFichier)
        {
            return OuvrirFichierCSV(
                new StreamReader(
                    File.OpenRead(strNomFichier), System.Text.Encoding.Default
                ));
        }

        public static DataTable OuvrirFichierCSV(StreamReader srFichier)
        {
            // Initialisations
            DataTable dtFichier = new DataTable("FichierCSV");
            string strChaineLue = srFichier.ReadLine();
            string[] tableauDesColonnes = strChaineLue.Split(CaractèreSéparateur.ToCharArray());
            int nbMaxCols = tableauDesColonnes.Length;
            //int i = 0;

            //if (nbCols < 2)
            //    throw new Exception("Fichier non valide");

            for (int i = 0; i < nbMaxCols; i++)
            {
                string nomColonne = tableauDesColonnes[i].ToString();

                if (TrimSpaces)
                    nomColonne = nomColonne.Trim();
                
                try
                {
                	dtFichier.Columns.Add(new DataColumn(nomColonne));
                }
                catch ( DuplicateNameException )
                {
                	int compteur = 1;
                	bool ok = false;
                	
                	while ( !ok )
                	{
                		compteur ++;
                	
                    	try
                    	{
                        	dtFichier.Columns.Add(new DataColumn(nomColonne+compteur));  
                        	ok = true;
	                    }
	                    catch ( DuplicateNameException )
	                    {
	                    	ok = false;
	                    }
                	}
                }
            }

            // Chargement du fichier
            while ((strChaineLue = srFichier.ReadLine()) != null)
            {
                string[] tableauDeStrings = strChaineLue.Split(CaractèreSéparateur.ToCharArray());

                if (TrimSpaces)
                    for (int i = 0; i < tableauDeStrings.Length; i++)
                        tableauDeStrings[i] = tableauDeStrings[i].Trim();

                if (ControleNbColonnes && tableauDeStrings.Length != nbMaxCols)
                    throw new Exception(nbMaxCols + " en-têtes =/= " + tableauDeStrings.Length + " colonnes -> erreur");

                // Manque une colonne
                if (tableauDeStrings.Length > nbMaxCols)
                {
                    for (int i = nbMaxCols; i < tableauDeStrings.Length; i++)
                        dtFichier.Columns.Add(new DataColumn("NewCol" + (i + 1)));

                    nbMaxCols = tableauDeStrings.Length;
                }

                DataRow row = dtFichier.NewRow();
                row.ItemArray = (object[])tableauDeStrings;
                dtFichier.Rows.Add(row);
            }

            return dtFichier;
        }

        public static string GénérerNomFichierExport(string racine)
        { 
            return
                racine + "_" +
                GénérerLongTimestampFichier() +
                "_export.csv";
        }

        public static string GénérerCourtTimestampFichier()
        {
            return String.Format("{0:yyyy'-'MM'-'dd}", DateTime.Now );
        }

        public static string GénérerLongTimestampFichier()
        {
            return String.Format("{0:yyyy'-'MM'-'dd'_'HH'h'mm'm'ss's'}", DateTime.Now );
                            //DateTime.Now.Year.ToString() +
                            //DateTime.Now.Month.ToString() +
                            //DateTime.Now.Day.ToString() +
                            //DateTime.Now.Hour.ToString() +
                            //DateTime.Now.Minute.ToString() +
                            //DateTime.Now.Second.ToString() +
        }

        public static List<string> ListeDesFichiers( string extension )
        {
            List<string> liste = new List<string>();

            try
            {
                DirectoryInfo dossier = new DirectoryInfo(Répertoire);
                FileInfo[] fichiers = dossier.GetFiles("*." + extension);

                foreach (FileInfo fichier in fichiers)
                {
                    if( 0 ==( fichier.Attributes & FileAttributes.Directory ))
                        liste.Add(fichier.Name);
                }
            }
            catch (Exception)
            { }

            return liste;
        }
    }
}

