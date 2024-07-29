using CAPADATOS;
using CAPADATOS.ADO.SISTEMA;
using CAPADATOS.Entidades;
using PRESENTACION.UTILERIAS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION.SISTEMA
{
    public partial class MDIMain : Form
    {
        protected clsUsuario ObjCredencial;
        protected List<clsModulosAccesoUsuario> LstPermisos;
        private string PaginaActiva;

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
                InicializarTabUsuario();
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

        private void InicializarTabUsuario()
        {
            PaginaActiva = "";
            tabControl1.Width = 29;
        }

        private void SetCredenciales()
        {
            ObjCredencial = Global.ObjUsuario;
            txtSysNombre.Text =ObjCredencial.Nombre;
            txtSysRol.Text = ObjCredencial.Rol;
            txtSysUsuario.Text = ObjCredencial.Alias;
        }
        private void LimpiarMenu()
        {
            mainMenuStrip.Items.Clear();    
        }
        private void ObtenerModulos()
        {
            using (var contexto = new ModuloPermisoADO())
            {
                LstPermisos = contexto.ListarAccesoPermisoUsuario(ObjCredencial.Id, ObjCredencial.RolId);
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
                            var icono = Enumeraciones.ListaImagenes().Where(x => x.Key == child.Icono).First().Value;
                            itemChild.Image = icono;
                            string rutaFormulario = child.Ruta;
                            itemChild.Click += (sender, e) =>
                            {
                                Form formularioExistente = (Form)Activator.CreateInstance(Type.GetType(rutaFormulario));

                                // Mostrar el formulario existente
                               formularioExistente.MdiParent = this;
                               formularioExistente.Text = child.Nombre;
                                formularioExistente.Resize += FormularioHijo_Resize;
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

        private void FormularioHijo_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
               
                this.Height = 130;
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
        private void eXCEPCIONESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SISTEMA.formExcepciones();
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

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            tabControl1.Width = 250;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {            
            if (!PaginaActiva.Equals(tabControl1.SelectedTab.Text))
            {
                PaginaActiva = tabControl1.SelectedTab.Text;
                tabControl1_Selected(new object(),
                   new TabControlEventArgs(tabControl1.SelectedTab, tabControl1.SelectedIndex,
                       TabControlAction.Selected));
               
            }
            else
            {
                PaginaActiva = "";
                tabControl1.Width = 29;
            }
        }

        private void cERRARSESIÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(var childs in MdiChildren)
            {
                childs.Close();
            }
            formLogin login = new formLogin();
            Hide();           
            login.ShowDialog();
        }

        private void cALCULADORAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = new Process
            {
                StartInfo =
                {
                    FileName = @"calc.exe"
                }
            };
            p.Start();
        }

        private void sITIOWEBJAADEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://www.jaade.net";
            Process.Start(url);
        }

        private void cMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://www.ejemplo.com";
            Process.Start(url);
        }
    }
}
