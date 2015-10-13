using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;


namespace Import_Export_Universel
{
    public class GestionnaireExcel
    {
        //private static BackgroundWorker backgroundWorker;
        private static Microsoft.Office.Interop.Excel.Application _appli_excel = null;

        public static Microsoft.Office.Interop.Excel.Application AppliExcel
        {
            get
            {
                if (_appli_excel == null)
                    _appli_excel = new Microsoft.Office.Interop.Excel.Application();

                _appli_excel.Visible = false;
                _appli_excel.DisplayAlerts = false;
                _appli_excel.ScreenUpdating = false;

                return _appli_excel;
            }
            // private set ;
        }

        public static void FermerExcel()
        {
            if (_appli_excel != null)
            {
                _appli_excel.Workbooks.Close();
                _appli_excel.DisplayAlerts = true;
                _appli_excel.ScreenUpdating = true;
                _appli_excel.Quit();
                _appli_excel = null;
            }
        }

        public static List<string> ListeDesFeuilles(string strNomFichier)
        {
            List<string> listeFeuilles = new List<string>();


            Microsoft.Office.Interop.Excel.Workbook fichierExcel =
                AppliExcel.Workbooks.Open(
                    strNomFichier,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    );


            if (fichierExcel != null)
            {
                foreach (Microsoft.Office.Interop.Excel.Worksheet feuille in fichierExcel.Sheets)
                {
                    listeFeuilles.Add(feuille.Name);
                }
            }

            fichierExcel.Close(Type.Missing, Type.Missing, Type.Missing);
            FermerExcel();
            return listeFeuilles;
        }

        public static List<ItemDeListe> ListeDesFeuillesAvecDétails(string strNomFichier)
        {
            List<ItemDeListe> listeFeuilles = new List<ItemDeListe>();

            Microsoft.Office.Interop.Excel.Workbook fichierExcel =
                AppliExcel.Workbooks.Open(
                    strNomFichier,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    );

            if (fichierExcel != null)
            {
                foreach (Microsoft.Office.Interop.Excel.Worksheet feuille in fichierExcel.Sheets)
                {
                    string label =
                        feuille.Name +
                        " (" + feuille.UsedRange.Rows.CountLarge +
                        " lignes x " + feuille.UsedRange.Columns.CountLarge +
                        " colonnes) ";
                    
                    ItemDeListe item = new ItemDeListe( feuille.Name, label );
                    
                    listeFeuilles.Add(item);
                }
            }

            fichierExcel.Close(Type.Missing, Type.Missing, Type.Missing);
            FermerExcel();
            return listeFeuilles;
        }

        public static int NombreDeLignes(string strNomFichier, string strNomFeuille)
        {
            int returnValue = -1;

            Microsoft.Office.Interop.Excel.Workbook fichierExcel =
                AppliExcel.Workbooks.Open(
                    strNomFichier,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    );

            Microsoft.Office.Interop.Excel.Worksheet feuille
                = (fichierExcel.Sheets[strNomFeuille] as Microsoft.Office.Interop.Excel.Worksheet);

            returnValue = feuille.UsedRange.Rows.Count;

            fichierExcel.Close(Type.Missing, Type.Missing, Type.Missing);
            FermerExcel();
            return returnValue;
        }
        
      	public static string GénérerNomFichierExport(string racine)
        {
            return
                racine + "_" +
                GénérerTimestampFichier() +
                "_export.xls";
        }

        public static string GénérerTimestampFichier()
        {
            return String.Format("{0:yyyy'-'MM'-'dd'_'HH'h'mm'm'ss's'}", DateTime.Now);
        }

        public static List<string> ListeDesFichiers(string extension)
        {
            List<string> liste = new List<string>();

            try
            {
                DirectoryInfo dossier = new DirectoryInfo(Répertoire);
                FileInfo[] fichiers = dossier.GetFiles("*." + extension);

                foreach (FileInfo fichier in fichiers)
                {
                    if (0 == (fichier.Attributes & FileAttributes.Directory))
                        liste.Add(fichier.Name);
                }
            }
            catch (Exception)
            { }

            return liste;
        }
        
