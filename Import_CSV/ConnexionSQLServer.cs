using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Windows.Forms;
using Import_Export_CSV.Properties;
using System.Data.Common;
using System;


namespace Import_Export_CSV
{
    public class ConnexionSQLServer : ConnexionBD
    {
        /*
        public static string CreerChaineConnexion(Type newTypeBase, Options newOptions, string strHost, string strDatabase, string strUser, string strPassword)
        {
            string strChaineDeConnexion = "Erreur";

            if (newTypeBase != null)
            {
                if (newOptions.ConnexionODBC == true)
                {
                    strChaineDeConnexion = string.Format(
                        strFormatDeConnexionODBC,
                        strHost);
                }
                else if (newTypeBase.eValue == Type.SQLServer.eValue)
                {
                    if (newOptions.AuthentificationIntegree == true)
                    {
                        strChaineDeConnexion = string.Format(
                            strFormatDeConnexionAuthWindows,
                            strHost,
                            strDatabase);
                    }
                    else
                    {
                        strChaineDeConnexion = string.Format(
                            strFormatDeConnexionAuthSQLServer,
                            strHost,
                            strDatabase,
                            strUser,
                            strPassword);
                    }
                }
            }
            return strChaineDeConnexion;
        }//*/
        public ConnexionSQLServer(string strNomConnexion, Options newOptions, string strConnectionString) 
            : base( strNomConnexion, SQLServer, newOptions, strConnectionString )
        {
        }
        /*
        public ConnexionSQLServer(Options newOptions, string strHost, string strDatabase, string strUser, string strPassword)
            : base( Type.SQLServer, newOptions, strHost, strDatabase, strUser, strPassword)
        {
        }
         * //*/

        protected override void Connect()
        {
            // Ouverture de la connexion
            if (this.options.ConnexionODBC == true)
            {
                this.dbConnexion = new OdbcConnection(this.strChaineDeConnexion);
            }
            else if (this.typeBase.intValue == SQLServer.intValue)
            {
                this.dbConnexion = new SqlConnection(this.strChaineDeConnexion);
            }

            if (this.dbConnexion != null)
                this.dbConnexion.Open();
        }

        public override DbParameter CréerDBParameter() 
        {
            if (this.options.ConnexionODBC == true)
            {
                OdbcParameter param = new OdbcParameter();
                return param;
            }
            else
            {
                SqlParameter param = new SqlParameter();
                return param;
            }
        }


        public override DataTable ChargerDataTable(string strRequete)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(strRequete, (SqlConnection)this.dbConnexion);
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            return dataSet.Tables[0];
        }

