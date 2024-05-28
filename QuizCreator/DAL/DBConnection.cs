using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.IO;
using System.Windows;

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

        public DBConnection()
        {
            _connection = new SqliteConnection($"Data Source=DAL/database.db");
        }
    }
}
