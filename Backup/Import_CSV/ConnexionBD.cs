using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data.Common;

namespace Import_Export_Universel
{
	public abstract class ConnexionBD
	{
		/*
		 *
		 * [Serializable]
		public class Type
		{
			public enum enumValue
			{
				Indefini = 0,
				Informix = 1,
				SQLServer = 2
			} ;

			[NonSerialized]
			public static readonly Type Indefini = new Type("Indefini", enumValue.Indefini, 0);
			[NonSerialized]
			public static readonly Type Informix = new Type("Informix", enumValue.Informix, 1);
			[NonSerialized]
			public static readonly Type SQLServer = new Type( "SQLServer", enumValue.SQLServer, 2 );

			[NonSerialized]
			public static readonly Type[] liste = new Type[3]
			{
				Type.Indefini,
				Type.Informix,
				Type.SQLServer
			};

			private string strValue;
			private int intValue;
			private enumValue enumerationValue;

			public string StrValue
			{
				get { return strValue; }
				//set { strValue = value; }
			}
			public enumValue eValue
			{
				get { return enumerationValue; }
				//set { intValue = value; }
			}
			public int iValue
			{
				get { return intValue; }
				// set { intValue = value; }
			}

			public Type()
			{
				this.strValue = "Indefini";
				this.enumerationValue = enumValue.Indefini;
				this.intValue = 0;
			}

			public Type( Type leType )
			{
				this.strValue = leType.strValue;
				this.enumerationValue = leType.enumerationValue;
				this.intValue = leType.intValue;
			}

			public Type(string strValue, enumValue eValue, int intValue )
			{
				this.strValue = strValue;
				this.enumerationValue = eValue;
				this.intValue = intValue;
			}

			public override string ToString()
			{
				return strValue;
			}
		}//*/

		public enum TypeEnumValue
		{
			Indefini = 0,
			Informix = 1,
			SQLServer = 2
		} ;

		public static Type Indefini = new Type("Indefini", TypeEnumValue.Indefini, 0);
		public static Type Informix = new Type("Informix", TypeEnumValue.Informix, 1);
		public static Type SQLServer = new Type( "SQLServer", TypeEnumValue.SQLServer, 2 );

		public static readonly Type[] Types = new Type[3]
		{
			ConnexionBD.Indefini,
			ConnexionBD.Informix,
			ConnexionBD.SQLServer
		};

		[Serializable]
		public struct Type
		{
			public string strValue;
			public int intValue;
			public TypeEnumValue enumerationValue;

			public Type(string strValue, TypeEnumValue eValue, int intValue )
			{
				this.strValue = strValue;
				this.enumerationValue = eValue;
				this.intValue = intValue;
			}
		}

		public struct Options
		{
			/*public Options()
			{
				this.AuthentificationIntegree = false;
				this.ConnexionODBC = false;
			}*/
			public Options(bool AuthentificationIntegree, bool ConnexionODBC)
			{
				this.AuthentificationIntegree = AuthentificationIntegree;
				this.ConnexionODBC = ConnexionODBC;
			}

			public bool AuthentificationIntegree;
			public bool ConnexionODBC;
		};
		

		public static String strFormatDeConnexionAuthSQLServer = @"Data Source={0};Initial Catalog={1};User ID={2};Password={3};";
		public static String strFormatDeConnexionAuthWindows = @"Data Source={0};Initial Catalog={1};Integrated Security=True";
		public static String strFormatDeConnexionInformix = @"Database={1}; Host={0}; Server={0}; Service=1960; Protocol=onsoctcp; UID={2}; Password={3}";
		//public static String strFormatDeConnexionODBCInformix = @"Dsn={0}; Driver={4}; Host={0}; Server={0}; Protocol=onsoctcp; Database={1}; Uid={2}; Pwd={3}";
		public static String strFormatDeConnexionODBC = @"Dsn={0};";
		//@"DSN={0}; UID={1}; PWD={2};";

