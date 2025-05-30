using System.Windows;
using System.Windows.Controls;

namespace Reactor.MVVM.View
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }

}