namespace Sistema_de_asistencias.Logica
{
    // Representa los campos de la tabla Personal en la base de datos
    public class LPersonal
    {
        public int Id_personal { set; get; }
        public string Nombres { set; get; }
        public string Identificacion { set; get; }
        public string Pais { set; get; }
        public int Id_cargo { set; get; }
        public double SueldoPorHora { set; get; }
        public string Estado { set; get; }
        public string Codigo { set; get; }
    }
}