		public static string CreerChaineConnexion( Type newTypeBase, Options newOptions, string strHost, string strDatabase, string strUser, string strPassword)
		{
			string strChaineDeConnexion = "Erreur";

			//if (newTypeBase != null)
			//{
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
				else if (newTypeBase.intValue == Informix.intValue)
				{
					strChaineDeConnexion = string.Format(
						strFormatDeConnexionInformix,
						strHost,
						strDatabase,
						strUser,
						strPassword);
				}
				else if (newTypeBase.intValue == SQLServer.intValue)
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
			//}
			return strChaineDeConnexion;
		}
		/*
		public static ConnexionBD CreerConnexion(Type newTypeBase, Options newOptions, string strHost, string strDatabase, string strUser, string strPassword )
		{
			ConnexionBD valRetour = null ;

			if( newTypeBase.eValue == Type.Informix )
			{
				ConnexionInformix valRetour2 = new ConnexionInformix(  Options newOptions, string strHost, string strDatabase, string strUser, string strPassword );
				
			}
			else
			{
				valRetour = new ConnexionSQLServer(  Options newOptions, string strHost, string strDatabase, string strUser, string strPassword );
			}

			return valRetour;
		}//*/

		protected string strChaineDeConnexion = null;

		protected IDbConnection dbConnexion = null;
		protected IDbCommand dbCommand = null;
		protected IDataReader dataReader = null;

		protected List<string> listeDesTables = null;
		protected List<string> listeDesVues = null;
		protected List<string> listeDesBases = null;

		protected Type typeBase = Indefini;
		protected Options options;

		public string NomConnexion { get; set; }

		public Type TypeBase
		{
			get { return this.typeBase; }
		}

		public ConnexionBD( string strNomConnexion, Type newTypeBase, Options newOptions, string strConnectionString)
		{
			this.NomConnexion = strNomConnexion;

			this.typeBase = newTypeBase;
			this.options = newOptions;
			this.strChaineDeConnexion = strConnectionString;

			Connect();
		}

		/*public ConnexionBD(Type newTypeBase, Options newOptions, string strHost, string strDatabase, string strUser, string strPassword)
		{
			this.typeBase = newTypeBase ;
			this.options = newOptions;
			this.strChaineDeConnexion = CreerChaineConnexion(newTypeBase, options, strHost, strDatabase, strUser, strPassword);
			//Connect();
		}//*/

		/// <summary>
		/// Destructeur
		/// </summary>
		~ConnexionBD()
		{
			Close();
		}

		protected abstract void Connect();

		public void Close()
		{
			try
			{
				if (this.dataReader != null)
					this.dataReader.Close();

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
				this.listeDesVues = null;
			}
		}

		public abstract DbParameter CréerDBParameter();


		public abstract DataTable ChargerDataTable( string strRequete );


		/// <summary>
		/// Exécuter une requête SQL
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public abstract IDataReader ExecuteReader(string sqlQuery);

		/// <summary>
		/// Exécuter une requête SQL
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public abstract IDataReader ExecuteReader(string sqlQuery, List<DbParameter> listeDesParamètres );

		/// <summary>
		/// Execute des requêtes INSERT / UPDATE / DELETE
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public abstract int ExecuteNonQuery(string sqlQuery);

		/// <summary>
		/// Execute des requêtes INSERT / UPDATE / DELETE
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public abstract int ExecuteNonQuery(string sqlQuery, List<DbParameter> listeDesParamètres );
		
		/// <summary>
		/// Execute des requêtes
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public abstract object ExecuteScalar(string sqlQuery);

		/// <summary>
		/// Execute des requêtes
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public abstract object ExecuteScalar(string sqlQuery, List<DbParameter> listeDesParamètres);

		/// <summary>
		/// Liste des tables
		/// </summary>
		/// <returns></returns>
		public abstract List<string> ListeDesTables();

		/// <summary>
		/// Liste des vues
		/// </summary>
		/// <returns></returns>
		public abstract List<string> ListeDesVues();
		
		/// <summary>
		/// Liste des tables
		/// </summary>
		/// <returns></returns>
		public abstract List<string> ListeDesBases();
		
