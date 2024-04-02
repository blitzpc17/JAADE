using CAPADATOS.Entidades;
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
                contexto.ObjPersona.Apellidos = txtApellidos.Text;             

                contexto.ObjUsuario.Alias = txtUsuario.Text;
                contexto.ObjUsuario.Password = txtPassword.Text;
                contexto.ObjUsuario.ESTADOId = (int)cbxEstado.SelectedValue;
                contexto.ObjUsuario.ROLId = (int)cbxRol.SelectedValue;
                contexto.ObjUsuario.FechaRegistro = DateTime.Now;
                if (contexto.ObjUsuarioData != null)
                {
                    contexto.ObjUsuario.Id = contexto.ObjUsuarioData.Id;
                }
                

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

        private void formUsuarios_Load(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void formUsuarios_Shown(object sender, EventArgs e)
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var busUsuarios = new BUSQUEDA.busUsuarios();
            busUsuarios.ShowDialog();

            contexto.ObjUsuarioData = busUsuarios.ObjEntidad;

            if(contexto.ObjUsuarioData == null)
            {
                MessageBox.Show("No ha seleccionado ningún registro.", "Advertencia",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);

                return;
            }

            SetDataUsuario(contexto.ObjUsuarioData);


        }

        private void SetDataUsuario(clsUsuario objUsuarioData)
        {
            txtUsuario.Text = contexto.ObjUsuarioData.Alias;
            txtPassword.Text = contexto.ObjUsuarioData.Contrasena;
            txtFechaRegistro.Text = contexto.ObjUsuarioData.FechaRegistro.ToString("dd/MM/yyyy");
            txtNombre.Text = contexto.ObjUsuarioData.Nombres;
            txtApellidos.Text = contexto.ObjUsuarioData.Apellidos;
            cbxEstado.SelectedValue = contexto.ObjUsuarioData.EstadoId;
            cbxRol.SelectedValue = contexto.ObjUsuarioData.RolId;

            contexto.ObjPersona = contexto.ObtenerPersona(objUsuarioData.PersonaId);
        }
    }
}
