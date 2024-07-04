using numFact.dao;
using numFact.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace numFact
{
    public partial class AsignacionDescuento : Form
    {

        private PixelDao pixelDAO;

        private Decimal valorTotalDescuento;

        private String idTabla;

        public Form1 FormularioPrincipal;

        public AsignacionDescuento(String idTabla, Decimal valorTotalDescuento)
        {
            InitializeComponent();
            this.valorTotalDescuento = valorTotalDescuento;
            this.idTabla = idTabla;
            pixelDAO = new PixelDao();
            iniciarColumnasGrid();
        }

        private void iniciarColumnasGrid()
        {
            dataGridViewDescuentos.ColumnCount = 4;
            dataGridViewDescuentos.Columns[0].Width = 200;
            dataGridViewDescuentos.Columns[0].Name = "Cédula persona descontada";
            dataGridViewDescuentos.Columns[1].Width = 350;
            dataGridViewDescuentos.Columns[1].Name = "Nombre persona descontada";
            dataGridViewDescuentos.Columns[2].Width = 100;
            dataGridViewDescuentos.Columns[2].Name = "Valor de descuento";
            dataGridViewDescuentos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewDescuentos.Columns[3].Visible = false;
            dataGridViewDescuentos.Columns[3].Width = 0;
            dataGridViewDescuentos.Columns[3].Name = "Tipo";

            DataGridViewButtonColumn eliminarButtonColumn = new DataGridViewButtonColumn();
            eliminarButtonColumn.Name = "eliminar_column";
            eliminarButtonColumn.Text = "X";
            eliminarButtonColumn.Width = 48;

            eliminarButtonColumn.HeaderText = "";
            eliminarButtonColumn.UseColumnTextForButtonValue = true;
            dataGridViewDescuentos.Columns.Add(eliminarButtonColumn);
            dataGridViewDescuentos.Columns[4].Width = 50;

            dataGridViewDescuentos.RowTemplate.Height = 48;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void AsignacionDescuento_Load(object sender, EventArgs e)
        {
            labelTotalDescuento.Text = valorTotalDescuento.ToString();
            buttonImprimirDocumento.Enabled = false;
            buttonFinalizar.Enabled = false;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            ((Form1)this.Owner).SeguirProcesandoVentas = false;
            this.Close();
        }

        private void buttonFinalizar_Click(object sender, EventArgs e)
        {
            using (Procesando proc = new Procesando(() => { ejecutarProcesoCompletoConBloqueo(); }))
            {
                proc.ShowDialog();
            }
        }


        private void ejecutarProcesoCompletoConBloqueo()
        {
            try
            {
                //valido que ya hayan sido agregados los valores
                if ((Decimal.Parse(labelTotalAsignado.Text) - Decimal.Parse(labelTotalDescuento.Text)) != 0)
                {
                    throw new Exception("Asigne el descuento antes de finalizar y procesar la información");
                }
                //saco todos los datos ingresados de descuentos
                List<Descuento> descuentos = new List<Descuento>();
                DataGridViewRowCollection filas = null;
                this.Invoke(new Action(() => { filas = dataGridViewDescuentos.Rows; }));
                foreach (DataGridViewRow registro in filas)
                {
                    Descuento descuento = new Descuento();
                    descuento.Cedula = registro.Cells[0].Value.ToString();
                    descuento.Nombre = registro.Cells[1].Value.ToString();
                    descuento.Valor = Decimal.Parse(registro.Cells[2].Value.ToString());
                    descuento.Tipo = registro.Cells[3].Value.ToString();
                    descuentos.Add(descuento);
                }

                pixelDAO.EjecutarProcesoVentasMasDescuentosMasNomina(idTabla, descuentos);
                this.Invoke(new Action(() => { MessageBox.Show(this, "Ventas cargadas y descuentos asignados", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information); }));

                this.Invoke(new Action(() => { ((Form1)this.Owner).SeguirProcesandoVentas = false; }));
                this.Invoke(new Action(() => { this.Close(); }));
                this.Invoke(new Action(() => { ((Form1)this.Owner).CargarVentasPendientes(); }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() => { MessageBox.Show(this, "Error cargando ventas y asignando descuentos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }));
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            using (Procesando proc = new Procesando(() => { onBuscarClick(); }))
            {
                proc.ShowDialog();
            }
        }

        private void onBuscarClick()
        {
            try
            {
                //se limpia el combo de personas
                this.Invoke(new Action(() => { comboBoxPersona.Items.Clear(); }));

                //dato de búsqueda y validación
                String filtro = textBoxBusqueda.Text;

                if (filtro.Trim().Length == 0)
                {
                    throw new Exception("ingrese parte del nombre o cédula de la persona que desea buscar");
                }

                //se agrega las personas encontradas según el sistema de nómina
                List<RhPersona> personas = pixelDAO.GetPersonaFromNominaByFiltros(filtro);

                foreach (RhPersona pers in personas)
                {
                    this.Invoke(new Action(() => { comboBoxPersona.Items.Add(pers); }));
                }
                this.Invoke(new Action(() => { MessageBox.Show(this, "Se encontraron " + personas.Count + " registros coincidentes", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information); }));
                this.Invoke(new Action(() => { comboBoxPersona.Focus(); }));
                if (comboBoxPersona.Items.Count > 0)
                {
                    this.Invoke(new Action(() => { comboBoxPersona.SelectedIndex = 0; }));
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() => { MessageBox.Show(this, ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
            }
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //validar que haya una persona ingresada
                if (comboBoxPersona.SelectedItem == null)
                {
                    throw new Exception("persona no seleccionada para descuento");
                }

                //validar que haya una cantidad ingresada
                if (textBoxValor.Text.Length == 0)
                {
                    throw new Exception("ingrese valor para descuento");
                }

                //calculo de asignación total y diferencias en base a lo ya ingresado y lo nuevo por ingresar
                Decimal asignado = Decimal.Parse(textBoxValor.Text) + Decimal.Parse(labelTotalAsignado.Text);
                Decimal diferencia = Decimal.Parse(labelTotalDescuento.Text) - asignado;

                //e valor debe ser mayor a 1 centavo
                if (Decimal.Parse(textBoxValor.Text) < 0.01m)
                {
                    throw new Exception("El valor que desea ingresar debe ser mayor a 0.01");
                }

                //la diferencia no puede ser menor a 0
                if (diferencia < 0)
                {
                    throw new Exception("El valor que desea ingresar supera el valor total de descuento");
                }

                //se crea la nueva fila
                Object[] row = new Object[4];
                RhPersona persona = ((RhPersona)comboBoxPersona.SelectedItem);

                //valido que la persona no pueda ser ingresada 2 veces
                foreach (DataGridViewRow registro in dataGridViewDescuentos.Rows)
                {
                    if (persona.PersonaId.Equals(registro.Cells[0].Value))
                    {
                        throw new Exception("La persona ya tiene un descuento asignado");
                    }
                }

                //asigna valores a cada columna de la fila
                row[0] = persona.PersonaId;
                row[1] = persona.PersonaNombre;
                row[2] = textBoxValor.Text;
                row[3] = persona.PersonaTipo;
                dataGridViewDescuentos.Rows.Add(row);

                //se asignan los valores calculados a los label
                labelTotalAsignado.Text = (asignado).ToString();
                labelDiferencia.Text = (diferencia).ToString();

                if (diferencia == 0)
                {
                    buttonImprimirDocumento.Enabled = true;
                    buttonImprimirDocumento.Focus();
                }
                else
                {
                    buttonImprimirDocumento.Enabled = false;
                    textBoxBusqueda.Focus();
                }

                //se muestra el mensaje y pasa el foco al campo de búsqueda
                MessageBox.Show(this, "descuento agregado", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewDescuentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                if (dataGridViewDescuentos.Columns[e.ColumnIndex].Name.Equals("eliminar_column"))
                {

                    //recalculo los totales de acuerdo a lo que elimino
                    Decimal asignado = Decimal.Parse(labelTotalAsignado.Text) - Decimal.Parse(dataGridViewDescuentos.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Decimal diferencia = Decimal.Parse(labelTotalDescuento.Text) - asignado;
                    labelTotalAsignado.Text = (asignado).ToString();
                    labelDiferencia.Text = (diferencia).ToString();

                    //elimino la fila
                    dataGridViewDescuentos.Rows.RemoveAt(e.RowIndex);

                    if (diferencia == 0)
                    {
                        buttonImprimirDocumento.Enabled = true;
                    }
                    else
                    {
                        buttonImprimirDocumento.Enabled = false;
                    }

                    //muestra el mensaje de eliminación y pasa el foco al campo de búsqueda
                    MessageBox.Show(this, "descuento eliminado", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxBusqueda.Focus();
                }
            }
        }

        private void textBoxBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonBuscar_Click(null, null);
            }
        }

        private void textBoxValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonAgregar_Click(null, null);
            }
        }

        private void buttonImprimirDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                String formato = "----------------------------------------\n";
                formato = formato + "DESCUENTOS POR FALTANTE DE CAJA\n";
                formato = formato + "----------------------------------------\n\n";

                formato = formato + "COD. DIA VENTA: " + idTabla + "\n\n";

                formato = formato + "MEDIANTE EL PRESENTE SE DEJA CONSTANCIA DEL DESCUENTO APLICADO A LAS SIGUIENTES PERSONAS:\n\n";


                foreach (DataGridViewRow registro in dataGridViewDescuentos.Rows)
                {
                    formato = formato + "CEDULA: " + registro.Cells[0].Value + "\n";
                    formato = formato + "NOMBRE: " + registro.Cells[1].Value + "\n";
                    formato = formato + "DESCUENTO: " + registro.Cells[2].Value + "\n\n";
                    formato = formato + "FIRMA: ______________________________\n\n\n";
                }

                formato = formato + "----------------------------------------\n";
                formato = formato + "NOTA: ADJUNTAR ESTE DOCUMENTO AL CIERRE DEL DIA, FIRMADO POR EL PERSONAL ARRIBA LISTADO.";

                print(formato);
                buttonFinalizar.Enabled = true;
            }
            catch (Exception ex)
            {
                buttonFinalizar.Enabled = false;
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void print(String formato)
        {
            try
            {

                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    e1.Graphics.DrawString(formato, new Font("Times New Roman", 12), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height - 1));
                };
                p.PrinterSettings.PrinterName = GetConfigProp("PrinterName");
                p.Print();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private String GetConfigProp(String key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

    }
}
