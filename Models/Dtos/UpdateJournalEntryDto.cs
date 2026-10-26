using System.ComponentModel.DataAnnotations;

namespace BirdJournal.Models;

public record class UpdateJournalEntryDto (
    // [Required] is a modifier
    string? Notes,
    ICollection<CreateBirdRecordDto>? Birds
);