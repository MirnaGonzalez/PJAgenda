using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda
{
   public class BDConexion
    {

        public static string cadenaConexion() {
            string Conexion = @"data source = 200.79.183.181; initial catalog = tsjhciviles; persist security info = True; user id = Programadores2017; password = Development10; MultipleActiveResultSets = True;";
            return Conexion;
        }

        public static SqlConnection ObtenerTipoConexion(string Distrito)
        {
            
            if (Distrito == "14")
            {
                return ObtenerConexionTula();
            }
            else if(Distrito=="15")
            {
                return ObtenerConexionTulancingo();
            }else
            {
                return ObtenerConexion();
            }
        }




        public static SqlConnection ObtenerConexionTula()
        {
            SqlConnection Conexion = new SqlConnection(@"data source = 200.79.180.65; initial catalog = tsjhciviles; persist security info = True; user id = SIJEH; password = SERVER; MultipleActiveResultSets = True;");
            Conexion.Open();
            return Conexion;

        }
        public static SqlConnection ObtenerConexionTulancingo()
        {
            SqlConnection Conexion = new SqlConnection(@"data source = 200.79.183.194; initial catalog = tsjhciviles; persist security info = True; user id = SIJEH; password = SERVER; MultipleActiveResultSets = True;");
            Conexion.Open();
            return Conexion;

        }


        public static SqlConnection ObtenerConexion()
        {
            SqlConnection Conexion = new SqlConnection(@"data source = 200.79.183.181; initial catalog = tsjhciviles; persist security info = True; user id = Programadores2017; password = Development10; MultipleActiveResultSets = True;");
            Conexion.Open();
            return Conexion;

        }

        public static SqlConnection ObtenerConexionPenal()
        {
            SqlConnection Conexion = new SqlConnection(@"data source = 200.79.183.181; initial catalog = tsjhpenales; persist security info = True; user id = Programadores2017; password = Development10; MultipleActiveResultSets = True;");
            Conexion.Open();
            return Conexion;
        }
    }
}
