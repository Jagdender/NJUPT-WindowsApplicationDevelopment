using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WindowsApplicationDevelop.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsApplicationDevelop.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [INotifyPropertyChanged]
    public sealed partial class NoteDetail : Page
    {
        private string _titleCopy;
        private string _contentCopy;
        public NoteItem Item { get; set; }

        [ObservableProperty]
        private bool _isEditing;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Item = e.Parameter as NoteItem;
        }

        public NoteDetail()
        {
            this.InitializeComponent();
        }

        private void Back(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            IsEditing = false;
            Item.Content = _contentCopy;
            Item.Title = _titleCopy;

            MainWindow.NavigateTo<NoteList>();
        }

        private void Edit(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            IsEditing = true;

            _titleCopy = Item.Title;
            _contentCopy = Item.Content;
        }

        private async void Save(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            IsEditing = false;
            _titleCopy = Item.Title;
            _contentCopy = Item.Content;

            await NoteList.SaveAsync();
        }

        private void Cancel(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            IsEditing = false;
            Item.Content = _contentCopy;
            Item.Title = _titleCopy;
        }
    }
}
