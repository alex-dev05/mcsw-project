using MySql.Data.MySqlClient;
namespace HelloWorld
{
    public class Program
    {
        public static string ConnectionString = @"server=localhost;userid=root;password=LMTpass2019*;database=sakila;";

        public static MySqlConnection OpenConnection()
        {

            MySqlConnection conn = new MySqlConnection(ConnectionString);
            conn.Open();

            // print the status of connection
            Console.WriteLine($"MySQL version : {conn.ServerVersion}");
            return conn;
        }

        public static void SelectFromTable(string table)
        {
            MySqlConnection localConn = OpenConnection();
            var statement = "Select * from " + table + ";";
            var command = new MySqlCommand(statement, localConn);
            using MySqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
                        rdr.GetString(2));
            }
        }
        static void Main()
        {
            SelectFromTable("language");
            Console.WriteLine("Hello World!");
        }
    }
}