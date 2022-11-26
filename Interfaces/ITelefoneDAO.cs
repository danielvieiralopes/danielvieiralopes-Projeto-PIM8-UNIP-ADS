using ProjetoPimViii.Models;

namespace ProjetoPimViii.Interfaces
{
    public interface ITelefoneDAO
    {
        Telefone Consultar(int id);
        bool Inserir(Telefone tel, int pessoaId);
        bool Alterar(Telefone tel, int id);
        bool Excluir(Telefone tel, int id);
        List<Telefone> ListarTelefones(int pessoaId);
    }
}
