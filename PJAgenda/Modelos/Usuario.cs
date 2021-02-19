using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
   public class User
    {
        public int Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Pass { get; set; }
        public int Activo { get; set; }

        public static List<User> Logear(string User, string Pass, ref RespuestaPeticion respuesta)
        {
            List<User> _lista = new List<User>();
            respuesta.Respuesta = 1;
            respuesta.Mensaje = "Exito";

            try
            {            
                SqlConnection conexion = BDConexion.ObtenerConexion();

                SqlCommand _comando = new SqlCommand($"select Id_Usuario, Nombre, Usuario, Pass, Activo  from AP_Users where Usuario='{User}' COLLATE SQL_Latin1_General_CP1_CS_AS  and Pass='{Pass}' COLLATE SQL_Latin1_General_CP1_CS_AS  and Activo=1 ", conexion);
                SqlDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    User us = new User();

                    us.Id_Usuario = _reader.GetInt32(0);
                    us.Nombre = _reader.GetString(1);
                    _lista.Add(us);
                }            
                return _lista;


            }
            catch (Exception ex)
            {
                respuesta.Respuesta = 0;

                respuesta.Mensaje = $"Error al ejecutar : {ex.Message}";
                return _lista;
            }





        }




    }
}
