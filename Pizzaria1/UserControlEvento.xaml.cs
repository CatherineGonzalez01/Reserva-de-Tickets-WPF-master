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
    public partial class UserControlEvento : UserControl
    {
        ReservaTicketEntities1 db = new ReservaTicketEntities1();
        public UserControlEvento()
        {
            InitializeComponent();
        }

        private void ObtenerDatos()
        {
            try
            {
                dtgEventos.ItemsSource = db.Evento.ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ObtenerDatos();
        }

        private void LimpiarCampos()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            txtFecha.Text = "";
            txtLugar.Text = "";
            txtCosto.Text = "";
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Evento evento = new Evento();
                evento.Nombre = txtNombre.Text;
                evento.FechaEvento = DateTime.Parse(txtFecha.SelectedDate.ToString());
                evento.Lugar = txtLugar.Text;
                evento.CostoEntrada = int.Parse(txtCosto.Text);
                db.Evento.Add(evento);
                db.SaveChanges();

                dtgEventos.Items.Refresh();
                ObtenerDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgEventos.SelectedItem != null)
            {
                Evento evento = (Evento)dtgEventos.SelectedItem;
                evento.EventoID = int.Parse(txtID.Text);
                evento.Nombre = txtNombre.Text;
                evento.FechaEvento = DateTime.Parse(txtFecha.SelectedDate.ToString()) ;
                evento.CostoEntrada = int.Parse(txtCosto.Text);

                db.Entry(evento).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Se ha modificado el registro");
                dtgEventos.Items.Refresh();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro para poder modificarlo");
            }
        }

        private void dtgAnimador_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
            if (dtgEventos.SelectedItem != null)
            {
                Evento evento = (Evento)dtgEventos.SelectedItem;
                txtID.Text = evento.EventoID.ToString();
                txtNombre.Text = evento.Nombre;
                txtFecha.SelectedDate = evento.FechaEvento;
                txtLugar.Text = evento.Lugar;
                txtCosto.Text = evento.CostoEntrada.ToString();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgEventos.SelectedItem != null)
            {
                Evento evento = (Evento)dtgEventos.SelectedItem;
               db.Evento.Remove(evento);
                db.SaveChanges();
                MessageBox.Show("El registro se ha eliminado correctamente");
                dtgEventos.Items.Refresh();
                ObtenerDatos();
                LimpiarCampos();

            }
            else
                MessageBox.Show("Debe seleccionar un registro para eliminarlo");
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
              
            {
                 LimpiarCampos();
                
             }
        }
    }
