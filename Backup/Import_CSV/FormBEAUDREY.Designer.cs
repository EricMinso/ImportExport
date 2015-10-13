/*
 * Created by SharpDevelop.
 * User: Minso
 * Date: 30/05/2013
 * Time: 14:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Import_Export_Universel
{
	partial class FormBEAUDREY
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.tlp_BEAUDREYmaster = new System.Windows.Forms.TableLayoutPanel();
			this.btOK = new System.Windows.Forms.Button();
			this.tbRepertoire = new System.Windows.Forms.TextBox();
			this.btTraiterFichier = new System.Windows.Forms.Button();
			this.dgvBEAUDREY = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.btTraiterTousLesFichiers = new System.Windows.Forms.Button();
			this.tlp_BEAUDREYmaster.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvBEAUDREY)).BeginInit();
			this.SuspendLayout();
			// 
			// tlp_BEAUDREYmaster
			// 
			this.tlp_BEAUDREYmaster.ColumnCount = 4;
			this.tlp_BEAUDREYmaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_BEAUDREYmaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlp_BEAUDREYmaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlp_BEAUDREYmaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlp_BEAUDREYmaster.Controls.Add(this.btOK, 3, 2);
			this.tlp_BEAUDREYmaster.Controls.Add(this.tbRepertoire, 0, 0);
			this.tlp_BEAUDREYmaster.Controls.Add(this.btTraiterFichier, 2, 2);
			this.tlp_BEAUDREYmaster.Controls.Add(this.dgvBEAUDREY, 0, 1);
			this.tlp_BEAUDREYmaster.Controls.Add(this.label1, 0, 2);
			this.tlp_BEAUDREYmaster.Controls.Add(this.btTraiterTousLesFichiers, 1, 2);
			this.tlp_BEAUDREYmaster.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp_BEAUDREYmaster.Location = new System.Drawing.Point(15, 15);
			this.tlp_BEAUDREYmaster.Name = "tlp_BEAUDREYmaster";
			this.tlp_BEAUDREYmaster.RowCount = 3;
			this.tlp_BEAUDREYmaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlp_BEAUDREYmaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp_BEAUDREYmaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlp_BEAUDREYmaster.Size = new System.Drawing.Size(650, 479);
			this.tlp_BEAUDREYmaster.TabIndex = 0;
			// 
			// btOK
			// 
			this.btOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOK.Location = new System.Drawing.Point(543, 442);
			this.btOK.Margin = new System.Windows.Forms.Padding(7);
			this.btOK.Name = "btOK";
			this.btOK.Size = new System.Drawing.Size(100, 30);
			this.btOK.TabIndex = 1;
			this.btOK.Text = "Fermer";
			this.btOK.UseVisualStyleBackColor = true;
			// 
			// tbRepertoire
			// 
			this.tbRepertoire.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tlp_BEAUDREYmaster.SetColumnSpan(this.tbRepertoire, 4);
			this.tbRepertoire.Location = new System.Drawing.Point(3, 3);
			this.tbRepertoire.Name = "tbRepertoire";
			this.tbRepertoire.Size = new System.Drawing.Size(644, 20);
			this.tbRepertoire.TabIndex = 0;
			// 
			// btTraiterFichier
			// 
			this.btTraiterFichier.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btTraiterFichier.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btTraiterFichier.Location = new System.Drawing.Point(353, 442);
			this.btTraiterFichier.Margin = new System.Windows.Forms.Padding(7);
			this.btTraiterFichier.Name = "btTraiterFichier";
			this.btTraiterFichier.Size = new System.Drawing.Size(176, 30);
			this.btTraiterFichier.TabIndex = 2;
			this.btTraiterFichier.Text = "Traiter le fichier";
			this.btTraiterFichier.UseVisualStyleBackColor = true;
			this.btTraiterFichier.Click += new System.EventHandler(this.BtTraiterFichierClick);
			// 
			// dgvBEAUDREY
			// 
			this.dgvBEAUDREY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tlp_BEAUDREYmaster.SetColumnSpan(this.dgvBEAUDREY, 4);
			this.dgvBEAUDREY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvBEAUDREY.Location = new System.Drawing.Point(7, 33);
			this.dgvBEAUDREY.Margin = new System.Windows.Forms.Padding(7);
			this.dgvBEAUDREY.Name = "dgvBEAUDREY";
			this.dgvBEAUDREY.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvBEAUDREY.Size = new System.Drawing.Size(636, 395);
			this.dgvBEAUDREY.TabIndex = 3;
			this.dgvBEAUDREY.DoubleClick += new System.EventHandler(this.DgvBEAUDREYDoubleClick);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 435);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(150, 44);
			this.label1.TabIndex = 4;
			this.label1.Text = "label1";
			// 
			// btTraiterTousLesFichiers
			// 
			this.btTraiterTousLesFichiers.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btTraiterTousLesFichiers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btTraiterTousLesFichiers.Location = new System.Drawing.Point(163, 442);
			this.btTraiterTousLesFichiers.Margin = new System.Windows.Forms.Padding(7);
			this.btTraiterTousLesFichiers.Name = "btTraiterTousLesFichiers";
			this.btTraiterTousLesFichiers.Size = new System.Drawing.Size(176, 30);
			this.btTraiterTousLesFichiers.TabIndex = 2;
			this.btTraiterTousLesFichiers.Text = "Traiter TOUS les fichiers";
			this.btTraiterTousLesFichiers.UseVisualStyleBackColor = true;
			this.btTraiterTousLesFichiers.Click += new System.EventHandler(this.BtTraiterTousLesFichiersClick);
			// 
			// FormBEAUDREY
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 509);
			this.Controls.Add(this.tlp_BEAUDREYmaster);
			this.Name = "FormBEAUDREY";
			this.Padding = new System.Windows.Forms.Padding(15);
			this.Text = "FormBEAUDREY";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FormBEAUDREYLoad);
			this.tlp_BEAUDREYmaster.ResumeLayout(false);
			this.tlp_BEAUDREYmaster.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvBEAUDREY)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btTraiterTousLesFichiers;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dgvBEAUDREY;
		private System.Windows.Forms.Button btTraiterFichier;
		private System.Windows.Forms.Button btOK;
		private System.Windows.Forms.TextBox tbRepertoire;
		private System.Windows.Forms.TableLayoutPanel tlp_BEAUDREYmaster;
	}
}
