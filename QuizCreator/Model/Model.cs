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
            return QuizRepository.GetQuestionWithId(_currentQuizId);
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
                if(_questions == null && _currentQuizId >= 0)
                {
                    var questions = QuestionRepository.GetAllQuestionsFromQuiz(_currentQuizId);
                    foreach (var question in questions)
                        _questions.Add(question);
                }
                return _questions;
            }
        }

        public Model()
        {
            
        }

        public void AddQuizWithName(string name)
        {
            Quiz quiz = new Quiz(null, name);
            AddQuiz(quiz);
        }
        public void AddQuiz(Quiz quiz)
        {
            Quizes.Add(quiz);
            QuizRepository.AddNewQuiz(quiz);
        }

        public void RemoveQuiz(Quiz quiz) 
        {
            Quizes.Remove(quiz);
            QuizRepository.DeleteQuiz(quiz);
        }

    }
}
