using System.Collections.ObjectModel;

namespace QuizCreator.Model
{
    using QuizCreator.DAL.Entities;
    using QuizCreator.DAL.Repositories;

    class Model
    {
        public static sbyte CurrentQuizId { get; set; } = -1;

        public static Quiz CurrentQuiz()
        {
            return QuizRepository.GetQuizWithId(CurrentQuizId);
        }

        private ObservableCollection<Quiz> _quizes = new ObservableCollection<Quiz>();
        public ObservableCollection<Quiz> Quizes
        {
            get
            {
                if(_quizes.Count == 0)
                {
                    var quizes = QuizRepository.GetAllQuizzes();
                    foreach (var quiz in quizes)
                        _quizes.Add(quiz);
                }
                return _quizes;
            }
        }

        private ObservableCollection<Question> _questions = new ObservableCollection<Question>();
        public ObservableCollection<Question> Questions 
        { 
            get
            {
                if(_questions.Count == 0 && CurrentQuizId >= 0)
                {
                    var questions = QuestionRepository.GetAllQuestionsFromQuiz(CurrentQuizId);
                    foreach (var question in questions)
                        _questions.Add(question);
                }
                return _questions;
            }
        }

        public Question? QuestionWithId(sbyte id)
        {
            foreach (var question in _questions)
            {
                if (question.Id == id)
                    return question;
            }
            return null;
        }

        public Quiz? QuizWithId(sbyte id)
        {
            foreach (var quiz in _quizes)
            {
                if (quiz.Id == id)
                    return quiz;
            }
            return null;
        }

    }
}
