namespace PRESENTACION.PAGOS
{
    partial class formPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPago));
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEstadoPagoCliente = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtZona = new System.Windows.Forms.TextBox();
            this.txtClaveLote = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFechaReimpresion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFolioContrato = new System.Windows.Forms.TextBox();
            this.txtRecibePago = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFechaEmision = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNoPago = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMontoRecibido = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuscarContrato = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscarPago = new System.Windows.Forms.Button();
            this.txtFolioPago = new System.Windows.Forms.TextBox();
            this.txtDiaPago = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImportar
            // 
            this.btnImportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Image = global::PRESENTACION.Properties.Resources.ticket;
            this.btnImportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportar.Location = new System.Drawing.Point(12, 398);
            this.btnImportar.MaximumSize = new System.Drawing.Size(110, 40);
            this.btnImportar.MinimumSize = new System.Drawing.Size(110, 40);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(110, 40);
            this.btnImportar.TabIndex = 20;
            this.btnImportar.Text = "     Recibo";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::PRESENTACION.Properties.Resources.cancelar;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(281, 398);
            this.btnCancelar.MaximumSize = new System.Drawing.Size(110, 40);
            this.btnCancelar.MinimumSize = new System.Drawing.Size(110, 40);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 40);
            this.btnCancelar.TabIndex = 40;
            this.btnCancelar.Text = "     Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = global::PRESENTACION.Properties.Resources.guardar_el_archivo;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(165, 398);
            this.btnGuardar.MaximumSize = new System.Drawing.Size(110, 40);
            this.btnGuardar.MinimumSize = new System.Drawing.Size(110, 40);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 40);
            this.btnGuardar.TabIndex = 30;
            this.btnGuardar.Text = "     Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtEstadoPagoCliente);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtZona);
            this.groupBox1.Controls.Add(this.txtClaveLote);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtFechaReimpresion);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtFolioContrato);
            this.groupBox1.Controls.Add(this.txtRecibePago);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtFechaEmision);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtNoPago);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMontoRecibido);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnBuscarContrato);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnBuscarPago);
            this.groupBox1.Controls.Add(this.txtFolioPago);
            this.groupBox1.Controls.Add(this.txtDiaPago);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 380);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del pago";
            // 
            // txtEstadoPagoCliente
            // 
            this.txtEstadoPagoCliente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstadoPagoCliente.Location = new System.Drawing.Point(131, 262);
            this.txtEstadoPagoCliente.Name = "txtEstadoPagoCliente";
            this.txtEstadoPagoCliente.ReadOnly = true;
            this.txtEstadoPagoCliente.Size = new System.Drawing.Size(225, 23);
            this.txtEstadoPagoCliente.TabIndex = 120;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 262);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 23);
            this.label10.TabIndex = 190;
            this.label10.Text = "Comp. Pago:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtZona
            // 
            this.txtZona.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZona.Location = new System.Drawing.Point(193, 88);
            this.txtZona.MaxLength = 10;
            this.txtZona.Name = "txtZona";
            this.txtZona.ReadOnly = true;
            this.txtZona.Size = new System.Drawing.Size(163, 23);
            this.txtZona.TabIndex = 60;
            // 
            // txtClaveLote
            // 
            this.txtClaveLote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaveLote.Location = new System.Drawing.Point(131, 88);
            this.txtClaveLote.MaxLength = 10;
            this.txtClaveLote.Name = "txtClaveLote";
            this.txtClaveLote.ReadOnly = true;
            this.txtClaveLote.Size = new System.Drawing.Size(57, 23);
            this.txtClaveLote.TabIndex = 50;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(247, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 23);
            this.label9.TabIndex = 186;
            this.label9.Text = "de cada mes.";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFechaReimpresion
            // 
            this.txtFechaReimpresion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaReimpresion.Location = new System.Drawing.Point(131, 291);
            this.txtFechaReimpresion.Name = "txtFechaReimpresion";
            this.txtFechaReimpresion.ReadOnly = true;
            this.txtFechaReimpresion.Size = new System.Drawing.Size(225, 23);
            this.txtFechaReimpresion.TabIndex = 140;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 23);
            this.label8.TabIndex = 183;
            this.label8.Text = "F. Reimpresión:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFolioContrato
            // 
            this.txtFolioContrato.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolioContrato.Location = new System.Drawing.Point(131, 59);
            this.txtFolioContrato.MaxLength = 9;
            this.txtFolioContrato.Name = "txtFolioContrato";
            this.txtFolioContrato.Size = new System.Drawing.Size(185, 23);
            this.txtFolioContrato.TabIndex = 30;
            this.txtFolioContrato.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolioContrato_KeyDown);
            this.txtFolioContrato.Leave += new System.EventHandler(this.txtFolioContrato_Leave);
            // 
            // txtRecibePago
            // 
            this.txtRecibePago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecibePago.Location = new System.Drawing.Point(131, 233);
            this.txtRecibePago.Name = "txtRecibePago";
            this.txtRecibePago.ReadOnly = true;
            this.txtRecibePago.Size = new System.Drawing.Size(225, 23);
            this.txtRecibePago.TabIndex = 130;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 23);
            this.label7.TabIndex = 179;
            this.label7.Text = "Recibe:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFechaEmision
            // 
            this.txtFechaEmision.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaEmision.Location = new System.Drawing.Point(131, 204);
            this.txtFechaEmision.Name = "txtFechaEmision";
            this.txtFechaEmision.ReadOnly = true;
            this.txtFechaEmision.Size = new System.Drawing.Size(225, 23);
            this.txtFechaEmision.TabIndex = 100;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 23);
            this.label6.TabIndex = 177;
            this.label6.Text = "Fecha Emisión:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNoPago
            // 
            this.txtNoPago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoPago.Location = new System.Drawing.Point(131, 146);
            this.txtNoPago.Name = "txtNoPago";
            this.txtNoPago.ReadOnly = true;
            this.txtNoPago.Size = new System.Drawing.Size(225, 23);
            this.txtNoPago.TabIndex = 80;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 23);
            this.label5.TabIndex = 175;
            this.label5.Text = "No Pago:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMontoRecibido
            // 
            this.txtMontoRecibido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoRecibido.Location = new System.Drawing.Point(131, 175);
            this.txtMontoRecibido.Name = "txtMontoRecibido";
            this.txtMontoRecibido.Size = new System.Drawing.Size(225, 23);
            this.txtMontoRecibido.TabIndex = 90;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 23);
            this.label4.TabIndex = 173;
            this.label4.Text = "Monto($):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBuscarContrato
            // 
            this.btnBuscarContrato.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarContrato.Image = global::PRESENTACION.Properties.Resources.lupa;
            this.btnBuscarContrato.Location = new System.Drawing.Point(328, 59);
            this.btnBuscarContrato.Name = "btnBuscarContrato";
            this.btnBuscarContrato.Size = new System.Drawing.Size(28, 28);
            this.btnBuscarContrato.TabIndex = 40;
            this.btnBuscarContrato.UseVisualStyleBackColor = true;
            this.btnBuscarContrato.Click += new System.EventHandler(this.btnBuscarContrato_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 23);
            this.label3.TabIndex = 170;
            this.label3.Text = "Contrato:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBuscarPago
            // 
            this.btnBuscarPago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarPago.Image = global::PRESENTACION.Properties.Resources.lupa;
            this.btnBuscarPago.Location = new System.Drawing.Point(328, 30);
            this.btnBuscarPago.Name = "btnBuscarPago";
            this.btnBuscarPago.Size = new System.Drawing.Size(28, 28);
            this.btnBuscarPago.TabIndex = 20;
            this.btnBuscarPago.UseVisualStyleBackColor = true;
            this.btnBuscarPago.Click += new System.EventHandler(this.btnBuscarPago_Click);
            // 
            // txtFolioPago
            // 
            this.txtFolioPago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolioPago.Location = new System.Drawing.Point(131, 30);
            this.txtFolioPago.MaxLength = 11;
            this.txtFolioPago.Name = "txtFolioPago";
            this.txtFolioPago.Size = new System.Drawing.Size(185, 23);
            this.txtFolioPago.TabIndex = 10;
            this.txtFolioPago.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolioPago_KeyDown);
            this.txtFolioPago.Leave += new System.EventHandler(this.txtFolioPago_Leave);
            // 
            // txtDiaPago
            // 
            this.txtDiaPago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiaPago.Location = new System.Drawing.Point(131, 117);
            this.txtDiaPago.Name = "txtDiaPago";
            this.txtDiaPago.ReadOnly = true;
            this.txtDiaPago.Size = new System.Drawing.Size(110, 23);
            this.txtDiaPago.TabIndex = 70;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(10, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 23);
            this.label13.TabIndex = 163;
            this.label13.Text = "Folio:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 23);
            this.label1.TabIndex = 162;
            this.label1.Text = "Día pago:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // formPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 450);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(419, 489);
            this.Name = "formPago";
            this.Text = "formPago";
            this.Load += new System.EventHandler(this.formPago_Load);
            this.Shown += new System.EventHandler(this.formPago_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtZona;
        private System.Windows.Forms.TextBox txtClaveLote;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFechaReimpresion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFolioContrato;
        private System.Windows.Forms.TextBox txtRecibePago;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFechaEmision;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNoPago;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMontoRecibido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBuscarContrato;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuscarPago;
        private System.Windows.Forms.TextBox txtFolioPago;
        private System.Windows.Forms.TextBox txtDiaPago;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEstadoPagoCliente;
        private System.Windows.Forms.Label label10;
    }
}