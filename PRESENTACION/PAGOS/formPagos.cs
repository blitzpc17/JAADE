using CAPALOGICA.LOGICAS.PAGOS;
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
using PRESENTACION.BUSQUEDA;

namespace PRESENTACION.PAGOS
{
    public partial class formPagos : Form
    {
        
        private formPagosLogica contexto;          

        public formPagos()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarFormulario()
        {
            contexto = new formPagosLogica();
            LimpiarControles();
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            txtFechaRegistro.Text = Global.FechaServidor().ToString("dd-MM-yyyy HH:mm:ss");
            dgvRegistros.DataSource = null;
            tsTotalRegistros.Text = @"0";
            contexto.ObjUsuarioRecibe = Global.ObtenerDataUsuario(Global.ObjUsuario.Id);
            txtUsuarioRecibePago.Text = contexto.ObjUsuarioRecibe.Nombre;
            
        }

        private void ListarPagosCliente(int clienteId)
        {

        }

        private void CalcularSaldosPendientes()
        {
            if (contexto.ObjLotes != null)
            {

            }
        }

        private void formPagos_Load(object sender, EventArgs e)
        {
            InicializarFormulario();
        }

        private void formPagos_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (contexto.ObjTicketPago == null) {
                MessageBox.Show("No hay ningún pago seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 
            var formTicket = new REPORTES.repTicket(contexto.ObjTicketPago);
            formTicket.ShowDialog();
        }

        private void btnUsuarioSolicita_Click(object sender, EventArgs e)
        {
            //filtre pagos por cliente
            busPagos busP;
            if (contexto.ObjCliente == null)
            {
                busP = new busPagos();
            }
            else
            {
                busP = new busPagos(contexto.ObjCliente.Id);
            }
            
            busP.ShowDialog();
            contexto.ObjPagoDato = busP.ObjEntidad;
            if(contexto.ObjPagoDato == null)
            {
                MessageBox.Show("No hay ningún pago seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetDataPago();
        }

        private void SetDataPago()
        {
            //llenar datos del objcliente y objlote
            contexto.ObjCliente = contexto.ObtenerDataCliente(contexto.ObjPagoDato.ClienteId);
            contexto.ObjLotes = contexto.ObtenerLoteData(contexto.ObjPagoDato.LoteId);
            txtNumeroReferencia.Text = contexto.ObjPagoDato.NumeroReferencia;
            txtFechaRegistro.Text = contexto.ObjPagoDato.FechaEmision.ToString("dd-MM-yyyy HH:mm:ss");
            CalcularDatosPago();
        }

        private void CalcularDatosPago()
        {
            contexto.ObjInformacionPagoLote = contexto.ObtenerDataPagoLote(contexto.ObjCliente.Id, contexto.ObjLotes.Id);

            txtEstado.Text = DefinirEstadoPagoLote();
            txtTotalPagos.Text = contexto.ObjInformacionPagoLote.NumeroPagos.ToString("N0");
            txtPagosRealizados.Text = contexto.ObjInformacionPagoLote.PagosRealizados.ToString("N0");
            txtNoPagoActual.Text = contexto.ObjInformacionPagoLote.PagoActual.ToString("N0");            
            txtSaldoFavor.Text = contexto.ObjInformacionPagoLote.SaldoFavor.ToString("N2");
            txtSaldoContra.Text = contexto.ObjInformacionPagoLote.SaldoContra.ToString("N2");
            txtMontoMensualidad.Text = contexto.ObjInformacionPagoLote.MontoMensualidad.ToString("N2");
            txtMontoAtrasado.Text = contexto.ObjInformacionPagoLote.MontoAtrasado.ToString("N2");
            txtMontoExcedePlazo.Text = contexto.ObjInformacionPagoLote.MontoExcedePlazo.ToString("N2");
            txtTotalPagar.Text = contexto.ObjInformacionPagoLote.TotalPagar.ToString("N2");
            
        }
        private void MarcarExcedioPlazo()
        {
            contexto.ActualizarExcedioPlazoPagoLote(contexto.ObjLotes.Id, contexto.ObjCliente.Id, true);
        }
        private string DefinirEstadoPagoLote()
        {
            if (contexto.ObjInformacionPagoLote.ExcedePlazoPago)
            {
                return "Número de pagos excedido.";
            }else if (contexto.ObjInformacionPagoLote.SaldoContra>0)
            {
                return "Atraso en pagos.";
            }
            else
            {
                return "Sin adeudos.";
            }
        }

        private void Guardar()
        {
            string _errMsj = "";
            if (contexto.ObjCliente == null)
            {
                _errMsj = "No se ha seleccionado ningún cliente.";
            }else if (contexto.ObjLotes == null)
            {
                _errMsj = "No se ha seleccionado el lote.";
            }else if (string.IsNullOrEmpty(txtMontoRecibido.Text))
            {
                _errMsj = "No ha ingresado el monto a pagar";
            }else if (!Global.EsValorDecimal(txtMontoRecibido.Text))
            {
                _errMsj = "Ha ingresado un valor no válido en el monto.";
            }

            if (!string.IsNullOrEmpty(_errMsj))
            {
                MessageBox.Show(_errMsj, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }
            else
            {
                if (contexto.ObjPagoDato == null)
                {
                    contexto.InstanciarPago();
                    contexto.ObjPago.NumeroReferencia = Global.ObtenerFolio("PAGO");
                }
                contexto.ObjPago.CLIENTEId = contexto.ObjCliente.Id;
                contexto.ObjPago.LOTEId = contexto.ObjLotes.Id;
                contexto.ObjPago.Monto = Convert.ToDecimal(txtMontoRecibido.Text);
                contexto.ObjPago.USUARIORECIBEPAGOId = Global.ObjUsuario.Id;
                contexto.ObjPago.FechaEmison = Global.FechaServidor();

                contexto.Guardar();

                MessageBox.Show("Pago registrado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }
        

        private void btnCliente_Click(object sender, EventArgs e)
        {
            var busClientes = new busClientes();   
            busClientes.ShowDialog();

            contexto.ObjCliente = busClientes.ObjEntidad;
            if (contexto.ObjCliente == null)
            {
                MessageBox.Show("No se selecciono ningún cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtCliente.Text = contexto.ObjCliente.Cliente;
        }

        private void btnLote_Click(object sender, EventArgs e)
        {
            if (contexto.ObjCliente == null)
            {
                MessageBox.Show("No se selecciono ningún cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var busLotes = new busLotesZona(contexto.ObjCliente.Id, true);
            busLotes.ShowDialog();
            contexto.ObjLotes = busLotes.ObjEntidad;
            if (contexto.ObjLotes == null)
            {
                MessageBox.Show("No se selecciono ningún cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtLote.Text = contexto.ObjLotes.Identificador;
            txtZona.Text = contexto.ObjLotes.Zona;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
    }
}
