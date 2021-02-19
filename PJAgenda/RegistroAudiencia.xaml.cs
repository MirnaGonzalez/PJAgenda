using MaterialDesignMessageBox;
using MaterialDesignThemes.Wpf;
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
    /// Lógica de interacción para RegistroAudiencia.xaml
    /// </summary>
    public partial class RegistroAudiencia : Window
    {
        RespuestaPeticion responseReport = new RespuestaPeticion();
        List<Distritos> ListaDistritos = new List<Distritos>();
        List<AsuntosJudiciales> ListaAsuntos = new List<AsuntosJudiciales>();
        List<Juzgados> ListaJuzgados = new List<Juzgados>();
        List<Personas> ListaPersonas = new List<Personas>();
        Personas deletepersona = new Personas();
        AsuntosJudiciales deleteasunto = new AsuntosJudiciales();

        List<ModeloAsunto> listAsuntoInsertar = new List<ModeloAsunto>();
        List<ModeloPersonas> listPersonasInsertar = new List<ModeloPersonas>();
        ModeloPersonas solicitante = new ModeloPersonas();
        ModeloAudiencias modelAudiencia = new ModeloAudiencias();


        public RegistroAudiencia()
        {
            InitializeComponent();
            inicializarPantalla();
        }

        void inicializarPantalla()
        {

            try
            {
                ListaDistritos = Distritos.ObtenerDistritos();
                cb_distritos.ItemsSource = ListaDistritos;
                cb_distritos.DisplayMemberPath = "nombre";
                cb_distritos.SelectedValuePath = "id_distrito";
                cb_distritos.SelectedIndex = 0;

                cb_tipoAsunto.ItemsSource = ListaGenerica.ObtenerAsuntos();
                cb_tipoAsunto.DisplayMemberPath = "Nombre";
                cb_tipoAsunto.SelectedValuePath = "valor";
                cb_tipoAsunto.SelectedIndex = 0;

                cb_tipovisita.ItemsSource = ListaGenerica.ObtenerVisitas();
                cb_tipovisita.DisplayMemberPath = "Nombre";
                cb_tipovisita.SelectedValuePath = "valor";
                cb_tipovisita.SelectedIndex = 0;

                gb_asunto.Visibility = Visibility.Collapsed;
                gb_participante.Visibility = Visibility.Collapsed;
                btn_partcancel.Visibility = Visibility.Collapsed;

                solicitante.TipoPersona = "Asistente";
                dp_fecha.DefaultValue = DateTime.Today;
                dp_fecha.CalendarWidth = 300;
                dp_fecha.Value = DateTime.Now;

                dp_fecha.Visibility = Visibility.Collapsed;
                btn_reporte.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                mensaje("Error de conexión al iniciar, cierre la presente pantalla y vuelva a iniciar.");
                this.Close();
            
            }
        }

        private void cb_distritos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListaJuzgados = Juzgados.ObtenerJuzgados(cb_distritos.SelectedValue.ToString());
            cb_juzgados.ItemsSource = ListaJuzgados;
            cb_juzgados.DisplayMemberPath = "nombre";
            cb_juzgados.SelectedValuePath = "id_juzgado";
            cb_juzgados.SelectedIndex = 0;
        }

        private void cb_juzgados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cb_tipoAsunto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_tipoAsunto.SelectedValue.Equals("J"))
            {
                gb_asunto.Visibility = Visibility.Visible;
                modelAudiencia.Tipo_Audiencia = "Judicial";
            }
            else
            {
                gb_asunto.Visibility = Visibility.Collapsed;
                modelAudiencia.Tipo_Audiencia = cb_tipoAsunto.SelectedValue.ToString();
            }
        }

        private void cb_tipovisita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_tipovisita.SelectedValue.Equals("N"))
            {
              

            }
            else {
                modelAudiencia.Tipo_Visita = cb_tipovisita.SelectedValue.ToString();
            }
        }

        private void check_solicitante_Checked(object sender, RoutedEventArgs e)
        {
            gb_participante.Visibility = Visibility.Visible;
            solicitante.TipoPersona = "Solicitante";
        }

        private void check_solicitante_Unchecked(object sender, RoutedEventArgs e)
        {
            gb_participante.Visibility = Visibility.Collapsed;
            solicitante.TipoPersona = "Solicitante";
        }

        private void btn_participante_Click(object sender, RoutedEventArgs e)
        {
            if (btn_participante.Content.ToString() == "Añadir")
            {
                agregarPersonaLista();
              
            }
            else {
                ListaPersonas.RemoveAll(x => x.Nombre == deletepersona.Nombre &&
                x.APaterno == deletepersona.APaterno &&
                x.AMaterno == deletepersona.AMaterno);
                agregarPersonaLista();
                btn_cancelar.Visibility = Visibility.Collapsed;
            }

        }

        public void agregarPersonaLista() {
            var mensaje = validarPersonas();
            if (mensaje.Equals(""))
            {
                Personas items = new Personas
                {
                    Nombre = txt_nomPart.Text.Trim(),
                    APaterno = txt_apPart.Text.Trim(),
                    AMaterno = txt_amPart.Text.Trim(),
                    TipoPersona = "Asistente",
                    URL_foto="Por capturar"
                };

                ListaPersonas.Add(items);
                lv_participantes.ItemsSource = null;
                lv_participantes.ItemsSource = ListaPersonas;
                txt_nomPart.Text = "";
                txt_apPart.Text = "";
                txt_amPart.Text = "";
                btn_participante.Content = "Añadir";
                btn_cancelar.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Faltan los siguientes elementos:" + mensaje, "Aviso");
            }

        }
   
        string validarPersonas()
        {
            string mensaje="";
            if (string.IsNullOrWhiteSpace(txt_nomPart.Text))
                mensaje = "\n* Nombre";
            if (string.IsNullOrWhiteSpace(txt_nomPart.Text))
                mensaje =mensaje+ "\n* Apellido Paterno";
            if (string.IsNullOrWhiteSpace(txt_nomPart.Text))
                mensaje = mensaje +"\n* Apellido Materno";
            return mensaje;

        }

        private void btn_agregarAsunto_Click(object sender, RoutedEventArgs e)
        {
           var id = cb_juzgados.SelectedValue.ToString();
           var juz= (from a in ListaJuzgados where a.id_juzgado == int.Parse(id) select a).First();
            var idDistrito = cb_distritos.SelectedValue.ToString();


            if (juz.tipo_juicio == "V")
            {// Consulta Segunda Civil
                try
                {
                    completarExpediente("toca");
                    var asunto = AsuntosJudiciales.ObtenerAsuntosCYF2da(juz.id_juzgado.ToString(), txt_expediente.Text.Trim());
                    for (int i = 0; i < asunto.Count; i++)
                    {
                        ModeloAsunto mA = new ModeloAsunto
                        {
                            IdExpediente = asunto.ElementAt(i).id_asunto,
                            Tipo = "2CFM",
                            Numero = asunto.ElementAt(i).numero, Juicio = asunto.ElementAt(i).juicio, Juzgado = asunto.ElementAt(i).juzgado
                        };

                        listAsuntoInsertar.Add(mA);
                        ListaAsuntos.Add(asunto.ElementAt(i));
                    }
                }
                catch (Exception ex )
                {//Errror
                    mensaje($"Error al ejecutar: {ex.Message}");
                }

            }
            else if (juz.id_juzgado == 80 || juz.id_juzgado == 60 || juz.id_juzgado == 59 || juz.id_juzgado == 58 || juz.id_juzgado == 61)
            {// Consulta Segunda Penal
                try
                {
                    completarExpediente("toca");
                    var asunto = AsuntosJudiciales.ObtenerAsuntosPenal2da(juz.id_juzgado.ToString(), txt_expediente.Text.Trim());
                    for (int i = 0; i < asunto.Count; i++)
                    {
                        ModeloAsunto mA = new ModeloAsunto
                        {
                            IdExpediente = asunto.ElementAt(i).id_asunto,
                            Tipo = "2P",
                            Numero = asunto.ElementAt(i).numero,
                            Juicio = asunto.ElementAt(i).juicio,
                            Juzgado = asunto.ElementAt(i).juzgado
                        };

                        listAsuntoInsertar.Add(mA);
                        ListaAsuntos.Add(asunto.ElementAt(i));
                    }
                }
                catch (Exception ex)
                {//Error
                    mensaje($"Error al ejecutar: {ex.Message}");
                }

            }
            else {

                try
                {
                    completarExpediente("toca");
                    var asunto = AsuntosJudiciales.ObtenerAsuntosPenal1ra(juz.id_juzgado.ToString(), txt_expediente.Text.Trim());              
                    if (asunto.Count==0)
                    {
                        completarExpediente("ex");
                        asunto = AsuntosJudiciales.ObtenerAsuntosCYF1ra(idDistrito, juz.id_juzgado.ToString(), txt_expediente.Text.Trim());
                        for (int i = 0; i < asunto.Count; i++)
                        {
                            ModeloAsunto mA = new ModeloAsunto
                            {
                                IdExpediente = asunto.ElementAt(i).id_asunto,
                                Tipo = "1CFM",
                                Numero = asunto.ElementAt(i).numero,
                                Juicio = asunto.ElementAt(i).juicio,
                                Juzgado = asunto.ElementAt(i).juzgado
                            };

                            listAsuntoInsertar.Add(mA);
                            ListaAsuntos.Add(asunto.ElementAt(i));
                        }
                    }
                    else {
                        for (int i = 0; i < asunto.Count; i++)
                        {
                            ModeloAsunto mA = new ModeloAsunto
                            {
                                IdExpediente = asunto.ElementAt(i).id_asunto,
                                Tipo = "1P",
                                Numero = asunto.ElementAt(i).numero,
                                Juicio = asunto.ElementAt(i).juicio,
                                Juzgado = asunto.ElementAt(i).juzgado
                            };

                            listAsuntoInsertar.Add(mA);
                            ListaAsuntos.Add(asunto.ElementAt(i));
                        }
                    }

                }
                catch (Exception ex)
                {//Error ex
                    mensaje($"Error al ejecutar: {ex.Message}");
                }
            }
            txt_expediente.Text = "";
            cb_distritos.SelectedIndex=0;
            cb_juzgados.SelectedIndex = 0;
            lv_asuntoJudicial.ItemsSource = null;
            lv_asuntoJudicial.ItemsSource = ListaAsuntos;

        }

        private void lv_participantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var personaSeleccionada = e.AddedItems[0] as Personas;
            //txt_nomPart.Text = personaSeleccionada.Nombre;
            //txt_apPart.Text = personaSeleccionada.APaterno;
            //txt_amPart.Text = personaSeleccionada.AMaterno;

        }

        private void lv_asuntoJudicial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    

        private void lv_participantes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListView list = (System.Windows.Controls.ListView)sender;
            Personas selectedObject = (Personas)list.SelectedItem;
            deletepersona = selectedObject;



            var alert = new SweetAlert();
            alert.Caption = "Aviso";
            alert.Message = "Seleccione la acción a realizar";

            StackPanel vPanel = new StackPanel
            {
                Margin = new Thickness(10),
                MinWidth = 150,
                MinHeight = 70,
                Orientation=Orientation.Horizontal
            };

            Button actualizar = new Button
            {
                Content = "Actualizar",
                Foreground = Brushes.White,
                Width = 100,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            Button delete = new Button
            {
                Content = "Eliminar",
                Foreground = Brushes.White,
                Width = 100,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button cancelar = new Button
            {
                Content = "Cancelar",
                Foreground = Brushes.White,
                Width = 100,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right
               
            };

            actualizar.Click += delegate {
                txt_nomPart.Text = selectedObject.Nombre;
                txt_apPart.Text = selectedObject.APaterno;
                txt_amPart.Text = selectedObject.AMaterno;
                btn_participante.Content = "Actualizar";
                btn_partcancel.Visibility = Visibility.Visible;
                alert.Close();

            };

            delete.Click += delegate {

                ListaPersonas.RemoveAll(x => x.Nombre == deletepersona.Nombre &&
                x.APaterno == deletepersona.APaterno &&
                x.AMaterno == deletepersona.AMaterno);
                lv_participantes.ItemsSource = null;
                lv_participantes.ItemsSource = ListaPersonas;
                alert.Close();

            };

            cancelar.Click += delegate {
                //validarPersonas();
                alert.Close();
            };


            vPanel.Children.Add(actualizar);
            vPanel.Children.Add(delete);
            vPanel.Children.Add(cancelar);        
            alert.ButtonContent = vPanel;
            alert.ShowDialog();

      
        }

        private void lv_asuntoJudicial_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListView list = (System.Windows.Controls.ListView)sender;
            AsuntosJudiciales selectedObject = (AsuntosJudiciales)list.SelectedItem;
            deleteasunto = selectedObject;

            var alert = new SweetAlert();
            alert.Caption = "Aviso";
            alert.Message = "Seleccione la acción a realizar";

            StackPanel vPanel = new StackPanel
            {
                Margin = new Thickness(10),
                MinWidth = 150,
                MinHeight = 70,
                Orientation = Orientation.Horizontal
            };



            Button delete = new Button
            {
                Content = "Eliminar",
                Foreground = Brushes.White,
                Width = 100,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button cancelar = new Button
            {
                Content = "Cancelar",
                Foreground = Brushes.White,
                Width = 100,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right

            };

   

            delete.Click += delegate {

                ListaAsuntos.RemoveAll(x => x.nombre == deleteasunto.nombre &&
                x.id_juzgado == deleteasunto.id_juzgado &&
                x.id_asunto == deleteasunto.id_asunto);
                listAsuntoInsertar.RemoveAll(x => x.IdExpediente == deleteasunto.id_asunto);
                lv_asuntoJudicial.ItemsSource = null;
                lv_asuntoJudicial.ItemsSource = ListaAsuntos;
                alert.Close();
            };

            cancelar.Click += delegate {
                //validarPersonas();
                alert.Close();
            };


            vPanel.Children.Add(delete);
            vPanel.Children.Add(cancelar);
            alert.ButtonContent = vPanel;
            alert.ShowDialog();

        }

        private void btn_partcancel_Click(object sender, RoutedEventArgs e)
        {
            txt_nomPart.Text = "";
            txt_apPart.Text = "";
            txt_amPart.Text = "";
            btn_participante.Content = "Añadir";
            btn_partcancel.Visibility = Visibility.Collapsed;

        }

        void completarExpediente(string tipo) {
            var ex = txt_expediente.Text.Trim();
            if (tipo == "ex")
            {
                while (ex.Length < 11)
                {
                    String ejemplo = ex;
                    ejemplo = "0" + ejemplo;
                    ex = ejemplo;
                }
            }
            else {
                while (ex.Length < 9)
                {
                    String ejemplo = ex;
                    ejemplo = "0" + ejemplo;
                    ex = ejemplo;
                }
            }
            txt_expediente.Text = ex;
        }

        private void check_confirmar_Checked(object sender, RoutedEventArgs e)
        {
            dp_fecha.Visibility = Visibility.Collapsed;
            check_asignarhora.IsEnabled = false;
            modelAudiencia.Fecha = DateTime.MinValue;
        }

        private void check_confirmar_Unchecked(object sender, RoutedEventArgs e)
        {     
            dp_fecha.Visibility = Visibility.Collapsed;
            check_asignarhora.IsEnabled = true;
            modelAudiencia.Fecha = DateTime.MinValue;

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
            modelAudiencia.Fecha = DateTime.MinValue;

        }

        private async void btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            var msj = validarGuardado();
            if (!String.IsNullOrEmpty(msj))
            {
                var alert = new SweetAlert();
                alert.Caption = "Aviso";
                alert.Message = msj;
                alert.MsgButton = SweetAlertButton.OK;
                alert.OkText = "Aceptar";
                alert.Show();
            }
            else
            {
                listPersonasInsertar.Clear();

                solicitante.Nombre = txt_NombreSoli.Text.Trim();
                solicitante.APaterno = txt_apSoli.Text.Trim();
                solicitante.AMaterno = txt_amSoli.Text.Trim();
                modelAudiencia.Telefono = txt_telSoli.Text.Trim();
                modelAudiencia.Referencia = txt_referencia.Text.Trim();
                solicitante.URL_foto = "Por Capturar";
                solicitante.TipoPersona = "Solicitante";

                listPersonasInsertar.Add(solicitante);
                if (check_asignarhora.IsChecked == true)
                {
                    modelAudiencia.Fecha = dp_fecha.Value.Value;
                }

                if (check_solicitante.IsChecked == true)
                {
                    llenarListaPersonas();
                }

                try
                {
                    Camara re = new Camara(listPersonasInsertar, "NO");
                    re.pasardato += new Camara.pasar(ejecutar);
                    re.ShowDialog();

                    if (modelAudiencia.Tipo_Audiencia == "Judicial")
                    {
                        responseReport = await CRUD.nuevaCitaPersonasJudicial(modelAudiencia, listPersonasInsertar, listAsuntoInsertar);
                        btn_guardar.IsEnabled = false;
                        btn_reporte.Visibility = Visibility.Visible;
                        mensaje($"Cita generada satisfactoriamente con número de folio: {responseReport.Respuesta}.");

                    }
                    else
                    {
                        responseReport = await CRUD.nuevaCitaNormal(modelAudiencia, listPersonasInsertar);
                        btn_guardar.IsEnabled = false;
                        btn_reporte.Visibility = Visibility.Visible;
                        mensaje($"Cita generada satisfactoriamente con número de folio: {responseReport.Respuesta}.");
                    }
                }
                catch (Exception ex)
                {
                    var alert = new SweetAlert();
                    alert.Caption = "Error al insertar";
                    alert.Message = ex.Message;
                    alert.MsgButton = SweetAlertButton.OK;
                    alert.OkText = "Aceptar";
                    alert.Show();
                }
            }
        }


        void llenarListaPersonas() {

            for (int i = 0; i < ListaPersonas.Count; i++)
            {
                ModeloPersonas mA = new ModeloPersonas
                {
                    Nombre = ListaPersonas.ElementAt(i).Nombre,
                    APaterno = ListaPersonas.ElementAt(i).APaterno,
                    AMaterno = ListaPersonas.ElementAt(i).AMaterno,
                    TipoPersona = ListaPersonas.ElementAt(i).TipoPersona,
                    URL_foto="Por Capturar"
                };
                listPersonasInsertar.Add(mA);
            }
        }

        private void  btn_reporte_Click(object sender, RoutedEventArgs e)
        {        
            Reporte report = new Reporte(responseReport.Respuesta, responseReport.Mensaje);
            report.ShowDialog();
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            verificarSalir();
        }

        string validarGuardado()
        {
            string mensaje = "";
            if (string.IsNullOrWhiteSpace(txt_NombreSoli.Text))
                mensaje = "\n* Nombre Solicitante";
            if (string.IsNullOrWhiteSpace(txt_apSoli.Text))
                mensaje = mensaje + "\n* Apellido Paterno";
            if (string.IsNullOrWhiteSpace(txt_amSoli.Text))
                mensaje = mensaje + "\n* Apellido Materno";
            if (string.IsNullOrWhiteSpace(txt_telSoli.Text))
                mensaje = mensaje + "\n* Telefono";
            if (cb_tipovisita.SelectedIndex==0)
                mensaje = mensaje + "\n* Tipo de Visita";
            if (cb_tipoAsunto.SelectedIndex==0)
                mensaje = mensaje + "\n* Tipo de Asunto";
            if (check_asignarhora.IsChecked==false && check_confirmar.IsChecked==false)
                mensaje = mensaje + "\n* Fecha y hora";
            if (check_solicitante.IsChecked == true && ListaPersonas.Count == 0)
                mensaje = mensaje + "\n* Agregar asistentes";

            return mensaje;

        }


        public void ejecutar(int indice, string dato)
        {
            listPersonasInsertar.ElementAt(indice).URL_foto = dato;
        }

        private void btn_tomar_foto_Click(object sender, RoutedEventArgs e)
        {

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

        void verificarSalir()
        {

            var alert = new SweetAlert();
            alert.Caption = "Aviso";
            alert.Message = "¿Desea Salir del registro?";
            alert.MsgButton = SweetAlertButton.YesNo;
            alert.OkText = "Si";
            alert.CancelText = "No";
            var a = alert.ShowDialog();

            if (a.Equals(SweetAlertSharp.Enums.SweetAlertResult.OK))
            {
                this.Close();
                    }
            else
            {
            }

        }



    }
   
}
