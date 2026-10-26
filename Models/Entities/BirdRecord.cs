namespace BirdJournal.Models;

public class BirdRecord()
{
    public int BirdRecordId { get; set; }
    public int JournalEntryId { get; set; } // foreign key
    public string Bird { get; set; } = "";
    public int DayCount { get; set; }
    public JournalEntry JournalEntry { get; set; } = null!;

}