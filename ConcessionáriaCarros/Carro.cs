using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConcessionáriaCarros.Carro;

namespace ConcessionáriaCarros
{
    internal class Carro
    {
        public string Id { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Km { get; set; }
        public string Cor { get; set; }
        public StatusCarro Status { get; set; }
        public Cliente? Comprador { get; set; }
        public List<Manutencao>? Manutencoes { get; set; }

        public enum StatusCarro
        {
            Estoque,
            Vendido
        };
    }
}
