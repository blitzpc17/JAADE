using CAPADATOS.Entidades;
using CAPALOGICA.LOGICAS.PAGOS;
using CAPALOGICA.LOGICAS.SISTEMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
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
    public partial class formContrato : Form
    {
        private bool cargado = false;
        private bool nuevoContrato = false;
        private formContratoLogica contexto;
        private busClientes busCliente;
        private busLotesZona busLoteZona;
        private busContratos busContrato;
        private bool lotescargados = false;
        private List<KeyValuePair<String, int>>LstVendedores;

        public formContrato()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        public void InicializarModulo()
        {
            try
            {
                lotescargados = false;
                cargado = false;
                LimpiarControles();
                InstanciarContexto();
                ListarCatalogos();
                DatosModulo();
                txtFolioContrato.Text = @"NUEVO";
                cargado = true;
                nuevoContrato = false;
                txtContratoReubidado.Enabled = false;
                btnBusContratoReubicado.Enabled = false;
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

        private void DatosModulo()
        {
            txtFechaEmision.Text = Global.FechaServidor().ToString("dd/MM/yyyy HH:mm:ss");
            txtRealizo.Text = Global.ObtenerNombreUsuario(Global.ObjUsuario);
            txtFechaReimpresion.Text = (contexto.ObjContratoData == null 
                || contexto.ObjContratoData.FechaReimpresion==null) ? "" : Convert.ToDateTime(contexto.ObjContratoData.FechaReimpresion).ToString("dd/MM/yyyy HH:mm:ss");
        }

        public void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
        }

        public void ListarCatalogos()
        {
            contexto.ListarCatalogos();

            cbxZona.DataSource = contexto.LstZonas;
            cbxZona.DisplayMember = "Nombre";
            cbxZona.ValueMember = "Id";
            cbxZona.SelectedIndex = -1;

            cbxEstado.DataSource = contexto.LstEstados;
            cbxEstado.DisplayMember = "Nombre";
            cbxEstado.ValueMember = "Id";
            cbxEstado.SelectedIndex = -1;

            LstVendedores = new List<KeyValuePair<string, int>>();
            LstVendedores.Add(new KeyValuePair<string, int>("ALEJANDRA GUADALUPE MORO GARCIA", 0));
            LstVendedores.Add(new KeyValuePair<string, int>("ANGEL DONATO BRAVO MORO", 1));
        

            cbxVendedores.DataSource = LstVendedores;
            cbxVendedores.DisplayMember = "Key";
            cbxVendedores.ValueMember = "Value";
            cbxVendedores.SelectedIndex = -1;


        }

        public void LlenarCbxSocios(string claveCliente)
        {
            contexto.ListarSociosCliente(claveCliente);
            cbxSocios.DataSource = contexto.LstSociosCliente;
            cbxSocios.DisplayMember = "Nombre";
            cbxSocios.ValueMember = "Id";
            cbxSocios.SelectedIndex = -1;
        }

        public void InstanciarContexto()
        {
            contexto = new formContratoLogica();
        }

        private void txtNoPagos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtDiaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void formContrato_Load(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void txtFolioContrato_Leave(object sender, EventArgs e)
        {
            if (!cargado) return;
            if (string.IsNullOrEmpty(txtFolioContrato.Text))
            {
                if (contexto.ObjContratoData == null)
                {
                    txtFolioContrato.Text = "NUEVO";
                }
                else
                {
                    txtFolioContrato.Text = contexto.ObjContratoData.NoReferencia;
                }
            }
        }

        private void btnBuscarContrato_Click(object sender, EventArgs e)
        {
            busContrato = new busContratos();
            cargado = false;
            busContrato.ShowDialog();

            if (busContrato.ObjEntidad == null)
            {
                MessageBox.Show("No se ha seleccionado ningún registro.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            contexto.BuscarContratoFolio(busContrato.ObjEntidad.NoReferencia);
            SetDataContrato();
            cargado = true;
        }

        private void btnNombreCliente_Click(object sender, EventArgs e)
        {
            busCliente = new busClientes();
            busCliente.ShowDialog();

            if (busCliente.ObjEntidad == null)
            {
                MessageBox.Show("No se ha seleccionado ningín registro.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            contexto.ObjCliente = busCliente.ObjEntidad;
            SetDataCliente();
        }

        private void SetDataCliente()
        {
            txtClaveCliente.Text = contexto.ObjCliente.Clave;
            txtNombreCliente.Text = contexto.ObjCliente.Cliente;
            LlenarCbxSocios(contexto.ObjCliente.Clave);
            LLenarCbxDirecciones();
        }

        private void LLenarCbxDirecciones()
        {
            contexto.LstAgenda = Global.ListarAgendaCliente(contexto.ObjCliente.Id);
            if (contexto.LstAgenda == null || !contexto.LstAgenda.Any(x=>x.TipoId == (int)Enumeraciones.TipoContactoAgenda.DIRECCION))
            {
                MessageBox.Show("El cliente no cuenta con domicilio registrado, tiene que darlo de alta en el módulo de clientes. Cuando termine el registro vuelva a intentar generar el contrato.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            cbxDomicilio.DataSource = contexto.LstAgenda.Where(x=>x.TipoId == (int)Enumeraciones.TipoContactoAgenda.DIRECCION).ToList();
            cbxDomicilio.DisplayMember = "Valor";
            cbxDomicilio.ValueMember = "Id";
            cbxDomicilio.SelectedIndex = -1;
        }

        private void btnBusLotes_Click(object sender, EventArgs e)
        {
            if (cbxZona.SelectedIndex == -1)
            {
                MessageBox.Show("No se ha seleccionado la zona.", 
                    "Advertencia", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }


            busLoteZona = new busLotesZona((int)cbxZona.SelectedValue);
            busLoteZona.ShowDialog();

            if (busLoteZona.ObjEntidad == null)
            {
                MessageBox.Show("No se ha seleccionado ningún registro.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            contexto.ObjLote = contexto.ObtenerLote(busLoteZona.ObjEntidad.Id);
           
            

        }

      /*  private void SetDataLote()
        {
          //  txtClaveLote.Text = contexto.ObjLote.Identificador;
            txtPrecio.Text = contexto.ObjLote.Precio.ToString("N2");
        }*/

        private void txtFolioContrato_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtFolioContrato.Text.Length < 9 && e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Folio de contrato no válida.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                cargado = false;
                BuscarContratoFolio(txtFolioContrato.Text);
                cargado = true;
            }
        }

        private void BuscarContratoFolio(string folioContrato)
        {
            contexto.BuscarContratoFolio(folioContrato);
            if(contexto.ObjContratoData == null)
            {
                MessageBox.Show("No se encontro ningún contrato con el folio ingresado..", "Advertencia",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                SetDataContrato();
            }
        }

        private void SetDataContrato()
        {
            //set socios
            LlenarCbxSocios(contexto.ObjContratoData.ClaveCliente);
            if (contexto.ObjContratoData.SocioId != null)
            {
                cbxSocios.SelectedValue = contexto.ObjContratoData.SocioId;
            }
            else
            {
                cbxSocios.SelectedIndex = -1;
            }

            txtFolioContrato.Text = contexto.ObjContratoData.NoReferencia;

            contexto.ObjCliente = contexto.BuscarClientePorClave(contexto.ObjContratoData.ClaveCliente);
            LlenarCbxSocios(contexto.ObjCliente.Clave);
            LLenarCbxDirecciones();

            txtClaveCliente.Text = contexto.ObjCliente.Clave;
            txtNombreCliente.Text = contexto.ObjCliente.Cliente;
            cbxZona.SelectedValue = contexto.ObjContratoData.ZonaId;

            //lotes
            List<string>lstLotes = contexto.ObjContratoData.LotesRelacionados.Split(',').ToList();
            contexto.LstLotesSeleccionados = contexto.ObtenerLotesPorClave(contexto.ObjContratoData.ZonaId, lstLotes);
            LlenarListBox();

            txtCNte.Text = contexto.ObjContratoData.ColindaNorte;
            txtCSur.Text = contexto.ObjContratoData.ColindaSur;
            txtCOeste.Text = contexto.ObjContratoData.ColindaOeste;
            txtCEste.Text = contexto.ObjContratoData.ColindaEste;
            txtMNte.Text = contexto.ObjContratoData.MideNorte.ToString("N2");
            txtMSur.Text = contexto.ObjContratoData.MideSur.ToString("N2");
            txtMEste.Text = contexto.ObjContratoData.MideEste.ToString("N2");
            txtMOeste.Text = contexto.ObjContratoData.MideOeste.ToString("N2");

            cbxDomicilio.SelectedValue = contexto.ObjContratoData.DomicilioClienteId;

            if (contexto.ObjContratoData.SocioId != null)
            {
                cbxSocios.SelectedValue = contexto.ObjContratoData.SocioId;
            }            

            //txtPrecio.Text = contexto.ObjContratoData.PrecioLote.ToString("N2");
            txtNoPagos.Text = contexto.ObjContratoData.NoPagos.ToString("N0");
            txtDiaPago.Text = contexto.ObjContratoData.DiaPago.ToString("N0");

            txtPagoInicial.Text = contexto.ObjContratoData.PagoInicial.ToString("N2");
            txtPagosGracia.Text = contexto.ObjContratoData.NoPagosGracia.ToString("N0");


            if (contexto.ObjContratoData.EstadoId == (int)Enumeraciones.EstadosProcesoContratos.ATRASADO)
            {
                txtMontoGracia.Text = Convert.ToDecimal(contexto.ObjContratoData.MontoGracia).ToString("N2");
            }

            cbxEstado.SelectedValue = contexto.ObjContratoData.EstadoId;

            if(contexto.ObjContratoData.EstadoId == (int)Enumeraciones.EstadosProcesoContratos.REUBICADO)
            {
                txtContratoReubidado.Text = contexto.ObjContratoData.FolioContratoOrigen;
                
            }

            txtObservacion.Text = contexto.ObjContratoData.Observacion;
            txtFechaEmision.Text = contexto.ObjContratoData.FechaEmision.ToString("dd/MM/yyyy HH:mm:ss");
            txtRealizo.Text = contexto.ObjContratoData.UsuarioOperacionNombre;
            txtFechaReimpresion.Text = contexto.ObjContratoData.FechaReimpresion != null ? 
            Convert.ToDateTime(contexto.ObjContratoData.FechaReimpresion).ToString("dd/MM/yyyy HH:mm:ss") : "";

            if(contexto.ObjContratoData.EstadoId == (int)Enumeraciones.EstadosProcesoContratos.ATRASADO)
            {
                txtMontoGracia.Text = Convert.ToDecimal(contexto.ObjContratoData.MontoGracia).ToString("N2");
            }
            
            
            


        }

        private void txtClaveCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtClaveCliente.Text.Length < 5 && e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Clave de cliente no válida.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                BuscarClientePorIdentificador(txtClaveCliente.Text);
            }
        }

        private void BuscarClientePorIdentificador(string claveCliente)
        {
            contexto.ObjCliente = contexto.BuscarClientePorClave(claveCliente);
            if (contexto.ObjCliente == null)
            {
                MessageBox.Show("No se encontro ningún cliente con la clave ingresada.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetDataCliente();
        }          

        private void cbxZona_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!cargado) return;
            contexto.ObjLote = null;
            ListarLotesLibresXZona((int)cbxZona.SelectedValue, (int)Enumeraciones.EstadosProcesoLote.LIBRE);

        }

        private void ListarLotesLibresXZona(int zonaId, int estadoId)
        {
            lotescargados = false;
            contexto.LstLotes = contexto.ListarLotesXZonaIdEstadoId(zonaId, estadoId);
            cbxLotes.DataSource = contexto.LstLotes;
            cbxLotes.DisplayMember = "Identificador";
            cbxLotes.ValueMember = "Id";
            cbxLotes.SelectedIndex = -1;
            lotescargados = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string[] msjErr = null;
                string[] msjSuccess = new string[2];
                //validaciones
                if (contexto.ObjCliente == null)
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha seleccionado ningún cliente.";
                }
                else if (cbxDomicilio.SelectedIndex == -1)
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha seleccionado ningún domicilio asociado al cliente.";
                }
                else if (contexto.LstLotesSeleccionados==null || contexto.LstLotesSeleccionados.Count<=0)
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha seleccionado ningún lote.";
                }
                else if (string.IsNullOrEmpty(txtCNte.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido la colindancia al Norte.";
                }
                else if (string.IsNullOrEmpty(txtCSur.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido la colindancia al Sur.";
                }
                else if (string.IsNullOrEmpty(txtCEste.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido la colindancia al Este.";
                }
                else if (string.IsNullOrEmpty(txtCOeste.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido la colindancia al Oeste.";
                }
                else if (string.IsNullOrEmpty(txtMNte.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido la medida al Norte.";
                }
                else if (string.IsNullOrEmpty(txtMSur.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido la medida al Sur.";
                }
                else if (string.IsNullOrEmpty(txtMEste.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido la medida al Este.";
                }
                else if (string.IsNullOrEmpty(txtMOeste.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido la medida al Oeste.";
                }
                else if (string.IsNullOrEmpty(txtNoPagos.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido el No. de Pagos.";
                }
                else if (string.IsNullOrEmpty(txtDiaPago.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha definido un día de Pago.";
                }
                else if (string.IsNullOrEmpty(txtPagoInicial.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha ingresado el pago inicial.";
                }
                else if (string.IsNullOrEmpty(txtPagosGracia.Text))
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha ingresado el Número de pagos extendidos.";
                }
                else if (contexto.ObjContratoData != null &&cbxEstado.SelectedIndex==-1)
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha seleccionado el estado del contrato.";
                }


                if (msjErr != null && msjErr.Length > 0)
                {
                    MessageBox.Show(msjErr[1],
                        msjErr[0],
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }           


                if (contexto.ObjContratoData == null)
                {
                    //insert
                    contexto.InstanciarContrato();
                    contexto.ObjContrato.Folio = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.CONTRATO);
                    contexto.ObjContrato.FechaArrendamiento = Global.FechaServidor();
                    contexto.ObjContrato.ESTADOId = (int)Enumeraciones.EstadosProcesoContratos.VIGENTE;                   
                    contexto.ObjContrato.PagoInicial = Convert.ToDecimal(txtPagoInicial.Text);
                    contexto.ObjContrato.NoPagosGracia = Convert.ToInt32(txtPagosGracia.Text);


                    nuevoContrato = true;
                    msjSuccess[0] = "Se ha generado el contrato " + contexto.ObjContrato.Folio;
                }
                else
                {
                    contexto.ObjContrato.ESTADOId = (int)cbxEstado.SelectedValue;

                    if (string.IsNullOrEmpty(msjSuccess[0]))
                    {
                        msjSuccess[0] = "Se han modificado los datos del contrato " + contexto.ObjContrato.Folio + " correctamente.";
                    }

                    if(contexto.ObjContrato.ESTADOId == (int)Enumeraciones.EstadosProcesoContratos.ATRASADO)
                    {
                        contexto.ObjContrato.MontoGracia = contexto.ObjMontoGraciaData.MontoGracia;
                    }
                    
                }

                contexto.ObjContrato.CLIENTEId = contexto.ObjCliente.Id;
                if(cbxSocios.SelectedValue != null)
                {
                    contexto.ObjContrato.SOCIOSId = (int)cbxSocios.SelectedValue;
                }

                contexto.ObjContrato.PrecioInicial = Convert.ToDecimal(txtPrecio.Text);
                contexto.ObjContrato.DiaPago = Convert.ToInt32(txtDiaPago.Text);
                contexto.ObjContrato.NoPagos = Convert.ToInt32(txtNoPagos.Text);
                contexto.ObjContrato.USUARIOOperacionId = Global.ObjUsuario.Id;
                contexto.ObjContrato.Observacion = txtObservacion.Text;

                contexto.ObjContrato.ColindaNorte = txtCNte.Text;
                contexto.ObjContrato.ColindaSur = txtCSur.Text;
                contexto.ObjContrato.ColindaEste = txtCEste.Text;
                contexto.ObjContrato.ColindaOeste = txtCOeste.Text;

                contexto.ObjContrato.MideNorte = Convert.ToDecimal(txtMNte.Text);
                contexto.ObjContrato.MideSur = Convert.ToDecimal(txtMSur.Text);
                contexto.ObjContrato.MideEste = Convert.ToDecimal(txtMEste.Text);
                contexto.ObjContrato.MideOeste = Convert.ToDecimal(txtMOeste.Text);

                contexto.ObjContrato.AGENDAId = (int)cbxDomicilio.SelectedValue;

                contexto.Guardar();  

                if (nuevoContrato)
                {
                    //asignarlote
                    if(Global.RelacionarLotesContrato(contexto.ObjContrato.Id, contexto.LstLotesSeleccionados, (int)Enumeraciones.EstadosProcesoContratos.VIGENTE, (int)Enumeraciones.EstadosProcesoLote.ASIGNADO))
                    {
                        //crear pagoinicial en pago
                        contexto.InstanciarPago();
                        contexto.ObjPago.Folio = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.PAGO);
                        contexto.ObjPago.Monto = contexto.ObjContrato.PagoInicial;
                        contexto.ObjPago.FechaEmision = Global.FechaServidor();
                        contexto.ObjPago.ContratoId = contexto.ObjContrato.Id;
                        contexto.ObjPago.USUARIORecibeId = Global.ObjUsuario.Id;
                        contexto.ObjPago.NoPago = 1;
                        contexto.ObjPago.PagoOrdinario = true;
                        contexto.GuardarPago();

                        msjSuccess[0] += " Se ha generado el pago inicial folio de seguimiento " + contexto.ObjPago.Folio + ".";

                    }
                    else
                    {
                        msjErr = new string[2];
                        msjErr[1] = "Advertencia";
                        msjErr[0] += " Error al tratar de asignar los lotes. Vuelva a cargar el contrato y verifique que esten los lotes que selecciono. Contrato con folio: "+contexto.ObjContrato.Folio;
                    }



                }


                if(msjErr != null && msjErr.Length > 0)
                {
                    MessageBox.Show(
                                       msjErr[0],
                                       msjErr[1],
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Warning
                                       );
                    return;
                }

                msjSuccess[1] = "Aviso";
                MessageBox.Show(
                    msjSuccess[0],
                    msjSuccess[1],
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
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

        private void CalcularMontoGracia(string folioContrato)
        {
           contexto.CalcularMontoGracia(folioContrato);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void formContrato_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void cbxEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!cargado) return;
            if (contexto.ObjContratoData == null) return;

            if((int)cbxEstado.SelectedValue == (int)Enumeraciones.EstadosProcesoContratos.REUBICADO)
            {
                txtContratoReubidado.Enabled = true;
                btnBusContratoReubicado.Enabled = true;
            }
            else
            {
                txtContratoReubidado.Enabled = false;
                btnBusContratoReubicado.Enabled = false;

                KeyValuePair<int?, string> objValidacion;
                objValidacion = Global.CambiarEstadoContrato(contexto.ObjContratoData, (int)cbxEstado.SelectedValue, txtObservacion.Text);
                if (objValidacion.Key != null)
                {
                    MessageBox.Show(objValidacion.Value, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InicializarModulo();
                }else if(objValidacion.Key==null && objValidacion.Value != null)
                {
                    MessageBox.Show(objValidacion.Value, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (contexto.ObjContratoData == null)
            {
                MessageBox.Show(
                  "No se ha cargado ningún registro. Intentelo nuevamente.",
                  "Advertencia",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
                return;
            }

            if(cbxVendedores.SelectedIndex == -1)
            {
                MessageBox.Show(
                  "No se ha seleccionado el vendedor para generar el contrato.",
                  "Advertencia",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
                return;
            }

            GenerarReporte();




        }

        private void GenerarReporte()
        {
            contexto.ObtenerDatosContratoImpreso(contexto.ObjContratoData.NoReferencia);
            DateTime _FechaReimpresion = Global.FechaServidor();
            clsDatosJaade ObjDatosJaade = JsonConvert.DeserializeObject<clsDatosJaade>(Global.DevulveVariableGlobal(Enumeraciones.VariablesGlobales.DomicilioJaade));
            clsFormatoFechaEscrito fechaData = Global.ObtenerFechaEscrita(contexto.ObjContratoImpresoData.FechaEmision);
            string[] items = contexto.ObjContratoImpresoData.NoLotesRelacionados.Split(',');
            bool multiple = false;
            string parteLotes = "";

            if (items.Length == 1)
            {
                multiple = false;
                parteLotes = items[0];
            }else if(items.Length == 2)
            {
                multiple = true;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items.Length - 2 == i)
                    {
                        parteLotes += items[i] + " y";
                    }
                    else
                    {
                        parteLotes += items[i];
                    }                   
                }
            }
            else
            {
                multiple = true;

                for (int i = 0; i < items.Length; i++)
                {
                    if(items.Length -2 == i)
                    {
                        parteLotes += items[i] + " y";
                    }else if (items.Length - 1 == i)
                    {
                        parteLotes += items[i];
                    }
                    else
                    {
                        parteLotes += items[i] + ", ";
                    }                    
                }
            }
            

            string fragmentoLotes = (multiple ? "lotes " : "lote ") +parteLotes;

            var repContrato = new REPORTES.repContratoLote();
            var cultureInfo = new System.Globalization.CultureInfo("es-MX");
            
            repContrato.InstanciarListaParametros();
            //llenar parametros
            repContrato.parametros.Add(new ReportParameter("NoReferencia", contexto.ObjContratoImpresoData.NumeroReferencia ));
            repContrato.parametros.Add(new ReportParameter("LotesRelacionados", fragmentoLotes ));
            repContrato.parametros.Add(new ReportParameter("NombreCliente", contexto.ObjContratoImpresoData.NombreCliente.ToUpper()));
            repContrato.parametros.Add(new ReportParameter("DiaPago", contexto.ObjContratoImpresoData.DiaPago.ToString("N0")));
            repContrato.parametros.Add(new ReportParameter("PagoInicial", string.Format(cultureInfo, "{0:C}", contexto.ObjContratoImpresoData.PagoInicial) ));
            repContrato.parametros.Add(new ReportParameter("NoPagos", contexto.ObjContratoImpresoData.NoPagos.ToString("N0") ));
            repContrato.parametros.Add(new ReportParameter("NoPagosExtendido", contexto.ObjContratoImpresoData.NoPagosExtendido.ToString("N0") ));
            repContrato.parametros.Add(new ReportParameter("ColindaNorte", contexto.ObjContratoImpresoData.ColindaNorte ));
            repContrato.parametros.Add(new ReportParameter("ColindaSur", contexto.ObjContratoImpresoData.ColindaEste ));
            repContrato.parametros.Add(new ReportParameter("ColindaEste", contexto.ObjContratoImpresoData.ColindaEste));
            repContrato.parametros.Add(new ReportParameter("ColindaOeste", contexto.ObjContratoImpresoData.ColindaOeste));
            repContrato.parametros.Add(new ReportParameter("MideNorte", contexto.ObjContratoImpresoData.MideNorte.ToString("N2") ));
            repContrato.parametros.Add(new ReportParameter("MideSur", contexto.ObjContratoImpresoData.MideSur.ToString("N2") ));
            repContrato.parametros.Add(new ReportParameter("MideEste", contexto.ObjContratoImpresoData.MideEste.ToString("N2") ));
            repContrato.parametros.Add(new ReportParameter("MideOeste", contexto.ObjContratoImpresoData.MideOeste.ToString("N2") ));
            repContrato.parametros.Add(new ReportParameter("DomicilioCliente", contexto.ObjContratoImpresoData.DomicilioCliente));
            repContrato.parametros.Add(new ReportParameter("Hora", fechaData.Hora.ToUpper()));
            repContrato.parametros.Add(new ReportParameter("Minuto", fechaData.Minuto.ToUpper()));
            repContrato.parametros.Add(new ReportParameter("Dia",fechaData.Dia.ToUpper()));
            repContrato.parametros.Add(new ReportParameter("Mes", fechaData.Mes.ToUpper()));
            repContrato.parametros.Add(new ReportParameter("Anio", fechaData.Anio.ToUpper()));
            repContrato.parametros.Add(new ReportParameter("CalleJaade", ObjDatosJaade.Calle));
            repContrato.parametros.Add(new ReportParameter("NoExtJaade", ObjDatosJaade.NoExt));
            repContrato.parametros.Add(new ReportParameter("LocalidadJaade", ObjDatosJaade.Localidad));
            repContrato.parametros.Add(new ReportParameter("MunicipioJaade", ObjDatosJaade.Municipio));
            repContrato.parametros.Add(new ReportParameter("EstadoJaade", ObjDatosJaade.Estado));
            repContrato.parametros.Add(new ReportParameter("TitularVentaJaade", LstVendedores[cbxVendedores.SelectedIndex].Key));//ALEJANDRA G MORO GARCIA
            repContrato.parametros.Add(new ReportParameter("PagoInicialLetra", Global.ConvertirNumeroALetras((int)Math.Round(contexto.ObjContratoImpresoData.PagoInicial)).ToUpperInvariant() ));
            repContrato.parametros.Add(new ReportParameter("MontoMensualidad", string.Format(cultureInfo, "{0:C}",  Global.CalcularMontoMensualidadContratoVigente(contexto.ObjContratoImpresoData.NoPagos, contexto.ObjContratoImpresoData.PagoInicial, contexto.ObjContratoImpresoData.TotalContrato)) ));
            repContrato.parametros.Add(new ReportParameter("MontoMensualidadLetra", Global.ConvertirNumeroALetras(Global.CalcularMontoMensualidadContratoVigente(contexto.ObjContratoImpresoData.NoPagos, contexto.ObjContratoImpresoData.PagoInicial, contexto.ObjContratoImpresoData.TotalContrato)).ToUpperInvariant()));
            repContrato.parametros.Add(new ReportParameter("TotalContrato", string.Format(cultureInfo, "{0:C}", contexto.ObjContratoImpresoData.TotalContrato) ));
            repContrato.parametros.Add(new ReportParameter("TotalContratoLetra", Global.ConvertirNumeroALetras((int)Math.Round(contexto.ObjContratoImpresoData.TotalContrato)).ToUpperInvariant()));
            repContrato.parametros.Add(new ReportParameter("UbicacionZona", contexto.ObjContratoImpresoData.UbicacionZona));
            repContrato.parametros.Add(new ReportParameter("NoManzana", contexto.ObjContratoImpresoData.NoManzana!=null? (" de la manzana "+ contexto.ObjContratoImpresoData.NoManzana):""));

            repContrato.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstLotesInvolucrados.DataSource == null) return;

            contexto.QuitarLoteSeleccionado((int)lstLotesInvolucrados.SelectedValue);
            LlenarListBox();
        }

        private void cbxLotes_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!lotescargados) return;

            contexto.AgregarLoteSeleccionado((int)cbxLotes.SelectedValue);
            LlenarListBox();
        }

        private void LlenarListBox()
        {
            lstLotesInvolucrados.DataSource = null;
            lstLotesInvolucrados.DataSource = contexto.LstLotesSeleccionados;
            lstLotesInvolucrados.DisplayMember = "Identificador";
            lstLotesInvolucrados.ValueMember = "Id";

            CalcularSumaLotesSeleccionados();
        }

        private void CalcularSumaLotesSeleccionados()
        {
            if (contexto.LstLotesSeleccionados.Count <= 0) txtPrecio.Text = @"0.00";
            txtPrecio.Text = contexto.LstLotesSeleccionados.Sum(x=>x.Precio).ToString("N2");
        }

        private void btnReubicar_Click(object sender, EventArgs e)
        {

        }
    }
}
