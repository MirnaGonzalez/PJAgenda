using MaterialSkin;
using MaterialSkin.Controls;
using PJAgenda.Modelos;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace PJAgenda
{
    public partial class Reporte : MaterialForm
    {
        int Audiencia;
        string tipo;

        public Reporte(int id, string tipoAudiencia)
        {
            
            Audiencia = id;
            tipo = tipoAudiencia;
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
            txt_folio.Text = id.ToString();
            llenarNombres(id);
        }

        async void llenarNombres(int id)
        {
            await Task.Delay(100);
            var a = CRUD.ObtenerPersonasReporte(id);  
            cb_asistentes.DataSource = a;
            cb_asistentes.DisplayMember = "Nombre";
            cb_asistentes.ValueMember = "Nombre";
            Vacio cr = new Vacio();
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Enabled = false;


        }






        private void cb_asistentes_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btn_generar_Click(object sender, EventArgs e)
        {
            var a = CRUD.ObtenerPersonasDS(Audiencia, cb_asistentes.SelectedValue.ToString());
            var perosnas = CRUD.ObtenerPersonasReporte(Audiencia);
            var persona = (from datos in perosnas
                           where datos.Nombre == cb_asistentes.SelectedValue.ToString()
                           select datos).FirstOrDefault();

             downloadImage(persona.URL_foto);

            try
            {
                crystalReportViewer1.Enabled = true;

              
                if (tipo == "Judicial")
                {
                    InfoReporteNormal cr = new InfoReporteNormal();
                  
                   
                    cr.SetDataSource(a);
                    crystalReportViewer1.ReportSource = cr;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    InfoReporte cr = new InfoReporte();
                 //   downloadImage(persona.URL_foto);
                    cr.SetDataSource(a);
                    crystalReportViewer1.ReportSource = cr;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception ex)
            {
                mensaje($"Error al ejecutar intenta nuevamente. Error {ex.Message}");
                
            }
                         
        }

        public void downloadImage(string nombre)
        {
            //WebClient client = new WebClient();
            //string username = @"FTPUER";
            //string pass = "ftp2020A";
            //string path = @"http://200.79.183.187/DoctosNotif/";
            ////code to get list of files in remote directory
            //DirectoryInfo info = new DirectoryInfo(path);
            //FileInfo[] files = info.GetFiles("Lupita_Lopez_Juarez.png", SearchOption.TopDirectoryOnly);
            //foreach (var item in files)
            //{
            //    string filename = item.ToString();
            //    string serverpath = path + "\\" + filename;
            //    string localpath = @"C:\Fotos\" + filename;
            //    NetworkCredential nc = new NetworkCredential(username, pass);
            //    client.DownloadFileAsync(new Uri(serverpath), localpath);
            //}


            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData("http://200.79.183.187/DoctosNotif/Imagenes/" + nombre);

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            // If you want it as Png
                            yourImage.Save($@"C:\Fotos\{nombre}", ImageFormat.Png);

                            // If you want it as Jpeg
                            //  yourImage.Save("path_to_your_file.jpg", ImageFormat.Jpeg);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

        
            }

        }
    }
}
