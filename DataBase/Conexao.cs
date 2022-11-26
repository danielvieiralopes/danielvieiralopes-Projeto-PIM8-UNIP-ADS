using System.Data;
using System.Data.SqlClient;

namespace ProjetoPimViii.DataBase
{
    public class Conexao
    {
        private static string connectionString = "Server=localhost,1433;Database=unip;User ID=sa;Password=Senha@2022";

        private SqlConnection connection;
        private SqlCommand command;

        public Conexao()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SqlCommand Query()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                return command;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Close()
        {
            connection.Close();
        }


    }
}
