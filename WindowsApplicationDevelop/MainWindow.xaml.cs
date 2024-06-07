using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WindowsApplicationDevelop.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsApplicationDevelop
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static readonly Frame ContentFrame = new();

        public MainWindow()
        {
            this.InitializeComponent();
            Navigation.Content = ContentFrame;
            ContentFrame.Navigate(typeof(NoteList));
        }

        public static void NavigateTo<T>()
        {
            ContentFrame.Navigate(typeof(T));
        }

        public static void NavigateTo<T>(object parameter)
        {
            ContentFrame.Navigate(typeof(T), parameter);
        }
    }
}
