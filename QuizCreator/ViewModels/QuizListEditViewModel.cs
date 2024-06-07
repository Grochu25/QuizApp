namespace QuizCreator.ViewModels
{
    using QuizCreator.DAL.Entities;
    using QuizCreator.Model;
    using QuizCreator.ViewModels.BaseViewModelClasses;
    using QuizCreator.ViewModels.Navigation;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    class QuizListEditViewModel : BaseViewModelClasses.ViewModel
    {
        private Model _model = new Model();
        private ViewModelChanger _viewModelChanger;
        private ObservableCollection<Quiz>? _quizes = new ObservableCollection<Quiz>();

        public QuizListEditViewModel(ViewModelChanger viewModelChanger)
        {
            _viewModelChanger = viewModelChanger;
            _quizes = _model.Quizes;
        }

        public ObservableCollection<Quiz>? Quizes
        {
            get { return _quizes; }
            set { _quizes = value; }
        }

        public string CreatedQuizName { get; set; } = "";

        private ICommand? _createQuiz = null;
        public ICommand CreateQuiz
        {
            get
            {
                if (_createQuiz == null)
                    _createQuiz = new RelayCommand(
                        arg =>
                        {
                            _model.AddQuizWithName(CreatedQuizName);
                            CreatedQuizName = "";
                        },
                        arg => CreatedQuizName.Length > 0
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
                                _model.RemoveQuizById((sbyte)arg);
                            }
                        },
                        arg => true
                        );
                return _deleteQuiz;
            }
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

        private ICommand? _anwserMode = null;
        public ICommand AnwserMode
        {
            get
            {
                if (_anwserMode == null)
                    _anwserMode = new RelayCommand(
                        arg => {
                            _viewModelChanger.CurrentViewModel = new QuizListViewModel(_viewModelChanger);
                        },
                        arg => true
                        );
                return _anwserMode;
            }
        }
    }
}
