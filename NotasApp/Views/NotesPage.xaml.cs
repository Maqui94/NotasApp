using System.Threading.Tasks;

namespace NotasApp.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotesPage : ContentPage
{
	public string ItemId
	{
		set { LoadNote(value); }
	}
	public NotesPage()
	{
		InitializeComponent();
		string appDataPath = FileSystem.AppDataDirectory;
		string randomFileName= $"{Path.GetRandomFileName()}.notes.txt";
        BindingContext = new Models.Note
        {
            Filename = Path.Combine(appDataPath, randomFileName),
            Date = DateTime.Now,
            Title = "",
            Text = ""
        };
    }
private async void SaveButton_Clicked(object sender, EventArgs e)
	{
        if (BindingContext is Models.Note note)
        {
            var contenido = $"{note.Title}\n===\n{TextEditor.Text}";
            File.WriteAllText(note.Filename, contenido);
        }
        await Shell.Current.GoToAsync("..");
	}
	private async void DeleteButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Note note)
		{
			if (File.Exists(note.Filename))
				File.Delete(note.Filename);
		}
		await Shell.Current.GoToAsync("..");
	}
	private void LoadNote(String fileName)
	{
        var noteModel = new Models.Note
        {
            Filename = fileName
        };

        if (File.Exists(fileName))
        {
            string[] contenido = File.ReadAllText(fileName).Split("===\n", 2);
            noteModel.Title = contenido.Length > 1 ? contenido[0].Trim() : "Sin título";
            noteModel.Text = contenido.Length > 1 ? contenido[1] : contenido[0];
            noteModel.Date = File.GetCreationTime(fileName);
        }

        BindingContext = noteModel;

    }
}