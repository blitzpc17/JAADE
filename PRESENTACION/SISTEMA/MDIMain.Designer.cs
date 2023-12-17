namespace PRESENTACION.SISTEMA
{
    partial class MDIMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.sISTEMAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cATALOGOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aCCESOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mODULOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pERMISOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSUARIOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cAPTURAYCONSULTAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.AutoSize = false;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sISTEMAToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(890, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "MenuStrip";
            // 
            // sISTEMAToolStripMenuItem
            // 
            this.sISTEMAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cATALOGOSToolStripMenuItem,
            this.aCCESOToolStripMenuItem,
            this.uSUARIOSToolStripMenuItem});
            this.sISTEMAToolStripMenuItem.Name = "sISTEMAToolStripMenuItem";
            this.sISTEMAToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.sISTEMAToolStripMenuItem.Text = "SISTEMA";
            // 
            // cATALOGOSToolStripMenuItem
            // 
            this.cATALOGOSToolStripMenuItem.Name = "cATALOGOSToolStripMenuItem";
            this.cATALOGOSToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.cATALOGOSToolStripMenuItem.Text = "CATALOGOS";
            // 
            // aCCESOToolStripMenuItem
            // 
            this.aCCESOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mODULOSToolStripMenuItem,
            this.pERMISOSToolStripMenuItem});
            this.aCCESOToolStripMenuItem.Name = "aCCESOToolStripMenuItem";
            this.aCCESOToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.aCCESOToolStripMenuItem.Text = "ACCESO";
            // 
            // mODULOSToolStripMenuItem
            // 
            this.mODULOSToolStripMenuItem.Name = "mODULOSToolStripMenuItem";
            this.mODULOSToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.mODULOSToolStripMenuItem.Text = "MODULOS";
            // 
            // pERMISOSToolStripMenuItem
            // 
            this.pERMISOSToolStripMenuItem.Name = "pERMISOSToolStripMenuItem";
            this.pERMISOSToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.pERMISOSToolStripMenuItem.Text = "PERMISOS";
            // 
            // uSUARIOSToolStripMenuItem
            // 
            this.uSUARIOSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cAPTURAYCONSULTAToolStripMenuItem});
            this.uSUARIOSToolStripMenuItem.Name = "uSUARIOSToolStripMenuItem";
            this.uSUARIOSToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.uSUARIOSToolStripMenuItem.Text = "USUARIOS";
            // 
            // cAPTURAYCONSULTAToolStripMenuItem
            // 
            this.cAPTURAYCONSULTAToolStripMenuItem.Name = "cAPTURAYCONSULTAToolStripMenuItem";
            this.cAPTURAYCONSULTAToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.cAPTURAYCONSULTAToolStripMenuItem.Text = "CAPTURA Y CONSULTA";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(890, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel.Text = "Estado";
            // 
            // MDIMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 453);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MDIMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MDIMain";
            this.Load += new System.EventHandler(this.MDIMain_Load);
            this.Shown += new System.EventHandler(this.MDIMain_Shown);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem sISTEMAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cATALOGOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aCCESOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mODULOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pERMISOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSUARIOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cAPTURAYCONSULTAToolStripMenuItem;
    }
}



