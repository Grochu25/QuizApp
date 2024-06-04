using System.Windows;
using System.Windows.Controls;

namespace QuizCreator.Views
{
    /// <summary>
    /// Logika interakcji dla klasy QuizListWindow.xaml
    /// </summary>
    public partial class QuizListView : UserControl
    {
        public QuizListView()
        {
            InitializeComponent();
        }

        public void CreateQuiz(object sender, RoutedEventArgs args)
        {
            var quizCreationDialog = new QuizCreationDialog()
            {
                DataContext = this.DataContext
            };
            quizCreationDialog.ShowDialog();
        }
    }
}
