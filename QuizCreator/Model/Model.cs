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
        private int _currentQuiz = -1;
        public ObservableCollection<Quiz> Quizes { get; set; } = new ObservableCollection<Quiz>();
        public List<Question>? Questions { get;  set; }

        public Model()
        {
            var quizes = QuizRepository.GetAllQuizzes();
            foreach (var quiz in quizes)
                Quizes.Add(quiz);
        }

        public void addQuiz(Quiz quiz)
        {
            Quizes.Add(quiz);
            QuizRepository.AddNewQuiz(quiz);
        }

        public void selectQuiz(Quiz quiz)
        {
            _currentQuiz = (int)quiz.Id;
            Questions = QuestionRepository.GetAllQuestionsFromQuiz(_currentQuiz);
        }
    }
}
