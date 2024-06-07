using CommunityToolkit.Mvvm.ComponentModel;

namespace WindowsApplicationDevelop.Models
{
    public sealed partial class NoteItem(string title, string content) : ObservableObject
    {
        [ObservableProperty]
        private string _title = title;

        [ObservableProperty]
        private string _content = content;
    }
}
