using Sistema_de_asistencias.Datos;
using Sistema_de_asistencias.Logica;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Presentacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }
        // Almacenará el id del usuario logueado actualmente, pero se llenará en el Formulario Login
        public int idUsuario;
        // Almacenará el usuario actual, pero se hará desde el Formulario Login
        public string loginV;

        // Al cargar el formulario de Menu Principal ...
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            // ... el panel de Bienvenida (contiene un label con el texto de bienvenida) rellenará (ocupará) todo el panel donde está contenido
            panelBienvenida.Dock = DockStyle.Fill;
            ValidarPermisos();
        }

        // Método que habilita los botones según los permisos que tenga cada usuario
        private void ValidarPermisos()
        {
            // Almacenará el idModulo y nombre del Modulo de un usuario según su id
            DataTable dt = new DataTable();
            DPermisos funcion = new DPermisos();
            LPermisos parametros = new LPermisos();
            // Almacenamos el idUsuario (se obtuvo en el Formulario Login)
            parametros.IdUsuario = idUsuario;
            // Almacenamos en dt los idModulos y los nombres de los mismos, a los que tiene acceso el usuario actual (según id)
            funcion.MostrarPermisos(ref dt, parametros);
            // Todos los botones se deshabilitan. Por medio de un bucle deshabilitamos los permisos que tiene cada usuario
            BtnConsultas.Enabled = false;
            BtnPersonal.Enabled = false;
            BtnRegistro.Enabled = false;
            BtnUsuarios.Enabled = false;
            BtnRestaurarBD.Enabled = false;
            BtnRespaldos.Enabled = false;
            // Habilitamos los botones (según los permisos del usuario)
            foreach(DataRow rowPermisos in dt.Rows)
            {
                // Almacena el nombre del Módulo actual
                string modulo = Convert.ToString(rowPermisos["Modulo"]);
                // Habilita los botones según los módulos a los que tenga acceso el usuario
                if(modulo == "PrePlanillas")
                {
                    BtnConsultas.Enabled = true;
                }
                if (modulo == "Usuarios")
                {
                    BtnUsuarios.Enabled = true;
                    BtnRegistro.Enabled = true;
                }
                if (modulo == "Personal")
                {
                    BtnPersonal.Enabled = true;
                }
                if (modulo == "Respaldos")
                {
                    BtnRespaldos.Enabled = true;
                    BtnRestaurarBD.Enabled = true;
                }
            }
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
