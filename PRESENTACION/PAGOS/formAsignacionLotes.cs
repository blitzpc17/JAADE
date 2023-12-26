using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION.PAGOS
{
    public partial class formAsignacionLotes : Form
    {
        public formAsignacionLotes()
        {
            InitializeComponent();
        }

        private void btnModulo_Click(object sender, EventArgs e)
        {
            var busquedaCliente = new BUSQUEDA.busClientes();
            busquedaCliente.ShowDialog();
        }
    }
}
