using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Aviso_Convenio
{
    class conn
    {
        public MySqlConnection connection;
        private string servidor;
        private string usuario;
        private string senha;
        private string banco;
        public void Iniciar()
        {
            servidor = "192.168.0.4";
            usuario = "root";
            senha = "infohisc";
            banco = "operadora";
            string conexao;
            conexao = "SERVER=" + servidor + ";" + "DATABASE=" +
        banco + ";" + "UID=" + usuario + ";" + "PASSWORD=" + senha + ";";
            connection = new MySqlConnection(conexao);
            
        }
        public bool AbrirConexao()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:

                        return false;
                        break;

                    case 1045:
                        return false;
                        break;
                }
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
               
                return false;
            }
        }

        //Insert statement
        public void  Insert(string nome,string login, string senha)
        {
            string query = "INSERT INTO tbl_operadora (Descricao, Login, Senha) VALUES('"+nome+"','"+login+"','"+senha+"')";

            //open connection
            this.Iniciar();
            if (this.AbrirConexao() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string selecao,string senha)
        {
            string query = "UPDATE tbl_operadora SET Senha='"+senha+"' WHERE id_operadora="+selecao;

            //Open connection
            this.Iniciar();
            if (this.AbrirConexao() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

      



    }
}
