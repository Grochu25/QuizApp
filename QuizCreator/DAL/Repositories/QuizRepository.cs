namespace QuizCreator.DAL.Repositories
{
    using Entities;
    using Microsoft.Data.Sqlite;

    static class QuizRepository
    {
        #region Queries

        private const string ALL_QUIZES = "SELECT * FROM quiz";
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
        #endregion
    }
}
