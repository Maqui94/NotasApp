using System.Collections.ObjectModel;

namespace NotasApp.Models;

internal class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
    public AllNotes() =>
        LoadNotes();
    public void LoadNotes()
    {
        Notes.Clear();
        string appDataPath = FileSystem.AppDataDirectory;

        IEnumerable<Note> notes = Directory
            .EnumerateFiles(appDataPath, "*.notes.txt")
            .Select(filename =>
            {
                string fileContent = File.ReadAllText(filename);
                string[] partes = fileContent.Split("===\n", 2);

                return new Note
                {
                    Filename = filename,
                    Title = partes.Length > 1 ? partes[0].Trim() : "Sin título",
                    Text = partes.Length > 1 ? partes[1] : partes[0],
                    Date = File.GetLastWriteTime(filename)
                };
            })
            .OrderBy(note => note.Date);

        foreach (Note note in notes)
        {
            Notes.Add(note);
        }
    }
}
