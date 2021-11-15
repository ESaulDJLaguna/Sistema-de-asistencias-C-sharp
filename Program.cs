using System;
using System.Windows.Forms;

namespace Sistema_de_asistencias
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // El formulario Login será el primero en mostrarse, así que creamos una instancia
            Presentacion.Login frm = new Presentacion.Login();
            // Qué queremos que pase al cerrar (destruir) el formulario Login
            frm.FormClosed += Frm_FormClosed;
            // Muestra el formulario Login
            frm.ShowDialog();
            // Ejecuta la aplicación
            Application.Run();


            // Qué pantalla se mostrará al inicio de la aplicación
            //Application.Run(new Presentacion.Login());
        }

        // Evento que se ejecuta cuando se destruye (Dispose) el formulario Login
        private static void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Termina (libera) todos los recursos que consume el formulario, pero que no se detenga la aplicación
            Application.ExitThread();
            // Termina la aplicación (si es que se finaliza el último formulario)
            Application.Exit();
        }
    }
}
