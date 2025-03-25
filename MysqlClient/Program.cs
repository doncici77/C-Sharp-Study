using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace MysqlClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;database=membership;password=0321";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {

                // login 로그인
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand();

                mySqlCommand.Connection = mySqlConnection;
                // mySqlCommand.CommandText = "select * from users where user_id = @user_id and user_password = @user_password";
                mySqlCommand.CommandText = "select * from users limit 0, 10";
                mySqlCommand.Prepare();
                //mySqlCommand.Parameters.AddWithValue("@user_id", "htk008kr");
                //mySqlCommand.Parameters.AddWithValue("@user_password", "5678");

                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader["name"] + " " + dataReader["email"]);
                }

                // 회원가입
                /*MySqlCommand mySqlCommand2 = new MySqlCommand();
                mySqlCommand2.Connection = mySqlConnection;
                mySqlCommand2.CommandText = "insert into users (user_id, user_password, name, email) values (@user_id, @user_password, @name, @email)";
                mySqlCommand2.Prepare();
                mySqlCommand2.Parameters.AddWithValue("@user_id", "abc001");
                mySqlCommand2.Parameters.AddWithValue("@user_password", "1111");
                mySqlCommand2.Parameters.AddWithValue("@name", "홍길동");
                mySqlCommand2.Parameters.AddWithValue("@email", "abc001@naver.com");

                mySqlCommand2.ExecuteNonQuery();*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }
    }
}
