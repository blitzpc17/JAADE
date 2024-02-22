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
    public partial class busContratos : Form
    {
        private busContratoLogica contexto;
        public clsContratoCliente ObjEntidad;
        private int rowIndexSeleccionado = -1;

        public busContratos()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            try
            {
                contexto = new busContratoLogica();
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
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = contexto.LstContratosAux;
            Apariencias();
        }

        private void Apariencias()
        {
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[0].Frozen = false;
            dgvRegistros.Columns[1].HeaderText = "FOLIO";
            dgvRegistros.Columns[1].Frozen = true;
            dgvRegistros.Columns[1].Width = 100;
            dgvRegistros.Columns[2].HeaderText = "FECHA EMISIÓN";
            dgvRegistros.Columns[2].Frozen = true;
            dgvRegistros.Columns[2].Width = 100;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].HeaderText = "CLAVE";
            dgvRegistros.Columns[4].Width = 90;
            dgvRegistros.Columns[5].HeaderText = "CLIENTE";
            dgvRegistros.Columns[5].Width = 90;
            dgvRegistros.Columns[6].Visible = false;
            dgvRegistros.Columns[7].HeaderText = "LOTE";
            dgvRegistros.Columns[7].Width = 250;
            dgvRegistros.Columns[8].Visible = false;
            dgvRegistros.Columns[9].HeaderText = "ZONA";
            dgvRegistros.Columns[9].Width = 90;
            dgvRegistros.Columns[10].Visible = false;
            dgvRegistros.Columns[11].HeaderText = "ESTADO";
            dgvRegistros.Columns[11].Width = 100;
            dgvRegistros.Columns[12].Visible = false;
            dgvRegistros.Columns[13].Visible = false;
            dgvRegistros.Columns[14].Visible = false;
            dgvRegistros.Columns[15].Visible = false;
            dgvRegistros.Columns[16].HeaderText = "DÍA PAGO";
            dgvRegistros.Columns[16].Width = 100;
            dgvRegistros.Columns[17].Visible = false;
            dgvRegistros.Columns[18].Visible = false;
            dgvRegistros.Columns[19].Visible = false;
            dgvRegistros.Columns[20].Visible = false;
            dgvRegistros.Columns[21].Visible = false;
            dgvRegistros.Columns[22].Visible = false;
            dgvRegistros.Columns[23].Visible = false;
            dgvRegistros.Columns[24].Visible = false;
            dgvRegistros.Columns[25].Visible = false;
            dgvRegistros.Columns[26].Visible = false;

            tsTotalRegistros.Text = contexto.LstContratosAux.Count.ToString("N0");

        }

        private void busContratos_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void busContratos_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            if (rowIndexSeleccionado == -1) return;
            ObjEntidad = contexto.ObtenerRegistroEnListaAux(rowIndexSeleccionado);
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
                ordenar(contexto.Column);
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
            ObjEntidad = contexto.ObtenerRegistroEnListaAux(rowIndexSeleccionado);
            Close();
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
    }
}
