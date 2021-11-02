using System;
// Para trabajar con parámetros y SQL Server, se deben importar sus librerías
using System.Data;
using System.Data.SqlClient;
using Sistema_de_asistencias.Logica;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Datos
{
    // Clase que representa ("convierte" a C#) los procedimientos almacenados que manipulan la tabla Personal en la BD: Buscar, Editar, Eliminar, Insertar, Mostrar
    public class DPersonal
    {
        // Implementa el procedimiento almacenado que Inserta Personal en la Tabla Personal de la BD
        public bool InsertarPersonal(LPersonal parametros)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertará, Eliminará, Editará
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("InsertarPersonal", CONEXIONMAESTRA.conectar)
                {
                    // Le indicamos que el procedimiento requiere de parámetros para funcionar
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificación", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@País", parametros.Pais);
                cmd.Parameters.AddWithValue("@id_cargo", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
                // ExecuteNonQuery ES UN MÉTODO QUE EJECUTA UNA INSTRUCCIÓN Transact-SQL EN LA CONEXIÓN Y DEVUELVE EL NÚMERO DE FILAS AFECTADAS
                // Ejecuta el procedimiento almacenado. Aquí se agrega la información en la BD
                cmd.ExecuteNonQuery();
                // Indica que todo ha salido correctamente al insertar la información en la BD
                return true;
            }
            // Si hubo algún fallo al manipular la BD...
            catch (Exception ex)
            {
                // ... muestra un mensaje de error. OBLIGATORIO usar Message si en SQL Server utilizamos Raiserror
                MessageBox.Show(ex.Message);
                // No inserta los datos en la BD
                return false;
            }
            // UN BLOQUE finally SIEMPRE SE EJECUTA, INDEPENDIENTEMENTE DE SI UNA EXCEPCIÓN FUE LANZADA O CAPTURADA
            finally
            {
                // Si está abierta la conexión a la BD y se ejecute o no el procedimiento: ciérrala
                CONEXIONMAESTRA.Cerrar();
            }
        }
        // Implementa el procedimiento almacenado que Edita Personal en la Tabla Personal de la BD
        public bool EditarPersonal(LPersonal parametros)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo.
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertar, Eliminar, Editar
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("EditarPersonal", CONEXIONMAESTRA.conectar)
                {
                    // Le indicamos que el procedimiento requiere de parámetros para funcionar
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificación", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@País", parametros.Pais);
                cmd.Parameters.AddWithValue("@id_cargo", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
                // ExecuteNonQuery ES UN MÉTODO QUE EJECUTA UNA INSTRUCCIÓN Transact-SQL EN LA CONEXIÓN Y DEVUELVE EL NÚMERO DE FILAS AFECTADAS
                // Ejecuta la manipulación en la BD editando la información en la tabla Personal
                cmd.ExecuteNonQuery();
                // Indica que todo ha salido correctamente al editar la información en la BD
                return true;
            }
            // Si hubo algún fallo al manipular la BD...
            catch (Exception ex)
            {
                // ... muestra un mensaje de error. OBLIGATORIO usar Message si en SQL Server utilizamos Raiserror
                MessageBox.Show(ex.Message);
                // No modifica la información de la BD
                return false;
            }
            // UN BLOQUE finally SIEMPRE SE EJECUTA, INDEPENDIENTEMENTE DE SI UNA EXCEPCIÓN FUE LANZADA O CAPTURADA
            finally
            {
                // Si está abierta la conexión a la BD y se ejecute o no el procedimiento: ciérrala
                CONEXIONMAESTRA.Cerrar();
            }
        }
        // Implementa el procedimiento almacenado que Elimina Personal en la Tabla Personal de la BD
        public bool EliminarPersonal(LPersonal parametros)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertar, Eliminar, Editar
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("EliminarPersonal", CONEXIONMAESTRA.conectar)
                {
                    // Le indicamos que el procedimiento requiere de parámetros para funcionar
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@id_personal", parametros.Id_personal);
                // ExecuteNonQuery ES UN MÉTODO QUE EJECUTA UNA INSTRUCCIÓN Transact-SQL EN LA CONEXIÓN Y DEVUELVE EL NÚMERO DE FILAS AFECTADAS
                // Ejecuta la manipulación en la BD eliminando el Personal según el id pasado
                cmd.ExecuteNonQuery();
                // Indica que todo ha salido correctamente al eliminar la información en la BD
                return true;
            }
            // Si hubo algún fallo al manipular la BD ...
            catch (Exception ex)
            {
                // ... muestra un mensaje de error. OBLIGATORIO usar Message si en SQL Server utilizamos Raiserror
                MessageBox.Show(ex.Message);
                // No modifica la información en la BD
                return false;
            }
            // UN BLOQUE finally SIEMPRE SE EJECUTA, INDEPENDIENTEMENTE DE SI UNA EXCEPCIÓN FUE LANZADA O CAPTURADA
            finally
            {
                // Si está abierta la conexión a la BD y se ejecuta o no el procedimiento: ciérrala
                CONEXIONMAESTRA.Cerrar();
            }
        }
        // Implementa el procedimiento almacenado que Muestra el Personal 
        public void MostrarPersonal(ref DataTable dt, int desde, int hasta)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // SqlDataAdapter REPRESENTA UN CONJUNTO DE COMANDOS DE DATOS Y UNA CONEXIÓN A UNA BD QUE SE USAN PARA RELLENAR DataSet Y ACTUALIZAR UNA BD DE SQL Server. EN SU MAYORÍA SE UTILIZA CUANDO QUEREMOS MOSTRAR INFORMACIÓN DE UNA BD
                // Clase utilizada para mostrar (adaptar) información de una base de datos. Pasamos el procedimiento almacenado al que hará referencia y la conexión a la BD
                SqlDataAdapter da = new SqlDataAdapter("MostrarPersonal", CONEXIONMAESTRA.conectar);
                // Indicamos que el procedimiento requiere de parámetros
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                // Pasamos los parámetros
                da.SelectCommand.Parameters.AddWithValue("@Desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
                // Pasamos los datos obtenidos por la consulta a una tabla (DataTable) que posteriormente se mostrará en un DataGridView
                da.Fill(dt);
            }
            // Si hubo algún fallo al manipular la BD ...
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
        // Representa el procedimiento almacenado que paginará buscando al personal
        public void BuscarPersonal(ref DataTable dt, string buscador)
        {
            // Protección del código. Evita que se obtenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // SqlDataAdapter REPRESENTA UN CONJUNTO DE COMANDOS DE DATOS Y UNA CONEXIÓN A UNA BD QUE SE USAN PARA RELLENAR DataSet Y ACTUALIZAR UNA BD DE SQL Server. EN SU MAYORÍA SE UTILIZA CUANDO QUEREMOS MOSTRAR INFORMACIÓN DE UNA BD
                // Clase utilizada para mostrar (adaptar) información de una base de datos. Pasamos el procedimiento almacenado al que hará referencia y la conexión a la BD
                SqlDataAdapter da = new SqlDataAdapter("BuscarPersonal", CONEXIONMAESTRA.conectar);
                // Indicamos que el procedimiento requiere de parámetros
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                // Pasamos los parámetros
                da.SelectCommand.Parameters.AddWithValue("@Buscador", buscador);
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
        // Implementa el procedimiento almacenado que Restaura el Personal en la Tabla Personal en la BD
        public bool RestaurarPersonal(LPersonal parametros)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertar, Eliminar, Editar
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("RestaurarPersonal", CONEXIONMAESTRA.conectar)
                {
                    // Le indicamos que el procedimiento requiere de parámetros para funcionar
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@id_personal", parametros.Id_personal);
                // ExecuteNonQuery ES UN MÉTODO QUE EJECUTA UNA INSTRUCCIÓN Transact-SQL EN LA CONEXIÓN Y DEVUELVE EL NÚMERO DE FILAS AFECTADAS
                // Ejecuta la manipulación en la BD eliminando el Personal según el id pasado
                cmd.ExecuteNonQuery();
                // Indica que todo ha salido correctamente al eliminar la información en la BD
                return true;
            }
            // Si hubo algún fallo al manipular la BD ...
            catch (Exception ex)
            {
                // ... muestra un mensaje de error. OBLIGATORIO usar Message si en SQL Server utilizamos Raiserror
                MessageBox.Show(ex.StackTrace);
                // No modifica la información en la BD
                return false;
            }
            // UN BLOQUE finally SIEMPRE SE EJECUTA, INDEPENDIENTEMENTE DE SI UNA EXCEPCIÓN FUE LANZADA O CAPTURADA
            finally
            {
                // Si está abierta la conexión a la BD y se ejecuta o no el procedimiento: ciérrala
                CONEXIONMAESTRA.Cerrar();
            }
        }
        // Método utilizado para devolver cuántos usuarios existen en la tabla Personal
        public void ContarPersonal(ref int Contador)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(id_personal) FROM Personal", CONEXIONMAESTRA.conectar);
                // ExecuteScalar(). EJECUTA LA CONSULTA Y DEVUELVE LA PRIMERA COLUMNA DE LA PRIMERA FILA DEL CONJUNTO DE RESULTADOS DEVUELTO POR LA CONSULTA. LAS DEMÁS COLUMNAS O FILAS NO SE TIENEN EN CUENTA.
                // Obtiene cuántos 
                Contador = Convert.ToInt32(cmd.ExecuteScalar());
            }
            // Si hubo algún fallo en el código del try ...
            catch (Exception)
            {
                // ... contador debe inicializarse de nuevo en 0
                Contador = 0;
            }
            // UN BLOQUE finally SIEMPRE SE EJECUTA, INDEPENDIENTEMENTE DE SI UNA EXCEPCIÓN FUE LANZADA O CAPTURADA
            finally
            {
                // Si está abierta la conexión a la BD y se ejecuta o no el procedimiento: ciérrala
                CONEXIONMAESTRA.Cerrar();
            }
        }
    }
}
