namespace Import_Export_CSV
{
    partial class GestionnaireDesTâches
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionnaireDesTâches));
            this.tlpErrMaster = new System.Windows.Forms.TableLayoutPanel();
            this.dgvAffichage = new System.Windows.Forms.DataGridView();
            this.tlpProgressBarre = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btArrêterTâche = new System.Windows.Forms.Button();
            this.labelStatut = new System.Windows.Forms.Label();
            this.flpErrBoutons = new System.Windows.Forms.FlowLayoutPanel();
            this.btOK = new System.Windows.Forms.Button();
            this.btQuitterApplication = new System.Windows.Forms.Button();
            this.btPurger = new System.Windows.Forms.Button();
            this.btSauver = new System.Windows.Forms.Button();
            this.btConsulter = new System.Windows.Forms.Button();
            this.flpCheckBoxes = new System.Windows.Forms.FlowLayoutPanel();
            this.cbAfficherMessages = new System.Windows.Forms.CheckBox();
            this.cbAfficherErreurs = new System.Windows.Forms.CheckBox();
            this.saveFileDialogErreurs = new System.Windows.Forms.SaveFileDialog();
            this.tlpErrMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAffichage)).BeginInit();
            this.tlpProgressBarre.SuspendLayout();
            this.flpErrBoutons.SuspendLayout();
            this.flpCheckBoxes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpErrMaster
            // 
            this.tlpErrMaster.AutoSize = true;
            this.tlpErrMaster.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpErrMaster.ColumnCount = 2;
            this.tlpErrMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpErrMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpErrMaster.Controls.Add(this.dgvAffichage, 0, 2);
            this.tlpErrMaster.Controls.Add(this.tlpProgressBarre, 0, 1);
            this.tlpErrMaster.Controls.Add(this.labelStatut, 0, 0);
            this.tlpErrMaster.Controls.Add(this.flpErrBoutons, 1, 3);
            this.tlpErrMaster.Controls.Add(this.flpCheckBoxes, 0, 3);
            this.tlpErrMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpErrMaster.Location = new System.Drawing.Point(15, 15);
            this.tlpErrMaster.Margin = new System.Windows.Forms.Padding(0);
            this.tlpErrMaster.Name = "tlpErrMaster";
            this.tlpErrMaster.RowCount = 4;
            this.tlpErrMaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpErrMaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpErrMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpErrMaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpErrMaster.Size = new System.Drawing.Size(860, 261);
            this.tlpErrMaster.TabIndex = 20;
            // 
            // dgvAffichage
            // 
            this.dgvAffichage.AllowUserToAddRows = false;
            this.dgvAffichage.AllowUserToDeleteRows = false;
            this.dgvAffichage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAffichage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpErrMaster.SetColumnSpan(this.dgvAffichage, 2);
            this.dgvAffichage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAffichage.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAffichage.Location = new System.Drawing.Point(5, 51);
            this.dgvAffichage.Margin = new System.Windows.Forms.Padding(5);
            this.dgvAffichage.MultiSelect = false;
            this.dgvAffichage.Name = "dgvAffichage";
            this.dgvAffichage.ReadOnly = true;
            this.dgvAffichage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAffichage.Size = new System.Drawing.Size(850, 166);
            this.dgvAffichage.TabIndex = 3;
            this.dgvAffichage.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvExceptions_SortCompare);
            this.dgvAffichage.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvExceptions_CellMouseDown);
            this.dgvAffichage.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExceptions_CellContentDoubleClick);
            this.dgvAffichage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExceptions_CellContentClick);
            // 
            // tlpProgressBarre
            // 
            this.tlpProgressBarre.AutoSize = true;
            this.tlpProgressBarre.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpProgressBarre.ColumnCount = 2;
            this.tlpErrMaster.SetColumnSpan(this.tlpProgressBarre, 2);
            this.tlpProgressBarre.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProgressBarre.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpProgressBarre.Controls.Add(this.progressBar, 0, 0);
            this.tlpProgressBarre.Controls.Add(this.btArrêterTâche, 1, 0);
            this.tlpProgressBarre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProgressBarre.Location = new System.Drawing.Point(0, 13);
            this.tlpProgressBarre.Margin = new System.Windows.Forms.Padding(0);
            this.tlpProgressBarre.Name = "tlpProgressBarre";
            this.tlpProgressBarre.RowCount = 1;
            this.tlpProgressBarre.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProgressBarre.Size = new System.Drawing.Size(860, 33);
            this.tlpProgressBarre.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(5, 5);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(719, 23);
            this.progressBar.TabIndex = 1;
            // 
            // btArrêterTâche
            // 
            this.btArrêterTâche.Enabled = false;
            this.btArrêterTâche.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btArrêterTâche.Location = new System.Drawing.Point(734, 5);
            this.btArrêterTâche.Margin = new System.Windows.Forms.Padding(5);
            this.btArrêterTâche.Name = "btArrêterTâche";
            this.btArrêterTâche.Size = new System.Drawing.Size(121, 23);
            this.btArrêterTâche.TabIndex = 2;
            this.btArrêterTâche.Text = "Arrêter Tâche";
            this.btArrêterTâche.UseVisualStyleBackColor = true;
            this.btArrêterTâche.Click += new System.EventHandler(this.btArrêterTâche_Click);
            this.btArrêterTâche.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btArrêterTâche_MouseDown);
            // 
            // labelStatut
            // 
            this.labelStatut.AutoSize = true;
            this.labelStatut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStatut.Location = new System.Drawing.Point(3, 0);
            this.labelStatut.Name = "labelStatut";
            this.labelStatut.Size = new System.Drawing.Size(209, 13);
            this.labelStatut.TabIndex = 0;
            this.labelStatut.Text = "Aucune tâche lancée";
            this.labelStatut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpErrBoutons
            // 
            this.flpErrBoutons.AutoSize = true;
            this.flpErrBoutons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpErrBoutons.Controls.Add(this.btOK);
            this.flpErrBoutons.Controls.Add(this.btQuitterApplication);
            this.flpErrBoutons.Controls.Add(this.btPurger);
            this.flpErrBoutons.Controls.Add(this.btSauver);
            this.flpErrBoutons.Controls.Add(this.btConsulter);
            this.flpErrBoutons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpErrBoutons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpErrBoutons.Location = new System.Drawing.Point(218, 225);
            this.flpErrBoutons.Name = "flpErrBoutons";
            this.flpErrBoutons.Size = new System.Drawing.Size(639, 33);
            this.flpErrBoutons.TabIndex = 10;
            // 
            // btOK
            // 
            this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOK.Location = new System.Drawing.Point(559, 5);
            this.btOK.Margin = new System.Windows.Forms.Padding(5);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 7;
            this.btOK.Text = "Masquer";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            this.btOK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btOK_MouseDown);
            // 
            // btQuitterApplication
            // 
            this.btQuitterApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btQuitterApplication.Location = new System.Drawing.Point(422, 5);
            this.btQuitterApplication.Margin = new System.Windows.Forms.Padding(5);
            this.btQuitterApplication.Name = "btQuitterApplication";
            this.btQuitterApplication.Size = new System.Drawing.Size(127, 23);
            this.btQuitterApplication.TabIndex = 8;
            this.btQuitterApplication.Text = "Quitter l\'application";
            this.btQuitterApplication.UseVisualStyleBackColor = true;
            this.btQuitterApplication.Click += new System.EventHandler(this.btQuitterApplication_Click);
            this.btQuitterApplication.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btQuitterApplication_MouseDown);
            // 
            // btPurger
            // 
            this.btPurger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPurger.Location = new System.Drawing.Point(337, 5);
            this.btPurger.Margin = new System.Windows.Forms.Padding(5);
            this.btPurger.Name = "btPurger";
            this.btPurger.Size = new System.Drawing.Size(75, 23);
            this.btPurger.TabIndex = 6;
            this.btPurger.Text = "Purger";
            this.btPurger.UseVisualStyleBackColor = true;
            this.btPurger.Click += new System.EventHandler(this.btPurger_Click);
            this.btPurger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btPurger_MouseDown);
            // 
            // btSauver
            // 
            this.btSauver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSauver.Location = new System.Drawing.Point(252, 5);
            this.btSauver.Margin = new System.Windows.Forms.Padding(5);
            this.btSauver.Name = "btSauver";
            this.btSauver.Size = new System.Drawing.Size(75, 23);
            this.btSauver.TabIndex = 5;
            this.btSauver.Text = "Sauver";
            this.btSauver.UseVisualStyleBackColor = true;
            this.btSauver.Click += new System.EventHandler(this.btSauver_Click);
            this.btSauver.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btSauver_MouseDown);
            // 
            // btConsulter
            // 
            this.btConsulter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btConsulter.Location = new System.Drawing.Point(167, 5);
            this.btConsulter.Margin = new System.Windows.Forms.Padding(5);
            this.btConsulter.Name = "btConsulter";
            this.btConsulter.Size = new System.Drawing.Size(75, 23);
            this.btConsulter.TabIndex = 4;
            this.btConsulter.Text = "Consulter";
            this.btConsulter.UseVisualStyleBackColor = true;
            this.btConsulter.Click += new System.EventHandler(this.btConsulter_Click);
            this.btConsulter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btConsulter_MouseDown);
            // 
            // flpCheckBoxes
            // 
            this.flpCheckBoxes.AutoSize = true;
            this.flpCheckBoxes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpCheckBoxes.Controls.Add(this.cbAfficherMessages);
            this.flpCheckBoxes.Controls.Add(this.cbAfficherErreurs);
            this.flpCheckBoxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCheckBoxes.Location = new System.Drawing.Point(3, 225);
            this.flpCheckBoxes.Name = "flpCheckBoxes";
            this.flpCheckBoxes.Size = new System.Drawing.Size(209, 33);
            this.flpCheckBoxes.TabIndex = 11;
            // 
            // cbAfficherMessages
            // 
            this.cbAfficherMessages.AutoCheck = false;
            this.cbAfficherMessages.AutoSize = true;
            this.cbAfficherMessages.Checked = true;
            this.cbAfficherMessages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAfficherMessages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAfficherMessages.Location = new System.Drawing.Point(3, 3);
            this.cbAfficherMessages.Name = "cbAfficherMessages";
            this.cbAfficherMessages.Size = new System.Drawing.Size(71, 17);
            this.cbAfficherMessages.TabIndex = 0;
            this.cbAfficherMessages.Text = "Messages";
            this.cbAfficherMessages.UseVisualStyleBackColor = true;
            this.cbAfficherMessages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cbAfficherMessages_MouseDown);
            this.cbAfficherMessages.CheckedChanged += new System.EventHandler(this.cbAfficherMessages_CheckedChanged);
            // 
            // cbAfficherErreurs
            // 
            this.cbAfficherErreurs.AutoCheck = false;
            this.cbAfficherErreurs.AutoSize = true;
            this.cbAfficherErreurs.Checked = true;
            this.cbAfficherErreurs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAfficherErreurs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAfficherErreurs.Location = new System.Drawing.Point(80, 3);
            this.cbAfficherErreurs.Name = "cbAfficherErreurs";
            this.cbAfficherErreurs.Size = new System.Drawing.Size(56, 17);
            this.cbAfficherErreurs.TabIndex = 1;
            this.cbAfficherErreurs.Text = "Erreurs";
            this.cbAfficherErreurs.UseVisualStyleBackColor = true;
            this.cbAfficherErreurs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cbAfficherErreurs_MouseDown);
            this.cbAfficherErreurs.CheckedChanged += new System.EventHandler(this.cbAfficherErreurs_CheckedChanged);
            // 
            // saveFileDialogErreurs
            // 
            this.saveFileDialogErreurs.DefaultExt = "csv";
            this.saveFileDialogErreurs.FileName = "erreurs.csv";
            this.saveFileDialogErreurs.Filter = "Fichiers CSV|*.csv|Fichiers texte|*.txt|Tous les fichiers|*.*";
            // 
            // GestionnaireDesTâches
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 291);
            this.Controls.Add(this.tlpErrMaster);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GestionnaireDesTâches";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Text = "Suivi de l\'opération";
            this.Load += new System.EventHandler(this.FormGestionnaireExceptions_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGestionnaireExceptions_FormClosing);
            this.tlpErrMaster.ResumeLayout(false);
            this.tlpErrMaster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAffichage)).EndInit();
            this.tlpProgressBarre.ResumeLayout(false);
            this.flpErrBoutons.ResumeLayout(false);
            this.flpCheckBoxes.ResumeLayout(false);
            this.flpCheckBoxes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpErrMaster;
        private System.Windows.Forms.FlowLayoutPanel flpErrBoutons;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.DataGridView dgvAffichage;
        private System.Windows.Forms.Button btPurger;
        private System.Windows.Forms.Button btSauver;
        private System.Windows.Forms.SaveFileDialog saveFileDialogErreurs;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TableLayoutPanel tlpProgressBarre;
        private System.Windows.Forms.Button btArrêterTâche;
        private System.Windows.Forms.Label labelStatut;
        private System.Windows.Forms.Button btConsulter;
        private System.Windows.Forms.FlowLayoutPanel flpCheckBoxes;
        private System.Windows.Forms.CheckBox cbAfficherMessages;
        private System.Windows.Forms.CheckBox cbAfficherErreurs;
        private System.Windows.Forms.Button btQuitterApplication;

    }
}