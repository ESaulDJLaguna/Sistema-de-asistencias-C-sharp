using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sistema_de_asistencias.Logica;
using Sistema_de_asistencias.Datos;
using System.IO;

namespace Sistema_de_asistencias.Presentacion.AsistenteInstalación
{
    public partial class UsuarioPrincipal : Form
    {
        public UsuarioPrincipal()
        {
            InitializeComponent();
        }

        int idUsuario;

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(TxtNombreUsuario.Text))
            {
                if(!string.IsNullOrEmpty(TxtPasswrd.Text))
                {
                    if(TxtPasswrd.Text == TxtConfPass.Text)
                    {
                        InsertarUsuarioDefecto();
                    }
                    else
                    {
                        MessageBox.Show("Las Contraseñas no coinciden", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Falta ingresar la Contraseña", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Falta ingresar el Nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertarUsuarioDefecto()
        {
            LUsuarios parametros = new LUsuarios();
            DUsuarios funcion = new DUsuarios();
            parametros.Nombre = TxtNombreUsuario.Text;
            parametros.Login = TxtUsuario.Text;
            parametros.Password = TxtPasswrd.Text;
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            if(funcion.InsertarUsuario(parametros))
            {
                InsertarCopiasBD();
                InsertarModulos();
                ObtenerIdUsuario();
                InsertarPermisos();
            }
        }
        private void InsertarCopiasBD()
        {
            DCopiasBD funcion = new DCopiasBD();
            funcion.InsertarCopiasBD();
        }

        private void ObtenerIdUsuario()
        {
            DUsuarios funcion = new DUsuarios();
            funcion.ObtenerIdUsuario(ref idUsuario, TxtUsuario.Text);

        }

        private void InsertarPermisos()
        {
            DataTable dt = new DataTable();
            DModulos funcionModulos = new DModulos();
            funcionModulos.MostrarModulos(ref dt);
            foreach(DataRow row in dt.Rows)
            {
                int idModulo = Convert.ToInt32(row["IdModulo"]);
                LPermisos parametros = new LPermisos();
                DPermisos funcion = new DPermisos();
                parametros.IdModulo = idModulo;
                parametros.IdUsuario = idUsuario;
                funcion.InsertarPermisos(parametros);
            }
            MessageBox.Show("¡LISTO! RECUERDA que para Iniciar Sesión tu Usuario es: " + TxtUsuario.Text + ", y tu Contraseña es: " + TxtPasswrd.Text, "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Dispose();
            Login frm = new Login();
            frm.ShowDialog();
        }

        private void InsertarModulos()
        {
            LModulos parametros = new LModulos();
            DModulos funcion = new DModulos();
            var modulos = new List<string> { "Usuarios", "Respaldos", "Personal", "PrePlanillas" };
            foreach( var modulo in modulos)
            {
                parametros.Modulo = modulo;
                funcion.InsertarModulos(parametros);
            }
        }
    }
}
