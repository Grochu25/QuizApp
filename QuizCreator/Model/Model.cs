using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreator.Model
{
    using QuizCreator.DAL.Entities;
    using QuizCreator.DAL.Repositories;
    using System.Windows;
    using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

    class Model
    {
        private static sbyte _currentQuizId = -1;
        public static sbyte CurrentQuizId {
            get
            {
                return _currentQuizId;
            }

            set
            {
                _currentQuizId = value;
            } 
        }

        public static Quiz CurrentQuiz()
        {
            return QuizRepository.GetQuizWithId(_currentQuizId);
        }

        private ObservableCollection<Quiz> _quiz = new ObservableCollection<Quiz>();
        public ObservableCollection<Quiz> Quizes
        {
            get
            {
                if(_quiz.Count == 0)
                {
                    var quizes = QuizRepository.GetAllQuizzes();
                    foreach (var quiz in quizes)
                        _quiz.Add(quiz);
                }
                return _quiz;
            }
        }

        private ObservableCollection<Question> _questions = new ObservableCollection<Question>();
        public ObservableCollection<Question> Questions 
        { 
            get
            {
                if(_questions.Count == 0 && _currentQuizId >= 0)
                {
                    var questions = QuestionRepository.GetAllQuestionsFromQuiz(_currentQuizId);
                    foreach (var question in questions)
                        _questions.Add(question);
                }
                return _questions;
            }
        }

        public Model(){}

        public void AddQuizWithName(string name)
        {
            Quiz quiz = new Quiz(null, name);
            AddQuiz(quiz);
        }
        public void AddQuiz(Quiz quiz)
        {
            QuizRepository.AddNewQuizAndGetId(quiz);
            if(quiz.Id >= 0)
                Quizes.Add(quiz);
        }

        public void ModifyCurrentQuizName(string name)
        {
            QuizRepository.UpdateQuiz(new Quiz(CurrentQuizId, name));
        }

        public void RemoveQuiz(Quiz quiz) 
        {
            Quizes.Remove(quiz);
            QuizRepository.DeleteQuiz(quiz);
        }


        public void AddQuestionToCurrentQuiz(Question question)
        {
            QuestionRepository.AddNewQuestion(question, CurrentQuizId);
        }


        public void ModifyQuestion(Question question)
        {
            QuestionRepository.UpdateQuestion(question);
        }

        public void SaveChangesInCurrentQuiz()
        {
            foreach (var question in Questions)
            {
                if (question.Id >= 0)
                {
                    ModifyQuestion(question);
                }
                else
                {
                    question.Id = null;
                    if (!question.QuestionContent.Equals(""))
                        AddQuestionToCurrentQuiz(question);
                }
            }
        }

        public void DeleteQuestionsWithId(List<sbyte> Ids)
        {
            foreach (var id in Ids)
            {
                QuestionRepository.DeleteQuestionWithId(id);
            }
        }
    }
}
