using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizCreator.ViewModels.BaseViewModelClasses;

namespace QuizCreator.ViewModels.Navigation
{
    class ViewModelChanger
    {
        public event Action CurrentViewModelChanged;

        private ViewModel _currentViewModel;

        public ViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChange();
            }
        }

        private void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
