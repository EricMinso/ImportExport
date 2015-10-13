/*
 * Created by SharpDevelop.
 * User: Minso
 * Date: 30/05/2013
 * Time: 14:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;

namespace Import_Export_Universel
{
	class EquipementSite
	{
		public int eqs_ID = -1;
		public string eqs_Ref_Affaire = "";
		public string eqs_Chargé_Affaire = "";
		public string eqs_Date_Install = "";
		
		public List<DossierPlan> listeDossiersPlan = null;
		
		public override string ToString()
		{
			return eqs_Ref_Affaire;
		}
	}
	
	class Machine
	{
		public int mac_ID = -1;
		public string mac_Code_Machine = "";
		public string mac_Abrégé = "";
		public string mac_Genre_Machine_FR = "";
		
		public override string ToString()
		{
			return mac_Genre_Machine_FR;
		}
	}
	
	class Plan
	{
		public int pln_ID = -1;
		public string pln_Numéro = "";
		public string pln_Indice = "";
		
		public override string ToString()
		{
			return pln_Numéro;
		}
	}
	
	class Elt_Liste_Plans
	{
		public int lpl_ID = -1;
		
		public DossierPlan dossierPlan = null;
		public Plan plan = null;
	}
	
	class DossierPlan
	{
		public FileInfo fichier = null;
		
		public EquipementSite equipementSite = null;
		public Machine machine = null;
		
		public int dop_ID = 0;
		
		public string dop_Num_Dossier = "";
		public string dop_Caracteristiques = "";
		public string dop_Circuit = "";
		public int dop_Quantité = 0;

		public int dop_Tranche = 0;
		public int dop_Voie = 0;
		
		public string dop_Lien_Doc = "";
		public List<Elt_Liste_Plans> listePlans = null;
		
		public override string ToString()
		{
			return fichier.Name;
		}
	}
	
	
	/// <summary>
	/// Description of FormBEAUDREY.
	/// </summary>
	public partial class FormBEAUDREY : Form
	{
		private DirectoryInfo dossier;
		private FileInfo[] fichiers;
		private DataTable dataTableContenuFichier;
		private DataTable dataTableListeFichiers;
		
//		private List<EquipementSite> listeEquipementsSiteSansDoublon;
//		private List<Plan> listePlansSansDoublon;
//		private List<Machine> listeMachinesSansDoublon;
		private DossierPlan[] listeDossiersPlan;
		
		
		public FormBEAUDREY( string strRépertoire )
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			this.tbRepertoire.Text = strRépertoire;
		}
		
		private void FormBEAUDREYLoad(object sender, EventArgs e)
		{
			dataTableListeFichiers = new DataTable();
			
			dossier = new DirectoryInfo(this.tbRepertoire.Text);
			fichiers = dossier.GetFiles( "*.xls" );
			listeDossiersPlan = new DossierPlan[ fichiers.Length ];
			
			dataTableListeFichiers.Columns.Add( "Nom du fichier", typeof(object) );
			dataTableListeFichiers.Columns.Add( "Ref Affaire", typeof(string) );
			dataTableListeFichiers.Columns.Add( "N° Machine", typeof(string) );
			dataTableListeFichiers.Columns.Add( "Quantité Eqt site", typeof(int) );

			int indexLignesDGV = 0;
			for ( int indexFichiers=0; indexFichiers<fichiers.Length; indexFichiers++ )
			{
				FileInfo fichier = fichiers[indexFichiers];
				
				// Split du nom et choppage des infos
				string[] splitNomFichier = fichier.Name.Split( new char[] { '-' } );
				
				if( splitNomFichier.Length > 3 )
				{
					DossierPlan dossierPlan = new DossierPlan();
					dossierPlan.fichier = fichier;
//	            	dossierPlan.refAffaire = splitNomFichier[ 0 ];
//	            	dossierPlan.numMachine = splitNomFichier[ 1 ];
//	            	
//	            	Int32.TryParse( 
//	            	               splitNomFichier[ 3 ], 
//	            	               out dossierPlan.quantité_eqt_site );
					int qté = -1;
					Int32.TryParse( splitNomFichier[ 3 ], out qté );
					
					DataRow row = dataTableListeFichiers.NewRow();            	
					
					EquipementSite eqs = ChargerEquipementSite( splitNomFichier[ 0 ] );
					Machine machine = ChargerMachine( splitNomFichier[ 1 ] );
					
					row[ 0 ] = dossierPlan;
					row[ 1 ] = ( eqs != null ) ? eqs.ToString() : "<Pas d'équipement>";
					row[ 2 ] = ( machine != null ) ? machine.ToString() : "<Pas de machine>";
					row[ 3 ] = qté;
//	            	row[ 1 ] = dossierPlan.refAffaire;
//	            	row[ 2 ] = dossierPlan.numMachine;
//	            	row[ 3 ] = dossierPlan.quantité_eqt_site;
					
					dataTableListeFichiers.Rows.Add( row );
	//                if (0 == (fichier.Attributes & FileAttributes.Directory))
	//                    liste.Add(fichier.Name);
					listeDossiersPlan[ indexLignesDGV++ ] = dossierPlan;
				}
			}
			
			dgvBEAUDREY.DataSource = dataTableListeFichiers;
		}
		
		private void DgvBEAUDREYDoubleClick(object sender, EventArgs e)
		{
			if( dgvBEAUDREY.SelectedRows.Count > 0 )
			{
				this.tlp_BEAUDREYmaster.Enabled = false;	
//				TraiterFichier(
//					dataTableListeFichiers
//						.Rows[ dgvBEAUDREY.SelectedRows[0].Index ][ 0 ]  as DossierPlan
//				);
				DossierPlan dplan = dataTableListeFichiers
				    	.Rows[ dgvBEAUDREY.SelectedRows[0].Index ][ 0 ]
					  as DossierPlan;
					
				
				GestionnaireDesTâches.Démarrer(
					new DoWorkEventHandler( BGWorkTraiterFichier ),
					null,
					new RunWorkerCompletedEventHandler( EvénementTerminéTraiterFichier ), 
					dplan
				);
			}
		}
		
		private void BtTraiterFichierClick(object sender, EventArgs e)
		{
			if( dgvBEAUDREY.SelectedRows.Count > 0 )
			{
				this.tlp_BEAUDREYmaster.Enabled = false;		
				
//				object lobj = dataTableListeFichiers
//				    	.Rows[ dgvBEAUDREY.SelectedRows[0].Index ][ 0 ];
				
				DossierPlan dplan = dataTableListeFichiers
				    	.Rows[ dgvBEAUDREY.SelectedRows[0].Index ][ 0 ]
					  as DossierPlan;
					
				
				GestionnaireDesTâches.Démarrer(
					new DoWorkEventHandler( BGWorkTraiterFichier ),
					null,
					new RunWorkerCompletedEventHandler( EvénementTerminéTraiterFichier ), 
					dplan
				);
			}
		}
		
		private void BtTraiterTousLesFichiersClick(object sender, EventArgs e)
		{
			GestionnaireDesTâches.Démarrer(
					new DoWorkEventHandler( BGWorkTraiterTousLesFichiers ),
					null,
					new RunWorkerCompletedEventHandler( EvénementTerminéTraiterFichier ), 
					null
				);
		}

		private void BGWorkTraiterTousLesFichiers(object sender, DoWorkEventArgs doWorkEventArgs)
		{
			for( int index = 0; index < listeDossiersPlan.Length && false == GestionnaireDesTâches.ArrêtDemandé ; index++ )
			{
				try
				{
					//BackgroundWorker backgroundWorker = sender as BackgroundWorker;
					DossierPlan dplan = listeDossiersPlan[ index ];
					dplan.listePlans = new List<Elt_Liste_Plans>();
					
					GestionnaireDesTâches.Titre( 
		            	"Traitement du fichier XLS " +
		            	(1+index)+"/"+listeDossiersPlan.Length +
		            	" : "+dplan.fichier.Name
		            );
					GestionnaireDesTâches.SignalerProgression( index, 0, listeDossiersPlan.Length, false );
					
					string[] splitNomFichier = dplan.fichier.Name.Split( new char[] { '-' } );
		
					if( splitNomFichier.Length > 3 )
					{
						string refAffaire = splitNomFichier[ 0 ];
						string numMachine = splitNomFichier[ 1 ];
						int quantité_eqt_site = -1;
						
						Int32.TryParse( splitNomFichier[ 3 ], out quantité_eqt_site );
						
						dplan.equipementSite = ChargerEquipementSite( refAffaire );
						dplan.machine = ChargerMachine( numMachine );
						dplan.dop_Num_Dossier = dplan.fichier.Name.Remove( dplan.fichier.Name.LastIndexOf( ".xls" ));
						
						if( null == dplan.equipementSite )
							GestionnaireDesTâches.Avertissement( "Equipement "+refAffaire+" non trouvé");
						
						if( null == dplan.machine )
						{
							GestionnaireDesTâches.Avertissement( "Machine "+numMachine+" non trouvée : abandon pour ce fichier");
							continue;
						}
						
						// Ouverture du fichier Excel
						dataTableContenuFichier = GestionnaireExcel.OuvrirFeuilleXLS( dplan.fichier.FullName, 1 );
						dplan.dop_Caracteristiques = dataTableContenuFichier.Columns[ 4 ].ColumnName;
						
						// Parcours du fichier Excel
						for( int indexLigne = 0; indexLigne < dataTableContenuFichier.Rows.Count && false == GestionnaireDesTâches.ArrêtDemandé ; indexLigne++ )
						{
							DataRow row = dataTableContenuFichier.Rows[ indexLigne ];
							
							if( row[2] != null )
							{
								try
								{
									string numéroPlan = row[2].ToString();
									
									if( numéroPlan.Length > 0 && ! numéroPlan.Contains( "N° des Plans" ))
									{
										Plan plan = ChargerPlan( numéroPlan );
										
										if( plan != null )
										{
											Elt_Liste_Plans elt = new Elt_Liste_Plans();
											elt.plan = plan;
											elt.dossierPlan = dplan;
											
											dplan.listePlans.Add( elt );
										}
										else GestionnaireDesTâches.Avertissement( "Plan '"+numéroPlan+"' non trouvé" );
									}
								}
								catch( Exception ex )
								{
									GestionnaireDesTâches.Exception( 
		                            	new ImportExportException( 
                                          "Traitement de la ligne "+(indexLigne+1)+" du fichier "+dplan.fichier.Name,
		                                  ex.Message,
		                                  ex
		                                 ));
								}
							}
						}
						
						if( false == GestionnaireDesTâches.ArrêtDemandé )
							insérerDossierPlan( dplan );
						//doWorkEventArgs.Result = dplan;
					}
				}
				catch( Exception ex )
				{
					GestionnaireDesTâches.Exception( 
                    	new ImportExportException( 
                          "Traitement du fichier "+listeDossiersPlan[ index ].fichier.Name, 
                          ex.Message,
                          ex
                         ));
				}
			}
		}
		

		
		private void BGWorkTraiterFichier(object sender, DoWorkEventArgs doWorkEventArgs)
		{
			//BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			GestionnaireDesTâches.Titre( "Traitement du fichier XLS" );
			DossierPlan dplan = doWorkEventArgs.Argument as DossierPlan ; //dossiersPlan[ dgvBEAUDREY.SelectedRows[0].Index ];
			dplan.listePlans = new List<Elt_Liste_Plans>();
			
			string[] splitNomFichier = dplan.fichier.Name.Split( new char[] { '-' } );

			if( splitNomFichier.Length > 3 )
			{
				string refAffaire = splitNomFichier[ 0 ];
				string numMachine = splitNomFichier[ 1 ];
				int quantité_eqt_site = -1;
				
				Int32.TryParse( splitNomFichier[ 3 ], out quantité_eqt_site );
				
				dplan.equipementSite = ChargerEquipementSite( refAffaire );
				dplan.machine = ChargerMachine( numMachine );
				dplan.dop_Num_Dossier = dplan.fichier.Name.Remove( dplan.fichier.Name.LastIndexOf( ".xls" ));
				dplan.dop_Quantité = quantité_eqt_site;
				
				if( null == dplan.equipementSite ) GestionnaireDesTâches.Avertissement( "Equipement "+refAffaire+" non trouvé");
				if( null == dplan.machine ) GestionnaireDesTâches.Avertissement( "Machine "+numMachine+" non trouvée");
				
				// Ouverture du fichier Excel
				GestionnaireDesTâches.SignalerProgression( 5, 0, 100, false );
				dataTableContenuFichier = GestionnaireExcel.OuvrirFeuilleXLS( dplan.fichier.FullName, 1 );
				
				//GestionnaireDesTâches.SignalerProgression( 10, 0, 100, false );
				GestionnaireDesTâches.Message( "Col 4 = "+dataTableContenuFichier.Columns[ 4 ] );
				GestionnaireDesTâches.Message( "Col 4 Name = "+dataTableContenuFichier.Columns[ 4 ].ColumnName );
				dplan.dop_Caracteristiques = dataTableContenuFichier.Columns[ 4 ].ColumnName;
				
				// Parcours du fichier Excel
				for( int indexLigne = 0; indexLigne < dataTableContenuFichier.Rows.Count ; indexLigne++ )
				{
					DataRow row = dataTableContenuFichier.Rows[ indexLigne ];
					
					GestionnaireDesTâches.SignalerProgression( 10+indexLigne, 0, 10+dataTableContenuFichier.Rows.Count, false );
					
					if( row[2] != null )
					{
						try
						{
							string numéroPlan = row[2].ToString();
							
							if( numéroPlan.Length > 0 && ! numéroPlan.Contains( "N° des Plans" ))
							{
								Plan plan = ChargerPlan( numéroPlan );
								
								if( plan != null )
								{
									Elt_Liste_Plans elt = new Elt_Liste_Plans();
									elt.plan = plan;
									elt.dossierPlan = dplan;
									
									dplan.listePlans.Add( elt );
								}
								else GestionnaireDesTâches.Avertissement( "Plan '"+numéroPlan+"' non trouvé" );
							}
						}
						catch( Exception ex )
						{
							GestionnaireDesTâches.Exception( 
                            	new ImportExportException( 
                                  "Traitement Fichier", 
                                  ex.Message,
                                  ex
                                 ));
						}
					}
				}
				
				GestionnaireDesTâches.Message( "Avant" );
				insérerDossierPlan( dplan );
				//GestionnaireDesTâches.Message( "Après" );
				doWorkEventArgs.Result = dplan;
			}
		}
		
		private void insérerDossierPlan( DossierPlan dplan )
		{
			if( GestionnaireBD.Connexion.CléExiste( 
                   "DOSSIER_PLAN", 
                   "DOP_NUM_DOSSIER",
                   dplan.dop_Num_Dossier ))
			{
				GestionnaireDesTâches.Avertissement( "Dossier Plan '"+dplan.dop_Num_Dossier+"' existe déjà dans la base : abandon" );
			}
				
			List<DbParameter> lstParamDP = new List<DbParameter>();
			
			if( dplan.machine != null )
			{
				DbParameter paramMacID = GestionnaireBD.Connexion.CréerDBParameter();
				
				paramMacID.ParameterName = "@DOP_MAC_ID";
				paramMacID.SourceColumn = "DOP_MAC_ID";
				paramMacID.Value = dplan.machine.mac_ID;
				
				lstParamDP.Add ( paramMacID );
			}
			if( dplan.equipementSite != null )
			{
				DbParameter paramEquID = GestionnaireBD.Connexion.CréerDBParameter();
				
				paramEquID.ParameterName = "@DOP_EQU_ID";
				paramEquID.SourceColumn = "DOP_EQU_ID";
				paramEquID.Value = dplan.equipementSite.eqs_ID;
				
				lstParamDP.Add ( paramEquID );
			}
			if( dplan.dop_Num_Dossier != null && dplan.dop_Num_Dossier.Length > 0 )
			{
				DbParameter paramNumDossier = GestionnaireBD.Connexion.CréerDBParameter();
				
				paramNumDossier.ParameterName = "@DOP_NUM_DOSSIER";
				paramNumDossier.SourceColumn = "DOP_NUM_DOSSIER";
				paramNumDossier.Value = dplan.dop_Num_Dossier;
				
				lstParamDP.Add ( paramNumDossier );
			}			
			if( dplan.dop_Caracteristiques != null && dplan.dop_Caracteristiques.Length > 0 )
			{
				DbParameter paramCaractéristiques = GestionnaireBD.Connexion.CréerDBParameter();
				
				paramCaractéristiques.ParameterName = "@DOP_CARACTERISTIQUES";
				paramCaractéristiques.SourceColumn = "DOP_CARACTERISTIQUES";
				paramCaractéristiques.Value = dplan.dop_Caracteristiques;
				
				lstParamDP.Add ( paramCaractéristiques );
			}			
			if( dplan.dop_Quantité > 0 )
			{
				DbParameter paramQuantité = GestionnaireBD.Connexion.CréerDBParameter();
				
				paramQuantité.ParameterName = "@DOP_QUANTITE";
				paramQuantité.SourceColumn = "DOP_QUANTITE";
				paramQuantité.Value = dplan.dop_Quantité;
				
				lstParamDP.Add ( paramQuantité );
			}
			
			//GestionnaireDesTâches.Message( "avant insertion DP" );
				
			if( 1 != GestionnaireBD.Connexion.Insérer( "DOSSIER_PLAN", lstParamDP ))
				throw new ImportExportException( "insérerDossierPlan", "Les données n'ont pas été insérées", null );
			
			//GestionnaireDesTâches.Message( "après insertion DP" );
			
			string strGetValeur = ""+GestionnaireBD.Connexion.GetValeur(
				"DOSSIER_PLAN", 
				"DOP_ID",
				"DOP_NUM_DOSSIER",
				dplan.dop_Num_Dossier );
				
			//GestionnaireDesTâches.Message( "ids = " + strGetValeur);
			
			dplan.dop_ID = Int32.Parse( strGetValeur );
			
			//GestionnaireDesTâches.Message( "idi = " + dplan.dop_ID );
			
			foreach (Elt_Liste_Plans element in dplan.listePlans) 
			{
				List<DbParameter> lstParamLP = new List<DbParameter>();

				DbParameter paramDOP_ID = GestionnaireBD.Connexion.CréerDBParameter();
				
				paramDOP_ID.ParameterName = "@LPL_DOP_ID";
				paramDOP_ID.SourceColumn = "LPL_DOP_ID";
				paramDOP_ID.Value = dplan.dop_ID;
				
				DbParameter paramPLN_ID = GestionnaireBD.Connexion.CréerDBParameter();
				
				paramPLN_ID.ParameterName = "@LPL_PLN_ID";
				paramPLN_ID.SourceColumn = "LPL_PLN_ID";
				paramPLN_ID.Value = element.plan.pln_ID;
				
				lstParamLP.Add( paramDOP_ID );
				lstParamLP.Add( paramPLN_ID );
				
				//GestionnaireDesTâches.Message( "avant insertion LP" );
				
				if( 1 != GestionnaireBD.Connexion.Insérer( "LISTE_PLAN", lstParamLP ))
					throw new ImportExportException( "insérerListePlan", "Les données n'ont pas été insérées", null );
				
				
				//GestionnaireDesTâches.Message( "après insertion LP" );
			}
		}
		
		private void EvénementTerminéTraiterFichier(object sender, RunWorkerCompletedEventArgs e)
		{
			
//			if( e.Error != null )
//			{
//				GestionnaireDesTâches.Exception(
//					new ImportExportException( 
//						"EvénementTerminéTraiterFichier", 
//						"La tâche d'arrière plan s'est terminée avec une erreur.", 
//						e.Error )
//				);
//			}
			
			this.tlp_BEAUDREYmaster.Enabled = true;
		}


		private Machine ChargerMachine( string abrégé_machine )
		{
			IDataReader reader = GestionnaireBD.Connexion.ExecuteReader( @"
				SELECT MAC_ID, MAC_CODE_MACHINE, MAC_ABREGE, MAC_GENRE_MACHINE_FR
				FROM MACHINE
				WHERE MAC_ABREGE LIKE '"+abrégé_machine+"' " );
			
			if( reader.Read() )
			{
				Machine machine = new Machine();
				
				Int32.TryParse( reader[ "MAC_ID" ]+"", out machine.mac_ID );
				machine.mac_Code_Machine = reader[ "MAC_CODE_MACHINE" ]+"";
				machine.mac_Abrégé = reader[ "MAC_ABREGE" ]+"";
				machine.mac_Genre_Machine_FR = reader[ "MAC_GENRE_MACHINE_FR" ]+"";
				
				reader.Close();
				return machine;
			}
			
			reader.Close();
			return null;
		}
	
		private EquipementSite ChargerEquipementSite( string ref_affaire )
		{
//        	if( GestionnaireBD.Connexion.CléExiste("EQUIPEMENT_SITE", "EQS_REF_AFFAIRE", ref_affaire ) )
//        	{
//        		foreach( EquipementSite eqt in listeEquipementsSite )
//        		{
//        			if( eqt.eqs_Ref_Affaire == ref_affaire )
//        				return eqt;
//        		}
//        		
//        		EquipementSite eqt = new EquipementSite();
//        		eqt.eqs_Ref_Affaire = ref_affaire;
//        		listeEquipementsSite.add( eqt );
//        		return eqt;
//        	}
//        	else
//        	{
//        		return null;
//        	}
//        	
			// Première tentative
			IDataReader reader = GestionnaireBD.Connexion.ExecuteReader( @"
					SELECT * 
					FROM EQUIPEMENT_SITE 
					WHERE EQS_REF_AFFAIRE LIKE '"+ref_affaire+"'" );
			
			if( reader.Read() )
			{
				EquipementSite eqt = new EquipementSite();
				
				Int32.TryParse( reader[ "EQS_ID" ]+"", out eqt.eqs_ID );
				
				eqt.eqs_Ref_Affaire = reader[ "EQS_REF_AFFAIRE" ]+"";
				eqt.eqs_Chargé_Affaire = reader[ "EQS_CHARGE_AFFAIRE" ]+"";
				
				reader.Close();
				return eqt;
			}
			
//        	
			// Seconde tentative
			ref_affaire = ref_affaire.Substring( 1 );
			reader = GestionnaireBD.Connexion.ExecuteReader( @"
					SELECT * 
					FROM EQUIPEMENT_SITE 
					WHERE EQS_REF_AFFAIRE LIKE '"+ref_affaire+"'" );
			
			if( reader.Read() )
			{
				EquipementSite eqt = new EquipementSite();
				
				Int32.TryParse( reader[ "EQS_ID" ]+"", out eqt.eqs_ID );
				
				eqt.eqs_Ref_Affaire = reader[ "EQS_REF_AFFAIRE" ]+"";
				eqt.eqs_Chargé_Affaire = reader[ "EQS_CHARGE_AFFAIRE" ]+"";
				
				reader.Close();
				return eqt;
			}
			reader.Close();
			
			return null;
		}
		
		private Plan ChargerPlan( string numéroPlan )
		{
			IDataReader reader = GestionnaireBD.Connexion.ExecuteReader( @"
					SELECT PLN_ID, PLN_NUMERO, PLN_INDICE 
					  FROM [PLAN]
					 WHERE PLN_NUMERO LIKE '"+numéroPlan+@"'
						OR PLN_NUMERO LIKE '"+numéroPlan.Replace( " ", "" )+"'" );
			
			if( reader.Read() )
			{
				Plan plan = new Plan();
				
				Int32.TryParse( reader[ "PLN_ID" ]+"", out plan.pln_ID );
				
				plan.pln_Numéro = reader[ "PLN_NUMERO" ]+"";
				plan.pln_Indice = reader[ "PLN_INDICE" ]+"";
				
				reader.Close();
				return plan;
			}
			
			reader.Close();
			return null;        	
		}
		
	}
}

// AutoResetEvent resetEvent = new AutoResetEvent(false);			
//			label1.Text += " - Evt Terminé"; 
//	
//			if( e.Cancelled == true )
//				label1.Text += " - Cancelled"; 
//			
//			if( e.Error != null )
//				label1.Text += " - Error : "+ e.Error.Message; 
//			
//			if( e.UserState !=  null )
//				label1.Text += " - UserState : "+ e.UserState.ToString();