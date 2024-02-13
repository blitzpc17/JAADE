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
    public partial class formPermisoRol : Form
    {
        private formPermisoRolLogica contexto;
        private bool cargado = false;
        public formPermisoRol()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        public void InicializarModulo()
        {
            try
            {
                cargado = false;
                LimpiarControles();
                dgvRegistros.DataSource = null;
                tsTotalRegistros.Text = @"0";
                InstanciarContexto();
                ListarRoles();
                contexto.RolId = -1;
                cargado = true;
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

        private void ListarRoles()
        {
            contexto.ListarRoles();
            cbxRol.DataSource = contexto.LstRol;
            cbxRol.DisplayMember = "Nombre";
            cbxRol.ValueMember = "Id";
            cbxRol.SelectedIndex = -1;
        }

        public void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            txtFechaRegistro.Text = Global.FechaServidor().ToString("dd-MM-yyyy");
            txtUsuarioAsigno.Text = Global.ObjUsuario.Nombre;
        }

        public void InstanciarContexto()
        {
            contexto = new formPermisoRolLogica();
        }

        public void Guardar()
        {
            try
            {
                if (contexto.RolId == -1)
                {
                    MessageBox.Show("No se ha seleccionado el rol.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (contexto.ObjModulo == null)
                {
                    MessageBox.Show("No se ha seleccionado el módulo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                contexto.ValidarPermisoEnUsuarios(contexto.ObjModulo.Id, contexto.RolId);
                if (contexto.LstUsuariosIdPermiso != null)
                {
                    contexto.InstanciarPermiso();
                    contexto.ObjPermiso.Motivo = txtMotivo.Text;
                    contexto.ObjPermiso.FechaRegistro = Global.FechaServidor();
                    contexto.ObjPermiso.USUARIOId = 0;
                    contexto.ObjPermiso.USUARIOAUTORIZOId = Global.ObjUsuario.Id;
                    contexto.ObjPermiso.MODULOId = contexto.ObjModulo.Id;

                    contexto.InsertarPermisoMasivoRol(contexto.ObjPermiso, contexto.LstUsuariosIdPermiso);                    

                    MessageBox.Show("Registro guardado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarRegistros();

                }
                else
                {
                    MessageBox.Show("No hay usuarios para agregar el permiso.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

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
            dgvRegistros.Columns[1].HeaderText = "MÓDULO";
            dgvRegistros.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].HeaderText = "RUTA";
            dgvRegistros.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[3].Visible = false;

            tsTotalRegistros.Text = contexto.LstPermisosAux.Count.ToString("N0");

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
            dgvRegistros.DataSource = contexto.LstPermisosAux;
            Apariencias();
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

        private void formPermisoRol_Load(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void formPermisoRol_Shown(object sender, EventArgs e)
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

        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuitarPermiso();
        }

        private void QuitarPermiso()
        {
            try
            {
                if (dgvRegistros.DataSource == null) return;

                if (contexto.RolId == -1)
                {
                    MessageBox.Show("No se ha seleccionado el rol.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int modulo= 0;
            
                if (dgvRegistros.CurrentRow==null)
                {                    
                    MessageBox.Show("Nu ha seleccionado el registro", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                modulo = (int)dgvRegistros.CurrentRow.Cells[0].Value;
                contexto.ValidarPermisoEnUsuarios(modulo, contexto.RolId, true);
                if (contexto.LstUsuariosIdPermiso.Count > 0)
                {
                    contexto.InstanciarPermiso();
                    contexto.ObjPermiso.MODULOId = modulo;
                    contexto.EliminarPermisoXRol(contexto.ObjPermiso, contexto.LstUsuariosIdPermiso);

                    MessageBox.Show("Se ha revocado el permiso de todos los usuarios con este ROL correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InicializarModulo();
                }
                else
                {
                    MessageBox.Show("Los usuarios con este ROL no tienen este permiso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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

        private void btnModulo_Click(object sender, EventArgs e)
        {
            try
            {
                var busModulos = new BUSQUEDA.busModulos();
                busModulos.ShowDialog();
                contexto.ObjModulo = busModulos.ObjEntidad;
                if (contexto.ObjModulo == null)
                {
                    MessageBox.Show("No ha seleccionado el módulo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                txtModulo.Text = contexto.ObjModulo.Nombre;
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar cargar los registros. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cbxRol_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!cargado) return;
            contexto.RolId = (int)cbxRol.SelectedValue;
            ListarRegistros();
        }

        private void ListarRegistros()
        {
            dgvRegistros.DataSource = null;
            contexto.ListarPermisosXRol();
            dgvRegistros.DataSource = contexto.LstPermisosAux;
            tsTotalRegistros.Text = contexto.LstPermisosAux.Count.ToString("N0");
            Apariencias();
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (contexto.Column == e.ColumnIndex) return;
            contexto.Column = e.ColumnIndex;
            ordenar(contexto.Column);
        }
    }
}
