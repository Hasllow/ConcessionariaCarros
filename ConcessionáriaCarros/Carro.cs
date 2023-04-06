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
        public string? Id { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public int? Km { get; set; }
        public string? Cor { get; set; }
        public StatusCarro Status { get; set; }
        public Cliente Comprador { get; set; }
        public List<Manutencao> Manutencoes { get; set; }

        public enum StatusCarro
        {
            Estoque,
            Vendido
        };

        public Carro(string? id, string? modelo, string? marca, int? km, string? cor)
        {
            Id = id;
            Modelo = modelo;
            Marca = marca;
            Km = km;
            Cor = cor;
            Comprador = new();
            Status = StatusCarro.Estoque;
            Manutencoes = new();
        }

        public Carro()
        {
            Comprador = new();
            Status = StatusCarro.Estoque;
            Manutencoes = new();
        }

        public override string ToString()
        {
            string str =
                $"\tModelo: {Modelo}"
                + $"\n\tMarca: {Marca}"
                + $"\n\tQuilometragem: {Km}"
                + $"\n\tCor: {Cor}"
                + $"\n\tStatus: {Status}";

            if (Status == Carro.StatusCarro.Vendido)
            {
                str +=
                    "\n\tCliente: "
                    + $"\n\t\tNome: {Comprador?.Nome}"
                    + $"\n\t\tCPF: {Comprador?.Cpf}";
            }

            if (Manutencoes.Count > 0)
            {
                str += "\n\tManutenções:";
                foreach (var item in Manutencoes)
                {
                    str +=
                        $"\n\t\tData: {item.Data}"
                        + $"\n\t\tOficina: {item.Oficina}"
                        + $"\n\t\tPeças: {string.Join(", ", item.Items)}";
                }
            }

            str += "\n";
            return str;
        }
    }
}
