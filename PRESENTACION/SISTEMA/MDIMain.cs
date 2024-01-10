using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
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
    public partial class MDIMain : Form
    {
        protected USUARIO ObjCredencial;
        protected List<clsModuloPermiso> LstPermisos;
        
        private int childFormNumber = 0;
        private bool darktheme = false;

        public MDIMain()
        {
            InitializeComponent();
        }

        private void Inicializar()
        {
            try
            {
                SetCredenciales();
                ObtenerModulos();
                LimpiarMenu();
                LLenarMenu();
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al realizar la operación de inicio de sesión. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
          
        }
        private void SetCredenciales()
        {
            ObjCredencial = Global.ObjUsuario;
        }
        private void LimpiarMenu()
        {
            mainMenuStrip.Items.Clear();    
        }
        private void ObtenerModulos()
        {
            using (var contexto = new ModuloPermisoADO())
            {
                LstPermisos = contexto.ListarModuloPermisoUsuario(ObjCredencial.Id);
            }
        }
        private void LLenarMenu()
        {
            if (LstPermisos == null)
            {
                MessageBox.Show("No tienes accesos al sistema, verifica con tu administrador e inicia sesión nuevamente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }

            foreach(var padre in LstPermisos.Where(x=>x.RutaModulo.Equals("#")).Select(x => new {PadreId = x.ModuloId, Nombre = x.NombreModulo}).Distinct())
            {
                ToolStripMenuItem itemPadre = new ToolStripMenuItem(padre.Nombre);
                foreach (var item in LstPermisos.Where(x=>x.RutaModulo!="#"))
                {
                    if (string.IsNullOrEmpty(item.RutaModulo))
                    {
                        //sub
                    }
                    else
                    {
                        //item
                        
                    }
                }
                MainMenuStrip.Items.Add(itemPadre);
            }


        }

        private void MDIMain_Load(object sender, EventArgs e)
        {
            Inicializar();  
        }

        private void MDIMain_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this, false);
            WindowState = FormWindowState.Maximized;
            
        }

        private void asignacionLotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new PAGOS.formAsignacionLotes();
            form.MdiParent = this;
            form.Show();
        }

        private void capturaYConsultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new PAGOS.formClientes();
            form.MdiParent = this;
            form.Show();
        }

        private void eXCEPCIONESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SISTEMA.formExcepciones();
            form.MdiParent = this;
            form.Show();
        }

        private void capturaYConsultaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new PAGOS.formPagos();
            form.MdiParent = this;
            form.Show();
        }

        private void mODULOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SISTEMA.formModulos();
            form.MdiParent = this;
            form.Show();
        }

        private void mODULOSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new SISTEMA.formPermiso();
            form.MdiParent = this;
            form.Show();
        }

        private void cAPTURAYCONSULTAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SISTEMA.formUsuarios();
            form.MdiParent= this;
            form.Show();
        }
    }
}
