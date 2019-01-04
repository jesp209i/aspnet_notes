# Authorization
Verificer at en identity har rettighederne til en specifik ressource

## Policies
Som inline-delegate tilføjes i service collection:
```c#
services.AddAuthorization(cfg =>
{
// "SuperUsers" er navnet på din policy
// "p=> p..." lambdaet er hvordan policyen skal overholdes.
  cfg.AddPolicy("SuperUsers", p => p.RequireClaim("SuperUser", "True"));
});
```

I dine Controlleren kan kun nu dekorere relevante metoder med `[Authorize(Policy = "SuperUsers")]` hvis de skal overholde en policy.

## Lav policy i klasser OOPstyle
- opret en mappe med navnen `Security` i roden af projektet.
- opret en klasse med et relevant navn i mappen eksempelvis `MinimumAgeRequirement`
  - bemærk at man kan dele denne op i en "requirement" klasse og en "handler" klasse, men verificerer at requirements overholdes.
  - "handleren" skal arve `AuthorizationsHandler<T>` og implementere `HandleRequirementAsync()`
  - bemærk at `<T>` er `requirement` klassen.

```c#
public class MinimumAgeRequirement : AuthorizationsHandler<MinimumAgeRequirement>, IAuthorizationRequirement
{
  private int _minimumAge;
  
  public MinimumAgeRequirement(int minimumAge)
  {
    _minimumAge = minimumage;
  }
  
  protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context, 
      MinimumAgeRequirement requirement)
  {
    var dobValue = context.User.FindFirstValue(ClaimTypes.DateOfBirth);
    var dob = DateTime.Parse(dobValue);
    if (dob.Date < DateTime.Now.Date.AddYears(-_minimumAge))
    {
      context.Succeed(requirement);
    }
    return Task.CompletedTask;
  }
}
```

i `Startup.ConfigureServices()` skal du registrere denne policy på følgende måde:

```c#
services.AddAuthorization(cfg =>
{
  cfg.AddPolicy("AgeLimit", policy => 
  {
    policy.AddRequirements(new MinimumAgeRequirement(21));
  }
});
```

## Claims
Et claim er en enkelt information af en Identity. 
