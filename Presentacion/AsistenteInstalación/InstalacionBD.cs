﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using Sistema_de_asistencias.Logica;
using System.IO;
using System.Diagnostics;

namespace Sistema_de_asistencias.Presentacion.AsistenteInstalación
{
    public partial class InstalacionBD : Form
    {
        public InstalacionBD()
        {
            InitializeComponent();
        }

        // Se utilizará uno de los métodos para encriptar la cadena de conexión a nuestro servidor y BD
        private Encriptacion aes_256 = new Encriptacion();
        string ruta;
        string nombreDelEquipoUsuario;
        public static int milisegundos;
        public static int milisegundos1;
        public static int segundos;
        public static int segundos1;
        public static int minutos1;

        private void InstalacionBD_Load(object sender, EventArgs e)
        {
            CentrarPaneles();
            Reemplazar();
            Comprobar_Si_Ya_Hay_Servidor_Instaldo_SQL_Express();
            Conectar();
        }

        private void Conectar()
        {
            // Si el botón para instalar un servidor es visible, algo salió mal
            if(BtnInstalarServidor.Visible == true)
            {
                Comprobar_Si_Ya_Hay_Servidor_Instalado_SQL_Normal();
            }
        }

        private void Comprobar_Si_Ya_Hay_Servidor_Instalado_SQL_Normal()
        {
            LblServidor.Text = ".";
            Ejecutar_Script_ELIMINAR_BD_Comprobacion_De_Inicio();
            Ejecutar_Script_CREAR_BD_Comprobacion_De_Inicio();
        }

        // Centra panel (principal) de instalación
        private void CentrarPaneles()
        {
            // Centramos el formulario de instalación (Botón Instalar Servidor y mensaje de instalación)
            PanelInstallServer.Location = new Point((Width - PanelInstallServer.Width) / 2, ((Height - PanelInstallServer.Height) / 2));
            // Obtiene el nombre del usuario actual de Windows
            nombreDelEquipoUsuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            // El cursor será un circulo que dará vueltas
            Cursor = Cursors.WaitCursor;
            // Panel que tiene el mensaje de 'Instalando servidor' y el contador estará oculto
            PanelInstalando.Visible = false;
            PanelInstalando.Dock = DockStyle.None;
        }

        // Reemplazamos los nombres en los scripts para crear la BD, eliminar y crear usuario en servidor
        private void Reemplazar()
        {
            //* Solo modificar este campo. Reemplaza el nombre de la BD en todo el Script, por el nombre que quiere usar el usuario para la nueva BD
            TxtScriptCreaBD.Text = TxtScriptCreaBD.Text.Replace("ORUS369", TxtNombreBD.Text);
            // Reemplaza el nombre de la BD en el script que elimina una BD
            TxtEliminaBD.Text = TxtEliminaBD.Text.Replace("BASEADACURSO", TxtNombreBD.Text);
            // Reemplaza el nombre de usuario, el nombre de la BD y la contraseña en el script que se usa para crear un usuario en la BD
            TxtCrearUsuarioBD.Text = TxtCrearUsuarioBD.Text.Replace("ada369", TxtUsuario.Text);
            TxtCrearUsuarioBD.Text = TxtCrearUsuarioBD.Text.Replace("BASEADA", TxtNombreBD.Text);
            TxtCrearUsuarioBD.Text = TxtCrearUsuarioBD.Text.Replace("softwarereal", TxtContraseñaSQL.Text);
            // Concatemanos el nuevo usuario y contraseña que se crearán al final del Script que se usa para crear la BD
            TxtScriptCreaBD.Text = TxtScriptCreaBD.Text + Environment.NewLine + TxtCrearUsuarioBD.Text;
        }

