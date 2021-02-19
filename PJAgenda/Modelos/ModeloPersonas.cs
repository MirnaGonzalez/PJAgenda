using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJAgenda.Modelos
{
    public class ModeloPersonas
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string TipoPersona { get; set; }
        public int IdAudiencia { get; set; }
        public string URL_foto { get; set; }

        public string NombreCompleto { get
            {
                return Nombre + APaterno + AMaterno;
            }
        }
    }
}
