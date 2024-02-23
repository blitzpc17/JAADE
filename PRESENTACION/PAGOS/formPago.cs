using CAPALOGICA.LOGICAS.PAGOS;
using PRESENTACION.BUSQUEDA;
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
        private bool cargado = false;

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
                        contexto.ObjPago.NoPago = contexto.ObjInformacionPago.NoPagosGraciaRealizados + 1;
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
            ImportarLayout();
        }

        private void ImportarLayout()
        {
            
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
            txtClaveLote.Text = contexto.ObjContratoData.IdentificadorLote;
            txtZona.Text = contexto.ObjContratoData.NombreZona;
            txtDiaPago.Text = contexto.ObjContratoData.DiaPago.ToString("N0");
            txtPrecioLote.Text = contexto.ObjContratoData.PrecioLote.ToString("N2");
            txtSaldoPendiente.Text = Convert.ToDecimal(contexto.ObjContratoData.PrecioLote - contexto.ObjInformacionPago.SaldoFavor).ToString("N2");
            txtSaldoFavor.Text = contexto.ObjInformacionPago.SaldoFavor.ToString("N2");
            txtMensualidad.Text = contexto.ObjInformacionPago.MontoMensualidad.ToString("N2");
            if (contexto.ObjPago == null)
            {
                if(contexto.ObjContratoData.EstadoId == (int)Enumeraciones.EstadosProcesoContratos.ATRASADO)
                {
                    txtNoPago.Text = (contexto.ObjInformacionPago.NoPagosGraciaRealizados + 1).ToString("N0");
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
            
            //validar si no excede el numero de pagos ordinarios poner el normal
            if (contexto.ObjContratoData.EstadoId == (int)Enumeraciones.EstadosProcesoContratos.VIGENTE)
            {
                int noPagosAtrasados = contexto.ObjInformacionPago.NoPagoActual - contexto.ObjInformacionPago.NoPagosRealizados;
                if ( noPagosAtrasados >= 3)
                {
                    if(MessageBox.Show("El contrato presenta un atraso de "+noPagosAtrasados.ToString("N0")+", ¿Desea proceder a su cancelación?",
                        "Advertencia",
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question
                        ) == DialogResult.Yes)
                    {
                        //cambiar estado del contrato y reiniciar el modulo
                        contexto.ObjContrato.ESTADOId = (int)Enumeraciones.EstadosProcesoContratos.CANCELADO;
                        contexto.ObjContrato.Observacion = @"CANCELADO POR EXCEDER 3 o MÁS PAGOS SIN ABONAR.";
                        contexto.GuardarContrato();

                        InicializarModulo();
                        return;
                    }
                    
                }

                if (contexto.ObjInformacionPago.NoPagoActual > contexto.ObjInformacionPago.NoPagosContrato)
                {
                    if (MessageBox.Show("Se ha excedido el plazo de pago ordinario, ¿desea cambiar el contrato a PERIODO DE GRACIA para que se recalculen los montos de pago?",
                        "Atención",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //globalCambiarestadocontrato
                        txtEstadoPagoCliente.Text = "PERIODO DE GRACIA.";
                    }
                    else
                    {
                        //preguntar que hacer en caso de que nel
                        txtEstadoPagoCliente.Text = "ATRASO DE PAGO.";
                    }

                }
                else if (contexto.ObjInformacionPago.NoPagoActual == contexto.ObjInformacionPago.NoPagosContrato)
                {
                    txtEstadoPagoCliente.Text = "VENCIMIENTO DEL PLAZO ACORDADO PRÓXIMO.";
                }
                else
                {
                    if (
                        (contexto.ObjInformacionPago.NoPagoActual * contexto.ObjInformacionPago.MontoMensualidad) >
                            (contexto.ObjInformacionPago.NoUltimoPago * contexto.ObjInformacionPago.MontoMensualidad)
                        )
                    {
                        txtEstadoPagoCliente.Text = "ATRASO DE PAGO.";
                    }
                    else
                    {
                        txtEstadoPagoCliente.Text = "PAGO AL CORRIENTE.";
                    }
                }


            }else if(contexto.ObjContratoData.EstadoId == (int)Enumeraciones.EstadosProcesoContratos.ATRASADO)
            {
                if (contexto.ObjInformacionPago.NoPagoProrrogaActual > contexto.ObjInformacionPago.NoPagosGracia)
                {
                    if (MessageBox.Show("Se ha excedido el plazo de pago ordinario, ¿desea cambiar el contrato a PERIODO DE GRACIA para que se recalculen los montos de pago?",
                        "Atención",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //globalCambiarestadocontrato
                        txtEstadoPagoCliente.Text = "PERIODO DE GRACIA.";
                    }
                    else
                    {
                        //preguntar que hacer en caso de que nel
                        txtEstadoPagoCliente.Text = "ATRASO DE PAGO.";
                    }

                }
                else if (contexto.ObjInformacionPago.NoPagoActual == contexto.ObjInformacionPago.NoPagosContrato)
                {
                    txtEstadoPagoCliente.Text = "VENCIMIENTO DEL PLAZO ACORDADO PRÓXIMO.";
                }
                else
                {
                    if (
                        (contexto.ObjInformacionPago.NoPagoActual * contexto.ObjInformacionPago.MontoMensualidad) >
                            (contexto.ObjInformacionPago.NoUltimoPago * contexto.ObjInformacionPago.MontoMensualidad)
                        )
                    {
                        txtEstadoPagoCliente.Text = "ATRASO DE PAGO.";
                    }
                    else
                    {
                        txtEstadoPagoCliente.Text = "PAGO AL CORRIENTE.";
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "No se pueden recibir pagos para el contrato seleccionado, su estado es: "+contexto.ObjContratoData.NombreEstado.ToUpper(),
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMontoRecibido.Enabled = false;
                    
            }


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
