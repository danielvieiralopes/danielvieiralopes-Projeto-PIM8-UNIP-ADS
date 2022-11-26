using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPimViii.Models
{
    public class Telefone
    {
        public int id { get; set; }
        public int numero { get; set; }
        public int ddd { get; set; }
        public int tipo { get; set; }
        public TipoTelefone tipos { get; set; }
    }
}