        public static string CaractèreSéparateur = "|";
        public static string Répertoire = "";
        public static bool TrimSpaces = true;
        public static bool ControleNbColonnes = false;
//        public static System.Data.DataTable dataTable
//        {
//        	get; private set;
//        }

        // 1er élément de l'index : indice 1
        private static int indexFeuilleXLS;
        private static string strNomFichierXLS;
        private static string strNomFeuilleXLS;

        public static void ExporterTamponDansExcel(System.Data.DataTable _tampon, string _strNomFichierXLS, RunWorkerCompletedEventHandler événementTâcheTerminée)
        {
        	strNomFichierXLS = _strNomFichierXLS;
            strNomFeuilleXLS = _tampon.TableName;
            
            GestionnaireDesTâches.Démarrer(
            	new DoWorkEventHandler(BGWorkExporterTamponDansExcel), 
            	null, 
            	événementTâcheTerminée, 
            	_tampon
            );
        }
        
        private static void BGWorkExporterTamponDansExcel(object sender, DoWorkEventArgs doWorkEventArgs)
        {
        	try
            {
        		BackgroundWorker backgroundWorker = sender as BackgroundWorker;
        		System.Data.DataTable dataTable = ( doWorkEventArgs.Argument as System.Data.DataTable );
        		
        		GestionnaireDesTâches.Titre( "Démarrage de l'export de la feuille "+strNomFeuilleXLS+" dans le fichier "+strNomFichierXLS );
	
	            Microsoft.Office.Interop.Excel.Workbook fichierExcel =
	                AppliExcel.Workbooks.Add(Type.Missing);
	
	            Microsoft.Office.Interop.Excel.Worksheet feuille =
	                (Microsoft.Office.Interop.Excel.Worksheet)
	                fichierExcel.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
	            
	            feuille.Name = strNomFeuilleXLS;
	            //AppliExcel.UserControl = true;
	            int oldPourcentage = 1;
                int newPourcentage = 1;
                backgroundWorker.ReportProgress(newPourcentage);
                System.Threading.Thread.Sleep(5);
	
	            for (int col = 0; col < dataTable.Columns.Count; col++)
	            {
	                DataColumn colonne = dataTable.Columns[col];
	                Microsoft.Office.Interop.Excel.Range range =
	                    (feuille.Cells[1, col + 1] as Microsoft.Office.Interop.Excel.Range);
	
	                if (TrimSpaces)
	                    range.Value2 = colonne.ColumnName.ToString().Trim();
	                else
	                    range.Value2 = colonne.ColumnName.ToString();
	            }
	            
	            for (int lgn = 0; lgn < dataTable.Rows.Count; lgn++)
	            {
	            	// ** Report de progression 
	                newPourcentage = (int)((100 * lgn + 10) / (dataTable.Rows.Count + 11));

                    if (newPourcentage > oldPourcentage)
                    {
                        backgroundWorker.ReportProgress(newPourcentage);
                        System.Threading.Thread.Sleep(5);
                        oldPourcentage = newPourcentage;
                    }
                    if (GestionnaireDesTâches.ArrêtDemandé ) //|| backgroundWorker.CancellationPending)
                    {
                        //backgroundWorker.CancelAsync();
                        doWorkEventArgs.Cancel = true;
                        break;
                    }
	            	//** Fin Report de progression 
	            	
	                DataRow ligne = dataTable.Rows[lgn];
	
	                for (int col = 0; col < dataTable.Columns.Count; col++)
	                {
	                    Microsoft.Office.Interop.Excel.Range range =
	                        (feuille.Cells[lgn + 2, col + 1] as Microsoft.Office.Interop.Excel.Range);
	
	                    if (TrimSpaces)
	                        range.Value2 = dataTable.Rows[lgn][col].ToString().Trim();
	                    else
	                        range.Value2 = dataTable.Rows[lgn][col].ToString();
	                }
	            }
	
	            // fichierExcel.Close(true, strFichier, Type.Missing);
	            // fichierExcel.FullName = strFichier;
	            fichierExcel.SaveAs(
	                strNomFichierXLS,
	                Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7,
	                Type.Missing, Type.Missing, false, false,
	                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
	                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
	                );//*/
	            //fichierExcel.SaveAs(
	            //    "d:\\output",
	            //    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
	            //    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
	            //    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
	            //    );
	            
		        // ** Report de progression 
	            backgroundWorker.ReportProgress(99);
                System.Threading.Thread.Sleep(5);
                
                if (GestionnaireDesTâches.ArrêtDemandé )//|| backgroundWorker.CancellationPending)
                {
                    //backgroundWorker.CancelAsync();
                    doWorkEventArgs.Cancel = true;
                    return;
                }
	            //** Fin Report de progression 
	            
	            fichierExcel.Close(Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                GestionnaireDesTâches.Exception(ex);
            }

            FermerExcel();
        }

        public static System.Data.DataTable OuvrirFeuilleXLS(string strNomFichierXLS, object objIdFeuilleXLS )
        {
    		System.Data.DataTable dataTable = new System.Data.DataTable(strNomFeuilleXLS);
            Microsoft.Office.Interop.Excel.Range range = null;
            
            Microsoft.Office.Interop.Excel.Workbook fichierExcel =
                AppliExcel.Workbooks.Open(
                    strNomFichierXLS,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    );
            
            Microsoft.Office.Interop.Excel.Worksheet feuille = null;
            feuille = (fichierExcel.Sheets[objIdFeuilleXLS] as Microsoft.Office.Interop.Excel.Worksheet);

            for (int iCol = 0; iCol < feuille.UsedRange.Columns.Count; iCol++)
            {
                range = (feuille.Cells[1, iCol + 1] as Microsoft.Office.Interop.Excel.Range);

                string nomColonne = (range == null || range.Value2 == null) ?
                    "Colonne" + (iCol + 1) :
                    range.Value2.ToString();

                if (TrimSpaces)
                    nomColonne = nomColonne.Trim();

                try
                {
                	dataTable.Columns.Add(new DataColumn(nomColonne));                    
                }
                catch ( DuplicateNameException)
                {
                	int compteur = 1;
                	bool ok = false;
                	
                	while ( !ok )
                	{
                		compteur ++;
                	
                    	try
                    	{
                        	dataTable.Columns.Add(new DataColumn(nomColonne+compteur));  
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
            for (int iRow = 1; iRow < feuille.UsedRange.Rows.Count; iRow++)
            {
                DataRow row = dataTable.NewRow();

                for (int iCol = 0; iCol < feuille.UsedRange.Columns.Count; iCol++)
                {
                    //try
                    //{
                    range = (feuille.Cells[iRow + 1, iCol + 1] as Microsoft.Office.Interop.Excel.Range);

                    if (range != null && range.Value2 != null)
                    {
                        string valeur = range.Value2.ToString();

                        if (TrimSpaces)
                            valeur = valeur.Trim();

                        row[iCol] = valeur;
                    }
                }

                dataTable.Rows.Add(row);
            }

            FermerExcel();
            return dataTable;
        }
        
        public static void OuvrirFeuilleXLS(string _strNomFichierXLS, string _strNomFeuilleXLS, RunWorkerCompletedEventHandler événementTâcheTerminée)
        {
        	indexFeuilleXLS = -1;
            strNomFichierXLS = _strNomFichierXLS;
            strNomFeuilleXLS = _strNomFeuilleXLS;
            
            GestionnaireDesTâches.Démarrer(
            	new DoWorkEventHandler(BGWorkOuvrirFeuilleXLS), 
            	null, 
            	événementTâcheTerminée, 
            	null
            );
        }

        public static void OuvrirFeuilleXLS(string _strNomFichierXLS, int _indexFeuilleXLS, RunWorkerCompletedEventHandler événementTâcheTerminée)
        {
        	// 1er élément de l'index : indice 1
        	indexFeuilleXLS  = _indexFeuilleXLS;
            strNomFichierXLS = _strNomFichierXLS;
            strNomFeuilleXLS = null;
            
            GestionnaireDesTâches.Démarrer(
            	new DoWorkEventHandler(BGWorkOuvrirFeuilleXLS), 
            	null, 
            	événementTâcheTerminée, 
            	null
            );
        }

        private static void BGWorkOuvrirFeuilleXLS(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            // Initialisations
    		BackgroundWorker backgroundWorker = sender as BackgroundWorker;
    		System.Data.DataTable dataTable = new System.Data.DataTable(strNomFeuilleXLS); //( doWorkEventArgs.Argument as System.Data.DataTable );
            Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            Microsoft.Office.Interop.Excel.Range range = null;
            
            GestionnaireDesTâches.Titre("Ouverture du fichier Excel");

            try
            {
                Microsoft.Office.Interop.Excel.Workbook fichierExcel =
                    AppliExcel.Workbooks.Open(
                        strNomFichierXLS,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                        );
                Microsoft.Office.Interop.Excel.Worksheet feuille = null;

                if( indexFeuilleXLS > 0 )
                {
	                feuille = (fichierExcel.Sheets[indexFeuilleXLS] as Microsoft.Office.Interop.Excel.Worksheet);
	                strNomFeuilleXLS = feuille.Name;
                }
                else
                {
	                feuille = (fichierExcel.Sheets[strNomFeuilleXLS] as Microsoft.Office.Interop.Excel.Worksheet);
                	indexFeuilleXLS = feuille.Index;
                }

                for (int iCol = 0; iCol < feuille.UsedRange.Columns.Count; iCol++)
                {
                    //try
                    //{
                    range = (feuille.Cells[1, iCol + 1] as Microsoft.Office.Interop.Excel.Range);

                    string nomColonne = (range == null || range.Value2 == null) ?
                        "Colonne" + (iCol + 1) :
                        range.Value2.ToString();

                    if (TrimSpaces)
                        nomColonne = nomColonne.Trim();

                    try
                    {
                    	dataTable.Columns.Add(new DataColumn(nomColonne));                    
                    }
                    catch ( DuplicateNameException)
                    {
                    	int compteur = 1;
                    	bool ok = false;
                    	
                    	while ( !ok )
                    	{
                    		compteur ++;
                    	
	                    	try
	                    	{
	                        	dataTable.Columns.Add(new DataColumn(nomColonne+compteur));  
	                        	ok = true;
		                    }
		                    catch ( DuplicateNameException )
		                    {
		                    	ok = false;
		                    }
                    	}
                    }
                }

                int oldPourcentage = 0;
                int newPourcentage = 0;

                // Chargement du fichier
                for (int iRow = 1; iRow < feuille.UsedRange.Rows.Count; iRow++)
                {
                    newPourcentage = (int)((100 * iRow) / (feuille.UsedRange.Rows.Count + 1));

                    if (newPourcentage > oldPourcentage)
                    {
                        backgroundWorker.ReportProgress(newPourcentage);
                        System.Threading.Thread.Sleep(5);
                        oldPourcentage = newPourcentage;
                    }
                    if (GestionnaireDesTâches.ArrêtDemandé )//|| backgroundWorker.CancellationPending)
                    {
                        //backgroundWorker.CancelAsync();
                        doWorkEventArgs.Cancel = true;
                        break;
                    }

                    DataRow row = dataTable.NewRow();

                    for (int iCol = 0; iCol < feuille.UsedRange.Columns.Count; iCol++)
                    {
                        //try
                        //{
                        range = (feuille.Cells[iRow + 1, iCol + 1] as Microsoft.Office.Interop.Excel.Range);

                        if (range != null && range.Value2 != null)
                        {
                            string valeur = range.Value2.ToString();

                            if (TrimSpaces)
                                valeur = valeur.Trim();

                            row[iCol] = valeur;
                        }
                        //}
                        //catch (Exception e)
                        //{
                        //    System.Windows.Forms.MessageBox.Show(
                        //        "irow = " + iRow + "/" + feuille.UsedRange.Rows.Count + "\n" +
                        //        "icol = "+iCol+"/"+feuille.UsedRange.Columns.Count, e.Message
                        //    );
                        //    System.Windows.Forms.MessageBox.Show(
                        //        "Range = " + range, e.Message
                        //    );
                        //}
                    }

                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                GestionnaireDesTâches.Exception(ex);
            }

           	doWorkEventArgs.Result = dataTable;
            FermerExcel();
        }
    }
}

        //string strReturnValue="<<";

        //try
        //{
        //    strReturnValue +=
        //        "\nA) feuille.Columns.Count = " +
        //        feuille.Columns.Count  +
        //        "\n   feuille.Columns.CountLarge = " +
        //        feuille.Columns.CountLarge;

        //    strReturnValue +=
        //        "\nB) feuille.Rows.Count = " +
        //        feuille.Rows.Count +
        //        "\n   feuille.Rows.CountLarge = " +
        //        feuille.Rows.CountLarge;
        //}
        //catch (Exception)
        //{ }

        //try
        //{
        //    strReturnValue +=
        //        "\nC) feuille.Cells.Columns.Count = " +
        //        feuille.Cells.Columns.Count;

        //    strReturnValue +=
        //        "\n   feuille.Cells.Rows.Count = " +
        //        feuille.Cells.Rows.Count;

        //    strReturnValue +=
        //        "\n   feuille.Cells.Count = " +
        //        feuille.Cells.Count;
        //    }
        //    catch (Exception)
        //    { }

        //Microsoft.Office.Interop.Excel.Range range;

        //try
        //{
        //    range = (feuille.Cells[ "A1", "B3" ] as Microsoft.Office.Interop.Excel.Range);

        //    strReturnValue +=
        //        "\nD) feuille.Cells[ 'A1', 'B3' ].Count = " +
        //        range.Columns.Count + "x" + range.Rows.Count;

        //    strReturnValue += " = " +
        //        range.Cells.Count;
        //}
        //catch (Exception)
        //{ }



        //try
        //{
        //    range = (feuille.get_Range("A1", "B3") as Microsoft.Office.Interop.Excel.Range);

        //    strReturnValue +=
        //    "\nE) feuille.get_Range() = " + range.Columns.Count + "x" + range.Rows.Count;

        //    strReturnValue += " = " + range.Cells.Count;
        //    strReturnValue += " = " + range.Count;

        //}
        //catch (Exception)
        //{ }


        //try
        //{
        //    range = feuille.Cells.get_End( XlDirection.xlDown );

        //    strReturnValue +=
        //        "\nF) feuille.Cells.get_End() = " + range.Columns.Count + "x" + range.Rows.Count;

        //    strReturnValue += " = " + range.Cells.Count;
        //    strReturnValue += " = " + range.Count;

        //}
        //catch (Exception)
        //{ }


        //try
        //{
        //    range = feuille.UsedRange;

        //    strReturnValue +=
        //        "\nG) feuille.UsedRange = " + range.Columns.Count + "x" + range.Rows.Count;

        //    strReturnValue += " = " + range.Cells.Count;
        //    strReturnValue += " = " + range.Count;

        //}
        //catch (Exception)
        //{ }




        //int compteur = 0;

        //try
        //{
        //    StreamReader stream = new StreamReader(
        //            new FileStream(strNomFichier, FileMode.Open
        //    ));

        //    while (stream.ReadLine() != null)
        //        compteur++;

        //    stream.Close();
        //}
        //catch (Exception)
        //{ }

        //return compteur;

