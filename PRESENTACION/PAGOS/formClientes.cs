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
    public partial class formClientes : Form
    {
        public formClientes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private formClientesLogica contexto;
        private int rowIndexSeleccionado = -1;

        public void InicializarModulo()
        {
            try
            {
                LimpiarControles();
                InstanciarContexto();
                ListarCatalogos();
                ListarRegistros();
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

        public void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
        }

        public void ListarCatalogos()
        {
            contexto.ListarCatalogos();
            cbxEstado.DataSource = contexto.LstEstado;
            cbxEstado.DisplayMember = "Nombre";
            cbxEstado.ValueMember = "Id";
            cbxEstado.SelectedIndex = -1;

        }

        public void InstanciarContexto()
        {
            contexto = new formClientesLogica();
        }

        public void Guardar()
        {
            try
            {
                if (cbxEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("El campo ESTADO es OBLIGATORIO.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (contexto.ObjClienteData == null)
                {
                    contexto.InstanciarPersona();
                    contexto.InstanciarCliente();
                    contexto.ObjCliente.Clave = Global.ObtenerFolio("CLIENTE");
                }
                contexto.ObjPersona.Nombres = txtNombre.Text;
                contexto.ObjPersona.ApellidoMaterno = txtAmaterno.Text;
                contexto.ObjPersona.ApellidoPaterno = txtApaterno.Text;
                contexto.ObjPersona.FechaNacimiento = dtpFechaNacimiento.Value;
                contexto.ObjPersona.Curp = txtCurp.Text;
                contexto.ObjPersona.Calle = txtCalle.Text;
                contexto.ObjPersona.NoExt = txtNoExt.Text;
                contexto.ObjPersona.NoInt = txtNoInt.Text;
                contexto.ObjPersona.Colonia = txtColonia.Text;
                contexto.ObjPersona.Localidad = txtLocalidad.Text;
                contexto.ObjPersona.CodigoPostal = txtCodigoPostal.Text;
                contexto.ObjPersona.EntidadFederativa = txtEntidadFederativa.Text;
                contexto.ObjPersona.Municipio = txtMunicipio.Text;


                contexto.ObjCliente.ESTADOId = (int)cbxEstado.SelectedValue;

                contexto.Guardar();


                MessageBox.Show("Registro guardado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InicializarModulo();
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar guardar el registro. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }



        }

        private void Apariencias()
        {
            if (dgvRegistros.DataSource == null) return;
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].HeaderText = "CLAVE";
            dgvRegistros.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;          
            dgvRegistros.Columns[2].HeaderText = "NOMBRE";
            dgvRegistros.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].Visible = false;
            dgvRegistros.Columns[5].Visible = false;
            dgvRegistros.Columns[6].Visible = false;
            dgvRegistros.Columns[7].Visible = false;
            dgvRegistros.Columns[8].Visible = false;
            dgvRegistros.Columns[9].Visible = false;
            dgvRegistros.Columns[10].Visible = false;
            dgvRegistros.Columns[11].HeaderText = "COLONIA";
            dgvRegistros.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[12].HeaderText = "LOCALIDAD";
            dgvRegistros.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[13].HeaderText = "MUNICIPIO";
            dgvRegistros.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[14].HeaderText = "ENTIDAD FED.";
            dgvRegistros.Columns[14].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[15].Visible = false;
            dgvRegistros.Columns[16].Visible = false;
            dgvRegistros.Columns[17].HeaderText = "ESTADO";
            dgvRegistros.Columns[17].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            tsTotalRegistros.Text = contexto.LstClientes.Count.ToString("N0");

        }

        private void ListarRegistros()
        {
            dgvRegistros.DataSource = null;
            contexto.ListarUsuarios();
            dgvRegistros.DataSource = contexto.LstClientesAux;
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

        private void ordenar(int column)
        {
            txtBuscar.Clear();
            txtBuscar.Focus();
            contexto.Ordenar(column);
            dgvRegistros.DataSource = contexto.LstClientesAux;
            Apariencias();
        }

        private void Modificar()
        {
            if (dgvRegistros.DataSource == null) return;
            if (dgvRegistros.CurrentRow.Cells[0].Value == null) return;
            rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            contexto.ObjClienteData = contexto.ObtenerDataCliente(rowIndexSeleccionado);
            contexto.ObjCliente = contexto.ObtenerCliente(contexto.ObjClienteData.Id);
            contexto.ObjPersona = contexto.ObtenerPersona(contexto.ObjCliente.PERSONAId);
            if (contexto.ObjCliente != null)
            {
                txtNombre.Text = contexto.ObjClienteData.Nombres;
                txtAmaterno.Text = contexto.ObjClienteData.Amaterno;
                txtApaterno.Text = contexto.ObjClienteData.Apaterno;
                dtpFechaNacimiento.Value = contexto.ObjClienteData.FechaNacimiento;
                txtCurp.Text = contexto.ObjClienteData.Curp;
                txtCalle.Text = contexto.ObjClienteData.Calle;
                txtNoExt.Text = contexto.ObjClienteData.NoExt;
                txtNoInt.Text = contexto.ObjClienteData.NoInt;
                txtColonia.Text = contexto.ObjClienteData.Colonia;
                txtLocalidad.Text = contexto.ObjClienteData.Localidad;
                txtCodigoPostal.Text = contexto.ObjClienteData.CodigoPostal;
                txtMunicipio.Text = contexto.ObjClienteData.Municipio;
                txtEntidadFederativa.Text = contexto.ObjClienteData.EntidadFederativa;
                txtClave.Text = contexto.ObjClienteData.Clave;
                cbxEstado.SelectedValue = contexto.ObjClienteData.EstadoId;
            }
        }





        private void formClientes_Load(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void formClientes_Shown(object sender, EventArgs e)
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
            InicializarModulo();
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

        private void btnAgenda_Click(object sender, EventArgs e)
        {
            if (contexto.ObjClienteData == null)
            {
                MessageBox.Show("Debe seleccionar un cliente para poder abrir el directorio de contacto.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var formAgenda = new formAgendaClientes(contexto.ObjCliente.PERSONAId);
            formAgenda.ShowDialog();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }
    }
}
