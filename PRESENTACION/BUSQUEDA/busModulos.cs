using CAPADATOS.Entidades;
using CAPALOGICA.LOGICAS.BUSQUEDA;
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

namespace PRESENTACION.BUSQUEDA
{
    public partial class busModulos : Form
    {
        private busModulosLogica contexto;
        public clsModulo ObjEntidad;
        private int rowIndexSeleccionado = -1;

        public busModulos()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }      
       

        private void InicializarForm()
        {
            try
            {
                contexto = new busModulosLogica();
                Listar();
                ordenar(1);
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar cargar el módulo. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
         
        }

        public void Listar()
        {
            contexto.ListarRegistros();
        }

        private void filtrar(int column, string termino)
        {
            if(column != contexto.index)
            {
                ordenar(column);
            }

            if (contexto.Filtrar(column, termino))
            {
                contexto.indexAux = contexto.index;
                dgvRegistros.Rows[contexto.index].Cells[column].Selected = true;
                dgvRegistros.FirstDisplayedScrollingRowIndex = contexto.index;
            }
        }

        private void ordenar(int column)
        {            
            txtBuscar.Focus();
            contexto.Ordenar(column);
            dgvRegistros.DataSource = contexto.LstModuloAux;
            Apariencias();
        }

        private void Apariencias()
        {
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].HeaderText = "NOMBRE";
            dgvRegistros.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].HeaderText = "RUTAS";
            dgvRegistros.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].HeaderText = "MODULO PADRE";
            dgvRegistros.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[5].Visible = false;

            tsTotalRegistros.Text = contexto.LstModuloAux.Count.ToString("N0");

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvRegistros.Rows.Count <= 0) return;

            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                contexto.index = -1;
                return;
            }

            filtrar(contexto.Column, txtBuscar.Text);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            if (rowIndexSeleccionado == -1) return;
            ObjEntidad = contexto.ObtenerRegistro(rowIndexSeleccionado);
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            ObjEntidad = contexto.ObtenerRegistro(rowIndexSeleccionado);
            Close();
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            if (contexto.Column != e.ColumnIndex)
            {
                contexto.Column = e.ColumnIndex;
                txtBuscar.Clear();
            }
            else
            {
                rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            }            
        }

        private void busModulos_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void busModulos_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }
    }
}
