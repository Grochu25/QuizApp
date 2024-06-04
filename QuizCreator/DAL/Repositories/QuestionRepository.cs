namespace QuizCreator.DAL.Repositories
{
    using Entities;
    using Microsoft.Data.Sqlite;

    static class QuestionRepository
    {
        #region Queries
        private const string ALL_QUESTIONS = "SELECT * FROM `question` WHERE quiz_id = ";
        private const string ADD_QUESTION = "INSERT INTO `question` (`quiz_id`,`question`,`anwser_1`,`anwser_2`,`anwser_3`,`anwser_4`,`right_anwser`) VALUES ";
        private const string DELETE_QUESTION = "DELETE FROM `question` WHERE id = ";
        private const string GET_QUESTION_WITH_ID = "SELECT * FROM `question` WHERE id = ";
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

        public static Question GetQuestionWithId(int id)
        {
            Question quiestion;
            using (var connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"{GET_QUESTION_WITH_ID} {id};", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                quiestion = new Question(reader);
                connection.Close();
            }
            return quiestion;
        }

        public static bool AddNewQuestion(Question question, int quizNumber)
        {
            bool status = false;
            using(SqliteConnection connection = DBConnection.Instance.Connection) 
            {
                SqliteCommand insertCommand = new SqliteCommand($"{ADD_QUESTION} ({quizNumber},{question.ToInsert()})", connection);
                connection.Open();
                insertCommand.ExecuteNonQuery();

                SqliteCommand checkLastIndexCommand = new SqliteCommand("select last_insert_rowid()", connection);
                var id = checkLastIndexCommand.ExecuteScalar();

                if (id != null)
                {
                    question.Id = sbyte.Parse(id.ToString());
                }
                connection.Close();
            }
            return status;
        }

        public static bool UpdateQuestion(Question question)
        {
            bool status = false;
            using (SqliteConnection connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand(
                    $"UPDATE `question` SET `question`=\"{question.QuestionContent}\", `anwser_1`=\"{question.Anwser1}\", `anwser_2`=\"{question.Anwser2}\", `anwser_3`=\"{question.Anwser3}\", `anwser_4`=\"{question.Anwser4}\",`right_anwser`={Question.ConvertTableToAnwsers(question.AnwserTable)} WHERE id={question.Id}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if ((int)n > 0)
                    status = true;
                connection.Close();
            }
            return status;
        }

        public static bool DeleteQuestion(Question question)
        {
            return DeleteQuestionWithId(question.Id);
        }

        public static bool DeleteQuestionWithId(sbyte? id)
        {
            bool status = false;
            using (SqliteConnection connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"{DELETE_QUESTION} {id}", connection);
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
