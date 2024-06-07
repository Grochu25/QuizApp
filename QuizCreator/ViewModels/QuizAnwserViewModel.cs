namespace QuizCreator.ViewModels
{
    using Model;
    using QuizCreator.DAL.Entities;
    using QuizCreator.ViewModels.BaseViewModelClasses;
    using QuizCreator.ViewModels.Navigation;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    class QuizAnwserViewModel : BaseViewModelClasses.ViewModel
    {
        public string QuizName { get; set; }
        private Model _model = new Model();
        private ViewModelChanger _viewModelChanger;
        private sbyte helpIndex = -1;
        private List<sbyte> _removedIds = new List<sbyte>();
        private bool _changesMade = false;
        private TimeSpan _timeElapsed;
        private Timer _timer;
        private bool _isStarted;

        private ObservableCollection<Question> _questions = new ObservableCollection<Question>();
        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set { _questions = value;
                onPropertyChanged(nameof(Questions));
            }
        }

        public Question? SelectedQuestion { get; set; }

        public QuizAnwserViewModel(ViewModelChanger viewModelChanger)
        {
            _isStarted = false;
            _viewModelChanger = viewModelChanger;
            QuizName = Model.CurrentQuiz().Name; 
            _questions = _model.Questions;
            _timeElapsed = TimeSpan.Zero;
            _timer = new Timer(UpdateTimer, null, Timeout.Infinite, 1000);

        }

        public bool ifQuestionSelected
        {
            get => SelectedQuestion != null; 
        }

        private ICommand? _selectQuestion = null;

        public ICommand SelectQuestion
        {
            get {
                if (_selectQuestion == null)
                {
                    _selectQuestion = new RelayCommand(
                            arg =>
                            {
                                if (_isStarted)
                                {
                                    SelectedQuestion = _model.QuestionWithId((sbyte)arg);
                                    onPropertyChanged(nameof(ifQuestionSelected), nameof(SelectedQuestion));
                                    _changesMade = true;
                                }
                                else
                                {
                                    MessageBox.Show("Quiz nie został uruchomiony. Naciśnij 'START'.");
                                }
                            },
                            arg => true);
                }
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
                            var result = (!_changesMade) ? MessageBoxResult.OK : MessageBox.Show("Wszelkie rozwiązania zostaną utracone.","Czy na pewno chcesz wrócić do Menu?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            if(result == MessageBoxResult.OK )
                                _viewModelChanger.CurrentViewModel = new QuizListViewModel(_viewModelChanger); },
                        arg => true);
                return _returnToMenu;
            }
        }

        public string QuizTimer => _timeElapsed.ToString(@"mm\:ss");

        private ICommand? _start = null;
        
        public ICommand Start
        {
            get
            {
                if (_start == null)
                    _start = new RelayCommand(
                        arg =>
                        {
                            _isStarted = true;
                            _timeElapsed = TimeSpan.Zero;
                            _timer.Change(0, 1000);
                        },
                        arg => !_isStarted
                    );
                return _start;
            }
        }

        private void UpdateTimer(object state)
        {
            _timeElapsed = _timeElapsed.Add(TimeSpan.FromSeconds(1));
            onPropertyChanged(nameof(QuizTimer));
        }

        private ICommand? _sprawdz;

        public ICommand Sprawdz
        {
            get
            {
                if (_sprawdz == null)
                    _sprawdz = new RelayCommand(
                        arg =>
                        {
                            _timer.Change(Timeout.Infinite, Timeout.Infinite);

                            int score = 0;
                            foreach (var question in Questions)
                            {
                                if (question.UserAnswers != null)
                                {
                                    if (question.UserAnswers.SequenceEqual(Question.ConvertAnwsersToTable(question.Right_anwser)))
                                    {
                                        score++;
                                    }
                                }else
                                {
                                    MessageBox.Show("Odpowiedzi lub poprawna odpowiedź są niezdefiniowane dla pytania.", "Błąd");
                                }
                            }

                            MessageBox.Show($"Your score: {score}/{Questions.Count}\nTime: {QuizTimer}", "Twoje wyniki");
                        },
                        arg => _isStarted
                        );
                return (_sprawdz);
            }
        }
    }
}
