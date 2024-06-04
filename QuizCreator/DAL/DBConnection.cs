using Microsoft.Data.Sqlite;

namespace QuizCreator.DAL
{
    class DBConnection
    {

        private static DBConnection _instance = null;
        public static DBConnection Instance
        {
            get
            {
                if(_instance == null) 
                    _instance = new DBConnection();
                return _instance;
            }
        }

        private SqliteConnection _connection;
        public SqliteConnection Connection => _connection;

        private DBConnection()
        {
            _connection = new SqliteConnection(@"Data Source=DAL/database.db");
        }
    }
}
