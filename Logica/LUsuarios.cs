using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_de_asistencias.Logica
{
    // Representa los campos de la tabla Usuarios en la base de datos
    public class LUsuarios
    {
        public int IdUsuario { set; get; }
        public string Nombre { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public byte[] Icono { set; get; }
        public string Estado { set; get; }
    }
}
