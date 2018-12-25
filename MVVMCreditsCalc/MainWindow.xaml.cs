using System.Windows;
using System.Windows.Controls;
using MVVMCreditsCalc.ViewModel;

namespace MVVMCreditsCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application app=Application.Current;
            app.Shutdown();
        }
    }
}