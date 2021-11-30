using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Sistema_de_asistencias.Logica;

namespace Sistema_de_asistencias.Datos
{
    public class DUsuarios
    {
        // Método que inserta usuarios 
        public bool InsertarUsuario(LUsuarios parametros)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertará, Eliminará, Editará
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("InsertarUsuario", CONEXIONMAESTRA.conectar)
                {
                    // Le indicamos que el procedimiento requiere de parámetros para funcionar
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombre);
                cmd.Parameters.AddWithValue("@Login", parametros.Login);
                cmd.Parameters.AddWithValue("@Password", parametros.Password);
                cmd.Parameters.AddWithValue("@Icono", parametros.Icono);
                cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
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

        // Almacena en dt todos los registros con todos sus campos de la tabla Usuarios
        public void MostrarUsuarios(ref DataTable dt)
        {
            // Protección del código, evita que se detenga la aplicación si salta una excepción
            try
            {
                // Abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // Representa un conjunto de comandos de datos y una conexión a una BD. Se utilizará para rellenar un DataTable. Pasamos la dirección del servidor y la BD a la que nos conectaremos
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Usuarios", CONEXIONMAESTRA.conectar);
                // Llena un DataTable con los resultados devuentos en la consulta SQL anterior
                da.Fill(dt);
            }
            // Si existe algún error al conectarse al servidor o a la BD
            catch (Exception ex)
            {
                // Muestra un mensaje de error
                MessageBox.Show(ex.StackTrace);
            }
            // finally siempre se ejecutará haya o no una excepción
            finally
            {
                // Cierra la conexión al servidor
                CONEXIONMAESTRA.Cerrar();
            }
        }

        // Recupera el id del nombre de usuario que se pasa como parámetro (Login)
        public void ObtenerIdUsuario(ref int IdUsuario, string Login)
        {
            // Protege el código de alguna excepción
            try
            {
                // Abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // Objeto que manipulará la BD. Su constructor recibe el procedimiento a ejecutar y la BD a la que se conectará
                SqlCommand cmd = new SqlCommand("ObtenerIdUsuario", CONEXIONMAESTRA.conectar)
                {
                    // El procedimiento requiere parámetros para funcionar
                    CommandType = CommandType.StoredProcedure
                };
                // Relacionamos los parámetros del SQL y C#
                cmd.Parameters.AddWithValue("@Login", Login);
                // ExecuteScalar() devuelve el valor de la primer columna de la primer fila de la consulta
                IdUsuario = Convert.ToInt32(cmd.ExecuteScalar());
            }
            // Si hubo algún error ...
            catch (Exception ex)
            {
                // ... muestra un mensaje
                MessageBox.Show(ex.StackTrace);
            }
            // Siempre se ejecuta un bloque finally
            finally
            {
                // Hay que cerrar la conexión al servidor
                CONEXIONMAESTRA.Cerrar();
            }
        }

        public bool EliminarUsuarios(LUsuarios parametros)
        {
            try
            {
                CONEXIONMAESTRA.Abrir();
                SqlCommand cmd = new SqlCommand("EliminarUsuarios", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
                cmd.Parameters.AddWithValue("@login", parametros.Login);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                CONEXIONMAESTRA.Cerrar();
            }
        }

        public bool RestaurarUsuario(LUsuarios parametros)
        {
            try
            {
                CONEXIONMAESTRA.Abrir();
                SqlCommand cmd = new SqlCommand("RestaurarUsuario", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                CONEXIONMAESTRA.Cerrar();
            }
        }

        // Manipula la BD y edita la información de los usuarios
        public bool EditarUsuarios(LUsuarios parametros)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertará, Eliminará, Editará
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("EditarUsuarios", CONEXIONMAESTRA.conectar)
                {
                    // Le indicamos que el procedimiento requiere de parámetros para funcionar
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@Id", parametros.IdUsuario);
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombre);
                cmd.Parameters.AddWithValue("@Usuario", parametros.Login);
                cmd.Parameters.AddWithValue("@Password", parametros.Password);
                cmd.Parameters.AddWithValue("@Icono", parametros.Icono);
                cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
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
        
        public void BuscarUsuarios(ref DataTable dt, string buscador)
        {
            try
            {
                CONEXIONMAESTRA.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("BuscarUsuarios", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                CONEXIONMAESTRA.Cerrar();
            }
        }

        // Comprueba que la tabla de Usuarios esté creada
        public void VericarUsuarios(ref string indicador)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo.
            try
            {
                int idUser;
                // Abre la conexión al servidor
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertará, Eliminará, Editará
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand da = new SqlCommand("SELECT idUsuario FROM Usuarios", CONEXIONMAESTRA.conectar);
                // ExecuteNonQuery. Ejecuta una instrucción de Transact-SQL en la conexión y devuelve el número de filas afectadas (con insert, update o delete). Si se usa un select devuevel -1
                idUser = Convert.ToInt32(da.ExecuteNonQuery());
                // Cierra la conexión al servidor
                CONEXIONMAESTRA.Cerrar();
                // Se usará para indicar que la BD existe en el servidor.
                indicador = "Correcto";
            }
            // Si la tabla no existe mandará una excepción
            catch (Exception)
            {
                // Se usará para abrir el formulario de instalación
                indicador = "Incorrecto";
            }
        }

        // Recupera el id de un usuario si coincide su nombre de usuario, contraseña y aún está activo
        public bool ValidarUsuario(LUsuarios parametros, ref int id)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertará, Eliminará, Editará
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("ValidarUsuario", CONEXIONMAESTRA.conectar)
                {
                    // Le indicamos que el procedimiento requiere de parámetros para funcionar
                    CommandType = CommandType.StoredProcedure
                };
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@passwrd", parametros.Password);
                cmd.Parameters.AddWithValue("@login", parametros.Login);
                // ExecuteScalar. DEVUELVE EL VALOR DE LA PRIMER COLUMNA DE LA PRIMER FILA DE SU CONSULTA.
                // En este caso devuelve el id del usuario si es encontrado
                id = Convert.ToInt32(cmd.ExecuteScalar());
                // Indica que todo ha salido correctamente al insertar la información en la BD
                return true;
            }
            // Si no encuentra al usuario que se está buscando...
            catch (Exception)
            {
                // ... establece el id en 0
                id = 0;
                return false;
            }
            finally
            {
                // Cierra la conexión al servidor
                CONEXIONMAESTRA.Cerrar();
            }
        }
    }
}
