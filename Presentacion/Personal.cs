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
        public Personal()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // El panel para agregar nuevos cargoes no será visible
            PanelCargos.Visible = false;
            // El panel inferior de paginano, no será visible
            PanelPaginado.Visible = false;
            // El panel de registros, será visible...
            PanelRegistros.Visible = true;
            // ... y ocupará el total del panel
            PanelRegistros.Dock = DockStyle.Fill;
            // El botón para guardar personal será visible
            btnGuardarPersonal.Visible = true;
            btnGuardarCambiosPersonal.Visible = false;
            Limpiar();
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
    }
}
