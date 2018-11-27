# Valider brugerinput og sikr din applikation mod uautoriserede brugere
## valider formular post-data
Brug relevante DataAnnotations attributter på properties i modellen.
- `[Required]`
  - dette felt skal udfyldes for at modellen kan valideres
- `[MinLength(5)]`
  - Der skal angives mindst 5 tegn for at feltet kan validere.
- `[MaxLength(100)]`
  - Der kan angives maksimalt 100 tegn. Ved flere kan feltet ikke valideres.
- `[StringLength(100, MinimumLength = 5)]`
  - som de to ovenstående, bare skrevet sammen i en attribut.
- man kan tilføje ErrorMessage til attributterne til at angive brugerdefinerede fejlbeskeder:
  - `[StringLength(100, MinimumLength = 5), ErrorMessage = "Hey, du skal skrive et navn på mellem 5 og 100 tegn!"]`
- `[DataType(DataType.Password)]`
  - Fortæller at datatypen i propertyen er et password.
- `[Display(Name = "Email-adresse")]`
  - Fortæller frameworket at det skal bruge denne værdi til brugeren i viewet, i stedet for navnet på propertyet.

### Serverside
  
I pagemodel-filens OnPostAsync() metode tilføjes:
```C#
if(!ModelState.IsValid){
  return Page();
}
```
Ved fejlindtastninger som ikke kan valideres, sørger frameworket for at returnere brugeren til samme side som han kom fra, og udfylde de felter han havde udfyldt. Så er det op til brugeren at rette eventuelle fejl.

### Clientside
For at brugeren af siden kan få afvide hvad der gik galt under valideringen, kan man tilføje følgende til viewet med formularen:
```html
@if(!ModelState.IsValid){
<div class="css">
  <h3 class="h3-css">Valideringsfejl</h3>
  <div asp-validation-summary="All"></div>
  <hr />
</div>
}
```

På grund af `@if(!ModelState.IsValid)` bliver fejlmeddelelsen kun vist hvis der rent faktisk er valideringsfejl.

Ovenstående viser alle fejl samlet, men man kan også tilføje en fejlmeddelelse til hvert felt hvor der er et problem:
```html
<span asp-validation-for="Property.Field" ></span>
```
På denne måde komme fejlmeddelelsen tættere på selve validerings problemet. (man kan også gøre begge dele)

## Sikr dine sider mod uautoriseret adgang
### Cookie authentication
- i `Startup.cs`'s Configure() metode tilføjes `app.UseAuthentication();`. Den skal stå over mvc og staticfiles.
- i `Startup.cs`'s ConfigureServices() metode tilføjes:
```C#
  services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
```
- tilføj `[Authorize]` til den klasse der skal kræve autentificering. (typisk en klasse der kan udføre CRUD funktionalitet)

Hvis brugeren ikke er autentificeret, vil frameworket redirect til `{host}/Account/Login`.

Hvis man vil tilføje sikkerhed til en hel mappe, og ikke kun en klasse, kan man tilføje følgende til `Startup.cs`'s ConfigureServices() metode:
```C#
public void ConfigureServices(IServiceCollection services)
{
  services.AddMvc()
    .AddRazorPagesOptions(options =>
    {
      options.Conventions.AuthorizeFolder("/Admin");
      options.Conventions.AuthorizeFolder("/Account");
      // en undtagelse til reglen ovenover, så man kan tilgå login siden.
      options.Conventions.AllowAnonymousToPage("/Account/Login"); 
    })
// kode udeladt
}
```
Man behøver derfor ikke længere at tilføje attributten `[Authorize]` på klasser der ligger i mapperne `/Admin` og `/Account`.

Se eksempel på et meget enkel login der bruger RazorPages (og som ikke er sikker) [Kap 6, afsnit 4](https://www.lynda.com/ASP-NET-tutorials/Implementing-basic-cookie-based-authentication/630622/679613-4.html)
