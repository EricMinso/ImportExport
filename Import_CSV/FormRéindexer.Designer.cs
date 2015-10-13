namespace Import_Export_CSV
{
    partial class FormRéindexer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRéindexer));
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.cbNomsColonnes = new System.Windows.Forms.ComboBox();
            this.lbColonne = new System.Windows.Forms.Label();
            this.numDépart = new System.Windows.Forms.NumericUpDown();
            this.lbDépart = new System.Windows.Forms.Label();
            this.rbIncrémentalNumérique = new System.Windows.Forms.RadioButton();
            this.rbUnique = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbNULL = new System.Windows.Forms.RadioButton();
            this.tbValeurUniqueCommune = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numDépart)).BeginInit();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancel.Location = new System.Drawing.Point(15, 267);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 0;
            this.btCancel.Text = "Annuler";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOK.Location = new System.Drawing.Point(336, 267);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "Réindexer";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // cbNomsColonnes
            // 
            this.cbNomsColonnes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbNomsColonnes.FormattingEnabled = true;
            this.cbNomsColonnes.Location = new System.Drawing.Point(195, 12);
            this.cbNomsColonnes.Name = "cbNomsColonnes";
            this.cbNomsColonnes.Size = new System.Drawing.Size(143, 21);
            this.cbNomsColonnes.TabIndex = 2;
            this.cbNomsColonnes.SelectedIndexChanged += new System.EventHandler(this.cbNomsColonnes_SelectedIndexChanged);
            // 
            // lbColonne
            // 
            this.lbColonne.AutoSize = true;
            this.lbColonne.Location = new System.Drawing.Point(12, 15);
            this.lbColonne.Name = "lbColonne";
            this.lbColonne.Size = new System.Drawing.Size(101, 13);
            this.lbColonne.TabIndex = 3;
            this.lbColonne.Text = "Colonne à réindexer";
            // 
            // numDépart
            // 
            this.numDépart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numDépart.Location = new System.Drawing.Point(195, 84);
            this.numDépart.Name = "numDépart";
            this.numDépart.Size = new System.Drawing.Size(143, 20);
            this.numDépart.TabIndex = 4;
            this.numDépart.Click += new System.EventHandler(this.numDépart_Click);
            // 
            // lbDépart
            // 
            this.lbDépart.AutoSize = true;
            this.lbDépart.Location = new System.Drawing.Point(50, 86);
            this.lbDépart.Name = "lbDépart";
            this.lbDépart.Size = new System.Drawing.Size(81, 13);
            this.lbDépart.TabIndex = 5;
            this.lbDépart.Text = "Index de départ";
            // 
            // rbIncrémentalNumérique
            // 
            this.rbIncrémentalNumérique.AutoSize = true;
            this.rbIncrémentalNumérique.Checked = true;
            this.rbIncrémentalNumérique.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncrémentalNumérique.Location = new System.Drawing.Point(15, 66);
            this.rbIncrémentalNumérique.Name = "rbIncrémentalNumérique";
            this.rbIncrémentalNumérique.Size = new System.Drawing.Size(169, 17);
            this.rbIncrémentalNumérique.TabIndex = 6;
            this.rbIncrémentalNumérique.TabStop = true;
            this.rbIncrémentalNumérique.Text = "Valeur incrémentale numérique";
            this.rbIncrémentalNumérique.UseVisualStyleBackColor = true;
            this.rbIncrémentalNumérique.Click += new System.EventHandler(this.rbIncrémentalNumérique_Click);
            // 
            // rbUnique
            // 
            this.rbUnique.AutoSize = true;
            this.rbUnique.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbUnique.Location = new System.Drawing.Point(15, 136);
            this.rbUnique.Name = "rbUnique";
            this.rbUnique.Size = new System.Drawing.Size(138, 17);
            this.rbUnique.TabIndex = 7;
            this.rbUnique.Text = "Valeur unique commune";
            this.rbUnique.UseVisualStyleBackColor = true;
            this.rbUnique.Click += new System.EventHandler(this.rbUnique_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Texte, Date, ou Nombre";
            // 
            // rbNULL
            // 
            this.rbNULL.AutoSize = true;
            this.rbNULL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbNULL.Location = new System.Drawing.Point(15, 202);
            this.rbNULL.Name = "rbNULL";
            this.rbNULL.Size = new System.Drawing.Size(85, 17);
            this.rbNULL.TabIndex = 9;
            this.rbNULL.Text = "Valeur NULL";
            this.rbNULL.UseVisualStyleBackColor = true;
            // 
            // tbValeurUniqueCommune
            // 
            this.tbValeurUniqueCommune.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValeurUniqueCommune.Location = new System.Drawing.Point(195, 153);
            this.tbValeurUniqueCommune.Name = "tbValeurUniqueCommune";
            this.tbValeurUniqueCommune.Size = new System.Drawing.Size(143, 20);
            this.tbValeurUniqueCommune.TabIndex = 10;
            this.tbValeurUniqueCommune.Click += new System.EventHandler(this.tbValeurUniqueCommune_Click);
            // 
            // FormRéindexer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 302);
            this.ControlBox = false;
            this.Controls.Add(this.tbValeurUniqueCommune);
            this.Controls.Add(this.rbNULL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbUnique);
            this.Controls.Add(this.rbIncrémentalNumérique);
            this.Controls.Add(this.lbDépart);
            this.Controls.Add(this.numDépart);
            this.Controls.Add(this.lbColonne);
            this.Controls.Add(this.cbNomsColonnes);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRéindexer";
            this.Text = "Réindexer";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormRéindexer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDépart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.ComboBox cbNomsColonnes;
        private System.Windows.Forms.Label lbColonne;
        private System.Windows.Forms.NumericUpDown numDépart;
        private System.Windows.Forms.Label lbDépart;
        private System.Windows.Forms.RadioButton rbIncrémentalNumérique;
        private System.Windows.Forms.RadioButton rbUnique;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbNULL;
        private System.Windows.Forms.TextBox tbValeurUniqueCommune;
    }
}