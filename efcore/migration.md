# Migrationer
Når dine databaseklasser er gjort klar i Visual Studio, skal du åbne `Package Manager Console`.

1. lav din første migration: `Add-Migration CityInfoInitialMigration` //navnet til sidst vælger du selv...
2. VS arbejder noget tid. Der oprettes blandt andet en ny mappe "Migrations", som indeholder dine migrationer.
3. Hvis du ikke støder på nogen problemer kan du køre næste kommando i Package Manager Console: `Update-Database`.
- __Alternativ:__ I din DbContextklasse kan du rette `Database.EnsureCreated();` til `Database.Migrate();`, så sørger frameworket for at køre migrations hvis der er nogen nye

Hvid du har brug for at ændre på dine databaser, kan du bare gøre det. Du skal bare huske at køre en omgang `Add-Migration navn` så ændringerne kommer med op i din database.
