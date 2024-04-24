using CAPALOGICA.LOGICAS.PAGOS;
using PRESENTACION.BUSQUEDA;
using PRESENTACION.UTILERIAS;
using SpreadsheetLight;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PRESENTACION.PAGOS
{
    public partial class formClientes : Form
    {
        private formClientesLogica contexto;
        private bool cargado = false;
        private busClientes bus;
        private int row = 0;//rowimport

        public formClientes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
        }       

        public void InicializarModulo()
        {
            try
            {
                cargado = false;
                LimpiarControles();
                InstanciarContexto();
                ListarCatalogos();
                txtClave.Text = @"NUEVO";
                cargado = true;
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

        public void LimpiarControles()
        {
            ThemeConfig.LimpiarControles(this);
        }

        public void ListarCatalogos()
        {
            contexto.ListarCatalogos();
            cbxEstado.DataSource = contexto.LstEstado;
            cbxEstado.DisplayMember = "Nombre";
            cbxEstado.ValueMember = "Id";
            cbxEstado.SelectedIndex = -1;

            cbxTipoContacto.DataSource = contexto.LstTipos;
            cbxTipoContacto.DisplayMember = "key";
            cbxTipoContacto.ValueMember = "value";
            cbxTipoContacto.SelectedIndex = -1; 

        }

        public void InstanciarContexto()
        {
            contexto = new formClientesLogica();
        }

        public void Guardar()
        {
            try
            {
                if (cbxEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("El campo ESTADO es OBLIGATORIO.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (contexto.ObjClienteData == null)
                {
                    contexto.InstanciarPersona();
                    contexto.InstanciarCliente();
                    contexto.ObjCliente.Clave = Global.ObtenerFolio(Enumeraciones.ProcesoFolio.CLIENTE);
                }

                contexto.ObjPersona.Nombres = txtNombre.Text;
                contexto.ObjPersona.Apellidos = txtApellidos.Text;
                if(dtpFechaNacimiento.Value.Year!= Global.FechaServidor().Year)
                {
                    contexto.ObjPersona.FechaNacimiento = dtpFechaNacimiento.Value;
                }                
                contexto.ObjPersona.Curp = txtCurp.Text; 

                contexto.ObjCliente.ESTADOId = (int)cbxEstado.SelectedValue;

                contexto.Guardar();


                MessageBox.Show("Registro guardado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InicializarModulo();
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
      
        private void SetDataCliente()
        {
        
            if (contexto.ObjCliente != null && contexto.ObjClienteData != null)
            {
                txtNombre.Text = contexto.ObjClienteData.Nombres;
                txtApellidos.Text = contexto.ObjClienteData.Apellidos;
                if (contexto.ObjClienteData.FechaNacimiento!=null)
                {
                    dtpFechaNacimiento.Value = Convert.ToDateTime(contexto.ObjClienteData.FechaNacimiento);
                }                
                txtCurp.Text = contexto.ObjClienteData.Curp;
               
                txtClave.Text = contexto.ObjClienteData.Clave;
                cbxEstado.SelectedValue = contexto.ObjClienteData.EstadoId;

                ListarClientesSocio(contexto.ObjClienteData.Clave);
                ListarAgendaCliente(contexto.ObjClienteData.Clave);
            }
            else
            {
                MessageBox.Show("No se encontro el registro seleccionado. Vuelva a intentarlo.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void formClientes_Load(object sender, EventArgs e)
        {
            InicializarModulo();
        }

        private void formClientes_Shown(object sender, EventArgs e)
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

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //eliminar contacto
            if (dgvAgenda.DataSource == null) return;
            if (contexto.ObjClienteData == null)
            {
                MessageBox.Show("No ha seleccionado ningún cliente.",
                   "Advertencia",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return;
            }
            EliminarContacto((int)dgvAgenda.CurrentRow.Cells[0].Value);
            ListarAgendaCliente(contexto.ObjClienteData.Clave);
        }

        private void EliminarContacto(int contactoId)
        {
            try
            {
                contexto.ObtenerContacto(contactoId);
                contexto.EliminarContacto();

                MessageBox.Show("Contacto eliminado de la agenda del cliente correctamente.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);                
            }
            catch(Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar eliminar el registro.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }

        private void btnImportar_Click(object sender, EventArgs e)
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

        private void ImportarExcel(string path)
        {
            try
            {
                contexto.InstanciarListasImportacion();
                //leer excel
                using (SLDocument sl = new SLDocument(path))
                {
                    //listar clientes
                    contexto.ListarClientes();

                    row = 2;

                    contexto.InstanciarListasImportacion();

                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(row, 1)))
                    {
                        contexto.InstanciarObjImportacion();
                        contexto.ObjImportacion.Cliente = sl.GetCellValueAsString(row, 1);
                        contexto.ObjImportacion.Socio = sl.GetCellValueAsString(row, 2);
                        contexto.ObjImportacion.Telefono = sl.GetCellValueAsString(row, 3);
                        contexto.ObjImportacion.Correo = sl.GetCellValueAsString(row, 4);
                        contexto.ObjImportacion.Direccion = sl.GetCellValueAsString(row, 5);
                        contexto.LstImportacion.Add(contexto.ObjImportacion);
                        row++;
                    }

                    foreach (var item in contexto.LstImportacion)
                    {
                        string[] nombreCliente = item.Cliente.Split(',');

                        contexto.ObjClienteData = contexto.LstClientesAux.FirstOrDefault(x => x.Nombres == nombreCliente[0].Trim() && x.Apellidos == nombreCliente[1].Trim());

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

                    MessageBox.Show(
                        "Registros importados correctamente.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    InicializarModulo();

                }
            
            }
            catch(Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar importar los registros. Ejecuón pausada en el row:"+row,
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                
            }

        }

        private void GenerarLayout()
        {
            return;
            throw new NotImplementedException();
        }

        private void txtClave_Click(object sender, EventArgs e)
        {
            if (txtClave.Text.Equals("NUEVO"))
            {
                txtClave.Clear();
                txtClave.Focus();
            }
                
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtClave.Text.Length < 5 && e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Clave de cliente no válida.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(e.KeyCode == Keys.Enter)
            {
                BuscarClientePorClave(txtClave.Text);
            }
        }

        private void BuscarClientePorClave(string clave)
        {
            contexto.BuscarClientePorClave(clave);
            SetDataCliente();
        }

        private void dgvAgenda_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddSocio_Click(object sender, EventArgs e)
        {
            try
            {
                if (contexto.ObjClienteData == null)
                {
                    MessageBox.Show("Debe cargar la información un cliente para poder agregar Socios.",
                        "Advertencia", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtSocio.Text))
                {
                    MessageBox.Show("Falta ingresar el nombre del socio.",
                       "Advertencia", MessageBoxButtons.OK,
                       MessageBoxIcon.Warning);
                    return;
                }

                contexto.InstanciarSocio();//AQUI SUSTITUIR ESE INSTANCIAR SOCIOS POR EL QUE SE OCUPA EN LA IMPORTACION
                contexto.ObjSocios.Nombre = txtSocio.Text;                
                contexto.GuardarSocio();
               

                ListarClientesSocio(contexto.ObjClienteData.Clave);
            }
            catch(Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar guardar el registro. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            

        }

        private void ListarClientesSocio(string clave)
        {
            contexto.ListarClientesSocio(clave);
            SetSociosClientes();
            txtSocio.Clear();  
        }

        private void SetSociosClientes()
        {
            dgvSocios.DataSource = contexto.LstClientesSocios;
            AparienciasSociosClientes();
            tsTotalSocios.Text = dgvSocios.RowCount.ToString("N0");
        }

        private void AparienciasSociosClientes()
        {
            dgvSocios.Columns[0].Visible = false;
            dgvSocios.Columns[1].HeaderText = "Nombre";
            dgvSocios.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            bus = new busClientes();
            bus.ShowDialog();

            if (bus.ObjEntidad == null)
            {
                MessageBox.Show("No se ha seleccionado ningín registro.", "Advertencia", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            contexto.ObjClienteData = contexto.ObtenerDataCliente(bus.ObjEntidad.Id);
            contexto.ObjCliente = contexto.ObtenerCliente(bus.ObjEntidad.Id);
            contexto.ObjPersona = contexto.ObtenerPersona(contexto.ObjCliente.PERSONAId);
            SetDataCliente();
        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //eliminarsocio
            try
            {
                if (contexto.ObjClienteData == null)
                {
                    MessageBox.Show("No ha seleccionado ningún cliente.",
                   "Advertencia",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                    return;
                }
                contexto.ObtenerSocio((int)dgvSocios.CurrentRow.Cells[0].Value);
                contexto.EliminarSocio();

                MessageBox.Show("Socio del cliente eliminado correctamente.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ListarClientesSocio(contexto.ObjClienteData.Clave);
            }
            catch (Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar eliminar el registro.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnTipo_Click(object sender, EventArgs e)
        {
            try
            {
                if (contexto.ObjClienteData == null)
                {
                    MessageBox.Show("Debe seleccionar un cliente para poder abrir el directorio de contacto.", "Advertencia",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbxTipoContacto.SelectedIndex == -1)
                {
                    MessageBox.Show("No ha seleccionado el tipo de contacto.", "Advertencia",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtDato.Text))
                {
                    MessageBox.Show("No ha ingresado el dato de contacto.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                contexto.InstanciarContactoAgenda();
                contexto.ObjAgenda.Tipo = (int)cbxTipoContacto.SelectedValue;
                contexto.ObjAgenda.Valor = txtDato.Text;
                contexto.GuardarContactoAgenda();
                MessageBox.Show("Se ha guardado el registro en agenda del cliente correctamente",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                ListarAgendaCliente(contexto.ObjClienteData.Clave);
                cbxTipoContacto.SelectedIndex = -1;
                txtDato.Clear();
            }
            catch(Exception ex)
            {
                Global.GuardarExcepcion(ex, Name);
                MessageBox.Show(
                    "Ocurrió un error al intentar guardar el registro. Intentelo nuevamente.",
                    "Error en la operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
           
        }

        private void ListarAgendaCliente(string claveCliente)
        {
            contexto.ListarAgendaContacto(claveCliente);
            SetAgendaCliente();
        }

        private void SetAgendaCliente()
        {
            dgvAgenda.DataSource = contexto.LstAgendasCliente;
            AparienciasAgendaCliente();
            tsTotalContacto.Text = dgvAgenda.RowCount.ToString("N0");
        }

        private void AparienciasAgendaCliente()
        {
            dgvAgenda.Columns[0].Visible = false;//id
            dgvAgenda.Columns[1].Visible = false;//tipoid
            dgvAgenda.Columns[2].HeaderText = "TIPO";
            dgvAgenda.Columns[3].HeaderText = "DATO";
            dgvAgenda.Columns[4].Visible = false;//relacion id
            dgvAgenda.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAgenda.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void txtClave_Leave(object sender, EventArgs e)
        {
            if (!cargado) return;
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                if (contexto.ObjCliente == null)
                {
                    txtClave.Text = "NUEVO";
                }
                else
                {
                    txtClave.Text = contexto.ObjCliente.Clave;
                }
            }
        }
    }
}
