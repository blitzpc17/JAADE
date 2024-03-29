﻿using CAPALOGICA.LOGICAS.LOTES;
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
        private int? Manzana = null;        

        public formLotes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            try
            {
                cargado = false;
                LimpiarControles();
                InstanciarContexto();
                ListarCatalogos();
                cargado = true;
                contexto.Column = 1;
            }
            catch(Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar cargar los registros. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
       
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
            contexto.ListarEstadosProceso(Enumeraciones.Procesos.LOTE.ToString());
            
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
            txtFechaRegistro.Text = Global.FechaServidor().ToString("dd-MM-yyyy HH:mm:ss");
            Manzana = null;
            dgvRegistros.DataSource = null;
            cbxManzana.Items.Clear();
            cbxManzana.SelectedIndex = -1;
            tsTotalRegistros.Text = @"0";
        }


        private void GuardarLotes(bool multiple)
        {
           try
           {
                string _errorMsj=null;
                if (cbxZona.SelectedValue == null)
                {
                    _errorMsj = "No seleccionado la ZONA del lote.";
                }
                else if (string.IsNullOrEmpty(txtPrecio.Text))
                {
                    _errorMsj = "El campo PRECIO es Obligatorio.";
                }
                else if (string.IsNullOrEmpty(txtMNorte.Text))
                {
                    _errorMsj = "El campo M. NORTE es Obligatorio.";
                }else if (string.IsNullOrEmpty(txtMSur.Text))
                {
                    _errorMsj = "El campo M. SUR es Obligatorio.";
                }
                else if (string.IsNullOrEmpty(txtMOeste.Text))
                {
                    _errorMsj = "El campo M. OESTE es Obligatorio.";
                }
                else if (string.IsNullOrEmpty(txtMEste.Text))
                {
                    _errorMsj = "El campo M. ESTE es Obligatorio.";
                }
                else if (string.IsNullOrEmpty(txtCNorte.Text))
                {
                    _errorMsj = "El campo C. NORTE es Obligatorio.";
                }
                else if (string.IsNullOrEmpty(txtCSur.Text))
                {
                    _errorMsj = "El campo C. SUR es Obligatorio.";
                }
                else if (string.IsNullOrEmpty(txtCEste.Text))
                {
                    _errorMsj = "El campo C. ESTE es Obligatorio.";
                }
                else if (string.IsNullOrEmpty(txtCOeste.Text))
                {
                    _errorMsj = "El campo C. OESTE es Obligatorio.";
                }else if((contexto.ObjZona.NoManzanas!=null && contexto.ObjZona.NoManzanas > 0)&&Manzana==null)
                {
                    _errorMsj = "El campo MANZANA es Obligatorio.";
                }
                 

                if (!string.IsNullOrEmpty(_errorMsj))
                {
                    MessageBox.Show(_errorMsj, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
                        for (int i = _UltimoLote + 1; i <= (_UltimoLote + _NoLotes); i++)
                        {
                            contexto.InstanciarLote();
                            contexto.ObjLote.Identificador = "L/" + i + (cbxManzana.Items.Count > 0 ? " M/" + cbxManzana.SelectedItem : "");
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
                            contexto.ObjLote.Manzana = Manzana;
                            contexto.ObjLote.ESTADOId = contexto.ObtenerEstadoLote(Enumeraciones.EstadosProcesoLote.LIBRE.ToString()).Id;
                            contexto.Guardar();
                        }

                        MessageBox.Show("Lotes generados corectamente.",
                           "Aviso",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    string[] identificadorAnterior = contexto.ObjLote.Identificador.Split(' ');
                    string manzanaAnterior = identificadorAnterior[1].Substring(2);
                    contexto.ObjLote.Identificador = contexto.ObjLote.Identificador.Substring(0, contexto.ObjLote.Identificador.Length - manzanaAnterior.Length) + (cbxManzana.Items.Count > 0 ? cbxManzana.SelectedItem : "");
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

                    MessageBox.Show("Registro guardado correctamente.",
                        "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                InicializarForm();
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
            dgvRegistros.Columns[14].HeaderText = "Manzana";
            dgvRegistros.Columns[14].DefaultCellStyle.Format = "N0";
            dgvRegistros.Columns[14].Width = 110;
            dgvRegistros.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRegistros.Columns[15].Visible= false;
            dgvRegistros.Columns[16].HeaderText = "Estado";
            dgvRegistros.Columns[16].Width = 110;
            tsTotalRegistros.Text = contexto.LstLoteAux.Count.ToString("N0");

            
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
            try
            {
                if (!string.IsNullOrEmpty(txtLotesDinamicos.Text))
                {
                    MessageBox.Show("No se puede guardar el registro porque tiene definido un número de lotes dinámicos.",
                           "Advertencia",
                           MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                GuardarLotes(false);
              

                
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
            int zonaId = (int)cbxZona.SelectedValue;
            contexto.ObtenerZona(zonaId);
            ListarLotesAsociadosZona(zonaId);
            cbxManzana.Items.Clear();            
            if (contexto.ObjZona.NoManzanas != null)
            {                 
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

        private void cbxManzana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargado) return;
            if (cbxManzana.SelectedIndex != -1)
            {
                Manzana = (int)cbxManzana.SelectedItem;
            }
            else
            {
                Manzana = null;
            }

        }
    }
}
