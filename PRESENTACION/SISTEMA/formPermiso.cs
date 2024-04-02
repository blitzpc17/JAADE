using CAPALOGICA.LOGICAS.SISTEMA;
using PRESENTACION.UTILERIAS;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PRESENTACION.SISTEMA
{
    public partial class formPermiso : Form
    {
        private formPermisoLogica contexto;

        public formPermiso()
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
                dgvRegistros.DataSource = null;
                tsTotalRegistros.Text = @"0";
                InstanciarContexto();
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

        public void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            txtFechaRegistro.Text = Global.FechaServidor().ToString("dd-MM-yyyy");
            txtUsuarioAsigno.Text = Global.ObjUsuario.Nombre;
        }

        public void InstanciarContexto()
        {
            contexto = new formPermisoLogica();
        }

        public void Guardar()
        {
            try
            {
                if (contexto.ObjUsuario == null)
                {
                    MessageBox.Show("No se ha seleccionado el usuario solicitante.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (contexto.ObjModulo == null)
                {
                    MessageBox.Show("No se ha seleccionado el módulo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (contexto.ValidarExistePermisoEnUsuario(contexto.ObjUsuario, contexto.ObjModulo.Id))
                {
                    MessageBox.Show(
                        "El usuario ya tiene asignado el permiso, verifique los permisos por rol o en el listado de permisos extra.", "Advertencia", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }

                contexto.InstanciarPermiso();

                contexto.ObjPermiso.Motivo = txtMotivo.Text;
                contexto.ObjPermiso.FechaRegistro = Global.FechaServidor();
                contexto.ObjPermiso.USUARIOId = contexto.ObjUsuario.Id;
                contexto.ObjPermiso.USUARIOAUTORIZOId = Global.ObjUsuario.Id;
                contexto.ObjPermiso.MODULOId = contexto.ObjModulo.Id;

                contexto.Guardar();

                MessageBox.Show("Permiso asignado correctamente, Vuelva a iniciar sesión para ver reflejado el módulo en el menú.",
                    "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            dgvRegistros.Columns[0].Frozen = true;
            dgvRegistros.Columns[1].Visible = false;
            dgvRegistros.Columns[1].Frozen = true;
            dgvRegistros.Columns[2].HeaderText = "MÓDULO";
            dgvRegistros.Columns[2].Width = 120;
            dgvRegistros.Columns[2].Frozen = true;
            dgvRegistros.Columns[3].HeaderText = "RUTA";
            dgvRegistros.Columns[3].Width = 250;
            dgvRegistros.Columns[4].Visible = false;
            dgvRegistros.Columns[5].HeaderText = "ASIGNO";
            dgvRegistros.Columns[5].Width = 110;
            dgvRegistros.Columns[6].HeaderText = "FECHA ASIGNACIÓN";
            dgvRegistros.Columns[6].Width = 100;
            dgvRegistros.Columns[7].Visible = false;
            dgvRegistros.Columns[8].Visible = false;
            dgvRegistros.Columns[9].HeaderText = "MOTIVO";
            dgvRegistros.Columns[9].Width = 100;

            tsTotalRegistros.Text = contexto.LstPermisosAux.Count.ToString("N0");

        }

        private void ListarRegistros(int UsuarioId)
        {
            dgvRegistros.DataSource = null;
            contexto.ListarPermisosUsuario(UsuarioId);
            dgvRegistros.DataSource = contexto.LstPermisosAux;
            Apariencias();
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
            dgvRegistros.DataSource = contexto.LstPermisosAux;
            Apariencias();
        }

        private void Modificar()
        {
            if (dgvRegistros.DataSource == null) return;
            if (dgvRegistros.CurrentRow.Cells[0].Value == null) return;
            int _UsuarioId = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            int _ModuloId = (int)dgvRegistros.CurrentRow.Cells[4].Value;
            contexto.ObjPermisoData = contexto.ObtenerDataPermiso(_UsuarioId,  _ModuloId);          

            if (contexto.ObjPermisoData != null)
            {
                contexto.ObjUsuario = contexto.ObtenerDataUsuario(_UsuarioId);
                contexto.ObjModulo = contexto.ObtenerDataModulo(_ModuloId);
                contexto.ObjPermiso = contexto.ObtenerPermiso(contexto.ObjPermisoData.PermisoId);

                txtMotivo.Text = contexto.ObjPermisoData.Motivo;
                txtModulo.Text = contexto.ObjModulo.Nombre;
                txtSolicita.Text = contexto.ObjUsuario.Nombres;
                txtUsuarioAsigno.Text = contexto.ObjPermisoData.NombreUsuarioAsigno;
                txtFechaRegistro.Text = contexto.ObjPermisoData.FechaAsigno.ToString("dd-MM-yyyy HH:mm:ss");

            }
        }




        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void formPermiso_Load(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void formPermiso_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
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

        private void btnModulo_Click(object sender, EventArgs e)
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

        private void btnUsuarioSolicita_Click(object sender, EventArgs e)
        {
            try
            {
                var busUsuarios = new BUSQUEDA.busUsuarios();
                busUsuarios.ShowDialog();
                contexto.ObjUsuario = busUsuarios.ObjEntidad;
                if (contexto.ObjUsuario == null)
                {
                    MessageBox.Show("No ha seleccionado el usuario solicitante.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ListarRegistros(contexto.ObjUsuario.Id);
                txtSolicita.Text = contexto.ObjUsuario.Nombre;
                contexto.Column = 2;
                ordenar(contexto.Column);
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
            if (dgvRegistros.DataSource != null) return;
            Modificar();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource != null) return;
            Modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRegistros.DataSource == null) return;
                if (dgvRegistros.CurrentRow == null) return;
                bool result = false;
                List<string> msjAlert = new List<string>();
                if (contexto.EliminarPermiso((int)dgvRegistros.CurrentRow.Cells[1].Value, contexto.ObjUsuario.Id))
                {
                    result = true;
                    msjAlert.AddRange(new string[]{ "Aviso", "Permiso revocado correctamente"});
                }
                else
                {
                    result = false;
                    msjAlert.AddRange(new string[] {"Advertencia", "Ocurrio un error al intentar revocar los permisos." });
                }

                MessageBox.Show(msjAlert[1],
                    msjAlert[0],
                    MessageBoxButtons.OK,
                    result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (result)
                {
                    ListarRegistros(contexto.ObjUsuario.Id);
                    contexto.ObjModulo = null;
                    txtModulo.Clear();  
                }
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar modificar los registros. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }
    }
}
