using CAPALOGICA.LOGICAS.SISTEMA;
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
    public partial class formExcepciones : Form
    {
        private formExcepcionesLogica contexto;
        public formExcepciones()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {    
            try
            {
                InstanciarContextos();
                LimpiarControles();
                ListarRegistros();
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al consultar los registros. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
        }

        private void InstanciarContextos()
        {
            contexto = new formExcepcionesLogica();
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            dtpFechaRegistro.Value = Global.FechaServidor();
            contexto.FechaRegistro = dtpFechaRegistro.Value;
            dtpFechaRegistro.Value = contexto.FechaRegistro;
            contexto.Column = 1;
        }

        private void Apariencias()
        {
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].HeaderText = "FECHA";
            dgvRegistros.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].HeaderText = "FORMULARIO";
            dgvRegistros.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].HeaderText = "DETALLE";
            dgvRegistros.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[5].Visible = false;
            dgvRegistros.Columns[6].HeaderText = "NOMBRE USUARIO";
            dgvRegistros.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            tsTotalRegistros.Text = contexto.LstExcepcionesAux.Count.ToString("N0");

        }

        private void ListarRegistros()
        {
            dgvRegistros.DataSource = null;
            contexto.LstExcepciones = contexto.ListarExcepciones(contexto.FechaRegistro);
            contexto.LstExcepcionesAux = contexto.LstExcepciones;
            dgvRegistros.DataSource = contexto.LstExcepcionesAux;
            Apariencias();
        }


        private void filtrar(int column, string termino)
        {
            if (column != contexto.Index)
            {
                ordenar(column);
            }

            if (contexto.Filtrar(column, termino))
            {
                contexto.IndexAux = contexto.Index;
                dgvRegistros.Rows[contexto.Index].Cells[column].Selected = true;
                dgvRegistros.FirstDisplayedScrollingRowIndex = contexto.Index;
            }
        }

        private void ordenar(int column)
        {
            contexto.Ordenar(column);
            dgvRegistros.DataSource = contexto.LstExcepcionesAux;
            Apariencias();
        }
        private void setData(int id)
        {
            contexto.ObjExcepcion = contexto.ObtenerDataExcepcion(id);
        }


        private void formExcepciones_Load(object sender, EventArgs e)
        {
            InicializarForm();            
        }

        private void formExcepciones_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            MaximizeBox = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ListarRegistros();
        }

        private void dtpFechaRegistro_ValueChanged(object sender, EventArgs e)
        {
            txtDetalle.Clear();
            contexto.FechaRegistro = dtpFechaRegistro.Value;
            ListarRegistros();
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            if (contexto.Column != e.ColumnIndex)
            {
                contexto.Column = e.ColumnIndex;
                txtBuscar.Clear();
            }
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            txtDetalle.Clear();
            setData((int)dgvRegistros.CurrentRow.Cells[0].Value);
            txtDetalle.Text += contexto.ObjExcepcion.Resumen + "\r\n\r\n\r\n";
            txtDetalle.Text += contexto.ObjExcepcion.Detalle;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvRegistros.Rows.Count <= 0) return;

            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                contexto.Index = -1;
                return;
            }

            filtrar(contexto.Column, txtBuscar.Text);
        }
    }
}
