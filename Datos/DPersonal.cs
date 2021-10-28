using System;
using System.Collections.Generic;
using System.Text;
// Para trabajar con parámetros y SQL Server, se deben importar sus librerías
using System.Data;
using System.Data.SqlClient;
using Sistema_de_asistencias.Logica;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Datos
{
    public class DPersonal
    {
        public bool InsertarPersonal(LPersonal parametros)
        {
            // Protección del código
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.abrir();
                // Procedimiento almacenado al que se hará referencia
                SqlCommand cmd = new SqlCommand("InsertarPersonal", CONEXIONMAESTRA.conectar);
                // Le indicamos que el procedimiento requiere de parámetros para funcionar
                cmd.CommandType = CommandType.StoredProcedure;
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificación", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@País", parametros.Pais);
                cmd.Parameters.AddWithValue("@id_cargo", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
                // Ejecuta el procedimiento almacenado
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
                CONEXIONMAESTRA.cerrar();
            }
        }
        public bool EditarPersonal(LPersonal parametros)
        {
            // Protección del código
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.abrir();
                // Procedimiento almacenado al que se hará referencia
                SqlCommand cmd = new SqlCommand("EditarPersonal", CONEXIONMAESTRA.conectar);
                // Le indicamos que el procedimiento requiere de parámetros para funcionar
                cmd.CommandType = CommandType.StoredProcedure;
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificación", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@País", parametros.Pais);
                cmd.Parameters.AddWithValue("@id_cargo", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
                // Ejecuta el procedimiento almacenado
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
                CONEXIONMAESTRA.cerrar();
            }
        }
        public bool EliminarPersonal(LPersonal parametros)
        {
            // Protección del código
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.abrir();
                // Procedimiento almacenado al que se hará referencia
                SqlCommand cmd = new SqlCommand("EliminarPersonal", CONEXIONMAESTRA.conectar);
                // Le indicamos que el procedimiento requiere de parámetros para funcionar
                cmd.CommandType = CommandType.StoredProcedure;
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@id_personal", parametros.Id_personal);
                // Ejecuta el procedimiento almacenado
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
                CONEXIONMAESTRA.cerrar();
            }
        }

    }
}
