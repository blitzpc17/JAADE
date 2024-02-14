using CAPALOGICA.LOGICAS.SISTEMA;
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

namespace PRESENTACION.LOTES
{
    public partial class formZonas : Form
    {
        private formZonaLogica contexto;
        private int row = 0;

        public formZonas()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            LimpiarControles();
            InstanciarContextos();
            ListarRegistros();
        }

        private void InstanciarContextos()
        {
            contexto = new formZonaLogica();
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
        }

        private void Apariencias()
        {
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[0].Frozen = true;
            dgvRegistros.Columns[1].HeaderText = "Nombre";
            dgvRegistros.Columns[1].Width = 110;
            dgvRegistros.Columns[3].HeaderText = "No. Manzanas";
            dgvRegistros.Columns[3].Width = 75;
            dgvRegistros.Columns[4].HeaderText = "No. Lotes";
            dgvRegistros.Columns[4].Width = 75;
            dgvRegistros.Columns[5].HeaderText = "Dirección";
            dgvRegistros.Columns[5].Width = 350;
            dgvRegistros.Columns[2].Visible = false;
            tsTotalRegistros.Text = contexto.LstZonaAux.Count.ToString("N0");

            contexto.Column = 1;

        }

        private void ListarRegistros()
        {
            dgvRegistros.DataSource = null;
            contexto.Listar();
            dgvRegistros.DataSource = contexto.LstZonaAux;
            Apariencias();
        }


        private void Guardar()
        {
            if (contexto.ObjZona == null)
            {
                contexto.InstanciarZona();
            }

            contexto.ObjZona.Nombre = txtNombre.Text;
            if (string.IsNullOrEmpty(txtManzanas.Text))
            {
                contexto.ObjZona.NoManzanas = null;
            }
            else
            {
                contexto.ObjZona.NoManzanas = int.Parse(txtManzanas.Text);
            }
            contexto.ObjZona.NoLotes = int.Parse(txtLotes.Text);
            contexto.ObjZona.FechaRegistro = DateTime.Now;
            contexto.ObjZona.Direccion = txtDomicilio.Text;

            contexto.Guardar();

            MessageBox.Show("Registro guardado correctamente.", "Aciso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InicializarForm();

        }

        private void filtrar(int column, string termino)
        {
            if (contexto.Filtrar(column, termino))
            {
                contexto.indexAux = contexto.index;
                dgvRegistros.Rows[contexto.index].Cells[column].Selected = true;
                dgvRegistros.FirstDisplayedScrollingRowIndex = contexto.index;
            }
        }

        private void ordenar(int column)
        {
            contexto.Ordenar(column);
            dgvRegistros.DataSource = contexto.LstZonaAux;
            Apariencias();
        }
        private void setData()
        {
            if (contexto.ObjZona != null)
            {
                txtNombre.Text = contexto.ObjZona.Nombre.ToString();
                txtManzanas.Text = (contexto.ObjZona.NoManzanas==null?null:contexto.ObjZona.NoManzanas.ToString()) ;
                txtLotes.Text = contexto.ObjZona.NoLotes.ToString();
                txtDomicilio.Text = contexto.ObjZona.Direccion;
            }
        }

        private void EliminarRegistro()
        {
            contexto.ObjZona = contexto.Obtener((int)dgvRegistros.CurrentRow.Cells[0].Value);
            contexto.Eliminar(contexto.ObjZona);
            contexto.Guardar();
            MessageBox.Show("Registro eliminado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (dgvRegistros.Rows.Count <= 0) return;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                contexto.index = -1;
                return;
            }

            filtrar(contexto.Column, txtNombre.Text);
        }

        private void formZonas_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void formZonas_Shown(object sender, EventArgs e)
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
            InicializarForm();
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (contexto.Column == e.ColumnIndex) return;
            contexto.Column = e.ColumnIndex;
            ordenar(contexto.Column);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;

            contexto.ObjZona = contexto.Obtener((int)dgvRegistros.CurrentRow.Cells[0].Value);

            setData();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;

            contexto.ObjZona = contexto.Obtener((int)dgvRegistros.CurrentRow.Cells[0].Value);

            setData();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;

            if (MessageBox.Show("Se borrará el registro. ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                EliminarRegistro();
                InicializarForm();
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            ImportarLayout();
        }

        private void ImportarLayout()
        {
            openFileDialog1.Filter = "Archivos de Excel (*.xlsx, *.xls)|*.xlsx;*.xls";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImportarExcel(openFileDialog1.FileName);
                return;
            }

            MessageBox.Show("No se selecciono ningún archivo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void ImportarExcel(string fileName)
        {
            try
            {
                contexto.InstanciarListaImportacion();
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
                        contexto.LstZonaAux.Add(contexto.ObjZona);
                        row++;
                    }

                    foreach (var item in contexto.LstZonaAux)
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
                            contexto.Guardar();

                        }
                        else
                        {
                            //existe
                            contexto.ObjZona.Nombre = item.Nombre;
                            contexto.ObjZona.NoLotes = item.NoLotes;
                            contexto.ObjZona.NoManzanas = item.NoManzanas;
                            contexto.ObjZona.Direccion = item.Direccion;
                            contexto.Guardar();
                        }
                      

                    }

                    MessageBox.Show(
                        "Registros importados correctamente.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    InicializarForm();

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
    }
}
