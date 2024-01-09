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
    public partial class busPagos : Form
    {
        private busPagosLogica contexto;
        public clsPago ObjEntidad;
        private int? clienteId;
        private int rowIndexSeleccionado = -1;
        public busPagos(int? clienteId = null)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            this.clienteId = clienteId;
        }

        private void InicializarForm()
        {
            contexto = new busPagosLogica();
            Listar();
            ordenar(1);
            contexto.Column = 1;
        }

        public void Listar()
        {
            contexto.ListarRegistros(clienteId);
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
            dgvRegistros.DataSource = contexto.LstPagosAux;
            Apariencias();
        }

        private void Apariencias()
        {
            if (dgvRegistros.DataSource == null) return;
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[0].Frozen = true;
            dgvRegistros.Columns[1].HeaderText = "Número Referencia";
            dgvRegistros.Columns[1].Width = 110;
            dgvRegistros.Columns[1].Frozen = true;
            dgvRegistros.Columns[2].HeaderText = "Fecha Emisión";
            dgvRegistros.Columns[2].Width = 180;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].HeaderText = "Cliente";
            dgvRegistros.Columns[4].Width = 250;
            dgvRegistros.Columns[5].Visible = false;
            dgvRegistros.Columns[6].HeaderText = "Lote";
            dgvRegistros.Columns[6].Width = 110;
            dgvRegistros.Columns[7].Visible = false;
            dgvRegistros.Columns[8].HeaderText = "Zona";
            dgvRegistros.Columns[8].Width = 180;
            dgvRegistros.Columns[9].HeaderText = "Manzana";
            dgvRegistros.Columns[9].Width = 110;
            dgvRegistros.Columns[10].HeaderText = "Monto";
            dgvRegistros.Columns[10].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[10].Width = 110;
            dgvRegistros.Columns[11].Visible = false;
            dgvRegistros.Columns[12].HeaderText = "Recibio";
            dgvRegistros.Columns[12].Width = 250;
            
            tsTotalRegistros.Text = contexto.LstPagosAux.Count.ToString("N0");
        }

        private void busPagos_Load(object sender, EventArgs e)
        {
            InicializarForm();   
        }

        private void busPagos_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            if (rowIndexSeleccionado == -1) return;
            rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            ObjEntidad = contexto.ObtenerRegistro(rowIndexSeleccionado);
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (contexto.Column == e.ColumnIndex) return;
            contexto.Column = e.ColumnIndex;
            ordenar(contexto.Column);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            ObjEntidad = contexto.ObtenerRegistro(rowIndexSeleccionado);
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
