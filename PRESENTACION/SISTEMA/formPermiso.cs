using PRESENTACION.UTILERIAS;
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
    public partial class formPermiso : Form
    {
        public formPermiso()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void formPermiso_Load(object sender, EventArgs e)
        {

        }

        private void formPermiso_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnModulo_Click(object sender, EventArgs e)
        {
            var busModulos = new BUSQUEDA.busModulos();
            busModulos.ShowDialog();
        }

        private void btnUsuarioSolicita_Click(object sender, EventArgs e)
        {
            var busUsuarios = new BUSQUEDA.busUsuarios();
            busUsuarios.ShowDialog();
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
