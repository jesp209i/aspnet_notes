# Output formatter
Som standard er giver et ASP.net core webapi kun json output. Man kan tillade andre output formater ved at configurere en outputformatter.

## XML
For at kunne bruge en XML-outputformatter skal følgende gøres:

I Startup.cs, ConfigureServices()-metoden, tilføles `AddMvcOptions(...)` til `services.AddMvc()`:

```c#
public void ConfigureServices(IServiceCollection services)
{
  services.AddMvc()
    .AddMvcOptions(o=> o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
}
```

Hvis du ikke gør andet i dit projekt, kan du nu lave requests som beder om at få svar i xml via `accept`-headeren
