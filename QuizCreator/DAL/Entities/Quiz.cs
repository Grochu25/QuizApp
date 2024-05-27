using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;

namespace QuizCreator.DAL.Entities
{
    class Quiz
    {
        #region Properties
        public sbyte? Id { get; set; }
        public string Name { get; set; }
        #endregion

        #region Constructors
        public Quiz(SqliteDataReader reader)
        {
            Id = sbyte.Parse(reader["id"].ToString());
            Name = reader["name"].ToString();
        }

        public Quiz(sbyte? id, string name)
        {
            Id = id;
            Name = name;
        }
        #endregion

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
