using System.ComponentModel.DataAnnotations;

namespace BirdJournal.Models;

public record class JournalEntryDto (
    // [Required] is a modifier
    int EntryNumber,
    DateOnly Date,
    int DateEntryNumber,
    string? Notes,
    ICollection<BirdRecordDto>? Birds
);