using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreator.ViewModels
{
    using Model;
    using QuizCreator.DAL.Entities;
    using QuizCreator.ViewModels.Navigation;
    using System.Collections.ObjectModel;
    using System.Windows;

    class QuizEditViewModel : BaseViewModelClasses.ViewModel
    {
        public string QuizName { get; set; }
        private Model _model = new Model();
        private ViewModelChanger _viewModelChanger;

        private ObservableCollection<Question> _questions = new ObservableCollection<Question>();
        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }

        public QuizEditViewModel(ViewModelChanger viewModelChanger)
        {
            _viewModelChanger = viewModelChanger;
            QuizName = Model.CurrentQuiz().Name;
            _questions = _model.Questions;
        }

    }
}
