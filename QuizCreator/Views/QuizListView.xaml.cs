using QuizCreator.ViewModels.BaseViewModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
