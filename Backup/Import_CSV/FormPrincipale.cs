using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

// HACK:   Tags
// TODO:   Spéciaux
// UNDONE: Visual
// FIXME:  Studio

namespace Import_Export_Universel
{
	public partial class FormPrincipale : Form
	{
		#region Static
		private static FormPrincipale instanceFormPrincipale = null;

		public static FormPrincipale Instance
		{
			get
			{
				if (null == instanceFormPrincipale)
					instanceFormPrincipale = new FormPrincipale();

				return instanceFormPrincipale;
			}
		}

		#endregion
		#region Variables

        private TextBox[] tabTextBoxSQL = new TextBox[3];

        private ComboBox[] tabComboBoxModèlesPrédéfinis = new ComboBox[3];

		private DataGridView[] tabGridView = new DataGridView[8];

		public DataGridView GridViewCourant
		{
			get
			{
				return tabGridView[ tabTampons.SelectedIndex ];
			}
		}

		public DataTable TamponCourant
		{
			get
			{
				DataTable selectedDataTable = null ;

				try
				{
					selectedDataTable = (DataTable) GridViewCourant.DataSource;
				}
				catch (Exception) { }

				return selectedDataTable;
			}
			set
			{
				GridViewCourant.DataSource = value;

				/*
				if (value != null)
				{
					//GridViewCourant.SortCompare += new DataGridViewSortCompareEventHandler(dgvTampon_SortCompare);
					foreach (DataGridViewColumn col in GridViewCourant.Columns)
					{
						
						col.SortMode = DataGridViewColumnSortMode.Programmatic;
						//col.
					}
				}//*/
			}
		}

        private TextBox EditeurSQLCourant
        {
            get
            {
                int selectedIndex = tabListeTablesEditeurSQL.SelectedIndex - 1;

                if (selectedIndex < 0)
                    return tabTextBoxSQL[0];
                else
                    return tabTextBoxSQL[selectedIndex];
            }
        }

		#endregion
		#region Constructeurs
	
		private FormPrincipale()
		{
			InitializeComponent();
		}

		~FormPrincipale()
		{
		}

		#endregion
		#region Méthodes Initialisation

