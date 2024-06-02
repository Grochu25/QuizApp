using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreator.DAL.Entities
{
    class Question
    {
        #region Properties
        public sbyte? Id { get; set; }
        public sbyte? QuizId { get; set; }
        public string QuestionContent { get; set; }
        public string Anwser1 { get; set; }
        public string Anwser2 { get; set; }
        public string Anwser3 { get; set; }
        public string Anwser4 { get; set; }
        public sbyte Right_anwser { get; set; }
        #endregion

        #region Constructors
        public Question(SqliteDataReader reader)
        {
            Id = sbyte.Parse(reader["id"].ToString());
            QuizId = sbyte.Parse(reader["quiz_id"].ToString());
            QuestionContent = reader["question"].ToString();
            Anwser1 = reader["anwser_1"].ToString();
            Anwser2 = reader["anwser_2"].ToString();
            Anwser3 = reader["anwser_3"].ToString();
            Anwser4 = reader["anwser_4"].ToString();
            Right_anwser = sbyte.Parse(reader["right_anwser"].ToString());
        }

        public Question(sbyte? id, sbyte? quizId, string questionContent, string anwser1, string anwser2, string anwser3, string anwser4, sbyte right_anwser)
        {
            Id = id;
            QuizId = quizId;
            QuestionContent = questionContent;
            Anwser1 = anwser1;
            Anwser2 = anwser2;
            Anwser3 = anwser3;
            Anwser4 = anwser4;
            Right_anwser = right_anwser;
        }


        #endregion

        public string ToInsert()
        {
            return $"\"{QuestionContent}\",\"{Anwser1}\",\"{Anwser2}\",\"{Anwser3}\",\"{Anwser4}\",{Right_anwser}";
        }
    }
}
