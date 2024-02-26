using CAPALOGICA.LOGICAS.PAGOS;
using DocumentFormat.OpenXml.Spreadsheet;
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

namespace PRESENTACION.PAGOS.REPORTES
{
    public partial class repPagosGlobal : Form
    {
        private repPagosGlobalLogica contexto;
        private int? zonaSeleccionada = null;
        private string path = "";

        public repPagosGlobal()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            try
            {
                contexto = new repPagosGlobalLogica();
                LimpiarControles();
                ListarZonas();
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

        private void ListarZonas()
        {
            contexto.ListarZonas();
            cbxZonas.DataSource = contexto.LstZonas;
            cbxZonas.DisplayMember = "Nombre";
            cbxZonas.ValueMember = "Id";

            cbxZonas.SelectedIndex = -1;
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            tsTotalRegistros.Text = @"0";
            chkTodos.Checked = false;
            
        }

        private void repPagosGlobal_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void repPagosGlobal_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                GenerarGlobal();
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar generar el reporte. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }

        private void GenerarGlobal()
        {
            if(cbxZonas.SelectedIndex==-1 && !chkTodos.Checked)
            {
                MessageBox.Show("Debe seleccionar una zona para poder generar el reporte.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }else if(chkTodos.Checked)
            {
                zonaSeleccionada = null;
            }
            else
            {
                zonaSeleccionada = (int)cbxZonas.SelectedValue;
            }
            saveFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*";
            saveFileDialog1.Title = "Guardar reporte de pagos";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
            }
            else
            {
                MessageBox.Show("No se selecciono ningúna ruta de guardado. Vuelva a intentarlo.",
                   "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            contexto.ListarEncabezadosPagoXZona(zonaSeleccionada);
            if(contexto.LstEncabezados!=null && contexto.LstEncabezados.Count > 0)
            {
                contexto.ListarPartidasPagoXZona(zonaSeleccionada);
                if(contexto.LstPartidas!=null && contexto.LstPartidas.Count > 0)
                {
                    var lstZonasAux = zonaSeleccionada != null ? contexto.LstZonas.Where(x => x.Id == zonaSeleccionada) : contexto.LstZonas;
                    contexto.InstanciarLstExportados();
                    using (SLDocument sl = new SLDocument())
                    {

                        //stylos
                        //pintar bordes
                        SLStyle styleBorde = sl.CreateStyle();

                        /*styleBorde.Border.LeftBorder.BorderStyle = BorderStyleValues.Thick;
                        styleBorde.Border.LeftBorder.Color = System.Drawing.Color.Black;
                        styleBorde.Border.TopBorder.BorderStyle = BorderStyleValues.Thick;
                        styleBorde.Border.TopBorder.Color = System.Drawing.Color.Black;
                        styleBorde.Border.RightBorder.BorderStyle = BorderStyleValues.Thick;
                        styleBorde.Border.RightBorder.Color = System.Drawing.Color.Black;
                        styleBorde.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
                        styleBorde.Border.BottomBorder.Color = System.Drawing.Color.Black;*/

                        styleBorde.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                        styleBorde.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                        styleBorde.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                        styleBorde.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

                        foreach (var zona in lstZonasAux)
                        {
                            sl.AddWorksheet(zona.Nombre);

                            int ex = 1, ey = 1;
                            decimal acumulado=0;
                            foreach (var encabezado in contexto.LstEncabezados.Where(x => x.ZonaId == zona.Id))
                            {
                                var lstPagosRelacionados = contexto.LstPartidas.Where(x => x.ContratoId == encabezado.ContratoId);

                                sl.SetCellValue(ey, ex+1, encabezado.ClaveCliente+". "+encabezado.NombreCliente);
                                sl.MergeWorksheetCells(ey, ex+1, ey, ex+2);
                                sl.AutoFitColumn(ex + 1, ex + 2);
                                sl.SetCellValue(ey+1,ex+1, "$"+encabezado.PrecioLote.ToString("N2") );
                                sl.SetCellValue(ey + 1, ex + 2, encabezado.NoPagos.ToString("N0")+" MESES");
                                sl.SetCellValue(ey + 2, ex + 1, encabezado.ZonaNombre);
                                sl.SetCellValue(ey + 3, ex + 1, encabezado.LoteIdentificador);
                                sl.SetCellValue(ey + 3, ex + 2, encabezado.FechaArrendamiento.ToString("dd-MM-yyyy"));
                                sl.SetCellValue(ey + 4, ex + 1, string.IsNullOrEmpty(encabezado.Observacion)?"SIN OBSERVACION":encabezado.Observacion);
                                sl.MergeWorksheetCells(ey + 4, ex+1 , ey + 4, ex + 2);
                                sl.SetCellValue(ey + 5, ex, "No.");
                                sl.SetCellValue(ey + 5, ex + 1, "MONTO");
                                sl.SetCellValue(ey + 5, ex + 2, "FECHA");

                                int rowPartida = 1;
                                foreach (var partida in contexto.LstPartidas.Where(x=>x.ContratoId == encabezado.ContratoId))
                                {
                                    sl.SetCellValue(ey+5+rowPartida, ex, partida.NoPago);
                                    sl.SetCellValue(ey+5+rowPartida, ex+1, "$ "+partida.Monto.ToString("N2"));
                                    sl.SetCellValue(ey+5+rowPartida, ex + 2, partida.FechaEmision.ToString("dd/MM/yyyy"));

                                    if (!string.IsNullOrEmpty(partida.Observacion))
                                    {
                                        rowPartida++;
                                        sl.SetCellValue(ey+5+rowPartida, ex, partida.Observacion);
                                        sl.MergeWorksheetCells(ey + 5 + rowPartida, ex, ey + 5 + rowPartida, ex+2);
                                    }
                                    acumulado += partida.Monto;
                                    rowPartida++;
                                }
                                rowPartida+=4;
                                sl.SetCellValue(ey + 5 + rowPartida, ex, "$ "+acumulado.ToString("N2"));

                                rowPartida += 4;
                                sl.SetCellStyle(ey, ex, rowPartida, ex+2, styleBorde);



                                ex = +4;
                            }

                            contexto.LstExportados.Add(new KeyValuePair<string, int>(zona.Nombre, contexto.LstEncabezados.Where(x => x.ZonaId == zona.Id).Count()));
                        }
                        sl.DeleteWorksheet("Sheet1");
                        sl.SaveAs(path);
                    }
                    

                }
            }
            

            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(contexto.LstExportados!=null && contexto.LstExportados.Count > 0)
            {
                AparienciasRegistros();
                MessageBox.Show("¡Reporte generado!.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay registros para mostrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

           
        }

        private void AparienciasRegistros()
        {
            dgvRegistros.DataSource = contexto.LstExportados;
            tsTotalRegistros.Text = contexto.LstExportados.Count.ToString("N0");


            dgvRegistros.Columns[0].HeaderText = "ZONA";
            dgvRegistros.Columns[1].HeaderText = "NO. CONTRATOS";


        }
    }
}
