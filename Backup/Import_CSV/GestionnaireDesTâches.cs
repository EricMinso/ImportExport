using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Import_Export_Universel
{
	public partial class GestionnaireDesTâches : Form
	{
		#region Static
	 
		private static GestionnaireDesTâches gestionnaire = null;

		public static GestionnaireDesTâches Instance
		{
			get
			{
				if (null == gestionnaire)
					gestionnaire = new GestionnaireDesTâches();

				return gestionnaire;
			}
		}

		public static bool ArrêtDemandé
		{
			get
			{
				return Instance.ArrêtTâcheDemandé;
			}
		}
		
				
		public static void SignalerProgression( long actuel, long minimum, long maximum, bool forcerSignalement )
		{
			Instance.SignalerProgressionTâche( actuel, minimum, maximum, forcerSignalement );
		}
		
		public static void SignalerProgression( long actuel, bool forcerSignalement )
		{
			Instance.SignalerProgressionTâche( actuel, forcerSignalement );
		}
		
		public static void IncrémenterProgression()
		{
			Instance.IncrémenterProgressionTâche();
		}
	
		
		public static void Exception( Exception ex )
		{
			Instance.AjouterException( ex);
		}

		public static void Message(string message)
		{
			Instance.AjouterMessage( message);
		}

		public static void Avertissement(string message)
		{
			Instance.AjouterAvertissement( message );
		}

		public static void Titre(string strNomTâche)
		{
			Instance.AjouterTitre( strNomTâche);
		}

		public static void Démarrer(
			DoWorkEventHandler work,
			ProgressChangedEventHandler événementProgression,
			RunWorkerCompletedEventHandler événementTâcheTerminée,
			object paramètre
		)
		{
			Instance.DémarrerTâche( work, événementProgression, événementTâcheTerminée, paramètre );
		}
		
		#endregion
		#region Instance
		
		private BackgroundWorker backgroundWorker = null;
		//private bool arrêtDemandé = false;
		private bool stopScroll = false;

		public enum Mode
		{
			PAS_INITIALISE,
			DEBUG_LOG,
			GESTIONNAIRE_TACHE
		}
		private Mode modeCourant = Mode.PAS_INITIALISE;

		private DataTable dataTable = null;
		private DataTable dtMessages = null;
		private DataTable dtAvertissements = null;
		private DataTable dtErreurs = null;

		private const string ColNo = "No";
		private const string ColDate = "Date";
		private const string ColType = "Type";
		private const string ColMessage = "Message";
		private const int ColNoIndex = 0;
		private const int ColDateIndex = 1;
		private const int ColTypeIndex = 2;
		private const int ColMessageIndex = 3;

		private int nbMessages = 0;
		private int nbAvertissements = 0;
		private int nbErreurs = 0;

		public Mode ModeCourant
		{
			get { return this.modeCourant;  }
			set { if( this.modeCourant != value ) InitialiserMode( value ); }
		}

		private delegate void TypeDéléguéAjouterLigne(Type type, string titre, string message);
		private void AjouterLigne( Type type, string titre, string message )
		{
			int debugCompteur = 0;

			try
			{
				if (this.InvokeRequired)
				{
					debugCompteur = -1;
					TypeDéléguéAjouterLigne délégué = new TypeDéléguéAjouterLigne(AjouterLigne);

					object[] paramètres = new object[3];
					paramètres[0] = type;
					paramètres[1] = titre;
					paramètres[2] = message;

					this.BeginInvoke(délégué, paramètres);
					//this.Invoke(délégué, paramètres);
				}
				else
				{
					System.Threading.Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

					if (modeCourant == Mode.PAS_INITIALISE)
						InitialiserMode(Mode.DEBUG_LOG);

					debugCompteur = 10;
					lock (this)
					{
						if (type == typeof(Exception))
						{
							nbErreurs++;
							
							debugCompteur = 20;
							DataRow rowErr = this.dtErreurs.NewRow(); ;

							rowErr[ColNo] = this.dataTable.Rows.Count + 1;
							rowErr[ColDate] = DateTime.Now;
							rowErr[ColType] = titre;
							rowErr[ColMessage] = message;

							this.dtErreurs.Rows.Add(rowErr);
						}
						else if( titre.Equals( "Avertissement" ))
						{
							nbAvertissements++;
							
							DataRow rowWarn = this.dtAvertissements.NewRow(); ;

							rowWarn[ColNo] = this.dataTable.Rows.Count + 1;
							rowWarn[ColDate] = DateTime.Now;
							rowWarn[ColType] = titre;
							rowWarn[ColMessage] = message;

							this.dtAvertissements.Rows.Add(rowWarn);
						}
						else
						{
							nbMessages++;
							debugCompteur = 30;

							if( titre.Equals( "Titre" ))
							{
								this.Text = message;
								this.labelStatut.Text = "Tâche '" + message + "' démarre...";
							}

							DataRow rowMsg = this.dtMessages.NewRow(); ;

							rowMsg[ColNo] = this.dataTable.Rows.Count + 1;
							rowMsg[ColDate] = DateTime.Now;
							rowMsg[ColType] = titre;
							rowMsg[ColMessage] = message;

							this.dtMessages.Rows.Add(rowMsg);
						}

						debugCompteur = 40;
						DataRow row = this.dataTable.NewRow();

						row[ColNo] = this.dataTable.Rows.Count + 1;
						row[ColDate] = DateTime.Now;
						row[ColType] = titre;
						row[ColMessage] = message;

						//this.dataTable.Rows.InsertAt(row, 0);
						this.dataTable.Rows.Add(row);

						debugCompteur = 50;
						DataTable dtCourant = (DataTable)this.dgvAffichage.DataSource;

						if( dtCourant != null && dtCourant.Rows.Count > 0 )
						{
							debugCompteur = 60;
							this.dgvAffichage.InvalidateRow(dtCourant.Rows.Count - 1);

							if (!stopScroll)
							{
								debugCompteur = 70;
								this.dgvAffichage.FirstDisplayedScrollingRowIndex = dtCourant.Rows.Count - 1;
							}
						}

						//this.dgvExceptions.Rows[0].Selected = true;
						//this.dgvExceptions.Rows[0].

						debugCompteur = 80;
						this.btPurger.Enabled = true;
						this.btSauver.Enabled = true;
						this.btConsulter.Enabled = (dgvAffichage.SelectedRows.Count > 0);				

						debugCompteur = 90;
						this.cbAfficherMessages.Text = "Messages (" + nbMessages + ") ";
						this.cbAfficherAvertissements.Text = "Avertissements (" + nbAvertissements + ") ";
						this.cbAfficherErreurs.Text = "Erreurs (" + nbErreurs + ") ";
					}

					//if (this.WindowState == FormWindowState.Minimized)
					//	this.WindowState = FormWindowState.Normal;

					debugCompteur = 100;
					this.Visible = true;
					//this.Show();
					this.BringToFront();
					this.Focus();
				}
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					MessageBox.Show(
						"[AjouterLigne] Erreur Principale : "+ex.Message + "\nErreur source : "+ex.InnerException.ToString()+"\n"+ex.InnerException.Message,
						"Cpt " + debugCompteur + " - " + ex,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
						);
				}
				else
				{
					MessageBox.Show(
						"[AjouterLigne] Erreur : "+ex.Message,
						"Cpt " + debugCompteur + " - " + ex,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
						);
				}
			}
		}

		public void InitialiserMode( Mode setMode )
		{
			lock (this)
			{
				//this.arrêtDemandé = false;
				this.stopScroll = false;

				this.btArrêterTâche.Enabled = false;

				this.btPurger.Enabled = false;
				this.btSauver.Enabled = false;
				this.btConsulter.Enabled = false;

				this.progressBar.Minimum = 0;
				this.progressBar.Maximum = 100;
				this.progressBar.Step = 1;
				this.progressBar.Value = 0;

				this.dgvAffichage.DataSource = null;
				this.dataTable = null;
				this.dtMessages = null;
				this.dtErreurs = null;

				this.dataTable = new DataTable("GestionnaireDExceptions");
				this.dataTable.Columns.Add(ColNo, typeof(int));
				this.dataTable.Columns.Add(ColDate, typeof(DateTime));
				this.dataTable.Columns.Add(ColType, typeof(string));
				this.dataTable.Columns.Add(ColMessage, typeof(string));

				this.dtMessages = new DataTable("Messages");
				this.dtMessages.Columns.Add(ColNo, typeof(int));
				this.dtMessages.Columns.Add(ColDate, typeof(DateTime));
				this.dtMessages.Columns.Add(ColType, typeof(string));
				this.dtMessages.Columns.Add(ColMessage, typeof(string));

				this.dtAvertissements = new DataTable("Avertissements");
				this.dtAvertissements.Columns.Add(ColNo, typeof(int));
				this.dtAvertissements.Columns.Add(ColDate, typeof(DateTime));
				this.dtAvertissements.Columns.Add(ColType, typeof(string));
				this.dtAvertissements.Columns.Add(ColMessage, typeof(string));

				this.dtErreurs = new DataTable("Erreurs");
				this.dtErreurs.Columns.Add(ColNo, typeof(int));
				this.dtErreurs.Columns.Add(ColDate, typeof(DateTime));
				this.dtErreurs.Columns.Add(ColType, typeof(string));
				this.dtErreurs.Columns.Add(ColMessage, typeof(string));

				this.dgvAffichage.DataSource = this.dataTable;

				this.dgvAffichage.Columns[ColNo].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				this.dgvAffichage.Columns[ColDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				this.dgvAffichage.Columns[ColType].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				this.dgvAffichage.Columns[ColMessage].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

				this.nbMessages = 0;
				this.nbAvertissements = 0;
				this.nbErreurs = 0;

				//if (setMode == Mode.GESTIONNAIRE_TACHE)
				//{
					this.Text = "Suivi de l'opération";
					this.labelStatut.Text = "Aucune tâche lancée";
					this.progressBar.Visible = true;
					this.btArrêterTâche.Visible = true;
				//} else
				if (setMode == Mode.DEBUG_LOG )
				{
					this.Text = "Fenêtre de débug";
					this.labelStatut.Text = "Liste des erreurs";
					this.progressBar.Visible = false;
					this.btArrêterTâche.Visible = false;
				}
				 
				this.modeCourant = setMode;

				if (this.WindowState == FormWindowState.Minimized)
					this.WindowState = FormWindowState.Normal;

				this.Visible = true;
				this.Show();
			}
		}

		//private delegate void TypeDéléguéVoidSansParam();
		//private void safeIncrémenterTâche()
		//{
		//	if (this.progressBar.InvokeRequired)
		//	{
		//		TypeDéléguéVoidSansParam délégué = new TypeDéléguéVoidSansParam(safeIncrémenterTâche);
		//		this.Invoke(délégué);
		//	}
		//	else
		//	{
		//		this.progressBar.PerformStep();
		//		//Application.DoEvents();
		//		//System.Threading.Thread.Sleep(1000);
		//	}
		//}

		
		public bool TâcheEnCours
		{
			get
			{
				if( null != backgroundWorker )
					return backgroundWorker.IsBusy;
				
				return false;
			}
		}

		public bool ArrêtTâcheDemandé
		{
			get
			{
				if( null != backgroundWorker )
					return backgroundWorker.CancellationPending;
				
				return false;
			}
		}

		public void AjouterException( /*string strOrigine,*/ Exception ex )
		{
			Type type = typeof(Exception);
			string titre;
			string message;
			Exception innerException;

			if (ex is ImportExportException)
			{
				ImportExportException ieEx = (ex as ImportExportException);

				titre = ieEx.ExceptionSource.GetType().ToString();
				message = ex.Message;

				innerException = ieEx.ExceptionSource.InnerException;
			}
			else
			{
				titre = ex.GetType().ToString();
				message = ex.Message;

				innerException = ex.InnerException;
			}

			while (null != innerException)
			{
				titre += " - [Source: " + innerException.GetType().ToString();
				message += "\n\nSource : " + innerException.GetType().ToString()
					+ "\n" + innerException.Message;

				innerException = innerException.InnerException;
			}

			AjouterLigne(type, titre, /*"[" + strOrigine + "] : "*/ message);
		}

		public void AjouterMessage(string message)
		{
			AjouterLigne(typeof(string), "Message", message);
		}

		public void AjouterAvertissement(string message )
		{
			AjouterLigne(typeof(string), "Avertissement", message);
		}

		public void AjouterTitre(string strNomTâche)
		{
			AjouterLigne(typeof(string), "Titre", strNomTâche);
		}
		
		public void DémarrerTâche(
			DoWorkEventHandler work,
			ProgressChangedEventHandler événementProgression,
			RunWorkerCompletedEventHandler événementTâcheTerminée,
			object paramètre
		)
		{
			InitialiserMode( Mode.GESTIONNAIRE_TACHE );
			btArrêterTâche.Enabled = true;
			cbAfficherErreurs.Checked = true;
			cbAfficherMessages.Checked = true;
			labelStatut.Text = "Tâche démarre...";
			Text = "Suivi de l'opération";
			
			btArrêterTâche.Focus();
			backgroundWorker = new BackgroundWorker();

			backgroundWorker.WorkerReportsProgress = true;
			backgroundWorker.WorkerSupportsCancellation = true;

			backgroundWorker.DoWork += work;
			backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BGWorkProgressChanged);
			backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWorkFinished);
			
			if( null != événementProgression )
				backgroundWorker.ProgressChanged += événementProgression;
			
			if( null != événementTâcheTerminée )
				backgroundWorker.RunWorkerCompleted += événementTâcheTerminée;

			backgroundWorker.RunWorkerAsync( paramètre );
		}

		private void BGWorkProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			DéfinirPourcentageTâche(e.ProgressPercentage);
		}
		
		private void BGWorkFinished(object sender, RunWorkerCompletedEventArgs e)
		{
			if( e.Error != null )
				AjouterException( 
				   new ImportExportException( 
                   	"Evénement Tâche terminée",
                   	"La tâche s'est terminée en renvoyant une erreur",
                   	e.Error
                   ));
			
			TâcheAchevée();
		}
		
		private long progrèsInitial = 0;
		private long progrèsFinal = 0;
		private long progrèsActuel = 0;
		private int pourcentagePrécédent = 0;
		private bool forcerAppelSignalement = false;
		
		public void SignalerProgressionTâche( long actuel, long minimum, long maximum, bool forcerSignalement )
		{
			progrèsActuel  = actuel;
			progrèsInitial = minimum;
			progrèsFinal   = maximum;
			forcerAppelSignalement = forcerSignalement;
			
			SignalerProgressionTâche();
		}
		
		public void SignalerProgressionTâche( long actuel, bool forcerSignalement )
		{
			progrèsActuel  = actuel;
			forcerAppelSignalement = forcerSignalement;
			
			SignalerProgressionTâche();
		}
		
		public void IncrémenterProgressionTâche()
		{
			progrèsActuel ++;
			SignalerProgressionTâche();
		}
		
		private void SignalerProgressionTâche()
		{
			long maximum = progrèsFinal - progrèsInitial;
			
			if( maximum < 1 ) 
				maximum = 1;
			
			int pourcentageActuel = (int)( 100 * progrèsActuel / maximum );
			
			if(( pourcentageActuel != pourcentagePrécédent )
			  || forcerAppelSignalement )
			{
				backgroundWorker.ReportProgress( pourcentageActuel );
				pourcentagePrécédent = pourcentageActuel;
			}
			
			System.Threading.Thread.Sleep(5);
		}
	
		
		private void DéfinirPourcentageTâche(int pourcentage)
		{
			if (pourcentage >= 0 && pourcentage <= 100 )
			{
				labelStatut.Text = pourcentage + "% effectués";
				progressBar.Minimum = 0;
				progressBar.Maximum = 100;
				progressBar.Step = 1;
				progressBar.Value = pourcentage;
				btArrêterTâche.Enabled = true;
			}
		}
		
		public void TâcheAchevée()
		{
			labelStatut.Text = "Tâche achevée";
			Text = "Tâche achevée";
			progressBar.Value = Instance.progressBar.Maximum;
			btArrêterTâche.Enabled = false;

			Message("Tâche achevée");

			//if (0 == Instance.dgvExceptions.Rows.Count)
			//	Instance.Visible = false;
			//Instance.cbAfficherErreurs.Enabled = true;
			//Instance.cbAfficherMessages.Enabled = true;

			Instance.btConsulter.Focus();
		}

		#endregion
		#region Méthodes

		private GestionnaireDesTâches()
		{
			InitializeComponent();
			modeCourant = Mode.PAS_INITIALISE;
		}

		private void Consulter( int index )
		{
			this.BringToFront();
			this.btConsulter.Focus();

			if (index >= 0 && index < dgvAffichage.Rows.Count)
			{
				string strDate = dgvAffichage.Rows[index].Cells[ColDate].Value.ToString();
				string strMessage = dgvAffichage.Rows[index].Cells[ColMessage].Value.ToString();

				if (dgvAffichage.Rows[index].Cells[ColType].Value.ToString().Equals("Message"))
				{
					MessageBox.Show(
						strMessage,
						strDate,
						MessageBoxButtons.OK,
						MessageBoxIcon.Information
					);
				}
				else
				{
					MessageBox.Show(
						strMessage,
						strDate,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning
					);
				}
			}
		}

		private void Sauver()
		{
			this.Enabled = false;
			this.Cursor = Cursors.No;

			try
			{
				saveFileDialogErreurs.FileName =
					GestionnaireCSV.Répertoire + "\\" +
					GestionnaireCSV.GénérerNomFichierExport("log");//"erreurs.csv";

				if (DialogResult.OK == saveFileDialogErreurs.ShowDialog())
				{
					DataTable dtExport = new DataTable();

					foreach (DataColumn col in this.dataTable.Columns)
						dtExport.Columns.Add(col.ColumnName);

					foreach (DataGridViewRow row in dgvAffichage.Rows)
						if (row.Visible == true)
							dtExport.ImportRow(
								((DataRowView)row.DataBoundItem).Row
							);

					//if (File.Exists(saveFileDialogAssociation.FileName.ToString()))
					//	File.Delete(saveFileDialogAssociation.FileName.ToString());
					GestionnaireCSV.CaractèreSéparateur = ";";
					GestionnaireCSV.ControleNbColonnes = true;

					GestionnaireCSV.ExporterTamponDansCSV(
						dtExport, //this.dataTable,
						saveFileDialogErreurs.FileName.ToString());
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(
					exception.Message,
					"Erreur d'enregistrement fichier",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
			}
			this.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		private void ArrêterTâche()
		{
			if( null != this.backgroundWorker && this.backgroundWorker.IsBusy )
			{
				this.btArrêterTâche.Enabled = false;
				this.BringToFront();
			   
				this.btConsulter.Enabled = (dgvAffichage.SelectedRows.Count > 0);
				this.labelStatut.Text = "Arrêt Tâche en cours ... La tâche va s'arrêter dès que possible.";
				this.AjouterLigne(typeof(string), "Message", "Tâche interrompue par l'utilisateur");
				
				this.backgroundWorker.CancelAsync();
			}
			// else tâcheAchevée();
		}

		private void AfficherDataTableCorrect()
		{
			try
			{
				if (cbAfficherMessages.Checked
				 && cbAfficherAvertissements.Checked
				 && cbAfficherErreurs.Checked)	   
					this.dgvAffichage.DataSource = this.dataTable;
				else 
				{
					DataTable dtPersonnalisé = new DataTable();
					
					if (cbAfficherMessages.Checked) 		dtPersonnalisé.Merge( this.dtMessages );
					if (cbAfficherAvertissements.Checked) 	dtPersonnalisé.Merge( this.dtAvertissements );
					if (cbAfficherErreurs.Checked) 			dtPersonnalisé.Merge( this.dtErreurs );
					
					this.dgvAffichage.DataSource = dtPersonnalisé;
				}
//				else if (.Checked)  this.dgvAffichage.DataSource = this.dtAvertissements;
//				else if (cbAfficherErreurs.Checked)  this.dgvAffichage.DataSource = this.dtErreurs;
//				else this.dgvAffichage.DataSource = null;

				if (null != this.dgvAffichage.DataSource)
				{
					if( this.dgvAffichage.Columns.Count == 4 )
					{
						this.dgvAffichage.Columns[ColNo].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
						this.dgvAffichage.Columns[ColDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
						this.dgvAffichage.Columns[ColType].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
						this.dgvAffichage.Columns[ColMessage].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					}
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show(
					"[GestionnaireDesTâches/AfficherDataTableCorrect] : " + ex.Message,
					ex.ToString(),
					MessageBoxButtons.OK, MessageBoxIcon.Error
				);
			}

			this.stopScroll = false;
		}

		private void QuitterApplication()
		{
			try
			{
				ArrêterTâche();
				System.GC.Collect();
				GestionnaireExcel.FermerExcel();
				FormPrincipale.Instance.Close();
				this.Close();
			}
			catch (Exception) { }

			Application.Exit();
		}

		#endregion
		#region Evénements Graphiques

		private void FormGestionnaireExceptions_Load(object sender, EventArgs e)
		{
			InitialiserMode(Mode.GESTIONNAIRE_TACHE);
		}
		private void FormGestionnaireExceptions_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Visible = false;
			FormPrincipale.Instance.BringToFront();
		}
		private void btOK_Click(object sender, EventArgs e)
		{
			if( this.backgroundWorker.IsBusy )
				WindowState = FormWindowState.Minimized;
			else
				this.Visible = false;
			
			FormPrincipale.Instance.BringToFront();
		}
		private void btOK_MouseDown(object sender, MouseEventArgs e)
		{
			this.Visible = false;
			FormPrincipale.Instance.BringToFront();
		}
		private void btPurger_Click(object sender, EventArgs e)
		{
			InitialiserMode(modeCourant);
		}
		private void btPurger_MouseDown(object sender, MouseEventArgs e)
		{
			InitialiserMode(modeCourant);
		}

		private void btSauver_Click(object sender, EventArgs e)
		{
			//btSauver.Enabled = false;
			Sauver();
		}
		private void btSauver_MouseDown(object sender, MouseEventArgs e)
		{
			//btSauver.Enabled = false;
			Sauver();
		}
		private void btArrêterTâche_Click(object sender, EventArgs e)
		{
			ArrêterTâche();
		}
		private void btArrêterTâche_MouseDown(object sender, MouseEventArgs e)
		{
			ArrêterTâche();
		}
		private void dgvExceptions_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			this.stopScroll = false;
			Consulter(e.RowIndex);
		}


		private void btQuitterApplication_Click(object sender, EventArgs e)
		{
			QuitterApplication();
		}
		private void btQuitterApplication_MouseDown(object sender, MouseEventArgs e)
		{
			QuitterApplication();
		}

		private void btConsulter_Click(object sender, EventArgs e)
		{
			btConsulter.Enabled = false;

			if (dgvAffichage.SelectedRows.Count > 0)
				Consulter(dgvAffichage.SelectedRows[0].Index);

			btConsulter.Enabled = true;
		}
		private void btConsulter_MouseDown(object sender, MouseEventArgs e)
		{
			btConsulter.Enabled = false;

			if (dgvAffichage.SelectedRows.Count > 0)
				Consulter(dgvAffichage.SelectedRows[0].Index);

			btConsulter.Enabled = true;
		}
		private void dgvExceptions_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			//this.stopScroll = true;
			btConsulter.Enabled = (dgvAffichage.SelectedRows.Count > 0);
		}
		
		private void cbAfficherXXX_CheckedChanged(object sender, EventArgs e)
		{
			this.Enabled = false;
			this.Cursor = Cursors.No;

			AfficherDataTableCorrect();
			//CurrencyManager currencyManager = (CurrencyManager) BindingContext[dgvAffichage.DataSource];
			//currencyManager.SuspendBinding();

			//foreach (DataGridViewRow row in dgvAffichage.Rows)
			//	if (row.Cells[ColType].Value.ToString().Equals("Message"))
			//		row.Visible = cbAfficherMessages.Checked;

			//currencyManager.ResumeBinding();

			this.Cursor = Cursors.Default;
			this.Enabled = true;
		}

		private void GestionnaireDesTâches_SizeChanged(object sender, EventArgs e)
		{
			//if (this.WindowState == FormWindowState.Minimized)
			//	this.Visible = false;
		}


		private void dgvExceptions_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
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
			}// */

			try
			{
				if (ColNoIndex == e.Column.Index)
				{
					int valeur1 = int.Parse(e.CellValue1.ToString());
					int valeur2 = int.Parse(e.CellValue2.ToString());

					e.SortResult = valeur1.CompareTo(valeur2);
					e.Handled = true;
				}
				/*else if (ColDateIndex == e.Column.Index)
				{
					//DateTimeConverter dt = new DateTimeConverter(

					DateTime dt1 = DateTime.Parse(e.CellValue1.ToString());
					DateTime dt2 = DateTime.Parse(e.CellValue2.ToString());

					e.SortResult = dt1.CompareTo(dt2);
					e.Handled = true;
				}//*/
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		private void dgvExceptions_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			this.stopScroll = true;
		}

		private void cbAfficherMessages_MouseDown(object sender, MouseEventArgs e)
		{
			this.cbAfficherMessages.Checked = !this.cbAfficherMessages.Checked;
		}

		private void cbAfficherAvertissements_MouseDown(object sender, MouseEventArgs e)
		{
			this.cbAfficherAvertissements.Checked = !this.cbAfficherAvertissements.Checked; 
		}

		private void cbAfficherErreurs_MouseDown(object sender, MouseEventArgs e)
		{
			this.cbAfficherErreurs.Checked = !this.cbAfficherErreurs.Checked;
		}
	}
	#endregion
}


