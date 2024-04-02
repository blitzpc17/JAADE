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
    public partial class busUsuarios : Form
    {
        private busUsuariosLogica contexto;
        public clsUsuario ObjEntidad;
        private int rowIndexSeleccionado = -1;

        public busUsuarios()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            try
            {
                contexto = new busUsuariosLogica();
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
            if (column != contexto.index)
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
            dgvRegistros.DataSource = contexto.LstUsuarioAux;
            Apariencias();
        }

        private void Apariencias()
        {
            if (dgvRegistros.DataSource == null) return;
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].HeaderText = "ALIAS";
            dgvRegistros.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].Visible = false;
            dgvRegistros.Columns[3].HeaderText = "NOMBRE";
            dgvRegistros.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[4].Visible = false;//nombres
            dgvRegistros.Columns[5].Visible = false;//apellidos
            dgvRegistros.Columns[6].Visible = false;//curp
            dgvRegistros.Columns[7].Visible = false;      //fechanac      
            dgvRegistros.Columns[8].Visible = false;//rolid
            dgvRegistros.Columns[9].HeaderText = "ROL";
            dgvRegistros.Columns[10].Visible=false;//estadoid
            dgvRegistros.Columns[11].HeaderText = "ESTADO";
            dgvRegistros.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[13].Visible = false; //personaId


            tsTotalRegistros.Text = contexto.LstUsuario.Count.ToString("N0");


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            if (rowIndexSeleccionado == -1) return;
            rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            ObjEntidad = contexto.ObtenerRegistro(rowIndexSeleccionado);
            Close();
        }

        private void busUsuarios_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void busUsuarios_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
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
    }
}
