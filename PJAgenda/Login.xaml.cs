using PJAgenda.Modelos;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            var msj = validar();
            if (!String.IsNullOrEmpty(msj))
            {
                var alert = new SweetAlert();
                alert.Caption = "Aviso";
                alert.Message = msj;
                alert.MsgButton = SweetAlertButton.OK;
                alert.OkText = "Aceptar";
                alert.Show();
            }
            else {

                try
                {
                    RespuestaPeticion respuesta = new RespuestaPeticion();
                    var lista = User.Logear(txt_user.Text, txt_pass.Password, ref respuesta);
                    if (respuesta.Respuesta==0)
                    {
                        var alert = new SweetAlert();
                        alert.Caption = "Aviso";
                        alert.Message =respuesta.Mensaje;
                        alert.MsgButton = SweetAlertButton.OK;
                        alert.OkText = "Aceptar";
                        alert.Show();
                        return;
                    }

                    if (lista.Count == 0)
                    {
                        var alert = new SweetAlert();
                        alert.Caption = "Aviso";
                        alert.Message = "Valide sus datos o intente nuevamente";
                        alert.MsgButton = SweetAlertButton.OK;
                        alert.OkText = "Aceptar";
                        alert.Show();
                    }
                    else {
                        string root = @"C:\FOTOS";
                        string temp = @"C:\FOTOSTEMPORAL";                   
                        // If directory does not exist, create it. 
                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }


                        if (!Directory.Exists(temp))
                        {
                            Directory.CreateDirectory(temp);
                        }

                        MainWindow re = new MainWindow();
                        re.Show();
                        this.Close ();
                    }
                }
                catch (Exception Ex)
                {

                    var alert = new SweetAlert();
                    alert.Caption = "Aviso";
                    alert.Message = Ex.Message;
                    alert.MsgButton = SweetAlertButton.OK;
                    alert.OkText = "Aceptar";
                    alert.Show();
                }           
            }
        }


        string validar() {
            string mensaje = "";
            if (string.IsNullOrWhiteSpace(txt_user.Text))
                mensaje = "\n* Usuario";
            if (string.IsNullOrWhiteSpace(txt_pass.Password))
                mensaje = mensaje + "\n* Contraseña";
          

            return mensaje;

        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
