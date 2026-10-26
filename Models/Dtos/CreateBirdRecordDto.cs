using System.ComponentModel.DataAnnotations;

namespace BirdJournal.Models;

public record CreateBirdRecordDto (
    string Bird,
    int DayCount
);