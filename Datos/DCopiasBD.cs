using Sistema_de_asistencias.Logica;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Datos
{
    public class DCopiasBD
    {
        // Implementa el procedimiento almacenado que Inserta Copias en la Tabla CopiasBD de la BD
        public bool InsertarCopiasBD()
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertará, Eliminará, Editará
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("InsertarCopiasBD", CONEXIONMAESTRA.conectar);
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
        public void MostrarRuta(ref String ruta)
        {
            try
            {
                CONEXIONMAESTRA.Abrir();
                SqlCommand da = new SqlCommand("SELECT Ruta FROM CopiasBD", CONEXIONMAESTRA.conectar);
                ruta = Convert.ToString(da.ExecuteScalar());
                CONEXIONMAESTRA.Cerrar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public bool EditarCopiasBD(LCopiasBD parametros)
        {
            // Protección del código. Evita que se detenga la aplicación en caso de algún fallo
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.Abrir();
                // SqlCommand REPRESENTA UN PROCEDIMIENTO ALMACENADO O UNA INSTRUCCIÓN DE Transact-SQL QUE SE EJECUTA EN UNA BD DE SQL SERVER. SE USA CUANDO SE MODIFICARÁ LA INFORMACIÓN EN UNA BD: Insertará, Eliminará, Editará
                // Procedimiento almacenado en la BD al que se hará referencia; se le indica la conexión a la base de datos
                SqlCommand cmd = new SqlCommand("EditarCopiasBD", CONEXIONMAESTRA.conectar);
                // Requiere parámetros para funcionar
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ruta", parametros.ruta);
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
    }
}
