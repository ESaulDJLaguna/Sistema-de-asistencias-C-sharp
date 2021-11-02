using System;
// Para trabajar con parámetros y SQL Server, se deben importar las librerías
using System.Data;
using System.Data.SqlClient;
using Sistema_de_asistencias.Logica;
// Para utilizar el método Message
using System.Windows.Forms;

namespace Sistema_de_asistencias.Datos
{
    // Clase que representa ("convierte a C#") lso procedimientos almacenados que manipulan la tabla Cargo en la BD: Buscar, Editar, Insertar
    public class DCargos
    {
        // Implementa el procedimiento almacenado que Inserta Cargos en la Tabla Cargo de la BD
        public bool InsertarCargo(LCargos parametros)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo.
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertará, Eliminará, Editará
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("InsertarCargo", CONEXIONMAESTRA.conectar)
                {
                    // Le indicamos que el procedimiento requiere de parámetros para funcionar
                    // Es lo mismo que cmd.CommandType = CommandType.StoredProcedure;
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
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
        // Implementa el procedimiento almacenado que Edita el Cargo en la Tabla Cargo de la BD
        public bool EditarCargo(LCargos parametros)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertar, Eliminar, Editar
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("EditarCargo", CONEXIONMAESTRA.conectar)
                {
                    // Le indicamos que el procedimiento requiere de parámetros para funcionar
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@id", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@Sueldo", parametros.SueldoPorHora);
                // ExecuteNonQuery ES UN MÉTODO QUE EJECUTA UNA INSTRUCCIÓN Transact-SQL EN LA CONEXIÓN Y DEVUELVE EL NÚMERO DE FILAS AFECTADAS
                // Ejecuta la manipulación en la BD editando la información en la tabla Cargo
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
                // Si está abierta la conexión a la BD y se ejecuta o no el procedimiento: ciérrala
                CONEXIONMAESTRA.Cerrar();
            }
        }
        // Representa el procedimiento almacenado que hace una consulta SQL: SELECT id_cargo, Cargo, SueldoPorHora AS [Sueldo Por Hora] FROM Cargo WHERE Cargo LIKE '%' + @Buscador + '%'
        public void BuscarCargos(ref DataTable dt, string buscador)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // SqlDataAdapter REPRESENTA UN CONJUNTO DE COMANDOS DE DATOS Y UNA CONEXIÓN A UNA BD QUE SE USAN PARA RELLENAR DataSet Y ACTUALIZAR UNA BD DE SQL Server. EN SU MAYORÍA SE UTILIZA CUANDO QUEREMOS MOSTRAR INFORMACIÓN DE UNA BD
                // Clase utilizada para mostrar (adaptar) información de una base de datos. Pasamos el procedimiento almacenado al que hará referencia y la conexión a la BD
                SqlDataAdapter da = new SqlDataAdapter("BuscarCargos", CONEXIONMAESTRA.conectar);
                // Indicamos que el procedimiento requiere de parámetros
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                // Pasamos los parámetros
                da.SelectCommand.Parameters.AddWithValue("@Buscador", buscador);
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

    }
}
