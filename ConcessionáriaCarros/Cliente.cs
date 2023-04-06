using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionáriaCarros
{
    internal class Cliente
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }

        public Cliente() { }
        
        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }
    }

}
