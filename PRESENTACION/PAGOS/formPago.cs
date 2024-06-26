﻿using CAPADATOS.Entidades;
using CAPALOGICA.LOGICAS.PAGOS;
using Microsoft.Reporting.WinForms;
using PRESENTACION.BUSQUEDA;
using PRESENTACION.PAGOS.REPORTES;
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

namespace PRESENTACION.PAGOS
{
    public partial class formPago : Form
    {
        private formPagoLogica contexto;
        private busContratos busContrato;
        private busPagos busPagos;

        private List<ReportParameter> parametros;
        private bool cargado = false;

        private clsValidacionContrato objValidacion;

        public formPago()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void formPago_Load(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        public void InicializarModulo()
        {
            try
            {
                cargado = false;
                LimpiarControles();
                InstanciarContexto();
                SetDataUsuarioCobra();
                txtFolioPago.Text = @"NUEVO";
                cargado = true;
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar cargar el módulo. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }

        }

        public void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            txtMontoRecibido.Enabled = true;
        }

        public void InstanciarContexto()
        {
            contexto = new formPagoLogica();
        }

        public void SetDataUsuarioCobra()
        {
            txtRecibePago.Text = Global.ObtenerNombreUsuario(Global.ObjUsuario);
            txtFechaEmision.Text = Global.FechaServidor().ToString("dd/MM/yyyy HH:mm:ss");

        }

