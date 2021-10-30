using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Logica
{
    public class Bases
    {
        public static void DiseñoDtv(ref DataGridView Listado)
        {
            Listado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // Color de fondo del listado
            Listado.BackgroundColor = Color.FromArgb(29, 29, 29);
            // Inhabilita el estilo por defecto de los encabezados
            Listado.EnableHeadersVisualStyles = false;
            // Elimina los bordes
            Listado.BorderStyle = BorderStyle.None;
            // Elimina los bordes de las celdas
            Listado.CellBorderStyle = DataGridViewCellBorderStyle.None;
            // Elimina los bordes de las columnas
            Listado.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            // Elimina el triángulo del lado izquierdo
            Listado.RowHeadersVisible = false;
            // Crearemos nuevos estilos para la cabecera
            DataGridViewCellStyle cabecera = new DataGridViewCellStyle();
            // Cambia el color de fondo de la cabecera
            cabecera.BackColor = Color.FromArgb(29, 29, 29);
            // Cambia el color de la letra de la cabecera
            cabecera.ForeColor = Color.White;
            // Cambia la fuente de la cabecera, fuente: Segoe UI; tamaño: 10, Negrita
            cabecera.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            // Establece los estilos definidos a la cabecera del objeto DataGridView
            Listado.ColumnHeadersDefaultCellStyle = cabecera;
        }
        public static object Decimales(TextBox CajaTexto, KeyPressEventArgs e)
        {
            // Si al precionar una tecla es un punto o una coma
            if((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                // Enviará lo que tenga el if en la posición 0, o sea, un punto
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }
            // Si lo que se ha presionado es un número decimal
            if(char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            // Sí permite presionar la tecla de borrado
            else if(char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // Si se presionó un punto y ya existe un punto decimal escrito en la caja de texto no permite la escritura
            else if(e.KeyChar == '.' && (~CajaTexto.Text.IndexOf(".") != 0))
            {
                e.Handled = true;
            }
            else if(e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else if(e.KeyChar == ',')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            return null;
        }
    }
}
