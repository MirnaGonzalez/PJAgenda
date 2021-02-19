using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
  public class ModeloReporte
    {
        public int IdAudiencia { get; set; }
        public string Tipo_Visita { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public string Tipo_Audiencia { get; set; }
        public int IdExpediente { get; set; }
        public string Referencia { get; set; }
        public string URL_foto { get; set; }

    }
}
