using PJAgenda.Modelos;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
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
using System.Windows.Shapes;

namespace PJAgenda
{
    /// <summary>
    /// Lógica de interacción para EditarAudiencia.xaml
    /// </summary>
    public partial class EditarAudiencia : Window
    {
        ModeloPersonas ModeloP = new ModeloPersonas();
        string url = "Por Capturar";
        List<AsuntosJudiciales> _list = new List<AsuntosJudiciales>();
        List<ModeloAudiencias> _listMA = new List<ModeloAudiencias>();
        ModeloConsultaAudiencia objetoPrincipal = new ModeloConsultaAudiencia();
      
        List<Distritos> ListaDistritos = new List<Distritos>();
        List<Juzgados> ListaJuzgados = new List<Juzgados>();

        public EditarAudiencia(ModeloConsultaAudiencia selectedObject, string conFecha)
        {
            try
            {
                InitializeComponent();
                inicializar();
                objetoPrincipal = selectedObject;

                lv_audiencia.ItemsSource = _listMA = CRUD.ObtenerAudiecia(selectedObject.IdAudiencia, conFecha);
                lv_personas.ItemsSource = CRUD.ObtenerPersonas(selectedObject.IdAudiencia);
                var a = CRUD.ObtenerIdAsunto(selectedObject.IdAudiencia);
                llenarAsuntos(a);


            }
            catch (Exception ex)
            {
                mensaje($"Error al iniciar. Cierre la pantalla y vuelva a intentar.{ex.Message}");
            }
        }

        void inicializar() {
            gb_actualizar_audiencia.Visibility = Visibility.Collapsed;
            try
            {
                // mostrarPopupDistrito();
                //ListaDistritos = Distritos.ObtenerDistritos();
                //cb_distritos.ItemsSource = ListaDistritos;
                //cb_distritos.DisplayMemberPath = "nombre";
                //cb_distritos.SelectedValuePath = "id_distrito";
                //cb_distritos.SelectedIndex = 0;

                txt_nomPart.Visibility = Visibility.Collapsed;
                txt_apPart.Visibility = Visibility.Collapsed;
                txt_amPart.Visibility = Visibility.Collapsed;
                lblnombre.Visibility = Visibility.Collapsed;
                lblap.Visibility = Visibility.Collapsed;
                lblam.Visibility = Visibility.Collapsed;
                btn_participante.Visibility = Visibility.Collapsed;
                btn_partcancel.Visibility = Visibility.Collapsed;

                cb_tipoAsunto.ItemsSource = ListaGenerica.ObtenerAsuntos();
                cb_tipoAsunto.DisplayMemberPath = "Nombre";
                cb_tipoAsunto.SelectedValuePath = "valor";
                cb_tipoAsunto.SelectedIndex = 0;

                cb_tipovisita.ItemsSource = ListaGenerica.ObtenerVisitas();
                cb_tipovisita.DisplayMemberPath = "Nombre";
                cb_tipovisita.SelectedValuePath = "valor";
                cb_tipovisita.SelectedIndex = 0;

                dp_fecha.DefaultValue = DateTime.Today;
                dp_fecha.CalendarWidth = 300;
                dp_fecha.Value = DateTime.Now;

                dp_fecha.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                mensaje("Error de conexión al iniciar");
                this.Close();

            }

        }

