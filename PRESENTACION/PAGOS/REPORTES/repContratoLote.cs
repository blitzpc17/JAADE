using Microsoft.Reporting.WinForms;
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

namespace PRESENTACION.PAGOS.REPORTES
{
    public partial class repContratoLote : Form
    {
        public List<ReportParameter> parametros;
        
        public repContratoLote()
        {
            InitializeComponent();
        }

        public bool InicializarReporteContrato()
        {
            if (parametros == null) return false;

            SetParametros();

            return true;
        }
        public void InstanciarListaParametros()
        {
            parametros = new List<ReportParameter>();
        }


        private void SetParametros()
        {
            reportViewer1.LocalReport.SetParameters(parametros);

            reportViewer1.RefreshReport();
        }

        private void repContratoLote_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;

            InicializarReporteContrato();
            
        }

        private void reportViewer1_PageSettingsChanged(object sender, EventArgs e)
        {

        }

        private void repContratoLote_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
        }
    }
}
