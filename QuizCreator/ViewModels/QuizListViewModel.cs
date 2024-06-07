namespace QuizCreator.ViewModels
{
    using QuizCreator.DAL.Entities;
    using QuizCreator.Model;
    using QuizCreator.ViewModels.BaseViewModelClasses;
    using QuizCreator.ViewModels.Navigation;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
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

        private ICommand? _rozwiaz = null;
        public ICommand Rozwiaz
        {
            get
            {
                if (_rozwiaz == null)
                    _rozwiaz = new RelayCommand(
                        arg =>
                        {
                            if (arg == null)
                            {
                                Debug.WriteLine("Argument arg jest null");
                                return;
                            }

                            Model.CurrentQuizId = (sbyte) arg;

                            // Sprawdzenie null dla _viewModelChanger
                            if (_viewModelChanger == null)
                            {
                                Debug.WriteLine("_viewModelChanger jest null");
                                return;
                            }

                            _viewModelChanger.CurrentViewModel = new QuizAnwserViewModel(_viewModelChanger);
                        },
                        arg => true
                        );
                return _rozwiaz;
            }
        }

    }
}
