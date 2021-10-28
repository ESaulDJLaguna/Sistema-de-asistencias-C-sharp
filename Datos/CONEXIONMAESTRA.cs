using System.Data;
using System.Data.SqlClient;


namespace Sistema_de_asistencias.Datos
{
    public class CONEXIONMAESTRA
    {
        // Cadena de conexión básica sin encriptación
        public static string conexion =
            "Data source=DESKTOP-F9NN2EK;"+
            "Initial Catalog=ORUS369;"+
            "Integrated Security=true";
        // Cada vez que se haga una nueva conexión, aquí se "dibujará" la conexión
        public static SqlConnection conectar = new SqlConnection(conexion);
        // Método que nos facilitará abrir de forma segura la conexión a la base de datos
        public static void abrir()
        {
            // Si el estado de la conexión es 'cerrado'...
            if(conectar.State == ConnectionState.Closed)
            {
                // ... abre la conexión
                conectar.Open();
            }
        }
        // Método que nos facilita cerrar nuestra conexión
        public static void cerrar()
        {
            // Si el estado de la conexión es 'abierto'...
            if(conectar.State == ConnectionState.Open)
            {
                // ... cierra la conexión
                conectar.Close();
            }
        }
    }
}
