namespace QuizCreator.ViewModels
{
    using QuizCreator.DAL.Entities;
    using System.Collections.ObjectModel;
    using QuizCreator.Model;
    using QuizCreator.ViewModels.Navigation;
    using System.Windows.Input;
    using QuizCreator.ViewModels.BaseViewModelClasses;
    using System.Windows;

    class QuizSolveViewModel : BaseViewModelClasses.ViewModel
    {
        private Model _model = new Model();
        private ViewModelChanger _viewModelChanger;

        private ObservableCollection<Question> _questions = new ObservableCollection<Question>();
        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }
        public string QuizName { get; set; }
        public Question? SelectedQuestion { get; set; }
        public bool QuizStarted { get; set; } = false;
        public bool ifQuestionSelected { get => SelectedQuestion != null; }
        public Timer _timer;
        private TimeSpan _timeElapsed;
        public string QuizTimer => _timeElapsed.ToString(@"mm\:ss");

        public QuizSolveViewModel(ViewModelChanger viewModelChanger)
        {
            _viewModelChanger = viewModelChanger;
            QuizName = Model.CurrentQuiz().Name;
            _questions = _model.Questions;
            _timeElapsed = TimeSpan.Zero;
            _timer = new Timer(_updateTimer, null, Timeout.Infinite, 1000);
        }
        
        private void _updateTimer(object state)
        {
            _timeElapsed = _timeElapsed.Add(TimeSpan.FromSeconds(1));
            onPropertyChanged(nameof(QuizTimer));
        }

        private ICommand? _returnToMenu = null;
        public ICommand ReturnToMenu
        {
            get
            {
                if (_returnToMenu == null)
                    _returnToMenu = new RelayCommand(
                        (args) => { 
                            var result = (QuizStarted) ? MessageBox.Show("Wynik quizu oraz aktualny przebieg zostaną utracone.", "Czy na pewno chcesz wrócić do Menu?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) : MessageBoxResult.OK;
                            if (result == MessageBoxResult.OK)
                                _viewModelChanger.CurrentViewModel = new QuizListViewModel(_viewModelChanger); },
                        (args) => true);
                return _returnToMenu;
            }
        }

        private ICommand? _selectQuestion = null;

        public ICommand SelectQuestion
        {
            get
            {
                if (_selectQuestion == null)
                    _selectQuestion = new RelayCommand(
                        arg => {
                            SelectedQuestion = _model.QuestionWithId((sbyte)arg);
                            onPropertyChanged(nameof(ifQuestionSelected), nameof(SelectedQuestion));
                        },
                        arg => true);
                return _selectQuestion;
            }
        }

        private ICommand? _startQuiz = null;
        public ICommand StartQuiz
        {
            get
            {
                if (_startQuiz == null)
                    _startQuiz = new RelayCommand(
                        args =>
                        {
                            _timer.Change(0, 1000);
                            _timeElapsed = TimeSpan.Zero;
                            QuizStarted = true;
                            onPropertyChanged(nameof(QuizStarted));
                        },
                        args => !QuizStarted);
                return _startQuiz;
            }
        }

        private ICommand? _stopQuiz = null;
        public ICommand StopQuiz
        {
            get
            {
                if (_stopQuiz == null)
                    _stopQuiz = new RelayCommand(
                        args =>
                        {
                            _timer.Change(Timeout.Infinite, Timeout.Infinite);
                            QuizStarted = false;
                            onPropertyChanged(nameof(QuizStarted));

                            int score = 0;
                            foreach(var question in _questions)
                            {
                                if (question.Right_anwser == Question.ConvertTableToAnwsers(question.UserAnwserTable))
                                    score++;
                            }

                            MessageBox.Show($"Twój wynik to: {score}/{_questions.Count}\nCzas: {QuizTimer}", "Twój wynik");
                            ReturnToMenu.Execute(this);
                        },
                        args => QuizStarted);
                return _stopQuiz;
            }
        }
    }
}
