namespace numFact
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonProvisional = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewFE = new System.Windows.Forms.DataGridView();
            this.dataGridViewVentas = new System.Windows.Forms.DataGridView();
            this.buttonProcesarFacturasElectronicas = new System.Windows.Forms.Button();
            this.buttonProcesarVentas = new System.Windows.Forms.Button();
            this.buttonIniciarDia = new System.Windows.Forms.Button();
            this.buttonProcesarTransaccionFE = new System.Windows.Forms.Button();
            this.textBoxTransaccionCodigo = new System.Windows.Forms.TextBox();
            this.panelOculto = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCerrar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVentas)).BeginInit();
            this.panelOculto.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonProvisional
            // 
            this.buttonProvisional.Location = new System.Drawing.Point(2, 80);
            this.buttonProvisional.Name = "buttonProvisional";
            this.buttonProvisional.Size = new System.Drawing.Size(111, 58);
            this.buttonProvisional.TabIndex = 15;
            this.buttonProvisional.Text = "procesar facturas no pasadas";
            this.buttonProvisional.UseVisualStyleBackColor = true;
            this.buttonProvisional.Click += new System.EventHandler(this.buttonProvisional_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(514, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Documentos xml pendientes:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(217, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ventas pendientes:";
            // 
            // dataGridViewFE
            // 
            this.dataGridViewFE.AllowUserToAddRows = false;
            this.dataGridViewFE.AllowUserToDeleteRows = false;
            this.dataGridViewFE.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFE.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewFE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFE.Location = new System.Drawing.Point(509, 113);
            this.dataGridViewFE.MultiSelect = false;
            this.dataGridViewFE.Name = "dataGridViewFE";
            this.dataGridViewFE.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFE.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewFE.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewFE.ShowEditingIcon = false;
            this.dataGridViewFE.Size = new System.Drawing.Size(259, 150);
            this.dataGridViewFE.TabIndex = 12;
            // 
            // dataGridViewVentas
            // 
            this.dataGridViewVentas.AllowUserToAddRows = false;
            this.dataGridViewVentas.AllowUserToDeleteRows = false;
            this.dataGridViewVentas.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewVentas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVentas.Location = new System.Drawing.Point(215, 113);
            this.dataGridViewVentas.MultiSelect = false;
            this.dataGridViewVentas.Name = "dataGridViewVentas";
            this.dataGridViewVentas.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewVentas.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewVentas.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewVentas.ShowEditingIcon = false;
            this.dataGridViewVentas.Size = new System.Drawing.Size(278, 150);
            this.dataGridViewVentas.TabIndex = 11;
            // 
            // buttonProcesarFacturasElectronicas
            // 
            this.buttonProcesarFacturasElectronicas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(128)))));
            this.buttonProcesarFacturasElectronicas.FlatAppearance.BorderSize = 0;
            this.buttonProcesarFacturasElectronicas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProcesarFacturasElectronicas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonProcesarFacturasElectronicas.ForeColor = System.Drawing.Color.Black;
            this.buttonProcesarFacturasElectronicas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonProcesarFacturasElectronicas.Location = new System.Drawing.Point(509, 272);
            this.buttonProcesarFacturasElectronicas.Name = "buttonProcesarFacturasElectronicas";
            this.buttonProcesarFacturasElectronicas.Size = new System.Drawing.Size(259, 48);
            this.buttonProcesarFacturasElectronicas.TabIndex = 10;
            this.buttonProcesarFacturasElectronicas.Text = "3.- Procesar FE";
            this.buttonProcesarFacturasElectronicas.UseVisualStyleBackColor = false;
            this.buttonProcesarFacturasElectronicas.Click += new System.EventHandler(this.buttonProcesarFacturasElectronicas_Click);
            // 
            // buttonProcesarVentas
            // 
            this.buttonProcesarVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(128)))));
            this.buttonProcesarVentas.FlatAppearance.BorderSize = 0;
            this.buttonProcesarVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProcesarVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonProcesarVentas.ForeColor = System.Drawing.Color.Black;
            this.buttonProcesarVentas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonProcesarVentas.Location = new System.Drawing.Point(215, 272);
            this.buttonProcesarVentas.Name = "buttonProcesarVentas";
            this.buttonProcesarVentas.Size = new System.Drawing.Size(278, 48);
            this.buttonProcesarVentas.TabIndex = 9;
            this.buttonProcesarVentas.Text = "2.- Procesar ventas";
            this.buttonProcesarVentas.UseVisualStyleBackColor = false;
            this.buttonProcesarVentas.Click += new System.EventHandler(this.buttonProcesarVentas_Click);
            // 
            // buttonIniciarDia
            // 
            this.buttonIniciarDia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(128)))));
            this.buttonIniciarDia.FlatAppearance.BorderSize = 0;
            this.buttonIniciarDia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonIniciarDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIniciarDia.ForeColor = System.Drawing.Color.Black;
            this.buttonIniciarDia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonIniciarDia.Location = new System.Drawing.Point(21, 272);
            this.buttonIniciarDia.Name = "buttonIniciarDia";
            this.buttonIniciarDia.Size = new System.Drawing.Size(181, 48);
            this.buttonIniciarDia.TabIndex = 8;
            this.buttonIniciarDia.Text = "1.- Iniciar día";
            this.buttonIniciarDia.UseVisualStyleBackColor = false;
            this.buttonIniciarDia.Click += new System.EventHandler(this.buttonIniciarDia_Click);
            // 
            // buttonProcesarTransaccionFE
            // 
            this.buttonProcesarTransaccionFE.Location = new System.Drawing.Point(2, 51);
            this.buttonProcesarTransaccionFE.Name = "buttonProcesarTransaccionFE";
            this.buttonProcesarTransaccionFE.Size = new System.Drawing.Size(179, 23);
            this.buttonProcesarTransaccionFE.TabIndex = 16;
            this.buttonProcesarTransaccionFE.Text = "Procesar transacción FE";
            this.buttonProcesarTransaccionFE.UseVisualStyleBackColor = true;
            this.buttonProcesarTransaccionFE.Click += new System.EventHandler(this.buttonProcesarTransaccionFE_Click);
            // 
            // textBoxTransaccionCodigo
            // 
            this.textBoxTransaccionCodigo.Location = new System.Drawing.Point(2, 25);
            this.textBoxTransaccionCodigo.Name = "textBoxTransaccionCodigo";
            this.textBoxTransaccionCodigo.Size = new System.Drawing.Size(179, 20);
            this.textBoxTransaccionCodigo.TabIndex = 17;
            // 
            // panelOculto
            // 
            this.panelOculto.Controls.Add(this.button1);
            this.panelOculto.Controls.Add(this.textBoxTransaccionCodigo);
            this.panelOculto.Controls.Add(this.buttonProcesarTransaccionFE);
            this.panelOculto.Controls.Add(this.buttonProvisional);
            this.panelOculto.Location = new System.Drawing.Point(10, 88);
            this.panelOculto.Name = "panelOculto";
            this.panelOculto.Size = new System.Drawing.Size(191, 174);
            this.panelOculto.TabIndex = 18;
            this.panelOculto.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(18, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(410, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "Procesamiento de información LTG V 3.0.2";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.panel2.Controls.Add(this.buttonCerrar);
            this.panel2.Controls.Add(this.label3);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(-1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(802, 56);
            this.panel2.TabIndex = 20;
            // 
            // buttonCerrar
            // 
            this.buttonCerrar.FlatAppearance.BorderSize = 0;
            this.buttonCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCerrar.ForeColor = System.Drawing.Color.Black;
            this.buttonCerrar.Location = new System.Drawing.Point(721, 3);
            this.buttonCerrar.Name = "buttonCerrar";
            this.buttonCerrar.Size = new System.Drawing.Size(75, 48);
            this.buttonCerrar.TabIndex = 20;
            this.buttonCerrar.Text = "X";
            this.buttonCerrar.UseVisualStyleBackColor = true;
            this.buttonCerrar.Click += new System.EventHandler(this.buttonCerrar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(120, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "tablas";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 342);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelOculto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewFE);
            this.Controls.Add(this.dataGridViewVentas);
            this.Controls.Add(this.buttonProcesarFacturasElectronicas);
            this.Controls.Add(this.buttonProcesarVentas);
            this.Controls.Add(this.buttonIniciarDia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Procesamiento de información LTG V 2.2";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVentas)).EndInit();
            this.panelOculto.ResumeLayout(false);
            this.panelOculto.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonProvisional;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewFE;
        private System.Windows.Forms.DataGridView dataGridViewVentas;
        private System.Windows.Forms.Button buttonProcesarFacturasElectronicas;
        private System.Windows.Forms.Button buttonProcesarVentas;
        private System.Windows.Forms.Button buttonIniciarDia;
        private System.Windows.Forms.Button buttonProcesarTransaccionFE;
        private System.Windows.Forms.TextBox textBoxTransaccionCodigo;
        private System.Windows.Forms.Panel panelOculto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonCerrar;
        private System.Windows.Forms.Button button1;
    }
}

