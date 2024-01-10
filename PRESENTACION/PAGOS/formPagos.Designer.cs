namespace PRESENTACION.PAGOS
{
    partial class formPagos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPagos));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsTotalRegistros = new System.Windows.Forms.ToolStripLabel();
            this.txtBuscar = new Controls.Controles.Txt();
            this.dgvRegistros = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTotalPagos = new Controls.Controles.Txt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFechaRegistro = new Controls.Controles.Txt();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCliente = new Controls.Controles.Txt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumeroReferencia = new Controls.Controles.Txt();
            this.txtZona = new Controls.Controles.Txt();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLote = new Controls.Controles.Txt();
            this.txtMontoExcedePlazo = new Controls.Controles.Txt();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMontoRecibido = new Controls.Controles.Txt();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnLote = new System.Windows.Forms.Button();
            this.btnBusPago = new System.Windows.Forms.Button();
            this.btnCliente = new System.Windows.Forms.Button();
            this.txtUsuarioRecibePago = new Controls.Controles.Txt();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEstado = new Controls.Controles.Txt();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPagosRealizados = new Controls.Controles.Txt();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMontoMensualidad = new Controls.Controles.Txt();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMontoAtrasado = new Controls.Controles.Txt();
            this.label14 = new System.Windows.Forms.Label();
            this.txtTotalPagar = new Controls.Controles.Txt();
            this.label15 = new System.Windows.Forms.Label();
            this.txtNoPagoActual = new Controls.Controles.Txt();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSaldoFavor = new Controls.Controles.Txt();
            this.label17 = new System.Windows.Forms.Label();
            this.txtSaldoContra = new Controls.Controles.Txt();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPrecioLote = new Controls.Controles.Txt();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistros)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Controls.Add(this.txtBuscar);
            this.groupBox2.Controls.Add(this.dgvRegistros);
            this.groupBox2.Location = new System.Drawing.Point(12, 236);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1135, 367);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Registros";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(386, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 23);
            this.label6.TabIndex = 22;
            this.label6.Text = "Buscar:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsTotalRegistros});
            this.toolStrip1.Location = new System.Drawing.Point(3, 339);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1129, 25);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(102, 22);
            this.toolStripLabel1.Text = "Total de Registros:";
            // 
            // tsTotalRegistros
            // 
            this.tsTotalRegistros.Name = "tsTotalRegistros";
            this.tsTotalRegistros.Size = new System.Drawing.Size(13, 22);
            this.tsTotalRegistros.Text = "0";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(507, 30);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(225, 23);
            this.txtBuscar.TabIndex = 10;
            // 
            // dgvRegistros
            // 
            this.dgvRegistros.AllowUserToAddRows = false;
            this.dgvRegistros.AllowUserToDeleteRows = false;
            this.dgvRegistros.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRegistros.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistros.Location = new System.Drawing.Point(13, 59);
            this.dgvRegistros.MultiSelect = false;
            this.dgvRegistros.Name = "dgvRegistros";
            this.dgvRegistros.ReadOnly = true;
            this.dgvRegistros.RowHeadersVisible = false;
            this.dgvRegistros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRegistros.Size = new System.Drawing.Size(1107, 277);
            this.dgvRegistros.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtPrecioLote);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtSaldoContra);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtSaldoFavor);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtNoPagoActual);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtTotalPagar);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtMontoAtrasado);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtMontoMensualidad);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtPagosRealizados);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtEstado);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtUsuarioRecibePago);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtMontoRecibido);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMontoExcedePlazo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNumeroReferencia);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnLote);
            this.groupBox1.Controls.Add(this.btnBusPago);
            this.groupBox1.Controls.Add(this.txtLote);
            this.groupBox1.Controls.Add(this.txtZona);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtTotalPagos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCliente);
            this.groupBox1.Controls.Add(this.txtFechaRegistro);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCliente);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1132, 218);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Encabezado";
            // 
            // txtTotalPagos
            // 
            this.txtTotalPagos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPagos.Location = new System.Drawing.Point(516, 88);
            this.txtTotalPagos.Name = "txtTotalPagos";
            this.txtTotalPagos.ReadOnly = true;
            this.txtTotalPagos.Size = new System.Drawing.Size(225, 23);
            this.txtTotalPagos.TabIndex = 120;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(395, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 23);
            this.label1.TabIndex = 52;
            this.label1.Text = "No. Pagos:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFechaRegistro
            // 
            this.txtFechaRegistro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaRegistro.Location = new System.Drawing.Point(131, 177);
            this.txtFechaRegistro.Name = "txtFechaRegistro";
            this.txtFechaRegistro.ReadOnly = true;
            this.txtFechaRegistro.Size = new System.Drawing.Size(225, 23);
            this.txtFechaRegistro.TabIndex = 90;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 23);
            this.label5.TabIndex = 14;
            this.label5.Text = "Fecha Registro:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 23);
            this.label4.TabIndex = 12;
            this.label4.Text = "Lote:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Cliente:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCliente
            // 
            this.txtCliente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.Location = new System.Drawing.Point(131, 30);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(185, 23);
            this.txtCliente.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Número Referencia:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNumeroReferencia
            // 
            this.txtNumeroReferencia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroReferencia.Location = new System.Drawing.Point(131, 59);
            this.txtNumeroReferencia.Name = "txtNumeroReferencia";
            this.txtNumeroReferencia.ReadOnly = true;
            this.txtNumeroReferencia.Size = new System.Drawing.Size(185, 23);
            this.txtNumeroReferencia.TabIndex = 30;
            // 
            // txtZona
            // 
            this.txtZona.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZona.Location = new System.Drawing.Point(131, 119);
            this.txtZona.Name = "txtZona";
            this.txtZona.ReadOnly = true;
            this.txtZona.Size = new System.Drawing.Size(225, 23);
            this.txtZona.TabIndex = 70;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 23);
            this.label7.TabIndex = 54;
            this.label7.Text = "Zona:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLote
            // 
            this.txtLote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLote.Location = new System.Drawing.Point(131, 88);
            this.txtLote.Name = "txtLote";
            this.txtLote.ReadOnly = true;
            this.txtLote.Size = new System.Drawing.Size(185, 23);
            this.txtLote.TabIndex = 50;
            // 
            // txtMontoExcedePlazo
            // 
            this.txtMontoExcedePlazo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoExcedePlazo.Location = new System.Drawing.Point(901, 117);
            this.txtMontoExcedePlazo.Name = "txtMontoExcedePlazo";
            this.txtMontoExcedePlazo.ReadOnly = true;
            this.txtMontoExcedePlazo.Size = new System.Drawing.Size(225, 23);
            this.txtMontoExcedePlazo.TabIndex = 190;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(780, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 23);
            this.label8.TabIndex = 58;
            this.label8.Text = "Monto Exc. Pzo:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(780, 175);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 23);
            this.label9.TabIndex = 60;
            this.label9.Text = "Monto Recibido:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMontoRecibido
            // 
            this.txtMontoRecibido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoRecibido.Location = new System.Drawing.Point(901, 175);
            this.txtMontoRecibido.Name = "txtMontoRecibido";
            this.txtMontoRecibido.Size = new System.Drawing.Size(225, 23);
            this.txtMontoRecibido.TabIndex = 210;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Image = global::PRESENTACION.Properties.Resources.ticket;
            this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerar.Location = new System.Drawing.Point(12, 609);
            this.btnGenerar.MaximumSize = new System.Drawing.Size(110, 40);
            this.btnGenerar.MinimumSize = new System.Drawing.Size(110, 40);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(110, 40);
            this.btnGenerar.TabIndex = 50;
            this.btnGenerar.Text = "     Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::PRESENTACION.Properties.Resources.cancelar;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(1037, 609);
            this.btnCancelar.MaximumSize = new System.Drawing.Size(110, 40);
            this.btnCancelar.MinimumSize = new System.Drawing.Size(110, 40);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 40);
            this.btnCancelar.TabIndex = 40;
            this.btnCancelar.Text = "     Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = global::PRESENTACION.Properties.Resources.guardar_el_archivo;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(921, 609);
            this.btnGuardar.MaximumSize = new System.Drawing.Size(110, 40);
            this.btnGuardar.MinimumSize = new System.Drawing.Size(110, 40);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 40);
            this.btnGuardar.TabIndex = 30;
            this.btnGuardar.Text = "     Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLote
            // 
            this.btnLote.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLote.Image = global::PRESENTACION.Properties.Resources.lupa;
            this.btnLote.Location = new System.Drawing.Point(332, 88);
            this.btnLote.Name = "btnLote";
            this.btnLote.Size = new System.Drawing.Size(24, 24);
            this.btnLote.TabIndex = 60;
            this.btnLote.UseVisualStyleBackColor = true;
            this.btnLote.Click += new System.EventHandler(this.btnLote_Click);
            // 
            // btnBusPago
            // 
            this.btnBusPago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBusPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBusPago.Image = global::PRESENTACION.Properties.Resources.lupa;
            this.btnBusPago.Location = new System.Drawing.Point(332, 59);
            this.btnBusPago.Name = "btnBusPago";
            this.btnBusPago.Size = new System.Drawing.Size(24, 24);
            this.btnBusPago.TabIndex = 40;
            this.btnBusPago.UseVisualStyleBackColor = true;
            this.btnBusPago.Click += new System.EventHandler(this.btnUsuarioSolicita_Click);
            // 
            // btnCliente
            // 
            this.btnCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCliente.Image = global::PRESENTACION.Properties.Resources.lupa;
            this.btnCliente.Location = new System.Drawing.Point(332, 30);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(24, 24);
            this.btnCliente.TabIndex = 20;
            this.btnCliente.UseVisualStyleBackColor = true;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // txtUsuarioRecibePago
            // 
            this.txtUsuarioRecibePago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioRecibePago.Location = new System.Drawing.Point(131, 148);
            this.txtUsuarioRecibePago.Name = "txtUsuarioRecibePago";
            this.txtUsuarioRecibePago.ReadOnly = true;
            this.txtUsuarioRecibePago.Size = new System.Drawing.Size(225, 23);
            this.txtUsuarioRecibePago.TabIndex = 80;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 23);
            this.label10.TabIndex = 111;
            this.label10.Text = "Recibio:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEstado
            // 
            this.txtEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstado.Location = new System.Drawing.Point(516, 30);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEstado.Size = new System.Drawing.Size(225, 23);
            this.txtEstado.TabIndex = 100;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(395, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 23);
            this.label11.TabIndex = 113;
            this.label11.Text = "Estado:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPagosRealizados
            // 
            this.txtPagosRealizados.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPagosRealizados.Location = new System.Drawing.Point(516, 117);
            this.txtPagosRealizados.Name = "txtPagosRealizados";
            this.txtPagosRealizados.ReadOnly = true;
            this.txtPagosRealizados.Size = new System.Drawing.Size(225, 23);
            this.txtPagosRealizados.TabIndex = 130;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(395, 117);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 23);
            this.label12.TabIndex = 131;
            this.label12.Text = "Pagos realizados:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMontoMensualidad
            // 
            this.txtMontoMensualidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoMensualidad.Location = new System.Drawing.Point(901, 59);
            this.txtMontoMensualidad.Name = "txtMontoMensualidad";
            this.txtMontoMensualidad.ReadOnly = true;
            this.txtMontoMensualidad.Size = new System.Drawing.Size(225, 23);
            this.txtMontoMensualidad.TabIndex = 170;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(780, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 23);
            this.label13.TabIndex = 133;
            this.label13.Text = "Mensualidad ($):";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMontoAtrasado
            // 
            this.txtMontoAtrasado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoAtrasado.Location = new System.Drawing.Point(901, 88);
            this.txtMontoAtrasado.Name = "txtMontoAtrasado";
            this.txtMontoAtrasado.ReadOnly = true;
            this.txtMontoAtrasado.Size = new System.Drawing.Size(225, 23);
            this.txtMontoAtrasado.TabIndex = 180;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(780, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 23);
            this.label14.TabIndex = 135;
            this.label14.Text = "Monto atrasado:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotalPagar
            // 
            this.txtTotalPagar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPagar.Location = new System.Drawing.Point(901, 146);
            this.txtTotalPagar.Name = "txtTotalPagar";
            this.txtTotalPagar.ReadOnly = true;
            this.txtTotalPagar.Size = new System.Drawing.Size(225, 23);
            this.txtTotalPagar.TabIndex = 200;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(780, 146);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(115, 23);
            this.label15.TabIndex = 137;
            this.label15.Text = "Total a pagar:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNoPagoActual
            // 
            this.txtNoPagoActual.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoPagoActual.Location = new System.Drawing.Point(516, 146);
            this.txtNoPagoActual.Name = "txtNoPagoActual";
            this.txtNoPagoActual.ReadOnly = true;
            this.txtNoPagoActual.Size = new System.Drawing.Size(225, 23);
            this.txtNoPagoActual.TabIndex = 140;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(395, 146);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(115, 23);
            this.label16.TabIndex = 139;
            this.label16.Text = "Pago Actual:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSaldoFavor
            // 
            this.txtSaldoFavor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoFavor.Location = new System.Drawing.Point(516, 175);
            this.txtSaldoFavor.Name = "txtSaldoFavor";
            this.txtSaldoFavor.ReadOnly = true;
            this.txtSaldoFavor.Size = new System.Drawing.Size(225, 23);
            this.txtSaldoFavor.TabIndex = 150;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(395, 176);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(115, 23);
            this.label17.TabIndex = 182;
            this.label17.Text = "Saldo a Favor:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSaldoContra
            // 
            this.txtSaldoContra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoContra.ForeColor = System.Drawing.Color.Red;
            this.txtSaldoContra.Location = new System.Drawing.Point(901, 30);
            this.txtSaldoContra.Name = "txtSaldoContra";
            this.txtSaldoContra.ReadOnly = true;
            this.txtSaldoContra.Size = new System.Drawing.Size(225, 23);
            this.txtSaldoContra.TabIndex = 160;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(780, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(115, 23);
            this.label18.TabIndex = 184;
            this.label18.Text = "Saldo en contra:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrecioLote
            // 
            this.txtPrecioLote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioLote.Location = new System.Drawing.Point(516, 59);
            this.txtPrecioLote.Name = "txtPrecioLote";
            this.txtPrecioLote.ReadOnly = true;
            this.txtPrecioLote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPrecioLote.Size = new System.Drawing.Size(225, 23);
            this.txtPrecioLote.TabIndex = 110;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(395, 59);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(115, 23);
            this.label19.TabIndex = 202;
            this.label19.Text = "Precio Lote:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // formPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 661);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1175, 650);
            this.Name = "formPagos";
            this.Text = "formPagos";
            this.Load += new System.EventHandler(this.formPagos_Load);
            this.Shown += new System.EventHandler(this.formPagos_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistros)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tsTotalRegistros;
        private Controls.Controles.Txt txtBuscar;
        private System.Windows.Forms.DataGridView dgvRegistros;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.Controles.Txt txtTotalPagos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBusPago;
        private System.Windows.Forms.Button btnCliente;
        private Controls.Controles.Txt txtFechaRegistro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Controls.Controles.Txt txtCliente;
        private System.Windows.Forms.Label label2;
        private Controls.Controles.Txt txtNumeroReferencia;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnLote;
        private Controls.Controles.Txt txtLote;
        private Controls.Controles.Txt txtZona;
        private System.Windows.Forms.Label label7;
        private Controls.Controles.Txt txtMontoRecibido;
        private System.Windows.Forms.Label label9;
        private Controls.Controles.Txt txtMontoExcedePlazo;
        private System.Windows.Forms.Label label8;
        private Controls.Controles.Txt txtEstado;
        private System.Windows.Forms.Label label11;
        private Controls.Controles.Txt txtUsuarioRecibePago;
        private System.Windows.Forms.Label label10;
        private Controls.Controles.Txt txtTotalPagar;
        private System.Windows.Forms.Label label15;
        private Controls.Controles.Txt txtMontoAtrasado;
        private System.Windows.Forms.Label label14;
        private Controls.Controles.Txt txtMontoMensualidad;
        private System.Windows.Forms.Label label13;
        private Controls.Controles.Txt txtPagosRealizados;
        private System.Windows.Forms.Label label12;
        private Controls.Controles.Txt txtNoPagoActual;
        private System.Windows.Forms.Label label16;
        private Controls.Controles.Txt txtSaldoContra;
        private System.Windows.Forms.Label label18;
        private Controls.Controles.Txt txtSaldoFavor;
        private System.Windows.Forms.Label label17;
        private Controls.Controles.Txt txtPrecioLote;
        private System.Windows.Forms.Label label19;
    }
}