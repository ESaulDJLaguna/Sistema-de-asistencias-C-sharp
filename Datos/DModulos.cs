using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Datos
{
    public class DModulos
    {
        // Muestra todos los campos de todos los registros de la tabla Modulos y los almacenará en un DataTable
        public void MostrarModulos(ref DataTable dt)
        {
            // No detiene la aplicación si existe alguna excepción
            try
            {
                // Abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // Ejecuta una instrucción SQL al servidor y BD indicada
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Modulos", CONEXIONMAESTRA.conectar);
                // Llena un DataTable con los registros obtenidos
                da.Fill(dt);
            }
            // Si se genera alguna excepción, muestra un mensaje de error
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            // finally se ejecuta siempre, exista una excepción o no
            finally
            {
                // Cierra la conexión a la BD
                CONEXIONMAESTRA.Cerrar();
            }
        }
    }
}
