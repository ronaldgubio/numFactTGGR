using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numFact.dto
{
    class RhPersona
    {
        public String PersonaId { get; set; }

        public String PersonaNombre { get; set; }

        public String PersonaTipo { get; set; }

        public override string ToString()
        {
            return PersonaNombre + " (" + PersonaId + ")";
        }
    }
}
