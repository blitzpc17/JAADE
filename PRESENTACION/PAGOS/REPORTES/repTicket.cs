using CAPADATOS.Entidades;
using Microsoft.Reporting.WinForms;
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
    public partial class repTicket : Form
    {
        private clsTicketPago obj;
        private dtTicket ds;
        public repTicket(clsTicketPago obj)
        {
            InitializeComponent();
            this.obj = obj;
        }

        private void InicializarModulo()
        {
            InstanciarDataSet();
        }
        private void InstanciarDataSet()
        {
            ds = new dtTicket();
            ds.dtEncabezado.Rows.Add(obj.Encabezado);
            foreach(var partidas in obj.Partidas)
            {
                ds.dtPartidas.Rows.Add(partidas);
            }
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", ds.Tables["dtEncabezado"]);
            ReportDataSource reportDataSource2 = new ReportDataSource("DataSet1", ds.Tables["dtPartidas"]);

            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.RefreshReport();

        }

        private void repTicket_Load(object sender, EventArgs e)
        {
            InicializarModulo();           
        }
    }
}
