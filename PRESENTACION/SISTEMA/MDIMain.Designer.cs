﻿namespace PRESENTACION.SISTEMA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIMain));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.sISTEMAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSUARIOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cAPTURAYCONSULTAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXCEPCIONESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSysUsuario = new System.Windows.Forms.TextBox();
            this.txtSysNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSysRol = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cALCULADORAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cERRARSESIÓNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cATALOGOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aCCESOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mODULOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pERMISOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mODULOSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cONTROLESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.uSUARIOSToolStripMenuItem,
            this.eXCEPCIONESToolStripMenuItem});
            this.sISTEMAToolStripMenuItem.Name = "sISTEMAToolStripMenuItem";
            this.sISTEMAToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.sISTEMAToolStripMenuItem.Text = "SISTEMA";
            // 
            // uSUARIOSToolStripMenuItem
            // 
            this.uSUARIOSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cAPTURAYCONSULTAToolStripMenuItem});
            this.uSUARIOSToolStripMenuItem.Name = "uSUARIOSToolStripMenuItem";
            this.uSUARIOSToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.uSUARIOSToolStripMenuItem.Text = "USUARIOS";
            // 
            // cAPTURAYCONSULTAToolStripMenuItem
            // 
            this.cAPTURAYCONSULTAToolStripMenuItem.Name = "cAPTURAYCONSULTAToolStripMenuItem";
            this.cAPTURAYCONSULTAToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.cAPTURAYCONSULTAToolStripMenuItem.Text = "CAPTURA Y CONSULTA";
            this.cAPTURAYCONSULTAToolStripMenuItem.Click += new System.EventHandler(this.cAPTURAYCONSULTAToolStripMenuItem_Click);
            // 
            // eXCEPCIONESToolStripMenuItem
            // 
            this.eXCEPCIONESToolStripMenuItem.Name = "eXCEPCIONESToolStripMenuItem";
            this.eXCEPCIONESToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.eXCEPCIONESToolStripMenuItem.Text = "EXCEPCIONES";
            this.eXCEPCIONESToolStripMenuItem.Click += new System.EventHandler(this.eXCEPCIONESToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(640, 24);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(250, 429);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSysRol);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtSysNombre);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtSysUsuario);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(218, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "USUARIO";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.toolStripContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(218, 421);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HERRAMIENTAS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(212, 390);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(3, 3);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(212, 415);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // txtSysUsuario
            // 
            this.txtSysUsuario.Location = new System.Drawing.Point(10, 59);
            this.txtSysUsuario.Name = "txtSysUsuario";
            this.txtSysUsuario.ReadOnly = true;
            this.txtSysUsuario.Size = new System.Drawing.Size(200, 25);
            this.txtSysUsuario.TabIndex = 1;
            // 
            // txtSysNombre
            // 
            this.txtSysNombre.Location = new System.Drawing.Point(10, 117);
            this.txtSysNombre.Multiline = true;
            this.txtSysNombre.Name = "txtSysNombre";
            this.txtSysNombre.ReadOnly = true;
            this.txtSysNombre.Size = new System.Drawing.Size(200, 50);
            this.txtSysNombre.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre:";
            // 
            // txtSysRol
            // 
            this.txtSysRol.Location = new System.Drawing.Point(10, 204);
            this.txtSysRol.Name = "txtSysRol";
            this.txtSysRol.ReadOnly = true;
            this.txtSysRol.Size = new System.Drawing.Size(200, 25);
            this.txtSysRol.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Rol:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cALCULADORAToolStripMenuItem,
            this.cERRARSESIÓNToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(212, 390);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cALCULADORAToolStripMenuItem
            // 
            this.cALCULADORAToolStripMenuItem.AutoSize = false;
            this.cALCULADORAToolStripMenuItem.Image = global::PRESENTACION.Properties.Resources.calculadora;
            this.cALCULADORAToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cALCULADORAToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cALCULADORAToolStripMenuItem.Name = "cALCULADORAToolStripMenuItem";
            this.cALCULADORAToolStripMenuItem.Size = new System.Drawing.Size(208, 52);
            this.cALCULADORAToolStripMenuItem.Text = "CALCULADORA";
            this.cALCULADORAToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cALCULADORAToolStripMenuItem.Click += new System.EventHandler(this.cALCULADORAToolStripMenuItem_Click);
            // 
            // cERRARSESIÓNToolStripMenuItem
            // 
            this.cERRARSESIÓNToolStripMenuItem.AutoSize = false;
            this.cERRARSESIÓNToolStripMenuItem.Image = global::PRESENTACION.Properties.Resources.cerrar_sesion;
            this.cERRARSESIÓNToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cERRARSESIÓNToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cERRARSESIÓNToolStripMenuItem.Name = "cERRARSESIÓNToolStripMenuItem";
            this.cERRARSESIÓNToolStripMenuItem.Size = new System.Drawing.Size(208, 52);
            this.cERRARSESIÓNToolStripMenuItem.Text = "CERRAR SESIÓN";
            this.cERRARSESIÓNToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cERRARSESIÓNToolStripMenuItem.Click += new System.EventHandler(this.cERRARSESIÓNToolStripMenuItem_Click);
            // 
            // cATALOGOSToolStripMenuItem
            // 
            this.cATALOGOSToolStripMenuItem.Image = global::PRESENTACION.Properties.Resources.registro;
            this.cATALOGOSToolStripMenuItem.Name = "cATALOGOSToolStripMenuItem";
            this.cATALOGOSToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.cATALOGOSToolStripMenuItem.Text = "CATALOGOS";
            // 
            // aCCESOToolStripMenuItem
            // 
            this.aCCESOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mODULOSToolStripMenuItem,
            this.pERMISOSToolStripMenuItem});
            this.aCCESOToolStripMenuItem.Image = global::PRESENTACION.Properties.Resources.permiso;
            this.aCCESOToolStripMenuItem.Name = "aCCESOToolStripMenuItem";
            this.aCCESOToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aCCESOToolStripMenuItem.Text = "ACCESO";
            // 
            // mODULOSToolStripMenuItem
            // 
            this.mODULOSToolStripMenuItem.Name = "mODULOSToolStripMenuItem";
            this.mODULOSToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.mODULOSToolStripMenuItem.Text = "MODULOS";
            this.mODULOSToolStripMenuItem.Click += new System.EventHandler(this.mODULOSToolStripMenuItem_Click);
            // 
            // pERMISOSToolStripMenuItem
            // 
            this.pERMISOSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mODULOSToolStripMenuItem1,
            this.cONTROLESToolStripMenuItem});
            this.pERMISOSToolStripMenuItem.Name = "pERMISOSToolStripMenuItem";
            this.pERMISOSToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.pERMISOSToolStripMenuItem.Text = "PERMISOS";
            // 
            // mODULOSToolStripMenuItem1
            // 
            this.mODULOSToolStripMenuItem1.Name = "mODULOSToolStripMenuItem1";
            this.mODULOSToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.mODULOSToolStripMenuItem1.Text = "MODULOS";
            this.mODULOSToolStripMenuItem1.Click += new System.EventHandler(this.mODULOSToolStripMenuItem1_Click);
            // 
            // cONTROLESToolStripMenuItem
            // 
            this.cONTROLESToolStripMenuItem.Name = "cONTROLESToolStripMenuItem";
            this.cONTROLESToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.cONTROLESToolStripMenuItem.Text = "CONTROLES";
            // 
            // MDIMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 453);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MDIMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JAADE - SISTEMA DE CONTROL DE PAGOS";
            this.Load += new System.EventHandler(this.MDIMain_Load);
            this.Shown += new System.EventHandler(this.MDIMain_Shown);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion


        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem sISTEMAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cATALOGOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aCCESOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mODULOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pERMISOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSUARIOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cAPTURAYCONSULTAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXCEPCIONESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mODULOSToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cONTROLESToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.TextBox txtSysRol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSysNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSysUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cALCULADORAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cERRARSESIÓNToolStripMenuItem;
    }
}