		private void Connexion()
		{
			btConnexion.Enabled = false;

			//BindingSource source = new BindingSource();
			//source.
			//SqlDataAdapter adapter = new SqlDataAdapter();
			//SqlCommandBuilder builder = new SqlCommandBuilder();

			try
			{
				ConnexionBD.Type typeConnexion = ConnexionBD.Types[cbTypeBase.SelectedIndex];
				ConnexionBD.Options optionsConnexion =
					new ConnexionBD.Options(
						cbAuthentificationIntegree.Checked,
						cbConnexionODBC.Checked
					);

				GestionnaireBD.Instance.OpenConnexion(cbBase.Text, typeConnexion, optionsConnexion, tbConnectionString.Text);

				List<string> listeDesStringsTables = GestionnaireBD.Connexion.ListeDesTables();
				List<string> listeDesStringsVues = GestionnaireBD.Connexion.ListeDesVues();

				foreach (string strNomTable in listeDesStringsTables)
				{
					InfoTable info = new InfoTable(strNomTable, false);
					lbListeTablesVues.Items.Add(info);
				}

				foreach (string strNomVue in listeDesStringsVues)
				{
					InfoTable info = new InfoTable(strNomVue, true);
					lbListeTablesVues.Items.Add(info);
				}

				lbNbTables.Text = listeDesStringsTables.Count + " tables, " + listeDesStringsVues.Count + " vues";

				/*AfficherListeDesElementsBD(
					true,
					true,
					cbMinLignes.Checked,
					(int)nuMinLignes.Value,
					cbMaxLignes.Checked,
					(int)nuMaxLignes.Value,
					cbTablesAvecErreur.Checked);///*/
				if( lbListeTablesVues.Items.Count > 0 )
					lbListeTablesVues.SetSelected(0, true);
				lbListeTablesVues.Focus();

				// Activation des boutons de l'interface
				btConnexion.Enabled = false;
				btDeconnexion.Enabled = true;

				labelHost.Enabled = false;
				labelLogin.Enabled = false;
				labelPass.Enabled = false;
				labelBase.Enabled = false;

				tbHost.Enabled = false;
				tbLogin.Enabled = false;
				tbPass.Enabled = false;
				cbBase.Enabled = false;

				cbConfig.Enabled = false;
				cbTypeBase.Enabled = false;
				cbAuthentificationIntegree.Enabled = false;
				cbConnexionODBC.Enabled = false;

				gbTables.Enabled = true;
				gbBD.Enabled = true;
				btExecuterRequete1.Enabled = true;

				btExtraireTouteLaBD.Enabled = true;
				btImporterTouteLaBD.Enabled = true;

				cbMinLignes.Checked = false;
				cbMaxLignes.Checked = false;
				nuMinLignes.Enabled = false;
				nuMaxLignes.Enabled = false;
				cbTablesAvecErreur.Checked = false;
				
				this.Text = cbBase.Text + " (" + tbHost.Text + ") - Import Export Flash Générique - v" + Application.ProductVersion.Substring(0, 3);
				this.gbTables.Text = "Liste des tables et vues de " + cbBase.Text;
				//this.gbBD.Text = "Base de données " + cbBase.Text;

				persisterFormAssociation = false;
				persisterFormImportDeMasse = false;
				btListerBasesDeDonnées.Enabled = false;

				tabCommandes.SelectedIndex = 2;
			}
			catch (Exception exception)
			{
				// Gestion des erreurs
				Deconnexion();

				MessageBox.Show(
					exception.Message,
					"Erreur de connexion",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
			}
		}

		private void Deconnexion()
		{
			this.Text = "Déconnecté - Import Export Flash Générique - v" + Application.ProductVersion.Substring(0, 3);

			this.btListerBasesDeDonnées.Enabled = (
				this.tbHost.TextLength > 0 &&
				this.tbLogin.TextLength > 0 &&
				this.tbPass.TextLength > 0);
			
			btConnexion.Enabled = true;
			btDeconnexion.Enabled = false;

			labelHost.Enabled = true;
			labelLogin.Enabled = true;
			labelPass.Enabled = true;
			labelBase.Enabled = true;

			tbHost.Enabled = true;
			tbLogin.Enabled = true;
			tbPass.Enabled = true;
			cbBase.Enabled = true;

			cbConfig.Enabled = true;
			cbTypeBase.Enabled = true;
			cbAuthentificationIntegree.Enabled = true;
			cbConnexionODBC.Enabled = true;

			gbTables.Enabled = false;
			gbBD.Enabled = false;
			btExecuterRequete1.Enabled = false;

			btExtraireTouteLaBD.Enabled = false;
			btImporterTouteLaBD.Enabled = false;

			lbListeTablesVues.DataSource = null;
			lbListeTablesVues.Items.Clear();
			dgvColonnes.DataSource = null;

			GestionnaireBD.Instance.CloseConnexion();

			persisterFormAssociation = false;
		}

		private void ListerBasesDeDonnées()
		{
			ConnexionBD tempCnx = null;
			this.btListerBasesDeDonnées.Enabled = false;

			try
			{
				cbBase.Items.Clear();

				if (GestionnaireBD.Connexion == null)
				{
					ConnexionBD.Type typeConnexion = ConnexionBD.Types[cbTypeBase.SelectedIndex];
					ConnexionBD.Options optionsConnexion =
						new ConnexionBD.Options(
							cbAuthentificationIntegree.Checked,
							cbConnexionODBC.Checked
						);

					if (typeConnexion.intValue == ConnexionBD.SQLServer.intValue)
					{
						cbBase.Text = "master";
						tempCnx = new ConnexionSQLServer("Temporaire", optionsConnexion, tbConnectionString.Text);
					}
					else throw new Exception("Type de base non implémenté");
				}
				else tempCnx = GestionnaireBD.Connexion;

				List<string> listeDesStrings = tempCnx.ListeDesBases();

				if (listeDesStrings != null)
				{
					foreach (string strNomBase in listeDesStrings)
						cbBase.Items.Add(strNomBase);

					cbBase.SelectedIndex = 0;
				}
				else throw new Exception("Impossible d'obtenir la liste des bases");

				if (GestionnaireBD.Connexion != tempCnx)
					tempCnx.Close();
			}
			catch (Exception exception)
			{
				MessageBox.Show(
					exception.Message,
					"Erreur de connexion",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
				if (tempCnx != null)
					tempCnx.Close();
			}
			this.btListerBasesDeDonnées.Enabled = true;
			this.cbBase.DroppedDown = true;
			//this.cbBase.
		}

		private void AfficherListeDesElementsBD(bool afficherListeDesTables, bool afficherListeDesVues, bool filtrerMinLignes, int minLignes, bool filtrerMaxLignes, int maxLignes, bool affErreurs)
		{
			bool estVue = true;
			List<string> liste;
			lbListeTablesVues.Items.Clear();
			lbNbTables.Text = "";

			for ( int passe = 0 ; passe < 2; passe ++ )
			{
				liste = new List<string>();

				if (afficherListeDesVues && passe == 0 )
				{
					estVue = true;
					List<string> lstVues = GestionnaireBD.Connexion.ListeDesVues();
					liste = lstVues;
					lbNbTables.Text = lstVues.Count + " vues ";
				}

				if (afficherListeDesTables && passe == 1 )
				{
					estVue = false;
					List<string> lstTables = GestionnaireBD.Connexion.ListeDesTables();
					liste = lstTables;

					if (lbNbTables.Text.Length > 0)
						lbNbTables.Text = lstTables.Count + " tables, " + lbNbTables.Text;
					else
						lbNbTables.Text = lstTables.Count + " tables";
				}

				foreach (string strNomTable in liste)
				{
					InfoTable info;

					try
					{
						info = new InfoTable(
							strNomTable,
							estVue,
							GestionnaireBD.Connexion.NombreDeLignes(strNomTable),
							null
							);
					}
					catch (Exception ex)
					{
						info = new InfoTable(
							strNomTable,
							estVue,
							-1,
							ex
							);
					}

					// Si Erreur, afficher juste les tables en erreur
					if (affErreurs)
					{
						if (info.Erreur!=null)
							lbListeTablesVues.Items.Add(info);
					}
					else if (!(info.Erreur!=null && !affErreurs)
					 && !(info.Count < minLignes && filtrerMinLignes)
					 && !(info.Count > maxLignes && filtrerMaxLignes))
					{
						lbListeTablesVues.Items.Add(info);
					}
				}
			}

			if( lbListeTablesVues.Items.Count > 0 )
				lbListeTablesVues.SetSelected(0, true);

			lbNbTables.Text = lbListeTablesVues.Items.Count + " éléments sur " + lbNbTables.Text;
		}

		private void OuvrirTable()
		{
			this.splitContainerPrincipal.Enabled = false;
			this.Cursor = Cursors.No;

			try
			{
				// Nom de la table
				InfoTable infoTable = ( lbListeTablesVues.SelectedItem as InfoTable );
				string strTable = infoTable.NomTable;
				infoTable.Count = GestionnaireBD.Connexion.NombreDeLignes( strTable );
				
				// ou bien : lstBox.Items[lstBox.SelectedIndex] = lstBox.SelectedItem;
//				typeof(ListBox).InvokeMember(
//					"RefreshItems",
//  					BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
//  					null, lbListeTablesVues, new object[] { });
				// lbListeTablesVues.Invalidate();
				// lbListeTablesVues.Refresh();
				
				DataTable dataTable =( cbConvertirBinairesEnAsciiHexa.Checked )?
					GestionnaireBD.Instance.OuvrirTableEtConvertirChampsBinaires(strTable) :
					GestionnaireBD.Instance.OuvrirTableStandard( strTable );

				FusionnerTamponCourant(dataTable, "Fusion avec "+strTable, strTable);
				ActualiserAffichageSelonTampon();
				
				//MessageBox.Show( infoTable.ToString() );
			}
			catch (Exception exception)
			{
				// Gestion des erreurs
				MessageBox.Show(
					exception.Message,
					"Erreur d'ouverture de table",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
			}

			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		#endregion
		#region Fichiers CSV XLS
		private void OuvrirFichierCSV_XLS()
		{
			try
			{
				// Ouverture du fichier
				openCsvXlsFileDialog.InitialDirectory = tbRepertoire.Text;

				if (DialogResult.OK == openCsvXlsFileDialog.ShowDialog())
				{
					tbNomFichierExport.Text = openCsvXlsFileDialog.SafeFileName;
					tbRepertoire.Text = openCsvXlsFileDialog.FileName.Replace(openCsvXlsFileDialog.SafeFileName, "");
					
					// Fichier Excel ?
					if (openCsvXlsFileDialog.FileName.LastIndexOf(".xls") + 5 >=
						openCsvXlsFileDialog.FileName.Length)
					{
						FormInput2 input2 = new FormInput2(
							"Seule une feuille peut être ouverte" ,
							"Choisissez la feuille à ouvrir",
							GestionnaireExcel.ListeDesFeuillesAvecDétails(openCsvXlsFileDialog.FileName)
							);

						if (DialogResult.OK == input2.ShowDialog())
						{
							this.splitContainerPrincipal.Enabled = false;
							this.Cursor = Cursors.No;
							ItemDeListe item = (ItemDeListe) input2.ItemSélectionné;

							GestionnaireExcel.OuvrirFeuilleXLS(
								openCsvXlsFileDialog.FileName,
								item.ObjetSource.ToString(),
								EvénementOuvertureXLSTerminée
								);
						}
						
						//ExporterTamponDansExcel(
						//	TamponCourant,
						//	openCsvXlsFileDialog.FileName
						//);
					}
					else
					{
						StreamReader srFichier = new StreamReader(openCsvXlsFileDialog.OpenFile(),System.Text.Encoding.Default);

						//ChargerFichierCSVdansTampon(srFichier);
						GestionnaireCSV.CaractèreSéparateur = tbCaractèreSéparateur.Text;
						GestionnaireCSV.ControleNbColonnes = cbControlerNbColonnes.Checked;
						GestionnaireCSV.TrimSpaces = cbTrimSpaces.Checked;

						DataTable dtFichier = GestionnaireCSV.OuvrirFichierCSV(srFichier);
						srFichier.Close();

						FusionnerTamponCourant(
							dtFichier,
							"Fusion avec " + srFichier.ToString(),
							srFichier.ToString());

						tabTampons.SelectedTab.Text = openCsvXlsFileDialog.SafeFileName;
						ActualiserAffichageSelonTampon();
					}
				}
			}
			catch (Exception exception)
			{

				// Gestion des erreurs
				MessageBox.Show(
					exception.Message,
					"Erreur de chargement fichier",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
			}

			//this.splitContainerPrincipal.Enabled = true;
			//this.Cursor = Cursors.Default;
		}

		private void EnregistrerFichier()
		{
			// Ne devrait pas arriver
			if (TamponCourant == null ||
				TamponCourant.Columns.Count < 1) //TamponCourant.Rows.Count < 1)
			{
				TamponCourant = null;
				ActualiserAffichageSelonTampon();
				return;
			}

			saveCsvXlsFileDialog.FileName = tbNomFichierExport.Text;
			saveCsvXlsFileDialog.InitialDirectory = tbRepertoire.Text;

			if (DialogResult.OK == saveCsvXlsFileDialog.ShowDialog())
			{
				this.splitContainerPrincipal.Enabled = false;
				this.Cursor = Cursors.No;
				
				//MessageBox.Show( saveCsvXlsFileDialog.Filter + "-"  + saveCsvXlsFileDialog.FilterIndex );
				
				string strFichier = saveCsvXlsFileDialog.FileName;

				// Fichier Excel ?
				if (strFichier.LastIndexOf(".xls") + 5 >=
					strFichier.Length)
				{
					GestionnaireExcel.ExporterTamponDansExcel(
						TamponCourant,
						strFichier,
						new RunWorkerCompletedEventHandler(EvénementTâcheDArrièrePlanTerminée)
					);
				}
				else
				{
					try
					{
						GestionnaireCSV.CaractèreSéparateur = tbCaractèreSéparateur.Text;
						GestionnaireCSV.ControleNbColonnes = cbControlerNbColonnes.Checked;
						GestionnaireCSV.TrimSpaces = cbTrimSpaces.Checked;

						GestionnaireCSV.ExporterTamponDansCSV(
							TamponCourant,
							strFichier
						);

						Program.MsgInformation(
							"Résultat exporté dans\n" + strFichier,
							"Opération terminée" );
//						MessageBox.Show(
//							"Résultat exporté dans\n" + strFichier,
//							"Opération terminée",
//							MessageBoxButtons.OK,
//							MessageBoxIcon.Information
//						 );
					}
					catch (Exception exception)
					{
						// Gestion des erreurs
						MessageBox.Show(
							exception.Message,
							"Erreur d'enregistrement fichier",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error
						 );
					}
					this.splitContainerPrincipal.Enabled = true;
					this.Cursor = Cursors.Default;
				}
			}

			//if (tbNomFichierExport.Text.Length == 0)
			//TaskDialog   MessageBox.Show("Il me faut un nom pour le fichier", "Divination impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/*private void ChargerFichierCSVdansTampon(StreamReader srFichier)
		{
			this.splitContainerPrincipal.Enabled = false;
			this.Cursor = Cursors.No;

			try
			{
				DataTable dtFichier = GestionnaireCSV.OuvrirFichierCSV(
					srFichier,
					tbCaractèreSéparateur.Text,
					cbTrimSpaces.Checked,
					cbControlerNbColonnes.Checked
					);
				srFichier.Close();

				FusionnerTamponCourant(dtFichier, "Fusion avec "+srFichier.ToString(), srFichier.ToString());

				this.splitContainerPrincipal.Enabled = true;
				this.Cursor = Cursors.Default;
			}
			catch (Exception ex)
			{
				this.splitContainerPrincipal.Enabled = true;
				this.Cursor = Cursors.Default;

				srFichier.Close();
				throw ex;
			}
		}//*/

		private void OuvrirFichierSQL()
		{
			try
			{
				// Ouverture du fichier
				openSqlFileDialog.InitialDirectory = tbRepertoire.Text.ToString();
				if (DialogResult.OK == openSqlFileDialog.ShowDialog())
				{
					StreamReader srFichier = new StreamReader(openSqlFileDialog.OpenFile(), System.Text.Encoding.Default);
					string strChaineLue = null;

					// Chargement du fichier
					while ((strChaineLue = srFichier.ReadLine()) != null)
					{
                        EditeurSQLCourant.AppendText(strChaineLue);
					}

					srFichier.Close();
				}
			}
			catch (Exception exception)
			{
				// Gestion des erreurs
				MessageBox.Show(
					exception.Message,
					"Erreur de chargement fichier",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
			}
		}

		#endregion
		#region Gestion du Tampon

		private bool ChoisirNouvelOngletVide()
		{
			int i = 0;
			bool trouvéNouveau = false;

			// Chercher un tampon vide
			while (!trouvéNouveau && tabTampons.SelectedIndex + i < tabTampons.TabCount)
			{
				if (tabGridView[tabTampons.SelectedIndex + i].DataSource == null)
				{
					trouvéNouveau = true;
					tabTampons.SelectTab(tabTampons.SelectedIndex + i);
				}
				else
					i++;
			}

			// Nouvelle recherche à partir du premier
			if (!trouvéNouveau)
			{
				i = 0;
				while (!trouvéNouveau && i < tabTampons.SelectedIndex)
				{
					if (tabGridView[i].DataSource == null)
					{
						trouvéNouveau = true;
						tabTampons.SelectTab(i);
					}
					else
						i++;
				}
			}

			return trouvéNouveau;
		}

		private void ActualiserAffichageSelonTampon()
		{
			// Le tampon actuel est vide ou plein ?
			if (TamponCourant == null)
			{
				btInsertTable.Enabled = false;
				btEnregistrerFichier.Enabled = false;
				btPurger.Enabled = false;
				btRéindexer.Enabled = false;
				btChercher.Enabled = false;
				btDupliquer.Enabled = false;
				btSupprLigne.Enabled = false;
				btSupprColonne.Enabled = false;
				btToutPurger.Enabled = false;
				btFusionnerTampons.Enabled = false;

				lbInformation.Text = "Tampon vide";
			}
			else
			{
				btInsertTable.Enabled = true;
				btEnregistrerFichier.Enabled = true;
				btPurger.Enabled = true;
				btRéindexer.Enabled = true;
				btChercher.Enabled = true;
				btDupliquer.Enabled = true;
				btSupprLigne.Enabled = true;
				btSupprColonne.Enabled = true;
				btToutPurger.Enabled = true;
				btFusionnerTampons.Enabled = true;

				lbInformation.Text = TamponCourant.Rows.Count + " ligne(s), " + TamponCourant.Columns.Count + " colonne(s)";
			}

			btInsertionsSuccès.Enabled = (GestionnaireBD.Instance.InsertionsSuccès != null) ;
			btInsertionsEchecs.Enabled = (GestionnaireBD.Instance.InsertionsEchecs != null) ;
							
			// ou bien : lstBox.Items[lstBox.SelectedIndex] = lstBox.SelectedItem;
			typeof(ListBox).InvokeMember(
				"RefreshItems",
					BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
					null, lbListeTablesVues, new object[] { });
			// lbListeTablesVues.Invalidate();
			// lbListeTablesVues.Refresh();
		}

		private void PurgerTamponCourant()
		{
			TamponCourant = null;
			tabTampons.SelectedTab.Text = "Tampon Vide";
			ActualiserAffichageSelonTampon();
		}

		private void PurgerTousTampons()
		{
			int i = 0;

			foreach (DataGridView dgv in tabGridView)
				dgv.DataSource = null;

			foreach (TabPage tab in tabTampons.TabPages)
				tab.Text = "Tampon "+(++i);

			GestionnaireBD.Instance.ViderTampons();
			tabTampons.SelectedIndex = 0;
			ActualiserAffichageSelonTampon();
		}

		private void FusionnerTousTampons()
		{
			this.splitContainerPrincipal.Enabled = false;
			this.Cursor = Cursors.No;
			try
			{

				GestionnaireBD.Instance.ViderTampons();

				DataTable dest = new DataTable();

				for (int i = 0; i < tabGridView.Length; i++)
				{
					DataTable orig = (DataTable)tabGridView[i].DataSource;

					if (orig != null)
						dest.Merge(orig);
				}

				PurgerTousTampons();
				tabGridView[0].DataSource = dest;
				tabTampons.SelectedIndex = 0;
				tabTampons.SelectedTab.Text = "Tampons fusionnés";

				ActualiserAffichageSelonTampon();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
						ex.Message,
						"Erreur de fusion de tampon",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					 );
				//PurgerTousTampons();
			}
			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		private void FusionnerTamponCourant(DataTable newDataTable, string strTitreSiFusion, string strTitreSiNouveau)
		{
			try
			{
				//if (this.rbConcaténerTampon.Checked)
				if (TamponCourant == null || this.rbNouveauTampon.Checked)
				{
					TamponCourant = newDataTable;
					tabTampons.SelectedTab.Text = strTitreSiNouveau;
				}
				else
				{
					TamponCourant.Merge(newDataTable);
					tabTampons.SelectedTab.Text = strTitreSiFusion;
				}

				for (int i = 0; i < TamponCourant.Columns.Count; i++)
				{
					DataGridViewColumn gvcol = GridViewCourant.Columns[i];
					DataColumn dtcol = TamponCourant.Columns[i];

					gvcol.HeaderText = dtcol.ColumnName + " [" + dtcol.DataType.ToString().Replace("System.", "") + "] ";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
						ex.Message,
						"Erreur de fusion de tampon",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					 );
				//PurgerTamponCourant();
			}
		}

		#endregion
		#region Requêtes SQL

		//private void EnregistrerTamponDansTableBD( AssociationChampsBDTampon association )//NameValueCollection associationChampsBdTampon, List<string> lstChampsClésPrimaires, bool insertsOK, bool updatesOK, string strConditionsAdditionnelles)
		//{
		//	this.splitContainerPrincipal.Enabled = false;
		//	this.Cursor = Cursors.No;

			
			//btInsertTable.Enabled = false;
			/*int compteurASucces = 0;
			
			this.dtInsertionsSuccès = new DataTable();
			this.dtInsertionsEchecs = new DataTable();

			foreach (DataColumn col in TamponCourant.Columns)
			{
				DataColumn newCol1 = new DataColumn(col.ColumnName);
				DataColumn newCol2 = new DataColumn(col.ColumnName);
				this.dtInsertionsSuccès.Columns.Add(newCol1);
				this.dtInsertionsEchecs.Columns.Add(newCol2);
			}

			try
			{
				string strTable = ((InfoTable)lbListeTables.SelectedItem).NomTable;
				GestionnaireBD.Connexion.DeverrouillerTable(strTable);
				int index = 0;

				// Traitement ligne par ligne
				foreach (DataRow ligne in TamponCourant.Rows)
				{
					// Dictionnaire des clés primaires
					NameValueCollection dictClésPrimaires = new NameValueCollection();

					foreach (string champBD in lstChampsClésPrimaires)
					{
						string champTampon = associationChampsBdTampon[champBD];
						string donnéeTampon = ligne[champTampon].ToString();

						dictClésPrimaires.Add(champBD, donnéeTampon);
					}

					bool donnéesExistent = GestionnaireBD.Connexion.ClésExistent(strTable, dictClésPrimaires);


					// Dictionnaire des valeurs
					NameValueCollection dictDonnées = new NameValueCollection();

					foreach (string champBD in associationChampsBdTampon.Keys)
					{
						//MessageBox.Show("Ajout " + champBD );

						string champTampon = associationChampsBdTampon[champBD].ToString();
						string donnéeTampon = ligne[champTampon].ToString();

						//MessageBox.Show(champBD + " = " + champTampon + " -> " + donnéeTampon);

						dictDonnées.Add(champBD, donnéeTampon);
					}

					// Les données n'existent pas et on peut insérer --> INSERT
					// Les données existent et on peut mettre à jour --> UPDATE
					try
					{
						GridViewCourant.Rows[index].DefaultCellStyle.BackColor = Color.LightGray;

						if (!donnéesExistent && insertsOK)
						{
							if (0 != GestionnaireBD.Connexion.Insérer(strTable, dictDonnées))
							{
								compteurASucces++;

								GridViewCourant.Rows[index].DefaultCellStyle.BackColor = Color.LawnGreen;

								this.dtInsertionsSuccès.Rows.Add(ligne.ItemArray);
							}
							//else tabGridView[ tabTampons.SelectedIndex ].Rows[index].DefaultCellStyle.BackColor = Color.Pink;
						}
						else if (donnéesExistent && updatesOK)
						{
							if (0 != GestionnaireBD.Connexion.MettreAJour(strTable, dictDonnées, dictClésPrimaires, strConditionsAdditionnelles))
							{
								compteurASucces++;
								///dgvTampon1.Rows[ligne].DefaultCellStyle.BackColor = Color.Green;
								GridViewCourant.Rows[index].DefaultCellStyle.BackColor = Color.LawnGreen;


								this.dtInsertionsSuccès.Rows.Add(ligne.ItemArray);
							}
							//else tabGridView[ tabTampons.SelectedIndex ].Rows[index].DefaultCellStyle.BackColor = Color.Peru;
						}
					}
					catch (Exception exception)
					{
						///dgvTampon1.Rows[ligne].DefaultCellStyle.BackColor = Color.Red;
						GridViewCourant.Rows[index].DefaultCellStyle.BackColor = Color.Red;

						this.dtInsertionsEchecs.Rows.Add(ligne.ItemArray);

						if (cbErreursBloquantes.Checked)
						{
							if (DialogResult.No == MessageBox.Show(
									exception.Message + "\n\nContinuer ?",
									"Erreur de la requête SQL",
									MessageBoxButtons.YesNo,
									MessageBoxIcon.Warning
								))
							{
								//ligne = TamponCourant.Rows.Count;*
								break;
							}
						}
						else
							GestionnaireDesTâches.ajouterException(exception);
					}
					index++;
				}

				GestionnaireBD.Connexion.ReverrouillerTable(strTable);

				//if( peutInterrompre )
				MessageBox.Show(
					compteurASucces + " lignes insérées dans la table " + strTable,
					"Opération terminée",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information
				 );

			}
			catch (Exception exception)
			{
				// Gestion des erreurs
				MessageBox.Show(
					exception.Message,
					"Erreur pendant copie BD",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
			}

			if (this.dtInsertionsSuccès.Rows.Count == 0) this.dtInsertionsSuccès = null;
			if (this.dtInsertionsEchecs.Rows.Count == 0) this.dtInsertionsEchecs = null;

			//btInsertTable.Enabled = true;
			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;//*/
		//}

		FormImportDeMasse frmImportDeMasse = null;
		bool persisterFormImportDeMasse = false;

		private void ImporterTouteLaBD()
		{
			try
			{
				GestionnaireCSV.Répertoire = tbRepertoire.Text;

				if (null == frmImportDeMasse || false == persisterFormImportDeMasse)
					frmImportDeMasse = new FormImportDeMasse();

				DialogResult resultat = frmImportDeMasse.ShowDialog();

				persisterFormImportDeMasse = true;

				if (DialogResult.OK == resultat)
				{
					this.splitContainerPrincipal.Enabled = false;
					this.Cursor = Cursors.No;

					GestionnaireCSV.Répertoire = tbRepertoire.Text;
					GestionnaireCSV.CaractèreSéparateur = tbCaractèreSéparateur.Text;
					GestionnaireCSV.ControleNbColonnes = cbControlerNbColonnes.Checked;
					GestionnaireCSV.TrimSpaces = cbTrimSpaces.Checked;

					GestionnaireBD.Instance.ImporterTouteLaBD(
						frmImportDeMasse.Association,
						new RunWorkerCompletedEventHandler(EvénementImportDeMasseTerminé)
						);
				}
				//else
				//{
				//	MessageBox.Show("Opération annulée", "Opération annulée", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
						"Erreur : " + ex.Message,
						"Impossible d'enregistrer",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
						);
			}
		}
			//FormVeuillezPatienter message = new FormVeuillezPatienter();
			//message.Activate();
			//message.Show();
			//this.splitContainerPrincipal.Enabled = false;
			//this.Cursor = Cursors.No;

			//GestionnaireCSV.Répertoire = tbRepertoire.Text;
			//GestionnaireBD.Instance.ImporterTouteLaBD(
			//	bDeleteTable,
			//	new RunWorkerCompletedEventHandler(EvénementTâcheDArrièrePlanTerminée)
			//	);

			//MessageBox.Show("Non disponible");
			/*
			DirectoryInfo di = new DirectoryInfo(tbRepertoire.Text);
			FileInfo[] listeDesFichiersDuRepertoire = di.GetFiles("*.csv");

			foreach (FileInfo fichier in listeDesFichiersDuRepertoire)
			{
				Application.DoEvents();

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
						GestionnaireBD.Connexion.ExecuteNonQuery(
							"DELETE FROM " +
							((InfoTable)lbListeTables.SelectedItem).NomTable
						);
					throw new NotImplementedException("Fonction Pas Faite");
					// InsérerTamponDansTableBD();
				}
				catch (Exception exception)
				{
					MessageBox.Show(
						exception.Message,
						"Erreur lors de l'import du fichier " + fichier.Name,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					 );
				}
			}

			PurgerTamponCourant();
			this.Visible = true;
			message.Visible = false;
			message.Dispose();
		}//*/

		private void ExporterTouteLaBD()
		{
			this.splitContainerPrincipal.Enabled = false;
			this.Cursor = Cursors.No;
			
			

			GestionnaireCSV.Répertoire = tbRepertoire.Text;
			GestionnaireCSV.CaractèreSéparateur = tbCaractèreSéparateur.Text;
			GestionnaireCSV.ControleNbColonnes = cbControlerNbColonnes.Checked;
			GestionnaireCSV.TrimSpaces = cbTrimSpaces.Checked;

			GestionnaireBD.Instance.ExporterTouteLaBD(
				//lbListeTables.Items,
				new RunWorkerCompletedEventHandler( EvénementTâcheDArrièrePlanTerminée )
				);
		}
			/*
			foreach (InfoTable infoTable in lbListeTables.Items)
			{
				try
				{
					// Création du fichier de sortie
					string strFichier =
						tbRepertoire.Text +
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

					List<string> listeDesColonnes = this.connexionBD.ListeDesColonnes(infoTable.NomTable);

					// sqlReader.Read();
					string strEntetes = listeDesColonnes[0].ToString();

					for (int i = 1; i < listeDesColonnes.Count; i++)
						strEntetes += tbCaractèreSéparateur.Text + listeDesColonnes[i];

					// sqlReader.Close();

					// Liste des données de la table
					IDataReader reader = this.connexionBD.ExecuteReader("SELECT * FROM " + infoTable.NomTable);

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
							else strData += tbCaractèreSéparateur.Text;

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

								try
								{
									unicodeString = System.Text.Encoding.Unicode.GetString(objetBinaire);
								}
								catch( Exception ) {}


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

					MessageBox.Show(
						"Résultat exporté dans\n" + strFichier,
						"Opération terminée",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information
					 );
				}
				catch (Exception exception)
				{
					// Gestion des erreurs
					if (DialogResult.No == MessageBox.Show(
						exception.Message + "\nContinuer ?",
						"Erreur d'extraction",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Warning
					 ))
						break;
				}
			}
			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;//*/

		private void ExecuterRequeteSQL()
        {
            btExecuterRequete1.Enabled = false;
            btExecuterRequete2.Enabled = false;
            btExecuterRequete3.Enabled = false;

            try
            {
                // Initialisations
                DataTable dtRequeteManuelle = new DataTable("RequeteManuelle");

                // Exécution de la requête
                //SqlCommand sqlCommand = new SqlCommand(tbRequete.Text, sqlConnexion);
                //SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                IDataReader reader = GestionnaireBD.Connexion.ExecuteReader(EditeurSQLCourant.Text); //tbRequete1.Text);

                //if (reader.HasRows)
                if (reader.FieldCount > 0)
                {
                    DataTable dtSchema = reader.GetSchemaTable();


                    for (int i = 0; i < dtSchema.Rows.Count; i++)
                        dtRequeteManuelle.Columns.Add(dtSchema.Rows[i][0].ToString());


                    if (reader.Read())
                    {
                        DataRow row = dtRequeteManuelle.NewRow();


                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            //dtRequeteManuelle.Columns.Add(new DataColumn("Col " + i));
                            //dtRequeteManuelle.Columns.Add(sqlReader.GetSchemaTable().Columns[i].ToString());
                            row[i] = reader[i].ToString();
                        }

                        dtRequeteManuelle.Rows.Add(row);

                        // Chargement des données de la table
                        while (reader.Read())
                        {
                            row = dtRequeteManuelle.NewRow();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
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


                                    row[i] = "<OBJ> Ascii:{" + asciiString + "} Hexa:{" + hexaString + "}";
                                }
                                else row[i] = reader[i].ToString();
                            }

                            dtRequeteManuelle.Rows.Add(row);
                        }
                    }

                    FusionnerTamponCourant(dtRequeteManuelle, "Ajout de données SQL", "Requête SQL");
                    tabTampons.SelectedTab.Text = "SQL";
                }
                else MessageBox.Show(
                    reader.RecordsAffected + " lignes affectées",
                    "Opération effectuée",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                 );

                // Fermeture
                reader.Close();
                
                
                if( lbListeTablesVues.SelectedItem != null )
                	( lbListeTablesVues.SelectedItem as InfoTable ).Count =
						GestionnaireBD.Connexion.NombreDeLignes(
                			( lbListeTablesVues.SelectedItem as InfoTable ).NomTable
                		);
                ActualiserAffichageSelonTampon();
            }
            catch (Exception exception)
            {
                // Gestion des erreurs
                // Deconnexion();

                MessageBox.Show(
                    exception.Message,
                    "Erreur de requête SQL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                 );
            }
            finally
            {
                btExecuterRequete1.Enabled = true;
                btExecuterRequete2.Enabled = true;
                btExecuterRequete3.Enabled = true;
            }
		}

		private void UpdateConnexionString()
		{
			if (cbTypeBase.SelectedIndex >= 0)
			{
				ConnexionBD.Type typeConnexion = ConnexionBD.Types[cbTypeBase.SelectedIndex];
				ConnexionBD.Options optionsConnexion = new ConnexionBD.Options(cbAuthentificationIntegree.Checked, cbConnexionODBC.Checked);
				tbConnectionString.Text = ConnexionBD.CreerChaineConnexion(typeConnexion, optionsConnexion, tbHost.Text, cbBase.Text, tbLogin.Text, tbPass.Text);
			}
			else tbConnectionString.Text = "Erreur....";
		}

		#endregion
		#region Evénements

		private void FormImport_Load(object sender, EventArgs e)
		{
			Deconnexion();

			tbRepertoire.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			this.Text = "Import Export Flash Générique - v" + Application.ProductVersion.Substring(0, 3);
			this.toolTip.ToolTipTitle = "Import Export Flash Générique - v"+ Application.ProductVersion;

			ConfigurationConnexion.ChargerListeConfig();

			if (ConfigurationConnexion.ExisteFichierConfig)
				lbEtatConfigurations.Text = "Les configurations sont stockées dans le fichier config.xml";
				//btEnregistrerConfig.Enabled =
			else
				lbEtatConfigurations.Text = "Aucun fichier de configuration trouvé";

			cbConfig.Items.AddRange(ConfigurationConnexion.listeDesConfigs.ToArray());
			cbConfig.SelectedIndex = 0;

			cbTypeBase.Items.Add(ConnexionBD.Indefini.strValue);
			cbTypeBase.Items.Add(ConnexionBD.Informix.strValue);
			cbTypeBase.Items.Add(ConnexionBD.SQLServer.strValue);
			cbTypeBase.SelectedIndex = 2;

            tabTextBoxSQL[0] = tbRequete1;
            tabTextBoxSQL[1] = tbRequete2;
            tabTextBoxSQL[2] = tbRequete3;
            tabComboBoxModèlesPrédéfinis[0] = cbModèlePrédéfini1;
            tabComboBoxModèlesPrédéfinis[1] = cbModèlePrédéfini2;
            tabComboBoxModèlesPrédéfinis[2] = cbModèlePrédéfini3;

			tabGridView[0] = dgvTampon1;
			tabGridView[1] = dgvTampon2;
			tabGridView[2] = dgvTampon3;
			tabGridView[3] = dgvTampon4;
			tabGridView[4] = dgvTampon5;
			tabGridView[5] = dgvTampon6;
			tabGridView[6] = dgvTampon7;
			tabGridView[7] = dgvTampon8;

			dgvTampon1.DataError += new DataGridViewDataErrorEventHandler(dgvTampon_DataError);
			dgvTampon2.DataError += new DataGridViewDataErrorEventHandler(dgvTampon_DataError);
			dgvTampon3.DataError += new DataGridViewDataErrorEventHandler(dgvTampon_DataError);
			dgvTampon4.DataError += new DataGridViewDataErrorEventHandler(dgvTampon_DataError);
			dgvTampon5.DataError += new DataGridViewDataErrorEventHandler(dgvTampon_DataError);
			dgvTampon6.DataError += new DataGridViewDataErrorEventHandler(dgvTampon_DataError);
			dgvTampon7.DataError += new DataGridViewDataErrorEventHandler(dgvTampon_DataError);
			dgvTampon8.DataError += new DataGridViewDataErrorEventHandler(dgvTampon_DataError);


			btSupprimerConfig.Enabled =( ConfigurationConnexion.ExisteFichierConfig && ConfigurationConnexion.listeDesConfigs.Count > 0 );
			btRéinitConfig.Enabled =( ConfigurationConnexion.ExisteFichierConfig &&  File.Exists(ConfigurationConnexion.NomFichierConfigXML));
			ActualiserAffichageSelonTampon();

			this.WindowState = FormWindowState.Maximized;
		}

		//private bool estDataErrorAffichée = false;
		void dgvTampon_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.Cancel = false;
			e.ThrowException = false;
			string strMessage =
				"Erreur de données dans la cellule (" + e.RowIndex +", " + e.ColumnIndex + ") de la grille :\n";

			if (e.Context == DataGridViewDataErrorContexts.Commit)				  strMessage += "Erreur de commit";
			if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)	   strMessage += "Erreur de modification de cellule";
			if (e.Context == DataGridViewDataErrorContexts.Parsing)				 strMessage += "Erreur de parsing";
			if (e.Context == DataGridViewDataErrorContexts.Display )				strMessage += "Erreur de display";
			if (e.Context == DataGridViewDataErrorContexts.Formatting )			 strMessage += "Erreur de formatting";
			if (e.Context == DataGridViewDataErrorContexts.ClipboardContent)		strMessage += "Données de presse-papier incorrectes";
			if (e.Context == DataGridViewDataErrorContexts.LeaveControl)			strMessage += "Erreur de Leave Control";
			if (e.Context == DataGridViewDataErrorContexts.InitialValueRestoration) strMessage += "Erreur de restauration des données initiales";

			GestionnaireDesTâches.Instance.ModeCourant = GestionnaireDesTâches.Mode.DEBUG_LOG;
			GestionnaireDesTâches.Exception(
				new ImportExportException(
					"Données de la grille",
					strMessage,
					e.Exception
					));
		}

		private void EvénementOuvertureXLSTerminée(object sender, RunWorkerCompletedEventArgs e)
		{
			if( e.Error == null )
			{
				DataTable dataTable = e.Result as DataTable;
				
				if( dataTable != null )
					FusionnerTamponCourant(
						dataTable,
						"Fusion avec " + dataTable.TableName,
						dataTable.TableName);
				//else MessageBox.Show( "datatable null" );
			}
			//else MessageBox.Show( "error non null" );

			EvénementTâcheDArrièrePlanTerminée(sender, e);
		}

		private void EvénementImportDeTableTerminé(object sender, RunWorkerCompletedEventArgs e)
		{
			if( lbListeTablesVues.SelectedItem != null )
				( lbListeTablesVues.SelectedItem as InfoTable ).Count =
							GestionnaireBD.Connexion.NombreDeLignes(
	                			( lbListeTablesVues.SelectedItem as InfoTable ).NomTable
	                		);
		
			EvénementTâcheDArrièrePlanTerminée(sender, e);
		}
		
		private void EvénementImportDeMasseTerminé(object sender, RunWorkerCompletedEventArgs e)
		{
			btFiltrerListeTables_Click( sender, e );
			EvénementTâcheDArrièrePlanTerminée(sender, e);
		}

		private void EvénementTâcheDArrièrePlanTerminée(object sender, RunWorkerCompletedEventArgs e)
		{
			//GestionnaireDesTâches.Message( "EvénementTâcheDArrièrePlanTerminée" );
			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;
			ActualiserAffichageSelonTampon();
			
			string retour = "";
			
			if( e.Cancelled == true )
				retour += "La tâche annulée a rendu la main. ";
			
			if( e.Error != null )
				retour += "La tâche s'est terminée en renvoyant une erreur : "+ e.Error.Message + ". ";
			
			if( e.UserState != null )
				retour += "UserState : "+ e.UserState.ToString() + ". ";
			
			if( retour.Length > 0 )
				GestionnaireDesTâches.Message( retour );
		}

		private void lbListeTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				persisterFormAssociation = false;

				// Nom de la table
				InfoTable info = (InfoTable)lbListeTablesVues.SelectedItem;

				if (info.EstVue) this.gbBD.Text = "Vue " + info.NomTable;
				else this.gbBD.Text = "Table " + info.NomTable;
				
				// Nom du fichier CSV de sortie
				tbNomFichierExport.Text = GestionnaireCSV.GénérerNomFichierExport(info.NomTable);
				dgvColonnes.DataSource = GestionnaireBD.Connexion.InformationsTable(info.NomTable);
				

			}
			catch (Exception exception)
			{
				// Gestion des erreurs
				MessageBox.Show(
					exception.Message,
					"Erreur d'accès aux colonnes de la table",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );

				Deconnexion();
			}
		}
		private void lbListeTables_DoubleClick(object sender, EventArgs e)
		{
			lbListeTables_SelectedIndexChanged(sender, e);
			OuvrirTable();
		}
		private void btConnexion_Click(object sender, EventArgs e)
		{
			Connexion();
		}
		private void btLister_Click(object sender, EventArgs e)
		{
			ListerBasesDeDonnées();
		}
		private void btDeconnexion_Click(object sender, EventArgs e)
		{
			Deconnexion();
		}
		private void btOuvrirFichier_Click(object sender, EventArgs e)
		{
			OuvrirFichierCSV_XLS();
		}
		private void btEnregistrerDansFichier_Click(object sender, EventArgs e)
		{
			EnregistrerFichier();
		}

