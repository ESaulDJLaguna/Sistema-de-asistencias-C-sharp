using Sistema_de_asistencias.Datos;
using Sistema_de_asistencias.Logica;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Sistema_de_asistencias.Presentacion
{
    public partial class ControlUsuarios : UserControl
    {
        public ControlUsuarios()
        {
            InitializeComponent();
        }

        int idUsuario;
        string login;
        string estado;

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Limpiar();
            HabilitarPaneles();
            MostrarModulos();
        }

        private void Limpiar()
        {
            TxtNombre.Clear();
            TxtUsuario.Clear();
            TxtContraseña.Clear();
            TxtUsuario.Enabled = true;
            DataListadoModulos.Enabled = true;
        }

        private void HabilitarPaneles()
        {
            PanelRegistros.Visible = true;
            LblAnuncioIcono.Visible = true;
            PanelIconos.Visible = false;
            PanelRegistros.Dock = DockStyle.Fill;
            PanelRegistros.BringToFront();
            BtnGuardar.Visible = true;
            BtnActualizar.Visible = false;
            BtnVolverRegistros.Visible = true;
        }

        // Muestra los registros de la tabla Modulos en un DataGridView
        private void MostrarModulos()
        {
            DModulos funcion = new DModulos();
            DataTable dt = new DataTable();
            // dt contiene todos los registros (modulos) de la BD
            funcion.MostrarModulos(ref dt);
            // El DataGridView tendrá mostrará todos los registros de la tabla Modulos con todos sus campos (idModulos, Modulo)
            DataListadoModulos.DataSource = dt;
            // La primer columna es el checkbox, la columna 1 es el idModulos: no la hagas visible.
            DataListadoModulos.Columns[1].Visible = false;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(TxtNombre.Text))
            {
                if(!string.IsNullOrEmpty(TxtUsuario.Text))
                {
                    if(!string.IsNullOrEmpty(TxtContraseña.Text))
                    {
                        if(LblAnuncioIcono.Visible == false)
                        {
                            InsertarUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("Seleccione un Ícono");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese la Contraseña");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el Usuario");
                }
            }
            else
            {
                MessageBox.Show("Ingrese el Nombre");
            }
        }

        private void InsertarUsuarios()
        {
            LUsuarios parametros = new LUsuarios();
            DUsuarios funcion = new DUsuarios();
            parametros.Nombre = TxtNombre.Text;
            parametros.Login = TxtUsuario.Text;
            parametros.Password = TxtContraseña.Text;
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            if(funcion.InsertarUsuario(parametros))
            {
                ObtenerIdUsuario();
                InsertarPermisos();
            }
        }

        private void InsertarPermisos()
        {
            foreach(DataGridViewRow row in DataListadoModulos.Rows)
            {
                int idModulo = Convert.ToInt32(row.Cells["IdModulo"].Value);
                bool marcado = Convert.ToBoolean(row.Cells["Marcar"].Value);
                if(marcado)
                {
                    LPermisos parametros = new LPermisos();
                    DPermisos funcion = new DPermisos();
                    parametros.IdModulo = idModulo;
                    parametros.IdUsuario = idUsuario;
                    funcion.InsertarPermisos(parametros);
                }
            }
            MostrarUsuarios();
            PanelRegistros.Visible = false;
        }

        private void MostrarUsuarios()
        {
            DataTable dt = new DataTable();
            DUsuarios funcion = new DUsuarios();
            funcion.MostrarUsuarios(ref dt);
            DataListadoPersonal.DataSource = dt;
            DiseñarDtvUsuarios();
        }

        private void DiseñarDtvUsuarios()
        {
            Bases.DiseñoDtv(ref DataListadoPersonal);
            Bases.DiseñoDtvEliminar(ref DataListadoPersonal);
            DataListadoPersonal.Columns[2].Visible = false;
            DataListadoPersonal.Columns[5].Visible = false;
            DataListadoPersonal.Columns[6].Visible = false;

        }

        private void ObtenerIdUsuario()
        {
            DUsuarios funcion = new DUsuarios();
            funcion.ObtenerIdUsuario(ref idUsuario, TxtUsuario.Text);
        }

        private void LblAnuncioIcono_Click(object sender, EventArgs e)
        {
            MostrarPanelIcono();
        }

        private void MostrarPanelIcono()
        {
            PanelIconos.Visible = true;
            PanelIconos.Dock = DockStyle.Fill;
            PanelIconos.BringToFront();
        }

        private void OcultarPanelIconos()
        {
            PanelIconos.Visible = false;
            LblAnuncioIcono.Visible = false;
            Icono.Visible = true;
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Icono.Image = PictureBox4.Image;
            OcultarPanelIconos();
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            Icono.Image = PictureBox5.Image;
            OcultarPanelIconos();
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            Icono.Image = PictureBox6.Image;
            OcultarPanelIconos();
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            Icono.Image = PictureBox7.Image;
            OcultarPanelIconos();
        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {
            Icono.Image = PictureBox8.Image;
            OcultarPanelIconos();
        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {
            Icono.Image = PictureBox9.Image;
            OcultarPanelIconos();
        }

        private void PictureBox10_Click(object sender, EventArgs e)
        {
            Icono.Image = PictureBox10.Image;
            OcultarPanelIconos();
        }

        private void PictureBox11_Click(object sender, EventArgs e)
        {
            Icono.Image = PictureBox11.Image;
            OcultarPanelIconos();
        }

        private void AgregarIconoPC_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imágenes|*.jpg; *.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de imágenes";
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                Icono.BackgroundImage = null;
                Icono.Image = new Bitmap(dlg.FileName);
                OcultarPanelIconos();
            }
        }

        private void Icono_Click(object sender, EventArgs e)
        {
            MostrarPanelIcono();
        }

        private void ControlUsuarios_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }

        private void TxtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if(char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void BtnVolverRegistros_Click(object sender, EventArgs e)
        {
            PanelRegistros.Visible = false;
        }

        private void BtnVolverdePanelIconos_Click(object sender, EventArgs e)
        {
            OcultarPanelIconos();
            if (Icono.Image == null)
                LblAnuncioIcono.Visible = true;
            else
                Icono.Visible = true;
        }

        private void DataListadoPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DataListadoPersonal.Columns["Eliminar"].Index)
            {
                ObtenerEstado();
                if(estado == "ELIMINADO")
                {
                    DialogResult resultado = MessageBox.Show("Este Usuario se Eliminó. ¿Desea Volver a Habilitarlo?", "Restauración de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if(resultado == DialogResult.OK)
                    {
                        RestaurarUsuario();
                    }
                }
                else
                {
                    ObtenerDatos();
                }
            }
            if (e.ColumnIndex == DataListadoPersonal.Columns["Editar"].Index)
            {
                DialogResult resultado = MessageBox.Show("¿Realmente desea eliminar este Registro?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(resultado == DialogResult.OK)
                {
                    CapturarIdUsuario();
                    EliminarUsuarios();
                }
            }

        }

        private void RestaurarUsuario()
        {
            CapturarIdUsuario();
            LUsuarios parametros = new LUsuarios();
            DUsuarios funcion = new DUsuarios();
            parametros.IdUsuario = idUsuario;
            if(funcion.RestaurarUsuario(parametros))
            {
                MostrarModulos();
            }
        }

        private void EliminarUsuarios()
        {
            LUsuarios parametros = new LUsuarios();
            DUsuarios funcion = new DUsuarios();
            parametros.IdUsuario = idUsuario;
            parametros.Login = login;
            if(funcion.EliminarUsuarios(parametros))
            {
                MostrarUsuarios();
            }
        }

        private void ObtenerEstado()
        {
            estado = DataListadoPersonal.SelectedCells[7].Value.ToString();
        }

        private void ObtenerDatos()
        {
            // Se obtiene el id del cargo de la celda seleccionada para modificarlo en la BD
            CapturarIdUsuario();
            // SelectedCells OBTIENE LA COLECCIÓN DE CELDAS SELECCIONADAS POR EL USUARIO
            TxtNombre.Text = DataListadoPersonal.SelectedCells[3].Value.ToString();
            TxtUsuario.Text = DataListadoPersonal.SelectedCells[4].Value.ToString();
            // Si el usuario es el administrador ...
            if(TxtUsuario.Text == "admin")
            {
                // ... no permitiremos la modificación de los permisos ni del nombre de usuario
                TxtUsuario.Enabled = false;
                DataListadoModulos.Enabled = false;
            }
            else
            {
                TxtUsuario.Enabled = true;
                DataListadoModulos.Enabled = true;
            }
            TxtContraseña.Text = DataListadoPersonal.SelectedCells[5].Value.ToString();
            TxtContraseña.UseSystemPasswordChar = false;
            // Obtenemos la imagen
            Icono.BackgroundImage = null;
            byte[] b = (byte[])(DataListadoPersonal.SelectedCells[6].Value);
            MemoryStream ms = new MemoryStream(b);
            Icono.Image = Image.FromStream(ms);
            LblAnuncioIcono.Visible = false;
            // El Panel para agregar un nuevo cargo será visible ...
            PanelRegistros.Visible = true;
            // ... ocupará todo el espacio del panel padre ...
            PanelRegistros.Dock = DockStyle.Fill;
            // ... y se mostrará en frente de todo
            PanelRegistros.BringToFront();
            // El foco estará puesto en el Text Box de Cargo
            TxtNombre.Focus();
            // Y estará el texto seleccionado para facilitar su eliminación
            TxtNombre.SelectAll();
            BtnGuardar.Visible = false;
            BtnActualizar.Visible = true;
            MostrarModulos();
            MostrarPermisos();
        }

        private void MostrarPermisos()
        {
            DataTable dt = new DataTable();
            DPermisos funcion = new DPermisos();
            LPermisos parametros = new LPermisos();
            parametros.IdUsuario = idUsuario;
            funcion.MostrarPermisos(ref dt, parametros);
            foreach( DataRow rowPermisos in dt.Rows)
            {
                int idModuloPermisos = Convert.ToInt32(rowPermisos["idModulo"]);
                foreach(DataGridViewRow rowModulos in DataListadoModulos.Rows)
                {
                    int idModulo = Convert.ToInt32(rowModulos.Cells["idModulo"].Value);
                    if(idModuloPermisos == idModulo)
                    {
                        rowModulos.Cells[0].Value = true;
                    }
                }
            }
        }

        private void CapturarIdUsuario()
        {
            idUsuario = Convert.ToInt32(DataListadoPersonal.SelectedCells[2].Value);
            login = DataListadoPersonal.SelectedCells[4].Value.ToString();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(TxtNombre.Text))
            {
                if(!string.IsNullOrEmpty(TxtUsuario.Text))
                {
                    if(!string.IsNullOrEmpty(TxtContraseña.Text))
                    {
                        if(LblAnuncioIcono.Visible == false)
                        {
                            EditarUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("Seleccione un Ícono");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese la Contraseña");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el Usuario");
                }
            }
            else
            {
                MessageBox.Show("Ingrese el Nombre");
            }
        }
        private void EditarUsuarios()
        {
            LUsuarios parametros = new LUsuarios();
            DUsuarios funcion = new DUsuarios();
            parametros.IdUsuario = idUsuario;
            parametros.Nombre = TxtNombre.Text;
            parametros.Login = TxtUsuario.Text;
            parametros.Password = TxtContraseña.Text;
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            if (funcion.EditarUsuarios(parametros))
            {
                EliminarPermisos();
                InsertarPermisos();
            }
        }
        private void EliminarPermisos()
        {
            LPermisos parametros = new LPermisos();
            DPermisos funcion = new DPermisos();
            parametros.IdUsuario = idUsuario;
            funcion.EliminarPermisos(parametros);
        }

        private void TxtBuscador_TextChanged(object sender, EventArgs e)
        {
            BuscarUsuarios();
        }

        private void BuscarUsuarios()
        {
            DataTable dt = new DataTable();
            DUsuarios funcion = new DUsuarios();
            funcion.BuscarUsuarios(ref dt, TxtBuscador.Text);
            DataListadoPersonal.DataSource = dt;
            DiseñarDtvUsuarios();
        }
    }
}
