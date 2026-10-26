using BirdJournal.Models;

namespace BirdJournal;

public static class Mapper
{
    public static JournalEntryDto ToDto(this JournalEntry entry)
    {
        return new(
            entry.EntryNumber,
            entry.Date,
            entry.DateEntryNumber,
            entry.Notes,
            [.. entry.Birds
                .Select(b => new BirdRecordDto(
                    b.Bird,
                    b.DayCount))]
        );
    }

    public static JournalEntry ToEntity(this CreateJournalEntryDto dto, int totalEntryCount, int dateEntryCount)
    {
        return new()
        {
            Date = dto.Date,
            EntryNumber = totalEntryCount, 
            DateEntryNumber = dateEntryCount, 
            Notes = dto.Notes,
            Birds = dto.Birds
            .Select(b => b.ToEntity())
            .ToList()
        };
    }

    /*public static JournalEntry ToEntity(this UpdateJournalEntryDto dto)
    {
        return new() {};
    }*/

    public static BirdRecord ToEntity(this CreateBirdRecordDto dto)
    {
        return new()
        {
            Bird = dto.Bird,
            DayCount = dto.DayCount,
        };
    }
}