		/// <summary>
		/// Liste des colonnes d'une table
		/// </summary>
		/// <returns></returns>
		public abstract List<string> ListeDesColonnes(string strNomTable);

		public abstract DataTable InformationsTable(string strNomTable);

		public abstract void DeverrouillerTable(string strTable);

		public abstract void ReverrouillerTable(string strTable);

		//public bool ClésExistent(string strNomTable, /*(Ordered)*/Dictionary<string, string> dictionnaire)
		public bool CléExiste(string strNomTable, NameValueCollection dictionnaire)
		{
			if ( dictionnaire.Count > 0 )
			{
				List<DbParameter> liste = new List<DbParameter>();
				//string strQuery = "" ;
				//bool first = true;

				foreach( string clé in dictionnaire.Keys )
				{
					DbParameter param = CréerDBParameter();

					param.ParameterName = "@" + clé;
					param.SourceColumn = clé;
					param.Value = dictionnaire[clé];

					liste.Add(param);
					/*string valeur = dictionnaire[clé].Replace('\'', '_').Replace('\\', '_').Replace(';', '_');

					//if (dictionnaire.TryGetValue(clé, out valeur))
					if (first)
					{
						strQuery =
							" WHERE " + clé + " = '" + valeur + "' ";
						first = false;
					}
					else
					{
						strQuery += " AND " + clé + " = '" + valeur + "' ";
					}//*/
				}

				return 0 < CompteWhereExiste(strNomTable, liste);
			}

			return false;
		}

		/*
		public bool ClésExistent(string strNomTable, List<string> lstNomsClés, List<string> lstValeursClés )
		{
			if (lstNomsClés.Count > 0 && lstNomsClés.Count == lstValeursClés.Count)
			{
				string strQuery =
					" WHERE " + lstNomsClés[0].Replace('\'', '_').Replace('\\', '_').Replace(';', '_') +
					" = '" + lstValeursClés[0].Replace('\'', '_').Replace('\\', '_').Replace(';', '_') + "' ";

				for (int i = 1; i < lstNomsClés.Count; i++)
				{
					strQuery +=
						" AND " + lstNomsClés[i].Replace('\'', '_').Replace('\\', '_').Replace(';', '_') +
						" = '" + lstValeursClés[i].Replace('\'', '_').Replace('\\', '_').Replace(';', '_') + "' ";
				}

				return 0 < WhereExiste(strNomTable, strQuery);
			}

			return false; // throw new Exception("Clé primaire incorrecte");
		}//*/

		public bool CléExiste(string strNomTable, string strNomClé, string strValeurClé )
		{
			List<DbParameter> liste = new List<DbParameter>();
			DbParameter param = CréerDBParameter();

			param.ParameterName = "@" + strNomClé;
			param.SourceColumn = strNomClé;
			param.Value = strValeurClé;

			liste.Add(param);
			return 0 < CompteWhereExiste( strNomTable, liste );
		}

		public bool CléExiste(string strNomTable, List<DbParameter> listeDesParamètres)
		{
			return( 0 < CompteWhereExiste(strNomTable, listeDesParamètres));
		}
		
		public int NombreDeLignes(string strNomTable)
		{
			return CompteWhereExiste(strNomTable, null);
		}

		public int CompteWhereExiste(string strNomTable, List<DbParameter> listeDesParamètres)
		{
			string strQuery = "SELECT COUNT( * ) FROM [" + strNomTable + "]  " ;
			bool first = true;

			if( null != listeDesParamètres )
			foreach (DbParameter param in listeDesParamètres)
			{
				if (first)
				{
					first = false;
					strQuery += " WHERE " + param.SourceColumn + " = " + param.ParameterName;
				}
				else
				{
					strQuery += " AND " + param.SourceColumn + " = " + param.ParameterName;
				}
			}

			return Int32.Parse(ExecuteScalar(strQuery,listeDesParamètres).ToString());
		}

