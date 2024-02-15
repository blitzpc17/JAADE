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
    public partial class formContrato : Form
    {
        private bool cargado = false;
        private formContratoLogica contexto;
        private busClientes busCliente;
        private busLotesZona busLoteZona;
        private busContratos busContato;

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
                cargado = false;
                LimpiarControles();
                InstanciarContexto();
                ListarCatalogos();
                DatosModulo();
                txtFolioContrato.Text = @"NUEVO";
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

        private void DatosModulo()
        {
            txtFechaEmision.Text = Global.FechaServidor().ToString("dd/MM/yyyy HH:mm:ss");
            txtRealizo.Text = Global.ObtenerNombreUsuario(Global.ObjUsuario);
            txtFechaReimpresion.Text = (contexto.ObjContratoData == null 
                || contexto.ObjContratoData.FechaReImpresion==null) ? "" : Convert.ToDateTime(contexto.ObjContratoData.FechaReImpresion).ToString("dd/MM/yyyy HH:mm:ss");
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
                    txtFolioContrato.Text = contexto.ObjContratoData.Folio;
                }
            }
        }

        private void btnBuscarContrato_Click(object sender, EventArgs e)
        {/*
            bus = new busClientes();
            bus.ShowDialog();

            if (bus.ObjEntidad == null)
            {
                MessageBox.Show("No se ha seleccionado ningín registro.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            contexto.ObjClienteData = contexto.ObtenerDataCliente(bus.ObjEntidad.Id);
            contexto.ObjCliente = contexto.ObtenerCliente(bus.ObjEntidad.Id);
            contexto.ObjPersona = contexto.ObtenerPersona(contexto.ObjCliente.PERSONAId);
            SetDataCliente();*/
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
            if(contexto.ObjLote.ESTADOId == (int)Enumeraciones.EstadosProcesoLote.LIBRE)
            {
                SetDataLote();
            }
            else
            {
                MessageBox.Show("No se puede seleccionar el lote "+contexto.ObjLote.Identificador+", " +
                    "porque su estado es "+contexto.ObjLote.ESTADO.Nombre+".", "Advertencia",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                contexto.ObjLote = null;
                txtClaveLote.Clear();
                return;
            }
            

        }

        private void SetDataLote()
        {
            txtClaveLote.Text = contexto.ObjLote.Identificador;
            txtPrecio.Text = contexto.ObjLote.Precio.ToString("N2");
        }

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
                BuscarContratoFolio(txtFolioContrato.Text);
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
            contexto.ObjCliente = contexto.BuscarClientePorClave(contexto.ObjContratoData.ClaveCliente);
            contexto.ObjLote = contexto.BuscarLotePorClave(contexto.ObjContratoData.ClaveLote);

            txtFolioContrato.Text = contexto.ObjContratoData.Folio;
            txtClaveCliente.Text = contexto.ObjContratoData.ClaveCliente;
            txtNombreCliente.Text = contexto.ObjContratoData.ClienteNombre;
            cbxZona.SelectedValue = contexto.ObjContratoData.ZonaLoteId;
            cbxSocios.SelectedValue = contexto.ObjContratoData.SocioId;
            txtClaveLote.Text = contexto.ObjContratoData.ClaveLote;
            txtPrecio.Text = contexto.ObjContratoData.PrecioLote.ToString("N2");
            txtNoPagos.Text = contexto.ObjContratoData.NoPagos.ToString("N0");
            txtDiaPago.Text = contexto.ObjContratoData.DiaPago.ToString("N0");
            txtFechaEmision.Text = contexto.ObjContratoData.FechaEmision.ToString("dd/MM/yyyy HH:mm:ss");
            txtRealizo.Text = contexto.ObjContratoData.UsuarioRealizo;
            txtFechaReimpresion.Text = contexto.ObjContratoData.FechaReImpresion == null ? 
                Convert.ToDateTime(contexto.ObjContratoData.FechaReImpresion).ToString("dd/MM/yyyy HH:mm:ss") : "";


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

        private void BuscarLotePorIdentificador(string claveLote)
        {
            contexto.ObjLote = contexto.BuscarLotePorClave(claveLote);
            if (contexto.ObjLote == null)
            {
                MessageBox.Show("No se encontro ningún lote con la clave ingresada.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetDataLote();
            cbxZona.SelectedValue = contexto.ObjLote.ZONAId;
        }

        private void txtClaveLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtClaveLote.Text.Length < 7 && e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Clave de lote no válida.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                BuscarLotePorIdentificador(txtClaveLote.Text);
            }
        }

        private void cbxZona_SelectedValueChanged(object sender, EventArgs e)
        {
            contexto.ObjLote = null;
            txtClaveLote.Clear();
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
                else if (contexto.ObjLote == null)
                {
                    msjErr = new string[2];
                    msjErr[0] = "Advertencia";
                    msjErr[1] = "No ha seleccionado ningún lote.";
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
                    msjSuccess[0] = "Se ha generado el contrato " + contexto.ObjContrato.Folio;
                }
                else
                {
                    msjSuccess[0] = "Se ha modificado el contrato " + contexto.ObjContrato.Folio;
                }

                contexto.ObjContrato.CLIENTEId = contexto.ObjCliente.Id;
                contexto.ObjContrato.SOCIOSId = (int)cbxSocios.SelectedValue;
                contexto.ObjContrato.LOTEId = contexto.ObjLote.Id;
                contexto.ObjContrato.PrecioInicial = contexto.ObjLote.Precio;
                contexto.ObjContrato.DiaPago = Convert.ToInt32(txtDiaPago.Text);
                contexto.ObjContrato.NoPagos = Convert.ToInt32(txtNoPagos.Text);
                contexto.ObjContrato.USUARIOOperacionId = Global.ObjUsuario.Id;

                contexto.Guardar();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarModulo();
        }
    }
}
