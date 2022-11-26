using ProjetoPimViii.DataBase;
using ProjetoPimViii.Interfaces;
using ProjetoPimViii.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPimViii.DAO
{
    public class EnderecoDAO : IEnderecoDAO
    {
        private static Conexao _conexao;

        public EnderecoDAO()
        {
            _conexao = new Conexao();
        }

        public bool Alterar(Endereco endereco, int id)
        {
            try
            {
                var query = _conexao.Query();
                query.CommandText = "UPDATE Endereco SET " +
                                    "logradouro=@logradouro," +
                                    "numero=@numero," +
                                    "cep=@cep, " +
                                    "bairro=@bairro," +
                                    "cidade=@cidade," +
                                    "estado=@estado " +
                                    "WHERE id=@id; ";



                query.Parameters.AddWithValue("@logradouro", endereco.logradouro);
                query.Parameters.AddWithValue("@numero", endereco.numero);
                query.Parameters.AddWithValue("@cep", endereco.cep);
                query.Parameters.AddWithValue("@bairro", endereco.bairro);
                query.Parameters.AddWithValue("@cidade", endereco.cidade);
                query.Parameters.AddWithValue("@estado", endereco.estado);
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

        public Endereco Consultar(int Id)
        {
            Endereco endereco = new Endereco();

            try
            {
                var query = _conexao.Query();
                query.CommandText = "SELECT * FROM Endereco WHERE id = @Id;";
                query.Parameters.AddWithValue("@id", Id);

                SqlDataReader reader = query.ExecuteReader();


                while (reader.Read())
                {
                    endereco.id = reader.GetInt32(0);
                    endereco.logradouro = reader.GetString(1);
                    endereco.numero = reader.GetInt32(2);
                    endereco.cep = reader.GetInt32(3);
                    endereco.bairro = reader.GetString(4);
                    endereco.cidade = reader.GetString(5);
                    endereco.estado = reader.GetString(6);
                }

                if (!reader.HasRows)
                    throw new Exception("Nenhum registro encontrado!");

                _conexao.Close();

                return endereco;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conexao.Query();
            }
        }

        public bool Excluir(Endereco endereco, int id)
        {
            var query = _conexao.Query();
            query.CommandText = "DELETE FROM Endereco WHERE id=@id; ";

            query.Parameters.AddWithValue("@id",id);

            var retorno = query.ExecuteNonQuery();

            if (retorno == 0)
            {
                throw new Exception("Registro não encontrado!");
            }

            _conexao.Close();

            return true;
        }

        public int Inserir(Endereco endereco)
        {
            try
            {
                var query = _conexao.Query();
                query.CommandText = "INSERT INTO Endereco(logradouro,numero,cep,bairro,cidade,estado) " +
                    "VALUES (@logradouro,@numero,@cep,@bairro,@cidade,@estado); " +
                    "SELECT SCOPE_IDENTITY();";

                int lastId;

                query.Parameters.AddWithValue("@logradouro", endereco.logradouro);
                query.Parameters.AddWithValue("@numero", endereco.numero);
                query.Parameters.AddWithValue("@cep", endereco.cep);
                query.Parameters.AddWithValue("@bairro", endereco.bairro);
                query.Parameters.AddWithValue("@cidade", endereco.cidade);
                query.Parameters.AddWithValue("@estado", endereco.estado);

                object Id = query.ExecuteScalar();
                lastId = Convert.ToInt32(Id);



                return lastId;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conexao.Close();
            }

            return -1;
        }
    }
}
