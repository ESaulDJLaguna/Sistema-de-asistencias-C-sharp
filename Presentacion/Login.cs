using Sistema_de_asistencias.Datos;
using Sistema_de_asistencias.Logica;
using Sistema_de_asistencias.Presentacion.AsistenteInstalación;
using Sistema_de_asistencias.Presentacion;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Presentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        string usuario; // Almacenará el nombre de usuario para mostrarlo en el panel de usuarios
        int idUsuario; // Almacenará el id de un usuario
        string indicador; // Se utiliza para indicar si hay conexión (existe una BD) o no
        int contador; // Se utiliza para saber si existen usuarios creados

        // Evento que se ejecuta cuando carga el formulario Login
        private void Login_Load(object sender, EventArgs e)
        {
            ValidarConexion();
        }
        // Validación de la conexión. Si hay conexión, abre la aplicación, si no, abre el instalador del servidor.
        private void ValidarConexion()
        {
            // Verifica si ya existe la BD en el servidor
            VerificarConexion();
            // Si ya existe la BD ...
            if( indicador == "Correcto")
            {   
                // Si contador es 0 (NO existen usuarios registrados), significa que es una instalación nueva ...
                MostrarUsuarios();
                if(contador == 0)
                {
                    // Destruye el formulario actual
                    Dispose();
                    // ... nos lleva al formulario para crera un usuario nuevo
                    UsuarioPrincipal frm = new UsuarioPrincipal();
                    frm.ShowDialog();
                }
                else
                {
                    // ... Muestra la lista de usuarios para loguearse
                    DibujarUsuarios();
                }
            }
            // Si no existe la BD ...
            else
            {
                // Destruye el formulario actual
                Dispose();
                // Crea una instancia del formulario para elegir el tipo de instalación
                EleccionServidor frm = new EleccionServidor();
                // Muestra el Formulario como una caja de diálogo modal
                frm.ShowDialog();
            }
        }
        // Obtiene la cantidad de usuarios registrados y los almacena en contador
        private void MostrarUsuarios()
        {
            DataTable dt = new DataTable();
            DUsuarios funcion = new DUsuarios();
            funcion.MostrarUsuarios(ref dt);
            // Cuenta todas las filas que tenga la variable dt
            contador = dt.Rows.Count;
        }
        // Verifica si ya está creada la BD en el servidor
        private void VerificarConexion()
        {
            DUsuarios funcion = new DUsuarios();
            // Verifica que la BD esté creada y se pasa un indicador como referencia
            funcion.VericarUsuarios(ref indicador);
        }

        // Muestra todos los usuarios almacenados en la BD
        private void DibujarUsuarios()
        {
            try
            {
                // Haz visible el panel de Usuarios ...
                PanelUsuarios.Visible = true;
                // ... ocupará toda la ventana ...
                PanelUsuarios.Dock = DockStyle.Fill;
                // ... y tráelo al frente
                PanelUsuarios.BringToFront();
                // Un DataTable contendrá los resultados de una consulta SQL, se utilizará para desplegar la información en un DataGridView o para iterar a través de los registros de la BD
                DataTable dt = new DataTable();
                DUsuarios funcion = new DUsuarios();
                // dt contiene todos los registros con todos sus campos de la tabla Usuarios
                funcion.MostrarUsuarios(ref dt);
                // Itera a través de las filas devueltas por la BD
                foreach (DataRow rdr in dt.Rows)
                {
                    // Contendrá el nombre del usuario obtenido en la consulta SQL
                    Label label = new Label();
                    // Creará un panel que se usará como contenedor de la imagen del usuario y su nombre
                    Panel panel = new Panel();
                    // Contendrá la imagen del usuario
                    PictureBox picBox = new PictureBox();
                    // Del registro actual, recupera su contenido del campo (columna) llamado "Login" (nombre en la BD)
                    label.Text = rdr["Login"].ToString();
                    // El nombre del label será el mismo que el id del usuario actual
                    label.Name = rdr["IdUsuario"].ToString();
                    // Tamaño del nombre del usuario (label)
                    label.Size = new Size(180, 25);
                    // Tipo de fuente y el tamaño de la misma, del nombre del usuario (label)
                    label.Font = new Font("Microsoft Sans Serif", 13);
                    // Color de fondo transparente
                    label.BackColor = Color.Transparent;
                    // Color de la letra será blanca
                    label.ForeColor = Color.White;
                    // Ocupará la barte de abajo del panel
                    label.Dock = DockStyle.Bottom;
                    // Alineación del texto: centrado al centro del label
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    // El cursor cámbialo por una mano al seleccionar el label
                    label.Cursor = Cursors.Hand;

                    // El panel que contendrá el nombre de usuario y su ícono tendrá el tamaño indicado en Size
                    panel.Size = new Size(180, 180);
                    // El panel no tendrá bordes
                    panel.BorderStyle = BorderStyle.None;
                    // El color de fondo será de tipo rgb
                    panel.BackColor = Color.FromArgb(20, 20, 20);

                    // Modificamos el tamaño del pictureBox que contendrá el ícono del usuario obtenido de la BD
                    picBox.Size = new Size(180, 155);
                    // Lo posicionamos en la parte superior del panel
                    picBox.Dock = DockStyle.Top;
                    // "Eliminamos" todas las imágenes que tenga
                    picBox.BackgroundImage = null;
                    // El ícono se almacena en la BD como un byte[]. Recuperamos el contenido del registro actual en el campo llamado Icono y lo convertimos a un arreglo de bytes
                    byte[] bi = (Byte[])rdr["Icono"];
                    // Se almacena el ícono en la memoria (se usa System.IO). Lee los bytes que se le pasan al constructor
                    MemoryStream ms = new MemoryStream(bi);
                    // Ya que está en la memoria, ya se puede mostrar en la imagen
                    picBox.Image = Image.FromStream(ms);
                    // Escala la imagen en modo Zoom
                    picBox.SizeMode = PictureBoxSizeMode.Zoom;
                    // Tag: DATOS DEFINIDOS POR EL USUARIO ASOCIADOS CON EL OBJETO
                    // Se utilizará para recuperar el nombre del usuario al que le pertenece el ícono
                    picBox.Tag = rdr["Login"].ToString();
                    // El tipo de cursor al pasar sobre la imagen será de una mano
                    picBox.Cursor = Cursors.Hand;

                    // Agregamos el nombre del usuario (label) al panel
                    panel.Controls.Add(label);
                    // Agregamos la imagen del usuario al panel
                    panel.Controls.Add(picBox);
                    // Agrega los labels al frente
                    label.BringToFront(); // (Envia al frente). El método: label.SendToBack() => envía al fondo

                    // Agrega el panel con información del usuario en el flowLayoutPanel
                    flowLayoutPanel2.Controls.Add(panel);

                    // Ejecuta un evento cuando el nombre del usuario es clickeado
                    label.Click += MiEventoLabel_Click;
                    // Ejecuta un evento cuando la imagen del usuario es clickeado
                    picBox.Click += MiEventoImagen_Click;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Cuando se da clic en la imagen de un usario ...
        private void MiEventoImagen_Click(object sender, EventArgs e)
        {
            // Como se está manipulando un objeto PictureBox, sender me permitirá acceder a sus propiedades
            // ... recupera de la propiedad Tag el nombre del usuario que se clickeo ...
            usuario = ((PictureBox)sender).Tag.ToString();
            // ... y muestra el panel para ingresar la contraseña del usuario
            MostrarPanelPasswrd();
        }
        // Cuando se da clic en el nombre del usuario ...
        private void MiEventoLabel_Click(object sender, EventArgs e)
        {
            // Estamos manejando un objeto Label y sender me permite acceder a sus propiedades
            // ... recupera de su propiedad Text el nombre del usuario ...
            usuario = ((Label)sender).Text;
            // ... y muestra el panel para ingresar la contraseña del usario seleccionado
            MostrarPanelPasswrd();
        }
        // Muestra el panel para ingresar la contraseña del usuario a loguearse
        private void MostrarPanelPasswrd()
        {
            // Haz visible el panel de contraseña
            PanelIngresoContraseña.Visible = true;
            // Width es el ancho del formulario y Heigh la altura del mismo. Centra el panel de contraseña
            PanelIngresoContraseña.Location = new Point((Width - PanelIngresoContraseña.Width) / 2, (Height - PanelIngresoContraseña.Height) / 2);
            // El TxtContraseña tendrá el foco
            TxtContraseña.Focus();
            // Dejará de visualizarse el panel de usuarios
            PanelUsuarios.Visible = false;
        }
        // Evento que se ejecuta cada vez que cambie el texto en el TxtContraseña (incluso si se pega el contenido)
        private void TxtContraseña_TextChanged(object sender, EventArgs e)
        {
            // Compara que la contraseña escrita coincida con la del usuario seleccionado, de ser así, da paso al sistema
            ValidarUsuarios();
        }
        // Compara que la contraseña escrita coincida con la del usuario seleccionado, de ser así, da paso al sistema
        private void ValidarUsuarios()
        {
            LUsuarios parametros = new LUsuarios();
            DUsuarios funcion = new DUsuarios();
            parametros.Password = TxtContraseña.Text;
            parametros.Login = usuario;
            // Si la contraseña coincide con el nombre de usuario elegido, se obtendrá el id al que pertenece dicho usuario
            funcion.ValidarUsuario(parametros, ref idUsuario);
            // Si se obtuvo un id, le permitirá loguearse en la aplicación
            if(idUsuario > 0)
            {
                // Destruye el formulario actual y y todos sus recursos (de esta forma, se liberan los recursos de la memoria). Pero C# al destruir un formulario, termina la aplicación. Para evitar eso modificamos el Program.cs
                Dispose();
                // Pasamos al MenuPrincipal (Necesitamos construirlo). Se crea una instancia del formulario ...
                MenuPrincipal frm = new MenuPrincipal();
                // ... lo mostramos como una ventana nueva
                frm.ShowDialog();
            }
        }
        // Permite acceder al sistema automáticamente al escribir correctamente la contraseña, por lo que al dar clic en el botón 'Iniciar Sesión' SIEMPRE mostrará un mensaje de contraseña incorrecta
        private void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contraseña Erronea", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // Concatena el dígito que representa el botón presionado y no permite agregar más de la longitud máxima elegida
        private void Btn1_Click(object sender, EventArgs e)
        {
            if(TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "1";
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "2";
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "3";
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "4";
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "5";
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "6";
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "7";
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "8";
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "9";
        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text.Length < TxtContraseña.MaxLength)
                TxtContraseña.Text += "0";
        }

        private void BtnBorrarTodo_Click(object sender, EventArgs e)
        {
            // Limpiamos el TxtContraseña
            TxtContraseña.Clear();
        }

        // Elimina el último dígito de la contraseña dentro del TxtContraseña
        private void BtnBorrarUnCaracter_Click(object sender, EventArgs e)
        {
            int contador;
            // Obten la longitud actual de la contraseña
            contador = Convert.ToInt32(TxtContraseña.Text.Length);
            // Si la contraseña tiene al menos un dígito ...
            if(contador > 0)
            {
                // Crea una sub cadena que inicie en la posición 0 de la cadena actual y que tenga una longitud (longitud actual - 1) y reemplaza la contraseña actual por la nueva subcadena
                TxtContraseña.Text = TxtContraseña.Text.Substring(0, TxtContraseña.Text.Length - 1);
            }
        }
        // Al dar clic en el botón 'Cambiar usuario' ...
        private void BtnCambiarUser_Click(object sender, EventArgs e)
        {
            // ... muestra el panel para elegir un nuevo usuario.
            PanelUsuarios.Visible = true;
        }
    }
}
