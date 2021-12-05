using Sistema_de_asistencias.Datos;
using Sistema_de_asistencias.Logica;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_de_asistencias.Presentacion
{
    public partial class TomarAsistencias : Form
    {
        public TomarAsistencias()
        {
            InitializeComponent();
        }
        // Almacenará el número de Identificación proveniente de la BD
        string Identificacion;
        // Se utilizará para modificar la tabla Asistencias utilizando el id de la persona actual
        int idPersonal;
        // Se utiliza para saber si un DataTable contiene registros y por ende, ya se registró la entrada de un personal
        int Contador;
        // DateTime REPRESENTA UN INSTANTE EN EL TIEMPO, TÍPICAMENTE EXPRESADO COMO UNA FECHA Y UNA HORA
        // Almacena la fecha en que se registró la entrada
        DateTime fechaReg;
        // Es utilizado para mostrar la hora en tiempo real
        private void TimerHora_Tick(object sender, EventArgs e)
        {
            // Muestra la fecha en un formato corto: dd/mm/aaaa
            LblFecha2.Text = DateTime.Now.ToShortDateString();
            // Muestra solamente la fecha. HH (formarto de 24h)
            LblHora2.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        // Actualiza la fecha de salida, el estado y las horas trabajadas de un Personal, si su estado ya está marcado como 'ENTRADA'
        private void ConfirmarSalida()
        {
            LAsistencias parametros = new LAsistencias();
            DAsistencias funcion = new DAsistencias();
            // Se utilza para actualizar el estado de un Personal
            parametros.Id_personal = idPersonal;
            // Establece la hora actual (de salida)
            parametros.Fecha_salida = DateTime.Now;
            // Calcula la diferencia entre la fecha actual (salida) con la fecha de entrada
            parametros.Horas = Bases.DateDiff(Bases.DateInterval.Hour, fechaReg, DateTime.Now);
            // Si se actualizó correctamente la BD
            if(funcion.ConfirmarSalida(parametros))
            {
                // Muestra el mensaje de que su salida fue registrada
                LblAviso.Text = "SALIDA REGISTRADA";
                // Limpia el Text Box de Indentificación ...
                TxtIdentificacion.Clear();
                // ... y pon el foco en él
                TxtIdentificacion.Focus();
            }
        }
        // Inserta los datos de la entrada de un trabajador en la tabla Asistencias de la BD
        private void InsertarAsistencias()
        {
            // Si no se quizo agregar una observación almacena un guion en el campo Observación
            if(string.IsNullOrEmpty(TxtObservaciones.Text))
            {
                // No se recomienda que se guarden campos vacíos en la BD
                TxtObservaciones.Text = "-";
            }
            // Llena los parámetros requeridos por el procedimiento de la BD
            LAsistencias parametros = new LAsistencias();
            // Ejecuta el procedimiento e inserta los datos en la BD
            DAsistencias funcion = new DAsistencias();
            // id del trabajador que está entrando
            parametros.Id_personal = idPersonal;
            // Registra la hora de entrada
            parametros.Fecha_entrada = DateTime.Now;
            // La hora de salida será la misma, se modificará después
            parametros.Fecha_salida = DateTime.Now;
            // Se está registrando la ENTRADA de un trabajador
            parametros.Estado = "ENTRADA";
            // Apenas comienza a trabajar, por lo que sus horas trabajadas son 0
            parametros.Horas = 0;
            // Guarda las observaciones
            parametros.Observacion = TxtObservaciones.Text;
            // Si se insertaron los datos correctamente en la BD
            if (funcion.InsertarAsistencias(parametros))
            {
                // Indica que se registró la entrada de forma correcta
                LblAviso.Text = "ENTRADA REGISTRADA";
                // Limpia la TextBox de identificación para registrar (E/S) de otro trabajador
                TxtIdentificacion.Clear();
                // Pon el foco en el Text Box de Identificación
                TxtIdentificacion.Focus();
                // No será visible el Panel de Observaciones
                PanelObservacion.Visible = false;
                // Borra las observaciones que se hayan hecho en el cuadro de texto
                TxtObservaciones.Clear();
            }
        }
        // Busca si un usuario ya registró su entrada, de ser así, obten la fecha de entrada
        private void BuscarAsistenciasPorId()
        {
            // Contendrá todos los campos de la consulta si se encontró un id usuario 
            DataTable dt = new DataTable();
            // Ejecuta el procedimiento en la BD
            DAsistencias funcion = new DAsistencias();
            // Busca en tabla Asistencias a un usuario con el idPersonal que su estado sea 'ENTRADA'
            funcion.BuscarAsistenciasPorId(ref dt, idPersonal);
            // Si se encontró el usuario, dt tendrá todos los campos de la tabla
            Contador = dt.Rows.Count;
            // Si se encontró al usuario (ya registró su entrada) ...
            if(Contador > 0)
            {
                // ... obten la fecha en que se registró su entrada
                fechaReg = Convert.ToDateTime(dt.Rows[0]["fecha_entrada"]);
            }
        }
        // Busca un usuario por su Identificación y si existe, muestra su nombre en LblNombre y guarda su id e identificación
        private void BuscarPersonalIdentidad()
        {
            // Almacenará los campos de la consulta
            DataTable dt = new DataTable();
            // Llamamos a la capa de Datos para hacer uso del procedimiento en la BD
            DAsistencia funcion = new DAsistencia();
            // 'dt' contiene todos los campos de la tabla Personal del usuario que coincida con TxtIdentificacion el cual se utiliza como parámetro de búsqueda en el procedimiento de la BD
            funcion.BuscarPersonalIdentidad(ref dt, TxtIdentificacion.Text);
            // Si el número de filas es mayor que 0, significa que sí existe un Personal con esa identificación
            if(dt.Rows.Count > 0)
            {
                // De la primer fila (la única obtenida en la consulta) obten el contenido de la celda que pertenece a la columna llamada 'Identificación' (así se llama en la consulta)
                Identificacion = dt.Rows[0]["Identificacion"].ToString();
                // Obten el contenido de la columna llamada 'id_personal' de la primer fila
                idPersonal = Convert.ToInt32(dt.Rows[0]["id_personal"]);
                // Muestra el Nombre del usuario 
                LblNombre.Text = dt.Rows[0]["Nombres"].ToString();
            }
        }
        // Evento que se desencadena al dar clic en el Botón Confirmar del Panel de Observaciones
        private void BtnConfirmarObservacion_Click(object sender, EventArgs e)
        {
            // Inserta la información de entrada de un trabajador en la tabla Asistencias
            InsertarAsistencias();
        }
        // Evento que se ejecuta al dar clic en el botón Registrar Entrada/Salida
        private void BtnRegistrarES_Click(object sender, EventArgs e)
        {
            // Busca un usuario por su Identificación y si existe, muestra su nombre en LblNombre y guarda su id e identificación
            BuscarPersonalIdentidad();
            // Si Identificacion es igual al usuario buscado
            if (Identificacion == TxtIdentificacion.Text)
            {
                // Busca si un usuario ya registró su entrada, de ser así, obten la fecha de entrada
                BuscarAsistenciasPorId();
                // El trabajador todavía no ha registrado su hora de entrada
                if (Contador == 0)
                {
                    // Se pregunta si se quieren agregar observaciones sobre la entrada de un trabajador
                    DialogResult resultado = MessageBox.Show("¿Agregar una observación?", "Observación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    // Si se quieren agregar observaciones de un trabajador
                    if (resultado == DialogResult.OK)
                    {
                        // Se mostrará el Panel de Observaciones que por defecto está oculto
                        PanelObservacion.Visible = true;
                        // Localiza el panel en el mismo lugar que está el Panel principal
                        PanelObservacion.Location = new Point(PanelPrincipal.Location.X, PanelPrincipal.Location.Y);
                        // El PanelObservaciones adopta el mismo tamaño que el Panel Principal
                        PanelObservacion.Size = new Size(PanelPrincipal.Width, PanelPrincipal.Height);
                        // Centra horizontalmente el Botón de confirmación (al dar clic en él se almacenará la entrada en la BD)
                        BtnConfirmarObservacion.Location = new Point((PanelPrincipal.Width - BtnConfirmarObservacion.Width) / 2, BtnConfirmarObservacion.Location.Y);
                        // Trae al frente el Panel de Observaciones
                        PanelObservacion.BringToFront();
                        // Limpia el texto por si anteriormente se escribió una observación
                        TxtObservaciones.Clear();
                        // Pon el foco en el cuadro de texto
                        TxtObservaciones.Focus();
                    }
                    // Si no se quiere agregar una observación ...
                    else
                    {
                        // ... Inserta la hora de entrada en la tabla Asistencias
                        InsertarAsistencias();
                    }
                }
                // El trabajador registrará su hora de salida
                else
                {
                    // Actualiza la fecha de salida, el estado y las horas trabajadas de un Personal, si su estado ya está marcado como 'ENTRADA'
                    ConfirmarSalida();
                }
            }
        }

        private void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            Dispose();
            Login frm = new Login();
            frm.ShowDialog();
        }
    }
}
