using PJAgenda.Modelos;
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

namespace PJAgenda
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
        {
            RegistroAudiencia re = new RegistroAudiencia();
            re.ShowDialog();
            //Camara cam = new Camara();
            //cam.ShowDialog();
        }

      

        private void btn_consultar_Click(object sender, RoutedEventArgs e)
        {

            ConsultarAudiencia re = new ConsultarAudiencia();
            re.ShowDialog();
        }

        private void btn_capturar_Click(object sender, RoutedEventArgs e)
        {
            var personas = CRUD.ObtenerPersonasFoto();
           
            Camara cam = new Camara(personas, "SI");
            cam.Show();
        }
    }
}
