using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreator.DAL.Repositories
{
    using Entities;
    using Microsoft.Data.Sqlite;
    using MySql.Data.MySqlClient;
    using System.CodeDom;

    static class QuizRepository
    {
        #region Queries

        private const string ALL_QUIZES = "SELECT * FROM quiz";
        private const string ADD_QUIZ = "INSERT INTO `quiz`(`name`) VALUES ";
        private const string DELETE_QUIZ = "DELETE FROM `quiz` WHERE id = ";

        #endregion

        #region CRUD methods

        public static List<Quiz> GetAllQuizzes()
        {
            List<Quiz> quizzes = new List<Quiz>();

            //TODO: Zmienić to na instancję singletonu
            using (var connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand(ALL_QUIZES, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    quizzes.Add(new Quiz(reader));
                }
                connection.Close();
            }
            return quizzes;
        }

        public static bool AddNewQuiz(Quiz quiz)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"{ADD_QUIZ} ({quiz.Name});", connection);
                connection.Open();
                var n = command.ExecuteScalar();
                if (n != null)
                {
                    state = true;
                    quiz.Id = sbyte.Parse(n.ToString());
                }
                connection.Close();
            }
            return state;
        }

        public static bool DeleteQuestion(Quiz quiz)
        {
            bool status = false;
            using (SqliteConnection connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"{DELETE_QUIZ} {quiz.Id})", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if ((int)n > 0)
                    status = true;
                connection.Close();
            }
            return status;
        }

        #endregion
    }
}
