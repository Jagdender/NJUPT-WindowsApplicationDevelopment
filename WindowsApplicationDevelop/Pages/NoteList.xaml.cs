using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.Storage;
using WindowsApplicationDevelop.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsApplicationDevelop.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [INotifyPropertyChanged]
    public sealed partial class NoteList : Page
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

        public NoteList()
        {
            this.InitializeComponent();
        }

        private void Add(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            MainWindow.NavigateTo<NoteCreate>();
        }

        [ObservableProperty]
        private static ObservableCollection<NoteItem> _notes;

        private void SelectNote(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListView list || list.SelectedItem is null)
                return;

            MainWindow.NavigateTo<NoteDetail>(list.SelectedItem);
        }

        public static async Task<NoteItem[]> LoadAllAsync()
        {
            try
            {
                var file = await ApplicationData.Current.TemporaryFolder.GetFileAsync("Notes");
                var json = await FileIO.ReadTextAsync(file);
                return JsonSerializer.Deserialize<NoteItem[]>(json, _jsonSerializerOptions);
            }
            catch
            {
                return [];
            }
        }

        public static async Task SaveAsync()
        {
            var file = await ApplicationData.Current.TemporaryFolder.GetFileAsync("Notes");
            await FileIO.WriteTextAsync(
                file,
                JsonSerializer.Serialize(_notes.AsEnumerable(), _jsonSerializerOptions)
            );
        }

        public static async Task AddAsync(NoteItem item)
        {
            _notes.Add(item);
            var file = await ApplicationData.Current.TemporaryFolder.GetFileAsync("Notes");
            await FileIO.WriteTextAsync(
                file,
                JsonSerializer.Serialize(_notes.AsEnumerable(), _jsonSerializerOptions)
            );
        }

        public static async Task RemoveAsync(NoteItem item)
        {
            _notes.Remove(item);
            var file = await ApplicationData.Current.TemporaryFolder.GetFileAsync("Notes");
            await FileIO.WriteTextAsync(
                file,
                JsonSerializer.Serialize(_notes.AsEnumerable(), _jsonSerializerOptions)
            );
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var notes = await LoadAllAsync();
            if (notes.Length != 0)
            {
                Notes = new(notes);
                return;
            }

            NoteItem[] items = [new("Title", "Content")];
            Notes = new(items);
            var file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync("Notes");
            await FileIO.WriteTextAsync(
                file,
                JsonSerializer.Serialize(items, _jsonSerializerOptions)
            );
        }
    }
}
