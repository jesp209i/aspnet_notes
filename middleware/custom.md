# Tilpasset middleware
- Request går ned igennem middlewaren.
  - Koden i `Invoke()` metoden udføres, indtil `next()` eller `Next(context)` kaldes.
  - Requestet bliver sendt videre ved hjælp af Next-delagaten.
- Response går tilbage op igennem middleware.
  - Når response kommer tilbage til en middleware, hvor der er mere kode efter Next-delegaten, vil denne kode blive udført nu.

## Inline 
Middlewaren udføres inline direkte i `Startup.Configure()`. I første linje giver vi context og next som parameter til Use() metoden.
```C#
app.Use(async (context, next) =>
{
  var myTimer = System.Diagnostics.StopWatch.StartNew();
  logger.LogInformation($"====> Beginning request in {env.EnviromentName} enviroment");
  
  await next();
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
  public RequestDelegate Next { get; private set; }
  public string EnviromentName { get; private set; }
  public EnviromentMiddleware(RequestDelegate next, IHostingEnviroment env)
  {
    Next = next;
    EnviromentName = env.EnviromentName;
  }
  
  public async Task Invoke(HttpContext context)
  {
    var myTimer = StopWatch.StartNew();
    context.Response.Headers.Add("X-HostingEnviromentName", new[] { EnviromentName });
    
    await Next(context);
    
    if (cintext.Response.ContentType != null && context.Response.ContentType.Contains("html"))
    {
      await context.Response.WriteAsync($"<p>From {EnviromentName} in {myTimer.ElapsedMilliseconds}ms</p>");
    }
  }
}
```
