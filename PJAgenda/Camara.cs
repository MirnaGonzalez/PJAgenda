using AForge.Video;
using AForge.Video.DirectShow;
using MaterialSkin;
using MaterialSkin.Controls;
using PJAgenda.Modelos;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace PJAgenda
{
    public partial class Camara : MaterialForm
    {
        string editar;
        public delegate void pasar(int indice, string dato);
        public event pasar pasardato;

        private bool ExisteDispositivo = false;
        private FilterInfoCollection DispositivoDeVideo;
        private VideoCaptureDevice FuenteDeVideo = null;
        List<ModeloPersonas> listaPer = new List<ModeloPersonas>();

        protected override void OnClosing(CancelEventArgs e)
        {
            TerminarFuenteDeVideo();
        }

        public Camara(List<ModeloPersonas> lista, string editado)
        {

            editar = editado;


            listaPer = lista;
            InitializeComponent();
            BuscarDispositivos();

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );

            for (int i = 0; i < lista.Count; i++) {
                cb_listaPersonas.Items.Add(lista.ElementAt(i).Nombre.ToString() + " " + lista.ElementAt(i).APaterno.ToString() + " " + lista.ElementAt(i).AMaterno.ToString());
                cb_listaPersonas.Text = cb_listaPersonas.Items[i].ToString();
            }

            btn_Guardar.Enabled = false;
        }

        public void CargarDispositivos(FilterInfoCollection Dispositivos)
        {
            for (int i = 0; i < Dispositivos.Count; i++) ;

            cbxDispositivos.Items.Add(Dispositivos[0].Name.ToString());
            cbxDispositivos.Text = cbxDispositivos.Items[0].ToString();

        }

        public void BuscarDispositivos()
        {
            DispositivoDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (DispositivoDeVideo.Count == 0)
            {
                ExisteDispositivo = false;
            }

            else
            {
                ExisteDispositivo = true;
                CargarDispositivos(DispositivoDeVideo);

            }
        }

        public void TerminarFuenteDeVideo()
        {
            if (!(FuenteDeVideo == null))
                if (FuenteDeVideo.IsRunning)
                {
                    FuenteDeVideo.SignalToStop();
                    FuenteDeVideo = null;
                }

        }

        public void Video_NuevoFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            EspacioCamara.Image = Imagen;


        }

        private void btnIniciar_Click_1(object sender, EventArgs e)
        {
            if (btnIniciar.Text == "Iniciar")

            {
                if (ExisteDispositivo)
                {

                    FuenteDeVideo = new VideoCaptureDevice(DispositivoDeVideo[cbxDispositivos.SelectedIndex].MonikerString);
                    FuenteDeVideo.NewFrame += new NewFrameEventHandler(Video_NuevoFrame);
                    FuenteDeVideo.Start();
                    Estado.Text = "Ejecutando Dispositivo...";
                    btnIniciar.Text = "Detener";
                    cbxDispositivos.Enabled = false;
                    groupBox1.Text = DispositivoDeVideo[cbxDispositivos.SelectedIndex].Name.ToString();
                    btn_Guardar.Enabled = true;

                }
                else
                    Estado.Text = "Error: No se encuenta el Dispositivo";

            }
            else
            {
                if (FuenteDeVideo.IsRunning)
                {
                    TerminarFuenteDeVideo();
                    Estado.Text = "Dispositivo Detenido...";
                    btnIniciar.Text = "Iniciar";
                    cbxDispositivos.Enabled = true;
                    btn_Guardar.Enabled = false;


                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnIniciar.Text == "Detener")
                btnIniciar_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Camara_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // var a =  EspacioCamara.Image.Clone();


        }

        private async void btn_Guardar_Click(object sender, EventArgs e)
        {
            FuenteDeVideo.Stop();
            await Task.Delay(1500);
            if (editar == "NO")
            {
                if (verificarGuardar())
                {

                    // EspacioCamara.Image.Save($@"D:\Fotos\{listaPer.ElementAt(cb_listaPersonas.SelectedIndex).Nombre}.png", System.Drawing.Imaging.ImageFormat.Png);
                    //pasardato(cb_listaPersonas.SelectedIndex,listaPer.ElementAt(cb_listaPersonas.SelectedIndex).Nombre);
                    int indice = cb_listaPersonas.SelectedIndex;
                    //  string direccion = $@"D:\Fotos\{listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png";
                    string direccion = $@"C:\Fotos\{listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png";
                    string direccion2 = $@"C:\FOTOSTEMPORAL\{listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png";
                    // string direccion = Server.MapPath("192.168.73.7/Notificaciones/Prueba/")+ listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()".png";

                    EspacioCamara.Image.Save(direccion2, System.Drawing.Imaging.ImageFormat.Png);
                    cambiarResolucion(direccion2,direccion);

                    uploadfiles($@"Imagenes\{listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png ", "192.168.73.7", direccion);

                    pasardato(cb_listaPersonas.SelectedIndex, $"{ listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png");
                }
                FuenteDeVideo.Start();
                btn_Guardar.Enabled = true;

            }
            else
            {

                FuenteDeVideo.Stop();
                await Task.Delay(1500);

                if (verificarGuardar())
                {

                    int indice = cb_listaPersonas.SelectedIndex;
                    string direccion = $@"C:\Fotos\{listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png";
                    string direccion2 = $@"C:\FOTOSTEMPORAL\{listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png";
                    // string direccion = $@"C:\Fotos\{listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png";

                    EspacioCamara.Image.Save(direccion2, System.Drawing.Imaging.ImageFormat.Png);
                     cambiarResolucion(direccion2,direccion);
                    uploadfiles($@"Imagenes\{listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png ", "192.168.73.7", direccion);

                    CRUD.actualizarParticipante(listaPer.ElementAt(indice), $"{ listaPer.ElementAt(indice).Nombre.ToString() + "_" + listaPer.ElementAt(indice).APaterno.ToString() + "_" + listaPer.ElementAt(indice).AMaterno.ToString()}.png");
                    listaPer.RemoveAt(indice);
                    cb_listaPersonas.Items.RemoveAt(indice);
                }

                FuenteDeVideo.Start();
                btn_Guardar.Enabled = true;

            }
        }

        void cambiarResolucion(string DireccionImage, string dire)
        {
            using (Bitmap bitmap = (Bitmap)Image.FromFile(DireccionImage))
            {

                if (File.Exists(dire))
                {
                    // If file found, delete it    
                    File.Delete(dire);
                }

                //using (Bitmap newBitmap = new Bitmap(bitmap))
                //{

                    ResizeBitmap(bitmap, 320, 253).Save(dire, ImageFormat.Png);
                    //newBitmap.SetResolution(360, 640);

                    //newBitmap.Save(dire, ImageFormat.Png);
               // }
            }
        }


        public Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }

        public void uploadfiles(string fileName, string ipAddress, string filepath)
        {
           // string filepath = @"C:\temp";
          //  code to upload files within network
            string dns = "\\\\" + ipAddress;
         //   string dns = "ftp://" + ipAddress;

          //  create a Share folder in Server in which you want to place files
            string Share = @"\Notificaciones\";
         //   string Share = @"/Notificaciones/";
            string serverpath = dns + Share;
            WebClient client = new WebClient();
            string username = @"FTPUER";
            string pass = "ftp2020A";
            string uri = serverpath + fileName;
            NetworkCredential nc = new NetworkCredential(username, pass);
            Uri ServerPath1 = new Uri(uri);
            client.Credentials = nc;
            byte[] arrReturn = client.UploadFile(ServerPath1, filepath);
            Console.WriteLine("uploaded file :" + fileName);
        }



        //public void upload()
        //{
        //    string local = @"C:\temp";
        //    string[] dir = Directory.GetFiles(local, "*.txt");
        //    string filename;
        //    foreach (var files in dir)
        //    {
        //        filename = files.Substring(8, files.Length - 8);
        //        //here files gives complete file path including file name as C:\temp\file1.txt
        //        //here dns is ip of server
        //        //calling method
        //        uploadfiles(filename, files, dns);
        //    }
        //}

        public static Byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();

            //Obtener tamaño necesario el estandar es 4096
            //long Tam = stream.Length;

            byte[] chunk = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
                ms.Write(chunk, 0, bytesRead);

            return ms.ToArray();
        }




        bool verificarGuardar() {

            var alert = new SweetAlert();
            alert.Caption = "Aviso";
            alert.Message = "¿Desea Guardar la imagen?";
            alert.MsgButton = SweetAlertButton.YesNo;
            alert.OkText = "Si";
            alert.CancelText = "No";
            var a = alert.ShowDialog();

            if (a.Equals(SweetAlertSharp.Enums.SweetAlertResult.OK))
            {
                return true;
            }
            else {
                return false;
            }

        }

    }
}

