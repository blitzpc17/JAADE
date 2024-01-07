using CAPALOGICA.LOGICAS.PAGOS;
using CAPALOGICA.LOGICAS.SISTEMA;
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
    public partial class formAsignacionLotes : Form
    {
        private int ZonaSeleccionadaId = -1;
        private bool Cargado = false;
        private formAsignacionLotesLogica contexto;
        private int? ClienteIdSeleccionado = null, LoteSeleccionado = null;


        public formAsignacionLotes()
        {
            InitializeComponent();
        }

        private void InicializarModulo()
        {
            InicializarContextos();
            LimpiarControles(); 
            ListarCatalogos();
            Cargado = true;            
        }

        private void InicializarContextos()
        {
            contexto = new formAsignacionLotesLogica();
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            txtFechaRegistro.Text = Global.FechaServidor().ToString("dd-MM-yyyy HH:mm:ss");
            dgvRegistros.DataSource = null;
            tsTotalRegistros.Text = @"0";
            Cargado = false;
            ZonaSeleccionadaId = -1;
            ClienteIdSeleccionado = null;
            LoteSeleccionado = null;
        }

        private void ListarCatalogos()
        {
            ListarZonas();
        }
        private void ListarZonas()
        {
            contexto.ListarZonas();
            cbxZona.DataSource = contexto.LstZonas;
            cbxZona.DisplayMember = "Nombre";
            cbxZona.ValueMember = "Id";
            cbxZona.SelectedIndex = -1;
        }

        private void Apariencias()
        {
            dgvRegistros.DataSource = contexto.LstClienteLotesAux;
            if (dgvRegistros.DataSource == null) return;
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[0].Frozen = true;
            dgvRegistros.Columns[1].Visible = false;
            dgvRegistros.Columns[2].Visible = false;
            dgvRegistros.Columns[3].HeaderText = "CÓDIGO LOTE";
            dgvRegistros.Columns[3].Width = 100;
            dgvRegistros.Columns[4].HeaderText = "ZONA NOMBRE";
            dgvRegistros.Columns[4].Width = 180;
            dgvRegistros.Columns[5].Visible = false;
            dgvRegistros.Columns[6].HeaderText = "FECHA ASIGNACIÓN";
            dgvRegistros.Columns[6].Width = 110;
            dgvRegistros.Columns[7].HeaderText = "MANZANA";
            dgvRegistros.Columns[7].Width = 80;
            dgvRegistros.Columns[8].HeaderText = "PRECIO LOTE";
            dgvRegistros.Columns[8].Width = 180;
            dgvRegistros.Columns[8].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[9].HeaderText = "PAGO INICIAL";
            dgvRegistros.Columns[9].Width = 180;
            dgvRegistros.Columns[9].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[10].HeaderText = "NO. PAGOS";
            dgvRegistros.Columns[10].Width = 180;
            dgvRegistros.Columns[10].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[11].HeaderText = "MONTO RESTANTE";
            dgvRegistros.Columns[11].Width = 180;
            dgvRegistros.Columns[11].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

           


            tsTotalRegistros.Text = contexto.LstClienteLotesAux.Count.ToString("N0");

        }

        private void Guardar()
        {
            try
            {
                if (contexto.ObjLoteSeleccionado == null ||
                ClienteIdSeleccionado == null ||
                string.IsNullOrEmpty(txtMontoRestante.Text) ||
                string.IsNullOrEmpty(txtNoPagos.Text)

                )
                {
                    MessageBox.Show("Faltan datos acompletar para porder realizar la ASIGNACIÓN DEL LOTE AL CLIENTE", "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                if (contexto.ObjClienteLote == null)
                {
                    contexto.InstanciarAsignacionLote();
                }
                contexto.ObjClienteLote.CLIENTEId = (int)ClienteIdSeleccionado;
                contexto.ObjClienteLote.LOTEId = contexto.ObjLoteSeleccionado.Id;
                contexto.ObjClienteLote.PagoInicial = Convert.ToDecimal(txtPagoInicial.Text);
                contexto.ObjClienteLote.FechaRegistro = Global.FechaServidor();
                contexto.ObjClienteLote.NoPagos = Convert.ToInt32(txtNoPagos.Text);
                contexto.ObjClienteLote.MontoRestante = Convert.ToDecimal(txtMontoRestante.Text);
                contexto.ObjClienteLote.USUARIOId = Global.ObjUsuario.Id;


                contexto.Guardar();
                MessageBox.Show("Asignación realizada correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void CalcularMontoRestante()
        {
            try
            {
                decimal  _precioLote = contexto.ObjLoteSeleccionado.Precio;
                decimal _montoInicial = Convert.ToDecimal(txtPagoInicial.Text);

                decimal _montoRestante = 0;

                _montoRestante = _precioLote - _montoInicial;

                txtMontoRestante.Text = _montoRestante.ToString("N2"); 

            }catch(Exception ex)
            {
                MessageBox.Show(
                    "Se ha generado un error al intentar realizar la operación actual. Cierre el módulo e intente nuevamente, si el problema persiste comuniquese con su proveedor de servicio.",
                    "Error en la operación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void filtrar(int column, string termino)
        {
            if (contexto.Filtrar(column, termino))
            {
                dgvRegistros.Rows[contexto.index].Cells[column].Selected = true;
                dgvRegistros.FirstDisplayedScrollingRowIndex = contexto.index;
            }
        }

        private void ordenar(int column)
        {
            contexto.Ordenar(column);
            dgvRegistros.DataSource = contexto.LstClienteLotesAux;
            Apariencias();
        }


        private void SetDataLote(int idLote) 
        {
            Cargado = false;
            contexto.ObjClienteLoteSeleccionado = contexto.ObtenerAsignacionesLotes((int)ClienteIdSeleccionado, idLote);
            ClienteIdSeleccionado = contexto.ObjClienteLoteSeleccionado.ClienteId;
            txtCliente.Text = contexto.ObjClienteLoteSeleccionado.Cliente;
            txtFechaRegistro.Text = contexto.ObjClienteLoteSeleccionado.FechaAsignacion.ToString("dd-MM-yyyy HH:mm:ss");
            txtNoPagos.Text = contexto.ObjClienteLoteSeleccionado.NoPagos.ToString("N0");
            txtPagoInicial.Text = contexto.ObjClienteLoteSeleccionado.PagoInicial.ToString("N2");
            txtPrecioLote.Text = contexto.ObjClienteLoteSeleccionado.PrecioLote.ToString("N2");
            LoteSeleccionado = contexto.ObjClienteLoteSeleccionado.LoteId;
            contexto.ObjLoteSeleccionado = contexto.ObtenerLote((int)LoteSeleccionado);
            txtLote.Text = contexto.ObjLoteSeleccionado.Identificador;
            CalcularMontoRestante();
            cbxZona.SelectedValue = contexto.ObjClienteLoteSeleccionado.ZonaId;
            ObtenerClienteData(contexto.ObjClienteLoteSeleccionado.ClienteId);
            Cargado = true;
            
        }

        private void ObtenerClienteData(int ClienteId)
        {
            contexto.ObjClienteSeleccionado = contexto.ObtenerClienteData(ClienteId);
        }

        private void btnModulo_Click(object sender, EventArgs e)
        {
            try
            {
                var busquedaCliente = new busClientes();
                busquedaCliente.ShowDialog();

                if (busquedaCliente.ObjEntidad == null)
                {
                    MessageBox.Show("No selecciono el CLIENTE", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClienteIdSeleccionado = busquedaCliente.ObjEntidad.Id;
                txtCliente.Text = busquedaCliente.ObjEntidad.Cliente;
                ListarLotesZonaCliente();
                Apariencias();
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar cargar los registros. Intentelo nuevamente.", 
                    "Error en la operación", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }

        }

        private void ListarLotesZonaCliente()
        {
            contexto.LstClienteLotes = contexto.ListarAsignacionesCliente((int)ClienteIdSeleccionado);
            contexto.LstClienteLotesAux = contexto.LstClienteLotes;
        }

        private void formAsignacionLotes_Load(object sender, EventArgs e)
        {
            InicializarModulo();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void formAsignacionLotes_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvRegistros.Rows.Count <= 0) return;

            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                contexto.index = -1;
                return;
            }

            filtrar(contexto.Column, txtBuscar.Text);
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            if (contexto.Column == e.ColumnIndex) return;
            contexto.Column = e.ColumnIndex;
            ordenar(contexto.Column);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            SetDataLote((int)dgvRegistros.CurrentRow.Cells[2].Value);
        }

        private void btnLotes_Click(object sender, EventArgs e)
        {
            if (cbxZona.SelectedIndex == -1||ZonaSeleccionadaId == -1)
            {
                MessageBox.Show("No se ha seleccionado la ZONA.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var busLotes = new busLotesZona(ZonaSeleccionadaId);   
            busLotes.ShowDialog();

            if (busLotes.ObjEntidad == null)
            {
                MessageBox.Show("No se selecciono ningún LOTE.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            contexto.ObjLoteSeleccionado = busLotes.ObjEntidad;
            LoteSeleccionado = contexto.ObjLoteSeleccionado.Id;            
            txtLote.Text = contexto.ObjLoteSeleccionado.Identificador;
            txtPrecioLote.Text = contexto.ObjLoteSeleccionado.Precio.ToString("N2");

        }

        private void txtPagoInicial_TextChanged(object sender, EventArgs e)
        {
            if (!Cargado) return;
            if (string.IsNullOrEmpty(txtPagoInicial.Text)) return;
            if (ClienteIdSeleccionado == null)
            {
                MessageBox.Show("No ha seleccionado ningún CLIENTE.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (LoteSeleccionado == null)
            {
                MessageBox.Show("No ha seleccionado ningún LOTE.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Global.EsValorDecimal(txtPrecioLote.Text))
            {
                MessageBox.Show("Verifique el valor del campo *PRECIO LOTE*, tiene un valor inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Global.EsValorDecimal(txtPagoInicial.Text))
            {
                MessageBox.Show("Verifique el valor del campo *PAGO INICIAL*, tiene un valor inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CalcularMontoRestante();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (contexto.ObjClienteLoteSeleccionado == null)
            {
                MessageBox.Show("No ha seleccionado ningún lote.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime _FechaHoraContrato = Global.FechaServidor();
            clsDatosJaade ObjDatosJaade = JsonConvert.DeserializeObject<clsDatosJaade>(Global.DevulveVariableGlobal(Enumeraciones.VariablesGlobales.DomicilioJaade));

            var repContrato = new REPORTES.repContratoLote();

            repContrato.InstanciarListaParametros();
            //llenar parametros
            repContrato.parametros.Add(new ReportParameter("HORA", _FechaHoraContrato.ToString("HH:mm:ss")));
            repContrato.parametros.Add(new ReportParameter("DIA", _FechaHoraContrato.Day.ToString()));
            repContrato.parametros.Add(new ReportParameter("MES", Global.MesALetra(_FechaHoraContrato.Month)));
            repContrato.parametros.Add(new ReportParameter("ANIO", Global.DigitoAnio(_FechaHoraContrato))) ;
            repContrato.parametros.Add(new ReportParameter("CLIENTE", contexto.ObjClienteLoteSeleccionado.Cliente));
            repContrato.parametros.Add(new ReportParameter("CALLEJAADE", ObjDatosJaade.Calle));
            repContrato.parametros.Add(new ReportParameter("NOEXTJAADE", ObjDatosJaade.NoExt));
            repContrato.parametros.Add(new ReportParameter("NOINTJAADE", ObjDatosJaade.NoInt));
            repContrato.parametros.Add(new ReportParameter("COLONIAJAADE", ObjDatosJaade.Colonia));
            repContrato.parametros.Add(new ReportParameter("LOCALIDADJAADE", ObjDatosJaade.Localidad));
            repContrato.parametros.Add(new ReportParameter("MUNICIPIOJAADE", ObjDatosJaade.Municipio));
            repContrato.parametros.Add(new ReportParameter("ESTADOJAADE", ObjDatosJaade.Estado));
            repContrato.parametros.Add(new ReportParameter("TELEFONOJAADE", ObjDatosJaade.Telefono));
            repContrato.parametros.Add(new ReportParameter("DOMICILIOCLIENTE",Global.ArmarDomicilioCliente(contexto.ObjClienteSeleccionado) ));
            repContrato.parametros.Add(new ReportParameter("CODIGOLOTE", contexto.ObjClienteLoteSeleccionado.CodigoLote));
            repContrato.parametros.Add(new ReportParameter("MANZANA", contexto.ObjClienteLoteSeleccionado.Manzana==null?"S/M": contexto.ObjClienteLoteSeleccionado.Manzana.ToString()));
            repContrato.parametros.Add(new ReportParameter("ZONA", contexto.ObjClienteLoteSeleccionado.ZonaNombre));
            repContrato.parametros.Add(new ReportParameter("MNORTE", contexto.ObjLoteSeleccionado.MNorte.ToString("N2") ));
            repContrato.parametros.Add(new ReportParameter("MSUR", contexto.ObjLoteSeleccionado.MSur.ToString("N2") ));
            repContrato.parametros.Add(new ReportParameter("MESTE", contexto.ObjLoteSeleccionado.MEste.ToString("N2")));
            repContrato.parametros.Add(new ReportParameter("MOESTE", contexto.ObjLoteSeleccionado.MOeste.ToString("N2")));
            repContrato.parametros.Add(new ReportParameter("CNORTE", contexto.ObjLoteSeleccionado.CNorte ));
            repContrato.parametros.Add(new ReportParameter("CSUR", contexto.ObjLoteSeleccionado.CSur));
            repContrato.parametros.Add(new ReportParameter("CESTE", contexto.ObjLoteSeleccionado.CEste));
            repContrato.parametros.Add(new ReportParameter("COESTE", contexto.ObjLoteSeleccionado.COeste));
            repContrato.parametros.Add(new ReportParameter("TITULARVENTAJAADE", ObjDatosJaade.TitularVenta ));
            repContrato.parametros.Add(new ReportParameter("NOMBRECLIENTE", contexto.ObjClienteLoteSeleccionado.Cliente));
            repContrato.parametros.Add(new ReportParameter("PRECIOLOTE", contexto.ObjClienteLoteSeleccionado.PrecioLote.ToString("N2")));
            repContrato.parametros.Add(new ReportParameter("PAGOINICIAL", contexto.ObjClienteLoteSeleccionado.PagoInicial.ToString("N2")));
            repContrato.parametros.Add(new ReportParameter("NOPAGOS", contexto.ObjClienteLoteSeleccionado.NoPagos.ToString("N0")));
            repContrato.parametros.Add(new ReportParameter("DIAPAGO", _FechaHoraContrato.ToString("dd")));
            decimal _MontoRestante = Global.CalcularPagoMensualRestante(contexto.ObjClienteLoteSeleccionado.MontoRestante, contexto.ObjClienteLoteSeleccionado.NoPagos);
            repContrato.parametros.Add(new ReportParameter("MONTOPAGOMENSUAL", _MontoRestante.ToString("N2")));


            repContrato.ShowDialog();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            SetDataLote((int)dgvRegistros.CurrentRow.Cells[2].Value);
        }

        private void cbxZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Cargado) return;
            ZonaSeleccionadaId = (int)cbxZona.SelectedValue;

        }
    }
}
