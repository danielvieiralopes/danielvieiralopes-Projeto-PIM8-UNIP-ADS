using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPimViii.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public long cpf { get; set; }
        public int endereco { get; set; }
        public Endereco enderecos { get; set; }
        public List<Telefone> telefones { get; set; }
    }
}
