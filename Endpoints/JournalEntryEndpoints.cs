using BirdJournal.Data;
using BirdJournal.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BirdJournal.Endpoints;

public static class JournalEntryEndpoints
{
    public static RouteGroupBuilder MapJournalEntryEndpoints(this WebApplication app) {
        var group = app.MapGroup("journalEntries");

        group.MapGet("/", async (BirdJournalContext dbContext) =>
        {
            var entries = (await dbContext.JournalEntries
                .Include(e => e.Birds)
                .ToListAsync())
                    .Select(e => e.ToDto());

            return Results.Ok(entries);
        })
        .WithName("GetJournalEntries");


        group.MapGet("/entry/{entryNumber:int}", async (int entryNumber, BirdJournalContext dbContext) =>
        {
            // should be JEDto, not JE. verify
            var entry = await dbContext.JournalEntries
                .Where(e => e.EntryNumber == entryNumber)
                .Include(e => e.Birds)
                .SingleOrDefaultAsync();

            return entry is null ? Results.NotFound() : Results.Ok(entry.ToDto());
        })
        .WithName("GetJournalEntryByEntryNumber");

        group.MapGet("/date/{date}", async (DateOnly date, BirdJournalContext dbContext) =>
        {
            var entries = (await dbContext.JournalEntries
                .Where(e => e.Date == date)
                .Include(e => e.Birds)
                .ToListAsync())
                    .Select(e => e.ToDto());

            return entries is null ? Results.NotFound() : Results.Ok(entries);
        })
        .WithName("GetJournalEntriesByDate");

        group.MapGet("/date/{date}/{dateEntryNumber:int}", async (DateOnly date, int dateEntryNumber, BirdJournalContext dbContext) => {
            var entry = await dbContext.JournalEntries
                .Include(e => e.Birds)
                .SingleOrDefaultAsync(e =>
                    e.Date == date &&
                    e.DateEntryNumber == dateEntryNumber);
            
            return entry is null ? Results.NotFound() : Results.Ok(entry.ToDto());
        })
        .WithName("GetJournalEntryByDateEntry");

        group.MapPost("/", async (CreateJournalEntryDto dto, BirdJournalContext dbContext) => {
            int totalEntryCount = (await dbContext.JournalEntries
                .CountAsync()) + 1;

            int dateEntryCount = (await dbContext.JournalEntries
                .CountAsync(e => e.Date == dto.Date)) + 1;
            
            // Note: entityFramework takes care of generating the keys and the JournalEntry pointer of BirdRecord
            JournalEntry entry = dto.ToEntity(totalEntryCount, dateEntryCount);

            dbContext.JournalEntries.Add(entry);
            await dbContext.SaveChangesAsync();

            return Results.Created(
                $"/journalEntries/entry/{entry.EntryNumber}",
                entry.ToDto()
            );
        })
        .WithName("CreateJournalEntry");

        group.MapPut("/entry/{entryNumber}", async (int entryNumber, UpdateJournalEntryDto dto, BirdJournalContext dbContext) => {
            JournalEntry? existingEntry = await dbContext.JournalEntries
                .Include(e => e.Birds)
                .SingleOrDefaultAsync(e => e.EntryNumber == entryNumber);

            if (existingEntry is null) return Results.NotFound();

            if (dto.Notes is not null)
            {
                existingEntry.Notes = dto.Notes;
            }

            if (dto.Birds is not null)
            {
                foreach (CreateBirdRecordDto birdDto in dto.Birds)
                {
                    existingEntry.Birds.Add(birdDto.ToEntity());
                }    
            }

            await dbContext.SaveChangesAsync();

            return Results.Ok(existingEntry.ToDto());
        })
        .WithName("UpdateJournalEntryByEntryNumber");

        group.MapDelete("/entry/{entryNumber}", async (int entryNumber, BirdJournalContext dbContext) => 
        {
            var entry = await dbContext.JournalEntries
                .SingleOrDefaultAsync(e => e.EntryNumber == entryNumber);

            if (entry is null)
                return Results.NotFound();

            dbContext.JournalEntries.Remove(entry);

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteJournalEntryByEntryNumber");

        return group;
            
    }
}