using QuizCreator.ViewModels;
using QuizCreator.ViewModels.Navigation;
using System.Windows;

namespace QuizCreator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ViewModelChanger viewModelChanger = new ViewModelChanger();
            viewModelChanger.CurrentViewModel = new QuizListViewModel(viewModelChanger);

            new CentralWindow()
            {
                DataContext = new CentralViewModel(viewModelChanger)
            }.Show();

            base.OnStartup(e);
        }
    }

}
