using CAPADATOS.Entidades;
using CAPALOGICA.LOGICAS.PAGOS;
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

    public partial class busClientes : Form
    {
        private busClientesLogica contexto;
        public clsClientes ObjEntidad;
        private int rowIndexSeleccionado = -1;

        public busClientes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            try
            {
                contexto = new busClientesLogica();
                Listar();
                ordenar(1);
                contexto.Column = 2;
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
            dgvRegistros.DataSource = contexto.LstClientesAux;
            Apariencias();
        }

        private void Apariencias()
        {
            if (dgvRegistros.DataSource == null) return;
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].HeaderText = "CLAVE";
            dgvRegistros.Columns[1].Width = 90;
            dgvRegistros.Columns[2].HeaderText = "NOMBRE"; //cliente
            dgvRegistros.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[3].Visible = false;//nombres
            dgvRegistros.Columns[4].Visible = false;//apellidos
            dgvRegistros.Columns[5].Visible = false;//curp
            dgvRegistros.Columns[6].Visible = false;//fechanacimiento
            dgvRegistros.Columns[7].Visible = false;//estadoid
            dgvRegistros.Columns[8].HeaderText = "ESTADO";
            dgvRegistros.Columns[8].Width = 100;


            tsTotalRegistros.Text = contexto.LstClientes.Count.ToString("N0");


        }

        private void busClientes_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void busClientes_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            /*if (dgvRegistros.DataSource == null) return;
            if (rowIndexSeleccionado == -1) return;
            rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            ObjEntidad = contexto.ObtenerRegistro(rowIndexSeleccionado);
            Close();*/

            if (dgvRegistros.DataSource == null) return;
            if (rowIndexSeleccionado == -1) return;
            ObjEntidad = contexto.ObtenerRegistro(rowIndexSeleccionado);
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
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

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {          

            if (dgvRegistros.DataSource == null) return;
            rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            ObjEntidad = contexto.ObtenerRegistro(rowIndexSeleccionado);
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dgvRegistros.Rows.Count <= 0) return;

            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                contexto.index = -1;
                return;
            }

            filtrar(contexto.Column, txtBuscar.Text);
        }
    }
}
