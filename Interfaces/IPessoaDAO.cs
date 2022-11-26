using ProjetoPimViii.Models;

namespace ProjetoPimViii.Interfaces
{
    public interface IPessoaDAO
    {
        Pessoa ConsultaPorId(int id);
        Pessoa Consultar(long cpf);
        bool Inserir(Pessoa p);
        bool Alterar(Pessoa p, int id);
        bool Excluir(Pessoa p, int id);
    }
}
