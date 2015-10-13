namespace Import_Export_Universel
{
    partial class FormNouveauTampon
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNouveauTampon));
        	this.tbListeDesColonnes = new System.Windows.Forms.TextBox();
        	this.lbTitreNouveauTampon = new System.Windows.Forms.Label();
        	this.btOK = new System.Windows.Forms.Button();
        	this.btCancel = new System.Windows.Forms.Button();
        	this.label1 = new System.Windows.Forms.Label();
        	this.numNbLignes = new System.Windows.Forms.NumericUpDown();
        	this.tlp_Master_NouveauTampon = new System.Windows.Forms.TableLayoutPanel();
        	this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        	this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
        	((System.ComponentModel.ISupportInitialize)(this.numNbLignes)).BeginInit();
        	this.tlp_Master_NouveauTampon.SuspendLayout();
        	this.flowLayoutPanel1.SuspendLayout();
        	this.flowLayoutPanel2.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// tbListeDesColonnes
        	// 
        	this.tbListeDesColonnes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        	this.tbListeDesColonnes.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tbListeDesColonnes.Location = new System.Drawing.Point(5, 28);
        	this.tbListeDesColonnes.Margin = new System.Windows.Forms.Padding(5);
        	this.tbListeDesColonnes.Multiline = true;
        	this.tbListeDesColonnes.Name = "tbListeDesColonnes";
        	this.tbListeDesColonnes.Size = new System.Drawing.Size(417, 241);
        	this.tbListeDesColonnes.TabIndex = 0;
        	// 
        	// lbTitreNouveauTampon
        	// 
        	this.lbTitreNouveauTampon.AutoSize = true;
        	this.lbTitreNouveauTampon.Location = new System.Drawing.Point(5, 5);
        	this.lbTitreNouveauTampon.Margin = new System.Windows.Forms.Padding(5);
        	this.lbTitreNouveauTampon.Name = "lbTitreNouveauTampon";
        	this.lbTitreNouveauTampon.Size = new System.Drawing.Size(382, 13);
        	this.lbTitreNouveauTampon.TabIndex = 1;
        	this.lbTitreNouveauTampon.Text = "Saisissez le nom des colonnes du nouveau tampon (1 ligne = 1 titre de colonne)";
        	// 
        	// btOK
        	// 
        	this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.btOK.Location = new System.Drawing.Point(256, 5);
        	this.btOK.Margin = new System.Windows.Forms.Padding(5);
        	this.btOK.Name = "btOK";
        	this.btOK.Size = new System.Drawing.Size(75, 23);
        	this.btOK.TabIndex = 2;
        	this.btOK.Text = "OK";
        	this.btOK.UseVisualStyleBackColor = true;
        	// 
        	// btCancel
        	// 
        	this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.btCancel.Location = new System.Drawing.Point(341, 5);
        	this.btCancel.Margin = new System.Windows.Forms.Padding(5);
        	this.btCancel.Name = "btCancel";
        	this.btCancel.Size = new System.Drawing.Size(75, 23);
        	this.btCancel.TabIndex = 3;
        	this.btCancel.Text = "Annuler";
        	this.btCancel.UseVisualStyleBackColor = true;
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(5, 5);
        	this.label1.Margin = new System.Windows.Forms.Padding(5);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(162, 13);
        	this.label1.TabIndex = 4;
        	this.label1.Text = "Nombre de lignes vierges à créer";
        	// 
        	// numNbLignes
        	// 
        	this.numNbLignes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        	this.numNbLignes.Location = new System.Drawing.Point(175, 3);
        	this.numNbLignes.Maximum = new decimal(new int[] {
        	        	        	1000,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.numNbLignes.Name = "numNbLignes";
        	this.numNbLignes.Size = new System.Drawing.Size(120, 20);
        	this.numNbLignes.TabIndex = 1;
        	// 
        	// tlp_Master_NouveauTampon
        	// 
        	this.tlp_Master_NouveauTampon.AutoSize = true;
        	this.tlp_Master_NouveauTampon.ColumnCount = 1;
        	this.tlp_Master_NouveauTampon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        	this.tlp_Master_NouveauTampon.Controls.Add(this.flowLayoutPanel1, 0, 3);
        	this.tlp_Master_NouveauTampon.Controls.Add(this.lbTitreNouveauTampon, 0, 0);
        	this.tlp_Master_NouveauTampon.Controls.Add(this.flowLayoutPanel2, 0, 2);
        	this.tlp_Master_NouveauTampon.Controls.Add(this.tbListeDesColonnes, 0, 1);
        	this.tlp_Master_NouveauTampon.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tlp_Master_NouveauTampon.Location = new System.Drawing.Point(15, 15);
        	this.tlp_Master_NouveauTampon.Name = "tlp_Master_NouveauTampon";
        	this.tlp_Master_NouveauTampon.RowCount = 4;
        	this.tlp_Master_NouveauTampon.RowStyles.Add(new System.Windows.Forms.RowStyle());
        	this.tlp_Master_NouveauTampon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        	this.tlp_Master_NouveauTampon.RowStyles.Add(new System.Windows.Forms.RowStyle());
        	this.tlp_Master_NouveauTampon.RowStyles.Add(new System.Windows.Forms.RowStyle());
        	this.tlp_Master_NouveauTampon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
        	this.tlp_Master_NouveauTampon.Size = new System.Drawing.Size(427, 345);
        	this.tlp_Master_NouveauTampon.TabIndex = 7;
        	// 
        	// flowLayoutPanel1
        	// 
        	this.flowLayoutPanel1.AutoSize = true;
        	this.flowLayoutPanel1.Controls.Add(this.btCancel);
        	this.flowLayoutPanel1.Controls.Add(this.btOK);
        	this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 309);
        	this.flowLayoutPanel1.Name = "flowLayoutPanel1";
        	this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        	this.flowLayoutPanel1.Size = new System.Drawing.Size(421, 33);
        	this.flowLayoutPanel1.TabIndex = 0;
        	// 
        	// flowLayoutPanel2
        	// 
        	this.flowLayoutPanel2.AutoSize = true;
        	this.flowLayoutPanel2.Controls.Add(this.label1);
        	this.flowLayoutPanel2.Controls.Add(this.numNbLignes);
        	this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 277);
        	this.flowLayoutPanel2.Name = "flowLayoutPanel2";
        	this.flowLayoutPanel2.Size = new System.Drawing.Size(421, 26);
        	this.flowLayoutPanel2.TabIndex = 2;
        	// 
        	// FormNouveauTampon
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(457, 375);
        	this.Controls.Add(this.tlp_Master_NouveauTampon);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.Name = "FormNouveauTampon";
        	this.Padding = new System.Windows.Forms.Padding(15);
        	this.Text = "Nouveau Tampon";
        	this.TopMost = true;
        	this.Load += new System.EventHandler(this.FormNouveauTamponLoad);
        	((System.ComponentModel.ISupportInitialize)(this.numNbLignes)).EndInit();
        	this.tlp_Master_NouveauTampon.ResumeLayout(false);
        	this.tlp_Master_NouveauTampon.PerformLayout();
        	this.flowLayoutPanel1.ResumeLayout(false);
        	this.flowLayoutPanel2.ResumeLayout(false);
        	this.flowLayoutPanel2.PerformLayout();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tbListeDesColonnes;
        private System.Windows.Forms.Label lbTitreNouveauTampon;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numNbLignes;
        private System.Windows.Forms.TableLayoutPanel tlp_Master_NouveauTampon;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}