		FormAssociation frmAssociation = null;
		bool persisterFormAssociation = false;

		private void btEnregistrerDansTable_Click(object sender, EventArgs e)
		{
			try
			{
				if (TamponCourant == null )
					return;

				string strNomTable = ((InfoTable)lbListeTablesVues.SelectedItem).NomTable;
				List<string> listeChampsTampon = new List<string>();

				foreach (DataColumn colonne in TamponCourant.Columns)
					listeChampsTampon.Add(colonne.ToString());

				if (null == frmAssociation || false == persisterFormAssociation)
					frmAssociation = new FormAssociation(strNomTable);

				frmAssociation.ChampsBDOrigine = GestionnaireBD.Connexion.ListeDesColonnes(strNomTable);
				frmAssociation.ChampsTamponOrigine = listeChampsTampon;

				//frmAssociation.ArretSiErreurDInsertion = this.cbErreursBloquantes.Checked;
				DialogResult resultat = frmAssociation.ShowDialog();

				persisterFormAssociation = true;
				//this.cbErreursBloquantes.Checked = frmAssociation.ArretSiErreurDInsertion;

				if (DialogResult.OK == resultat)
				{
					this.splitContainerPrincipal.Enabled = false;
					this.Cursor = Cursors.No;

					GestionnaireBD.Instance.ImporterTableBD(
						((InfoTable)lbListeTablesVues.SelectedItem).NomTable,
						TamponCourant,
						GridViewCourant,
						frmAssociation.Association,
						new RunWorkerCompletedEventHandler(EvénementImportDeTableTerminé)
						);
				}
				//else
				//{
				//	MessageBox.Show("Opération annulée", "Opération annulée", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
						"Erreur : "+ex.Message,
						"Impossible d'enregistrer",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
						);
			}

