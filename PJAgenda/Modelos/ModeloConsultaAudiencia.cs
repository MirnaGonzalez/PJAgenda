using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
    public class ModeloConsultaAudiencia
    {
            public int IdAudiencia { get; set; }
            public string Tipo_Visita { get; set; }
            public DateTime Fecha { get; set; }
            public string Nombre { get; set; }
            public string Tipo_Audiencia { get; set; }
            public long IdExpediente { get; set; }




        public static List<ModeloConsultaAudiencia> ObtenerAudiencias(string consulta)
        {
            List<ModeloConsultaAudiencia> _lista = new List<ModeloConsultaAudiencia>();

            SqlConnection conexion = BDConexion.ObtenerConexion();
            SqlCommand _comando = new SqlCommand($"select IdAudiencia, Tipo_Visita, Fecha, Nombre, Tipo_Audiencia, IdExpediente  from Vta_ConsultaAudiencia {consulta} order by Fecha desc", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                ModeloConsultaAudiencia dis = new ModeloConsultaAudiencia();

                dis.IdAudiencia = _reader.GetInt32(0);
                dis.Tipo_Visita = _reader.GetString(1);
                dis.Fecha = _reader.GetDateTime(2);
                dis.Nombre = _reader.GetString(3);
                dis.Tipo_Audiencia = _reader.GetString(4);
                dis.IdExpediente = _reader.GetInt64(5); 
                _lista.Add(dis);
            }
 
            return _lista;
        }

        public static List<ModeloConsultaAudiencia> ObtenerAudiencias()
        {
            List<ModeloConsultaAudiencia> _lista = new List<ModeloConsultaAudiencia>();

            SqlConnection conexion = BDConexion.ObtenerConexion();
            SqlCommand _comando = new SqlCommand($"select IdAudiencia, Tipo_Visita, Fecha, Nombre, Tipo_Audiencia, IdExpediente  from Vta_ConsultaAudiencia where Fecha is null order by Fecha desc", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                ModeloConsultaAudiencia dis = new ModeloConsultaAudiencia();

                dis.IdAudiencia = _reader.GetInt32(0);
                dis.Tipo_Visita = _reader.GetString(1);
                dis.Nombre = _reader.GetString(3);
                dis.Tipo_Audiencia = _reader.GetString(4);
                dis.IdExpediente = _reader.GetInt64(5);
                _lista.Add(dis);
            }

            return _lista;
        }
    }
}

