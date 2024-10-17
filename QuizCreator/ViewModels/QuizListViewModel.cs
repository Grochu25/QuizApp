namespace QuizCreator.ViewModels
{
    using QuizCreator.DAL.Entities;
    using QuizCreator.ViewModels.Navigation;
    using System.Collections.ObjectModel;
    using QuizCreator.Model;
    using QuizCreator.ViewModels.BaseViewModelClasses;
    using System.Windows.Input;

    class QuizListViewModel : BaseViewModelClasses.ViewModel
    {
        private ViewModelChanger _viewModelChanger;
        private Model _model = new Model();
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

        private ICommand? _editMode = null;
        public ICommand EditMode
        {
            get
            {
                if (_editMode == null)
                    _editMode = new RelayCommand(
                        arg => {
                            _viewModelChanger.CurrentViewModel = new QuizEditListViewModel(_viewModelChanger);
                        },
                        arg => true
                        );
                return _editMode;
            }
        }

        private ICommand? _takeQuiz = null;

        public ICommand? TakeQuiz
        {
            get
            {
                if (_takeQuiz == null)
                    _takeQuiz = new RelayCommand(
                        arg => {
                            Model.CurrentQuizId = (sbyte)arg;
                            _viewModelChanger.CurrentViewModel = new QuizSolveViewModel(_viewModelChanger);
                        },
                        arg => true
                        );
                return _takeQuiz;
            }
        }
    }
}
