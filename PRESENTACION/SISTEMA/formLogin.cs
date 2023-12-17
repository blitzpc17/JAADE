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
        public formLogin()
        {
            InitializeComponent();            
        }

        private void formLogin_Load(object sender, EventArgs e)
        {

        }

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
            MDIMain mDIMain = new MDIMain();
            Hide();
            mDIMain.WindowState = FormWindowState.Maximized;
            mDIMain.ShowDialog();
            Close();
        }
    }
}
