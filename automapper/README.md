# Automapper
Kopier nemt properties værdier mellem klasser, hvor properties har samme navn.

Start med at hente AutoMapper NuGet pakken.

## Konfigurer AutoMapper
Følgende kan tilføjes til `Startup.cs` i Configure()-metoden:
```c#
AutoMapper.Mapper.Initialize(cfg =>
{
  cfg.CreateMap<KildeKlasse,DestinationsKlasse>();
});
```
Du kan tilføje så mange mappings som du vil.

## brug AutoMapper
Du bruger en mapping ved at kalde koden:
```c#
var destinationsInstans = Mapper.Map<destinationsKlasse>(kildeInstans);
```