using CAPADATOS.ADO.SISTEMA;
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
    public partial class formLogin : Form
    {
        private bool ocultarPass = true;
        public formLogin()
        {
            InitializeComponent();            
        }

        private bool Authenticate(string Alias, string Password)
        {
            using (var contexto = new UsuariosADO())
            {
                try
                {
                    Global.CredencialesSesionAcceso(contexto.Authenticate(Alias, Password));
                    return Global.ObjUsuario != null;
                }
                catch(Exception ex)
                {
                    return false;
                }
            
            }
         
        }

        private void formLogin_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;        }

        private void formLogin_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this, false);
            this.MaximizeBox = false;
            groupBox1.Font = new Font("Arial", 10f, FontStyle.Bold);
            groupBox1.BackColor = ThemeConfig.Basep;
            panelBack.BackColor = ThemeConfig.Principal;
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            if (Authenticate(txtUsuario.Text, txtPassword.Text))
            {                
                MDIMain mDIMain = new MDIMain();
                Hide();
                mDIMain.WindowState = FormWindowState.Maximized;
                mDIMain.ShowDialog();  
                Close();
            }
            else
            {
                MessageBox.Show("No se pudo acceder con las credenciales ingresadas. Verifique sus datos e intentelo nuevamente.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOcultarPass_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !ocultarPass;
            ocultarPass = !ocultarPass;
        }

        private void btnOcultarPass_Click_1(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !ocultarPass;
            ocultarPass = !ocultarPass;
        }
    }
}
