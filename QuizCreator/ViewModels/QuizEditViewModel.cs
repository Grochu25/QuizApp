namespace QuizCreator.ViewModels
{
    using Model;
    using QuizCreator.DAL.Entities;
    using QuizCreator.ViewModels.BaseViewModelClasses;
    using QuizCreator.ViewModels.Navigation;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    class QuizEditViewModel : BaseViewModelClasses.ViewModel
    {
        public string QuizName { get; set; }
        private Model _model = new Model();
        private ViewModelChanger _viewModelChanger;
        private sbyte helpIndex = -1;
        private List<sbyte> _removedIds = new List<sbyte>();
        private bool _changesMade = false;

        private ObservableCollection<Question> _questions = new ObservableCollection<Question>();
        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }

        public Question? SelectedQuestion { get; set; }

        public QuizEditViewModel(ViewModelChanger viewModelChanger)
        {
            _viewModelChanger = viewModelChanger;
            QuizName = Model.CurrentQuiz().Name;
            _questions = _model.Questions;
        }

        public bool ifQuestionSelected
        {
            get => SelectedQuestion != null; 
        }

        private ICommand? _selectQuestion = null;

        public ICommand SelectQuestion
        {
            get
            {
                if (_selectQuestion == null)
                    _selectQuestion = new RelayCommand(
                        arg => { 
                            SelectedQuestion = _model.QuestionWithId((sbyte)arg); onPropertyChanged(nameof(ifQuestionSelected), nameof(SelectedQuestion));
                            _changesMade = true;
                        },
                        arg => true);
                return _selectQuestion;
            }
        }

        private ICommand? _returnToMenu = null;

        public ICommand ReturnToMenu
        {
            get
            {
                if (_returnToMenu == null)
                    _returnToMenu = new RelayCommand(
                        arg => { 
                            var result = (!_changesMade) ? MessageBoxResult.OK : MessageBox.Show("Wszelkie niezapisane zmiany zostaną utracone.","Czy na pewno chcesz wrócić do Menu?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            if(result == MessageBoxResult.OK )
                                _viewModelChanger.CurrentViewModel = new QuizListEditViewModel(_viewModelChanger); },
                        arg => true);
                return _returnToMenu;
            }
        }

        private ICommand? _addNewQuestion = null;
        public ICommand AddNewQuestion
        {
            get
            {
                if (_addNewQuestion == null)
                    _addNewQuestion = new RelayCommand(
                        arg => { 
                            Questions.Add(new Question(helpIndex--, Model.CurrentQuizId,"","","","","",0));
                            _changesMade = true;
                        },
                        arg => true);
                return _addNewQuestion;
            }
        }

        private ICommand? _removeQuestion = null;
        public ICommand RemoveQuestion
        {
            get
            {
                if (_removeQuestion == null)
                    _removeQuestion = new RelayCommand(
                        arg => {
                            _model.RemoveQuestionById((sbyte)arg);
                            _removedIds.Add((sbyte)arg);
                            _changesMade = true;
                        },
                        arg => true);
                return _removeQuestion;
            }
        }

        private ICommand? _saveChanges = null;
        public ICommand SaveChanges
        {
            get
            {
                if (_saveChanges == null)
                    _saveChanges = new RelayCommand(
                        arg => {
                            
                            _model.SaveChangesInCurrentQuiz();

                            _model.DeleteQuestionsWithId(_removedIds);

                            _model.ModifyCurrentQuizName(QuizName);
                            MessageBox.Show("Zapisano modyfikacje", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                            _changesMade = false;
                        },
                        arg => true);
                return _saveChanges;
            }
        }
    }
}
