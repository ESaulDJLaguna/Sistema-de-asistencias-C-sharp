using System;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Presentacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }
        // Al cargar el formulario de Menu Principal ...
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            // ... el panel de Bienvenida (contiene un label con el texto de bienvenida) rellenará (ocupará) todo el panel donde está contenido
            panelBienvenida.Dock = DockStyle.Fill;
        }
        // Cuando se presiona el botón 'Personal', muestra el User Control (Personal)
        private void BtnPersonal_Click(object sender, EventArgs e)
        {
            // Limpiamos todo lo que tenga dentro el 'Panel Principal'
            PanelPrincipal.Controls.Clear();
            // Instancia del elemento 'User Control' (NO formulario) para agregar Personal
            Personal control = new Personal
            {
                // Se expande y ocupa todo el Panel Principal
                // control.Dock = DockStyle.Fill;
                Dock = DockStyle.Fill
            };
            // Agrega el 'User Control' al panel principal
            PanelPrincipal.Controls.Add(control);
        }
        // Al presionar el botón 'Usuarios' ...
        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            // ... limpia el PanelPrincipal
            PanelPrincipal.Controls.Clear();
            // Crea una nueva instancia del "User Control" ControlUsuarios ...
            ControlUsuarios control = new ControlUsuarios
            {
                // control.Dock = DockStyle.Fill;
                // ... y ocupará todo el espacio del PanelPrincipal.
                Dock = DockStyle.Fill
            };
            // Agregamos el control al panel principal
            PanelPrincipal.Controls.Add(control);
        }
    }
}
