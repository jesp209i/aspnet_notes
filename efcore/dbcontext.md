# DbContext klassen
Der er lidt forskellige meninger om hvor din context fil skal ligge i projektet. Flere kilder siger at context skal ligge sammen med Entity-klasserne. Både `root\Entities` eller `root\Models` kunne være muligheder.

## Opsætning
Din context-fil skal arve DbContext, så du får adgang til metoderne derfra, og entities lægges i properties:
```c#
class MinContext : DbContext
{
public DbSet<EntityKlasse> Entities { get; set; }
public DbSet<EkstraEntityKlasse> EkstraEntities { get; set; }
}
```
Hvis Klassen `EkstraEntityKlasse` indeholder en navigation property til `EntityKlasse`, behøves `EntityKlasse` ikke at få en `DbSet<>`, da klasses bliver fundet alligevel.
```c#
public class EkstraEntityKlasse
{
  public int Id { get; set; }
  public string Name { get; set; }
  public EntityKlasse EntityKlasse { get; set; } // navigation property til EntityKlasse
}
```
### Dependency injection
Registrer dependency'en i `Startup.ConfigureServices()`:
```c#
  services.AddDbContext<MinContext>(options => options.UseSqlServer("forbindelses-streng-her!!"));
```
og tilføj en constructor til `MinContext`-klassen efter følgende struktur:
```c#
class MinContext : DbContext
{
  public MinContext(DbContextOptions<MinContext> options) : base(options)
  {
  // der behøver ikke være noget kode her.
  }
// udelad kode
}
```

Nu er `MinContext` klar til at blive constructor injected i controller-klasser eller andre steder det er nødvendigt.

### Sikre sig at databasen er oprettet
For at sikre sig at databasen er klar og oprettet når man kører sin kode, kan man udføre dette lille trick:
```c#
class MinContext : DbContext
{
  public MinContext(DbContextOptions<MinContext> options) : base(options)
  {
    Database.EnsureCreated(); // sikrer at databasen bliver oprettet runtime, hvis der endnu ikke er nogen
  }
// udelad kode
}
```

Bruger du migrations til din database, kan du skrive `Database.Migrate();` i stedet. På denne måde bliver klargjorte, men ikke udførte, migrationer kørt op i databasen.


kilder: https://docs.microsoft.com/da-dk/ef/core/miscellaneous/configuring-dbcontext