			ActualiserAffichageSelonTampon();
		}

		private void btOuvrirTable_Click(object sender, EventArgs e)
		{
			tabListeTablesEditeurSQL.SelectedIndex = 0;
			OuvrirTable();
		}
		//private void btConcaténerTable_Click(object sender, EventArgs e)
		//{
		//	OuvrirTable(true);
		//}
		private void btRequete_Click(object sender, EventArgs e)
		{
			ExecuterRequeteSQL();
		}
		private void btPurger_Click(object sender, EventArgs e)
		{
			PurgerTamponCourant();
		}

		private void btToutPurger_Click(object sender, EventArgs e)
		{
			PurgerTousTampons();
		}

		private void btFusionnerTampons_Click(object sender, EventArgs e)
		{
			FusionnerTousTampons();
		}

		private void btFichierSQL_Click(object sender, EventArgs e)
		{
			OuvrirFichierSQL();
		}

		private void cbConfig_SelectedIndexChanged(object sender, EventArgs e)
		{
			ConfigurationConnexion newConfig = ( cbConfig.SelectedItem as ConfigurationConnexion );

			tbHost.Text = newConfig.Host;
			tbLogin.Text = newConfig.Login;
			tbPass.Text = newConfig.Pass;
			cbBase.Text = newConfig.Base;

			if( cbTypeBase.Items.Count > 0 )
				cbTypeBase.SelectedIndex = newConfig.TypeBase.intValue;

			cbAuthentificationIntegree.Checked = newConfig.Options.AuthentificationIntegree;
			cbConnexionODBC.Checked = newConfig.Options.ConnexionODBC;

			tbNomConfigurationCourante.Text = newConfig.NomConfig;
			/*switch (newConfig.TypeBase)
			{
				case ConnexionBD.eTypeConnexion.SQLServer_AuthWindows:
					rbWindows.Select();
					break;
				default:
				case ConnexionBD.eTypeConnexion.SQLServer_AuthSQLServer:
					rbSQLServer.Select();
					break;
				case ConnexionBD.eTypeConnexion.Informix:
					rbInformix.Select();
					break;
				case ConnexionBD.eTypeConnexion.ODBC_Informix:
					rbODBC.Select();
					break;
			}*/
		}

