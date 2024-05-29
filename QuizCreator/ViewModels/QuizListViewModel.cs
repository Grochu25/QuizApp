using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreator.ViewModels
{
    using QuizCreator.DAL.Entities;
    using QuizCreator.Model;
    using QuizCreator.Views;
    using QuizCreator.ViewModels.BaseViewModelClasses;
    using QuizCreator.ViewModels.Navigation;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    class QuizListViewModel : BaseViewModelClasses.ViewModel
    {
        private Model _model = new Model();
        private ViewModelChanger _viewModelChanger;
        private ObservableCollection<Quiz>? _quizes = new ObservableCollection<Quiz>();

        public QuizListViewModel(ViewModelChanger viewModelChanger)
        {
            _viewModelChanger = viewModelChanger;
            _quizes = _model.Quizes;
        }

        public ObservableCollection<Quiz>? Quizes
        {
            get { return _quizes; }
            set { _quizes = value; }
        }

        private ICommand? _createQuiz = null;
        public ICommand CreateQuiz
        {
            get
            {
                if (_createQuiz == null)
                    _createQuiz = new RelayCommand(
                        arg => {
                            //TODO: jeden pomysł to to co wysłał Brociek
                            //Drugi pomysł to code behind
                            var quizCreationDialog = new QuizCreationDialog();
                            quizCreationDialog?.ShowDialog();

                            if(QuizCreationDialogViewModel.DialogResult)
                                _model.AddQuizWithName(QuizCreationDialogViewModel.NewQuizName);
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
                            {
                                Quiz quiz = _quizWithId((sbyte)arg);
                                _model.RemoveQuiz(quiz);
                            }
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

        private ICommand? _editQuiz = null;
        public ICommand EditQuiz
        {
            get
            {
                if (_editQuiz == null)
                    _editQuiz = new RelayCommand(
                        arg => {
                            Model.CurrentQuizId = (sbyte)arg;

                            _viewModelChanger.CurrentViewModel = new QuizEditViewModel(_viewModelChanger);
                        },
                        arg => true
                        );
                return _editQuiz;
            }
        }
    }
}
