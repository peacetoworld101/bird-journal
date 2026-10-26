using System.Numerics;

namespace BirdJournal.Models;

public class DateEntry
{
    public DateOnly Date { get; }
    public int? TrueEntryNo { get; }
    public int? DateEntryNo { get; }

    public DateEntry(DateOnly date, int trueEntryNo, int dateEntryNo = -1)
    {
        Date = date;
        TrueEntryNo = trueEntryNo;
        DateEntryNo = dateEntryNo > -1 ? dateEntryNo : GetDateEntry();
    }

    public DateEntry(string dateEntry)
    {
        int indexOfSlash = dateEntry.IndexOf('/');
        if (indexOfSlash == -1)
        {
            throw new Exception("DateEntry(string dateEntry) must have a slash between the date and date entry number.");
        }
        Date = DateOnly.Parse(dateEntry.Substring(0, indexOfSlash));
        DateEntryNo = int.Parse(dateEntry.Substring(indexOfSlash));
    }
    private int? GetDateEntry()
    {
        // define. depends on how we're storing the data
        
        return DateEntryNo;
    }

    private int? GetTrueEntryNo()
    {
        // TODO
        return TrueEntryNo;
    }

    public override string ToString() 
    {
        return String.Concat(Date, '/', DateEntryNo);
    }
}