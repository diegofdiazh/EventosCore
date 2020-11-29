using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventosCore.Models.Responses
{
    public class ResponceEventosCiudades
    {
        public int tourEventId { get; set; }
        public string shortDescription { get; set; }
        public string description { get; set; }
        public DateTime eventDate { get; set; }
        public string eventCity { get; set; }
        public string imgLocal { get; set; }
        public string imgVisitante { get; set; }
        public long value { get; set; }
        public string concatenated { get; set; }

    }
}