        void mensaje(string msj)
        {

            var alert = new SweetAlert();
            alert.Caption = "Aviso";
            alert.Message = msj;
            alert.MsgButton = SweetAlertButton.OK;
            alert.OkText = "Aceptar";
            alert.Show();
        }
        void llenarAsuntos(List<ModeloAsunto> a) {
            _list.Clear();
            if (a.Count > 0)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    switch (a.ElementAt(i).Tipo)
                    {
                        case "1CFM":

                            var l = AsuntosJudiciales.ObtenerAsuntosCYF1ra(a.ElementAt(i).IdExpediente);
                            for (int c = 0; c < l.Count; c++)
                            {
                                _list.Add(l.ElementAt(c));
                            }

                            break;
                        case "2CFM":
                            var d = AsuntosJudiciales.ObtenerAsuntosCYF2da(a.ElementAt(i).IdExpediente);
                            for (int c = 0; c < d.Count; c++)
                            {
                                _list.Add(d.ElementAt(c));
                            }
                            break;
                        case "1P":
                            var o = AsuntosJudiciales.ObtenerAsuntosPenal1ra(a.ElementAt(i).IdExpediente);
                            for (int c = 0; c < o.Count; c++)
                            {
                                _list.Add(o.ElementAt(c));
                            }
                            break;
                        case "2P":
                            var p = AsuntosJudiciales.ObtenerAsuntosPenal2da(a.ElementAt(i).IdExpediente);
                            for (int c = 0; c < p.Count; c++)
                            {
                                _list.Add(p.ElementAt(c));
                            }
                            break;
                    }
                    lv_asuntoJudicial.ItemsSource = null;
                    lv_asuntoJudicial.ItemsSource = _list;
                }
            }
            else {

                lv_asuntoJudicial.ItemsSource = null;
            }

        }

        private void lv_audiencia_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gb_actualizar_audiencia.Visibility = Visibility.Visible;
          //  gb_ingresarasunto.Visibility = Visibility.Collapsed;
            gb_personas.Visibility = Visibility.Collapsed;
            gb_listasuntos.Visibility = Visibility.Collapsed;

            switch (_listMA.FirstOrDefault().Tipo_Audiencia)
            {
                case "Judicial":
                    cb_tipoAsunto.SelectedIndex = 1;
                    break;
                case "Personal":
                    cb_tipoAsunto.SelectedIndex = 2;
                    break;
                case "Laboral":
                    cb_tipoAsunto.SelectedIndex = 3;
                    break;
            }

            switch (_listMA.FirstOrDefault().Tipo_Visita)
            {
                case "Interna":
                    cb_tipovisita.SelectedIndex = 1;
                    break;
                case "Externa":
                    cb_tipovisita.SelectedIndex = 2;
                    break;

            }

            txt_referencia.Text = _listMA.FirstOrDefault().Referencia;
            if (_listMA.FirstOrDefault().Fecha == DateTime.MinValue)
            {
                dp_fecha.Value = DateTime.Now;

            }
            else
            {
                dp_fecha.Value = _listMA.FirstOrDefault().Fecha;
            }

        }

        private void lv_asuntoJudicial_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListView list = (System.Windows.Controls.ListView)sender;
            AsuntosJudiciales selectedObject = (AsuntosJudiciales)list.SelectedItem;

            var alert = new SweetAlert();
            alert.Caption = "Aviso";
            alert.Message = "¿Esta seguro de eliminar el asunto?";

            StackPanel vPanel = new StackPanel
            {
                Margin = new Thickness(10),
                MinWidth = 150,
                MinHeight = 70,
                Orientation = Orientation.Horizontal
            };

            Button cancel = new Button
            {
                Content = "Cancelar",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button delete = new Button
            {
                Content = "Eliminar Asunto",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            delete.Click += delegate {
                try
                {
                    CRUD.eliminarAsunto(selectedObject.id_asunto);
                    mensaje("Eliminado Satisfactoriamente");
                    var a = CRUD.ObtenerIdAsunto(_listMA.FirstOrDefault().IdAudiencia);
                    llenarAsuntos(a);
                    alert.Close();

                }
                catch (Exception)
                {
                    mensaje("Error al intentar consultar");

                }
            };

         

            cancel.Click += delegate {
                //Generar Reporte
                alert.Close();
            };


            vPanel.Children.Add(delete);
            vPanel.Children.Add(cancel);
            alert.ButtonContent = vPanel;
            alert.ShowDialog();

        }

        private void lv_personas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListView list = (System.Windows.Controls.ListView)sender;
            ModeloPersonas selectedObject = (ModeloPersonas)list.SelectedItem;
            ModeloP = selectedObject;

            var alert = new SweetAlert();
            alert.Caption = "Aviso";
            alert.Message = "Seleccione la accion a realizar";

            StackPanel vPanel = new StackPanel
            {
                Margin = new Thickness(10),
                MinWidth = 150,
                MinHeight = 70,
                Orientation = Orientation.Horizontal
            };

            Button edit = new Button
            {
                Content = "Editar",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };
            Button cancel = new Button
            {
                Content = "Cancelar",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button delete = new Button
            {
                Content = "Eliminar",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            delete.Click += delegate {
                try
                {
                    CRUD.eliminarParticipante(selectedObject.IdPersona);
                    mensaje("Eliminado Satisfactoriamente");
                    lv_personas.ItemsSource = null;
                    lv_personas.ItemsSource = CRUD.ObtenerPersonas(selectedObject.IdAudiencia);
                    alert.Close();

                }
                catch (Exception)
                {
                    mensaje("Error al intentar consultar");

                }
            };

            edit.Click += delegate {
                txt_nomPart.Visibility = Visibility.Visible;
                txt_apPart.Visibility = Visibility.Visible;
                txt_amPart.Visibility = Visibility.Visible;

                lblnombre.Visibility = Visibility.Visible;
                lblap.Visibility = Visibility.Visible;
                lblam.Visibility = Visibility.Visible;
                btn_participante.Visibility = Visibility.Visible;
                btn_partcancel.Visibility = Visibility.Visible;
                txt_nomPart.Text = selectedObject.Nombre;
                txt_apPart.Text = selectedObject.APaterno;
                txt_amPart.Text = selectedObject.AMaterno;
                btn_participante.Content = "Actualizar";
                alert.Close();

            };


            cancel.Click += delegate {
                //Generar Reporte
                alert.Close();
            };


            vPanel.Children.Add(edit);
            vPanel.Children.Add(delete);
            vPanel.Children.Add(cancel);
            alert.ButtonContent = vPanel;
            alert.ShowDialog();

        }

        private void cb_tipoAsunto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (_listMA.FirstOrDefault().Tipo_Audiencia!= "Judicial")
            //{
            //    if (cb_tipoAsunto.SelectedValue.ToString() == "J")
            //    {
            //        gb_ingresarasunto.Visibility = Visibility.Visible;

            //    }
            //    else
            //        gb_ingresarasunto.Visibility = Visibility.Collapsed;
            //}
        }

        private void cb_tipovisita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void check_confirmar_Checked(object sender, RoutedEventArgs e)
        {
            dp_fecha.Visibility = Visibility.Collapsed;
            check_asignarhora.IsEnabled = false;
           // _listMA.FirstOrDefault().Fecha = DateTime.MinValue;
        }

        private void check_confirmar_Unchecked(object sender, RoutedEventArgs e)
        {
            dp_fecha.Visibility = Visibility.Collapsed;
            check_asignarhora.IsEnabled = true;
          //  _listMA.FirstOrDefault().Fecha = DateTime.MinValue;
        }

        private void check_asignarhora_Checked(object sender, RoutedEventArgs e)
        {
            dp_fecha.Visibility = Visibility.Visible;
            check_confirmar.IsEnabled = false;
        }

        private void check_asignarhora_Unchecked(object sender, RoutedEventArgs e)
        {
            dp_fecha.Visibility = Visibility.Collapsed;
            check_confirmar.IsEnabled = true;
          //  _listMA.FirstOrDefault().Fecha = DateTime.MinValue;

        }

        private void guardar_ACAudiencia_Click(object sender, RoutedEventArgs e)
        {
            var conFecha = "No";
            var msj = validarActualizarAudiencia();
            if ( msj != "")
            {
                mensaje("Verifique los siguientes datos:"+msj);
            }
            else {
                ModeloAudiencias ma = new ModeloAudiencias();
               
               switch (cb_tipoAsunto.SelectedValue.ToString())
                {
                    case "J":
                        if (_listMA.FirstOrDefault().Tipo_Audiencia == "Judicial")
                        {
                            ma.Tipo_Audiencia = "Judicial";
                        }
                        else {

                            AgregarAsunto ventana = new AgregarAsunto(_listMA.FirstOrDefault().IdAudiencia);
                            ventana.ShowDialog();
                            ma.Tipo_Audiencia = "Judicial";

                        }
                        break;
                    case "Personal":
                        if (_listMA.FirstOrDefault().Tipo_Audiencia == "Judicial")
                        {//Mensaje confirmar y eliminar
                            ma.Tipo_Audiencia = cb_tipoAsunto.SelectedValue.ToString();

                        }
                        else
                        {

                            ma.Tipo_Audiencia = cb_tipoAsunto.SelectedValue.ToString();
                        }
                        break;
                    case "Laboral":
                        if (_listMA.FirstOrDefault().Tipo_Audiencia == "Judicial")
                        {//Mensaje confirmar y eliminar
                            ma.Tipo_Audiencia = cb_tipoAsunto.SelectedValue.ToString();
                        }
                        else {

                            ma.Tipo_Audiencia = cb_tipoAsunto.SelectedValue.ToString();
                        }

                        break;
                }


                ma.IdAudiencia = _listMA.FirstOrDefault().IdAudiencia;
                ma.Referencia = txt_referencia.Text.Trim();
                ma.Telefono = _listMA.FirstOrDefault().Telefono;
                ma.Tipo_Visita = cb_tipovisita.SelectedValue.ToString();
                ma.Fecha = DateTime.MinValue;

                if (check_asignarhora.IsChecked == true)
                {
                    ma.Fecha = dp_fecha.Value.Value;
                    conFecha = "SI";
                }

                CRUD.actualizarAudiencia(ma);
                lv_audiencia.ItemsSource = null;
                lv_audiencia.ItemsSource = _listMA = CRUD.ObtenerAudiecia(ma.IdAudiencia, conFecha );
                mensaje("Actualizado Satisdactoriamente"); 

                gb_actualizar_audiencia.Visibility = Visibility.Collapsed;
           //     gb_ingresarasunto.Visibility = Visibility.Collapsed;
                gb_personas.Visibility = Visibility.Visible;
                gb_listasuntos.Visibility = Visibility.Visible;
            }

        }

        string validarActualizarAudiencia()
        {
            string mensaje = "";

            //if (string.IsNullOrWhiteSpace(txt_referencia.Text))
            //    mensaje = mensaje + "\n* Referencia";
            if (cb_tipovisita.SelectedIndex == 0)
                mensaje = mensaje + "\n* Tipo de Visita";
            if (cb_tipoAsunto.SelectedIndex == 0)
                mensaje = mensaje + "\n* Tipo de Asunto";
            if (check_asignarhora.IsChecked == false && check_confirmar.IsChecked == false)
                mensaje = mensaje + "\n* Fecha y hora";

            return mensaje;
        }

        private void cancelar_ACAudiencia_Click(object sender, RoutedEventArgs e)
        {
            gb_actualizar_audiencia.Visibility = Visibility.Collapsed;
         //   gb_ingresarasunto.Visibility = Visibility.Collapsed;
            gb_personas.Visibility = Visibility.Visible;
            gb_listasuntos.Visibility = Visibility.Visible;
        }

        private void edita_audienciar_Click(object sender, RoutedEventArgs e)
        {
           

        }
        private void cb_distritos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ListaJuzgados = Juzgados.ObtenerJuzgados(cb_distritos.SelectedValue.ToString());
            //cb_juzgados.ItemsSource = ListaJuzgados;
            //cb_juzgados.DisplayMemberPath = "nombre";
            //cb_juzgados.SelectedValuePath = "id_juzgado";
            //cb_juzgados.SelectedIndex = 0;
        }

        private void cb_juzgados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btn_agregarAsunto_Click(object sender, RoutedEventArgs e)
        {

            AgregarAsunto nuevo = new AgregarAsunto(_listMA.FirstOrDefault().IdAudiencia);
            nuevo.ShowDialog();
            var a = CRUD.ObtenerIdAsunto(_listMA.FirstOrDefault().IdAudiencia);
            llenarAsuntos(a);

        }

        private void btn_participante_Click(object sender, RoutedEventArgs e)
        {
            
                List<ModeloPersonas> lista = new List<ModeloPersonas>();
                string mensaje = "";
                if (string.IsNullOrWhiteSpace(txt_nomPart.Text))
                    mensaje = "\n* Nombre Solicitante";
                if (string.IsNullOrWhiteSpace(txt_apPart.Text))
                    mensaje = mensaje + "\n* Apellido Paterno";
                if (string.IsNullOrWhiteSpace(txt_amPart.Text))
                    mensaje = mensaje + "\n* Apellido Materno";

                if (mensaje == "")
                {
                    url = "Por Capturar";
                    ModeloPersonas mp = new ModeloPersonas();
                    mp.Nombre = txt_nomPart.Text.Trim();
                    mp.APaterno = txt_apPart.Text.Trim();
                    mp.AMaterno = txt_amPart.Text.Trim();
                    mp.IdAudiencia = _listMA.FirstOrDefault().IdAudiencia;

                    lista.Add(mp);

                    Camara re = new Camara(lista, "NO");
                    re.pasardato += new Camara.pasar(ejecutar);
                    re.ShowDialog();
                    mp.URL_foto = url;

                if (btn_participante.Content.ToString() == "Añadir")
                {
                    CRUD.AgregarParticipante(mp);
                    this.mensaje("Guardado Satisfactoriamente");
                    txt_nomPart.Text = "";
                    txt_apPart.Text = "";
                    txt_amPart.Text = "";
                    lv_personas.ItemsSource = null;
                    lv_personas.ItemsSource = CRUD.ObtenerPersonas(mp.IdAudiencia);
                }
                else {
                    mp.URL_foto = mp.Nombre+"_"+mp.APaterno+"_"+mp.AMaterno+".png";
                    mp.IdPersona = ModeloP.IdPersona;
                    CRUD.actualizarParticipante(mp);
                    this.mensaje("Actualizado Satisfactoriamente");
                    txt_nomPart.Visibility = Visibility.Collapsed;
                    txt_apPart.Visibility = Visibility.Collapsed;
                    txt_amPart.Visibility = Visibility.Collapsed;
                    txt_nomPart.Text = "";
                    txt_apPart.Text = "";
                    txt_amPart.Text = "";

                    lblnombre.Visibility = Visibility.Collapsed;
                    lblap.Visibility = Visibility.Collapsed;
                    lblam.Visibility = Visibility.Collapsed;
                    btn_participante.Visibility = Visibility.Collapsed;
                    btn_partcancel.Visibility = Visibility.Collapsed;

                    lv_personas.ItemsSource = null;
                    lv_personas.ItemsSource = CRUD.ObtenerPersonas(mp.IdAudiencia);

                }


            }
                else
                {
                    this.mensaje(mensaje);
                }
            

        }

        public void ejecutar(int indice, string dato)
        {
            url = dato;
        }

        private void btn_partcancel_Click(object sender, RoutedEventArgs e)
        {
            txt_nomPart.Visibility = Visibility.Collapsed;
            txt_apPart.Visibility = Visibility.Collapsed;
            txt_amPart.Visibility = Visibility.Collapsed;
            txt_nomPart.Text ="";
            txt_apPart.Text = "";
            txt_amPart.Text = "";

            lblnombre.Visibility = Visibility.Collapsed;
            lblap.Visibility = Visibility.Collapsed;
            lblam.Visibility = Visibility.Collapsed;
            btn_participante.Visibility = Visibility.Collapsed;
            btn_partcancel.Visibility = Visibility.Collapsed;
        }

        private void btn_agregarParticipante_Click(object sender, RoutedEventArgs e)
        {
            txt_nomPart.Visibility = Visibility.Visible;
            txt_apPart.Visibility = Visibility.Visible;
            txt_amPart.Visibility = Visibility.Visible;
            txt_nomPart.Text = "";
            txt_apPart.Text = "";
            txt_amPart.Text = "";
            lblnombre.Visibility = Visibility.Visible;
            lblap.Visibility = Visibility.Visible;
            lblam.Visibility = Visibility.Visible;
            btn_participante.Visibility = Visibility.Visible;
            btn_partcancel.Visibility = Visibility.Visible;
            btn_participante.Content = "Añadir";

        }
    }
}