		private void CheckBox_ValueHasChanged(object sender, EventArgs e)
		{
			tbLogin.Enabled = true;
			tbPass.Enabled = true;
			cbBase.Enabled = true;

			if (cbAuthentificationIntegree.Checked == true)
			{
				tbLogin.Enabled = false;
				tbPass.Enabled = false;
			}

			if (cbConnexionODBC.Checked == true)
			{
				tbLogin.Enabled = false;
				tbPass.Enabled = false;
				cbBase.Enabled = false;
			}

			UpdateConnexionString();
		}

		private void btImporterTouteLaBD_Click(object sender, EventArgs e)
		{
			ImporterTouteLaBD();
				//(DialogResult.Yes == MessageBox.Show(
				//		"Souhaitez-vous supprimer les informations déjà présentes dans les tables avant de réaliser cet import ?",
				//		"Que faire des données existantes ?",
				//		MessageBoxButtons.YesNo,
				//		MessageBoxIcon.Information
				//	 ))
				//);
			//this.splitContainerPrincipal.Enabled = true;
		}

		private void btExtraireTouteLaBD_Click(object sender, EventArgs e)
		{
			ExporterTouteLaBD();
		}

		private void TextHasChanged(object sender, EventArgs e)
		{
			UpdateConnexionString();

			this.btListerBasesDeDonnées.Enabled = (
				this.tbHost.TextLength > 0 &&
				this.tbLogin.TextLength > 0 &&
				this.tbPass.TextLength > 0);
		}

		private void cbTypeBase_SelectedValueChanged(object sender, EventArgs e)
		{
			UpdateConnexionString();
		}

		private void btChangerRep_Click(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = tbRepertoire.Text;
			folderBrowserDialog.ShowDialog(this);
			tbRepertoire.Text = folderBrowserDialog.SelectedPath;

			GestionnaireCSV.Répertoire = tbRepertoire.Text;
			persisterFormImportDeMasse = false;
		}

		private void tabTampons_SelectedIndexChanged(object sender, EventArgs e)
		{
			ActualiserAffichageSelonTampon();

			tbNomFichierExport.Text = GestionnaireCSV.GénérerNomFichierExport(tabTampons.SelectedTab.Text);
/*				"export_" + tabTampons.SelectedTab.Text + "_" +
				DateTime.Now.Year.ToString() + "-" +
				DateTime.Now.Month.ToString() + "-" +
				DateTime.Now.Day.ToString() + "_" +
				DateTime.Now.Hour.ToString() + "h" +
				DateTime.Now.Minute.ToString() + "m" +
				DateTime.Now.Second.ToString() +
				".csv";//*/
		}

		private void btTamponPersonnalisé_Click(object sender, EventArgs e)
		{
			this.splitContainerPrincipal.Enabled = false;
			FormNouveauTampon dialogue = new FormNouveauTampon();

			if (DialogResult.OK == dialogue.ShowDialog())
			{
				List<string> liste = dialogue.ListeDesColonnes();

				DataTable table = new DataTable();

				foreach (string nomColonne in liste)
					table.Columns.Add(new DataColumn(nomColonne));

				for (int i = 0; i < dialogue.NouvellesLignesVierges; i++)
				{
					table.Rows.Add( table.NewRow() );
				}

				FusionnerTamponCourant(table, "Modifié", "Personnalisé");
			}
			ActualiserAffichageSelonTampon();
			this.splitContainerPrincipal.Enabled = true;
		}

		private void btRéindexer_Click(object sender, EventArgs e)
		{
			FormRéindexer dialogue = new FormRéindexer( TamponCourant );
			dialogue.ShowDialog();

			//this.TamponCourant.
		}

