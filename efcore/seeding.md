# Seeding af databasen
Denne side beskriver hvordan du kan fylde data i en tom database. Beskrivelsen kommer fra [PluralSight](https://app.pluralsight.com/player?course=asp-dotnet-core-api-building-first&author=kevin-dockx&name=asp-dotnet-core-api-building-first-m5&clip=6&mode=live).

Bemærk at denne fremgangsmåde skaber en slags udvidelse til din contextklasse.

1. Opret en extension klasse af din DbContext, som er statisk:
 `public static class DinKlasseExtensions`
2. Lav en metode efter følgende eksempel (se kommentarer): 
```c# {.line-numbers}
public static void EnsureSeedDataForContext(this DinContextKlasse context)
{  
  if (context.Cities.Any()) // Cities er her den collection du tester for at finde data
    {
      return; // hvis der findes data, afsluttes metoden, uden at seede yderligere data ind i databasen.
    }
  var cities = new List<City>()
  {
      new City()
      {
          Name = "New York City",
          Description = "The one with that big park.",
          PointsOfInterest = new List<PointsOfInterest>()
          {
              new PointsOfInterest()
              {
                  Name = "Central Park",
                  Description = "The most visited urban park in the United States."
              },
              new PointsOfInterest()
              {
                  Name = "Empire State Building",
                  Description = "A 102-story skyscraper located in Midtown Manhattan."
              },
          }
      },
      new City()
      {
          Name = "Antwerp",
          Description = "The one with the cathedral that was never really finished."
      },
      new City()
      {
          Name = "Paris",
          Description = "The one with the big tower."
      }
  };
  context.Cities.AddRange(cities); // cities + relationer tilføjes
  context.SaveChanges(); // men gemmes først til databasen her!
}
```

I din `Startup.cs` `Configure()`sørger du for at bruge din context klasse i signaturen, og kalde `EnsureSeedDataForContext()` i din pipeline:

```c#
public void Configure(
  IApplicationBuilder app, 
  IHostingEnvironment env, 
  ILoggerFactory loggerFactory, 
  CityInfoContext cityInfoContext) // læg mærke til mig!!
{
  loggerFactory.AddNLog();

  if (env.IsDevelopment())
  {
      app.UseDeveloperExceptionPage();
  }
  cityInfoContext.EnsureSeedDataForContext(); // læg mærke til mig!!

  app.UseStatusCodePages();

  app.UseMvc().UseMvcWithDefaultRoute();
}
```