/*




namespace Import_Export_CSV
{
	public partial class GestionnaireDesTâches : Form
	{
#region Static

		private static GestionnaireDesTâches gestionnaire = null;

		public static GestionnaireDesTâches Instance
		{
			get
			{
				if (null == gestionnaire)
					gestionnaire = new GestionnaireDesTâches();

				return gestionnaire;
			}
		}

		public static bool ArrêtTâcheDemandé
		{
			get
			{
				return Instance.arrêtDemandé;
			}
		}

		public static void ajouterException( Exception ex )
		{
			Instance.ajouterLigne(ex.GetType().ToString(), ex.Message);
		}

		public static void ajouterMessage(string message)
		{
			Instance.ajouterLigne("Message", message);
		}

		public static void démarrerTâcher( int valMin, int valMax )
		{
			Instance.arrêtDemandé = false;
			Instance.btArrêterTâche.Enabled = true;

			Instance.progressBar.Minimum = valMin;
			Instance.progressBar.Maximum = valMax;
			Instance.progressBar.Step = 1;
			Instance.progressBar.Value = valMin;
		}
		
		//public delegate void TypeDéléguéIncrémenterTâche();
		public static void incrémenterTâche()
		{
			Instance.safeIncrémenterTâche();

			Application.DoEvents();
			System.Threading.Thread.Sleep(1000);
		}

		public static void tâcheAchevée()
		{
			Instance.progressBar.Value = Instance.progressBar.Maximum;
			Instance.btArrêterTâche.Enabled = false;
		}

#endregion
#region Instance
		private bool arrêtDemandé = false;
		private DataTable dataTable = null;
		
		private void ajouterLigne( string titre, string message )
		{
			DataRow row = this.dataTable.NewRow();

			row[0] = DateTime.Now;
			row[1] = titre;
			row[2] = message;

			this.dataTable.Rows.InsertAt(row, 0);
			this.dgvExceptions.InvalidateRow(0);
			this.dgvExceptions.Rows[0].Selected = true;
			this.Visible = true;
			this.Show();
		}

		private void Initialiser()
		{
			this.progressBar.Minimum = 0;
			this.progressBar.Maximum = 10;
			this.progressBar.Step = 1;
			this.progressBar.Value = 0;

			this.dataTable = new DataTable("GestionnaireDExceptions");
			this.dataTable.Columns.Add( "Date" );
			this.dataTable.Columns.Add( "Type d'exception" );
			this.dataTable.Columns.Add( "Message" );
			
			this.dgvExceptions.DataSource = this.dataTable;

			this.Visible = true;
			this.Show();
		}

		private delegate void TypeDéléguéVoidSansParam();
		private void safeIncrémenterTâche()
		{
			if (this.progressBar.InvokeRequired)
			{
				TypeDéléguéVoidSansParam délégué = new TypeDéléguéVoidSansParam(safeIncrémenterTâche);
				this.Invoke(délégué);
			}
			else
			{
				this.progressBar.PerformStep();
				Application.DoEvents();
				System.Threading.Thread.Sleep(1000);
			}
		}

#endregion
#region Evénements Graphiques
		private GestionnaireDesTâches()
		{
			InitializeComponent();
			Initialiser();
		}

		private void FormGestionnaireExceptions_Load(object sender, EventArgs e)
		{
		}

		private void FormGestionnaireExceptions_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Visible = false;
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void btPurger_Click(object sender, EventArgs e)
		{
			Initialiser();
		}

		private void btSauver_Click(object sender, EventArgs e)
		{
			this.Enabled = false;
			this.Cursor = Cursors.No;
			try
			{
				saveFileDialogErreurs.FileName = "erreurs.csv";

				if (DialogResult.OK == saveFileDialogErreurs.ShowDialog())
				{
					//if (File.Exists(saveFileDialogAssociation.FileName.ToString()))
					//	File.Delete(saveFileDialogAssociation.FileName.ToString());

					GestionnaireCSV.ExporterTamponDansCSV(
						this.dataTable,
						saveFileDialogErreurs.FileName.ToString(),
						";", true);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(
					exception.Message,
					"Erreur d'enregistrement fichier",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
			}
			this.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		private void btArrêterTâche_Click(object sender, EventArgs e)
		{
			this.btArrêterTâche.Enabled = false;
			this.arrêtDemandé = true;
		}
	}
#endregion
}



// */