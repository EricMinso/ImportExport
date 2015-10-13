using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace Import_Export_Universel
{
    public partial class FormImportDeMasse : Form
    {
        #region Variables
        private bool estInitialisé;
        //private bool validation;

        private List<string> lstTablesBDOrigine;
        private List<string> lstFichiersImportOrigine;
        private List<string> lstFichiersAssociationOrigine;
        private AssociationTablesBDFichiersImport association;
        private DataTable dtAssociation;

        public List<string> TablesBDOrigine
        {
            get { return lstTablesBDOrigine; }
            set { lstTablesBDOrigine = value; }
        }
        public List<string> ChampsTamponOrigine
        {
            get { return lstFichiersImportOrigine; }
            set { lstFichiersImportOrigine = value; }
        }

        #endregion
        #region Accesseurs

        public AssociationTablesBDFichiersImport Association
        {
            get { return association; }
        }

        #endregion
        #region Constructeurs

        public FormImportDeMasse()
        {
            InitializeComponent();
            
            this.estInitialisé = false;
            //this.validation = false;
            this.lstTablesBDOrigine = new List<string>(); 
            this.lstFichiersImportOrigine = new List<string>(); 
        }

        #endregion
        #region Méthodes

        private void InitialiserDonnées()
        {
            //GestionnaireDesTâches.démarrerTâche();

            this.association = new AssociationTablesBDFichiersImport();

            this.dtAssociation = new DataTable();
            this.dtAssociation.Columns.Add("Tables de Base de Données");
            this.dtAssociation.Columns.Add("Fichiers d'Import");
            this.dtAssociation.Columns.Add("Fichiers d'Association");

            this.lstTablesBDOrigine = GestionnaireBD.Connexion.ListeDesTables();
            this.lstFichiersImportOrigine = GestionnaireCSV.ListeDesFichiers("csv");
            this.lstFichiersAssociationOrigine = GestionnaireCSV.ListeDesFichiers("xml");

            //GestionnaireDesTâches.addMessage("Répertoire : " + GestionnaireCSV.Répertoire);
            //GestionnaireDesTâches.addMessage("Fichiers CSV : " + lstFichiersImportOrigine.Count);
            //GestionnaireDesTâches.addMessage("Fichiers XML : " + lstFichiersAssociationOrigine.Count);
            
            
            this.dgvAssociation.DataSource = this.dtAssociation;

            // Pour chaque table
            for(int i = 0; i < lstTablesBDOrigine.Count; i++)
            {
                bool fichierTrouvé = false;
                bool assoceTrouvée = false;

                string strTableBD = this.lstTablesBDOrigine[i];
                string strFichierImport = AssociationTablesBDFichiersImport.AucunFichierAssociation;
                string strFichierAssociation = AssociationTablesBDFichiersImport.AucunFichierAssociation;

                Regex matchImport = new Regex(
                    "^" + strTableBD + "_[0-9]+-[0-9]+-[0-9]+_[hms0-9]+_export.csv$",
                    RegexOptions.IgnoreCase);

                Regex matchImport2èmeChance = new Regex(
                    "^" + strTableBD + ".csv$",
                    RegexOptions.IgnoreCase);

                Regex matchAssociation = new Regex(
                    "^" + strTableBD + "_association.xml$",
                    RegexOptions.IgnoreCase);

                // Pour chaque fichier
                foreach (string fImport in lstFichiersImportOrigine)
                {
                    if (matchImport.IsMatch(fImport))
                    {
                        strFichierImport = fImport;
                        fichierTrouvé = true;
                        break;
                    }
                }
                // Pour chaque fichier, 2ème chance
                if( fichierTrouvé == false )
                    foreach (string fImport in lstFichiersImportOrigine)
                    {
                        if (matchImport2èmeChance.IsMatch(fImport))
                        {
                            strFichierImport = fImport;
                            fichierTrouvé = true;
                            break;
                        }
                   }

                // Pour chaque association
                foreach( string fAsso in lstFichiersAssociationOrigine )
                {
                    if( matchAssociation.IsMatch( fAsso ))
                    {
                        strFichierAssociation = fAsso;
                        assoceTrouvée = true;
                        break;
                    }
                }

                DataRow row = this.dtAssociation.NewRow();
                row[0] = strTableBD;
                row[1] = strFichierImport;
                row[2] = strFichierAssociation;
                this.dtAssociation.Rows.Add(row);

                //this.dgvAssociation.Rows[i].Cells[0].Style.BackColor = Color.Thistle; ////Color.Salmon;

                if( !fichierTrouvé )
                    this.dgvAssociation.Rows[i].Cells[1].Style.BackColor = Color.LightCoral;

                if (!assoceTrouvée)
                    this.dgvAssociation.Rows[i].Cells[2].Style.BackColor = Color.LightCoral;
                //GestionnaireDesTâches.addMessage("Nouvelle Ligne " + strTableBD+" "+strFichierImport+" "+strFichierAssociation);

            }

            this.estInitialisé = true;
        }

        private void DéfinirAssociation()
        {
            this.association.Clear();

            foreach( DataRow row in this.dtAssociation.Rows )
            {
                string strTableBD = row[0].ToString();
                string strFichierImport = row[1].ToString();
                string strFichierAssociation = row[2].ToString();

                this.association.Add(strTableBD, strFichierImport, strFichierAssociation);
            }
        }

        #endregion
        #region Evénements

        private void FormAssociation_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;

                if (!estInitialisé)
                    InitialiserDonnées();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        "Erreur : " + ex.Message,
                        "Impossible de charger",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
            }
        }

        private void FormAssociation_FormClosing(object sender, FormClosingEventArgs e)
        {
            DéfinirAssociation();
        }

        private void btValiderAssociation_Click(object sender, EventArgs ea)
        {
            //this.validation = true;
        }

        private void dgvAssociation_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dgvAssociation.SelectedCells.Count > 0
                && !dgvAssociation.IsCurrentCellInEditMode )
                {
                    Point clientPoint = dgvAssociation.PointToClient(new Point(e.X, e.Y));

                    DataGridViewCell selectedCell = dgvAssociation.SelectedCells[0];
                    DataGridView.HitTestInfo hit = dgvAssociation.HitTest(e.X, e.Y);
                    int row = hit.RowIndex;
                    int col = hit.ColumnIndex;

                    try
                    {
                        if (
                               selectedCell.RowIndex    != row 
                            && selectedCell.ColumnIndex == col
                            && row >= 0
                            && col >= 0
                            && row < dgvAssociation.RowCount
                            && col < dgvAssociation.ColumnCount
                            )
                        {
                            int idxCur;

                            if( selectedCell.RowIndex < row )
                            {
                                for (idxCur = selectedCell.RowIndex; idxCur < row; idxCur++)
                                {
                                    DataGridViewCell cell1 = dgvAssociation.Rows[idxCur].Cells[col];
                                    DataGridViewCell cell2 = dgvAssociation.Rows[idxCur + 1].Cells[col];

                                    object tempValue = cell1.Value;
                                    cell1.Value = cell2.Value;
                                    cell2.Value = tempValue;

                                    cell1.Style.BackColor = Color.LightGray;
                                    cell2.Style.BackColor = Color.LightGray;

                                    // Cas particulier où on déplace la colonne 0 : on déplace toute la ligne
                                    if (col == 0)
                                    {
                                        DataGridViewCell cell1col1 = dgvAssociation.Rows[idxCur].Cells[1];
                                        DataGridViewCell cell2col1 = dgvAssociation.Rows[idxCur + 1].Cells[1];
                                        DataGridViewCell cell1col2 = dgvAssociation.Rows[idxCur].Cells[2];
                                        DataGridViewCell cell2col2 = dgvAssociation.Rows[idxCur + 1].Cells[2];

                                        object tempValueCol1 = cell1col1.Value;
                                        object tempValueCol2 = cell1col2.Value;
                                        cell1col1.Value = cell2col1.Value;
                                        cell1col2.Value = cell2col2.Value;
                                        cell2col1.Value = tempValueCol1;
                                        cell2col2.Value = tempValueCol2;
                                        cell1col1.Style.BackColor = Color.LightGray;
                                        cell1col2.Style.BackColor = Color.LightGray;
                                        cell2col1.Style.BackColor = Color.LightGray;
                                        cell2col2.Style.BackColor = Color.LightGray;
                                    }
                                }
                            }
                            else
                            {
                                for (idxCur = selectedCell.RowIndex; idxCur > row; idxCur--)
                                {
                                    DataGridViewCell cell1 = dgvAssociation.Rows[idxCur].Cells[col];
                                    DataGridViewCell cell2 = dgvAssociation.Rows[idxCur - 1].Cells[col];

                                    object tempValue = cell1.Value;
                                    cell1.Value = cell2.Value;
                                    cell2.Value = tempValue;

                                    cell1.Style.BackColor = Color.LightGray;
                                    cell2.Style.BackColor = Color.LightGray;


                                    // Cas particulier où on déplace la colonne 0 : on déplace toute la ligne
                                    if (col == 0)
                                    {
                                        DataGridViewCell cell1col1 = dgvAssociation.Rows[idxCur].Cells[1];
                                        DataGridViewCell cell2col1 = dgvAssociation.Rows[idxCur - 1].Cells[1];
                                        DataGridViewCell cell1col2 = dgvAssociation.Rows[idxCur].Cells[2];
                                        DataGridViewCell cell2col2 = dgvAssociation.Rows[idxCur - 1].Cells[2];

                                        object tempValueCol1 = cell1col1.Value;
                                        object tempValueCol2 = cell1col2.Value;
                                        cell1col1.Value = cell2col1.Value;
                                        cell1col2.Value = cell2col2.Value;
                                        cell2col1.Value = tempValueCol1;
                                        cell2col2.Value = tempValueCol2;
                                        cell1col1.Style.BackColor = Color.LightGray;
                                        cell1col2.Style.BackColor = Color.LightGray;
                                        cell2col1.Style.BackColor = Color.LightGray;
                                        cell2col2.Style.BackColor = Color.LightGray;
                                    }
                                }
                            }

                            DataGridViewCell pointedCell = dgvAssociation.Rows[row].Cells[col];
                            selectedCell.Selected = false;
                            pointedCell.Selected = true;
                            pointedCell.Style.BackColor = Color.Orange;
                        }
                    }
                    catch (Exception)
                    {
                    }                    
                }
            }
        }

        private void dgvAssociation_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeAll;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dgvAssociation_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dgvAssociation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (dgvAssociation.SelectedCells.Count > 0)
                    dgvAssociation.Rows.RemoveAt(dgvAssociation.SelectedCells[0].RowIndex);
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            InitialiserDonnées();
        }

        private void btSauverAssociation_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Cursor = Cursors.No;
            try
            {
                DéfinirAssociation();
                saveFileDialogAssociation.FileName = GestionnaireBD.Connexion.NomConnexion +"_import_de_masse.xml";

                if (DialogResult.OK == saveFileDialogAssociation.ShowDialog())
                {
                    /*GestionnaireCSV.CaractèreSéparateur="|";
                    GestionnaireCSV.ControleNbColonnes=true;
                    GestionnaireCSV.TrimSpaces=true;

                    GestionnaireCSV.ExporterTamponDansCSV(
                        dtAssociation,
                        saveFileDialogAssociation.FileName.ToString()
                    );//*/

                    this.association.Enregistrer(saveFileDialogAssociation.FileName);
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

        private void btChargerAssociation_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Cursor = Cursors.No;

            try
            {
                // Ouverture du fichier
                openFileDialogAssociation.FileName = GestionnaireBD.Connexion.NomConnexion + "_import_de_masse.xml";

                if (DialogResult.OK == openFileDialogAssociation.ShowDialog())
                {
                    this.association.Charger(openFileDialogAssociation.FileName);

                    this.dtAssociation = new DataTable();
                    this.dtAssociation.Columns.Add("Tables de Base de Données");
                    this.dtAssociation.Columns.Add("Fichiers d'Import");
                    this.dtAssociation.Columns.Add("Fichiers d'Association");

                    for (int i = 0; i < this.association.TablesBD.Count; i++)
                    {
                        DataRow newRow = this.dtAssociation.NewRow();

                        string strTableBD = this.association.TablesBD[i];
                        string strFichierImport = this.association.FichiersImport[i];
                        string strFichierAssociation = this.association.FichiersAssociation[i];

                        if (!this.lstTablesBDOrigine.Contains(strTableBD))
                        {
                            if (DialogResult.No == MessageBox.Show(
                                "La BD ne contient pas la table " + strTableBD +
                                "\nContinuer ?",
                                "Erreur de table",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning))
                                    throw new Exception("Erreur de table BD : interruption par l'utilisateur");
                        }

                        if( !strFichierImport.Equals(AssociationTablesBDFichiersImport.AucunFichierAssociation)
                         && !this.lstFichiersImportOrigine.Contains(strFichierImport))
                        {
                            if (DialogResult.No == MessageBox.Show(
                                "Le répertoire ne contient pas le fichier " + strFichierImport +
                                "\nContinuer ?",
                                "Erreur de champ",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning))
                                throw new Exception("Erreur de fichier import: interruption par l'utilisateur");
                        }
                        if( !strFichierAssociation.Equals( AssociationTablesBDFichiersImport.AucunFichierAssociation )
                         && !this.lstFichiersAssociationOrigine.Contains(strFichierAssociation))
                        {
                            if (DialogResult.No == MessageBox.Show(
                                "Le répertoire ne contient pas le fichier " + strFichierAssociation +
                                "\nContinuer ?",
                                "Erreur de champ",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning))
                                throw new Exception("Erreur de fichier association : interruption par l'utilisateur");
                        }

                        newRow[0] = strTableBD;
                        newRow[1] = strFichierImport;
                        newRow[2] = strFichierAssociation;
                        this.dtAssociation.Rows.Add(newRow);
                    }
                    /*
                    StreamReader srFichier = new StreamReader(openFileDialogAssociation.OpenFile(), System.Text.Encoding.Default, true);

                    GestionnaireCSV.CaractèreSéparateur = "|";
                    GestionnaireCSV.ControleNbColonnes = true;
                    GestionnaireCSV.TrimSpaces = true;

                    DataTable dtFichier = GestionnaireCSV.OuvrirFichierCSV(srFichier);

                    if (2 != dtFichier.Columns.Count
                     || 0 >= dtFichier.Rows.Count)
                    // || this.lstChampsBD.Count < dtFichier.Rows.Count)
                    {
                        throw new Exception("Ceci ne semble pas être un fichier d'association valide");
                    }

                    foreach (DataRow row in dtFichier.Rows)
                    {
                        string strChampBD = row[0].ToString();
                        string strChampTampon = row[1].ToString();

                        if (!this.lstChampsBDOrigine.Contains(strChampBD))
                        {
                            if (DialogResult.No == MessageBox.Show(
                                "La BD ne contient pas le champ " + strChampBD +
                                "\nContinuer ?",
                                "Erreur de champ",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning))
                                throw new Exception("Erreur de champ BD : interruption par l'utilisateur");
                        }
                        if (!this.lstChampsTamponOrigine.Contains(strChampTampon))
                        {
                            if (DialogResult.No == MessageBox.Show(
                                "Le tampon ne contient pas le champ " + strChampTampon +
                                "\nContinuer ?",
                                "Erreur de champ",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning))
                                throw new Exception("Erreur de champ Tampon : interruption par l'utilisateur");
                        }
                    }
                    //*/

                    //this.dtAssociation = dtFichier;
                    this.dgvAssociation.DataSource = this.dtAssociation;
                }
            }
            catch (Exception exception)
            {
                // Gestion des erreurs
                MessageBox.Show(
                    exception.Message,
                    "Erreur de chargement du fichier d'association",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                 );
            }
            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }
        #endregion
    }
}
