using CAPALOGICA.LOGICAS.LOTES;
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

namespace PRESENTACION.LOTES
{
    public partial class formLotes : Form
    {
        private formLotesLogica contexto;
        private bool cargado = false;

        public formLotes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            cargado = false;
            LimpiarControles();
            InstanciarContexto();            
            ListarCatalogos();
            cargado = true;
        }

        private void InstanciarContexto()
        {
            contexto = new formLotesLogica();
        }

        private void ListarCatalogos()
        {
            contexto.ListarCatalogos();
            cbxZona.DisplayMember = "Nombre";
            cbxZona.ValueMember = "Id";
            cbxZona.DataSource = contexto.LstZona;           
            cbxZona.SelectedIndex = -1;
            cbxManzana.Items.Clear();
            
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            txtFechaRegistro.Text = Global.FechaServidor().ToString("dd-MM-yyyy HH:mm:ss");
        }


        private void GuardarLotes(bool multiple)
        {
           
            DateTime _FechaRegistro = Global.FechaServidor();
            int _UltimoLote = contexto.ObtenerUltimoLote((int)cbxZona.SelectedValue);
            if (multiple)
            {
                if (!Global.EsValorEntero(txtLotesDinamicos.Text))
                {
                    MessageBox.Show("Valor inválido, ingrese la cantidad de lotes a generar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                int _NoLotes = int.Parse(txtLotesDinamicos.Text);
                int _LimiteLotes = contexto.ObjZona.NoLotes;

                if ((_UltimoLote + _NoLotes) <= _LimiteLotes)
                {
                    for (int i = _UltimoLote+1; i <= _NoLotes; i++)
                    {
                        contexto.InstanciarLote();
                        contexto.ObjLote.Identificador = "L/"+i+(cbxManzana.Items.Count>0? " M/"+cbxManzana.SelectedItem : "");
                        contexto.ObjLote.MNorte = Convert.ToDecimal(txtMNorte.Text);
                        contexto.ObjLote.MSur = Convert.ToDecimal(txtMSur.Text);
                        contexto.ObjLote.MEste = Convert.ToDecimal(txtMEste.Text);
                        contexto.ObjLote.MOeste = Convert.ToDecimal(txtMOeste.Text);
                        contexto.ObjLote.CNorte = txtCNorte.Text;
                        contexto.ObjLote.CEste = txtCEste.Text;
                        contexto.ObjLote.COeste = txtCOeste.Text;
                        contexto.ObjLote.CSur = txtCSur.Text;
                        contexto.ObjLote.Precio = Convert.ToDecimal(txtPrecio.Text);
                        contexto.ObjLote.FechaRegistro = _FechaRegistro;
                        contexto.ObjLote.ZONAId = (int)cbxZona.SelectedValue;
                        contexto.ObjLote.Manzana = (int)cbxManzana.SelectedItem;
                        contexto.Guardar();
                    }

                    MessageBox.Show("Lotes generados corectamente.",
                       "Aviso",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);

                    InicializarForm();
                }
                else
                {
                    MessageBox.Show("El número de lotes a crear es superior a la cantidad de lotes que tiene la zona seleccionada.\r\n Verifique su ifnromación",
                        "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                
            }
            else
            {
                string [] identificadorAnterior = contexto.ObjLote.Identificador.Split(' ');
                string manzanaAnterior = identificadorAnterior[1].Substring(2);
                contexto.ObjLote.Identificador = contexto.ObjLote.Identificador.Substring(0, contexto.ObjLote.Identificador.Length-manzanaAnterior.Length)+(cbxManzana.Items.Count>0?cbxManzana.SelectedItem:"");
                contexto.ObjLote.MNorte = Convert.ToDecimal(txtMNorte.Text);
                contexto.ObjLote.MSur = Convert.ToDecimal(txtMSur.Text);
                contexto.ObjLote.MEste = Convert.ToDecimal(txtMEste.Text);
                contexto.ObjLote.MOeste = Convert.ToDecimal(txtMOeste.Text);
                contexto.ObjLote.CNorte = txtCNorte.Text;
                contexto.ObjLote.CEste = txtCEste.Text;
                contexto.ObjLote.COeste = txtCOeste.Text;
                contexto.ObjLote.CSur = txtCSur.Text;
                contexto.ObjLote.Precio = Convert.ToDecimal(txtPrecio.Text);
                contexto.ObjLote.FechaRegistro = _FechaRegistro;
                contexto.ObjLote.ZONAId = (int)cbxZona.SelectedValue;
                contexto.ObjLote.Manzana = (int)cbxManzana.SelectedItem;
                contexto.Guardar();
            }
        }

        private void ListarLotesAsociadosZona(int ZonaId)
        {
            contexto.ListarLotesZonas(ZonaId);
            dgvRegistros.DataSource = contexto.LstLoteAux;
            Apariencias();
        }

        private void Apariencias()
        {
            if (dgvRegistros.DataSource == null) return;
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[0].Frozen = true;
            dgvRegistros.Columns[1].HeaderText = "Identificador";
            dgvRegistros.Columns[1].Width = 110;
            dgvRegistros.Columns[1].Frozen = true;
            dgvRegistros.Columns[2].HeaderText = "Zona";
            dgvRegistros.Columns[2].Width = 200;
            dgvRegistros.Columns[2].Frozen = true;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].HeaderText = "Medida Norte";
            dgvRegistros.Columns[4].Width = 110;
            dgvRegistros.Columns[4].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[5].HeaderText = "Medida Sur";
            dgvRegistros.Columns[5].Width = 110;
            dgvRegistros.Columns[5].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[6].HeaderText = "Medida Este";
            dgvRegistros.Columns[6].Width = 110;
            dgvRegistros.Columns[6].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[7].HeaderText = "Medida Oeste";
            dgvRegistros.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[7].Width = 110;
            dgvRegistros.Columns[7].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[8].HeaderText = "Colinada Norte";
            dgvRegistros.Columns[8].Width = 110;
            dgvRegistros.Columns[9].HeaderText = "Colinada Sur"; 
            dgvRegistros.Columns[9].Width = 110;
            dgvRegistros.Columns[10].HeaderText = "Colinada Este";
            dgvRegistros.Columns[10].Width = 110;
            dgvRegistros.Columns[11].HeaderText = "Colinada Oeste";
            dgvRegistros.Columns[11].Width = 110;
            dgvRegistros.Columns[12].HeaderText = "Fecha Registro";
            dgvRegistros.Columns[12].Width = 135;
            dgvRegistros.Columns[13].HeaderText = "Precio";
            dgvRegistros.Columns[13].DefaultCellStyle.Format = "N2";
            dgvRegistros.Columns[13].Width = 110;
            dgvRegistros.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tsTotalRegistros.Text = contexto.LstLoteAux.Count.ToString("N0");

            contexto.Column = 1;
        }


        private void SetDataLote(int idLote)
        {
            contexto.ObtenerLote(idLote);
            contexto.ObjLote = contexto.Obtener(idLote);
            if (contexto.ObjLoteData == null) return;

            txtMNorte.Text = contexto.ObjLoteData.MNorte.ToString("N2");
            txtMEste.Text = contexto.ObjLoteData.MEste.ToString("N2");
            txtMOeste.Text = contexto.ObjLoteData.MOeste.ToString("N2");
            txtMSur.Text = contexto.ObjLoteData.MSur.ToString("N2");

            txtCNorte.Text = contexto.ObjLoteData.CNorte;
            txtCEste.Text = contexto.ObjLoteData.CEste;
            txtCOeste.Text = contexto.ObjLoteData.COeste;
            txtCSur.Text = contexto.ObjLoteData.CSur;

            txtFechaRegistro.Text = contexto.ObjLoteData.FechaRegistro.ToString("dd/MM/yyyy HH:mm:sss");
            txtIdentificador.Text = contexto.ObjLoteData.Identificador;
            txtPrecio.Text = contexto.ObjLoteData.Precio.ToString("N2");
            cbxManzana.SelectedItem = contexto.ObjLoteData.Manzana;
            cbxZona.SelectedValue = contexto.ObjLoteData.ZonaId;

        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLotesDinamicos.Text))
            {
                MessageBox.Show("No se puede guardar el registro porque tiene definido un número de lotes dinámicos.",
                       "Advertencia",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }

            GuardarLotes(false);
            MessageBox.Show("Registro guardado correctamente.",
                      "Aviso",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

            InicializarForm();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void formLotes_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void formLotes_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            this.MaximizeBox = false;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDataLote((int)dgvRegistros.CurrentRow.Cells[0].Value);
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (contexto.Column == e.ColumnIndex) return;
            contexto.Column = e.ColumnIndex;
            ordenar(contexto.Column);
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SetDataLote((int)dgvRegistros.CurrentRow.Cells[0].Value);
        }

        private void btnModulo_Click(object sender, EventArgs e)
        {
            GuardarLotes(true);
        }

        private void cbxZona_SelectedValueChanged(object sender, EventArgs e)
        {            
            if (!cargado) return;
            contexto.ObtenerZona((int)cbxZona.SelectedValue);
            ListarLotesAsociadosZona(1);
            if (contexto.ObjZona.NoManzanas != null)
            {
                cbxManzana.Items.Clear();   
                for (int i = 1; i<= contexto.ObjZona.NoManzanas; i++)
                {
                    cbxManzana.Items.Add(i);
                }
            }         
             
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
            dgvRegistros.DataSource = contexto.LstLoteAux;
            Apariencias();
        }





    }
}
