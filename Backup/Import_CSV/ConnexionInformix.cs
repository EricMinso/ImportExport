using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System;
using System.Data.Common;
//using IBM.Data.Informix;


namespace Import_Export_Universel
{
    public class ConnexionInformix : ConnexionBD
    {
        
        public ConnexionInformix(string strNomConnexion, Options newOptions, string strConnectionString)
            : base( strNomConnexion, Informix, newOptions, strConnectionString )
        {
            Connect();
        }

        protected override void Connect()
        {
            // Ouverture de la connexion
            if (this.options.ConnexionODBC == true)
            {
                this.dbConnexion = new OdbcConnection(this.strChaineDeConnexion);
            }
            else ///if (this.typeBase.eValue == Type.Informix.eValue)
            {
                // this.dbConnexion = new IfxConnection(this.strChaineDeConnexion);
                throw new Exception("Pas implémenté");
            }

            if (this.dbConnexion != null)
                this.dbConnexion.Open();
        }

        public override DbParameter CréerDBParameter() 
        { 
            throw new Exception("Pas implémenté"); 
        }


        public override DataTable ChargerDataTable(string strRequete)
        {
            throw new Exception("pas implémenté");
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
                        //if (this.typeBase == Type.Informix)
                        
                        /*IfxCommand ifxCommand = new IfxCommand(sqlQuery, (this.dbConnexion as IfxConnection));
                        IfxDataReader ifxDataReader = ifxCommand.ExecuteReader(CommandBehavior.Default);

                        this.dbCommand = ifxCommand;
                        this.dataReader = ifxDataReader; */

                        throw new Exception("Pas implémenté");
                    }
                }
            }
            return this.dataReader;
        }
        public override IDataReader ExecuteReader(string sqlQuery, List<DbParameter> listeDesParamètres)
        {
            throw new Exception("Pas implémenté");
        }

        public override int ExecuteNonQuery(string sqlQuery)
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
                    //if (this.typeBase == Type.Informix)
                    /*IfxCommand ifxCommand = new IfxCommand(sqlQuery, (this.dbConnexion as IfxConnection));
                    returnValue = ifxCommand.ExecuteNonQuery();*/

                    throw new Exception("Pas implémenté");
                }

                return returnValue;
            }
        }

        public override int ExecuteNonQuery(string sqlQuery, List<DbParameter> listeDesParamètres)
        {
            throw new Exception("Pas implémenté");
        }

        /// <summary>
        /// Execute des requêtes 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public override object ExecuteScalar(string sqlQuery)
        {
            lock (this.dbConnexion)
            {
                object returnValue = null;

                if (this.options.ConnexionODBC == true)
                {
                    OdbcCommand odbcCommand = new OdbcCommand(sqlQuery, (this.dbConnexion as OdbcConnection));
                    returnValue = odbcCommand.ExecuteScalar();
                }
                else
                {
                    /*IfxCommand ifxCommand = new IfxCommand(sqlQuery, (this.dbConnexion as IfxConnection));
                    returnValue = ifxCommand.ExecuteScalar();*/

                    throw new Exception("Pas implémenté");
                }

                return returnValue;
            }
        }

        public override object ExecuteScalar(string sqlQuery, List<DbParameter> listeDesParamètres)
        { 
            throw new Exception("Pas implémenté");
        }

        public override List<string> ListeDesBases()
        {
            return null;
        }

        public override List<string> ListeDesVues()
        {
            throw new Exception("pas implémenté");
        }

        public override List<string> ListeDesTables()
        {
            lock (this.dbConnexion)
            {
                if (this.listeDesTables == null)
                {
                    this.listeDesTables = new List<string>();

                    // Liste des tables
                    ExecuteReader("SELECT tabname FROM systables ORDER BY tabname");
                    

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

        public override List<string> ListeDesColonnes(string strNomTable)
        {
            //lock (this.dbConnexion)
            //{
            List<string> listeDesColonnes = new List<string>();


            //if (this.typeBase.eValue == ConnexionBD.Type.Informix.eValue)
            ExecuteReader(@"
                SELECT syscolumns.colname 
                  FROM syscolumns, systables 
                 WHERE syscolumns.tabid = systables.tabid
                   AND systables.tabname LIKE '" + strNomTable + "'");
            

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
        
        public override DataTable InformationsTable(string strNomTable)
        {
            //lock (this.dbConnexion)
            //{
            DataTable dataTable = new DataTable();

            // Liste des tables
            ExecuteReader(@"
                SELECT 
                    syscolumns.colname, 
                    syscolumns.coltype,
                    syscolumns.collength,
                    syscolumns.colmin,
                    syscolumns.colmax

                  FROM
                    syscolumns,
                    systables 

                 WHERE
                    syscolumns.tabid = systables.tabid
                   AND
                    systables.tabname LIKE '" + strNomTable + @"'
                 ORDER BY
                    colno
            ");

            dataTable.Columns.Add("colname");
            dataTable.Columns.Add("coltype");
            dataTable.Columns.Add("collength");
            dataTable.Columns.Add("colmin");
            dataTable.Columns.Add("colmax");

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
        { }

        public override void ReverrouillerTable(string strTable)
        { }
    }
}
