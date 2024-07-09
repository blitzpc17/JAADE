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

namespace PRESENTACION.PAGOS.REPORTES
{
    public partial class repCorteCaja : Form
    {

        private repCorteCajaLogica contexto;
        private DateTime fechaInicio, fechaFin;

        public repCorteCaja()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            try
            {
                contexto = new repCorteCajaLogica();
                LimpiarControles();
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

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this); 
        }

        private void GenerarReporte()
        {
            try
            {
                backgroundWorker1.RunWorkerAsync();

            }catch(Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar generar el reporte.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
        }

        private void Apariencias()
        {
            dgvRegistros.DataSource = contexto.LstRegistros;
            tsTotalRegistros.Text = contexto.LstRegistros.Count.ToString("N0");

            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[0].Frozen = true;
            dgvRegistros.Columns[1].HeaderText = "FOLIO";
            dgvRegistros.Columns[1].Frozen = true;
            dgvRegistros.Columns[1].Width = 100;
            dgvRegistros.Columns[2].HeaderText = "FECHA EMISIÓN PAGO";
            dgvRegistros.Columns[2].Width = 110;
            dgvRegistros.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dgvRegistros.Columns[3].HeaderText = "CONTRATO";
            dgvRegistros.Columns[3].Width = 100;
            dgvRegistros.Columns[4].HeaderText = "LOTE(S)";
            dgvRegistros.Columns[4].Width = 90;
            dgvRegistros.Columns[5].HeaderText = "MONTO";
            dgvRegistros.Columns[5].Width = 100;
            dgvRegistros.Columns[5].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[6].HeaderText = "RECIBIO PAGO";
            dgvRegistros.Columns[6].Width = 280;
            dgvRegistros.Columns[7].HeaderText = "OBSERVACION";
            dgvRegistros.Columns[7].Width = 350;
        }

        private void Exportar()
        {
            if (dgvRegistros.DataSource == null) return;
            saveFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*";
            saveFileDialog1.Title = "Guardar reporte corte caja";

            if (saveFileDialog1.ShowDialog()== DialogResult.OK)
            {
                int rowActual = 1;
                using (SLDocument sl = new SLDocument())
                {
                    sl.SetCellValue("A1", "FOLIO");
                    sl.SetCellValue("B1", "FECHA EMISIÓN");
                    sl.SetCellValue("C1", "CONTRATO");
                    sl.SetCellValue("D1", "LOTE");
                    sl.SetCellValue("E1", "MONTO");
                    sl.SetCellValue("F1", "RECIBIO");
                    sl.SetCellValue("G1", "OBSERVACIÓN");

                    foreach(var item in contexto.LstRegistros)
                    {
                        rowActual++;
                        sl.SetCellValue("A"+rowActual, item.FolioPago);

                        sl.SetCellValue("B" + rowActual, item.FechaEmisionPago);
                        SLStyle style = sl.CreateStyle();
                        style.FormatCode = "dd/MM/yyyy HH:mm:ss";
                        sl.SetCellStyle("B"+rowActual, style);

                        sl.SetCellValue("C" + rowActual, item.FolioContrato);
                        sl.SetCellValue("D" + rowActual, item.IdentificadorLote);
                        sl.SetCellValue("E" + rowActual, item.Monto);
                        sl.SetCellValue("F" + rowActual, item.NombreRecibio);
                        sl.SetCellValue("G" + rowActual, item.Observacion);

                    }


                    sl.SaveAs(saveFileDialog1.FileName);

                    MessageBox.Show("Reporte generado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No se selecciona la ruta de guardado, intentelo nuevamente.", 
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void repCorteCaja_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void repCorteCaja_Shown(object sender, EventArgs e)
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
            Exportar();
        }    

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            contexto.ListarPagosPorFechaEmision(fechaInicio, fechaFin);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            fechaInicio = dtpFecha.Value;
            fechaFin = dtpFechaFin.Value;
            GenerarReporte();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(contexto.LstRegistros!=null && contexto.LstRegistros.Count() > 0)
            {
                Apariencias();
                MessageBox.Show("Reporte generado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       
    }
}
