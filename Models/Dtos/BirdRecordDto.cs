using System.ComponentModel.DataAnnotations;

namespace BirdJournal.Models;

public record class BirdRecordDto(
    string? Bird,
    int DayCount
);