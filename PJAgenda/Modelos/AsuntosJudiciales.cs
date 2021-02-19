using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
    public class AsuntosJudiciales
    {
        public int id_juzgado { get; set; }
        public long id_asunto { get; set; }
        public string numero { get; set; }
        public string juzgado { get; set; }
        public string juicio { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }



        public static List<AsuntosJudiciales> ObtenerAsuntosCYF1ra(string distrito, string idJuz, string expediente)
        {
            List<AsuntosJudiciales> _lista = new List<AsuntosJudiciales>();

            SqlConnection conexion =  BDConexion.ObtenerTipoConexion(distrito);

            SqlCommand _comando = new SqlCommand($"select id_juzgado, id_asunto, numero, juzgado, juicio, nombre, fecha  from Vta_APJuicioExpediente where id_juzgado={idJuz} and numero='{expediente}'", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                AsuntosJudiciales dis = new AsuntosJudiciales();

                dis.id_juzgado = _reader.GetInt32(0);
                dis.id_asunto = _reader.GetInt64(1);
                dis.numero = _reader.GetString(2);
                dis.juzgado = _reader.GetString(3);
                dis.juicio = _reader.GetString(4);
                dis.nombre = _reader.GetString(5);
                dis.fecha = _reader.GetDateTime(6);

                _lista.Add(dis);
            }

            return _lista;
        }
        public static List<AsuntosJudiciales> ObtenerAsuntosCYF1ra(long id)
        {
            List<AsuntosJudiciales> _lista = new List<AsuntosJudiciales>();

            SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand($"select id_juzgado, id_asunto, numero, juzgado, juicio, nombre, fecha  from Vta_APJuicioExpediente where id_asunto={id}", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                AsuntosJudiciales dis = new AsuntosJudiciales();

                dis.id_juzgado = _reader.GetInt32(0);
                dis.id_asunto = _reader.GetInt64(1);
                dis.numero = _reader.GetString(2);
                dis.juzgado = _reader.GetString(3);
                dis.juicio = _reader.GetString(4);
                dis.nombre = _reader.GetString(5);
                dis.fecha = _reader.GetDateTime(6);

                _lista.Add(dis);
            }

            return _lista;
        }


        public static List<AsuntosJudiciales> ObtenerAsuntosCYF2da(string idJuz, string expediente)
        {
            List<AsuntosJudiciales> _lista = new List<AsuntosJudiciales>();

            SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand($"select id_juzgado, id_asunto, numero, juzgado, juicio, nombre, fecha  from Vta_APJuicioToca where id_juzgado={idJuz} and numero='{expediente}'", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                AsuntosJudiciales dis = new AsuntosJudiciales();

                dis.id_juzgado = _reader.GetInt32(0);
                dis.id_asunto = _reader.GetInt64(1);
                dis.numero = _reader.GetString(2);
                dis.juzgado = _reader.GetString(3);
                dis.juicio = _reader.GetString(4);
                dis.nombre = _reader.GetString(5);
                dis.fecha = _reader.GetDateTime(6);

                _lista.Add(dis);
            }

            return _lista;
        }
        public static List<AsuntosJudiciales> ObtenerAsuntosCYF2da(long id)
        {
            List<AsuntosJudiciales> _lista = new List<AsuntosJudiciales>();

            SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand($"select id_juzgado, id_asunto, numero, juzgado, juicio, nombre, fecha  from Vta_APJuicioToca where id_asunto={id}", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                AsuntosJudiciales dis = new AsuntosJudiciales();

                dis.id_juzgado = _reader.GetInt32(0);
                dis.id_asunto = _reader.GetInt64(1);
                dis.numero = _reader.GetString(2);
                dis.juzgado = _reader.GetString(3);
                dis.juicio = _reader.GetString(4);
                dis.nombre = _reader.GetString(5);
                dis.fecha = _reader.GetDateTime(6);

                _lista.Add(dis);
            }

            return _lista;
        }

        public static List<AsuntosJudiciales> ObtenerAsuntosPenal1ra(string idJuz, string expediente)
        {
            List<AsuntosJudiciales> _lista = new List<AsuntosJudiciales>();

            SqlConnection conexion = BDConexion.ObtenerConexionPenal();

            SqlCommand _comando = new SqlCommand($"select id_juzgado, id_asunto, numero, juzgado, juicio, nombre, fecha  from Vta_APCausaDelitos1ra where id_juzgado={idJuz} and numero='{expediente}'", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                AsuntosJudiciales dis = new AsuntosJudiciales();

                dis.id_juzgado = _reader.GetInt32(0);
                dis.id_asunto = _reader.GetInt64(1);
                dis.numero = _reader.GetString(2);
                dis.juzgado = _reader.GetString(3);
                dis.juicio = _reader.GetString(4);
                dis.nombre = _reader.GetString(5);
                dis.fecha = _reader.GetDateTime(6);

                _lista.Add(dis);
            }

            return _lista;
        }
        public static List<AsuntosJudiciales> ObtenerAsuntosPenal1ra(long id)
        {
            List<AsuntosJudiciales> _lista = new List<AsuntosJudiciales>();
       
            SqlConnection conexion = BDConexion.ObtenerConexionPenal();

            SqlCommand _comando = new SqlCommand($"select id_juzgado, id_asunto, numero, juzgado, juicio, nombre, fecha  from Vta_APCausaDelitos1ra where id_asunto={id}", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                AsuntosJudiciales dis = new AsuntosJudiciales();

                dis.id_juzgado = _reader.GetInt32(0);
                dis.id_asunto = _reader.GetInt64(1);
                dis.numero = _reader.GetString(2);
                dis.juzgado = _reader.GetString(3);
                dis.juicio = _reader.GetString(4);
                dis.nombre = _reader.GetString(5);
                dis.fecha = _reader.GetDateTime(6);

                _lista.Add(dis);
            }

            return _lista;
        }

        public static List<AsuntosJudiciales> ObtenerAsuntosPenal2da(string idJuz, string expediente)
        {
            List<AsuntosJudiciales> _lista = new List<AsuntosJudiciales>();

            SqlConnection conexion = BDConexion.ObtenerConexionPenal();

            SqlCommand _comando = new SqlCommand($"select id_juzgado, id_asunto, numero, juzgado, juicio, nombre, fecha  from Vta_APCausaDelitos2da where id_juzgado={idJuz} and numero='{expediente}'", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                AsuntosJudiciales dis = new AsuntosJudiciales();

                dis.id_juzgado = _reader.GetInt32(0);
                dis.id_asunto = _reader.GetInt64(1);
                dis.numero = _reader.GetString(2);
                dis.juzgado = _reader.GetString(3);
                dis.juicio = _reader.GetString(4);
                dis.nombre = _reader.GetString(5);
                dis.fecha = _reader.GetDateTime(6);

                _lista.Add(dis);
            }

            return _lista;
        }
        public static List<AsuntosJudiciales> ObtenerAsuntosPenal2da(long id)
        {
            List<AsuntosJudiciales> _lista = new List<AsuntosJudiciales>();

            SqlConnection conexion = BDConexion.ObtenerConexionPenal();

            SqlCommand _comando = new SqlCommand($"select id_juzgado, id_asunto, numero, juzgado, juicio, nombre, fecha  from Vta_APCausaDelitos2da where id_asunto={id}", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                AsuntosJudiciales dis = new AsuntosJudiciales();

                dis.id_juzgado = _reader.GetInt32(0);
                dis.id_asunto = _reader.GetInt64(1);
                dis.numero = _reader.GetString(2);
                dis.juzgado = _reader.GetString(3);
                dis.juicio = _reader.GetString(4);
                dis.nombre = _reader.GetString(5);
                dis.fecha = _reader.GetDateTime(6);

                _lista.Add(dis);
            }

            return _lista;
        }
    }
}
