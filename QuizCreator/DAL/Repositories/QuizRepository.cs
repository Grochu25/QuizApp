﻿namespace QuizCreator.DAL.Repositories
{
    using Entities;
    using Microsoft.Data.Sqlite;

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

        public static Quiz GetQuizWithId(int id)
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

        public static sbyte? AddNewQuizAndGetId(Quiz quiz)
        {
            using (var connection = DBConnection.Instance.Connection)
            {
                SqliteCommand insertCommand = new SqliteCommand($"{ADD_QUIZ} (\"{quiz.Name}\");", connection);
                connection.Open();
                insertCommand.ExecuteNonQuery();

                SqliteCommand checkLastIndexCommand = new SqliteCommand("select last_insert_rowid()", connection);
                var id = checkLastIndexCommand.ExecuteScalar();
                
                if (id != null)
                {
                    quiz.Id = sbyte.Parse(id.ToString());
                }
                connection.Close();
            }

            return quiz.Id;
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
