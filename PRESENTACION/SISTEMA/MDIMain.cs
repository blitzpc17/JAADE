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
        protected List<clsModulosAccesoUsuario> LstPermisos;
        
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
            tsNombreUsuario.Text = Global.ObtenerNombreUsuario(ObjCredencial);
            tsRol.Text = ObjCredencial.ROL.Nombre;
        }
        private void LimpiarMenu()
        {
            mainMenuStrip.Items.Clear();    
        }
        private void ObtenerModulos()
        {
            using (var contexto = new ModuloPermisoADO())
            {
                LstPermisos = contexto.ListarAccesoPermisoUsuario(ObjCredencial.Id);
            }
        }
        private void LLenarMenu()
        {
            if (LstPermisos == null)
            {
                MessageBox.Show("No tienes accesos al sistema, verifica con tu administrador e inicia sesión nuevamente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            var listaPadres = LstPermisos.Select(x => new { PadreId = x.ModuloPadreId, Nombre = x.ModuloPadreNombre }).Distinct();
            foreach (var padre in listaPadres)
            {
                ToolStripMenuItem itemPadre = new ToolStripMenuItem(padre.Nombre);
                var listaSubs = LstPermisos.Where(x => x.ModuloPadreId == padre.PadreId).Select(x => new { SubId = x.ModuloSubId, PadreId = x.ModuloPadreId, Nombre = x.ModuloSubNombre }).Distinct();
                foreach (var item in listaSubs)
                {
                    if(item.PadreId == padre.PadreId)
                    {
                        ToolStripMenuItem itemSub = new ToolStripMenuItem(item.Nombre);
                     
                        
                        var listaChilds = LstPermisos.Where(x=>x.ModuloSubId==item.SubId).Select(x=>new {Id= x.ModuloId, Nombre = x.Nombre, Icono = x.Icono, Ruta = x.Ruta}).Distinct();
                        foreach(var child in listaChilds)
                        {
                            ToolStripMenuItem itemChild = new ToolStripMenuItem(child.Nombre);
                            itemChild.Image = Enumeraciones.ListaImagenes().Where(x => x.Key == child.Icono).First().Value;
                            string rutaFormulario = child.Ruta;
                            itemChild.Click += (sender, e) =>
                            {
                                Form formularioExistente = (Form)Activator.CreateInstance(Type.GetType(rutaFormulario));

                               // Mostrar el formulario existente
                               formularioExistente.Show();
                            };
                            itemSub.DropDownItems.Add(itemChild);
                            
                        }

                        itemPadre.DropDownItems.Add(itemSub);
                    }
                
                }

                mainMenuStrip.Items.Add(itemPadre); 
             
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
