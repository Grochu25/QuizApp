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
    using System.Windows;

    static class QuizRepository
    {
        #region Queries

        private const string ALL_QUIZES = "SELECT * FROM quiz";
        private const string ADD_QUIZ = "INSERT INTO `quiz` (`name`) VALUES ";
        private const string DELETE_QUIZ = "DELETE FROM `quiz` WHERE id = ";
        private const string GET_QUIZ_WITH_ID = "SELECT * FROM `quiz` WHERE id = ";

        #endregion

        #region CRUD methods

        public static List<Quiz> GetAllQuizzes()
        {
            List<Quiz> quizzes = new List<Quiz>();

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

        public static Quiz GetQuestionWithId(int id)
        {
            Quiz quiz;
            using (var connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"{GET_QUIZ_WITH_ID} {id};", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                quiz = new Quiz(reader);
                connection.Close();
            }
            return quiz;
        }

        public static bool AddNewQuiz(Quiz quiz)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"{ADD_QUIZ} (\"{quiz.Name}\");", connection);
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

        public static bool UpdateQuiz(Quiz quiz)
        {
            bool status = false;
            using (SqliteConnection connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"UPDATE `quiz` SET name=\"{quiz.Name}\" WHERE id={quiz.Id}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if ((int)n > 0)
                    status = true;
                connection.Close();
            }
            return status;
        }

        public static bool DeleteQuiz(Quiz quiz)
        {
            bool status = false;
            using (SqliteConnection connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"{DELETE_QUIZ} {quiz.Id}", connection);
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
