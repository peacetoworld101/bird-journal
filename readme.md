# BirdJournal Web API
A simple web api where you can store and manage entries to a bird journal to keep track of the times you went birding and what birds you saw.

### To run locally:
1. Change connectionString to point to localSqlDatabase
2. Update the database by typing into the commandline `dotnet ef database update`
3. Run using `dotnet run -lp {lp}`, where launch profile (lp) can be http, https, or container

### To connect to an Azure Sql Database and run using a container:
1. Create the database in Azure
2. Update the database by typing into the commandline `dotnet ef database update`
3. Add a .env file with the following contents: `ConnectionStrings__AzureSqlDatabase={YourConnectionStringHere}`
4. Run `docker build -f Dockerfile -t birdjournal:standalone .`
5. Run `docker run --rm --name birdjournal --env-file .env -p 8080:8080 birdjournal:standalone`

### How to test and send HTTP requests:
1. Open BirdJournal.http
2. Click on `Send Request` to send requests, or write your own requests

### Supported Operations:
`GET {Uri}/openapi/v1.json` Get OpenAPI documentation for the BirdJournal Web API

`GET {Uri}/journalEntries` Get full list of bird journal entries

`GET {Uri}/journalEntries/entry/{entryNumber}` Get single bird journal entry by entryNumber

`GET {Uri}/journalEntries/date/{date}` Get list of bird journal entries by date

`GET {Uri}/journalEntries/date/{date}/{dateEntryNumber}` Get single bird journal entry by date and dateEntryNumber

`POST {Uri}/journalEntries/` Create bird journal entry. Example payload:
```
{
  "Date": "2026-10-26",
  "Notes": "Hello world, this is my first entry into my bird journal",
  "Birds": [
    {
        "bird": "red_tailed_hawk",
        "dayCount": 1
    },
    {
        "bird": "wood_duck",
        "dayCount": 15
    }
  ]
}
```

`PUT {Uri}/journalEntries/entry/{entryNumber}` Update an existing bird journal entry by replacing the entry's notes and adding additional bird sightings. Example payload:
```
{
  "Birds": [
    {
        "bird": "eagle",
        "dayCount": 1
    }
  ]
}
```

`DELETE {Uri}/journalEntries/entry/{entryNumber}` Delete an existing bird journal entry