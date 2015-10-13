namespace Import_Export_CSV
{
    partial class FormAssociation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAssociation));
            this.dgvAssociation = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btValiderAssociation = new System.Windows.Forms.Button();
            this.btAnnulerAssociation = new System.Windows.Forms.Button();
            this.gbCléPrimaire = new System.Windows.Forms.GroupBox();
            this.tlpClésPrimaires = new System.Windows.Forms.TableLayoutPanel();
            this.rbTestCléPrimaire = new System.Windows.Forms.RadioButton();
            this.btAjouterCléPrimaire = new System.Windows.Forms.Button();
            this.rbInsertUpdate = new System.Windows.Forms.RadioButton();
            this.btSupprimerCléPrimaire = new System.Windows.Forms.Button();
            this.rbUPDATEonly = new System.Windows.Forms.RadioButton();
            this.lbClésPrimaires = new System.Windows.Forms.ListBox();
            this.rbINSERTonly = new System.Windows.Forms.RadioButton();
            this.gbConditionsOptionnelles = new System.Windows.Forms.GroupBox();
            this.tlpWhere = new System.Windows.Forms.TableLayoutPanel();
            this.lbWHERE = new System.Windows.Forms.Label();
            this.tbConditionsOptionnelles = new System.Windows.Forms.TextBox();
            this.btEffacer = new System.Windows.Forms.Button();
            this.btAjouterChamp = new System.Windows.Forms.Button();
            this.tlpAssociations = new System.Windows.Forms.TableLayoutPanel();
            this.flpBoutonsValidationAssociation = new System.Windows.Forms.FlowLayoutPanel();
            this.flpBoutonsGestionAssociation2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btSauverAssociation = new System.Windows.Forms.Button();
            this.btChargerAssociation = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.saveFileDialogAssociation = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogAssociation = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociation)).BeginInit();
            this.gbCléPrimaire.SuspendLayout();
            this.tlpClésPrimaires.SuspendLayout();
            this.gbConditionsOptionnelles.SuspendLayout();
            this.tlpWhere.SuspendLayout();
            this.tlpAssociations.SuspendLayout();
            this.flpBoutonsValidationAssociation.SuspendLayout();
            this.flpBoutonsGestionAssociation2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAssociation
            // 
            this.dgvAssociation.AllowDrop = true;
            this.dgvAssociation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAssociation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpAssociations.SetColumnSpan(this.dgvAssociation, 2);
            this.dgvAssociation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAssociation.Location = new System.Drawing.Point(5, 28);
            this.dgvAssociation.Margin = new System.Windows.Forms.Padding(5);
            this.dgvAssociation.MultiSelect = false;
            this.dgvAssociation.Name = "dgvAssociation";
            this.tlpAssociations.SetRowSpan(this.dgvAssociation, 2);
            this.dgvAssociation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvAssociation.Size = new System.Drawing.Size(378, 452);
            this.dgvAssociation.TabIndex = 30;
            this.dgvAssociation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvAssociation_MouseMove);
            this.dgvAssociation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvAssociation_MouseUp);
            this.dgvAssociation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvAssociation_KeyDown);
            this.dgvAssociation.SelectionChanged += new System.EventHandler(this.dgvAssociation_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tlpAssociations.SetColumnSpan(this.label1, 3);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(563, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Associez par drag and drop les champs de la base de données avec ceux du tampon, " +
                "puis validez";
            // 
            // btValiderAssociation
            // 
            this.btValiderAssociation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btValiderAssociation.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btValiderAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btValiderAssociation.Location = new System.Drawing.Point(233, 5);
            this.btValiderAssociation.Margin = new System.Windows.Forms.Padding(5);
            this.btValiderAssociation.Name = "btValiderAssociation";
            this.btValiderAssociation.Size = new System.Drawing.Size(152, 30);
            this.btValiderAssociation.TabIndex = 230;
            this.btValiderAssociation.Text = "Démarrer l\'opération";
            this.btValiderAssociation.UseVisualStyleBackColor = true;
            this.btValiderAssociation.Click += new System.EventHandler(this.btValiderAssociation_Click);
            // 
            // btAnnulerAssociation
            // 
            this.btAnnulerAssociation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAnnulerAssociation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btAnnulerAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAnnulerAssociation.Location = new System.Drawing.Point(123, 5);
            this.btAnnulerAssociation.Margin = new System.Windows.Forms.Padding(5);
            this.btAnnulerAssociation.Name = "btAnnulerAssociation";
            this.btAnnulerAssociation.Size = new System.Drawing.Size(100, 30);
            this.btAnnulerAssociation.TabIndex = 220;
            this.btAnnulerAssociation.Text = "Fermer";
            this.btAnnulerAssociation.UseVisualStyleBackColor = true;
            this.btAnnulerAssociation.Click += new System.EventHandler(this.btAnnulerAssociation_Click);
            // 
            // gbCléPrimaire
            // 
            this.gbCléPrimaire.AutoSize = true;
            this.gbCléPrimaire.Controls.Add(this.tlpClésPrimaires);
            this.gbCléPrimaire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCléPrimaire.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCléPrimaire.Location = new System.Drawing.Point(393, 28);
            this.gbCléPrimaire.Margin = new System.Windows.Forms.Padding(5);
            this.gbCléPrimaire.Name = "gbCléPrimaire";
            this.gbCléPrimaire.Padding = new System.Windows.Forms.Padding(15, 5, 15, 15);
            this.gbCléPrimaire.Size = new System.Drawing.Size(380, 221);
            this.gbCléPrimaire.TabIndex = 6;
            this.gbCléPrimaire.TabStop = false;
            this.gbCléPrimaire.Text = "Clé(s) primaire(s)";
            // 
            // tlpClésPrimaires
            // 
            this.tlpClésPrimaires.AutoSize = true;
            this.tlpClésPrimaires.ColumnCount = 2;
            this.tlpClésPrimaires.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpClésPrimaires.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpClésPrimaires.Controls.Add(this.rbTestCléPrimaire, 0, 5);
            this.tlpClésPrimaires.Controls.Add(this.btAjouterCléPrimaire, 0, 0);
            this.tlpClésPrimaires.Controls.Add(this.rbInsertUpdate, 0, 4);
            this.tlpClésPrimaires.Controls.Add(this.btSupprimerCléPrimaire, 0, 1);
            this.tlpClésPrimaires.Controls.Add(this.rbUPDATEonly, 0, 3);
            this.tlpClésPrimaires.Controls.Add(this.lbClésPrimaires, 1, 0);
            this.tlpClésPrimaires.Controls.Add(this.rbINSERTonly, 0, 2);
            this.tlpClésPrimaires.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpClésPrimaires.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlpClésPrimaires.Location = new System.Drawing.Point(15, 18);
            this.tlpClésPrimaires.Name = "tlpClésPrimaires";
            this.tlpClésPrimaires.RowCount = 6;
            this.tlpClésPrimaires.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpClésPrimaires.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpClésPrimaires.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpClésPrimaires.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpClésPrimaires.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpClésPrimaires.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpClésPrimaires.Size = new System.Drawing.Size(350, 188);
            this.tlpClésPrimaires.TabIndex = 6;
            // 
            // rbTestCléPrimaire
            // 
            this.rbTestCléPrimaire.AutoSize = true;
            this.tlpClésPrimaires.SetColumnSpan(this.rbTestCléPrimaire, 2);
            this.rbTestCléPrimaire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbTestCléPrimaire.Location = new System.Drawing.Point(5, 166);
            this.rbTestCléPrimaire.Margin = new System.Windows.Forms.Padding(5);
            this.rbTestCléPrimaire.Name = "rbTestCléPrimaire";
            this.rbTestCléPrimaire.Size = new System.Drawing.Size(337, 17);
            this.rbTestCléPrimaire.TabIndex = 73;
            this.rbTestCléPrimaire.Text = "Tester juste l\'existence des clés primaires - Aucun changement BD";
            this.rbTestCléPrimaire.UseVisualStyleBackColor = true;
            // 
            // btAjouterCléPrimaire
            // 
            this.btAjouterCléPrimaire.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btAjouterCléPrimaire.AutoSize = true;
            this.btAjouterCléPrimaire.Enabled = false;
            this.btAjouterCléPrimaire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAjouterCléPrimaire.Location = new System.Drawing.Point(5, 5);
            this.btAjouterCléPrimaire.Margin = new System.Windows.Forms.Padding(5);
            this.btAjouterCléPrimaire.Name = "btAjouterCléPrimaire";
            this.btAjouterCléPrimaire.Size = new System.Drawing.Size(95, 25);
            this.btAjouterCléPrimaire.TabIndex = 40;
            this.btAjouterCléPrimaire.Text = ">> Ajouter";
            this.btAjouterCléPrimaire.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAjouterCléPrimaire.UseVisualStyleBackColor = true;
            this.btAjouterCléPrimaire.Click += new System.EventHandler(this.btAjouterCléPrimaire_Click);
            // 
            // rbInsertUpdate
            // 
            this.rbInsertUpdate.AutoSize = true;
            this.rbInsertUpdate.Checked = true;
            this.tlpClésPrimaires.SetColumnSpan(this.rbInsertUpdate, 2);
            this.rbInsertUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbInsertUpdate.Location = new System.Drawing.Point(5, 139);
            this.rbInsertUpdate.Margin = new System.Windows.Forms.Padding(5);
            this.rbInsertUpdate.Name = "rbInsertUpdate";
            this.rbInsertUpdate.Size = new System.Drawing.Size(255, 17);
            this.rbInsertUpdate.TabIndex = 72;
            this.rbInsertUpdate.TabStop = true;
            this.rbInsertUpdate.Text = "Si clé primaire existe : UPDATE - sinon : INSERT";
            this.rbInsertUpdate.UseVisualStyleBackColor = true;
            // 
            // btSupprimerCléPrimaire
            // 
            this.btSupprimerCléPrimaire.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btSupprimerCléPrimaire.AutoSize = true;
            this.btSupprimerCléPrimaire.Enabled = false;
            this.btSupprimerCléPrimaire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSupprimerCléPrimaire.Location = new System.Drawing.Point(5, 45);
            this.btSupprimerCléPrimaire.Margin = new System.Windows.Forms.Padding(5);
            this.btSupprimerCléPrimaire.Name = "btSupprimerCléPrimaire";
            this.btSupprimerCléPrimaire.Size = new System.Drawing.Size(95, 25);
            this.btSupprimerCléPrimaire.TabIndex = 50;
            this.btSupprimerCléPrimaire.Text = "<< Supprimer";
            this.btSupprimerCléPrimaire.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSupprimerCléPrimaire.UseVisualStyleBackColor = true;
            this.btSupprimerCléPrimaire.Click += new System.EventHandler(this.btSupprimerCléPrimaire_Click);
            // 
            // rbUPDATEonly
            // 
            this.rbUPDATEonly.AutoSize = true;
            this.tlpClésPrimaires.SetColumnSpan(this.rbUPDATEonly, 2);
            this.rbUPDATEonly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbUPDATEonly.Location = new System.Drawing.Point(5, 112);
            this.rbUPDATEonly.Margin = new System.Windows.Forms.Padding(5);
            this.rbUPDATEonly.Name = "rbUPDATEonly";
            this.rbUPDATEonly.Size = new System.Drawing.Size(126, 17);
            this.rbUPDATEonly.TabIndex = 71;
            this.rbUPDATEonly.Text = "UPDATE uniquement";
            this.rbUPDATEonly.UseVisualStyleBackColor = true;
            // 
            // lbClésPrimaires
            // 
            this.lbClésPrimaires.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbClésPrimaires.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbClésPrimaires.FormattingEnabled = true;
            this.lbClésPrimaires.Location = new System.Drawing.Point(110, 5);
            this.lbClésPrimaires.Margin = new System.Windows.Forms.Padding(5);
            this.lbClésPrimaires.Name = "lbClésPrimaires";
            this.tlpClésPrimaires.SetRowSpan(this.lbClésPrimaires, 2);
            this.lbClésPrimaires.Size = new System.Drawing.Size(235, 67);
            this.lbClésPrimaires.TabIndex = 60;
            this.lbClésPrimaires.SelectedIndexChanged += new System.EventHandler(this.lbClésPrimaires_SelectedIndexChanged);
            this.lbClésPrimaires.DoubleClick += new System.EventHandler(this.lbClésPrimaires_DoubleClick);
            // 
            // rbINSERTonly
            // 
            this.rbINSERTonly.AutoSize = true;
            this.tlpClésPrimaires.SetColumnSpan(this.rbINSERTonly, 2);
            this.rbINSERTonly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbINSERTonly.Location = new System.Drawing.Point(5, 85);
            this.rbINSERTonly.Margin = new System.Windows.Forms.Padding(5);
            this.rbINSERTonly.Name = "rbINSERTonly";
            this.rbINSERTonly.Size = new System.Drawing.Size(122, 17);
            this.rbINSERTonly.TabIndex = 70;
            this.rbINSERTonly.Text = "INSERT uniquement";
            this.rbINSERTonly.UseVisualStyleBackColor = true;
            // 
            // gbConditionsOptionnelles
            // 
            this.gbConditionsOptionnelles.AutoSize = true;
            this.gbConditionsOptionnelles.Controls.Add(this.tlpWhere);
            this.gbConditionsOptionnelles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbConditionsOptionnelles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbConditionsOptionnelles.Location = new System.Drawing.Point(393, 259);
            this.gbConditionsOptionnelles.Margin = new System.Windows.Forms.Padding(5);
            this.gbConditionsOptionnelles.Name = "gbConditionsOptionnelles";
            this.gbConditionsOptionnelles.Padding = new System.Windows.Forms.Padding(15, 5, 15, 15);
            this.gbConditionsOptionnelles.Size = new System.Drawing.Size(380, 221);
            this.gbConditionsOptionnelles.TabIndex = 7;
            this.gbConditionsOptionnelles.TabStop = false;
            this.gbConditionsOptionnelles.Text = "Conditions supplémentaires (UPDATE uniquement)";
            // 
            // tlpWhere
            // 
            this.tlpWhere.AutoSize = true;
            this.tlpWhere.ColumnCount = 2;
            this.tlpWhere.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpWhere.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWhere.Controls.Add(this.lbWHERE, 0, 0);
            this.tlpWhere.Controls.Add(this.tbConditionsOptionnelles, 1, 0);
            this.tlpWhere.Controls.Add(this.btEffacer, 0, 2);
            this.tlpWhere.Controls.Add(this.btAjouterChamp, 0, 1);
            this.tlpWhere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpWhere.Location = new System.Drawing.Point(15, 18);
            this.tlpWhere.Name = "tlpWhere";
            this.tlpWhere.RowCount = 3;
            this.tlpWhere.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpWhere.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpWhere.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpWhere.Size = new System.Drawing.Size(350, 188);
            this.tlpWhere.TabIndex = 8;
            // 
            // lbWHERE
            // 
            this.lbWHERE.AutoSize = true;
            this.lbWHERE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWHERE.Location = new System.Drawing.Point(5, 5);
            this.lbWHERE.Margin = new System.Windows.Forms.Padding(5);
            this.lbWHERE.Name = "lbWHERE";
            this.lbWHERE.Size = new System.Drawing.Size(48, 13);
            this.lbWHERE.TabIndex = 0;
            this.lbWHERE.Text = "WHERE";
            // 
            // tbConditionsOptionnelles
            // 
            this.tbConditionsOptionnelles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbConditionsOptionnelles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConditionsOptionnelles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConditionsOptionnelles.Location = new System.Drawing.Point(110, 5);
            this.tbConditionsOptionnelles.Margin = new System.Windows.Forms.Padding(5);
            this.tbConditionsOptionnelles.Multiline = true;
            this.tbConditionsOptionnelles.Name = "tbConditionsOptionnelles";
            this.tlpWhere.SetRowSpan(this.tbConditionsOptionnelles, 3);
            this.tbConditionsOptionnelles.Size = new System.Drawing.Size(235, 178);
            this.tbConditionsOptionnelles.TabIndex = 100;
            this.tbConditionsOptionnelles.TextChanged += new System.EventHandler(this.tbConditionsOptionnelles_TextChanged);
            // 
            // btEffacer
            // 
            this.btEffacer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btEffacer.Enabled = false;
            this.btEffacer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEffacer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEffacer.Location = new System.Drawing.Point(5, 129);
            this.btEffacer.Margin = new System.Windows.Forms.Padding(5);
            this.btEffacer.Name = "btEffacer";
            this.btEffacer.Size = new System.Drawing.Size(95, 25);
            this.btEffacer.TabIndex = 90;
            this.btEffacer.Text = "Effacer";
            this.btEffacer.UseVisualStyleBackColor = true;
            this.btEffacer.Click += new System.EventHandler(this.btEffacer_Click);
            // 
            // btAjouterChamp
            // 
            this.btAjouterChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btAjouterChamp.Enabled = false;
            this.btAjouterChamp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAjouterChamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAjouterChamp.Location = new System.Drawing.Point(5, 67);
            this.btAjouterChamp.Margin = new System.Windows.Forms.Padding(5);
            this.btAjouterChamp.Name = "btAjouterChamp";
            this.btAjouterChamp.Size = new System.Drawing.Size(95, 25);
            this.btAjouterChamp.TabIndex = 80;
            this.btAjouterChamp.Text = ">> Ajouter";
            this.btAjouterChamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAjouterChamp.UseVisualStyleBackColor = true;
            this.btAjouterChamp.Click += new System.EventHandler(this.btAjouterChamp_Click);
            // 
            // tlpAssociations
            // 
            this.tlpAssociations.AutoSize = true;
            this.tlpAssociations.ColumnCount = 3;
            this.tlpAssociations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpAssociations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpAssociations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAssociations.Controls.Add(this.label1, 0, 0);
            this.tlpAssociations.Controls.Add(this.gbCléPrimaire, 2, 1);
            this.tlpAssociations.Controls.Add(this.flpBoutonsValidationAssociation, 2, 3);
            this.tlpAssociations.Controls.Add(this.gbConditionsOptionnelles, 2, 2);
            this.tlpAssociations.Controls.Add(this.dgvAssociation, 0, 1);
            this.tlpAssociations.Controls.Add(this.flpBoutonsGestionAssociation2, 0, 3);
            this.tlpAssociations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAssociations.Location = new System.Drawing.Point(15, 15);
            this.tlpAssociations.Name = "tlpAssociations";
            this.tlpAssociations.RowCount = 4;
            this.tlpAssociations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpAssociations.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAssociations.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAssociations.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpAssociations.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAssociations.Size = new System.Drawing.Size(778, 525);
            this.tlpAssociations.TabIndex = 10;
            // 
            // flpBoutonsValidationAssociation
            // 
            this.flpBoutonsValidationAssociation.AutoSize = true;
            this.flpBoutonsValidationAssociation.Controls.Add(this.btValiderAssociation);
            this.flpBoutonsValidationAssociation.Controls.Add(this.btAnnulerAssociation);
            this.flpBoutonsValidationAssociation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpBoutonsValidationAssociation.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBoutonsValidationAssociation.Location = new System.Drawing.Point(388, 485);
            this.flpBoutonsValidationAssociation.Margin = new System.Windows.Forms.Padding(0);
            this.flpBoutonsValidationAssociation.Name = "flpBoutonsValidationAssociation";
            this.flpBoutonsValidationAssociation.Size = new System.Drawing.Size(390, 40);
            this.flpBoutonsValidationAssociation.TabIndex = 9;
            // 
            // flpBoutonsGestionAssociation2
            // 
            this.flpBoutonsGestionAssociation2.AutoSize = true;
            this.flpBoutonsGestionAssociation2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpAssociations.SetColumnSpan(this.flpBoutonsGestionAssociation2, 2);
            this.flpBoutonsGestionAssociation2.Controls.Add(this.btSauverAssociation);
            this.flpBoutonsGestionAssociation2.Controls.Add(this.btChargerAssociation);
            this.flpBoutonsGestionAssociation2.Controls.Add(this.btReset);
            this.flpBoutonsGestionAssociation2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBoutonsGestionAssociation2.Location = new System.Drawing.Point(0, 485);
            this.flpBoutonsGestionAssociation2.Margin = new System.Windows.Forms.Padding(0);
            this.flpBoutonsGestionAssociation2.Name = "flpBoutonsGestionAssociation2";
            this.flpBoutonsGestionAssociation2.Size = new System.Drawing.Size(388, 40);
            this.flpBoutonsGestionAssociation2.TabIndex = 213;
            // 
            // btSauverAssociation
            // 
            this.btSauverAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSauverAssociation.Location = new System.Drawing.Point(5, 5);
            this.btSauverAssociation.Margin = new System.Windows.Forms.Padding(5);
            this.btSauverAssociation.Name = "btSauverAssociation";
            this.btSauverAssociation.Size = new System.Drawing.Size(100, 30);
            this.btSauverAssociation.TabIndex = 211;
            this.btSauverAssociation.Text = "Sauver";
            this.btSauverAssociation.UseVisualStyleBackColor = true;
            this.btSauverAssociation.Click += new System.EventHandler(this.btSauverAssociation_Click);
            // 
            // btChargerAssociation
            // 
            this.btChargerAssociation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btChargerAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btChargerAssociation.Location = new System.Drawing.Point(115, 5);
            this.btChargerAssociation.Margin = new System.Windows.Forms.Padding(5);
            this.btChargerAssociation.Name = "btChargerAssociation";
            this.btChargerAssociation.Size = new System.Drawing.Size(100, 30);
            this.btChargerAssociation.TabIndex = 212;
            this.btChargerAssociation.Text = "Charger";
            this.btChargerAssociation.UseVisualStyleBackColor = true;
            this.btChargerAssociation.Click += new System.EventHandler(this.btChargerAssociation_Click);
            // 
            // btReset
            // 
            this.btReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReset.Location = new System.Drawing.Point(225, 5);
            this.btReset.Margin = new System.Windows.Forms.Padding(5);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(100, 30);
            this.btReset.TabIndex = 210;
            this.btReset.Text = "Ré-initialiser";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // saveFileDialogAssociation
            // 
            this.saveFileDialogAssociation.DefaultExt = "xml";
            this.saveFileDialogAssociation.FileName = "association.xml";
            this.saveFileDialogAssociation.Filter = "Fichiers XML|*.xml|Fichiers texte|*.txt|Tous les fichiers|*.*";
            // 
            // openFileDialogAssociation
            // 
            this.openFileDialogAssociation.DefaultExt = "xml";
            this.openFileDialogAssociation.FileName = "association.xml";
            this.openFileDialogAssociation.Filter = "Fichiers XML|*.xml|Fichiers texte|*.txt|Tous les fichiers|*.*";
            // 
            // FormAssociation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 555);
            this.Controls.Add(this.tlpAssociations);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAssociation";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Associations de champs";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormAssociation_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAssociation_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociation)).EndInit();
            this.gbCléPrimaire.ResumeLayout(false);
            this.gbCléPrimaire.PerformLayout();
            this.tlpClésPrimaires.ResumeLayout(false);
            this.tlpClésPrimaires.PerformLayout();
            this.gbConditionsOptionnelles.ResumeLayout(false);
            this.gbConditionsOptionnelles.PerformLayout();
            this.tlpWhere.ResumeLayout(false);
            this.tlpWhere.PerformLayout();
            this.tlpAssociations.ResumeLayout(false);
            this.tlpAssociations.PerformLayout();
            this.flpBoutonsValidationAssociation.ResumeLayout(false);
            this.flpBoutonsGestionAssociation2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAssociation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btValiderAssociation;
        private System.Windows.Forms.Button btAnnulerAssociation;
        private System.Windows.Forms.GroupBox gbCléPrimaire;
        private System.Windows.Forms.RadioButton rbInsertUpdate;
        private System.Windows.Forms.RadioButton rbUPDATEonly;
        private System.Windows.Forms.RadioButton rbINSERTonly;
        private System.Windows.Forms.ListBox lbClésPrimaires;
        private System.Windows.Forms.Button btAjouterCléPrimaire;
        private System.Windows.Forms.Button btSupprimerCléPrimaire;
        private System.Windows.Forms.GroupBox gbConditionsOptionnelles;
        private System.Windows.Forms.TextBox tbConditionsOptionnelles;
        private System.Windows.Forms.Label lbWHERE;
        private System.Windows.Forms.Button btEffacer;
        private System.Windows.Forms.Button btAjouterChamp;
        private System.Windows.Forms.TableLayoutPanel tlpAssociations;
        private System.Windows.Forms.TableLayoutPanel tlpWhere;
        private System.Windows.Forms.TableLayoutPanel tlpClésPrimaires;
        private System.Windows.Forms.FlowLayoutPanel flpBoutonsValidationAssociation;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Button btChargerAssociation;
        private System.Windows.Forms.Button btSauverAssociation;
        private System.Windows.Forms.SaveFileDialog saveFileDialogAssociation;
        private System.Windows.Forms.OpenFileDialog openFileDialogAssociation;
        private System.Windows.Forms.FlowLayoutPanel flpBoutonsGestionAssociation2;
        private System.Windows.Forms.RadioButton rbTestCléPrimaire;
    }
}