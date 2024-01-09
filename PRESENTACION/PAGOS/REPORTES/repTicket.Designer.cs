namespace PRESENTACION.PAGOS.REPORTES
{
    partial class repTicket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(repTicket));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEnvioWhats = new System.Windows.Forms.Button();
            this.btnEnvioEmail = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PRESENTACION.PAGOS.REPORTES.repTicketPago.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(774, 561);
            this.reportViewer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEnvioEmail);
            this.panel1.Controls.Add(this.btnEnvioWhats);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 513);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(774, 48);
            this.panel1.TabIndex = 1;
            // 
            // btnEnvioWhats
            // 
            this.btnEnvioWhats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnvioWhats.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnvioWhats.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnvioWhats.Image = global::PRESENTACION.Properties.Resources.reporte;
            this.btnEnvioWhats.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnvioWhats.Location = new System.Drawing.Point(545, 5);
            this.btnEnvioWhats.MaximumSize = new System.Drawing.Size(110, 40);
            this.btnEnvioWhats.MinimumSize = new System.Drawing.Size(110, 40);
            this.btnEnvioWhats.Name = "btnEnvioWhats";
            this.btnEnvioWhats.Size = new System.Drawing.Size(110, 40);
            this.btnEnvioWhats.TabIndex = 43;
            this.btnEnvioWhats.Text = "     Whats App";
            this.btnEnvioWhats.UseVisualStyleBackColor = true;
            // 
            // btnEnvioEmail
            // 
            this.btnEnvioEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnvioEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnvioEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnvioEmail.Image = global::PRESENTACION.Properties.Resources.reporte;
            this.btnEnvioEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnvioEmail.Location = new System.Drawing.Point(661, 5);
            this.btnEnvioEmail.MaximumSize = new System.Drawing.Size(110, 40);
            this.btnEnvioEmail.MinimumSize = new System.Drawing.Size(110, 40);
            this.btnEnvioEmail.Name = "btnEnvioEmail";
            this.btnEnvioEmail.Size = new System.Drawing.Size(110, 40);
            this.btnEnvioEmail.TabIndex = 44;
            this.btnEnvioEmail.Text = "     Email";
            this.btnEnvioEmail.UseVisualStyleBackColor = true;
            // 
            // repTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "repTicket";
            this.Text = "repTicket";
            this.Load += new System.EventHandler(this.repTicket_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEnvioEmail;
        private System.Windows.Forms.Button btnEnvioWhats;
    }
}