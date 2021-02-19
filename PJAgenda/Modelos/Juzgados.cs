using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
    public class Juzgados
    {
        public int id_juzgado { get; set; }
        public string tipo_juicio { get; set; }
        public string nombre { get; set; }


        public static List<Juzgados> ObtenerJuzgados(string distrito)
        {
            List<Juzgados> _lista = new List<Juzgados>();
            Juzgados juzgado = new Juzgados();
            juzgado.id_juzgado = 0;
            juzgado.nombre = " - Seleccione aquí - ";
            _lista.Add(juzgado);

            SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand($"select id_juzgado, tipo_juicio, nombre from juzgados where distrito={distrito} order by tipo_juicio desc, nombre ASC", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                Juzgados juz = new Juzgados();

                juz.id_juzgado = _reader.GetInt32(0);
                juz.tipo_juicio = _reader.GetString(1);
                juz.nombre = _reader.GetString(2);
                _lista.Add(juz);
            }

            return _lista;
        }


    }
}
