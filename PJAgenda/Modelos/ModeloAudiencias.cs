using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
    public class ModeloAudiencias
    {
        public int IdAudiencia { get; set; }
        public string Tipo_Visita { get; set; }
        public string Telefono { get; set; }
        public string Tipo_Audiencia { get; set; }
        public string Referencia { get; set; }
        public DateTime Fecha { get; set; }
    }
}
