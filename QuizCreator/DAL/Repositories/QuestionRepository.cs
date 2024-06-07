namespace QuizCreator.DAL.Repositories
{
    using Entities;
    using Microsoft.Data.Sqlite;

    static class QuestionRepository
    {
        #region Queries
        private const string ALL_QUESTIONS = "SELECT * FROM `question` WHERE quiz_id = ";
        #endregion

        #region CRUD methods
        public static List<Question> GetAllQuestionsFromQuiz(int number)
        {
            List<Question> questions = new List<Question>();
            using (SqliteConnection connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"{ALL_QUESTIONS} {number};", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    questions.Add(new Question(reader));
                }
                connection.Close();
            }
            return questions;
        }

        #endregion
    }
}
