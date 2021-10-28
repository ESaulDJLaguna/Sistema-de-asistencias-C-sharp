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
        public void MostrarPersonal(ref DataTable dt, int desde, int hasta)
        {
            // Protección del código
            try
            {
                // Abrir la conexión
                CONEXIONMAESTRA.abrir();
                // Clase utilizada para mostrar (adaptar) información de una base de datos
                SqlDataAdapter da = new SqlDataAdapter("MostrarPersonal", CONEXIONMAESTRA.conectar);
                // Indicamos que el procedimiento requiere de parámetros
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                // Pasamos los parámetros
                da.SelectCommand.Parameters.AddWithValue("@Desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
                // Pasamos los datos a la variable dt
                da.Fill(dt);
            }
            // Qué hacer si hay un error
            catch (Exception ex)
            {
                // Como en SQL Server no tenemos un Raiserror, usamos un StackTrace
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                // Cerrar la conexión
                CONEXIONMAESTRA.cerrar();
            }
        }
        public void BuscarPersonal(ref DataTable dt, int desde, int hasta, string buscador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BuscarPersonal", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
                da.SelectCommand.Parameters.AddWithValue("@Buscador", buscador);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }
    }
}
