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
    /// Lógica de interacción para ConsultarAudiencia.xaml
    /// </summary>
    public partial class ConsultarAudiencia : Window
    {
        List<Distritos> ListaDistritos = new List<Distritos>();
        List<Juzgados> ListaJuzgados = new List<Juzgados>();
        List<ModeloConsultaAudiencia> _lista = new List<ModeloConsultaAudiencia>();

        public ConsultarAudiencia()
        {
            InitializeComponent();
            inicializar();

            
        }

        void consultar(string query) {

            _lista = ModeloConsultaAudiencia.ObtenerAudiencias(query).GroupBy(n => new { n.IdAudiencia })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList(); ;
            lv_audiencias.ItemsSource = null;
            lv_audiencias.ItemsSource = _lista;
            total_resultados.Content = $"Total de Audiencias: {_lista.Count} ";
            gb_resultados.Visibility = Visibility.Visible;
            gb_resultados2.Visibility = Visibility.Collapsed;
        }
        void consultarNull()
        {

            _lista = ModeloConsultaAudiencia.ObtenerAudiencias().GroupBy(n => new { n.IdAudiencia })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList(); ;
            lv_audiencias2.ItemsSource = null;
            lv_audiencias2.ItemsSource = _lista;
            total_resultadoss.Content = $"Total de Audiencias: {_lista.Count} ";
            gb_resultados.Visibility = Visibility.Collapsed;
            gb_resultados2.Visibility = Visibility.Visible;
        }


        void inicializar() {

            ListaDistritos = Distritos.ObtenerDistritos();
            cb_distrito.ItemsSource = ListaDistritos;
            cb_distrito.DisplayMemberPath = "nombre";
            cb_distrito.SelectedValuePath = "id_distrito";
            cb_distrito.SelectedIndex = 0;

            cb_tipoasunto.ItemsSource = ListaGenerica.ObtenerAsuntos();
            cb_tipoasunto.DisplayMemberPath = "Nombre";
            cb_tipoasunto.SelectedValuePath = "valor";
            cb_tipoasunto.SelectedIndex = 0;

            cb_tipovisita.ItemsSource = ListaGenerica.ObtenerVisitas();
            cb_tipovisita.DisplayMemberPath = "Nombre";
            cb_tipovisita.SelectedValuePath = "valor";
            cb_tipovisita.SelectedIndex = 0;

            cb_tipobusqueda.ItemsSource = ListaGenerica.ObtenerTipoBusqueda();
            cb_tipobusqueda.DisplayMemberPath = "Nombre";
            cb_tipobusqueda.SelectedValuePath = "valor";
            cb_tipobusqueda.SelectedIndex = 0;

            lbl_titulo.Visibility = Visibility.Collapsed;
            txt_folio.Visibility = Visibility.Collapsed;       
            grid_juzgado.Visibility = Visibility.Collapsed;
            grid_porNombre.Visibility = Visibility.Collapsed;
            gb_todo.Visibility = Visibility.Collapsed;
            gb_resultados.Visibility = Visibility.Collapsed;
            dp_fin.SelectedDate = DateTime.Now;
            dp_inicio.SelectedDate = DateTime.Now;
        }

        void visualizar(){
            lbl_titulo.Visibility = Visibility.Visible;
            txt_folio.Visibility = Visibility.Collapsed;
            grid_juzgado.Visibility = Visibility.Collapsed;
            grid_porNombre.Visibility = Visibility.Collapsed;
            cb_tipoasunto.Visibility = Visibility.Collapsed;
            cb_tipovisita.Visibility = Visibility.Collapsed;
            gb_todo.Visibility = Visibility.Visible;
            grid_porPeriodo.Visibility = Visibility.Collapsed;
            gb_resultados.Visibility = Visibility.Collapsed;
            gb_resultados2.Visibility = Visibility.Collapsed;
        }


        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            switch (cb_tipobusqueda.SelectedValue.ToString())
            {
                case "1":
                    if (string.IsNullOrWhiteSpace(txt_folio.Text.Trim()))
                    {
                        mensaje("Ingrese numero de folio");
                    }
                    else {
                        // Consultar
                        consultar($" where IdAudiencia={txt_folio.Text.Trim()}");

                    }

                    break;
                case "2":
                    if (cb_tipovisita.SelectedIndex == 0)
                    {
                        mensaje("Seleccione el tipo de visita");
                    }
                    else
                    { // Consultar
                        consultar($" where Tipo_Visita='{cb_tipovisita.SelectedValue}'");

                    }

                    break;
                case "3":
                    if (cb_tipoasunto.SelectedIndex == 0)
                    {
                        mensaje("Seleccione el tipo de visita");
                    }
                    else
                    { // Consultar
                        if (cb_tipoasunto.SelectedValue.ToString() == "J") {
                            var id = ConsultaridExpediente();
                            if (id == 999999999)
                            {
                                mensaje("Error de consulta");
                            }
                            else if (id== 888888888)
                            {
                                mensaje("No existen datos asociados");
                            }
                            else {

                                consultar($" where IdExpediente={id}");
                            }
                                
                        }
                        else
                            consultar($" where Tipo_Audiencia='{cb_tipoasunto.SelectedValue.ToString()}'");

                    }

                    break;
                case "4":
                    if (dp_inicio.SelectedDate.Value== dp_fin.SelectedDate.Value)
                    {
                        mensaje("Seleccione diferentes fechas");
                    }
                    else
                    { // Consultar
                            consultar($" where Fecha >= '{dp_inicio.SelectedDate.Value.AddDays(-1).ToString("dd/MM/yyyy")}' and Fecha <= '{dp_fin.SelectedDate.Value.AddDays(1).ToString("dd/MM/yyyy")}'");

                    }

                    break;
                case "5":
                    string mensaj = "";
                    if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                        mensaj = "\n* Nombre Solicitante";
                    if (string.IsNullOrWhiteSpace(txt_ap.Text))
                        mensaj = mensaj + "\n* Apellido Paterno";
                    if (string.IsNullOrWhiteSpace(txt_am.Text))
                        mensaj = mensaj + "\n* Apellido Materno";

                    if (mensaj != "")
                    {
                        mensaje("Faltan los siguientes campos:" + mensaj);
                    }
                    else {
                        //Consultar
                        consultar($" where Nombre='{txt_Nombre.Text} {txt_ap.Text} {txt_am.Text}'");
                    }
                    break;
                case "6":
                    break;
            }
        }

        void mensaje(string msj) {

            var alert = new SweetAlert();
            alert.Caption = "Aviso";
            alert.Message = msj;
            alert.MsgButton = SweetAlertButton.OK;
            alert.OkText = "Aceptar";
            alert.Show();
        }

        private void cb_tipobusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = cb_tipobusqueda.SelectedItem as ListaGenerica;
            if (a!=null)
            {
                lbl_titulo.Content = a.Nombre.ToString();
           
            }
            switch (cb_tipobusqueda.SelectedValue.ToString())
            {
                case "N":
                    visualizar();
                    lbl_titulo.Content = "";
                    gb_todo.Visibility = Visibility.Collapsed;

                    break;
                case "1":
                    visualizar();
                    txt_folio.Text = "";
                    txt_folio.Visibility = Visibility.Visible;
                    break;
                case "2":
                    visualizar();
                    cb_tipovisita.Visibility = Visibility.Visible;
                    cb_tipovisita.SelectedItem = 0;
                    break;
                case "3":
                    visualizar();
                    cb_tipoasunto.Visibility = Visibility.Visible;
                    cb_tipoasunto.SelectedItem = 0;
                    break;
                case "4":
                    visualizar();
                     grid_porPeriodo.Visibility = Visibility.Visible;
                    dp_inicio.SelectedDate = DateTime.Now;
                    dp_fin.SelectedDate = DateTime.Now;
                    break;
                case "5":
                    visualizar();
                    grid_porNombre.Visibility = Visibility.Visible;
                    lbl_titulo.Content = "";
                    txt_Nombre.Text = "";
                    txt_am.Text = "";
                    txt_ap.Text = "";
                    break;
                case "6":
                    //visualizar();
                    visualizar();
                    gb_todo.Visibility = Visibility.Collapsed;
                    consultar($" where (YEAR(Fecha)=YEAR(GETDATE()))and  (MONTH(Fecha)=MONTH(GETDATE())) and  (DAY(Fecha)=DAY(GETDATE())) ");

                    break;

                case "7":
                    //visualizar();
                    visualizar();
                    gb_todo.Visibility = Visibility.Collapsed;
                    consultarNull();
                    break;
            }
        }

        long ConsultaridExpediente() {
            var id = cb_juzgado.SelectedValue.ToString();
            var juz = (from a in ListaJuzgados where a.id_juzgado == int.Parse(id) select a).First();
            var idDistrito = cb_distrito.SelectedValue.ToString();


            if (juz.tipo_juicio == "V")
            {// Consulta Segunda Civil
                try
                {
                    completarExpediente("toca");
                    var asunto = AsuntosJudiciales.ObtenerAsuntosCYF2da(juz.id_juzgado.ToString(), txt_expediente.Text.Trim());
                    if (asunto.Count==0)
                    {
                        return 888888888;

                    }else
                    return asunto.ElementAt(0).id_asunto;
                }
                catch (Exception)
                {//Errror
                    return 999999999;
                }

            }
            else if (juz.id_juzgado == 80 || juz.id_juzgado == 60 || juz.id_juzgado == 59 || juz.id_juzgado == 58 || juz.id_juzgado == 61)
            {// Consulta Segunda Penal
                try
                {
                    completarExpediente("toca");
                    var asunto = AsuntosJudiciales.ObtenerAsuntosPenal2da(juz.id_juzgado.ToString(), txt_expediente.Text.Trim());

                    if (asunto.Count == 0)
                    {
                        return 888888888;

                    }
                    else
                        return asunto.ElementAt(0).id_asunto;
                }
                catch (Exception)
                {//Error
                    return 999999999;

                }

            }
            else
            {

                try
                {
                    completarExpediente("toca");
                    var asunto = AsuntosJudiciales.ObtenerAsuntosPenal1ra(juz.id_juzgado.ToString(), txt_expediente.Text.Trim());
                    if (asunto.Count == 0)
                    {
                        completarExpediente("ex");
                        asunto = AsuntosJudiciales.ObtenerAsuntosCYF1ra(idDistrito, juz.id_juzgado.ToString(), txt_expediente.Text.Trim());
                        if (asunto.Count == 0)
                        {
                            return 888888888;

                        }
                        else
                            return asunto.ElementAt(0).id_asunto;
                    }
                    else
                    {
                          if (asunto.Count==0)
                    {
                        return 888888888;

                    }else
                    return asunto.ElementAt(0).id_asunto;


                    
                }

                }
                catch (Exception ex)
                {//Error ex
                    return 999999999;
                }
            }

        }

        void completarExpediente(string tipo)
        {
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
            else
            {
                while (ex.Length < 9)
                {
                    String ejemplo = ex;
                    ejemplo = "0" + ejemplo;
                    ex = ejemplo;
                }
            }
            txt_expediente.Text = ex;
        }

        private void cb_tipovisita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cb_tipoasunto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cb_tipoasunto.SelectedValue.ToString())
            {
                case "J":
                    grid_juzgado.Visibility = Visibility.Visible;
                    txt_expediente.Text = "";
                    break;
                default:
                    grid_juzgado.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void cb_distrito_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListaJuzgados = Juzgados.ObtenerJuzgados(cb_distrito.SelectedValue.ToString());
            cb_juzgado.ItemsSource = ListaJuzgados;
            cb_juzgado.DisplayMemberPath = "nombre";
            cb_juzgado.SelectedValuePath = "id_juzgado";
            cb_juzgado.SelectedIndex = 0;
        }

        private void cb_juzgado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lv_audiencias_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListView list = (System.Windows.Controls.ListView)sender;
            ModeloConsultaAudiencia selectedObject = (ModeloConsultaAudiencia)list.SelectedItem;

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
            Button cancel = new Button
            {
                Content = "Cancelar",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button edit = new Button
            {
                Content = "Editar Audiencia",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button delete = new Button
            {
                Content = "Eliminar Audiencia",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button reporte = new Button
            {
                Content = "Generar reporte",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            delete.Click += delegate {
                try
                {
                    CRUD.eliminar(selectedObject.IdAudiencia);
                    mensaje("Eliminado Satisfactoriamente");
                    alert.Close();
                }
                catch (Exception)
                {
                    mensaje("Error al intentar consultar");

                }
            };

            edit.Click += delegate {
                //Editar
                alert.Close();
                EditarAudiencia c = new EditarAudiencia(selectedObject,"SI");
                gb_resultados.Visibility = Visibility.Collapsed;
                c.ShowDialog();
            };
            reporte.Click += delegate {
                //Generar Reporte
                alert.Close();
                Reporte report = new Reporte(selectedObject.IdAudiencia, selectedObject.Tipo_Audiencia);
                report.ShowDialog();
            };

            cancel.Click += delegate {
                //Generar Reporte
                alert.Close();
            };


            vPanel.Children.Add(delete);
            vPanel.Children.Add(edit);
            vPanel.Children.Add(reporte);
            vPanel.Children.Add(cancel);
            alert.ButtonContent = vPanel;
            alert.ShowDialog();
        }

        private void lv_audiencias2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListView list = (System.Windows.Controls.ListView)sender;
            ModeloConsultaAudiencia selectedObject = (ModeloConsultaAudiencia)list.SelectedItem;

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
            Button cancel = new Button
            {
                Content = "Cancelar",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button edit = new Button
            {
                Content = "Editar Audiencia",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button delete = new Button
            {
                Content = "Eliminar Audiencia",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };

            Button reporte = new Button
            {
                Content = "Generar reporte",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right

            };

            delete.Click += delegate {
                try
                {
                    CRUD.eliminar(selectedObject.IdAudiencia);
                    mensaje("Eliminado Satisfactoriamente");
                    alert.Close();
                }
                catch (Exception)
                {
                    mensaje("Error al intentar consultar");

                }
            };

            edit.Click += delegate {
                //Editar
                alert.Close();
                EditarAudiencia c = new EditarAudiencia(selectedObject,"No");
                gb_resultados2.Visibility = Visibility.Collapsed;

                c.ShowDialog();
            };
            reporte.Click += delegate {
                //Generar Reporte
                alert.Close();
            };

            cancel.Click += delegate {
                //Generar Reporte
                alert.Close();
            };


            vPanel.Children.Add(delete);
            vPanel.Children.Add(edit);
            vPanel.Children.Add(reporte);
            vPanel.Children.Add(cancel);
            alert.ButtonContent = vPanel;
            alert.ShowDialog();

        }
    }
}
