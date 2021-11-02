using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Logica
{
    public class Bases
    {
        // Diseño que tendrán todos los DataGridView (elemento gráfico que muestra contenido de un DataTable)
        public static void DiseñoDtv(ref DataGridView Listado)
        {
            // AutoSizeColumnsMode establece cómo se verán los anchos de las columnas, 'AllCells' indica que se ajustará el contenido al ancho de la celda
            Listado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // Color de fondo del DataGridView
            Listado.BackgroundColor = Color.FromArgb(29, 29, 29);
            // Inhabilita el estilo por defecto de los encabezados
            Listado.EnableHeadersVisualStyles = false;
            // Elimina los bordes del DataGridView
            Listado.BorderStyle = BorderStyle.None;
            // Elimina los bordes de las celdas
            Listado.CellBorderStyle = DataGridViewCellBorderStyle.None;
            // Elimina los bordes de las columnas
            Listado.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            // Elimina el triángulo del lado izquierdo
            Listado.RowHeadersVisible = false;
            // UN 'DataGridViewCellStyle' REPRESENTA EL FORMATO Y ESTILO APLICADO A LAS CELDAS INDIVIDUALES DE UN 'DataGridView'
            // Este DataGridViewCellStyle representa una celda cualquiera, pero la pensaremos para darle estilos a la cabecera
            DataGridViewCellStyle cabecera = new DataGridViewCellStyle
            {
                // Cambia el color de fondo de la cabecera
                BackColor = Color.FromArgb(29, 29, 29),
                // Cambia el color de la letra de la cabecera
                ForeColor = Color.White,
                // Cambia la fuente de la cabecera, fuente: Segoe UI; tamaño: 10, Negrita
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            // Establece los estilos definidos a la cabecera del objeto DataGridView
            Listado.ColumnHeadersDefaultCellStyle = cabecera;
        }
        // Método que evalua la tecla presionada actualmente (evento) en un text box que representará un número decimal
        public static object Decimales(TextBox CajaTexto, KeyPressEventArgs e)
        {
            // Si al precionar una tecla es un punto o una coma
            if((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                // Establece KeyChar (el caracter presionado actualmente) con la información que tiene ESTE if en la condición (de la posición) 0 (e.KeyChar == '.'). Solo el punto. O sea, este if solo convierte la coma en punto, NO evalúa si se mostrará en la caja de texto o no.
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }
            // LA PROPIEDAD Handled OBTIENE O ESTABLECE UN VALOR QUE INDICA SI SE CONTROLÓ EL EVENTO    
            // Si la tecla presionada es un número...
            if(char.IsDigit(e.KeyChar))
            {
                // ... no se "bloquea" el teclado y permite mostrarla en el text Box
                e.Handled = false;
            }
            // Sí permite presionar la tecla de borrado
            else if(char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // Si se presionó un punto y ya existe un punto decimal escrito en la caja de texto...
            else if(e.KeyChar == '.' && (~CajaTexto.Text.IndexOf(".") != 0))
            {
                // ... no permite la escritura ("bloquea" el teclado)
                e.Handled = true;
            }
            // Si se precionó un punto, ...
            else if(e.KeyChar == '.')
            {
                // ... lo permite. Se muestra en el Text Box
                e.Handled = false;
            }
            // Si se presiona una coma, ...
            else if(e.KeyChar == ',')
            {
                // ... lo permite. Se mostraría como coma en el Text Box si no existiera el if del principio que convierte la coma en punto.
                e.Handled = false;
            }
            // Bloquea cualquier otro caracter.
            else
            {
                e.Handled = true;
            }
            return null;
        }
        //static int indice = 0;
        public static void DiseñoDtvEliminar(ref DataGridView Listado)
        {
            /*
             * UN 'foreach' SE APLICA A LO QUE ES UN RECORRIDO DE DATOS. SE PUEDE RECORRER: DataGridView, DataTables, ComboBox, etc.
             * DataGridViewRow (class). REPRESENTA UNA FILA EN UN DataGridView
             * DataGridViewRow.Cells. PROPIEDAD QUE OBTIENE LA COLECCIÓN DE CELDAS QUE PUEBLAN LA FILA.
             */
            // Procesa las filas del DataGridView Listado por separadas
            foreach (DataGridViewRow row in Listado.Rows)
            {
                string estado;
                // Almacena el valor que contenga la celda llamada 'Estado'
                estado = row.Cells["Estado"].Value.ToString();
                // Si el estado de la celda actual es 'ELIMINADO' ...
                if(estado == "ELIMINADO")
                {
                    // ... cambia el estilo de la fuente por defecto de la celda. Tipo de letra: Segoe UI; tamaño: 10, Subrayado y Negrita ...
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Strikeout | FontStyle.Bold);
                    // ... además, agrega un color de texto rojo (FromArbg - Color rgb)
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 128, 128);
                }
            }
        }
    }
}
