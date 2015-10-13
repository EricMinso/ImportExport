using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;


namespace Import_CSV
{
    public class ConnexionBD
    {



        //public static String strFormatDeConnexionAuthSQLServer = @"Data Source={0};Initial Catalog={1};User ID={2};Password={3};";
        //public static String strFormatDeConnexionAuthWindows = @"Data Source={0};Initial Catalog={1};Integrated Security=True";
        //public static String strFormatDeConnexionInformix = @"Database={1}; Host={0}; Server={0}; Service=1960; UID={2}; Password={3}";
        //public static String strFormatDeConnexionODBCInformix = @"Dsn={0}; Driver={4}; Host={0}; Server={0}; Protocol=onsoctcp; Database={1}; Uid={2}; Pwd={3}"; 
        //public static String strFormatDeConnexionODBC = @"Dsn={0};";
        //@"DSN={0}; UID={1}; PWD={2};";

        public static string CreerChaineConnexion(Type newTypeBase, Options newOptions, string strHost, string strDatabase, string strUser, string strPassword)
        {
            string strChaineDeConnexion = "Erreur";

            if (newTypeBase != null)
            {
                if (newOptions.ConnexionODBC == true)
                {
                    /*strChaineDeConnexion = string.Format(
                        strFormatDeConnexionODBCInformix,
                        strHost,
                        strDatabase,
                        strUser,
                        strPassword,
                        "{INFORMIX}");*/
                    strChaineDeConnexion = string.Format(
                        strFormatDeConnexionODBC,
                        strHost);
                }
                else if (newTypeBase.eValue == Type.Informix.eValue)
                {
                    strChaineDeConnexion = string.Format(
                        strFormatDeConnexionInformix,
                        strHost,
                        strDatabase,
                        strUser,
                        strPassword);
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
        }


        private string strChaineDeConnexion = null;

        private IDbConnection dbConnexion = null;
        private IDbCommand dbCommand = null;
        private IDataReader dataReader = null;

        private List<string> listeDesTables = null;

        private Type typeBase = Type.Indefini;
        private Options options;

        public Type TypeBase
        {
            get { return this.typeBase; }
        }

        public ConnexionBD(Type newTypeBase, Options newOptions, string strConnectionString)
        {
            this.typeBase = newTypeBase;
            this.options = newOptions;
            this.strChaineDeConnexion = strConnectionString;
            Connect();
        }

        public ConnexionBD(Type newTypeBase, Options newOptions, string strHost, string strDatabase, string strUser, string strPassword)
        {
            this.typeBase = newTypeBase;
            this.options = newOptions;
            this.strChaineDeConnexion = CreerChaineConnexion(newTypeBase, options, strHost, strDatabase, strUser, strPassword);
            Connect();
        }

        /// <summary>
        /// Destructeur
        /// </summary>
        ~ConnexionBD()
        {
            Close();
        }

        private void Connect()
        {
            // Ouverture de la connexion
            if (this.options.ConnexionODBC == true)
            {
                this.dbConnexion = new OdbcConnection(this.strChaineDeConnexion);
            }
            else if (this.typeBase.eValue == Type.Informix.eValue)
            {
                this.dbConnexion = new IfxConnection(this.strChaineDeConnexion);
            }
            else if (this.typeBase.eValue == Type.SQLServer.eValue)
            {
                this.dbConnexion = new SqlConnection(this.strChaineDeConnexion);
            }

            if (this.dbConnexion != null)
                this.dbConnexion.Open();
        }

        public void Close()
        {
            try
            {
                if (this.dataReader != null) this.dataReader.Close();

                if (this.dbConnexion != null && this.dbConnexion.State != ConnectionState.Closed)
                    this.dbConnexion.Close();
            }
            catch (Exception)
            { }
            finally
            {
                this.dataReader = null;
                this.dbCommand = null;
                this.dbConnexion = null;
                this.listeDesTables = null;
            }
        }

        /// <summary>
        /// Exécuter une requête SQL
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string sqlQuery)
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
                        if (this.typeBase == Type.Informix)
                        {
                            IfxCommand ifxCommand = new IfxCommand(sqlQuery, (this.dbConnexion as IfxConnection));
                            IfxDataReader ifxDataReader = ifxCommand.ExecuteReader(CommandBehavior.Default);

                            this.dbCommand = ifxCommand;
                            this.dataReader = ifxDataReader;
                        }
                        else if (this.typeBase == Type.SQLServer)
                        {
                            SqlCommand sqlCommand = new SqlCommand(sqlQuery, (this.dbConnexion as SqlConnection));
                            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.Default);

                            this.dbCommand = sqlCommand;
                            this.dataReader = sqlDataReader;
                        }
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
        public int ExecuteNonQuery(string sqlQuery)
        {
            lock (this.dbConnexion)
            {
                int returnValue = -1;

                if (this.options.ConnexionODBC == true)
                {
                    OdbcCommand odbcCommand = new OdbcCommand(sqlQuery, (this.dbConnexion as OdbcConnection));
                    returnValue = odbcCommand.ExecuteNonQuery();
                }
                else
                {
                    if (this.typeBase == Type.Informix)
                    {
                        IfxCommand ifxCommand = new IfxCommand(sqlQuery, (this.dbConnexion as IfxConnection));
                        returnValue = ifxCommand.ExecuteNonQuery();
                    }
                    else if (this.typeBase == Type.SQLServer)
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlQuery, (this.dbConnexion as SqlConnection));
                        returnValue = sqlCommand.ExecuteNonQuery();
                    }
                }

