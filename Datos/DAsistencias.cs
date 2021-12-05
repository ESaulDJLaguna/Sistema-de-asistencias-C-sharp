using Sistema_de_asistencias.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Datos
{
    public class DAsistencias
    {
        // Método que busca si ya está registrada la Asistencia de un Personal (Estado = 'ENTRADA')
        public void BuscarAsistenciasPorId(ref DataTable dt, int idPersonal)
        {
            // Protección del código. Evita que se obtenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // SqlDataAdapter REPRESENTA UN CONJUNTO DE COMANDOS DE DATOS Y UNA CONEXIÓN A UNA BD QUE SE USAN PARA RELLENAR DataSet Y ACTUALIZAR UNA BD DE SQL Server. EN SU MAYORÍA SE UTILIZA CUANDO QUEREMOS MOSTRAR INFORMACIÓN DE UNA BD
                // Clase utilizada para mostrar (adaptar) información de una base de datos. Pasamos el procedimiento almacenado al que hará referencia y la conexión a la BD
                SqlDataAdapter da = new SqlDataAdapter("BuscarAsistenciasPorId", CONEXIONMAESTRA.conectar);
                // Indicamos que el procedimiento requiere de parámetros
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                // Pasamos los parámetros
                da.SelectCommand.Parameters.AddWithValue("@id_personal", idPersonal);
                // Pasamos los datos obtenidos por la consulta a una tabla (DataTable) que posteriormente se mostrará en un DataGridView
                da.Fill(dt);
            }
            // Si hubo algún fallo al manipular la BD...
            catch (Exception ex)
            {
                // ... muestra un mensaje de error. Como en SQL Server no tenemos un Raiserror, usamos un StackTrace
                MessageBox.Show(ex.StackTrace);
            }
            // UN BLOQUE finally SIEMPRE SE EJECUTA, INDEPENDIENTEMENTE DE SI UNA EXCEPCIÓN FUE LANZADA O CAPTURADA
            finally
            {
                // Si está abierta la conexión a la BD y se ejecutó o no el procedimiento: ciérrala
                CONEXIONMAESTRA.Cerrar();
            }
        }
        // Método que inserta las Asistencias en la BD en la tabla del mismo nombre
        public bool InsertarAsistencias(LAsistencias parametros)
        {
            // Protección del código. Evita que se obtenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertar, Eliminar, Editar
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("InsertarAsistencias", CONEXIONMAESTRA.conectar)
                {
                    // Indicamos que el procedimiento requiere de parámetros
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos los parámetros
                cmd.Parameters.AddWithValue("@id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@fecha_entrada", parametros.Fecha_entrada);
                cmd.Parameters.AddWithValue("@fecha_salida", parametros.Fecha_salida);
                cmd.Parameters.AddWithValue("@estado", parametros.Estado);
                cmd.Parameters.AddWithValue("@horas", parametros.Horas);
                cmd.Parameters.AddWithValue("@observacion", parametros.Observacion);
                cmd.ExecuteNonQuery();
                return true;
            }
            // Si hubo algún fallo al manipular la BD...
            catch (Exception ex)
            {
                // ... muestra un mensaje de error.
                MessageBox.Show(ex.StackTrace);
                // No inserta los datos en la BD
                return false;
            }
            // UN BLOQUE finally SIEMPRE SE EJECUTA, INDEPENDIENTEMENTE DE SI UNA EXCEPCIÓN FUE LANZADA O CAPTURADA
            finally
            {
                // Si está abierta la conexión a la BD y se ejecutó o no el procedimiento: ciérrala
                CONEXIONMAESTRA.Cerrar();
            }
        }
        // Método que cambia el estado a 'SALIDA' de un Personal en la tabla Asistencias de la BD
        public bool ConfirmarSalida(LAsistencias parametros)
        {
            // Protección del código. Evita que se obtenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertar, Eliminar, Editar
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("ConfirmarSalida", CONEXIONMAESTRA.conectar)
                {
                    // Indicamos que el procedimiento requiere de parámetros
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos los parámetros
                cmd.Parameters.AddWithValue("@id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@fecha_salida", parametros.Fecha_salida);
                cmd.Parameters.AddWithValue("@horas", parametros.Horas);
                cmd.ExecuteNonQuery();
                return true;
            }
            // Si hubo algún fallo al manipular la BD...
            catch (Exception ex)
            {
                // ... muestra un mensaje de error.
                MessageBox.Show(ex.StackTrace);
                // No inserta los datos en la BD
                return false;
            }
            // UN BLOQUE finally SIEMPRE SE EJECUTA, INDEPENDIENTEMENTE DE SI UNA EXCEPCIÓN FUE LANZADA O CAPTURADA
            finally
            {
                // Si está abierta la conexión a la BD y se ejecutó o no el procedimiento: ciérrala
                CONEXIONMAESTRA.Cerrar();
            }
        }
    }
}
