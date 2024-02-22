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
                }

                saveFileDialog1.Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*";
                saveFileDialog1.Title = "Guardar archivo de Excel";
                saveFileDialog1.ShowDialog();
                string rutaArchivo = saveFileDialog1.FileName;

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
            throw new NotImplementedException();
        }

        private void GenerarLayoutLotes(int opc, string rutaArchivo)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private void ImportarPagos(string fileName)
        {
            throw new NotImplementedException();
        }

        private void ImportarContratos(string fileName)
        {
            throw new NotImplementedException();
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

        private void ImportarLotes(string fileName)
        {
            throw new NotImplementedException();
        }

        private void ImportarClientes(string fileName)
        {
            throw new NotImplementedException();
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
