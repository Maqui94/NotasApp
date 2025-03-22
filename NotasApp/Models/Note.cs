namespace NotasApp.Models;

internal class Note
{
    public int Id { get; set; }
    public string Filename { get; set; }
    public string Title { get; set; }
    public string Text {  get; set; }
    public DateTime Date { get; set; }
}