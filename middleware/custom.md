# Tilpasset middleware
- Et request går ned igennem middlewaren.
  - __inline:__ koden indtil `next()` udføres
  - __ekstern klasse:__ Koden i `Invoke()` metoden udføres, indtil `Next(context)` kaldes.
    - både `next()` og `Next(context)` henviser til RequestDelegaten
- Response går tilbage op igennem middleware.
  - Når response kommer tilbage til en middleware, hvor der er mere kode efter Next-delegaten, vil denne kode blive udført nu.

## Inline 
Middlewaren udføres inline direkte i `Startup.Configure()`. I første linje giver vi context og next som parameter til Use() metoden.
```C#
app.Use(async (context, next) =>
{
  var myTimer = System.Diagnostics.StopWatch.StartNew();
  logger.LogInformation($"====> Beginning request in {env.EnviromentName} enviroment");
  
  await next(); // kalder næste middleware
  logger.LogInformation($"====> Request completed in {myTimer.ElapsedMilliseconds}ms");
});

```

## Ekstern klasse
Skriv en middleware som ligger i en klasse. Den registreres på følgende måde i `Startup.cs`. 
```C#
public void Configure(...)
{
  app.UseMiddleware<EnviromentMiddleware>();
}
``` 
I klassen:
```C# 
public class EnviromentMiddleware
{
  public RequestDelegate Next { get; private set; } // Delegaten som videregiver kontroller over contexten
  public string EnviromentName { get; private set; }
  
  public EnviromentMiddleware(RequestDelegate next, IHostingEnviroment env)
  {
    Next = next;
    EnviromentName = env.EnviromentName;
  }
  // Denne metode kaldes fra pipelinen og overgiver kontrollen til next-delegaten.
  public async Task Invoke(HttpContext context) 
  {
    var myTimer = StopWatch.StartNew();
    context.Response.Headers.Add("X-HostingEnviromentName", new[] { EnviromentName });
    
    await Next(context); // Kalder næste middleware
    
    if (context.Response.ContentType != null && context.Response.ContentType.Contains("html"))
    {
      await context.Response.WriteAsync($"<p>From {EnviromentName} in {myTimer.ElapsedMilliseconds}ms</p>");
    }
  }
}
```