                return returnValue;
            }
        }

        /// <summary>
        /// Liste des tables
        /// </summary>
        /// <returns></returns>
        public List<string> ListeDesTables()
        {
            lock (this.dbConnexion)
            {
                if (this.listeDesTables == null)
                {
                    this.listeDesTables = new List<string>();

                    // Liste des tables
                    // SqlCommand sqlCommand = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME", sqlConnexion);
                    // SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                    if (this.typeBase.eValue == Type.Informix.eValue)
                    {
                        ExecuteReader("SELECT tabname FROM systables ORDER BY tabname");
                    }
                    else if (this.typeBase == Type.SQLServer)
                    {
                        ExecuteReader("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME");
                    }

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

        /// <summary>
        /// Liste des colonnes d'une table
        /// </summary>
        /// <returns></returns>
        public List<string> ListeDesColonnes(string strNomTable)
        {
            //lock (this.dbConnexion)
            //{
            List<string> listeDesColonnes = new List<string>();


            if (this.typeBase.eValue == ConnexionBD.Type.Informix.eValue)
            {
                ExecuteReader(@"
                SELECT syscolumns.colname 
                  FROM syscolumns, systables 
                 WHERE syscolumns.tabid = systables.tabid
                   AND systables.tabname LIKE '" + strNomTable + "'");
            }
            else if (this.typeBase == ConnexionBD.Type.SQLServer)
            {
                // Liste des tables
                // SqlCommand sqlCommand = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME", sqlConnexion);
                // SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                //executeReader( "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE '" + strTable + "'" );
                //SqlDataReader slqReader = (SqlDataReader) 
                ExecuteReader("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE '" + strNomTable + "'");

                /*if (slqReader != null && slqReader.HasRows)
                {
                    while ( true == slqReader.Read() )
                    {
                        string strNomColonne = slqReader.GetString(0);
                        listeDesColonnes.Add(strNomColonne);
                        //listeDesColonnes.Add(this.dataReader[0].ToString());
                    }

                    this.dataReader.Close();
                    this.dataReader = null;
                }*/
            }

            if (this.dataReader != null)
            {
                while (this.dataReader.Read())
                {
                    string strNomColonne = this.dataReader.GetString(0);
                    listeDesColonnes.Add(strNomColonne);
                    //listeDesColonnes.Add(this.dataReader[0].ToString());
                }

                this.dataReader.Close();
                this.dataReader = null;
            } //*/

            return listeDesColonnes;
            //}
        }

        public void DeverrouillerTable(string strTable)
        {
            lock (this.dbConnexion)
            {
                if (this.typeBase.eValue == Type.SQLServer.eValue)
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand("SET IDENTITY_INSERT " + strTable + " ON", (this.dbConnexion as SqlConnection));
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException) { }
                }
            }
        }
        public void ReverrouillerTable(string strTable)
        {
            lock (this.dbConnexion)
            {
                if (this.typeBase.eValue == Type.SQLServer.eValue)
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand("SET IDENTITY_INSERT " + strTable + " OFF", (this.dbConnexion as SqlConnection));
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException) { }
                }
            }
        }
    }
}