		public object GetValeur( string strNomTable, string strNomChampVoulu, string strNomCléRecherche, string strValeurCléRecherche )
		{
			DbParameter paramClé = CréerDBParameter();

			paramClé.ParameterName = "@" + strNomCléRecherche;
			paramClé.SourceColumn = strNomCléRecherche;
			paramClé.Value = strValeurCléRecherche;

			DbParameter paramChampVoulu = CréerDBParameter();

//			paramChampVoulu.ParameterName = "@" + strNomChampVoulu;
//			paramChampVoulu.SourceColumn = strNomChampVoulu;
//			paramChampVoulu.Value = DBNull.Value;
			return GetValeur( strNomTable, strNomChampVoulu, paramClé );
		}
	   
		public object GetValeur( string strNomTable, string nomChampVoulu, DbParameter cléRecherche )
		{
			List<DbParameter> liste = new List<DbParameter>();
			liste.Add( cléRecherche );
			
			string strRequete = 
				" SELECT  "+nomChampVoulu+
				"   FROM ["+strNomTable+
				"] WHERE "+cléRecherche.SourceColumn+" = "+cléRecherche.ParameterName;
			
			//GestionnaireDesTâches.Message(strRequete);
			return ExecuteScalar( strRequete, liste );
		}

		public int MettreAJour(string strNomTable, List<string> lstNomsChamps, List<string> lstValeursChamps, List<string> lstChampsCléPrimaire, List<string> lstValeursCléPrimaire, string strConditionsAdditionnelles )
		{
			List<DbParameter> listeDesParamètres = new List<DbParameter>();
			List<DbParameter> listeDesClésPrimaires = new List<DbParameter>();
			//NameValueCollection dictDonnées = new NameValueCollection();
			//NameValueCollection dictCléPrimaire = new NameValueCollection();

			for (int i = 0; i < lstNomsChamps.Count; i++)
			{
				DbParameter newParam = CréerDBParameter();

				newParam.ParameterName = "@" + lstNomsChamps[i];
				newParam.SourceColumn = lstNomsChamps[i];
				newParam.Value = lstValeursChamps[i];

				listeDesParamètres.Add(newParam);
				//dictDonnées.Add(lstNomsChamps[i], lstValeursChamps[i]);
			}

			for (int i = 0; i < lstChampsCléPrimaire.Count; i++)
			{
				DbParameter newParam = CréerDBParameter();

				newParam.ParameterName = "@" + lstNomsChamps[i];
				newParam.SourceColumn = lstNomsChamps[i];
				newParam.Value = lstValeursChamps[i];

				listeDesClésPrimaires.Add(newParam);
				listeDesParamètres.Add(newParam);
				//dictCléPrimaire.Add(lstChampsCléPrimaire[i], lstValeursCléPrimaire[i]);
			}
			return MettreAJour(strNomTable, listeDesParamètres, listeDesClésPrimaires, strConditionsAdditionnelles);
			//return MettreAJour(strNomTable, dictDonnées, dictCléPrimaire, strConditionsAdditionnelles);
		}