        private void Comprobar_Si_Ya_Hay_Servidor_Instaldo_SQL_Express()
        {
            // C# reconoce \ como terminación de línea, para poder utilizarlo se le antepone un @.
            // Lo que está antes de la diagonal invertida es el nombre del servidor. Si solo tiene un punto, también se puede conectar al servidor.
            // LblServidor contiene el nombre del servidor y el tipo de SQL a utilizar
            LblServidor.Text = @".\" + TxtNombreInstancia.Text;
            Ejecutar_Script_ELIMINAR_BD_Comprobacion_De_Inicio();
            Ejecutar_Script_CREAR_BD_Comprobacion_De_Inicio();
        }

        // Si ya existe una BD con el mismo nombre, la elimina.
        private void Ejecutar_Script_ELIMINAR_BD_Comprobacion_De_Inicio()
        {
            // Almacenará el Script para eliminar la BD
            string str;
            // LA INTEGRACIÓN DE SEGURIDAD NOS AYUDA A QUE NO PIDA USUARIO NI CONTRASEÑA PARA ELIMINAR LA BD
            // Almacena la información del servidor al que nos conectaremos y la BD a la que haremos referencia (BD master). Creamos una conexión y le pasamos una cadena de conexión
            SqlConnection conexion = new SqlConnection("Data source=" + LblServidor.Text + ";Initial Catalog=master;Integrated Security=True");
            // Carga el script que ejecutaremos
            str = TxtEliminaBD.Text;
            // Indicamos en qué servidor y en qué BD se ejecutará un script
            SqlCommand myCommand = new SqlCommand(str, conexion);
            // Protegemos el código
            try
            {
                // Abrimos la conexión al servidor
                conexion.Open();
                // Ejecutamos el procedimiento
                myCommand.ExecuteNonQuery();
            }
            catch (Exception){}
            finally
            {
                // Si la conexión al servidor sigue abierta, la cerramos
                if(conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // Creación de la BD
        private void Ejecutar_Script_CREAR_BD_Comprobacion_De_Inicio()
        {
            // Almacena la información del servidor al que nos conectaremos y la BD a la que haremos referencia (BD master). Creamos una conexión y le pasamos una cadena de conexión (Server es un sinónimo de Data source)
            SqlConnection conexion = new SqlConnection("Server=" + LblServidor.Text + "; database=master; integrated security=yes");
            // Carga el script que ejecutaremos
            string s = "CREATE DATABASE " + TxtNombreBD.Text;
            // Indicamos en qué servidor y en qué BD se ejecutará un script
            SqlCommand cmd = new SqlCommand(s, conexion);
            // Protegemos el código
            try
            {
                // Abrimos la conexión al servidor
                conexion.Open();
                // Ejecutamos el script para crear la BD
                cmd.ExecuteNonQuery();
                // Se crea la cadena de conexión encriptada. Le pasamos toda la cadena de conexión que se creará con los datos en el formulario. Nombre de servidor, base de datos y seguridad integrada, además se utiliza la llave única y en cuántos bits se encriptará
                SaveToXml(aes_256.Encrypt("Data Source=" + LblServidor.Text + ";Initial Catalog=" + TxtNombreBD.Text + ";Integrated Security=True", Desencriptacion.appPwdUnique, int.Parse("256")));
                EjecutarScript();
                PanelInstalando.Visible = true;
                PanelInstalando.Dock = DockStyle.Fill;
                LblMsjInstalando.Text = @"Instancia Encontrada... No Cierre esta Ventana, se cerrara Automaticamente cuando este todo Listo";
                LblMsjInstalando.BringToFront();
                PanelTemporizador.Visible = false;
                // Iniciamos el timer3
                timer3.Start();
            }
            catch (Exception)
            {
                BtnInstalarServidor.Visible = true;
                PanelTemporizador.Visible = true;
                PanelInstalando.Visible = false;
                PanelInstalando.Dock = DockStyle.None;
                Lbl_buscador_de_servidores.Text = "De clic a Instalar Servidor, luego de clic a SÍ cuando se le pida, luego presione ACEPTAR y espere, por favor.";
            }
        }

        // Método que se utilizará para ejecutar el script que crea todas las tablas y procedimientos almacenados dentro de la BD
        private void EjecutarScript()
        {
            // Crearemos un archivo (de nombre que utilizamos en nuestro Text Box nombre sript) con extensión .txt, si aún no existe, que contenga todo el contenido del script para crear la estructura de la BD.
            // ruta contendrá la dirección donde está almacenado dicho archivo
            ruta = Path.Combine(Directory.GetCurrentDirectory(),TxtNombreScript.Text + ".txt");
            // Proporciona métodos de instancia para crear, copiar, eliminar, mover y abrir archivos y contribuye a la creación de objetos FileStream.
            // fi nos permitirá crear el archivo desde cero, pero por ahora no es lo que se quiere
            //FileInfo fi = new FileInfo(ruta);
            // sw nos permitirá escribir en el archivo creado por fi
            StreamWriter sw;
            // Protegemos el código
            try
            {
                // Si no existe el archivo buscado ...
                if(File.Exists(ruta)==false)
                {
                    // ... lo creamos en la ruta actual
                    sw = File.CreateText(ruta);
                    // Sobreescribiremos el archivo que acabamos de crear. Contendrá todo el texto de nuestro TxtScriptCreaBD
                    sw.WriteLine(TxtScriptCreaBD.Text);
                    // Limpia el buffer (Cierra la última línea en la que se escribió)
                    sw.Flush();
                    sw.Close();
                }
                // Si ya existe el archivo ...
                else if(File.Exists(ruta)==true)
                {
                    // ... eliminalo y vuelve a crearlo
                    File.Delete(ruta);
                    // Lo creamos en la ruta actual
                    sw = File.CreateText(ruta);
                    // Sobreescribiremos el archivo que acabamos de crear. Contendrá todo el texto de nuestro TxtScriptCreaBD
                    sw.WriteLine(TxtScriptCreaBD.Text);
                    // Limpia el buffer (Cierra la última línea en la que se escribió)
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception){}
            // Una vez que se termina de crear el archivo con el script, protegemos de nuevo el código. Este try-catch lo que hará es restaurar el script, pero utilizando la consola de sqlcmd
            try
            {
                // Esta clase permite abrir la consola
                Process proc = new Process();
                // FileName obtiene o establece el documento o aplicación que queremos iniciar. En este caso, al consola de SQL Server
                proc.StartInfo.FileName = "sqlcmd";
                // Argumentos utilizados en consola: S, indicamos que ejecutará un script, 
                proc.StartInfo.Arguments = " -S " + LblServidor.Text + " -i " + TxtNombreScript.Text + ".txt";
                // Ejecutarmos el comando
                proc.Start();

            }
            catch (Exception){}
        }

        // Encripta la cadena de conexión
        private void SaveToXml(object dbCnString)
        {
            XmlDocument doc = new XmlDocument();
            // Lee el archivo xml dentro de la carpeta bin
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbCnString);
            // Sobreescribirá el archivo
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            // Lo sobreescribe
            doc.Save(writer);
            writer.Close();
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            milisegundos++;
            Ms3.Text = milisegundos.ToString();
            if(milisegundos == 60)
            {
                segundos++;
                Seg3.Text = segundos.ToString();
                milisegundos = 0;
            }
            // Si ya pasaron 15 segundos ...
            if(segundos == 15)
            {
                // ... detenemos el Timer
                timer3.Stop();
                // ... eliminamos el archivo donde está almacenado el script
                try
                {
                    File.Delete(ruta);
                }
                catch (Exception){}
                // Destruimos el formulario actual
                Dispose();
            }
        }

        private void BtnInstalarServidor_Click(object sender, EventArgs e)
        {
            try
            {
                TxtConfigFile.Text = TxtConfigFile.Text.Replace("PRUEBAFINAL22", TxtNombreInstancia.Text);
                TimerCRARINI.Start();
                Executa();
                timer1.Start();
                PanelInstalando.Visible = true;
                PanelInstalando.Dock = DockStyle.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            
        }
        // Muestra mensaje que pide dónde se extrearán los archivos para instalar el Servidor
        private void Executa()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "SQLEXPR_x86_ENU.exe";
                proc.StartInfo.Arguments = "/ConfigurationFile=ConfigurationFile.ini /ACTION=Install /IACCEPTSQLSERVERLICENSETERMS /SECURITYMODE=SQL /SAPWD=" + TxtContraseñaSQL.Text + " /SQLSYSADMINACCOUNTS=" + nombreDelEquipoUsuario;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                proc.Start();
                PanelInstallServer.Visible = true;
                PanelInstallServer.Dock = DockStyle.Fill;
            }
            catch (Exception ex) { }
        }

        private void TimerCRARINI_Tick(object sender, EventArgs e)
        {
            string rutaPreparar;
            StreamWriter sw;
            rutaPreparar = Path.Combine(Directory.GetCurrentDirectory(), "ConfigurationFile.ini");
            rutaPreparar = rutaPreparar.Replace("ConfigurationFile.ini", @"SQLEXPR_x86_ENU\ConfigurationFile.ini");
            if (File.Exists(rutaPreparar))
            {
                TimerCRARINI.Stop();
            }
            try
            {
                sw = File.CreateText(rutaPreparar);
                sw.WriteLine(TxtConfigFile.Text);
                sw.Flush();
                sw.Close();
                TimerCRARINI.Stop();
            }
            catch (Exception ex) { }
        }
        // Se da un estimado de 6 minutos para instalar SQL server
        private void timer1_Tick(object sender, EventArgs e)
        {
            milisegundos1 += 1;
            Ms.Text = Convert.ToString(milisegundos1);
            if (milisegundos1 == 60)
            {
                segundos1 += 1;
                Seg.Text = Convert.ToString(segundos1);
                milisegundos1 = 0;
            }
            if(segundos1 == 60)
            {
                minutos1 += 1;
                Min.Text = Convert.ToString(minutos1);
                segundos1 = 0;
            }
            // Después de 6 minutos, intentamos eliminar la base de datos
            if(minutos1 == 6)
            {
                timer1.Stop();
                Ejecutar_Script_Eliminar_Base();
                Ejecutar_Script_Crear_Base();
                timer2.Start();
            }
        }
        private void Ejecutar_Script_Eliminar_Base()
        {
            string str;
            SqlConnection myConn = new SqlConnection("Data Source=" + LblServidor.Text + ";Initial Catalog=master;Integrated Security=True");
            str = TxtEliminaBD.Text;
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
        private void Ejecutar_Script_Crear_Base()
         {
            SqlConnection cnn = new SqlConnection("Server=" + LblServidor.Text + "; database=master;integrated security=yes");
            string s = "CREATE DATABASE " + TxtNombreBD.Text;
            SqlCommand cmd = new SqlCommand(s, cnn);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                SaveToXml(aes_256.Encrypt("Data Source=" + LblServidor.Text + ";Initial Catalog=" + TxtNombreBD.Text + ";Integrated Security=True", Desencriptacion.appPwdUnique, int.Parse("256")));
                EjecutarScript();
                timer3.Start();
            }
            catch (Exception ex) { }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            milisegundos1 += 1;
            Ms.Text = Convert.ToString(milisegundos1);
            if (milisegundos1 == 60)
            {
                segundos1 += 1;
                Seg.Text = Convert.ToString(segundos1);
                milisegundos1 = 0;
            }
            if (segundos1 == 60)
            {
                minutos1 += 1;
                Min.Text = Convert.ToString(minutos1);
                segundos1 = 0;
            }
            if (minutos1 == 1)
            {
                Ejecutar_Script_Eliminar_Base();
                Ejecutar_Script_Crear_Base();
            }
        }
    }
}
