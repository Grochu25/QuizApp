using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreator.ViewModel
{
    using QuizCreator.DAL.Entities;
    using QuizCreator.Model;
    using QuizCreator.ViewModel.Base_Classes;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    class QuizListViewModel
    {
        private Model _model = new Model();

        public ObservableCollection<Quiz>? quizes = new ObservableCollection<Quiz>();

        public QuizListViewModel()
        {
            quizes = _model.Quizes;
        }

        public ObservableCollection<Quiz>? Quizes
        {
            get { return quizes; }
            set { quizes = value; }
        }

        private ICommand? _createQuiz = null;

        public ICommand CreateQuiz
        {
            get
            {
                if (_createQuiz == null)
                    _createQuiz = new RelayCommand(
                        arg => {
                            //zmiana okna
                        },
                        arg => true
                        );
                return _createQuiz;
            }
        }

        private ICommand? _deleteQuiz = null;
        public ICommand DeleteQuiz
        {
            get
            {
                if (_deleteQuiz == null)
                    _deleteQuiz = new RelayCommand(
                        arg => {
                            var result = MessageBox.Show("Czy na pewno usunąć quiz?", "Jesteś pewien?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                                Quizes.Remove(_quizWithId((sbyte)arg));
                        },
                        arg => true
                        );
                return _deleteQuiz;
            }
        }

        private Quiz? _quizWithId(sbyte id)
        {
            for (int i = 0; i < Quizes.Count(); i++)
            {
                if (Quizes.ElementAt(i).Id == id)
                    return Quizes.ElementAt(i);
            }
            return null;
        }
    }
}
