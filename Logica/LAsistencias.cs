using System;

// Representa los campos de la tabla Asistencias en la base de datos
namespace Sistema_de_asistencias.Logica
{
	public class LAsistencias
	{
		public int Id_asistencia { set; get; }
		public int Id_personal {set; get;}
		public DateTime Fecha_entrada { set; get; }
		public DateTime Fecha_salida { set; get; }
		public String Estado { set; get; }
		public double Horas { set; get; }
		public String Observacion { set; get; }
	}
}
