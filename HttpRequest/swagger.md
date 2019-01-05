# Swagger
Indstil swagger i dit API:

følgende dependencies skal tilføjes:
- Swashbuckle.AspNetCore.Swagger
- Swashbuckle.AspNetCore.SwaggerGen
- Swashbuckle.AspNetCore.SwaggerUI: 

Til din `Startup.ConfigureServices()` tilføjes:
 ```c#
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
});
 ```
 
I din middleware pipeline, `Startup.Configure()`,  tilføjes:
```c#
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
```
Hvis API'et er korrekt opsat kan til tilgås fra `http://localhost:<port\>/swagger`.
 
Se desuden kilden for yderligere opsætning af Swagger.

## Fejlfinding
Hvis Swagger har besvær med at danne en pæn side, så undersøg om dine endpoints er pænt dekorerede med `[HttpVerbum]` så swagger kan se hvilken type request hvert endpoint kræver. 

Se de typiske verber under teksten om [Http request](request.md).

kilde: [Swagger/OpenAPI](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio)
