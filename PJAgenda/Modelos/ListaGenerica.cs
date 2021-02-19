using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
   public class ListaGenerica
    {
        public string Nombre { get; set; }
        public string valor { get; set; }


        public static List<ListaGenerica> ObtenerAsuntos() {
            List<ListaGenerica> ListaAsunto = new List<ListaGenerica>();
            ListaAsunto.Add(new ListaGenerica() { Nombre = " - Seleccione aquí - ", valor = "N" });
            ListaAsunto.Add(new ListaGenerica() { Nombre = "Judicial", valor = "J" });
            ListaAsunto.Add(new ListaGenerica() { Nombre = "Personal", valor = "Personal" });
            ListaAsunto.Add(new ListaGenerica() { Nombre = "Laboral", valor = "Laboral" });

            return ListaAsunto;
        }

        public static List<ListaGenerica> ObtenerVisitas()
        {
            List<ListaGenerica> ListaVisita = new List<ListaGenerica>();
            ListaVisita.Add(new ListaGenerica() { Nombre = " - Seleccione aquí - ", valor = "N" });
            ListaVisita.Add(new ListaGenerica() { Nombre = "Interna", valor = "Interna" });
            ListaVisita.Add(new ListaGenerica() { Nombre = "Externa", valor = "Externa" });
          

            return ListaVisita;
        }

        public static List<ListaGenerica> ObtenerTipoBusqueda()
        {
            List<ListaGenerica> ListaVisita = new List<ListaGenerica>();
            ListaVisita.Add(new ListaGenerica() { Nombre = " - Seleccione aquí - ", valor = "N" });
            ListaVisita.Add(new ListaGenerica() { Nombre = "Folio", valor = "1" });
            ListaVisita.Add(new ListaGenerica() { Nombre = "Tipo de Visita", valor = "2" });
            ListaVisita.Add(new ListaGenerica() { Nombre = "Tipo de Asunto", valor = "3" });
            ListaVisita.Add(new ListaGenerica() { Nombre = "Periodo de fecha", valor = "4" });
            ListaVisita.Add(new ListaGenerica() { Nombre = "Nombre", valor = "5" });
            ListaVisita.Add(new ListaGenerica() { Nombre = "Hoy", valor = "6" });
            ListaVisita.Add(new ListaGenerica() { Nombre = "Fecha por confirmar", valor = "7" });
            return ListaVisita;
        }


    }
}
