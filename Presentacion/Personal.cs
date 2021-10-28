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
        }
    }
}
