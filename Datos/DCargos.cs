using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sistema_de_asistencias.Logica;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Datos
{
    public class DCargos
    {
        public bool InsertarCargo(LCargos parametros)
        {
            // Protección del código
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.abrir();
                // Procedimiento almacenado al que se hará referencia
                SqlCommand cmd = new SqlCommand("InsertarCargo", CONEXIONMAESTRA.conectar);
                // Le indicamos que el procedimiento requiere de parámetros para funcionar
                cmd.CommandType = CommandType.StoredProcedure;
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
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
        public bool EditarCargo(LCargos parametros)
        {
            // Protección del código
            try
            {
                // Se abre la conexión al servidor.
                CONEXIONMAESTRA.abrir();
                // Procedimiento almacenado al que se hará referencia
                SqlCommand cmd = new SqlCommand("EditarCargo", CONEXIONMAESTRA.conectar);
                // Le indicamos que el procedimiento requiere de parámetros para funcionar
                cmd.CommandType = CommandType.StoredProcedure;
                // Pasamos todos los parámetros que requiere el procedimiento almacenado
                cmd.Parameters.AddWithValue("@id", parametros.id_cargo);
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@Sueldo", parametros.SueldoPorHora);
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
        public void BuscarCargos(ref DataTable dt, string buscador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BuscarCargos", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
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
