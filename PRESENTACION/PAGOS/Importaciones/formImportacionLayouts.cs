using CAPADATOS.Entidades;
using CAPALOGICA.LOGICAS.PAGOS;
using PRESENTACION.UTILERIAS;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public formImportacionLayouts()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            LimpiarControles();
            InstanciarContextos();
        }

        private void InstanciarContextos()
        {
            contexto = new formImportacionLayoutsLogica();
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
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
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar importar los registros. Ejecuón pausada en el row:" + row,
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
                        GenerarLayoutCliente(opc, rutaArchivo);
                        break;
                    case 1:
                        GenerarLayoutZonas(opc, rutaArchivo);
                        break;
                    case 2:
                        GenerarLayoutLotes(opc, rutaArchivo);
                        break;
                    case 3:
                        GenerarLayoutContratos(opc, rutaArchivo);
                        break;
                    case 4:
                        GenerarLayoutPagos(opc, rutaArchivo);
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

        private void GenerarLayoutPagos(int opc, string rutaArchivo)
        {
            throw new NotImplementedException();
        }

        private void GenerarLayoutContratos(int opc, string rutaArchivo)
        {
            using (SLDocument sl = new SLDocument())
            {
                sl.SetCellValue(1, 1, "FECHA ARRENDAMIENTO");
                sl.SetCellValue(1, 2, "CLAVE CLIENTE");
                sl.SetCellValue(1, 3, "SOCIO");
                sl.SetCellValue(1, 4, "IDENTIFICADOR LOTE");
                sl.SetCellValue(1, 5, "NO. PAGOS");
                sl.SetCellValue(1, 6, "PRECIO INICIAL");
                sl.SetCellValue(1, 7, "DIA PAGO");
                sl.SetCellValue(1, 8, "PAGO INICIAL");
                sl.SetCellValue(1, 9, "NO. PAGOS GRACIA");
                sl.SetCellValue(1, 10, "OBSERVACION");

                sl.SaveAs(rutaArchivo);
            }
        }

        private void GenerarLayoutLotes(int opc, string rutaArchivo)
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

        private void GenerarLayoutZonas(int opc, string rutaArchivo)
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

        private void GenerarLayoutCliente(int opc, string rutaArchivo)
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
            throw new NotImplementedException();
        }

        private void ImportarContratos(string fileName)
        {
            contexto.InstanciarListaImportacionContratos();
            List<string> lstError = new List<string>();
            using (SLDocument sl = new SLDocument(fileName))
            {
                row = 2;

                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(row, 1)))
                {
                    contexto.InstanciarContratoImportacion();
                    DateTime fechaRecibida = sl.GetCellValueAsDateTime(row, 1);
                    contexto.ObjContratoImportacion.FechaArrendamiento = fechaRecibida;//DateTime.ParseExact(fechaRecibida, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    contexto.ObjContratoImportacion.ClaveCliente = sl.GetCellValueAsString(row, 2);
                    contexto.ObjContratoImportacion.Socio = sl.GetCellValueAsString(row, 3);
                    contexto.ObjContratoImportacion.IdentificadorLote = sl.GetCellValueAsString(row, 4);
                    contexto.ObjContratoImportacion.NoPagos = sl.GetCellValueAsInt32(row, 5);
                    contexto.ObjContratoImportacion.PrecioInicial = sl.GetCellValueAsDecimal(row, 6);
                    contexto.ObjContratoImportacion.DiaPago = sl.GetCellValueAsInt32(row, 7);
                    contexto.ObjContratoImportacion.PagoInicial = sl.GetCellValueAsDecimal(row, 8);
                    contexto.ObjContratoImportacion.NoPagosGracia = sl.GetCellValueAsInt32(row, 9);
                    contexto.ObjContratoImportacion.Observacion = sl.GetCellValueAsString(row, 10);

                    contexto.LstContratosImpotacion.Add(contexto.ObjContratoImportacion);
                    row++;
                }
              
                foreach (var item in contexto.LstContratosImpotacion)
                {
                    contexto.InstanciarContrato();

                    contexto.ObjContrato.Folio = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.CONTRATO);
                    contexto.ObjContrato.FechaArrendamiento = item.FechaArrendamiento;
                    contexto.ObtenerClienteXClave(item.ClaveCliente);
                    if (contexto.ObjCliente == null)
                    {
                        lstError.Add("No se encontro el cliente con clave: " + item.ClaveCliente);
                        continue;
                    }
                    contexto.ObjContrato.CLIENTEId = contexto.ObjCliente.Id;
                    contexto.ObtenerSocioXNombre(contexto.ObjCliente.Id, item.Socio);
                    if (contexto.ObjSocios != null)
                    {
                        contexto.ObjContrato.SOCIOSId = contexto.ObjSocios.Id;
                    }
                    contexto.ObtenerLoteXIdentidicador(item.IdentificadorLote);
                    if (contexto.ObjLote == null)
                    {
                        lstError.Add("No se encontro el lote: " + item.IdentificadorLote+"del cliente: "+item.ClaveCliente);
                        continue;
                    }

                    contexto.ObjContrato.LOTEId = contexto.ObjLote.Id;
                    contexto.ObjContrato.NoPagos = item.NoPagos;
                    contexto.ObjContrato.PrecioInicial = item.PrecioInicial;    
                    contexto.ObjContrato.DiaPago = item.DiaPago;
                    contexto.ObjContrato.PagoInicial = item.PagoInicial;    
                    contexto.ObjContrato.NoPagosGracia = item.NoPagosGracia;
                    contexto.ObjContrato.ESTADOId = (int)Enumeraciones.EstadosProcesoContratos.VIGENTE;
                    contexto.ObjContrato.Observacion = item.Observacion;
                    contexto.ObjContrato.USUARIOOperacionId = Global.ObjUsuario.Id;

                    contexto.GuardarContrato();

                    if (contexto.ObjContrato.Id != 0)
                    {
                        contexto.InstanciarPago();
                        contexto.ObjPago.Folio = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.PAGO);
                        contexto.ObjPago.FechaEmision = Global.FechaServidor();
                        contexto.ObjPago.ContratoId = contexto.ObjContrato.Id;
                        contexto.ObjPago.NoPago = 1;
                        contexto.ObjPago.Monto = item.PagoInicial;
                        contexto.ObjPago.Observacion = item.Observacion;
                        contexto.ObjPago.USUARIORecibeId = Global.ObjUsuario.Id;
                        contexto.ObjPago.PagoOrdinario = true;
                        contexto.GuardarPago();
                    }

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

                AparienciasContratos();

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
            dgvRegistros.Columns[7].Width = 12;
            dgvRegistros.Columns[8].HeaderText = "NO. PAGOS GRACIA";
            dgvRegistros.Columns[8].Width = 120;
            dgvRegistros.Columns[9].HeaderText = "OSERVACION";
            dgvRegistros.Columns[9].Width = 120;
           
        }

        private void ImportarLotes(string fileName)
        {
            List<string>lstMsj = new List<string>();
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

                    contexto.ObjLoteImportacion.ZONAId = contexto.ObjZonaAux.Id;
                    contexto.ObjLoteImportacion.Zona = contexto.ObjZonaAux.Nombre;
                    contexto.ObjLoteImportacion.Cantidad = Convert.ToInt32(sl.GetCellValueAsString(row, 2));
                    contexto.ObjLoteImportacion.MSur = Convert.ToDecimal(sl.GetCellValueAsString(row,3));
                    contexto.ObjLoteImportacion.MOeste = Convert.ToDecimal(sl.GetCellValueAsString(row, 4));
                    contexto.ObjLoteImportacion.MEste = Convert.ToDecimal(sl.GetCellValueAsString(row, 5));
                    contexto.ObjLoteImportacion.MNorte = Convert.ToDecimal(sl.GetCellValueAsString(row, 6));
                    contexto.ObjLoteImportacion.CSur = sl.GetCellValueAsString(row, 7);
                    contexto.ObjLoteImportacion.COeste = sl.GetCellValueAsString(row, 8);
                    contexto.ObjLoteImportacion.CEste = sl.GetCellValueAsString(row, 9);
                    contexto.ObjLoteImportacion.CNorte = sl.GetCellValueAsString(row, 10);
                    contexto.ObjLoteImportacion.Precio = Convert.ToDecimal(sl.GetCellValueAsString(row, 11));
                    contexto.ObjLoteImportacion.Manzana = Convert.ToInt32(sl.GetCellValueAsString(row, 12));

                    contexto.LstLotesImportados.Add(contexto.ObjLoteImportacion);
                    row++;
                }
                DateTime fechaServer = Global.FechaServidor();
                int zonaIdActual = 0;
                int consecutivoZona = 0;
                foreach (var item in contexto.LstLotesImportados.OrderBy(x=>x.ZONAId))
                {
                    if(zonaIdActual!= item.ZONAId)
                    {
                        zonaIdActual = item.ZONAId;
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

        private void AparienciasLotes()
        {
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].Visible = false;
            dgvRegistros.Columns[2].Visible = false;
            dgvRegistros.Columns[0].Frozen = true;
            dgvRegistros.Columns[1].Frozen =  true;
            dgvRegistros.Columns[2].Frozen =  true;
            dgvRegistros.Columns[3].HeaderText = "ZONA";
            dgvRegistros.Columns[3].Width = 120;
            dgvRegistros.Columns[4].HeaderText = "CANTIDAD";
            dgvRegistros.Columns[4].Width = 80;
            dgvRegistros.Columns[5].HeaderText = "MIDE NORTE";
            dgvRegistros.Columns[5].Width = 100;
            dgvRegistros.Columns[6].HeaderText = "MIDE SUR";
            dgvRegistros.Columns[6].Width = 100;
            dgvRegistros.Columns[7].HeaderText = "MIDE OESTE";
            dgvRegistros.Columns[7].Width = 100;
            dgvRegistros.Columns[8].HeaderText = "MIDE ESTE";
            dgvRegistros.Columns[8].Width = 100;
            dgvRegistros.Columns[9].HeaderText = "COL. NORTE";
            dgvRegistros.Columns[9].Width = 120;
            dgvRegistros.Columns[10].HeaderText = "COL. SUR";
            dgvRegistros.Columns[10].Width = 12;
            dgvRegistros.Columns[11].HeaderText = "COL. OESTE";
            dgvRegistros.Columns[11].Width = 120;
            dgvRegistros.Columns[12].HeaderText = "COL. ESTE";
            dgvRegistros.Columns[12].Width = 120;
            dgvRegistros.Columns[13].Visible = false;
            dgvRegistros.Columns[14].HeaderText = "PRECIO";
            dgvRegistros.Columns[14].Width = 100;
            dgvRegistros.Columns[15].HeaderText = "MANZANA";
            dgvRegistros.Columns[15].Width = 90;
            dgvRegistros.Columns[16].Visible = false;
        }

        private void GuardarLote(clsLoteImportacion item, int consecutivo, DateTime fechaServer)
        {
            contexto.InstanciarLote();
            contexto.ObjLote.Identificador = "L/" + consecutivo + (item.Manzana !=null? " M/" + item.Manzana : "");
            contexto.ObjLote.MNorte = item.MNorte;
            contexto.ObjLote.MSur = item.MSur;
            contexto.ObjLote.MEste = item.MEste;
            contexto.ObjLote.MOeste = item.MOeste;
            contexto.ObjLote.CNorte = item.CNorte;
            contexto.ObjLote.CEste = item.CEste;
            contexto.ObjLote.COeste = item.COeste;
            contexto.ObjLote.CSur = item.CSur;
            contexto.ObjLote.Precio = item.Precio;
            contexto.ObjLote.FechaRegistro = fechaServer;
            contexto.ObjLote.ZONAId = item.ZONAId;
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
    }
}