        /// <summary>
        /// Exécuter une requête SQL
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(string sqlQuery)
        {
            if (this.dbConnexion != null)
            {
                lock (this.dbConnexion)
                {
                    if (this.dataReader != null)
                    {
                        this.dataReader.Close();
                    }

                    this.dataReader = null;
                    this.dbCommand = null;

                    if (this.options.ConnexionODBC == true)
                    {
                        OdbcCommand odbcCommand = new OdbcCommand(sqlQuery, (this.dbConnexion as OdbcConnection));
                        OdbcDataReader odbcDataReader = odbcCommand.ExecuteReader(CommandBehavior.Default);

                        this.dbCommand = odbcCommand;
                        this.dataReader = odbcDataReader;
                    }
                    else
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlQuery, (this.dbConnexion as SqlConnection));
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.Default);

                        this.dbCommand = sqlCommand;
                        this.dataReader = sqlDataReader;
                    }
                }
            }
            return this.dataReader;
        }

        /// <summary>
        /// Execute des requêtes INSERT / UPDATE / DELETE
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public override int ExecuteNonQuery(string sqlQuery)
        {
            return ExecuteNonQuery(sqlQuery, null);
        }

        /// <summary>
        /// Execute des requêtes INSERT / UPDATE / DELETE
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public override int ExecuteNonQuery(string sqlQuery, List<DbParameter> listeDesParamètres)
        {
            int returnValue = -1;
            DbCommand dbCommand=null;

            try
            {
                lock (this.dbConnexion)
                {
                    if (this.options.ConnexionODBC == true)
                    {
                        dbCommand = new OdbcCommand(sqlQuery, (this.dbConnexion as OdbcConnection));
                    }
                    else
                    {
                        dbCommand = new SqlCommand(sqlQuery, (this.dbConnexion as SqlConnection));
                    }

                    if (null != listeDesParamètres)
                    {
                        foreach (DbParameter param in listeDesParamètres)
                        {
                            if (param.Value != null && (param.Value.ToString().Length == 0 || param.Value.ToString().Equals("NULL")))
                                param.Value = DBNull.Value;
                            dbCommand.Parameters.Add(param);
                        }
                    }

                    returnValue = dbCommand.ExecuteNonQuery();
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                string strMessageErreur = (dbCommand == null) ?
                    "Echec création dbCommand\n" :
                    "Echec Requête\n\n" + dbCommand.CommandText + "\n\n";


                foreach( DbParameter param in listeDesParamètres )
                    strMessageErreur += param.ParameterName + " = '" + param.Value.ToString() + "'\n" ;

                throw new ImportExportException(
                    "ExecuteNonQuery",
                    strMessageErreur,
                    ex
                );
            }

        }

        /// <summary>
        /// Execute des requêtes 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public override object ExecuteScalar(string sqlQuery)
        {
                return ExecuteScalar( sqlQuery, null );
        }

        public override object ExecuteScalar(string sqlQuery, List<DbParameter> listeDesParamètres)
        { 
            object returnValue = null;
            DbCommand dbCommand=null;

            try
            {
                lock (this.dbConnexion)
                {
                    if (this.options.ConnexionODBC == true)
                    {
                        dbCommand = new OdbcCommand(sqlQuery, (this.dbConnexion as OdbcConnection));
                    }
                    else
                    {
                        dbCommand = new SqlCommand(sqlQuery, (this.dbConnexion as SqlConnection));
                    }

                    if (null != listeDesParamètres)
                    {
                        foreach (DbParameter param in listeDesParamètres)
                        {
                            if (param.Value != null && (param.Value.ToString().Length == 0 || param.Value.ToString().Equals("NULL")))
                                param.Value = DBNull.Value;
                            dbCommand.Parameters.Add(param);
                        }
                    }

                    returnValue = dbCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                string strMessageErreur = (dbCommand == null) ?
                    "Echec création dbCommand\n" :
                    "Echec Requête\n\n" + dbCommand.CommandText + "\n\n";


                foreach( DbParameter param in listeDesParamètres )
                    strMessageErreur += param.ParameterName + " = '" + param.Value.ToString() + "'\n" ;

                throw new ImportExportException(
                    "ExecuteScalar",
                    strMessageErreur,
                    ex
                );
                //throw new ImportExportException(
                //    "ExecuteScalar",
                //    (dbCommand != null ) ? "Echec Requête " + dbCommand.CommandText : "Echec création dbCommand",
                //    ex
                //    );
            }

            return returnValue;
        }
        /// Liste des bases
        /// </summary>
        /// <returns></returns>
        public override List<string> ListeDesBases()
        {
            lock (this.dbConnexion)
            {
                if (this.listeDesBases == null)
                {
                    this.listeDesBases = new List<string>();

                    // Liste des tables
                    ExecuteReader("SELECT name FROM sys.databases ORDER BY name");

                    if (this.dataReader != null)
                    {
                        while (this.dataReader.Read())
                        {;
                            string strNomBase = this.dataReader.GetString(0);
                            this.listeDesBases.Add(strNomBase);
                        }

                        this.dataReader.Close();
                        this.dataReader = null;
                    }
                }
                return this.listeDesBases;
            }
        }

        /// <summary>
        /// Liste des tables
        /// </summary>
        /// <returns></returns>
        public override List<string> ListeDesTables()
        {
            lock (this.dbConnexion)
            {
                if (this.listeDesTables == null)
                {
                    this.listeDesTables = new List<string>();

                    // Liste des tables
                    ExecuteReader(
                        @"SELECT TABLE_NAME 
                            FROM INFORMATION_SCHEMA.TABLES 
                           WHERE TABLE_TYPE LIKE 'BASE_TABLE'
                        ORDER BY TABLE_NAME ");

                    if (this.dataReader != null)
                    {
                        while (this.dataReader.Read())
                        {
                            // string strNomTable = this.dataReader[0].ToString();
                            string strNomTable = this.dataReader.GetString(0);
                            this.listeDesTables.Add(strNomTable);
                        }

                        this.dataReader.Close();
                        this.dataReader = null;
                    }
                }
                return this.listeDesTables;
            }
        }

        public override List<string> ListeDesVues()
        {

            lock (this.dbConnexion)
            {
                if (this.listeDesVues == null)
                {
                    this.listeDesVues = new List<string>();

                    // Liste des tables
                    ExecuteReader("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS ORDER BY TABLE_NAME");

                    if (this.dataReader != null)
                    {
                        while (this.dataReader.Read())
                        {
                            // string strNomTable = this.dataReader[0].ToString();
                            string strNomVue = this.dataReader.GetString(0);
                            this.listeDesVues.Add(strNomVue);
                        }

                        this.dataReader.Close();
                        this.dataReader = null;
                    }
                }
                return this.listeDesVues;
            }
        }

        /// <summary>
        /// Liste des colonnes d'une table
        /// </summary>
        /// <returns></returns>
        public override List<string> ListeDesColonnes(string strNomTable)
        {
            //lock (this.dbConnexion)
            //{
            List<string> listeDesColonnes = new List<string>();
            //List<string> listeDesTypesDeColonnes = new List<string>();

            // Liste des tables
            ExecuteReader("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE '" + strNomTable + "'");

            if (this.dataReader != null)
            {
                while (this.dataReader.Read())
                {
                    listeDesColonnes.Add(this.dataReader.GetString(0));
                    //listeDesTypesDeColonnes.Add(this.dataReader.GetString(1));
                    //listeDesColonnes.Add(this.dataReader[0].ToString());
                }

                this.dataReader.Close();
                this.dataReader = null;
            } //*/

            return listeDesColonnes;
        }

        public override DataTable InformationsTable(string strNomTable)
        {
            //lock (this.dbConnexion)
            //{
            DataTable dataTable = new DataTable();

            // Liste des tables
            ExecuteReader(@"
                SELECT
                    COLUMN_NAME, 
                    DATA_TYPE,
                    CHARACTER_MAXIMUM_LENGTH,
                    IS_NULLABLE,
                    COLUMN_DEFAULT

                FROM
                    INFORMATION_SCHEMA.COLUMNS 

                WHERE 
                    TABLE_NAME LIKE '" + strNomTable + @"'

                ORDER BY
                    ORDINAL_POSITION
");

            dataTable.Columns.Add("COLUMN_NAME");
            dataTable.Columns.Add("DATA_TYPE");
            dataTable.Columns.Add("CHARACTER_MAXIMUM_LENGTH");
            dataTable.Columns.Add("IS_NULLABLE");
            dataTable.Columns.Add("COLUMN_DEFAULT");

            if (this.dataReader != null)
            {
                while (this.dataReader.Read())
                {
                    DataRow row = dataTable.NewRow();

                    row[0] = this.dataReader[0].ToString();
                    row[1] = this.dataReader[1].ToString();
                    row[2] = this.dataReader[2].ToString();
                    row[3] = this.dataReader[3].ToString();
                    row[4] = this.dataReader[4].ToString();

                    dataTable.Rows.Add(row);
                }

                this.dataReader.Close();
                this.dataReader = null;
            } //*/

            return dataTable;
        }

        public override void DeverrouillerTable(string strTable)
        {
            lock (this.dbConnexion)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SET IDENTITY_INSERT [" + strTable + "] ON", (this.dbConnexion as SqlConnection));
                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException) { }
            }
        }
        public override void ReverrouillerTable(string strTable)
        {
            lock (this.dbConnexion)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SET IDENTITY_INSERT [" + strTable + "] OFF", (this.dbConnexion as SqlConnection));
                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException) { }
            }
        }
    }
}
