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
    public partial class busLotesZona : Form
    {
        private busLotesZonaLogica contexto;
        public clsLotes ObjEntidad;
        private int rowIndexSeleccionado = -1;
        private int zonaId = -1, clienteId;
        private bool buscarPorCliente = false;

        public busLotesZona(int id, bool buscarPorCliente= false)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            this.buscarPorCliente = buscarPorCliente;
            if (this.buscarPorCliente)
            {
                clienteId = id;
            }
            else
            {
                zonaId = id;
            }
            
        }  
        private void InicializarForm()
        {
            contexto = new busLotesZonaLogica();
            Listar();
            ordenar(1);
            contexto.Column = 1;
        }

        public void Listar()
        {
            if (buscarPorCliente)
            {
                contexto.ClienteId = clienteId;
            }
            else
            {
                contexto.ZonaId = zonaId;
            }

            contexto.ListarRegistros(buscarPorCliente);
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
            dgvRegistros.DataSource = contexto.LstLoteAux;
            Apariencias();
        }

        private void Apariencias()
        {
            if (dgvRegistros.DataSource == null) return;
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[0].Frozen = true;
            dgvRegistros.Columns[1].HeaderText = "Identificador";
            dgvRegistros.Columns[1].Width = 110;
            dgvRegistros.Columns[1].Frozen = true;
            dgvRegistros.Columns[2].HeaderText = "Zona";
            dgvRegistros.Columns[2].Width = 200;
            dgvRegistros.Columns[2].Frozen = true;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].HeaderText = "Medida Norte";
            dgvRegistros.Columns[4].Width = 110;
            dgvRegistros.Columns[4].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[5].HeaderText = "Medida Sur";
            dgvRegistros.Columns[5].Width = 110;
            dgvRegistros.Columns[5].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[6].HeaderText = "Medida Este";
            dgvRegistros.Columns[6].Width = 110;
            dgvRegistros.Columns[6].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[7].HeaderText = "Medida Oeste";
            dgvRegistros.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[7].Width = 110;
            dgvRegistros.Columns[7].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[8].HeaderText = "Colinada Norte";
            dgvRegistros.Columns[8].Width = 110;
            dgvRegistros.Columns[9].HeaderText = "Colinada Sur";
            dgvRegistros.Columns[9].Width = 110;
            dgvRegistros.Columns[10].HeaderText = "Colinada Este";
            dgvRegistros.Columns[10].Width = 110;
            dgvRegistros.Columns[11].HeaderText = "Colinada Oeste";
            dgvRegistros.Columns[11].Width = 110;
            dgvRegistros.Columns[12].HeaderText = "Fecha Registro";
            dgvRegistros.Columns[12].Width = 135;
            dgvRegistros.Columns[13].HeaderText = "Precio";
            dgvRegistros.Columns[13].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[13].Width = 110;
            dgvRegistros.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tsTotalRegistros.Text = contexto.LstLoteAux.Count.ToString("N0");
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

        private void busLotesZona_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void busLotesZona_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

     
    }
}
