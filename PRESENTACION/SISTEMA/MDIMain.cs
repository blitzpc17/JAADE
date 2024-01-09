﻿using PRESENTACION.UTILERIAS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION.SISTEMA
{
    public partial class MDIMain : Form
    {
        private int childFormNumber = 0;
        private bool darktheme = false;

        public MDIMain()
        {
            InitializeComponent();
        }

        private void MDIMain_Load(object sender, EventArgs e)
        {

        }

        private void MDIMain_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this, false);
            WindowState = FormWindowState.Maximized;
       
            
            
        }

        private void asignacionLotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new PAGOS.formAsignacionLotes();
            form.MdiParent = this;
            form.Show();
        }

        private void capturaYConsultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new PAGOS.formClientes();
            form.MdiParent = this;
            form.Show();
        }

        private void eXCEPCIONESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SISTEMA.formExcepciones();
            form.MdiParent = this;
            form.Show();
        }

        private void capturaYConsultaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new PAGOS.formPagos();
            form.MdiParent = this;
            form.Show();
        }
    }
}
