using CAPADATOS;
using CAPADATOS.ADO.LOTES;
using CAPADATOS.Entidades;
using CAPALOGICA.LOGICAS.PAGOS;
using PRESENTACION.UTILERIAS;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION.PAGOS.Importaciones
{
    public partial class formImportacionLayouts : Form
    {
        private formImportacionLayoutsLogica contexto;
        private int row = 0;
        private List<string> lstMsj;
        List<KeyValuePair<string, int>> listaItems;
        bool cargado = false;

        public formImportacionLayouts()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            cargado = false;
            LimpiarControles();
            InstanciarContextos();
            cargado = true;
        }

        private void InstanciarContextos()
        {
            contexto = new formImportacionLayoutsLogica();
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            tsTotalRegistros.Text = @"0";
            tsTotalErrores.Text = @"0";
            ListarTipoExportacion();
            cbxTipo.SelectedIndex = -1;
        }

        private void ListarTipoExportacion()
        {
            listaItems = new List<KeyValuePair<string, int>>();
            listaItems.Add(new KeyValuePair<string, int>("CLIENTES", 1));
            listaItems.Add(new KeyValuePair<string, int>("ZONAS", 2));
            listaItems.Add(new KeyValuePair<string, int>("LOTES", 3));
            listaItems.Add(new KeyValuePair<string, int>("CONTRATOS", 3));
            listaItems.Add(new KeyValuePair<string, int>("PAGOS", 3));

            cbxTipo.DataSource = listaItems;
            cbxTipo.DisplayMember = "Key";
            cbxTipo.ValueMember = "Value";
        }

        private void ImportarLayout()
        {
            if (cbxTipo.SelectedIndex == -1)
            {
                MessageBox.Show("No se ha seleccionado el tipo de importación.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            openFileDialog1.Filter = "Archivos de Excel (*.xlsx, *.xls)|*.xlsx;*.xls";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImportarExcel(cbxTipo.SelectedIndex, openFileDialog1.FileName);
                return;
            }

            MessageBox.Show("No se selecciono ningún archivo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void ImportarExcel(int opc, string fileName)
        {
            try
            {
                switch (opc)
                {
                    case 0:
                        ImportarClientes(fileName);
                        break;
                    case 1:
                        ImportarZonas(fileName);
                        break;
                    case 2:
                        ImportarLotes(fileName);
                        break;
                    case 3:
                        ImportarContratos(fileName);
                        break;
                    case 4:
                        ImportarPagos(fileName);
                        break;
                }
                
            }
            catch (DbUpdateException ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar importar los registros. Ejecución pausada en el row:" + row,
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }

        private void GenerarLayout(int opc)
        {
            try
            {
                if (cbxTipo.SelectedIndex == -1)
                {
                    MessageBox.Show("No ha seleccionado el tipo de layout a generar.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                saveFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*";
                saveFileDialog1.Title = "Guardar archivo de Excel";
                saveFileDialog1.ShowDialog();
                string rutaArchivo = saveFileDialog1.FileName;
                if (string.IsNullOrEmpty(rutaArchivo))
                {
                    MessageBox.Show("No se selecciono ningún archivo para realizar la operación.",
                      "Advertencia",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Warning);
                    return;
                }
                switch (opc)
                {
                    case 0:
                        GenerarLayoutCliente(rutaArchivo);
                        break;
                    case 1:
                        GenerarLayoutZonas(rutaArchivo);
                        break;
                    case 2:
                        GenerarLayoutLotes( rutaArchivo);
                        break;
                    case 3:
                        GenerarLayoutContratos( rutaArchivo);
                        break;
                    case 4:
                        GenerarLayoutPagos(rutaArchivo);
                        break;
                }

                MessageBox.Show("Layout generado correctamente.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar generar el layout. ",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }

        private void GenerarLayoutPagos( string rutaArchivo)
        {
            using (SLDocument sl = new SLDocument())
            {
                sl.SetCellValue(1, 1, "CONTRATO");
                sl.SetCellValue(1, 2, "NOPAGO");
                sl.SetCellValue(1, 3, "MONTO");
                sl.SetCellValue(1, 4, "FECHA");
                sl.SetCellValue(1, 5, "OBSERVACION");

                sl.SaveAs(rutaArchivo);
            }
        }

        private void GenerarLayoutContratos(string rutaArchivo)
        {
            using (SLDocument sl = new SLDocument())
            {
                sl.SetCellValue(1, 1, "FECHA ARRENDAMIENTO");
                sl.SetCellValue(1, 2, "CLAVE CLIENTE");
                sl.SetCellValue(1, 3, "SOCIO");
                sl.SetCellValue(1, 4, "ZONA");
                sl.SetCellValue(1, 5, "IDENTIFICADOR LOTE");
                sl.SetCellValue(1, 6, "NO. PAGOS");
                sl.SetCellValue(1, 7, "PRECIO INICIAL");
                sl.SetCellValue(1, 8, "DIA PAGO");
                sl.SetCellValue(1, 9, "PAGO INICIAL");
                sl.SetCellValue(1, 10, "NO. PAGOS GRACIA");
                sl.SetCellValue(1, 11, "OBSERVACION");

                sl.SaveAs(rutaArchivo);
            }
        }

        private void GenerarLayoutLotes( string rutaArchivo)
        {
            using (SLDocument sl = new SLDocument())
            {
                sl.SetCellValue(1, 1, "ZONA");
                sl.SetCellValue(1, 2, "CANTIDAD");
                sl.SetCellValue(1, 3, "MIDE SUR");
                sl.SetCellValue(1, 4, "MIDE OESTE");
                sl.SetCellValue(1, 5, "MIDE ESTE");
                sl.SetCellValue(1, 6, "MIDE NORTE");
                sl.SetCellValue(1, 7, "COLINDA SUR");
                sl.SetCellValue(1, 8, "COLINDA OESTE");
                sl.SetCellValue(1, 9, "COLINDA ESTE");
                sl.SetCellValue(1, 10, "COLINDA NORTE");
                sl.SetCellValue(1, 11, "PRECIO");
                sl.SetCellValue(1, 12, "MANZANA");


                sl.SaveAs(rutaArchivo);
            }
        }

        private void GenerarLayoutZonas( string rutaArchivo)
        {
            using (SLDocument sl = new SLDocument())
            {
             
                sl.SetCellValue(1, 1, "nombre");
                sl.SetCellValue(1, 2, "manzana");
                sl.SetCellValue(1, 3, "lotes");
                sl.SetCellValue(1, 4, "direccion");


                sl.SaveAs(rutaArchivo);
            }
        }

        private void GenerarLayoutCliente( string rutaArchivo)
        {
            using (SLDocument sl = new SLDocument())
            {

                sl.SetCellValue(1, 1, "NOMBRE");
                sl.SetCellValue(1, 2, "SOCIO");
                sl.SetCellValue(1, 3, "TELEFONO");
                sl.SetCellValue(1, 4, "CORREO");
                sl.SetCellValue(1, 4, "DOMICILIO");

                sl.SaveAs(rutaArchivo);
            }
        }

        private void ImportarPagos(string fileName)
        {
            contexto.InstanciarListaPago();
            lstMsj = new List<string>();
            using (SLDocument sl = new SLDocument(fileName))
            {
                row = 2;
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(row, 1)))
                {
                    contexto.InstanciarPagoImportacion();
                    DateTime fechaRecibida = sl.GetCellValueAsDateTime(row, 4);
                    contexto.ObjPagoImportacion.Fecha = fechaRecibida;
                    contexto.ObjPagoImportacion.Contrato = sl.GetCellValueAsString(row, 1);
                    contexto.ObjPagoImportacion.NoPago = sl.GetCellValueAsInt32(row, 2);
                    contexto.ObjPagoImportacion.Monto = sl.GetCellValueAsDecimal(row, 3);
                    contexto.ObjPagoImportacion.Observacion = sl.GetCellValueAsString(row, 5);

                    contexto.LstPagosImportacion.Add(contexto.ObjPagoImportacion);
                    row++;
                }

                foreach(var item in contexto.LstPagosImportacion)
                {
                    contexto.ObtenerContrato(item.Contrato);
                    if (contexto.ObjContrato==null)
                    {
                        lstMsj.Add("No se encontro el contrato: " + item.Contrato);
                        continue;
                    }
                    contexto.InstanciarPago();
                    contexto.ObjPago.Folio = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.PAGO);
                    contexto.ObjPago.FechaEmision = item.Fecha;
                    contexto.ObjPago.ContratoId = contexto.ObjContrato.Id;
                    contexto.ObjPago.NoPago = item.NoPago;
                    contexto.ObjPago.Monto = item.Monto;
                    contexto.ObjPago.Observacion = item.Observacion;
                    contexto.ObjPago.USUARIORecibeId = Global.ObjUsuario.Id;
                    contexto.ObjPago.PagoOrdinario = true;
                    contexto.ObjPago.ViaGenerado = (int)Enumeraciones.PagosViaGeneracion.IMPORTACION;

                    contexto.GuardarPago();

                }

                MostrarPagosImportados();


            }
        }

        private void MostrarPagosImportados()
        {
           // AparienciasPagos();
            if (contexto.LstPagosImportacion != null && contexto.LstPagosImportacion.Count > 0)
            {
                dgvRegistros.DataSource = contexto.LstPagosImportacion;
                tsTotalRegistros.Text = dgvRegistros.RowCount.ToString("N0");

                string msjSuccess = "Registros importados correctamente.";
                if (lstMsj != null && lstMsj.Count > 0)
                {
                    MostrarErrores();
                    msjSuccess = "Operación de importación terminada, pero se detectaron detalles en el proceso, verifica en el apartado de errores.";
                }

                MessageBox.Show(
                 msjSuccess,
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                 "No hubo registros para importar.",
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
        }

        private void AparienciasPagos()
        {
            dgvRegistros.Columns[0].HeaderText = "CONTRATO";
            dgvRegistros.Columns[0].Width = 100;
            dgvRegistros.Columns[1].HeaderText = "NO. PAGO";
            dgvRegistros.Columns[1].Width = 80;
            dgvRegistros.Columns[2].HeaderText = "MONTO";
            dgvRegistros.Columns[2].Width = 100;
            dgvRegistros.Columns[3].HeaderText = "FECHA";
            dgvRegistros.Columns[3].Width = 120;
            dgvRegistros.Columns[4].HeaderText = "OBSERVACION";
            dgvRegistros.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ImportarContratos(string fileName)
        {
            contexto.InstanciarListaImportacionContratos();
            lstMsj = new List<string>();
            using (SLDocument sl = new SLDocument(fileName))
            {
                row = 2;

                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(row, 1)))
                {
                    contexto.InstanciarContratoImportacion();
                    DateTime fechaRecibida = sl.GetCellValueAsDateTime(row, 1);
                    contexto.ObjContratoImportacion.FechaArrendamiento = fechaRecibida;
                    contexto.ObjContratoImportacion.ClaveCliente = sl.GetCellValueAsString(row, 2);
                    contexto.ObjContratoImportacion.Socio = sl.GetCellValueAsString(row, 3);
                    contexto.ObjContratoImportacion.NoPagos = sl.GetCellValueAsInt32(row, 4);                    
                    contexto.ObjContratoImportacion.PrecioInicial = sl.GetCellValueAsDecimal(row, 5);
                    contexto.ObjContratoImportacion.DiaPago = sl.GetCellValueAsInt32(row, 6);
                    contexto.ObjContratoImportacion.NoPagosGracia = sl.GetCellValueAsInt32(row, 7);
                    contexto.ObjContratoImportacion.Observacion = sl.GetCellValueAsString(row, 8);
                    contexto.ObjContratoImportacion.Zona = sl.GetCellValueAsString(row,9);
                    contexto.ObjContratoImportacion.IdentificadorLote = sl.GetCellValueAsString(row, 10);
                    contexto.ObjContratoImportacion.ColindaNorte = sl.GetCellValueAsString(row, 11);
                    contexto.ObjContratoImportacion.ColindaSur = sl.GetCellValueAsString(row, 12);
                    contexto.ObjContratoImportacion.ColindaEste = sl.GetCellValueAsString(row, 13);
                    contexto.ObjContratoImportacion.ColindaOeste = sl.GetCellValueAsString(row, 14);
                    contexto.ObjContratoImportacion.MideNorte = sl.GetCellValueAsDecimal(row, 15);
                    contexto.ObjContratoImportacion.MideSur = sl.GetCellValueAsDecimal(row, 16);
                    contexto.ObjContratoImportacion.MideEste = sl.GetCellValueAsDecimal(row, 17);
                    contexto.ObjContratoImportacion.MideOeste = sl.GetCellValueAsDecimal(row, 18);
                    contexto.ObjContratoImportacion.Direccion = sl.GetCellValueAsString(row, 19);
                    contexto.ObjContratoImportacion.Estado = sl.GetCellValueAsString(row, 20);
                    contexto.ObjContratoImportacion.PagoInical = sl.GetCellValueAsDecimal(row, 21);

                    contexto.LstContratosImpotacion.Add(contexto.ObjContratoImportacion);
                    row++;
                }
              
                foreach (var item in contexto.LstContratosImpotacion)
                {
                    contexto.InstanciarContrato();
                    
                   
                    contexto.ObtenerClienteXClave(item.ClaveCliente);
                    if (contexto.ObjCliente == null)
                    {
                        lstMsj.Add("No se encontro el cliente con clave: " + item.ClaveCliente);
                        continue;
                    }

                    contexto.ObjPersona = contexto.ObtenerPersona(contexto.ObjCliente.PERSONAId);                
                    contexto.ObtenerSocioXNombre(contexto.ObjCliente.Id, item.Socio);                   

                    contexto.ObjContrato.Folio = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.CONTRATO);
                    contexto.ObjContrato.FechaArrendamiento = item.FechaArrendamiento;
                    contexto.ObjContrato.CLIENTEId = contexto.ObjCliente.Id;
                    if (contexto.ObjSocios != null)
                    {
                        contexto.ObjContrato.SOCIOSId = contexto.ObjSocios.Id;
                    }
                    contexto.ObjContrato.NoPagos = item.NoPagos;
                    contexto.ObjContrato.PrecioInicial = item.PrecioInicial;
                    contexto.ObjContrato.DiaPago = item.DiaPago;
                    contexto.ObjContrato.PagoInicial = item.PagoInical;
                    contexto.ObjContrato.NoPagosGracia = item.NoPagosGracia;
                    contexto.ObjContrato.ESTADOId = (int)Enum.Parse(typeof(Enumeraciones.EstadosProcesoContratos), contexto.ObjContratoImportacion.Estado.ToUpper());
                    contexto.ObjContrato.Observacion = item.Observacion;
                    contexto.ObjContrato.USUARIOOperacionId = Global.ObjUsuario.Id;

                    contexto.ObjContrato.ColindaNorte = item.ColindaNorte;
                    contexto.ObjContrato.ColindaSur = item.ColindaSur;  
                    contexto.ObjContrato.ColindaEste = item.ColindaEste;    
                    contexto.ObjContrato.ColindaOeste = item.ColindaOeste;

                    contexto.ObjContrato.MideNorte = item.MideNorte;
                    contexto.ObjContrato.MideSur = item.MideSur;
                    contexto.ObjContrato.MideEste = item.MideEste;
                    contexto.ObjContrato.MideOeste = item.MideOeste;

                    contexto.ObjDireccionContrato = contexto.ObtenerDireccionCliente(item.Direccion, contexto.ObjCliente.Id);
                    if (contexto.ObjDireccionContrato != null)
                    {
                        contexto.ObjContrato.AGENDAId = contexto.ObjDireccionContrato.Id;
                    }
                    else
                    {
                        //crearla
                        contexto.InstanciarContactoAgenda();
                        contexto.ObjAgenda.Tipo = (int)Enumeraciones.TipoContactoAgenda.DIRECCION;
                        contexto.ObjAgenda.Valor = item.Direccion;
                        contexto.GuardarContactoAgenda();
                        contexto.ObjContrato.AGENDAId = contexto.ObjAgenda.Id;
                    }

                    contexto.GuardarContrato();

                    contexto.ObjZona = contexto.ObtenerZonaNombre(contexto.ObjContratoImportacion.Zona);

                    //como va a venir multiples hacerlo multiple
                    List<string> lotesRelacionados;
                    lotesRelacionados = item.IdentificadorLote.Split(',').ToList();
                    List<LOTE> lstLotes = contexto.ObtenerLoteXIdentidicadorMultiple(lotesRelacionados, contexto.ObjZona.Id);
                    
                    Global.RelacionarLotesContrato(contexto.ObjContrato.Id,lstLotes, contexto.ObjContrato.ESTADOId, 
                        ( (contexto.ObjContrato.ESTADOId == (int)Enumeraciones.EstadosProcesoContratos.VIGENTE || 
                        contexto.ObjContrato.ESTADOId == (int)Enumeraciones.EstadosProcesoContratos.ATRASADO)? 
                        (int)Enumeraciones.EstadosProcesoLote.ASIGNADO: 
                        (contexto.ObjContrato.ESTADOId == (int)Enumeraciones.EstadosProcesoContratos.TERMINADO) ? (int)Enumeraciones.EstadosProcesoLote.VENDIDO: (int)Enumeraciones.EstadosProcesoLote.LIBRE ));

                    /* se omite el pago porque se va a meter la boleta completa
                    if (contexto.ObjContrato.Id != 0)
                    {
                        contexto.InstanciarPago();
                        contexto.ObjPago.Folio = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.PAGO);
                        contexto.ObjPago.FechaEmision = Global.FechaServidor();
                        contexto.ObjPago.ContratoId = contexto.ObjContrato.Id;
                        contexto.ObjPago.NoPago = 1;
                        contexto.ObjPago.Observacion = item.Observacion;
                        contexto.ObjPago.USUARIORecibeId = Global.ObjUsuario.Id;
                        contexto.ObjPago.PagoOrdinario = true;
                        contexto.GuardarPago();
                    }
                    */
                }


                MostrarContratosImportados();

            }
        }

        private void MostrarContratosImportados()
        {
            if (contexto.LstContratosImpotacion != null && contexto.LstContratosImpotacion.Count > 0)
            {
                dgvRegistros.DataSource = contexto.LstContratosImpotacion;
                tsTotalRegistros.Text = dgvRegistros.RowCount.ToString("N0");

              //  AparienciasContratos();

                string msjSuccess = "Registros importados correctamente.";
                if (lstMsj!=null && lstMsj.Count > 0)
                {
                    MostrarErrores();
                    msjSuccess = "Operación de importación terminada, pero se detectaron detalles en el proceso, verifica en el apartado de errores.";
                }                   

                MessageBox.Show(
                 msjSuccess,
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                 "No hubo registros para importar.",
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
        }

        private void AparienciasContratos()
        {            
            dgvRegistros.Columns[0].HeaderText = "FECHA ARRENDAMIENTO";
            dgvRegistros.Columns[0].Width = 120;
            dgvRegistros.Columns[1].HeaderText = "CLAVE CLIENTE";
            dgvRegistros.Columns[1].Width = 80;
            dgvRegistros.Columns[2].HeaderText = "SOCIO";
            dgvRegistros.Columns[2].Width = 100;
            dgvRegistros.Columns[3].HeaderText = "IDETIFICADOR LOTE";
            dgvRegistros.Columns[3].Width = 100;
            dgvRegistros.Columns[4].HeaderText = "NO. PAGOS";
            dgvRegistros.Columns[4].Width = 100;
            dgvRegistros.Columns[5].HeaderText = "PRECIO INICIAL";
            dgvRegistros.Columns[5].Width = 100;
            dgvRegistros.Columns[6].HeaderText = "DIA PAGO";
            dgvRegistros.Columns[6].Width = 120;
            dgvRegistros.Columns[7].HeaderText = "PAGO INICIAL";
            dgvRegistros.Columns[7].Width = 120;
            dgvRegistros.Columns[8].HeaderText = "NO. PAGOS GRACIA";
            dgvRegistros.Columns[8].Width = 120;
            dgvRegistros.Columns[9].HeaderText = "OSERVACION";
            dgvRegistros.Columns[9].Width = 120;
           
        }

        private void ImportarLotes(string fileName)
        {
            lstMsj = new List<string>();
            contexto.InstanciarListaImportacionLote();
            contexto.ListarZonas();
            //leer excel
            using (SLDocument sl = new SLDocument(fileName))
            {
                row = 2;

                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(row, 1)))
                {
                    contexto.InstanciarLoteImportacion();
                    string nombreZona = sl.GetCellValueAsString(row,1);
                    contexto.ObjZonaAux = contexto.LstZonasAux.FirstOrDefault(x=>x.Nombre == nombreZona);

                    if (contexto.ObjZonaAux==null)
                    {
                        lstMsj.Add("No se encontro la zona "+nombreZona);
                        continue;
                    }
                    contexto.ObjLoteImportacion.Zona = contexto.ObjZonaAux.Nombre;
                    contexto.ObjLoteImportacion.ZonaId = contexto.ObjZonaAux.Id;
                    contexto.ObjLoteImportacion.Manzana = Convert.ToInt32(sl.GetCellValueAsString(row, 2));
                    contexto.ObjLoteImportacion.Precio = Convert.ToDecimal(sl.GetCellValueAsString(row, 3));
                    contexto.ObjLoteImportacion.Cantidad = Convert.ToInt32(sl.GetCellValueAsString(row, 4));

                    contexto.LstLotesImportados.Add(contexto.ObjLoteImportacion);
                    row++;
                }

                DateTime fechaServer = Global.FechaServidor();
                int zonaIdActual = 0;
                int consecutivoZona = 0;
                foreach (var item in contexto.LstLotesImportados.OrderBy(x=>x.ZonaId))
                {
                    if(zonaIdActual!= item.ZonaId)
                    {
                        zonaIdActual = item.ZonaId;
                        //consultar ultimo lote para obtener el consecutivo
                        string identificadorUltimoLote = contexto.ObtenerIdentificadorUltimoLote(zonaIdActual);
                        if (string.IsNullOrEmpty(identificadorUltimoLote))
                        {
                            consecutivoZona = 0;
                        }
                        else
                        {
                            string [] arrayIdentificador = new string [2];
                            arrayIdentificador = identificadorUltimoLote.Split(' ');
                            consecutivoZona = Convert.ToInt32(arrayIdentificador[0].Substring(2));
                        }
                        
                    }

                    if (item.Cantidad > 1)
                    {
                        for(int i = 1; i<=item.Cantidad; i++)
                        {
                            consecutivoZona++;
                            GuardarLote(item, consecutivoZona, fechaServer);
                        }                        
                    }
                    else
                    {
                        consecutivoZona++;
                        GuardarLote(item, consecutivoZona, fechaServer);
                    }
                   

                }


                MostrarLotesImportados();

            }
        }

        private void MostrarLotesImportados()
        {            
            if (contexto.LstLotesImportados != null && contexto.LstLotesImportados.Count > 0)
            {
                dgvRegistros.DataSource = contexto.LstLotesImportados;
                tsTotalRegistros.Text = dgvRegistros.RowCount.ToString("N0");
                AparienciasLotes();
                string msjSuccess = "Registros importados correctamente.";
                if (lstMsj != null && lstMsj.Count > 0)
                {
                    MostrarErrores();
                    msjSuccess = "Operación de importación terminada, pero se detectaron detalles en el proceso, verifica en el apartado de errores.";
                }

                MessageBox.Show(
                 msjSuccess,
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                 "No hubo registros para importar.",
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
        }

        private void AparienciasLotes()
        {
            dgvRegistros.Columns[0].HeaderText = "ZONA";
            dgvRegistros.Columns[0].Width = 120;
            dgvRegistros.Columns[1].Visible = false;
            dgvRegistros.Columns[2].HeaderText = "MANZANA";
            dgvRegistros.Columns[2].Width = 90;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].HeaderText = "PRECIO";
            dgvRegistros.Columns[4].Width = 100;
            dgvRegistros.Columns[5].HeaderText = "CANTIDAD";
            dgvRegistros.Columns[5].Width = 80;            
           
        }

        private void GuardarLote(clsLoteImportacion item, int consecutivo, DateTime fechaServer)
        {
            contexto.InstanciarLote();
            contexto.ObjLote.Identificador = "L/" + consecutivo + (item.Manzana !=null? " M/" + item.Manzana : "");
            contexto.ObjLote.NoLote = consecutivo.ToString("N0");
            contexto.ObjLote.Precio = item.Precio;
            contexto.ObjLote.FechaRegistro = fechaServer;
            contexto.ObjLote.ZONAId = item.ZonaId;
            contexto.ObjLote.Manzana = item.Manzana;
            contexto.ObjLote.ESTADOId = (int)Enumeraciones.EstadosProcesoLote.LIBRE;

            contexto.GuardarLote();
        }

        private void ImportarZonas(string fileName)
        {
            contexto.InstanciarListaImportacionZona();
            //leer excel
            using (SLDocument sl = new SLDocument(fileName))
            {
                row = 2;

                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(row, 1)))
                {
                    contexto.InstanciarZona();
                    contexto.ObjZona.Nombre = sl.GetCellValueAsString(row, 1);
                    contexto.ObjZona.NoManzanas = Convert.ToInt32(sl.GetCellValueAsString(row, 2));
                    contexto.ObjZona.NoLotes = Convert.ToInt32(sl.GetCellValueAsString(row, 3));
                    contexto.ObjZona.Direccion = sl.GetCellValueAsString(row, 4);
                    contexto.LstZonasImportadas.Add(contexto.ObjZona);
                    row++;
                }

                foreach (var item in contexto.LstZonasImportadas)
                {
                    contexto.ObjZona = contexto.ObtenerZonaNombre(item.Nombre);

                    if (contexto.ObjZona == null)
                    {
                        //crear
                        contexto.InstanciarZona();
                        contexto.ObjZona.Nombre = item.Nombre;
                        contexto.ObjZona.NoLotes = item.NoLotes;
                        contexto.ObjZona.NoManzanas = item.NoManzanas;
                        contexto.ObjZona.Direccion = item.Direccion;
                        contexto.ObjZona.FechaRegistro = Global.FechaServidor();
                        contexto.GuardarZona();

                    }
                    else
                    {
                        //existe
                        contexto.ObjZona.Nombre = item.Nombre;
                        contexto.ObjZona.NoLotes = item.NoLotes;
                        contexto.ObjZona.NoManzanas = item.NoManzanas;
                        contexto.ObjZona.Direccion = item.Direccion;
                        contexto.GuardarZona();
                    }


                }


                MostrarZonasImportadas();

            }
        }

        private void MostrarZonasImportadas()
        {
            if(contexto.LstZonasImportadas!=null && contexto.LstZonasImportadas.Count > 0)
            {
                dgvRegistros.DataSource = contexto.LstZonasImportadas;
                tsTotalRegistros.Text = dgvRegistros.RowCount.ToString("N0");


                AparienciasZonas();

                MessageBox.Show(
                 "Registros importados correctamente.",
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                 "No hubo registros para importar.",
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
           
        }

        private void AparienciasZonas()
        {
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].HeaderText = "Nombre";
            dgvRegistros.Columns[1].Width = 110;
            dgvRegistros.Columns[3].HeaderText = "No. Manzanas";
            dgvRegistros.Columns[3].Width = 75;
            dgvRegistros.Columns[4].HeaderText = "No. Lotes";
            dgvRegistros.Columns[4].Width = 75;
            dgvRegistros.Columns[5].HeaderText = "Dirección";
            dgvRegistros.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].Visible = false;
        }
               
        private void ImportarClientes(string fileName)
        {
            using (SLDocument sl = new SLDocument(fileName))
            {
                //listar clientes
                contexto.InstanciarListaImportacionClientes();
                contexto.ListarClientes();  
                row = 2;

                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(row, 1)))
                {
                    contexto.InstanciarObjClienteImportacion();
                    contexto.ObjClienteImportacion.Cliente = sl.GetCellValueAsString(row, 1);
                    contexto.ObjClienteImportacion.Socio = sl.GetCellValueAsString(row, 2);
                    contexto.ObjClienteImportacion.Telefono = sl.GetCellValueAsString(row, 3);
                    contexto.ObjClienteImportacion.Correo = sl.GetCellValueAsString(row, 4);
                    contexto.ObjClienteImportacion.Direccion = sl.GetCellValueAsString(row, 5);
                    contexto.LstImportacionCliente.Add(contexto.ObjClienteImportacion);
                    row++;
                }

                foreach (var item in contexto.LstImportacionCliente)
                {
                    string[] nombreCliente = item.Cliente.Split(',');

                    contexto.ObjClienteData = contexto.LstClientesExistentes.FirstOrDefault(x => x.Nombres == nombreCliente[0].Trim() && x.Apellidos == nombreCliente[1].Trim());

                    if (contexto.ObjClienteData == null)
                    {
                        //crear
                        contexto.InstanciarPersona();
                        contexto.InstanciarCliente();
                        contexto.ObjPersona.Nombres = nombreCliente[0].Trim();
                        contexto.ObjPersona.Apellidos = nombreCliente[1].Trim();
                        contexto.ObjCliente.Clave = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.CLIENTE);
                        contexto.ObjCliente.ESTADOId = (int)Enumeraciones.EstadosProcesoCliente.ACTIVO;
                        contexto.Guardar();

                    }
                    else
                    {
                        //existe
                        contexto.ObjCliente = contexto.ObtenerCliente(contexto.ObjClienteData.Id);
                        contexto.ObjPersona = contexto.ObtenerPersona(contexto.ObjCliente.PERSONAId);
                    }
                    //socio
                    if (!string.IsNullOrEmpty(item.Socio))
                    {
                        string[] nombreSocio = item.Socio.Split(',');
                        if (contexto.ObjClienteData != null)
                        {
                            contexto.ObjSocios = contexto.BuscarSocioPorNombre(nombreSocio[0].Trim() + " " + nombreSocio[1].Trim(), contexto.ObjClienteData.Id);
                        }

                        if (contexto.ObjSocios == null)
                        {
                            contexto.InstanciarSocio();
                            contexto.ObjSocios.Nombre = nombreSocio[0].Trim() + " " + nombreSocio[1].Trim();
                            contexto.GuardarSocio();
                        }

                    }

                    //telefono
                    if (!string.IsNullOrEmpty(item.Telefono))
                    {
                        contexto.InstanciarContactoAgenda();
                        contexto.ObjAgenda.Tipo = 1;
                        contexto.ObjAgenda.Valor = item.Telefono;
                        contexto.GuardarContactoAgenda();
                    }

                    //correo
                    if (!string.IsNullOrEmpty(item.Correo))
                    {
                        contexto.InstanciarContactoAgenda();
                        contexto.ObjAgenda.Tipo = 2;
                        contexto.ObjAgenda.Valor = item.Correo;
                        contexto.GuardarContactoAgenda();
                    }

                    //direccion
                    if (!string.IsNullOrEmpty(item.Direccion))
                    {
                        contexto.InstanciarContactoAgenda();
                        contexto.ObjAgenda.Tipo = 1;
                        contexto.ObjAgenda.Valor = item.Direccion;
                        contexto.GuardarContactoAgenda();
                    }

                }

            }

            MostrarClientesImportados();


        }

        private void MostrarClientesImportados()
        {
            if (contexto.LstImportacionCliente != null && contexto.LstImportacionCliente.Count > 0)
            {
                dgvRegistros.DataSource = contexto.LstImportacionCliente;
                tsTotalRegistros.Text = dgvRegistros.RowCount.ToString("N0");


                AparienciasClientes();

                MessageBox.Show(
                 "Registros importados correctamente.",
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                 "No hubo registros para importar.",
                 "Aviso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
            }

        
        
        }

        private void AparienciasClientes()
        {
            dgvRegistros.Columns[0].HeaderText = "Nombre";
            dgvRegistros.Columns[0].Width = 210;
            dgvRegistros.Columns[1].HeaderText = "Socio";
            dgvRegistros.Columns[1].Width = 210;
            dgvRegistros.Columns[2].HeaderText = "Telefono";
            dgvRegistros.Columns[2].Width = 100;
            dgvRegistros.Columns[3].HeaderText = "Correo";
            dgvRegistros.Columns[3].Width = 100;
            dgvRegistros.Columns[4].HeaderText = "Dirección";
            dgvRegistros.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void MostrarErrores()
        {
            if(lstMsj!=null && lstMsj.Count > 0)
            {
                dgvErrores.Rows.Clear();
                dgvErrores.Columns.Clear();
                dgvErrores.Columns.Add(new DataGridViewTextBoxColumn());
                foreach(var item in lstMsj)
                {
                    dgvErrores.Rows.Add(item);  
                }
                tsTotalErrores.Text = lstMsj.Count.ToString("N0");
                AparienciasErrores();
            }
        }

        private void AparienciasErrores()
        {
            dgvErrores.Columns[0].HeaderText = "Error";
            dgvErrores.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;   
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ImportarLayout();
        }

        private void formImportacionLayouts_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void btnLayout_Click(object sender, EventArgs e)
        {
            GenerarLayout(cbxTipo.SelectedIndex);
        }

        private void formImportacionLayouts_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void cbxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargado||cbxTipo.SelectedIndex == -1) return;
            if (dgvRegistros.DataSource == null) return;
            if(MessageBox.Show("¿Desea cambiar el tipo de importación de datos?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvErrores.Rows.Clear();
                dgvErrores.Columns.Clear();
                dgvRegistros.DataSource = null;
                tsTotalRegistros.Text = @"0";
                tsTotalErrores.Text = @"0";
            }
        }
    }
}
