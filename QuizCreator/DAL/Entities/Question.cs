using Microsoft.Data.Sqlite;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizCreator.DAL.Entities
{
    class Question
    {
        public static bool[] ConvertAnwsersToTable(sbyte anwser)
        {
            bool[] table = new bool[4];
            if(anwser >=8) { table[0]  = true; anwser -= 8; }
            if(anwser >=4) { table[1]  = true; anwser -= 4; }
            if(anwser >=2) { table[2]  = true; anwser -= 2; }
            if(anwser >=1) { table[3]  = true; anwser -= 1; }
            return table;
        }

        public static sbyte ConvertTableToAnwsers(bool[] table)
        {
            sbyte anwsers = 0;
            anwsers += (table[0])? (sbyte)8 : (sbyte)0;
            anwsers += (table[1])? (sbyte)4 : (sbyte)0;
            anwsers += (table[2])? (sbyte)2 : (sbyte)0;
            anwsers += (table[3])? (sbyte)1 : (sbyte)0;
            return anwsers;
        }

        #region Properties
        public sbyte? Id { get; set; }
        public sbyte? QuizId { get; set; }
        public string QuestionContent { get; set; }
        public string Anwser1 { get; set; }
        public string Anwser2 { get; set; }
        public string Anwser3 { get; set; }
        public string Anwser4 { get; set; }
        public sbyte Right_anwser { get; set; }
        public bool[] AnwserTable { get; set; }
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
            AnwserTable = ConvertAnwsersToTable(Right_anwser);
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
            AnwserTable = ConvertAnwsersToTable(Right_anwser);
        }

        #endregion

        public string ToInsert()
        {
            return $"\"{QuestionContent}\",\"{Anwser1}\",\"{Anwser2}\",\"{Anwser3}\",\"{Anwser4}\",{ConvertTableToAnwsers(AnwserTable)}";
        }

        public override string ToString()
        {
            return $"\"{QuestionContent}\",\"{Anwser1}\",\"{Anwser2}\",\"{Anwser3}\",\"{Anwser4}\",{Right_anwser}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            Question q = (Question) obj;
            return q.Id == this.Id;
        }
    }
}
