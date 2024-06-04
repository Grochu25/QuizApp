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

        public void RemoveQuizById(sbyte id)
        {
            RemoveQuiz(QuizWithId(id));
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

        public void RemoveQuestionById(sbyte id)
        {
            _questions.Remove(QuestionWithId(id));
        }
    }
}
