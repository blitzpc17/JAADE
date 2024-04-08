using CAPADATOS.ADO.PAGOS;
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
        private List<clsAGENDACLIENTE> lstContactos;
        public List<KeyValuePair<string, int>> LstTipos { get; private set; }
        private bool cargado = false;
        private bool whats = true; //default whats
        private clsCorreo ObjCorreo;

        public repTicket(clsTicketPago obj)
        {
            InitializeComponent();
            this.obj = obj;
        }

        private void InicializarModulo()
        {
            cargado = false;
            InstanciarDataSet();
            LlenarCatalogos();
            txtCliente.Text = obj.Encabezado.Cliente;
        }

        private void LlenarCatalogos()
        {
            ListarTiposEnvio();
            cbxTipoContacto.DataSource = LstTipos;
            cbxTipoContacto.DisplayMember = "key";
            cbxTipoContacto.ValueMember = "value";
            cbxTipoContacto.SelectedIndex = -1;

            ListarContactosCliente();

        }

        private void ListarContactosCliente() {
            using (var contexto = new Persona_AgendaADO())
            {
                lstContactos = contexto.ListarAgendaCliente(obj.Encabezado.ClienteId);
                cargado = true;
            }
        }
        private void FiltrarListaContactosXTipo(int tipo)
        {
            if (lstContactos == null) return;

            cbxDestino.DataSource = lstContactos.Where(x => x.TipoId == tipo).ToList();
            cbxDestino.ValueMember = "Valor";
            cbxDestino.DisplayMember = "Valor";


        }

        private void ListarTiposEnvio()
        {
            LstTipos = new List<KeyValuePair<string, int>>();

            LstTipos.Add(new KeyValuePair<string, int>("TELEFONO", 1));
            LstTipos.Add(new KeyValuePair<string, int>("CORREO ELECTRONICO", 3));
        }



        private void InstanciarDataSet()
        {
            ds = new dtTicket();
            ds.dtEncabezado.Rows.Add(obj.Encabezado.Fecha.ToString("dd/MM/yyyy"), obj.Encabezado.Cliente, "$ "+obj.Encabezado.PrecioLote.ToString("N2"), obj.Encabezado.NoPagos.ToString("N0"), obj.Encabezado.Zona, obj.Encabezado.IdentificadorLote, obj.Encabezado.ObsComportamientoPago, obj.Encabezado.Contrato);
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
            if(cbxTipoContacto.SelectedIndex == -1)
            {
                MessageBox.Show("No ha seleccionado el tipo de contacto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(cbxDestino.SelectedIndex == -1)
            {
                MessageBox.Show("No ha seleccionado el destino para poder enviar el recibo.","Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }           

            string mainPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            byte[] pdf = reportViewer1.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streams, out Warning[] warnings);
            pathPdf = Path.Combine(mainPath, "recibos\\" + obj.Encabezado.Contrato + ".pdf");

            // Guardar el archivo PDF en disco
            File.WriteAllBytes(pathPdf, pdf);

            whats = (cbxTipoContacto.SelectedValue.ToString() == "TELEFONO");

            if (whats)
            {                
                objWhats = new clsWhatsApp();
                objWhats.TelefonoDestino = cbxDestino.SelectedValue.ToString();
                objWhats.Cuerpo = "Hola somos de JAADE! Le enviamos su recibo de pago. ¡Saludos!";
                objWhats.PathMediaFile = @"https://jaade.net/recibos/reciboenvio.pdf";
            }
            else
            {
                ObjCorreo = new clsCorreo();
                ObjCorreo.Asunto = "RECIBO DE PAGO JAADE";
                ObjCorreo.Cuerpo = "<p>Buen día: <br>Por medio del presente se le hace el envio de su recibo de pago. <br>Saludos</p>";
                ObjCorreo.CorreoDestino = new List<string> { cbxDestino.SelectedValue.ToString() };
                ObjCorreo.PathAttach = new List<string> { };
            }

            
            lblStatusEnvio.Text = "Enviando....";
            //envio...
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (whats)
            {
                //subir archivo ftp
                Global.SubirArchivoFtp(Enumeraciones.VariablesGlobales.CredencialesFtp, pathPdf, "recibos/reciboenvio.pdf");
                //envio twilio
                Global.EnviarWhatsApp(Enumeraciones.VariablesGlobales.CredencialesTwilio, objWhats);
            }
            else
            {
                Global.EnviarCorreo(Enumeraciones.VariablesGlobales.CredencialesCorreo, ObjCorreo);
            }          

            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatusEnvio.Text = "";
            MessageBox.Show("Subido!!", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void cbxTipoContacto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!cargado) return;
            if (cbxTipoContacto.SelectedIndex == -1) return;
            FiltrarListaContactosXTipo((int)cbxTipoContacto.SelectedValue);
            
        }
    }
}
