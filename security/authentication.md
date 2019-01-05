# Authentication
Bruge credentials til at identificere en bruger i systemet.
- brug [ssl](ssl.md)!

## Authentication typer (for API'er)
- App Authentication
  - Brug en secret til at identificere en app til dit API
  - Det er udvikleren, ikke brugeren, der bliver authenticated
  - `App key + Secret` er et typisk scenarie.
- Bruger Authentication
  - Tilgå API'et som bruger

## ASP.Net Identity
- simpelt system til at gemme bruger identiteter, roller og claims.
  - ikke brugbar til App Authentication
  - gør det nemt at bruge Cookie-baseret authentication (default)
  - grundlaget for både basal og Token authentication
  
### Identity
I `Startup.cs`service collection:
```c#
public void ConfigureServices(...)
{
  // kode udeladt
  // UserKlasse er din definerede klasse, som har nedarvet `:IdentityUser`
  // DBContextKlasse er den context hvor UserKlasse er registreret
  services.AddIdentity<UserKlasse,IdentityRole>() 
    .AddEntityFrameworkStores<DBContextKlasse>(); 

// Denne opsætning er brugbar ved rene API'er, som ikke har login-sider
// IdentityOptions kan også bruges til at sætte regler om Password
  services.Configure<IdentityOptions>(config => 
  {
    config.Cookies.ApplicationCookie.Events =
      new CookieAuthenticationEvents()
      {
        OnRedirectToLogin = (ctx) => // forhindrer redirects til login og sætter statuskoden til 401
        {
          if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
          {
            ctx.Response.StatusCode = 401;
          }
          return Task.CompletedTask;
        },
        OnAccessDenied = (ctx) => // forhindrer redirects til access denied og sætter statuskoden til 403
        {
          if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
          {
            ctx.Response.StatusCode = 403;
          }
          return Task.CompletedTask;
        }
      };
  });
}
```

i `Configure()` tilføjes `app.UseAuthentication();`.

Nu kan du dekorere controllere eller metoder i controllere med `[Authorize]`, som vil kræve at en bruger har identificeret sig til systemet, før de kan tilgå den ressource.

## Cookies
- opret `AuthController`
```c#
public class AuthController : Controller
{
  private DBContextKlasse _context;
  private SignInManager<UserKlasse> _signInMgr;
  private ILogger<AuthController> _logger;
  public AuthController(DBContextKlasse context, 
    SignInManager<UserKlasse> signInMgr, 
    ILogger<AuthController> logger)
  {
    _context = context;
    _signInMgr = signInMgr;
    _logger = logger;
  }
  
  [HttpPost("api/auth/login")]
  public async Task<IActionResult> Login([FromBody]CredentialModel model)
  {
    try 
    {
      var result = await _signInMgr.PasswordSignInAsync(model.UserName, model.Password, false, false)
      if (result.Succeded)
      {
        return Ok();
      }
    }
    catch (Exception ex)
    {
    // logning af fejl
      _logger.LogError($"Exception thrown while logging in: {ex}");
    }
    // af hensyn til sikkerhed fortæller man ikke hvad der gik galt, blot at login ikke lykkedes
    return BadRequest("Failed to login");
  }
}
```

CredentialModel-klasse:
```c#
public class CredentialModel
{
  [Required]
  public string UserName { get; set; }
  [Required]
  public string Password { get; set; }
}
```

## Token
Se `code/AuthController.cs` for hvordan en token forespørges og genereres. Se construktoren og metoden `CreateToken()`.

Brug JWT middelware til at verificere efterfølgende requests:
```c#
app.UseIdentity(); // brug den nye
app.UseJwtBearerAuthentication(new JwtBearerOptions()
{
  AutomaticAuthenticate = true,
  AutomaticChallenge = true,
  TokenValidationParameters = new TokenValidationParameters()
  {
    ValidIssuer = _config["Tokens:Issuer"],
    ValidAudience = _config["Tokens:Audience"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"])),
    ValidateLifeTime = true
  }
});

```
