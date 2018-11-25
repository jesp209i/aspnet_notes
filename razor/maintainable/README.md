# Byg vedligeholdbare applikationer
## Page model
- tilføjes via "New Item" context-menuen og vælg derefter "Razor Page".
- Visual Studio tilføjer to filer til din solution
  - Hvis du kaldte din page for `Hest.cshtml`, får du også filen `Hest.cshtml.cs`, som indeholder klassen `HestModel`, som nedarver fra `PageModel`.
  - I dit view er din model automatisk blevet tilføjet ved hjælp af linjen `@model HestModel`, så nu er alt logikken i din HestModel klasse tilgængelig fra `@Model` i view'et.
  - Tilføj en værdi til en property i klassen ved at bruge attributten `[FromRoute]`, så bliver den populatet med data fra url'en.
```C#
[FromRoute]
public long? Id {get; set;}
``` 

_Kapitel 4, afsnit 2_

## Dependency Injection i views og pages
registrer dine dependencies i filen `Startup.cs`, metoden `ConfigureServices(...)`.
Brug en passende af følgende metoder:
- `services.AddTransient<IDinService, DinService>()`
  - starter en ny instans hver gang `DinService` forespørges et sted i applikationen (også selvom samme service bruges flere gange under en web-request).
- `services.AddSingleton<IDinService, DinService>()`
  - starter en ny instans første gang og bruger den samme hver gang. Også til efterfølgende forespørgsler.
- `services.AddScoped<IDinService, DinService>>()`
  - starter en ny instans og bruger samme instans til alle kald under samme web-request.

I de views og/eller pages hvor du skal bruge servicen tilføjer du øverst
```
@page // øverste linje
@inject IDinService DinService
```
Hvis servicen skal bruges overalt i din app, kan linjen `@inject IDinService DinService` tilføjes til `_ViewImports.cshtml`
