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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class AgregarAsunto : Window
    {
        List<Distritos> ListaDistritos = new List<Distritos>();
        List<Juzgados> ListaJuzgados = new List<Juzgados>();
        int audiencia;
        public AgregarAsunto(int idAudiencia)
        {
            InitializeComponent();
            iniciar();
            audiencia = idAudiencia;
        }

        void iniciar() {
            ListaDistritos = Distritos.ObtenerDistritos();
            cb_distritos.ItemsSource = ListaDistritos;
            cb_distritos.DisplayMemberPath = "nombre";
            cb_distritos.SelectedValuePath = "id_distrito";
            cb_distritos.SelectedIndex = 0; }

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
        private void btn_agregarAsunto_Click(object sender, RoutedEventArgs e)
        {
            var id = cb_juzgados.SelectedValue.ToString();
            var idDistrito = cb_distritos.SelectedValue.ToString();
            var juz = (from a in ListaJuzgados where a.id_juzgado == int.Parse(id) select a).First();
            
            if (juz.tipo_juicio == "V")
            {// Consulta Segunda Civil
                try
                {
                    completarExpediente("toca");
                    var asunto = AsuntosJudiciales.ObtenerAsuntosCYF2da(juz.id_juzgado.ToString(), txt_expediente.Text.Trim());

                    ModeloAsunto mA = new ModeloAsunto
                    {
                        IdExpediente = asunto.ElementAt(0).id_asunto,
                        Tipo = "2CFM",
                        Numero = asunto.ElementAt(0).numero,
                        Juicio = asunto.ElementAt(0).juicio,
                        Juzgado = asunto.ElementAt(0).juzgado
                    };

                    confirmarGuadar(asunto.FirstOrDefault(), mA);

                }
                catch (Exception)
                {//Errrorma

                }

            }
            else if (juz.id_juzgado == 80 || juz.id_juzgado == 60 || juz.id_juzgado == 59 || juz.id_juzgado == 58 || juz.id_juzgado == 61)
            {// Consulta Segunda Penal
                try
                {
                    completarExpediente("toca");
                    var asunto = AsuntosJudiciales.ObtenerAsuntosPenal2da(juz.id_juzgado.ToString(), txt_expediente.Text.Trim());

                    ModeloAsunto mA = new ModeloAsunto
                    {
                        IdExpediente = asunto.ElementAt(0).id_asunto,
                        Tipo = "2P",
                        Numero = asunto.ElementAt(0).numero,
                        Juicio = asunto.ElementAt(0).juicio,
                        Juzgado = asunto.ElementAt(0).juzgado
                    };

                    confirmarGuadar(asunto.FirstOrDefault(), mA);


                }
                catch (Exception)
                {//Error

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

                        ModeloAsunto mA = new ModeloAsunto
                        {
                            IdExpediente = asunto.ElementAt(0).id_asunto,
                            Tipo = "1CFM",
                            Numero = asunto.ElementAt(0).numero,
                            Juicio = asunto.ElementAt(0).juicio,
                            Juzgado = asunto.ElementAt(0).juzgado
                        };

                        confirmarGuadar(asunto.FirstOrDefault(), mA);


                    }
                    else
                    {

                        ModeloAsunto mA = new ModeloAsunto
                        {
                            IdExpediente = asunto.ElementAt(0).id_asunto,
                            Tipo = "1P",
                            Numero = asunto.ElementAt(0).numero,
                            Juicio = asunto.ElementAt(0).juicio,
                            Juzgado = asunto.ElementAt(0).juzgado
                        };

                        confirmarGuadar(asunto.FirstOrDefault(), mA);


                    }

                }
                catch (Exception ex)
                {//Error ex

                }
            }
            txt_expediente.Text = "";
            cb_distritos.SelectedIndex = 0;
            cb_juzgados.SelectedIndex = 0;

        }

        void completarExpediente(string tipo)
        {
            var NumExpediente = txt_expediente.Text.Trim();
            string NumExpCompleto="";
            if (tipo == "ex")
            {
                NumExpediente = NumExpediente.Replace("-", "/");
                 NumExpCompleto = NumExpediente.ToUpper();
                if (NumExpCompleto.Substring(0, 1) == "E")
                {
                    string[] splitchar = { NumExpCompleto.Substring(0, 1) };
                    string[] NumDiv = NumExpCompleto.Split(splitchar, StringSplitOptions.None);
                    if (NumDiv[1].Length < 10)
                        NumExpCompleto = NumExpCompleto.Substring(0, 1) + NumDiv[1].PadLeft(10, '0');
                }
                else if (NumExpCompleto.Substring(0, 1) == "T")
                {
                    string[] splitchar = { NumExpCompleto.Substring(0, 2) };
                    string[] NumDiv = NumExpCompleto.Split(splitchar, StringSplitOptions.None);
                    if (NumDiv[1].Length < 9)
                        NumExpCompleto = NumExpCompleto.Substring(0, 2) + NumDiv[1].PadLeft(9, '0');
                }
                else
                    NumExpCompleto = NumExpCompleto.PadLeft(11, '0');

              
            }
            else
            {
                NumExpediente = NumExpediente.Replace("-", "/");
                 NumExpCompleto = NumExpediente.ToUpper();
                if (NumExpCompleto.Substring(0, 1) == "E")
                {
                    string[] splitchar = { NumExpCompleto.Substring(0, 1) };
                    string[] NumDiv = NumExpCompleto.Split(splitchar, StringSplitOptions.None);
                    if (NumDiv[1].Length < 10)
                        NumExpCompleto = NumExpCompleto.Substring(0, 1) + NumDiv[1].PadLeft(10, '0');
                }
                else if (NumExpCompleto.Substring(0, 1) == "T")
                {
                    string[] splitchar = { NumExpCompleto.Substring(0, 2) };
                    string[] NumDiv = NumExpCompleto.Split(splitchar, StringSplitOptions.None);
                    if (NumDiv[1].Length < 9)
                        NumExpCompleto = NumExpCompleto.Substring(0, 2) + NumDiv[1].PadLeft(9, '0');
                }
                else
                    NumExpCompleto = NumExpCompleto.PadLeft(9, '0');
            }
            txt_expediente.Text = NumExpCompleto;
        }

       void confirmarGuadar(AsuntosJudiciales a, ModeloAsunto b)
        {
            b.IdAudiencia = audiencia;
            var alert = new SweetAlert();
            alert.Caption = "Aviso";
            alert.Message = $"Los datos del caso son:\n Juzgado: {a.juzgado}\n Parte {a.nombre}\n Juicio: {a.juicio}";

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

            Button Guardar = new Button
            {
                Content = "Guardar asunto",
                Foreground = Brushes.White,
                Width = 150,
                Margin = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Right,

            };




            Guardar.Click += delegate {
                //Editar
                try
                {
                    CRUD.agregarAsunto(b);
                    mensaje("Guardado Satisfacotiamente");
                    alert.Close();
                }
                catch (Exception)
                {
                    mensaje("Error al insertar intente nuevamente");
                }
            };


            cancel.Click += delegate {
                //Generar Reporte
                alert.Close();
            };

            vPanel.Children.Add(Guardar);

            vPanel.Children.Add(cancel);
            alert.ButtonContent = vPanel;
            alert.ShowDialog();
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
    }
}
