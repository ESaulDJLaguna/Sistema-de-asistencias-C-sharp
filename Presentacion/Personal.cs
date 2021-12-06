using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Sistema_de_asistencias.Logica;
using Sistema_de_asistencias.Datos;

namespace Sistema_de_asistencias.Presentacion
{
    public partial class Personal : UserControl
    {
        public Personal()
        {
            InitializeComponent();
        }
        // Obtendrá el id del cargo actual. Se utiliza para manipular la BD que requeire ese id_cargo
        int idCargo = 0;
        //  Se utilizarán en el procedimiento almacenado que busca y muestra Personal, para indicar ...
        int desde = 1;
        // desde qué fila hasta cual será la que mostrarán resultados
        int hasta = 10;
        // Se mostrarán solamente 10 resultados de Personal
        private readonly int itemsPorPagina = 10;
        // Almacena el número de usuarios que existen en la tabla Personal
        int contador;
        // Almacena el id del usuario
        int idPersonal;
        // Almacena el estado de un usuario: ACTIVO o ELIMINADO
        string estado;
        // Almacena cuántas páginas de usuarios existen (cada una mostrará 10 usuarios)
        int totalPaginas;
        // Evento que ocurrirá al dar clic en el botón (azul) utilizado para agregar nuevo personal
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            // Posiciona el DataGridView que muestra los cargos
            LocalizarDtvCargos();
            // El panel para agregar nuevos cargos no será visible
            PanelCargos.Visible = false;
            // El panel inferior de paginano, no será visible
            PanelPaginado.Visible = false;
            // El panel de registros, será visible...
            PanelRegistros.Visible = true;
            // ... y ocupará el total del panel
            PanelRegistros.Dock = DockStyle.Fill;
            // Ocultar el panel del buscador y agregar nuevo personal
            panel1.Visible = false;
            // El botón para guardar personal será visible
            BtnGuardarPersonal.Visible = true;
            // El botón para guardar cambios en el personal no se verá
            BtnGuardarCambiosPersonal.Visible = false;
            // Método que limpia los textBox
            Limpiar();
        }
        // Método para posicionar el dataGridView de Cargos
        private void LocalizarDtvCargos()
        {
            // Posiciona el listado de Cargos en una coordenada del 'User Control'
            DataListadoCargos.Location = new Point(TxtSueldoHora.Location.X, TxtSueldoHora.Location.Y);
            // Modificamos el tamaño del listado de Cargos
            DataListadoCargos.Size = new Size(300, 250);
            // Que sea visible el listado de Cargos
            DataListadoCargos.Visible = true;
            // Ocultamos el label 'Sueldo Por Hora'
            lblSueldo.Visible = false;
            // Se ocultan los botones de Guardar (personal)
            PanelBtnGuardarPersonal.Visible = false;
        }
        // Método para limpiar los textBox de la parte para agregar personal
        private void Limpiar()
        {
            // Limpiamos el textBox de Nombre
            //* Otra forma de limpiar sería: txtNombre.Text = "";
            TxtNombre.Clear();
            // Limpiamos el textBox de Identificación
            TxtIdentificacion.Clear();
            // Limpiamos el textBox de Cargo
            TxtCargo.Clear();
            // Limpiamos el textBox de Sueldo por hora
            TxtSueldoHora.Clear();
            // Limpiamos el país
            //* CbxPais.SelectedIndex = 0; // Elige una opción por defecto de toda la lista
            CbxPais.Text = "";
            // Muestra los Cargos en el DataGridView (dataListadoCargos), si no se llama se verá un cuadro gris
            BuscarCargos();
        }
        // Evento que ocurrirá al dar clic en el botón utilizado para guardar información del nuevo personal
        private void BtnGuardarPersonal_Click(object sender, EventArgs e)
        {
            // El 'Nombre y apellido' no puede estar vacío
            if(!string.IsNullOrEmpty(TxtNombre.Text))
            {
                // La 'Identificación' no puede estar vacía
                if(!string.IsNullOrEmpty(TxtIdentificacion.Text))
                {
                    // El 'País' no puede estar vacío
                    if(!string.IsNullOrEmpty(CbxPais.Text))
                    {
                        // El cargo no debe ser 0 (por lo tanto, se debió elegir un cargo)
                        if(idCargo > 0)
                        {
                            // El 'Sueldo por hora' no puede estar vacío
                            if(!string.IsNullOrEmpty(TxtSueldoHora.Text))
                            {
                                InsertarPersonal();
                            }
                            else
                            {
                                MessageBox.Show("Agregue el Sueldo", "Falta el Sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Agregue el cargo", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Agregue el país", "Falta el país", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Agregue la identificación", "Falta la identificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agregue el nombre y apellido", "Falta el nombre y apellido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // Inserta la información de los TextBox del Personal en la BD
        private void InsertarPersonal()
        {
            LPersonal parametrosPasar = new LPersonal();
            DAsistencia funcionesEjecutar = new DAsistencia();
            // Llena los parámetros de la capa Lógica Personal con la información de los Text Box del panelRegistros
            parametrosPasar.Nombres = TxtNombre.Text;
            parametrosPasar.Identificacion = TxtIdentificacion.Text;
            parametrosPasar.Pais = CbxPais.Text;
            parametrosPasar.Id_cargo = idCargo;
            // El sueldo debe convertirse a Double
            parametrosPasar.SueldoPorHora = Convert.ToDouble(TxtSueldoHora.Text);
            // Si la insersión del Personal en la BD fue correcta ...
            if(funcionesEjecutar.InsertarPersonal(parametrosPasar))
            {
                ReiniciarPaginado();
                // ,, muestra el personal en el DataGridView del personal
                MostrarPersonal();
                // Oculta el panel de agregar nuevo registro de Personal
                PanelRegistros.Visible = false;
            }
        }
        // Muestra el personal en el DataGridView que lista el personal. Recordemos que el procedimiento MostrarPersonal en la BD está generando una columna nueva llamada 'Numero_de_fila', que enumera todos los registros del 1 hasta n (donde sería el total de registros que hay -Supongamos que hemos agregado 100 usuarios, así que Numero_de_fila enumerará del 1 al 100 cada registro); pero este procedimiento solo esta mostrando los registros según indiquen los parámetros @Desde y @Hasta
        private void MostrarPersonal()
        {
            // UN DataTable REPRESENTA UNA TABLA DE DATOS EN MEMORIA (SE MUESTRA SU CONTENIDO EN UN DataGridView)
            DataTable dt = new DataTable();
            DAsistencia funcion = new DAsistencia();
            // Llena la tabla (DataTable) dt con los datos obtenidos de la BD
            funcion.MostrarPersonal(ref dt, desde, hasta);
            // Muestra la tabla en un objeto visual (DataGridView)
            DataListadoPersonal.DataSource = dt;
            // Agrega nuevos estilos al DataGridView que representa el listado del personal
            DiseñarDtvPersonal();

        }
        // Elimina los estilos por defecto del DataGridView que muestra todo el Personal
        private void DiseñarDtvPersonal()
        {
            // Agrega nuevos estilos al DataGridView
            Bases.DiseñoDtv(ref DataListadoPersonal);
            // Agrega estilos distinto a usuarios eliminados
            Bases.DiseñoDtvEliminar(ref DataListadoPersonal);
            // Muestra el Panel del paginado (panel inferior)
            PanelPaginado.Visible = true;
            // La columna del id_personal no será visible
            DataListadoPersonal.Columns[2].Visible = false;
            // La columna del id_cargo no será visible
            DataListadoPersonal.Columns[7].Visible = false;
        }
        // Se ejecuta cuando se guardará (al presionar botón de guardar cargos) un nuevo cargo. Agrega el cargo en la base de datos
        private void InsertarCargo()
        {
            // Si se ha agregado algo en el textBox Cargo
            if(!string.IsNullOrEmpty(TxtCargoG.Text))
            {
                // Si también se ha agregado algo en 'Sueldo por hora'
                if(!string.IsNullOrEmpty(TxtSueldoHoraG.Text))
                {
                    // Contendrá los parámetros que se pasarán al procedimiento almacenado de la base de datos
                    LCargos parametros = new LCargos();
                    // Hace referencia a los métodos que se comunican con la base de datos
                    DCargos funciones = new DCargos();
                    // Obtiene el contenido del text Box Cargo
                    parametros.Cargo = TxtCargoG.Text;
                    // Obtiene el contenido del text Box Sueldo Por Hora
                    parametros.SueldoPorHora = Convert.ToDouble(TxtSueldoHoraG.Text);
                    // Inserta información a la base de datos y si todo salió correcto ejecuta
                    if (funciones.InsertarCargo(parametros) == true)
                    {
                        // Limpia el text Box de Cargo en la pantalla de agregar personal
                        TxtCargo.Clear();
                        // Muestra los resultados de la consulta hecha en el procedimiento almacenado de búsqueda de cargos creado en la base de datos
                        BuscarCargos();
                        // Una vez que se guardó el nuevo cargo en la base de datos, ya no debe mostrarse el panel para agregar nuevos cargos
                        PanelCargos.Visible = false;
                    }
                }
                // Ya se agregó información en el textBox Cargo, pero no se ha agregado nada en el textBox 'Sueldo Por Hora'
                else
                {
                    MessageBox.Show("Agregue el Sueldo", "Falta el Sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            // No se ha agregado nada en el textBox Cargo
            else
            {
                MessageBox.Show("Agregue el Cargo", "Falta el Cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // Muestra el resultado de la consulta que se programó en el procedimiento almacenado BuscarCargos en la base de datos
        private void BuscarCargos()
        {
            //* Un DataTable representa una tabla de datos en memoria (se muestra su contenido en un DataGridView)
            DataTable dt = new DataTable();
            // Se utilizará el método de búsqueda en la capa de Datos de Cargo
            DCargos funciones = new DCargos();
            // dt almacenará los resultados obtenidos por la consulta SQL de búsqueda. El parámetro de búsqueda será lo que se está escribiendo en el TextBox de Cargo.
            funciones.BuscarCargos(ref dt, TxtCargo.Text);
            // Muestra la información de la tabla (DataTable): id_cargo, Cargo y SueldoPorHora, de forma gráfica en un DataGridView
            DataListadoCargos.DataSource = dt;
            // Cambia el diseño por defecto de un DataGridView
            Bases.DiseñoDtv(ref DataListadoCargos);
            // Recordar que la columna 0 del dataListadoCargos es la imagen (lapiz) que se usa para editar.
            // La columna 1 del dataListadoCargos son los id_Cargo: no los muestres
            DataListadoCargos.Columns[1].Visible = false;
            // La columna 3 del dataListadoCargos son los SueldosPorHora: no se muestran
            DataListadoCargos.Columns[3].Visible = false;
            // Haz visible el dataListadoCargos por si en algún evento anterior se ocultó
            DataListadoCargos.Visible = true;
        }
        //* TextChanged ES UN EVENTO QUE SE PRODUCE CUANDO CAMBIAN LAS PROPIEDADES Text Y SelectedValue
        // Cada vez que se escribe una letra en el TextBox de Cargo...
        private void TxtCargo_TextChanged(object sender, EventArgs e)
        {
            // ... busca en la BD todas las coincidencias con la tecla presionada
            BuscarCargos();
        }
        // Evento que ocurrirá al dar clic en el botón (+ Agregar cargo). Muestra el panel para agregar un nuevo cargo
        private void BtnAgregarCargo_Click(object sender, EventArgs e)
        {
            // Haz visible el PanelCargos
            PanelCargos.Visible = true;
            // Ocupa todo el contenido del panel padre
            PanelCargos.Dock = DockStyle.Fill;
            // Trae al frente de todo
            PanelCargos.BringToFront();
            // Haz visible el botón para Guardar un nuevo cargo en la BD
            BtnGuardarCargo.Visible = true;
            // No muestres el botón para Editar el cargo en la BD
            BtnGuardarCambiosCargo.Visible = false;
            // Limpia el Text Box de Cargo
            TxtCargoG.Clear();
            // Limpia el Text Box del Sueldo por hora
            TxtSueldoHoraG.Clear();
        }
        // Evento que ocurre al presionar el botón para guardar un nuevo cargo
        private void BtnGuardarCargo_Click(object sender, EventArgs e)
        {
            // Método que evalúa los TextBox de la información del cargo y lo inserta en la BD
            InsertarCargo();
        }
        // Evento que se ejecuta cada vez que se presiona una tecla en el text Box 'Sueldo por hora' del PanelCargos
        private void TxtSueldoHoraG_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Evalúa que los caracteres ingresados sean los correctos para un tipo decimal
            Bases.Decimales(TxtSueldoHoraG, e);
        }
        // Evento que se ejecuta cuando se presiona una tecla en el textBox 'Sueldo por hora' del PanelRegistros
        private void TxtSueldoHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Evalúa que los caracteres ingresados sean los correctos para un tipo decimal
            Bases.Decimales(TxtSueldoHora, e);
        }
        // Evento que se ejecuta cuando se seleccione una celda de un DataGridView. Queremos recuperar el id del cargo seleccionado en dataListadoCargos
        private void DataListadoCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /* * 
             - e.ColumnIndex obtiene el índice de la columna presionada (0 es la imagen de lápiz).
             - Columns (de un DataGridView) es una propiedad que obtiene una colección que contiene todas las columnas del DataGridView
             */
            // Si se hace clic en una columna del listado de Cargos y coincide con el índice de la columna llamada EditarC (imagen lapiz) ...
            if (e.ColumnIndex == DataListadoCargos.Columns["EditarC"].Index)
            {
                // ... edita el cargo seleccionado
                ObtenerCargosEditar();
            }
            // Si la celda seleccionada coincide con un nombre de Cargo ...
            if(e.ColumnIndex == DataListadoCargos.Columns["Cargo"].Index)
            {
                // ... establece los Text Box en el panel del personal con el cargo y sueldo correspondiente
                ObtenerDatosCargos();
            }
        }
        // Rellena los Text Box Cargo y Sueldo por hora del PanelRegistros con la información del cargo seleccionado en el dataListadoCargos
        private void ObtenerDatosCargos()
        {
            //* SelectedCells OBTIENE LA COLECCIÓN DE CELDAS SELECCIONADAS POR EL USUARIO
            // Obtiene el id del cargo seleccionado (posteriormente se utilizará para guardarlo en la BD en la tabla del Personal)
            idCargo = Convert.ToInt32(DataListadoCargos.SelectedCells[1].Value);
            // Rellena el Text Box Cargo (del panel Personal) con el contenido del cargo seleccionado
            TxtCargo.Text = DataListadoCargos.SelectedCells[2].Value.ToString();
            // Rellena el Text Box 'Sueldo por hora' (del panel Personal) con el valor de la celda seleccionada
            TxtSueldoHora.Text = DataListadoCargos.SelectedCells[3].Value.ToString();
            // El DataGridView con la tabla de cargos se oculta
            DataListadoCargos.Visible = false;
            // Los botones serán visibles
            PanelBtnGuardarPersonal.Visible = true;
            // El label 'Sueldo por hora' del Panel Personal es visible
            lblSueldo.Visible = true;

        }
        // Cuando se dio a elegir un cargo se presionó el botón editar (imagen del lápiz)
        private void ObtenerCargosEditar()
        {
            //* SelectedCells OBTIENE LA COLECCIÓN DE CELDAS SELECCIONADAS POR EL USUARIO
            // Se obtiene el id del cargo de la celda seleccionada para modificarlo en la BD
            idCargo = Convert.ToInt32(DataListadoCargos.SelectedCells[1].Value);
            // Rellena el Text Box Cargo del PanelCargos con el cargo seleccionado a editar
            TxtCargoG.Text = DataListadoCargos.SelectedCells[2].Value.ToString();
            // Rellena el Text Box 'Sueldo por hora' del cargo seleccionado a editar
            TxtSueldoHoraG.Text = DataListadoCargos.SelectedCells[3].Value.ToString();
            // El botón para guardar un nuevo cargo en la BD no es visible
            BtnGuardarCargo.Visible = false;
            // El bótón para guardar cambios en la BD SÍ es visible
            BtnGuardarCambiosCargo.Visible = true;
            // El Panel para agregar un nuevo cargo será visible ...
            PanelCargos.Visible = true;
            // ... ocupará todo el espacio del panel padre ...
            PanelCargos.Dock = DockStyle.Fill;
            // ... y se mostrará en frente de todo
            PanelCargos.BringToFront();
            // El foco estará puesto en el Text Box de Cargo
            TxtCargoG.Focus();
            // Y estará el texto seleccionado para facilitar su eliminación
            TxtCargoG.SelectAll();
        }
        // Evento que se disparará cuando se dé clic a la flecha verde en el PanelCargos
        private void BtnVolverCargos_Click(object sender, EventArgs e)
        {
            // El Panel para agregar nuevos cargos ya no será visible
            PanelCargos.Visible = false;
        }
        // Evento que se disparará cuando se dé clic a la flecha verde en el PanelRegistros
        private void BtnVolverPersonal_Click(object sender, EventArgs e)
        {
            // Vuelve a mostrar el panel con el buscador y agregar nuevo personal
            panel1.Visible = true;
            // El Panel para agregar nuevo personal ya no será visible
            PanelRegistros.Visible = false;
            // El Panel del paginado será visible
            PanelPaginado.Visible = true;

        }
        // Evento que se ejecuta al dar clic en el botón de Guardar (al editar un cargo)
        private void BtnGuardarCambiosCargo_Click(object sender, EventArgs e)
        {
            // Edita la información en la BD del cargo elegido
            EditarCargos();
        }
        // Manipula la tabla Cargos de la BD, edita la información de un cargo
        private void EditarCargos()
        {
            LCargos parametros = new LCargos();
            DCargos funcion = new DCargos();
            // idCargo contiene el id del cargo que se quiere editar
            parametros.Id_cargo = idCargo;
            // Obtiene el contenido del Text Box Cargo (del panel para agregar un nuevo cargo)
            parametros.Cargo = TxtCargoG.Text;
            // Obtiene el contenido del Text Box 'Sueldo por hora' (del panel para agregar un nuevo cargo)
            parametros.SueldoPorHora = Convert.ToDouble(TxtSueldoHoraG.Text);
            // Si la manipulación (edición del cargo) de la BD ha sido correcta
            if (funcion.EditarCargo(parametros))
            {
                // Limpia el Text Box Cargo
                TxtCargo.Clear();
                // Muestra el DataGridView con todos los cargos
                BuscarCargos();
                // Ya no será visible el PanelCargos
                PanelCargos.Visible = false;
            }
        }
        // Cuando cargue el 'User Control' Personal, mostrará la tabla del Personal
        private void Personal_Load(object sender, EventArgs e)
        {
            // Reinicia variables 'desde' y 'hasta' ...
            ReiniciarPaginado();
            // ... para mostrar los 10 primeros usuarios (primer página)
            MostrarPersonal();
        }
        // Reinicia las variables desde y hasta y empieza a mostrar los primeros registros
        private void ReiniciarPaginado()
        {
            // Empezará a mostrar los primeros ...
            desde = 1;
            // ... 10 usuarios en la tabla Personal
            hasta = itemsPorPagina;
            // Almacena en 'contador' cuántos usuarios existen en la BD
            Contar();
            // Si existen más usuarios de los que queremos mostrar en una página ...
            if(contador > hasta)
            {
                // muestra los botones: siguiente
                BtnSiguiente.Visible = true;
                // Al estar mostrando los primeros 10 registros, no será visible el botón Anterior
                BtnAnterior.Visible = false;
                // Los botones de Primera ...
                BtnUltimaPagina.Visible = true;
                // ... y última página serán visibles
                BtnPrimerPagina.Visible = true;
            }
            // De lo contrario, existen menos usuarios de los que nos gustaría ver en una página (10)
            else
            {
                // Es imposible querer ver otras páginas si no existen más de 10 usuarios, así que deshabilita todos los botones del paginado.
                BtnSiguiente.Visible = false;
                BtnAnterior.Visible = false;
                BtnUltimaPagina.Visible = false;
                BtnPrimerPagina.Visible = false;
            }
            // Muestra el número de página actual de cuántas (en los labels) (Pagina n de n)
            Paginar();
        }

        // Método que muestra el panel de Usuarios y la información del usuario a editar
        private void ObtenerDatos()
        {
            //* SelectedCells OBTIENE LA COLECCIÓN DE CELDAS SELECCIONADAS POR EL USUARIO
            // Se obtiene el id del Personal de la celda seleccionada para modificarlo en la BD
            idPersonal = Convert.ToInt32(DataListadoPersonal.SelectedCells[2].Value);
            // Obtendrá el estado de un usuario para saber si ya se eliminó
            estado = DataListadoPersonal.SelectedCells[8].Value.ToString();
            // Si el usuario a editar está eliminado ...
            if (estado == "ELIMINADO")
            {
                // ... permite restaurarlo
                RestaurarPersonal();
            }
            // Si el usuario sigue ACTIVO, permite editar sus datos
            else
            {
                // Posiciona el DataLisatdoCargos si se modifica el Cargo
                LocalizarDtvCargos();
                // Obtiene los valores del personal seleccionado y rellena sus Text Box respectivos
                TxtNombre.Text = DataListadoPersonal.SelectedCells[3].Value.ToString();
                TxtIdentificacion.Text = DataListadoPersonal.SelectedCells[4].Value.ToString();
                CbxPais.Text = DataListadoPersonal.SelectedCells[10].Value.ToString();
                TxtCargo.Text = DataListadoPersonal.SelectedCells[6].Value.ToString();
                idCargo = Convert.ToInt32(DataListadoPersonal.SelectedCells[7].Value);
                TxtSueldoHora.Text = DataListadoPersonal.SelectedCells[5].Value.ToString();
                // El panel para agregar nuevos cargos no será visible
                PanelCargos.Visible = false;
                // El panel del paginado no será visible
                PanelPaginado.Visible = false;
                // El panel para agregar nuevos registros será visible (con los datos anteriores) ...
                PanelRegistros.Visible = true;
                // ... y ocupará todo el contenido del panel padre
                PanelRegistros.Dock = DockStyle.Fill;
                // El Listado de cargos no será visible a menos que se modifique
                DataListadoCargos.Visible = false;
                // El label 'Sueldo por hora' será visible
                lblSueldo.Visible = true;
                // El panel de botones Guardar (personal) serán visibles
                PanelBtnGuardarPersonal.Visible = true;
                // El botón de agregar nuevo personal no se verá
                BtnGuardarPersonal.Visible = false;
                // El botón de guardar cambios será visible
                BtnGuardarCambiosPersonal.Visible = true;
            }
        }
        // Método que pregunta ...
        private void RestaurarPersonal()
        {
            // ... si se desea restaurar a un usuario, ...
            DialogResult result = MessageBox.Show("Este Personal se Eliminó. ¿Desea volver a habilitarlo?", "Restauración de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            // en caso afirmativo (OK) ...
            if (result == DialogResult.OK)
            {
                // ... habilita de nuevo al usuario indicado
                HabilitarPersonal();
            }
        }
        // Método que accede al procedimiento de la BD que habilita a los usuarios
        private void HabilitarPersonal()
        {
            LPersonal parametros = new LPersonal();
            DAsistencia funcion = new DAsistencia();
            parametros.Id_personal = idPersonal;
            // Si la manipulación a la BD ha salido correctamente ...
            if(funcion.RestaurarPersonal(parametros))
            {
                // ... muestra a los usuarios
                MostrarPersonal();
            }
        }
        // Método que elimina un personal seleccionado de la BD
        private void EliminarPersonal()
        {
            //* SelectedCells OBTIENE LA COLECCIÓN DE CELDAS SELECCIONADAS POR EL USUARIO
            // Se obtiene el id del Personal de la celda seleccionada para Eliminar en la BD
            idPersonal = Convert.ToInt32(DataListadoPersonal.SelectedCells[2].Value);
            LPersonal parametros = new LPersonal();
            DAsistencia funcion = new DAsistencia();
            parametros.Id_personal = idPersonal;
            // Si la eliminación fue correcta ...
            if(funcion.EliminarPersonal(parametros))
            {
                // ... muestra la pantalla de personal
                MostrarPersonal();
            }
        }
        /* *
         - UN TIMER ES UN TEMPORIZADOR Y TRABAJA EN ms. POR DEFECTO ESTA DESACTIVADO, POR LO QUE SI QUEREMOS QUE FUNCIONE AL INICIAR LA APLICACIÓN, DEBEMOS CAMBIAR Enable = true.
         */
        // Cuando entremos al Timer ...
        private void Timer1_Tick(object sender, EventArgs e)
        {
            // ... queremos que diseñe de nuevo el DataReviewPersonal
            DiseñarDtvPersonal();
            ReiniciarPaginado();
            // A continuación, lo deshabilitamos, existen dos formas para hacerlo:
            // timer1.Enabled = false;
            Timer1.Stop();
        }
        // Evento que se produce cuando se da clic en el botón Guardar (al editar un personal)
        private void BtnGuardarCambiosPersonal_Click(object sender, EventArgs e)
        {
            EditarPersonal();
        }
        // Modifica la información en la BD de un Usuario
        private void EditarPersonal()
        {
            LPersonal parametros = new LPersonal();
            DAsistencia funcion = new DAsistencia();
            parametros.Id_personal = idPersonal;
            parametros.Nombres = TxtNombre.Text;
            parametros.Identificacion = TxtIdentificacion.Text;
            parametros.Pais = CbxPais.Text;
            parametros.Id_cargo = idCargo;
            parametros.SueldoPorHora = Convert.ToDouble(TxtSueldoHora.Text);
            if(funcion.EditarPersonal(parametros))
            {
                MostrarPersonal();
                PanelRegistros.Visible = false;
            }
        }

        // Evalúa qué celda se presiona en la tabla de personal
        private void DataListadoPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            /* *
             - e.ColumnIndex obtiene el índice de la columna presionada (0 es la imagen del tache; 1 del lápiz).
             - Columns (de un DataGridView) es una propiedad que obtiene una colección que contiene todas las columnas del DataGridView
             */
            // Si se hace clic en una columna del listado de Cargos y coincide con el índice de la columna llamada Eliminar (imagen del tache) ...
            if (e.ColumnIndex == DataListadoPersonal.Columns["Eliminar"].Index)
            {
                //* SelectedCells OBTIENE LA COLECCIÓN DE CELDAS SELECCIONADAS POR EL USUARIO
                // Se obtiene el id del Personal de la celda seleccionada para modificarlo en la BD
                idPersonal = Convert.ToInt32(DataListadoPersonal.SelectedCells[2].Value);
                // Obtendrá el estado de un usuario para saber si ya se eliminó
                estado = DataListadoPersonal.SelectedCells[8].Value.ToString();
                // Si el usuario a editar está eliminado ...
                if (estado == "ELIMINADO")
                {
                    // ... permite restaurarlo
                    RestaurarPersonal();
                }
                // El usuario a eliminar aún sigue ACTIVO
                else
                {
                    /* *
                     - DialogResult (enum). Especifica identificadores para indicar el valor de retorno de un cuadro de diálogo. Campos: Abort(3), Cancel(2), Ignore(5), No(7), None(0), OK(1), Retry(4), Yes(6)
                     - MessageBox (class). Muestra una ventana de mensaje (caja de diálogo), que muestra un mensaje al usuario.
                     - Show (método sobrecargado). Despliega una caja de mensaje con un texto especificado, un título, un botón (botones) y un ícono.
                     - MessageBoxButton (enum). Especifica constantes definidas con botones que se mostrarán en un MessageBox: AbortRetryIgnore(2), OK(0), OKCancel(1), RetryCancel(5), YesNo(4), YesNoCancel(3)
                     - MessageBoxIcon (enum). Especifica constantes que definen qué información mostrar.
                    */
                    // result contendrá si el usuario presionó el botón OK o Cancel
                    DialogResult result = MessageBox.Show("Solo se cambiará el Estado para que no pueda acceder, ¿Desea continuar?", "Eliminando registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    // Si el usuario presionó el botón OK ...
                    if (result == DialogResult.OK)
                    {
                        // ... "elimina" (cambia su estado a ELIMINADO) el usuario de la BD
                        EliminarPersonal();
                    }
                }
            }
            // ColumnIndex retorna la columna donde está la celda presionada y la compara con el índice de la columna llamada 'Editar', si son iguales ...
            if (e.ColumnIndex == DataListadoPersonal.Columns["Editar"].Index)
            {
                // ... despliega el panel para agregar Personal con los datos del usuario a editar
                ObtenerDatos();
            }
        }
        // Evento que se desencadena cuando se da clic en el botón de página siguiente
        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            // Incrementamos en 10 'desde' y 'hasta' que se utilizan en el ...
            desde += itemsPorPagina;
            hasta += itemsPorPagina;
            // ... procedimiento de la BD para mostrar el personal según el 'Numero_de_fila'
            MostrarPersonal();
            // Almacena en 'contador' cuántos usuarios en la tabla Personal existen
            Contar();
            // Si existen más usuarios (contador) que de los que se están mostrando (hasta)
            if( contador > hasta)
            {
                // Los botones siguiente y ...
                BtnSiguiente.Visible = true;
                // ... anterior, serán visibles
                BtnAnterior.Visible = true;
            }
            // Si no (ya no existen más usuarios qué mostrar)
            else
            {
                // No tiene sentido mostrar el botón siguiente ya que no hay más usuarios
                BtnSiguiente.Visible = false;
                // Es posible retroceder en las páginas
                BtnAnterior.Visible = true;
            }
            // Muestra el número de páginas que hay (Página n de n) en los labels
            Paginar();
        }

        private void Paginar()
        {
            try
            {
                // Calcula en qué página esta actualmente (recordar que 'hasta' incrementa de 10 en 10)
                LblPagina.Text = (hasta / itemsPorPagina).ToString();
                //* EL MÉTODO Ceiling RETORNA EL ENTERO MAYOR MÁS PRÓXIMO (si su argumento es 0.3, retorna 1)
                // Una división entre enteros trunca el decimal, por eso debemos convertir uno de ellos a decimal. Si existen 23 usuarios y se divide entre 10, el resultado es 2.3, por lo que se necesitan 3 páginas para mostrar a todos los usuarios, Ceiling incrementa a 3 el resultado y así se calcula el total de páginas
                LblTotalPaginas.Text = Math.Ceiling(Convert.ToSingle(contador) / itemsPorPagina).ToString();
                totalPaginas = Convert.ToInt32(LblTotalPaginas.Text);
            }
            catch (Exception)
            {

            }
        }
        // Obtiene cuántos usuarios existen en la BD ...
        private void Contar()
        {
            DAsistencia funcion = new DAsistencia();
            // ... y los almacena en 'contador'
            funcion.ContarPersonal(ref contador);
        }
        // Evento que se desencadena cuando se presiona el botón 'Página anterior'
        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            // 'desde' y 'hasta' decrementan en 10 para usarlos como parámetros y ...
            desde -= itemsPorPagina;
            // ... mostar los usuarios según el procedimiento ...
            hasta -= itemsPorPagina;
            // ... MostrarPersonal almacenado en la BD
            MostrarPersonal();
            // 'contador' contiene el número de usuarios en la tabla Personal
            Contar();
            // Si hay más usuarios (contador) que los mostrados actualmente (hasta) ...
            if(contador > hasta)
            {
                // ... tiene sentido mostrar los botones siguientes y anterior
                BtnSiguiente.Visible = true;
                BtnAnterior.Visible = true;
            }
            // Si no, significa que hay menos usuarios (contador) que los mostrados actualmente (hasta) ...
            else
            {
                // ... no tiene sentido mostrar el botón siguiente (porque no hay más usuarios qué mostar)
                BtnSiguiente.Visible = false;
                // El botón 'Página Anterior' será visible
                BtnAnterior.Visible = true;
            }
            // Si desde vale 1 ...
            if( desde == 1)
            {
                // ... muestra los usuarios desde el principio
                ReiniciarPaginado();
            }
            // Muestra en qué página estamos de cuántas (Página n de n)
            Paginar();
        }
        // Evento que sucede al presionar el botón (Última página)
        private void BtnUltimaPagina_Click(object sender, EventArgs e)
        {
            // "Existen" 13 usuarios. El totalPaginas = 2, itemsPorPagina = 10 => hasta = 2*10 = 20
            hasta = totalPaginas * itemsPorPagina;
            // desde = 20 - 9 = 11
            desde = hasta - 9;
            // Muestra usuarios según 'Numero_de_fila' del procedimiento MostrarPersonal de la BD, desde el 11 hasta el 20
            MostrarPersonal();
            // 'contador' almacena el total de usuarios en la tabla Personal
            Contar();
            // Si el número de usuarios (contador) es mayor que los usuarios mostrados (hasta) ...
            if (contador > hasta)
            {
                // ... aún es posible mostrar más páginas con usuarios, así que los botones Siguiente ...
                BtnSiguiente.Visible = true;
                // ... y Anterior serán visibles
                BtnAnterior.Visible = true;
            }
            // Si no, entonces ya se están mostrando TODOS los usuarios ...
            else
            {
                // ... ya no tiene sentido navegar a una página siguiente, por tanto, el botón Siguiente no se ve
                BtnSiguiente.Visible = false;
                // Sí es posible regresar a una página anterior.
                BtnAnterior.Visible = true;
            }
            // Muestra en qué página estamos de cúántas (Página n de n)
            Paginar();
        }
        // Evento que se ejecuta al presionar el botón 'Primer página'
        private void BtnPrimerPagina_Click(object sender, EventArgs e)
        {
            // Reinicia las variables desde y hasta y empieza a mostrar los primeros registros
            ReiniciarPaginado();
            // Muestra el Personal según las variables desde y hasta
            MostrarPersonal();
        }
        // Evento que se dispara cuando se cambia el texto en el Text Box Buscador (de Personal)
        private void TxtBuscador_TextChanged(object sender, EventArgs e)
        {
            // Busca un usuario por su nombre, identificación, sueldo, cargo, estado o país
            BuscarPersonal();
        }
        // Implementa el procedimiento BuscarPersonal de la BD
        private void BuscarPersonal()
        {
            // Almacenará los resultados obtenidos por la BD
            DataTable dt = new DataTable();
            // Se usará el método que implementa el procedimiento BuscarPersonal de la BD
            DAsistencia funcion = new DAsistencia();
            // dt contendrá la tabla con los resultados obtenidos según el texto en el buscador, de la BD 
            funcion.BuscarPersonal(ref dt, TxtBuscador.Text);
            // dt se mostrará en un DataGridView, en este caso, será en el de Personal
            DataListadoPersonal.DataSource = dt;
            // Cambiamos el diseño de DataGridView del Personal
            DiseñarDtvPersonal();
        }
        // Evento que mostrará de nuevo todos los usuarios cuando se presiona el botón MostrarTodo
        private void BtnMostrarTodo_Click(object sender, EventArgs e)
        {
            // El paginado comienza en la Página 1 de n (reinicia variables 'desde' y 'hasta')
            ReiniciarPaginado();
            // Muestra los primeros 10 usuarios en la BD
            MostrarPersonal();
            // Limpia el texto en el Text Box del buscador.
            TxtBuscador.Clear();
        }
    }
}
