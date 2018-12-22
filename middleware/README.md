# Middleware
_kilde: [Lynda: ASP.NET Core : Middleware](https://www.lynda.com/ASP-NET-tutorials/ASP-NET-Core-Middleware/647683-2.html)_

- Pipelinen med middleware bliver udført i hvert request til serveren.
- rækkefølgen på middleware har betydning for udførelsen (response)
  - Middlewaren bliver udført oppefra og ned
- __Placering:__ kan placeres i en mappe der hedder "Middleware" i roden af projektet, men dette er ikke et krav
- [Mere om tilpasning af Middleware](custom.md)

## Typer af kald til middleware
### app.Use()
Sætter middleware ind i http-pipelinen. Middlewaren giver kontrollen tilbage til den næste i pipelinen.

_Execute this delegate and proceed to next in the pipeline._

- app.Use...()
  - UseStaticFiles() 
  - UseMvc()
  - Andet indbygget i frameworket
- app.Use(async (context, next) => {...})
- app.UseMiddleware()
- app.UseMiddleware\<ExternKlasse>()

### app.Run()
Fungerer som afsluttende (terminating) middleware, og overgiver ikke kontrollen tilbage til andet middleware.

Hvis der forekommer mere kode efter en "Run()", vil det altså ikke blive udført.

_executes this delegate and terminate processing._

- app.Run(async (context) => {...})
  
### app.Map()  
Bruges til at omdirigere bestemte kald til at udføre en anden metode. Denne type kald, returnerer ikke kontrollen tilbage til pipelinen.

Betingelsene for at denne kaldes kan både være en bestem sti, eller andre forhold i requestet.

_conditionally execute a method and does not return to the pipeline._

- app.Map("/ny/sti", a => a.Run(async context => {...}))
- app.MapWhen(context => {...})
