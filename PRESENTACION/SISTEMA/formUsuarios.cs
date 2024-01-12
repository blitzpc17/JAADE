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
    public partial class formUsuarios : Form
    {
        private formUsuariosLogica contexto;
        private int rowIndexSeleccionado = -1;

        public formUsuarios()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

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
            txtFechaRegistro.Text = Global.FechaServidor().ToString("dd-MM-yyyy HH:mm:ss");
        }

        public void ListarCatalogos()
        {
            contexto.ListarCatalogos();
            cbxEstado.DataSource = contexto.LstEstado;
            cbxEstado.DisplayMember = "Nombre";
            cbxEstado.ValueMember = "Id";
            cbxEstado.SelectedIndex = -1;
            cbxRol.DataSource = contexto.LstRol;
            cbxRol.DisplayMember = "Nombre";
            cbxRol.ValueMember = "Id";
            cbxRol.SelectedIndex = -1;

        }

        public void InstanciarContexto()
        {
            contexto = new formUsuariosLogica();
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

                if (cbxRol.SelectedIndex == -1)
                {
                    MessageBox.Show("El campo ROLES es OBLIGATORIO.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (contexto.ObjUsuario == null)
                {
                    contexto.InstanciarUsuario();
                }

                if (contexto.ObjUsuarioData == null)
                {
                    contexto.InstanciarPersona();
                    contexto.InstanciarUsuario();
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
                contexto.ObjPersona.Municipio = txtMunicipio.Text;  
                contexto.ObjPersona.CodigoPostal = txtCodigoPostal.Text;

                contexto.ObjUsuario.Alias = txtUsuario.Text;
                contexto.ObjUsuario.Password = txtPassword.Text;
                contexto.ObjUsuario.ESTADOId = (int)cbxEstado.SelectedValue;
                contexto.ObjUsuario.ROLId = (int)cbxRol.SelectedValue;
                contexto.ObjUsuario.FechaRegistro = DateTime.Now;

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
            dgvRegistros.Columns[1].HeaderText = "ALIAS";
            dgvRegistros.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].Visible = false;
            dgvRegistros.Columns[3].HeaderText = "NOMBRE";
            dgvRegistros.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[4].Visible = false;
            dgvRegistros.Columns[5].Visible = false;
            dgvRegistros.Columns[6].Visible = false;
            dgvRegistros.Columns[7].Visible = false;
            dgvRegistros.Columns[8].Visible = false;
            dgvRegistros.Columns[9].Visible = false;
            dgvRegistros.Columns[10].Visible = false;
            dgvRegistros.Columns[11].Visible = false;
            dgvRegistros.Columns[12].Visible = false;
            dgvRegistros.Columns[13].Visible = false;
            dgvRegistros.Columns[14].Visible = false;
            dgvRegistros.Columns[15].Visible = false;
            dgvRegistros.Columns[16].HeaderText = "ROL";
            dgvRegistros.Columns[16].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[17].Visible = false;
            dgvRegistros.Columns[18].HeaderText = "ESTADO";
            dgvRegistros.Columns[18].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[19].Visible = false;


            tsTotalRegistros.Text = contexto.LstUsuario.Count.ToString("N0");

        }

        private void ListarRegistros()
        {
            dgvRegistros.DataSource = null;
            contexto.ListarUsuarios();
            dgvRegistros.DataSource = contexto.LstUsuarioAux;
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
            dgvRegistros.DataSource = contexto.LstUsuarioAux;
            Apariencias();
        }

        private void Modificar()
        {
            if (dgvRegistros.DataSource == null) return;
            if (dgvRegistros.CurrentRow.Cells[0].Value == null) return;
            rowIndexSeleccionado = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            contexto.ObjUsuarioData = contexto.ObtenerDataUsuario(rowIndexSeleccionado);
            contexto.ObjUsuario = contexto.ObtenerUsuario(contexto.ObjUsuarioData.Id);
            contexto.ObjPersona = contexto.ObtenerPersona(contexto.ObjUsuario.PERSONAId);
            if (contexto.ObjUsuario != null)
            {
                txtNombre.Text = contexto.ObjUsuarioData.Nombres;
                txtAmaterno.Text = contexto.ObjUsuarioData.Amaterno;
                txtApaterno.Text = contexto.ObjUsuarioData.Apaterno;
                dtpFechaNacimiento.Value = contexto.ObjUsuarioData.FechaNacimiento;
                txtCurp.Text = contexto.ObjUsuarioData.Curp;
                txtCalle.Text = contexto.ObjUsuarioData.Calle;
                txtNoExt.Text = contexto.ObjUsuarioData.NoExt;
                txtNoInt.Text = contexto.ObjUsuarioData.NoInt;
                txtColonia.Text = contexto.ObjUsuarioData.Colonia;
                txtLocalidad.Text = contexto.ObjUsuarioData.Localidad;
                txtCodigoPostal.Text = contexto.ObjUsuarioData.CodigoPostal;

                txtUsuario.Text = contexto.ObjUsuarioData.Alias;
                txtPassword.Text = contexto.ObjUsuarioData.Contrasena;
                cbxEstado.SelectedValue = contexto.ObjUsuarioData.EstadoId;
                cbxRol.SelectedValue = contexto.ObjUsuarioData.RolId;
            }
        }


        private void formUsuarios_Load(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void formUsuarios_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }
    }
}
