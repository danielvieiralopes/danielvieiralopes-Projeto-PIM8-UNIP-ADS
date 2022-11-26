using Newtonsoft.Json.Linq;
using ProjetoPimViii.DAO;
using ProjetoPimViii.Models;


Pessoa pessoa = new Pessoa()
{
    nome = "Daniel",
    cpf = 41597826812,
    enderecos = new Endereco()
    {
        logradouro = "Av Joao de Matos",
        numero = 1,
        bairro = "Jardim das Flores",
        cep = 14840000,
        cidade = "Guariba",
        estado = "Sao Paulo"
    },
    telefones = new List<Telefone>()
        {
            new Telefone()
            {
                numero = 988548456,
                ddd = 16,
                tipo = TelefoneDAO.Celular

            },
            new Telefone()
            {
                numero = 954895625,
                ddd = 16,
                tipo = TelefoneDAO.Casa
            }
        }

};
Pessoa pessoa1 = new Pessoa()
{
    nome = "Jose",
    cpf = 41597826812,
    enderecos = new Endereco()
    {
        logradouro = "Av Oliveira",
        numero = 23,
        bairro = "Jardim das Flores",
        cep = 32145000,
        cidade = "Ribeirao Preto",
        estado = "Sao Paulo"
    },
    telefones = new List<Telefone>()
        {
            new Telefone()
            {
                numero = 923575687,
                ddd = 16,
                tipo = TelefoneDAO.Celular

            },
            new Telefone()
            {
                numero = 979856898,
                ddd = 16,
                tipo = TelefoneDAO.Casa
            }
        }

};

PessoaDAO pessoaDAO = new PessoaDAO();

//Inserir();
//ConsultarPorId();
//ConsultarPorCPF();
//Alterar();
Excluir();

void ConsultarPorId()
{
    pessoaDAO.ConsultaPorId(8);
}

void ConsultarPorCPF()
{
    pessoaDAO.Consultar(41597826812);

}

void Inserir()
{
    PessoaDAO pessoaDAO = new PessoaDAO();
    pessoaDAO.Inserir(pessoa);
    Console.WriteLine("Dados inseridos com sucesso!");
}

void Alterar()
{
    pessoa.nome = "Daniel Oliveira Jr";
    pessoaDAO.Alterar(pessoa, 8);
}

void Excluir()
{
    pessoaDAO.Excluir(pessoa, 12);
}