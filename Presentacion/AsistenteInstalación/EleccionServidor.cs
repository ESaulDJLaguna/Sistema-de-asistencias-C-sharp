using System;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Presentacion.AsistenteInstalación
{
    public partial class EleccionServidor : Form
    {
        public EleccionServidor()
        {
            InitializeComponent();
        }
        // Al presionar el botón 'Principal' abre el formulario que se usará para instalar el servidor de la aplicación
        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            // Destruye el formulario actual (el que se está mostrando)
            Dispose();
            // Crea una instancia dle formulario para la instalación del servidor y la BD
            InstalacionBD frm = new InstalacionBD();
            // Muestra el formulario en pantalla
            frm.ShowDialog();
        }
        // Al presionar el botón 'Punto de Control' abre el formulario 
        private void BtnPuntoCtl_Click(object sender, EventArgs e)
        {
            // Destruye el formulario actaul que se está mostrando
            Dispose();
            ConexionRemota frm = new ConexionRemota();
            frm.ShowDialog();
        }
    }
}
