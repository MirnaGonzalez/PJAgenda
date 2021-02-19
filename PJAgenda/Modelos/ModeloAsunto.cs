using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
    public class ModeloAsunto
    {
        public int IdAsunto { get; set; }
        public int IdAudiencia { get; set; }
        public long IdExpediente { get; set; }
        public string Tipo { get; set; }
        public string Juzgado { get; set; }
        public string Numero { get; set; }
        public string Juicio { get; set; }

    }
}
