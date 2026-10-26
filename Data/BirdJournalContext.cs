using BirdJournal.Models;
using Microsoft.EntityFrameworkCore;

namespace BirdJournal.Data;

public class BirdJournalContext(DbContextOptions<BirdJournalContext> options) 
    : DbContext(options)
{
    //public DbSet<Bird> Birds => Set<Bird>();

    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<BirdRecord> BirdRecords => Set<BirdRecord>();
}