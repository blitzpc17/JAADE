using CAPADATOS.Entidades;
using Microsoft.Reporting.WinForms;
using PRESENTACION.UTILERIAS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public List<ReportParameter> parametros;
        private string pathPdf = "";
        private clsWhatsApp objWhats;

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
            ds.dtEncabezado.Rows.Add(obj.Encabezado.Fecha.ToString("dd/MM/yyyy"), obj.Encabezado.Cliente, "$ "+obj.Encabezado.PrecioLote.ToString("N2"), obj.Encabezado.NoPagos.ToString("N0"), obj.Encabezado.Zona, obj.Encabezado.IdentificadorLote, obj.Encabezado.ObsComportamientoPago);
            foreach(var partidas in obj.Partidas)
            {
                ds.dtPartidas.Rows.Add(partidas.No.ToString("N0"), "$ "+partidas.Monto.ToString("N2"), partidas.Fecha.ToString("dd/MM/yyyy"), partidas.Observacion);
            }
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", ds.Tables["dtEncabezado"]);
            ReportDataSource reportDataSource2 = new ReportDataSource("DataSet2", ds.Tables["dtPartidas"]);

            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            //agregar parametros

            reportViewer1.LocalReport.SetParameters(parametros);

            this.reportViewer1.RefreshReport();

        }

        private void repTicket_Load(object sender, EventArgs e)
        {
            InicializarModulo();           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            RealizarEnvio();
        }

        private void RealizarEnvio()
        {
            string mainPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
          
            byte[] pdf = reportViewer1.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streams, out Warning[] warnings);
            pathPdf = Path.Combine(mainPath, "recibos\\reciboenvio.pdf");

            // Guardar el archivo PDF en disco
            File.WriteAllBytes(pathPdf, pdf);
            objWhats = new clsWhatsApp();
            objWhats.TelefonoDestino = "2381458680";
            objWhats.Cuerpo = "Hola somos de JAADE! Le enviamos su recibo de pago. ¡Saludos!";
            objWhats.PathMediaFile = @"https://jaade.net/recibos/reciboenvio.pdf";

            //envio...
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //subir archivo ftp
            Global.SubirArchivoFtp(Enumeraciones.VariablesGlobales.CredencialesFtp, pathPdf, "recibos/reciboenvio.pdf");
            //envio twilio
            Global.EnviarWhatsApp(Enumeraciones.VariablesGlobales.CredencialesTwilio, objWhats );
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Subido!!", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
