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

namespace PRESENTACION.LOTES
{
    public partial class formZonas : Form
    {
        private formZonaLogica contexto;

        public formZonas()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            LimpiarControles();
            InstanciarContextos();
            ListarRegistros();
        }

        private void InstanciarContextos()
        {
            contexto = new formZonaLogica();
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
        }

        private void Apariencias()
        {
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[0].Frozen = true;
            dgvRegistros.Columns[1].HeaderText = "Nombre";
            dgvRegistros.Columns[1].Width = 110;
            dgvRegistros.Columns[1].Frozen = true;
            dgvRegistros.Columns[3].HeaderText = "No. Manzanas";
            dgvRegistros.Columns[3].Width = 75;
            dgvRegistros.Columns[4].HeaderText = "No. Lotes";
            dgvRegistros.Columns[4].Width = 75;
            dgvRegistros.Columns[5].HeaderText = "Dirección";
            dgvRegistros.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].Visible = false;
            dgvRegistros.Columns[2].Frozen = true;
            tsTotalRegistros.Text = contexto.LstZonaAux.Count.ToString("N0");

            contexto.Column = 1;

        }

        private void ListarRegistros()
        {
            dgvRegistros.DataSource = null;
            contexto.Listar();
            dgvRegistros.DataSource = contexto.LstZonaAux;
            Apariencias();
        }


        private void Guardar()
        {
            if (contexto.ObjZona == null)
            {
                contexto.InstanciarZona();
            }

            contexto.ObjZona.Nombre = txtNombre.Text;
            if (string.IsNullOrEmpty(txtManzanas.Text))
            {
                contexto.ObjZona.NoManzanas = null;
            }
            else
            {
                contexto.ObjZona.NoManzanas = int.Parse(txtManzanas.Text);
            }
            contexto.ObjZona.NoLotes = int.Parse(txtLotes.Text);
            contexto.ObjZona.FechaRegistro = DateTime.Now;
            contexto.ObjZona.Direccion = txtDomicilio.Text;

            contexto.Guardar();

            MessageBox.Show("Registro guardado correctamente.", "Aciso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InicializarForm();

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
            contexto.Ordenar(column);
            dgvRegistros.DataSource = contexto.LstZonaAux;
            Apariencias();
        }
        private void setData()
        {
            if (contexto.ObjZona != null)
            {
                txtNombre.Text = contexto.ObjZona.Nombre.ToString();
                txtManzanas.Text = (contexto.ObjZona.NoManzanas==null?null:contexto.ObjZona.NoManzanas.ToString()) ;
                txtLotes.Text = contexto.ObjZona.NoLotes.ToString();
                txtDomicilio.Text = contexto.ObjZona.Direccion;
            }
        }

        private void EliminarRegistro()
        {
            contexto.ObjZona = contexto.Obtener((int)dgvRegistros.CurrentRow.Cells[0].Value);
            contexto.Eliminar(contexto.ObjZona);
            contexto.Guardar();
            MessageBox.Show("Registro eliminado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (dgvRegistros.Rows.Count <= 0) return;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                contexto.index = -1;
                return;
            }

            filtrar(contexto.Column, txtNombre.Text);
        }

        private void formZonas_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void formZonas_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarForm();
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

            contexto.ObjZona = contexto.Obtener((int)dgvRegistros.CurrentRow.Cells[0].Value);

            setData();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;

            contexto.ObjZona = contexto.Obtener((int)dgvRegistros.CurrentRow.Cells[0].Value);

            setData();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;

            if (MessageBox.Show("Se borrará el registro. ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                EliminarRegistro();
                InicializarForm();
            }
        }
    }
}
