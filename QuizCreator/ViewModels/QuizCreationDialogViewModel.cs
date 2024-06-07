using QuizCreator.ViewModels.BaseViewModelClasses;
using System.Windows.Input;

namespace QuizCreator.ViewModels
{
    class QuizCreationDialogViewModel : BaseViewModelClasses.ViewModel
    {
        public static bool DialogResult = false;
        public static string NewQuizName { get; set; } = "";

        public QuizCreationDialogViewModel()
        {
            DialogResult = false;
            NewQuizName = "";
        }

        ICommand? _approve = null;
        public ICommand Approve { 
            get {
                if (_approve == null)
                    _approve = new RelayCommand(
                        arg => { DialogResult = true; },
                        arg => NewQuizName.Length > 0
                        );
              return _approve; 
            } 
        }

        ICommand? _cancel = null;
        public ICommand Cancel
        {
            get
            {
                if (_cancel == null)
                    _cancel = new RelayCommand(
                        arg => { DialogResult = false; },
                        arg => true
                        );
                return _cancel;
            }
        }
    }
}
