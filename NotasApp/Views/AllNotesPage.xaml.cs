namespace NotasApp.Views;

public partial class AllNotesPage : ContentPage
{
    public AllNotesPage()
    {
        InitializeComponent();
        BindingContext = new Models.AllNotes();
    }
    protected override void OnAppearing()
    {
        ((Models.AllNotes)BindingContext).LoadNotes();
    }
    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NotesPage));
    }
    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var note = e.CurrentSelection.FirstOrDefault() as Models.Note;
        if (note == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(NotesPage)}?{nameof(NotesPage.ItemId)}={note.Filename}");
        notesCollection.SelectedItem = null;
    }
}