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

namespace PRESENTACION.PAGOS
{
    public partial class formPago : Form
    {
        private formPagoLogica contexto;
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
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha seleccionado ningún contrato de lote.";
                }               
                else if (string.IsNullOrEmpty(txtMontoRecibido.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha ingresado el monto($) recibido.";
                }             


                if (msjErr != null && msjErr.Length > 0)
                {
                    MessageBox.Show(msjErr[1],
                        msjErr[0],
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (contexto.ObjPagoData == null)
                {                    
                    contexto.InstanciarPago();
                    contexto.ObjPago.Folio = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.PAGO);
                    contexto.ObjPago.FechaEmision = Global.FechaServidor();
                    contexto.ObjPago.NoPago = contexto.ObjContratoData.PagosRealizados + 1;
                    msjSuccess[0] = "Se ha generado el contrato " + contexto.ObjPago.Folio;
                }
                else
                {
                    msjSuccess[0] = "Se ha modificado el pago " + contexto.ObjPago.Folio;
                }

                contexto.ObjPago.ContratoId = contexto.ObjContratoData.ContratoId;
                contexto.ObjPago.USUARIORecibeId = Global.ObjUsuario.Id;
                contexto.ObjPago.Monto = Convert.ToDecimal(txtMontoRecibido.Text);


                contexto.Guardar();


                MessageBox.Show("Registro guardado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (contexto.ObjPago != null && contexto.ObjPagoData != null)
            {
                txtFolioPago.Text = contexto.ObjPagoData.FolioPago;
                txtFolioContrato.Text = contexto.ObjPagoData.FolioContrato;
                txtClaveLote.Text = contexto.ObjPagoData.ClaveLote;
                txtZona.Text = contexto.ObjPagoData.Zona;
                txtMontoRecibido.Text = contexto.ObjPagoData.MontoRecibido.ToString("N2");
                txtFechaEmision.Text = contexto.ObjPagoData.FechaEmision.ToString("dd/MM/yyyy HH:mm:ss");
                txtRecibePago.Text = contexto.ObjPagoData.UsuarioRecibe;
                txtFechaReimpresion.Text = contexto.ObjPagoData.FechaReimpresion != null ? 
                                            (Convert.ToDateTime(contexto.ObjPagoData.FechaReimpresion).ToString("dd/MM/yyyy HH:mm:ss")) : "";

                //ver aqui como definir el estado del comportamiento de pago del cliente
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

        }

        private void btnBuscarContrato_Click(object sender, EventArgs e)
        {

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
                    txtFolioPago.Text = contexto.ObjPagoData.FolioPago;
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
            txtFolioContrato.Text = contexto.ObjContratoData.Folio;
            txtClaveLote.Text = contexto.ObjContratoData.ClaveLote;
            txtZona.Text = contexto.ObjContratoData.ZonaLote;
            txtDiaPago.Text = contexto.ObjContratoData.DiaPago.ToString("N0");
            txtNoPago.Text = (contexto.ObjContratoData.PagosRealizados + 1).ToString("N0");

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
                    txtFolioContrato.Text = contexto.ObjContratoData.Folio;
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
                BuscarPagoPorFolio(txtFolioContrato.Text);
            }
        }

        private void BuscarPagoPorFolio(string folioPago)
        {
            contexto.BuscarPagoFolio(folioPago);
            if (contexto.ObjPagoData == null)
            {
                MessageBox.Show("No se encontro ningún pago asociado al folio ingresado.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetDataPago();
        }
    }
}
