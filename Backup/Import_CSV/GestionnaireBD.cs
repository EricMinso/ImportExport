using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Import_Export_Universel
{
    public class GestionnaireBD
    {
        #region Static
        private static GestionnaireBD gestionnaire = null;

        public static GestionnaireBD Instance
        {
            get
            {
                if (null == gestionnaire)
                    gestionnaire = new GestionnaireBD();

                return gestionnaire;
            }
        }

        public static ConnexionBD Connexion
        {
            get
            {
                return Instance.connexionBD;
            }
            set
            {
                Instance.connexionBD = value;
            }
        }

        #endregion
        #region Variables d'Instance

        private ConnexionBD connexionBD;
        //private BackgroundWorker backgroundWorker;

        // Pour l'insertion
        private DataTable dtTampon = null;
        private DataTable dtInsertionsEchecs = null;
        private DataTable dtInsertionsSuccès = null;
        private DataGridView dataGridViewCourant;
        private AssociationChampsBDTampon assoChampsBDTampon;
        //private NameValueCollection associationChampsBdTampon=null;
        //private List<string> lstChampsClésPrimaires=null;
        //private bool insertsOK;
        //private bool updatesOK;
        //private string strConditionsAdditionnelles;

        // Pour l'import
        private AssociationTablesBDFichiersImport assoTablesBDFichiersImport;

        #endregion
        #region Méthodes d'Instance

        public DataTable InsertionsEchecs
        {
            get
            {
                if( null != this.dtInsertionsEchecs )
                    if (this.dtInsertionsEchecs.Rows.Count == 0)
                        return null;

                return dtInsertionsEchecs;
            }
        }

        public DataTable InsertionsSuccès
        {
            get
            {
                if( null != this.dtInsertionsSuccès )
                    if (this.dtInsertionsSuccès.Rows.Count == 0) 
                        return null;

                return dtInsertionsSuccès;
            }        
        }

        public void OpenConnexion(string strNomConnexion, ConnexionBD.Type typeConnexion, ConnexionBD.Options optionsConnexion, string strConnexionString)
        {
            if (typeConnexion.intValue == ConnexionBD.Informix.intValue)
            {
                this.connexionBD = new ConnexionInformix(strNomConnexion, optionsConnexion, strConnexionString);
            }
            else if (typeConnexion.intValue == ConnexionBD.SQLServer.intValue)
            {
                this.connexionBD = new ConnexionSQLServer(strNomConnexion, optionsConnexion, strConnexionString);
            }
            else throw new Exception("Type de base non implémenté");
        }

        public void CloseConnexion()
        {
            if (this.connexionBD != null)
            {
                this.connexionBD.Close();
                this.connexionBD = null;
            }
        }

        public void ViderTampons()
        {
            this.dtTampon = null;
            this.dtInsertionsEchecs = null;
            this.dtInsertionsSuccès = null;
            this.assoChampsBDTampon = null;
            //this.lstChampsClésPrimaires = null;
        }

        private DbType getDBType(Type type)
        {
            if (type == typeof(string)) return DbType.String;

            if (type == typeof(Int16)) return DbType.Int16;
            if (type == typeof(int)) return DbType.Int32;
            if (type == typeof(Int64)) return DbType.Int64;
            if (type == typeof(Decimal)) return DbType.Decimal;

            if (type == typeof(float)) return DbType.Double;
            if (type == typeof(double)) return DbType.Double;

            if (type == typeof(DateTime)) return DbType.DateTime;

            if (type == typeof(bool)) return DbType.Boolean;

            if (type == typeof(Byte[])) return DbType.Binary;

            return DbType.String;
        }

        public DataTable GetSchemaTable(string strTable)
        {
            IDataReader reader = this.connexionBD.ExecuteReader("SELECT * FROM [" + strTable + "]");
            DataTable dataTable = reader.GetSchemaTable();
            reader.Close();
            return dataTable;
        }

        public DataTable OuvrirTableStandard(string strTable)
        {
            return this.connexionBD.ChargerDataTable("SELECT * FROM [" + strTable + "]");
        }

        public DataTable OuvrirTableEtConvertirChampsBinaires(string strTable)
        {
            ViderTampons();

            // Initialisations
            this.dtTampon = new DataTable(strTable);

            //List<string> listeDesColonnes = this.connexionBD.ListeDesColonnes(strTable);

            // Liste des colonnes de la table
            //foreach (string nomColonne in listeDesColonnes)
            //    this.dtTampon.Columns.Add(nomColonne);
            //for (int i = 0; i < dgvColonnes.RowCount; i++)
            //    dtImport.Columns.Add(new DataColumn((dgvColonnes.DataSource as DataTable).Rows[i][0].ToString()));

            // Liste des données de la table
            IDataReader reader = this.connexionBD.ExecuteReader("SELECT * FROM [" + strTable+"]");
            DataTable dtSchema = reader.GetSchemaTable();
            //bool firstRow = true;
            int ligne = 0;
            //int colIdx = 0;

            foreach ( DataRow row in dtSchema.Rows )
            {
                string colName = (string) row[ "ColumnName" ];
                Type typeColonne = (Type) row[ "DataType" ];

                if( typeColonne == DBNull.Value.GetType()
                 || typeColonne == typeof(System.Byte[]) )
                    typeColonne = typeof(string);

                DataColumn colonne = new DataColumn(colName, typeColonne );
                colonne.AllowDBNull = true;
                this.dtTampon.Columns.Add(colonne);
            }

            // Chargement des données de la table
            while (reader.Read())
            {
                ligne++;
                DataRow row = this.dtTampon.NewRow();

                for( int colIdx=0; colIdx < this.dtTampon.Columns.Count; colIdx++ )
                {
                    if (reader[colIdx].GetType().ToString().Equals("System.Byte[]"))
                    {
                        Byte[] objetBinaire = (Byte[])reader[colIdx];
                        string hexaString = BitConverter.ToString(objetBinaire);
                        string asciiString = "";

                        try
                        {
                            asciiString = System.Text.Encoding.ASCII.GetString(objetBinaire);
                            asciiString = Regex.Replace(asciiString, "[^a-zA-Z0-9]+", String.Empty);
                        }
                        catch (Exception) { }

                        //try{ unicodeString = System.Text.Encoding.Unicode.GetString(objetBinaire);
                        //} catch( Exception ) {}

                        row[colIdx] = "<BIN> Ascii:{" + asciiString + "} Hexa:{" + hexaString + "}";
                    }
                    else
                    {
                        //if (typeof(string) == this.dtTampon.Columns[f].DataType)
                        //    row[f] = reader[f].ToString();
                        //else
                            row[colIdx] = reader[colIdx];
                    }
                }

                this.dtTampon.Rows.Add(row);
            }

            // Fermeture
            reader.Close();
            return this.dtTampon;
        }

        public void ImporterTableBD( string _strNomTable, DataTable _tampon, DataGridView _dataGridViewAColorer, AssociationChampsBDTampon _association, RunWorkerCompletedEventHandler événementTâcheTerminée)
        {
            ViderTampons();
            this.dtInsertionsSuccès = new DataTable();
            this.dtInsertionsEchecs = new DataTable();

            foreach (DataColumn col in _tampon.Columns)
            {
                DataColumn newCol1 = new DataColumn(col.ColumnName);
                DataColumn newCol2 = new DataColumn(col.ColumnName);

                this.dtInsertionsSuccès.Columns.Add(newCol1);
                this.dtInsertionsEchecs.Columns.Add(newCol2);
            }

            this.dtTampon = _tampon;
            this.dtTampon.TableName = _strNomTable;
            this.assoChampsBDTampon = _association;
            //this.lstChampsClésPrimaires = _lstChampsClésPrimaires;
            //this.insertsOK = _insertsOK;
            //this.updatesOK = _updatesOK;
            //this.strConditionsAdditionnelles = _strConditionsAdditionnelles;
            this.dataGridViewCourant = _dataGridViewAColorer;

            GestionnaireDesTâches.Démarrer(
            	new DoWorkEventHandler(BGWorkImporterTableBD), 
            	null, 
            	événementTâcheTerminée, 
            	null
            );
        }

        public void ExporterTouteLaBD( RunWorkerCompletedEventHandler événementTâcheTerminée )
        {
            ViderTampons();
            //DémarrerBackgroundWork(new DoWorkEventHandler(BGWorkExporterTouteLaBD),événementTâcheTerminée);
            
            GestionnaireDesTâches.Démarrer(
            	new DoWorkEventHandler(BGWorkExporterTouteLaBD), 
            	null, 
            	événementTâcheTerminée, 
            	null
            );
        }

        public void ImporterTouteLaBD(AssociationTablesBDFichiersImport association, RunWorkerCompletedEventHandler événementTâcheTerminée)
        {
            ViderTampons();
            this.assoTablesBDFichiersImport = association;
            //DémarrerBackgroundWork(new DoWorkEventHandler(BGWorkImporterTouteLaBD), événementTâcheTerminée);
            
            GestionnaireDesTâches.Démarrer(
            	new DoWorkEventHandler(BGWorkImporterTouteLaBD), 
            	null, 
            	événementTâcheTerminée, 
            	null
            );
        }

        private void BGWorkImporterTableBD(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            System.Threading.Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            //BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            //backgroundWorker

            //int compteurASucces = 0;
            int indexLigne = 0;

            string strNomTableBD = this.dtTampon.TableName; //((InfoTable)lbListeTables.SelectedItem).NomTable;
            GestionnaireDesTâches.Titre("Enregistrement dans la table " + this.dtTampon.TableName);

            try
            {
                DataTable dtSchema = GetSchemaTable(strNomTableBD);
                Dictionary<string,Type> dict = new Dictionary<string,Type>();

                // Dictionnaire des types de colonne BD 
                foreach (DataRow row in dtSchema.Rows)
                {
                    string colName = (string)row["ColumnName"];
                    Type typeColonne = (Type)row["DataType"];

                    dict.Add(colName, typeColonne);
                }

                // Traitement ligne par ligne
                foreach (DataRow ligne in this.dtTampon.Rows)
                {
                    List<DbParameter> listeClésPrimaires = new List<DbParameter>();
                    List<DbParameter> listeParamètres = new List<DbParameter>();

                    try
                    {
                        // Dictionnaire des clés primaires
                        for( int indexPK = 0; indexPK < assoChampsBDTampon.ClésPrimaires.Count; indexPK++ )
                        {
                            string nomPK = assoChampsBDTampon.ClésPrimaires[indexPK];
                            object valeurPK = ligne[assoChampsBDTampon[nomPK]];
                            Type typePK;

                            try
                            {
                                dict.TryGetValue(nomPK, out typePK);
                                DbParameter paramPK = this.connexionBD.CréerDBParameter();

                                paramPK.ParameterName = "@" + nomPK;
                                paramPK.SourceColumn = nomPK;
                                paramPK.Value = Convert.ChangeType(valeurPK, typePK);
                                paramPK.DbType = getDBType(typePK);

                                listeClésPrimaires.Add(paramPK);
                            }
                            catch (Exception ex)
                            {
                                throw new ImportExportException(
                                    "Clé Primaire",
                                    "Echec pour <" + nomPK + "/" + assoChampsBDTampon[nomPK] + "='" + valeurPK + "'>",
                                    ex
                                    );
                            }
                        }

                        bool donnéesExistent =
                            ( listeClésPrimaires.Count > 0 )
                             && this.connexionBD.CléExiste(strNomTableBD, listeClésPrimaires);

                        if (listeClésPrimaires.Count > 0)
                            this.connexionBD.DeverrouillerTable(strNomTableBD);

                        for( int colIdx = 0; colIdx < assoChampsBDTampon.ChampsBD.Count; colIdx++ )
                        {
                            string nomChampBD = assoChampsBDTampon.ChampsBD[colIdx];
                            string nomChampTampon = assoChampsBDTampon.ChampsTampon[colIdx];
                            object valeurFromTampon = ligne[nomChampTampon];
                            Type typeChamp;

                            try
                            {
                                dict.TryGetValue(nomChampBD, out typeChamp);

                                DbParameter param = this.connexionBD.CréerDBParameter();

                                //param.ParameterName = "@" + associationChampsBdTampon[champ];
                                //param.SourceColumn = associationChampsBdTampon[champ];
                                param.ParameterName = "@" + nomChampBD;
                                param.SourceColumn = nomChampBD;
                                //param.Value = Convert.ChangeType(valeurFromTampon, typeChamp);
                                param.DbType = getDBType(typeChamp);

                                // Valeur nulle
                                if (0 == valeurFromTampon.ToString().Length)
                                    param.Value = DBNull.Value;
                                else
                                    param.Value = Convert.ChangeType(valeurFromTampon, typeChamp);

                                listeParamètres.Add(param);
                            }
                            catch (Exception ex)
                            {
                                System.Threading.Thread.Sleep(10);
                                throw new ImportExportException(
                                    "Paramètre de requête",
                                    "Echec pour <" + nomChampBD + "/" + nomChampTampon + "='" + valeurFromTampon + "'>",
                                    ex
                                    );
                            }
                        }

                        // Les données n'existent pas et on peut insérer --> INSERT
                        // Les données existent et on peut mettre à jour --> UPDATE

                        dataGridViewCourant.Rows[indexLigne].DefaultCellStyle.BackColor = Color.LightGray;

                        if ( !donnéesExistent && assoChampsBDTampon.InsertsOK )
                        {
                            if (0 != this.connexionBD.Insérer(strNomTableBD, listeParamètres))
                            {
                                //compteurASucces++;
                                dataGridViewCourant.Rows[indexLigne].DefaultCellStyle.BackColor = Color.LawnGreen;

                                this.dtInsertionsSuccès.Rows.Add(ligne.ItemArray);
                            }
                        }
                        else if (donnéesExistent && assoChampsBDTampon.UpdatesOK)
                        {
                            if (0 != this.connexionBD.MettreAJour(strNomTableBD, listeParamètres, listeClésPrimaires, assoChampsBDTampon.ConditionsAdditionnelles))
                            {
                                //compteurASucces++;
                                dataGridViewCourant.Rows[indexLigne].DefaultCellStyle.BackColor = Color.LawnGreen;

                                this.dtInsertionsSuccès.Rows.Add(ligne.ItemArray);
                            }
                        }

                        // Test clé primaire
                        else if (!assoChampsBDTampon.InsertsOK
                            &&   !assoChampsBDTampon.UpdatesOK )
                        {
                            if (donnéesExistent)
                            {
                                //compteurASucces++;
                                dataGridViewCourant.Rows[indexLigne].DefaultCellStyle.BackColor = Color.LawnGreen;
                                this.dtInsertionsSuccès.Rows.Add(ligne.ItemArray);
                            }
                            else
                            {
                                dataGridViewCourant.Rows[indexLigne].DefaultCellStyle.BackColor = Color.Red;
                                this.dtInsertionsEchecs.Rows.Add(ligne.ItemArray);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        string infoClésPrimaires = " <clé(s):";

                        foreach (DbParameter clé in listeClésPrimaires)
                            infoClésPrimaires +=
                                clé.ParameterName + "=" + clé.Value.ToString();

                        infoClésPrimaires += "> ";

                        GestionnaireDesTâches.Exception(
                            new ImportExportException(
                            "Table " + strNomTableBD,
                            "Enregistrement non importé ligne " + indexLigne + infoClésPrimaires,
                            exception
                            ));

                        dataGridViewCourant.Rows[indexLigne].DefaultCellStyle.BackColor = Color.Red;
                        this.dtInsertionsEchecs.Rows.Add(ligne.ItemArray);

                        System.Threading.Thread.Sleep(10);
                    }

                    indexLigne++;
                    GestionnaireDesTâches.SignalerProgression( indexLigne, 0, this.dtTampon.Rows.Count + 1, false );

                    //backgroundWorker.ReportProgress((100 * indexLigne) / this.dtTampon.Rows.Count);

                    if (GestionnaireDesTâches.ArrêtDemandé ) //|| backgroundWorker.CancellationPending)
                    {
                        doWorkEventArgs.Cancel = true;
                        break;
                    }
                }

                //if (listeClésPrimaires.Count > 0)

                //if( peutInterrompre )
                //MessageBox.Show(
                //    compteurASucces + " lignes insérées dans la table " + strTable,
                //    "Opération terminée",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Information
                // );

            }
            catch (Exception exception)
            {
                GestionnaireDesTâches.Exception(
                    new ImportExportException(
                        "Import de la table " + strNomTableBD,
                        "Echec d'import de '" + strNomTableBD + "' (erreur ligne " + indexLigne + ")",
                        exception
                        ));
                System.Threading.Thread.Sleep(5);
                //GestionnaireDesTâches.addException(exception);
                // Gestion des erreurs
                //MessageBox.Show(
                //    exception.Message,
                //    "Erreur pendant copie BD",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Error
                // );
            }
            this.connexionBD.ReverrouillerTable(strNomTableBD);
        }

        private void BGWorkExporterTouteLaBD( object sender, DoWorkEventArgs doWorkEventArgs )
        {
            System.Threading.Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            //BackgroundWorker backgroundWorker = sender as BackgroundWorker;

            long compteurEnregistrements = 0;
            long nombreTotalEnregistrements = 0;

            GestionnaireDesTâches.Titre("Exporter Toute La BD ");
            
            string répertoireDesExports = 
            	GestionnaireCSV.Répertoire + "\\" +
                GestionnaireCSV.GénérerCourtTimestampFichier() + 
            	"_ExportBD\\";
            Directory.CreateDirectory( répertoireDesExports );

            List<string> listeDesTables = this.connexionBD.ListeDesTables();

            for (int i = 0; i < listeDesTables.Count; i++)
                nombreTotalEnregistrements +=
                    GestionnaireBD.Connexion.NombreDeLignes(
                        listeDesTables[i]);
            nombreTotalEnregistrements += listeDesTables.Count;

            //foreach (InfoTable infoTable in infoTableCollection )
            foreach( string strNomTable in listeDesTables )
            {
                // Un incrément pour chaque table
                compteurEnregistrements++;

                try
                {
                    //** Création du fichier de sortie **//
                    string strFichier =
                        répertoireDesExports + 
                        GestionnaireCSV.GénérerNomFichierExport(strNomTable);

                    List<string> listeDesColonnes = connexionBD.ListeDesColonnes(strNomTable);
                    string strEntetes = listeDesColonnes[0].ToString();

                    for (int i = 1; i < listeDesColonnes.Count; i++)
                        strEntetes += GestionnaireCSV.CaractèreSéparateur + listeDesColonnes[i];

                    //** Données de la table **/
                    IDataReader reader = connexionBD.ExecuteReader("SELECT * FROM [" + strNomTable+"]" );

                    //** Ecriture des données de la table **//
                    FileStream fsSortie = new FileStream(strFichier, FileMode.Append);
                    StreamWriter swSortie = new StreamWriter(fsSortie, System.Text.Encoding.Default);

                    swSortie.WriteLine(strEntetes);
                    string strData = "";

                    while (reader.Read())
                    {
                        // Un incrément pour chaque ligne
                        compteurEnregistrements++;
                    	GestionnaireDesTâches.SignalerProgression( compteurEnregistrements, 0, (nombreTotalEnregistrements + 1), false );

                        //backgroundWorker.ReportProgress((int)((100 * compteurEnregistrements) / (nombreTotalEnregistrements + 1)));

                        strData = ""; // reader[0].ToString();
                        bool isFirst = true;

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (isFirst) isFirst = false;
                            else strData += GestionnaireCSV.CaractèreSéparateur;

                            if (reader[i].GetType().ToString().Equals("System.Byte[]"))
                            {
                                Byte[] objetBinaire = (Byte[])reader[i];
                                string hexaString = BitConverter.ToString(objetBinaire);
                                string asciiString = "";
                                //string unicodeString = "";

                                try
                                {
                                    asciiString = System.Text.Encoding.ASCII.GetString(objetBinaire);
                                }
                                catch (Exception) { }

                                /*try
                                {
                                    unicodeString = System.Text.Encoding.Unicode.GetString(objetBinaire);
                                }
                                catch( Exception ) {}*/


                                strData += "<BIN> Ascii:{" + asciiString + "} Hexa:{" + hexaString + "}";
                            }
                            else
                                strData += reader[i].ToString();
                        }

                        swSortie.WriteLine(strData);

                        if (GestionnaireDesTâches.ArrêtDemandé ) //|| backgroundWorker.CancellationPending)
                        {
                            //backgroundWorker.CancelAsync();
                        	doWorkEventArgs.Cancel = true;
                            break;
                        }
                    }

                    // Fermeture
                    reader.Close();
                    swSortie.Close();
                    fsSortie.Close();

                    GestionnaireDesTâches.Message("Fichier exporté :\n" + strFichier);
                }
                catch (Exception exception)
                {
                    GestionnaireDesTâches.Exception(
                        new ImportExportException(
                            "ExportBD",
                            "Export de la table " + strNomTable,
                            exception
                            ));
                    System.Threading.Thread.Sleep(5);
                }

                if (GestionnaireDesTâches.ArrêtDemandé )//|| backgroundWorker.CancellationPending)
                {
                    //backgroundWorker.CancelAsync();
                    doWorkEventArgs.Cancel = true;
                    break;
                }
            }
        }

        private void BGWorkImporterTouteLaBD(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            System.Threading.Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            //BackgroundWorker backgroundWorker = sender as BackgroundWorker;

            long nombreTotalEnregistrements = 0;
            long compteurEnregistrements = 0;
            long compteurdExceptions = 1;

            int indexTable = 0;
            int indexLigne = 0;

            string strNomTableBD = "";
            string strNomFichierImport = "";
            string strNomFichierAssociation = "";

            GestionnaireDesTâches.Titre("Importer Toute La BD ");

            for (int i = 0; i < this.assoTablesBDFichiersImport.FichiersImport.Count; i++)
                nombreTotalEnregistrements +=
                    GestionnaireCSV.NombreDeLignes(
                        GestionnaireCSV.Répertoire + "\\" + 
                        this.assoTablesBDFichiersImport.FichiersImport[i]);
            nombreTotalEnregistrements += this.assoTablesBDFichiersImport.FichiersImport.Count;
            //GestionnaireDesTâches.addMessage("Nb lignes lues : " + nombreTotalEnregistrements);


            for(indexTable=0; indexTable<this.assoTablesBDFichiersImport.TablesBD.Count; indexTable++ )
            {
                try
                {
                    indexLigne = 0;

                    //** Report de progression : Incrément pour chaque table et chaque ligne de fichier **//
                    compteurEnregistrements++;

                    // Tous les 100 exceptions, pause obligatoire pour que l'interface graphique puisse se mettre à jour
                    //if (0 == (compteurdExceptions % 250))
                    //    System.Threading.Thread.Sleep(3500);

                    strNomTableBD = this.assoTablesBDFichiersImport.TablesBD[indexTable];
                    strNomFichierImport = this.assoTablesBDFichiersImport.FichiersImport[indexTable];
                    strNomFichierAssociation = this.assoTablesBDFichiersImport.FichiersAssociation[indexTable];

                    if( strNomFichierImport.Equals( AssociationTablesBDFichiersImport.AucunFichierAssociation ) )
                    {
                        GestionnaireDesTâches.Message("Import de "+strNomTableBD+" ignoré : pas de fichier" );
                        continue;
                    }
                    GestionnaireDesTâches.Message("Import de "+strNomTableBD+" depuis "+strNomFichierImport+" - association via : "+strNomFichierAssociation);

                    //** Import du tampon depuis le CSV **//
                    this.dtTampon = GestionnaireCSV.OuvrirFichierCSV(
                        GestionnaireCSV.Répertoire + "\\" + strNomFichierImport
                        );

                    //** Import du fichier d'association depuis le XML **//
                    bool chargerAssociationParDefaut = true;
                    this.assoChampsBDTampon =  new AssociationChampsBDTampon();

                    if( ! strNomFichierAssociation.Equals( AssociationTablesBDFichiersImport.AucunFichierAssociation ) ) 
                    {
                        if( this.assoChampsBDTampon.Charger( GestionnaireCSV.Répertoire + "\\" + strNomFichierAssociation))
                            chargerAssociationParDefaut = false;
                    }

                    //** Si besoin création d'un fichier d'association par défaut **//
                    if( chargerAssociationParDefaut )
                    {
                        this.assoChampsBDTampon.ChampsBD = this.connexionBD.ListeDesColonnes(strNomTableBD);
                        
                        foreach( DataColumn colTampon in this.dtTampon.Columns )
                            this.assoChampsBDTampon.ChampsTampon.Add(colTampon.ColumnName);

                        this.assoChampsBDTampon.InsertsOK = true;
                        this.assoChampsBDTampon.UpdatesOK = false;
                    }

                    //** Chargement du schéma de la table cible et des types de données associés pour conversion **//
                    DataTable dtSchema = GetSchemaTable(strNomTableBD);
                    Dictionary<string, Type> dict = new Dictionary<string, Type>();

                    // Dictionnaire des types de colonne BD 
                    foreach (DataRow row in dtSchema.Rows)
                    {
                        string colName = (string)row["ColumnName"];
                        Type typeColonne = (Type)row["DataType"];

                        dict.Add(colName, typeColonne);
                    }

                    //** Insertion ligne par ligne dans table BD **//
                    for( indexLigne = 0; indexLigne < this.dtTampon.Rows.Count; indexLigne++ )
                    {
                        compteurEnregistrements++;
                        //backgroundWorker.ReportProgress((int)((100 * compteurEnregistrements) / (nombreTotalEnregistrements+1)));
//                        newPourcentage = (int)((100 * compteurEnregistrements) / (nombreTotalEnregistrements + 1));
//
//                        if ( newPourcentage > oldPourcentage )
//                        {
//                            backgroundWorker.ReportProgress(newPourcentage);
//                            System.Threading.Thread.Sleep(5);
//                            oldPourcentage = newPourcentage;
//                            //GestionnaireDesTâches.addMessage(oldPourcentage + "% -> " + newPourcentage + "%");
//                        }


    		            GestionnaireDesTâches.SignalerProgression( compteurEnregistrements, 0, (nombreTotalEnregistrements+1), false );	
                        DataRow ligne = this.dtTampon.Rows[ indexLigne ];

                        // Dictionnaire des clés primaires
                        List<DbParameter> listeClésPrimaires = new List<DbParameter>();
                        List<DbParameter> listeParamètres = new List<DbParameter>();

                        try
                        {
                            // Parcours de la liste des clés primaires
                            for (int indexPK = 0; indexPK < assoChampsBDTampon.ClésPrimaires.Count; indexPK++)
                            {
                                string nomPK = assoChampsBDTampon.ClésPrimaires[indexPK];
                                object valeurPK = ligne[assoChampsBDTampon[nomPK]];
                                Type typePK;
							    try
							    {
								    dict.TryGetValue(nomPK, out typePK);

								    DbParameter paramPK = this.connexionBD.CréerDBParameter();

								    paramPK.ParameterName = "@" + nomPK;
								    paramPK.SourceColumn = nomPK;
								    paramPK.Value = Convert.ChangeType(valeurPK, typePK);
								    paramPK.DbType = getDBType(typePK);

								    listeClésPrimaires.Add(paramPK);
							    }
							    catch (Exception ex)
							    {
                                    compteurdExceptions++;
                                    System.Threading.Thread.Sleep(5);
								    throw new ImportExportException(
									    "Clé Primaire",
									    "Echec pour <" + nomPK + "/" + assoChampsBDTampon[nomPK] + "='" + valeurPK + "'>",
									    ex
									    );
							    }
                            }

                            bool donnéesExistent =
                                ( listeClésPrimaires.Count > 0 )
                                 && this.connexionBD.CléExiste(strNomTableBD, listeClésPrimaires);

                            if (listeClésPrimaires.Count > 0)
                                this.connexionBD.DeverrouillerTable(strNomTableBD);

                            // Parcours de la liste des champs
                            for (int colIdx = 0; colIdx < assoChampsBDTampon.ChampsBD.Count; colIdx++)
                            {
                                string nomChampBD = assoChampsBDTampon.ChampsBD[colIdx];
                                string nomChampTampon = assoChampsBDTampon.ChampsTampon[colIdx];
                                object valeurFromTampon = ligne[nomChampTampon];
                                Type typeChamp;
    							
						        try
						        {
							        dict.TryGetValue(nomChampBD, out typeChamp);

							        DbParameter param = this.connexionBD.CréerDBParameter();

							        param.ParameterName = "@" + nomChampBD;
							        param.SourceColumn = nomChampBD;
							        //param.Value = Convert.ChangeType(valeurFromTampon, typeChamp);
							        param.DbType = getDBType(typeChamp);
    								
							        // Valeur nulle
							        if (0 == valeurFromTampon.ToString().Length)
								        param.Value = DBNull.Value;
							        else
								        param.Value = Convert.ChangeType(valeurFromTampon, typeChamp);

							        listeParamètres.Add(param);
						        }
						        catch (Exception ex)
						        {
                                    compteurdExceptions++;
                                    System.Threading.Thread.Sleep(5);
							        throw new ImportExportException(
								        "Paramètre de requête",
								        "Echec pour <" + nomChampBD + "/" + nomChampTampon + "='" + valeurFromTampon + "'>",
								        ex
								        );
						        }
                            }

                            // Les données n'existent pas et on peut insérer --> INSERT
                            // Les données existent et on peut mettre à jour --> UPDATE
                            if (!donnéesExistent && assoChampsBDTampon.InsertsOK)
                                this.connexionBD.Insérer(strNomTableBD, listeParamètres);
                            else if (donnéesExistent && assoChampsBDTampon.UpdatesOK)
                                this.connexionBD.MettreAJour(strNomTableBD, listeParamètres, listeClésPrimaires, assoChampsBDTampon.ConditionsAdditionnelles);
                        }
                        catch (Exception exception)
                        {
                            string infoClésPrimaires = " <clé(s):";

                            foreach (DbParameter clé in listeClésPrimaires)
                                infoClésPrimaires +=
                                    clé.ParameterName + "=" + clé.Value.ToString();

                            infoClésPrimaires += "> ";

                            GestionnaireDesTâches.Exception(
                                new ImportExportException(
                                "Table "+ strNomTableBD,
                                "Enregistrement non importé ligne "+indexLigne + infoClésPrimaires,
                                exception
                                ));

                            compteurdExceptions++;
                            System.Threading.Thread.Sleep(5);
                        }
                        
                        if (GestionnaireDesTâches.ArrêtDemandé ) //|| backgroundWorker.CancellationPending)
                        {
                            // backgroundWorker.CancelAsync();
                        	doWorkEventArgs.Cancel = true;
                            //GestionnaireDesTâches.addMessage("Annulation prise en compte (1)");
                            break;
                        }
                    } // Fin FOREACH Ligne
                }
                catch (Exception exception)
                {
                    GestionnaireDesTâches.Exception(
                        new ImportExportException(
                            "Import de masse",
                            "Echec d'import de '"+strNomTableBD+"' (erreur ligne "+indexLigne+")",
                            exception
                            ));

                    compteurdExceptions++;
                    System.Threading.Thread.Sleep(5);
                }

                this.connexionBD.ReverrouillerTable(strNomTableBD);

                if (GestionnaireDesTâches.ArrêtDemandé ) //|| backgroundWorker.CancellationPending)
                {
                    //backgroundWorker.CancelAsync();
                    doWorkEventArgs.Cancel = true;
                    break;
                }
            }// Fin FOR 
        }

        #endregion
    }
}
        /*
        DirectoryInfo di = new DirectoryInfo(GestionnaireCSV.Répertoire);
        FileInfo[] listeDesFichiersDuRepertoire = di.GetFiles("*.csv");

        foreach (FileInfo fichier in listeDesFichiersDuRepertoire)
        {


            try
            {
                PurgerTamponCourant();
                ChargerFichierCSVdansTampon(new StreamReader(fichier.OpenRead()));

                Regex regex = new Regex("^export_[a-zA-Z_]+_[0-9]+\\.csv$");

                int start = fichier.Name.IndexOf('_') + 1;
                int stop = fichier.Name.LastIndexOf('_');
                string strNomTable = fichier.Name.Substring(start, stop - start);

                //lbListeTables.SelectedItem = strNomTable;

                if (bDeleteTable == true)
                    this.connexionBD.ExecuteNonQuery(
                        "DELETE FROM " +
                        ((InfoTable)lbListeTables.SelectedItem).NomTable
                    );
                throw new NotImplementedException("Fonction Pas Faite");
                // InsérerTamponDansTableBD();
            }

        }//*/




/*

using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace Import_Export_CSV
{
    public class GestionnaireBD
    {
        private static GestionnaireBD gestionnaire = null;

        public static GestionnaireBD Instance
        {
            get
            {
                if (null == gestionnaire)
                    gestionnaire = new GestionnaireBD();

                return gestionnaire;
            }
        }


        private ConnexionBD connexionBD;
        private ICollection infoTableCollection;
        private string repertoireExport;
        private string caractèreSéparateur;

        public void ExporterTouteLaBD( ConnexionBD connexion, ICollection collectionDesTables, string repertoireDEnregistrementDesExports, string caractèreDeSéparation )
        {
            connexionBD = connexion;
            infoTableCollection = collectionDesTables;
            repertoireExport = repertoireDEnregistrementDesExports;
            caractèreSéparateur = caractèreDeSéparation;

            GestionnaireDesTâches.démarrerTâcher(1, infoTableCollection.Count);

            Thread newThread;
            newThread = new System.Threading.Thread(this.ThreadExporterTouteLaBD);
            newThread.Start();

            while(!newThread.IsAlive) 
                Thread.Sleep( 1000 );

            newThread.Join();
            GestionnaireDesTâches.tâcheAchevée();
        }

        private void ThreadExporterTouteLaBD()
        {
            foreach (InfoTable infoTable in infoTableCollection )
            {
                try
                {
                    // Création du fichier de sortie
                    string strFichier =
                        repertoireExport +
                        "\\export_" + infoTable.NomTable + "_" +
                        DateTime.Now.Year.ToString() +
                        DateTime.Now.Month.ToString() +
                        DateTime.Now.Day.ToString() +
                        DateTime.Now.Hour.ToString() +
                        DateTime.Now.Minute.ToString() +
                        DateTime.Now.Second.ToString() +
                        ".csv";

                    // Liste des colonnes de la table
                    // SqlCommand sqlCommand = new SqlCommand( "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE '"+strTable+"'", sqlConnexion  );
                    // SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                    List<string> listeDesColonnes = connexionBD.ListeDesColonnes(infoTable.NomTable);

                    // sqlReader.Read();
                    string strEntetes = listeDesColonnes[0].ToString();

                    for (int i = 1; i < listeDesColonnes.Count; i++)
                        strEntetes += caractèreSéparateur + listeDesColonnes[i];

                    // sqlReader.Close();

                    // Liste des données de la table
                    IDataReader reader = connexionBD.ExecuteReader("SELECT * FROM " + infoTable.NomTable);

                    // Ecriture des données de la table
                    FileStream fsSortie = new FileStream(strFichier, FileMode.Append);
                    StreamWriter swSortie = new StreamWriter(fsSortie);

                    swSortie.WriteLine(strEntetes);
                    string strData = "";

                    while (reader.Read())
                    {
                        strData = ""; // reader[0].ToString();
                        bool isFirst = true;

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (isFirst) isFirst = false;
                            else strData += caractèreSéparateur;

                            if (reader[i].GetType().ToString().Equals("System.Byte[]"))
                            {
                                Byte[] objetBinaire = (Byte[])reader[i];
                                string hexaString = BitConverter.ToString(objetBinaire);
                                string asciiString = "";
                                //string unicodeString = "";

                                try
                                {
                                    asciiString = System.Text.Encoding.ASCII.GetString(objetBinaire);
                                }
                                catch (Exception) { }


                                strData += "<OBJ> Ascii:{" + asciiString + "} Hexa:{" + hexaString + "}";
                            }
                            else
                                strData += reader[i].ToString();
                        }

                        swSortie.WriteLine(strData);
                    }

                    // Fermeture
                    reader.Close();
                    swSortie.Close();
                    fsSortie.Close();


                }
                catch (Exception exception)
                {
                    GestionnaireDesTâches.ajouterException(exception);
                    // Gestion des erreurs
                    //if (DialogResult.No == MessageBox.Show(
                    //    exception.Message + "\nContinuer ?",
                    //    "Erreur d'extraction",
                    //    MessageBoxButtons.YesNo,
                    //    MessageBoxIcon.Warning
                    // ))
                    //    break;
                }

                GestionnaireDesTâches.incrémenterTâche();

                if (GestionnaireDesTâches.ArrêtTâcheDemandé)
                    return;
            }
        }   
    }
}



// */