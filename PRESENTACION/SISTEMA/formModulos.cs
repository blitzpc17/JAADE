﻿using CAPALOGICA.LOGICAS.SISTEMA;
using PRESENTACION.BUSQUEDA;
using PRESENTACION.UTILERIAS;
using System;
using System.Windows.Forms;

namespace PRESENTACION.SISTEMA
{
    public partial class formModulos : Form
    {
        private int ModuloPadreSeleccionadoId = -1;
        private formModulosLogica contexto;
        public formModulos()
        {
            InitializeComponent(); 
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }

        private void InicializarForm()
        {
            try
            {
                LimpiarControles();
                InstanciarContextos();
                ListarRegistros();
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar cargar el modulo. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
           
        }

        private void InstanciarContextos()
        {
            contexto = new formModulosLogica();
        }

        private void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);          
        }

        private void Apariencias()
        {
            dgvRegistros.Columns[0].Visible = false;
            dgvRegistros.Columns[1].HeaderText = "NOMBRE";
            dgvRegistros.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[2].HeaderText = "RUTAS";
            dgvRegistros.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[3].Visible = false;
            dgvRegistros.Columns[4].HeaderText = "MODULO PADRE";
            dgvRegistros.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRegistros.Columns[5].Visible = false;

            tsTotalRegistros.Text = contexto.LstModuloAux.Count.ToString("N0");

            contexto.Column = 1;

        }

        private void ListarRegistros()
        {
            dgvRegistros.DataSource = null;
            contexto.ListarModulos();
            dgvRegistros.DataSource = contexto.LstModuloAux;
            Apariencias();
        }


        private void Guardar()
        {
            try
            {
                if (contexto.ObjModulo == null)
                {
                    contexto.InstanciarRol();
                }

                contexto.ObjModulo.Nombre = txtNombre.Text;
                contexto.ObjModulo.Icono = txtIcono.Text;
                contexto.ObjModulo.Ruta = txtRuta.Text;
                if (ModuloPadreSeleccionadoId == -1)
                {
                    contexto.ObjModulo.MODULOId = null;
                }
                else
                {
                    contexto.ObjModulo.MODULOId = ModuloPadreSeleccionadoId;
                }

                contexto.Guardar();

                MessageBox.Show("Registro guardado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void filtrar(int column, string termino)
        {
            if (column != contexto.index)
            {
                ordenar(column);
            }

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
            dgvRegistros.DataSource = contexto.LstModuloAux;
            Apariencias();
        }

        private void EliminarRegistro()
        {
            contexto.ObjModulo = contexto.Obtener((int)dgvRegistros.CurrentRow.Cells[0].Value);
            contexto.Eliminar(contexto.ObjModulo);
            contexto.Guardar();
            MessageBox.Show("Registro eliminado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InicializarForm();
        }

        private void Modificar()
        {
            if (dgvRegistros.DataSource == null) return;
            int registroSeleccionadoId = (int)dgvRegistros.CurrentRow.Cells[0].Value;
            contexto.ObjModuloData = contexto.ObtenerData(registroSeleccionadoId);
            contexto.ObjModulo = contexto.Obtener(registroSeleccionadoId);
            if (contexto.ObjModulo != null)
            {
                txtIcono.Text = contexto.ObjModuloData.Icono;
                txtModulo.Text = contexto.ObjModuloData.ModuloPadre;
                txtRuta.Text = contexto.ObjModuloData.Ruta;
                txtNombre.Text = contexto.ObjModuloData.Nombre;
            }
        }

        private void formModulos_Load(object sender, EventArgs e)
        {
            InicializarForm();
        }

        private void formModulos_Shown(object sender, EventArgs e)
        {
            ThemeConfig.ThemeControls(this);
            MaximizeBox = false;
            txtRuta.CharacterCasing = CharacterCasing.Normal;
            txtIcono.CharacterCasing = CharacterCasing.Normal;
        }

        private void btnModulo_Click(object sender, EventArgs e)
        {
            var busModulo = new busModulos();
            busModulo.ShowDialog();
            if (busModulo.ObjEntidad == null)
            {
                MessageBox.Show("No se ha seleccionado ningín registro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtModulo.Text = busModulo.ObjEntidad.Nombre;
            ModuloPadreSeleccionadoId = busModulo.ObjEntidad.Id;

            


        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void dgvRegistros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegistros.DataSource == null) return;
            if (contexto.Column != e.ColumnIndex)
            {
                contexto.Column = e.ColumnIndex;
                txtBuscar.Clear();
            }
        }

        private void dgvRegistros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Modificar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarForm();
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
    }
}