		public int MettreAJour(string strNomTable, List<DbParameter> listeDesParamètres, List<DbParameter> listeDesClésPrimaires, string strConditionsAdditionnelles)
		{
			bool first = true;
			//string valeur = null;
			string strRequeteSQL = null;

			List<string> nomDesClésPrimaires = new List<string>();

			foreach (DbParameter param in listeDesClésPrimaires)
				nomDesClésPrimaires.Add(param.SourceColumn);

			foreach (DbParameter param in listeDesParamètres)
			{
				// Seulement si cela ne fait pas partie de la clé primaire
				//if (null == dictCléPrimaire.GetValues(champ) || 0 == dictCléPrimaire.GetValues(champ).Length)
				if (!nomDesClésPrimaires.Contains(param.SourceColumn))
				{
					//valeur = dictDonnées[champ].Replace('\'', '_').Replace('\\', '_').Replace(';', '_');

					if (first)
					{
						strRequeteSQL =
							"UPDATE [" + strNomTable +
							"] SET " + param.SourceColumn +
							" = " + param.ParameterName  ;
						//if (valeur.Length > 0) strRequeteSQL += " = '" + valeur + "' ";
						//else strRequeteSQL += " = NULL ";

						first = false;
					}
					else
					{
						strRequeteSQL += ", " + param.SourceColumn + " = " + param.ParameterName;
						//if (valeur.Length > 0) strRequeteSQL += ", " + champ + " = '" + valeur + "' ";
						//else strRequeteSQL += ", " + champ + " = NULL ";
					}
				}
			}

			first = true;

			foreach (DbParameter param in listeDesClésPrimaires)
			{
				//if (dictCléPrimaire.TryGetValue(champ, out valeur))
				//{
				//valeur = dictCléPrimaire[champ].Replace('\'', '_').Replace('\\', '_').Replace(';', '_');

				if (first)
				{
					//if (valeur.Length > 0) strRequeteSQL += " WHERE " + champ + " = '" + valeur + "' ";
					//else strRequeteSQL += " WHERE " + champ + " = NULL ";

					strRequeteSQL += " WHERE " + param.SourceColumn + " = " + param.ParameterName ;
					first = false;
				}
				else
				{
					//if (valeur.Length > 0) strRequeteSQL += " AND " + champ + " = '" + valeur + "' ";
					//else strRequeteSQL += " AND " + champ + " = NULL ";

					strRequeteSQL += " AND " + param.SourceColumn + " = " + param.ParameterName ;
				}
				//}
			}

			if (strConditionsAdditionnelles.Length > 1)
			{
				if (first) strRequeteSQL += " WHERE " + strConditionsAdditionnelles;
				else strRequeteSQL += " AND " + strConditionsAdditionnelles;

			}

			return ExecuteNonQuery( strRequeteSQL, listeDesParamètres );
		}
/*
		public int MettreAJour(string strNomTable, NameValueCollection dictDonnées, NameValueCollection dictCléPrimaire, string strConditionsAdditionnelles )
		{
			bool first = true;
			string valeur = null;
			string strRequeteSQL = null;

			try
			{
				foreach (string champ in dictDonnées.Keys)
				{
					// Seulement si cela ne fait pas partie de la clé primaire
					if (null == dictCléPrimaire.GetValues(champ) || 0 == dictCléPrimaire.GetValues(champ).Length)
					{
						valeur = dictDonnées[champ].Replace('\'', '_').Replace('\\', '_').Replace(';', '_');

						if (first)
						{
							strRequeteSQL = "UPDATE " + strNomTable + " SET " + champ;

							if (valeur.Length > 0) strRequeteSQL += " = '" + valeur + "' ";
							else strRequeteSQL += " = NULL ";

							first = false;
						}
						else
						{
							if (valeur.Length > 0) strRequeteSQL += ", " + champ + " = '" + valeur + "' ";
							else strRequeteSQL += ", " + champ + " = NULL ";
						}
					}
				}

				first = true;

				foreach (string champ in dictCléPrimaire.Keys)
				{
					//if (dictCléPrimaire.TryGetValue(champ, out valeur))
					//{
					valeur = dictCléPrimaire[champ].Replace('\'', '_').Replace('\\', '_').Replace(';', '_');

					if (first)
					{
						if (valeur.Length > 0)  strRequeteSQL += " WHERE " + champ + " = '" + valeur + "' ";
						else					strRequeteSQL += " WHERE " + champ + " = NULL ";

						first = false;
					}
					else
					{
						if (valeur.Length > 0)  strRequeteSQL += " AND " + champ + " = '" + valeur + "' ";
						else					strRequeteSQL += " AND " + champ + " = NULL ";
					}
					//}
				}

				if( strConditionsAdditionnelles.Length > 1 )
				{
					if (first)				  strRequeteSQL += " WHERE " + strConditionsAdditionnelles;
					else						strRequeteSQL += " AND " + strConditionsAdditionnelles;

				}

				return ExecuteNonQuery(strRequeteSQL);
			}
			catch (Exception plantage)
			{
				throw new Exception(
					"MettreAJour : Erreur de la requête SQL : \n" + strRequeteSQL +
					"\n\n" + plantage.Message);
			}
		}
		// */
		public int Insérer(string strNomTable, List<string> lstNomsChamps, List<string> lstValeursChamps )
		{
			List<DbParameter> listeDesParamètres = new List<DbParameter>();

			for (int i = 0; i < lstNomsChamps.Count; i++)
			{
				DbParameter newParam = CréerDBParameter();

				newParam.ParameterName = "@" + lstNomsChamps[i];
				newParam.SourceColumn = lstNomsChamps[i];
				newParam.Value = lstValeursChamps[i];

				listeDesParamètres.Add( newParam );
			}

			return Insérer(strNomTable, listeDesParamètres);
		}

