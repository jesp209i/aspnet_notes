# CORS
Cross Origin Resource Sharing

Registrer CORS i service containeren i `Startup.cs`:
```c#
public void ConfigureServices(IServiceCollection services)
{
  // udeladt kode
  services.AddCors();
}
```

Aktiver CORS i `Configure()` hvis du vil have CORS skal gælde globalt:
```c#
public void Configure(...)
{
  // udeladt kode
  app.UseCors(cfg =>
  {
    cfg.AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin();
  });
  app.UseMvc();
}
```
## Policies
Opret forskellige regler (policies) som du vælger hvornår der skal bruges.

```c#
public void ConfigureServices(IServiceCollection services)
{
  // udeladt kode
  services.AddCors(cfg =>
  {
    cfg.AddPolicy("regel", bldr =>
    {
      bldr.AllowAnyHeader()
          .AllowAnyMethod()
          .WithOrigins("http://localhost:44388"); // denne eksterne side må lave alle typer requests
    });
    cfg.AddPolicy("AnyGet", bldr =>
    {
      bldr.AllowAnyHeader()
          .WithMethods("GET") // alle må lave get-requests
          .AllowAnyOrigin();
    });
  });
}
```

Nu behøver du ikke have `app.UseCors()` i `Startup.Configure()` længere, men i din controller gør du følgende:
```C#
[EnableCors("AnyGet")] // alle "GETS" kan forspørges
[Route("api/[controller]")]
public class RandomController : BaseController
{

  [EnableCors("regel")] // forespørgsler der overholder "regel" må udføre denne Post action.
  [HttpPost]
  public IActionResult Post(...)
  {
  // kode udeladt
  }

}
```

[mere om CORS](https://docs.microsoft.com/da-dk/aspnet/core/security/cors?view=aspnetcore-2.2)
