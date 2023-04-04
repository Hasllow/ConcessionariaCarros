using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionáriaCarros
{
    internal class Cliente
    {
        private string _nome;
        private string _cpf;

        public Cliente(string nome, string cpf)
        {
            _nome = nome;
            _cpf = cpf;
        }

        public string Nome { get => _nome; set => _nome = value; }
        public string Cpf { get => _cpf; set => _cpf = value; }
    }
}
