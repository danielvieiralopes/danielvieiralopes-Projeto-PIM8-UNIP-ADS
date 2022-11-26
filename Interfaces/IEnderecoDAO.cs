using ProjetoPimViii.Models;

namespace ProjetoPimViii.Interfaces
{
    public interface IEnderecoDAO
    {
        Endereco Consultar(int Id);
        int Inserir(Endereco endereco);
        bool Alterar(Endereco endereco, int id);
        bool Excluir(Endereco endereco, int id);
    }
}
