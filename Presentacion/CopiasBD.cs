using Sistema_de_asistencias.Datos;
using Sistema_de_asistencias.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Presentacion
{
    public partial class CopiasBD : UserControl
    {
        public CopiasBD()
        {
            InitializeComponent();
        }
        string ruta;
        string txtSoftware = "Orus369";
        string base_de_datos = "ORUS369";
        private Thread hilo;
        private bool acaba = false;

        private void CopiasBD_Load(object sender, EventArgs e)
        {
            MostrarRuta();
        }
        private void MostrarRuta()
        {
            DCopiasBD funcion = new DCopiasBD();
            funcion.MostrarRuta(ref ruta);
            TxtRuta.Text = ruta;
        }

        private void BtnCopiaSeguridad_Click(object sender, EventArgs e)
        {
            GenerarCopia();
        }
        private void GenerarCopia()
        {
            if(!String.IsNullOrEmpty(TxtRuta.Text))
            {
                hilo = new Thread(new ThreadStart(Ejecucion));
                hilo.Start();
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Selecciona una Ruta dónde Guardar las Copias de Seguridad", "Ruta Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtRuta.Focus();
            }
        }

        private void Ejecucion()
        {
            // Creará una carpeta con este nombre para almacenar las copias de seguridad
            string miCarpeta = "Copias_de_Seguridad_de_" + txtSoftware;
            // Si esa ruta ya existe en nuestra computadora
            if (!System.IO.Directory.Exists(TxtRuta.Text + miCarpeta))
            {
                System.IO.Directory.CreateDirectory(TxtRuta.Text + miCarpeta);
            }
            string rutaCompleta = TxtRuta.Text + miCarpeta;
            string subCarpeta = rutaCompleta + @"\Respaldo_al_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
            // Creamos una subcarpeta donde se guardarán las copias de seguridad
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(rutaCompleta, subCarpeta));
            }
            catch(Exception) { }
            try
            {
                string nombreRespaldo = base_de_datos + ".bak";
                CONEXIONMAESTRA.Abrir();
                SqlCommand cmd = new SqlCommand("BACKUP DATABASE " + base_de_datos + " TO DISK = '" + subCarpeta + @"\" + nombreRespaldo + "'", CONEXIONMAESTRA.conectar);
                cmd.ExecuteNonQuery();
                acaba = true;
            }
            catch (Exception ex)
            {
                acaba = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }
        private void ObtenerRuta()
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                TxtRuta.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(acaba)
            {
                timer1.Stop();
                EditarRespaldo();
            }
        }
        private void EditarRespaldo()
        {
            LCopiasBD parametros = new LCopiasBD();
            DCopiasBD funcion = new DCopiasBD();
            parametros.ruta = TxtRuta.Text;
            if(funcion.EditarCopiasBD(parametros))
            {
                MessageBox.Show("Copia de Base de Datos Generada", "Generación de Copia de BD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
