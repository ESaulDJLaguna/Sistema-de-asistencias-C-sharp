using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sistema_de_asistencias.Logica;
using Sistema_de_asistencias.Datos;

namespace Sistema_de_asistencias.Presentacion
{
    public partial class Personal : UserControl
    {
        int idCargo;
        public Personal()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            LocalizarDtvCargos();
            // El panel para agregar nuevos cargos no será visible
            PanelCargos.Visible = false;
            // El panel inferior de paginano, no será visible
            PanelPaginado.Visible = false;
            // El panel de registros, será visible...
            PanelRegistros.Visible = true;
            // ... y ocupará el total del panel
            PanelRegistros.Dock = DockStyle.Fill;
            // El botón para guardar personal será visible
            btnGuardarPersonal.Visible = true;
            // El botón para guardar cambios en el personal no se verá
            btnGuardarCambiosPersonal.Visible = false;
            // Método que limpia los textBox
            Limpiar();
        }
        // Método para posicionar el dataGridView de Cargos
        private void LocalizarDtvCargos()
        {
            // Posiciona el listado de Cargos en una coordenada del 'User Control'
            dataListadoCargos.Location = new Point(txtSueldoHora.Location.X, txtSueldoHora.Location.Y);
            // Modificamos el tamaño del listado de Cargos
            dataListadoCargos.Size = new Size(469, 641);
            // Que sea visible el listado de Cargos
            dataListadoCargos.Visible = true;
            // Ocultamos el label 'Sueldo Por Hora'
            lblSueldo.Visible = false;
            // Se ocultan los botones de Guardar (personal)
            PanelBtnGuardarPersonal.Visible = false;
        }
        // Método para limpiar los textBox
        private void Limpiar()
        {
            // Limpiamos el textBox de nombre
            // Otra forma de lipiar sería: txtNombre.Text = "";
            txtNombre.Clear();
            txtIdentificacion.Clear();
            txtCargo.Clear();
            txtSueldoHora.Clear();
            BuscarCargos();
        }

        private void btnGuardarPersonal_Click(object sender, EventArgs e)
        {

        }
        private void InsertarPersonal()
        {
            LPersonal parametrosPasar = new LPersonal();
            DPersonal funcionesEjecutar = new DPersonal();
            parametrosPasar.Nombres = txtNombre.Text;
            parametrosPasar.Identificacion = txtIdentificacion.Text;
            parametrosPasar.Pais = cbxPais.Text;
            //cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
            //cmd.Parameters.AddWithValue("@Identificación", parametros.Identificacion);
            //cmd.Parameters.AddWithValue("@País", parametros.Pais);
            //cmd.Parameters.AddWithValue("@id_cargo", parametros.Id_cargo);
            //cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
        }
        private void InsertarCargo()
        {
            if(!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if(!string.IsNullOrEmpty(txtSueldoHoraG.Text))
                {
                    LCargos parametros = new LCargos();
                    DCargos funciones = new DCargos();
                    parametros.Cargo = txtCargoG.Text;
                    parametros.SueldoPorHora = Convert.ToDouble(txtSueldoHoraG.Text);
                    if (funciones.InsertarCargo(parametros) == true)
                    {
                        txtCargo.Clear();
                        BuscarCargos();
                        PanelCargos.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agregue el Sueldo", "Falta el Sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agregue el Cargo", "Falta el Cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BuscarCargos()
        {
            DataTable dt = new DataTable();
            DCargos funciones = new DCargos();
            funciones.BuscarCargos(ref dt, txtCargo.Text);
            dataListadoCargos.DataSource = dt;
            Bases.DiseñoDtv(ref dataListadoCargos);
            dataListadoCargos.Columns[1].Visible = false;
            dataListadoCargos.Columns[3].Visible = false;
            dataListadoCargos.Visible = true;
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            BuscarCargos();
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            PanelCargos.Visible = true;
            PanelCargos.Dock = DockStyle.Fill;
            PanelCargos.BringToFront();
            btnGuardarCargo.Visible = true;
            btnGuardarCambiosCargo.Visible = false;
            txtCargoG.Clear();
            txtSueldoHoraG.Clear();
        }

        private void btnGuardarCargo_Click(object sender, EventArgs e)
        {
            InsertarCargo();
        }

        private void txtSueldoHoraG_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoHoraG, e);
        }

        private void txtSueldoHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoHora, e);
        }
        // Cuando se seleccione una celda del listado de Cargos, queremos recuperar su id
        private void dataListadoCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si se hace clic en una columna del listado de Cargos y coincide con el índice de la columna llamada EditarC
            if(e.ColumnIndex == dataListadoCargos.Columns["EditarC"].Index)
            {
                ObtenerCargosEditar();
            }
            if(e.ColumnIndex == dataListadoCargos.Columns["Cargo"].Index)
            {
                ObtenerDatosCargos();
            }
        }
        private void ObtenerDatosCargos()
        {
            idCargo = Convert.ToInt32(dataListadoCargos.SelectedCells[1].Value);
            txtCargo.Text = dataListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoHora.Text = dataListadoCargos.SelectedCells[3].Value.ToString();
            dataListadoCargos.Visible = false;
            PanelBtnGuardarPersonal.Visible = true;
            lblSueldo.Visible = true;

        }
        private void ObtenerCargosEditar()
        {
            idCargo = Convert.ToInt32(dataListadoCargos.SelectedCells[1].Value);
            txtCargoG.Text = dataListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoHoraG.Text = dataListadoCargos.SelectedCells[3].Value.ToString();
            btnGuardarCargo.Visible = false;
            btnGuardarCambiosCargo.Visible = true;
            PanelCargos.Visible = true;
            PanelCargos.Dock = DockStyle.Fill;
            PanelCargos.BringToFront();
            txtCargoG.Focus();
            txtCargoG.SelectAll();
        }

        private void btnVolverCargos_Click(object sender, EventArgs e)
        {
            PanelCargos.Visible = false;
        }

        private void btnVolverPersonal_Click(object sender, EventArgs e)
        {
            PanelRegistros.Visible = false;
        }

        private void btnGuardarCambiosCargo_Click(object sender, EventArgs e)
        {
            EditarCargos();
        }
        private void EditarCargos()
        {
            LCargos parametros = new LCargos();
            DCargos funcion = new DCargos();
            parametros.id_cargo = idCargo;
            parametros.Cargo = txtCargoG.Text;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoHoraG.Text);
            if (funcion.EditarCargo(parametros) == true)
            {
                txtCargo.Clear();
                BuscarCargos();
                PanelCargos.Visible = false;
            }
        }
    }
}
