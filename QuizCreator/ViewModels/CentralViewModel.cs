using QuizCreator.ViewModels.BaseViewModelClasses;
using QuizCreator.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreator.ViewModels
{
    class CentralViewModel : ViewModel
    {
        private readonly ViewModelChanger _viewModelChanger;
        public ViewModel CurrentViewModel => _viewModelChanger.CurrentViewModel;

        public CentralViewModel(ViewModelChanger viewModelChanger)
        {
            _viewModelChanger = viewModelChanger;

            _viewModelChanger.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            onPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
