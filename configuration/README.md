# Konfiguations filer
Tilføj en eller flere filer der hedder appSettings.json. __Developmemt__ eller __Production__ kan tilføjes til filenavnene så der er konfigurationsfiler der gælder i forskellige staging miljøer:

- appSettings.Production.json
- appSettings.Development.json

I `Startup.cs` tilføjes en constructor samt en statisk property:

```C#
public class Startup
  {
    public static IConfiguration Configuration { get; private set; }
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
...
}
```

ASP.Net Core 2.x sørger selv for at tage fat i appSettings-filerne.

For at tilgå et felt i `appSettings` skrives blot:
``` 
Startup.Configuration["sectionTwo:nextProperty"]

```
Da property'en er statisk kan den tilgås overalt i programmet.

Se følgende eksempel på en `appSettings.json`-fil:
```js
{
"sectionOne": {
    "propertyOne": "something",
    "propertyTwo": "somethingElse"
  }
"sectionTwo": {
    "nextProperty": "something something something dark side"
  }
}

```

Gem forbindelsesstrenge til databaser heri, og gerne andre indstillinger som ikke skal hardcodes ind i programmet.
