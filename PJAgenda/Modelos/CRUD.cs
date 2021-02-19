using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
    public class CRUD
    {
        public static List<ModeloAudiencias> ObtenerAudiecia(int id, string conFecha)
        {

            List<ModeloAudiencias> _list = new List<ModeloAudiencias>();

             SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand($"select IdAudiencia, Tipo_Visita, Telefono, Tipo_Audiencia, Fecha, ISNULL([Referencia], '---') AS [Referencia]  from [AP_Audiencias] where IdAudiencia={id}", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                ModeloAudiencias audiencia = new ModeloAudiencias();
                audiencia.IdAudiencia = _reader.GetInt32(0);
                audiencia.Tipo_Visita = _reader.GetString(1);
                audiencia.Telefono = _reader.GetString(2);
                audiencia.Tipo_Audiencia = _reader.GetString(3);
                if (conFecha=="SI")
                {
                  audiencia.Fecha = _reader.GetDateTime(4);
                }
               
                audiencia.Referencia = _reader.GetString(5);
                _list.Add(audiencia);
            }

            return _list;
        }

        public static List<ModeloPersonas> ObtenerPersonas(int id)
        {
            List<ModeloPersonas> _lista = new List<ModeloPersonas>();

            SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand($"select IdPersona, [Nombre], [APaterno], [AMaterno], [TipoPersona], [IdAudiencia], [URL_foto]  from AP_Personas where IdAudiencia={id}", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                ModeloPersonas dis = new ModeloPersonas();

                dis.IdPersona = _reader.GetInt32(0);
                dis.Nombre = _reader.GetString(1);
                 dis.APaterno = _reader.GetString(2);
                dis.AMaterno = _reader.GetString(3);
                dis.TipoPersona = _reader.GetString(4);
                dis.IdAudiencia = _reader.GetInt32(5);
                dis.URL_foto = _reader.GetString(6);
                _lista.Add(dis);
            }

            return _lista;
        }

        public static List<ModeloPersonas> ObtenerPersonasFoto()
        {
            List<ModeloPersonas> _lista = new List<ModeloPersonas>();

            SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand($"select IdPersona, [Nombre], [APaterno], [AMaterno], [TipoPersona], [IdAudiencia], [URL_foto]  from AP_Personas where URL_foto='Por Capturar'", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                ModeloPersonas dis = new ModeloPersonas();

                dis.IdPersona = _reader.GetInt32(0);
                dis.Nombre = _reader.GetString(1);
                dis.APaterno = _reader.GetString(2);
                dis.AMaterno = _reader.GetString(3);
                dis.TipoPersona = _reader.GetString(4);
                dis.IdAudiencia = _reader.GetInt32(5);
                dis.URL_foto = _reader.GetString(6);
                _lista.Add(dis);
            }

            return _lista;
        }

        public static List<ModeloPersonas> ObtenerPersonasReporte(int id)
        {
            List<ModeloPersonas> _lista = new List<ModeloPersonas>();

            SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand($"select Distinct  [Nombre] , URL_foto  from Vta_APReporte where IdAudiencia={id}", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                ModeloPersonas dis = new ModeloPersonas();
                dis.Nombre = _reader.GetString(0);
                dis.URL_foto = _reader.GetString(1);

                _lista.Add(dis);
            }

            return _lista;
        }

        public static DataTable ObtenerPersonasDS(int id, string nombre)
        {
            List<ModeloPersonas> _lista = new List<ModeloPersonas>();

            SqlConnection conexion = BDConexion.ObtenerConexion();

           string sql =$"SELECT  [IdAudiencia], [Tipo_Visita] , [Fecha] , [Nombre] , [Tipo_Audiencia] , [IdExpediente] , [Referencia] , [URL_foto] , [Juzgado], [Numero] , [Juicio] FROM [Vta_APReporte] where IdAudiencia={id} and [Nombre]='{nombre}' ";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conexion);
            adapter.Fill(ds, "Vta_APReporte");

            return ds.Tables["Vta_APReporte"];

        }
        public static List<ModeloAsunto> ObtenerIdAsunto(int id)
        {
            List<ModeloAsunto> _lista = new List<ModeloAsunto>();

            SqlConnection conexion = BDConexion.ObtenerConexion();

            SqlCommand _comando = new SqlCommand($"select [IdAsunto], IdAudiencia, IdExpediente, Tipo  from AP_AsuntosJudiciales where IdAudiencia={id}", conexion);
            SqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                ModeloAsunto dis = new ModeloAsunto();

                dis.IdAsunto = _reader.GetInt32(0);
                dis.IdAudiencia = _reader.GetInt32(1);
                dis.IdExpediente = _reader.GetInt64(2);
                dis.Tipo = _reader.GetString(3);
                    _lista.Add(dis);

            }

                return _lista;
            
        }

        public static async Task<RespuestaPeticion> nuevaCitaPersonasJudicial(ModeloAudiencias audiencias, List<ModeloPersonas> listapersonas, List<ModeloAsunto> listasunto)
        {
            RespuestaPeticion respuesta = new RespuestaPeticion();
            string insertarAudiencia = @"INSERT INTO AP_Audiencias (Tipo_Visita, Telefono, Tipo_Audiencia, Fecha, Referencia) VALUES  
                                        (@Tipo_Visita, @Telefono, @Tipo_Audiencia, @Fecha, @Referencia);
                                             SELECT SCOPE_IDENTITY();";

            string insertarAsistentes = @"INSERT INTO AP_Personas ([Nombre], [APaterno], [AMaterno], [TipoPersona], [IdAudiencia], [URL_foto]) VALUES 
                                        (@Nombre, @APaterno, @AMaterno, @TipoPersona, @IdAudiencia, @URL_foto);
                                            SELECT SCOPE_IDENTITY();";

            string insertarAsunto = @"INSERT INTO AP_AsuntosJudiciales (IdAudiencia, IdExpediente, Tipo, Juzgado, Numero, Juicio) VALUES 
                                        (@IdAudiencia, @IdExpediente, @Tipo, @Juzgado, @Numero, @Juicio);
                                            SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand(insertarAudiencia, connection, tx);
                    command.Parameters.Add(new SqlParameter("@Tipo_Visita", SqlDbType.VarChar)).Value = audiencias.Tipo_Visita;
                    command.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = audiencias.Telefono;
                    command.Parameters.Add(new SqlParameter("@Tipo_Audiencia", SqlDbType.VarChar)).Value = audiencias.Tipo_Audiencia;

                    command.Parameters.Add(new SqlParameter("@Referencia", SqlDbType.VarChar)).Value = audiencias.Referencia;

                    if (audiencias.Fecha == DateTime.MinValue)
                    {
                        command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime)).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime)).Value = audiencias.Fecha;
                    }

                    int idAudiencia = Convert.ToInt32(command.ExecuteScalar());

                    for (int i = 0; i < listapersonas.Count; i++)
                    {
                        SqlCommand command2 = new SqlCommand(insertarAsistentes, connection, tx);
                        command2.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).Nombre;
                        command2.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).APaterno;
                        command2.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).AMaterno;
                        command2.Parameters.Add(new SqlParameter("@TipoPersona", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).TipoPersona;
                        command2.Parameters.Add(new SqlParameter("@URL_foto", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).URL_foto;
                        command2.Parameters.Add(new SqlParameter("@IdAudiencia", SqlDbType.Int)).Value = idAudiencia;
                        command2.ExecuteScalar();
                    }

                    var a = (from obj in listasunto
                             select obj).GroupBy(n => new { n.IdExpediente })
                                           .Select(g => g.FirstOrDefault())
                                           .ToList();

                    for (int i = 0; i < a.Count; i++)
                    {
                        SqlCommand command3 = new SqlCommand(insertarAsunto, connection, tx);
                        command3.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.VarChar)).Value = a.ElementAt(i).Tipo;
                        command3.Parameters.Add(new SqlParameter("@IdExpediente", SqlDbType.Int)).Value = a.ElementAt(i).IdExpediente;
                        command3.Parameters.Add(new SqlParameter("@IdAudiencia", SqlDbType.Int)).Value = idAudiencia;
                        command3.Parameters.Add(new SqlParameter("@Juzgado", SqlDbType.VarChar)).Value = a.ElementAt(i).Juzgado;
                        command3.Parameters.Add(new SqlParameter("@Numero", SqlDbType.VarChar)).Value = a.ElementAt(i).Numero;
                        command3.Parameters.Add(new SqlParameter("@Juicio", SqlDbType.VarChar)).Value = a.ElementAt(i).Juicio;
                        command3.ExecuteScalar();
                    }

                    tx.Commit();
                    await Task.Delay(100);
                    respuesta.Mensaje = "Judicial";
                    respuesta.Respuesta = idAudiencia;
                    return respuesta;

                }
            }
        }

        public static async Task<RespuestaPeticion> nuevaCitaNormal(ModeloAudiencias audiencias, List<ModeloPersonas> listapersonas)
        {
            RespuestaPeticion respuesta = new RespuestaPeticion();

            string insertarAudiencia = @"INSERT INTO AP_Audiencias (Tipo_Visita, Telefono, Tipo_Audiencia, Fecha, Referencia) VALUES 
                                        (@Tipo_Visita, @Telefono, @Tipo_Audiencia, @Fecha, @Referencia);
                                             SELECT SCOPE_IDENTITY();";

            string insertarAsistentes = @"INSERT INTO AP_Personas ([Nombre], [APaterno], [AMaterno], [TipoPersona], [IdAudiencia], [URL_foto]) VALUES 
                                        (@Nombre, @APaterno, @AMaterno, @TipoPersona, @IdAudiencia, @URL_foto);
                                                 SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand(insertarAudiencia, connection, tx);
                    command.Parameters.Add(new SqlParameter("@Tipo_Visita", SqlDbType.VarChar)).Value = audiencias.Tipo_Visita;
                    command.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = audiencias.Telefono;
                    command.Parameters.Add(new SqlParameter("@Tipo_Audiencia", SqlDbType.VarChar)).Value = audiencias.Tipo_Audiencia;
                    command.Parameters.Add(new SqlParameter("@Referencia", SqlDbType.VarChar)).Value = audiencias.Referencia;

                    if (audiencias.Fecha == DateTime.MinValue)
                    {
                        command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime)).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime)).Value = audiencias.Fecha;
                    }
                    int idAudiencia = Convert.ToInt32(command.ExecuteScalar());

                    for (int i = 0; i < listapersonas.Count; i++)
                    {
                        SqlCommand command2 = new SqlCommand(insertarAsistentes, connection, tx);
                        command2.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).Nombre;
                        command2.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).APaterno;
                        command2.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).AMaterno;
                        command2.Parameters.Add(new SqlParameter("@TipoPersona", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).TipoPersona;
                        command2.Parameters.Add(new SqlParameter("@URL_foto", SqlDbType.VarChar)).Value = listapersonas.ElementAt(i).URL_foto;
                        command2.Parameters.Add(new SqlParameter("@IdAudiencia", SqlDbType.Int)).Value = idAudiencia;
                        command2.ExecuteScalar();
                    }

                    tx.Commit();
                    await Task.Delay(100);
                    respuesta.Mensaje = "Normal";
                    respuesta.Respuesta = idAudiencia;
                    return respuesta;
                }
            }
        }

        public static void AgregarParticipante(ModeloPersonas listapersonas)
        {
          
            string insertarAsistentes = @"INSERT INTO AP_Personas ([Nombre], [APaterno], [AMaterno], [TipoPersona], [IdAudiencia], [URL_foto]) VALUES 
                                        (@Nombre, @APaterno, @AMaterno, @TipoPersona, @IdAudiencia, @URL_foto);
                                                 SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    SqlCommand command2 = new SqlCommand(insertarAsistentes, connection, tx);
                    command2.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = listapersonas.Nombre;
                    command2.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = listapersonas.APaterno;
                    command2.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = listapersonas.AMaterno;
                    command2.Parameters.Add(new SqlParameter("@TipoPersona", SqlDbType.VarChar)).Value = "Asistente";
                    command2.Parameters.Add(new SqlParameter("@URL_foto", SqlDbType.VarChar)).Value = listapersonas.URL_foto;
                    command2.Parameters.Add(new SqlParameter("@IdAudiencia", SqlDbType.Int)).Value = listapersonas.IdAudiencia;
                    command2.ExecuteScalar();
                                  
                    tx.Commit();
                }
            }
        }
        public static void actualizarParticipante(ModeloPersonas listapersonas)
        {
            string sql = $@"UPDATE AP_Personas SET
                            [Nombre] = @Nombre, [APaterno] = @APaterno, [AMaterno] = @AMaterno, " +
                           "[URL_foto] = @URL_foto Where IdPersona=" + listapersonas.IdPersona;

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    SqlCommand command2 = new SqlCommand(sql, connection, tx);
                    command2.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = listapersonas.Nombre;
                    command2.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = listapersonas.APaterno;
                    command2.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = listapersonas.AMaterno;
                    command2.Parameters.Add(new SqlParameter("@URL_foto", SqlDbType.VarChar)).Value = listapersonas.URL_foto;
                    command2.ExecuteScalar();

                    tx.Commit();
                }
            }
        }
        public static void actualizarParticipante(ModeloPersonas persona, string url)
        {
            string sql = $@"UPDATE AP_Personas SET  
                           [URL_foto] = @URL_foto Where IdAudiencia={persona.IdAudiencia}";

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    SqlCommand command2 = new SqlCommand(sql, connection, tx);
                    command2.Parameters.Add(new SqlParameter("@URL_foto", SqlDbType.VarChar)).Value = url;
                    command2.ExecuteScalar();

                    tx.Commit();
                }
            }
        }


        public static void eliminarParticipante(int id)
        {

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    var query3 = $"delete from [AP_Personas] where [IdPersona]={id}";

                    SqlCommand command3 = new SqlCommand(query3, connection, tx);
                    command3.ExecuteNonQuery();

                    tx.Commit();
                }
            }
        }
        public static void actualizarAudiencia(ModeloAudiencias audiencias)
        {
            string sql = $@"UPDATE AP_Audiencias SET
                            [Tipo_Visita] = @Tipo_Visita, [Telefono] = @Telefono, [Tipo_Audiencia] = @Tipo_Audiencia, "+
                           "[Fecha] = @Fecha, [Referencia] = @Referencia Where IdAudiencia=" +audiencias.IdAudiencia;

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    SqlCommand command = new SqlCommand(sql, connection, tx);
                    command.Parameters.Add(new SqlParameter("@Tipo_Visita", SqlDbType.VarChar)).Value = audiencias.Tipo_Visita;
                    command.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = audiencias.Telefono;
                    command.Parameters.Add(new SqlParameter("@Tipo_Audiencia", SqlDbType.VarChar)).Value = audiencias.Tipo_Audiencia;
                    command.Parameters.Add(new SqlParameter("@Referencia", SqlDbType.VarChar)).Value = audiencias.Referencia;

                   if (audiencias.Fecha == DateTime.MinValue)
                    {
                        command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime)).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.DateTime)).Value = audiencias.Fecha;
                    }

                    command.ExecuteNonQuery();
                    tx.Commit();
                }
            }
        }

        public static void agregarAsunto(ModeloAsunto asunto)
        {


            //string insertarAsunto = @"INSERT INTO AP_AsuntosJudiciales (IdAudiencia, IdExpediente, Tipo) VALUES 
            //                            (@IdAudiencia, @IdExpediente, @Tipo);
            //                                SELECT SCOPE_IDENTITY();";

            string insertarAsunto = @"INSERT INTO AP_AsuntosJudiciales (IdAudiencia, IdExpediente, Tipo, Juzgado, Numero, Juicio) VALUES 
                                        (@IdAudiencia, @IdExpediente, @Tipo, @Juzgado, @Numero, @Juicio);
                                            SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    SqlCommand command3 = new SqlCommand(insertarAsunto, connection, tx);
                    command3.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.VarChar)).Value = asunto.Tipo;
                    command3.Parameters.Add(new SqlParameter("@IdExpediente", SqlDbType.Int)).Value = asunto.IdExpediente;
                    command3.Parameters.Add(new SqlParameter("@IdAudiencia", SqlDbType.Int)).Value = asunto.IdAudiencia;
                    command3.Parameters.Add(new SqlParameter("@Juzgado", SqlDbType.VarChar)).Value = asunto.Juzgado;
                    command3.Parameters.Add(new SqlParameter("@Numero", SqlDbType.VarChar)).Value = asunto.Numero;
                    command3.Parameters.Add(new SqlParameter("@Juicio", SqlDbType.VarChar)).Value = asunto.Juicio;

                    command3.ExecuteScalar();


                    tx.Commit();
                }
            }
        }

        public static void eliminarAsunto(long id)
        {

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    var query = $"delete from [AP_AsuntosJudiciales] where [IdExpediente]={id}";
                    SqlCommand command = new SqlCommand(query, connection, tx);
                    command.ExecuteNonQuery();
                    tx.Commit();
                }
            }
        }
        public static void eliminar(int id)
        {

            using (var connection = new SqlConnection(BDConexion.cadenaConexion()))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    var query = $"delete from [AP_AsuntosJudiciales] where [IdAudiencia]={id}";
                    var query2 = $"delete from [AP_Audiencias] where [IdAudiencia]={id}";
                    var query3 = $"delete from [AP_Personas] where [IdAudiencia]={id}";

                    SqlCommand command = new SqlCommand(query, connection, tx);
                    command.ExecuteNonQuery();

                    SqlCommand command2 = new SqlCommand(query2, connection, tx);
               
                    command2.ExecuteNonQuery();


                    SqlCommand command3 = new SqlCommand(query3, connection, tx);
                    command3.ExecuteNonQuery();

                    tx.Commit();
                }
            }
        }
    }
}
