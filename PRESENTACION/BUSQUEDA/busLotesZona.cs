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
            try
            {
                contexto = new busLotesZonaLogica();
                Listar();
                ordenar(1);
                contexto.Column = 1;
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
            dgvRegistros.DataSource = contexto.LstLoteAux;
            Apariencias();
        }

        private void Apariencias()
        {
            if (dgvRegistros.DataSource == null) return;

            dgvRegistros.Columns[0].Visible = false; //id
            dgvRegistros.Columns[0].Frozen = true;

            dgvRegistros.Columns[1].HeaderText = "Identificador";
            dgvRegistros.Columns[1].Width = 110;
            dgvRegistros.Columns[1].Frozen = true;

            dgvRegistros.Columns[2].Frozen = true;//zonaid

            dgvRegistros.Columns[3].HeaderText = "Zona";//zona
            dgvRegistros.Columns[3].Width = 200;
            dgvRegistros.Columns[3].Frozen = true;

            dgvRegistros.Columns[4].HeaderText = "Manzana";
            dgvRegistros.Columns[4].DefaultCellStyle.Format = "N0";
            dgvRegistros.Columns[4].Width = 110;
            dgvRegistros.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvRegistros.Columns[5].HeaderText = "No. Lote";
            dgvRegistros.Columns[5].Width = 110;
            dgvRegistros.Columns[5].DefaultCellStyle.Format = "N0";
            dgvRegistros.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvRegistros.Columns[6].HeaderText = "Precio";
            dgvRegistros.Columns[6].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[6].Width = 110;
            dgvRegistros.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvRegistros.Columns[7].Visible = false;//estadoid

            dgvRegistros.Columns[8].HeaderText = "Estado";
            dgvRegistros.Columns[8].Width = 110;

            dgvRegistros.Columns[9].HeaderText = "Medida Norte";
            dgvRegistros.Columns[9].Width = 110;
            dgvRegistros.Columns[9].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvRegistros.Columns[10].HeaderText = "Medida Sur";
            dgvRegistros.Columns[10].Width = 110;
            dgvRegistros.Columns[10].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvRegistros.Columns[11].HeaderText = "Medida Este";
            dgvRegistros.Columns[11].Width = 110;
            dgvRegistros.Columns[11].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvRegistros.Columns[12].HeaderText = "Medida Oeste";
            dgvRegistros.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[12].Width = 110;
            dgvRegistros.Columns[12].DefaultCellStyle.Format = "N2";

            dgvRegistros.Columns[13].HeaderText = "Colinada Norte";
            dgvRegistros.Columns[13].Width = 110;

            dgvRegistros.Columns[14].HeaderText = "Colinada Sur";
            dgvRegistros.Columns[14].Width = 110;

            dgvRegistros.Columns[15].HeaderText = "Colinada Este";
            dgvRegistros.Columns[15].Width = 110;

            dgvRegistros.Columns[16].HeaderText = "Colinada Oeste";
            dgvRegistros.Columns[16].Width = 110;

            dgvRegistros.Columns[17].HeaderText = "Fecha Registro";
            dgvRegistros.Columns[17].Width = 135;

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
