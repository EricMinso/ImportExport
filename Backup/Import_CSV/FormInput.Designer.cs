namespace Import_Export_Universel
{
    partial class FormInput
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInput));
        	this.tbSaisie = new System.Windows.Forms.TextBox();
        	this.labelSaisie = new System.Windows.Forms.Label();
        	this.btAnnuler = new System.Windows.Forms.Button();
        	this.btOK = new System.Windows.Forms.Button();
        	this.tlpSaisie = new System.Windows.Forms.TableLayoutPanel();
        	this.flpBoutons = new System.Windows.Forms.FlowLayoutPanel();
        	this.tlpSaisie.SuspendLayout();
        	this.flpBoutons.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// tbSaisie
        	// 
        	this.tbSaisie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        	this.tbSaisie.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tbSaisie.Location = new System.Drawing.Point(5, 38);
        	this.tbSaisie.Margin = new System.Windows.Forms.Padding(5);
        	this.tbSaisie.Name = "tbSaisie";
        	this.tbSaisie.Size = new System.Drawing.Size(286, 20);
        	this.tbSaisie.TabIndex = 0;
        	this.tbSaisie.TextChanged += new System.EventHandler(this.tbSaisie_TextChanged);
        	this.tbSaisie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSaisie_KeyPress);
        	// 
        	// labelSaisie
        	// 
        	this.labelSaisie.AutoSize = true;
        	this.labelSaisie.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.labelSaisie.Location = new System.Drawing.Point(5, 5);
        	this.labelSaisie.Margin = new System.Windows.Forms.Padding(5);
        	this.labelSaisie.Name = "labelSaisie";
        	this.labelSaisie.Size = new System.Drawing.Size(286, 23);
        	this.labelSaisie.TabIndex = 1;
        	this.labelSaisie.Text = "Saisissez la valeur :";
        	this.labelSaisie.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
        	// 
        	// btAnnuler
        	// 
        	this.btAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.btAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.btAnnuler.Location = new System.Drawing.Point(95, 5);
        	this.btAnnuler.Margin = new System.Windows.Forms.Padding(5);
        	this.btAnnuler.Name = "btAnnuler";
        	this.btAnnuler.Size = new System.Drawing.Size(90, 30);
        	this.btAnnuler.TabIndex = 2;
        	this.btAnnuler.Text = "Annuler";
        	this.btAnnuler.UseVisualStyleBackColor = true;
        	// 
        	// btOK
        	// 
        	this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.btOK.Location = new System.Drawing.Point(195, 5);
        	this.btOK.Margin = new System.Windows.Forms.Padding(5);
        	this.btOK.Name = "btOK";
        	this.btOK.Size = new System.Drawing.Size(90, 30);
        	this.btOK.TabIndex = 3;
        	this.btOK.Text = "OK";
        	this.btOK.UseVisualStyleBackColor = true;
        	// 
        	// tlpSaisie
        	// 
        	this.tlpSaisie.ColumnCount = 1;
        	this.tlpSaisie.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        	this.tlpSaisie.Controls.Add(this.labelSaisie, 0, 0);
        	this.tlpSaisie.Controls.Add(this.tbSaisie, 0, 1);
        	this.tlpSaisie.Controls.Add(this.flpBoutons, 0, 2);
        	this.tlpSaisie.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tlpSaisie.Location = new System.Drawing.Point(15, 15);
        	this.tlpSaisie.Margin = new System.Windows.Forms.Padding(0);
        	this.tlpSaisie.Name = "tlpSaisie";
        	this.tlpSaisie.RowCount = 3;
        	this.tlpSaisie.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        	this.tlpSaisie.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        	this.tlpSaisie.RowStyles.Add(new System.Windows.Forms.RowStyle());
        	this.tlpSaisie.Size = new System.Drawing.Size(296, 113);
        	this.tlpSaisie.TabIndex = 4;
        	// 
        	// flpBoutons
        	// 
        	this.flpBoutons.AutoSize = true;
        	this.flpBoutons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        	this.flpBoutons.Controls.Add(this.btOK);
        	this.flpBoutons.Controls.Add(this.btAnnuler);
        	this.flpBoutons.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.flpBoutons.Location = new System.Drawing.Point(3, 69);
        	this.flpBoutons.Name = "flpBoutons";
        	this.flpBoutons.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        	this.flpBoutons.Size = new System.Drawing.Size(290, 41);
        	this.flpBoutons.TabIndex = 2;
        	// 
        	// FormInput
        	// 
        	this.AcceptButton = this.btOK;
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(326, 143);
        	this.ControlBox = false;
        	this.Controls.Add(this.tlpSaisie);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "FormInput";
        	this.Padding = new System.Windows.Forms.Padding(15);
        	this.Text = "Saisie";
        	this.TopMost = true;
        	this.Load += new System.EventHandler(this.FormInput_Load);
        	this.tlpSaisie.ResumeLayout(false);
        	this.tlpSaisie.PerformLayout();
        	this.flpBoutons.ResumeLayout(false);
        	this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox tbSaisie;
        private System.Windows.Forms.Label labelSaisie;
        private System.Windows.Forms.Button btAnnuler;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.TableLayoutPanel tlpSaisie;
        private System.Windows.Forms.FlowLayoutPanel flpBoutons;
    }
}