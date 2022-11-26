using ProjetoPimViii.DataBase;
using ProjetoPimViii.Interfaces;
using ProjetoPimViii.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPimViii.DAO
{
    public class TelefoneDAO : ITelefoneDAO
    {
        public const int Casa = 1;
        public const int Celular = 2;

        private static Conexao _conexao;

        public TelefoneDAO()
        {
            _conexao = new Conexao();
        }

        public bool Alterar(Telefone tel, int id)
        {
            try
            {
                var query = _conexao.Query();
                query.CommandText = "UPDATE Telefone" +
                                    "SET " +
                                    "numero=@numero," +
                                    "ddd=@ddd " +
                                    "WHERE id=@id";



                query.Parameters.AddWithValue("@numero", tel.numero);
                query.Parameters.AddWithValue("@ddd", tel.ddd);
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

        public Telefone Consultar(int id)
        {
            Telefone tel = new Telefone();
            try
            {
                var query = _conexao.Query();
                query.CommandText = "SELECT * FROM Telefone WHERE id = @Id";

                query.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    tel.id = reader.GetInt32(0);
                    tel.numero = reader.GetInt32(1);
                    tel.ddd = reader.GetInt32(2);
                }

                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }

            return tel;
        }

        public bool Excluir(Telefone tel, int id)
        {
            //ARMAZENA ID_TELEFONES
            var telefones = ListarTelefones(id);


            //DELETA PESSOA_TELEFONE
            _conexao = new Conexao();
            var queryPessoaTelefone = _conexao.Query();
            queryPessoaTelefone.CommandText = "DELETE FROM Pessoa_Telefone WHERE id_pessoa=@id; ";

            queryPessoaTelefone.Parameters.AddWithValue("@id", id);

            var result = queryPessoaTelefone.ExecuteNonQuery();

            _conexao.Close();

            if (result == 0)
            {
                throw new Exception("Registro não encontrado!");
            }

            //DELETA TELEFONES
            _conexao = new Conexao();
            foreach (var telefone in telefones)
            {
                var queryTelefone = _conexao.Query();
                queryTelefone.CommandText = "DELETE FROM Telefone WHERE id=@id; ";

                queryTelefone.Parameters.AddWithValue("@id", telefone.id);

                var retorno = queryTelefone.ExecuteNonQuery();

                if (retorno == 0)
                {
                    throw new Exception("Registro não encontrado!");
                }
            }

            _conexao.Close();

            return true;
        }

        public bool Inserir(Telefone tel, int pessoaId)
        {
            try
            {
                var query = _conexao.Query();
                query.CommandText = "INSERT INTO Telefone(numero,ddd, tipo) " +
                    "VALUES (@numero,@ddd,@tipo); " +
                    "SELECT SCOPE_IDENTITY();";

                query.Parameters.AddWithValue("@numero", tel.numero);
                query.Parameters.AddWithValue("@ddd", tel.ddd);
                query.Parameters.AddWithValue("@tipo", tel.tipo);

                object Id = query.ExecuteScalar();
                int telefoneId = Convert.ToInt32(Id);


                var query2 = _conexao.Query();
                query2.CommandText = "INSERT INTO Pessoa_telefone(id_pessoa,id_telefone) " +
                "VALUES (@id_pessoa,@id_telefone); " +
                "SELECT SCOPE_IDENTITY();";

                query2.Parameters.AddWithValue("@id_pessoa", pessoaId);
                query2.Parameters.AddWithValue("@id_telefone", telefoneId);
                query2.ExecuteNonQuery();
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

        public List<Telefone> ListarTelefones(int pessoaId)
        {
            List<Telefone> telefones = new List<Telefone>();

            try
            {


                var query = _conexao.Query();
                query.CommandText = "SELECT * FROM Pessoa_telefone where id_pessoa = @id_pessoa";

                query.Parameters.AddWithValue("@id_pessoa", pessoaId);
                query.ExecuteNonQuery();


                SqlDataReader reader = query.ExecuteReader();

                List<int> telefoneIds = new List<int>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        telefoneIds.Add(reader.GetInt32(1));
                    }
                }

                reader.Close();

                foreach (var telefone in telefoneIds)
                {
                    telefones.Add(Consultar(telefone));
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

            return telefones;
        }

        
    }
}
