using BirdJournal.Models;

namespace BirdJournal.Models;

public class JournalEntry
{
    public int JournalEntryId { get; set; }
    public DateOnly Date { get; set; }
    public int EntryNumber { get; set; }
    public int DateEntryNumber { get; set; }
    public string? Notes { get; set; }
    public ICollection<BirdRecord> Birds { get; set; } = [];
}