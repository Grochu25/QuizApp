using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreator.DAL.Repositories
{
    using Entities;
    using Microsoft.Data.Sqlite;
    using System.Collections.ObjectModel;

    static class QuestionRepository
    {
        #region Queries
        private const string ALL_QUESTIONS = "SELECT * FROM questions WHERE quiz_id = ";
        private const string ADD_QUESTION = "INSERT INTO `questions` (`quiz_id`,`question`,`anwser_1`,`anwser_2`,`anwser_3`,`anwser_4`,`right_anwser`) VALUES ";
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
                SqliteCommand command = new SqliteCommand($"{ADD_QUESTION} ({quizNumber},{question.ToInsert()})", connection);
                connection.Open();
                var n = command.ExecuteScalar();
                if (n != null)
                {
                    status = true;
                    question.Id = sbyte.Parse(n.ToString());
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
                    $"UPDATE `quiestion` SET `question`=`{question.QuestionContent}`, `anwser_1`=`{question.Anwser1}`, `anwser_2`=`{question.Anwser2}`, `anwser_3`=`{question.Anwser3}`, `anwser_4`=`{question.Anwser4}`,`right_anwser`={question.Right_anwser}", connection);
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
            bool status = false;
            using (SqliteConnection connection = DBConnection.Instance.Connection)
            {
                SqliteCommand command = new SqliteCommand($"{DELETE_QUESTION} {question.Id})", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if((int)n >0)
                    status = true;
                connection.Close();
            }
            return status;
        }
        #endregion
    }
}
