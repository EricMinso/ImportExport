using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Import_Export_Universel
{
    public partial class FormAssociation : Form
    {
        #region Variables

        private bool estInitialisé;
        private bool validation;
        private string strNom;

        private List<string> lstChampsBDOrigine;
        private List<string> lstChampsTamponOrigine;

        //private List<string> lstClésPrimaires;
        //private NameValueCollection associationBdTampon;
        private AssociationChampsBDTampon association;

        private DataTable dtAssociation;

        public List<string> ChampsBDOrigine
        {
            get { return lstChampsBDOrigine; }
            set
            {
                lstChampsBDOrigine = value;
                //lstChampsBDModif = new List<string>();
                //lstChampsBDModif.AddRange(value);
            }
        }
        public List<string> ChampsTamponOrigine
        {
            get { return lstChampsTamponOrigine; }
            set
            {
                lstChampsTamponOrigine = value;
                //lstChampsTamponModif = new List<string>();
                //lstChampsTamponModif.AddRange(value);
            }
        }//*/

        #endregion
        #region Accesseurs

        public AssociationChampsBDTampon Association
        {
            get { return association; }
            //set { associationBdTampon = value; }
        }
        /*public NameValueCollection AssociationBdTampon
        {
            get { return associationBdTampon; }
            set { associationBdTampon = value; }
        }

        public List<string> ClésPrimaires
        {
            get { return lstClésPrimaires; }
            set { lstClésPrimaires = value; }
        }

        public bool InsertsOK
        {
            get
            {
                return this.rbINSERTonly.Checked || this.rbInsertUpdate.Checked ;
            }
        }
        public bool UpdatesOK
        {
            get
            {
                return this.rbUPDATEonly.Checked || this.rbInsertUpdate.Checked ;
            }
        }

        public string ConditionsAdditionnelles
        {
            get
            {
                return this.tbConditionsOptionnelles.Text;
            }
        }

        public bool ArretSiErreurDInsertion
        {
            get
            {
                return this.cbArretSiErreurDInsertion.Checked;
            }
            set 
            {
                this.cbArretSiErreurDInsertion.Checked = value;
            }
        }// */

        #endregion
        #region Constructeurs

        public FormAssociation( string strNomTable )//, List<string> _lstColonnesBD, List<string> _lstColonnesTampon )
        {
            InitializeComponent();

            this.strNom = strNomTable;
            this.lstChampsBDOrigine = new List<string>(); // _lstColonnesBD;
            this.lstChampsTamponOrigine = new List<string>(); //_lstColonnesTampon;
            this.estInitialisé = false;
        }

        #endregion
        #region Méthodes

        private void InitialiserDonnées()
        {
            this.validation = false;

            //this.lstClésPrimaires = new List<string>();
            //this.associationBdTampon = null;
            this.association = new AssociationChampsBDTampon();

            this.dtAssociation = new DataTable();
            this.dtAssociation.Columns.Add("Champs de Base de Données");
            this.dtAssociation.Columns.Add("Colonnes du Tampon");

            /*this.lstChampsBDModif = new List<string>();
            this.lstChampsBDModif.AddRange(this.ChampsBDOrigine);

            this.lstChampsTamponModif = new List<string>();
            this.lstChampsTamponModif.AddRange(this.ChampsTamponOrigine);//*/

            int maxSize = Math.Max(lstChampsBDOrigine.Count, lstChampsTamponOrigine.Count);

            for (int i = 0; i < maxSize; i++)
            {
                string strNomChampBD = "";
                string strNomChampTampon = "";

                try { strNomChampBD = this.lstChampsBDOrigine[i]; }
                catch (Exception ) { }

                try { strNomChampTampon = this.lstChampsTamponOrigine[i]; }
                catch (Exception ) { }

                DataRow row = this.dtAssociation.NewRow();
                row[0] = strNomChampBD;
                row[1] = strNomChampTampon;
                this.dtAssociation.Rows.Add(row);
            }

            this.dgvAssociation.DataSource = this.dtAssociation;
            this.lbClésPrimaires.Items.Clear();
            this.tbConditionsOptionnelles.Clear();

            this.estInitialisé = true;
        }

        /*private void AjouterCléPrimaire( string strCléPrimaire )
        {
            if (!this.association.ClésPrimaires.Contains(strCléPrimaire))
                this.lbClésPrimaires.Items.Add(strCléPrimaire);
        }

        private void SupprimerCléPrimaire( string strCléPrimaire )
        {
            if (this.association.ClésPrimaires.Contains(strCléPrimaire))
            {
                //this.association.ClésPrimaires.Remove(strCléPrimaire);
                this.lbClésPrimaires.Items.Remove(strCléPrimaire);
            }
            //else MessageBox.Show("Nan, contient pas '"+strCléPrimaire+"'");
        }//*/

        private void DéfinirAssociation()
        {
            this.association.Clear();
            this.association.InsertsOK = (this.rbINSERTonly.Checked || this.rbInsertUpdate.Checked);
            this.association.UpdatesOK = (this.rbUPDATEonly.Checked || this.rbInsertUpdate.Checked);
            this.association.ConditionsAdditionnelles = this.tbConditionsOptionnelles.Text;

            foreach( string strCléPrimaire in this.lbClésPrimaires.Items )
                this.association.ClésPrimaires.Add(strCléPrimaire);

            foreach( DataRow row in this.dtAssociation.Rows )
            {
                string strChampBD = row[0].ToString();
                string strChampTampon = row[1].ToString();

                if( strChampBD.Length > 0 && strChampBD != "NULL"
                 && strChampTampon.Length > 0 && strChampTampon != "NULL" )
                    this.association.Add(strChampBD, strChampTampon);
            }
        }

        #endregion
        #region Evénements

        private void FormAssociation_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;

                if( !estInitialisé )
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

            if (true == this.validation)
            {
                // L'opération UPDATE nécessite une clé primaire
                if (this.rbInsertUpdate.Checked || this.rbUPDATEonly.Checked || this.rbTestCléPrimaire.Checked )
                {
                    if (0 == this.association.ClésPrimaires.Count)
                    {
                        MessageBox.Show(
                            "Il faut définir au moins une clé primaire pour les opérations UPDATE sur la base de données",
                            "Validation impossible",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                         );
                        e.Cancel = true;
                    }
                }

                // Si les clés primaires n'appartiennent pas à la BD
                foreach (string strCléPrimaire in this.association.ClésPrimaires)
                {
                    if (!this.lstChampsBDOrigine.Contains(strCléPrimaire))
                    {
                        MessageBox.Show(
                            "La clé primaire " + strCléPrimaire + " est invalide et ne peut pas être utilisée",
                            "Validation impossible",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }

                // Si les champs n'appartiennent pas à la BD ou au tampon
                for (int i = 0; i < this.association.ChampsBD.Count; i++)
                {
                    string strChampBD = this.association.ChampsBD[i].ToString();
                    string strChampTampon = this.association.ChampsTampon[i].ToString();

                    if (!this.lstChampsBDOrigine.Contains(strChampBD))
                    {
                        MessageBox.Show(
                            "La BD ne contient pas le champ " + strChampBD,
                            "Validation impossible",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                    if (!this.lstChampsTamponOrigine.Contains(strChampTampon))
                    {
                        MessageBox.Show(
                            "Le tampon ne contient pas le champ " + strChampTampon,
                            "Validation impossible",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
        }

        private void btValiderAssociation_Click(object sender, EventArgs ea)
        {
            this.validation = true;
        }

        private void btSupprimerCléPrimaire_Click(object sender, EventArgs ea)
        {
            string strCléPrimaire;

            try
            {
                strCléPrimaire = this.lbClésPrimaires.SelectedItem.ToString();
                this.lbClésPrimaires.Items.Remove(strCléPrimaire);
                //SupprimerCléPrimaire(strCléPrimaire);
            }
            catch (Exception )
            { 
                //MessageBox.Show(ex.Message);
            }
        }

        private void btAjouterCléPrimaire_Click(object sender, EventArgs ea)
        {
            string strCléPrimaire;

            try
            {
                strCléPrimaire = this.dgvAssociation.SelectedCells[0].Value.ToString();
                this.lbClésPrimaires.Items.Add(strCléPrimaire);
                //strCléPrimaire = this.dgvAssociation.SelectedCells[0].Value.ToString();
                //AjouterCléPrimaire(strCléPrimaire);
            }
            catch (Exception )
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void lbClésPrimaires_DoubleClick(object sender, EventArgs ea)
        {
            btSupprimerCléPrimaire_Click( sender, ea);
        }

        private void btAjouterChamp_Click(object sender, EventArgs e)
        {
            string strChamp;

            try
            {
                strChamp = this.dgvAssociation.SelectedCells[0].Value.ToString();
                tbConditionsOptionnelles.Text = tbConditionsOptionnelles.Text + " " + strChamp + " ";
            }
            catch (Exception )
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btEffacer_Click(object sender, EventArgs e)
        {
            tbConditionsOptionnelles.Text = "";
        }

        private void dgvAssociation_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //dgvAssociation.
                
                //dgvAssociation.CurrentCell.IsInEditMode
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
                                    //cell1.Style.BackColor = Color.Yellow;
                                    //cell2.Style.BackColor = Color.Yellow;
                                    cell1.Style.BackColor = Color.LightGray;
                                    cell2.Style.BackColor = Color.LightGray;
                                }
                            }

                            DataGridViewCell pointedCell = dgvAssociation.Rows[row].Cells[col];
                            selectedCell.Selected = false;
                            pointedCell.Selected = true;

                            //selectedCell.Style.BackColor = Color.Yellow;
                            pointedCell.Style.BackColor = Color.Orange;
                        }
                    }
                    catch (Exception)
                    {
                        //selectedCell.Value = "sel("+row+","+col+") ptd("+row2+","+col2+")";
                        //label2.Text = ex.Message;
                    }//*/
                    
                }
            }
        }

        private void dgvAssociation_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeAll;

                //if (dgvAssociation.SelectedCells.Count > 0)
                //{
                //    DragDropEffects dropEffect = dgvAssociation.DoDragDrop(
                //        dgvAssociation.SelectedCells[0],
                //        DragDropEffects.All
                //        );
                //}
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dgvAssociation_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAssociation.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = dgvAssociation.SelectedCells[0];

                btAjouterCléPrimaire.Enabled = (selectedCell.ColumnIndex == 0);
                btAjouterChamp.Enabled = (selectedCell.ColumnIndex == 0);
            }
        }

        private void lbClésPrimaires_SelectedIndexChanged(object sender, EventArgs e)
        {
            btSupprimerCléPrimaire.Enabled = ( lbClésPrimaires.SelectedItem != null );
        }

        private void tbConditionsOptionnelles_TextChanged(object sender, EventArgs e)
        {
            btEffacer.Enabled = (tbConditionsOptionnelles.Text.Length > 0);
        }

        //private void dgvAssociation_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    MessageBox.Show("Code : " + ((int)e.KeyChar));
        //    //if (.ToString().Equals( Keys.Delete.ToString() ))
        //    //{
        //    //}
        //}

        private void dgvAssociation_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("Code : " + ((int)e.KeyCode));

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
                saveFileDialogAssociation.FileName = this.strNom + "_association.xml";

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
                openFileDialogAssociation.FileName = this.strNom + "_association.xml";

                if (DialogResult.OK == openFileDialogAssociation.ShowDialog())
                {
                    this.association.Charger(openFileDialogAssociation.FileName);

                    this.rbINSERTonly.Checked = this.association.InsertsOK && ! this.association.UpdatesOK;
                    this.rbUPDATEonly.Checked = ! this.association.InsertsOK && this.association.UpdatesOK;
                    this.rbInsertUpdate.Checked = this.association.InsertsOK && this.association.UpdatesOK;
                    this.rbTestCléPrimaire.Checked = ! this.association.InsertsOK && ! this.association.UpdatesOK;
                    this.tbConditionsOptionnelles.Text = this.association.ConditionsAdditionnelles;
                    this.lbClésPrimaires.Items.Clear();

                    foreach (string strCléPrimaire in this.association.ClésPrimaires)
                    {
                        this.lbClésPrimaires.Items.Add(strCléPrimaire);

                        if (!this.lstChampsBDOrigine.Contains(strCléPrimaire))
                        {
                            if (DialogResult.No == MessageBox.Show(
                                "La BD ne contient pas le champ " + strCléPrimaire +
                                "\nContinuer ?",
                                "Erreur de champ",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning))
                                    throw new Exception("Erreur de champ BD : interruption par l'utilisateur");
                        }
                    }

                    this.dtAssociation = new DataTable();
                    this.dtAssociation.Columns.Add("Champs de Base de Données");
                    this.dtAssociation.Columns.Add("Colonnes du Tampon");

                    for (int i = 0; i < this.association.ChampsBD.Count; i++)
                    {
                        DataRow newRow = this.dtAssociation.NewRow();

                        string strChampBD = this.association.ChampsBD[i].ToString();
                        string strChampTampon = this.association.ChampsTampon[i].ToString();

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

                        newRow[0] = strChampBD;
                        newRow[1] = strChampTampon;
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

        private void btAnnulerAssociation_Click(object sender, EventArgs e)
        {
            this.validation = false;
        }

        /*
        private void dgvAssociation_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void dgvAssociation_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void dgvAssociation_DragLeave(object sender, EventArgs e)
        {

        }

        private void dgvAssociation_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        } //*/
        #endregion
    }
}
