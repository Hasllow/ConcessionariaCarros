using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionáriaCarros
{
    internal class Manutencao
    {
        public string? Oficina { get; set; }
        public string? Data { get; set; }
        public List<string>? Items { get; set; }    

        public Manutencao() { }

        public Manutencao(string oficina, string data, List<string> items)
        {
            Oficina = oficina;
            Data = data;
            Items = items;
        }
    }
}
