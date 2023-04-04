using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionáriaCarros
{
    internal class Manutencao
    {
        private string _oficina;
        private string _data;
        private List<string> _items;

        public Manutencao(string oficina, string data, List<string> items)
        {
            Oficina = oficina;
            Data = data;
            Items = items;
        }

        public string Oficina { get => _oficina; set => _oficina = value; }
        public string Data { get => _data; set => _data = value; }
        public List<string> Items { get => _items; set => _items = value; }
    }
}
