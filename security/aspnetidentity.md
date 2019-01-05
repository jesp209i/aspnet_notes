# ASP.NET Core Identity
Identity indeholder disse komponenter:
- Password hashing
- bruger og kodeords validering
- reset af kodeord og email confirmation
- bruger lockout
- multi-faktor godkendelse
- eksterne identities

Identity data består af:
- identifikation
- personlig information
- identitys rolle i organisationen eller proffession.

## Opsætning af Identity og EFC
- I `Startup.Configure()` tilføjes `app.UseAuthentication()`.
- i `Startup.ConfigureServices()` tilføjes:
```c#
services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>();
```
- opret en DbContext til Identity:
  - samme fremgangsmåde som alle andre DbContext, men klassen skal arve fra `IdentityDbContext<T>` hvor T er den userklasse du bruger. (I dette eksempel er T `IdentityUser`).
  - husk at tilføje den til service collection.
- `UserManager<T>` og `SignInManager<T>` er automatisk tilgængelige igennem dependency injection
  

# Roles
- identity role
- permission role

En rolle kan give adgang til en gruppe af claims, som yderligere giver adgang til forskellige dele af systemet. 
