using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Presentacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            panelBienvenida.Dock = DockStyle.Fill;
        }
        // Se ejecuta el evento cuando se presiona el botón (+ azul), para agregar nuevo personal.
        private void BtnPersonal_Click(object sender, EventArgs e)
        {
            // Limpiamos el panel principal
            PanelPrincipal.Controls.Clear();
            // Instancia del elemento 'User Control' (NO formulario) para agregar Personal
            Personal control = new Personal
            {
                // Ocupará todo el Panel
                Dock = DockStyle.Fill
            };
            // Agrega el 'User Control' al panel principal
            PanelPrincipal.Controls.Add(control);
        }
    }
}
