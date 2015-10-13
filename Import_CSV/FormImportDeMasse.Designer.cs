namespace Import_Export_CSV
{
    partial class FormImportDeMasse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportDeMasse));
            this.dgvAssociation = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btValiderAssociation = new System.Windows.Forms.Button();
            this.btAnnulerAssociation = new System.Windows.Forms.Button();
            this.tlpAssociations = new System.Windows.Forms.TableLayoutPanel();
            this.flpBoutonsGestionAssociation2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btReset = new System.Windows.Forms.Button();
            this.btChargerAssociation = new System.Windows.Forms.Button();
            this.btSauverAssociation = new System.Windows.Forms.Button();
            this.saveFileDialogAssociation = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogAssociation = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociation)).BeginInit();
            this.tlpAssociations.SuspendLayout();
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
            this.dgvAssociation.Size = new System.Drawing.Size(768, 452);
            this.dgvAssociation.TabIndex = 30;
            this.dgvAssociation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvAssociation_MouseMove);
            this.dgvAssociation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvAssociation_MouseUp);
            this.dgvAssociation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvAssociation_KeyDown);
            this.dgvAssociation.SelectionChanged += new System.EventHandler(this.dgvAssociation_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tlpAssociations.SetColumnSpan(this.label1, 2);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(573, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Associez par drag and drop les tables de la base de données avec les fichiers d\'i" +
                "mport, puis validez";
            // 
            // btValiderAssociation
            // 
            this.btValiderAssociation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btValiderAssociation.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btValiderAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btValiderAssociation.Location = new System.Drawing.Point(598, 5);
            this.btValiderAssociation.Margin = new System.Windows.Forms.Padding(5);
            this.btValiderAssociation.Name = "btValiderAssociation";
            this.btValiderAssociation.Size = new System.Drawing.Size(175, 30);
            this.btValiderAssociation.TabIndex = 230;
            this.btValiderAssociation.Text = "Démarrer l\'import de masse";
            this.btValiderAssociation.UseVisualStyleBackColor = true;
            this.btValiderAssociation.Click += new System.EventHandler(this.btValiderAssociation_Click);
            // 
            // btAnnulerAssociation
            // 
            this.btAnnulerAssociation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAnnulerAssociation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btAnnulerAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAnnulerAssociation.Location = new System.Drawing.Point(488, 5);
            this.btAnnulerAssociation.Margin = new System.Windows.Forms.Padding(5);
            this.btAnnulerAssociation.Name = "btAnnulerAssociation";
            this.btAnnulerAssociation.Size = new System.Drawing.Size(100, 30);
            this.btAnnulerAssociation.TabIndex = 220;
            this.btAnnulerAssociation.Text = "Fermer";
            this.btAnnulerAssociation.UseVisualStyleBackColor = true;
            // 
            // tlpAssociations
            // 
            this.tlpAssociations.AutoSize = true;
            this.tlpAssociations.ColumnCount = 2;
            this.tlpAssociations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpAssociations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpAssociations.Controls.Add(this.label1, 0, 0);
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
            this.tlpAssociations.Size = new System.Drawing.Size(778, 525);
            this.tlpAssociations.TabIndex = 10;
            // 
            // flpBoutonsGestionAssociation2
            // 
            this.flpBoutonsGestionAssociation2.AutoSize = true;
            this.flpBoutonsGestionAssociation2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpAssociations.SetColumnSpan(this.flpBoutonsGestionAssociation2, 2);
            this.flpBoutonsGestionAssociation2.Controls.Add(this.btValiderAssociation);
            this.flpBoutonsGestionAssociation2.Controls.Add(this.btAnnulerAssociation);
            this.flpBoutonsGestionAssociation2.Controls.Add(this.btReset);
            this.flpBoutonsGestionAssociation2.Controls.Add(this.btChargerAssociation);
            this.flpBoutonsGestionAssociation2.Controls.Add(this.btSauverAssociation);
            this.flpBoutonsGestionAssociation2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBoutonsGestionAssociation2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBoutonsGestionAssociation2.Location = new System.Drawing.Point(0, 485);
            this.flpBoutonsGestionAssociation2.Margin = new System.Windows.Forms.Padding(0);
            this.flpBoutonsGestionAssociation2.Name = "flpBoutonsGestionAssociation2";
            this.flpBoutonsGestionAssociation2.Size = new System.Drawing.Size(778, 40);
            this.flpBoutonsGestionAssociation2.TabIndex = 213;
            // 
            // btReset
            // 
            this.btReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReset.Location = new System.Drawing.Point(378, 5);
            this.btReset.Margin = new System.Windows.Forms.Padding(5);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(100, 30);
            this.btReset.TabIndex = 210;
            this.btReset.Text = "Ré-initialiser";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // btChargerAssociation
            // 
            this.btChargerAssociation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btChargerAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btChargerAssociation.Location = new System.Drawing.Point(268, 5);
            this.btChargerAssociation.Margin = new System.Windows.Forms.Padding(5);
            this.btChargerAssociation.Name = "btChargerAssociation";
            this.btChargerAssociation.Size = new System.Drawing.Size(100, 30);
            this.btChargerAssociation.TabIndex = 212;
            this.btChargerAssociation.Text = "Charger";
            this.btChargerAssociation.UseVisualStyleBackColor = true;
            this.btChargerAssociation.Click += new System.EventHandler(this.btChargerAssociation_Click);
            // 
            // btSauverAssociation
            // 
            this.btSauverAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSauverAssociation.Location = new System.Drawing.Point(158, 5);
            this.btSauverAssociation.Margin = new System.Windows.Forms.Padding(5);
            this.btSauverAssociation.Name = "btSauverAssociation";
            this.btSauverAssociation.Size = new System.Drawing.Size(100, 30);
            this.btSauverAssociation.TabIndex = 211;
            this.btSauverAssociation.Text = "Sauver";
            this.btSauverAssociation.UseVisualStyleBackColor = true;
            this.btSauverAssociation.Click += new System.EventHandler(this.btSauverAssociation_Click);
            // 
            // saveFileDialogAssociation
            // 
            this.saveFileDialogAssociation.DefaultExt = "xml";
            this.saveFileDialogAssociation.FileName = "import_de_masse.xml";
            this.saveFileDialogAssociation.Filter = "Fichiers XML|*.xml|Fichiers texte|*.txt|Tous les fichiers|*.*";
            // 
            // openFileDialogAssociation
            // 
            this.openFileDialogAssociation.DefaultExt = "xml";
            this.openFileDialogAssociation.FileName = "import_de_masse.xml";
            this.openFileDialogAssociation.Filter = "Fichiers XML|*.xml|Fichiers texte|*.txt|Tous les fichiers|*.*";
            // 
            // FormImportDeMasse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 555);
            this.Controls.Add(this.tlpAssociations);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormImportDeMasse";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Import de masse";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormAssociation_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAssociation_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociation)).EndInit();
            this.tlpAssociations.ResumeLayout(false);
            this.tlpAssociations.PerformLayout();
            this.flpBoutonsGestionAssociation2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAssociation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btValiderAssociation;
        private System.Windows.Forms.Button btAnnulerAssociation;
        private System.Windows.Forms.TableLayoutPanel tlpAssociations;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Button btChargerAssociation;
        private System.Windows.Forms.Button btSauverAssociation;
        private System.Windows.Forms.FlowLayoutPanel flpBoutonsGestionAssociation2;
        private System.Windows.Forms.SaveFileDialog saveFileDialogAssociation;
        private System.Windows.Forms.OpenFileDialog openFileDialogAssociation;
    }
}