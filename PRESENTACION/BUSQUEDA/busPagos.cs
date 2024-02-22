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
        public clsBusquedaPago ObjEntidad;
        private string contrato;
        public busPagos(string contrato = null)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            this.contrato = contrato;
        }

        private void InicializarForm()
        {
            try
            {
                contexto = new busPagosLogica();
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
            contexto.ListarRegistros(contrato);
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
            dgvRegistros.Columns[1].Width = 90;
            dgvRegistros.Columns[1].Frozen = true;
            dgvRegistros.Columns[2].HeaderText = "Fecha Emisión";
            dgvRegistros.Columns[2].Width = 120;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].HeaderText = "Contrato";
            dgvRegistros.Columns[4].Width = 90;
            dgvRegistros.Columns[5].HeaderText = "Cliente";
            dgvRegistros.Columns[5].Width = 210;
            dgvRegistros.Columns[6].HeaderText = "Zona";
            dgvRegistros.Columns[6].Width = 110;
            dgvRegistros.Columns[7].HeaderText = "Lote";           
            dgvRegistros.Columns[7].Width = 90;
            dgvRegistros.Columns[8].HeaderText = "Monto";
            dgvRegistros.Columns[8].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[7].Width = 100;
            

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
            ObjEntidad = contexto.ObtenerRegistro(dgvRegistros.CurrentRow.Cells[1].Value.ToString());
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
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

        private void dgvRegistros_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            ObjEntidad = contexto.ObtenerRegistro(dgvRegistros.CurrentRow.Cells[1].Value.ToString());
            Close();
        }

        private void dgvRegistros_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (contexto.Column == e.ColumnIndex) return;
            contexto.Column = e.ColumnIndex;
            ordenar(contexto.Column);
        }
    }
}
