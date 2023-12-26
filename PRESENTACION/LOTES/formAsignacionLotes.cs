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

namespace PRESENTACION.LOTES
{
    public partial class formAsignacionLotes : Form
    {
        public formAsignacionLotes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;

        }

        private void formAsignacionLotes_Load(object sender, EventArgs e)
        {

        }

        private void formAsignacionLotes_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }
    }
}
