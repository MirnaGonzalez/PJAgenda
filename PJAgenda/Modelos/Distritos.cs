using PJAgenda;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
    public class Distritos
    {
        public int id_distrito { get; set; }
        public string distrito { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }


        public static List<Distritos> ObtenerDistritos()
        {
            List<Distritos> _lista = new List<Distritos>();
            Distritos distrito = new Distritos();
            distrito.id_distrito = 0;
            distrito.nombre =" - Seleccione aquí - ";
            _lista.Add(distrito);

            SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand("select id_distrito, distrito, nombre, tipo  from Distritos ", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                Distritos dis = new Distritos();

                dis.id_distrito = _reader.GetInt32(0);
                dis.distrito = _reader.GetString(1);
                dis.nombre = _reader.GetString(2);
                dis.tipo = _reader.GetString(3);
                _lista.Add(dis);
            }

            return _lista;
        }
    }
}