		private void btDupliquer_Click(object sender, EventArgs e)
		{
			// Le tampon courant va changer
			DataTable nouveauDataTable = new DataTable();
			nouveauDataTable.Merge(TamponCourant, false);
			string strNomOnglet = tabTampons.SelectedTab.Text;

			if (ChoisirNouvelOngletVide())
			{
				TamponCourant = nouveauDataTable;
				tabTampons.SelectedTab.Text = strNomOnglet;
				ActualiserAffichageSelonTampon();
			}
			else MessageBox.Show(
					"Tous les tampons sont pleins : purgez un tampon pour continuer",
					"Impossible de dupliquer",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
					);
			//try
			//{
			//	int increment = 1;
			//	while( null != tabGridView[ tabTampons.SelectedIndex + increment ].DataSource )
			//		increment ++ ;
			//	tabGridView[ tabTampons.SelectedIndex + increment ].DataSource = nouveauDataTable;
			//	tabTampons.SelectedIndex = tabTampons.SelectedIndex + increment;
			//	tabTampons.SelectedTab.Text = "Tampon dupliqué";
			//}
			//catch (Exception)
			//{}
		}

		private void btEnregistrerConfig_Click(object sender, EventArgs e)
		{
			if( tbNomConfigurationCourante.TextLength > 0
				&& tbHost.TextLength > 0
				&& tbLogin.TextLength > 0
				&& tbPass.TextLength > 0
				&& cbBase.Text.Length > 0 )
			{
				ConfigurationConnexion nouvelleConfig = ConfigurationConnexion.Get(tbNomConfigurationCourante.Text);

				if (null == nouvelleConfig)
				{
					nouvelleConfig = new ConfigurationConnexion(
						tbNomConfigurationCourante.Text,
						tbHost.Text,
						tbLogin.Text,
						tbPass.Text,
						cbBase.Text,
						ConnexionBD.Types[cbTypeBase.SelectedIndex],
						new ConnexionBD.Options(
								cbAuthentificationIntegree.Checked,
								cbConnexionODBC.Checked
							)
						);
					ConfigurationConnexion.listeDesConfigs.Add(nouvelleConfig);
					cbConfig.Items.Add(nouvelleConfig);
					cbConfig.SelectedItem = nouvelleConfig;
				}
				else
				{
					nouvelleConfig.Host = tbHost.Text;
					nouvelleConfig.Login = tbLogin.Text;
					nouvelleConfig.Pass = tbPass.Text;
					nouvelleConfig.Base = cbBase.Text;
					nouvelleConfig.TypeBase = ConnexionBD.Types[cbTypeBase.SelectedIndex];
					nouvelleConfig.Options = new ConnexionBD.Options(
								cbAuthentificationIntegree.Checked,
								cbConnexionODBC.Checked
							);
				}
				
				ConfigurationConnexion.Enregistrer();
				lbEtatConfigurations.Text = "Les configurations sont sauvegardées dans le fichier config.xml";

				MessageBox.Show(
					"Configuration enregistrée dans " + ConfigurationConnexion.NomFichierConfigXML,
					"Enregistrement OK",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information
						);
				tabCommandes.SelectedIndex = 0;
			}
			else
				MessageBox.Show(
					"Pour enregistrer une nouvelle configuration, remplissez tous les champs sur la page de connexion, puis, donnez un nom à votre nouvelle configuration",
					"Informations manquantes",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation
						);

			btSupprimerConfig.Enabled = (ConfigurationConnexion.ExisteFichierConfig && ConfigurationConnexion.listeDesConfigs.Count > 0);
			btRéinitConfig.Enabled = (ConfigurationConnexion.ExisteFichierConfig && File.Exists(ConfigurationConnexion.NomFichierConfigXML));
		}

