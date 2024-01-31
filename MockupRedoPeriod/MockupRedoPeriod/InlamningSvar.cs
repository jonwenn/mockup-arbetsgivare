using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MockupRedoPeriod
{
    internal class InlamningSvar
    {
        [JsonPropertyName("inlamningId")]
        public int InlamningId { get; set; }
    }
}
