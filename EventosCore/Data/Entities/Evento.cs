using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventosCore.Data.Entities
{
    public class Evento
    {
        public int Id { get; set; }
        public int AeropuertosId { get; set; }
        public string DescripcionCorta { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEvento { get; set; }
        public string ImgLocal { get; set; }
        public string ImgVisitante { get; set; }
        public long Precio { get; set; }
        public virtual Aeropuertos Aeropuertos { get; set; }

    }
}