		private void btSupprimerConfig_Click(object sender, EventArgs e)
		{
			try
			{
				ConfigurationConnexion configASupprimer = ConfigurationConnexion.Get(tbNomConfigurationCourante.Text);

				if (null == configASupprimer)
				{
					MessageBox.Show(
						"La configuration " + tbNomConfigurationCourante.Text + " n'existe pas !",
						"Configuration non trouvée",
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation
						);
				}
				else if (DialogResult.OK == MessageBox.Show(
					"Supprimer la configuration " + configASupprimer.NomConfig + " et enregistrer le fichier ? ",
					"Suppression",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question))
				{
					cbConfig.Items.Remove(configASupprimer);

					ConfigurationConnexion.listeDesConfigs.Remove(configASupprimer);
					ConfigurationConnexion.Enregistrer();

					if (cbConfig.Items.Count > 0)
					{
						tbNomConfigurationCourante.Text = "";
						cbConfig.SelectedIndex = 0;
						lbEtatConfigurations.Text = "La configuration " + configASupprimer.NomConfig + " a été supprimée";
					}
					else
					{
						File.Delete(ConfigurationConnexion.NomFichierConfigXML);
						cbConfig.Items.Clear();
						tbNomConfigurationCourante.Text = "";
						lbEtatConfigurations.Text = "Le fichier des configurations a été effacé";

						//tabCommandes.SelectedIndex = 0;

						ConfigurationConnexion.ChargerListeConfigParDefaut();
						cbConfig.Items.AddRange(ConfigurationConnexion.listeDesConfigs.ToArray());
						cbConfig.SelectedIndex = 0;

						tabCommandes.SelectedIndex = 0;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			btSupprimerConfig.Enabled = (ConfigurationConnexion.ExisteFichierConfig && ConfigurationConnexion.listeDesConfigs.Count > 0);
			btRéinitConfig.Enabled = (ConfigurationConnexion.ExisteFichierConfig && File.Exists(ConfigurationConnexion.NomFichierConfigXML));
		}

		private void btRéinitConfig_Click(object sender, EventArgs e)
		{
			try
			{
				ConfigurationConnexion configSelectionnée = (ConfigurationConnexion)cbConfig.SelectedItem;

				if (DialogResult.OK == MessageBox.Show(
					"Supprimer le fichier " + ConfigurationConnexion.NomFichierConfigXML + " ? ",
					"Suppression de toutes les configurations",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question))
				{
					File.Delete(ConfigurationConnexion.NomFichierConfigXML);
					cbConfig.Items.Clear();
					tbNomConfigurationCourante.Text = "";
					lbEtatConfigurations.Text = "Le fichier des configurations a été effacé";
					
					tabCommandes.SelectedIndex = 0;

					ConfigurationConnexion.ChargerListeConfigParDefaut();
					cbConfig.Items.AddRange(ConfigurationConnexion.listeDesConfigs.ToArray());
					cbConfig.SelectedIndex = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show( ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}

			btSupprimerConfig.Enabled = (ConfigurationConnexion.ExisteFichierConfig && ConfigurationConnexion.listeDesConfigs.Count > 0);
			btRéinitConfig.Enabled = (ConfigurationConnexion.ExisteFichierConfig && File.Exists(ConfigurationConnexion.NomFichierConfigXML));
		}

		private void tbCaractèreSéparateur_Click(object sender, EventArgs e)
		{
			btOuvrirFichierCSV.Enabled = false;
			tbCaractèreSéparateur.Text = "";
			tbCaractèreSéparateur.Focus();
		}

		private void tbCaractèreSéparateur_TextChanged(object sender, EventArgs e)
		{
			if (tbCaractèreSéparateur.Text.Length > 0)
			{
				GestionnaireCSV.CaractèreSéparateur = tbCaractèreSéparateur.Text;
				btOuvrirFichierCSV.Enabled = true;
				btOuvrirFichierCSV.Focus();
			}
			else
			{
				tbCaractèreSéparateur.Text = "";
				tbCaractèreSéparateur.Focus();
			}
		}

		private void cbControlerNbColonnes_CheckedChanged(object sender, EventArgs e)
		{
			GestionnaireCSV.ControleNbColonnes = cbControlerNbColonnes.Checked;
		}

		private void cbTrimSpaces_CheckedChanged(object sender, EventArgs e)
		{
			GestionnaireCSV.TrimSpaces = cbTrimSpaces.Checked;
		}

		private void tbConnectionString_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\n' || e.KeyChar == '\r' || e.KeyChar == 10 || e.KeyChar == 13)
			{
				btConnexion.Focus();
				btConnexion.PerformClick();
			}
		}

		private void cbModèlePrédéfini_SelectedIndexChanged(object sender, EventArgs e)
		{
			// string selectedText = "\r\nselected text = "+cbModèlePrédéfini.SelectedText;
			// string selectedValue = "\r\nselected value = " + ((cbModèlePrédéfini.SelectedValue != null) ? cbModèlePrédéfini.SelectedValue.ToString() : "<null>");
			// string selectedItem = "\r\nselected item = " + cbModèlePrédéfini.SelectedItem;
			//tbRequete1.AppendText("\r\n" + cbModèlePrédéfini1.SelectedItem + "\r\n");
			//tbRequete1.Focus();
            EditeurSQLCourant.AppendText(
                "\r\n" + 
                tabComboBoxModèlesPrédéfinis[ tabListeTablesEditeurSQL.SelectedIndex-1 ].SelectedItem + 
                "\r\n" );
            EditeurSQLCourant.Focus();
		}

		private void tabListeTablesEditeurSQL_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (tabCommandes.SelectedIndex)
			{
				case 0:
					lbListeTablesVues.Focus();
					break;
				case 1:
                    EditeurSQLCourant.Focus();
					break;
			}
		}

		private void tabCommandes_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (tabCommandes.SelectedIndex)
			{
				case 0:
					tbHost.Focus();
					break;
				case 1:
					btEnregistrerConfig.Focus();
					break;
				case 2:
					btOuvrirTable.Focus();
					break;
				case 3:
                    EditeurSQLCourant.Focus();
					break;
				case 4:
					tbRepertoire.Focus();
					break;
			}
		}

		private void btEffacerRequetes_Click(object sender, EventArgs e)
		{
            EditeurSQLCourant.Clear();
            EditeurSQLCourant.Focus();
		}

		private void btSupprLigne_Click(object sender, EventArgs e)
		{
			this.splitContainerPrincipal.Enabled = false;
			this.Cursor = Cursors.No;
			//DataGridView currentDGV = tabGridView[tabTampons.SelectedIndex];
			try
			{
				if (GridViewCourant.SelectedCells.Count > 0)
				{
					int rowIndexMin = int.MaxValue;// ;
					int rowIndexMax = int.MinValue;

					foreach (DataGridViewCell currentCell in GridViewCourant.SelectedCells)
					{
						if (rowIndexMin > currentCell.RowIndex) rowIndexMin = currentCell.RowIndex;
						if (rowIndexMax < currentCell.RowIndex) rowIndexMax = currentCell.RowIndex;
					}

					//MessageBox.Show("Suppr Ligne " + rowIndex);
					for (int index = rowIndexMax; index >= rowIndexMin; index--)
						TamponCourant.Rows.RemoveAt(index);

					GridViewCourant.NotifyCurrentCellDirty(true);
					GridViewCourant.Update();
					//currentDGV.Rows.RemoveAt(	rowIndex );
				}
			}
			catch (Exception)
			{
				TamponCourant = null;
			}

			ActualiserAffichageSelonTampon();
			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		private void btSupprColonne_Click(object sender, EventArgs e)
		{
			this.splitContainerPrincipal.Enabled = false;
			this.Cursor = Cursors.No;

			try
			{
				//DataGridView currentDGV = tabGridView[tabTampons.SelectedIndex];

				if (GridViewCourant.SelectedCells.Count > 0)
				{
					int firstCol = GridViewCourant.SelectedCells[0].ColumnIndex;
					int firstRow = GridViewCourant.SelectedCells[0].RowIndex;

					int colIndexMin = int.MaxValue;
					int colIndexMax = int.MinValue;

					foreach (DataGridViewCell currentCell in GridViewCourant.SelectedCells)
					{
						if (colIndexMin > currentCell.ColumnIndex) colIndexMin = currentCell.ColumnIndex;
						if (colIndexMax < currentCell.ColumnIndex) colIndexMax = currentCell.ColumnIndex;
					}

					for (int index = colIndexMax; index >= colIndexMin; index--)
						TamponCourant.Columns.RemoveAt(index);

					//foreach (DataGridViewCell currentCell in GridViewCourant.SelectedCells)
					//	currentCell.Selected = false;

					//if( firstRow < GridViewCourant.Rows.Count
					// && firstCol < GridViewCourant.Rows[firstRow].Cells.Count )
					//		GridViewCourant.Rows[firstRow].Cells[firstCol].Selected = true;

					GridViewCourant.NotifyCurrentCellDirty(true);
					GridViewCourant.Update();
				}
			}
			catch (Exception )
			{
				TamponCourant = null;
			}

			ActualiserAffichageSelonTampon();
			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		private void btFiltrerListeTables_Click(object sender, EventArgs e)
		{
			bool afficherTables = true;
			bool afficherVues = true;

			if (cbChoixFiltresTablesVues.Text.Equals("Tables")) afficherVues = false;
			else if (cbChoixFiltresTablesVues.Text.Equals("Vues")) afficherTables = false;
			
			AfficherListeDesElementsBD(
				afficherTables,
				afficherVues,
				cbMinLignes.Checked,
				(int) nuMinLignes.Value,
				cbMaxLignes.Checked,
				(int) nuMaxLignes.Value,
				cbTablesAvecErreur.Checked);
			lbListeTablesVues.Focus();
		}

		private void MasquerPanelsSelonRadioBoxes(object sender, EventArgs e)
		{
			if (rbInterfaceNormale.Checked)
			{
				//tabListeTablesEditeurSQL.Visible = true;
				//tabCommandes.Visible = true;
				tlpGestionnaireBoutonsTampons.Visible = true;
				splitContainerPrincipal.Panel1Collapsed = false;
				splitContainerPrincipal.Panel2Collapsed = false;
				splitContainerDuPanelHaut.Panel1Collapsed = false;

				GridViewCourant.Focus();
			}
			else if (rbPleinEcranTampon.Checked)
			{
				//tabListeTablesEditeurSQL.Visible = false;
				//tabCommandes.Visible = false;
				tlpGestionnaireBoutonsTampons.Visible = false;
				splitContainerPrincipal.Panel1Collapsed = true;
				splitContainerPrincipal.Panel2Collapsed = false;

				lbListeTablesVues.Focus();
			}
			else if (rbPleinEcranTablesBD.Checked)
			{
				splitContainerPrincipal.Panel1Collapsed = false;
				splitContainerPrincipal.Panel2Collapsed = true;
				splitContainerDuPanelHaut.Panel1Collapsed = true;

				GridViewCourant.Focus();
			}

			//tabGridView[tabTampons.SelectedIndex].Focus();
			GridViewCourant.Focus();
		}

		private void cbMinLignes_CheckedChanged(object sender, EventArgs e)
		{
			nuMinLignes.Enabled = cbMinLignes.Checked;
		}

		private void cbMaxLignes_CheckedChanged(object sender, EventArgs e)
		{
			nuMaxLignes.Enabled = cbMaxLignes.Checked;
		}

		private void cbTablesAvecErreur_CheckedChanged(object sender, EventArgs e)
		{
			cbMinLignes.Enabled = !cbTablesAvecErreur.Checked;
			cbMaxLignes.Enabled = !cbTablesAvecErreur.Checked;
			nuMinLignes.Enabled = !cbTablesAvecErreur.Checked && cbMinLignes.Checked;
			nuMaxLignes.Enabled = !cbTablesAvecErreur.Checked && cbMaxLignes.Checked;
		}

		private void btInsertionsSuccès_Click(object sender, EventArgs e)
		{
			if (ChoisirNouvelOngletVide())
			{
				TamponCourant = GestionnaireBD.Instance.InsertionsSuccès;
				tabTampons.SelectedTab.Text = "Lignes en succès";

				foreach (DataGridViewRow row in GridViewCourant.Rows)
				{
					row.DefaultCellStyle.BackColor = Color.LawnGreen;
				}

				ActualiserAffichageSelonTampon();
			}
			else MessageBox.Show("Tous les tampons sont pleins. Veuillez purger un tampon pour continuer.", "Aucun tampon vide trouvé", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void btInsertionsEchecs_Click(object sender, EventArgs e)
		{
			if (ChoisirNouvelOngletVide())
			{
				TamponCourant = GestionnaireBD.Instance.InsertionsEchecs;
				tabTampons.SelectedTab.Text = "Lignes en échec";

				foreach (DataGridViewRow row in GridViewCourant.Rows)
				{
					row.DefaultCellStyle.BackColor = Color.Red;
				}

				ActualiserAffichageSelonTampon();
			}
			else MessageBox.Show("Tous les tampons sont pleins. Veuillez purger un tampon pour continuer.", "Aucun tampon vide trouvé", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void btMontrerGestionnaireDesTâches_Click(object sender, EventArgs e)
		{
			GestionnaireDesTâches.Instance.Visible = true;

			if (GestionnaireDesTâches.Instance.WindowState == FormWindowState.Minimized)
				GestionnaireDesTâches.Instance.WindowState = FormWindowState.Normal;

			GestionnaireDesTâches.Instance.BringToFront();
		}

		private void btChercher_Click(object sender, EventArgs e)
		{
			this.splitContainerPrincipal.Enabled = false;
			this.Cursor = Cursors.No;

			try
			{
				// Le tampon courant va changer
				DataTable nouveauDataTable = new DataTable();
				nouveauDataTable.Merge(TamponCourant, false);

				if (ChoisirNouvelOngletVide())
				{
					List<DataRow> listeASupprimer = new List<DataRow>();
					TamponCourant = nouveauDataTable;
					tabTampons.SelectedTab.Text = "Résultats de la recherche";

					FormInput input = new FormInput("Rechercher", "Veuillez saisir la valeur recherchée :");

					if (DialogResult.OK == input.ShowDialog())
					{
						string pattern = input.ValeurSaisie;
						int ligne = 0;
						int colonne = 0;

						foreach (DataRow row in nouveauDataTable.Rows)
						{
							bool trouvé = false;
							colonne = 0;

							foreach (object valeur in row.ItemArray)
							{
								if (valeur.ToString().ToLower().Contains(pattern.ToLower()))
								{
									trouvé = true;
									GridViewCourant.Rows[ligne].Cells[colonne].Style.BackColor = Color.LightGreen;
									//break;
								}
								colonne++;
							}

							if (!trouvé)
								listeASupprimer.Add(row);

							ligne++;
						}

						foreach (DataRow row in listeASupprimer)
							nouveauDataTable.Rows.Remove(row);
					}
					else TamponCourant = null;
				}
				else MessageBox.Show("Tous les tampons sont pleins. Veuillez purger un tampon pour continuer.", "Aucun tampon vide trouvé", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				GridViewCourant.InvalidateRow(GridViewCourant.Rows.Count - 1);
				GridViewCourant.NotifyCurrentCellDirty(true);
				GridViewCourant.PerformLayout();
				GridViewCourant.Refresh();
				GridViewCourant.Update();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					ex.Message,
					"Erreur",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			

			ActualiserAffichageSelonTampon();
			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		private void tbRepertoire_TextChanged(object sender, EventArgs e)
		{
			GestionnaireCSV.Répertoire = tbRepertoire.Text;
			lbRépertoire.Text = tbRepertoire.Text;
		}

		private void dgvTampon_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			//MessageBox.Show("iii");
			/* Exemple trouvé sur le net
			if (e.Column.Index == 0) // Colonne qui nous concerne
			{
				if (int.Parse(e.CellValue1.ToString()) > int.Parse(e.CellValue2.ToString())) // Trier comme des entiers
				{
					e.SortResult = -1;
				}
				else
				{
					e.SortResult = 1;
				}
				e.Handled = true; // Signaler le tri
			}// * /
			try
			{
				int valeur1 = int.Parse(e.CellValue1.ToString());
				int valeur2 = int.Parse(e.CellValue2.ToString());

				if (valeur1 > valeur2)
					e.SortResult = -1;
				else
					e.SortResult = 1;
				GestionnaireDesTâches.addMessage("" + valeur1 + " > " + valeur2 + " -> " + e.SortResult + " | " + valeur1.CompareTo(valeur2));
				e.Handled = true;
			}
			catch (Exception ex) { GestionnaireDesTâches.addException(ex); } // */
		}

		private void dgvTampon_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			//TamponCourant
			//GridViewCourant.Sort(new MonCompareur());
			//GridViewCourant.Sort(GridViewCourant.Columns[0], ListSortDirection.Ascending);
		}

		private void btSchémaTable_Click(object sender, EventArgs e)
		{
			this.splitContainerPrincipal.Enabled = false;
			this.Cursor = Cursors.No;
			tabListeTablesEditeurSQL.SelectedIndex = 0;

			if (lbListeTablesVues.SelectedItem != null)
			{
				// Nom de la table
				string strTable = ((InfoTable)lbListeTablesVues.SelectedItem).NomTable;
				DataTable schéma = GestionnaireBD.Instance.GetSchemaTable(strTable);

				/*new DataTable(strTable);
				 * List<string> listeDesColonnes = GestionnaireBD.Connexion.ListeDesColonnes(strTable);

				// Liste des colonnes de la table
				foreach (string nomColonne in listeDesColonnes)
					schéma.Columns.Add(nomColonne);//*/


				FusionnerTamponCourant(schéma, "Tampon modifié", "Schéma de " + strTable);
				ActualiserAffichageSelonTampon();
			}

			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		private void btEditerFichierConfig_Click(object sender, EventArgs e)
		{
			Process p = new System.Diagnostics.Process();
			p.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
			
			if( File.Exists( @"C:\Program Files\Notepad++\notepad++.exe" ) )
				p.StartInfo.FileName = @"C:\Program Files\Notepad++\notepad++.exe";
			else
				p.StartInfo.FileName = "notepad.exe";

			p.StartInfo.Verb = "open";
			p.StartInfo.Arguments = "Import_Export_CSV.Config.xml";

			p.Start();
		}

		private void btEffacerBD_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == MessageBox.Show(
					"Générer un script dans la fenêtre SQL 1 pour effacer toutes les données de la base ? ",
					"Confirmez la suppression",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question))
			{
                //if( 0 == tabListeTablesEditeurSQL.SelectedIndex )
				    tabListeTablesEditeurSQL.SelectedIndex = 1;

                EditeurSQLCourant.Text = "\r\n";

				foreach (InfoTable table in lbListeTablesVues.Items)
                    EditeurSQLCourant.Text += "DELETE FROM [" + table.NomTable + "]\r\n";

				//tabCommandes.SelectedIndex = 3;
			}
		}

		private void btEffacerTable_Click(object sender, EventArgs e)
		{
			if (lbListeTablesVues.SelectedItem != null)
			{
				// Nom de la table
				string strTable = ((InfoTable)lbListeTablesVues.SelectedItem).NomTable;

				if (DialogResult.OK == MessageBox.Show(
						"Générer un script dans la fenêtre SQL 1 pour effacer les données de la table " + strTable + " ? ",
						"Confirmez la suppression",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Question))
				{
					tabListeTablesEditeurSQL.SelectedIndex = 1;
                    EditeurSQLCourant.Text = "\r\nDELETE FROM [" + strTable + "]\r\n\r\n";
					//tabCommandes.SelectedIndex = 3;
				}
			}
		}

		private void btSelectFromTable_Click(object sender, EventArgs e)
		{

			if (lbListeTablesVues.SelectedItem != null)
			{
				tabListeTablesEditeurSQL.SelectedIndex = 1;

				// Nom de la table
				string strTable = ((InfoTable)lbListeTablesVues.SelectedItem).NomTable;

                EditeurSQLCourant.Text = "\r\nSELECT * FROM [" + strTable + "] \r\n\r\n";
				//tabCommandes.SelectedIndex = 3;
			}
		}

		private void btGC_Click(object sender, EventArgs e)
		{
			this.btGC.Enabled = false;
			this.splitContainerPrincipal.Enabled = false;
			this.Cursor = Cursors.No;

			GestionnaireExcel.FermerExcel();
			System.GC.Collect();

			this.splitContainerPrincipal.Enabled = true;
			this.Cursor = Cursors.Default;
			this.btGC.Enabled = true;
		}

		private void btExcel_Click(object sender, EventArgs e)
		{
			
			//GestionnaireExcel.AppliExcel.Visible = true;

			try
			{
				FormBEAUDREY formBEAUDREY = new FormBEAUDREY(this.tbRepertoire.Text);		
				formBEAUDREY.ShowDialog();

			}
			catch (Exception ex)
			{
				MessageBox.Show("[btExcel_Click] = "+ex.ToString(), ex.Message);

				if( ex.InnerException != null )
					MessageBox.Show(ex.InnerException.Message, ex.InnerException.ToString());
			}
		}

		private void FormPrincipale_FormClosing(object sender, FormClosingEventArgs e)
		{
			GestionnaireExcel.FermerExcel();
			System.GC.Collect();
		}


		/* // Gets all System data source names for the local machine.
		private System.Collections.SortedList GetSystemDataSourceNames()
		{
			System.Collections.SortedList dsnList = new System.Collections.SortedList();

			try
			{
				// get system dsn's
				Microsoft.Win32.RegistryKey reg =
					(Microsoft.Win32.Registry.LocalMachine)
					.OpenSubKey("Software")
					.OpenSubKey("ODBC")
					.OpenSubKey("ODBC.INI")
					.OpenSubKey("ODBC Data Sources");

				// Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
				foreach (string sName in reg.GetValueNames())
				{
					dsnList.Add(sName, DataSourceType.System);
				}

				reg.Close();
			}
			catch (Exception) { }

			return dsnList;
		}

		// Gets all User data source names for the local machine.
		public System.Collections.SortedList GetUserDataSourceNames()
		{
			System.Collections.SortedList dsnList = new System.Collections.SortedList();

			try
			{
				// get system dsn's
				Microsoft.Win32.RegistryKey reg =
					(Microsoft.Win32.Registry.LocalMachine)
					.OpenSubKey("Software")
					.OpenSubKey("ODBC")
					.OpenSubKey("ODBC.INI")
					.OpenSubKey("ODBC Data Sources");

				// Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
				foreach (string sName in reg.GetValueNames())
				{
					dsnList.Add(sName, DataSourceType.System);
				}

				reg.Close();
				reg =
					(Microsoft.Win32.Registry.CurrentUser)
					.OpenSubKey("Software")
					.OpenSubKey("ODBC")
					.OpenSubKey("ODBC.INI")
					.OpenSubKey("ODBC Data Sources");

				foreach (string sName in reg.GetValueNames())
				{
					dsnList.Add(sName, DataSourceType.System);
				}
			}
			catch (Exception) { }

			return dsnList;
		}
		 
		System.Collections.SortedList dsnList = new System.Collections.SortedList();

		// get user dsn's
		Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software");
		if (reg != null)
		{
			reg = reg.OpenSubKey("ODBC");
			if (reg != null)
			{
				reg = reg.OpenSubKey("ODBC.INI");
				if (reg != null)
				{
					reg = reg.OpenSubKey("ODBC Data Sources");
					if (reg != null)
					{
						// Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
						foreach (string sName in reg.GetValueNames())
						{
							dsnList.Add(sName, DataSourceType.User);
						}
					}
					try
					{
						reg.Close();
					}
					catch { / * ignore this exception if we couldn't close * / }
				}
			}
		}

		return dsnList;
	}
	// Returns a list of data source names from the local machine.
	public System.Collections.SortedList GetAllDataSourceNames()
	{
		// Get the list of user DSN's first.
		System.Collections.SortedList dsnList = GetUserDataSourceNames();

		// Get list of System DSN's and add them to the first list.
		System.Collections.SortedList systemDsnList = GetSystemDataSourceNames();
		for (int i = 0; i < systemDsnList.Count; i++)
		{
			string sName = systemDsnList.GetKey(i) as string;
			DataSourceType type = (DataSourceType)systemDsnList.GetByIndex(i);
			try
			{
				// This dsn to the master list
				dsnList.Add(sName, type);
			}
			catch
			{
				// An exception can be thrown if the key being added is a duplicate so
				// we just catch it here and have to ignore it.
			}
		}

		return dsnList;
	}
//*/
        
		
		private void dgvTampon_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if( e.RowIndex >= 0 )
            {
            	string strNumDeLigne = (e.RowIndex+1).ToString();
            	Size taille = e.Graphics.MeasureString( 
						strNumDeLigne, 
						Font
					).ToSize();
            	
            	// int incrementX =( e.RowBounds.Width - taille.Width )/2;
            	int incrementY =( e.RowBounds.Height - taille.Height )/2;
            
                e.Graphics.DrawString(
            		strNumDeLigne,
            		Font, 
            		Brushes.Black,
            		40, //e.RowBounds.X + incrementX, 
            		e.RowBounds.Y + incrementY,
            		new StringFormat( StringFormatFlags.DirectionRightToLeft) 
            	);
            	
//            	GestionnaireDesTâches.Message( 
//                  		strNumDeLigne + 
//                  		" - Width x Height = " + e.RowBounds.Width + "x" + e.RowBounds.Height +
//                  		" - X x Y = " + e.RowBounds.X + "x" + e.RowBounds.Y +
//                  		" - increment X = " + incrementX
//                  	);
            }
        }
		
        private void DgvTampon_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
//        	
//			if (e.RowIndex >= 0)
//				if (e.ColumnIndex == 0)
//				{
//					Font lafont = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold, GraphicsUnit.Pixel );
//					string strNumDeLigne = (e.RowIndex+1).ToString();
//					
//						
//					
//					
//					Size taille = tailleF.ToSize();
//					
//					for( int i=0; i<20; i++ )
//					{
//						e.Graphics.DrawString(
//							i.ToString(),
//							lafont, 
//							Brushes.Red, 
//							20*i, 
//							20*i
//						);
//					}
					
					
					
//					e.Graphics.DrawString(
//						"A"+strNumDeLigne, 
//						lafont, 
//						Brushes.Black, 
//						0, 
//						e.CellBounds.Y - 20
//					);
//					e.Graphics.DrawString(
//						"B"+strNumDeLigne, 
//						lafont, 
//						Brushes.Black, 
//						e.CellBounds.X - 20, 
//						e.CellBounds.Y - 20
//					);
//					e.Graphics.DrawString(
//						"C"+strNumDeLigne, 
//						lafont, 
//						Brushes.Black, 
//						0, 
//						e.CellBounds.Y
//					);
//					e.Graphics.DrawString(
//						"D"+strNumDeLigne, 
//						lafont, 
//						Brushes.Black, 
//						e.CellBounds.X - 20, 
//						e.CellBounds.Y
//					);
					
					
//					int tailleY = ( taille.Height / 2 );
//					int posX = e.CellBounds.Width - taille.Width ;
//					int posY = e.CellBounds.Height - tailleY;
//
//					e.Graphics.DrawString(
//						strNumDeLigne, 
//						Font, 
//						Brushes.Black, 
//						posX, 
//						posY
//					);
					
					//GestionnaireDesTâches.Message( "Taille String Width x Height = " + taille.Width + "x" + taille.Height );
//					GestionnaireDesTâches.Message( 
//                  		strNumDeLigne + 
//                  		" - Width x Height = " + e.CellBounds.Width + "x" + e.CellBounds.Height +
//                  		" - X x Y = " + e.CellBounds.X + "x" + e.CellBounds.Y );
//				}
        }
		#endregion
	}
}
	/*
	class MonCompareur : IComparer
	{
				public int Compare( object a, object b )
				{
					int returnValue = 0;

					try
					{
						int valeur1 = int.Parse(a.ToString());
						int valeur2 = int.Parse(b.ToString());

						if (valeur1 > valeur2)
							returnValue = -1;
						else
							returnValue = 1;
						GestionnaireDesTâches.addMessage("" + valeur1 + " > " + valeur2 + " -> " + returnValue + " | " + valeur1.CompareTo(valeur2));
						
						return returnValue;
					}
					catch (Exception ex) { GestionnaireDesTâches.addException(ex); }

					return a.ToString().CompareTo( b.ToString() );
				}
			}*
	 */


