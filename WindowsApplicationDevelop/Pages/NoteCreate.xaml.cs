using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WindowsApplicationDevelop.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsApplicationDevelop.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [INotifyPropertyChanged]
    public sealed partial class NoteCreate : Page
    {
        public NoteCreate()
        {
            this.InitializeComponent();
        }

        [ObservableProperty]
        private NoteItem _item = new(string.Empty, string.Empty);

        public async void Save(object sender, RoutedEventArgs e)
        {
            await NoteList.AddAsync(Item);
            MainWindow.NavigateTo<NoteList>();
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateTo<NoteList>();
        }
    }
}
