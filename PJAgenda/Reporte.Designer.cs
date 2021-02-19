namespace PJAgenda
{
    partial class Reporte
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_folio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_asistentes = new System.Windows.Forms.ComboBox();
            this.btn_generar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.Vacio1 = new PJAgenda.Vacio();
            this.txt_folio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "N° Folio:";
            // 
            // lbl_folio
            // 
            this.lbl_folio.AutoSize = true;
            this.lbl_folio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_folio.Location = new System.Drawing.Point(116, 86);
            this.lbl_folio.Name = "lbl_folio";
            this.lbl_folio.Size = new System.Drawing.Size(0, 17);
            this.lbl_folio.TabIndex = 1;
            this.lbl_folio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(333, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Asistentes";
            // 
            // cb_asistentes
            // 
            this.cb_asistentes.FormattingEnabled = true;
            this.cb_asistentes.Location = new System.Drawing.Point(445, 85);
            this.cb_asistentes.Name = "cb_asistentes";
            this.cb_asistentes.Size = new System.Drawing.Size(278, 21);
            this.cb_asistentes.TabIndex = 3;
            this.cb_asistentes.SelectedIndexChanged += new System.EventHandler(this.cb_asistentes_SelectedIndexChanged);
            // 
            // btn_generar
            // 
            this.btn_generar.AutoSize = true;
            this.btn_generar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_generar.Depth = 0;
            this.btn_generar.Icon = null;
            this.btn_generar.Location = new System.Drawing.Point(792, 79);
            this.btn_generar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_generar.Name = "btn_generar";
            this.btn_generar.Primary = true;
            this.btn_generar.Size = new System.Drawing.Size(145, 36);
            this.btn_generar.TabIndex = 4;
            this.btn_generar.Text = "Generar Reporte";
            this.btn_generar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_generar.UseVisualStyleBackColor = true;
            this.btn_generar.Click += new System.EventHandler(this.btn_generar_Click);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(13, 122);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.Vacio1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(999, 542);
            this.crystalReportViewer1.TabIndex = 5;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // txt_folio
            // 
            this.txt_folio.AutoSize = true;
            this.txt_folio.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_folio.Location = new System.Drawing.Point(104, 86);
            this.txt_folio.Name = "txt_folio";
            this.txt_folio.Size = new System.Drawing.Size(0, 20);
            this.txt_folio.TabIndex = 6;
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 676);
            this.Controls.Add(this.txt_folio);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.btn_generar);
            this.Controls.Add(this.cb_asistentes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_folio);
            this.Controls.Add(this.label1);
            this.Name = "Reporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_folio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_asistentes;
        private MaterialSkin.Controls.MaterialRaisedButton btn_generar;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Label txt_folio;
        private Vacio Vacio1;
    }
}