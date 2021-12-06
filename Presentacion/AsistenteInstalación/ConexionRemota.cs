using Sistema_de_asistencias.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Sistema_de_asistencias.Presentacion.AsistenteInstalación
{
    public partial class ConexionRemota : Form
    {
        //* CONSTRUCTOR
        public ConexionRemota()
        {
            InitializeComponent();
        }

        //* CAMPOS GLOBALES
        string cadenaDeConexion;
        string indicadorDeConexion;
        private Encriptacion aes_256 = new Encriptacion();
        int id;

        //* MÉTODOS
        private void BtnConectar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(TxtIP.Text))
            {
                ConectarManualmente();
            }
            else
            {
                MessageBox.Show("Ingresa la IP", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ConectarManualmente()
        {
            string ip = TxtIP.Text;
            cadenaDeConexion = "Data Source =" + ip + ";Initial Catalog=ORUS369;Integrated Security=False;User Id=orus;Password=orus369";
            ComprobarConexion();
            MessageBox.Show(indicadorDeConexion);
            if(indicadorDeConexion == "HAY CONEXIÓN")
            {
                SaveToXml(aes_256.Encrypt(cadenaDeConexion, Desencriptacion.appPwdUnique, int.Parse("256")));
                MessageBox.Show("Conexión correcta. Vuelva a abrir el sistema.", "Conexión Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
            }
        }
        private void SaveToXml(object dbCnString)
        {
            XmlDocument doc = new XmlDocument();
            // Lee el archivo xml dentro de la carpeta bin
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbCnString);
            // Sobreescribirá el archivo
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            // Lo sobreescribe
            doc.Save(writer);
            writer.Close();
        }
        private void ComprobarConexion()
        {
            try
            {
                SqlConnection conexionManual = new SqlConnection(cadenaDeConexion);
                conexionManual.Open();
                SqlCommand da = new SqlCommand("SELECT * FROM Usuarios", conexionManual);
                id = Convert.ToInt32(da.ExecuteScalar());
                indicadorDeConexion = "HAY CONEXIÓN";
            }
            catch (Exception ex)
            {
                indicadorDeConexion = "NO HAY CONEXIÓN";
            }
        }
    }
}
