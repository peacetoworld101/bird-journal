using System.ComponentModel.DataAnnotations;

namespace BirdJournal.Models;

public record CreateJournalEntryDto (
    [Required]DateOnly Date,
    string? Notes,
    IReadOnlyCollection<CreateBirdRecordDto> Birds
);