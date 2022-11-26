using System.Data.SqlClient;
using ProjetoPimViii.DataBase;
using ProjetoPimViii.Interfaces;
using ProjetoPimViii.Models;

namespace ProjetoPimViii.DAO
{
    public class PessoaDAO : IPessoaDAO
    {
        private static Conexao _conexao;

        public PessoaDAO()
        {
            _conexao = new Conexao();
        }

        public bool Alterar(Pessoa p, int id)
        {

            try
            {
                var query = _conexao.Query();
                query.CommandText = "UPDATE Pessoa " +
                                    "SET nome=@nome," +
                                    "cpf=@cpf " +
                                    "WHERE id = @id";


                query.Parameters.AddWithValue("@nome", p.nome);
                query.Parameters.AddWithValue("@cpf", p.cpf);

                query.Parameters.AddWithValue("@id", id);

                query.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conexao.Close();
            }
        }

        public Pessoa ConsultaPorId(int id)
        {
            Pessoa pessoa = new Pessoa();

            var telefones = new TelefoneDAO().ListarTelefones(id);

            try
            {
                var query = _conexao.Query();
                query.CommandText = "SELECT * FROM PESSOA WHERE id = @id;";
                query.Parameters.AddWithValue("@id", id);
            
                SqlDataReader reader = query.ExecuteReader();


                while (reader.Read())
                {
                    pessoa.Id = reader.GetInt32(0);
                    pessoa.nome = reader.GetString(1);
                    pessoa.cpf = reader.GetInt64(2);
                    pessoa.endereco = Convert.ToInt32(reader.GetInt32(3));
                    pessoa.telefones = telefones;
                }

                if (!reader.HasRows)
                    throw new Exception("Nenhum registro encontrado!");

                _conexao.Close();

                return pessoa; 
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conexao.Query();
            }
            return new Pessoa();
        }

        public Pessoa Consultar(long cpf)
        {
            
            
            Pessoa pessoa = new Pessoa();

            var queryTelefone = _conexao.Query();
            queryTelefone.CommandText = "SELECT * FROM Pessoa WHERE cpf = @cpf";
            queryTelefone.Parameters.AddWithValue("@cpf", cpf);
            queryTelefone.ExecuteNonQuery();

            SqlDataReader readerTelefone = queryTelefone.ExecuteReader();

            while (readerTelefone.Read())
            {
                pessoa.Id = readerTelefone.GetInt32(0);
            }

            readerTelefone.Close();

            var telefones = new TelefoneDAO().ListarTelefones(pessoa.Id);

           

            try
            {
                var query = _conexao.Query();
                query.CommandText = "SELECT * FROM Pessoa WHERE cpf = @cpf";
                query.Parameters.AddWithValue("@cpf", cpf);
                query.ExecuteNonQuery();

                SqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    pessoa.Id = reader.GetInt32(0);
                    pessoa.nome = reader.GetString(1);
                    pessoa.cpf = reader.GetInt64(2);
                    pessoa.endereco = Convert.ToInt32(reader.GetInt32(3));
                    pessoa.telefones = telefones;
                }


                return pessoa;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Excluir(Pessoa p, int id)
        {
            var tel = new Telefone();
            var telefone = new TelefoneDAO();
            telefone.Excluir(tel,id);

            //Armazena cod endereco
            var pessoa = new PessoaDAO();
            var resultado = pessoa.ConsultaPorId(id);
            var enderecoId = resultado.Id;


            try
            {
                //Exclui Pessoa
                _conexao = new Conexao();
                var query = _conexao.Query();
                query.CommandText = "DELETE FROM Pessoa WHERE id=@id; "; 

                query.Parameters.AddWithValue("@id", id);

                var result = query.ExecuteNonQuery();

                if(result == 0)
                {
                    throw new Exception("Registro não encontrado!");
                }

                _conexao.Close();

                //Exclui Endereco
                var end = new Endereco();
                var endereco = new EnderecoDAO();
                endereco.Excluir(end,enderecoId);

                

                return true;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conexao.Close();
            }
        }

        public bool Inserir(Pessoa p)
        {
            try
            {
                var query = _conexao.Query();
                query.CommandText = "INSERT INTO Pessoa(nome,cpf,endereco) " +
                    "VALUES (@nome,@cpf,@endereco); " +
                    "SELECT SCOPE_IDENTITY();";

                if(p.enderecos.id == 0 && p.enderecos.logradouro != null)
                {
                    p.enderecos.id = new EnderecoDAO().Inserir(p.enderecos);
                }

                query.Parameters.AddWithValue("@nome", p.nome);
                query.Parameters.AddWithValue("@cpf", p.cpf);
                query.Parameters.AddWithValue("@endereco", p.enderecos.id);

                object Id = query.ExecuteScalar();
                int pessoaId = Convert.ToInt32(Id);

                foreach (var telefone in p.telefones)
                {
                    new TelefoneDAO().Inserir(telefone, pessoaId);
                }


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conexao.Close();
            }
      

            return true;
        }
    }
}
