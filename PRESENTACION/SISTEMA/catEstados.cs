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
    public partial class catEstados : Form
    {
        private catEstadosLogica contexto;

        public catEstados()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            try
            {
                LimpiarControles();
                InstanciarContextos();
                ListarRegistros();
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar cargar el modulo. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
         
        }

        private void InstanciarContextos()
        {
            contexto = new catEstadosLogica();
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
        }

        private void Apariencias()
        {
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].HeaderText = "Nombre";
            dgvRegistros.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].HeaderText = "Proceso";
            dgvRegistros.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            tsTotalRegistros.Text = contexto.LstEstado.Count.ToString("N0");

        }

        private void ListarRegistros()
        {
            dgvRegistros.DataSource = null;
            contexto.Listar();
            dgvRegistros.DataSource = contexto.LstEstado;
            Apariencias();
        }

        private void Guardar()
        {
            try
            {
                if (contexto.ObjEstado == null)
                {
                    contexto.InstanciarEstado();
                    contexto.ObjEstado.Baja = false;
                }

                contexto.ObjEstado.Nombre = txtNombre.Text;
                contexto.ObjEstado.Proceso = txtProceso.Text;

                contexto.Guardar();

                MessageBox.Show("Registro guardado correctamente.", "Aciso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InicializarForm();
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar guardar el registro. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
       

        }


        private void EliminarRegistro()
        {
            contexto.ObjEstado = contexto.Obtener((int)dgvRegistros.CurrentRow.Cells[0].Value);
            contexto.ObjEstado.Baja = !contexto.ObjEstado.Baja;
            contexto.Guardar();
            MessageBox.Show("Registro "+(contexto.ObjEstado.Baja?"DESACTIVADO":"ACTIVADO")+" correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InicializarForm();
        }

        private void setData()
        {
            if (contexto.ObjEstado != null)
            {
                txtNombre.Text = contexto.ObjEstado.Nombre;
                txtProceso.Text = contexto.ObjEstado.Proceso;
            }
        }

        private void filtrar(int column, string termino)
        {
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
            dgvRegistros.DataSource = contexto.LstEstado;
            Apariencias();
        }


        private void catEstados_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void catEstados_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (dgvRegistros.Rows.Count <= 0) return;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                contexto.Index = -1;
                return;
            }

            filtrar(contexto.Column, txtNombre.Text);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;

            contexto.ObjEstado = contexto.Obtener((int)dgvRegistros.CurrentRow.Cells[0].Value);

            setData();
        }

        private void activarDesactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (contexto.Column == e.ColumnIndex) return;
            contexto.Column = e.ColumnIndex;
            ordenar(contexto.Column);
        }
    }
}
