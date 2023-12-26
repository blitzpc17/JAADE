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

namespace PRESENTACION.PAGOS
{
    public partial class formAgendaClientes : Form
    {
        private formAgendaClienteLogica contexto;
        private int rowIndexSeleccionado = -1;
        private int? personaId=null;
        public formAgendaClientes(int personaId)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;  
            this.personaId = personaId;
        }

        private void InicializarForm()
        {
            if (this.personaId == null) Close();
            contexto = new formAgendaClienteLogica();
            contexto.personaId = this.personaId;
            ListarDatosContacto();
            ListarRegistros();
            
        }

        private void ListarDatosContacto()
        {
            contexto.ListarCatalogos();
            cbxTipoContacto.DataSource = contexto.LstTipos;
            cbxTipoContacto.DisplayMember = "key";
            cbxTipoContacto.ValueMember = "value";  
        }

        private void ListarRegistros()
        {
            contexto.LstDatosContacto = contexto.ListarDataContactoCliente((int)contexto.personaId);
            contexto.LstDatosContactoAux = contexto.LstDatosContacto;
            dgvRegistros.DataSource = contexto.LstDatosContactoAux;          
            Apariencias();
        }

        private void Apariencias()
        {
            if (dgvRegistros.DataSource == null) return;
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].HeaderText = "TIPO";
            dgvRegistros.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].Visible = false;
            dgvRegistros.Columns[3].HeaderText = "CONTACTO";
            dgvRegistros.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[4].Visible = false;
            tsTotalRegistros.Text = contexto.LstDatosContactoAux.Count().ToString("N0");
        }


        private void GuardarRegistroContacto()
        {
            if (contexto.ObjDatoContactoData == null)
            {
                contexto.InstanciarDatoContacto();
            }

            contexto.ObjDatoContacto.Tipo = (int)cbxTipoContacto.SelectedValue;
            contexto.ObjDatoContacto.Valor = txtDato.Text; 

            contexto.Guardar();

            MessageBox.Show("Registro guardado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InicializarForm();
        }

        private void Modificar()
        {
            if (dgvRegistros.DataSource == null) return;
            if (dgvRegistros.CurrentRow.Cells[0].Value == null) return;
            rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            contexto.ObjDatoContactoData = contexto.ObtenerDataContactoCliente(rowIndexSeleccionado);
            contexto.ObjDatoContacto = contexto.ObtenerAgenda(contexto.ObjDatoContactoData.Id);
          
            if (contexto.ObjDatoContacto != null)
            {
                cbxTipoContacto.SelectedValue = contexto.ObjDatoContacto.Tipo;
                txtDato.Text = contexto.ObjDatoContacto.Valor;
            }
        }

        private void ordenar(int column)
        {
            txtBuscar.Clear();
            txtBuscar.Focus();
            contexto.Ordenar(column);
            dgvRegistros.DataSource = contexto.LstDatosContactoAux;
            Apariencias();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarRegistroContacto();  
        }

        private void formAgendaClientes_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void formAgendaClientes_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (contexto.Column == e.ColumnIndex) return;
            contexto.Column = e.ColumnIndex;
            ordenar(contexto.Column);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Modificar();
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