		public int Insérer(string strNomTable, List<DbParameter> listeDesParamètres)
		{
			bool first = true;
			string strChampsRequete = null;
			string strValeursRequete = null;
			//string valeur = null;
			string strRequeteSQL = null;

			foreach (DbParameter param in listeDesParamètres)
			{
				if (first)
				{
					strChampsRequete = "INSERT INTO [" + strNomTable + "] ( " + param.SourceColumn;
					strValeursRequete = " ) VALUES ( " + param.ParameterName;

					//if (valeur.Length > 0) strValeursRequete = " ) VALUES ( " + param.ParameterName + " ";
					//else strValeursRequete = " ) VALUES ( NULL ";

					first = false;
				}
				else
				{
					strChampsRequete += ", " + param.SourceColumn;
					strValeursRequete += ", " + param.ParameterName;

					//if (valeur.Length > 0) strValeursRequete += ", '" + valeur + "' ";
					//else strValeursRequete += ", NULL ";
				}
			}

			strRequeteSQL = strChampsRequete + strValeursRequete + " ) ";
			return ExecuteNonQuery(strRequeteSQL, listeDesParamètres);
			//}
			//catch (Exception plantage)
			//{
			//	throw new Exception(
			//		"Insérer : Erreur de la requête SQL : \n" + strRequeteSQL +
			//		"\n\n" + plantage.Message);
			//}
		}
	}
}



	//HybridDictionary hybrid = new HybridDictionary();
	//StringDictionary sd = new StringDictionary();
	//NameValueCollection dictionnaire = new NameValueCollection();

	//for( int i=0; i<lstNomsChamps.Count; i++ )
	//{
	//	dictionnaire.Add(lstNomsChamps[i], lstValeursChamps[i]);
	//}

	//return Insérer( strNomTable, dictionnaire );
/*public int Insérer(string strNomTable, NameValueCollection dictionnaire )
{
	bool first = true;
	string strChampsRequete = null;
	string strValeursRequete = null;
	string valeur = null;
	string strRequeteSQL = null;

	//Hashtable hache = new Hashtable();
	//hache.Keys[0].
	//hybrid.Keys
	//ordered.Keys
	//sd.Keys
	//ordered[

	try
	{
		foreach (string champ in dictionnaire.Keys)
		{
			valeur = dictionnaire[champ].Replace('\'', '_').Replace('\\', '_').Replace(';', '_');

			if (first)
			{
				strChampsRequete = "INSERT INTO " + strNomTable + " ( " + champ;

				if (valeur.Length > 0)   strValeursRequete = " ) VALUES ( '" + valeur + "' ";
				else					 strValeursRequete = " ) VALUES ( NULL ";

				first = false;
			}
			else
			{
				strChampsRequete += ", " + champ;

				if (valeur.Length > 0)  strValeursRequete += ", '" + valeur + "' ";
				else					strValeursRequete += ", NULL ";
			}
		}

		strRequeteSQL = strChampsRequete + strValeursRequete + " ) ";
		return ExecuteNonQuery(strRequeteSQL);
	}
	catch (Exception plantage)
	{
		throw new Exception(
			"Insérer : Erreur de la requête SQL : \n" + strRequeteSQL +
			"\n\n" + plantage.Message);
	}
}//*/