using Dapper;
using System.Data;
using System.Data.SQLite;

namespace CSharp {
    class DBConnector {
        string dbPath = "MyDatabase.sqlite";

        public void CreateDBFile() {
            SQLiteConnection.CreateFile(dbPath);
        }

        public SQLiteConnection getConnection() {
            string connectionString = $"Data Source={dbPath};Version=3;";
            return new SQLiteConnection(connectionString);
        }

        public void CreateTable() {
            SQLiteConnection conn = getConnection();
            conn.Open();
            string sql = "DROP TABLE IF EXISTS customers; CREATE TABLE customers (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, email TEXT, age INTEGER)";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void InsertData() {
            SQLiteConnection conn = getConnection();
            conn.Open();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = @"INSERT INTO customers (name, email, age) VALUES ('Park','mun03922@gmail.com', 31);";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = @"INSERT INTO customers (name, email, age) VALUES ('Kim','kim1234@gmail.com', 33);";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = @"INSERT INTO customers (name, email, age) VALUES ('Song','song4321@gmail.com', 28);";
            sqlite_cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Tuple<int, string, string, int>> SelectData() {
            SQLiteConnection conn = getConnection();
            conn.Open();
            List<Tuple<int, string, string, int>> resultList = new List<Tuple<int, string, string, int>>();
            string sql = "SELECT * FROM customers";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                resultList.Add(new Tuple<int, string, string, int>(
                    reader.GetInt16(0), // id
                    reader.GetString(1), // name
                    reader.GetString(2), // email
                    reader.GetInt16(3))); // age
            }

            conn.Close();
            return resultList;
        }

        public List<Customer> SelectDataWithDapper() {
            using (IDbConnection cnn = getConnection()) {
                string pstmt = "select * from customers";
                var output = cnn.Query<Customer>(pstmt, new DynamicParameters());
                return output.ToList();
            }
        }
    }


    public class Customer {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
