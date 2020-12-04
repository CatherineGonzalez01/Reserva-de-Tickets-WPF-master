using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizzaria1
{
    /// <summary>
    /// Interação lógica para UserControlInicio.xam
    /// </summary>
    public partial class UserControlInicio : UserControl
    {
        public UserControlInicio()
        {
            InitializeComponent();
        }

        private async void ObtenerDatos()
        {
            List<Animador> lista = await Animador.ObtenerTodos();
            dtgAnimador.ItemsSource = lista;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ObtenerDatos();
        }

        private void LimpiarCampos()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtTelefono.Text = "";
        }

        private async void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Animador a = new Animador();
            a.animadorID = int.Parse(txtID.Text);
            a.Nombre = txtNombre.Text;
            a.Apellido = txtApellido.Text;
            a.Telefono = txtTelefono.Text;

            await Animador.AgregarAnimador(a);
            LimpiarCampos();
            ObtenerDatos();
           
        }

        private async void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAnimador.SelectedItem != null)
            {
                Animador animadorSeleccionado = (Animador)dtgAnimador.SelectedItem;
                txtID.IsEnabled = false;
                animadorSeleccionado.animadorID = int.Parse(txtID.Text);
                animadorSeleccionado.Nombre = txtNombre.Text;
                animadorSeleccionado.Apellido = txtApellido.Text;
                animadorSeleccionado.Telefono = txtTelefono.Text;

                await Animador.ModificarAnimador(animadorSeleccionado); 
                ObtenerDatos();
            }
            else
                MessageBox.Show("Debe seleccionar primeramente la persona a modificar ");
        }

        private void dtgAnimador_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgAnimador.SelectedItem != null)
            {
                txtID.IsEnabled = false;
                Animador animadorSeleccionado = (Animador)dtgAnimador.SelectedItem;
                txtID.Text = animadorSeleccionado.animadorID.ToString();
                txtNombre.Text = animadorSeleccionado.Nombre;
                txtApellido.Text = animadorSeleccionado.Apellido;
                txtTelefono.Text = animadorSeleccionado.Telefono;
            }
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAnimador.SelectedItem != null)
            {
                Animador animadorSeleccionado = (Animador)dtgAnimador.SelectedItem;
                await Animador.EliminarAnimador(animadorSeleccionado); 
                ObtenerDatos();
                LimpiarCampos();
            }
            else
                MessageBox.Show("Debe seleccionar primeramente la persona a eliminar ");
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            txtID.IsEnabled = true;
        }
    }
}