        public void Guardar()
        {
            try
            {
                string[] msjErr = null;
                string[] msjSuccess = new string[2];

                //validaciones
                if (contexto.ObjContratoData == null)
                {
                    msjErr = new string[2];
                    msjErr[1] = "Advertencia";
                    msjErr[0] = "No ha seleccionado ningún contrato de lote.";
                }               
                else if (string.IsNullOrEmpty(txtMontoRecibido.Text))
                {
                    msjErr = new string[2];
                    msjErr[1] = "Advertencia";
                    msjErr[0] = "No ha ingresado el monto($) recibido.";
                }
                else if (
                    contexto.ObjContratoData.EstadoId != (int)Enumeraciones.EstadosProcesoContratos.VIGENTE &&
                    contexto.ObjContratoData.EstadoId != (int)Enumeraciones.EstadosProcesoContratos.ATRASADO
                    )
                {
                    msjErr = new string[2];
                    msjErr[1] = "Advertencia";
                    msjErr[0] = "No se puede realizar el pago ya que el estado actual del contrato es "+contexto.ObjContratoData.NombreEstado+".";
                }          


                if (msjErr != null && msjErr.Length > 0)
                {
                    MessageBox.Show(msjErr[0],
                        msjErr[1],
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (contexto.ObjPago == null)
                {                    
                    contexto.InstanciarPago();
                    contexto.ObjPago.Folio = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.PAGO);
                    contexto.ObjPago.FechaEmision = Global.FechaServidor();
                    if (contexto.ObjContratoData.EstadoId == (int)Enumeraciones.EstadosProcesoContratos.ATRASADO)
                    {
                        contexto.ObjPago.PagoOrdinario = false;

                        if (contexto.ObjInformacionPago.NoPagosGraciaRealizados != null)
                        {
                            contexto.ObjPago.NoPago = Convert.ToInt32(contexto.ObjInformacionPago.NoPagosGraciaRealizados) + 1;

                        }                            
                        
                    }
                    else
                    {
                        contexto.ObjPago.PagoOrdinario = true;
                        contexto.ObjPago.NoPago = contexto.ObjInformacionPago.NoPagosRealizados + 1;
                    }

                    msjSuccess[0] = "Se ha generado el pago " + contexto.ObjPago.Folio +" exitosanente.";
                }
                else
                {
                    msjSuccess[0] = "Se ha modificado el pago " + contexto.ObjPago.Folio +".";
                }
                
                contexto.ObjPago.ContratoId = contexto.ObjContratoData.ContratoId;
                contexto.ObjPago.USUARIORecibeId = Global.ObjUsuario.Id;
                contexto.ObjPago.Monto = Convert.ToDecimal(txtMontoRecibido.Text);
                contexto.ObjPago.ViaGenerado = (int)Enumeraciones.PagosViaGeneracion.PAGO;

                contexto.Guardar();


                MessageBox.Show(msjSuccess[0], "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InicializarModulo();
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar guardar el registro. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetDataPago()
        {

            if (contexto.ObjPago!=null)
            {
                txtFolioPago.Text = contexto.ObjPago.Folio;
                contexto.BuscarContratoId(contexto.ObjPago.ContratoId);
                contexto.ObtenerInformacionPago(contexto.ObjContratoData.NoReferencia);
                
                SetDataContrato();
            }
            else
            {
                MessageBox.Show("No se encontro el registro seleccionado. Vuelva a intentarlo.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            GenerarRecibo();
        }

        private void GenerarRecibo()
        {
            if (contexto.ObjPago == null)
            {
                MessageBox.Show("No se ha seleccionado ningún registro.", "Advertencia",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            contexto.InstanciarObjTicket();
            contexto.InstanciarEncabezadoTicket();
            //traer agenda del cliente
            contexto.ObjEncabezadoTicket.ClienteId = contexto.ObjContratoData.ClienteId;
            contexto.ObjEncabezadoTicket.Cliente = contexto.ObjContratoData.ClaveCliente + " " + contexto.ObjContratoData.NombreCliente;
            contexto.ObjEncabezadoTicket.ObsComportamientoPago = contexto.ObjContratoData.Observacion;
            contexto.ObjEncabezadoTicket.NoPagos = contexto.ObjContratoData.NoPagos;
            contexto.ObjEncabezadoTicket.Fecha = contexto.ObjContratoData.FechaEmision;
            contexto.ObjEncabezadoTicket.PrecioLote = contexto.ObjContratoData.PrecioLote;
            contexto.ObjEncabezadoTicket.IdentificadorLote = contexto.ObjContratoData.LotesRelacionados;
            contexto.ObjEncabezadoTicket.Zona = contexto.ObjContratoData.ZonaNombre;
            contexto.ObjEncabezadoTicket.Contrato = contexto.ObjContratoData.NoReferencia;

            contexto.ObjTicket.Encabezado = contexto.ObjEncabezadoTicket;

            contexto.ObtenerPartidasPagoContrato(contexto.ObjContratoData.NoReferencia);

            contexto.ObjTicket.Partidas = contexto.LstPartidasTicket;

            InstanciarListaParametros();

            parametros.Add(new ReportParameter("MontoAcumulado", contexto.LstPartidasTicket.Sum(x=>x.Monto).ToString("N2")));
            parametros.Add(new ReportParameter("FechaReimpresion", Global.FechaServidor().ToString("dd/MM/yyyy HH:mm:ss")));
            
            repTicket rep = new repTicket(contexto.ObjTicket);
            rep.parametros = parametros;    
            rep.ShowDialog();


        }

        public void InstanciarListaParametros()
        {
            parametros = new List<ReportParameter>();
        }

        private void btnBuscarPago_Click(object sender, EventArgs e)
        {
            busPagos = new busPagos();
            busPagos.ShowDialog();

            if (busPagos.ObjEntidad == null)
            {
                MessageBox.Show("No se ha seleccionado ningún registro.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            contexto.ObtenerPago(busPagos.ObjEntidad.PagoId);
            SetDataPago();           
        }

        private void btnBuscarContrato_Click(object sender, EventArgs e)
        {
            busContrato = new busContratos();
            busContrato.ShowDialog();

            if (busContrato.ObjEntidad == null)
            {
                MessageBox.Show("No se ha seleccionado ningún registro.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            contexto.BuscarContratoFolio(busContrato.ObjEntidad.NoReferencia);
            SetDataContrato();

            
         
        }

        private void txtFolioPago_Leave(object sender, EventArgs e)
        {
            if (!cargado) return;
            if (string.IsNullOrEmpty(txtFolioPago.Text))
            {
                if (contexto.ObjContratoData == null)
                {
                    txtFolioPago.Text=@"NUEVO";
                }
                else
                {
                    txtFolioPago.Text = contexto.ObjPago.Folio;
                }
            }
        }

        private void BuscarContratoPorFolio(string folioContrato)
        {
            contexto.BuscarContratoFolio(folioContrato);
            if (contexto.ObjContratoData == null)
            {
                MessageBox.Show("No se encontro ningún contrato asociado al folio ingresado.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetDataContrato();        
        }

        private void SetDataContrato()
        {
            txtFolioContrato.Text = contexto.ObjContratoData.NoReferencia;
            txtZona.Text = contexto.ObjContratoData.ZonaNombre;
            txtDiaPago.Text = contexto.ObjContratoData.DiaPago.ToString("N0");
            txtClaveLote.Text = contexto.ObjContratoData.LotesRelacionados;

            if (contexto.ObjContratoData.EstadoId == (int)Enumeraciones.EstadosProcesoContratos.ATRASADO)
            {
                lblPrecioLote.Text = @"Importe Ext. ($):";
                txtPrecioLote.Text = Convert.ToDecimal(contexto.ObjContratoData.MontoGracia).ToString("N2");
                lblSaldoPendiente.Text = @"Saldo Ext. Pend. ($):";
                txtSaldoPendiente.Text = Convert.ToDecimal(contexto.ObjContratoData.MontoGracia - contexto.ObjContratoData.MontoExtendidoDado).ToString("N2");
                lblSaldoFavor.Text = @"Saldo Favor ($):";
                txtSaldoFavor.Text = Convert.ToDecimal(contexto.ObjContratoData.MontoExtendidoDado).ToString("N2");
                lblMensualidad.Text = @"Mensualidad ($):";
                txtMensualidad.Text = Convert.ToDecimal(contexto.ObjContratoData.MontoGracia/contexto.ObjContratoData.NoPagosGracia).ToString("N2");
            }
            else
            {
                lblPrecioLote.Text = @"Precio Lote ($):";
                txtPrecioLote.Text = contexto.ObjContratoData.PrecioLote.ToString("N2");
                lblSaldoPendiente.Text = @"Saldo Pendiente ($):";
                txtSaldoPendiente.Text = Convert.ToDecimal(contexto.ObjContratoData.PrecioLote - contexto.ObjInformacionPago.SaldoFavor).ToString("N2");
                lblSaldoFavor.Text = @"Saldo Favor ($):";
                txtSaldoFavor.Text = contexto.ObjInformacionPago.SaldoFavor.ToString("N2");
                lblMensualidad.Text = @"Mensualidad ($):";
                txtMensualidad.Text = contexto.ObjInformacionPago.MontoMensualidad.ToString("N2");
            }
            

            if (contexto.ObjPago == null)
            {
                if(contexto.ObjContratoData.EstadoId == (int)Enumeraciones.EstadosProcesoContratos.ATRASADO)
                {
                    txtNoPago.Text = (contexto.ObjInformacionPago.NoPagosGraciaRealizados + 1).ToString();
                }
                else
                {
                    txtNoPago.Text = (contexto.ObjInformacionPago.NoPagosRealizados + 1).ToString("N0");
                }
                
            }
            else
            {
                txtNoPago.Text = contexto.ObjPago.NoPago.ToString("N0");
                txtMontoRecibido.Text = contexto.ObjPago.Monto.ToString("N2");
                txtObservacion.Text = contexto.ObjPago.Observacion;
            }

            objValidacion = Global.ValidarEstadoContrato(contexto.ObjContratoData);
            if (!objValidacion.ProcedePagar)
            {
                MessageBox.Show(
                   "No se pueden recibir pagos para el contrato seleccionado: " + objValidacion.Mensaje,
                   "Advertencia",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
                
                txtEstadoPagoCliente.Text = "NO SE PUEDE COBRAR.";
            }
            else
            {
                txtEstadoPagoCliente.Text = "PAGO AL CORRIENTE.";
            }

            txtMontoRecibido.Enabled = objValidacion.ProcedePagar;
            btnGuardar.Enabled = objValidacion.ProcedePagar;


        }

        private void txtFolioContrato_Leave(object sender, EventArgs e)
        {
            if (!cargado) return;
            if (string.IsNullOrEmpty(txtFolioContrato.Text))
            {
                if (contexto.ObjContratoData == null)
                {
                    txtFolioContrato.Clear();
                }
                else
                {
                    txtFolioContrato.Text = contexto.ObjContratoData.NoReferencia;
                }
            }
        }

        private void formPago_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void txtFolioContrato_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtFolioContrato.Text.Length < 5 && e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Folio de contrato no válido.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                BuscarContratoPorFolio(txtFolioContrato.Text);
            }
        }

        private void txtFolioPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtFolioPago.Text.Length < 11 && e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Folio de pago no válido.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                BuscarPagoPorFolio(txtFolioPago.Text);
            }
        }

        private void BuscarPagoPorFolio(string folioPago)
        {
            contexto.ObtenerPagFolioo(folioPago);
            if (contexto.ObjPago == null)
            {
                MessageBox.Show("No se encontro ningún pago asociado al folio ingresado.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetDataPago();
        }
    }
}
