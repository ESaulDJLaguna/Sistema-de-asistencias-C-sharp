using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_de_asistencias.Logica
{
    // Representa los campos de la tabla Permisos en la base de datos
    public class LPermisos
    {
        public int IdPermiso { set; get; }
        public int IdModulo { set; get; }
        public int IdUsuario { set; get; }
    }
}
